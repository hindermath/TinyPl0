# Datenmodell: Sandbox-Bewertung / Data Model: Sandbox Assessment

## AssessmentRecord

Repräsentiert die gesamte Feature-Bewertung. / Represents the complete feature assessment.

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `featureId` | string | exakt `005-sandbox-secure-development` |
| `evaluatedRepository` | string | `TinyPl0`; kein privater Host-Pfad |
| `sandboxRepository` | string | Registry-Identität `container-images/absdd-image-sandbox` |
| `sandboxCommit` | Git object ID | exakter 40-Zeichen-Commit |
| `assessmentDate` | date | ISO `YYYY-MM-DD` |
| `responsibleRole` | string | Rolle, kein privates Kontoprofil |
| `reviewerRole` | string | getrennte menschliche Review-Rolle |
| `usageDecision` | UsageDecision | genau eine aktuelle Entscheidung |
| `items` | CL12Item[12] | eindeutige IDs `CL-12-01` bis `CL-12-12` in Reihenfolge |
| `mounts` | MountBoundary[] | mindestens TinyPl0, Build, Audit und verbotene Kategorien |
| `workLocations` | WorkLocation[] | Build/Test/Docs/A11Y/Golden/Review abgedeckt |
| `followUps` | FollowUp[] | referenziert jede offene/unvollständige Zeile |

## CL12Item

| Feld / Field | Erlaubte Werte / Allowed values | Pflichtregel / Required rule |
|---|---|---|
| `id` | `CL-12-01`…`CL-12-12` | eindeutig, kanonische Reihenfolge |
| `applicability` | `Applicable`, `N/A`, `Open` | genau ein Wert |
| `implementation` | `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed` | genau ein Wert |
| `learningStage` | `Grundlage`, `Aufbau`, `Vertiefung` | genau ein Wert |
| `ownerRole` | nicht leer | menschliche oder Projektrolle |
| `rationaleDe`, `rationaleEn` | Text | konkrete Begründung auf CEFR B2 |
| `evidence` | relative Pfade/IDs | kein privater Host-Pfad; Zielpfad erlaubt |
| `residualRiskDe`, `residualRiskEn` | Text | `None` nur mit Begründung |
| `reevaluationTriggerDe`, `reevaluationTriggerEn` | Text | Ereignis, Änderung oder Termin |
| `followUpId` | ID oder `N/A` | Pflicht bei `Open`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed` |

### Statusbeziehungen / Status Relationships

- `N/A` verlangt eine konkrete Nichtanwendbarkeitsbegründung und darf nicht `Fulfilled` sein.
- `Open` darf nicht `Fulfilled` sein und benötigt eine Folgeaufgabe.
- `Applicable` plus `Fulfilled` benötigt positive, vorhandene und reviewbare Evidenz.
- Fehlende menschliche Freigabe bleibt `Open`/`Not Fulfilled`; Agenten- oder CI-Evidenz kann sie nicht ersetzen.
- Beschriebene Toolchain ohne TinyPl0-Ausführung ist höchstens `Applicable`/`Not Assessed` oder `Open`.

## UsageDecision

| Feld / Field | Beschreibung / Description |
|---|---|
| `mode` | `Not Ready`, `Conditional Pilot`, `Approved` |
| `allowedWork` | ausdrücklich erlaubte Arbeitsarten |
| `prohibitedWork` | agentische Schreibarbeit, Secrets oder Remote-Aktionen soweit nicht erlaubt |
| `conditions` | exakte Identität, Freigabe, Mounts, Egress, Secret-Trennung, Baseline-Evidenz |
| `residualRisks` | verbleibende Risiken mit Owner |
| `expiresOrReevaluates` | Datum oder Trigger |

`Approved` ist in diesem Lauf kein erreichbarer Agentenstatus. Er setzt eine unabhängige menschliche Freigabe voraus.

## MountBoundary

| Feld / Field | Beschreibung / Description |
|---|---|
| `sourceSymbol` | z. B. `<TinyPl0-repository>`; nie absoluter privater Pfad |
| `sandboxTarget` | stabiler Containerpfad |
| `purpose` | Quellarbeit, Build oder Audit |
| `access` | `ReadOnly`, `ReadWrite`, `NotMounted` |
| `agentAccess` | `ReadOnly`, `ReadWrite`, `Denied` |
| `forbiddenNeighbors` | Home, Profile, Keys, andere Projekte oder ähnliche Kategorien |
| `evidenceTarget` | spätere Konfiguration oder negativer Grenztest |

## WorkLocation

| Feld / Field | Beschreibung / Description |
|---|---|
| `workType` | Build, Test, Coverage, Docs, A11Y, Golden, Review oder Remote Delivery |
| `preferredLocation` | `Sandbox`, `Local`, `CI`, `HumanOnly` |
| `currentStatus` | `Supported`, `Open`, `N/A`, `Prohibited` |
| `prerequisites` | technische und menschliche Bedingungen |
| `fallback` | sicherer Alternativpfad |
| `evidenceBoundary` | was das Ergebnis beweist und ausdrücklich nicht beweist |

## FollowUp

| Feld / Field | Regel / Rule |
|---|---|
| `id` | stabile `FUP-SBX-NNN`-ID |
| `sourceIds` | eine oder mehrere CL-/Gate-IDs |
| `priority` | `P0`, `P1`, `P2`, `P3` |
| `risk` | konkrete Auswirkung, keine vage Aussage |
| `ownerRole` | benannte verantwortliche Rolle |
| `nextAction` | kleinste getrennte Folgeaktion |
| `dueDate` | ISO-Datum oder begründeter Ereignistermin |
| `expectedEvidence` | Pfad, Konfiguration, Review oder Ausführungsnachweis |
| `reevaluationTrigger` | Änderung oder Ereignis |
| `scopeBoundary` | erklärt, warum dieser Lauf die Aktion nicht umsetzt |

## Lebenszyklus / Lifecycle

```text
Observed -> Assessed -> Reviewed -> Delivered -> Archived
                     \-> Open Follow-up (separate authority)
```

DE: `Open` ist kein Fehler im Bewertungsartefakt, solange Risiko und Folgeweg vollständig sind. EN: `Open` is not a defect in the assessment artefact when risk and follow-up are complete.
