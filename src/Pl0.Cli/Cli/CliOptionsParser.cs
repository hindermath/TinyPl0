using System.Globalization;

namespace Pl0.Cli.Cli;

/// <summary>
/// Parses CLI arguments into compiler options and diagnostics.
/// </summary>
public sealed class CliOptionsParser
{
    /// <summary>
    /// Exit code when an emit mode is requested but missing.
    /// </summary>
    private const int EmitModeMissingExitCode = 96;
    /// <summary>
    /// Exit code for unexpected arguments or switches.
    /// </summary>
    private const int UnexpectedTerminationExitCode = 99;

    /// <summary>
    /// Parses the provided argument list.
    /// </summary>
    /// <param name="args">CLI arguments.</param>
    /// <returns>Parse result containing options and diagnostics.</returns>
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
        var command = CliCommand.None;
        string? sourcePath = null;
        string? outputPath = null;

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
                        "Unexpected positional argument: '{0}'.",
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

                if (sourcePath is null)
                {
                    sourcePath = arg;
                    continue;
                }

                diagnostics.Add(new CliDiagnostic(
                    UnexpectedTerminationExitCode,
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Unexpected positional argument: '{0}'.",
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
                case "out":
                    if (TryReadOptionValue(args, ref i, out var outValue))
                    {
                        outputPath = outValue;
                    }
                    else
                    {
                        diagnostics.Add(new CliDiagnostic(
                            UnexpectedTerminationExitCode,
                            "Missing value for '--out'."));
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
                            "Unknown switch: '{0}'.",
                            arg)));
                    break;
            }
        }

        if (emitRequested && emitMode == EmitMode.None)
        {
            diagnostics.Add(new CliDiagnostic(
                EmitModeMissingExitCode,
                "No emitter mode found. Use '--emit asm' or '--emit cod'."));
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
            SourcePath = sourcePath,
            OutputPath = outputPath,
        };

        return new CliParseResult(options, diagnostics);
    }

    /// <summary>
    /// Reads the next argument as an option value.
    /// </summary>
    /// <param name="args">Argument list.</param>
    /// <param name="index">Current index (advanced on success).</param>
    /// <param name="value">Parsed value.</param>
    /// <returns>True if a value was read.</returns>
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
    /// Parses a command token.
    /// </summary>
    /// <param name="token">Command token.</param>
    /// <param name="command">Parsed command.</param>
    /// <returns>True if a valid command was found.</returns>
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
    /// Determines whether a switch indicates help.
    /// </summary>
    /// <param name="sw">Switch value.</param>
    /// <returns>True if the switch is a help switch.</returns>
    private static bool IsHelpSwitch(string sw) =>
        sw is "?" or "h" or "help";

    /// <summary>
    /// Checks if a value looks like an absolute Unix path.
    /// </summary>
    /// <param name="value">Value to check.</param>
    /// <returns>True if the value is an absolute Unix path.</returns>
    private static bool IsUnixAbsolutePath(string value) =>
        !string.IsNullOrWhiteSpace(value) && value.Length > 1 && value[0] == '/';

    /// <summary>
    /// Determines whether a slash-prefixed token is a known legacy switch.
    /// </summary>
    /// <param name="value">Switch token.</param>
    /// <returns>True if it is a known legacy switch.</returns>
    private static bool IsKnownLegacySlashSwitch(string value)
    {
        if (!value.StartsWith("/", StringComparison.Ordinal) || value.Length <= 1)
        {
            return false;
        }

        var sw = value[1..].ToLowerInvariant();
        if (sw is "?" or "h" or "help" or "errmsg" or "wopcod" or "conly" or "compile-only" or "list-code"
            or "out" or "emit" or "asm" or "cod")
        {
            return true;
        }

        return TryParseEmitEqualsValue(sw, out _);
    }

    /// <summary>
    /// Parses a switch token into a canonical switch string.
    /// </summary>
    /// <param name="value">Token to parse.</param>
    /// <param name="sw">Parsed switch value.</param>
    /// <returns>True if the token is a switch.</returns>
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
    /// Tries to parse an emit mode from a token.
    /// </summary>
    /// <param name="token">Token value.</param>
    /// <param name="mode">Parsed emit mode.</param>
    /// <returns>True if an emit mode was parsed.</returns>
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
    /// Parses --emit=&lt;mode&gt; values.
    /// </summary>
    /// <param name="switchValue">Switch value to parse.</param>
    /// <param name="mode">Parsed emit mode.</param>
    /// <returns>True if a valid emit mode was parsed.</returns>
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
    /// Merges a candidate emit mode into the current mode and reports conflicts.
    /// </summary>
    /// <param name="current">Current mode.</param>
    /// <param name="candidate">Candidate mode.</param>
    /// <param name="diagnostics">Diagnostics collection.</param>
    /// <returns>The merged emit mode.</returns>
    private static EmitMode MergeEmitMode(EmitMode current, EmitMode candidate, IList<CliDiagnostic> diagnostics)
    {
        if (current == EmitMode.None || current == candidate)
        {
            return candidate;
        }

        diagnostics.Add(new CliDiagnostic(
            UnexpectedTerminationExitCode,
            "Conflicting emitter modes. Use only one of 'asm' or 'cod'."));
        return current;
    }
}
