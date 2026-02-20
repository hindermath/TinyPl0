using Terminal.Gui;

namespace Pl0.Ide;

internal sealed class IdeMainView : Toplevel
{
    public IdeMainView()
    {
        var editorWindow = new Window
        {
            Title = "Quellcode",
            X = 0,
            Y = 0,
            Width = Dim.Percent(70),
            Height = Dim.Percent(70)
        };

        var pCodeWindow = new Window
        {
            Title = "P-Code",
            X = Pos.Right(editorWindow),
            Y = 0,
            Width = Dim.Fill(),
            Height = Dim.Percent(70)
        };

        var messagesWindow = new Window
        {
            Title = "Meldungen",
            X = 0,
            Y = Pos.Bottom(editorWindow),
            Width = Dim.Fill(),
            Height = Dim.Fill()
        };

        Add(editorWindow, pCodeWindow, messagesWindow);
    }
}
