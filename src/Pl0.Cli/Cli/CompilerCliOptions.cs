namespace Pl0.Cli.Cli;

public sealed record CompilerCliOptions(
    bool ShowHelp,
    bool LongErrorMessages,
    bool WriteOpcodesInListing,
    bool CompileOnly,
    bool EmitRequested,
    EmitMode EmitMode,
    string? SourcePath);
