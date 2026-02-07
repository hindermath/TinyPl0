using Pl0.Cli.Cli;

namespace Pl0.Tests;

public sealed class CliOptionsParserTests
{
    private readonly CliOptionsParser _sut = new();

    [Theory]
    [InlineData("-h")]
    [InlineData("-?")]
    [InlineData("-help")]
    [InlineData("/h")]
    [InlineData("/?")]
    [InlineData("/help")]
    [InlineData("--help")]
    public void Parses_Help_Aliases(string arg)
    {
        var result = _sut.Parse([arg]);

        Assert.True(result.Options.ShowHelp);
        Assert.False(result.HasErrors);
    }

    [Theory]
    [InlineData("-errmsg")]
    [InlineData("/errmsg")]
    [InlineData("--errmsg")]
    public void Parses_ErrMsg_Aliases(string arg)
    {
        var result = _sut.Parse([arg]);

        Assert.True(result.Options.LongErrorMessages);
        Assert.False(result.HasErrors);
    }

    [Theory]
    [InlineData("-wopcod")]
    [InlineData("/wopcod")]
    [InlineData("--wopcod")]
    public void Parses_Wopcod_Aliases(string arg)
    {
        var result = _sut.Parse([arg]);

        Assert.True(result.Options.WriteOpcodesInListing);
        Assert.False(result.HasErrors);
    }

    [Theory]
    [InlineData("-conly")]
    [InlineData("/conly")]
    [InlineData("--conly")]
    [InlineData("--compile-only")]
    public void Parses_CompileOnly_Aliases(string arg)
    {
        var result = _sut.Parse([arg]);

        Assert.True(result.Options.CompileOnly);
        Assert.False(result.HasErrors);
    }

    [Theory]
    [InlineData("--emit", "asm")]
    [InlineData("--emit", "cod")]
    [InlineData("-emit", "-asm")]
    [InlineData("/emit", "/cod")]
    public void Parses_Emit_With_Mode(string emitSwitch, string modeSwitch)
    {
        var result = _sut.Parse([emitSwitch, modeSwitch]);

        Assert.True(result.Options.EmitRequested);
        Assert.Equal(
            modeSwitch.Contains("asm", StringComparison.OrdinalIgnoreCase)
                ? EmitMode.Asm
                : EmitMode.Cod,
            result.Options.EmitMode);
        Assert.False(result.HasErrors);
    }

    [Theory]
    [InlineData("--emit=asm", EmitMode.Asm)]
    [InlineData("--emit=cod", EmitMode.Cod)]
    public void Parses_Emit_Equals_Value(string arg, EmitMode expectedMode)
    {
        var result = _sut.Parse([arg]);

        Assert.True(result.Options.EmitRequested);
        Assert.Equal(expectedMode, result.Options.EmitMode);
        Assert.False(result.HasErrors);
    }

    [Fact]
    public void Emit_Without_Mode_Returns_Error96()
    {
        var result = _sut.Parse(["--emit"]);

        Assert.True(result.HasErrors);
        Assert.Equal(96, result.ExitCode);
    }

    [Fact]
    public void Unknown_Switch_Returns_Error99()
    {
        var result = _sut.Parse(["--unknown"]);

        Assert.True(result.HasErrors);
        Assert.Equal(99, result.ExitCode);
    }

    [Fact]
    public void Detects_Conflicting_Emit_Modes()
    {
        var result = _sut.Parse(["--emit", "asm", "--cod"]);

        Assert.True(result.HasErrors);
        Assert.Equal(99, result.ExitCode);
    }

    [Fact]
    public void Parses_Source_Path()
    {
        var result = _sut.Parse(["sample.pl0"]);

        Assert.Equal("sample.pl0", result.Options.SourcePath);
        Assert.False(result.HasErrors);
    }
}
