using Terminal.Gui;

namespace Pl0.Ide;

internal enum IdeDebugHighlightKind
{
    None = 0,
    BasePointer,
    StackPointer
}

internal readonly record struct IdeDebugHighlightSpan(
    int Row,
    int StartColumn,
    int Length,
    IdeDebugHighlightKind Kind);

internal sealed class IdeDebugView : TextView
{
    private Dictionary<int, Dictionary<int, IdeDebugHighlightKind>> highlightMap = [];

    public IdeDebugView()
    {
        Multiline = true;
        ReadOnly = true;
        Text = string.Empty;
    }

    internal void SetDebugContent(string text, IReadOnlyList<IdeDebugHighlightSpan> spans)
    {
        Text = text;
        RebuildHighlightMap(spans);
    }

    internal IdeDebugHighlightKind GetHighlightKindAt(int rowIndex, int columnIndex)
    {
        if (!highlightMap.TryGetValue(rowIndex, out var lineHighlights))
        {
            return IdeDebugHighlightKind.None;
        }

        return lineHighlights.TryGetValue(columnIndex, out var kind)
            ? kind
            : IdeDebugHighlightKind.None;
    }

    protected override void OnDrawNormalColor(List<Cell> line, int idxCol, int idxRow)
    {
        var highlightKind = GetHighlightKindAt(idxRow, idxCol);
        if (highlightKind == IdeDebugHighlightKind.None)
        {
            base.OnDrawNormalColor(line, idxCol, idxRow);
            return;
        }

        var scheme = ColorScheme;
        var background = scheme?.Normal.Background ?? Color.Black;
        var attribute = highlightKind switch
        {
            IdeDebugHighlightKind.BasePointer => new Terminal.Gui.Attribute(Color.BrightYellow, background),
            IdeDebugHighlightKind.StackPointer => new Terminal.Gui.Attribute(Color.BrightBlue, background),
            _ => scheme?.Normal ?? default
        };

        SetAttribute(attribute);
    }

    private void RebuildHighlightMap(IReadOnlyList<IdeDebugHighlightSpan> spans)
    {
        var map = new Dictionary<int, Dictionary<int, IdeDebugHighlightKind>>();

        foreach (var span in spans)
        {
            if (span.Length <= 0 || span.Row < 0 || span.StartColumn < 0)
            {
                continue;
            }

            if (!map.TryGetValue(span.Row, out var lineHighlights))
            {
                lineHighlights = [];
                map[span.Row] = lineHighlights;
            }

            for (var offset = 0; offset < span.Length; offset++)
            {
                lineHighlights[span.StartColumn + offset] = span.Kind;
            }
        }

        highlightMap = map;
    }
}
