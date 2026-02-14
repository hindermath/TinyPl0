using System.Globalization;

namespace Pl0.Core;

/// <summary>
/// Serializes and parses P-Code in text form.
/// </summary>
public static class PCodeSerializer
{
    /// <summary>
    /// Converts instructions to mnemonic assembly format.
    /// </summary>
    /// <param name="instructions">Instructions to serialize.</param>
    /// <returns>Assembly text with one instruction per line.</returns>
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
    /// Parses P-Code text into instructions.
    /// </summary>
    /// <param name="text">Input text containing p-code.</param>
    /// <returns>Parsed instruction list.</returns>
    public static IReadOnlyList<Instruction> Parse(string text)
    {
        var result = new List<Instruction>();
        var lines = text.Replace("\r\n", "\n", StringComparison.Ordinal).Split('\n');
        for (var lineNo = 1; lineNo <= lines.Length; lineNo++)
        {
            var line = lines[lineNo - 1];
            var stripped = StripComment(line).Trim();
            if (string.IsNullOrWhiteSpace(stripped))
            {
                continue;
            }

            var parts = stripped.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length != 3)
            {
                throw new FormatException($"Invalid p-code line {lineNo}: '{line}'.");
            }

            var opcode = ParseOpcode(parts[0], lineNo);
            var level = ParseInt(parts[1], lineNo, "level");
            var argument = ParseInt(parts[2], lineNo, "argument");
            result.Add(new Instruction(opcode, level, argument));
        }

        return result;
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

        throw new FormatException($"Invalid {partName} on p-code line {lineNo}: '{value}'.");
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

            throw new FormatException($"Unknown numeric opcode on p-code line {lineNo}: '{value}'.");
        }

        return value.ToLowerInvariant() switch
        {
            "lit" => Opcode.Lit,
            "opr" => Opcode.Opr,
            "lod" => Opcode.Lod,
            "sto" => Opcode.Sto,
            "cal" => Opcode.Cal,
            "int" => Opcode.Int,
            "jmp" => Opcode.Jmp,
            "jpc" => Opcode.Jpc,
            _ => throw new FormatException($"Unknown mnemonic opcode on p-code line {lineNo}: '{value}'."),
        };
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
