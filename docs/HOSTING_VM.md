# TinyPl0 als Hostbibliothek / TinyPl0 as a Host Library

## Deutsch

Installiere TinyPl0.Core und TinyPl0.Vm immer in derselben stabilen Version.
Core kompiliert PL/0, Vm führt den erzeugten P-Code aus. Ein Host stellt I/O
ausschließlich über IPl0Io bereit.

Prüfe zuerst CompilationResult.Success. Erzeuge danach begrenzte
VirtualMachineOptions und rufe VirtualMachine.Run auf. Für Debugger rufst Du
SteppableVirtualMachine.Initialize einmal und danach Step auf.

Success ist nur bei VmCompletionReason.Halted wahr. Cancellation wird vor dem
nächsten Dispatch geprüft. Budgetende verhindert Instruktion N+1 ohne
Seiteneffekt. Ungültige Optionen und ungültiger P-Code werden vor der
Stackallokation terminal. Ein begonnener IPl0Io-Aufruf kann nicht
zurückgerollt werden; Cancellation während des Aufrufs wirkt an der nächsten
Grenze.

Nach jedem terminalen Grund liefert Step dieselbe Momentaufnahme, denselben
Grund, dieselben Diagnosen und dieselbe Instruktionszahl. Stackarrays sind
defensive Kopien.

## English

Install TinyPl0.Core and TinyPl0.Vm at the same stable version. Core compiles
PL/0 and Vm executes the generated P-Code. A host supplies all I/O through
IPl0Io.

Check CompilationResult.Success first. Then create bounded
VirtualMachineOptions and call VirtualMachine.Run. For a debugger, call
SteppableVirtualMachine.Initialize once and then Step.

Success is true only for VmCompletionReason.Halted. Cancellation is checked
before the next dispatch, and budget exhaustion prevents instruction N+1
without side effects. Invalid options and P-Code become terminal before stack
allocation. Started host I/O cannot be rolled back; cancellation observed
during it takes effect at the next instruction boundary. Repeated terminal
steps return the same projection, and stack arrays are defensive copies.
