using Pl0.Core;

namespace Pl0.Tests;

public sealed class ParserGoldenTests
{
    [Theory]
    [InlineData("feature_const_var_assignment.pl0", "feature_const_var_assignment.pcode.txt")]
    [InlineData("feature_io_q_bang_relops.pl0", "feature_io_q_bang_relops.pcode.txt")]
    public void Matches_Golden_Instruction_Stream(string sourceFile, string expectedFile)
    {
        var sourcePath = Path.Combine(RepoRoot, "tests", "data", "pl0", "valid", sourceFile);
        var expectedPath = Path.Combine(RepoRoot, "tests", "data", "expected", "code", expectedFile);

        var source = File.ReadAllText(sourcePath);
        var expected = NormalizeLineEndings(File.ReadAllText(expectedPath)).TrimEnd();

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(result.Success, string.Join(Environment.NewLine, result.Diagnostics.Select(d => d.Message)));

        var actual = string.Join(Environment.NewLine, result.Instructions.Select(FormatInstruction));
        Assert.Equal(expected, NormalizeLineEndings(actual).TrimEnd());
    }

    [Fact]
    public void Reports_Error_For_Undeclared_Identifier()
    {
        const string source = """
                              begin
                              y := 1;
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 11);
    }

    [Fact]
    public void Rejects_Question_Statement_In_Classic_Dialect()
    {
        const string source = """
                              var x;
                              begin
                              ? x;
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, new CompilerOptions(Pl0Dialect.Classic));
        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 19);
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

    private static string FormatInstruction(Instruction instruction) =>
        $"{instruction.Op.ToString().ToLowerInvariant()} {instruction.Level} {instruction.Argument}";

    private static string NormalizeLineEndings(string text) =>
        text.Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace("\n", Environment.NewLine, StringComparison.Ordinal);
}
