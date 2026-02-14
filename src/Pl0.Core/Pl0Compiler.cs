namespace Pl0.Core;

/// <summary>
/// Compiles PL/0 source text into P-Code instructions and diagnostics.
/// </summary>
public sealed class Pl0Compiler
{
    /// <summary>
    /// Compiles a PL/0 source string using the provided options.
    /// </summary>
    /// <param name="source">PL/0 source code to compile.</param>
    /// <param name="options">Optional compiler options; defaults are used when null.</param>
    /// <returns>The compilation result containing instructions and diagnostics.</returns>
    public CompilationResult Compile(string source, CompilerOptions? options = null)
    {
        var effectiveOptions = options ?? CompilerOptions.Default;

        var lexResult = new Pl0Lexer(source, effectiveOptions).Lex();
        var diagnostics = new List<CompilerDiagnostic>(
            lexResult.Diagnostics.Select(
                d => new CompilerDiagnostic(d.Code, d.Message, d.Position)));

        if (diagnostics.Count > 0)
        {
            return new CompilationResult([], diagnostics);
        }

        var parser = new Pl0Parser(lexResult.Tokens, effectiveOptions);
        var parseResult = parser.Parse();
        if (parseResult.Diagnostics.Count > 0)
        {
            diagnostics.AddRange(parseResult.Diagnostics);
        }

        return new CompilationResult(parseResult.Instructions, diagnostics);
    }
}
