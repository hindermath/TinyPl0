using System.Globalization;
using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeRuntimeDialogService
{
    int? ReadInt(string prompt);
}

internal sealed class TerminalGuiIdeRuntimeDialogService : IIdeRuntimeDialogService
{
    private readonly IApplication? application;

    public TerminalGuiIdeRuntimeDialogService(IApplication? application = null)
    {
        this.application = application;
    }

    public int? ReadInt(string prompt)
    {
        var app = application ?? throw new InvalidOperationException("Die IDE-Anwendung wurde nicht initialisiert.");
        int? value = null;

        var dialog = new Dialog
        {
            Title = "Eingabe",
            Width = 64,
            Height = 10
        };
        var promptLabel = new Label
        {
            X = 1,
            Y = 1,
            Width = Dim.Fill(2),
            Text = prompt
        };
        var inputField = new TextField
        {
            X = 1,
            Y = 3,
            Width = Dim.Fill(2)
        };
        var validationLabel = new Label
        {
            X = 1,
            Y = 5,
            Width = Dim.Fill(2),
            Text = string.Empty
        };

        var cancelButton = new Button
        {
            Text = "_Abbrechen"
        };
        cancelButton.Accepting += (_, e) =>
        {
            e.Handled = true;
            app.RequestStop(dialog);
        };

        var okButton = new Button
        {
            Text = "_OK",
            IsDefault = true
        };
        okButton.Accepting += (_, e) =>
        {
            e.Handled = true;
            var input = inputField.Text?.ToString() ?? string.Empty;
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
            {
                validationLabel.Text = "Bitte eine gueltige Ganzzahl eingeben.";
                return;
            }

            value = parsed;
            app.RequestStop(dialog);
        };

        dialog.Add(promptLabel, inputField, validationLabel);
        dialog.AddButton(cancelButton);
        dialog.AddButton(okButton);

        app.Run(dialog);
        return value;
    }
}
