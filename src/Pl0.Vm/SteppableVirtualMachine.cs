using System.Globalization;
using Pl0.Core;

namespace Pl0.Vm;

/// <summary>Zustandsbehaftete PL/0-VM mit Einzelschritten. / Stateful PL/0 VM with single-step execution.</summary>
public sealed class SteppableVirtualMachine
{
    private VmExecutionSession? session;
    /// <summary>Ruft den aktuellen defensiven Zustand ab. / Gets the current defensive state.</summary>
    public VmState State => session?.State ?? new VmState(0, 0, 0, [], null, 0);
    /// <summary>Gibt an, ob die Ausführung fortgesetzt werden kann. / Indicates whether execution can continue.</summary>
    public bool IsRunning => session?.IsRunning == true;

    /// <summary>Initialisiert die VM mit dem bisherigen Vertrag. / Initializes the VM with the previous contract.</summary>
    /// <param name="program">P-Code-Programm. / P-Code program.</param>
    /// <param name="io">Host-Ein-/Ausgabe. / Host input/output.</param>
    /// <param name="options">VM-Optionen. / VM options.</param>
    /// <exception cref="CultureNotFoundException">Die Diagnosesprache ist ungültig. / The diagnostic language is invalid.</exception>
    public void Initialize(IReadOnlyList<Instruction> program, IPl0Io? io = null, VirtualMachineOptions? options = null) =>
        Initialize(program, io, options, default);

    /// <summary>Initialisiert die VM mit Abbruchunterstützung. / Initializes the VM with cancellation support.</summary>
    /// <param name="program">P-Code-Programm. / P-Code program.</param>
    /// <param name="io">Host-Ein-/Ausgabe. / Host input/output.</param>
    /// <param name="options">VM-Optionen. / VM options.</param>
    /// <param name="cancellationToken">Abbruchsignal für folgende Schritte. / Cancellation signal for following steps.</param>
    /// <exception cref="CultureNotFoundException">Die Diagnosesprache ist ungültig. / The diagnostic language is invalid.</exception>
    public void Initialize(IReadOnlyList<Instruction> program, IPl0Io? io,
        VirtualMachineOptions? options, CancellationToken cancellationToken) =>
        session = VmExecutionSession.Create(program, io ?? new ConsolePl0Io(),
            options ?? VirtualMachineOptions.Default, cancellationToken);

    /// <summary>Führt genau eine Instruktion aus. / Executes exactly one instruction.</summary>
    /// <returns>Das Schrittergebnis. / The step result.</returns>
    public VmStepResult Step() => Step(default);

    /// <summary>Führt höchstens eine Instruktion mit Abbruchunterstützung aus. / Executes at most one instruction with cancellation support.</summary>
    /// <param name="cancellationToken">Abbruchsignal an dieser Instruktionsgrenze. / Cancellation signal at this instruction boundary.</param>
    /// <returns>Das idempotente Schrittergebnis. / The idempotent step result.</returns>
    public VmStepResult Step(CancellationToken cancellationToken)
    {
        if (session is not null) return session.ExecuteNext(cancellationToken);
        VmState state = State;
        return new VmStepResult(state, VmStepStatus.Error,
            [new VmDiagnostic(99, "VM ist nicht initialisiert. / VM is not initialized.")],
            VmCompletionReason.InvalidConfiguration, 0);
    }

    internal VmExecutionResult ToExecutionResult() => session?.ToExecutionResult() ??
        new VmExecutionResult([], 0, [new VmDiagnostic(99, "VM ist nicht initialisiert. / VM is not initialized.")],
            VmCompletionReason.InvalidConfiguration, 0, State);
}
