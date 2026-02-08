using System.Text.Json;
using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class CatalogCasesTests
{
    private static readonly string[] RequiredCaseFileNames =
    [
        "feature_const_var_assignment.pl0",
        "feature_begin_end_sequence.pl0",
        "feature_if_then_relops.pl0",
        "feature_while_do.pl0",
        "feature_odd_condition.pl0",
        "feature_procedure_call_nested_levels.pl0",
        "feature_unary_minus_precedence.pl0",
        "feature_parentheses.pl0",
        "feature_input_qstmt.pl0",
        "feature_output_bangstmt.pl0",
        "feature_io_roundtrip_q_bang.pl0",
        "error_undeclared_identifier.pl0",
        "error_assign_to_const.pl0",
        "error_missing_then.pl0",
        "error_missing_do.pl0",
        "error_bad_factor.pl0",
        "error_qstmt_non_variable.pl0",
        "error_bangstmt_missing_expression.pl0",
        "compat_relop_brackets.pl0",
        "compat_store_trace_output.pl0",
        "dialect_classic_reject_qstmt.pl0",
        "dialect_classic_reject_bangstmt.pl0",
        "dialect_extended_accept_qstmt.pl0",
        "dialect_extended_accept_bangstmt.pl0",
        "limit_identifier_max_length_ok.pl0",
        "limit_identifier_too_long.pl0",
        "limit_number_max_digits_ok.pl0",
        "limit_number_too_long.pl0",
        "limit_amax_exceeded.pl0",
        "limit_levmax_exceeded.pl0",
        "limit_cxmax_exceeded.pl0",
        "limit_txmax_exceeded.pl0",
        "error_duplicate_identifier_same_scope.pl0",
        "error_call_non_procedure.pl0",
        "error_missing_end.pl0",
        "error_missing_period.pl0",
        "error_multiple_errors_recovery.pl0",
        "runtime_division_by_zero.pl0",
        "io_input_negative_and_whitespace.pl0",
        "io_input_non_integer.pl0",
        "io_input_eof_before_value.pl0",
    ];

    private static readonly string[] RequiredGroups =
    [
        "valid",
        "invalid",
        "compat",
        "dialect",
        "limits",
        "runtime/io-edge",
    ];

    public static IEnumerable<object[]> Cases()
    {
        foreach (var @case in LoadCases())
        {
            yield return [@case];
        }
    }

    [Fact]
    public void All_Required_Case_Files_Are_Present()
    {
        var existing = FindAllPl0CaseFiles().ToHashSet(StringComparer.Ordinal);
        foreach (var name in RequiredCaseFileNames)
        {
            Assert.Contains(name, existing);
        }
    }

    [Fact]
    public void All_Required_Cases_Have_Expected_Artifacts()
    {
        var cases = LoadCases();
        var byName = cases.Select(c => c.Name).ToHashSet(StringComparer.Ordinal);

        foreach (var name in RequiredCaseFileNames)
        {
            Assert.Contains(name, byName);
        }

        foreach (var group in RequiredGroups)
        {
            Assert.Contains(cases, c => string.Equals(c.Group, group, StringComparison.Ordinal));
        }
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void Catalog_Case_Behavior_Matches_Expectations(CatalogCase @case)
    {
        var sourcePath = ResolveCasePath(@case.Name, @case.Folder);
        Assert.True(File.Exists(sourcePath), $"Case file not found: {sourcePath}");

        var source = File.ReadAllText(sourcePath);
        var dialect = @case.Dialect?.Equals("classic", StringComparison.OrdinalIgnoreCase) == true
            ? Pl0Dialect.Classic
            : Pl0Dialect.Extended;
        var options = new CompilerOptions(
            dialect,
            MaxLevel: @case.MaxLevel ?? 3,
            MaxAddress: @case.MaxAddress ?? 2047,
            MaxIdentifierLength: @case.MaxIdentifierLength ?? 10,
            MaxNumberDigits: @case.MaxNumberDigits ?? 14,
            MaxSymbolCount: @case.MaxSymbolCount ?? 100,
            MaxCodeLength: @case.MaxCodeLength ?? 200);

        var compilation = new Pl0Compiler().Compile(source, options);
        Assert.Equal(@case.CompileSuccess, compilation.Success);

        if (!@case.CompileSuccess)
        {
            Assert.NotEmpty(compilation.Diagnostics);
            if (@case.ExpectedCompileCodes is { Count: > 0 })
            {
                Assert.Contains(
                    compilation.Diagnostics,
                    d => @case.ExpectedCompileCodes.Contains(d.Code));
            }

            return;
        }

        if (!@case.Run)
        {
            return;
        }

        var io = CreateIo(@case);
        var vm = new VirtualMachine();
        var vmResult = vm.Run(
            compilation.Instructions,
            io,
            new VirtualMachineOptions(EnableStoreTrace: @case.StoreTrace));

        var expectedRuntimeSuccess = @case.RuntimeSuccess ?? true;
        Assert.Equal(expectedRuntimeSuccess, vmResult.Success);

        if (!expectedRuntimeSuccess)
        {
            Assert.NotEmpty(vmResult.Diagnostics);
            if (@case.ExpectedRuntimeCodes is { Count: > 0 })
            {
                Assert.Contains(
                    vmResult.Diagnostics,
                    d => @case.ExpectedRuntimeCodes.Contains(d.Code));
            }

            return;
        }

        if (@case.ExpectedOutput is { Count: > 0 })
        {
            Assert.Equal(@case.ExpectedOutput, io.Output);
        }
        else
        {
            Assert.Empty(io.Output);
        }
    }

    private static CatalogIo CreateIo(CatalogCase @case)
    {
        var behavior = @case.IoBehavior?.ToLowerInvariant() ?? "buffered";
        return behavior switch
        {
            "eof" => CatalogIo.Eof(),
            "formaterror" => CatalogIo.FormatError(),
            _ => CatalogIo.Buffered(@case.Input ?? []),
        };
    }

    private static string ResolveCasePath(string name, string? folderHint)
    {
        if (!string.IsNullOrWhiteSpace(folderHint))
        {
            var hinted = Path.Combine(RepoRoot, "tests", "data", "pl0", folderHint, name);
            if (File.Exists(hinted))
            {
                return hinted;
            }
        }

        var valid = Path.Combine(RepoRoot, "tests", "data", "pl0", "valid", name);
        if (File.Exists(valid))
        {
            return valid;
        }

        var invalid = Path.Combine(RepoRoot, "tests", "data", "pl0", "invalid", name);
        return invalid;
    }

    private static IReadOnlyList<CatalogCase> LoadCases()
    {
        var path = Path.Combine(RepoRoot, "tests", "data", "expected", "catalog", "cases.json");
        var json = File.ReadAllText(path);
        var cases = JsonSerializer.Deserialize<List<CatalogCase>>(
            json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return cases ?? [];
    }

    private static IEnumerable<string> FindAllPl0CaseFiles() =>
        Directory.EnumerateFiles(
                Path.Combine(RepoRoot, "tests", "data", "pl0"),
                "*.pl0",
                SearchOption.AllDirectories)
            .Select(Path.GetFileName)
            .Where(name => !string.IsNullOrWhiteSpace(name))
            .Select(name => name!);

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

    public sealed class CatalogCase
    {
        public string Name { get; set; } = string.Empty;

        public string Group { get; set; } = string.Empty;

        public string? Folder { get; set; }

        public string? Dialect { get; set; }

        public bool CompileSuccess { get; set; }

        public List<int>? ExpectedCompileCodes { get; set; }

        public bool Run { get; set; }

        public bool? RuntimeSuccess { get; set; }

        public List<int>? ExpectedRuntimeCodes { get; set; }

        public List<int>? Input { get; set; }

        public List<int>? ExpectedOutput { get; set; }

        public bool StoreTrace { get; set; }

        public string? IoBehavior { get; set; }

        public int? MaxLevel { get; set; }

        public int? MaxAddress { get; set; }

        public int? MaxIdentifierLength { get; set; }

        public int? MaxNumberDigits { get; set; }

        public int? MaxSymbolCount { get; set; }

        public int? MaxCodeLength { get; set; }
    }

    private sealed class CatalogIo : IPl0Io
    {
        private readonly Queue<int> _input;
        private readonly string _behavior;
        private readonly List<int> _output = [];

        private CatalogIo(IEnumerable<int> input, string behavior)
        {
            _input = new Queue<int>(input);
            _behavior = behavior;
        }

        public IReadOnlyList<int> Output => _output;

        public static CatalogIo Buffered(IEnumerable<int> input) => new(input, "buffered");

        public static CatalogIo Eof() => new([], "eof");

        public static CatalogIo FormatError() => new([], "formatError");

        public int ReadInt()
        {
            return _behavior switch
            {
                "eof" => throw new EndOfStreamException("No input available."),
                "formatError" => throw new FormatException("Input is not a valid integer."),
                _ => _input.Count > 0
                    ? _input.Dequeue()
                    : throw new EndOfStreamException("No buffered input value available."),
            };
        }

        public void WriteInt(int value)
        {
            _output.Add(value);
        }
    }
}
