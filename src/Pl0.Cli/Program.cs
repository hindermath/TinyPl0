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
var compiler = new Pl0Compiler();
var compilation = compiler.Compile(source, CompilerOptions.Default);

if (!compilation.Success)
{
    foreach (var diagnostic in compilation.Diagnostics)
    {
        Console.Error.WriteLine(
            $"Error {diagnostic.Code} at {diagnostic.Position.Line}:{diagnostic.Position.Column}: {diagnostic.Message}");
    }

    Environment.ExitCode = CompilerErrorExitCode;
    return;
}

if (result.Options.EmitRequested)
{
    foreach (var instruction in compilation.Instructions)
    {
        if (result.Options.EmitMode == EmitMode.Cod)
        {
            Console.WriteLine($"{(int)instruction.Op} {instruction.Level} {instruction.Argument}");
        }
        else
        {
            Console.WriteLine($"{instruction.Op.ToString().ToLowerInvariant()} {instruction.Level} {instruction.Argument}");
        }
    }
}
else
{
    Console.WriteLine($"Compiled {compilation.Instructions.Count} instructions.");
}
