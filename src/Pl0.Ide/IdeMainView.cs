using Terminal.Gui;

namespace Pl0.Ide;

internal sealed class IdeMainView : Toplevel
{
    private static readonly Action NoOp = static () => { };

    public IdeMainView()
    {
        var menuBar = CreateMenuBar();
        Add(menuBar);

        var editorWindow = new Window
        {
            Title = "Quellcode",
            X = 0,
            Y = Pos.Bottom(menuBar),
            Width = Dim.Percent(70),
            Height = Dim.Percent(70)
        };

        var pCodeWindow = new Window
        {
            Title = "P-Code",
            X = Pos.Right(editorWindow),
            Y = Pos.Bottom(menuBar),
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

    private static MenuBar CreateMenuBar()
    {
        return new MenuBar
        {
            Menus =
            [
                new MenuBarItem("_Datei",
                [
                    new MenuItem("_Neu", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_Oeffnen", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_Speichern", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_Beenden", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Bearbeiten",
                [
                    new MenuItem("_Formatieren", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Kompilieren",
                [
                    new MenuItem("_Build", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Ausfuehren",
                [
                    new MenuItem("_Run", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Debug",
                [
                    new MenuItem("_Step", string.Empty, NoOp, () => true, null, default)
                ], null),
                new MenuBarItem("_Hilfe",
                [
                    new MenuItem("_Bedienung", string.Empty, NoOp, () => true, null, default),
                    new MenuItem("_PL/0-Sprache", string.Empty, NoOp, () => true, null, default)
                ], null)
            ]
        };
    }
}
