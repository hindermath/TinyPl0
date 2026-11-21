using Terminal.Gui;

namespace Pl0.Ide;

internal static class Program
{
    private static int Main()
    {
        using var app = CreateApplication();

        app.Init();
        _ = IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable();
        var mainView = new IdeMainView(app);
        app.Run(mainView);
        return mainView.ExitCode;
    }

    internal static IApplication CreateApplication()
    {
        return Application.Create();
    }
}
