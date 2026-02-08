using Pl0.Core;

namespace Pl0.Tests;

public sealed class ParserDiagnosticsTests
{
    [Fact]
    public void Assignment_To_Constant_Reports_Error12()
    {
        const string source = """
                              const x = 1;
                              begin
                                x := 2
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 12);
    }

    [Fact]
    public void Call_On_Variable_Reports_Error15()
    {
        const string source = """
                              var x;
                              begin
                                call x
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 15);
    }

    [Fact]
    public void Missing_Then_Reports_Error16()
    {
        const string source = """
                              var x;
                              begin
                                if x = 1 ! x
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 16);
    }

    [Fact]
    public void Missing_Do_Reports_Error18()
    {
        const string source = """
                              var x;
                              begin
                                while x < 1 ! x
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 18);
    }

    [Fact]
    public void Procedure_Identifier_In_Expression_Reports_Error21()
    {
        const string source = """
                              procedure p;
                              begin
                              end;
                              begin
                                ! p
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 21);
    }

    [Fact]
    public void Duplicate_Identifier_In_Same_Scope_Reports_Error31()
    {
        const string source = """
                              var x, x;
                              begin
                              end.
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 31);
    }

    [Fact]
    public void Incomplete_Program_Reports_Error98()
    {
        const string source = """
                              var x;
                              begin
                                x := 1
                              """;

        var result = new Pl0Compiler().Compile(source, CompilerOptions.Default);

        Assert.False(result.Success);
        Assert.Contains(result.Diagnostics, d => d.Code == 98);
    }
}
