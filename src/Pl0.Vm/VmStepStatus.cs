namespace Pl0.Vm;

/// <summary>
/// Status of a single VM step.
/// </summary>
public enum VmStepStatus
{
    /// <summary>
    /// Execution can continue.
    /// </summary>
    Running,

    /// <summary>
    /// Program has terminated.
    /// </summary>
    Halted,

    /// <summary>
    /// A runtime error occurred.
    /// </summary>
    Error
}
