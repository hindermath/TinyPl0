namespace Pl0.Vm;

/// <summary>
/// Buffered I/O implementation for deterministic VM tests.
/// </summary>
public sealed class BufferedPl0Io : IPl0Io
{
    /// <summary>
    /// Input queue of integer values.
    /// </summary>
    private readonly Queue<int> _input;
    /// <summary>
    /// Collected output values.
    /// </summary>
    private readonly List<int> _output = [];

    /// <summary>
    /// Creates a buffered I/O instance with optional input values.
    /// </summary>
    /// <param name="input">Initial input values.</param>
    public BufferedPl0Io(IEnumerable<int>? input = null)
    {
        _input = new Queue<int>(input ?? []);
    }

    /// <summary>
    /// Gets the output values written so far.
    /// </summary>
    public IReadOnlyList<int> Output => _output;

    /// <summary>
    /// Reads the next input value.
    /// </summary>
    /// <returns>The next integer in the input buffer.</returns>
    public int ReadInt()
    {
        if (_input.Count == 0)
        {
            throw new EndOfStreamException("No buffered input value available.");
        }

        return _input.Dequeue();
    }

    /// <summary>
    /// Writes an output value to the buffer.
    /// </summary>
    /// <param name="value">Value to write.</param>
    public void WriteInt(int value)
    {
        _output.Add(value);
    }
}
