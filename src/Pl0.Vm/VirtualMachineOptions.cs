using System.Resources;

namespace Pl0.Vm;

/// <summary>
/// Configuration options for the PL/0 virtual machine.
/// </summary>
/// <param name="StackSize">Maximum stack size.</param>
/// <param name="EnableStoreTrace">Whether STO writes are echoed to output.</param>
/// <param name="Language">BCP-47-Sprachcode für VM-Fehlertexte (Standard: "de").</param>
/// <param name="Messages">Optionaler ResourceManager für Dependency Injection (z. B. in Tests); Standard: Pl0VmMessages.ResourceManager.</param>
public sealed record VirtualMachineOptions(
    int StackSize = 500,
    bool EnableStoreTrace = false,
    string Language = "de",
    ResourceManager? Messages = null)
{
    /// <summary>
    /// Default VM options.
    /// </summary>
    public static VirtualMachineOptions Default { get; } = new();
}
