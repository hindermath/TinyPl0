namespace Pl0.Cli.Cli;

/// <summary>
/// Prints CLI usage information.
/// </summary>
public static class CliHelpPrinter
{
    /// <summary>
    /// Writes usage lines to the provided writer.
    /// </summary>
    /// <param name="writer">Destination writer.</param>
    /// <param name="executableName">Executable name to display.</param>
    public static void PrintUsage(TextWriter writer, string executableName)
    {
        foreach (var line in GetUsageLines(executableName))
        {
            writer.WriteLine(line);
        }
    }

    /// <summary>
    /// Returns the usage lines for the CLI.
    /// </summary>
    /// <param name="executableName">Executable name to display.</param>
    /// <returns>List of usage lines.</returns>
    public static IReadOnlyList<string> GetUsageLines(string executableName) =>
        [
            "Usage:",
            $"{executableName} [-|/]h | [-|/]? | [-|/]help | --help",
            $"{executableName} compile <file.pl0> [--out <file.pcode>] [--emit asm|cod] [--list-code]",
            $"{executableName} run <file.pl0> [--emit asm|cod] [--list-code]",
            $"{executableName} run-pcode <file.pcode> [--list-code]",
            $"{executableName} --api",
            $"{executableName} <file.pl0> [legacy mode; compile and run]",
            "",
            "Switches:",
            "  [-|/]errmsg | --errmsg                show long compiler error messages",
            "  [-|/]wopcod | --wopcod                include numeric opcodes in listing output",
            "  --list-code                            print generated/loaded code listing",
            "  --api                                  start web server to show documentation",
            "  [-|/]conly | --conly | --compile-only compile only; do not run VM",
            "  [-|/]emit {[-|/]asm | [-|/]cod}",
            "  --emit asm|cod                         emit PL/0 code to STDOUT",
            "  --out <file>                           output path for 'compile' command",
        ];
}
