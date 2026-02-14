namespace Pl0.Cli.Cli;

/// <summary>
/// Parsed CLI options for the compiler and VM.
/// </summary>
public sealed class CompilerCliOptions
{
    /// <summary>
    /// Gets the selected CLI command.
    /// </summary>
    public CliCommand Command { get; init; } = CliCommand.None;

    /// <summary>
    /// Gets a value indicating whether help text was requested.
    /// </summary>
    public bool ShowHelp { get; init; }

    /// <summary>
    /// Gets a value indicating whether long error messages are enabled.
    /// </summary>
    public bool LongErrorMessages { get; init; }

    /// <summary>
    /// Gets a value indicating whether opcode numbers are included in listings.
    /// </summary>
    public bool WriteOpcodesInListing { get; init; }

    /// <summary>
    /// Gets a value indicating whether code listing should be printed.
    /// </summary>
    public bool ListCode { get; init; }

    /// <summary>
    /// Gets a value indicating whether compilation should stop before running the VM.
    /// </summary>
    public bool CompileOnly { get; init; }

    /// <summary>
    /// Gets a value indicating whether an emit output was requested.
    /// </summary>
    public bool EmitRequested { get; init; }

    /// <summary>
    /// Gets the emit mode selection.
    /// </summary>
    public EmitMode EmitMode { get; init; } = EmitMode.None;

    /// <summary>
    /// Gets the source path provided on the command line.
    /// </summary>
    public string? SourcePath { get; init; }

    /// <summary>
    /// Gets the output path for emitted P-Code.
    /// </summary>
    public string? OutputPath { get; init; }
}
