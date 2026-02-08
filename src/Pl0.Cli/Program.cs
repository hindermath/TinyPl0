using Pl0.Cli.Cli;
using Pl0.Core;
using Pl0.Vm;

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
    Console.WriteLine("TinyPl0 Phase 5 CLI ready. Use 'compile', 'run', or 'run-pcode'.");
    return;
}

if (!File.Exists(result.Options.SourcePath))
{
    Console.Error.WriteLine($"Error {UnexpectedTerminationExitCode}: Source file not found: {result.Options.SourcePath}");
    Environment.ExitCode = UnexpectedTerminationExitCode;
    return;
}

IReadOnlyList<Instruction> instructions;
if (result.Options.Command == CliCommand.RunPCode)
{
    try
    {
        var pcodeText = await File.ReadAllTextAsync(result.Options.SourcePath);
        instructions = PCodeSerializer.Parse(pcodeText);
    }
    catch (FormatException ex)
    {
        Console.Error.WriteLine($"Error {UnexpectedTerminationExitCode}: {ex.Message}");
        Environment.ExitCode = UnexpectedTerminationExitCode;
        return;
    }
}
else
{
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

    instructions = compilation.Instructions;

    if (result.Options.Command == CliCommand.Compile && !string.IsNullOrWhiteSpace(result.Options.OutputPath))
    {
        var outputDir = Path.GetDirectoryName(result.Options.OutputPath);
        if (!string.IsNullOrWhiteSpace(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        await File.WriteAllTextAsync(result.Options.OutputPath, PCodeSerializer.ToAsm(instructions));
    }
}

if (result.Options.ListCode)
{
    for (var i = 0; i < instructions.Count; i++)
    {
        var instruction = instructions[i];
        if (result.Options.WriteOpcodesInListing)
        {
            Console.Error.WriteLine(
                $"{i,5}|{(int)instruction.Op,5}{instruction.Level,3}{instruction.Argument,5}|{instruction.Op.ToString().ToLowerInvariant(),5}{instruction.Level,3}{instruction.Argument,5}");
        }
        else
        {
            Console.Error.WriteLine($"{i,5} {instruction.Op.ToString().ToLowerInvariant(),5}{instruction.Level,3}{instruction.Argument,5}");
        }
    }
}

if (result.Options.EmitRequested)
{
    var payload = result.Options.EmitMode == EmitMode.Cod
        ? PCodeSerializer.ToCod(instructions)
        : PCodeSerializer.ToAsm(instructions);
    if (!string.IsNullOrWhiteSpace(payload))
    {
        Console.WriteLine(payload);
    }
}

var shouldRun = result.Options.Command switch
{
    CliCommand.Compile => false,
    CliCommand.RunPCode => !result.Options.CompileOnly,
    CliCommand.Run => !result.Options.CompileOnly,
    CliCommand.None => !result.Options.CompileOnly,
    _ => false,
};

if (shouldRun)
{
    var vm = new VirtualMachine();
    var vmResult = vm.Run(instructions, new ConsolePl0Io(), VirtualMachineOptions.Default);
    if (!vmResult.Success)
    {
        foreach (var diagnostic in vmResult.Diagnostics)
        {
            Console.Error.WriteLine($"Runtime {diagnostic.Code}: {diagnostic.Message}");
        }

        Environment.ExitCode = vmResult.Diagnostics[0].Code;
        return;
    }
}

if (result.Options.Command == CliCommand.Compile && !result.Options.EmitRequested)
{
    if (!string.IsNullOrWhiteSpace(result.Options.OutputPath))
    {
        Console.WriteLine($"Compiled {instructions.Count} instructions to '{result.Options.OutputPath}'.");
    }
    else
    {
        Console.WriteLine($"Compiled {instructions.Count} instructions.");
    }
}
