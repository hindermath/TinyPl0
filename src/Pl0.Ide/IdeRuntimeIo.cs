using Pl0.Vm;

namespace Pl0.Ide;

internal sealed class IdeRuntimeIo(Func<int?> readInt, Action<int> writeInt) : IPl0Io
{
    public int ReadInt()
    {
        var input = readInt();
        if (!input.HasValue)
        {
            throw new EndOfStreamException("Eingabe wurde abgebrochen.");
        }

        return input.Value;
    }

    public void WriteInt(int value)
    {
        writeInt(value);
    }
}
