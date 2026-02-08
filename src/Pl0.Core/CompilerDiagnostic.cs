namespace Pl0.Core;

public readonly record struct CompilerDiagnostic(
    int Code,
    string Message,
    TextPosition Position);
