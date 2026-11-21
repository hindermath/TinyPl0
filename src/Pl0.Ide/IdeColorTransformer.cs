using Terminal.Gui.Editor.Rendering;
using GuiAttribute = Terminal.Gui.Drawing.Attribute;

namespace Pl0.Ide;

internal sealed class IdeColorTransformer(Func<int, int, GuiAttribute?> colorAt) : IVisualLineTransformer
{
    public void Transform(CellVisualLine line)
    {
        var row = line.DocumentLine.LineNumber - 1;
        foreach (var element in line.Elements)
        {
            // Dokument-Offsets bleiben auch bei breiten Terminal-Zeichen stabil.
            // Document offsets stay stable even when terminal characters occupy multiple cells.
            var column = element.DocumentOffset - line.DocumentLine.Offset;
            var attribute = colorAt(row, column);
            if (attribute.HasValue)
            {
                element.Attribute = attribute.Value;
            }
        }
    }
}
