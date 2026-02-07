using Pl0.Core;

namespace Pl0.Tests;

public sealed class LexerTests
{
    [Fact]
    public void Tracks_Line_And_Column_Across_Newline()
    {
        var result = new Pl0Lexer("var x;\nvar y;").Lex();

        var firstVar = result.Tokens[0];
        var secondVar = result.Tokens[3];

        Assert.Equal(TokenKind.Var, firstVar.Kind);
        Assert.Equal(new TextPosition(1, 1, 0), firstVar.Position);
        Assert.Equal(TokenKind.Var, secondVar.Kind);
        Assert.Equal(new TextPosition(2, 1, 7), secondVar.Position);
    }

    [Fact]
    public void Recognizes_Historical_Bracket_RelOps()
    {
        var result = new Pl0Lexer("[ ]").Lex();

        Assert.Empty(result.Diagnostics);
        Assert.Equal(TokenKind.LessOrEqual, result.Tokens[0].Kind);
        Assert.Equal(TokenKind.GreaterOrEqual, result.Tokens[1].Kind);
    }

    [Fact]
    public void Reports_Diagnostic_For_Bare_Colon()
    {
        var result = new Pl0Lexer(":").Lex();

        Assert.Single(result.Diagnostics);
        Assert.Equal(99, result.Diagnostics[0].Code);
        Assert.Equal(TokenKind.Nul, result.Tokens[0].Kind);
    }
}
