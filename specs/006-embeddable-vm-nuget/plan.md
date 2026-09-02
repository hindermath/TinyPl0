# Implementierungsplan: Einbettbare VM und NuGet-Pakete / Implementation Plan: Embeddable VM and NuGet Packages

**Branch**: `codex/006-embeddable-vm-nuget` | **Datum / Date**: 2026-09-02 | **Spezifikation / Spec**: [spec.md](spec.md)
**Eingabe / Input**: akzeptierte Spezifikation, [Klärungsbericht](clarification-report.md), [Pre-Implementation-Checkliste](checklists/pre-implementation.md) und aktiver autonomer Lauf / accepted specification, clarification report, pre-implementation checklist, and active autonomous run

## Zusammenfassung / Summary

TinyPl0 erhält einen stabilen .NET-10-Hostvertrag für vollständige und
schrittweise PL/0-Ausführung. Beide öffentlichen Einstiege delegieren an genau
eine interne `VmExecutionSession`. Sie prüft Optionen und P-Code einmal,
entscheidet an jeder Instruktionsgrenze in der festen Reihenfolge bereits
terminaler Zustand, Cancellation, Budget und Dispatch und zählt unmittelbar
vor dem gemeinsamen Dispatch. Terminale Ergebnisse werden gespeichert;
spätere Steps geben dieselbe unveränderte Projektion zurück.

*TinyPl0 gains a stable .NET 10 host contract for full and stepped PL/0
execution. Both public entry points delegate to one internal execution session,
which validates once, applies one boundary order, shares one dispatcher, and
caches terminal results for idempotent later steps.*

`TinyPl0.Core` und `TinyPl0.Vm` werden als getrennte Pakete mit derselben von
Release Please gepflegten SemVer gebaut. Der OIDC-First-Releasepfad verwendet
vollständige Action-SHAs, getrennte Least-Privilege-Jobs, Paket-/Symbolhashes,
SBOM, VEX und SLSA-Provenance. Ein fail-closed Zustandsautomat akzeptiert nur
`None` oder bereits vollständig hashgleiches `Both`; Teilrelease, unbekannter
409 oder fehlende OIDC-Policy blockiert. Diese Phase führt keine
Implementierung, Builds/Tests, Commits oder Remote-/Provideraktionen aus.

## Technischer Kontext / Technical Context

**Sprache/Version / Language/Version**: C# 14, .NET 10 (`net10.0`), MSBuild/XML, YAML, JSON und zweisprachiges Markdown
**Primäre Abhängigkeiten / Primary Dependencies**: .NET SDK 10.0.x, xUnit 2.9.3, Microsoft.NET.Test.Sdk 18.0.1, Coverlet 8.0.0, DocFX, CycloneDX .NET 6.2.0, Node 24, Playwright 1.62.1, axe 4.13.0 und `lynx`; keine neue Produkt-Laufzeitabhängigkeit
**Speicherung / Storage**: unveränderliche In-Memory-Snapshots und dateibasierte Paket-/Hash-/SBOM-/VEX-/Provenance-Evidence; keine Datenbank
**Tests / Testing**: xUnit Unit-, Paritäts-, Boundary-, Kompatibilitäts-, Pack- und Workflow-Vertragstests; sauberer Consumer; XPlat Coverage; DocFX/axe/`lynx`
**Zielplattform / Target Platform**: `macos-15`, `ubuntu-24.04`, `windows-2025`; NuGet.org V3
**Projekttyp / Project Type**: Compilerbibliothek, Stack-VM, CLI/IDE-Verbraucher und öffentliche NuGet-Lieferkette
**Leistungsziel / Performance Goal**: höchstens `InstructionBudget` begonnene Dispatches, kein Effekt von `N+1`; keine Zeit-/Sandbox-Garantie
**Grenzen / Constraints**: Stack `3..1_000_000` (Standard `500`), Budget `1..10_000_000` (Standard `1_000_000`), Programmlänge `1..100_000` (Standard `10_000`), Level `0..3`; VM-I/O ausschließlich über `IPl0Io`
**Umfang / Scale**: zwei Pakete, ein Kernel, zehn Gates, zwölf SCs, 26 FRs, 13 CRs, drei Plattformen

## Constitution Check

*Gate vor Phase 0 und nach Phase 1 erneut geprüft; alle Punkte sind aufgelöst. /
Gate before Phase 0 and rechecked after Phase 1; every item is resolved.*

Die Registry-Zeile `RiderProjects/TinyPl0` bindet .NET 10/C# 14, die
vorhandenen Module, `dotnet restore/build/test`, xUnit, Coverage, Golden-
Nachweise, DocFX/A11Y, Statistikbasen `80`/`125` Zeilen pro Arbeitstag und die
Agentenflächen. C#/.NET ist MSL; das ersetzt keine Boundary-, Ressourcen-,
I/O-, Fehler- oder Supply-Chain-Prüfung.

| Prüfpunkt / Checkpoint | Entscheidung und Evidence / Decision and evidence |
|---|---|
| Branch/PR | `Pass`: Arbeit bleibt auf `codex/006-embeddable-vm-nuget`; `main` ist geschützt. Commit, PR, Merge und Sync bleiben spätere getrennte Zustände des `MergeAndSync`-Laufs. |
| Toolchain | `Pass`: .NET SDK 10.0.x/C# 14, PowerShell 7 lokal und die drei benannten Hosted Runner. Jede Action erhält einen vollständigen geprüften SHA. |
| Modulgrenzen | `Pass`: `Core -> none`, `Vm -> Core`, `Cli/Ide -> Core+Vm`; Session, Validator und Resulttypen bleiben in `Pl0.Vm`. |
| Allgemeine Architektur | `Applicable`: Runtime, öffentliche API, Packaging und Deployment ändern sich. `docs/ARCHITECTURE.md`, eine arc42-Sicht und ADRs für Kernel und Releasepfad dokumentieren Kontext, Bausteine, Runtime, Deployment, Qualität, Risiken und Schuld. |
| Sichere Architektur | `Applicable`: Trust Boundaries Source→Compiler, P-Code/Optionen/Cancellation→Session, `IPl0Io`, Repo→CI, GitHub OIDC→NuGet.org und NuGet.org→Consumer. Threat Model, arc42 Security, Quality Scenarios und S-ADRs werden aktualisiert. |
| Secure Coding | `Applicable`: NIST SSDF, CWE Top 25 und Microsoft-Regeln. Review prüft numerische Grenzen vor Allokation, defensive Kopien, Cancellation-Rennen, sichere Exception-Übersetzung und keine internen/credentialbezogenen Details. |
| Standards | `Applicable`: SBOM, VEX, GitHub-Attestierung/SLSA Build L2, STRIDE/CAPEC, OpenSSF Scorecard, SAMM, BSI C3A/C5 und CRA. ASVS, Zero Trust, AI-SBOM, NIS2, EU AI Act und DORA bleiben begründet `N/A` mit Spec-Triggern. |
| Security-Dokumente | `UpdateRequired`: Threat Model, arc42, Checkliste, Security Quality Scenarios, Dependency Audit, Supply Chain `.md/.json`, SAMM, C3A/C5, CRA/Regulatory und S-ADRs. ASVS/Zero Trust bestätigen N/A. |
| TDD | `Applicable`: vorab abgebrochener Run/Step ist der erste Rot-Grün-Schnitt; danach shared-dispatch-, Boundary-, Paket- und Workflow-Scheiben. Roter Grund und unveränderter Testhash werden erfasst. |
| Coverage | `Applicable`: Gesamtlinie `>=70%`, Ziel `>=80%`; gemeinsamer Kernel `>=85%` Linie und Branch. Cobertura wird am exakten Head geprüft. |
| Dependencies | `Applicable`: Core ohne Runtime-Abhängigkeit; VM nur exakte Core-Range `[$(TinyPl0PackageVersion)]`. Lockfiles, Aktualität, Lizenz und CVEs sind Pflicht; Pin-Ausnahmen benötigen Owner/Trigger. |
| Daten/Serialisierung | `Applicable`: defensive Kopien/read-only Projektionen. Evidence ist UTF-8-JSON mit stabilen IDs, lowercase SHA-256 und sicheren relativen Pfaden; keine polymorphe/unsichere Deserialisierung. |
| XML/API | `Applicable`: `VirtualMachine`, `SteppableVirtualMachine`, Optionen, Results, Status, State, Diagnostic und `IPl0Io` ändern sich; `VmCompletionReason` kommt hinzu. Vollständige DE-first/EN-second XML-Elemente, keine CS1591-Unterdrückung; private/lokale/generierte Flächen ausgenommen. |
| DocFX/A11Y | `Applicable`: DocFX, Playwright/axe, `lynx` und manuelle Textprüfung im selben Work Item; Start, Hostleitfaden, Paket-READMEs und neue API-Seiten; keine offenen serious/critical Funde. |
| Bilingual/Lernende | `Applicable`: README, Hostleitfaden, Diagnosen, XML, Evidence und Handoff DE zuerst/EN danach, CEFR B2, text-first und ohne Spec-Kit-Vorwissen. |
| Cross-Platform | `Applicable`: Pack/Test/Consumer auf `macos-15`, `ubuntu-24.04`, `windows-2025`. Kein neues allgemeines Script-Tool, daher Scriptpaar/Manpage/Cmdlet/Dry-Run `N/A`; Trigger ist ein neues/geändertes Repo-Skript. |
| IDE-Version je Aufruf | `Applicable`: vor jedem lokalen Build/Test setzt ein serialisierter Writer die drei IDE-Felder auf Major `1`, Minor = revalidierte PR-Nummer, Patch = Commitcount nach Commit, Build = +1. Keine parallelen Aufrufe; CI schreibt nicht. |
| Release Please/SemVer | `Applicable`: `eng/TinyPl0.PackageVersion.props` ist einzige Paketversion und Release-Please-`extra-files`-Ziel. IDE-Version wird nie importiert. |
| Agentenparität | `N/A`: keine Guidance-/Template-/Constitution-/Routing-Regel ändert sich. Bei tatsächlichem Regelbedarf löst CR-012 den atomaren Dateisatz aus. |
| Statistik | `Applicable`: letzter chronologischer Ledger-Eintrag nach Implementierung mit Linienarten, Arbeitsfenster, `80`/`125`, `7.8h`, `21.5` Tagen/Monat und blended speedup; Renderer/CheckOnly. |
| Security-first | `Pass`: keine Secrets oder `.codex`-Zustände werden gelesen/gespeichert. OIDC fehlt/driftet ⇒ blockiert. API-Key-Fallback ist unter aktueller Autorität nicht ausführbar. |
| Dokumentationswirkung | `UpdateRequired`: Reader Path Paket-README → Hostleitfaden → Run/Step-/Fehlergrenzen → Release-Evidence. Home-sync `false`; Trigger sind API-, Paket-, Provider-, Navigations- oder Sprachdrift. |
| Parallel Autonomous | `N/A`: keine Kampagne/Delegation autorisiert. |

## Architektur- und API-Entscheidungen / Architecture and API Decisions

### Gemeinsamer Kernel / Shared Kernel

`internal sealed VmExecutionSession` besitzt Programmkopie, validierte Optionen,
Token, `IPl0Io`, Register, Stack, Diagnosen, Zähler und gespeichertes
Terminalresultat. `ExecuteNext()` enthält den einzigen Opcode-/OPR-Dispatch.
`VirtualMachine.Run()` loopt darüber; `SteppableVirtualMachine.Step()` ruft ihn
genau einmal auf. Es bleibt keine zweite Switch-Anweisung.

`VmProgramValidator` läuft nach Optionsvalidierung und vor Stackallokation. Er
akzeptiert nur `1..MaximumProgramLength`, definierte Opcodes, Level `0..3`,
erlaubte OPR-Codes, nichtnegative Stack-/Adressargumente und Sprung-/Call-Ziele
innerhalb des Programms. Diagnosen sind nach Instruktionsindex stabil.

### Reihenfolge und Zählpunkt / Precedence and Counting Point

1. Optionen: Stack, Budget, Programmlänge, Sprache.
2. Danach vollständige Programmvalidierung.
3. An jeder Grenze: gespeichertes Terminalresultat.
4. Danach Cancellation.
5. Danach Budget.
6. Danach Pointer/Instruktion wählen, Zähler erhöhen, Dispatch beginnen.

Ein später fehlschlagender Dispatch zählt einmal. Cancellation/Budget vor
Dispatch zählen nicht. Cancellation während `IPl0Io` rollt den begonnenen
Aufruf nicht zurück, wird aber vor dem nächsten Dispatch terminal. Weitere
Steps verändern Resultat, State, Zähler, Diagnosen und I/O nicht.

### Öffentlicher Vertrag / Public Contract

| API | Vertrag / Contract | Kompatibilität / Compatibility |
|---|---|---|
| `VirtualMachine.Run(program, io = null, options = null, cancellationToken = default)` | voller Lauf über Session; null-Programm wird `InvalidProgram` | letzter Optionalparameter |
| `SteppableVirtualMachine.Initialize(..., cancellationToken = default)` | gleiche Session; Token gilt für folgende parameterlose Steps | letzter Optionalparameter |
| `VirtualMachineOptions` | bestehende fünf Parameter bleiben; letzter Parameter `MaximumProgramLength = 10_000`; Budget max. `10_000_000` | nur optionale Ergänzung am Ende |
| `VmCompletionReason` | explizit nummeriert: `Running`, `Halted`, `Cancelled`, `InstructionBudgetExceeded`, `InvalidConfiguration`, `InvalidProgram`, `StackFault`, `ArithmeticFault`, `InputEndOfStream`, `InputFormatError`, `IoFault`, `RuntimeFault` | neuer stabiler Enum |
| `VmExecutionResult` | `Success`, Reason, `ExecutedInstructions`, `State`, Diagnostics; alte Stack/Top-Projektionen bleiben | alter Konstruktor bleibt; defensive Kopien |
| `VmStepResult` | gemeinsame Felder plus alter Status; Mapping Running/Halted/sonst Error | alter Konstruktor und Deconstruct bleiben |
| `VmState` | P/B/T/CurrentInstruction; Stack liefert Kopie; neuer Zähler | bestehende Signatur/Deconstruct bleiben |
| `VmDiagnostic` | stabiler Code und lokalisierte sichere Nachricht; keine fremde Exception-Nachricht | bestehende Felder bleiben |

`Success` gilt nur für `Halted`. Erwartete I/O-/Formatfehler und nicht-fatale
Host-I/O-Ausnahmen werden ohne fremde Nachricht strukturiert. OOM,
StackOverflow und AccessViolation werden nicht als normaler I/O-Fehler
verschleiert.

## Tests und Gates / Tests and Gates

Erster Rot-Grün-Schnitt: `VmCancellationTests` startet Run und Step mit bereits
abgebrochenem Token und beobachtbarem I/O; erwartet `Cancelled`, Zähler `0`,
leere Ausgabe und drei unveränderliche Folge-Steps. Ausgangsstand kompiliert
wegen fehlendem Token-/Reason-Vertrag nicht. Minimal Grün ergänzt Token, Reason,
Sessionhülle und Terminalcache. Danach erzwingt ein roter Paritätstest einen
gemeinsamen Dispatch, bevor der Opcode-/OPR-Code genau einmal verschoben wird.

| Matrix | Pflichtfälle / Required cases | Beweis / Proof |
|---|---|---|
| Parität | Halt, Division null, ungültiger OPR/Opcode/IP, Stack, EOF, Format, Host-I/O, Budget, Cancellation | Reason, Zähler, Diagnosefolge, Snapshot und gepufferte Ausgabe gleich |
| Ressourcen | Budget `0/-1/1/N/10_000_000/10_000_001`; Stack `2/3/500/1_000_000/1_000_001/int.MaxValue`; Programmlänge `0/1/10_000/10_001` | kein Dispatch/keine Allokation bei invalid; kein Effekt `N+1` |
| P-Code | jeder Opcode, OPR-Allowlist, fremder Enumwert, Level `-1/0/3/4`, negative Argumente, Ziel `-1/count` | Vorvalidierung und stabile Diagnosen |
| Idempotenz | jeder terminale Reason plus wiederholte Steps | gleiche Projektion, keine Mutation/I/O |
| Kompatibilität | alte Konstruktoren/Methoden, CLI, IDE, 41 Fälle, Golden | vollständige Regression |
| Pakete | Nuspec, README, XML, DLL, snupkg, SourceLink, exakte VM→Core-Range | ZIP-/Nuspec-Inventar und Hashmanifest |
| Consumer | leerer Cache, nur NuGet.org, gleiche Version, compile/run/step | drei OS, keine ProjectReference/privater Feed |

Gesamt-Coverage muss `>=70%` bleiben, Ziel `>=80%`; Kernel `>=85%` Linie und
Branch. DocFX/A11Y und alle bestehenden Compiler-/CLI-/IDE-/Golden-/L10N-Tests
sind Regression.

## Paket- und Release-Design / Package and Release Design

- `eng/TinyPl0.PackageVersion.props` enthält nur
  `TinyPl0PackageVersion`. Core/VM leiten Package/Assembly/File-Version ab; IDE
  importiert nicht. Release Please aktualisiert die Property per XML-XPath.
- Core: `TinyPl0.Core`, keine Runtime-Abhängigkeit. VM: `TinyPl0.Vm`, nur
  `TinyPl0.Core` mit Range `[$(TinyPl0PackageVersion)]`. Ein enges Target setzt
  vor `GenerateNuspec` nur den `ProjectVersion` dieser ProjectReference; der
  Pack-Test verwirft jede andere Range.
- Beide Pakete enthalten Repository URL/Type, MIT, Autor, Beschreibung, Tags,
  README, XML, SourceLink/PDB und `.snupkg`; `PublishRepositoryUrl`,
  `EmbedUntrackedSources`, CI deterministic build und Lockfiles sind Pflicht.
- Release-Tag, Manifest, Props, nupkg/snupkg, Nuspec, Hashes, SBOM, VEX und
  Consumer verwenden dieselbe stabile SemVer. Die IDE-Vierteilung bleibt
  getrennt.

Der bestehende `.github/workflows/release-please.yml` erhält:

1. `build-release`: `contents: read`, `id-token: write`,
   `attestations: write`; einmal packen, prüfen, hashen, CycloneDX/VEX erzeugen,
   attestieren und als einen Artefaktsatz hochladen.
2. `publish-nuget`: nur `contents: read`, `id-token: write`, Environment
   `nuget-release`; denselben Satz laden, Zustandsautomat prüfen,
   `NuGet/login` mit `vars.NUGET_USER`, dann beide nupkg pushen. Der temporäre
   Key liegt nur als Step-Env `NUGET_API_KEY` vor, nie in Argument/Log/Datei.
3. `verify-public`: `contents: read`; beide öffentlichen Pakete/Symbole neu
   laden, Hashes vergleichen, Consumer ausführen, Manifest finalisieren.

Vollständige Pins: `release-please-action@45996ed1f6d02564a971a2fa1b5860e934307cf7`,
`checkout@3d3c42e5aac5ba805825da76410c181273ba90b1`,
`setup-dotnet@d4c94342e560b34958eacfc5d055d21461ed1c5d`,
`upload-artifact@ea165f8d65b6e75b540449e92b4886f43607fa02`,
`download-artifact@d3f86a106a0bac45b974a628896c90dbdf5c8093`,
`NuGet/login@8d196754b4036150537f80ac539e15c2f1028841` und
`actions/attest@508db95dd578ae2727ebd6217d5ba78e4fbda05d`.

NuGet.org-Policy: Owner `hindermath`, Repository `TinyPl0`, Workflowdatei
`release-please.yml`, Environment `nuget-release`, Scope nur beide IDs.
Fehlende/abweichende Evidence blockiert; kein Workflow legt Policies an oder
beschafft Secrets.

`tools/Pl0.ReleaseVerifier` verarbeitet keine Credentials und kennt:

- `None`: beide fehlen; Push darf beginnen.
- `BothMatching`: beide vorhanden und nupkg-Hashes passen; idempotenter Erfolg
  ohne Push.
- `Partial`: genau eine ID vorhanden; sofort Fehler, keine fehlende Hälfte
  nachschieben.
- `Conflict`: beide vorhanden, aber Hash/Releasebindung weicht ab; Fehler.

409 ist nie allein Erfolg. Nach Push/409 muss `BothMatching` bewiesen werden.
`--skip-duplicate` wird nicht verwendet; ein späterer Einsatz dürfte den
Nachabgleich nicht ersetzen. Teilrelease verlangt neue SemVer und vollständigen
Releasepfad.

## Phasen / Phases

### Phase 0 — Forschung / Research

Accepted hashes, Run-ID, Scope, API, Tests, Workflows, Security-/Paketbestand
read-only binden und offizielle NuGet-/GitHub-Verträge sowie Pins in
[research.md](research.md) festhalten. Keine Build-/Provideraktion.

### Phase 1 — Design / Design

[data-model.md](data-model.md) definiert Session, Result, Snapshot, Paketpaar,
Releasezustand und Evidence. [contracts/host-api.md](contracts/host-api.md)
bindet API/Defaults/Precedence/Idempotenz;
[contracts/release-evidence.md](contracts/release-evidence.md) bindet
SemVer/Paket/OIDC/Hash/Handoff. [quickstart.md](quickstart.md) beschreibt die
spätere TDD-/Validierungsreihenfolge. [gate-requirements.json](gate-requirements.json)
bindet zehn Gates vor dem ersten Implementierungsedit.

### Phase 2 — spätere Tasks / Later Tasks

`/speckit.tasks`: Revalidierung → Cancellation-Rot → Session-Grün →
Shared-Dispatch-Rot/Grün → Parität/Bounds → Package/SemVer → lokaler
Pack/Consumer → Workflow/ReleaseVerifier-Simulation → Architektur/Security →
DocFX/A11Y → drei OS → Coverage/Statistik → Exact-Head/Provider/Publish/Closeout.
Kein Produktedit vor bestandenem Plan Review, Tasks und Analyze.

## Traceability

| Anforderungen | Arbeitspaket | Gate |
|---|---|---|
| FR-001–009; SC-001–004 | Session, Validator, Cancellation, Result, Parität/Boundary/Kompatibilität | `RUN-STEP-GATE-001`, `RESOURCE-GATE-001` |
| FR-010–012; SC-006 | zentrale SemVer, csproj/README/XML/Symbole/SourceLink, exakte Range | `PACKAGE-GATE-001` |
| FR-013–022; SC-005–008, SC-011 | Release Please, Verifier, OIDC, Pins, Hash, SBOM/VEX/SLSA, drei OS, Restore | `CONSUMER-GATE-001`, `SECURITY-GATE-001`, `REMOTE-REVIEW-GATE-001`, `NUGET-PUBLISH-GATE-001` |
| FR-023–024; SC-009–010, SC-012 | Architektur/VM/Host/Paket-Doku, DocFX/A11Y, Matrix, Statistik, Handoff | `DOC-A11Y-GATE-001`, `MERGE-CLOSEOUT-GATE-001` |
| FR-025–026; CR-001–013 | Scope/Authority, Intake stabil, kausaler Merge/Sync/Closeout, kein Folgefeature | `SPECIFY-GATE-001`, `REMOTE-REVIEW-GATE-001`, `MERGE-CLOSEOUT-GATE-001` |

## Projektstruktur / Project Structure

```text
specs/006-embeddable-vm-nuget/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── gate-requirements.json
├── checklists/pre-implementation.md
└── contracts/{host-api.md,release-evidence.md}

eng/TinyPl0.PackageVersion.props
src/Pl0.Core/{Pl0.Core.csproj,PACKAGE_README.md}
src/Pl0.Vm/
├── Pl0.Vm.csproj
├── VirtualMachine.cs
├── SteppableVirtualMachine.cs
├── VirtualMachineOptions.cs
├── VmExecutionSession.cs
├── VmProgramValidator.cs
├── VmCompletionReason.cs
├── VmExecutionResult.cs
├── VmStepResult.cs
├── VmStepStatus.cs
├── VmState.cs
├── VmDiagnostic.cs
├── IPl0Io.cs
├── PACKAGE_README.md
└── Resources/Pl0VmMessages*.resx
tools/Pl0.ReleaseVerifier/
tests/Pl0.Tests/{VmRunStepParityTests.cs,VmCancellationTests.cs,VmExecutionBoundaryTests.cs,NuGetPackageContractTests.cs,NuGetReleaseWorkflowContractTests.cs}
tests/consumers/TinyPl0.PackageConsumer/{TinyPl0.PackageConsumer.csproj,NuGet.config,Program.cs}
.github/workflows/{ci.yml,release-please.yml}
release-please-config.json
.release-please-manifest.json
.config/dotnet-tools.json
docs/{ARCHITECTURE.md,VM_INSTRUCTION_SET.md,TRACEABILITY_MATRIX.md,project-statistics.md}
docs/architecture/{embeddable-vm-nuget.md,adr/0001-vm-resource-budget.md,adr/0002-shared-vm-execution-kernel.md,adr/0003-nuget-release-boundary.md}
docs/security/ and docs/security/adr/0002-oidc-nuget-publication.md
docs/accessibility/embeddable-vm-nuget.md
docs/host/embeddable-vm.md
docs/handoff/tinycalc-vm-packages.md
tests/a11y/{package.json,package-lock.json,docfx-a11y.spec.mjs}
src/Pl0.Ide/Pl0.Ide.csproj
```

Agenten-/Constitution-/Templateflächen bleiben außerhalb. Der aktive Intake
bleibt bis zu separat autorisiertem Post-Merge unverändert; kein Folgefeature.

## Serialisierte Writer / Serialized Writers

| Artefakt | Writer | Regel |
|---|---|---|
| VM API/Session | VM task | keine parallelen Dispatch-/Resultedits; Rot/Grün-Testhash je Scheibe |
| Paket-SemVer | package task | nur `eng/TinyPl0.PackageVersion.props`; Gleichheit durch Release/Pack-Vertrag |
| IDE-Version | version task | eigener Commit vor jedem lokalen Build/Test; monotone Invocation-ID |
| Workflow/Verifier | release task | statische Tests plus simulierte None/Both/Partial/Conflict/409-Fixtures |
| Security/Architektur/A11Y | Evidence-Owner | Owner/Reviewer, Restrisiko, Trigger; keine überhöhte Compliance-Aussage |
| Statistik | statistics task | ein letzter chronologischer Eintrag; Gesamtstatistik bleibt letzter Block |

## Pre-Implementation-Disposition

Die [Checkliste](checklists/pre-implementation.md) ist als Anforderungen- und
Planungsprüfung vollständig disponiert, nicht als Implementierungsnachweis:
CHK001–007 durch Scope/API/Precedence, CHK008–015 durch Bounds/API/Paketvertrag,
CHK016–023 durch Trust Boundaries und Releasezustand, CHK024–028 durch
Plattform/A11Y und CHK029–034 durch Mapping, Gatevertrag, Authority und
Closeout-Grenzen.

## Complexity Tracking

Keine Constitution-Verletzung. `Pl0.ReleaseVerifier` ist kein Produktmodul und
hat keine Produktabhängigkeit; es ist nötig, weil NuGet.org keinen atomaren
Zwei-Paket-Commit anbietet und Exitcode/409 einen Teilrelease nicht sicher
klassifizieren. Die Produktarchitektur behält vier Module und ersetzt zwei
divergierende VM-Loops durch einen kleineren internen Kernel.
