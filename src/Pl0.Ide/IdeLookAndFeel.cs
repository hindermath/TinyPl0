using Terminal.Gui;

namespace Pl0.Ide;

internal static class IdeLookAndFeel
{
    internal const string TurboPascalThemeName = "TurboPascal 5";

    internal static bool ApplyTurboPascalThemeIfAvailable()
    {
        try
        {
            var themeManager = ThemeManager.Instance;
            var availableThemes = ThemeManager.Themes;
            if (availableThemes is null || !availableThemes.ContainsKey(TurboPascalThemeName))
            {
                return false;
            }

            themeManager.Theme = TurboPascalThemeName;
            return string.Equals(themeManager.Theme, TurboPascalThemeName, StringComparison.Ordinal);
        }
        catch
        {
            return false;
        }
    }
}
