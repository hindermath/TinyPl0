using System.Text.Json;

namespace Pl0.Tests;

public sealed class TraceabilityMatrixTests
{
    private static readonly string[] RequiredLanguageRules =
    [
        "program",
        "block",
        "const-decl",
        "var-decl",
        "procedure-decl",
        "assignment",
        "call",
        "qstmt",
        "bangstmt",
        "begin-end",
        "if-then",
        "while-do",
        "condition-odd",
        "condition-relop",
        "expression-unary",
        "expression-binary",
        "term-mul-div",
        "factor-ident",
        "factor-number",
        "factor-paren",
    ];

    private static readonly string[] RequiredVmRules =
    [
        "lit",
        "opr-0-return",
        "opr-1-neg",
        "opr-2-add",
        "opr-3-sub",
        "opr-4-mul",
        "opr-5-div",
        "opr-6-odd",
        "opr-8-eq",
        "opr-9-neq",
        "opr-10-lt",
        "opr-11-ge",
        "opr-12-gt",
        "opr-13-le",
        "opr-14-read",
        "opr-15-write",
        "lod",
        "sto",
        "cal",
        "int",
        "jmp",
        "jpc",
        "base-l",
    ];

    [Fact]
    public void Traceability_Matrix_Covers_All_Required_Language_And_Vm_Rules()
    {
        var matrix = LoadMatrix();
        var catalogCases = LoadCatalogCaseNames();

        var languageRuleIds = matrix.LanguageRules.Select(r => r.Id).OrderBy(x => x, StringComparer.Ordinal).ToArray();
        var vmRuleIds = matrix.VmRules.Select(r => r.Id).OrderBy(x => x, StringComparer.Ordinal).ToArray();

        Assert.Equal(RequiredLanguageRules.OrderBy(x => x, StringComparer.Ordinal), languageRuleIds);
        Assert.Equal(RequiredVmRules.OrderBy(x => x, StringComparer.Ordinal), vmRuleIds);

        AssertAllMappingsReferToCatalogCases(matrix.LanguageRules, catalogCases);
        AssertAllMappingsReferToCatalogCases(matrix.VmRules, catalogCases);
    }

    private static void AssertAllMappingsReferToCatalogCases(
        IReadOnlyList<RuleCoverage> rules,
        ISet<string> catalogCases)
    {
        foreach (var rule in rules)
        {
            Assert.False(string.IsNullOrWhiteSpace(rule.Id));
            Assert.NotNull(rule.Cases);
            Assert.NotEmpty(rule.Cases);
            foreach (var caseName in rule.Cases)
            {
                Assert.Contains(caseName, catalogCases);
            }
        }
    }

    private static ISet<string> LoadCatalogCaseNames()
    {
        var path = Path.Combine(RepoRoot, "tests", "data", "expected", "catalog", "cases.json");
        var json = File.ReadAllText(path);
        var items = JsonSerializer.Deserialize<List<CatalogCase>>(json, SerializerOptions) ?? [];
        return items.Select(i => i.Name).ToHashSet(StringComparer.Ordinal);
    }

    private static TraceabilityMatrix LoadMatrix()
    {
        var path = Path.Combine(RepoRoot, "tests", "data", "expected", "traceability", "matrix.json");
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<TraceabilityMatrix>(json, SerializerOptions) ?? new TraceabilityMatrix();
    }

    private static JsonSerializerOptions SerializerOptions { get; } =
        new() { PropertyNameCaseInsensitive = true };

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

    private sealed class TraceabilityMatrix
    {
        public List<RuleCoverage> LanguageRules { get; set; } = [];

        public List<RuleCoverage> VmRules { get; set; } = [];
    }

    private sealed class RuleCoverage
    {
        public string Id { get; set; } = string.Empty;

        public List<string> Cases { get; set; } = [];
    }

    private sealed class CatalogCase
    {
        public string Name { get; set; } = string.Empty;
    }
}
