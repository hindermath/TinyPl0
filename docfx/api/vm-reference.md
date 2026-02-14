# Pl0.Vm API – Detaillierte Referenz

## Überblick

Das Modul **Pl0.Vm** (Virtual Machine) implementiert die Laufzeit-Engine, die P-Code-Instruktionen (vom Compiler generiert) ausführt. Es ist eine klassische Stack-basierte virtuelle Maschine, ähnlich wie die hypothetische PL/0-Maschine aus dem Original-Compiler.

---

## Hauptkomponenten

### VirtualMachine – Der zentrale Interpreter

[`VirtualMachine`](xref:Pl0.Vm.VirtualMachine) ist die Hauptklasse und implementiert den P-Code-Interpreter:

- **Constructor:** Akzeptiert die P-Code-Instruktionen und optionale [`VirtualMachineOptions`](xref:Pl0.Vm.VirtualMachineOptions)
- **Methode `Execute()`:** Führt den P-Code aus und gibt ein [`VmExecutionResult`](xref:Pl0.Vm.VmExecutionResult) zurück

**Beispiel-Nutzung:**
```csharp
var instructions = compilationResult.Instructions;
var vm = new VirtualMachine(instructions);

var result = vm.Execute();
if (result.IsSuccess)
{
    Console.WriteLine($"Ausführung erfolgreich!");
    Console.WriteLine($"Return-Value: {result.ReturnValue}");
}
else
{
    Console.WriteLine($"Fehler: {result.Error}");
}
```

---

### VmExecutionResult – Ausführungsergebnis

[`VmExecutionResult`](xref:Pl0.Vm.VmExecutionResult) kapselt das Ergebnis der Programmausführung:

- **IsSuccess:** Boolesch, ob die Ausführung erfolgreich war
- **ReturnValue:** Der Rückgabewert des Programms (meist 0 bei Erfolg)
- **Error:** Fehlermeldung bei Ausführungsfehlern
- **ExecutionStatistics:** Statistiken wie Anzahl der ausgeführten Instruktionen
- **Diagnostics:** Liste von [`VmDiagnostic`](xref:Pl0.Vm.VmDiagnostic) Objekten

**Nutzung:**
```csharp
var result = vm.Execute();

if (!result.IsSuccess)
{
    foreach (var diagnostic in result.Diagnostics)
    {
        Console.WriteLine($"[{diagnostic.Level}] {diagnostic.Message}");
    }
}
```

---

### IPl0Io – I/O-Abstraktion

[`IPl0Io`](xref:Pl0.Vm.IPl0Io) ist eine Schnittstelle, die Ein-/Ausgabe abstrahiert. Dies ermöglicht verschiedene I/O-Implementierungen:

- **Methode `Read()`:** Liest einen Integer von der Eingabe
- **Methode `Write()`:** Schreibt einen Integer zur Ausgabe

Implementierungen:
- [`ConsolePl0Io`](xref:Pl0.Vm.ConsolePl0Io) – Direkte Konsolen-Ein-/Ausgabe
- [`BufferedPl0Io`](xref:Pl0.Vm.BufferedPl0Io) – Gepufferte I/O für Tests und Automatisierung

---

### ConsolePl0Io – Standard-Konsolen-I/O

[`ConsolePl0Io`](xref:Pl0.Vm.ConsolePl0Io) ist die Standard-Implementierung für Konsolen-Ein-/Ausgabe:

```csharp
// Standardmäßig verwendet von VirtualMachine
var vm = new VirtualMachine(instructions, new VirtualMachineOptions 
{ 
    Io = new ConsolePl0Io() 
});
```

---

### BufferedPl0Io – Gepufferte I/O für Tests

[`BufferedPl0Io`](xref:Pl0.Vm.BufferedPl0Io) speichert Ein-/Ausgabe in Buffern. Ideal für Tests:

```csharp
// I/O gepuffert für Test-Automatisierung
var io = new BufferedPl0Io(new[] { 5, 10 }); // Eingabewerte
var vm = new VirtualMachine(instructions, new VirtualMachineOptions { Io = io });

var result = vm.Execute();
Console.WriteLine($"Ausgabe: {string.Join(", ", io.OutputBuffer)}");
```

---

### VirtualMachineOptions – Konfiguration

[`VirtualMachineOptions`](xref:Pl0.Vm.VirtualMachineOptions) ermöglicht die Anpassung der VM:

- **Io:** Die I/O-Implementierung (Standard: `ConsolePl0Io`)
- **StackSize:** Maximale Stack-Größe (Standard: 1024)
- **MemorySize:** Maximale Speichergröße (Standard: 2048)

```csharp
var options = new VirtualMachineOptions
{
    Io = new BufferedPl0Io(new[] { 42 }),
    StackSize = 512,
    MemorySize = 1024
};

var vm = new VirtualMachine(instructions, options);
```

---

## P-Code Instruktionen

Die VM interpretiert P-Code-Instruktionen vom Typ [`Instruction`](xref:Pl0.Core.Instruction) mit verschiedenen Opcodes:

**Wichtige Opcodes:**
- **LIT k** – Push constant k onto stack
- **LOD l, a** – Load variable from address into stack
- **STO l, a** – Store top of stack into variable
- **ADD** – Add top two stack elements
- **SUB** – Subtract
- **MUL** – Multiply
- **DIV** – Divide
- **EQ** – Equal comparison
- **NEQ** – Not equal
- **LT** – Less than
- **LE** – Less or equal
- **GT** – Greater than
- **GE** – Greater or equal
- **JMP adr** – Unconditional jump
- **JPC adr** – Jump if top of stack is 0 (conditional)
- **READ** – Read from input
- **WRITE** – Write to output
- **CALL adr** – Call procedure
- **RET** – Return from procedure
- **HALT** – Stop execution

---

## Fehlerbehandlung

Laufzeitfehler werden in [`VmDiagnostic`](xref:Pl0.Vm.VmDiagnostic) Objekten erfasst:

```csharp
var result = vm.Execute();

if (!result.IsSuccess)
{
    foreach (var diagnostic in result.Diagnostics)
    {
        Console.WriteLine($"Fehler: {diagnostic.Message}");
        Console.WriteLine($"Level: {diagnostic.Level}");
    }
}
```

---

## Stack-Machine-Konzept

Die VM ist eine Stack-basierte Maschine:
- **Stack:** Lokaler Arbeitsspeicher für Operationen
- **Memory:** Globaler Speicher für Variablen
- **Program Counter:** Zeiger auf nächste Instruktion
- **Return Stack:** Für Funktionsaufrufe

---

## Workflow-Beispiel: Fibonacci

```csharp
// Annahme: instructions enthalten kompilierten Fibonacci-Code
var io = new BufferedPl0Io(new[] { 10 }); // Eingabe: 10
var options = new VirtualMachineOptions { Io = io };
var vm = new VirtualMachine(instructions, options);

var result = vm.Execute();
if (result.IsSuccess)
{
    var outputs = io.OutputBuffer;
    Console.WriteLine($"Fibonacci(10) = {outputs[outputs.Length - 1]}");
}
```

---

## Verbindung zu anderen Modulen

- **← Pl0.Core:** Nutzt P-Code-Instruktionen vom Compiler
- **← Pl0.Cli:** Wird vom CLI zur Ausführung von P-Code aufgerufen

---

## Weiterführende Ressourcen

- [Pl0.Vm Namespace API](xref:Pl0.Vm)
- [Architektur-Übersicht: VM](../projects/pl0-vm.md)
- [P-Code & VM Handbuch](../handbook/pcode/index.md)
- [VM Instruction Set](../curated/vm-instruction-set.md)


