using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class PCodeSerializerTests
{
    public static TheoryData<string> CurrentIntMnemonics => new() { "int", "Int", "INT" };

    public static TheoryData<string> HistoricalIncMnemonics => new() { "Inc", "inc", "iNc" };

    public static TheoryData<string> InvalidNearMissMnemonics => new() { "Incc", "inta", "imc" };

    [Fact]
    public void Roundtrip_Asm_Serialization_Preserves_Instructions()
    {
        var original = new[]
        {
            new Instruction(Opcode.Jmp, 0, 1),
            new Instruction(Opcode.Int, 0, 4),
            new Instruction(Opcode.Lit, 0, 10),
            new Instruction(Opcode.Opr, 0, 0),
        };

        var asm = PCodeSerializer.ToAsm(original);
        var parsed = PCodeSerializer.Parse(asm);

        Assert.Equal(original, parsed);
    }

    [Fact]
    public void Roundtrip_Cod_Serialization_Preserves_Instructions()
    {
        var original = new[]
        {
            new Instruction(Opcode.Lit, 0, 5),
            new Instruction(Opcode.Lit, 0, 6),
            new Instruction(Opcode.Opr, 0, 2),
        };

        var cod = PCodeSerializer.ToCod(original);
        var parsed = PCodeSerializer.Parse(cod);

        Assert.Equal(original, parsed);
    }

    [Fact]
    public void EndToEnd_Source_ToPCode_ToVm_Works()
    {
        var sourcePath = Path.Combine(RepoRoot, "tests", "data", "pl0", "valid", "feature_io_q_bang_relops.pl0");
        var source = File.ReadAllText(sourcePath);
        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var asm = PCodeSerializer.ToAsm(compilation.Instructions);
        var loaded = PCodeSerializer.Parse(asm);

        var io = new BufferedPl0Io([5]);
        var vmResult = new VirtualMachine().Run(loaded, io);
        Assert.True(vmResult.Success);
        Assert.Equal([5, 5, 5], io.Output);
    }

    [Theory]
    [MemberData(nameof(CurrentIntMnemonics))]
    public void Parse_Current_Int_Mnemonics_Produces_Int_Instruction(string mnemonic)
    {
        var parsed = ParseSingleInstruction($"{mnemonic} 0 4");

        Assert.Equal(new Instruction(Opcode.Int, 0, 4), parsed);
    }

    [Fact]
    public void ToAsm_Uses_Canonical_Int_Mnemonic_For_Int_Instruction()
    {
        var asm = PCodeSerializer.ToAsm([new Instruction(Opcode.Int, 0, 4)]);

        Assert.Equal("int 0 4", asm);
    }

    [Theory]
    [MemberData(nameof(HistoricalIncMnemonics))]
    public void Parse_Historical_Inc_Mnemonics_Produces_Int_Instruction(string mnemonic)
    {
        var parsed = ParseSingleInstruction($"{mnemonic} 0 4");

        Assert.Equal(new Instruction(Opcode.Int, 0, 4), parsed);
    }

    [Theory]
    [MemberData(nameof(HistoricalIncMnemonics))]
    public void Historical_Inc_Mnemonics_Run_Like_Int_Programs(string mnemonic)
    {
        var historicalProgram = ParseProgram($"""
            {mnemonic} 0 3
            Lit 0 7
            Opr 0 15
            Opr 0 0
            """);
        var currentProgram = ParseProgram("""
            Int 0 3
            Lit 0 7
            Opr 0 15
            Opr 0 0
            """);

        Assert.Equal(currentProgram, historicalProgram);
        Assert.Equal(ExecuteProgram(currentProgram), ExecuteProgram(historicalProgram));
    }

    [Theory]
    [MemberData(nameof(HistoricalIncMnemonics))]
    public void ToAsm_Serializes_Historical_Inc_Parse_Result_As_Canonical_Int(string mnemonic)
    {
        var parsed = ParseProgram($"""
            {mnemonic} 0 4
            Lit 0 1
            Opr 0 0
            """);

        var asm = PCodeSerializer.ToAsm(parsed);

        Assert.StartsWith("int 0 4", asm, StringComparison.Ordinal);
    }

    [Theory]
    [MemberData(nameof(InvalidNearMissMnemonics))]
    public void Parse_Invalid_NearMiss_Mnemonics_Throws_FormatException(string mnemonic)
    {
        var exception = Assert.Throws<FormatException>(() => PCodeSerializer.Parse($"{mnemonic} 0 4"));

        Assert.Contains("Unknown mnemonic opcode", exception.Message, StringComparison.Ordinal);
        Assert.Contains(mnemonic, exception.Message, StringComparison.Ordinal);
    }

    private static string RepoRoot => FindRepoRoot();

    private static Instruction ParseSingleInstruction(string text) =>
        Assert.Single(PCodeSerializer.Parse(text));

    private static IReadOnlyList<Instruction> ParseProgram(string text) => PCodeSerializer.Parse(text);

    private static IReadOnlyList<int> ExecuteProgram(IReadOnlyList<Instruction> instructions)
    {
        var io = new BufferedPl0Io();
        var vmResult = new VirtualMachine().Run(instructions, io);

        Assert.True(vmResult.Success);
        return io.Output.ToArray();
    }

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
}
