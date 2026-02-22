using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeMessageDialogService
{
    void ShowInfo(string title, string message);
}

internal interface IIdeAboutDialogService
{
    void ShowAboutDialog(string title, IReadOnlyList<string> asciiArt, string slogan, string versionText, string buildCounterText);
}

internal sealed class TerminalGuiIdeMessageDialogService : IIdeMessageDialogService, IIdeAboutDialogService
{
    public void ShowInfo(string title, string message)
    {
        _ = MessageBox.Query(title, message, "OK");
    }

    public void ShowAboutDialog(string title, IReadOnlyList<string> asciiArt, string slogan, string versionText, string buildCounterText)
    {
        var artHeight = asciiArt.Count;
        var artWidth = 0;
        foreach (var line in asciiArt)
        {
            if (line.Length > artWidth)
            {
                artWidth = line.Length;
            }
        }

        string[] details = [slogan, $"Version: {versionText}", $"Buildzaehler: {buildCounterText}"];
        var detailsWidth = 0;
        foreach (var line in details)
        {
            if (line.Length > detailsWidth)
            {
                detailsWidth = line.Length;
            }
        }

        var contentWidth = Math.Max(artWidth, detailsWidth);
        var dialog = new Dialog
        {
            Title = title,
            Width = Math.Max(52, contentWidth + 6),
            Height = artHeight + details.Length + 7
        };

        var rainbowView = new IdeRainbowAsciiArtView
        {
            X = 1,
            Y = 1,
            Width = Dim.Fill(2),
            Height = artHeight,
            CanFocus = false
        };
        rainbowView.SetAsciiArt(asciiArt);

        var sloganLabel = new Label
        {
            X = 1,
            Y = Pos.Bottom(rainbowView) + 1,
            Width = Dim.Fill(2),
            Text = slogan
        };
        var versionLabel = new Label
        {
            X = 1,
            Y = Pos.Bottom(sloganLabel),
            Width = Dim.Fill(2),
            Text = $"Version: {versionText}"
        };
        var buildLabel = new Label
        {
            X = 1,
            Y = Pos.Bottom(versionLabel),
            Width = Dim.Fill(2),
            Text = $"Buildzaehler: {buildCounterText}"
        };

        var okButton = new Button
        {
            Text = "_OK",
            IsDefault = true
        };
        okButton.Accepting += (_, e) =>
        {
            e.Cancel = true;
            dialog.Canceled = false;
            Application.RequestStop(dialog);
        };

        dialog.Add(rainbowView, sloganLabel, versionLabel, buildLabel);
        dialog.AddButton(okButton);

        Application.Run(dialog);
    }
}
