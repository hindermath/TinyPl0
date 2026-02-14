namespace Pl0.Core;

/// <summary>
/// Represents a symbol table entry for constants, variables, or procedures.
/// </summary>
public sealed class SymbolEntry
{
    /// <summary>
    /// Creates a new symbol table entry.
    /// </summary>
    /// <param name="name">Symbol identifier.</param>
    /// <param name="kind">Symbol kind.</param>
    /// <param name="level">Lexical level where the symbol is declared.</param>
    /// <param name="address">Address or code location for the symbol.</param>
    /// <param name="value">Constant value if applicable.</param>
    public SymbolEntry(string name, SymbolKind kind, int level, int address, int value = 0)
    {
        Name = name;
        Kind = kind;
        Level = level;
        Address = address;
        Value = value;
    }

    /// <summary>
    /// Gets the symbol name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the symbol kind.
    /// </summary>
    public SymbolKind Kind { get; }

    /// <summary>
    /// Gets the lexical level of the declaration.
    /// </summary>
    public int Level { get; }

    /// <summary>
    /// Gets or sets the address or code location for this symbol.
    /// </summary>
    public int Address { get; set; }

    /// <summary>
    /// Gets the constant value if applicable.
    /// </summary>
    public int Value { get; }
}
