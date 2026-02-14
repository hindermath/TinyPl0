using Pl0.Core;

namespace Pl0.Cli.Cli;

/// <summary>
/// Helper methods for compiler diagnostics and CLI exit codes.
/// </summary>
public static class CompilationDiagnostics
{
    /// <summary>
    /// Exit code for compiler errors.
    /// </summary>
    public const int CompilerErrorExitCode = 97;
    /// <summary>
    /// Exit code for incomplete programs.
    /// </summary>
    public const int ProgramIncompleteExitCode = 98;

    /// <summary>
    /// Selects the exit code for a set of diagnostics.
    /// </summary>
    /// <param name="diagnostics">Compiler diagnostics.</param>
    /// <returns>The exit code.</returns>
    public static int SelectExitCode(IReadOnlyList<CompilerDiagnostic> diagnostics) =>
        diagnostics.Any(d => d.Code == ProgramIncompleteExitCode)
            ? ProgramIncompleteExitCode
            : CompilerErrorExitCode;

    /// <summary>
    /// Formats a compiler diagnostic for CLI output.
    /// </summary>
    /// <param name="diagnostic">The diagnostic to format.</param>
    /// <param name="longMessages">Whether to include full message text.</param>
    /// <returns>The formatted diagnostic string.</returns>
    public static string FormatCompilerDiagnostic(CompilerDiagnostic diagnostic, bool longMessages)
    {
        var location = $"{diagnostic.Position.Line}:{diagnostic.Position.Column}";
        return longMessages
            ? $"Error {diagnostic.Code} at {location}: {diagnostic.Message}"
            : $"Error {diagnostic.Code} at {location}.";
    }
}
