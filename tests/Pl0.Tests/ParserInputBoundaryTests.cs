using Pl0.Core;

namespace Pl0.Tests;

public sealed class ParserInputBoundaryTests
{
    [Fact]
    public void Missing_End_Of_File_Token_Is_Normalized_Without_Hanging()
    {
        Pl0Token[] tokens =
        [
            Token(TokenKind.Begin, "begin", 0),
            Token(TokenKind.Semicolon, ";", 5),
        ];

        CompilationResult result = new Pl0Parser(tokens, CompilerOptions.Default).Parse();

        Assert.False(result.Success);
        Assert.NotEmpty(result.Diagnostics);
    }

    [Theory]
    [InlineData(TokenKind.Ident, "value")]
    [InlineData(TokenKind.Plus, "+")]
    public void Other_Missing_End_Of_File_Forms_Return_Diagnostics(
        TokenKind finalKind,
        string lexeme)
    {
        Pl0Token[] tokens = [Token(finalKind, lexeme, 0)];

        CompilationResult result = new Pl0Parser(tokens, CompilerOptions.Default).Parse();

        Assert.False(result.Success);
        Assert.NotEmpty(result.Diagnostics);
    }

    [Fact]
    public void Empty_Token_Stream_Returns_Diagnostics_Without_Throwing()
    {
        CompilationResult result = new Pl0Parser([], CompilerOptions.Default).Parse();

        Assert.False(result.Success);
        Assert.NotEmpty(result.Diagnostics);
    }

    private static Pl0Token Token(TokenKind kind, string lexeme, int offset) =>
        new(kind, lexeme, new TextPosition(1, offset + 1, offset));
}
