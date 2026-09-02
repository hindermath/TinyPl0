# Feature Specification: Einbettbare VM und NuGet-Pakete / Embeddable VM and NuGet Packages

**Feature Branch**: `codex/006-embeddable-vm-nuget`
**Created**: 2026-09-02
**Status**: Draft, ready for clarification review
**Input**: Binding intake `requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md`

## Bindung, Reihenfolge und Autorität / Binding, Order, and Authority

Diese Spezifikation setzt genau den akzeptierten Rang-4-Intake für eine
einbettbare PL/0-VM und zwei öffentliche NuGet-Pakete um. Der abgeschlossene
Sandbox-Intake ist der harte interne Vorgänger. Danach folgt als unmittelbarer
Seriennachfolger `Lastenheft_Quellcode_Doku.md`; die spätere IDE-Erweiterung
bleibt zusätzlich von diesem Feature abhängig. TinyCalc bleibt ein externer,
blockierter Verbraucher, bis der öffentliche Paket- und Handoff-Nachweis
vollständig ist. Kein anderes Feature darf in diesem Lauf begonnen werden.

*This specification implements exactly the accepted rank-4 intake for an
embeddable PL/0 VM and two public NuGet packages. The completed sandbox intake
is the hard internal predecessor. The source-documentation intake follows in
the series, while the later IDE extension also remains dependent on this
feature. TinyCalc stays an externally blocked consumer until the public package
and handoff evidence is complete. This run must not start another feature.*

Die akzeptierten Eingaben sind der Intake mit SHA-256
`a6e752dcc372c26626cf40cc0b1fb1da1a195a895f51129b87dc0920310b64d5`,
der Review-Request mit SHA-256
`b23706568d8c66a62ca6df0dfd506378166a5d8108bf3012d30ec2802a3b7e04`,
das Review-Ergebnis mit SHA-256
`09d26eb8f267b92ce21ad9acaa0d316d29e7b51d893c8e3eed7910e7199cfea2`
und das Serienmanifest mit SHA-256
`c73a65227e91123ccf017b03720695ad1c21b5910eb966a79a824069c8ff0a17`.
Der Feature-lokale Laufzustand liegt in
`specs/006-embeddable-vm-nuget/autonomous-run-state.json`.

Die aktuelle Nutzerautorität gilt als Lieferkontext für den Gesamtlauf:
`MergeAndSync`, ausdrücklich erlaubte Veröffentlichung beider Pakete auf
NuGet.org und ein Admin-Bypass nur für eine nach vollständiger Technik-,
Risiko-, Evidence-, Exact-Head- und Review-Prüfung verbleibende
Repository-Policy-Schranke. Der Bypass ersetzt weder Review noch Approval und
gilt nicht für NuGet.org- oder Credential-Policies. Diese Specify-Phase darf
nur `spec.md`, ihre Requirements-Qualitätscheckliste und das strukturierte
Phasenergebnis ändern. Sie darf nicht implementieren, committen, pushen, einen
Pull Request erstellen oder mergen, Pakete veröffentlichen, den Intake ändern
oder ein Folgefeature starten.

*Current user authority is delivery context for the whole run: `MergeAndSync`,
explicit publication of both packages to NuGet.org, and admin bypass only for a
remaining repository-policy barrier after all technical, risk, evidence,
exact-head, and review gates pass. Bypass replaces neither review nor approval
and does not cover NuGet.org or credential policy. This Specify phase may
change only the specification, its requirements-quality checklist, and its
structured phase result. It performs no implementation, commit, push, pull
request, merge, package publication, intake edit, or follow-up feature.*

Geheimnisse werden weder beschafft noch offengelegt. Der spätere autorisierte
Veröffentlichungsschritt verwendet bevorzugt den bereits autorisierten,
eng gebundenen OIDC-Providerpfad. Ist dieser Pfad nicht verfügbar, muss die
Veröffentlichung blockieren. Der im Produktvertrag erhaltene API-Key-Fallback
darf erst nach einer neuen, ausdrücklich passenden Secret-Autorität verwendet
werden; der aktuelle Auftrag erteilt keine Erlaubnis, einen Schlüssel zu
beschaffen oder seinen Wert zu lesen, auszugeben oder zu speichern.

*Secrets are neither obtained nor disclosed. The later authorized publication
step prefers the already authorized, narrowly bound OIDC provider route. If
that route is unavailable, publication blocks. The API-key fallback retained
in the product contract requires fresh, explicit secret authority; this request
does not permit obtaining, reading, printing, or storing a key value.*

## Nutzungsszenarien und Tests / User Scenarios & Testing *(mandatory)*

### Nutzungsszenario 1 / User Story 1 - Begrenzte normale Ausführung / Bounded Normal Execution (Priority: P1)

Als .NET-Hostanwendende möchten Lehrende, Lernende und Anwendungen ein
kompiliertes PL/0-Programm vollständig ausführen, dabei ein endliches
Instruktionsbudget und kooperativen Abbruch vorgeben und anschließend einen
strukturierten Abschluss erhalten. So kann ein fehlerhaftes oder absichtlich
endloses Lernprogramm den Host nicht unbegrenzt beschäftigen.

*As .NET host users, teachers, learners, and applications want to run a
compiled PL/0 program with a finite instruction budget and cooperative
cancellation, then receive a structured completion result. A faulty or
deliberately endless learning program therefore cannot occupy the host without
a bound.*

**Why this priority**: Sichere, kontrollierbare Ausführung ist die notwendige
Grundlage jeder Einbettung und schützt die Verfügbarkeit des Hosts. / Safe,
controllable execution is the necessary basis for every embedding and protects
host availability.

**Independent Test**: Ein Host kompiliert ein erfolgreiches Programm, eine
Endlosschleife und ein fehlerhaftes Programm, führt sie über den normalen
Laufweg aus und prüft Abschlussgrund, Instruktionszahl, Diagnosen und sicheren
Snapshot ohne CLI oder IDE. / A host compiles successful, endless, and faulty
programs, executes them through the normal run path, and verifies completion
reason, instruction count, diagnostics, and safe snapshot without the CLI or
IDE.

**Acceptance Scenarios**:

1. **Given** ein gültiges Programm und ein ausreichendes positives Budget,
   **When** der Host den vollständigen Lauf startet, **Then** endet er
   erfolgreich mit genauer Instruktionszahl, leeren Fehlerdiagnosen und einem
   sicheren Abschlusszustand. / Given a valid program and sufficient positive
   budget, when the host starts a full run, then it completes successfully with
   an exact instruction count, no error diagnostics, and a safe final state.
2. **Given** eine Endlosschleife und Budget `N`, **When** der Host den Lauf
   startet, **Then** endet er vor Instruktion `N+1` reproduzierbar mit dem
   Abschlussgrund Budget erreicht und ohne deren Seiteneffekt. / Given an
   endless loop and budget `N`, when the host starts the run, then it ends
   reproducibly before instruction `N+1`, reports budget exhaustion, and
   produces no side effect from that instruction.
3. **Given** ein bereits angefordertes Abbruchsignal, **When** der Lauf geprüft
   wird, **Then** wird keine Programminstruktion ausgeführt und der stabile
   Abschlussgrund ist Abbruch. / Given cancellation already requested, when
   the run is evaluated, then no program instruction executes and the stable
   completion reason is cancellation.

---

### Nutzungsszenario 2 / User Story 2 - Paritätische Einzelschritte / Parity-Preserving Steps (Priority: P1)

Als Debugger- oder Lernhost möchte ich dasselbe Programm in einzelnen
Instruktionen ausführen. Wiederholte Steps sollen fachlich dasselbe Ergebnis
wie der vollständige Lauf liefern und nach einem terminalen Ereignis
unveränderlich bleiben.

*As a debugger or learning host, I want to execute the same program one
instruction at a time. Repeated steps shall produce the same functional result
as a full run and remain immutable after a terminal event.*

**Why this priority**: Run/Step-Parität verhindert, dass Unterricht,
Fehlersuche und normale Programmausführung unterschiedliche PL/0-Semantik
zeigen. / Run/step parity prevents teaching, debugging, and normal execution
from presenting different PL/0 semantics.

**Independent Test**: Für dieselben Programme werden ein vollständiger Lauf
und Steps bis zum terminalen Zustand ausgeführt. Abschlussgrund,
Instruktionszahl, Diagnosen, Ausgabe und Snapshot werden vollständig
verglichen. / The same programs are executed once as a full run and once as
steps until terminal. Completion reason, instruction count, diagnostics,
output, and snapshot are compared in full.

**Acceptance Scenarios**:

1. **Given** eine laufende, initialisierte Step-VM, **When** `Step` einmal
   aufgerufen wird, **Then** wird genau eine Instruktion ausgeführt oder vor
   ihrer Ausführung ein terminaler Grenzgrund gemeldet. / Given a running,
   initialized step VM, when `Step` is called once, then exactly one instruction
   executes or a terminal boundary reason is reported before execution.
2. **Given** dasselbe Programm, dieselben Optionen, dieselbe Eingabe und
   dasselbe Abbruchsignal, **When** es vollständig und schrittweise ausgeführt
   wird, **Then** stimmen alle fachlichen Abschlussdaten überein. / Given the
   same program, options, input, and cancellation signal, when it runs in full
   and step modes, then all functional completion data match.
3. **Given** Halt, Abbruch, Budgetende oder Laufzeitfehler, **When** weitere
   Steps angefordert werden, **Then** bleiben Zustand, Ausgabe,
   Instruktionszahl und Diagnosen unverändert. / Given halt, cancellation,
   budget exhaustion, or runtime error, when further steps are requested, then
   state, output, instruction count, and diagnostics remain unchanged.

---

### Nutzungsszenario 3 / User Story 3 - Öffentlicher Paketverbrauch / Public Package Consumption (Priority: P2)

Als TinyCalc- oder anderer .NET-Host möchte ich `TinyPl0.Core` und
`TinyPl0.Vm` in derselben stabilen Version aus dem öffentlichen Feed beziehen,
PL/0 kompilieren und beide VM-Modi verwenden, ohne TinyPl0-Quellcode oder eine
lokale Projektverknüpfung zu kopieren.

*As TinyCalc or another .NET host, I want matching stable versions of
`TinyPl0.Core` and `TinyPl0.Vm` from the public feed, compile PL/0, and use both
VM modes without copying TinyPl0 source or adding a local project reference.*

**Why this priority**: Der öffentliche, versionierte Paketvertrag entkoppelt
Verbraucher vom Repository und ist das eigentliche Handoff an TinyCalc. / The
public, versioned package contract decouples consumers from the repository and
is the actual handoff to TinyCalc.

**Independent Test**: Ein neues .NET-10-Consumer-Projekt stellt beide Pakete
aus einem sauberen öffentlichen Feed wieder her, kompiliert PL/0 und prüft Run
und Step auf macOS, Linux und Windows. / A new .NET 10 consumer project restores
both packages from a clean public feed, compiles PL/0, and verifies run and step
on macOS, Linux, and Windows.

**Acceptance Scenarios**:

1. **Given** ein leerer Paketcache und keine lokale TinyPl0-Projektreferenz,
   **When** der Consumer dieselbe stabile Version beider Pakete wiederherstellt,
   **Then** kann er kompilieren, normal ausführen und schrittweise debuggen. /
   Given an empty package cache and no local TinyPl0 project reference, when
   the consumer restores the same stable version of both packages, then it can
   compile, run normally, and debug step by step.
2. **Given** das VM-Paket einer Version, **When** seine Abhängigkeiten geprüft
   werden, **Then** verweist es nur auf die passende Core-Version und führt
   keine Terminal.Gui-, IDE- oder TinyCalc-Abhängigkeit ein. / Given a VM
   package version, when its dependencies are inspected, then it references
   only the matching Core version and introduces no Terminal.Gui, IDE, or
   TinyCalc dependency.

---

### Nutzungsszenario 4 / User Story 4 - Nachvollziehbare Veröffentlichung / Traceable Publication (Priority: P2)

Als Release-Verantwortliche möchte ich beide unveränderlichen Pakete als ein
zusammengehöriges Release direkt auf NuGet.org veröffentlichen. Paketversion,
Quellcommit, Tag, Hashes, Credential-Modus, SBOM, VEX, Provenance und öffentlicher
Consumer-Nachweis sollen eindeutig zusammengehören.

*As a release owner, I want to publish both immutable packages as one coherent
release directly to NuGet.org. Package version, source commit, tag, hashes,
credential mode, SBOM, VEX, provenance, and public consumer evidence shall be
unambiguously linked.*

**Why this priority**: Ein lokales Paket oder ein einzelner erfolgreicher Push
beweist keine vollständige, vertrauenswürdige Lieferung. / A local package or
one successful push does not prove complete, trustworthy delivery.

**Independent Test**: Der Release-Kandidat wird aus einem festen Commit
erstellt, beide Pakete und Sicherheitsnachweise werden geprüft, anschließend
werden beide öffentlichen IDs derselben Version aus einem sauberen Consumer
wiederhergestellt. / The release candidate is produced from one fixed commit,
both packages and security records are verified, and both public IDs at the
same version are then restored by a clean consumer.

**Acceptance Scenarios**:

1. **Given** gültige Exact-Head-, Review-, Paket-, Provider- und OIDC-Evidence,
   **When** der autorisierte Release ausgeführt wird, **Then** sind beide
   Paket-IDs derselben Version öffentlich abrufbar und demselben Release
   zugeordnet. / Given valid exact-head, review, package, provider, and OIDC
   evidence, when the authorized release runs, then both package IDs at the
   same version are public and bound to the same release.
2. **Given** nur eines von zwei Paketen ist nachweislich veröffentlicht,
   **When** der Release bewertet wird, **Then** ist der Zustand sichtbar
   fehlgeschlagen und kein Lieferabschluss. / Given only one of two packages is
   proven published, when the release is evaluated, then the state is visibly
   failed and not a delivery completion.
3. **Given** NuGet.org meldet für ID und Version einen 409-Konflikt, **When**
   der Abgleich keine bereits beabsichtigte, hashgebundene Veröffentlichung
   beweist, **Then** scheitert der Release trotz `--skip-duplicate`. / Given
   NuGet.org reports a 409 conflict for an ID and version, when reconciliation
   cannot prove the intended hash-bound prior publication, then the release
   fails despite `--skip-duplicate`.

---

### Nutzungsszenario 5 / User Story 5 - Lern- und Prüfpfad / Learning and Evidence Path (Priority: P3)

Als Auszubildende, Lehrende oder Prüfende möchte ich den Hostvertrag, die
VM-Grenzen, Paketnutzung, Sicherheitsentscheidungen und Release-Evidence in
deutschsprachigen und direkt folgenden englischen Texten verstehen. Alle
wesentlichen Aussagen sollen mit Screenreader, Braillezeile und Textbrowser
ohne Farbcodes oder nur visuelle Diagramme nutzbar sein.

*As an apprentice, teacher, or reviewer, I want to understand the host
contract, VM limits, package use, security decisions, and release evidence in
German-first and directly following English text. All essential information
shall work with screen readers, Braille displays, and text browsers without
colour codes or visual-only diagrams.*

**Why this priority**: TinyPl0 ist ein Lernprojekt; Verständlichkeit,
vollständige öffentliche API-Dokumentation und barrierefreie Evidence sind
Abnahmekriterien, keine nachträgliche Ergänzung. / TinyPl0 is a learning
project; clarity, complete public API documentation, and accessible evidence
are acceptance conditions, not later additions.

**Independent Test**: Repräsentative API- und Lernseiten werden mit DocFX,
Tastaturpfad, Playwright/axe, `lynx` und manueller Textprüfung bewertet; jeder
öffentliche geänderte Vertrag besitzt vollständige zweisprachige
XML-Dokumentation. / Representative API and learning pages are assessed with
DocFX, keyboard paths, Playwright/axe, `lynx`, and manual text review; every
changed public contract has complete bilingual XML documentation.

**Acceptance Scenarios**:

1. **Given** eine geänderte öffentliche API, **When** die API-Dokumentation
   erzeugt wird, **Then** sind Zweck, Parameter, Rückgabe, zugesicherte
   Ausnahmen und Beispiele verständlich deutsch zuerst und englisch danach
   dokumentiert. / Given a changed public API, when API documentation is
   generated, then purpose, parameters, return value, guaranteed exceptions,
   and examples are understandable in German first and English second.
2. **Given** ein repräsentativer generierter Dokumentationspfad, **When** er
   textorientiert und gegen WCAG 2.2 AA geprüft wird, **Then** bleiben
   Navigation, Status, Entscheidungen und nächste Schritte ohne visuelle
   Zusatzinformation verständlich. / Given a representative generated
   documentation path, when it is reviewed text-first and against WCAG 2.2 AA,
   then navigation, status, decisions, and next steps remain understandable
   without visual-only information.

### Grenz- und Fehlerfälle / Edge Cases

- Ein Budget von `0`, ein negatives Budget oder eine unzulässige Stackgröße
  wird vor Allokation und Ausführung als strukturierte Diagnose abgewiesen. /
  Zero or negative budget and an invalid stack size are rejected with a
  structured diagnostic before allocation or execution.
- Ein leeres, zu langes oder manipuliertes Programm sowie ungültige Opcode-,
  Sprung-, Level- oder Stackargumente werden an der Vertrauensgrenze sicher
  abgewiesen; interne Stacktraces werden nicht ausgegeben. / Empty, oversized,
  or manipulated programs and invalid opcode, jump, level, or stack arguments
  are safely rejected at the trust boundary without internal stack traces.
- Treffen mehrere terminale Bedingungen an derselben Instruktionsgrenze
  zusammen, gilt in beiden Modi dieselbe dokumentierte Reihenfolge:
  Konfigurations-/Programmvalidierung, bereits terminaler Zustand,
  Cancellation, Budget, dann Instruktionsausführung. / When terminal
  conditions coincide at one instruction boundary, both modes use the same
  documented precedence: configuration/program validation, existing terminal
  state, cancellation, budget, then instruction execution.
- Eine während I/O angeforderte Cancellation kann den bereits begonnenen
  Host-I/O-Aufruf nicht rückgängig machen. Vor der nächsten Instruktion muss
  sie jedoch ohne weiteren P-Code-Seiteneffekt terminal werden. / Cancellation
  requested during I/O cannot undo an already-started host I/O call, but it
  must become terminal before the next instruction without another P-Code side
  effect.
- I/O-Ende, ungültige Eingabe, Division durch null, Stackunter-/überlauf und
  Instruktionszeiger außerhalb des Programms liefern in Run und Step denselben
  Abschlussgrund, dieselben Diagnosen und denselben sicheren Zustand. / End of
  input, invalid input, division by zero, stack underflow/overflow, and an
  out-of-range instruction pointer produce matching completion reasons,
  diagnostics, and safe states in run and step modes.
- Wiederholte Steps nach Halt, Cancellation, Budgetende oder Fehler sind
  idempotent und verändern weder I/O noch VM-Zustand. / Repeated steps after
  halt, cancellation, budget exhaustion, or error are idempotent and change
  neither I/O nor VM state.
- Ist eine vorgesehene Paket-ID unmittelbar vor Veröffentlichung nicht mehr
  verfügbar, stoppt die Veröffentlichung ohne Umbenennung oder neues Feature. /
  If an intended package ID is no longer available immediately before
  publication, publishing stops without renaming or starting a new feature.
- Verschiedene Versionen von Core und VM, fehlende Symbol-/XML-Dateien,
  unerwartete Paketdateien oder eine lokale ProjectReference lassen das
  Consumer-Gate scheitern. / Different Core and VM versions, missing
  symbol/XML files, unexpected package content, or a local project reference
  fail the consumer gate.
- Ein vorhandenes Paket derselben ID/Version ist unveränderlich. Korrekturen
  verwenden eine neue SemVer; Auslisten ersetzt kein vollständiges neues
  Release. / An existing package at the same ID/version is immutable. Fixes
  use a new SemVer; unlisting does not replace a complete new release.
- Fehlt die autorisierte OIDC-Route oder kann ihre enge Policy nicht
  nachgewiesen werden, blockiert der aktuelle Lauf, ohne einen Schlüssel zu
  beschaffen oder auszugeben. / If the authorized OIDC route is unavailable or
  its narrow policy cannot be proven, the current run blocks without obtaining
  or printing a key.

## Anforderungen / Requirements *(mandatory)*

### Umfang / Scope

- Öffentlicher Hostvertrag für begrenzte normale und schrittweise
  PL/0-P-Code-Ausführung. / Public host contract for bounded normal and stepped
  PL/0 P-Code execution.
- Gemeinsame Zustands-, Abschluss-, Diagnose-, Instruktionszähler-, Ressourcen-
  und Cancellation-Semantik. / Shared state, completion, diagnostic,
  instruction-count, resource, and cancellation semantics.
- Rückwärtskompatible Weiterentwicklung der vorhandenen Compiler- und VM-APIs.
  / Backward-compatible evolution of existing compiler and VM APIs.
- Pack-Konfiguration, Paketmetadaten, Symbole, Quellzuordnung, README und
  XML-Dokumentation für `TinyPl0.Core` und `TinyPl0.Vm`. / Pack configuration,
  package metadata, symbols, source mapping, README, and XML documentation for
  `TinyPl0.Core` and `TinyPl0.Vm`.
- Direkte, autorisierte Veröffentlichung auf NuGet.org mit unabhängigen
  Consumer- und Lieferkettennachweisen. / Direct authorized publication to
  NuGet.org with independent consumer and supply-chain evidence.
- Unit-, Paritäts-, Grenzwert-, Abbruch-, Paket-, Consumer- und
  Cross-Platform-Tests sowie Security-, DocFX-, A11Y-, Traceability- und
  Statistiknachweise. / Unit, parity, boundary, cancellation, package,
  consumer, and cross-platform tests plus security, DocFX, accessibility,
  traceability, and statistics evidence.

### Nicht-Ziele / Non-Goals

- Keine neue PL/0-Syntax, kein TinyCalc-spezifischer Dialekt und keine
  Tabellenzellen-Semantik. / No new PL/0 syntax, TinyCalc-specific dialect, or
  spreadsheet-cell semantics.
- Keine Gleitkomma-, Dezimal- oder Festkommaerweiterung; `integer` bleibt der
  einzige Datentyp. / No floating-point, decimal, or fixed-point extension;
  `integer` remains the only data type.
- Kein JIT-, CLR-, nativer oder anderer P-Code-fremder Backendpfad. / No JIT,
  CLR, native, or other non-P-Code backend.
- Keine Compileroptimierung und keine Änderung historischer PL/0-Semantik. /
  No compiler optimization or change to historical PL/0 semantics.
- Keine Paketabhängigkeit auf Terminal.Gui, TinyCalc oder die TinyPl0-IDE. / No
  package dependency on Terminal.Gui, TinyCalc, or the TinyPl0 IDE.
- Keine unautorisierte Remote-Veröffentlichung, kein Secret-Erwerb, keine
  Wiederverwendung einer veröffentlichten Version und kein stiller
  Teilrelease. / No unauthorized remote publication, secret acquisition,
  reuse of a published version, or silent partial release.
- Keine neue Sandbox- oder Prozessisolationsgarantie. Budget, Stacklimit,
  Cancellation und isoliertes I/O begrenzen Risiken, ersetzen aber keine
  Betriebssystem-Sandbox. / No new sandbox or process-isolation guarantee.

### Öffentlicher Hostvertrag / Public Host Contract

Der Vertrag umfasst die bestehenden öffentlichen Compiler-, P-Code- und
VM-Flächen, darunter `Pl0Compiler`, `CompilationResult`, `Instruction`,
`VirtualMachine`, `SteppableVirtualMachine`, `VirtualMachineOptions`,
`IPl0Io`, Lauf-/Step-Ergebnisse, Diagnosen und Zustands-Snapshots. Neue oder
geänderte Vertragstypen müssen quellkompatibel ergänzt, stabil benannt,
vollständig XML-dokumentiert und in einem unabhängigen Consumer nutzbar sein.

*The contract covers the existing public compiler, P-Code, and VM surfaces,
including `Pl0Compiler`, `CompilationResult`, `Instruction`, `VirtualMachine`,
`SteppableVirtualMachine`, `VirtualMachineOptions`, `IPl0Io`, run/step results,
diagnostics, and state snapshots. New or changed contract types must be added
source-compatibly, have stable names and complete XML documentation, and work
from an independent consumer.*

Der normale Einstieg führt ein vollständiges Programm aus. Der Step-Einstieg
initialisiert denselben P-Code mit denselben Hostoptionen und führt je Aufruf
genau eine Instruktion aus. Beide Wege akzeptieren denselben endlichen
Ressourcenvertrag und kooperativen `CancellationToken`. Ein strukturierter
Abschluss enthält mindestens Erfolg, eindeutigen Abschlussgrund, Zahl der
tatsächlich begonnenen Instruktionsausführungen, vollständige sichere
Diagnosen sowie einen unveränderlichen sicheren Zustands- oder Stack-Snapshot.
Eine Instruktion, deren Dispatch nach allen Vorprüfungen begonnen hat und die
einen Laufzeitfehler meldet, zählt einmal; eine wegen Cancellation oder Budget
nicht begonnene Instruktion zählt nicht.

*The normal entry point executes a complete program. The step entry point
initializes the same P-Code with the same host options and executes exactly one
instruction per call. Both accept the same finite resource contract and
cooperative `CancellationToken`. Structured completion contains at least
success, one unambiguous completion reason, the number of instruction
executions actually started, complete safe diagnostics, and an immutable safe
state or stack snapshot. An instruction whose dispatch begins after all
prechecks and then reports a runtime error counts once; an instruction not
started because of cancellation or budget does not count.*

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: `VirtualMachine` und `SteppableVirtualMachine` müssen denselben
  ausführbaren P-Code, dieselben Stackregeln, dieselben historischen PL/0-Regeln
  und dieselben Laufzeitfehler semantisch gleich behandeln. / Both VM modes
  must treat executable P-Code, stack rules, historical PL/0 rules, and runtime
  errors equivalently.
- **FR-002**: Jede Ausführung muss ein validiertes positives
  Instruktionsbudget mit sicherem endlichem Standard besitzen. Beim Erreichen
  endet sie deterministisch vor Instruktion `N+1` mit strukturiertem Status und
  Diagnose. / Every execution must have a validated positive instruction
  budget with a safe finite default and end deterministically before
  instruction `N+1` when exhausted.
- **FR-003**: Normale und schrittweise Ausführung müssen kooperative
  Cancellation über einen .NET-`CancellationToken` unterstützen. Ein vor oder
  an einer Instruktionsgrenze beobachteter Abbruch führt ohne weitere
  Instruktion in einen stabilen Endzustand. / Normal and stepped execution must
  support cooperative cancellation and become terminal without another
  instruction when cancellation is observed before or at an instruction
  boundary.
- **FR-004**: Das Ergebnis muss mindestens Erfolg, Abschlussgrund,
  Instruktionszahl, Diagnosen und einen sicheren unveränderlichen Zustands-
  oder Stack-Snapshot enthalten. / Results must contain at least success,
  completion reason, instruction count, diagnostics, and a safe immutable state
  or stack snapshot.
- **FR-005**: Ein Step muss exakt eine Instruktion ausführen. Nach Halt,
  Cancellation, Budgetende oder Fehler darf kein weiterer Step Programmzustand,
  Instruktionszahl oder I/O verändern. / One step must execute exactly one
  instruction, and steps after any terminal condition must not change state,
  count, or I/O.
- **FR-006**: Stackgröße, Programmlänge, Instruktionsargumente, Sprungziele,
  Opcodes und I/O-Fehler müssen vor oder an ihrer Vertrauensgrenze validiert
  werden. Fehler dürfen keine internen Stacktraces oder Hostinterna ausgeben. /
  Stack size, program length, instruction arguments, jump targets, opcodes,
  and I/O errors must be validated at their trust boundary without exposing
  internal stack traces or host internals.
- **FR-007**: Die VM darf ausschließlich über das bereitgestellte `IPl0Io`
  kommunizieren und keinen Datei-, Netzwerk-, Prozess- oder Umgebungszugriff
  hinzufügen. / The VM may communicate only through the supplied `IPl0Io` and
  must add no file, network, process, or environment access.
- **FR-008**: Compilerdiagnosen müssen weiterhin gesammelt statt während der
  Kompilierung geworfen werden. Öffentliche Änderungen müssen vollständig
  deutsch/englisch dokumentiert sein. / Compiler diagnostics must continue to
  be collected rather than thrown during compilation, and public changes must
  be completely documented in German and English.
- **FR-009**: Gemeinsame Ausführungslogik oder vollständige Paritätstests müssen
  beweisen, dass Run und Step bei Erfolg, Halt, Budget, Cancellation,
  Division durch null, Stack- und I/O-Fehlern nicht auseinanderlaufen. /
  Shared execution logic or complete parity tests must prove that run and step
  do not diverge for success, halt, budget, cancellation, division by zero,
  stack errors, or I/O errors.
- **FR-010**: Die Paket-IDs müssen `TinyPl0.Core` und `TinyPl0.Vm` lauten.
  `TinyPl0.Vm` darf nur von der passenden Version von `TinyPl0.Core` abhängen;
  Core darf keine neue Laufzeitabhängigkeit erhalten. / Package IDs must be
  `TinyPl0.Core` and `TinyPl0.Vm`; VM may depend only on matching Core, and Core
  gains no new runtime dependency.
- **FR-011**: Beide Pakete müssen dieselbe von Release Please abgeleitete
  stabile SemVer verwenden. Die vierteilige IDE-Dateiversion darf die
  Paketversion nicht bestimmen. / Both packages must use the same stable
  Release Please SemVer; the four-part IDE file version must not determine it.
- **FR-012**: Beide Pakete müssen Repository-URL, Lizenz, Beschreibung, Tags,
  README, XML-Dokumentation, Symbole und Quellzuordnung enthalten und vor
  Veröffentlichung in einem unabhängigen Consumer geprüft werden. / Both
  packages must include repository URL, license, description, tags, README,
  XML documentation, symbols, and source mapping and be tested by an
  independent consumer before publication.
- **FR-013**: Lieferabschluss erfordert die direkte Verfügbarkeit beider
  Pakete auf NuGet.org. Ein lokaler Feed ist nur Vorprüfung. / Delivery
  completion requires direct availability of both packages on NuGet.org; a
  local feed is preflight only.
- **FR-014**: Der Releasepfad muss die Paket-ID-Verfügbarkeit unmittelbar vor
  Veröffentlichung erneut prüfen, beide Paketdateien atomar derselben Version
  zuordnen und einen Teilrelease sichtbar als Fehler behandeln. / The release
  path must recheck package-ID availability immediately before publication,
  bind both package files to one version, and expose a partial release as a
  failure.
- **FR-015**: Veröffentlichung darf nur mit aktueller ausdrücklicher Provider-
  und Veröffentlichungserlaubnis erfolgen. Die aktuelle Autorität erlaubt den
  späteren NuGet.org-Schritt, aber keine Secret-Beschaffung oder -Offenlegung. /
  Publication requires current explicit provider and publication authority;
  current authority permits the later NuGet.org step but no secret acquisition
  or disclosure.
- **FR-016**: Der plattformübergreifende Standardpfad muss die installierte
  .NET-SDK-Toolchain mit `dotnet pack` und `dotnet nuget push` gegen
  `https://api.nuget.org/v3/index.json` verwenden. `nuget.exe` oder eine
  separate Veröffentlichungs-App sind nicht erforderlich. / The cross-platform
  standard path must use the installed .NET SDK with `dotnet pack` and
  `dotnet nuget push` against the NuGet.org HTTPS V3 source; `nuget.exe` and a
  separate publishing app are not required.
- **FR-017**: GitHub Actions muss bevorzugt NuGet Trusted Publishing über den
  bereits autorisierten OIDC-Pfad verwenden. Die Vertrauenspolicy bindet
  GitHub-Owner, Repository, Workflow-Datei und gegebenenfalls das
  Release-Environment; der Job erhält nur `contents: read` und
  `id-token: write`. `NuGet/login` muss auf eine geprüfte unveränderliche
  Revision gebunden sein; `user` bezeichnet den NuGet.org-Profilnamen. /
  GitHub Actions must prefer NuGet Trusted Publishing through the already
  authorized OIDC route, with a policy bound to owner, repository, workflow,
  and optional release environment, only `contents: read` and
  `id-token: write`, an immutable reviewed `NuGet/login` revision, a temporary
  exchanged credential valid for no more than one hour, and the NuGet.org
  profile name rather than an email address as `user`.
- **FR-018**: Ein API-Key bleibt nur der dokumentierte Fallback, wenn Trusted
  Publishing nachweislich ungeeignet ist und eine neue ausdrückliche
  Secret-Autorität vorliegt. Er muss auf Push und beide Paket-IDs oder ein
  gleich enges Muster beschränkt, kurz gültig und von CI-Schlüsseln getrennt
  sein. `NUGET_API_KEY` ohne Kommandozeilenwert ist nur nach Nachweis einer
  NuGet-Version ab 7.6 zulässig; interaktive PowerShell-Eingabe muss maskiert
  sein und die Prozessvariable auch im Fehlerfall entfernt werden.
  Benutzername/Kennwort, Repositorydateien, Logs und Argumentlisten sind keine
  Secret-Speicher. / An API key remains only a documented fallback after
  proven OIDC unsuitability and fresh secret authority, with minimal push and
  package scope, short lifetime, separation from CI keys, proven NuGet 7.6+
  environment-variable support, masked interactive PowerShell input, and
  guaranteed process-variable cleanup; credentials must never enter source,
  logs, or command arguments.
- **FR-019**: Eine veröffentlichte Paket-ID/Version ist unveränderlich. Eine
  Korrektur muss gegebenenfalls auslisten, eine neue SemVer erhalten und den
  vollständigen Releasepfad erneut durchlaufen. / A published package
  ID/version is immutable; a correction may unlist, must receive a new SemVer,
  and must repeat the complete release path.
- **FR-020**: `--skip-duplicate` darf 409 nicht pauschal in Erfolg umdeuten.
  Nur ein fail-closed Abgleich von ID, Version, beabsichtigtem Release und
  Paket-Hash darf eine vorhandene Veröffentlichung anerkennen; beide Pakete
  müssen weiterhin gemeinsam belegt sein. / `--skip-duplicate` must not turn
  409 into generic success; only fail-closed reconciliation of ID, version,
  intended release, and package hash may accept an existing publication, and
  both packages must still be proven together.
- **FR-021**: Paket-, Consumer- und Cross-Platform-Tests müssen einen sauberen
  Restore ohne lokale ProjectReference oder privaten Feed auf macOS, Linux und
  Windows belegen. / Package, consumer, and cross-platform tests must prove a
  clean restore without a local project reference or private feed on macOS,
  Linux, and Windows.
- **FR-022**: Release-Tag, Quellcommit, Paketversion, Paket-Hashes, Symbol-
  und Quellzuordnung, Lockfile, Push-Ausgänge, Workflow-Identität,
  Credential-Modus, SBOM, VEX, Provenance/SLSA und öffentlicher Consumer-Restore
  müssen demselben Release zugeordnet sein. / Tag, source commit, package
  version and hashes, symbols and source mapping, lockfile, push outcomes,
  workflow identity, credential mode, SBOM, VEX, provenance/SLSA, and public
  consumer restore must bind to one release.
- **FR-023**: Aktualisierte Architektur-, VM-, API-, Paket-, DocFX- und
  Lernendokumentation, Traceability-Matrix, Sicherheits-/A11Y-Evidence und
  Projektstatistik gehören zum Feature-Abschluss. / Updated architecture, VM,
  API, package, DocFX, and learner documentation, traceability, security and
  accessibility evidence, and project statistics are part of completion.
- **FR-024**: TinyCalc erhält erst nach vollständiger öffentlicher Abnahme
  einen eindeutigen Handoff aus Paketversion, Hostvertrag und Gate-Evidence;
  eine lokale ProjectReference ist kein Fallback. / TinyCalc receives a clear
  handoff of package version, host contract, and gate evidence only after full
  public acceptance; a local project reference is not a fallback.
- **FR-025**: Merge, Default-Branch-Synchronisierung und kausaler Closeout
  dürfen erst nach vollständigen technischen, Review-, Paket-, Provider- und
  Evidence-Gates erfolgen. Admin-Bypass ist ausschließlich die eng begrenzte
  Repository-Policy-Ausnahme aus dem Autoritätsabschnitt. / Merge, default
  branch sync, and causal closeout may occur only after all technical, review,
  package, provider, and evidence gates; admin bypass is only the narrowly
  bounded repository-policy exception stated above.
- **FR-026**: Der manifestgebundene aktive Intake-Pfad bleibt während
  Implementierung und `MergeAndSync` unverändert. Archivierung oder Umbenennung
  ist nur eine separat autorisierte Post-Merge-Aktion; dieser Lauf startet kein
  Folgefeature. / The manifest-bound active intake path remains unchanged
  through implementation and `MergeAndSync`; archival or rename is a
  separately authorized post-merge action, and this run starts no follow-up
  feature.

### Verfassungsanforderungen / Constitution Requirements *(mandatory)*

- **CR-001**: Die TinyPl0-Zeile des Level-2-Umgebungsregisters ist verbindlich:
  .NET 10/C# 14, die vorhandenen Core-/VM-/CLI-/IDE-Module, xUnit,
  `dotnet restore/build/test`, DocFX und textorientierte A11Y-Prüfung. / The
  TinyPl0 Level-2 registry row is binding.
- **CR-002**: C#/.NET bleibt die speichersichere Hauptlaufzeit. Das Feature
  folgt Microsoft Secure Coding Guidelines; die MSL-Einstufung ersetzt keine
  Grenz-, I/O-, Ressourcen- oder Lieferkettenprüfung. / C#/.NET remains the
  memory-safe primary runtime and still requires secure boundary, I/O,
  resource, and supply-chain review.
- **CR-003**: NIST SSDF und CWE Top 25 sind anwendbar. Für diese Funktion sind
  insbesondere Ressourcenerschöpfung, Grenzverletzungen, unsichere Fehler- und
  Lieferkettenpfade sowie Credential-Offenlegung zu prüfen. / NIST SSDF and CWE
  Top 25 apply, especially resource exhaustion, boundary violations, unsafe
  error/supply-chain paths, and credential disclosure.
- **CR-004**: WCAG 2.2 AA ist für generierte HTML-Dokumentation und anwendbare
  Nutzerflächen die Prüfbasis. Alle wesentlichen Aussagen erhalten einen
  text-first Pfad. / WCAG 2.2 AA is the baseline for generated HTML and
  applicable user surfaces, with a text-first path for all essential meaning.
- **CR-005**: Lern- und nutzerseitige Dokumentation steht deutsch zuerst und
  englisch danach auf CEFR B2. Begriffe werden beim ersten Gebrauch erklärt;
  Spec-Kit-Vorkenntnisse werden nicht vorausgesetzt. / Learner and user
  documentation is German-first, English-second, CEFR B2, defines terms at
  first use, and assumes no Spec Kit knowledge.
- **CR-006**: Jede geänderte öffentliche API besitzt vollständige
  XML-Dokumentation. API- oder XML-Änderungen erfordern DocFX sowie
  Playwright/axe- und `lynx`-Evidence im selben Arbeitsgegenstand. / Every
  changed public API has complete XML documentation; API or XML changes require
  DocFX plus Playwright/axe and `lynx` evidence in the same work item.
- **CR-007**: Neue oder geänderte Logik zeigt TDD Rot, Grün und Regression.
  Nicht-triviale Ausführungs-, Cancellation- und Release-Logik wird auf kurze
  zweisprachige Warum-Kommentare geprüft. / New or changed logic records TDD
  red, green, and regression; non-trivial execution, cancellation, and release
  logic is reviewed for concise bilingual why-comments.
- **CR-008**: SBOM, VEX, Provenance/SLSA und OpenSSF Scorecard sind für die
  verteilbaren öffentlichen Pakete anwendbar. AI-SBOM ist N/A, weil KI nur
  Entwicklungswerkzeug ist. / SBOM, VEX, provenance/SLSA, and OpenSSF
  Scorecard apply; AI-SBOM is N/A because AI is development tooling only.
- **CR-009**: STRIDE/CIA und relevante CAPEC-Muster decken manipulierten
  Quelltext/P-Code, I/O, Ressourcenerschöpfung, Package Substitution,
  Workflow-/OIDC-Fehlbindung und Teilveröffentlichung ab. / STRIDE/CIA and
  relevant CAPEC patterns cover manipulated source/P-Code, I/O, resource
  exhaustion, package substitution, workflow/OIDC misbinding, and partial
  publication.
- **CR-010**: OWASP ASVS ist N/A, weil kein Web-, HTTP-, Authentifizierungs-
  oder API-Dienst entsteht. Zero Trust ist für die in-process VM N/A; OIDC
  bleibt eine Supply-Chain-Identitätsgrenze. / OWASP ASVS is N/A because no
  web, HTTP, authentication, or API service is created. Zero Trust is N/A for
  the in-process VM; OIDC remains a supply-chain identity boundary.
- **CR-011**: Die acht installierten Governance-Presets gelten ohne Ausnahme:
  Security, Secure Architecture, iSAQB Architecture, A11Y, Cross-Platform,
  Agent Parity, Autonomous Run und Parallel Autonomous Run. Das Parallel-Preset
  autorisiert keine Kampagne. / All eight installed governance presets apply;
  the parallel preset does not authorize a campaign.
- **CR-012**: Shared Agent Guidance, Projektvorlagen und die Constitution
  werden durch diese Produktfunktion nicht geändert. Falls Planung eine
  gemeinsame Regeländerung entdeckt, müssen `AGENTS.md`, `CLAUDE.md`,
  `GEMINI.md`, `.github/copilot-instructions.md`,
  `.github/agents/copilot-instructions.md`, betroffene Templates und
  `.specify/memory/constitution.md` gemeinsam neu bewertet werden. / This
  product feature does not change shared guidance, templates, or constitution;
  any discovered shared-rule change triggers joint reassessment of all listed
  surfaces.
- **CR-013**: Die Projektstatistik wird nach abgeschlossener Implementierung
  aktualisiert; Methodik und Baselines bleiben unverändert. / Project
  statistics are updated after completed implementation; methodology and
  baselines remain unchanged.

### Schlüsselentitäten / Key Entities

- **Hostoptionen / Host options**: Der gemeinsame, validierte Vertrag für
  Stackgrenze, positives Instruktionsbudget, Sprache, I/O-bezogene Optionen und
  Cancellation. / The shared validated contract for stack bound, positive
  instruction budget, language, I/O-related options, and cancellation.
- **Abschlussgrund / Completion reason**: Ein stabiler, maschinenlesbarer Grund
  wie Erfolg/Halt, Cancellation, Budget erreicht, Konfigurationsfehler,
  P-Code-/Stackfehler oder I/O-Fehler. / A stable machine-readable reason such
  as success/halt, cancellation, budget exhaustion, configuration error,
  P-Code/stack error, or I/O error.
- **Ausführungsergebnis / Execution result**: Erfolg, Abschlussgrund,
  Instruktionszahl, sichere Diagnosen und unveränderlicher Snapshot eines
  vollständigen Laufs. / Success, completion reason, instruction count, safe
  diagnostics, and immutable snapshot for a full run.
- **Schrittergebnis / Step result**: Genau ein Step oder eine vorgelagerte
  terminale Grenze mit demselben Abschluss-, Zähler-, Diagnose- und
  Snapshotvertrag. / Exactly one step or a pre-execution terminal boundary
  with the same completion, count, diagnostic, and snapshot contract.
- **Paketpaar / Package pair**: `TinyPl0.Core` und `TinyPl0.Vm` mit identischer
  stabiler SemVer, wobei VM nur die passende Core-Version referenziert. / The
  two package IDs at one stable SemVer, with VM referencing matching Core only.
- **Release-Evidence-Satz / Release evidence set**: Unveränderliche Zuordnung
  von Commit, Tag, Version, Paket-/Symbolhashes, Lockfile, Push-Ausgängen,
  Credential-Modus, Workflow-Identität, SBOM, VEX, Provenance/SLSA und
  öffentlichem Consumer-Restore. / Immutable binding of commit, tag, version,
  package/symbol hashes, lockfile, push outcomes, credential mode, workflow
  identity, SBOM, VEX, provenance/SLSA, and public consumer restore.

## Governance- und Evidence-Anwendbarkeit / Governance and Evidence Applicability

### Sicherheitsstandards / Security Standards

| Checkpoint | Applicability | Implementation | Rationale, evidence, and trigger |
|---|---|---|---|
| NIST SSDF | Applicable | Not Assessed | Full secure lifecycle; evidence in `docs/security/security-checklist.md` and feature gates. Reassess at every phase and release boundary. |
| CWE Top 25 | Applicable | Not Assessed | Boundary, resource, error, I/O, and credential risks; evidence in the security checklist, threat model, tests, and review. Reassess when public APIs or release flows change. |
| OWASP ASVS | N/A | Not Assessed | No web, HTTP, authentication-bearing, or service API is delivered. Reassess if such a service enters scope. |
| SBOM | Applicable | Not Assessed | Two public distributable packages require machine-readable component inventories; evidence in release assets and `docs/security/supply-chain-evidence.md`. |
| VEX | Applicable | Not Assessed | Known vulnerabilities in shipped/evaluated components require status; evidence in `docs/security/supply-chain-evidence.md` and `docs/security/dependency-audit.md`. |
| AI-SBOM | N/A | Not Assessed | AI is development tooling only; no model, dataset, inference service, or AI runtime ships. Reassess if an AI product component is added. |
| SLSA/provenance | Applicable | Not Assessed | Public CI-built packages require traceable provenance, targeting at least the practically achievable level and public-consumer integrity evidence. |
| OpenSSF Scorecard | Applicable | Not Assessed | TinyPl0 is public OSS and publishes packages; record observations in supply-chain and dependency evidence before release. |
| STRIDE/CAPEC | Applicable | Not Assessed | VM and publication trust boundaries materially change; update `docs/security/threat-model.md` and security quality scenarios. |
| Zero Trust | N/A | Not Assessed | The product execution boundary is in-process, not a distributed service. Reassess if remote execution or management is introduced; OIDC is handled as supply-chain identity. |
| OWASP SAMM | Applicable | Not Assessed | TinyPl0 is long-lived; update `docs/security/samm-assessment.md` if the release process changes maturity findings. |
| BSI C3A | Applicable | Not Assessed | NuGet.org/GitHub-based publication is provider-dependent; update `docs/security/cloud-autonomy-applicability.md` without claiming provider independence. |
| BSI C5 | Applicable | Not Assessed | Provider assurance for the publication path needs review in `docs/security/cloud-compliance-assurance.md`; no C5 certification claim is implied. |
| CRA | Open | Not Assessed | Public package distribution may be affected depending on manufacturer/commercial role. Owner: release owner; reviewer: security/legal reviewer; resolve in `docs/security/cra-applicability.md` before publication. Residual risk: wrong market-role assumption. Trigger: final distribution and commercial-context decision. |
| NIS2 | N/A | Not Assessed | This feature does not operate an essential/important entity or managed service. Reassess for adoption within a regulated operator or supply-chain contract. |
| EU AI Act | N/A | Not Assessed | No AI system or model is shipped or operated. Reassess if AI becomes a runtime/product component. |
| DORA | N/A | Not Assessed | No financial entity or regulated ICT service is operated by this feature. Reassess for a regulated financial-sector service contract. |

Für anwendbare Punkte sind Feature Owner und Implementierende für die
Erstellung verantwortlich; Architektur-, Security-, A11Y- oder Release-Review
prüfen jeweils unabhängig. Restrisiken bleiben bis zu den benannten Evidence-
und Abnahmegates offen. / For applicable items, the feature owner and
implementer produce evidence, with independent architecture, security,
accessibility, or release review. Residual risks remain open until the named
evidence and acceptance gates pass.

Die bestehenden Standardpfade `docs/security/asvs-verification.md`,
`supply-chain-evidence.md`, `zero-trust-applicability.md`,
`samm-assessment.md`, `cloud-autonomy-applicability.md`,
`cloud-compliance-assurance.md`, `cra-applicability.md` und
`regulatory-applicability.md` werden je nach Tabellenstatus aktualisiert oder
mit dem begründeten N/A bestätigt. Für jeden anwendbaren Eintrag ist der
Feature Owner Evidence-Owner; der passende Security-, Architektur-, A11Y- oder
Release-Reviewer prüft sie. Follow-up ist die Aktualisierung des benannten
Pfads vor dem jeweiligen Gate. Restrisiko ist unvollständige oder veraltete
Evidence; Re-Evaluation erfolgt an jeder Phase, bei Scope-/Providerdrift und
vor Veröffentlichung. / Existing default security paths are updated or retain
their reasoned N/A status. For every applicable entry, the feature owner owns
the evidence and the relevant specialist reviewer checks it. Follow-up is the
named file update before its gate; residual risk is incomplete or stale
evidence, with reassessment at every phase, on drift, and before publication.

### Architekturanwendbarkeit / Architecture Applicability

Das Feature ändert Systemkontext, öffentliche Schnittstellen, VM-Laufzeit,
Paket-/Deploymentgrenzen, Lieferkette und Qualitätsmerkmale. Daher sind
`docs/ARCHITECTURE.md`, `docs/VM_INSTRUCTION_SET.md`, die bestehende
Ressourcenbudget-ADR unter `docs/architecture/adr/0001-vm-resource-budget.md`
und die passende S-ADR zu prüfen und zu aktualisieren. Der Paket-/OIDC-
Lieferweg benötigt eine eigene nachvollziehbare Architekturentscheidung, wenn
er nicht vollständig von einer bestehenden ADR abgedeckt ist. Kontext-,
Building-Block-, Runtime-, Deployment-, Qualitäts- und Risikosicht werden in
`docs/architecture/` oder klar verlinkten bestehenden Dokumenten aktualisiert.

*The feature changes system context, public interfaces, VM runtime, package and
deployment boundaries, supply chain, and quality attributes. The architecture
overview, VM instruction set, existing resource-budget ADR, and matching S-ADR
therefore require review and update. Package/OIDC delivery requires a traceable
architecture decision unless an existing ADR fully covers it. Context,
building-block, runtime, deployment, quality, and risk views are updated under
`docs/architecture/` or in clearly linked existing documents.*

Runtime und Zielhardware verlangen keine nicht-speichersichere Sprache; die
vorhandene C#/.NET-Laufzeit bleibt für alle betroffenen Bausteine geeignet. /
Runtime and target hardware impose no non-memory-safe constraint; the existing
C#/.NET runtime remains suitable for all affected building blocks.

Trust Boundaries sind: unzuverlässiger PL/0-Quelltext zum Compiler;
manipulierbarer P-Code zur VM; Host-I/O über `IPl0Io`; Hostoptionen und
Cancellation zur Laufzeit; Repository/CI zum Paketartefakt; GitHub-OIDC zur
NuGet.org-Policy; NuGet.org zum öffentlichen Consumer. Quelltext, P-Code und
Host-I/O sind intern oder potenziell untrusted, Paketmetadaten und öffentliche
Artefakte sind öffentlich, Credentials und Identitätstoken sind restricted und
dürfen nicht in Evidence erscheinen.

*Trust boundaries are untrusted PL/0 source to compiler, manipulable P-Code to
VM, host I/O through `IPl0Io`, options and cancellation into runtime,
repository/CI to package artefact, GitHub OIDC to NuGet.org policy, and
NuGet.org to public consumer. Source, P-Code, and host I/O are internal or
potentially untrusted; package metadata and public artefacts are public;
credentials and identity tokens are restricted and must not appear in
evidence.*

Security-Architektur-Evidence aktualisiert `docs/security/threat-model.md`,
`docs/security/arc42-security.md`, `docs/security/security-quality-scenarios.md`
und erforderliche S-ADRs in `docs/security/adr/`. Defense in Depth besteht aus
Compiler-/P-Code-Validierung, positivem Budget, Stackgrenze, Cancellation,
isoliertem I/O, Paket-/Hashprüfung, eng gebundener OIDC-Identität und
öffentlichem Consumer-Restore. / Security architecture evidence updates the
threat model, arc42 security concepts, security quality scenarios, and needed
S-ADRs. Defense in depth combines compiler/P-Code validation, positive budget,
stack bound, cancellation, isolated I/O, package/hash verification, narrowly
bound OIDC identity, and public consumer restore.

### Plattformanwendbarkeit / Cross-Platform Applicability

Die Produktanforderung verlangt macOS-, Linux- und Windows-Parität für Pack,
Consumer-Restore und Tests. Diese Spezifikation schreibt kein neues
allgemeines Script-Tool fest; Script-Parität, Manpage, Cmdlet-Name und
`--dry-run`/`-WhatIf` sind daher derzeit `N/A`. Re-Evaluation: Sobald Planung
ein neues oder geändertes Script-Tool vorsieht, müssen Bash- und PowerShell-7-
Varianten gemeinsam in Scope kommen, die Bash-Manpage unter `docs/man/`,
zweisprachige PowerShell-Hilfe, ein genehmigtes `Verb-Noun`-Cmdlet und
Dry-Run-Parität festgelegt werden. Repository-native Workflows oder
Testprojekte ersetzen nicht die drei Plattformnachweise.

*Pack, public consumer restore, and tests require macOS, Linux, and Windows
parity. The specification does not mandate a new general-purpose script tool,
so script parity, man page, cmdlet name, and dry-run/WhatIf are currently N/A.
If planning introduces or changes a script tool, paired Bash and PowerShell 7
variants, a Bash man page, bilingual PowerShell help, an approved Verb-Noun
cmdlet, and dry-run parity become mandatory.*

### Agentenparität / Agent Parity Applicability

Shared Agent Guidance, `.specify/templates/` und
`.specify/memory/constitution.md` sind `N/A` für die beabsichtigte
Produktfunktion, weil weder gemeinsame Workflow-Regeln noch Model Routing
geändert werden. Es gibt keine beabsichtigte Abweichung zwischen Agentenflächen.
Re-Evaluation: Entdeckt Planung eine notwendige gemeinsame Regeländerung,
werden alle in CR-012 genannten Flächen atomar einbezogen. / Shared agent
guidance, templates, and constitution are N/A because the product feature
changes neither shared workflow rules nor model routing. There is no intended
surface deviation; any discovered shared-rule change triggers atomic review of
all CR-012 surfaces.

### Barrierefreiheit / Accessibility Applicability

Betroffene Nutzerflächen sind öffentliche API-/XML-Dokumentation,
Paket-README und -Metadaten, VM-Diagnosen, Release-/Security-Evidence,
Changelog/Release Notes und generiertes DocFX-HTML. CLI- oder IDE-Ausgaben
ändern sich nur, soweit sie den gemeinsamen VM-Abschlussvertrag anzeigen. Für
HTML gelten WCAG 2.2 AA, insbesondere Seitensprache, Bypass-Blöcke,
Tastaturfokus, Landmarken, Nicht-Text-Kontrast und lesbare Struktur. Markdown,
Diagnosen und Evidence benötigen text-first Statuswörter, semantische
Überschriften, beschriftete Codeblöcke und Textalternativen zu Diagrammen.

*Affected surfaces are public API/XML documentation, package README and
metadata, VM diagnostics, release/security evidence, changelog/release notes,
and generated DocFX HTML. CLI or IDE output changes only when presenting the
shared completion contract. HTML follows WCAG 2.2 AA; Markdown, diagnostics,
and evidence use text-first status words, semantic headings, labelled code
blocks, and text alternatives.*

Zielgruppe sind Lernende ab dem ersten Ausbildungsjahr, Lehrende und .NET-
Hostanwendende mit Grundbegriffen zu Compiler, VM und NuGet, aber ohne
Spec-Kit-Vorkenntnisse. A11Y-Evidence wird unter
`docs/accessibility/` aktualisiert. Nicht-triviale Logik ist `Applicable` für
didaktische Warum-Kommentare; reine offensichtliche Anweisungsumschaltung erhält
keine kommentierende Wiederholung. / The audience is first-year apprentices,
teachers, and .NET host users with basic compiler, VM, and NuGet terms but no
Spec Kit experience. Accessibility evidence is updated under
`docs/accessibility/`. Didactic why-comments apply to non-trivial logic, not to
obvious restatement.

### Dokumentationswirkung / Documentation Impact

**Decision: `UpdateRequired`**

- **Audiences**: Lernende, Lehrende, .NET-Hostanwendende, TinyCalc-Team,
  Release-, Security- und Architekturprüfende. / Apprentices, teachers, .NET
  hosts, TinyCalc team, and release/security/architecture reviewers.
- **Documentation families and reader paths**: API/XML/DocFX, Paket-README und
  Release Notes, `docs/ARCHITECTURE.md`, `docs/VM_INSTRUCTION_SET.md`,
  Host-/Paketnutzungsanleitung, Traceability-Matrix, Sicherheits-, A11Y- und
  Statistik-Evidence. Der Leserpfad führt von Paket-README zu Hostvertrag,
  Run/Step-Beispielen, Fehler-/Grenzvertrag und Release-Evidence. / API/XML/
  DocFX, package README and release notes, architecture, VM instruction set,
  host/package guide, traceability, security, accessibility, and statistics;
  the reader path runs from package README to host contract, examples,
  boundary/error contract, and release evidence.
- **Canonical source and owner**: `spec.md` und öffentliche XML-Kommentare sind
  während des Features kanonisch; Feature Owner pflegt Produkttexte,
  Security/Architecture/A11Y/Release Reviewer prüfen ihre Evidence. / The spec
  and public XML comments are canonical during the feature; the feature owner
  maintains product text and specialist reviewers verify evidence.
- **Navigation impact**: DocFX- und Repository-Navigation muss neue Host- und
  Paketpfade auffindbar machen; keine visuelle-only Navigation. / DocFX and
  repository navigation must expose new host and package paths without
  visual-only navigation.
- **Document class**: öffentliche Lern-/Referenzdokumentation plus
  quellinterne Audit-Evidence; Release-Evidence ist öffentlich, soweit sie
  keine Credentials oder restricted Details enthält. / Public learning and
  reference documentation plus source audit evidence; release evidence is
  public only when free of credentials and restricted details.
- **Language strategy and partner**: Deutsch inline zuerst, Englisch direkt
  danach, CEFR B2. Eine `.EN.md`-Sidecar ist nur für große normative Dokumente
  zulässig und muss synchron bleiben. / German first and English directly
  after, CEFR B2; synchronized `.EN.md` sidecars only for large normative docs.
- **Platform/example proof**: Beispiele und Consumer-Restore müssen auf macOS,
  Linux und Windows funktionieren; DocFX, Playwright/axe und `lynx` liefern
  den A11Y-Nachweis. / Examples and consumer restore work on all three target
  platforms; DocFX, Playwright/axe, and `lynx` provide accessibility proof.
- **Distribution class**: öffentliche NuGet-Pakete, öffentliche API-/Release-
  Dokumentation und repository-interne Prüfartefakte. / Public NuGet packages,
  public API/release documentation, and repository-internal audit artefacts.
- **Home-sync**: `false`; keine home-baseline-Regel oder Vorlage ändert sich. /
  `false`; no home-baseline rule or template changes.
- **Evidence and re-evaluation trigger**: Pfade unter `docs/security/`,
  `docs/accessibility/`, `docs/architecture/`, `docs/TRACEABILITY_MATRIX.md`
  und `docs/project-statistics.md`; neu bewerten bei API-, Paket-, Provider-,
  Navigations- oder Sprachenänderung. / Evidence lives in the named docs paths;
  reassess on API, package, provider, navigation, or language change.

### Anwendbarkeit des autonomen Laufs / Autonomous-run Applicability

Der Lauf verwendet `MergeAndSync` aus der aktuellen ausdrücklichen Autorität.
Akzeptierte Artefakte und Feature-Identität stehen im Bindungsabschnitt. Der
Lauf darf Scope, Nicht-Ziele, Reihenfolge, Hostvertrag, Paket-IDs oder
Evidence-Grenzen nicht erweitern. Nutzer-, Betriebs-, Security-, A11Y- und
historische Trigger sind Änderungen an API/Diagnosen, Laufzeitgrenzen,
Paketinhalt/-version, Provideridentität, Abhängigkeiten, Dokumentation,
Barrierefreiheit oder akzeptierten Hashes.

*The run uses `MergeAndSync` from current explicit authority. Accepted
artefacts and feature identity are listed above. It must not widen scope,
non-goals, order, host contract, package IDs, or evidence boundaries. Triggers
include changes to APIs/diagnostics, runtime bounds, package content/version,
provider identity, dependencies, documentation, accessibility, or accepted
hashes.*

Ein kausaler Closeout ist erforderlich, weil öffentliche Paketverfügbarkeit,
Merge, Default-Branch-Synchronisierung und Handoff erst nach ihrem tatsächlichen
Eintreten behauptet werden dürfen. Mutable Validation Tokens: `N/A` in der
Specify-Phase; vor Remote-Grenzen werden Exact-Head-, Review-, OIDC-/Provider-
und Public-Consumer-Evidence neu erzeugt. Ein absichtlicher Stop erfolgt nur an
einer sicheren Phasengrenze. `PausedByUser` verlangt ausdrücklich Resume;
unerwartete Unterbrechung, Hash-/Policy-/Authority-Drift, fehlende OIDC-Route,
offene technische/Review-Gates oder unsichere Teilveröffentlichung führen zu
`NeedsRevalidation` oder `Blocked`, niemals zu angenommenem Erfolg.

*Causal closeout is required because public availability, merge, default-branch
sync, and handoff may be claimed only after they occur. Mutable validation
tokens are N/A in Specify; exact-head, review, provider/OIDC, and public
consumer evidence is regenerated before remote boundaries. Deliberate stop is
at a safe phase boundary. `PausedByUser` requires explicit resume; interruption,
drift, missing OIDC, open technical/review gates, or unsafe partial publication
causes revalidation or blocking, never assumed success.*

Die stabilen Akzeptanzgates sind: `SPECIFY-GATE-001` Intake-/Scope-Parität;
`RUN-STEP-GATE-001` vollständige Run/Step-Parität; `RESOURCE-GATE-001`
Budget/Stack/Cancellation; `PACKAGE-GATE-001` Paketinhalt und SemVer-Paar;
`CONSUMER-GATE-001` sauberer öffentlicher Drei-Plattform-Restore;
`SECURITY-GATE-001` Security-/Architecture-/Supply-Chain-Evidence;
`DOC-A11Y-GATE-001` zweisprachige Dokumentation und A11Y;
`REMOTE-REVIEW-GATE-001` Exact-Head-CI, Review und Authority;
`NUGET-PUBLISH-GATE-001` OIDC, beide immutable Push-Ergebnisse und
409-Abgleich; `MERGE-CLOSEOUT-GATE-001` Merge, Sync, Handoff und kausaler
Abschluss. Konkrete Commands und Plattformtoken werden in Plan/Tasks und dem
Schema-2.0-Gatevertrag festgelegt; jede Scope-, API-, Paket-, Provider- oder
Policyänderung löst Re-Evaluation aus.

Retrospektive Erkenntnisse dürfen nur portable Regeln oder ausdrücklich als
TinyPl0-spezifisch markierte Fakten enthalten. Es wird kein leeres
Retrospektive-/Closeout-PR und kein Folgefeature erzeugt. / Retrospective
findings must be portable rules or explicitly TinyPl0-specific facts. No empty
retrospective/closeout PR or follow-up feature is created.

## Erfolgskriterien / Success Criteria *(mandatory)*

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: 100 % der Paritätsfälle für Erfolg, Halt, Division durch null,
  Stackfehler, I/O-Fehler, Budget und Cancellation liefern in Run und Step
  denselben Abschlussgrund, dieselbe Instruktionszahl, Ausgabe, Diagnosen und
  sicheren Snapshot. / All required parity cases match across both modes.
- **SC-002**: Für jedes geprüfte positive Budget `N` beendet eine
  Endlosschleife beide Modi nach genau `N` begonnenen Instruktionen und vor
  jedem Effekt von `N+1`. / Endless loops stop after exactly `N` started
  instructions with no effect from `N+1`.
- **SC-003**: In 100 % der Abbruchtests vor Start und an kontrollierten
  Laufgrenzen wird nach beobachteter Cancellation keine weitere Instruktion
  ausgeführt; nachfolgende Steps verändern den Zustand nicht. / All controlled
  cancellation tests execute no later instruction and terminal steps are
  immutable.
- **SC-004**: Alle bestehenden Compiler-, CLI-, IDE-, Golden- und VM-Tests
  bestehen; jede absichtlich geänderte Hostsemantik ist explizit dokumentiert
  und durch neue Regressionstests belegt. / All existing suites pass, with any
  intentional host-semantic change explicitly documented and regression-tested.
- **SC-005**: Ein unabhängiger .NET-10-Consumer stellt auf macOS, Linux und
  Windows beide Pakete aus einem sauberen NuGet.org-Feed ohne lokale
  ProjectReference wieder her und demonstriert Kompilierung, Run und Step. /
  A clean independent consumer succeeds on all three target platforms.
- **SC-006**: `TinyPl0.Core` und `TinyPl0.Vm` sind öffentlich unter derselben
  stabilen SemVer verfügbar; Paketinhalt, Abhängigkeiten, README, XML,
  Symbole und Quellzuordnung erfüllen 100 % der Paketchecks. / Both public
  packages share one stable SemVer and pass all content/metadata checks.
- **SC-007**: Release-Tag, Commit, SemVer, beide Paket- und Symbolhashes,
  Lockfile, zwei Push-Ausgänge, OIDC-Workflowidentität, SBOM, VEX,
  Provenance/SLSA und öffentlicher Consumer-Restore sind ohne Widerspruch einem
  Release zugeordnet. / All named release and supply-chain evidence converges
  on one release without contradiction.
- **SC-008**: Kein Secret-Wert erscheint in Quelltext, Git-Status,
  Kommandozeilenargumenten, Logs, Evidence oder Chat; die Veröffentlichung
  verwendet die autorisierte OIDC-Route oder blockiert. / No secret value
  appears in any prohibited surface; publication uses authorized OIDC or
  blocks.
- **SC-009**: Repräsentative DocFX-Seiten haben keine offenen schwerwiegenden
  oder kritischen automatisierten A11Y-Funde; `lynx`- und manuelle Textprüfung
  bestätigen vollständige Navigation und Bedeutung. / Representative DocFX
  pages have no open serious or critical automated accessibility finding, and
  text-browser/manual checks preserve navigation and meaning.
- **SC-010**: 100 % der geänderten öffentlichen APIs besitzen vollständige
  anwendbare XML-Dokumentation deutsch zuerst und englisch danach; Traceability,
  Architektur, Security, A11Y und Statistik sind aktuell. / Every changed
  public API has complete bilingual XML documentation and all named evidence
  families are current.
- **SC-011**: Ein 409-Wiederholungsfall kann weder Inhalt überschreiben noch
  ohne hashgebundene Beweise beider Pakete als Erfolg gelten. / A 409 retry
  cannot overwrite content or count as success without hash-bound proof of
  both packages.
- **SC-012**: TinyCalc erhält genau einen textorientierten Handoff mit
  öffentlicher Paketversion, Hostvertrag und vollständiger Gate-Evidence; bis
  dahin bleibt es blockiert. / TinyCalc receives one text-first handoff only
  after all public package and gate evidence is complete.

## Annahmen / Assumptions

- Der akzeptierte Intake und seine vier gespeicherten Hashes bleiben während
  dieses Features unverändert. / The accepted intake and four stored hashes
  remain unchanged during this feature.
- Die Paket-IDs `TinyPl0.Core` und `TinyPl0.Vm` sind beabsichtigt; ihre
  Live-Verfügbarkeit wurde bei der Intake-Vorprüfung am 29.08.2026 als frei
  beobachtet und wird unmittelbar vor der Provideraktion erneut geprüft. /
  The package IDs are intended, were observed as unregistered during intake
  preflight on 2026-08-29, and are rechecked just before provider action.
- Release Please bleibt die Quelle der gemeinsamen Paket-SemVer; die
  IDE-Dateiversion folgt weiterhin ihrem getrennten Vertrag. / Release Please
  remains the package SemVer source; IDE file versioning remains separate.
- Bestehende Budget-/Stack-Härtung ist ein zu erhaltender Ausgangspunkt, kein
  Ersatz für den vollständigen Host-, Cancellation-, Result- und
  Paketvertrag. / Existing budget/stack hardening is a baseline to preserve,
  not a substitute for the complete host, cancellation, result, and package
  contract.
- Der bereits autorisierte OIDC-Providerpfad kann ohne Offenlegung eines
  langlebigen Secrets verwendet werden. Falls diese Annahme an der
  Veröffentlichungsgrenze nicht belegbar ist, blockiert der Lauf. / The
  authorized OIDC route can be used without exposing a long-lived secret; if
  this cannot be proven at publication, the run blocks.
- Ein API-Key-Fallback bleibt fachlich dokumentiert, ist unter der aktuellen
  Secret-Grenze aber nicht ausführbar. / The API-key fallback remains part of
  the product contract but is not executable under the current secret boundary.
- Keine neue Sprache, kein neues Backend, keine Optimierung, keine
  TinyCalc-Logik und kein zusätzliches Feature werden benötigt. / No new
  language, backend, optimization, TinyCalc logic, or additional feature is
  required.
