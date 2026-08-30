using System.Globalization;
using System.Resources;

namespace Pl0.Vm;

/// <summary>
/// Konfigurationsoptionen für die PL/0-virtuelle Maschine.
///
/// Configuration options for the PL/0 virtual machine.
/// </summary>
/// <param name="StackSize">
/// Maximale Stackgröße von 3 bis 1.000.000 Einträgen.
///
/// Maximum stack size from 3 through 1,000,000 entries.
/// </param>
/// <param name="EnableStoreTrace">
/// Gibt an, ob <c>STO</c>-Schreibvorgänge an die Ausgabe weitergegeben werden.
///
/// Indicates whether <c>STO</c> writes are forwarded to the output.
/// </param>
/// <param name="Language">
/// BCP-47-Sprachcode für VM-Diagnosen; der Standard ist <c>de</c>.
///
/// BCP-47 language code for VM diagnostics; the default is <c>de</c>.
/// </param>
/// <param name="Messages">
/// Optionaler Ressourcenmanager für Dependency Injection, zum Beispiel in Tests;
/// standardmäßig wird <c>Pl0VmMessages.ResourceManager</c> verwendet.
///
/// Optional resource manager for dependency injection, for example in tests;
/// <c>Pl0VmMessages.ResourceManager</c> is used by default.
/// </param>
/// <param name="InstructionBudget">
/// Positive Höchstzahl ausgeführter Instruktionen pro Lauf oder Initialisierung;
/// der Standard ist 1.000.000. Das Budget misst Instruktionen, keine Zeit.
///
/// Positive maximum number of instructions executed per run or initialization;
/// the default is 1,000,000. The budget counts instructions, not time.
/// </param>
public sealed record VirtualMachineOptions(
    int StackSize = 500,
    bool EnableStoreTrace = false,
    string Language = "de",
    ResourceManager? Messages = null,
    int InstructionBudget = 1_000_000)
{
    /// <summary>
    /// Ruft die Standardoptionen der VM ab.
    ///
    /// Gets the default VM options.
    /// </summary>
    public static VirtualMachineOptions Default { get; } = new();
}

/// <summary>
/// Prüft VM-Optionen und erzeugt gemeinsame, lokalisierte Diagnosen.
///
/// Validates VM options and creates shared localized diagnostics.
/// </summary>
// Der gemeinsame interne Validator ist kein Teil des öffentlichen Lern-API-Vertrags.
// The shared internal validator is not part of the public learner-facing API contract.
[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
internal static class VirtualMachineOptionsValidator
{
    /// <summary>
    /// Der erste freie Code nach der bestehenden VM-Diagnose 206 kennzeichnet
    /// alle Instruktionsbudgetfehler. / The first free code after the existing
    /// VM diagnostic 206 identifies all instruction-budget failures.
    /// </summary>
    internal const int InstructionBudgetDiagnosticCode = 207;

    /// <summary>
    /// Der folgende freie Code kennzeichnet ausschließlich ungültige
    /// Stackoptionen. / The next free code identifies invalid stack options only.
    /// </summary>
    internal const int StackConfigurationDiagnosticCode = 208;

    internal const int MinimumStackSize = 3;
    internal const int MaximumStackSize = 1_000_000;

    /// <summary>
    /// Prüft zuerst die Stackgrenze und danach das Budget, damit die Reihenfolge
    /// bei mehreren Fehlern stabil bleibt. / Validates the stack boundary first
    /// and the budget second so multiple diagnostics retain a stable order.
    /// </summary>
    internal static IReadOnlyList<VmDiagnostic> Validate(
        VirtualMachineOptions options,
        ResourceManager messages,
        CultureInfo culture)
    {
        List<VmDiagnostic> diagnostics = [];

        if (options.StackSize is < MinimumStackSize or > MaximumStackSize)
        {
            diagnostics.Add(CreateDiagnostic(
                StackConfigurationDiagnosticCode,
                "Vm_E208_InvalidStackSize",
                "Die VM-Stackgröße muss zwischen 3 und 1.000.000 liegen.",
                "The VM stack size must be between 3 and 1,000,000.",
                messages,
                culture));
        }

        if (options.InstructionBudget <= 0)
        {
            diagnostics.Add(CreateDiagnostic(
                InstructionBudgetDiagnosticCode,
                "Vm_E207_InvalidInstructionBudget",
                "Das VM-Instruktionsbudget muss größer als 0 sein.",
                "The VM instruction budget must be greater than 0.",
                messages,
                culture));
        }

        return diagnostics;
    }

    /// <summary>
    /// Erzeugt die gemeinsame Diagnose vor Instruktion N+1.
    ///
    /// Creates the shared diagnostic before instruction N+1.
    /// </summary>
    internal static VmDiagnostic CreateInstructionBudgetExceededDiagnostic(
        ResourceManager messages,
        CultureInfo culture) =>
        CreateDiagnostic(
            InstructionBudgetDiagnosticCode,
            "Vm_E207_InstructionBudgetExceeded",
            "Das VM-Instruktionsbudget ist erreicht. Die nächste Instruktion wurde nicht ausgeführt.",
            "The VM instruction budget has been reached. The next instruction was not executed.",
            messages,
            culture);

    private static VmDiagnostic CreateDiagnostic(
        int code,
        string resourceKey,
        string germanFallback,
        string englishFallback,
        ResourceManager messages,
        CultureInfo culture)
    {
        var fallback = culture.TwoLetterISOLanguageName == "de"
            ? germanFallback
            : englishFallback;
        return new VmDiagnostic(code, messages.GetString(resourceKey, culture) ?? fallback);
    }
}
