using System.Globalization;
using Pl0.Core;
using Terminal.Gui;

namespace Pl0.Ide;

internal interface IIdeCompilerSettingsDialogService
{
    IdeCompilerSettingsDialogResult? ShowCompilerSettingsDialog(CompilerOptions currentOptions, IdeCodeDisplayMode currentCodeDisplayMode);
}

internal sealed class TerminalGuiIdeCompilerSettingsDialogService : IIdeCompilerSettingsDialogService
{
    private readonly IApplication? application;

    public TerminalGuiIdeCompilerSettingsDialogService(IApplication? application = null)
    {
        this.application = application;
    }

    public IdeCompilerSettingsDialogResult? ShowCompilerSettingsDialog(CompilerOptions currentOptions, IdeCodeDisplayMode currentCodeDisplayMode)
    {
        var app = application ?? throw new InvalidOperationException("Die IDE-Anwendung wurde nicht initialisiert.");
        var state = new IdeCompilerSettingsDialogState(currentOptions, currentCodeDisplayMode);
        IdeCompilerSettingsDialogResult? acceptedOptions = null;

        var dialog = new Dialog
        {
            Title = "Compiler-Einstellungen",
            Width = 72,
            Height = 24
        };

        var dialectLabel = new Label
        {
            X = 1,
            Y = 1,
            Text = "Dialect:"
        };
        // Die Auswahl-API trennt stabile Werte von angezeigten Texten; die Indizes bleiben dadurch eindeutig.
        // The selector API separates stable values from labels, keeping the indices unambiguous.
        var dialectRadio = new OptionSelector
        {
            X = 28,
            Y = 1,
            Width = 18,
            Height = 2,
            Values = [0, 1],
            Labels = ["Classic", "Extended"]
        };

        var displayModeLabel = new Label
        {
            X = 1,
            Y = 4,
            Text = "Code-Anzeige:"
        };
        var displayModeRadio = new OptionSelector
        {
            X = 28,
            Y = 4,
            Width = 20,
            Height = 2,
            Values = [0, 1],
            Labels = ["P-Code", "Assembler"]
        };

        var maxLevelLabel = CreateLabel("MaxLevel:", 7);
        var maxLevelField = CreateNumericField(7);
        var maxAddressLabel = CreateLabel("MaxAddress:", 9);
        var maxAddressField = CreateNumericField(9);
        var maxIdentifierLengthLabel = CreateLabel("MaxIdentifierLength:", 11);
        var maxIdentifierLengthField = CreateNumericField(11);
        var maxNumberDigitsLabel = CreateLabel("MaxNumberDigits (abgeleitet):", 13);
        var maxNumberDigitsField = CreateNumericField(13);
        maxNumberDigitsField.ReadOnly = true;
        var maxSymbolCountLabel = CreateLabel("MaxSymbolCount:", 15);
        var maxSymbolCountField = CreateNumericField(15);
        var maxCodeLengthLabel = CreateLabel("MaxCodeLength:", 17);
        var maxCodeLengthField = CreateNumericField(17);

        var validationLabel = new Label
        {
            X = 1,
            Y = 19,
            Width = Dim.Fill(2),
            Text = string.Empty
        };

        var resetButton = new Button
        {
            Text = "_Zuruecksetzen"
        };
        resetButton.Accepting += (_, e) =>
        {
            e.Handled = true;
            state.ResetToDefaults();
            ApplyStateToFields(state, dialectRadio, displayModeRadio, maxLevelField, maxAddressField, maxIdentifierLengthField, maxNumberDigitsField, maxSymbolCountField, maxCodeLengthField);
            validationLabel.Text = "Standardwerte geladen.";
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
            validationLabel.Text = string.Empty;

            if (!TryParseField(maxLevelField, "MaxLevel", out var maxLevel, out var parseError) ||
                !TryParseField(maxAddressField, "MaxAddress", out var maxAddress, out parseError) ||
                !TryParseField(maxIdentifierLengthField, "MaxIdentifierLength", out var maxIdentifierLength, out parseError) ||
                !TryParseField(maxSymbolCountField, "MaxSymbolCount", out var maxSymbolCount, out parseError) ||
                !TryParseField(maxCodeLengthField, "MaxCodeLength", out var maxCodeLength, out parseError))
            {
                validationLabel.Text = parseError ?? "Ungueltige Eingabe.";
                return;
            }

            var selectedDialect = GetDialectForSelectedIndex(dialectRadio.Value ?? 0);
            state.SetDialect(selectedDialect);
            state.SetCodeDisplayMode(GetDisplayModeForSelectedIndex(displayModeRadio.Value ?? 0));
            if (!state.TryApplyValues(maxLevel, maxAddress, maxIdentifierLength, maxSymbolCount, maxCodeLength, out var validationError))
            {
                validationLabel.Text = validationError ?? "Ungueltige Eingabe.";
                return;
            }

            acceptedOptions = new IdeCompilerSettingsDialogResult(state.Options, state.CodeDisplayMode);
            app.RequestStop(dialog);
        };

        dialectRadio.ValueChanged += (_, _) =>
        {
            state.SetDialect(GetDialectForSelectedIndex(dialectRadio.Value ?? 0));
            maxNumberDigitsField.Text = state.Options.MaxNumberDigits.ToString(CultureInfo.InvariantCulture);
        };

        displayModeRadio.ValueChanged += (_, _) =>
        {
            state.SetCodeDisplayMode(GetDisplayModeForSelectedIndex(displayModeRadio.Value ?? 0));
        };

        dialog.Add(
            dialectLabel,
            dialectRadio,
            displayModeLabel,
            displayModeRadio,
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

        ApplyStateToFields(state, dialectRadio, displayModeRadio, maxLevelField, maxAddressField, maxIdentifierLengthField, maxNumberDigitsField, maxSymbolCountField, maxCodeLengthField);

        // Nur ein erfolgreich bestätigter Dialog setzt acceptedOptions; Abbruch und Esc lassen es leer.
        // Only a confirmed dialog sets acceptedOptions; Cancel and Esc leave it empty.
        app.Run(dialog);
        return acceptedOptions;
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
        OptionSelector dialectRadio,
        OptionSelector displayModeRadio,
        TextField maxLevelField,
        TextField maxAddressField,
        TextField maxIdentifierLengthField,
        TextField maxNumberDigitsField,
        TextField maxSymbolCountField,
        TextField maxCodeLengthField)
    {
        dialectRadio.Value = GetSelectedIndexForDialect(state.Options.Dialect);
        displayModeRadio.Value = GetSelectedIndexForDisplayMode(state.CodeDisplayMode);
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

    private static int GetSelectedIndexForDisplayMode(IdeCodeDisplayMode codeDisplayMode)
    {
        return codeDisplayMode == IdeCodeDisplayMode.PCode ? 0 : 1;
    }

    private static IdeCodeDisplayMode GetDisplayModeForSelectedIndex(int selectedItem)
    {
        return selectedItem == 0 ? IdeCodeDisplayMode.PCode : IdeCodeDisplayMode.Assembler;
    }
}

internal sealed class IdeCompilerSettingsDialogState
{
    internal IdeCompilerSettingsDialogState(CompilerOptions options, IdeCodeDisplayMode codeDisplayMode)
    {
        Options = IdeCompilerOptionsRules.Normalize(options);
        CodeDisplayMode = codeDisplayMode;
    }

    internal CompilerOptions Options { get; private set; }
    internal IdeCodeDisplayMode CodeDisplayMode { get; private set; }

    internal void SetDialect(Pl0Dialect dialect)
    {
        Options = IdeCompilerOptionsRules.Normalize(Options with { Dialect = dialect });
    }

    internal void SetCodeDisplayMode(IdeCodeDisplayMode codeDisplayMode)
    {
        CodeDisplayMode = codeDisplayMode;
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
        CodeDisplayMode = IdeCodeDisplayMode.Assembler;
    }
}
