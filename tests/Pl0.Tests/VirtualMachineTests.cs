using Pl0.Core;
using Pl0.Vm;

namespace Pl0.Tests;

public sealed class VirtualMachineTests
{
    [Fact]
    public void EndToEnd_Run_With_Input_And_Output_Works()
    {
        var source = File.ReadAllText(
            Path.Combine(RepoRoot, "tests", "data", "pl0", "valid", "feature_io_q_bang_relops.pl0"));

        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var io = new BufferedPl0Io([5]);
        var vm = new VirtualMachine();
        var result = vm.Run(compilation.Instructions, io);

        Assert.True(result.Success);
        Assert.Equal([5, 5, 5], io.Output);
    }

    [Fact]
    public void While_Loop_Executes_Correctly()
    {
        const string source = """
                              var x;
                              begin
                              x := 0;
                              while x < 3 do
                              begin
                                ! x;
                                x := x + 1
                              end
                              end.
                              """;

        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var io = new BufferedPl0Io();
        var result = new VirtualMachine().Run(compilation.Instructions, io);

        Assert.True(result.Success);
        Assert.Equal([0, 1, 2], io.Output);
    }

    [Fact]
    public void Procedure_Call_Updates_Outer_Variable()
    {
        const string source = """
                              var x;
                              procedure p;
                              begin
                                x := x + 1
                              end;
                              begin
                                x := 0;
                                call p;
                                call p;
                                ! x
                              end.
                              """;

        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var io = new BufferedPl0Io();
        var result = new VirtualMachine().Run(compilation.Instructions, io);

        Assert.True(result.Success);
        Assert.Equal([2], io.Output);
    }

    [Fact]
    public void Division_By_Zero_Returns_Runtime_Diagnostic()
    {
        const string source = """
                              var x;
                              begin
                                x := 10 / (5 - 5);
                                ! x
                              end.
                              """;

        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var result = new VirtualMachine().Run(compilation.Instructions, new BufferedPl0Io());

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 206);
    }

    [Fact]
    public void Input_Eof_Returns_Runtime_Diagnostic()
    {
        const string source = """
                              var x;
                              begin
                                ? x
                              end.
                              """;

        var compilation = new Pl0Compiler().Compile(source, CompilerOptions.Default);
        Assert.True(compilation.Success);

        var io = new BufferedPl0Io();
        var result = new VirtualMachine().Run(compilation.Instructions, io);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 98);
    }

    [Fact]
    public void Store_Trace_Option_Writes_Assigned_Value()
    {
        var program = new[]
        {
            new Instruction(Opcode.Jmp, 0, 1),
            new Instruction(Opcode.Int, 0, 4),
            new Instruction(Opcode.Lit, 0, 7),
            new Instruction(Opcode.Sto, 0, 3),
            new Instruction(Opcode.Opr, 0, 0),
        };

        var io = new BufferedPl0Io();
        var options = new VirtualMachineOptions(EnableStoreTrace: true);
        var result = new VirtualMachine().Run(program, io, options);

        Assert.True(result.Success);
        Assert.Equal([7], io.Output);
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
