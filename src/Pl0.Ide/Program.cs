using Terminal.Gui;

namespace Pl0.Ide;

internal static class Program
{
    private static int Main()
    {
        var app = CreateApplication();

        try
        {
            app.Init();
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
        }
    }

    internal static IApplication CreateApplication()
    {
        return new ApplicationV2();
    }
}
