namespace Pl0.Core;

public readonly record struct Pl0Token(
    TokenKind Kind,
    string Lexeme,
    TextPosition Position,
    int? NumberValue = null);
