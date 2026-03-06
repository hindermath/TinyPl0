using System.Globalization;
using System.Resources;
using Pl0.Core;

namespace Pl0.Vm;

/// <summary>
/// Executes PL/0 P-Code instructions on a stack-based virtual machine.
/// </summary>
public sealed class VirtualMachine
{
    /// <summary>
    /// Exit code for generic runtime errors.
    /// </summary>
    private const int RuntimeErrorExitCode = 99;
    /// <summary>
    /// Exit code for end-of-input during read.
    /// </summary>
    private const int InputEofExitCode = 98;
    /// <summary>
    /// Exit code for input format errors.
    /// </summary>
    private const int InputFormatExitCode = 97;
    /// <summary>
    /// Exit code for division-by-zero.
    /// </summary>
    private const int DivisionByZeroExitCode = 206;

    /// <summary>
    /// ResourceManager für lokalisierte Fehlertexte; wird in Run() gesetzt.
    /// </summary>
    private ResourceManager _rm = Pl0VmMessages.ResourceManager;
    /// <summary>
    /// Zielsprache für Fehlertexte; wird in Run() gesetzt.
    /// </summary>
    private CultureInfo _culture = CultureInfo.InvariantCulture;

    /// <summary>
    /// Runs a program and returns the execution result.
    /// </summary>
    /// <param name="program">P-Code instructions to execute.</param>
    /// <param name="io">Optional I/O implementation.</param>
    /// <param name="options">Optional VM options.</param>
    /// <returns>The execution result.</returns>
    public VmExecutionResult Run(
        IReadOnlyList<Instruction> program,
        IPl0Io? io = null,
        VirtualMachineOptions? options = null)
    {
        io ??= new ConsolePl0Io();
        options ??= VirtualMachineOptions.Default;
        _rm = options.Messages ?? Pl0VmMessages.ResourceManager;
        _culture = CultureInfo.GetCultureInfo(options.Language);

        var diagnostics = new List<VmDiagnostic>();
        var stack = new int[options.StackSize + 1];

        var p = 0;
        var b = 1;
        var t = 0;
        stack[1] = 0;
        stack[2] = 0;
        stack[3] = 0;

        while (true)
        {
            if (p < 0 || p >= program.Count)
            {
                diagnostics.Add(new VmDiagnostic(
                    RuntimeErrorExitCode,
                    string.Format(_culture,
                        _rm.GetString("Vm_E99_IPOutOfRange", _culture)
                            ?? "Instruction pointer out of range: {0}.",
                        p)));
                break;
            }

            var instruction = program[p];
            p++;

            switch (instruction.Op)
            {
                case Opcode.Lit:
                    if (!TryPush(ref t, options.StackSize, diagnostics))
                    {
                        return BuildResult(stack, t, diagnostics);
                    }

                    stack[t] = instruction.Argument;
                    break;

                case Opcode.Opr:
                    if (!ExecuteOpr(instruction.Argument, ref p, ref b, ref t, stack, io, options, diagnostics))
                    {
                        return BuildResult(stack, t, diagnostics);
                    }
                    break;

                case Opcode.Lod:
                {
                    var baseAddress = ResolveBase(instruction.Level, b, stack, diagnostics);
                    if (diagnostics.Count > 0 || !TryPush(ref t, options.StackSize, diagnostics))
                    {
                        return BuildResult(stack, t, diagnostics);
                    }

                    var index = baseAddress + instruction.Argument;
                    if (!IsValidStackIndex(index, options.StackSize))
                    {
                        diagnostics.Add(new VmDiagnostic(
                            RuntimeErrorExitCode,
                            string.Format(_culture,
                                _rm.GetString("Vm_E99_InvalidLodIndex", _culture)
                                    ?? "Invalid LOD access at stack index {0}.",
                                index)));
                        return BuildResult(stack, t, diagnostics);
                    }

                    stack[t] = stack[index];
                    break;
                }

                case Opcode.Sto:
                {
                    var baseAddress = ResolveBase(instruction.Level, b, stack, diagnostics);
                    if (diagnostics.Count > 0)
                    {
                        return BuildResult(stack, t, diagnostics);
                    }

                    var index = baseAddress + instruction.Argument;
                    if (!IsValidStackIndex(index, options.StackSize))
                    {
                        diagnostics.Add(new VmDiagnostic(
                            RuntimeErrorExitCode,
                            string.Format(_culture,
                                _rm.GetString("Vm_E99_InvalidStoIndex", _culture)
                                    ?? "Invalid STO access at stack index {0}.",
                                index)));
                        return BuildResult(stack, t, diagnostics);
                    }

                    if (!TryPeek(t, diagnostics))
                    {
                        return BuildResult(stack, t, diagnostics);
                    }

                    stack[index] = stack[t];
                    if (options.EnableStoreTrace)
                    {
                        io.WriteInt(stack[t]);
                    }

                    t--;
                    break;
                }

                case Opcode.Cal:
                {
                    var staticBase = ResolveBase(instruction.Level, b, stack, diagnostics);
                    if (diagnostics.Count > 0)
                    {
                        return BuildResult(stack, t, diagnostics);
                    }

                    if (!IsValidStackIndex(t + 3, options.StackSize))
                    {
                        diagnostics.Add(new VmDiagnostic(
                            RuntimeErrorExitCode,
                            _rm.GetString("Vm_E99_StackOverflowCallFrame", _culture)
                                ?? "Stack overflow while creating call frame."));
                        return BuildResult(stack, t, diagnostics);
                    }

                    stack[t + 1] = staticBase;
                    stack[t + 2] = b;
                    stack[t + 3] = p;
                    b = t + 1;
                    p = instruction.Argument;
                    break;
                }

                case Opcode.Int:
                    t += instruction.Argument;
                    if (!IsValidStackIndex(t, options.StackSize))
                    {
                        diagnostics.Add(new VmDiagnostic(
                            RuntimeErrorExitCode,
                            _rm.GetString("Vm_E99_StackOverflowInt", _culture)
                                ?? "Stack overflow on INT."));
                        return BuildResult(stack, t, diagnostics);
                    }
                    break;

                case Opcode.Jmp:
                    p = instruction.Argument;
                    break;

                case Opcode.Jpc:
                    if (!TryPeek(t, diagnostics))
                    {
                        return BuildResult(stack, t, diagnostics);
                    }

                    if (stack[t] == 0)
                    {
                        p = instruction.Argument;
                    }

                    t--;
                    break;

                default:
                    diagnostics.Add(new VmDiagnostic(
                        RuntimeErrorExitCode,
                        string.Format(_culture,
                            _rm.GetString("Vm_E99_UnsupportedOpcode", _culture)
                                ?? "Unsupported opcode: {0}.",
                            instruction.Op)));
                    return BuildResult(stack, t, diagnostics);
            }

            if (p == 0)
            {
                break;
            }
        }

        return BuildResult(stack, t, diagnostics);
    }

    /// <summary>
    /// Executes an OPR instruction.
    /// </summary>
    /// <param name="code">OPR subcode.</param>
    /// <param name="p">Program counter.</param>
    /// <param name="b">Base pointer.</param>
    /// <param name="t">Top-of-stack pointer.</param>
    /// <param name="stack">Stack storage.</param>
    /// <param name="io">I/O implementation.</param>
    /// <param name="options">VM options.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <returns>True if execution should continue.</returns>
    private bool ExecuteOpr(
        int code,
        ref int p,
        ref int b,
        ref int t,
        int[] stack,
        IPl0Io io,
        VirtualMachineOptions options,
        IList<VmDiagnostic> diagnostics)
    {
        switch (code)
        {
            case 0:
                t = b - 1;
                p = stack[t + 3];
                b = stack[t + 2];
                return true;

            case 1:
                if (!TryPeek(t, diagnostics))
                {
                    return false;
                }

                stack[t] = -stack[t];
                return true;

            case 2:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x + y);

            case 3:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x - y);

            case 4:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x * y);

            case 5:
                if (!TryPeek(t, diagnostics) || !TryPeek(t - 1, diagnostics))
                {
                    return false;
                }

                if (stack[t] == 0)
                {
                    diagnostics.Add(new VmDiagnostic(
                        DivisionByZeroExitCode,
                        _rm.GetString("Vm_E206_DivisionByZero", _culture)
                            ?? "Division by zero."));
                    return false;
                }

                t--;
                stack[t] = stack[t] / stack[t + 1];
                return true;

            case 6:
                if (!TryPeek(t, diagnostics))
                {
                    return false;
                }

                stack[t] = Math.Abs(stack[t] % 2);
                return true;

            case 8:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x == y ? 1 : 0);

            case 9:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x != y ? 1 : 0);

            case 10:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x < y ? 1 : 0);

            case 11:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x >= y ? 1 : 0);

            case 12:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x > y ? 1 : 0);

            case 13:
                return BinaryOp(ref t, stack, diagnostics, (x, y) => x <= y ? 1 : 0);

            case 14:
                if (!TryPush(ref t, options.StackSize, diagnostics))
                {
                    return false;
                }

                try
                {
                    stack[t] = io.ReadInt();
                    return true;
                }
                catch (EndOfStreamException)
                {
                    diagnostics.Add(new VmDiagnostic(
                        InputEofExitCode,
                        _rm.GetString("Vm_E98_EndOfInput", _culture)
                            ?? "End of input while reading."));
                    return false;
                }
                catch (FormatException)
                {
                    diagnostics.Add(new VmDiagnostic(
                        InputFormatExitCode,
                        _rm.GetString("Vm_E97_InputFormatError", _culture)
                            ?? "Invalid input format."));
                    return false;
                }

            case 15:
                if (!TryPeek(t, diagnostics))
                {
                    return false;
                }

                io.WriteInt(stack[t]);
                t--;
                return true;

            default:
                diagnostics.Add(new VmDiagnostic(
                    RuntimeErrorExitCode,
                    string.Format(_culture,
                        _rm.GetString("Vm_E99_UnsupportedOpr", _culture)
                            ?? "Unsupported OPR code: {0}.",
                        code)));
                return false;
        }
    }

    /// <summary>
    /// Executes a binary operation on the stack.
    /// </summary>
    /// <param name="t">Top-of-stack pointer.</param>
    /// <param name="stack">Stack storage.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <param name="op">Operation to execute.</param>
    /// <returns>True if execution should continue.</returns>
    private bool BinaryOp(
        ref int t,
        int[] stack,
        IList<VmDiagnostic> diagnostics,
        Func<int, int, int> op)
    {
        if (!TryPeek(t, diagnostics) || !TryPeek(t - 1, diagnostics))
        {
            return false;
        }

        t--;
        stack[t] = op(stack[t], stack[t + 1]);
        return true;
    }

    /// <summary>
    /// Resolves the base pointer for a given lexical level.
    /// </summary>
    /// <param name="level">Lexical level to resolve.</param>
    /// <param name="currentBase">Current base pointer.</param>
    /// <param name="stack">Stack storage.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <returns>Resolved base address.</returns>
    private int ResolveBase(int level, int currentBase, int[] stack, IList<VmDiagnostic> diagnostics)
    {
        var baseAddress = currentBase;
        while (level > 0)
        {
            if (baseAddress < 1 || baseAddress >= stack.Length)
            {
                diagnostics.Add(new VmDiagnostic(
                    RuntimeErrorExitCode,
                    string.Format(_culture,
                        _rm.GetString("Vm_E99_InvalidBasePointer", _culture)
                            ?? "Invalid base pointer while resolving level: {0}.",
                        baseAddress)));
                return 0;
            }

            baseAddress = stack[baseAddress];
            level--;
        }

        return baseAddress;
    }

    /// <summary>
    /// Attempts to push a value onto the stack.
    /// </summary>
    /// <param name="t">Top-of-stack pointer.</param>
    /// <param name="stackSize">Maximum stack size.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <returns>True if push is possible.</returns>
    private bool TryPush(ref int t, int stackSize, IList<VmDiagnostic> diagnostics)
    {
        if (t + 1 > stackSize)
        {
            diagnostics.Add(new VmDiagnostic(
                RuntimeErrorExitCode,
                _rm.GetString("Vm_E99_StackOverflow", _culture) ?? "Stack overflow."));
            return false;
        }

        t++;
        return true;
    }

    /// <summary>
    /// Ensures a stack index is valid for reading.
    /// </summary>
    /// <param name="index">Stack index.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <returns>True if the index is valid.</returns>
    private bool TryPeek(int index, IList<VmDiagnostic> diagnostics)
    {
        if (index < 1)
        {
            diagnostics.Add(new VmDiagnostic(
                RuntimeErrorExitCode,
                _rm.GetString("Vm_E99_StackUnderflow", _culture) ?? "Stack underflow."));
            return false;
        }

        return true;
    }

    /// <summary>
    /// Validates a stack index against bounds.
    /// </summary>
    /// <param name="index">Stack index.</param>
    /// <param name="stackSize">Maximum stack size.</param>
    /// <returns>True if the index is within bounds.</returns>
    private static bool IsValidStackIndex(int index, int stackSize) => index >= 1 && index <= stackSize;

    /// <summary>
    /// Builds an execution result with a safe stack snapshot.
    /// </summary>
    /// <param name="stack">Stack storage.</param>
    /// <param name="top">Top-of-stack pointer.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <returns>The execution result.</returns>
    private static VmExecutionResult BuildResult(int[] stack, int top, IReadOnlyList<VmDiagnostic> diagnostics)
    {
        var safeTop = Math.Clamp(top, 0, stack.Length - 1);
        var snapshot = new int[safeTop + 1];
        Array.Copy(stack, snapshot, safeTop + 1);
        return new VmExecutionResult(snapshot, safeTop, diagnostics);
    }
}
