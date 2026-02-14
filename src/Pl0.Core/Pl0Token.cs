namespace Pl0.Core;

/// <summary>
/// Represents a single token from the PL/0 lexer.
/// </summary>
/// <param name="Kind">The token kind.</param>
/// <param name="Lexeme">The source text for the token.</param>
/// <param name="Position">The start position of the token.</param>
/// <param name="NumberValue">Parsed numeric value for number tokens.</param>
public readonly record struct Pl0Token(
    TokenKind Kind,
    string Lexeme,
    TextPosition Position,
    int? NumberValue = null);
