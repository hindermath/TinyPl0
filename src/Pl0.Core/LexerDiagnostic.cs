namespace Pl0.Core;

/// <summary>
/// Diagnostic information produced by the lexer.
/// </summary>
/// <param name="Code">The diagnostic code.</param>
/// <param name="Message">Human-readable message.</param>
/// <param name="Position">Source position where the diagnostic occurred.</param>
public readonly record struct LexerDiagnostic(
    int Code,
    string Message,
    TextPosition Position);
