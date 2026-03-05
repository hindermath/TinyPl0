using System.Globalization;
using System.Resources;

namespace Pl0.Cli.Cli;

/// <summary>
/// Parst CLI-Argumente in Compiler-Optionen und Diagnosen.
/// </summary>
public sealed class CliOptionsParser
{
    /// <summary>
    /// Exit-Code wenn ein Emit-Modus angefordert, aber nicht angegeben wurde.
    /// </summary>
    private const int EmitModeMissingExitCode = 96;
    /// <summary>
    /// Exit-Code für unerwartete Argumente oder Schalter.
    /// </summary>
    private const int UnexpectedTerminationExitCode = 99;

    /// <summary>
    /// Ausgabe-Stream für Fallback-Warnungen (z. B. unbekannter Sprachcode).
    /// </summary>
    private readonly TextWriter _errorOutput;
    /// <summary>
    /// ResourceManager für CLI-Meldungen; ermöglicht Dependency Injection in Tests.
    /// </summary>
    private readonly ResourceManager _cliMessages;

    /// <summary>
    /// Erstellt einen CliOptionsParser mit optionaler Dependency Injection.
    /// </summary>
    /// <param name="errorOutput">Optionaler TextWriter für Fallback-Warnungen; Standard: Console.Error.</param>
    /// <param name="cliMessages">Optionaler ResourceManager für CLI-Meldungen; Standard: Pl0CliMessages.ResourceManager.</param>
    public CliOptionsParser(TextWriter? errorOutput = null, ResourceManager? cliMessages = null)
    {
        _errorOutput = errorOutput ?? Console.Error;
        _cliMessages = cliMessages ?? Pl0CliMessages.ResourceManager;
    }

    /// <summary>
    /// Parst die übergebene Argumentliste.
    /// </summary>
    /// <param name="args">CLI-Argumente.</param>
    /// <returns>Parse-Ergebnis mit Optionen und Diagnosen.</returns>
    public CliParseResult Parse(IReadOnlyList<string> args)
    {
        var diagnostics = new List<CliDiagnostic>();
        var options = new CompilerCliOptions();

        var emitRequested = false;
        var emitMode = EmitMode.None;
        var showHelp = false;
        var longErrorMessages = false;
        var writeOpcodesInListing = false;
        var listCode = false;
        var compileOnly = false;
        var showApi = false;
        var command = CliCommand.None;
        string? sourcePath = null;
        string? outputPath = null;
        var language = "de";

        for (var i = 0; i < args.Count; i++)
        {
            var arg = args[i];

            if (IsUnixAbsolutePath(arg) && !IsKnownLegacySlashSwitch(arg))
            {
                if (sourcePath is null)
                {
                    sourcePath = arg;
                    continue;
                }

                diagnostics.Add(new CliDiagnostic(
                    UnexpectedTerminationExitCode,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        _cliMessages.GetString("Cli_Err_UnexpectedPositional", CultureInfo.InvariantCulture)
                            ?? "Unexpected argument: '{0}'.",
                        arg)));
                continue;
            }

            if (!TryParseSwitch(arg, out var sw))
            {
                if (command == CliCommand.None && TryParseCommand(arg, out var parsedCommand))
                {
                    command = parsedCommand;
                    continue;
                }

                if (command == CliCommand.None && (arg.EndsWith(".cod", StringComparison.OrdinalIgnoreCase) || arg.EndsWith(".pcode", StringComparison.OrdinalIgnoreCase)))
                {
                    command = CliCommand.RunPCode;
                }

                if (sourcePath is null)
                {
                    sourcePath = arg;
                    continue;
                }

                diagnostics.Add(new CliDiagnostic(
                    UnexpectedTerminationExitCode,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        _cliMessages.GetString("Cli_Err_UnexpectedPositional", CultureInfo.InvariantCulture)
                            ?? "Unexpected argument: '{0}'.",
                        arg)));
                continue;
            }

            if (IsHelpSwitch(sw))
            {
                showHelp = true;
                continue;
            }

            switch (sw)
            {
                case "errmsg":
                    longErrorMessages = true;
                    break;
                case "wopcod":
                    writeOpcodesInListing = true;
                    break;
                case "list-code":
                    listCode = true;
                    break;
                case "conly":
                case "compile-only":
                    compileOnly = true;
                    break;
                case "api":
                    showApi = true;
                    break;
                case "out":
                    if (TryReadOptionValue(args, ref i, out var outValue))
                    {
                        outputPath = outValue;
                    }
                    else
                    {
                        diagnostics.Add(new CliDiagnostic(
                            UnexpectedTerminationExitCode,
                            _cliMessages.GetString("Cli_Err_MissingValueForOut", CultureInfo.InvariantCulture)
                                ?? "Missing value for '--out'."));
                    }

                    break;
                case "lang":
                    if (TryReadOptionValue(args, ref i, out var langValue))
                    {
                        language = ParseLanguageCode(langValue);
                    }
                    break;
                case "emit":
                    emitRequested = true;
                    if (i + 1 < args.Count && TryParseEmitModeFromToken(args[i + 1], out var nextEmitMode))
                    {
                        i++;
                        emitMode = MergeEmitMode(emitMode, nextEmitMode, diagnostics);
                    }
                    break;
                case "asm":
                    emitMode = MergeEmitMode(emitMode, EmitMode.Asm, diagnostics);
                    break;
                case "cod":
                    emitMode = MergeEmitMode(emitMode, EmitMode.Cod, diagnostics);
                    break;
                default:
                    if (TryParseEmitEqualsValue(sw, out var valueMode))
                    {
                        emitRequested = true;
                        emitMode = MergeEmitMode(emitMode, valueMode, diagnostics);
                        break;
                    }

                    diagnostics.Add(new CliDiagnostic(
                        UnexpectedTerminationExitCode,
                        string.Format(
                            CultureInfo.InvariantCulture,
                            _cliMessages.GetString("Cli_Err_UnknownSwitch", CultureInfo.InvariantCulture)
                                ?? "Unknown switch: '{0}'.",
                            arg)));
                    break;
            }
        }

        if (emitRequested && emitMode == EmitMode.None)
        {
            diagnostics.Add(new CliDiagnostic(
                EmitModeMissingExitCode,
                _cliMessages.GetString("Cli_Err_NoEmitMode", CultureInfo.InvariantCulture)
                    ?? "No emit mode specified. Use '--emit asm' or '--emit cod'."));
        }

        if (command == CliCommand.Compile)
        {
            compileOnly = true;
            if (sourcePath is not null && string.IsNullOrWhiteSpace(outputPath))
            {
                outputPath = Path.ChangeExtension(sourcePath, ".pcode");
            }
        }

        options = new CompilerCliOptions
        {
            Command = command,
            ShowHelp = showHelp,
            LongErrorMessages = longErrorMessages,
            WriteOpcodesInListing = writeOpcodesInListing,
            ListCode = listCode,
            CompileOnly = compileOnly,
            EmitRequested = emitRequested,
            EmitMode = emitMode,
            ShowApi = showApi,
            SourcePath = sourcePath,
            OutputPath = outputPath,
            Language = language,
        };

        return new CliParseResult(options, diagnostics);
    }

    /// <summary>
    /// Gibt den ResourceManager für CLI-Meldungen zurück (für CliHelpPrinter).
    /// </summary>
    internal ResourceManager CliMessages => _cliMessages;

    /// <summary>
    /// Parst einen Sprachcode und fällt bei ungültigem Code auf "de" zurück.
    /// </summary>
    /// <param name="code">BCP-47-Sprachcode.</param>
    /// <returns>Gültiger Sprachcode oder "de".</returns>
    private string ParseLanguageCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return "de";
        }

        try
        {
            // predefinedOnly: true → plattformkonsistente Validierung auch unter ICU (macOS/Linux)
            CultureInfo.GetCultureInfo(code, predefinedOnly: true);
            return code;
        }
        catch (CultureNotFoundException)
        {
            var warning = string.Format(
                CultureInfo.InvariantCulture,
                _cliMessages.GetString("Cli_Err_UnknownLanguage", CultureInfo.InvariantCulture)
                    ?? "Unknown language code '{0}', using fallback 'de'.",
                code);
            _errorOutput.WriteLine(warning);
            return "de";
        }
    }

    /// <summary>
    /// Liest den nächsten Wert aus der Argumentliste.
    /// </summary>
    /// <param name="args">Argumentliste.</param>
    /// <param name="index">Aktueller Index (wird bei Erfolg erhöht).</param>
    /// <param name="value">Gelesener Wert.</param>
    /// <returns>True wenn ein Wert gelesen wurde.</returns>
    private static bool TryReadOptionValue(IReadOnlyList<string> args, ref int index, out string value)
    {
        value = string.Empty;
        if (index + 1 >= args.Count)
        {
            return false;
        }

        index++;
        value = args[index];
        return true;
    }

    /// <summary>
    /// Parst ein Befehls-Token.
    /// </summary>
    /// <param name="token">Befehls-Token.</param>
    /// <param name="command">Geparster Befehl.</param>
    /// <returns>True wenn ein gültiger Befehl gefunden wurde.</returns>
    private static bool TryParseCommand(string token, out CliCommand command)
    {
        command = token.ToLowerInvariant() switch
        {
            "compile" => CliCommand.Compile,
            "run" => CliCommand.Run,
            "run-pcode" => CliCommand.RunPCode,
            _ => CliCommand.None,
        };

        return command != CliCommand.None;
    }

    /// <summary>
    /// Ermittelt ob ein Schalter ein Hilfe-Schalter ist.
    /// </summary>
    /// <param name="sw">Schalter-Wert.</param>
    /// <returns>True wenn es ein Hilfe-Schalter ist.</returns>
    private static bool IsHelpSwitch(string sw) =>
        sw is "?" or "h" or "help";

    /// <summary>
    /// Prüft ob ein Wert wie ein absoluter Unix-Pfad aussieht.
    /// </summary>
    /// <param name="value">Zu prüfender Wert.</param>
    /// <returns>True wenn der Wert ein absoluter Unix-Pfad ist.</returns>
    private static bool IsUnixAbsolutePath(string value) =>
        !string.IsNullOrWhiteSpace(value) && value.Length > 1 && value[0] == '/';

    /// <summary>
    /// Ermittelt ob ein Schrägstrich-Token ein bekannter Legacy-Schalter ist.
    /// </summary>
    /// <param name="value">Schalter-Token.</param>
    /// <returns>True wenn es ein bekannter Legacy-Schalter ist.</returns>
    private static bool IsKnownLegacySlashSwitch(string value)
    {
        if (!value.StartsWith("/", StringComparison.Ordinal) || value.Length <= 1)
        {
            return false;
        }

        var sw = value[1..].ToLowerInvariant();
        if (sw is "?" or "h" or "help" or "errmsg" or "wopcod" or "conly" or "compile-only" or "list-code"
            or "out" or "emit" or "asm" or "cod" or "lang")
        {
            return true;
        }

        return TryParseEmitEqualsValue(sw, out _);
    }

    /// <summary>
    /// Parst ein Schalter-Token in einen kanonischen Schalter-String.
    /// </summary>
    /// <param name="value">Zu parsendes Token.</param>
    /// <param name="sw">Geparster Schalter-Wert.</param>
    /// <returns>True wenn das Token ein Schalter ist.</returns>
    private static bool TryParseSwitch(string value, out string sw)
    {
        sw = string.Empty;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        if (value.StartsWith("--", StringComparison.Ordinal))
        {
            if (value.Length == 2)
            {
                return false;
            }

            sw = value[2..].ToLowerInvariant();
            return true;
        }

        if (value[0] is '-' or '/')
        {
            if (value.Length == 1)
            {
                return false;
            }

            sw = value[1..].ToLowerInvariant();
            return true;
        }

        return false;
    }

    /// <summary>
    /// Versucht einen Emit-Modus aus einem Token zu parsen.
    /// </summary>
    /// <param name="token">Token-Wert.</param>
    /// <param name="mode">Geparster Emit-Modus.</param>
    /// <returns>True wenn ein Emit-Modus geparst wurde.</returns>
    private static bool TryParseEmitModeFromToken(string token, out EmitMode mode)
    {
        mode = EmitMode.None;
        if (!TryParseSwitch(token, out var switchValue))
        {
            switchValue = token.ToLowerInvariant();
        }

        if (switchValue == "asm")
        {
            mode = EmitMode.Asm;
            return true;
        }

        if (switchValue == "cod")
        {
            mode = EmitMode.Cod;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Parst --emit=&lt;mode&gt;-Werte.
    /// </summary>
    /// <param name="switchValue">Zu parsender Schalter-Wert.</param>
    /// <param name="mode">Geparster Emit-Modus.</param>
    /// <returns>True wenn ein gültiger Emit-Modus geparst wurde.</returns>
    private static bool TryParseEmitEqualsValue(string switchValue, out EmitMode mode)
    {
        mode = EmitMode.None;
        if (!switchValue.StartsWith("emit=", StringComparison.Ordinal))
        {
            return false;
        }

        var value = switchValue["emit=".Length..];
        if (value == "asm")
        {
            mode = EmitMode.Asm;
            return true;
        }

        if (value == "cod")
        {
            mode = EmitMode.Cod;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Führt zwei Emit-Modi zusammen und meldet Konflikte.
    /// </summary>
    /// <param name="current">Aktueller Modus.</param>
    /// <param name="candidate">Kandidaten-Modus.</param>
    /// <param name="diagnostics">Diagnosen-Sammlung.</param>
    /// <returns>Der zusammengeführte Emit-Modus.</returns>
    private EmitMode MergeEmitMode(EmitMode current, EmitMode candidate, IList<CliDiagnostic> diagnostics)
    {
        if (current == EmitMode.None || current == candidate)
        {
            return candidate;
        }

        diagnostics.Add(new CliDiagnostic(
            UnexpectedTerminationExitCode,
            _cliMessages.GetString("Cli_Err_ConflictingEmitModes", CultureInfo.InvariantCulture)
                ?? "Conflicting emit modes. Specify only one of 'asm' or 'cod'."));
        return current;
    }
}
