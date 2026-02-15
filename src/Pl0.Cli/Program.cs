using Pl0.Cli.Cli;
using Pl0.Core;
using Pl0.Vm;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

const int HelpExitCode = 99;
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

if (result.Options.ShowApi)
{
    var docPath = Path.Combine(AppContext.BaseDirectory, "_site");
    if (!Directory.Exists(docPath))
    {
        Console.Error.WriteLine($"Error: Documentation directory not found at {docPath}");
        Environment.ExitCode = UnexpectedTerminationExitCode;
        return;
    }

    var builder = WebApplication.CreateBuilder(new WebApplicationOptions
    {
        ContentRootPath = docPath,
        WebRootPath = docPath
    });
    builder.WebHost.UseUrls("http://localhost:5000");
    var app = builder.Build();
    
    var provider = new FileExtensionContentTypeProvider();
    provider.Mappings[".pl0"] = "text/plain";

    app.UseDefaultFiles();
    app.UseStaticFiles(new StaticFileOptions
    {
        ContentTypeProvider = provider,
        OnPrepareResponse = ctx =>
        {
            if (ctx.File.Name.EndsWith(".pl0", StringComparison.OrdinalIgnoreCase))
            {
                var fileName = ctx.File.Name;
                var requestPath = ctx.Context.Request.Path.Value;
                if (!string.IsNullOrEmpty(requestPath))
                {
                    var parts = requestPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2 && string.Equals(parts[^1], "program.pl0", StringComparison.OrdinalIgnoreCase))
                    {
                        var folderName = parts[^2];
                        if (string.Equals(folderName, "example-1", StringComparison.OrdinalIgnoreCase) && parts.Length >= 3)
                        {
                            folderName = parts[^3];
                        }
                        fileName = folderName + ".pl0";
                    }
                }
                ctx.Context.Response.Headers.ContentDisposition = "attachment; filename=\"" + fileName + "\"";
            }
        }
    });

    Console.WriteLine("Documentation server started.");
    Console.WriteLine("Click here to view documentation: http://localhost:5000");
    Console.WriteLine("Press Ctrl+C to stop the server.");

    await app.RunAsync();
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
            Console.Error.WriteLine(CompilationDiagnostics.FormatCompilerDiagnostic(
                diagnostic,
                result.Options.LongErrorMessages));
        }

        Environment.ExitCode = CompilationDiagnostics.SelectExitCode(compilation.Diagnostics);
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
