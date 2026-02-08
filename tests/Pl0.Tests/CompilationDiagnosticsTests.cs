using Pl0.Cli.Cli;
using Pl0.Core;

namespace Pl0.Tests;

public sealed class CompilationDiagnosticsTests
{
    [Fact]
    public void Formats_Short_Compiler_Diagnostic_Without_Long_Message_Text()
    {
        var diagnostic = new CompilerDiagnostic(11, "Undeclared identifier.", new TextPosition(2, 5, 12));

        var text = CompilationDiagnostics.FormatCompilerDiagnostic(diagnostic, longMessages: false);

        Assert.Equal("Error 11 at 2:5.", text);
    }

    [Fact]
    public void Formats_Long_Compiler_Diagnostic_With_Message_Text()
    {
        var diagnostic = new CompilerDiagnostic(11, "Undeclared identifier.", new TextPosition(2, 5, 12));

        var text = CompilationDiagnostics.FormatCompilerDiagnostic(diagnostic, longMessages: true);

        Assert.Equal("Error 11 at 2:5: Undeclared identifier.", text);
    }

    [Fact]
    public void Selects_ExitCode98_When_Program_Incomplete_Diagnostic_Is_Present()
    {
        var diagnostics = new List<CompilerDiagnostic>
        {
            new(17, "Semicolon or 'end' expected.", new TextPosition(3, 1, 20)),
            new(98, "Program incomplete: unexpected end of input.", new TextPosition(3, 1, 20)),
        };

        var exitCode = CompilationDiagnostics.SelectExitCode(diagnostics);

        Assert.Equal(98, exitCode);
    }
}
