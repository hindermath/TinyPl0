namespace Pl0.Cli.Cli;

/// <summary>
/// Represents a CLI diagnostic with code and message.
/// </summary>
/// <param name="Code">Diagnostic code.</param>
/// <param name="Message">Diagnostic message.</param>
public sealed record CliDiagnostic(int Code, string Message);
