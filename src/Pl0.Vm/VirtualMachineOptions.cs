namespace Pl0.Vm;

/// <summary>
/// Configuration options for the PL/0 virtual machine.
/// </summary>
/// <param name="StackSize">Maximum stack size.</param>
/// <param name="EnableStoreTrace">Whether STO writes are echoed to output.</param>
public sealed record VirtualMachineOptions(
    int StackSize = 500,
    bool EnableStoreTrace = false)
{
    /// <summary>
    /// Default VM options.
    /// </summary>
    public static VirtualMachineOptions Default { get; } = new();
}
