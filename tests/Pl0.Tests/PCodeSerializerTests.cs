using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class PCodeSerializerTests
{
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
}
