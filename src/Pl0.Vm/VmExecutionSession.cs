using System.Globalization;
using System.Resources;
using Pl0.Core;

namespace Pl0.Vm;

// Diese Session ist die einzige Stelle, die Opcodes und OPR-Codes dekodiert.
// This session is the only place that decodes opcodes and OPR codes.
internal sealed class VmExecutionSession
{
    private const int RuntimeCode = 99;
    private const int InputEofCode = 98;
    private const int InputFormatCode = 97;
    private const int DivisionCode = 206;
    private static readonly int[] AllowedOprCodes = [0, 1, 2, 3, 4, 5, 6, 8, 9, 10, 11, 12, 13, 14, 15];

    private Instruction[] program;
    private readonly IPl0Io io;
    private readonly VirtualMachineOptions options;
    private readonly ResourceManager messages;
    private readonly CultureInfo culture;
    private readonly CancellationToken initializationToken;
    private readonly List<VmDiagnostic> diagnostics = [];
    private int[] stack = [];
    private int p;
    private int b;
    private int t;
    private int executedInstructions;
    private VmStepResult? terminalResult;

    private VmExecutionSession(Instruction[] program, IPl0Io io, VirtualMachineOptions options,
        ResourceManager messages, CultureInfo culture, CancellationToken initializationToken)
    {
        this.program = program;
        this.io = io;
        this.options = options;
        this.messages = messages;
        this.culture = culture;
        this.initializationToken = initializationToken;
    }

    internal VmState State { get; private set; } = new(0, 0, 0, [], null, 0);
    internal bool IsRunning => terminalResult is null;

    internal static VmExecutionSession Create(IReadOnlyList<Instruction>? source, IPl0Io io,
        VirtualMachineOptions options, CancellationToken cancellationToken)
    {
        ResourceManager messages = options.Messages ?? Pl0VmMessages.ResourceManager;
        CultureInfo culture = CultureInfo.GetCultureInfo(options.Language);
        IReadOnlyList<VmDiagnostic> optionDiagnostics = VirtualMachineOptionsValidator.Validate(options, messages, culture);
        var session = new VmExecutionSession([], io, options, messages, culture, cancellationToken);
        if (optionDiagnostics.Count > 0)
        {
            session.Complete(VmCompletionReason.InvalidConfiguration, optionDiagnostics, session.State);
            return session;
        }

        IReadOnlyList<VmDiagnostic> programDiagnostics = ValidateProgram(source, options, messages, culture);
        if (programDiagnostics.Count > 0)
        {
            session.Complete(VmCompletionReason.InvalidProgram, programDiagnostics, session.State);
            return session;
        }

        // Erst nach allen Grenzen werden Programm und Stack kopiert bzw. angelegt.
        // Program and stack are copied or allocated only after every boundary passed.
        session.program = source!.ToArray();
        session.stack = new int[options.StackSize + 1];
        session.p = 0;
        session.b = 1;
        session.t = 0;
        session.stack[1] = 0;
        session.stack[2] = 0;
        session.stack[3] = 0;
        session.State = session.CaptureState();
        return session;
    }

    internal VmStepResult ExecuteNext(CancellationToken stepToken = default)
    {
        if (terminalResult is not null)
            return terminalResult;

        if (initializationToken.IsCancellationRequested || stepToken.IsCancellationRequested)
            return Complete(VmCompletionReason.Cancelled,
                [VirtualMachineOptionsValidator.Cancelled(messages, culture)], State);

        if (executedInstructions >= options.InstructionBudget)
            return Complete(VmCompletionReason.InstructionBudgetExceeded,
                [VirtualMachineOptionsValidator.BudgetExceeded(messages, culture)], State);

        if (p < 0 || p >= program.Length)
            return Complete(VmCompletionReason.RuntimeFault,
                [LocalizedDiagnostic(RuntimeCode, "Vm_E99_IPOutOfRange",
                    "Befehlszeiger außerhalb des Bereichs: {0}.",
                    "Instruction pointer out of range: {0}.", p)], State);

        VmState stateBeforeDispatch = State;
        Instruction instruction = program[p++];
        executedInstructions++;

        VmCompletionReason? fault = Dispatch(instruction);
        if (fault.HasValue)
            return Complete(fault.Value, diagnostics, stateBeforeDispatch);

        State = CaptureState();
        if (p == 0)
            return Complete(VmCompletionReason.Halted, diagnostics, State);

        return new VmStepResult(State, VmStepStatus.Running, diagnostics,
            VmCompletionReason.Running, executedInstructions);
    }

    internal VmExecutionResult ToExecutionResult()
    {
        VmStepResult result = terminalResult ?? ExecuteNext();
        int safeTop = Math.Clamp(result.State.T, 0, result.State.Stack.Length == 0 ? 0 : result.State.Stack.Length - 1);
        int[] fullStack = result.State.Stack;
        int[] compact = fullStack.Length == 0 ? [] : fullStack[..(safeTop + 1)];
        return new VmExecutionResult(compact, safeTop, result.Diagnostics, result.Reason,
            result.ExecutedInstructions, result.State);
    }

    private static IReadOnlyList<VmDiagnostic> ValidateProgram(IReadOnlyList<Instruction>? program,
        VirtualMachineOptions options, ResourceManager messages, CultureInfo culture)
    {
        List<VmDiagnostic> result = [];
        if (program is null)
        {
            result.Add(VirtualMachineOptionsValidator.ProgramError("null", messages, culture));
            return result;
        }
        if (program.Count == 0)
        {
            result.Add(new VmDiagnostic(RuntimeCode, string.Format(culture,
                messages.GetString("Vm_E99_IPOutOfRange", culture) ??
                (culture.TwoLetterISOLanguageName == "de"
                    ? "Befehlszeiger außerhalb des Bereichs: {0}."
                    : "Instruction pointer out of range: {0}."), 0)));
            return result;
        }
        if (program.Count > options.MaximumProgramLength)
        {
            result.Add(VirtualMachineOptionsValidator.ProgramError(
                $"length {program.Count}; allowed 1..{options.MaximumProgramLength}", messages, culture));
            return result;
        }

        for (int index = 0; index < program.Count; index++)
        {
            Instruction instruction = program[index];
            string? detail = null;
            if (!Enum.IsDefined(instruction.Op))
            {
                result.Add(new VmDiagnostic(VirtualMachineOptionsValidator.ProgramDiagnosticCode,
                    string.Format(culture, messages.GetString("Vm_E99_UnsupportedOpcode", culture) ??
                        (culture.TwoLetterISOLanguageName == "de"
                            ? "Nicht unterstützter Opcode: {0}."
                            : "Unsupported opcode: {0}."), instruction.Op)));
                continue;
            }
            else if (instruction.Level is < 0 or > 3)
                detail = $"instruction {index}: level {instruction.Level}";
            else if (instruction.Op == Opcode.Opr && !AllowedOprCodes.Contains(instruction.Argument))
            {
                result.Add(new VmDiagnostic(VirtualMachineOptionsValidator.ProgramDiagnosticCode,
                    string.Format(culture, messages.GetString("Vm_E99_UnsupportedOpr", culture) ??
                        (culture.TwoLetterISOLanguageName == "de"
                            ? "Nicht unterstützter OPR-Code: {0}."
                            : "Unsupported OPR code: {0}."), instruction.Argument)));
                continue;
            }
            else if (instruction.Op is Opcode.Lod or Opcode.Sto or Opcode.Int && instruction.Argument < 0)
                detail = $"instruction {index}: negative argument {instruction.Argument}";
            else if (instruction.Op is Opcode.Cal or Opcode.Jmp or Opcode.Jpc &&
                     (instruction.Argument < 0 || instruction.Argument >= program.Count))
                detail = $"instruction {index}: target {instruction.Argument}";
            if (detail is not null)
                result.Add(VirtualMachineOptionsValidator.ProgramError(detail, messages, culture));
        }
        return result;
    }

    private VmCompletionReason? Dispatch(Instruction instruction)
    {
        switch (instruction.Op)
        {
            case Opcode.Lit:
                if (!TryPush()) return VmCompletionReason.StackFault;
                stack[t] = instruction.Argument;
                return null;
            case Opcode.Opr:
                return ExecuteOpr(instruction.Argument);
            case Opcode.Lod:
                {
                    int baseAddress = ResolveBase(instruction.Level);
                    if (diagnostics.Count > 0 || !TryPush()) return VmCompletionReason.StackFault;
                    long candidate = (long)baseAddress + instruction.Argument;
                    if (!IsValidStackIndex(candidate)) return StackFault(
                        "Vm_E99_InvalidLodIndex",
                        "Ungültiger LOD-Zugriff bei Stack-Index {0}.",
                        "Invalid LOD access at stack index {0}.", candidate);
                    stack[t] = stack[(int)candidate];
                    return null;
                }
            case Opcode.Sto:
                {
                    int baseAddress = ResolveBase(instruction.Level);
                    if (diagnostics.Count > 0) return VmCompletionReason.StackFault;
                    long candidate = (long)baseAddress + instruction.Argument;
                    if (!IsValidStackIndex(candidate)) return StackFault(
                        "Vm_E99_InvalidStoIndex",
                        "Ungültiger STO-Zugriff bei Stack-Index {0}.",
                        "Invalid STO access at stack index {0}.", candidate);
                    if (!TryPeek(t)) return VmCompletionReason.StackFault;
                    stack[(int)candidate] = stack[t];
                    if (options.EnableStoreTrace && !TryWrite(stack[t])) return VmCompletionReason.IoFault;
                    t--;
                    return null;
                }
            case Opcode.Cal:
                {
                    int staticBase = ResolveBase(instruction.Level);
                    if (diagnostics.Count > 0) return VmCompletionReason.StackFault;
                    if (!IsValidStackIndex((long)t + 3)) return StackFault(
                        "Vm_E99_StackOverflowCallFrame",
                        "Stack-Überlauf beim Erstellen eines Aufrufrahmens.",
                        "Stack overflow while creating call frame.");
                    stack[t + 1] = staticBase; stack[t + 2] = b; stack[t + 3] = p;
                    b = t + 1; p = instruction.Argument;
                    return null;
                }
            case Opcode.Int:
                if (instruction.Argument > options.StackSize - t) return StackFault(
                    "Vm_E99_StackOverflowInt", "Stack-Überlauf bei INT.", "Stack overflow on INT.");
                t += instruction.Argument;
                return null;
            case Opcode.Jmp:
                p = instruction.Argument;
                return null;
            case Opcode.Jpc:
                if (!TryPeek(t)) return VmCompletionReason.StackFault;
                if (stack[t] == 0) p = instruction.Argument;
                t--;
                return null;
            default:
                return RuntimeFaultReason($"opcode {(int)instruction.Op}");
        }
    }

    private VmCompletionReason? ExecuteOpr(int code)
    {
        switch (code)
        {
            case 0:
                t = b - 1; p = stack[t + 3]; b = stack[t + 2]; return null;
            case 1:
                if (!TryPeek(t)) return VmCompletionReason.StackFault;
                stack[t] = -stack[t]; return null;
            case 2: return Binary((x, y) => x + y);
            case 3: return Binary((x, y) => x - y);
            case 4: return Binary((x, y) => x * y);
            case 5:
                if (!TryPeek(t) || !TryPeek(t - 1)) return VmCompletionReason.StackFault;
                if (stack[t] == 0)
                {
                    diagnostics.Add(new VmDiagnostic(DivisionCode,
                        messages.GetString("Vm_E206_DivisionByZero", culture) ?? "Division by zero."));
                    return VmCompletionReason.ArithmeticFault;
                }
                t--; stack[t] /= stack[t + 1]; return null;
            case 6:
                if (!TryPeek(t)) return VmCompletionReason.StackFault;
                stack[t] = Math.Abs(stack[t] % 2); return null;
            case 8: return Binary((x, y) => x == y ? 1 : 0);
            case 9: return Binary((x, y) => x != y ? 1 : 0);
            case 10: return Binary((x, y) => x < y ? 1 : 0);
            case 11: return Binary((x, y) => x >= y ? 1 : 0);
            case 12: return Binary((x, y) => x > y ? 1 : 0);
            case 13: return Binary((x, y) => x <= y ? 1 : 0);
            case 14:
                if (!TryPush()) return VmCompletionReason.StackFault;
                try { stack[t] = io.ReadInt(); return null; }
                catch (EndOfStreamException)
                { diagnostics.Add(new VmDiagnostic(InputEofCode, messages.GetString("Vm_E98_EndOfInput", culture) ?? "End of input.")); return VmCompletionReason.InputEndOfStream; }
                catch (FormatException)
                { diagnostics.Add(new VmDiagnostic(InputFormatCode, messages.GetString("Vm_E97_InputFormatError", culture) ?? "Invalid input format.")); return VmCompletionReason.InputFormatError; }
                catch (Exception ex) when (IsRecoverableHostException(ex))
                { diagnostics.Add(VirtualMachineOptionsValidator.IoError(messages, culture)); return VmCompletionReason.IoFault; }
            case 15:
                if (!TryPeek(t)) return VmCompletionReason.StackFault;
                if (!TryWrite(stack[t])) return VmCompletionReason.IoFault;
                t--; return null;
            default:
                return RuntimeFaultReason($"OPR {code}");
        }
    }

    private VmCompletionReason? Binary(Func<int, int, int> operation)
    {
        if (!TryPeek(t) || !TryPeek(t - 1)) return VmCompletionReason.StackFault;
        t--; stack[t] = operation(stack[t], stack[t + 1]); return null;
    }

    private int ResolveBase(int level)
    {
        int baseAddress = b;
        while (level-- > 0)
        {
            if (!IsValidStackIndex(baseAddress))
            {
                StackFault("Vm_E99_InvalidBasePointer",
                    "Ungültiger Basiszeiger beim Auflösen der Ebene: {0}.",
                    "Invalid base pointer while resolving level: {0}.", baseAddress);
                return 0;
            }
            baseAddress = stack[baseAddress];
        }
        return baseAddress;
    }

    private bool TryPush()
    {
        if (t + 1 > options.StackSize)
        {
            StackFault("Vm_E99_StackOverflow", "Stack-Überlauf.", "Stack overflow.");
            return false;
        }
        t++; return true;
    }

    private bool TryPeek(int index)
    {
        if (index >= 1 && index <= options.StackSize) return true;
        StackFault("Vm_E99_StackUnderflow", "Stack-Unterlauf.", "Stack underflow.");
        return false;
    }

    private bool TryWrite(int value)
    {
        try { io.WriteInt(value); return true; }
        catch (Exception ex) when (IsRecoverableHostException(ex))
        { diagnostics.Add(VirtualMachineOptionsValidator.IoError(messages, culture)); return false; }
    }

    private static bool IsRecoverableHostException(Exception exception) =>
        exception is not OutOfMemoryException and not StackOverflowException and not AccessViolationException;
    private bool IsValidStackIndex(long index) => index >= 1 && index <= options.StackSize;

    private VmCompletionReason StackFault(
        string resourceKey,
        string germanFallback,
        string englishFallback,
        params object[] arguments)
    {
        if (diagnostics.Count == 0 || diagnostics[^1].Code != RuntimeCode)
            diagnostics.Add(LocalizedDiagnostic(RuntimeCode, resourceKey,
                germanFallback, englishFallback, arguments));
        return VmCompletionReason.StackFault;
    }

    private VmDiagnostic LocalizedDiagnostic(
        int code,
        string resourceKey,
        string germanFallback,
        string englishFallback,
        params object[] arguments)
    {
        string template = messages.GetString(resourceKey, culture) ??
            (culture.TwoLetterISOLanguageName == "de" ? germanFallback : englishFallback);
        return new VmDiagnostic(code, string.Format(culture, template, arguments));
    }

    private VmCompletionReason RuntimeFaultReason(string detail)
    {
        diagnostics.Add(new VmDiagnostic(RuntimeCode,
            culture.TwoLetterISOLanguageName == "de" ? $"VM-Laufzeitfehler: {detail}." : $"VM runtime fault: {detail}."));
        return VmCompletionReason.RuntimeFault;
    }

    private VmStepResult RuntimeFault(string detail, VmState state) =>
        Complete(RuntimeFaultReason(detail), diagnostics, state);

    private VmStepResult Complete(VmCompletionReason reason, IEnumerable<VmDiagnostic> source, VmState state)
    {
        if (terminalResult is not null) return terminalResult;
        VmDiagnostic[] snapshot = source.ToArray();
        State = new VmState(state.P, state.B, state.T, state.Stack, state.CurrentInstruction, executedInstructions);
        VmStepStatus status = reason == VmCompletionReason.Halted ? VmStepStatus.Halted : VmStepStatus.Error;
        terminalResult = new VmStepResult(State, status, snapshot, reason, executedInstructions);
        return terminalResult;
    }

    private VmState CaptureState()
    {
        Instruction? current = p >= 0 && p < program.Length ? program[p] : null;
        return new VmState(p, b, t, stack, current, executedInstructions);
    }
}
