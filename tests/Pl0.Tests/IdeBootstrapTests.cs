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
        var windows = mainView.Subviews.OfType<Window>().ToArray();

        Assert.Equal(3, windows.Length);
        Assert.Contains(windows, window => window.Title.ToString() == "Quellcode");
        Assert.Contains(windows, window => window.Title.ToString() == "P-Code");
        Assert.Contains(windows, window => window.Title.ToString() == "Meldungen");
    }
}
