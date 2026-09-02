namespace Pl0.Vm;

/// <summary>Ergebnis einer vollständigen VM-Ausführung. / Result of a complete VM execution.</summary>
public sealed class VmExecutionResult
{
    private readonly int[] stackSnapshot;
    private readonly VmDiagnostic[] diagnostics;
    /// <summary>Erstellt das bisherige Ergebnisformat. / Creates the previous result shape.</summary>
    /// <param name="stackSnapshot">Zu kopierender Stack. / Stack to copy.</param>
    /// <param name="top">Stackzeiger. / Stack pointer.</param>
    /// <param name="diagnostics">Zu kopierende Diagnosen. / Diagnostics to copy.</param>
    public VmExecutionResult(int[] stackSnapshot, int top, IReadOnlyList<VmDiagnostic> diagnostics)
        : this(stackSnapshot, top, diagnostics, diagnostics.Count == 0 ? VmCompletionReason.Halted : VmCompletionReason.RuntimeFault,
            0, new VmState(0, 0, top, stackSnapshot, null, 0))
    { }
    /// <summary>Erstellt ein vollständiges Ausführungsergebnis. / Creates a complete execution result.</summary>
    /// <param name="stackSnapshot">Zu kopierender Stack. / Stack to copy.</param>
    /// <param name="top">Stackzeiger. / Stack pointer.</param>
    /// <param name="diagnostics">Zu kopierende Diagnosen. / Diagnostics to copy.</param>
    /// <param name="reason">Abschlussgrund. / Completion reason.</param>
    /// <param name="executedInstructions">Begonnene Instruktionen. / Started instructions.</param>
    /// <param name="state">Terminaler Zustand. / Terminal state.</param>
    public VmExecutionResult(int[] stackSnapshot, int top, IReadOnlyList<VmDiagnostic> diagnostics,
        VmCompletionReason reason, int executedInstructions, VmState state)
    {
        this.stackSnapshot = (int[])stackSnapshot.Clone(); Top = top; this.diagnostics = diagnostics.ToArray();
        Reason = reason; ExecutedInstructions = executedInstructions; State = state;
    }
    /// <summary>Ruft eine defensive Stackkopie ab. / Gets a defensive stack copy.</summary>
    public int[] StackSnapshot => (int[])stackSnapshot.Clone();
    /// <summary>Ruft den Stackzeiger ab. / Gets the stack pointer.</summary>
    public int Top { get; }
    /// <summary>Ruft sichere Diagnosen ab. / Gets safe diagnostics.</summary>
    public IReadOnlyList<VmDiagnostic> Diagnostics => Array.AsReadOnly(diagnostics);
    /// <summary>Ruft den Abschlussgrund ab. / Gets the completion reason.</summary>
    public VmCompletionReason Reason { get; }
    /// <summary>Ruft den Abschlussgrund unter dem Vertragsnamen ab. / Gets the completion reason by its contract name.</summary>
    public VmCompletionReason CompletionReason => Reason;
    /// <summary>Ruft den Instruktionszähler ab. / Gets the instruction counter.</summary>
    public int ExecutedInstructions { get; }
    /// <summary>Ruft den terminalen Zustand ab. / Gets the terminal state.</summary>
    public VmState State { get; }
    /// <summary>Gibt an, ob regulär gehalten wurde. / Indicates whether execution halted normally.</summary>
    public bool Success => Reason == VmCompletionReason.Halted;
}
