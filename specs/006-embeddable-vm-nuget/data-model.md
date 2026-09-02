# Datenmodell: Host- und Releaseverträge / Data Model: Host and Release Contracts

## 1. Laufzeitmodell / Runtime model

### `VirtualMachineOptions`

| Feld / Field | Typ / Type | Standard / Default | Gültig / Valid | Regel / Rule |
|---|---|---:|---:|---|
| `StackSize` | `int` | `500` | `3..1_000_000` | vor Allokation prüfen / validate before allocation |
| `InstructionBudget` | `int` | `1_000_000` | `1..10_000_000` | zählt begonnene Dispatches / counts started dispatches |
| `MaximumProgramLength` | `int` | `10_000` | `1..100_000` | letzter neuer optionaler Parameter / final new optional parameter |
| `Language` | `string` | `de` | vorhandene Allowlist / existing allowlist | vor Ressourcenprüfung lokalisierbar / localizable before resource use |
| bestehende Felder / existing fields | unverändert / unchanged | unverändert / unchanged | bestehender Vertrag / existing contract | Quellkompatibilität / source compatibility |

### `VmExecutionSession` (intern / internal)

Besitzt genau eine defensive Programmkopie, validierte Optionen, `IPl0Io`,
`CancellationToken`, Stack, Register `P/B/T`, Zähler, Diagnosen und optionales
Terminalresultat. Nur `ExecuteNext()` darf eine Instruktion dispatchen. / Owns
exactly one defensive program copy, validated options, I/O boundary, token,
stack, registers, counter, diagnostics, and optional terminal result. Only
`ExecuteNext()` may dispatch an instruction.

Invarianten / Invariants:

- Optionen und Programm werden einmal vor der Stackallokation vollständig
  geprüft. / Options and program are fully validated once before stack
  allocation.
- `0 <= ExecutedInstructions <= InstructionBudget`.
- Der Zähler steigt direkt vor dem Dispatch um eins. / The count increases
  immediately before dispatch.
- Ein gesetztes `TerminalResult` wird nie ersetzt; weiterer Aufruf erzeugt
  keine Mutation und kein I/O. / A stored terminal result is never replaced;
  later calls cause no mutation or I/O.
- VM-Code öffnet keine Datei, kein Netzwerk, keinen Prozess und liest keine
  Umgebung. / VM code opens no file, network, or process and reads no
  environment state.

### Zustandsautomat / State machine

```text
Uninitialized
    |
    | Initialize/Run + valid options/program
    v
Running -- one shared ExecuteNext --> Running
   |                                  |
   +----------------------------------+
   |
   +--> Halted
   +--> Cancelled
   +--> InstructionBudgetExceeded
   +--> InvalidConfiguration
   +--> InvalidProgram
   +--> StackFault
   +--> ArithmeticFault
   +--> InputEndOfStream
   +--> InputFormatError
   +--> IoFault
   +--> RuntimeFault

Every terminal state -- Step --> same terminal projection
```

`InvalidConfiguration` und `InvalidProgram` können beim Aufbau direkt terminal
werden; sie führen keinen Dispatch aus. / Invalid configuration and program may
become terminal during creation; they execute no dispatch.

### `VmCompletionReason`

Explizit nummerierter öffentlicher Enum: `Running`, `Halted`, `Cancelled`,
`InstructionBudgetExceeded`, `InvalidConfiguration`, `InvalidProgram`,
`StackFault`, `ArithmeticFault`, `InputEndOfStream`, `InputFormatError`,
`IoFault`, `RuntimeFault`. Neue Werte dürfen später nur angehängt werden. /
Explicitly numbered public enum. Future values may only be appended.

### Ergebnisprojektionen / Result projections

`VmExecutionResult` und `VmStepResult` teilen:

- `CompletionReason`;
- `ExecutedInstructions`;
- defensive `VmState`-Projektion;
- unveränderliche Diagnosefolge.

`VmExecutionResult.Success` ist nur bei `Halted` wahr. `VmStepResult.Status`
projiziert `Running -> Running`, `Halted -> Halted`, alle anderen Gründe nach
`Error`, damit der bestehende Statusvertrag bleibt. Bestehende Konstruktoren
und Deconstructs bleiben erhalten. / Success is true only for Halted. The old
step status maps running and halted directly and all other reasons to Error.
Existing constructors and deconstructors remain.

`VmState` enthält `ProgramCounter`, `BasePointer`, `StackTop`,
`CurrentInstruction`, `ExecutedInstructions` und eine Stackkopie. Keine
öffentliche Collection referenziert den internen Stack oder die interne
Diagnoseliste. / No public collection references internal stack or diagnostics.

### `VmDiagnostic`

Stabile Codes, lokalisierte DE-/EN-Nachrichten und optionaler sicherer
Instruktionsindex. Keine fremden Exceptiontexte, Stacktraces, Pfade,
Umgebungswerte oder Credentials. / Stable codes, localized messages, and an
optional safe instruction index. No foreign exception message, stack trace,
path, environment value, or credential.

## 2. Validierungsmodell / Validation model

Globale Präzedenz / Global precedence:

1. `StackSize`;
2. `InstructionBudget`;
3. `MaximumProgramLength`;
4. `Language`;
5. Programmliste: null, Länge, Opcode, Level, OPR-Allowlist, Argumente und
   Ziele; / program: null, length, opcode, level, OPR allowlist, arguments,
   and targets;
6. an jeder Instruktionsgrenze: Terminalcache, Cancellation, Budget,
   Instruction Pointer/Fetch, Zählen, Dispatch. / at every boundary: cached
   terminal state, cancellation, budget, instruction pointer/fetch, count,
   dispatch.

Mehrere ungültige Werte liefern nur den ersten stabilen Fehler nach dieser
Reihenfolge. Programmdiagnosen sind nach Instruktionsindex und danach
Prüfregel stabil sortiert. / Multiple invalid values return the first stable
error by this order. Program diagnostics are stably ordered by instruction
index and then validation rule.

## 3. Paketmodell / Package model

### `PackagePair`

| Attribut / Attribute | Regel / Rule |
|---|---|
| Version | eine stabile Release-Please-SemVer / one stable Release Please SemVer |
| Core | `TinyPl0.Core.<version>.nupkg` und `.snupkg` |
| VM | `TinyPl0.Vm.<version>.nupkg` und `.snupkg` |
| VM-Abhängigkeit / dependency | genau / exactly `TinyPl0.Core [<version>]` |
| Bindung / Binding | derselbe Commit, Tag, Workflowlauf und Hashmanifest / same commit, tag, workflow run, and hash manifest |
| Inhalt / Contents | DLL, XML, README, Repository-/Lizenzmetadaten, SourceLink/PDB / assembly, XML, README, repository/license metadata, SourceLink/PDB |

Der Paarzustand ist `LocalBuilt`, `LocallyVerified`, `Attested`,
`PublicMatching` oder `Rejected`. Nur `PublicMatching` erfüllt die öffentliche
Lieferung. / Only PublicMatching satisfies public delivery.

## 4. Veröffentlichungsmodell / Publication model

### `PublicationState`

| Wert / Value | Beobachtung / Observation | Zulässige Aktion / Allowed action |
|---|---|---|
| `None` | beide IDs fehlen / both IDs absent | beide aus demselben Set pushen / push both from one set |
| `BothMatching` | beide IDs und Hashes passen / both IDs and hashes match | idempotenter Erfolg, kein Push / idempotent success, no push |
| `Partial` | genau eine ID vorhanden / one ID exists | blockieren; neue SemVer planen / block; plan new SemVer |
| `Conflict` | Version vorhanden, Bindung/Hash weicht ab / version exists, binding/hash differs | blockieren und untersuchen / block and investigate |
| `Unknown` | Provider nicht beweisbar / provider cannot be proven | fail closed |

Ein Push-Exitcode oder HTTP 409 ändert allein keinen Zustand zu Erfolg. Nach
jedem Pushversuch muss der öffentliche Abgleich `BothMatching` liefern. / A
push exit code or HTTP 409 alone never establishes success. Public reconciliation
must return BothMatching after every push attempt.

### `ReleaseEvidenceManifest`

Maschinenlesbares UTF-8-JSON mit stabilen IDs und relativen Pfaden:

- Version, Tag, Commit-SHA und Workflow-Run-ID;
- Paket-/Symbolpfad, Größe und lowercase SHA-256;
- Nuspec-Abhängigkeit und Toolversionen;
- SBOM- und VEX-Pfade/Hashes;
- Attestation-/Provenance-Referenz und belegtes SLSA-Niveau;
- OIDC-Policy-Fingerprint ohne Token oder Key;
- Vor-/Nachzustand, beide Pushausgänge und öffentliche URLs;
- Restore-/Compile-/Run-/Step-Ergebnis je Betriebssystem.

Das Manifest ist vollständig, wenn alle referenzierten Dateien existieren,
Hashes passen, genau ein Commit/Tag/Version verwendet wird und alle drei
Consumerläufe erfolgreich sind. / The manifest is complete only when all
referenced files exist, hashes match, one commit/tag/version is used, and all
three consumer runs succeed.

## 5. Lebensdauer und Schreibrechte / Lifetime and writers

- Sessiondaten leben nur im Hostprozess. / Session data lives only in the host
  process.
- Paketartefakte werden einmal im `build-release`-Job erzeugt und danach nur
  gelesen. / Package artifacts are created once and read thereafter.
- Nur Release Please schreibt die Paket-SemVer; nur der serialisierte
  IDE-Version-Writer schreibt die vier IDE-Felder. / Only Release Please writes
  package SemVer; only the serialized IDE writer writes the four IDE fields.
- Evidence enthält keine Secrets oder OIDC-Token und wird nach dem im Release-
  Vertrag genannten Aufbewahrungsweg veröffentlicht. / Evidence contains no
  secrets or OIDC tokens and follows the release contract retention path.
