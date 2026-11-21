using Terminal.Gui;

namespace Pl0.Ide;

internal static class Program
{
    private static int Main()
    {
        using var app = CreateApplication();

        app.Init();
        if (!IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable())
        {
            Console.Error.WriteLine("Die kontrastreiche IDE-Palette konnte nicht geladen werden.");
            Console.Error.WriteLine("The high-contrast IDE palette could not be loaded.");
            return 1;
        }

        var mainView = new IdeMainView(app);
        app.Run(mainView);
        return mainView.ExitCode;
    }

    internal static IApplication CreateApplication()
    {
        return Application.Create();
    }
}
