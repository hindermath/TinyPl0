namespace Pl0.Vm;

/// <summary>
/// Beschreibt den stabilen Ausführungszustand oder Abschlussgrund einer VM-Ausführung.
///
/// Describes the stable execution state or completion reason of a VM execution.
/// </summary>
public enum VmCompletionReason
{
    /// <summary>Die Ausführung läuft und ist noch nicht terminal. / Execution is running and is not terminal yet.</summary>
    Running = 0,
    /// <summary>Das Programm wurde regulär beendet. / The program halted normally.</summary>
    Halted = 1,
    /// <summary>Die Ausführung wurde abgebrochen. / Execution was cancelled.</summary>
    Cancelled = 2,
    /// <summary>Das Instruktionsbudget wurde erreicht. / The instruction budget was reached.</summary>
    InstructionBudgetExceeded = 3,
    /// <summary>Die Konfiguration ist ungültig. / The configuration is invalid.</summary>
    InvalidConfiguration = 4,
    /// <summary>Das P-Code-Programm ist ungültig. / The P-Code program is invalid.</summary>
    InvalidProgram = 5,
    /// <summary>Ein Stackfehler ist aufgetreten. / A stack fault occurred.</summary>
    StackFault = 6,
    /// <summary>Ein Rechenfehler ist aufgetreten. / An arithmetic fault occurred.</summary>
    ArithmeticFault = 7,
    /// <summary>Die Eingabe ist beendet. / The input stream ended.</summary>
    InputEndOfStream = 8,
    /// <summary>Das Eingabeformat ist ungültig. / The input format is invalid.</summary>
    InputFormatError = 9,
    /// <summary>Die Host-Ein-/Ausgabe ist fehlgeschlagen. / Host input/output failed.</summary>
    IoFault = 10,
    /// <summary>Ein sonstiger Laufzeitfehler ist aufgetreten. / Another runtime fault occurred.</summary>
    RuntimeFault = 11
}
