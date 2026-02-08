namespace Pl0.Cli.Cli;

public sealed class CompilerCliOptions
{
    public CliCommand Command { get; init; } = CliCommand.None;

    public bool ShowHelp { get; init; }

    public bool LongErrorMessages { get; init; }

    public bool WriteOpcodesInListing { get; init; }

    public bool ListCode { get; init; }

    public bool CompileOnly { get; init; }

    public bool EmitRequested { get; init; }

    public EmitMode EmitMode { get; init; } = EmitMode.None;

    public string? SourcePath { get; init; }

    public string? OutputPath { get; init; }
}
