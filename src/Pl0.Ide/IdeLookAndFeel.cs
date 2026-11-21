using Microsoft.Extensions.Configuration;
using Terminal.Gui;

namespace Pl0.Ide;

internal static class IdeLookAndFeel
{
    internal const string TurboPascalThemeName = "TurboPascal 5";

    internal static bool ApplyTurboPascalThemeIfAvailable()
    {
        try
        {
            var configuration = TuiConfigurationBuilder.Shared.Configuration;
            if (configuration is null || !configuration.GetSection("Themes").GetChildren()
                    .Any(theme => string.Equals(theme.Key, TurboPascalThemeName, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            ThemeManager.Theme = TurboPascalThemeName;
            return string.Equals(ThemeManager.Theme, TurboPascalThemeName, StringComparison.Ordinal);
        }
        catch
        {
            return false;
        }
    }
}
