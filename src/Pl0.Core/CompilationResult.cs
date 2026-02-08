namespace Pl0.Core;

public sealed class CompilationResult
{
    public CompilationResult(
        IReadOnlyList<Instruction> instructions,
        IReadOnlyList<CompilerDiagnostic> diagnostics)
    {
        Instructions = instructions;
        Diagnostics = diagnostics;
    }

    public IReadOnlyList<Instruction> Instructions { get; }

    public IReadOnlyList<CompilerDiagnostic> Diagnostics { get; }

    public bool Success => Diagnostics.Count == 0;
}
