using System.Globalization;
using Pl0.Core;

namespace Pl0.Vm;

/// <summary>Führt PL/0-P-Code auf der gemeinsamen Stack-VM aus. / Executes PL/0 P-Code on the shared stack VM.</summary>
public sealed class VirtualMachine
{
    /// <summary>Führt ein Programm mit dem bisherigen Vertrag aus. / Runs a program with the previous contract.</summary>
    /// <param name="program">P-Code-Programm. / P-Code program.</param>
    /// <param name="io">Host-Ein-/Ausgabe. / Host input/output.</param>
    /// <param name="options">VM-Optionen. / VM options.</param>
    /// <returns>Das terminale Ausführungsergebnis. / The terminal execution result.</returns>
    /// <exception cref="CultureNotFoundException">Die Diagnosesprache ist ungültig. / The diagnostic language is invalid.</exception>
    public VmExecutionResult Run(IReadOnlyList<Instruction> program, IPl0Io? io = null,
        VirtualMachineOptions? options = null) => Run(program, io, options, default);

    /// <summary>Führt ein Programm mit Abbruchunterstützung aus. / Runs a program with cancellation support.</summary>
    /// <param name="program">P-Code-Programm. / P-Code program.</param>
    /// <param name="io">Host-Ein-/Ausgabe. / Host input/output.</param>
    /// <param name="options">VM-Optionen. / VM options.</param>
    /// <param name="cancellationToken">Abbruchsignal. / Cancellation signal.</param>
    /// <returns>Das terminale Ausführungsergebnis. / The terminal execution result.</returns>
    /// <exception cref="CultureNotFoundException">Die Diagnosesprache ist ungültig. / The diagnostic language is invalid.</exception>
    public VmExecutionResult Run(IReadOnlyList<Instruction> program, IPl0Io? io,
        VirtualMachineOptions? options, CancellationToken cancellationToken)
    {
        var vm = new SteppableVirtualMachine();
        vm.Initialize(program, io, options, cancellationToken);
        while (vm.IsRunning) _ = vm.Step();
        return vm.ToExecutionResult();
    }
}
