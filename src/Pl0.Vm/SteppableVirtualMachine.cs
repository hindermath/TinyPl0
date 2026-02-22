using Pl0.Core;

namespace Pl0.Vm;

/// <summary>
/// Stateful PL/0 virtual machine with explicit single-step execution.
/// </summary>
public sealed class SteppableVirtualMachine
{
    private const int RuntimeErrorExitCode = 99;
    private const int InputEofExitCode = 98;
    private const int InputFormatExitCode = 97;
    private const int DivisionByZeroExitCode = 206;

    private IReadOnlyList<Instruction> program = [];
    private IPl0Io io = new ConsolePl0Io();
    private VirtualMachineOptions options = VirtualMachineOptions.Default;
    private readonly List<VmDiagnostic> diagnostics = [];
    private int[] stack = [];
    private int p;
    private int b;
    private int t;
    private bool initialized;

    /// <summary>
    /// Gets the current VM state snapshot.
    /// </summary>
    public VmState State { get; private set; } = new(0, 0, 0, [], null);

    /// <summary>
    /// Gets a value indicating whether execution can continue.
    /// </summary>
    public bool IsRunning { get; private set; }

    /// <summary>
    /// Initializes the VM and resets state for debugging.
    /// </summary>
    /// <param name="program">P-Code instructions to execute.</param>
    /// <param name="io">Optional I/O abstraction.</param>
    /// <param name="options">Optional VM options.</param>
    public void Initialize(
        IReadOnlyList<Instruction> program,
        IPl0Io? io = null,
        VirtualMachineOptions? options = null)
    {
        this.program = program;
        this.io = io ?? new ConsolePl0Io();
        this.options = options ?? VirtualMachineOptions.Default;

        diagnostics.Clear();
        stack = new int[this.options.StackSize + 1];
        p = 0;
        b = 1;
        t = 0;
        stack[1] = 0;
        stack[2] = 0;
        stack[3] = 0;

        initialized = true;
        IsRunning = true;
        State = CaptureState();
    }

    /// <summary>
    /// Executes exactly one instruction.
    /// </summary>
    /// <returns>Step result with updated state and status.</returns>
    public VmStepResult Step()
    {
        if (!initialized)
        {
            diagnostics.Clear();
            diagnostics.Add(new VmDiagnostic(RuntimeErrorExitCode, "VM ist nicht initialisiert."));
            IsRunning = false;
            return new VmStepResult(State, VmStepStatus.Error, diagnostics.ToArray());
        }

        if (!IsRunning)
        {
            var terminalStatus = diagnostics.Count > 0 ? VmStepStatus.Error : VmStepStatus.Halted;
            return new VmStepResult(State, terminalStatus, diagnostics.ToArray());
        }

        var stateBeforeStep = State;
        var diagnosticsBeforeStep = diagnostics.Count;

        if (p < 0 || p >= program.Count)
        {
            diagnostics.Add(new VmDiagnostic(
                RuntimeErrorExitCode,
                $"Instruction pointer out of range: {p}."));
            IsRunning = false;
            State = stateBeforeStep;
            return new VmStepResult(State, VmStepStatus.Error, diagnostics.ToArray());
        }

        var instruction = program[p];
        p++;

        var continueExecution = ExecuteInstruction(instruction);
        if (!continueExecution)
        {
            IsRunning = false;
        }
        else if (p == 0)
        {
            IsRunning = false;
        }

        var status = VmStepStatus.Running;
        if (diagnostics.Count > diagnosticsBeforeStep)
        {
            status = VmStepStatus.Error;
            State = stateBeforeStep;
        }
        else
        {
            State = CaptureState();
            if (!IsRunning)
            {
                status = VmStepStatus.Halted;
            }
        }

        return new VmStepResult(State, status, diagnostics.ToArray());
    }

    private bool ExecuteInstruction(Instruction instruction)
    {
        switch (instruction.Op)
        {
            case Opcode.Lit:
                if (!TryPush())
                {
                    return false;
                }

                stack[t] = instruction.Argument;
                return true;

            case Opcode.Opr:
                return ExecuteOpr(instruction.Argument);

            case Opcode.Lod:
            {
                var baseAddress = ResolveBase(instruction.Level);
                if (diagnostics.Count > 0 || !TryPush())
                {
                    return false;
                }

                var index = baseAddress + instruction.Argument;
                if (!IsValidStackIndex(index))
                {
                    diagnostics.Add(new VmDiagnostic(
                        RuntimeErrorExitCode,
                        $"Invalid LOD access at stack index {index}."));
                    return false;
                }

                stack[t] = stack[index];
                return true;
            }

            case Opcode.Sto:
            {
                var baseAddress = ResolveBase(instruction.Level);
                if (diagnostics.Count > 0)
                {
                    return false;
                }

                var index = baseAddress + instruction.Argument;
                if (!IsValidStackIndex(index))
                {
                    diagnostics.Add(new VmDiagnostic(
                        RuntimeErrorExitCode,
                        $"Invalid STO access at stack index {index}."));
                    return false;
                }

                if (!TryPeek(t))
                {
                    return false;
                }

                stack[index] = stack[t];
                if (options.EnableStoreTrace)
                {
                    io.WriteInt(stack[t]);
                }

                t--;
                return true;
            }

            case Opcode.Cal:
            {
                var staticBase = ResolveBase(instruction.Level);
                if (diagnostics.Count > 0)
                {
                    return false;
                }

                if (!IsValidStackIndex(t + 3))
                {
                    diagnostics.Add(new VmDiagnostic(
                        RuntimeErrorExitCode,
                        "Stack overflow while creating call frame."));
                    return false;
                }

                stack[t + 1] = staticBase;
                stack[t + 2] = b;
                stack[t + 3] = p;
                b = t + 1;
                p = instruction.Argument;
                return true;
            }

            case Opcode.Int:
                t += instruction.Argument;
                if (!IsValidStackIndex(t))
                {
                    diagnostics.Add(new VmDiagnostic(
                        RuntimeErrorExitCode,
                        "Stack overflow on INT."));
                    return false;
                }

                return true;

            case Opcode.Jmp:
                p = instruction.Argument;
                return true;

            case Opcode.Jpc:
                if (!TryPeek(t))
                {
                    return false;
                }

                if (stack[t] == 0)
                {
                    p = instruction.Argument;
                }

                t--;
                return true;

            default:
                diagnostics.Add(new VmDiagnostic(
                    RuntimeErrorExitCode,
                    $"Unsupported opcode: {instruction.Op}."));
                return false;
        }
    }

    private bool ExecuteOpr(int code)
    {
        switch (code)
        {
            case 0:
                t = b - 1;
                p = stack[t + 3];
                b = stack[t + 2];
                return true;

            case 1:
                if (!TryPeek(t))
                {
                    return false;
                }

                stack[t] = -stack[t];
                return true;

            case 2:
                return BinaryOp((x, y) => x + y);

            case 3:
                return BinaryOp((x, y) => x - y);

            case 4:
                return BinaryOp((x, y) => x * y);

            case 5:
                if (!TryPeek(t) || !TryPeek(t - 1))
                {
                    return false;
                }

                if (stack[t] == 0)
                {
                    diagnostics.Add(new VmDiagnostic(DivisionByZeroExitCode, "Division by zero."));
                    return false;
                }

                t--;
                stack[t] /= stack[t + 1];
                return true;

            case 6:
                if (!TryPeek(t))
                {
                    return false;
                }

                stack[t] = Math.Abs(stack[t] % 2);
                return true;

            case 8:
                return BinaryOp((x, y) => x == y ? 1 : 0);

            case 9:
                return BinaryOp((x, y) => x != y ? 1 : 0);

            case 10:
                return BinaryOp((x, y) => x < y ? 1 : 0);

            case 11:
                return BinaryOp((x, y) => x >= y ? 1 : 0);

            case 12:
                return BinaryOp((x, y) => x > y ? 1 : 0);

            case 13:
                return BinaryOp((x, y) => x <= y ? 1 : 0);

            case 14:
                if (!TryPush())
                {
                    return false;
                }

                try
                {
                    stack[t] = io.ReadInt();
                    return true;
                }
                catch (EndOfStreamException ex)
                {
                    diagnostics.Add(new VmDiagnostic(InputEofExitCode, ex.Message));
                    return false;
                }
                catch (FormatException ex)
                {
                    diagnostics.Add(new VmDiagnostic(InputFormatExitCode, ex.Message));
                    return false;
                }

            case 15:
                if (!TryPeek(t))
                {
                    return false;
                }

                io.WriteInt(stack[t]);
                t--;
                return true;

            default:
                diagnostics.Add(new VmDiagnostic(
                    RuntimeErrorExitCode,
                    $"Unsupported OPR code: {code}."));
                return false;
        }
    }

    private bool BinaryOp(Func<int, int, int> op)
    {
        if (!TryPeek(t) || !TryPeek(t - 1))
        {
            return false;
        }

        t--;
        stack[t] = op(stack[t], stack[t + 1]);
        return true;
    }

    private int ResolveBase(int level)
    {
        var baseAddress = b;
        while (level > 0)
        {
            if (baseAddress < 1 || baseAddress >= stack.Length)
            {
                diagnostics.Add(new VmDiagnostic(
                    RuntimeErrorExitCode,
                    $"Invalid base pointer while resolving level: {baseAddress}."));
                return 0;
            }

            baseAddress = stack[baseAddress];
            level--;
        }

        return baseAddress;
    }

    private bool TryPush()
    {
        if (t + 1 > options.StackSize)
        {
            diagnostics.Add(new VmDiagnostic(RuntimeErrorExitCode, "Stack overflow."));
            return false;
        }

        t++;
        return true;
    }

    private bool TryPeek(int index)
    {
        if (index < 1)
        {
            diagnostics.Add(new VmDiagnostic(RuntimeErrorExitCode, "Stack underflow."));
            return false;
        }

        return true;
    }

    private bool IsValidStackIndex(int index) => index >= 1 && index <= options.StackSize;

    private VmState CaptureState()
    {
        var snapshot = new int[stack.Length];
        Array.Copy(stack, snapshot, stack.Length);
        Instruction? currentInstruction = p >= 0 && p < program.Count ? program[p] : null;
        return new VmState(p, b, t, snapshot, currentInstruction);
    }
}
