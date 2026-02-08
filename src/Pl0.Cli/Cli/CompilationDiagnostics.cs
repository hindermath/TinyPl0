using Pl0.Core;

namespace Pl0.Cli.Cli;

public static class CompilationDiagnostics
{
    public const int CompilerErrorExitCode = 97;
    public const int ProgramIncompleteExitCode = 98;

    public static int SelectExitCode(IReadOnlyList<CompilerDiagnostic> diagnostics) =>
        diagnostics.Any(d => d.Code == ProgramIncompleteExitCode)
            ? ProgramIncompleteExitCode
            : CompilerErrorExitCode;

    public static string FormatCompilerDiagnostic(CompilerDiagnostic diagnostic, bool longMessages)
    {
        var location = $"{diagnostic.Position.Line}:{diagnostic.Position.Column}";
        return longMessages
            ? $"Error {diagnostic.Code} at {location}: {diagnostic.Message}"
            : $"Error {diagnostic.Code} at {location}.";
    }
}
