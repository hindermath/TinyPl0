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
            $"{executableName} [-|/]h | [-|/]? | [-|/]help | --help - print this information",
            $"{executableName} [-|/]errmsg | --errmsg - show long compiler error messages",
            $"{executableName} [-|/]wopcod | --wopcod - include opcodes in instruction listing",
            $"{executableName} [-|/]conly | --conly | --compile-only - compile only and do not interpret",
            $"{executableName} [-|/]emit {{[-|/]asm | [-|/]cod}}",
            $"{executableName} --emit asm|cod - emit PL0 assembler instructions or opcodes to STDOUT",
        ];
}
