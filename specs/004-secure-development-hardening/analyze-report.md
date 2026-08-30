# Spezifikationsanalyse / Specification Analysis Report

**Feature / Feature**: `specs/004-secure-development-hardening`
**Lauf / Run**: `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7`
**Phase / Phase**: `analyze`
**Datum / Date**: 2026-08-30
**Branch / Branch**: `codex/004-secure-development-hardening`
**Geprüfter Ausgangs-HEAD / Inspected baseline HEAD**:
`8cce89e09ef624e9875d1ca86ea2c878ce8cdd54`
**Ergebnis / Outcome**: `PASS` — keine offenen Critical-, High- oder
Medium-Befunde. / `PASS` — no open Critical, High, or Medium findings.

## 1. Umfang und Autoritätsgrenze / Scope and Authority Boundary

Die Analyse umfasst den bindenden Intake, `spec.md`,
`clarification-report.md`, beide Checklisten, den nach Plan Review geltenden
Plan mit Supporting Artifacts und Contracts, `gate-requirements.json`,
`plan-review.md`, `tasks.md`, den aktuellen Repository-Stand sowie den
autonomen Run-State und seine vorhandene Evidence. Der Intake, das
Serienmanifest und `autonomous-run-state.json` wurden nicht geändert. Es wurde
keine Produkt-, Governance-, Sandbox- oder Folge-Intake-Implementierung
begonnen.

*The analysis covers the binding intake, specification, clarification report,
both checklists, the reviewed plan and supporting artefacts and contracts,
gate requirements, plan review, tasks, current repository reality, and the
autonomous run state and evidence. Intake, series manifest, and run state were
not changed. No product, governance, sandbox, or next-intake implementation was
started.*

Die ausdrücklich erlaubte minimale Remediation blieb auf folgende
Planungsflächen begrenzt:

- `plan.md`, `research.md`, `data-model.md`, `quickstart.md`
- `contracts/evidence-contract.md`
- `gate-requirements.json`
- `tasks.md`
- `checklists/autonomous-readiness.md`

*The explicitly authorised minimal remediation was limited to the planning,
task, gate, contract, and readiness artefacts listed above.*

## 2. Befunde und Auflösung / Findings and Resolution

| ID | Kategorie / Category | Schwere / Severity | Befund / Finding | Auflösung / Resolution | Zustand / State |
|---|---|---|---|---|---|
| C-001 | Ausführbarkeit / Executability | Critical | Der 157-ID-Befehl bildete `$counts` als ein einzelnes Array-Count-Ergebnis und konnte die erwartete Folge `12/13/15/10/13/11/12/13/17/17/12/12` nie bestätigen. Die Sammelbandprüfung war zudem nur mengen-, nicht reihenfolgegebunden. / The 157-ID command collapsed the per-file counts and could never pass; compendium comparison was set-based rather than ordered. | Per-file counts werden einzeln materialisiert; kanonische IDs und Sammelband werden mit typisierten Arrays und `SequenceEqual` verglichen. Der korrigierte Befehl bestand mit 157 eindeutigen IDs. / Materialise each count and compare typed ordered arrays; the corrected command passed. | Resolved |
| H-001 | Aufgabenfolge / Task sequence | High | Mehrere finale Build-/Test-/Versionsschritte erzeugten nacheinander neue Heads. Dadurch konnten nicht alle finalen Nachweise denselben exakten HEAD binden. / Several final build, test, and version steps created successive heads, so all final evidence could not bind one exact head. | T087/T088 sind read-only, T089 erzeugt genau einen nicht-zirkulären finalen Versionscommit, und T090 führt genau einen vollständigen Release-/Coverage-Testaufruf auf diesem sauberen HEAD aus. / T087/T088 are read-only, T089 creates one non-circular final version commit, and T090 performs one full Release/coverage invocation on that clean head. | Resolved |
| H-002 | Phasenbindung / Phase binding | High | T002/T004 und zwei Gates wollten historische Specify-/Plan-/Plan-Review-/Tasks-Payloads erneut als aktuelle Bytes validieren, obwohl erlaubte spätere Phasen diese Artefakte kausal fortgeschrieben hatten. / T002/T004 and two gates attempted to revalidate superseded historical payload bytes as current. | Specify, Plan, Plan Review und Tasks bleiben über ihre unveränderten Run-State-Dateihashes historische Evidence. Clarify und Checklist werden gegen aktuelle Payloads validiert; das aktuelle Analyze-Resultat bindet die letzte minimale Planungs-/Task-Remediation. / Historical result files stay hash-bound; current Clarify/Checklist and the final Analyze result provide causal current binding. | Resolved |
| M-001 | Scope-Konsistenz / Scope consistency | Medium | Die Planungsartefakte nannten uneinheitlich fünf, sechs oder sieben bedingte Pakete und konnten HTTP als siebten Zweig lesen lassen. / Planning artefacts inconsistently described five, six, or seven conditional packages and could imply HTTP as a seventh branch. | Exakt sechs IDs sind jetzt überall bindend; `FND-HTTP-001` ist ausdrücklich nur `Open|FollowUp` und blockiert bei Critical/High. / Exactly six IDs are binding; HTTP remains an out-of-scope finding and blocks on Critical/High. | Resolved |
| M-002 | Autorisierungsgate / Authorisation gate | Medium | Der maschinenlesbare Finding-Befehl prüfte den Status benannter Findings, wies unbekannte Finding-IDs aber nicht selbst zurück. / The machine-readable finding command checked states but did not reject unknown finding IDs. | Eine explizite Allowlist der sechs IDs und ein fail-closed Reject für jede weitere ID wurden in `FINDING-AUTHORIZATION-GATE-001` ergänzt. / Add an explicit six-ID allowlist and fail closed on every other ID. | Resolved |
| M-003 | Evidence-first | Medium | Drei bedingte Pakete waren nicht auf konkrete CL-Zeilen zurückgebunden. / Three conditional packages lacked concrete checklist-row bindings. | `FND-GOV-001` bindet CL-09-13, `FND-GITIGNORE-001` CL-10-07 und `FND-A11Y-001` CL-08-12 plus CL-10-09; T018–T020 verlangen diese Evidence vor Rot oder Edit. / Bind each package to named CL rows before red evidence or edits. | Resolved |
| M-004 | Modulrealität / Module reality | Medium | Der Plan ordnete die VM-Härtung teilweise `Pl0.Core` zu, obwohl die aktuelle Options- und Ausführungsfläche ausschließlich in `Pl0.Vm` liegt. / The plan partly assigned VM hardening to `Pl0.Core`, while the real option and runtime surface is in `Pl0.Vm`. | Modul- und Dateizuordnung wurden auf `Pl0.Vm` plus die benannten Testdateien begrenzt; der vorgeschriebene Abhängigkeitsgraph bleibt unverändert. / Limit the change to `Pl0.Vm` and named tests while preserving the dependency graph. | Resolved |
| M-005 | Bedingte Abhängigkeit / Conditional dependency | Medium | T039 konnte von einem nicht ausgelösten T032-Zweig blockiert werden. / T039 could be blocked by a non-triggered T032 branch. | T039 folgt der abgeschlossenen T032/T033-Disposition, unabhängig davon, ob der SC-Befund editiert oder als Nicht-Trigger belegt wurde. / T039 follows the completed T032/T033 disposition whether edited or evidenced as a non-trigger. | Resolved |

Es bleiben keine offenen Critical-, High- oder Medium-Befunde. Es wurden keine
weiteren nicht blockierenden Duplikate oder unbestimmten Platzhalter gefunden,
die vor Implementierung eine Änderung erfordern.

*No Critical, High, or Medium finding remains open. No other non-blocking
duplication or unresolved placeholder requires a pre-implementation change.*

## 3. Anforderungsabdeckung / Requirement Coverage

Die maschinelle Prüfung fand FR-001 bis FR-028 und SC-001 bis SC-014 jeweils
vollständig und fortlaufend. Alle 42 Anforderungen besitzen mindestens eine
explizite Task-Referenz; kein Task ist ohne User-Story- oder FR/SC-Zuordnung.
Die Tabelle nennt die primären Ausführungsbereiche; weitere Querschnittstasks
bleiben in `tasks.md` explizit referenziert.

*The machine check found all consecutive FR-001..FR-028 and SC-001..SC-014.
All 42 requirements have explicit task coverage, and no task is unmapped. The
table lists primary execution ranges; additional cross-cutting references stay
explicit in `tasks.md`.*

| Anforderung / Requirement | Primäre Tasks / Primary tasks | Abdeckung / Coverage |
|---|---|---|
| FR-001 | T003, T009–T012, T072, T102–T103 | Covered |
| FR-002 | T006, T009–T011, T071, T085, T102–T103 | Covered |
| FR-003 | T021–T027, T060, T074, T092 | Covered |
| FR-004 | T022–T027, T060, T074, T092 | Covered |
| FR-005 | T021, T090, T092 | Covered |
| FR-006 | T047–T061, T087–T090 | Covered |
| FR-007 | T021, T090, T092 | Covered |
| FR-008 | T064–T065, T077, T090, T092 | Covered |
| FR-009 | T082, T090, T092 | Covered |
| FR-010 | T048, T052–T059, T081, T087, T089–T090 | Covered |
| FR-011 | T021–T027, T064, T071, T073 | Covered |
| FR-012 | T016, T031–T033, T062–T063, T097–T098 | Covered |
| FR-013 | T007, T016, T031–T033, T062–T074, T098, T100, T104 | Covered |
| FR-014 | T017, T034–T035, T070–T073, T082 | Covered |
| FR-015 | T066, T071, T073–T074 | Covered |
| FR-016 | T067, T071, T073–T074 | Covered |
| FR-017 | T068, T071, T073–T074 | Covered |
| FR-018 | T009, T015, T028–T030, T099 | Covered |
| FR-019 | T015, T028–T030, T099 | Covered |
| FR-020 | T001, T005, T012, T018, T025–T026, T031, T036–T045, T062, T080, T092 | Covered |
| FR-021 | T013, T047–T061, T088–T092, T101 | Covered |
| FR-022 | T019, T034–T040, T045, T054, T071, T078–T082 | Covered |
| FR-023 | T019, T038–T040, T051, T057, T075–T082, T087, T090, T097–T098 | Covered |
| FR-024 | T020, T041–T042, T089, T093, T095, T101 | Covered |
| FR-025 | T001, T003, T020, T041–T042, T097 | Covered |
| FR-026 | T005, T012, T045, T066–T074, T083–T085, T107–T109 | Covered |
| FR-027 | T001, T004, T006–T008, T013–T014, T037, T044–T046, T061, T080, T086, T092–T110 | Covered |
| FR-028 | T004, T008, T013–T020, T028–T046, T061, T065, T086, T092–T103, T110 | Covered |
| SC-001 | T005, T009–T012, T016, T031, T063, T072 | Covered |
| SC-002 | T003, T011–T014, T043–T046, T071–T074, T100 | Covered |
| SC-003 | T021–T027, T060, T065, T074 | Covered |
| SC-004 | T047–T061, T088, T090 | Covered |
| SC-005 | T048, T052–T061, T081, T087–T090 | Covered |
| SC-006 | T047–T061, T088–T091, T101 | Covered |
| SC-007 | T064–T065, T073, T077, T098 | Covered |
| SC-008 | T016, T031–T033, T062–T063, T097–T098 | Covered |
| SC-009 | T001, T003, T016–T018, T021–T026, T031–T036, T062–T074, T097, T100 | Covered |
| SC-010 | T009, T015, T028–T030, T099 | Covered |
| SC-011 | T017, T019, T034–T040, T077–T082, T097–T098 | Covered |
| SC-012 | T017, T019, T034–T040, T051, T054, T057–T059, T071, T075–T090, T097–T098 | Covered |
| SC-013 | T001, T020, T041–T042, T089, T093, T095, T101 | Covered |
| SC-014 | T001, T004–T008, T012–T013, T018, T036–T046, T065–T074, T080, T085–T110 | Covered |

## 4. Checklisten- und Pfadabdeckung / Checklist and Path Coverage

Der reproduzierbare Inventarbefehl bestand mit zwölf Dateien, den Counts
`12/13/15/10/13/11/12/13/17/17/12/12`, insgesamt 157 IDs, 157 eindeutigen
IDs und identischer kanonischer Reihenfolge im Sammelband. Die 157 IDs sind
kein Freibrief für Änderungen; Assessment, Owner/Reviewer, Red-Evidence und
Finding-Autorisierung liegen zwingend vor jedem bedingten Edit.

*The inventory command passed with twelve files, the declared per-file counts,
157 total and unique IDs, and ordered compendium parity. The inventory is not
change authority; assessment, independent review, red evidence, and finding
authorisation precede every conditional edit.*

| Pfadgruppe / Path group | Taskbereich / Task area | Ergebnis / Result |
|---|---|---|
| 157-Assessment und Findings / assessment and findings | T009–T020, T043–T046, T072 | Exact paths present |
| Architektur, Threat Model, ADR/S-ADR / architecture and threat evidence | T021–T027, T060 | Exact paths present |
| VM-Optionen, Batch, Step und Tests / VM options, batch, step, tests | T047–T061 | Exact paths present |
| Security, ASVS, Dependencies, SBOM/VEX/SLSA | T062–T074 | Exact paths present |
| XML, DocFX, axe, `lynx`, Accessibility | T038–T040, T075–T082 | Exact paths present |
| Baseline-Generator und Plattformparität / baseline generator and platform parity | T015, T028–T030, T099 | Exact paths present |
| Coverage, Golden, Traceability, Statistik / coverage, golden, traceability, statistics | T083–T093 | Exact paths present |
| Delivery, Review, PreMerge, Merge, PostMerge | T094–T110 | Exact repository and temporary paths present |

`tasks.md` enthält exakt T001 bis T110 in fortlaufender Reihenfolge. Die
maschinelle Prüfung meldete null fehlende Tasks, null ungemappte Tasks und null
Anforderungen ohne Task.

*`tasks.md` contains the exact T001..T110 sequence. Machine analysis reported
zero missing tasks, zero unmapped tasks, and zero requirements without a task.*

## 5. Exakt sechs bedingte Pakete / Exactly Six Conditional Packages

| Paket / Package | Bindende Quelle / Binding source | Rot/Disposition / Red or disposition | Möglicher Edit / Possible edit |
|---|---|---|---|
| `FND-BASELINE-001` | BASE-004, CL-10-17 | T015 | T028–T030 |
| `FND-SC-001` | CL-05-01/02/04/11 | T016 | T031–T033 |
| `FND-CVD-001` | CL-06-01/02 | T017 | T034–T035 |
| `FND-GITIGNORE-001` | CL-10-07 | T018 | T036 |
| `FND-A11Y-001` | CL-08-12, CL-10-09, CR-002/FR-023 | T019 | T038–T040 |
| `FND-GOV-001` | CL-09-13 | T020 | T041–T042 |

Die Allowlist im Finding-Gate enthält genau diese sechs IDs. Jede unbekannte
ID scheitert fail-closed. `FND-HTTP-001` und jeder neue Befund bleiben ohne
Edit als `Open|FollowUp`; ein Critical-/High-Befund blockiert.

*The finding gate allowlist contains exactly these six IDs. Every unknown ID
fails closed. `FND-HTTP-001` and every new finding remain no-edit follow-ups;
a Critical or High finding blocks.*

## 6. Gate-Abdeckung / Gate Coverage

`gate-requirements.json` ist schema-gültig, besitzt 31 eindeutige Gate-IDs,
25 `Applicable` und sechs begründete, nicht ausführende `N/A`. Alle 31 IDs sind
mindestens einem Task zugeordnet. Dies belegt die Vollständigkeit und
Ausführbarkeit des Vertrags vor Implementierung; die späteren Produkt-,
Provider-, PreMerge- und PostMerge-Gates sind noch nicht als ausgeführt zu
werten.

*The gate contract is schema-valid with 31 unique IDs: 25 Applicable and six
reasoned, non-executing N/A gates. Every ID maps to tasks. This proves contract
coverage and executability before implementation, not later implementation or
delivery completion.*

| Gate-ID | Anwendbarkeit / Applicability | Primäre Tasks / Primary tasks |
|---|---|---|
| PLAN-GATE-001 | Applicable | T002 |
| PLAN-REVIEW-GATE-001 | Applicable | T002, T004 |
| GATE-REQUIREMENTS-SCHEMA-GATE-001 | Applicable | T003 |
| TASKS-ANALYZE-GATE-001 | Applicable | T001, T004–T005 |
| IDENTITY-INPUT-GATE-001 | Applicable | T001–T002, T086 |
| ASSESSMENT-157-GATE-001 | Applicable | T009–T012, T072 |
| FINDING-AUTHORIZATION-GATE-001 | Applicable | T006, T013–T020, T036–T046, T061, T065, T086, T092 |
| ARCHITECTURE-THREAT-GATE-001 | Applicable | T021–T027, T060 |
| VM-TDD-GATE-001 | Applicable | T048–T055, T058, T061, T088 |
| VM-CONFIGURATION-GATE-001 | Applicable | T048–T057, T061, T088 |
| BUILD-TEST-GOLDEN-GATE-001 | Applicable | T059, T087, T090 |
| COVERAGE-GATE-001 | Applicable | T090–T091 |
| XML-DOC-DOCFX-A11Y-GATE-001 | Applicable | T019, T035, T038–T040, T051, T057, T075–T082, T087, T098 |
| SECURITY-EVIDENCE-GATE-001 | Applicable | T012, T025–T026, T045, T054, T058–T059, T066–T074, T082, T092 |
| ASVS-L1-GATE-001 | Applicable | T064–T065, T073, T077 |
| DEPENDENCY-REVIEW-GATE-001 | Applicable | T031, T040, T062, T073 |
| SUPPLY-CHAIN-SBOM-VEX-SLSA-GATE-001 | Applicable | T016, T032–T033, T063, T098 |
| BASELINE-GENERATOR-PARITY-GATE-001 | Applicable | T009, T015, T028–T030, T099 |
| CVD-SECURITY-TXT-GATE-001 | Applicable | T017, T034–T035, T070 |
| AGENT-PRESET-PARITY-GATE-001 | Applicable | T020, T041–T042 |
| STATISTICS-GATE-001 | Applicable | T083–T084 |
| IDE-VERSION-SERIAL-GATE-001 | Applicable | T047, T049–T050, T055–T057, T086, T089–T090, T093, T095, T101 |
| DELIVERY-EVIDENCE-GATE-001 | Applicable | T005–T006, T037, T046, T080, T093, T101–T103 |
| REMOTE-REVIEW-GATE-001 | Applicable | T007, T094–T104 |
| MERGE-CLOSEOUT-GATE-001 | Applicable | T007, T094, T104–T110 |
| AI-SBOM-GATE-001 | N/A | T069, T073 |
| ZERO-TRUST-GATE-001 | N/A | T068, T073 |
| PRODUCT-CRYPTO-DPIA-GATE-001 | N/A | T069, T073 |
| NIS2-AIACT-DORA-GATE-001 | N/A | T066, T073 |
| PARALLEL-AUTONOMOUS-GATE-001 | N/A | T008, T073, T110 |
| SANDBOX-HARDENING-GATE-001 | N/A | T008, T010, T073, T110 |

## 7. Sicherheits-, Datenschutz-, A11Y- und Architekturprüfung / Security, Privacy, A11Y, and Architecture Review

- NIST SSDF und CWE Top 25 sind immer anwendbar. C#/.NET Secure Coding,
  Trust Boundaries, Defense in Depth, fail-safe Defaults und Least Privilege
  sind in T021–T027, T058, T062–T074 und T092 gebunden.
- OWASP ASVS 5.0.0 Level 1 gilt für den begrenzten `pl0c --api`-Scope. Das
  spätere Gate verlangt 70 exakte L1-IDs und null offene Critical/High.
- SBOM gilt für veröffentlichbare Artefaktsätze; VEX ist fundabhängig; SLSA
  darf nur im tatsächlich belegten Niveau behauptet werden.
- AI-SBOM ist wegen reiner Entwicklungswerkzeug-Nutzung `N/A`; Zero Trust,
  Produktkryptografie, DPIA, NIS2, EU AI Act und DORA bleiben begründet `N/A`
  mit Wiedervorlage-Triggern.
- WCAG 2.2 AA, text-first, Tastatur/Fokus soweit betroffen, DocFX,
  Playwright/axe und ein getrenntes `lynx`-Verfahren sind ausführbar geplant.
- Geänderte Lern-, Nutzer- und Governance-Flächen verlangen Deutsch zuerst,
  Englisch danach und CEFR B2. Öffentliche API-Änderungen verlangen
  vollständige XML-Dokumentation und DocFX-Regeneration mit A11Y-Nachweis.
- Die VM-Ressourcenentscheidung besitzt geplante allgemeine ADR-, S-ADR-,
  arc42-, STRIDE/CIA-, CAPEC- und Qualitätsszenario-Evidence.

*NIST SSDF and CWE Top 25 always apply. The plan also binds C#/.NET secure
coding, architecture and threat evidence, bounded ASVS Level 1, release-set
SBOM and conditional VEX, evidence-limited SLSA, reasoned privacy and AI N/A
decisions, WCAG 2.2 AA and text-first validation, German-first/English-second
B2 content, XML/DocFX proof, and ADR/S-ADR/arc42/STRIDE/CAPEC evidence.*

## 8. Aktuelle Repository- und Toolrealität / Current Repository and Tool Reality

| Fläche / Surface | Beobachtung / Observation | Planbindung / Plan binding |
|---|---|---|
| Modulgraph / module graph | `Pl0.Core` ohne Projektabhängigkeit; `Pl0.Vm -> Pl0.Core`; CLI und IDE -> Core+VM; Tests -> alle vier Module. / Core has no project dependency; VM depends on Core; CLI/IDE on Core+VM; tests on all four. | Unverändert; Architecture Guard bleibt Abschlussgate. / Unchanged; Architecture Guard remains a final gate. |
| VM-Ressourcen / VM resources | `VirtualMachineOptions` besitzt `StackSize=500`, aber kein `InstructionBudget`; beide VM-Wege allokieren `StackSize + 1` vor neuer Vorvalidierung. / No instruction budget; both VM paths allocate before the planned validation. | Genau die zwei vorautorisierten Produktmaßnahmen in T048–T061. / Exactly the two pre-authorised product changes. |
| HTTP | `src/Pl0.Cli/Program.cs` bindet aktuell `http://localhost:5000`. / Current binding is localhost:5000. | Read-only Prüfung; kein HTTP-Produktedit in diesem Lauf. / Read-only review; no HTTP product edit. |
| Docs/A11Y/Supply Chain | Der vorhandene Pages-Workflow besitzt noch keinen verwalteten Node-24-/axe-/separaten `lynx`-Vertrag und keinen vollständigen SBOM-Anschluss. / Current workflow lacks the planned Node 24, axe, separate lynx, and complete SBOM path. | Nur nach den jeweiligen Finding-Gates; lokales Node 26 gilt nicht als Ersatz. / Conditional only; local Node 26 is not a substitute. |
| Lokale Tools / local tools | macOS, PowerShell 7, .NET SDK `10.0.400`, DocFX `2.78.5`, Node `26.7.0`, npm `11.19.0`, Lynx `2.9.3`. | T001/T019/T038 verlangen den verwalteten Node-24-Vertrag für A11Y. / Managed Node 24 remains required. |
| Constitution | `constitution.md` und `.specify/memory/constitution.md` sind bytegleich. / Constitution mirrors are byte-identical. | Keine Constitution-Änderung. / No constitution change. |

Es wurde in dieser Analyze-Phase bewusst kein `dotnet build` und kein
`dotnet test` gestartet. Diese Aufrufe würden nach der Repository-Regel einen
vorherigen IDE-Versions-/Build-Writer verlangen und gehören in die
Implementierungsaufgaben T049/T050/T055–T057 beziehungsweise T089/T090.

*No `dotnet build` or `dotnet test` was started in Analyze. Those invocations
require the governed version/build writer and belong to implementation tasks.*

## 9. IDE-Version und Exact-Head-Konvergenz / IDE Version and Exact-Head Convergence

Der aktuelle Vorimplementierungsstand hat Commitcount `447` und die bestehende
IDE-Version `1.71.446.32`. Das ist Baseline-Evidence, kein finaler Kandidat.
Der kanonische Minor ist der vorläufige PR-Slot `72`, nicht Feature `004`, und
wird in T047, T086 und T095 read-only neu geprüft.

*The pre-implementation baseline has commit count 447 and IDE version
1.71.446.32. This is baseline evidence, not a final candidate. The canonical
Minor is provisional PR slot 72, never feature 004, and is revalidated in
T047, T086, and T095.*

Die nicht-zirkuläre Endfolge lautet:

1. T083–T085 schließen Statistik, Evidence und alle fachlichen getrackten
   Änderungen ab und committen sie.
2. T086–T088 prüfen Identität, Finding-Fläche und frühere TDD-Evidence
   read-only.
3. T089 berechnet `Patch = aktueller Commitcount + 1`, erhöht `Build` einmal,
   setzt alle drei Versionsfelder identisch und erzeugt genau den erwarteten
   Versionscommit. Danach gilt `Patch == git rev-list --count HEAD`.
4. T090 führt auf diesem sauberen HEAD nach `dotnet restore` genau einen
   vollständigen Release-`dotnet test` mit Coverage aus. Es folgt kein
   getrackter Edit.
5. T091–T093 werten Coverage, Sicherheit und Delivery-Set read-only aus.

*T083–T085 freeze and commit domain changes; T086–T088 are read-only; T089
predicts and commits the final Patch/Build value once; T090 runs the single
full Release/coverage invocation on that clean head; T091–T093 are read-only.*

Frühe TDD-Aufrufe T049, T050 und T055–T057 besitzen jeweils ihren eigenen
vorherigen Version-/Build-Commit. Sie sind historische Rot-/Grün-Evidence und
werden durch T088 an den finalen Gesamtlauf gebunden; sie erzeugen keine
unendliche finale Wiederholung. Provider-Checks konsumieren den bereits
versionierten HEAD und sind keine lokalen Version-Writer.

*Each early TDD invocation has its own preceding version/build commit. These
are historical red/green evidence, not a final loop. Provider checks consume
the immutable versioned head and are not local version writers.*

## 10. Delivery, PreMerge und kausales PostMerge / Delivery, PreMerge, and Causal PostMerge

Remote-Autorität beginnt erst mit T094. T102/T103 erzeugen und validieren
temporäre Schema-2.0-`PreMerge`-Evidence für den exakten reviewed PR-Head und
alle 31 Gate-IDs. T105 führt erst nach Checks, unabhängiger Review und
fail-closed Bypass-Entscheidung den Merge aus. T106 belegt Main-Sync. Erst
danach erzeugt T107 neue Schema-2.0-`PostMerge`-Evidence mit tatsächlichem
Merge-Commit; PreMerge wird nicht umetikettiert. T108 setzt die vier
terminalen Closeout-Felder ausschließlich aus dieser kausalen Evidence.

*Remote authority starts at T094. Temporary exact-head PreMerge evidence is
validated before merge. The actual merge and main sync precede newly created
PostMerge evidence; PreMerge is never relabelled. Terminal closeout is derived
only from that causal evidence.*

Der aktive Intake, das Serienmanifest, Sandbox-Härtung und der nächste Intake
bleiben in T008/T110 read-only und dürfen in diesem Lauf weder gestartet noch
geändert werden.

*The active intake, series manifest, sandbox hardening, and next intake remain
read-only and must not be started or changed in this run.*

## 11. Historische und aktuelle autonome Evidence / Historical and Current Autonomous Evidence

Die vier akzeptierten Input-Hashes stimmen bytegenau mit dem Run-State
überein:

| Artefakt / Artefact | SHA-256 |
|---|---|
| Binding intake | `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de` |
| Intake review result | `acdcf2dcb7411be6fa3389cf642748fcb1225e9bcbcf32e6bad8a76da54314fe` |
| Intake review request | `49cddf9ce3391048a12fc4314f1ef2cdf4c500de73956623875a916cde1f3c50` |
| Series manifest | `1ca91db4ec4970c45a7c27b8623d03c29f52c9295305f8ee7d574b23d3f6cadf` |

Die historischen Result-Dateihashes für Specify, Plan, Plan Review und Tasks
stimmen mit dem Run-State überein. Clarify und Checklist wurden zusätzlich
semantisch gegen ihre aktuellen Payloads validiert. Der Run-State-Validator
meldete `Active`, Stage `Analyze`, Phase `Running`, Tasks `0/110`.

Der in `checklists/requirements.md` notierte Spec-Hash
`a447fc6e29fc165b50b8bf0c89e091994008dff52ac5986e3e5c49028272b6cc`
ist die unveränderte Checklist-Zeitpunktbindung vor Plan. Der aktuelle
Spec-Hash `e954207426cca04bce55d26f78fb59cf0a1f03d4afead8d1f8a2e0a00a1e2219`
entstand durch die später ausdrücklich erlaubte Plan-Review-Remediation und
ist in `plan-review.md` sowie durch dieses Analyze-Ergebnis kausal gebunden.
Der historische Checklist-Payload wurde deshalb nicht umgeschrieben.

*The spec hash recorded inside the requirements checklist is the immutable
checklist-time binding before Plan. The current spec hash resulted from later
authorised Plan Review remediation and is causally bound by Plan Review and
Analyze. The historical checklist payload was therefore not rewritten.*

| Phase / Phase | Historischer Result-Dateihash / Historical result-file SHA-256 |
|---|---|
| specify | `b21ced8bf8ab8532e4dd47336f0a9eefea81f3ac3f5628be1bc6ab39f7baea15` |
| clarify | `29eaf4e3f7a7437aa0a85a1a1c006f6700eaf2b3270039f5d34d1c71c52e9217` |
| checklist | `8be14b9083b6f82e2642ece6face940b051e6c6a5fb8a5ac97a6c82063fb6cf8` |
| plan | `7ad616e7c60bbb91b05cc8896d32f409c69d2ca898f09d3283c04515ec5c87ca` |
| plan-review | `e6d8731de740577b2603094b0a1f2eeda824bfb7853dfecbe756e5b239d024cc` |
| tasks | `ea564925f7982f95c27cedaa5cc4cd896a640268645199ae986ef8328cd67002` |

*Historical result files remain immutable evidence. Current Analyze becomes
the causal binding for the final remediated planning and task set.*

## 12. Metriken und Phasenentscheidung / Metrics and Phase Decision

| Metrik / Metric | Wert / Value |
|---|---:|
| Functional Requirements | 28/28 covered |
| Success Criteria | 14/14 covered |
| Gesamtanforderungen / Total requirements | 42/42 covered, 100% |
| Tasks | 110, exact T001..T110 |
| Unmapped tasks | 0 |
| Canonical checklist IDs | 157 total, 157 unique, ordered parity |
| Gates | 31/31 task-mapped |
| Applicable gates | 25 |
| Reasoned N/A gates | 6 |
| Offene Critical / Open Critical | 0 |
| Offene High / Open High | 0 |
| Offene Medium / Open Medium | 0 |
| Analyze-phase tasks | 0 expected, 0 completed |
| Implementation tasks | 0/110 completed; implementation not started |

**Phasenentscheidung / Phase decision**: `Completed`. Die Analyze-spezifischen
Konsistenz-, Coverage-, Autoritäts- und Ausführbarkeitsgates sind vollständig
belegt. Die 31 Implementierungs-/Delivery-Gates bleiben Aufgabe der
Implementierungs- und Lieferphasen und werden durch dieses Analyze-Ergebnis
nicht vorweggenommen.

*Phase decision: `Completed`. Analyze-specific consistency, coverage,
authority, and executability evidence is complete. The 31 implementation and
delivery gates remain pending and are not claimed as executed by this report.*

**Nächste erlaubte Aktion / Next permitted action**: Nach erfolgreicher
semantischer Validierung von `analyze.result.json` darf die bestehende
autonome Implementierungsphase bei T001 fortfahren. Es wurde weder eine
Remote-Aktion noch ein anderer Intake gestartet.

*After semantic validation of `analyze.result.json`, the existing autonomous
implementation phase may continue at T001. No remote action or other intake
was started.*
