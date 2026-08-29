# Artefaktmodell / Artefact Model

**Feature**: `003-constitution-change`

## Zweck / Purpose

Dieses Feature ändert kein Laufzeit-Datenmodell. Das folgende Modell beschreibt
die versionierten Governance- und Evidence-Artefakte, ihre Invarianten und
Zustandsübergänge. / This feature changes no runtime data model. The model below
defines the versioned governance and evidence artefacts, their invariants, and
state transitions.

## Entitäten / Entities

### 1. ConstitutionMirrorPair

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `canonicalPath` | path | exakt `constitution.md` / exactly |
| `mirrorPath` | path | exakt `.specify/memory/constitution.md` / exactly |
| `securityFirstInvariant` | boolean | Prinzip I bleibt unverändert / Principle I remains unchanged |
| `localAddendumTitle` | string | `Didaktische und sprachliche Klarheit / Pedagogical and Linguistic Clarity` |
| `constitutionVersion` | SemVer | MINOR-Erhöhung für neuen Abschnitt / MINOR increase for new section |
| `lastAmended` | date | Datum der tatsächlichen Änderung / actual amendment date |
| `normalizedSha256` | 64-char lowercase hex | beide Dateien müssen denselben Wert haben / both files must match |

**Invariante / Invariant**: Nach der Änderung sind beide Dateien bytegleich;
der neue Abschnitt liegt im Level-2-Addendum und nicht unter Prinzip I.

### 2. GuidanceParitySet

| Rolle / Role | Pfade / Paths |
|---|---|
| Gepflegte Laufzeit-Guidance / Maintained runtime guidance | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md` |
| Generische Spec-Kit-Templates / Generic Spec Kit templates | `.specify/templates/agent-file-template.md`, `.specify/templates/plan-template.md`, `.specify/templates/tasks-template.md`, `.specify/templates/commands/plan.md`, `.specify/templates/commands/tasks.md` |
| Bootstrap-Agenten-Templates / Bootstrap agent templates | `scripts/templates/AGENTS.md.tmpl`, `CLAUDE.md.tmpl`, `GEMINI.md.tmpl`, `copilot-instructions.tmpl`, `speckit-workflow-section.md` |

**Semantische Pflichtfelder / Required semantic fields**:

- TinyPl0-spezifischer didaktischer Titel ohne Ersatz von Security-First;
- vollständige anwendbare öffentliche XML-Dokumentation;
- keine globale oder projektweite CS1591-Unterdrückung;
- DE-first/EN-second, CEFR B2 und text-first;
- TDD Rot → Grün → Regression oder begründetes `N/A` mit Trigger;
- DocFX plus textorientierte A11Y-Folgeprüfung;
- aktuelle Acht-Preset-IDs, -Versionen und -Prioritäten, wo eine Matrix geführt
  wird.

*Every maintained surface carries the same semantic rules. Formatting may vary
by agent, but an intentional semantic deviation requires an explicit rationale.*

### 3. ProductDocumentationGate

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `projectPath` | path | einer der vier Produkt-`.csproj` / one of four product projects |
| `generateDocumentationFile` | boolean | muss `true` sein / must be true |
| `noWarnCodes` | set of warning IDs | darf `1591` nicht enthalten / must not contain 1591 |
| `publicApiMembers` | set | durch Compiler-Sichtbarkeit bestimmt / compiler visibility determines scope |
| `xmlElements` | set | `summary`, anwendbare `param`, `returns`, `exception`; optionale `remarks`, `example` |
| `buildResult` | enum | `RedExpected`, `Green`, `FailedUnexpectedly` |

**Öffentliche Fläche / Public surface**: Nur extern sichtbare öffentliche Typen
und Mitglieder. `public`-Mitglieder eines `internal`-Typs sind keine
öffentliche Produkt-API für FR-002, können aber wegen `includePrivateMembers`
in DocFX erscheinen.

### 4. TddEvidenceRecord

| Feld / Field | Erlaubte Werte / Allowed values |
|---|---|
| `scope` | `BuildGovernance` |
| `redCommand` | exakter gefilterter xUnit-Befehl / exact filtered command |
| `redExit` | nicht null und nicht 0 / non-zero |
| `redReason` | Assertion nennt CS1591-Unterdrückung / assertion identifies suppression |
| `greenCommand` | derselbe gefilterte Befehl / same filtered command |
| `greenExit` | `0` |
| `regressionCommands` | Release-Build, Gesamtsuite, Coverage |
| `productLogicChanged` | `false` |

**Zustandsübergang / Transition**:

```text
Declared -> RedObserved -> SuppressionRemoved -> XmlComplete -> GreenObserved -> RegressionPassed
```

DE: Der Zustand darf nicht von `Declared` direkt zu `GreenObserved` springen;
sonst fehlt der beobachtbare Rot-Nachweis. EN: The state must not jump directly
from `Declared` to `GreenObserved`, because that would omit observable red
evidence.

### 5. DocFxAccessibilityEvidence

| Feld / Field | Regel / Rule |
|---|---|
| `docfxCommand` | exakt `docfx docfx.json` |
| `generatedTrackedPaths` | `api/.manifest`, `api/**/*.yml` |
| `generatedTemporaryRoot` | `_site/`, ignoriert / ignored |
| `serverOrigin` | `http://127.0.0.1:8080` |
| `nodeVersion` | Node 24 LTS |
| `playwrightVersion` | `1.62.1` |
| `axePlaywrightVersion` | `4.13.0` |
| `representativePages` | Startseite, `Pl0Compiler`, `VirtualMachine` |
| `axeViolations` | exakt `0` / exactly zero |
| `lynxOutputs` | nicht leer, verständliche Überschriften/API-Texte / non-empty meaningful text |

### 6. PresetMatrixSnapshot

| Feld / Field | Regel / Rule |
|---|---|
| `sourcePath` | `scripts/config/spec-kit-governance-presets.json` |
| `schemaVersion` | `1` |
| `count` | exakt `8` |
| `ids` | eindeutig / unique |
| `priorities` | `10,20,30,40,50,60,70,80` |
| `documentedCopies` | Constitution-Paar, README, fünf Agentenflächen und betroffene Templates |

### 7. IdeVersionTuple

| Komponente / Component | Regel / Rule |
|---|---|
| `Major` | `1` |
| `Minor` | `3` für Feature `003` |
| `Patch` | `git rev-list --count HEAD` des enthaltenen Commits / of containing commit |
| `Build` | manueller Zähler, vor jedem `dotnet build/test` +1 / manual counter increment |

**Invariante / Invariant**: `Version == AssemblyVersion == FileVersion`.

### 8. ProjectStatisticsEntry

| Feld / Field | Regel / Rule |
|---|---|
| `date` | Implementierungsdatum / implementation date |
| `branchOrPhase` | `codex/003-constitution-change` bzw. Implementierungsphase |
| `workWindow` | beobachtbares, nicht als Stoppuhr behauptetes Fenster |
| `productionLines` | exakter/erklärter Wert |
| `testLines` | exakter/erklärter Wert |
| `documentationLines` | exakter/erklärter Wert |
| `workPackages` | Governance, Guard/XML, DocFX/A11Y, Statistik |
| `manualBaseline` | `80` Zeilen/Arbeitstag |
| `thorstenSoloBaseline` | `125` Zeilen/Arbeitstag |

**Invariante / Invariant**: Eintrag ist der neueste am Ende der chronologischen
Tabelle; der generierte `## Gesamtstatistik / Overall Statistics`-Block bleibt
der letzte Top-Level-Abschnitt.

### 9. GateRequirement

| Feld / Field | Regel / Rule |
|---|---|
| `gateId` | stabil und eindeutig / stable and unique |
| `applicability` | genau `Applicable` oder `N/A` |
| `requiredScope` | prüfbarer fachlicher Umfang / reviewable technical scope |
| `requiredCommandTokens` | bei `Applicable` mindestens der exakte Befehl / exact command for applicable gates |
| `requiredRunnerOrPlatformTokens` | bei `Applicable` nicht leer / non-empty for applicable gates |
| `rationale` | bei `N/A` nicht leer / non-empty for N/A |
| `reevaluationTrigger` | bei `N/A` nicht leer / non-empty for N/A |

### 10. GateEvidenceRecord

Die spätere Evidence folgt dem installierten autonomen Schema 2.0. Je Gate gibt
es genau eine `Primary`-Zeile. `Applicable` benötigt `Pass`, exakten Head,
ausgeführten Befehl, Runner/Plattform und Referenz. `N/A` benötigt `N/A`,
Begründung und Wiedervorlage.

*Later schema-2.0 evidence has exactly one primary row per declared gate and
binds the reviewed head, requirements hash, actual command, platform, and
evidence reference.*

## Keine Laufzeitmigration / No Runtime Migration

Es gibt keine Datenmigration, keine Abwärtskompatibilitätslogik und keine
Änderung der `.pcode`-/Listing-Serialisierung. Die einzige Versionsmigration
betrifft normative Constitution-SemVer- und IDE-Build-Metadaten nach ihren
vorhandenen Regeln.

*There is no runtime data migration or P-Code serialization change. Only
existing constitution and IDE metadata version contracts apply.*
