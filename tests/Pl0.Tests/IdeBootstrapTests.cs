using Pl0.Ide;
using Terminal.Gui;

namespace Pl0.Tests;

public sealed class IdeBootstrapTests
{
    [Fact]
    public void CreateApplication_Uses_V2_Backend()
    {
        var app = Pl0.Ide.Program.CreateApplication();
        Assert.False(app.IsLegacy);
    }

    [Fact]
    public void MainView_Contains_Core_Panes()
    {
        var mainView = new IdeMainView();
        Assert.Single(mainView.Subviews.OfType<MenuBar>());

        var windows = mainView.Subviews.OfType<Window>().ToArray();

        Assert.Equal(3, windows.Length);
        Assert.Contains(windows, window => window.Title.ToString() == "Quellcode");
        Assert.Contains(windows, window => window.Title.ToString() == "P-Code");
        Assert.Contains(windows, window => window.Title.ToString() == "Meldungen");
    }

    [Fact]
    public void MainView_Contains_Dedicated_Source_Code_Editor_In_Source_Window()
    {
        var mainView = new IdeMainView();
        var sourceWindow = mainView
            .Subviews
            .OfType<Window>()
            .Single(window => window.Title.ToString() == "Quellcode");

        var sourceEditor = Assert.Single(sourceWindow.Subviews.OfType<TextView>());
        Assert.Same(mainView.SourceEditor, sourceEditor);
        Assert.False(sourceEditor.ReadOnly);
        Assert.True(sourceEditor.Multiline);
    }

    [Fact]
    public void MainView_Uses_Classic_Turbo_Pascal_Menu_Structure()
    {
        var mainView = new IdeMainView();
        var menuBar = Assert.Single(mainView.Subviews.OfType<MenuBar>());

        var menuTitles = menuBar.Menus.Select(menu => menu.Title.ToString()).ToArray();
        Assert.Equal(["_Datei", "_Bearbeiten", "_Kompilieren", "_Ausfuehren", "_Debug", "_Hilfe"], menuTitles);
    }

    [Fact]
    public void LookAndFeel_Applies_TurboPascal5_Theme_When_Available()
    {
        var exception = Record.Exception(() => _ = IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        Assert.Null(exception);
    }

    [Fact]
    public void LookAndFeel_Uses_TurboPascal5_Theme_Name()
    {
        Assert.Equal("TurboPascal 5", IdeLookAndFeel.TurboPascalThemeName);
    }
}
