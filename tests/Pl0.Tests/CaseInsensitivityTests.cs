using Pl0.Core;
using Xunit;

namespace Pl0.Tests;

public sealed class CaseInsensitivityTests
{
    [Fact]
    public void Lexer_Converts_Keywords_To_Lowercase()
    {
        var result = new Pl0Lexer("VAR X;\nBEGIN CALL P; END.").Lex();

        Assert.Equal(TokenKind.Var, result.Tokens[0].Kind);
        Assert.Equal("var", result.Tokens[0].Lexeme);
        
        Assert.Equal(TokenKind.Ident, result.Tokens[1].Kind);
        Assert.Equal("x", result.Tokens[1].Lexeme);

        Assert.Equal(TokenKind.Begin, result.Tokens[3].Kind);
        Assert.Equal("begin", result.Tokens[3].Lexeme);

        Assert.Equal(TokenKind.Call, result.Tokens[4].Kind);
        Assert.Equal("call", result.Tokens[4].Lexeme);
        
        Assert.Equal(TokenKind.Ident, result.Tokens[5].Kind);
        Assert.Equal("p", result.Tokens[5].Lexeme);

        Assert.Equal(TokenKind.End, result.Tokens[7].Kind);
        Assert.Equal("end", result.Tokens[7].Lexeme);
    }

    [Fact]
    public void Lexer_Converts_Odd_Keyword_To_Lowercase()
    {
        var result = new Pl0Lexer("IF ODD X THEN").Lex();

        Assert.Equal(TokenKind.If, result.Tokens[0].Kind);
        Assert.Equal(TokenKind.Odd, result.Tokens[1].Kind);
        Assert.Equal("odd", result.Tokens[1].Lexeme);
        Assert.Equal(TokenKind.Ident, result.Tokens[2].Kind);
        Assert.Equal("x", result.Tokens[2].Lexeme);
        Assert.Equal(TokenKind.Then, result.Tokens[3].Kind);
    }
}
