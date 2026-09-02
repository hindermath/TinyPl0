using System.Globalization;
using System.Resources;

namespace Pl0.Vm;

/// <summary>
/// Konfigurationsoptionen für die PL/0-virtuelle Maschine.
///
/// Configuration options for the PL/0 virtual machine.
/// </summary>
public sealed record VirtualMachineOptions
{
    /// <summary>Erstellt Optionen mit allen aktuellen Grenzen. / Creates options with all current limits.</summary>
    /// <param name="StackSize">Stackgröße von 3 bis 1.000.000. / Stack size from 3 through 1,000,000.</param>
    /// <param name="EnableStoreTrace">Aktiviert STO-Ausgaben. / Enables STO output.</param>
    /// <param name="Language">BCP-47-Sprache für Diagnosen. / BCP-47 language for diagnostics.</param>
    /// <param name="Messages">Optionaler Ressourcenmanager. / Optional resource manager.</param>
    /// <param name="InstructionBudget">Budget von 1 bis 10.000.000. / Budget from 1 through 10,000,000.</param>
    /// <param name="MaximumProgramLength">Programmgrenze von 1 bis 100.000. / Program limit from 1 through 100,000.</param>
    public VirtualMachineOptions(int StackSize = 500, bool EnableStoreTrace = false,
        string Language = "de", ResourceManager? Messages = null,
        int InstructionBudget = 1_000_000, int MaximumProgramLength = 10_000)
    {
        this.StackSize = StackSize;
        this.EnableStoreTrace = EnableStoreTrace;
        this.Language = Language;
        this.Messages = Messages;
        this.InstructionBudget = InstructionBudget;
        this.MaximumProgramLength = MaximumProgramLength;
    }

    /// <summary>Erstellt den bisherigen Fünf-Parameter-Vertrag. / Creates the previous five-parameter contract.</summary>
    /// <param name="StackSize">Stackgröße. / Stack size.</param>
    /// <param name="EnableStoreTrace">STO-Ausgabe. / STO output.</param>
    /// <param name="Language">Diagnosesprache. / Diagnostic language.</param>
    /// <param name="Messages">Ressourcenmanager. / Resource manager.</param>
    /// <param name="InstructionBudget">Instruktionsbudget. / Instruction budget.</param>
    public VirtualMachineOptions(int StackSize, bool EnableStoreTrace, string Language,
        ResourceManager? Messages, int InstructionBudget)
        : this(StackSize, EnableStoreTrace, Language, Messages, InstructionBudget, 10_000) { }

    /// <summary>Ruft die Stackgröße ab. / Gets the stack size.</summary>
    public int StackSize { get; init; }
    /// <summary>Ruft die STO-Ausgabeoption ab. / Gets the STO output option.</summary>
    public bool EnableStoreTrace { get; init; }
    /// <summary>Ruft die Diagnosesprache ab. / Gets the diagnostic language.</summary>
    public string Language { get; init; }
    /// <summary>Ruft den Ressourcenmanager ab. / Gets the resource manager.</summary>
    public ResourceManager? Messages { get; init; }
    /// <summary>Ruft das Instruktionsbudget ab. / Gets the instruction budget.</summary>
    public int InstructionBudget { get; init; }
    /// <summary>Ruft die maximale Programmlänge ab. / Gets the maximum program length.</summary>
    public int MaximumProgramLength { get; init; }
    /// <summary>Ruft die Standardoptionen ab. / Gets the default options.</summary>
    public static VirtualMachineOptions Default { get; } = new();
}

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
internal static class VirtualMachineOptionsValidator
{
    internal const int InstructionBudgetDiagnosticCode = 207;
    internal const int StackConfigurationDiagnosticCode = 208;
    internal const int ProgramDiagnosticCode = 209;
    internal const int CancellationDiagnosticCode = 210;
    internal const int IoDiagnosticCode = 211;
    internal const int MinimumStackSize = 3;
    internal const int MaximumStackSize = 1_000_000;
    internal const int MaximumInstructionBudget = 10_000_000;
    internal const int MaximumAllowedProgramLength = 100_000;

    internal static IReadOnlyList<VmDiagnostic> Validate(VirtualMachineOptions options,
        ResourceManager messages, CultureInfo culture)
    {
        List<VmDiagnostic> diagnostics = [];
        if (options.StackSize is < MinimumStackSize or > MaximumStackSize)
            diagnostics.Add(CreateDiagnostic(StackConfigurationDiagnosticCode, "Vm_E208_InvalidStackSize",
                "Die VM-Stackgröße muss zwischen 3 und 1.000.000 liegen.",
                "The VM stack size must be between 3 and 1,000,000.", messages, culture));
        if (options.InstructionBudget is < 1 or > MaximumInstructionBudget)
            diagnostics.Add(CreateDiagnostic(InstructionBudgetDiagnosticCode, "Vm_E207_InvalidInstructionBudget",
                "Das VM-Instruktionsbudget muss zwischen 1 und 10.000.000 liegen.",
                "The VM instruction budget must be between 1 and 10,000,000.", messages, culture));
        if (options.MaximumProgramLength is < 1 or > MaximumAllowedProgramLength)
            diagnostics.Add(CreateDiagnostic(ProgramDiagnosticCode, "Vm_E209_InvalidMaximumProgramLength",
                "Die maximale Programmlänge muss zwischen 1 und 100.000 liegen.",
                "The maximum program length must be between 1 and 100,000.", messages, culture));
        return diagnostics;
    }

    internal static VmDiagnostic BudgetExceeded(ResourceManager messages, CultureInfo culture) =>
        CreateDiagnostic(InstructionBudgetDiagnosticCode, "Vm_E207_InstructionBudgetExceeded",
            "Das VM-Instruktionsbudget ist erreicht. Die nächste Instruktion wurde nicht ausgeführt.",
            "The VM instruction budget has been reached. The next instruction was not executed.", messages, culture);
    internal static VmDiagnostic Cancelled(ResourceManager messages, CultureInfo culture) =>
        CreateDiagnostic(CancellationDiagnosticCode, "Vm_E210_Cancelled",
            "Die VM-Ausführung wurde abgebrochen.", "VM execution was cancelled.", messages, culture);
    internal static VmDiagnostic ProgramError(string detail, ResourceManager messages, CultureInfo culture) =>
        new(ProgramDiagnosticCode, string.Format(culture, messages.GetString("Vm_E209_InvalidProgram", culture) ??
            (culture.TwoLetterISOLanguageName == "de" ? "Ungültiges P-Code-Programm: {0}." : "Invalid P-Code program: {0}."), detail));
    internal static VmDiagnostic IoError(ResourceManager messages, CultureInfo culture) =>
        CreateDiagnostic(IoDiagnosticCode, "Vm_E211_IoFailure",
            "Die Host-Ein-/Ausgabe ist fehlgeschlagen.", "Host input/output failed.", messages, culture);

    private static VmDiagnostic CreateDiagnostic(int code, string key, string de, string en,
        ResourceManager messages, CultureInfo culture) =>
        new(code, messages.GetString(key, culture) ?? (culture.TwoLetterISOLanguageName == "de" ? de : en));
}
