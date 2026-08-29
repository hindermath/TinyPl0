# Autonome Bereitschaftscheckliste / Autonomous Readiness Checklist

**Zweck / Purpose**: Verbindliche Schranken vor Planung, Implementierung und
Lieferung für den aktiven autonomen Lauf festlegen. / Define binding gates
before planning, implementation, and delivery for the active autonomous run.

**Erstellt / Created**: 2026-08-29
**Feature / Feature**: [spec.md](../spec.md)
**Lauf / Run**: `064927e0-8389-4692-a53c-f1ce79e6043d`,
`codex/003-constitution-change`, `MergeAndSync`
**Quellen / Sources**: `spec.md`, `requirements.md` (27/27 bestanden / passed),
`clarification-report.md` (keine materielle Unklarheit / no material ambiguity),
autonomes Preset `0.4.1` / autonomous preset `0.4.1`

`[x]` bedeutet: Evidenz ist an dieser Phasengrenze vollständig. `[ ]` bedeutet:
Die Schranke ist für eine spätere Grenze erklärt und bleibt bis dahin offen;
dies ist kein Fehler der Checklist-Phase. / `[x]` means evidence is complete at
this phase boundary. `[ ]` means the gate is declared for a later boundary and
remains open until then; this is not a checklist-phase defect.

## Aktuelle Phasenbereitschaft / Current Phase Readiness

- [x] CHK001 Stimmen Feature, Branch, Run-ID, Liefermodus und die enge
  Phasenautorität überein? Diese Phase erlaubt nur diese Checkliste und ihr
  strukturiertes Ergebnis. / Do feature, branch, run ID, delivery mode, and
  narrow phase authority agree? This phase permits only this checklist and its
  structured result. [Scope, Spec §Autonomous-Run-Anwendbarkeit]
- [x] CHK002 Stimmen die vier akzeptierten normalisierten Eingabe-Hashes exakt
  mit dem validen Run-Zustand überein? / Do the four accepted normalized input
  hashes exactly match the valid run state? [Evidence, Spec §SPEC-GATE-001]
- [x] CHK003 Sind Spezifikation, 27/27-Anforderungscheck und Klärungsbericht
  vollständig, text-first, DE zuerst/EN danach, CEFR B2 und ohne offene
  materielle Frage? / Are the specification, 27/27 requirements check, and
  clarification report complete, text-first, German-first/English-second,
  CEFR B2, and free of material open questions? [Completeness, Spec
  §SPEC-GATE-002–003]
- [x] CHK004 Bleiben Scope, Nicht-Ziele, Intake-Reihenfolge und die Regel „nur
  `Applicable` planen“ unverändert? / Do scope, non-goals, intake order, and the
  “plan only `Applicable`” rule remain unchanged? [Consistency, Spec §FR-009]
- [x] CHK005 Sind `specify` und `clarify` durch strukturierte Ergebnisse mit
  passender Phasen-ID, vollständigen Aufgaben, erfüllten Gates und exaktem
  Payload-Hash belegt? / Are `specify` and `clarify` evidenced by structured
  results with matching phase ID, complete tasks, satisfied gates, and exact
  payload hash? [Evidence, Autonomous readiness]
- [x] CHK006 Sind NIST SSDF und CWE Top 25 anwendbar sowie alle bedingten
  Sicherheits-, Architektur-, A11Y- und Plattformstandards entweder anwendbar
  oder mit Begründung und Wiedervorlage erfasst? / Are NIST SSDF and CWE Top 25
  applicable, with every conditional security, architecture, accessibility,
  and platform standard either applicable or recorded with rationale and
  re-evaluation? [Coverage, Spec §Governance-Anwendbarkeit]
- [x] CHK007 Ist der nächste erlaubte Schritt ausschließlich `/speckit.plan`,
  ohne Implementierung, Git-/Remote-Aktion oder Start eines weiteren Intakes? /
  Is the only permitted next step `/speckit.plan`, without implementation,
  Git/remote action, or another intake? [Ordering, Spec §Reihenfolge]

## Schranken vor Implementierung / Gates Before Implementation

- [x] CHK008 Sind `plan.md` und der Plan-Review abgeschlossen, ohne offene
  Critical-/High-Findings und mit aufgelösten oder namentlich verantworteten
  Medium-Findings? / Are `plan.md` and plan review complete, with no open
  Critical/High finding and every Medium resolved or assigned to a named
  owner? Evidence: `plan.result.json` payload
  `5c48873e62ce82bd0b9f0cdb4c1faae0ca1f51e133a3e7b23a5ec4b051ff801f`;
  `plan-review.result.json` payload
  `36fabbd09de29558745af7bfd4b129a3abd952db1c05cf1cdaf7bd91d35d9a6`;
  validator exit 0. [Future gate, Autonomous readiness]
- [x] CHK009 Ist `tasks.md` abhängigkeitsgeordnet und nennt es vor dem ersten
  Edit genaue Pfade für Evidenz, Run-Zustand, Gate-Anforderungen, Delivery-Set,
  PreMerge/PostMerge und Retrospektive? / Is `tasks.md` dependency-ordered and,
  before the first edit, does it name exact paths for evidence, run state, gate
  requirements, delivery set, PreMerge/PostMerge, and retrospective? [Future
  gate, Autonomous task rules] Evidence: `tasks.result.json`, payload
  `dd94fe367f9e084907d3e54c225378f381f6098cd042764a96872032d72e5966`,
  73 eindeutige Aufgaben, validator exit 0. / 73 unique tasks, validator exit 0.
- [x] CHK010 Hat Analyze keine Critical-/High-Findings und nur behobene oder
  akzeptierte Medium-Findings mit Owner? / Does Analyze have no Critical/High
  finding and only resolved or accepted Medium findings with an owner? [Future
  gate, Autonomous convergence] Evidence: `analyze.result.json`, payload
  `2aa58cb9bce2e98a9594919405d7c14aa3ae652797dd62991030d2e03acbffc9`,
  null offene Critical-, High- oder Medium-Befunde, validator exit 0. / Zero
  unresolved Critical, High, or Medium findings, validator exit 0.
- [x] CHK011 Existieren vor dem ersten Implementierungs-Edit das Evidenz-Ledger
  und das geprüfte Gate-Anforderungs-JSON mit stabilen IDs, Scope und exakten
  Befehls-/Runner-Tokens? / Before the first implementation edit, do the
  evidence ledger and reviewed gate-requirements JSON exist with stable IDs,
  scope, and exact command/runner tokens? Evidence: `autonomous-run-evidence.md`;
  `gate-requirements.json` normalized SHA-256
  `c49b0d0327d6b1596cee2b0e12d5b333ab837032c37e0e7da7a8af25bf3ced68`;
  Schema 1.0, 31 eindeutige IDs, 20 `Applicable`, 11 begründete `N/A`.
  [Future gate, Autonomous proof]
- [x] CHK012 Ist jede abgeschlossene Routing-Phase validiert und mit Profil,
  Ausführungsmetadaten, Ergebnis und normalisiertem SHA-256 im Run-Zustand
  gebunden? / Is every completed routed phase validated and bound in run state
  to profile, execution metadata, result, and normalized SHA-256? [Future gate,
  Autonomous phase result] Evidence: `autonomous-run-state.json`; validierte
  Resultat-Hashes `aa87ba96...3dd8`, `1877d2f8...7c40`,
  `02269087...e464`, `f61ecbe5...ea6`, `cbd89ff3...15f1`,
  `cfb4cf54...e6a1` und `7e7bfe66...ce19`. / All seven predecessor results
  validate with exit 0.
- [x] CHK013 Legen Plan und Tasks den vollständigen Build-/Ausführungs-Scope,
  eine repräsentative vertikale Scheibe sowie Rot-, Grün- und
  Regressionsevidenz fest, oder ein prüfbares TDD-`N/A`, falls die Umsetzung
  rein text-/governancebezogen bleibt? / Do plan and tasks define the complete
  build/execution surface, one representative vertical slice, and red, green,
  and regression evidence, or a reviewable TDD `N/A` if implementation remains
  text/governance-only? Evidence: `plan.md` TDD slice and `tasks.md` T030-T040
  bind the four product projects, one unchanged guard from red to green, the
  XML inventory, and the refactor review. [Future gate, Spec §FR-004]

## Schranken vor Lieferung / Gates Before Delivery

- [x] CHK014 Sind alle Aufgaben abgeschlossen oder bedingt belegt und sind
  FR-001 bis FR-009 sowie SC-001 bis SC-007 vollständig nachgewiesen? / Are all
  tasks complete or conditionally evidenced, with FR-001 through FR-009 and
  SC-001 through SC-007 fully proven? [Delivery gate, Spec §Messbare Ergebnisse]
- [x] CHK015 Bestehen Restore, Build und vollständige Testsuite mit aktiver
  öffentlicher XML-Dokumentationsschranke, ohne globale/projektweite
  CS1591-Unterdrückung und ohne fatalen Fehlerkanal? / Do restore, build, and
  the full test suite pass with the public XML-documentation gate active, no
  global/project-wide CS1591 suppression, and no fatal error channel? [Delivery
  gate, Spec §FR-002] Evidence: final Release build 0 warnings/0 errors;
  qualifying rebuilt test `1.3.433.26`, 266/266 passed; no CS1591 suppression.
- [x] CHK016 Liegen bei jeder API-/XML-Änderung erfolgreicher DocFX-Aufbau,
  Playwright/axe-Prüfung und `lynx`-Textprüfung aus demselben Arbeitsgegenstand
  vor; andernfalls ist der Nicht-Trigger am finalen Diff belegt? / For every
  API/XML change, do successful DocFX, Playwright/axe, and `lynx` text-review
  evidence exist from the same work item; otherwise, is the non-trigger proven
  from the final diff? [Delivery gate, Spec §FR-005] Evidence: DocFX 0 errors;
  Node 24 with pinned Playwright/axe reports three `violations: []`; three
  named UTF-8 Lynx dumps are non-empty and meaningful.
- [x] CHK017 Sind Constitution-Spiegel, alle gepflegten Agentenflächen,
  betroffene Templates, ausführbare Acht-Preset-Matrix und Projektstatistik
  atomar sowie semantisch konsistent? / Are the constitution mirror, all
  maintained agent surfaces, affected templates, executable eight-preset
  matrix, and project statistics atomically and semantically consistent?
  [Delivery gate, Spec §FR-006–008] Evidence: byte-identical constitution
  pair; eight standard and four optional non-conflicting presets; clean
  candidate homogeneity score 100 and statistics JSON `CURRENT`.
- [x] CHK018 Besteht der explizite Delivery-Set-Nachweis einschließlich aller
  beabsichtigten untracked Dateien, `git diff --cached --check` oder lokaler
  nicht-mutierender Entsprechung, Statusabgleich, Versionsregel und Erhalt
  fremder Arbeit? / Does explicit delivery-set evidence cover every intended
  untracked file, `git diff --cached --check` or a non-mutating local
  equivalent, status reconciliation, version rule, and preservation of
  unrelated work? [Delivery gate, Autonomous candidate integrity]
- [x] CHK019 Bindet temporäre Schema-2.0-`PreMerge`-Evidenz den akzeptierten
  Anforderungs-Hash und exakten geprüften Head, mit genau einer Primary-Zeile je
  Gate und erfolgreichem installiertem Validator? / Does temporary schema-2.0
  `PreMerge` evidence bind the accepted requirements hash and exact reviewed
  head, with exactly one Primary row per gate and a passing installed validator?
  [Delivery gate, Autonomous gate evidence]
- [x] CHK020 Sind erforderliche Checks grün, alle ausführbaren Befehle und
  Runner aus Workflow/Logs abgeleitet, alle Review-Threads erledigt und kein
  fehlender Review oder Bypass als Erfolg gewertet? / Are required checks green,
  executed commands and runners derived from workflow/logs, all review threads
  resolved, and no missing review or bypass counted as success? [Delivery gate,
  Autonomous remote convergence]
- [x] CHK021 Sind Merge, Cleanup, Default-Branch-Synchronisierung,
  schema-2.0-`PostMerge`-Evidenz und alle vier schema-1.1-Closeout-Felder
  kausal und terminal belegt, bevor der Gesamtlauf `Completed` wird? / Are merge,
  cleanup, default-branch synchronization, schema-2.0 `PostMerge` evidence, and
  all four schema-1.1 closeout fields causally and terminally proven before the
  overall run becomes `Completed`? [Delivery gate, Autonomous closeout]

## Verbindlicher Gate-Vertrag / Binding Gate Contract

| Gate-ID | Grenze / Boundary | Status jetzt / Status now | Erforderliche Evidenz / Required evidence | Erforderliche Tokens / Required tokens |
|---|---|---|---|---|
| `READY-GATE-001` | Checklist-Abschluss / checklist completion | `Applicable`, erfüllt / satisfied | Akzeptierte Hashes, valider Run-Zustand, 27/27, keine materielle Unklarheit, gültige Phasenergebnisse. / Accepted hashes, valid run state, 27/27, no material ambiguity, valid phase results. | `pwsh`, `validate-autonomous-run-state.ps1`, `validate-autonomous-phase-result.ps1`, macOS |
| `READY-GATE-002` | Vor Tasks / before tasks | `Applicable`, erfüllt / satisfied | Akzeptierter Plan und Plan-Review ohne Critical/High. / Accepted plan and plan review without Critical/High. | `speckit.plan`, `speckit.analyze` |
| `READY-GATE-003` | Vor erstem Implementierungs-Edit / before first implementation edit | `Applicable`, erfüllt / satisfied | Abhängigkeitsgeordnete Tasks, Analyze-Konvergenz, vorab angelegte Evidenz und Gate-Anforderungen. / Dependency-ordered tasks, Analyze convergence, pre-created evidence and gate requirements. | `speckit.tasks`, `speckit.analyze`, `validate-autonomous-run-state.ps1` |
| `DELIVERY-GATE-001` | Vor lokaler Abnahme / before local acceptance | `Applicable`, erfüllt / satisfied | FR-001–FR-009 und SC-001–SC-007; sichere .NET-/CWE-Prüfung. / FR-001–FR-009 and SC-001–SC-007; secure .NET/CWE review. | `dotnet restore`, `dotnet build`, `dotnet test` |
| `DELIVERY-GATE-002` | Bei API/XML-Trigger, sonst final begründetes `N/A` / on API/XML trigger, otherwise final reasoned `N/A` | `Applicable`, erfüllt / satisfied | DocFX plus textorientierter A11Y-Nachweis desselben Arbeitsgegenstands. / DocFX plus text-oriented accessibility evidence from the same work item. | `docfx docfx.json`, `Playwright`, `@axe-core/playwright`, `lynx`, Node 24 LTS |
| `DELIVERY-GATE-003` | Vor Delivery-Kandidat / before delivery candidate | `Applicable`, erfüllt auf sauberer Kandidatenkopie / satisfied on clean candidate copy | Atomare Agenten-/Constitution-/Template-Parität, Acht-Preset-Abgleich und fortgeschriebene Statistik. / Atomic agent/constitution/template parity, eight-preset reconciliation, and updated statistics. | `pwsh -NoProfile -File scripts/check-homogeneity.ps1 -Json`, `pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly` |
| `DELIVERY-GATE-004` | Vor Commit/Push / before commit/push | `Applicable`, erfüllt / satisfied | Explizites unverändertes Delivery-Set, exakter Kandidat, Status- und Versionsabgleich. / Explicit immutable delivery set, exact candidate, status and version reconciliation. | `validate-autonomous-delivery-set.ps1`, `git diff --cached --check`, `git status --short` |
| `DELIVERY-GATE-005` | Vor Merge / before merge | `Applicable`, erfüllt mit dokumentierter Nutzer-Override-Autoritaet / satisfied with documented user override authority | Temporäre exakter-Head-`PreMerge`-Evidenz, Gate-Mapping, Checks und Review-Konvergenz. / Temporary exact-head `PreMerge` evidence, gate mapping, checks, and review convergence. | `validate-autonomous-gate-evidence.ps1`, `gh pr checks`, `gh pr view` |
| `DELIVERY-GATE-006` | Merge und Abschluss / merge and closeout | `Applicable`, erfüllt / satisfied | Richtlinienkonformer Merge, Sync, PostMerge-Bindung, Retrospektive und terminale Closeout-Felder. / Policy-compliant merge, sync, PostMerge binding, retrospective, and terminal closeout fields. | `gh pr merge`, `gh repo sync --branch main`, `validate-autonomous-run-state.ps1` |

## Begründete N/A-Entscheidungen / Reasoned N/A Decisions

| ID | Entscheidung und Begründung / Decision and rationale | Wiedervorlage / Re-evaluation trigger |
|---|---|---|
| `NAR-001` | OWASP ASVS ist `N/A`: kein Web-, API-, HTTP- oder Auth-Scope. / OWASP ASVS is `N/A`: no web, API, HTTP, or authentication scope. | Neuer Web-/API-/HTTP-/Auth-Scope. / New web/API/HTTP/auth scope. |
| `NAR-002` | SBOM, VEX, SLSA, OpenSSF Scorecard, AI-SBOM sowie NIS2/CRA/EU AI Act/DORA sind `N/A`: keine neue Abhängigkeit, Release-/Provenienzänderung, bekannte CVE, KI-Runtime oder regulierte Leistung. / These controls are `N/A`: no new dependency, release/provenance change, known CVE, product AI runtime, or regulated service. | Abhängigkeit, Release, CVE, Pipeline/Provenienz, Produkt-KI oder regulierter Scope. / Dependency, release, CVE, pipeline/provenance, product AI, or regulated scope. |
| `NAR-003` | STRIDE/CIA, CAPEC, S-ADR, arc42 Security/allgemeine Architektur, Zero Trust, SAMM und BSI C3A/C5 sind `N/A`: keine Laufzeit-, Datenfluss-, Trust-Boundary-, Cloud- oder Strukturänderung. / These architecture controls are `N/A`: no runtime, data-flow, trust-boundary, cloud, or structural change. | Externer Input, Datei-/Netzwerkfluss, Privileg, Deployment, Cloud oder Strukturänderung. / External input, file/network flow, privilege, deployment, cloud, or structural change. |
| `NAR-004` | Skript-/Cmdlet-Parität ist `N/A`: kein Repository-Automationsskript, Cmdlet, Workflow-Helper oder keine Manpage wird geändert; browserseitige DocFX-A11Y-Logik wird separat unter JavaScript/A11Y geprüft. / Script/cmdlet parity is `N/A`: no repository automation, cmdlet, workflow helper, or man page changes; browser-side DocFX A11Y is reviewed separately. | Jede entsprechende Automations- oder Manpage-Änderung. / Any matching automation or man-page change. |
| `NAR-005` | TDD darf nur dann `N/A` sein, wenn der finale Scope rein aus Text/Governance ohne Produktlogik besteht. / TDD may be `N/A` only if final scope is text/governance-only with no product logic. | Neue Funktion, Fehlerkorrektur oder geänderte nicht-triviale Logik; dann Rot → Grün → Regression. / Feature, fix, or changed non-trivial logic; then red → green → regression. |
| `NAR-006` | Home-Sync ist `N/A`: repository-lokaler `sourceOnly`-Inhalt ohne Home-Runtime-Vertrag. / Home sync is `N/A`: repository-local `sourceOnly` content without a home-runtime contract. | Neuer Home-Distribution-/Runtime-Vertrag. / New home distribution/runtime contract. |
| `NAR-007` | Runner-/Delivery-Schemaänderung, mutable Provider-Tokens und kausaler Closeout sind für diese Checklist-Phase `N/A`; es gibt keine Semantikänderung oder Remote-Operation. / Runner/delivery-schema change, mutable provider tokens, and causal closeout are `N/A` for this checklist phase; there is no semantics change or remote operation. | Änderung autonomer Semantik oder Beginn einer Provider-, Review-, Merge- oder Sync-Phase. / Autonomous-semantics change or start of a provider, review, merge, or sync phase. |
| `NAR-008` | Didaktische Inline-Kommentare und ein neues `docs/accessibility/`-Artefakt sind aktuell `N/A`: keine nicht-triviale Logik und kein neuer UI-/Bedienfluss. DocFX-A11Y bleibt bei Trigger anwendbar. / Didactic inline comments and a new `docs/accessibility/` artifact are currently `N/A`: no non-trivial logic and no new UI/interaction flow. Triggered DocFX accessibility remains applicable. | Nicht-triviale Logik, UI-/Bedienfluss oder geänderte DocFX-Navigation. / Non-trivial logic, UI/interaction flow, or changed DocFX navigation. |

## Abschlussregel / Completion Rule

Diese Checklist-Phase ist nur dann `Completed`, wenn CHK001–CHK007 und
`READY-GATE-001` belegt sind, diese Datei vollständig ist und das strukturierte
Ergebnis ihren exakten normalisierten SHA-256 besteht. Die offenen späteren
Punkte blockieren erst ihre erklärte Grenze. / This checklist phase is
`Completed` only when CHK001–CHK007 and `READY-GATE-001` are evidenced, this
file is complete, and the structured result passes with its exact normalized
SHA-256. Open later items block only their declared boundary.
