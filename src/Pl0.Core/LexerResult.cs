namespace Pl0.Core;

/// <summary>
/// Result of lexical analysis containing tokens and diagnostics.
/// </summary>
public sealed class LexerResult
{
    /// <summary>
    /// Creates a new lexer result.
    /// </summary>
    /// <param name="tokens">The lexed tokens.</param>
    /// <param name="diagnostics">Diagnostics produced by the lexer.</param>
    public LexerResult(IReadOnlyList<Pl0Token> tokens, IReadOnlyList<LexerDiagnostic> diagnostics)
    {
        Tokens = tokens;
        Diagnostics = diagnostics;
    }

    /// <summary>
    /// Gets the tokens produced by the lexer.
    /// </summary>
    public IReadOnlyList<Pl0Token> Tokens { get; }

    /// <summary>
    /// Gets the diagnostics produced by the lexer.
    /// </summary>
    public IReadOnlyList<LexerDiagnostic> Diagnostics { get; }
}
