namespace Pl0.Core;

public sealed class SymbolEntry
{
    public SymbolEntry(string name, SymbolKind kind, int level, int address, int value = 0)
    {
        Name = name;
        Kind = kind;
        Level = level;
        Address = address;
        Value = value;
    }

    public string Name { get; }

    public SymbolKind Kind { get; }

    public int Level { get; }

    public int Address { get; set; }

    public int Value { get; }
}
