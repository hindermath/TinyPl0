namespace Pl0.Core;

public sealed class Pl0Compiler
{
    public CompilationResult Compile(string source, CompilerOptions? options = null)
    {
        var effectiveOptions = options ?? CompilerOptions.Default;

        var lexResult = new Pl0Lexer(source).Lex();
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
