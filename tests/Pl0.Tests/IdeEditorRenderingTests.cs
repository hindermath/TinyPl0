using Pl0.Ide;
using Terminal.Gui.Editor.Document;
using Terminal.Gui.Editor.Rendering;
using Terminal.Gui.Drawing;

namespace Pl0.Tests;

[Collection("IDE GUI")]
public sealed class IdeEditorRenderingTests
{
    [Fact]
    public void Source_Highlighting_Colors_Visible_Tokens()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        var editor = new Pl0SourceEditorView { Text = "begin 42" };
        var scheme = editor.GetScheme();
        var line = CreateLine(editor.Document!, "begin 42", scheme.Normal);

        Assert.Single(editor.LineTransformers.OfType<IdeColorTransformer>()).Transform(line);

        Assert.Equal(scheme.HotNormal, line.Elements[0].Attribute);
        Assert.Equal(scheme.Normal, line.Elements[5].Attribute);
        Assert.Equal(scheme.HotFocus, line.Elements[6].Attribute);
    }

    [Fact]
    public void Debug_Pointer_Markers_Keep_Their_Visible_Colors()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        var editor = new IdeDebugView();
        editor.SetDebugContent("BP>> SP>>", [
            new IdeDebugHighlightSpan(0, 0, 4, IdeDebugHighlightKind.BasePointer),
            new IdeDebugHighlightSpan(0, 5, 4, IdeDebugHighlightKind.StackPointer)
        ]);
        var scheme = editor.GetScheme();
        var line = CreateLine(editor.Document!, "BP>> SP>>", scheme.Normal);

        Assert.Single(editor.LineTransformers.OfType<IdeColorTransformer>()).Transform(line);

        Assert.Equal(IdeDebugView.BasePointerColor, line.Elements[0].Attribute.Foreground);
        Assert.Equal(IdeDebugView.StackPointerColor, line.Elements[5].Attribute.Foreground);
        Assert.Equal(scheme.Normal.Background, line.Elements[5].Attribute.Background);
    }

    [Fact]
    public void Rainbow_Colors_Only_Nonspace_Characters()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        var editor = new IdeRainbowAsciiArtView();
        editor.SetAsciiArt(["A B"]);
        var scheme = editor.GetScheme();
        var line = CreateLine(editor.Document!, "A B", scheme.Normal);

        Assert.Single(editor.LineTransformers.OfType<IdeColorTransformer>()).Transform(line);

        Assert.Equal(IdeRainbowAsciiArtView.RainbowPalette[0], line.Elements[0].Attribute.Foreground);
        Assert.Equal(scheme.Normal, line.Elements[1].Attribute);
        Assert.Equal(IdeRainbowAsciiArtView.RainbowPalette[2], line.Elements[2].Attribute.Foreground);
    }

    private static CellVisualLine CreateLine(TextDocument document, string text, Terminal.Gui.Drawing.Attribute normal)
    {
        var line = new CellVisualLine(document.GetLineByNumber(1));
        line.ReplaceElements(text.Select((character, index) =>
            (CellVisualLineElement)new TextRunElement(index, 1, index, character.ToString(), normal)).ToArray());
        return line;
    }
}
