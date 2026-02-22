namespace Pl0.Vm;

/// <summary>
/// Result of executing exactly one VM instruction.
/// </summary>
/// <param name="State">State snapshot after the step.</param>
/// <param name="Status">Step execution status.</param>
/// <param name="Diagnostics">Diagnostics reported by the VM.</param>
public sealed record VmStepResult(
    VmState State,
    VmStepStatus Status,
    IReadOnlyList<VmDiagnostic> Diagnostics);
