namespace Pl0.Vm;

public sealed record VirtualMachineOptions(
    int StackSize = 500,
    bool EnableStoreTrace = false)
{
    public static VirtualMachineOptions Default { get; } = new();
}
