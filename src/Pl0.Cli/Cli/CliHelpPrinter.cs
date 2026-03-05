using System.Globalization;
using System.Resources;

namespace Pl0.Cli.Cli;

/// <summary>
/// Gibt CLI-Hilfetexte aus.
/// </summary>
public static class CliHelpPrinter
{
    /// <summary>
    /// Schreibt Verwendungszeilen in den angegebenen Writer.
    /// </summary>
    /// <param name="writer">Ziel-Writer.</param>
    /// <param name="executableName">Anzeigename der ausführbaren Datei.</param>
    public static void PrintUsage(TextWriter writer, string executableName)
    {
        foreach (var line in GetUsageLines(executableName))
        {
            writer.WriteLine(line);
        }
    }

    /// <summary>
    /// Schreibt Verwendungszeilen in den angegebenen Writer mit lokalisiertem ResourceManager.
    /// </summary>
    /// <param name="writer">Ziel-Writer.</param>
    /// <param name="executableName">Anzeigename der ausführbaren Datei.</param>
    /// <param name="rm">ResourceManager für Hilfetexte.</param>
    /// <param name="culture">Zielsprache.</param>
    public static void PrintUsage(TextWriter writer, string executableName, ResourceManager rm, CultureInfo culture)
    {
        foreach (var line in GetUsageLines(executableName, rm, culture))
        {
            writer.WriteLine(line);
        }
    }

    /// <summary>
    /// Gibt die Verwendungszeilen für die CLI zurück (Standardsprache Deutsch).
    /// </summary>
    /// <param name="executableName">Anzeigename der ausführbaren Datei.</param>
    /// <returns>Liste der Verwendungszeilen.</returns>
    public static IReadOnlyList<string> GetUsageLines(string executableName)
        => GetUsageLines(executableName, Pl0CliMessages.ResourceManager, CultureInfo.InvariantCulture);

    /// <summary>
    /// Gibt die Verwendungszeilen für die CLI zurück, lokalisiert über den angegebenen ResourceManager.
    /// </summary>
    /// <param name="executableName">Anzeigename der ausführbaren Datei.</param>
    /// <param name="rm">ResourceManager für Hilfetexte.</param>
    /// <param name="culture">Zielsprache.</param>
    /// <returns>Liste der Verwendungszeilen.</returns>
    public static IReadOnlyList<string> GetUsageLines(string executableName, ResourceManager rm, CultureInfo culture)
    {
        string M(string key) => rm.GetString(key, culture) ?? key;
        string MF(string key) => string.Format(culture, M(key), executableName);

        return
        [
            M("Cli_Help_UsageHeader"),
            MF("Cli_Help_HelpLine"),
            MF("Cli_Help_CompileLine"),
            MF("Cli_Help_RunLine"),
            MF("Cli_Help_RunPcodeLine"),
            MF("Cli_Help_RunPcodeDirectLine"),
            MF("Cli_Help_ApiLine"),
            MF("Cli_Help_LegacyLine"),
            "",
            M("Cli_Help_SwitchesHeader"),
            M("Cli_Help_SwitchErrmsg"),
            M("Cli_Help_SwitchWopcod"),
            M("Cli_Help_SwitchListCode"),
            M("Cli_Help_SwitchApi"),
            M("Cli_Help_SwitchConly"),
            M("Cli_Help_SwitchEmitFormat"),
            M("Cli_Help_SwitchEmit"),
            M("Cli_Help_SwitchOut"),
            M("Cli_Help_SwitchLang"),
        ];
    }
}
