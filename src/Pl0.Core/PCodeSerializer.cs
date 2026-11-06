using System.Globalization;

namespace Pl0.Core;

/// <summary>
/// Serializes and parses P-Code in text form.
/// </summary>
public static class PCodeSerializer
{
    private const int DefaultMaximumInstructionCount = 10_000;
    private const int MaximumAcceptedInstructionCount = 100_000;
    private const int MaximumLineLength = 4_096;
    private const int MaximumTextLength = 1_048_576;
    /// <summary>
    /// Wandelt Befehle in das mnemonische Assemblerformat um.
    ///
    /// Converts instructions to mnemonic assembly format.
    /// </summary>
    /// <param name="instructions">Die zu serialisierenden Befehle. / Instructions to serialize.</param>
    /// <returns>Assemblertext mit einem Befehl pro Zeile. / Assembly text with one instruction per line.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Ein Befehl enthält einen nicht unterstützten Opcode. / An instruction contains an unsupported opcode.</exception>
    public static string ToAsm(IReadOnlyList<Instruction> instructions) =>
        string.Join(Environment.NewLine, instructions.Select(i => $"{ToMnemonic(i.Op)} {i.Level} {i.Argument}"));

    /// <summary>
    /// Converts instructions to numeric opcode format.
    /// </summary>
    /// <param name="instructions">Instructions to serialize.</param>
    /// <returns>Numeric opcode text with one instruction per line.</returns>
    public static string ToCod(IReadOnlyList<Instruction> instructions) =>
        string.Join(Environment.NewLine, instructions.Select(i => $"{(int)i.Op} {i.Level} {i.Argument}"));

    /// <summary>
    /// Liest P-Code-Text als Befehlsfolge ein.
    ///
    /// Parses P-Code text into instructions.
    /// </summary>
    /// <param name="text">Der Eingabetext mit P-Code. / Input text containing P-Code.</param>
    /// <returns>Die eingelesene Befehlsliste. / The parsed instruction list.</returns>
    /// <exception cref="FormatException">Eine nicht leere Zeile besitzt kein gültiges Opcode-, Ebenen- oder Argumentformat. / A non-empty line has no valid opcode, level, or argument format.</exception>
    public static IReadOnlyList<Instruction> Parse(string text)
    {
        if (text.Length > MaximumTextLength)
        {
            throw new FormatException($"P-code input exceeds {MaximumTextLength} characters.");
        }

        using var reader = new StringReader(text);
        return Parse(reader);
    }

    /// <summary>
    /// Liest P-Code zeilenweise mit einer festen Befehlsgrenze ein.
    /// Parses P-Code line by line with a fixed instruction limit.
    /// </summary>
    /// <param name="reader">Textquelle. / Text source.</param>
    /// <param name="maximumInstructionCount">Maximale Befehlsanzahl. / Maximum instruction count.</param>
    /// <returns>Die eingelesene Befehlsliste. / The parsed instruction list.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Die Grenze liegt außerhalb des unterstützten Bereichs. / The limit is outside the supported range.</exception>
    /// <exception cref="FormatException">Der Text ist ungültig oder überschreitet eine Grenze. / The text is invalid or exceeds a limit.</exception>
    public static IReadOnlyList<Instruction> Parse(
        TextReader reader,
        int maximumInstructionCount = DefaultMaximumInstructionCount)
    {
        if (maximumInstructionCount is < 1 or > MaximumAcceptedInstructionCount)
        {
            throw new ArgumentOutOfRangeException(nameof(maximumInstructionCount));
        }

        var result = new List<Instruction>();
        var lineNo = 0;
        var charactersRead = 0;
        while (ReadBoundedLine(reader, MaximumLineLength, ref charactersRead) is { } line)
        {
            lineNo++;
            var stripped = StripComment(line).Trim();
            if (string.IsNullOrWhiteSpace(stripped))
            {
                continue;
            }

            var parts = stripped.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 3)
            {
                throw new FormatException($"Invalid p-code structure on line {lineNo}.");
            }

            var opcode = ParseOpcode(parts[0], lineNo);
            var level = ParseInt(parts[1], lineNo, "level");
            var argument = ParseInt(parts[2], lineNo, "argument");
            result.Add(new Instruction(opcode, level, argument));
            if (result.Count > maximumInstructionCount)
            {
                throw new FormatException(
                    $"P-code program exceeds {maximumInstructionCount} instructions.");
            }
        }

        return result;
    }

    private static string? ReadBoundedLine(
        TextReader reader,
        int maximumLength,
        ref int charactersRead)
    {
        var builder = new System.Text.StringBuilder(Math.Min(maximumLength, 256));
        while (true)
        {
            var value = reader.Read();
            if (value < 0)
            {
                return builder.Length == 0 ? null : builder.ToString();
            }

            charactersRead++;
            if (charactersRead > MaximumTextLength)
            {
                throw new FormatException(
                    $"P-code input exceeds {MaximumTextLength} characters.");
            }

            var character = (char)value;
            if (character == '\n')
            {
                return builder.ToString();
            }
            if (character == '\r')
            {
                if (reader.Peek() == '\n')
                {
                    _ = reader.Read();
                }
                return builder.ToString();
            }
            if (builder.Length >= maximumLength)
            {
                throw new FormatException($"P-code line exceeds {maximumLength} characters.");
            }
            builder.Append(character);
        }
    }

    /// <summary>
    /// Removes // comments from a line.
    /// </summary>
    /// <param name="line">Input line.</param>
    /// <returns>Line content without comments.</returns>
    private static string StripComment(string line)
    {
        var commentIndex = line.IndexOf("//", StringComparison.Ordinal);
        return commentIndex >= 0 ? line[..commentIndex] : line;
    }

    /// <summary>
    /// Parses an integer value or throws a formatted exception.
    /// </summary>
    /// <param name="value">Text to parse.</param>
    /// <param name="lineNo">Line number for error reporting.</param>
    /// <param name="partName">Name of the parsed part.</param>
    /// <returns>Parsed integer.</returns>
    private static int ParseInt(string value, int lineNo, string partName)
    {
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        throw new FormatException(
            $"Invalid {partName} on p-code line {lineNo}: '{SafeDiagnosticValue(value)}'.");
    }

    /// <summary>
    /// Parses an opcode from mnemonic or numeric form.
    /// </summary>
    /// <param name="value">Opcode value.</param>
    /// <param name="lineNo">Line number for error reporting.</param>
    /// <returns>The parsed opcode.</returns>
    private static Opcode ParseOpcode(string value, int lineNo)
    {
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericOpcode))
        {
            if (Enum.IsDefined(typeof(Opcode), numericOpcode))
            {
                return (Opcode)numericOpcode;
            }

            throw new FormatException(
                $"Unknown numeric opcode on p-code line {lineNo}: '{SafeDiagnosticValue(value)}'.");
        }

        return value.ToLowerInvariant() switch
        {
            "lit" => Opcode.Lit,
            "opr" => Opcode.Opr,
            "lod" => Opcode.Lod,
            "sto" => Opcode.Sto,
            "cal" => Opcode.Cal,
            // Deutsch: Historische Unterrichtsartefakte verwenden oft `Inc`
            // fuer dieselbe Stack-Reservierung wie TinyPl0 mit `int`.
            // English: Historical teaching artifacts often use `Inc` for the
            // same stack-reservation operation that TinyPl0 exposes as `int`.
            "int" or "inc" => Opcode.Int,
            "jmp" => Opcode.Jmp,
            "jpc" => Opcode.Jpc,
            _ => throw new FormatException(
                $"Unknown mnemonic opcode on p-code line {lineNo}: '{SafeDiagnosticValue(value)}'."),
        };
    }

    private static string SafeDiagnosticValue(string value)
    {
        var builder = new System.Text.StringBuilder(capacity: Math.Min(value.Length, 80));
        foreach (var character in value.Take(80))
        {
            if (char.IsControl(character))
            {
                builder.Append("\\u").Append(((int)character).ToString("X4", CultureInfo.InvariantCulture));
            }
            else
            {
                builder.Append(character);
            }
        }
        if (value.Length > 80)
        {
            builder.Append('…');
        }
        return builder.ToString();
    }

    /// <summary>
    /// Converts an opcode to its mnemonic form.
    /// </summary>
    /// <param name="opcode">Opcode to convert.</param>
    /// <returns>Mnemonic string.</returns>
    private static string ToMnemonic(Opcode opcode) =>
        opcode switch
        {
            Opcode.Lit => "lit",
            Opcode.Opr => "opr",
            Opcode.Lod => "lod",
            Opcode.Sto => "sto",
            Opcode.Cal => "cal",
            Opcode.Int => "int",
            Opcode.Jmp => "jmp",
            Opcode.Jpc => "jpc",
            _ => throw new ArgumentOutOfRangeException(nameof(opcode), opcode, "Unsupported opcode."),
        };
}
