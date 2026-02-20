using Terminal.Gui;

namespace Pl0.Ide;

internal static class Program
{
    private static int Main()
    {
        var app = CreateApplication();
        var previousApplication = ApplicationImpl.Instance;

        ApplicationImpl.ChangeInstance(app);

        try
        {
            app.Init();
            _ = IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable();
            app.Run<IdeMainView>();
            return 0;
        }
        finally
        {
            app.Shutdown();
            if (app is IDisposable disposable)
            {
                disposable.Dispose();
            }

            ApplicationImpl.ChangeInstance(previousApplication);
        }
    }

    internal static IApplication CreateApplication()
    {
        return new ApplicationV2();
    }
}
