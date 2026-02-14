using System.Globalization;

namespace Pl0.Vm;

/// <summary>
/// Console-based I/O for interactive VM execution.
/// </summary>
public sealed class ConsolePl0Io : IPl0Io
{
    /// <summary>
    /// Reads an integer value from standard input.
    /// </summary>
    /// <returns>The parsed integer.</returns>
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

    /// <summary>
    /// Writes an integer value to standard output.
    /// </summary>
    /// <param name="value">Value to write.</param>
    public void WriteInt(int value)
    {
        Console.WriteLine(value.ToString(CultureInfo.InvariantCulture));
    }
}
