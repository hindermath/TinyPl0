using System.Globalization;
using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeRuntimeDialogService
{
    int? ReadInt(string prompt);
}

internal sealed class TerminalGuiIdeRuntimeDialogService : IIdeRuntimeDialogService
{
    public int? ReadInt(string prompt)
    {
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
            e.Cancel = true;
            dialog.Canceled = true;
            Application.RequestStop(dialog);
        };

        var okButton = new Button
        {
            Text = "_OK",
            IsDefault = true
        };
        okButton.Accepting += (_, e) =>
        {
            e.Cancel = true;
            var input = inputField.Text?.ToString() ?? string.Empty;
            if (!int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed))
            {
                validationLabel.Text = "Bitte eine gueltige Ganzzahl eingeben.";
                return;
            }

            value = parsed;
            dialog.Canceled = false;
            Application.RequestStop(dialog);
        };

        dialog.Add(promptLabel, inputField, validationLabel);
        dialog.AddButton(cancelButton);
        dialog.AddButton(okButton);

        Application.Run(dialog);
        return dialog.Canceled ? null : value;
    }
}
