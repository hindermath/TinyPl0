using System.Globalization;

namespace Pl0.Core;

public static class PCodeSerializer
{
    public static string ToAsm(IReadOnlyList<Instruction> instructions) =>
        string.Join(Environment.NewLine, instructions.Select(i => $"{ToMnemonic(i.Op)} {i.Level} {i.Argument}"));

    public static string ToCod(IReadOnlyList<Instruction> instructions) =>
        string.Join(Environment.NewLine, instructions.Select(i => $"{(int)i.Op} {i.Level} {i.Argument}"));

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

    private static string StripComment(string line)
    {
        var commentIndex = line.IndexOf("//", StringComparison.Ordinal);
        return commentIndex >= 0 ? line[..commentIndex] : line;
    }

    private static int ParseInt(string value, int lineNo, string partName)
    {
        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
        {
            return parsed;
        }

        throw new FormatException($"Invalid {partName} on p-code line {lineNo}: '{value}'.");
    }

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
