using Pl0.Cli.Cli;

const int HelpExitCode = 99;

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

Console.WriteLine("TinyPl0 Phase 1 scaffold ready.");
