using Pl0.Core;

namespace Pl0.Vm;

public sealed class VirtualMachine
{
    private const int RuntimeErrorExitCode = 99;
    private const int InputEofExitCode = 98;
    private const int InputFormatExitCode = 97;
    private const int DivisionByZeroExitCode = 206;

    public VmExecutionResult Run(
        IReadOnlyList<Instruction> program,
        IPl0Io? io = null,
        VirtualMachineOptions? options = null)
    {
        io ??= new ConsolePl0Io();
        options ??= VirtualMachineOptions.Default;

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
                    $"Instruction pointer out of range: {p}."));
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
                            $"Invalid LOD access at stack index {index}."));
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
                            $"Invalid STO access at stack index {index}."));
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
                            "Stack overflow while creating call frame."));
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
                            "Stack overflow on INT."));
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
                        $"Unsupported opcode: {instruction.Op}."));
                    return BuildResult(stack, t, diagnostics);
            }

            if (p == 0)
            {
                break;
            }
        }

        return BuildResult(stack, t, diagnostics);
    }

    private static bool ExecuteOpr(
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
                    diagnostics.Add(new VmDiagnostic(DivisionByZeroExitCode, "Division by zero."));
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
                    $"Unsupported OPR code: {code}."));
                return false;
        }
    }

    private static bool BinaryOp(
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

    private static int ResolveBase(int level, int currentBase, int[] stack, IList<VmDiagnostic> diagnostics)
    {
        var baseAddress = currentBase;
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

    private static bool TryPush(ref int t, int stackSize, IList<VmDiagnostic> diagnostics)
    {
        if (t + 1 > stackSize)
        {
            diagnostics.Add(new VmDiagnostic(RuntimeErrorExitCode, "Stack overflow."));
            return false;
        }

        t++;
        return true;
    }

    private static bool TryPeek(int index, IList<VmDiagnostic> diagnostics)
    {
        if (index < 1)
        {
            diagnostics.Add(new VmDiagnostic(RuntimeErrorExitCode, "Stack underflow."));
            return false;
        }

        return true;
    }

    private static bool IsValidStackIndex(int index, int stackSize) => index >= 1 && index <= stackSize;

    private static VmExecutionResult BuildResult(int[] stack, int top, IReadOnlyList<VmDiagnostic> diagnostics)
    {
        var safeTop = Math.Clamp(top, 0, stack.Length - 1);
        var snapshot = new int[safeTop + 1];
        Array.Copy(stack, snapshot, safeTop + 1);
        return new VmExecutionResult(snapshot, safeTop, diagnostics);
    }
}
