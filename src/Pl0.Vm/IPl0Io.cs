namespace Pl0.Vm;

/// <summary>
/// Abstraction for PL/0 input/output.
/// </summary>
public interface IPl0Io
{
    /// <summary>
    /// Reads an integer value from the input source.
    /// </summary>
    /// <returns>The read integer.</returns>
    int ReadInt();

    /// <summary>
    /// Writes an integer value to the output destination.
    /// </summary>
    /// <param name="value">Value to write.</param>
    void WriteInt(int value);
}
