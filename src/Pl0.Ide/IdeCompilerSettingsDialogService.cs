using System.Globalization;
using Pl0.Core;
using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeCompilerSettingsDialogService
{
    CompilerOptions? ShowCompilerSettingsDialog(CompilerOptions currentOptions);
}

internal sealed class TerminalGuiIdeCompilerSettingsDialogService : IIdeCompilerSettingsDialogService
{
    public CompilerOptions? ShowCompilerSettingsDialog(CompilerOptions currentOptions)
    {
        var state = new IdeCompilerSettingsDialogState(currentOptions);
        CompilerOptions? acceptedOptions = null;

        var dialog = new Dialog
        {
            Title = "Compiler-Einstellungen",
            Width = 72,
            Height = 21
        };

        var dialectLabel = new Label
        {
            X = 1,
            Y = 1,
            Text = "Dialect:"
        };
        var dialectRadio = new RadioGroup
        {
            X = 28,
            Y = 1,
            Width = 18,
            Height = 2,
            RadioLabels = ["Classic", "Extended"]
        };

        var maxLevelLabel = CreateLabel("MaxLevel:", 4);
        var maxLevelField = CreateNumericField(4);
        var maxAddressLabel = CreateLabel("MaxAddress:", 6);
        var maxAddressField = CreateNumericField(6);
        var maxIdentifierLengthLabel = CreateLabel("MaxIdentifierLength:", 8);
        var maxIdentifierLengthField = CreateNumericField(8);
        var maxNumberDigitsLabel = CreateLabel("MaxNumberDigits (abgeleitet):", 10);
        var maxNumberDigitsField = CreateNumericField(10);
        maxNumberDigitsField.ReadOnly = true;
        var maxSymbolCountLabel = CreateLabel("MaxSymbolCount:", 12);
        var maxSymbolCountField = CreateNumericField(12);
        var maxCodeLengthLabel = CreateLabel("MaxCodeLength:", 14);
        var maxCodeLengthField = CreateNumericField(14);

        var validationLabel = new Label
        {
            X = 1,
            Y = 16,
            Width = Dim.Fill(2),
            Text = string.Empty
        };

        var resetButton = new Button
        {
            Text = "_Zuruecksetzen"
        };
        resetButton.Accepting += (_, e) =>
        {
            e.Cancel = true;
            state.ResetToDefaults();
            ApplyStateToFields(state, dialectRadio, maxLevelField, maxAddressField, maxIdentifierLengthField, maxNumberDigitsField, maxSymbolCountField, maxCodeLengthField);
            validationLabel.Text = "Standardwerte geladen.";
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
            validationLabel.Text = string.Empty;

            if (!TryParseField(maxLevelField, "MaxLevel", out var maxLevel, out var parseError) ||
                !TryParseField(maxAddressField, "MaxAddress", out var maxAddress, out parseError) ||
                !TryParseField(maxIdentifierLengthField, "MaxIdentifierLength", out var maxIdentifierLength, out parseError) ||
                !TryParseField(maxSymbolCountField, "MaxSymbolCount", out var maxSymbolCount, out parseError) ||
                !TryParseField(maxCodeLengthField, "MaxCodeLength", out var maxCodeLength, out parseError))
            {
                validationLabel.Text = parseError;
                return;
            }

            var selectedDialect = GetDialectForSelectedIndex(dialectRadio.SelectedItem);
            state.SetDialect(selectedDialect);
            if (!state.TryApplyValues(maxLevel, maxAddress, maxIdentifierLength, maxSymbolCount, maxCodeLength, out var validationError))
            {
                validationLabel.Text = validationError ?? "Ungueltige Eingabe.";
                return;
            }

            acceptedOptions = state.Options;
            dialog.Canceled = false;
            Application.RequestStop(dialog);
        };

        dialectRadio.SelectedItemChanged += (_, _) =>
        {
            state.SetDialect(GetDialectForSelectedIndex(dialectRadio.SelectedItem));
            maxNumberDigitsField.Text = state.Options.MaxNumberDigits.ToString(CultureInfo.InvariantCulture);
        };

        dialog.Add(
            dialectLabel,
            dialectRadio,
            maxLevelLabel,
            maxLevelField,
            maxAddressLabel,
            maxAddressField,
            maxIdentifierLengthLabel,
            maxIdentifierLengthField,
            maxNumberDigitsLabel,
            maxNumberDigitsField,
            maxSymbolCountLabel,
            maxSymbolCountField,
            maxCodeLengthLabel,
            maxCodeLengthField,
            validationLabel);
        dialog.AddButton(resetButton);
        dialog.AddButton(cancelButton);
        dialog.AddButton(okButton);

        ApplyStateToFields(state, dialectRadio, maxLevelField, maxAddressField, maxIdentifierLengthField, maxNumberDigitsField, maxSymbolCountField, maxCodeLengthField);

        Application.Run(dialog);
        return dialog.Canceled ? null : acceptedOptions;
    }

    private static Label CreateLabel(string text, int y)
    {
        return new Label
        {
            X = 1,
            Y = y,
            Text = text
        };
    }

    private static TextField CreateNumericField(int y)
    {
        return new TextField
        {
            X = 28,
            Y = y,
            Width = 12
        };
    }

    private static void ApplyStateToFields(
        IdeCompilerSettingsDialogState state,
        RadioGroup dialectRadio,
        TextField maxLevelField,
        TextField maxAddressField,
        TextField maxIdentifierLengthField,
        TextField maxNumberDigitsField,
        TextField maxSymbolCountField,
        TextField maxCodeLengthField)
    {
        dialectRadio.SelectedItem = GetSelectedIndexForDialect(state.Options.Dialect);
        maxLevelField.Text = state.Options.MaxLevel.ToString(CultureInfo.InvariantCulture);
        maxAddressField.Text = state.Options.MaxAddress.ToString(CultureInfo.InvariantCulture);
        maxIdentifierLengthField.Text = state.Options.MaxIdentifierLength.ToString(CultureInfo.InvariantCulture);
        maxNumberDigitsField.Text = state.Options.MaxNumberDigits.ToString(CultureInfo.InvariantCulture);
        maxSymbolCountField.Text = state.Options.MaxSymbolCount.ToString(CultureInfo.InvariantCulture);
        maxCodeLengthField.Text = state.Options.MaxCodeLength.ToString(CultureInfo.InvariantCulture);
    }

    private static bool TryParseField(TextField field, string fieldName, out int value, out string? error)
    {
        if (int.TryParse(field.Text?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
        {
            error = null;
            return true;
        }

        error = $"{fieldName} muss eine Ganzzahl sein.";
        return false;
    }

    private static int GetSelectedIndexForDialect(Pl0Dialect dialect)
    {
        return dialect == Pl0Dialect.Classic ? 0 : 1;
    }

    private static Pl0Dialect GetDialectForSelectedIndex(int selectedItem)
    {
        return selectedItem == 0 ? Pl0Dialect.Classic : Pl0Dialect.Extended;
    }
}

internal sealed class IdeCompilerSettingsDialogState
{
    internal IdeCompilerSettingsDialogState(CompilerOptions options)
    {
        Options = IdeCompilerOptionsRules.Normalize(options);
    }

    internal CompilerOptions Options { get; private set; }

    internal void SetDialect(Pl0Dialect dialect)
    {
        Options = IdeCompilerOptionsRules.Normalize(Options with { Dialect = dialect });
    }

    internal bool TryApplyValues(int maxLevel, int maxAddress, int maxIdentifierLength, int maxSymbolCount, int maxCodeLength, out string? validationError)
    {
        var candidate = Options with
        {
            MaxLevel = maxLevel,
            MaxAddress = maxAddress,
            MaxIdentifierLength = maxIdentifierLength,
            MaxSymbolCount = maxSymbolCount,
            MaxCodeLength = maxCodeLength
        };

        if (!IdeCompilerOptionsRules.TryValidateAndNormalize(candidate, out var normalized, out validationError))
        {
            return false;
        }

        Options = normalized;
        return true;
    }

    internal void ResetToDefaults()
    {
        Options = IdeCompilerOptionsRules.GetResetDefaults();
    }
}
