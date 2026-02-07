using Pl0.Core;

namespace Pl0.Vm;

public sealed class VirtualMachine
{
    public int[] Run(IReadOnlyList<Instruction> program)
    {
        _ = program;
        return [];
    }
}
