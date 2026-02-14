namespace Pl0.Vm;

/// <summary>
/// Diagnostic information produced by the virtual machine.
/// </summary>
/// <param name="Code">Diagnostic code.</param>
/// <param name="Message">Diagnostic message.</param>
public readonly record struct VmDiagnostic(int Code, string Message);
