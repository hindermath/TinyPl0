namespace Pl0.Core;

public sealed class LexerResult
{
    public LexerResult(IReadOnlyList<Pl0Token> tokens, IReadOnlyList<LexerDiagnostic> diagnostics)
    {
        Tokens = tokens;
        Diagnostics = diagnostics;
    }

    public IReadOnlyList<Pl0Token> Tokens { get; }

    public IReadOnlyList<LexerDiagnostic> Diagnostics { get; }
}
