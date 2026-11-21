using Microsoft.Extensions.Configuration;
using Terminal.Gui;

namespace Pl0.Ide;

internal static class IdeLookAndFeel
{
    internal const string TurboPascalThemeName = "TurboPascal 5";
    private const string PaletteResourceName = "Pl0.Ide.ide-theme.json";

    internal static bool ApplyTurboPascalThemeIfAvailable(string? resourceName = null)
        => LoadAndApplyTheme(TurboPascalThemeName, resourceName);

    internal static bool LoadAndApplyTheme(string themeName, string? resourceName = null)
    {
        try
        {
            using var paletteStream = typeof(IdeLookAndFeel).Assembly.GetManifestResourceStream(resourceName ?? PaletteResourceName);
            if (paletteStream is null)
            {
                return false;
            }

            using var paletteReader = new StreamReader(paletteStream);
            var builder = TuiConfigurationBuilder.Shared;
            builder.RuntimeConfig = paletteReader.ReadToEnd();
            builder.ApplyToStaticFacades();

            var configuration = builder.Configuration;
            if (!configuration.GetSection("Themes").GetChildren()
                    .Any(theme => string.Equals(theme.Key, themeName, StringComparison.Ordinal)))
            {
                return false;
            }

            return builder.ThemeManager.SwitchTheme(themeName);
        }
        catch
        {
            return false;
        }
    }
}
