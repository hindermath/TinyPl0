namespace Pl0.Core;

/// <summary>
/// Result of compiling PL/0 source code.
/// </summary>
public sealed class CompilationResult
{
    /// <summary>
    /// Creates a new compilation result.
    /// </summary>
    /// <param name="instructions">Generated P-Code instructions.</param>
    /// <param name="diagnostics">Diagnostics produced during compilation.</param>
    public CompilationResult(
        IReadOnlyList<Instruction> instructions,
        IReadOnlyList<CompilerDiagnostic> diagnostics)
    {
        Instructions = instructions;
        Diagnostics = diagnostics;
    }

    /// <summary>
    /// Gets the generated P-Code instructions.
    /// </summary>
    public IReadOnlyList<Instruction> Instructions { get; }

    /// <summary>
    /// Gets the diagnostics produced during compilation.
    /// </summary>
    public IReadOnlyList<CompilerDiagnostic> Diagnostics { get; }

    /// <summary>
    /// Gets a value indicating whether compilation succeeded without diagnostics.
    /// </summary>
    public bool Success => Diagnostics.Count == 0;
}
