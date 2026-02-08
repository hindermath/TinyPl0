namespace Pl0.Core;

public sealed class SymbolTable
{
    private readonly List<SymbolEntry> _entries = [];
    private readonly Stack<int> _scopeStarts = [];
    public int Count => _entries.Count;

    public void EnterScope()
    {
        _scopeStarts.Push(_entries.Count);
    }

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
