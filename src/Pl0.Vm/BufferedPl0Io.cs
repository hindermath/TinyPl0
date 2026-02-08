namespace Pl0.Vm;

public sealed class BufferedPl0Io : IPl0Io
{
    private readonly Queue<int> _input;
    private readonly List<int> _output = [];

    public BufferedPl0Io(IEnumerable<int>? input = null)
    {
        _input = new Queue<int>(input ?? []);
    }

    public IReadOnlyList<int> Output => _output;

    public int ReadInt()
    {
        if (_input.Count == 0)
        {
            throw new EndOfStreamException("No buffered input value available.");
        }

        return _input.Dequeue();
    }

    public void WriteInt(int value)
    {
        _output.Add(value);
    }
}
