namespace Pl0.Cli.Cli;

public sealed class CliParseResult
{
    public CliParseResult(CompilerCliOptions options, IReadOnlyList<CliDiagnostic> diagnostics)
    {
        Options = options;
        Diagnostics = diagnostics;
    }

    public CompilerCliOptions Options { get; }

    public IReadOnlyList<CliDiagnostic> Diagnostics { get; }

    public bool HasErrors => Diagnostics.Count > 0;

    public int ExitCode => Diagnostics.Count == 0 ? 0 : Diagnostics[0].Code;
}
