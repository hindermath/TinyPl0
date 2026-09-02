# Öffentlicher Host-API-Vertrag / Public Host API Contract

**Vertragsstatus / Contract status**: verbindliches Design für die spätere
Implementierung; keine API ist in dieser Phase umgesetzt. / Binding design for
later implementation; no API is implemented in this phase.

## 1. Einstiegspunkte / Entry points

```csharp
public static VmExecutionResult Run(
    IReadOnlyList<Instruction> program,
    IPl0Io? io = null,
    VirtualMachineOptions? options = null,
    CancellationToken cancellationToken = default);

public void Initialize(
    IReadOnlyList<Instruction> program,
    IPl0Io? io = null,
    VirtualMachineOptions? options = null,
    CancellationToken cancellationToken = default);

public VmStepResult Step();
```

Der neue Token ist jeweils der letzte optionale Parameter. `Step()` bleibt
parameterlos und verwendet den bei `Initialize` gespeicherten Token. Bestehende
Aufrufe kompilieren unverändert. Der Implementierungstask prüft zusätzlich die
binäre Oberfläche und erhält bestehende Konstruktoren/Deconstructs. / The new
token is the final optional parameter. Step remains parameterless and uses the
token stored at initialization. Existing source calls remain valid. Binary
surface tests preserve existing constructors and deconstructors.

`VirtualMachine.Run` und `SteppableVirtualMachine.Step` delegieren beide an
dieselbe interne `VmExecutionSession.ExecuteNext()`. Es darf nur einen Opcode-
und OPR-Dispatch geben. / Run and Step both delegate to the same internal
execution method. Only one opcode and OPR dispatcher may exist.

## 2. Optionen / Options

Der bestehende positional record erhält ausschließlich am Ende:

```csharp
int MaximumProgramLength = 10_000
```

Verbindliche Grenzen / Binding bounds:

| Option | Minimum | Standard / Default | Maximum |
|---|---:|---:|---:|
| `StackSize` | 3 | 500 | 1,000,000 |
| `InstructionBudget` | 1 | 1,000,000 | 10,000,000 |
| `MaximumProgramLength` | 1 | 10,000 | 100,000 |

Die Sprache muss aus der bestehenden Allowlist stammen. Kein ungültiger Wert
darf Stackallokation, Programmausführung oder Host-I/O auslösen. / Language must
come from the existing allowlist. No invalid value may cause stack allocation,
execution, or host I/O.

## 3. Validierungspräzedenz / Validation precedence

Vor Ausführung / Before execution:

1. `StackSize`;
2. `InstructionBudget`;
3. `MaximumProgramLength`;
4. `Language`;
5. `program`: null, Anzahl, Opcode, lexikalische Ebene `0..3`, OPR-Code,
   nichtnegative Argumente sowie gültige Jump-/Call-Ziele. / null, count,
   opcode, lexical level, OPR code, non-negative arguments, and valid targets.

An jeder Instruktionsgrenze / At every instruction boundary:

1. gespeichertes Terminalresultat zurückgeben / return cached terminal result;
2. Cancellation;
3. Budget;
4. Instruction Pointer und Fetch prüfen / validate pointer and fetch;
5. `ExecutedInstructions` erhöhen / increment count;
6. gemeinsame Instruktion dispatchen / dispatch the shared instruction.

Nur der erste Fehler gemäß dieser Reihenfolge wird terminal. Mehrere
Programmdiagnosen werden nach Index und Regel stabil sortiert. / Only the first
error by this order becomes terminal. Multiple program diagnostics are stably
sorted by index and rule.

## 4. Zähl- und Seiteneffektvertrag / Counting and side-effect contract

- Ein Dispatch zählt unmittelbar vor seinem Beginn genau einmal. / A dispatch
  counts exactly once immediately before it starts.
- Cancellation oder Budget vor Dispatch zählt nicht. / Cancellation or budget
  before dispatch does not count.
- Bei Budget `N` beginnen höchstens `N` Dispatches; `N+1` hat keinen
  Seiteneffekt. / At most N dispatches start; N+1 has no side effect.
- Ein Fehler im begonnenen Dispatch zählt, auch wenn er terminal endet. / A
  failure inside a started dispatch counts even when it terminates.
- Cancellation während synchronem `IPl0Io` unterbricht den fremden Aufruf nicht
  rückwirkend; vor dem nächsten Dispatch wird `Cancelled` terminal. / Cancellation
  during synchronous I/O does not roll back that call; it becomes terminal
  before the next dispatch.

## 5. Completion Reasons

`VmCompletionReason` ist ein explizit nummerierter Enum:

| Reason | Bedeutung / Meaning | `Success` | Alter Step-Status / Old step status |
|---|---|---:|---|
| `Running` | weitere Instruktion möglich / more execution possible | false | `Running` |
| `Halted` | normales PL/0-Ende / normal PL/0 completion | true | `Halted` |
| `Cancelled` | Token vor nächstem Dispatch gesetzt / token set before next dispatch | false | `Error` |
| `InstructionBudgetExceeded` | Budget vollständig verbraucht / budget fully consumed | false | `Error` |
| `InvalidConfiguration` | Optionen ungültig / invalid options | false | `Error` |
| `InvalidProgram` | P-Code vorab ungültig / P-Code invalid before execution | false | `Error` |
| `StackFault` | Stackgrenze verletzt / stack boundary violated | false | `Error` |
| `ArithmeticFault` | z. B. Division durch null / e.g. division by zero | false | `Error` |
| `InputEndOfStream` | erwartete Eingabe fehlt / expected input absent | false | `Error` |
| `InputFormatError` | Eingabe nicht als Integer lesbar / input not an integer | false | `Error` |
| `IoFault` | erlaubter Host-I/O-Fehler / allowed host I/O failure | false | `Error` |
| `RuntimeFault` | übriger sicher übersetzbarer VM-Fehler / other safely translated VM fault | false | `Error` |

Neue Werte dürfen nur angehängt werden. OOM, `StackOverflowException` und
`AccessViolationException` werden nicht als gewöhnlicher I/O- oder
Runtimefehler verschleiert. / New values may only be appended. Fatal runtime
failures are not disguised as ordinary I/O or runtime faults.

## 6. Ergebnis- und Snapshotvertrag / Result and snapshot contract

Beide Resulttypen stellen mindestens `CompletionReason`,
`ExecutedInstructions`, `State` und eine read-only Diagnosefolge bereit.
Bestehende Stack-/Top-/Status-Projektionen bleiben erhalten. / Both result types
expose reason, count, state, and read-only diagnostics while retaining existing
projections.

`VmState` enthält `P`, `B`, `T`, die nächste Instruktion oder null, den
kumulierten Zähler und eine defensive Stackkopie. Rückgaben dürfen keine
veränderliche interne Collection oder Sessionreferenz enthalten. / State
contains registers, next instruction or null, cumulative count, and a defensive
stack copy. Results expose no mutable internal collection or session reference.

Nach jedem terminalen Reason gilt für beliebig viele weitere `Step()`-Aufrufe:

- gleicher Reason, Status, Zähler, Register, Stack und Diagnoseinhalt;
- keine neue Diagnose;
- keine Programmmutation;
- kein `IPl0Io`-Aufruf und kein weiterer Dispatch.

After any terminal reason, every later Step returns the same observable
projection and performs no mutation, I/O, diagnostic append, or dispatch.

## 7. I/O- und Sicherheitsgrenze / I/O and security boundary

Die VM kommuniziert ausschließlich über das übergebene `IPl0Io`. Sie öffnet
keine Dateien oder Sockets, startet keine Prozesse, liest keine
Umgebungsvariablen und beschafft keine Credentials. Null-I/O verwendet die
bestehende sichere Standardimplementierung. / The VM communicates only through
the supplied I/O abstraction. It opens no files or sockets, starts no process,
reads no environment variables, and obtains no credentials. Null I/O uses the
existing safe default.

Erwartete EOF-, Format- und nicht-fatale I/O-Fehler werden in stable Reason und
lokalisierte Diagnose übersetzt. Die Nachricht enthält keinen fremden
Exceptiontext, Stacktrace, Hostpfad oder internen Zustand. / Expected EOF,
format, and non-fatal I/O failures become a stable reason and localized safe
diagnostic without foreign exception text or host internals.

## 8. XML-, Lern- und Kompatibilitätsnachweis / XML, learning, and compatibility proof

Jede geänderte öffentliche API erhält vollständige XML-Elemente: `summary`,
alle `param`, `returns`, tatsächlich zugesicherte `exception` sowie `remarks`
und `example`, wenn sie Grenzen erklären. Deutsch steht zuerst, Englisch
danach, CEFR B2. DocFX, API-Compile-Beispiele und vorhandene Caller belegen den
Vertrag. / Every changed public API receives complete bilingual XML elements,
DocFX output, compiling examples, and existing-caller compatibility proof.

Die VM bleibt in-process und ressourcenbegrenzt, aber keine Zeit-, Speicher-
oder Betriebssystem-Sandbox für feindlichen Code. Hosts dokumentieren stärkere
Isolation als eigene Trust Boundary. / The VM remains bounded in-process code,
not an OS sandbox for hostile programs. Hosts document stronger isolation as
their own trust boundary.
