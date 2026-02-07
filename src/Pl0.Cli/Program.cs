using Pl0.Cli.Cli;
using Pl0.Core;

const int HelpExitCode = 99;
const int CompilerErrorExitCode = 97;
const int UnexpectedTerminationExitCode = 99;

var parser = new CliOptionsParser();
var result = parser.Parse(args);

var executableName = Path.GetFileName(Environment.GetCommandLineArgs()[0]);

if (result.Options.ShowHelp)
{
    CliHelpPrinter.PrintUsage(Console.Error, executableName);
    Environment.ExitCode = HelpExitCode;
    return;
}

if (result.HasErrors)
{
    foreach (var diagnostic in result.Diagnostics)
    {
        Console.Error.WriteLine($"Error {diagnostic.Code}: {diagnostic.Message}");
    }

    Console.Error.WriteLine("Use --help for usage.");
    Environment.ExitCode = result.ExitCode;
    return;
}

if (string.IsNullOrWhiteSpace(result.Options.SourcePath))
{
    Console.WriteLine("TinyPl0 Phase 2 lexer scaffold ready. Provide a .pl0 file path to lex.");
    return;
}

if (!File.Exists(result.Options.SourcePath))
{
    Console.Error.WriteLine($"Error {UnexpectedTerminationExitCode}: Source file not found: {result.Options.SourcePath}");
    Environment.ExitCode = UnexpectedTerminationExitCode;
    return;
}

var source = await File.ReadAllTextAsync(result.Options.SourcePath);
var lexer = new Pl0Lexer(source);
var lexResult = lexer.Lex();

if (lexResult.Diagnostics.Count > 0)
{
    foreach (var diagnostic in lexResult.Diagnostics)
    {
        Console.Error.WriteLine(
            $"Error {diagnostic.Code} at {diagnostic.Position.Line}:{diagnostic.Position.Column}: {diagnostic.Message}");
    }

    Environment.ExitCode = CompilerErrorExitCode;
    return;
}

Console.WriteLine($"Lexed {lexResult.Tokens.Count} tokens.");
