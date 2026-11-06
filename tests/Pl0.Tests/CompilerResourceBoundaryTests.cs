using Pl0.Core;

namespace Pl0.Tests;

public sealed class CompilerResourceBoundaryTests
{
    [Fact]
    public void CompilerOptions_Preserves_NineValue_Construction_And_Deconstruction()
    {
        var options = new CompilerOptions(Pl0Dialect.Classic, 3, 2047, 10, 14, 100, 200, "de", null);

        var (dialect, maxLevel, maxAddress, maxIdentifierLength, maxNumberDigits,
            maxSymbolCount, maxCodeLength, language, messages) = options;

        Assert.Equal(Pl0Dialect.Classic, dialect);
        Assert.Equal(3, maxLevel);
        Assert.Equal(2047, maxAddress);
        Assert.Equal(10, maxIdentifierLength);
        Assert.Equal(14, maxNumberDigits);
        Assert.Equal(100, maxSymbolCount);
        Assert.Equal(200, maxCodeLength);
        Assert.Equal("de", language);
        Assert.Null(messages);
        Assert.Equal(1_048_576, options.MaxSourceLength);
        Assert.Equal(100_000, options.MaxTokenCount);
        Assert.Equal(100, options.MaxDiagnosticCount);
    }

    [Fact]
    public void Lexer_Rejects_Source_Above_Configured_And_Absolute_Boundary()
    {
        var options = CompilerOptions.Default with { MaxSourceLength = 32 };

        LexerResult result = new Pl0Lexer(new string('a', 33), options).Lex();

        LexerDiagnostic diagnostic = Assert.Single(result.Diagnostics);
        Assert.Equal(37, diagnostic.Code);
        Assert.Single(result.Tokens);
        Assert.Equal(TokenKind.EndOfFile, result.Tokens[0].Kind);
    }

    [Fact]
    public void Lexer_Bounds_Diagnostic_Flooding()
    {
        var options = CompilerOptions.Default with { MaxDiagnosticCount = 3 };

        LexerResult result = new Pl0Lexer("@@@@@@", options).Lex();

        Assert.Equal(4, result.Diagnostics.Count);
        Assert.Equal(37, result.Diagnostics[^1].Code);
        Assert.Equal(TokenKind.EndOfFile, result.Tokens[^1].Kind);
    }

    [Fact]
    public void Lexer_Bounds_Total_Token_Count()
    {
        var options = CompilerOptions.Default with { MaxTokenCount = 3 };

        LexerResult result = new Pl0Lexer("+ + + + +", options).Lex();

        Assert.Equal(3, result.Tokens.Count(token => token.Kind == TokenKind.Plus));
        Assert.Equal(37, Assert.Single(result.Diagnostics).Code);
        Assert.Equal(TokenKind.EndOfFile, result.Tokens[^1].Kind);
    }

    [Fact]
    public void Parser_Bounds_Diagnostics_From_Valid_Tokens()
    {
        var options = CompilerOptions.Default with { MaxDiagnosticCount = 3 };
        string statements = string.Join("; ", Enumerable.Repeat("! missing", 20));

        CompilationResult result = new Pl0Compiler().Compile($"begin {statements} end.", options);

        Assert.Equal(4, result.Diagnostics.Count);
        Assert.Equal(37, result.Diagnostics[^1].Code);
    }
}
