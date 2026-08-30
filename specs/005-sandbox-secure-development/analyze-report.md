# Spezifikationsanalyse / Specification Analysis Report

**Feature / Feature**: `specs/005-sandbox-secure-development`
**Lauf / Run**: `91e9fb51-2e69-4eab-85b7-cb28ec23749d`
**Phase / Phase**: `analyze`
**Datum / Date**: 2026-08-30
**Branch / Branch**: `codex/005-sandbox-secure-development`
**Geprüfter Ausgangs-HEAD / Inspected baseline HEAD**:
`24612a34dd04cfc42cb02df9d675fe6a93dfe716`
**Ergebnis / Outcome**: `PASS` — keine offenen Critical-, High- oder
Medium-Befunde. / `PASS` — no open Critical, High, or Medium findings.

## 1. Umfang und Autoritätsgrenze / Scope and Authority Boundary

Die Analyse umfasst den bindenden Intake, die aktuelle Ready-Serienreview,
`spec.md`, Klärungsbericht, beide Checklisten, den nach Plan Review geltenden
Plan, Research, Datenmodell, Quickstart, Verträge, Gate-Anforderungen,
`tasks.md`, Run-State und bisherige Evidence. Intake, Serie, Produktcode,
Sandbox-Repository, Sandbox-Image und bestehende `docs/security/`-Dateien
wurden nicht geändert. Die einzige Remediation liegt in `tasks.md`.

*The analysis covers the binding intake, current Ready series review,
specification, clarification, checklists, reviewed plan, supporting design
artefacts, contracts, gate requirements, tasks, run state, and existing
evidence. It changed neither intake and series data nor product, Sandbox, or
existing `docs/security/` content. The only remediation is in `tasks.md`.*

Der einmalige Standard-Prerequisite-Check suchte wegen der bekannten
`codex/`-Branchpfad-Auslegung irrtümlich unter
`specs/codex/005-sandbox-secure-development`. Er wurde nicht wiederholt. Die
explizite Feature-Auswahl in `.specify/feature.json`, Run-State und
Phasenauftrag bindet stattdessen den vorhandenen Pfad
`specs/005-sandbox-secure-development`.

*The one standard prerequisite check hit the known `codex/` branch-path issue.
It was not retried; the explicit feature selector, run state, and phase request
bind the existing feature path.*

## 2. Befunde und Auflösung / Findings and Resolution

| ID | Kategorie / Category | Schwere / Severity | Befund / Finding | Auflösung / Resolution | Zustand / State |
|---|---|---|---|---|---|
| M-001 | Anforderungsabdeckung / Requirement coverage | Medium | `SC-002` zur vollständigen Standards- und Governance-Abdeckung besaß keine explizite Task-Referenz. / `SC-002` lacked an explicit task reference. | T046 bindet `SC-002` an den ausführbaren Standards-/Preset-Validator. / T046 now binds it to the executable standards and preset validator. | Resolved |
| M-002 | Gate-Traceability / Gate traceability | Medium | Alle zwölf lesbaren SBX-Gates waren abgedeckt, die 13 exakten IDs aus `gate-requirements.json` jedoch nicht einzeln als primäre Task-Zuordnung benannt. / The readable gates were covered, but the 13 exact machine gate IDs lacked one primary task mapping each. | T002, T006, T043–T048, T050–T051, T053, T060 und T066 nennen jede maschinenlesbare Gate-ID genau einmal. / The named tasks now map every machine gate ID exactly once. | Resolved |
| H-001 | Kandidatenscan / Candidate scan | High | Der private-path-Check las bei jeder geänderten getrackten Datei den vollständigen Inhalt. Dadurch blockierte eine unveränderte historische Pfadzeile in `docs/project-statistics.md` den sonst sauberen Kandidaten. / The private-path check read the full content of every changed tracked file, so an unchanged historical path line blocked an otherwise clean candidate. | Der Gate scannt bei getrackten Dateien nur hinzugefügte Diffzeilen und bei neuen untracked Dateien den vollständigen Inhalt. Der zur Laufzeit zusammengesetzte Regex bleibt self-safe; ein absichtlicher neuer Sentinelpfad scheitert, der aktuelle Kandidat besteht. / Scan only added diff lines for tracked files and full content for new files; retain the self-safe runtime regex and prove positive/negative cases. | Resolved |
| H-002 | Statistik-Scope / Statistics scope | High | Das bindende Statistikgebot verlangt eine fortgeschriebene Phasenkurve, während die Scope-Allowlist nur `docs/project-statistics.md`, nicht deren versionierte Renderer-Konfiguration zuließ. / The statistics policy requires an updated phase curve, while the scope allowlist permitted only the ledger, not its versioned renderer configuration. | Spec, Plan, Vertrag, Task T052/T055 und Scope-Gate erlauben genau `docs/project-statistics.config.json` für den neuen renderergeprüften Phasenslot; keine weitere Governance- oder Produktfläche wird geöffnet. / Permit exactly the renderer configuration for the new validated phase slot across all artefacts, without opening any other governance or product surface. | Resolved |

Es bleiben keine offenen Critical-, High- oder Medium-Befunde. Die Analyse fand
keine zusätzliche Scope-Erweiterung, kein stilles technisches
Härtungspaket und keinen unbestimmten Platzhalter, der vor Implementierung
behoben werden muss.

*No Critical, High, or Medium finding remains. The analysis found no scope
expansion, silent technical hardening package, or unresolved placeholder that
must be fixed before implementation.*

## 3. Anforderungs- und Story-Abdeckung / Requirement and Story Coverage

Die maschinelle Prüfung fand exakt T001 bis T069 in fortlaufender Reihenfolge,
ohne `[P]`-Task. FR-001 bis FR-017, CR-001 bis CR-005 und SC-001 bis SC-007
besitzen jeweils mindestens eine Task-Referenz. Alle Tasks gehören genau einer
der drei User Stories an.

*The machine check found the exact consecutive sequence T001..T069 without a
parallel task. Every FR-001..FR-017, CR-001..CR-005, and SC-001..SC-007 has at
least one task reference. Every task belongs to exactly one user story.*

| Bereich / Area | Primäre Taskbereiche / Primary task ranges | Ergebnis / Result |
|---|---|---|
| US1 sichere Nutzungsentscheidung | T001–T023, T043–T062 | Covered |
| US2 verständlicher Arbeitsort | T024–T034, T045–T046 | Covered |
| US3 auditfähige Folgearbeit | T035–T042, T063–T069 | Covered |
| 17 funktionale Anforderungen | Verteilte Trace-Hinweise in T001–T069 | 17/17 covered |
| 5 Verfassungsanforderungen | T018, T021, T032–T033, T046–T053 | 5/5 covered |
| 7 Erfolgskriterien | T006, T022–T023, T034–T052 | 7/7 covered |

Die Intake-Akzeptanz bleibt erhalten: die drei zentralen Spec-Kit-Artefakte
nennen Sandbox-Anwendbarkeit und Evidence; der Vorgänger wird über sein
byte-identisches Archiv und die aktuelle Review-Pfadauflösung bewahrt; private
Pfade und Secrets sind ausgeschlossen; die Nutzungsentscheidung bleibt
nachvollziehbar; `N/A` verlangt Begründung und Trigger.

## 4. CL-12-, Standards- und Gate-Abdeckung / CL-12, Standards, and Gate Coverage

- CL-12-01 bis CL-12-12 sind in T010–T021 einzeln und kanonisch geordnet.
- Die Pflichtfelder aus `contracts/sandbox-assessment-contract.md` sind in
  T009 und T043–T044 gebunden.
- Mounts, Schreibrechte und private Nachbarbereiche liegen in T011, T022,
  T036, T045 und T051.
- Alle geforderten Arbeitsarten liegen in T024–T034 und T045.
- NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC, SBOM/VEX/SLSA/OpenSSF, SAMM und
  begründete N/A-Standards liegen in T031–T033, T039 und T046.
- Die acht installierten Presets liegen in T021 und T046; Parallelisierung
  wird nicht als Befugnis verwendet.

`gate-requirements.json` ist schema-gültig und enthält 13 eindeutige Gates:
zwölf `Applicable` und ein begründetes `N/A`. Jede exakte Gate-ID erscheint in
`tasks.md` genau einmal als primäre Zuordnung; die zwölf lesbaren Akzeptanzgates
`SBX-G001` bis `SBX-G012` sind ebenfalls vollständig abgedeckt.

| Maschinen-Gate / Machine gate | Primärer Task / Primary task |
|---|---|
| `SBX-IDENTITY-GATE-001` | T002 |
| `SBX-REFERENCE-GATE-002` | T006 |
| `SBX-DOCUMENT-REDGREEN-GATE-003` | T043 |
| `SBX-CL12-GATE-004` | T044 |
| `SBX-BOUNDARY-GATE-005` | T045 |
| `SBX-STANDARDS-GATE-006` | T046 |
| `SBX-SCOPE-GATE-007` | T050 |
| `SBX-SECRET-PATH-GATE-008` | T051 |
| `SBX-A11Y-GATE-009` | T047 |
| `SBX-PRODUCT-BUILD-GATE-010` | T048 |
| `SBX-VERSION-DELIVERY-GATE-011` | T053 |
| `SBX-REMOTE-REVIEW-GATE-012` | T060 |
| `SBX-CLOSEOUT-GATE-013` | T066 |

## 5. Sicherheits-, Architektur- und A11Y-Konsistenz / Security, Architecture, and A11Y Consistency

Die Artefakte unterscheiden konsistent zwischen einer vorhandenen technischen
Beschreibung, einer tatsächlich ausgeführten TinyPl0-Baseline und einer
menschlichen Betriebsfreigabe. Der stabile Sandbox-Commit ist nur
Beobachtungsbasis. Nicht übernommene Änderungen und der konkrete private
Checkout-Pfad bleiben ausgeschlossen.

*The artefacts consistently distinguish an available technical description,
an actually executed TinyPl0 baseline, and human operating approval. The stable
Sandbox commit is observation evidence only; uncommitted work and the concrete
private checkout path remain excluded.*

Die geplante Entscheidung ist konsistent: reguläre oder autonome
TinyPl0-Schreibarbeit ist `Not Ready`. Ein späterer Read/Build/Test-Pilot bleibt
`Conditional/Open`, bis exakte Image-Identität, menschliche Freigabe, minimaler
Mount, Secret-Trennung, aktuelle Egress-Entscheidung und reale Baseline-Evidence
vorliegen. Commit, Push, PR und Merge bleiben beim autorisierten
TinyPl0-Orchestrator.

Produktarchitektur, API, XML/DocFX, Produkt-TDD/Coverage, Script-Parität,
Agent-Guidance und bestehende Security-Dokumente sind begründet `N/A` oder
read-only. Die Feature-Dokumente selbst bleiben DE-zuerst/EN-danach, ungefähr
CEFR B2, semantisch und text-first gemäß anwendbarer WCAG-2.2-AA-Basis.

## 6. Ausführbarkeit und Lieferreihenfolge / Executability and Delivery Order

Die Reihenfolge ist vollständig seriell:

1. Identität und Dokumentvertrag-Rot.
2. CL-12-Entscheidung und Grenzen.
3. Arbeitsort und Supply-Chain-Trennung.
4. Follow-ups und Evidence-Matrix.
5. Dokumentvertrag-Grün, lokale Gates, Statistik und Version.
6. Exact-Head-PR, technische Checks, null offene Threads und echte unabhängige
   `APPROVED`-Review.
7. Autorisierter Merge/Sync und erst danach der kausale Serien-Closeout.

Admin-Bypass kann nur nach vollständiger technischer Evidence und Approval eine
verbleibende Branch-Policy-Schranke behandeln. Er ersetzt weder Technik noch
Review. T063–T069 starten keinen Folge-Intake; der nächste Serienstatus wird nur
ermittelt.

## 7. Reproduzierbare Analyseergebnisse / Reproducible Analysis Results

| Prüfung / Check | Ergebnis / Result |
|---|---|
| Tasks-IDs | 69 fortlaufend, 0 fehlend, 0 parallel |
| FR-/CR-/SC-Trace | 17/17, 5/5, 7/7 |
| CL-12-Trace | 12/12 |
| Lesbare SBX-Gates | 12/12 |
| Maschinenlesbare Gates | 13/13, je genau eine primäre Task-Zuordnung |
| Gate-Schema | Pass, Draft 2020-12; H-001-Remediation revalidiert |
| Private Pfade in `tasks.md` | 0 |
| Whitespace-Diff | Pass |
| Offene Critical/High/Medium-Befunde | 0/0/0 |

Der Tasks-Phasenresultat-Hash bindet den vor der Analyze-Remediation erzeugten
Tasks-Stand als historische Evidence. Der aktuelle `tasks.md`-Hash wird im
Run-State fortgeschrieben; das Analyze-Resultat bindet die vier minimalen,
aufgelösten Traceability-/Kandidatenscan-Änderungen kausal.

*The Tasks phase result preserves the pre-analysis task payload as historical
evidence. Run state tracks the current task hash, while the Analyze result
causally binds the four minimal resolved traceability, candidate-scan, and
statistics-scope
changes.*
