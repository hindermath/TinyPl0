using Pl0.Ide;
using Terminal.Gui.Configuration;
using Terminal.Gui.Drawing;
using Terminal.Gui.Views;

namespace Pl0.Tests;

[Collection("IDE GUI")]
public sealed class IdeContrastTests
{
    [Fact]
    public void Alternate_Themes_Update_Existing_Editor_And_Menu()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        using var mainView = new IdeMainView();
        mainView.SourceEditor.Text = "begin ! 42 end.";
        var document = mainView.SourceEditor.Document;
        var menu = Assert.Single(mainView.SubViews.OfType<MenuBar>());
        var original = menu.GetScheme().Normal.Background;
        var builder = TuiConfigurationBuilder.Shared;

        try
        {
            foreach (var (theme, background) in new[]
                     {
                         ("TinyPl0 TP6", "#101A50"),
                         ("TinyPl0 TP7", "#102C32")
                     })
            {
                Assert.True(builder.ThemeManager.SwitchTheme(theme));
                var attribute = mainView.SourceEditor.GetScheme().Editable;
                Assert.Equal(new Color(background), attribute.Background);
                Assert.True(ContrastRatio(attribute.Foreground, attribute.Background) >= 4.5);
                Assert.NotEqual(original, menu.GetScheme().Normal.Background);
                Assert.Same(document, mainView.SourceEditor.Document);
                foreach (var name in new[] { "Base", "Accent", "Dialog", "Menu", "Error" })
                {
                    AssertSchemeContrast(name);
                }

                foreach (var color in IdeRainbowAsciiArtView.RainbowPalette
                             .Concat([IdeDebugView.BasePointerColor, IdeDebugView.StackPointerColor]))
                {
                    Assert.True(ContrastRatio(color, attribute.Background) >= 4.5, $"{theme}: {color}");
                }
            }
        }
        finally
        {
            Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        }
    }

    [Fact]
    public void TurboPascal5_ReadOnly_Output_Meets_WcagAa_Text_Contrast()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());

        var mainView = new IdeMainView();
        var attribute = mainView.MessagesOutput.GetScheme().ReadOnly;
        var contrast = ContrastRatio(attribute.Foreground, attribute.Background);

        Assert.True(contrast >= 4.5, $"Read-only output contrast was {contrast:F2}:1; expected at least 4.5:1.");
    }

    [Theory]
    [InlineData("Base")]
    [InlineData("Accent")]
    [InlineData("Dialog")]
    [InlineData("Menu")]
    [InlineData("Error")]
    public void TurboPascal5_Scheme_Roles_Meet_WcagAa_Contrast(string schemeName)
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        AssertSchemeContrast(schemeName);
    }

    private static void AssertSchemeContrast(string schemeName)
    {
        var scheme = Assert.IsType<Scheme>(TuiConfigurationBuilder.Shared.SchemeManager.GetScheme(schemeName));
        var roles = new (string Name, Terminal.Gui.Drawing.Attribute Attribute)[]
        {
            ("Normal", scheme.Normal),
            ("Focus", scheme.Focus),
            ("Active", scheme.Active),
            ("HotNormal", scheme.HotNormal),
            ("HotFocus", scheme.HotFocus),
            ("HotActive", scheme.HotActive),
            ("Highlight", scheme.Highlight),
            ("Editable", scheme.Editable),
            ("ReadOnly", scheme.ReadOnly),
            ("Disabled", scheme.Disabled)
        };

        foreach (var (name, attribute) in roles)
        {
            var contrast = ContrastRatio(attribute.Foreground, attribute.Background);
            Assert.True(contrast >= 4.5,
                $"{schemeName}.{name} contrast was {contrast:F2}:1; expected at least 4.5:1.");
        }

        var focusBoundaryContrast = ContrastRatio(scheme.Focus.Background, scheme.Normal.Background);
        Assert.True(focusBoundaryContrast >= 3.0,
            $"{schemeName} focus boundary contrast was {focusBoundaryContrast:F2}:1; expected at least 3:1.");
    }

    [Fact]
    public void Appearance_Preference_Survives_Restart_And_Handles_Unusable_Settings()
    {
        var directory = Directory.CreateTempSubdirectory("tinypl0-appearance-");
        try
        {
            var path = Path.Combine(directory.FullName, "settings", "color-scheme");
            var settings = new IdeAppearanceSettings(path);
            Assert.Equal("tp5", settings.Read().Id);
            Assert.True(settings.TrySave(IdeAppearanceSettings.Schemes[2]));
            Assert.Equal("tp7", new IdeAppearanceSettings(path).Read().Id);
            Assert.True(settings.TrySave(IdeAppearanceSettings.Schemes[1]));
            Assert.Equal("tp6", new IdeAppearanceSettings(path).Read().Id);
            File.WriteAllText(path, "unknown");
            Assert.Equal("tp5", settings.Read().Id);
            Assert.False(new IdeAppearanceSettings(Path.Combine(path, "color-scheme"))
                .TrySave(IdeAppearanceSettings.Schemes[1]));
        }
        finally
        {
            directory.Delete(recursive: true);
        }
    }

    [Fact]
    public void TurboPascal5_Views_Use_Explicit_Dark_Text_Area_Colors()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        var mainView = new IdeMainView();
        var navy = new Color("#000080");
        var yellow = new Color("#FFFF66");

        Assert.Equal(navy, mainView.SourceEditor.GetScheme().Editable.Background);
        Assert.Equal(yellow, mainView.SourceEditor.GetScheme().Editable.Foreground);

        foreach (var output in new[] { mainView.PCodeOutput, mainView.MessagesOutput, mainView.RuntimeOutput, mainView.DebugOutput })
        {
            Assert.Equal(navy, output.GetScheme().ReadOnly.Background);
            Assert.Equal(yellow, output.GetScheme().ReadOnly.Foreground);
        }

        var menu = Assert.Single(mainView.SubViews.OfType<MenuBar>());
        Assert.Equal(new Color("#00FFFF"), menu.GetScheme().Normal.Background);
        Assert.Equal(new Color("#000000"), menu.GetScheme().Normal.Foreground);

        var dialog = new Dialog();
        Assert.Equal(new Color("#D9D9D9"), dialog.GetScheme().Normal.Background);
        Assert.Equal(new Color("#000000"), dialog.GetScheme().Normal.Foreground);

        foreach (var window in mainView.SubViews.OfType<Window>())
        {
            var border = window.GetScheme().Normal;
            Assert.True(ContrastRatio(border.Foreground, border.Background) >= 3.0);
        }
    }

    [Fact]
    public void TurboPascal5_Debug_And_Rainbow_Accents_Meet_Text_Contrast()
    {
        Assert.True(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable());
        var background = new Color("#000080");
        var accentColors = IdeRainbowAsciiArtView.RainbowPalette
            .Concat([IdeDebugView.BasePointerColor, IdeDebugView.StackPointerColor]);

        foreach (var color in accentColors)
        {
            var contrast = ContrastRatio(color, background);
            Assert.True(contrast >= 4.5, $"Accent color {color} contrast was {contrast:F2}:1.");
        }
    }

    [Fact]
    public void Missing_Contrast_Palette_Is_Reported_As_Unavailable()
    {
        Assert.False(IdeLookAndFeel.ApplyTurboPascalThemeIfAvailable("Pl0.Ide.missing-theme.json"));
    }

    private static double ContrastRatio(Color foreground, Color background)
    {
        var foregroundLuminance = RelativeLuminance(foreground);
        var backgroundLuminance = RelativeLuminance(background);
        return (Math.Max(foregroundLuminance, backgroundLuminance) + 0.05)
               / (Math.Min(foregroundLuminance, backgroundLuminance) + 0.05);
    }

    private static double RelativeLuminance(Color color)
    {
        return 0.2126 * Linearize(color.R)
               + 0.7152 * Linearize(color.G)
               + 0.0722 * Linearize(color.B);
    }

    private static double Linearize(byte channel)
    {
        var value = channel / 255.0;
        return value <= 0.04045 ? value / 12.92 : Math.Pow((value + 0.055) / 1.055, 2.4);
    }
}
