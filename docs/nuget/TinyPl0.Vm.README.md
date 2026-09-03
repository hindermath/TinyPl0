# TinyPl0.Vm

## Deutsch

`TinyPl0.Vm` ist eine einbettbare und didaktische virtuelle Maschine für
.NET 10. Sie führt den typisierten P-Code aus, den `TinyPl0.Core` aus
PL/0-Quelltext erzeugt. Die Stack-Architektur folgt dem historischen
PL/0-Modell mit Programmzähler, Basiszeiger und Stackzeiger.

Das Paket hängt von exakt derselben stabilen Version von `TinyPl0.Core` ab.
Installierst du `TinyPl0.Vm` über NuGet, wird diese passende Core-Version
automatisch aufgelöst.

### Für wen ist das Paket gedacht?

- Lernende können Stack, Aktivierungsrahmen und P-Code-Ausführung untersuchen.
- Lehrende können vollständige Läufe und einzelne Instruktionsschritte zeigen.
- Host-Entwickler können PL/0 mit eigener Ein- und Ausgabe einbetten.
- Werkzeugentwickler können Debugger und Zustandsansichten aufbauen.

### Was ist enthalten?

- `VirtualMachine` für einen vollständigen Lauf bis zu einem Abschlussgrund
- `SteppableVirtualMachine` für die Ausführung einzelner Instruktionen
- `IPl0Io` als kleine Grenze zwischen P-Code und Host-Ein-/Ausgabe
- `BufferedPl0Io` und `ConsolePl0Io` als fertige I/O-Implementierungen
- `VirtualMachineOptions` für Stack-, Programm- und Instruktionsgrenzen
- `VmExecutionResult`, `VmStepResult` und `VmState` für strukturierte Ergebnisse
- stabile `VmCompletionReason`- und Diagnosewerte für Fehlerbehandlung

### Installation

Installiere das VM-Paket mit der .NET-CLI:

```console
dotnet add package TinyPl0.Vm
```

Für die direkte Übersetzung von PL/0-Quelltext verwendest du zusätzlich die
automatisch gekoppelte API aus `TinyPl0.Core`.

### Schnellstart: kompilieren und vollständig ausführen

Das folgende vollständige Beispiel kompiliert ein PL/0-Programm, verwendet
gepufferte Ein- und Ausgabe und führt den P-Code mit ausdrücklichen Grenzen
aus.

```csharp
using Pl0.Core;
using Pl0.Vm;

string source = """
    const answer = 42;
    begin
      ! answer
    end.
    """;

CompilerOptions compilerOptions = new(Pl0Dialect.Extended, Language: "de");
CompilationResult compilation = new Pl0Compiler().Compile(source, compilerOptions);
if (!compilation.Success)
{
    foreach (CompilerDiagnostic diagnostic in compilation.Diagnostics)
    {
        Console.Error.WriteLine($"{diagnostic.Code}: {diagnostic.Message}");
    }

    return;
}

BufferedPl0Io io = new();
VirtualMachineOptions options = new(
    StackSize: 500,
    Language: "de",
    InstructionBudget: 10_000,
    MaximumProgramLength: 1_000);

VmExecutionResult result = new VirtualMachine().Run(
    compilation.Instructions,
    io,
    options);

if (result.Reason != VmCompletionReason.Halted)
{
    foreach (VmDiagnostic diagnostic in result.Diagnostics)
    {
        Console.Error.WriteLine($"{diagnostic.Code}: {diagnostic.Message}");
    }

    return;
}

Console.WriteLine(string.Join(", ", io.Output));
```

Der typische Ablauf ist:

```text
PL/0-Quelltext
  -> TinyPl0.Core
  -> typisierter P-Code
  -> TinyPl0.Vm + IPl0Io + VirtualMachineOptions
  -> VmExecutionResult
```

### Vollständige und schrittweise Ausführung

`VirtualMachine.Run` eignet sich für Anwendungen, die nur das Endergebnis
benötigen. `SteppableVirtualMachine` hält den Zustand zwischen Aufrufen und
eignet sich für Debugger, Unterricht und Ablaufvisualisierungen.

Dieses kurze Beispiel führt genau einen Schritt aus und liest den Zustand über
beschreibende Property-Namen:

```csharp
using Pl0.Core;
using Pl0.Vm;

Instruction[] program = [new(Opcode.Opr, 0, 0)];

SteppableVirtualMachine debugger = new();
debugger.Initialize(program, options: new VirtualMachineOptions(Language: "de"));

VmStepResult step = debugger.Step();
Console.WriteLine(
    $"P={step.State.ProgramCounter}, B={step.State.BasePointer}, " +
    $"T={step.State.StackTop}, Grund={step.Reason}");
```

Rufe `Initialize` einmal auf. Danach führt jeder Aufruf von `Step` höchstens
eine Instruktion aus. Solange `IsRunning` den Wert `true` hat, kann die
Ausführung fortgesetzt werden.

### Host-Ein- und -Ausgabe mit IPl0Io

`IPl0Io` enthält nur `ReadInt()` und `WriteInt(int)`. Dadurch entscheidet der
Host, ob Werte aus der Konsole, aus einem Testpuffer, aus einer Lernoberfläche
oder aus einer anderen Quelle kommen.

`BufferedPl0Io` eignet sich für deterministische Tests und Dienste ohne direkte
Konsole. Für eine eigene Integration implementierst du `IPl0Io`. Verarbeite
Eingaben dort nach den Regeln deines Hosts und gib interne Ausnahmeinformationen
nicht ungeprüft an Endnutzer weiter.

### Grenzen, Abbruch und nicht vertrauenswürdiger P-Code

Setze für nicht vertrauenswürdigen P-Code immer passende Grenzen in
`VirtualMachineOptions`:

- `StackSize` begrenzt den VM-Stack.
- `MaximumProgramLength` begrenzt die Anzahl der P-Code-Instruktionen.
- `InstructionBudget` begrenzt die Zahl der begonnenen Instruktionen.
- `Language` wählt die Sprache der VM-Diagnosen, zum Beispiel `"de"` oder `"en"`.

Übergib für abbrechbare Abläufe zusätzlich ein `CancellationToken` an `Run`
oder `Initialize`. Die VM prüft den Abbruch an Instruktionsgrenzen. Ein bereits
begonnener Aufruf von `IPl0Io` wird nicht zurückgerollt. Der Host muss deshalb
selbst festlegen, wie lange seine Ein- und Ausgabe blockieren darf.

Ungültige Optionen und ein ungültiges Programm werden vor der eigentlichen
Ausführung abgelehnt. Die Grenzen schützen den VM-Prozess, sind aber keine
vollständige Sicherheits-Sandbox für beliebigen fremden Code.

### Erfolg und Fehlerbehandlung

Nur `VmCompletionReason.Halted` bedeutet einen regulären Erfolg. Prüfe deshalb
`VmExecutionResult.Reason` oder `VmExecutionResult.Success`. Eine leere
Diagnoseliste allein ist kein ausreichendes Erfolgskriterium.

Andere Gründe beschreiben zum Beispiel Abbruch, ein erreichtes
Instruktionsbudget, ungültigen P-Code, Stack- oder Rechenfehler und fehlgeschlagene
Host-Ein-/Ausgabe. Zeige Diagnosecode und Meldung als Text an. Die Diagnosen
verwenden stabile Codes und enthalten keine fremden Exception-Texte.

### Weiterführende Dokumentation

- [Handbuch zur Einbettung von Core und VM](https://github.com/hindermath/TinyPl0/blob/main/docs/HOSTING_VM.md)
- [P-Code-Instruktionssatz der virtuellen Maschine](https://github.com/hindermath/TinyPl0/blob/main/docs/VM_INSTRUCTION_SET.md)
- [TinyPl0-Architektur und VM-Modell](https://github.com/hindermath/TinyPl0/blob/main/docs/ARCHITECTURE.md)
- [TinyPl0.Vm API-Referenz](https://hindermath.github.io/TinyPl0/api/Pl0.Vm.html)
- [TinyPl0-Dokumentationsportal](https://hindermath.github.io/TinyPl0/)
- [Quellcode und Issue-Tracker](https://github.com/hindermath/TinyPl0)

## English

`TinyPl0.Vm` is an embeddable and educational virtual machine for .NET 10. It
executes the typed P-Code that `TinyPl0.Core` generates from PL/0 source text.
The stack architecture follows the historical PL/0 model with a program
counter, base pointer, and stack pointer.

The package depends on the exact same stable version of `TinyPl0.Core`. When
you install `TinyPl0.Vm` through NuGet, this matching Core version is resolved
automatically.

### Who is this package for?

- Learners can examine the stack, activation records, and P-Code execution.
- Teachers can demonstrate complete runs and individual instruction steps.
- Host developers can embed PL/0 with their own input and output.
- Tool developers can build debuggers and state views.

### What is included?

- `VirtualMachine` for a complete run until a completion reason is reached
- `SteppableVirtualMachine` for executing individual instructions
- `IPl0Io` as a small boundary between P-Code and host input/output
- `BufferedPl0Io` and `ConsolePl0Io` as ready-to-use I/O implementations
- `VirtualMachineOptions` for stack, program, and instruction limits
- `VmExecutionResult`, `VmStepResult`, and `VmState` for structured results
- stable `VmCompletionReason` and diagnostic values for error handling

### Installation

Install the VM package with the .NET CLI:

```console
dotnet add package TinyPl0.Vm
```

To compile PL/0 source text directly, also use the automatically paired API
from `TinyPl0.Core`.

### Quick start: compile and run completely

The following complete example compiles a PL/0 program, uses buffered input
and output, and executes the P-Code with explicit limits.

```csharp
using Pl0.Core;
using Pl0.Vm;

string source = """
    const answer = 42;
    begin
      ! answer
    end.
    """;

CompilerOptions compilerOptions = new(Pl0Dialect.Extended, Language: "en");
CompilationResult compilation = new Pl0Compiler().Compile(source, compilerOptions);
if (!compilation.Success)
{
    foreach (CompilerDiagnostic diagnostic in compilation.Diagnostics)
    {
        Console.Error.WriteLine($"{diagnostic.Code}: {diagnostic.Message}");
    }

    return;
}

BufferedPl0Io io = new();
VirtualMachineOptions options = new(
    StackSize: 500,
    Language: "en",
    InstructionBudget: 10_000,
    MaximumProgramLength: 1_000);

VmExecutionResult result = new VirtualMachine().Run(
    compilation.Instructions,
    io,
    options);

if (result.Reason != VmCompletionReason.Halted)
{
    foreach (VmDiagnostic diagnostic in result.Diagnostics)
    {
        Console.Error.WriteLine($"{diagnostic.Code}: {diagnostic.Message}");
    }

    return;
}

Console.WriteLine(string.Join(", ", io.Output));
```

The typical flow is:

```text
PL/0 source text
  -> TinyPl0.Core
  -> typed P-Code
  -> TinyPl0.Vm + IPl0Io + VirtualMachineOptions
  -> VmExecutionResult
```

### Complete and stepwise execution

`VirtualMachine.Run` is suitable for applications that only need the final
result. `SteppableVirtualMachine` keeps its state between calls and is suitable
for debuggers, teaching, and execution visualizations.

This short example executes exactly one step and reads the state through
descriptive property names:

```csharp
using Pl0.Core;
using Pl0.Vm;

Instruction[] program = [new(Opcode.Opr, 0, 0)];

SteppableVirtualMachine debugger = new();
debugger.Initialize(program, options: new VirtualMachineOptions(Language: "en"));

VmStepResult step = debugger.Step();
Console.WriteLine(
    $"P={step.State.ProgramCounter}, B={step.State.BasePointer}, " +
    $"T={step.State.StackTop}, reason={step.Reason}");
```

Call `Initialize` once. After that, each call to `Step` executes at most one
instruction. Execution can continue while `IsRunning` is `true`.

### Host input and output with IPl0Io

`IPl0Io` contains only `ReadInt()` and `WriteInt(int)`. This lets the host
decide whether values come from the console, a test buffer, a learning
interface, or another source.

`BufferedPl0Io` is suitable for deterministic tests and services without a
direct console. Implement `IPl0Io` for a custom integration. Process input
according to your host rules and do not expose internal exception information
to end users without review.

### Limits, cancellation, and untrusted P-Code

Always configure suitable `VirtualMachineOptions` limits for untrusted P-Code:

- `StackSize` limits the VM stack.
- `MaximumProgramLength` limits the number of P-Code instructions.
- `InstructionBudget` limits the number of started instructions.
- `Language` selects the VM diagnostic language, such as `"de"` or `"en"`.

For cancellable operations, also pass a `CancellationToken` to `Run` or
`Initialize`. The VM observes cancellation at instruction boundaries. An
already started `IPl0Io` call is not rolled back. The host must therefore
define how long its input and output operations may block.

Invalid options and an invalid program are rejected before actual execution.
The limits protect the VM process, but they are not a complete security sandbox
for arbitrary foreign code.

### Success and error handling

Only `VmCompletionReason.Halted` means normal success. Therefore, check
`VmExecutionResult.Reason` or `VmExecutionResult.Success`. An empty diagnostic
list alone is not a sufficient success condition.

Other reasons describe cancellation, an exhausted instruction budget, invalid
P-Code, stack or arithmetic faults, and failed host input/output. Expose the
diagnostic code and message as text. Diagnostics use stable codes and do not
contain foreign exception text.

### Further documentation

- [Guide for embedding Core and VM](https://github.com/hindermath/TinyPl0/blob/main/docs/HOSTING_VM.md)
- [P-Code instruction set of the virtual machine](https://github.com/hindermath/TinyPl0/blob/main/docs/VM_INSTRUCTION_SET.md)
- [TinyPl0 architecture and VM model](https://github.com/hindermath/TinyPl0/blob/main/docs/ARCHITECTURE.md)
- [TinyPl0.Vm API reference](https://hindermath.github.io/TinyPl0/api/Pl0.Vm.html)
- [TinyPl0 documentation portal](https://hindermath.github.io/TinyPl0/)
- [Source code and issue tracker](https://github.com/hindermath/TinyPl0)
