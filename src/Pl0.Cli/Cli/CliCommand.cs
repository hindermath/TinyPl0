namespace Pl0.Cli.Cli;

/// <summary>
/// CLI commands supported by TinyPl0.
/// </summary>
public enum CliCommand
{
    /// <summary>
    /// No explicit command provided.
    /// </summary>
    None = 0,
    /// <summary>
    /// Compile PL/0 source to P-Code.
    /// </summary>
    Compile,
    /// <summary>
    /// Compile and run PL/0 source.
    /// </summary>
    Run,
    /// <summary>
    /// Run a P-Code file directly.
    /// </summary>
    RunPCode,
}
