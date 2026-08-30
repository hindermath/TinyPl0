# Implementierungsplan: Secure-Development-Härtung / Implementation Plan: Secure Development Hardening

**Branch**: `codex/004-secure-development-hardening` | **Datum / Date**: 2026-08-30 | **Spezifikation / Spec**: [spec.md](spec.md)
**Eingabe / Input**: geklärte Spezifikation, [Klärungsbericht](clarification-report.md) und bestandene [Anforderungscheckliste](checklists/requirements.md) / clarified specification, clarification report, and passing requirements checklist

## Zusammenfassung / Summary

Der Lauf erzeugt zuerst eine auditfähige, zweiachsige Prüfinstanz für alle 157
kanonischen CL-IDs. Eine positive Erfüllungsaussage ist nur zulässig, wenn ihr
Nachweis existiert, zum geprüften Commit gehört und unabhängig reviewbar ist.
Erst danach folgen die zwei bereits autorisierten Produktänderungen: ein
positives, konfigurierbares VM-Instruktionsbudget mit Standard `1_000_000` und
die Diagnose nichtpositiver Budgets sowie ungültiger Stackgrößen vor Allokation
oder Ausführung. Batch- und Step-VM verwenden dieselbe Zähl- und
Diagnosebedeutung.

*The run first creates an audit-ready two-axis assessment for all 157 canonical
CL IDs. A fulfilled claim requires evidence tied to the reviewed commit. Only
then are the two pre-authorised product changes made: a positive configurable
VM instruction budget with a default of `1_000_000`, and diagnostic rejection
of nonpositive budgets and invalid stack sizes before allocation or execution.
Batch and step execution share the same counting and diagnostic semantics.*

Weitere Code-, UI-, CI- oder Workflow-Änderungen gehören nicht zum
bedingungslosen Delivery-Set. Sie dürfen erst nach einem Eintrag im
Befundregister mit CL-ID, `Applicable` plus `Partly Fulfilled` oder
`Not Fulfilled`, Risiko, rotem Test, kleinstem Dateisatz und Regressionsevidenz
in Tasks aufgenommen werden. Die bereits belegten Baseline-, CVD- und
Supply-Chain-Lücken erhalten dafür benannte bedingte Befundpakete; ohne
bestandene Befundschranke bleiben sie Dokumentations- oder Follow-up-Arbeit.

## Technischer Kontext / Technical Context

**Sprache/Version / Language/Version**: C# 14 auf .NET 10 (`net10.0`), Markdown, JSON, XML/RESX, YAML, PowerShell 7 und vorhandene Bash-Wrapper / C# 14 on .NET 10, Markdown, JSON, XML/RESX, YAML, PowerShell 7, and existing Bash wrappers
**Primäre Abhängigkeiten / Primary Dependencies**: .NET SDK 10.0.x, xUnit, Coverlet Collector, Terminal.Gui 2.0.0, DocFX; CycloneDX .NET 6.2.0 nur nach bestandener Dependency-/Lizenzprüfung und befundgebundener Auswahl / CycloneDX .NET only after the dependency and finding gates
**Speicherung / Storage**: dateibasierte Quell-, Test-, Markdown-, JSON-, RESX-, Workflow- und Evidenzartefakte; keine Laufzeitdatenbank / file-based source, test, documentation, workflow, and evidence artefacts; no runtime database
**Tests / Testing**: xUnit, 41-Fälle-Katalog, Golden-/Traceability-Tests, XPlat Code Coverage, PowerShell-Validatoren, DocFX, Playwright/axe und `lynx` bei geänderter HTML-/API-Dokumentation / xUnit, catalogue, golden, traceability, coverage, validators, and accessibility checks
**Zielplattform / Target Platform**: lokale Referenz macOS mit PowerShell 7; bestehende CI auf `ubuntu-latest`; Bash- und PowerShell-Generatorpfade zusätzlich auf macOS/Linux sowie Windows für PowerShell nachweisen / local macOS and GitHub `ubuntu-latest`, with cross-platform generator evidence
**Projekttyp / Project Type**: Compiler, P-Code-Serializer, Stack-VM, CLI, lokaler statischer HTTP-Server und Terminal-IDE / compiler, serializer, stack VM, CLI, local static HTTP server, and terminal IDE
**Leistungsziel / Performance Goal**: höchstens `N` VM-Instruktionen; Abbruch vor `N+1`; Produktstandard `N=1_000_000`; keine wall-clock-Sicherheitsbehauptung / at most `N` instructions and abort before `N+1`
**Einschränkungen / Constraints**: keine neue PL/0-Semantik, kein Secret-Zugriff, keine Sandbox-Härtung, kein Release, keine Intake-/Serien-/Run-State-Änderung; Golden-Artefakte bleiben unverändert, solange kein separat akzeptierter Befund dies verlangt / no language, secret, sandbox, release, governance-state, or unapproved golden change
**Umfang / Scale**: 157 CL-Zeilen, 28 FRs, 14 Erfolgskriterien, zwei VM-Wege, vier Produktprojekte, zwölf Checklistenfamilien und die projektspezifischen Security-/Architektur-/A11Y-/Supply-Chain-Nachweise / 157 assessment rows and the named evidence surfaces

## Constitution Check

*Gate vor Phase 0; nach Phase 1 erneut geprüft. Alle Entscheidungen sind
aufgelöst. / Gate before Phase 0 and rechecked after Phase 1; all decisions are
resolved.*

### Verbindlicher Level-2-Kontext / Binding Level-2 Context

Der Eintrag `RiderProjects/TinyPl0` in `constitution.md` bindet .NET 10/C# 14,
`dotnet restore/build/test`, xUnit, Coverage, Golden-Prüfung, DE-zuerst/EN-
danach, WCAG 2.2 AA-orientierte Textprüfung, Statistikbasen `80` und `125`
Zeilen/Arbeitstag und die gepflegten Agentenflächen. C# ist nach Prinzip XI
eine speichersichere Sprache; MSL ersetzt die sichere I/O-, Ressourcen-,
Fehler-, Dependency- und Lieferkettenprüfung nicht.

### Gates und Entscheidungen / Gates and Decisions

| Prüfpunkt / Checkpoint | Entscheidung / Decision | Plan und Evidenz / Plan and evidence |
|---|---|---|
| Branch und PR | `Pass` | Arbeit bleibt auf `codex/004-secure-development-hardening`; kein Commit/Push in Plan. Die spätere Lieferung folgt ausschließlich dem gespeicherten, an jeder Grenze neu zu validierenden `MergeAndSync`-Vertrag. |
| Toolchain | `Pass` | .NET SDK 10.0.x, C# 14, PowerShell 7 und `ubuntu-latest` bleiben die ausführbaren Plattformtokens. Neue Werkzeuge werden gepinnt und vor Aufnahme auf Quelle, Lizenz und CVE geprüft. |
| Schichten | `Pass` | `Core -> none`, `Vm -> Core`, `Cli/Ide -> Core+Vm`, Tests -> Produktprojekte bleiben unverändert. Das Budget liegt in `Pl0.Vm`; keine neue Modulabhängigkeit. |
| Allgemeine Architektur | `Applicable` | Das Budget ändert Runtime-Verhalten und das Quality Attribute Ressourcenbegrenzung. `docs/ARCHITECTURE.md`, `docs/architecture/secure-development-hardening.md` und `docs/architecture/adr/0001-vm-resource-budget.md` dokumentieren Kontext, Runtime-Sicht, Qualitätsfälle, Risiko und Trade-off. Deployment bleibt unverändert. |
| Sichere Architektur | `Applicable` | Source/P-Code/CLI/File/IDE/HTTP sind Trust Boundaries. `docs/security/threat-model.md`, `arc42-security.md`, S-ADR `0001-vm-resource-budget.md` und Szenarien verwenden STRIDE/CIA sowie CAPEC für hohe Wege. Defense in depth = Vorvalidierung plus Laufzeitbudget. |
| Secure Coding | `Applicable` | NIST SSDF und CWE Top 25 gelten immer. C#-Review prüft Eingaben, numerische Grenzen, Allokation vor Validierung, kontrollierte Diagnosen, keine internen Details, keine unsichere Deserialisierung und keine neue Secret-/Auth-/Crypto-Fläche. |
| Standards | `Applicable` | ASVS 5.0.0 L1 für `pl0c --api`; SBOM/VEX/SLSA/Scorecard für veröffentlichbare Pages-/Release-Flächen; SAMM, CRA, C3A/C5 und CAPEC als Evidenz. AI-SBOM, Zero Trust, NIS2, EU AI Act, DORA, Produktkrypto und DPIA bleiben begründet `N/A` mit Trigger. |
| 157-ID-Schranke | `Applicable` | Zehn Pflichtspalten plus Zusatzfelder werden gegen ein JSON-Schema geprüft. Genau 157 eindeutige IDs, darunter CL-12 genau zwölfmal als `N/A`/`Not Assessed`, sind Pflicht. |
| TDD | `Applicable` | Repräsentativer Rot-Grün-Schnitt: unveränderter Test für Budget `N=2` scheitert zunächst, danach bestehen Batch und Step exakt an `N`/vor `N+1`; separate rote Fälle prüfen Budget `0`, `-1` und Stackgrößen `0..2` vor Allokation. |
| Coverage | `Applicable` | Gesamtlinie `>=70%` und nicht unter `70,23%`; `>=80%` bleibt Ziel. Geänderte sicherheitskritische VM-Flächen `>=85%` Branch; KI-erzeugter geänderter Code zusätzlich `>=80%` Linie und Branch. |
| XML/DocFX | `Applicable` | `VirtualMachineOptions` erhält einen öffentlichen Parameter und vollständige DE-/EN-XML-Dokumentation. Deshalb DocFX, repräsentative axe-Prüfung und `lynx` im selben Arbeitsgegenstand. Keine CS1591-Unterdrückung. |
| A11Y und Lernende | `Applicable` | Geänderte Diagnosen und Lerntexte bleiben text-first, ohne Farbcodierung, DE zuerst/EN danach auf CEFR B2. CLI-/IDE-UX ändert sich nur nach Befund; dann gelten Tastatur-, Fokus- und statusverständliche Tests. |
| Abhängigkeiten | `Applicable` | Direkte/transitive NuGet-Pakete, veraltete/vulnerable Pakete, Lizenzen, Quellen und fehlende Lockfiles werden erfasst. Kritische/hohe Funde blockieren; Pinning-Ausnahmen brauchen Owner und Trigger. |
| Supply Chain | `Applicable` | Lokale CycloneDX-Erzeugung und JSON-Validierung werden geplant. Eine CI-/Workflow-Erweiterung ist nur mit `FND-SC-001`; geplante Actions müssen auf vollständige Commit-SHAs gepinnt sein. VEX entsteht nur bei bekanntem Fund. SLSA-Ziel ist mindestens Build L1, langfristig L2. |
| Baseline-Generator | `Applicable`, befundgebunden | `FND-BASELINE-001` muss CL-10-17 und BASE-004 bestätigen. Dann bilden PowerShell-Engine, Bash-Wrapper, DE-/EN-Hilfe, `Build-SecureDevelopmentDocs`, `-WhatIf`/`--dry-run`, `-Check`/`--check` und Manpage eine atomare Einheit. |
| Agentenparität | `Applicable` als Review | Gemeinsame Guidance wird nicht vorab geändert. Fünf Agentenflächen, Constitution-Spiegel und Templates werden semantisch geprüft; eine Änderung wäre nur mit `FND-GOV-001` und atomarem Dateisatz zulässig. |
| Statistik und Version | `Applicable` | `docs/project-statistics.md` erhält genau einen letzten chronologischen Eintrag. GitHub zeigte am 2026-08-30 read-only PR `#71` als höchste vergebene Nummer; der vorläufige nächste Slot und damit der vorläufige IDE-`Minor` ist `72`, nicht Feature `004`. Der Slot wird vor dem ersten Versionierungscommit und unmittelbar vor PR-Erzeugung erneut read-only geprüft. `Patch` entspricht am jeweiligen versionierten Build-/Test-Commit `git rev-list --count HEAD`; `Build` wird vor jedem einzelnen Build/Test erhöht. Evidence-, Versions- und Statistikdateien haben jeweils genau einen serialisierten Writer. |
| Parallel Autonomous | `N/A` | Keine Kampagne oder Delegation. Wiedervorlage nur bei ausdrücklicher Kampagnenautorität. |
| Security-first | `Pass` | Keine `.codex`-Credentials, Logs, History, SQLite-State, Secrets oder private Endpunktdaten werden gelesen oder getrackt. |
| Dokumentationsauswirkung | `UpdateRequired` | Quellen: Spec, Constitution, Secure-Development-Basis und projektspezifische Nachweise. Owner: TinyPl0-Maintainer; Reviewer getrennt. Navigation: `README.md` und `docs/security/README.md`. Home-Sync `false`, solange keine gemeinsame Regel geändert wird. |

## Architektur- und Bedrohungsentscheidungen / Architecture and Threat Decisions

1. `VirtualMachineOptions` bleibt der einzige öffentliche Konfigurationsvertrag.
   `InstructionBudget` wird als positiver `int` mit Standard `1_000_000`
   ergänzt. Ein größerer Typ ist unnötig, weil die Produktgrenze weit unter
   `int.MaxValue` liegt und die Zählung pro VM-Lauf neu beginnt.
2. Ein interner, gemeinsamer Validierungsvertrag akzeptiert nur
   `3 <= StackSize <= 1_000_000` und liefert eine stabile `VmDiagnostic`, bevor
   `new int[StackSize + 1]`, Registerinitialisierung oder
   Instruktionsausführung stattfindet. Die obere Grenze verhindert sowohl den
   `StackSize + 1`-Überlauf bei `int.MaxValue` als auch eine unbeschränkte
   Speicherallokation. Batch liefert ein fehlerhaftes
   `VmExecutionResult`; Step-VM setzt einen terminalen Error-State, ohne Stack
   zu allokieren. Keine Konfigurationsausnahme gelangt zur Nutzerfläche.
3. Eine Instruktion zählt genau dann, wenn sie nach erfolgreicher
   Pointer-/Budgetprüfung zur Ausführung ausgewählt wird. Bei bereits `N`
   ausgeführten Instruktionen wird vor Auswahl/Ausführung von `N+1` dieselbe
   lokalisierbare Budgetdiagnose erzeugt. `Step()` verbraucht pro erfolgreichem
   Aufruf genau eine Einheit.
4. Das Budget ist Defense-in-Depth gegen CWE-400/CWE-770-artigen
   Ressourcenverbrauch; es ersetzt weder Stack-/Indexprüfungen noch
   Betriebssystemgrenzen. Die Grenze ist deterministisch und keine
   Zeitgarantie.
5. Der lokale HTTP-Server, Datei-I/O und Lieferkettenpfade werden vollständig
   bewertet, aber nicht ohne Befund geändert. ASVS-, STRIDE- und CAPEC-Evidenz
   darf keine nicht getestete Härtung behaupten.

## Evidenz-zuerst- und Änderungsautorisierung / Evidence-First Change Authorisation

Die Implementierung beginnt mit `assessment.json` und dem lesbaren Index. Für
jede mögliche Änderung außerhalb der zwei VM-Punkte ist folgendes Paket vor dem
ersten Edit Pflicht:

| Feld | Pflichtwert / Required value |
|---|---|
| `findingId` | stabil, z. B. `FND-SC-001` |
| Checkpoint | konkrete CL-ID aus der 157er Matrix |
| Status | `Applicable` plus `Partly Fulfilled` oder `Not Fulfilled` |
| Risiko | Asset, Missbrauch, Auswirkung, Schwere und Restrisiko |
| Rot | exakter Test/Validator, erwarteter Fehler und unveränderte Testquelle |
| Kleinste Maßnahme | exakte Dateien und warum kein kleinerer Satz genügt |
| Grün/Regression | unveränderter Test grün plus relevante Gesamtregression |
| Owner/Reviewer | getrennte Rollen; kritische/hohe Akzeptanz nur Maintainer |

Genau sechs vorbenannte, noch zu bestätigende und in diesem Lauf maximal
autorisierbare Pakete:

- `FND-BASELINE-001`: BASE-004 + CL-10-17; Manifest-/Generatorparität.
- `FND-SC-001`: CL-05-01/02/04/11; reproduzierbarer SBOM-, Lizenz- und
  Provenienzanschluss für den tatsächlich veröffentlichten Pages-/Release-Satz.
- `FND-CVD-001`: CL-06-01/02; auffindbare CVD-Richtlinie und `security.txt`.
- `FND-GOV-001`: CL-09-13; konkrete semantische Agenten-/Template-Abweichung;
  reine
  Versions- oder Standardsentscheidung genügt nicht.
- `FND-GITIGNORE-001`: CL-10-07; die aktuelle `.gitignore`
  besitzt nicht das von Constitution-Prinzip I verlangte deny-by-default-
  Wurzelmodell. Der Befund darf nur Dateinamen und synthetische Sentinelpfade
  prüfen, niemals Secret-Inhalte oder private Agentenzustände lesen.
- `FND-A11Y-001`: CL-08-12 und CL-10-09 plus CR-002/FR-023; der vorhandene Pages-Workflow
  baut und smoketestet DocFX, besitzt aber keinen ausführbaren
  Playwright/axe- und `lynx`-Vertrag für die durch die öffentliche VM-API-
  Änderung zwingend betroffenen Seiten.

Die sechs bereits aus der Spec-Baseline und dem aktuellen Repository-Stand
ableitbaren bedingten Pakete besitzen
vorab folgenden ausführbaren Rahmen. Sie werden erst `Authorised`, wenn die
genannten CL-Zeilen in `assessment.json` den erforderlichen Status bestätigen:

| Finding | Befund und Risiko / Finding and risk | Rot / Red | Kleinster exakter Fix / Smallest exact fix | Grün und Regression / Green and regression |
|---|---|---|---|---|
| `FND-BASELINE-001` | BASE-004, erwartet CL-10-17 `Applicable`/`Not Fulfilled`: veraltete Versionen und fehlender Generator können eine manuell driftende Prüfbasis als aktuell erscheinen lassen; Integritäts- und Auditrisiko `High`. | PowerShell-Validator vergleicht Manifestversionen mit Richtlinie/CL-09/CL-12/Sammelband, verlangt beide dokumentierten Skripte und endet im Ausgangsstand ausschließlich wegen Version-/Dateidrift nonzero. | `docs/secure-development/baseline-manifest.json`, `scripts/build-secure-development-docs.ps1`, `scripts/build-secure-development-docs.sh`, `docs/man/build-secure-development-docs.1.md`, `.github/workflows/powershell-analysis.yml` für den exakten macOS-/Linux-/Windows-Nachweis. | Derselbe Validator grün; PowerShell/Bash Check und Dry-run, 157 IDs/Reihenfolge/Output-Hash gleich; gepinnte Remote-Matrix auf macOS/Linux/Windows; keine direkte Sammelbandänderung. |
| `FND-SC-001` | Erwartet CL-05-01/02/04/11 `Applicable`/`Not Fulfilled`: veröffentlichte Pages-/Release-Artefakte besitzen kein zugeordnetes maschinenlesbares Inventar und keine belegte Herkunft; Supply-Chain-/CVE-/Lizenzrisiko `High`. | PowerShell-Validator verlangt gepinntes CycloneDX-Tool, SBOM-Datei plus Artefakt-Hash, VEX-Triggerstatus, SLSA-Ist/Ziel und volle Action-SHAs in den zwei Lieferworkflows; Ausgangsstand scheitert an den fehlenden Nachweisen. | `.config/dotnet-tools.json` und `.github/workflows/docs-pages.yml`; `.github/workflows/release-please.yml` wird nur read-only auf den bereits vorhandenen vollständigen Action-SHA geprüft. | Derselbe Validator grün; lokales SBOM-JSON validiert; Dependency-/Lizenzreview; Pages-Build/Upload-Smoke; Release-Please-Pin read-only bestätigt; VEX nur bei Fund; Provenienzclaim nicht höher als Evidence. |
| `FND-CVD-001` | Erwartet CL-06-01/02 `Applicable`/`Not Fulfilled`: Meldende finden keinen verbindlichen Kontakt/Prozess und Meldungen können verspätet oder unsicher behandelt werden; Disclosure-Risiko `High`. | PowerShell-Validator verlangt `.github/SECURITY.md`, RFC-9116-Pflichtfelder und veröffentlichbaren `.well-known`-Pfad; Ausgangsstand scheitert nur an den fehlenden Artefakten. | `.github/SECURITY.md`, `docfx/.well-known/security.txt`, `docfx.json`; Navigation in `docs/security/README.md` ist bereits Teil des unbedingten Dokumentationssets. | Derselbe Validator grün; DocFX/Pages-Smoke, Linkcheck, axe/`lynx`, DE-/EN-B2-Review und Ablaufdatumprüfung. |
| `FND-GITIGNORE-001` | Aktuelle `.gitignore` beginnt nicht mit den bindenden Root-Deny-Regeln aus Prinzip I; versehentlich neue Dateien können standardmäßig trackbar werden. Veröffentlichungs- und Secret-Risiko `High`. | Read-only PowerShell-Validator verlangt wirksame `/*`-/`/.*`-Deny-Regeln und prüft synthetische Credential-/Agent-State-Pfade mit `git check-ignore`; Ausgangsstand scheitert, ohne Dateiinhalt zu lesen. | Nur `.gitignore`; bestehende getrackte Pfade werden aus `git ls-files` abgeleitet und explizit freigehalten. Keine Secret-, Scanner- oder Hook-Datei wird gelesen oder geändert. | Derselbe Validator grün; jeder getrackte Pfad bleibt sichtbar, alle synthetischen Sentinelpfade bleiben ignoriert, `git status --short` zeigt keine unbeabsichtigt verschwundene Delivery-Fläche. |
| `FND-A11Y-001` | Die öffentliche Options-API löst DocFX/A11Y aus, aber der aktuelle Workflow besitzt keinen axe-/Textbrowser-Test. A11Y- und Abschlussrisiko `High`. | Read-only Inventarvalidator verlangt einen gepinnten Node-24-/Playwright-/axe-Vertrag und scheitert im Ausgangsstand an der fehlenden ausführbaren Prüffläche. | `tests/a11y/package.json`, `tests/a11y/package-lock.json`, `tests/a11y/docfx-a11y.spec.mjs` und `.github/workflows/docs-pages.yml`; keine globale Installation und kein ungepinnter `latest`-Download. | `npm ci` mit geprüftem Lockfile; DocFX auf Loopback; drei benannte Seiten mit axe ohne Critical/Serious und ohne neue andere Verletzung; zwei API-Seiten mit `lynx`; der Harness beendet den Server sicher; Dependency-/Lizenz-/CVE-Nachweis und Remote-Log auf `ubuntu-latest` liegen vor. |
| `FND-GOV-001` | Der Nutzer hat die vorhandene Guidance-Auslegung ausdrücklich korrigiert: `Minor` ist die kanonische GitHub-PR-Nummer, nicht Feature `004`. Eine fortbestehende Feature-Nummer-Regel erzeugt falsche Versionen; Auditrisiko `Medium`. | Semantischer Paritätsvalidator sucht die alte Feature-/Branch-Nummer-Auslegung in allen gepflegten Agentenflächen und Templates und endet im Ausgangsstand nonzero. | Die fünf gepflegten Agentenflächen plus tatsächlich betroffene `scripts/templates/*`; Constitution-Spiegel nur, falls dort dieselbe Regel ergänzt oder geändert wird. | Paritätsvalidator grün; alle Flächen nennen PR-Slot-Revalidierung, HEAD-Commitcount für `Patch` und den Vor-jedem-Build/Test-Zähler; keine Feature-`004`-Ableitung bleibt. |

`FND-HTTP-001` ist ausdrücklich **kein siebtes autorisierbares Paket**. Ein
HTTP-Befund wird nur als `Open` oder `FollowUp` mit Owner, Priorität, Trigger
und Evidenzziel erfasst; ein Critical-/High-Befund blockiert den Abschluss.
Für `FND-GOV-001` ist nur die ausdrücklich verlangte Versionsregel als exakte
bedingte Fläche bestimmt. Weitere reale Befunde erweitern die sechs Pakete in
diesem Lauf nicht.

Ohne bestätigtes Paket werden keine Dateien in `.github/workflows/`,
`src/Pl0.Cli`, `src/Pl0.Ide`, `src/Pl0.Core` oder Agenten-/Constitution-Flächen
geändert.

## Anforderungs- und Gate-Zuordnung / Requirement and Gate Mapping

Die spätere Tasks-Phase MUSS jede Zeile in pfadgenaue, abhängigkeitsgeordnete
Aufgaben zerlegen. Eine Gruppe erlaubt keine pauschale Änderung: Für alle
befundgebundenen Flächen gilt weiterhin der einzelne rote Validator und der
kleinste Dateisatz aus der Autorisierungstabelle.

| Anforderungen / Requirements | Planpaket und primäre Dateien / Plan package and primary files | Abschlussgate / Completion gate |
|---|---|---|
| FR-001–FR-004; SC-001–SC-003 | 157-ID-Assessment, Threat Model, arc42, Qualitäts-Szenarien, ADR/S-ADR; `docs/security/secure-development/2026-08-30-tinypl0-hardening/`, `docs/security/`, `docs/architecture/` | `ASSESSMENT-157-GATE-001`, `ARCHITECTURE-THREAT-GATE-001` |
| FR-005–FR-010; SC-004–SC-006 | Compiler-/P-Code-/VM-/CLI-/Datei-/HTTP-Inventar; verbindliche VM-Scheibe ausschließlich in `Pl0.Vm` und den benannten VM-/L10N-Testklassen; andere Produktflächen nur nach eigenem Finding | `FINDING-AUTHORIZATION-GATE-001`, `VM-TDD-GATE-001`, `VM-CONFIGURATION-GATE-001`, `BUILD-TEST-GOLDEN-GATE-001`, `COVERAGE-GATE-001` |
| FR-011–FR-017; SC-007–SC-009 | Security-Dokumente, ASVS-L1, Dependency Review, SBOM/VEX/SLSA, CVD sowie regulatorische Anwendbarkeit; JSON-Evidence plus text-first Sichten | `SECURITY-EVIDENCE-GATE-001`, `ASVS-L1-GATE-001`, `DEPENDENCY-REVIEW-GATE-001`, `SUPPLY-CHAIN-SBOM-VEX-SLSA-GATE-001`, `CVD-SECURITY-TXT-GATE-001` und begründete N/A-Gates |
| FR-018–FR-020; SC-010 | Baseline-Manifest/Generator nur nach `FND-BASELINE-001`; AI-SBOM bleibt mit Trigger `N/A` | `BASELINE-GENERATOR-PARITY-GATE-001`, `AI-SBOM-GATE-001` |
| FR-021–FR-023; SC-011–SC-012 | Rot-Grün-Hashes, API-Kompatibilität/XML-Docs, DE-zuerst/EN-danach B2, DocFX, axe, `lynx`; A11Y-Harness nur nach `FND-A11Y-001` | `VM-TDD-GATE-001`, `XML-DOC-DOCFX-A11Y-GATE-001` |
| FR-024–FR-028; SC-013–SC-014 | fünf Agentenflächen atomar nur nach `FND-GOV-001`, Preset-/Constitution-Parität, priorisierte Ergebnisübersicht, serialisierte Statistik/Version/Evidence, exakte Delivery-Fläche | `AGENT-PRESET-PARITY-GATE-001`, `STATISTICS-GATE-001`, `IDE-VERSION-SERIAL-GATE-001`, `DELIVERY-EVIDENCE-GATE-001`, `REMOTE-REVIEW-GATE-001` |

*The Tasks phase must turn every row into path-exact, dependency-ordered work.
Grouping does not grant blanket edit authority: every conditional surface still
requires its own red validator and the smallest file set declared by the
finding authorisation table.*

## Phasen / Phases

### Phase 0 — Forschung und Bestandsbindung / Research and Baseline Binding

1. Branch, Run-ID, akzeptierte Hashes, Featurepfad, Presetmatrix und Level-2-
   Registry read-only bestätigen.
2. Zwölf Checklisten und Sammelband maschinell auf 157 eindeutige, identische
   IDs und Reihenfolge prüfen; Versionen aus Dokumenten gegen Manifest binden.
3. Code, Tests, Stubs, Workflows, DocFX-/A11Y-, Dependency-, Release- und
   Statistikpfade inventarisieren; keine Erfüllung aus bloßer Dateiexistenz
   ableiten.
4. Entscheidungen in [research.md](research.md) und die Befundschranke im
   [Evidenzvertrag](contracts/evidence-contract.md) festhalten.

### Phase 1 — Design und Verträge / Design and Contracts

1. [data-model.md](data-model.md) bindet AssessmentRow, Finding,
   EvidenceReference, VmExecutionPolicy, RiskRecord, GateEvidence und
   SerialWriterLease.
2. `contracts/assessment-record.schema.json` validiert die 157-ID-Matrix;
   `contracts/vm-hardening-contract.md` bindet Budget-/Optionssemantik;
   `contracts/evidence-contract.md` bindet Gates, Befunde und Writer.
3. [quickstart.md](quickstart.md) legt Reihenfolge, Rot-Grün-Schnitt, exakte
   Kommandos und erwartete Signale fest.
4. [gate-requirements.json](gate-requirements.json) erklärt alle Delivery-Gates
   mit stabilen IDs, `Applicable`/`N/A`, Befehls- und Plattformtokens.

### Phase 2 — spätere Tasks, nicht Teil von Plan / Later Tasks, Not This Phase

`/speckit.tasks` zerlegt die Reihenfolge in: Revalidierung → 157-ID-Evidenz →
Threat/Architektur → bestätigte Befundpakete → VM-Rot → minimale VM-Änderung →
VM-Grün/Regression → Security-/ASVS-/Supply-Chain-/A11Y-Nachweise → Coverage →
serialisierte Version/Statistik/Evidence → Delivery-Gates. Kein
Implementierungsedit beginnt vor bestandenem Plan Review, Tasks und Analyze.

## Repräsentativer Rot-Grün-Vertikalschnitt / Representative Red-Green Vertical Slice

1. In `tests/Pl0.Tests/VirtualMachineTests.cs` und
   `SteppableVirtualMachineTests.cs` dieselbe Schleife `[Jmp 0 0]` und
   `InstructionBudget: 2` verwenden. Options-Grenztests umfassen zusätzlich
   `StackSize=1_000_001` und `int.MaxValue`; kein Fall darf vor der Diagnose
   ein Array anlegen oder `StackSize + 1` auswerten. Ein Kompatibilitätstest
   verwendet weiterhin den bisherigen Vier-Parameter-Konstruktor; der neue
   Budgetparameter wird deshalb am Ende der Positionsparameter angehängt.
2. Rot: `VirtualMachineOptions` besitzt noch kein Budget; der neue Test kann
   nicht kompilieren oder die Schleife endet nicht. Für die ausführbare rote
   Evidence wird der Budgetvertrag zuerst testseitig ergänzt und der selektive
   Test mit kontrolliertem externem Timeout ausgeführt; nur das erwartete
   Budgetassertion-/Timeoutsignal zählt als Rot.
3. Minimal Grün: Optionsfeld, gemeinsame Vorvalidierung, Zähler in beiden
   VM-Wegen und zwei lokalisierte Diagnose-Ressourcen ergänzen. Keine CLI-/IDE-
   Option und keine PL/0-Semantik ändern.
4. Grenzbeweis: bei `N=2` werden genau zwei Instruktionen ausgeführt; der
   nächste Versuch liefert denselben Code und semantisch denselben DE-/EN-Text.
   `N=0`, `N=-1`, `StackSize=0`, `1`, `2` liefern vor Allokation/Ausführung
   Konfigurationsdiagnosen.
5. Regression: beide Testklassen, L10N, vollständige Suite, 41 Pflichtfälle,
   Golden-/Traceability-Prüfung, Coverage und DocFX/A11Y.

## Projektstruktur und exakte Dateiflächen / Project Structure and Exact File Surfaces

```text
specs/004-secure-development-hardening/
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── gate-requirements.json
└── contracts/
    ├── assessment-record.schema.json
    ├── evidence-contract.md
    └── vm-hardening-contract.md

src/Pl0.Vm/
├── VirtualMachineOptions.cs
├── VirtualMachine.cs
├── SteppableVirtualMachine.cs
└── Resources/
    ├── Pl0VmMessages.resx
    └── Pl0VmMessages.en.resx

tests/Pl0.Tests/
├── VirtualMachineTests.cs
├── SteppableVirtualMachineTests.cs
└── L10nTests.cs

docs/security/
├── README.md
├── secure-development/2026-08-30-tinypl0-hardening/
│   ├── README.md
│   ├── assessment.json
│   ├── findings.md
│   └── residual-risks.md
├── threat-model.md
├── arc42-security.md
├── security-checklist.md
├── security-quality-scenarios.md
├── dependency-audit.md
├── asvs-verification.md
├── asvs-verification.json
├── supply-chain-evidence.md
├── supply-chain-evidence.json
├── zero-trust-applicability.md
├── samm-assessment.md
├── cra-applicability.md
├── regulatory-applicability.md
├── cloud-autonomy-applicability.md
├── cloud-compliance-assurance.md
└── adr/0001-vm-resource-budget.md

docs/architecture/
├── secure-development-hardening.md
└── adr/0001-vm-resource-budget.md

docs/accessibility/secure-development-hardening.md
docs/ARCHITECTURE.md
docs/TRACEABILITY_MATRIX.md
docs/VM_INSTRUCTION_SET.md
docs/project-statistics.md
src/Pl0.Ide/Pl0.Ide.csproj

tests/a11y/                         # nur nach FND-A11Y-001 / only after finding
├── package.json
├── package-lock.json
└── docfx-a11y.spec.mjs
```

Die vorstehende Liste ist das bedingungslose Planungs- und VM-Delivery-Set.
Folgende Dateien werden nur nach dem jeweils benannten Befundpaket ergänzt oder
geändert:

- `FND-BASELINE-001`: `docs/secure-development/baseline-manifest.json`,
  `scripts/build-secure-development-docs.ps1`,
  `scripts/build-secure-development-docs.sh`,
  `docs/man/build-secure-development-docs.1.md` und
  `.github/workflows/powershell-analysis.yml` für die gepinnte
  macOS-/Linux-/Windows-Parität.
- `FND-SC-001`: `.config/dotnet-tools.json`, `.github/workflows/docs-pages.yml`
  sowie ausschließlich bei einem eigenen belegten Release-Artefakt-Bedarf
  weitere Releaseflächen. `.github/workflows/release-please.yml` ist aktuell
  bereits auf einen vollständigen Action-SHA gepinnt und bleibt read-only;
  dieser Lauf plant und erzeugt keinen Release. Kein Workflow wird ohne
  vorherigen Red-Validator editiert.
- `FND-CVD-001`: `.github/SECURITY.md`, `docfx/.well-known/security.txt` und
  `docfx.json`.
- `FND-HTTP-001`: exakt die durch den roten Test belegten Dateien aus
  `src/Pl0.Cli/Program.cs` und `tests/Pl0.Tests/`; nicht pauschal freigegeben.
- `FND-GOV-001`: alle fünf Agentenflächen plus betroffene Constitution-
  Spiegel/Templates atomar; nicht pauschal freigegeben.
- `FND-GITIGNORE-001`: ausschließlich `.gitignore` mit abgeleiteten
  Allowlist-Regeln für bereits getrackte Wurzelpfade.
- `FND-A11Y-001`: `tests/a11y/package.json`,
  `tests/a11y/package-lock.json`, `tests/a11y/docfx-a11y.spec.mjs` und
  `.github/workflows/docs-pages.yml`.

## Serialisierte Writer / Serialized Writers

| Gemeinsames Artefakt / Shared artefact | Einziger Writer / Single writer | Reihenfolge / Ordering |
|---|---|---|
| Assessment, Findings, Gate-Ledger | Evidence writer task | JSON atomar in Temp-Datei validieren, dann ersetzen; danach Markdown-Ableitungen, niemals parallele Appends. |
| `src/Pl0.Ide/Pl0.Ide.csproj` | Version writer task | Direkt vor jedem einzelnen Build/Test alle drei Felder gemeinsam erhöhen; kein paralleler Build/Test. |
| `docs/project-statistics.md` | Statistics writer task | Nach finaler Inventur genau einen chronologischen Eintrag, dann Renderer und `-CheckOnly -Json`; `## Gesamtstatistik` bleibt letzter Block. |
| Secure-Development-Sammelband | Generator writer, falls `FND-BASELINE-001` besteht | Nur aus zwölf kanonischen Dateien erzeugen; nie direkt editieren; PowerShell und Bash teilen Checksum-/Reihenfolgevertrag. |
| Workflows und Navigation | jeweils ein benannter Task | Kein paralleles Schreiben; danach YAML-/Link-/A11Y-Validatoren. |

## Coverage-, Dependency- und Release-Grenzen / Coverage, Dependency, and Release Boundaries

- Der Coverage-Floor `70%` und die Nichtabsenkung unter `70,23%` sind
  blockierend; das Ziel `80%` wird getrennt als `TargetMet` oder `TargetOpen`
  berichtet. Eine offene Ziellücke benötigt Owner, Trigger und Evidenzziel.
- VM-Klassen werden getrennt nach Linie und Branch ausgewertet. Geänderte
  sicherheitskritische Flächen müssen `>=85%` Branch erreichen; der KI-
  Mindestwert `>=80%` Linie/Branch ist zusätzlich, nicht ersetzend.
- `dotnet package list ... --vulnerable --include-transitive` und
  `--outdated` sind read-only. Eine neue Toolabhängigkeit wird erst nach
  Quellen-, Lizenz-, Wartungs- und CVE-Prüfung gepinnt.
- Ein lokal erzeugtes CycloneDX-JSON wird schema-/inhaltlich validiert und dem
  tatsächlichen `_site`-/Release-Artefakt-Hash zugeordnet. CI-Upload ist kein
  Release. VEX ist nur bei bekanntem Fund ein Datensatz; ohne Fund wird der
  nachvollziehbare Triggerstatus dokumentiert.
- SLSA wird nicht überbehauptet: vorhandene GitHub-Builds plus dokumentierte
  Herkunft begründen höchstens den nachgewiesenen Stand. Eine geplante
  Attestation benötigt minimale `id-token`, `attestations` und `contents`
  Permissions sowie eine auf vollen SHA gepinnte Action.

Der IDE-Versionspfad verwendet keine Feature-Nummer. Der am 2026-08-30
read-only beobachtete nächste GitHub-Slot ist vorläufig `72`. Solange keine PR
existiert, ist dies eine Reservierungsannahme und kein Providerclaim. Vor dem
ersten Versionierungscommit sowie direkt vor PR-Erzeugung wird die höchste
vergebene PR-Nummer erneut gelesen. Eine Kollision aktualisiert `Minor` und
erzwingt eine vollständige finale Build-/Testwiederholung. Jeder vom Bot lokal
gestartete Build/Test läuft auf einem zuvor erzeugten lokalen Commit: `Patch`
ist dann exakt `git rev-list --count HEAD`, und `Build` ist gegenüber dem
vorherigen Aufruf erhöht. Der finale Kandidat verwendet genau **einen**
versionierten `dotnet test`-Aufruf in Release-Konfiguration; dieser baut die
Solution, führt die vollständige Suite einschließlich VM-, L10N-, Katalog-,
Golden- und Traceability-Tests aus und erzeugt Coverage. So bindet alle finale
lokale Build-/Test-Evidence denselben exakten HEAD. Provider-Checks sind
immutable Verbraucher dieses bereits versionierten Heads und keine lokalen
Version-Writer; ein nachträglicher Versionsedit wäre ungültig.

## Complexity Tracking

Keine Constitution-Verletzung wird akzeptiert. Die Zahl der Evidenzartefakte
entsteht aus den bindenden Level-2-Standards und ersetzt keine neue
Produktarchitektur. Bedingte Workflow-/Generatorflächen bleiben hinter der
Befundschranke; dadurch wird der Plan nicht zu einer pauschalen Tool- oder
Pipeline-Aufrüstung.
