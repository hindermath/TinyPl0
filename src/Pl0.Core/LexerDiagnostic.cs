namespace Pl0.Core;

public readonly record struct LexerDiagnostic(
    int Code,
    string Message,
    TextPosition Position);
