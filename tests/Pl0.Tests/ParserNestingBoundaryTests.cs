using System.Text;
using Pl0.Core;

namespace Pl0.Tests;

public sealed class ParserNestingBoundaryTests
{
    private const int MaximumSupportedNesting = 10;

    [Theory]
    [InlineData(10, true)]
    [InlineData(11, false)]
    public void Enforces_Parenthesized_Expression_Nesting_Boundary(int depth, bool expectedSuccess)
    {
        var source = $"! {new string('(', depth)}1{new string(')', depth)}.";

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.Equal(expectedSuccess, result.Success);
        Assert.Equal(expectedSuccess ? 0 : 1, result.Diagnostics.Count(d => d.Code == 36));
    }

    [Theory]
    [InlineData("begin", 10, true)]
    [InlineData("begin", 11, false)]
    [InlineData("if", 10, true)]
    [InlineData("if", 11, false)]
    [InlineData("while", 10, true)]
    [InlineData("while", 11, false)]
    public void Enforces_Structured_Statement_Nesting_Boundary(
        string statementKind,
        int depth,
        bool expectedSuccess)
    {
        var source = statementKind switch
        {
            "begin" => CreateNestedBeginSource(depth),
            "if" => CreateNestedConditionalSource(depth, "if 1 = 1 then "),
            "while" => CreateNestedConditionalSource(depth, "while 1 = 1 do "),
            _ => throw new ArgumentOutOfRangeException(nameof(statementKind)),
        };

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.Equal(expectedSuccess, result.Success);
        Assert.Equal(expectedSuccess ? 0 : 1, result.Diagnostics.Count(d => d.Code == 36));
    }

    [Fact]
    public void Shares_Nesting_Budget_Across_Statements_And_Expressions()
    {
        var accepted = "begin begin if 1 = 1 then if 1 = 1 then "
            + "while 1 = 1 do while 1 = 1 do ! ((((1)))) end end.";
        var rejected = accepted.Replace("((((1))))", "(((((1)))))", StringComparison.Ordinal);

        var acceptedResult = new Pl0Compiler().Compile(accepted, CompilerOptions.Default);
        var rejectedResult = new Pl0Compiler().Compile(rejected, CompilerOptions.Default);

        Assert.True(acceptedResult.Success);
        Assert.Single(rejectedResult.Diagnostics, d => d.Code == 36);
    }

    [Fact]
    public void Direct_Parser_Enforces_Nesting_Boundary()
    {
        var source = $"! {new string('(', MaximumSupportedNesting + 1)}1"
            + $"{new string(')', MaximumSupportedNesting + 1)}.";
        var lexResult = new Pl0Lexer(source, CompilerOptions.Default).Lex();
        Assert.Empty(lexResult.Diagnostics);

        var result = new Pl0Parser(lexResult.Tokens, CompilerOptions.Default).Parse();

        Assert.Single(result.Diagnostics, d => d.Code == 36);
    }

    [Theory]
    [InlineData("de", "Maximale Syntaxverschachtelungstiefe von 10 überschritten.")]
    [InlineData("en", "Maximum syntax nesting depth of 10 exceeded.")]
    public void Localizes_Nesting_Diagnostic(string language, string expectedMessage)
    {
        var options = CompilerOptions.Default with { Language = language };
        var source = $"! {new string('(', MaximumSupportedNesting + 1)}1"
            + $"{new string(')', MaximumSupportedNesting + 1)}.";

        var result = new Pl0Compiler().Compile(source, options);

        var diagnostic = Assert.Single(result.Diagnostics);
        Assert.Equal(36, diagnostic.Code);
        Assert.Equal(expectedMessage, diagnostic.Message);
    }

    [Fact]
    public void Rejects_Extreme_Nesting_Without_Throwing_Or_Diagnostic_Flooding()
    {
        const int hostileDepth = 10_000;
        var source = $"! {new string('(', hostileDepth)}1{new string(')', hostileDepth)}.";

        var exception = Record.Exception(
            () =>
            {
                var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);
                Assert.Single(result.Diagnostics, d => d.Code == 36);
            });

        Assert.Null(exception);
    }

    [Fact]
    public void Stops_Once_When_Closing_Parentheses_Are_Missing_Beyond_The_Boundary()
    {
        var source = $"! {new string('(', MaximumSupportedNesting + 1)}1.";

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        var diagnostic = Assert.Single(result.Diagnostics);
        Assert.Equal(36, diagnostic.Code);
    }

    [Theory]
    [InlineData(3, 3, true)]
    [InlineData(4, 3, false)]
    [InlineData(10, 10, true)]
    [InlineData(11, 10, false)]
    [InlineData(11, int.MaxValue, false)]
    public void Enforces_Configured_And_Absolute_Lexical_Level_Boundaries(
        int procedureDepth,
        int configuredMaximum,
        bool expectedSuccess)
    {
        var options = CompilerOptions.Default with { MaxLevel = configuredMaximum };

        var result = new Pl0Compiler().Compile(CreateNestedProcedureSource(procedureDepth), options);

        Assert.Equal(expectedSuccess, result.Success);
        Assert.Equal(expectedSuccess ? 0 : 1, result.Diagnostics.Count(d => d.Code == 32));
    }

    private static string CreateNestedBeginSource(int depth) =>
        string.Concat(Enumerable.Repeat("begin ", depth))
        + "! 1 "
        + string.Concat(Enumerable.Repeat("end ", depth))
        + ".";

    private static string CreateNestedConditionalSource(int depth, string prefix) =>
        string.Concat(Enumerable.Repeat(prefix, depth)) + "! 1.";

    private static string CreateNestedProcedureSource(int depth)
    {
        var source = new StringBuilder();
        for (var level = 1; level <= depth; level++)
        {
            source.Append("procedure p").Append(level).AppendLine(";");
        }

        for (var level = depth; level >= 1; level--)
        {
            source.AppendLine("begin end;");
        }

        return source.Append("begin end.").ToString();
    }
}
