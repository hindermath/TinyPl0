namespace Pl0.Core;

/// <summary>
/// Simple symbol table with scope tracking.
/// </summary>
public sealed class SymbolTable
{
    /// <summary>
    /// All symbol entries in stack order.
    /// </summary>
    private readonly List<SymbolEntry> _entries = [];
    /// <summary>
    /// Stack of scope start indices.
    /// </summary>
    private readonly Stack<int> _scopeStarts = [];
    /// <summary>
    /// Gets the number of entries in the symbol table.
    /// </summary>
    public int Count => _entries.Count;

    /// <summary>
    /// Opens a new scope.
    /// </summary>
    public void EnterScope()
    {
        _scopeStarts.Push(_entries.Count);
    }

    /// <summary>
    /// Closes the current scope and removes scoped entries.
    /// </summary>
    public void ExitScope()
    {
        if (_scopeStarts.Count == 0)
        {
            return;
        }

        var start = _scopeStarts.Pop();
        if (start < _entries.Count)
        {
            _entries.RemoveRange(start, _entries.Count - start);
        }
    }

    /// <summary>
    /// Tries to declare a symbol in the current scope.
    /// </summary>
    /// <param name="entry">The symbol entry to add.</param>
    /// <returns>True if the symbol was declared; false if it was a duplicate.</returns>
    public bool TryDeclare(SymbolEntry entry)
    {
        var scopeStart = _scopeStarts.Count == 0 ? 0 : _scopeStarts.Peek();
        for (var i = _entries.Count - 1; i >= scopeStart; i--)
        {
            if (_entries[i].Name == entry.Name)
            {
                return false;
            }
        }

        _entries.Add(entry);
        return true;
    }

    /// <summary>
    /// Looks up the most recent symbol with the given name.
    /// </summary>
    /// <param name="name">The symbol name.</param>
    /// <returns>The symbol entry, or null if not found.</returns>
    public SymbolEntry? Lookup(string name)
    {
        for (var i = _entries.Count - 1; i >= 0; i--)
        {
            if (_entries[i].Name == name)
            {
                return _entries[i];
            }
        }

        return null;
    }
}
