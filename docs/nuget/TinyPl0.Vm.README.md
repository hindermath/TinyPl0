# TinyPl0.Vm

## Deutsch

`TinyPl0.Vm` führt P-Code vollständig oder instruktionsweise aus. Übergib
nicht vertrauenswürdigen P-Code nur mit angemessenen Stack-, Programm- und
Budgetgrenzen. Cancellation wirkt an Instruktionsgrenzen; ein bereits
begonnener Host-I/O-Aufruf wird nicht zurückgerollt.

```csharp
using Pl0.Core;
using Pl0.Vm;

Instruction[] code = [new(Opcode.Opr, 0, 0)];
VmExecutionResult result = new VirtualMachine().Run(code);

var debugger = new SteppableVirtualMachine();
debugger.Initialize(code);
VmStepResult step = debugger.Step();
```

Nur `VmCompletionReason.Halted` bedeutet Erfolg. Diagnosen enthalten stabile
Codes und keine fremden Exception-Texte.

## English

`TinyPl0.Vm` executes P-Code either completely or one instruction at a time.
Use suitable stack, program, and budget limits for untrusted P-Code.
Cancellation is observed at instruction boundaries; started host I/O is not
rolled back. Only `VmCompletionReason.Halted` means success.
