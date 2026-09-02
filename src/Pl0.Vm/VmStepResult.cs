namespace Pl0.Vm;

/// <summary>Ergebnis eines VM-Schritts. / Result of one VM step.</summary>
public sealed record VmStepResult
{
    private readonly VmDiagnostic[] diagnostics;
    /// <summary>Erstellt das bisherige Ergebnisformat. / Creates the previous result shape.</summary>
    /// <param name="State">Zustand. / State.</param>
    /// <param name="Status">Kompatibler Status. / Compatible status.</param>
    /// <param name="Diagnostics">Zu kopierende Diagnosen. / Diagnostics to copy.</param>
    public VmStepResult(VmState State, VmStepStatus Status, IReadOnlyList<VmDiagnostic> Diagnostics)
        : this(State, Status, Diagnostics, Status == VmStepStatus.Running ? VmCompletionReason.Running :
            Status == VmStepStatus.Halted ? VmCompletionReason.Halted : VmCompletionReason.RuntimeFault,
            State.ExecutedInstructions)
    { }
    /// <summary>Erstellt ein vollständiges Schrittergebnis. / Creates a complete step result.</summary>
    /// <param name="State">Zustand. / State.</param>
    /// <param name="Status">Kompatibler Status. / Compatible status.</param>
    /// <param name="Diagnostics">Zu kopierende Diagnosen. / Diagnostics to copy.</param>
    /// <param name="Reason">Abschlussgrund. / Completion reason.</param>
    /// <param name="ExecutedInstructions">Begonnene Instruktionen. / Started instructions.</param>
    public VmStepResult(VmState State, VmStepStatus Status, IReadOnlyList<VmDiagnostic> Diagnostics,
        VmCompletionReason Reason, int ExecutedInstructions)
    {
        this.State = State; this.Status = Status; diagnostics = Diagnostics.ToArray();
        this.Reason = Reason; this.ExecutedInstructions = ExecutedInstructions;
    }
    /// <summary>Ruft den Zustand ab. / Gets the state.</summary>
    public VmState State { get; }
    /// <summary>Ruft den kompatiblen Status ab. / Gets the compatible status.</summary>
    public VmStepStatus Status { get; }
    /// <summary>Ruft sichere Diagnosen ab. / Gets safe diagnostics.</summary>
    public IReadOnlyList<VmDiagnostic> Diagnostics => Array.AsReadOnly(diagnostics);
    /// <summary>Ruft den Abschlussgrund ab. / Gets the completion reason.</summary>
    public VmCompletionReason Reason { get; }
    /// <summary>Ruft den Abschlussgrund unter dem Vertragsnamen ab. / Gets the completion reason by its contract name.</summary>
    public VmCompletionReason CompletionReason => Reason;
    /// <summary>Ruft den Instruktionszähler ab. / Gets the instruction counter.</summary>
    public int ExecutedInstructions { get; }
    /// <summary>Zerlegt das bisherige Ergebnisformat. / Deconstructs the previous result shape.</summary>
    /// <param name="State">Zustand. / State.</param>
    /// <param name="Status">Kompatibler Status. / Compatible status.</param>
    /// <param name="Diagnostics">Defensive Diagnosefolge. / Defensive diagnostic sequence.</param>
    public void Deconstruct(out VmState State, out VmStepStatus Status, out IReadOnlyList<VmDiagnostic> Diagnostics)
    { State = this.State; Status = this.Status; Diagnostics = this.Diagnostics; }
}
