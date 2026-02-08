namespace Pl0.Cli.Cli;

public static class CliHelpPrinter
{
    public static void PrintUsage(TextWriter writer, string executableName)
    {
        foreach (var line in GetUsageLines(executableName))
        {
            writer.WriteLine(line);
        }
    }

    public static IReadOnlyList<string> GetUsageLines(string executableName) =>
        [
            "Usage:",
            $"{executableName} [-|/]h | [-|/]? | [-|/]help | --help",
            $"{executableName} compile <file.pl0> [--out <file.pcode>] [--emit asm|cod] [--list-code]",
            $"{executableName} run <file.pl0> [--emit asm|cod] [--list-code]",
            $"{executableName} run-pcode <file.pcode> [--list-code]",
            $"{executableName} <file.pl0> [legacy mode; compile and run]",
            "",
            "Switches:",
            "  [-|/]errmsg | --errmsg                show long compiler error messages",
            "  [-|/]wopcod | --wopcod                include numeric opcodes in listing output",
            "  --list-code                            print generated/loaded code listing",
            "  [-|/]conly | --conly | --compile-only compile only; do not run VM",
            "  [-|/]emit {[-|/]asm | [-|/]cod}",
            "  --emit asm|cod                         emit PL/0 code to STDOUT",
            "  --out <file>                           output path for 'compile' command",
        ];
}
