using Terminal.Gui;
using Terminal.Gui.Editor.Document;
using EditorView = Terminal.Gui.Editor.Editor;

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

internal sealed class IdeDebugView : EditorView
{
    internal static readonly Color BasePointerColor = new("#FFD700");
    internal static readonly Color StackPointerColor = new("#00FFFF");
    private Dictionary<int, Dictionary<int, IdeDebugHighlightKind>> highlightMap = [];

    public IdeDebugView()
    {
        Multiline = true;
        ReadOnly = true;
        Text = string.Empty;
        LineTransformers.Add(new IdeColorTransformer(GetHighlightAttributeAt));
    }

    internal void SetDebugContent(string text, IReadOnlyList<IdeDebugHighlightSpan> spans)
    {
        RebuildHighlightMap(spans);
        // Ein neues Dokument verwirft zwischengespeicherte Farbzeilen nach jedem Debug-Schritt.
        // A new document drops cached colored lines after each debug step.
        Document = new TextDocument(text);
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

    private Terminal.Gui.Drawing.Attribute? GetHighlightAttributeAt(int rowIndex, int columnIndex)
    {
        var highlightKind = GetHighlightKindAt(rowIndex, columnIndex);
        if (highlightKind == IdeDebugHighlightKind.None)
        {
            return null;
        }

        var scheme = GetScheme();
        var background = scheme?.Normal.Background ?? Color.Black;
        var attribute = highlightKind switch
        {
            IdeDebugHighlightKind.BasePointer => new Terminal.Gui.Drawing.Attribute(BasePointerColor, background),
            IdeDebugHighlightKind.StackPointer => new Terminal.Gui.Drawing.Attribute(StackPointerColor, background),
            _ => scheme?.Normal ?? default
        };

        return attribute;
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
