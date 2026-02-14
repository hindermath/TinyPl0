namespace Pl0.Vm;

/// <summary>
/// Result of executing a P-Code program on the VM.
/// </summary>
public sealed class VmExecutionResult
{
    /// <summary>
    /// Creates a new execution result.
    /// </summary>
    /// <param name="stackSnapshot">Captured stack snapshot.</param>
    /// <param name="top">Top-of-stack index.</param>
    /// <param name="diagnostics">Diagnostics produced during execution.</param>
    public VmExecutionResult(
        int[] stackSnapshot,
        int top,
        IReadOnlyList<VmDiagnostic> diagnostics)
    {
        StackSnapshot = stackSnapshot;
        Top = top;
        Diagnostics = diagnostics;
    }

    /// <summary>
    /// Gets the stack snapshot.
    /// </summary>
    public int[] StackSnapshot { get; }

    /// <summary>
    /// Gets the top-of-stack index.
    /// </summary>
    public int Top { get; }

    /// <summary>
    /// Gets the execution diagnostics.
    /// </summary>
    public IReadOnlyList<VmDiagnostic> Diagnostics { get; }

    /// <summary>
    /// Gets a value indicating whether execution completed without diagnostics.
    /// </summary>
    public bool Success => Diagnostics.Count == 0;
}
