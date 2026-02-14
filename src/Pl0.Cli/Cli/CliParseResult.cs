namespace Pl0.Cli.Cli;

/// <summary>
/// Result of parsing CLI arguments.
/// </summary>
public sealed class CliParseResult
{
    /// <summary>
    /// Creates a new parse result.
    /// </summary>
    /// <param name="options">Parsed options.</param>
    /// <param name="diagnostics">Diagnostics from parsing.</param>
    public CliParseResult(CompilerCliOptions options, IReadOnlyList<CliDiagnostic> diagnostics)
    {
        Options = options;
        Diagnostics = diagnostics;
    }

    /// <summary>
    /// Gets the parsed options.
    /// </summary>
    public CompilerCliOptions Options { get; }

    /// <summary>
    /// Gets the parse diagnostics.
    /// </summary>
    public IReadOnlyList<CliDiagnostic> Diagnostics { get; }

    /// <summary>
    /// Gets a value indicating whether any errors were found.
    /// </summary>
    public bool HasErrors => Diagnostics.Count > 0;

    /// <summary>
    /// Gets the exit code derived from diagnostics.
    /// </summary>
    public int ExitCode => Diagnostics.Count == 0 ? 0 : Diagnostics[0].Code;
}
