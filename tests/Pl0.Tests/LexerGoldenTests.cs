using Pl0.Core;

namespace Pl0.Tests;

public sealed class LexerGoldenTests
{
    [Theory]
    [InlineData("feature_const_var_assignment.pl0", "feature_const_var_assignment.tokens.txt")]
    [InlineData("feature_io_q_bang_relops.pl0", "feature_io_q_bang_relops.tokens.txt")]
    public void Matches_Golden_Token_Stream(string sourceFile, string expectedFile)
    {
        var sourcePath = Path.Combine(RepoRoot, "tests", "data", "pl0", "valid", sourceFile);
        var expectedPath = Path.Combine(RepoRoot, "tests", "data", "expected", "lexer", expectedFile);

        var source = File.ReadAllText(sourcePath);
        var expected = NormalizeLineEndings(File.ReadAllText(expectedPath)).TrimEnd();

        var result = new Pl0Lexer(source).Lex();
        Assert.Empty(result.Diagnostics);

        var actual = string.Join(
            Environment.NewLine,
            result.Tokens.Select(FormatToken));

        Assert.Equal(expected, NormalizeLineEndings(actual).TrimEnd());
    }

    private static string RepoRoot => FindRepoRoot();

    private static string FindRepoRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "TinyPl0.sln")))
            {
                return dir.FullName;
            }

            dir = dir.Parent;
        }

        throw new InvalidOperationException("Could not locate repository root from test context.");
    }

    private static string FormatToken(Pl0Token token)
    {
        var lexeme = token.Kind == TokenKind.EndOfFile ? "<eof>" : token.Lexeme;
        return $"{token.Position.Line}:{token.Position.Column} {token.Kind} {lexeme}";
    }

    private static string NormalizeLineEndings(string text) =>
        text.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace("\n", Environment.NewLine, StringComparison.Ordinal);
}
