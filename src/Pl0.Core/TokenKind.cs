namespace Pl0.Core;

/// <summary>
/// Token kinds produced by the lexer.
/// </summary>
public enum TokenKind
{
    /// <summary>
    /// Invalid or null token placeholder.
    /// </summary>
    Nul = 0,
    /// <summary>
    /// Identifier token.
    /// </summary>
    Ident,
    /// <summary>
    /// Numeric literal token.
    /// </summary>
    Number,
    /// <summary>
    /// Plus sign.
    /// </summary>
    Plus,
    /// <summary>
    /// Minus sign.
    /// </summary>
    Minus,
    /// <summary>
    /// Multiplication operator.
    /// </summary>
    Times,
    /// <summary>
    /// Division operator.
    /// </summary>
    Slash,
    /// <summary>
    /// odd keyword.
    /// </summary>
    Odd,
    /// <summary>
    /// Equality operator.
    /// </summary>
    Equal,
    /// <summary>
    /// Inequality operator.
    /// </summary>
    NotEqual,
    /// <summary>
    /// Less-than operator.
    /// </summary>
    Less,
    /// <summary>
    /// Less-than-or-equal operator.
    /// </summary>
    LessOrEqual,
    /// <summary>
    /// Greater-than operator.
    /// </summary>
    Greater,
    /// <summary>
    /// Greater-than-or-equal operator.
    /// </summary>
    GreaterOrEqual,
    /// <summary>
    /// Left parenthesis.
    /// </summary>
    LParen,
    /// <summary>
    /// Right parenthesis.
    /// </summary>
    RParen,
    /// <summary>
    /// Comma separator.
    /// </summary>
    Comma,
    /// <summary>
    /// Semicolon separator.
    /// </summary>
    Semicolon,
    /// <summary>
    /// Period program terminator.
    /// </summary>
    Period,
    /// <summary>
    /// Assignment operator :=.
    /// </summary>
    Becomes,
    /// <summary>
    /// begin keyword.
    /// </summary>
    Begin,
    /// <summary>
    /// end keyword.
    /// </summary>
    End,
    /// <summary>
    /// if keyword.
    /// </summary>
    If,
    /// <summary>
    /// then keyword.
    /// </summary>
    Then,
    /// <summary>
    /// while keyword.
    /// </summary>
    While,
    /// <summary>
    /// do keyword.
    /// </summary>
    Do,
    /// <summary>
    /// call keyword.
    /// </summary>
    Call,
    /// <summary>
    /// const keyword.
    /// </summary>
    Const,
    /// <summary>
    /// var keyword.
    /// </summary>
    Var,
    /// <summary>
    /// procedure keyword.
    /// </summary>
    Procedure,
    /// <summary>
    /// Input statement token (?).
    /// </summary>
    Question,
    /// <summary>
    /// Output statement token (!).
    /// </summary>
    Bang,
    /// <summary>
    /// End-of-file token.
    /// </summary>
    EndOfFile,
}
