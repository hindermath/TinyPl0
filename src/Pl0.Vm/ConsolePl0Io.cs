using System.Globalization;

namespace Pl0.Vm;

public sealed class ConsolePl0Io : IPl0Io
{
    public int ReadInt()
    {
        var line = Console.ReadLine();
        if (line is null)
        {
            throw new EndOfStreamException("No input available.");
        }

        if (!int.TryParse(line, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value))
        {
            throw new FormatException($"Input '{line}' is not a valid integer.");
        }

        return value;
    }

    public void WriteInt(int value)
    {
        Console.WriteLine(value.ToString(CultureInfo.InvariantCulture));
    }
}
