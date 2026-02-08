namespace Pl0.Vm;

public sealed class VmExecutionResult
{
    public VmExecutionResult(
        int[] stackSnapshot,
        int top,
        IReadOnlyList<VmDiagnostic> diagnostics)
    {
        StackSnapshot = stackSnapshot;
        Top = top;
        Diagnostics = diagnostics;
    }

    public int[] StackSnapshot { get; }

    public int Top { get; }

    public IReadOnlyList<VmDiagnostic> Diagnostics { get; }

    public bool Success => Diagnostics.Count == 0;
}
