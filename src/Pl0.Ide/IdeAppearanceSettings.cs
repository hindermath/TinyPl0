using Terminal.Gui;

namespace Pl0.Ide;

internal sealed record IdeColorScheme(string Id, string ThemeName, string Label);

internal sealed class IdeAppearanceSettings(string settingsPath)
{
    internal static IReadOnlyList<IdeColorScheme> Schemes { get; } = Array.AsReadOnly<IdeColorScheme>(
    [
        new("tp5", IdeLookAndFeel.TurboPascalThemeName, "Turbo Pascal 5 (Standard / Default)"),
        new("tp6", "TinyPl0 TP6", "Inspiriert von / Inspired by Turbo Pascal 6"),
        new("tp7", "TinyPl0 TP7", "Inspiriert von / Inspired by Turbo Pascal 7")
    ]);

    internal static IdeAppearanceSettings ForCurrentUser() => new(Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TinyPl0", "color-scheme"));

    internal IdeColorScheme Read()
    {
        try
        {
            var id = File.ReadAllText(settingsPath).Trim();
            return Schemes.FirstOrDefault(scheme => scheme.Id == id) ?? Schemes[0];
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException)
        {
            return Schemes[0];
        }
    }

    internal bool TrySave(IdeColorScheme scheme)
    {
        var directory = Path.GetDirectoryName(settingsPath)!;
        var temporaryPath = Path.Combine(directory, $".color-scheme-{Guid.NewGuid():N}.tmp");
        try
        {
            // Umbenennen verhindert eine teilweise geschriebene Einstellung.
            // Renaming prevents a partially written preference.
            Directory.CreateDirectory(directory);
            File.WriteAllText(temporaryPath, scheme.Id);
            File.Move(temporaryPath, settingsPath, overwrite: true);
            return true;
        }
        catch (Exception error) when (error is IOException or UnauthorizedAccessException)
        {
            return false;
        }
        finally
        {
            try
            {
                File.Delete(temporaryPath);
            }
            catch (Exception error) when (error is IOException or UnauthorizedAccessException)
            {
                // Die gültige Einstellung bleibt am Zielpfad erhalten.
                // The valid preference remains at the destination.
            }
        }
    }

    internal static IdeColorScheme? ShowDialog(IApplication app, string currentTheme)
    {
        IdeColorScheme? accepted = null;
        var indices = Enumerable.Range(0, Schemes.Count).ToArray();
        var currentIndex = Array.FindIndex(indices, index => Schemes[index].ThemeName == currentTheme);
        using var dialog = new Dialog { Title = "Darstellung / Appearance", Width = 54, Height = 9 };
        var choices = new OptionSelector
        {
            X = 1,
            Y = 1,
            Width = Dim.Fill(1),
            Height = Schemes.Count,
            Values = indices,
            Labels = Schemes.Select(scheme => scheme.Label).ToArray(),
            Value = Math.Max(0, currentIndex)
        };
        var ok = new Button { Text = "_OK", IsDefault = true };
        var cancel = new Button { Text = "_Abbrechen / Cancel" };
        ok.Accepting += (_, e) =>
        {
            e.Handled = true;
            accepted = Schemes[choices.Value ?? 0];
            app.RequestStop(dialog);
        };
        cancel.Accepting += (_, e) =>
        {
            e.Handled = true;
            app.RequestStop(dialog);
        };
        dialog.Add(choices);
        dialog.AddButton(ok);
        dialog.AddButton(cancel);
        choices.SetFocus();
        app.Run(dialog);
        return accepted;
    }
}
