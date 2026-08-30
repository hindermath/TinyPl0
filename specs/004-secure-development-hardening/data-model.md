# Datenmodell: Secure-Development-Härtung / Data Model: Secure Development Hardening

## Überblick / Overview

Das Feature führt keine Fachdatenbank ein. Sein dauerhaftes Datenmodell besteht
aus versionierten JSON-/Markdown-Evidenzen und dem bestehenden öffentlichen
VM-Optionsvertrag. Die maschinenlesbare Prüfinstanz ist kanonisch; lesbare
Markdown-Sichten dürfen keine davon abweichenden Statuswerte erfinden.

*The feature introduces no application database. Its persistent model consists
of versioned JSON/Markdown evidence and the existing public VM options contract.
The machine-readable assessment is canonical; readable views must not invent
different states.*

## 1. AssessmentDocument

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `schemaVersion` | string | exakt `1.0` |
| `assessmentId` | string | exakt `2026-08-30-tinypl0-hardening` |
| `featurePath` | string | `specs/004-secure-development-hardening` |
| `runId` | UUID | `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7` |
| `evaluatedCommit` | 40/64 hex | vollständiger Commit der späteren Evidence; kein Kurz-SHA |
| `baselineManifestSha256` | SHA-256 | Hash des tatsächlich geprüften Manifests |
| `canonicalChecklistSha256` | object | zwölf Pfad→Hash-Einträge |
| `reviewerRole` | string | vom Evidence-Erzeuger getrennte Rolle |
| `items` | AssessmentRow[157] | exakt 157, eindeutige und kanonische CL-ID-Menge |

**Invarianten / Invariants**:

- Die ID-Mengen von zwölf Einzelchecklisten, Sammelband und `items` sind gleich.
- Die Reihenfolge folgt CL-01-01 bis CL-12-12 in kanonischer Dateireihenfolge.
- Alle zwölf `CL-12-*` sind `N/A` plus `Not Assessed` und zeigen nur auf den
  getrennten Sandbox-Folge-Intake.
- `evaluatedCommit` und jeder positive EvidenceReference gehören zusammen.

## 2. AssessmentRow

| Feld / Field | Typ / Type | Pflicht / Required | Bedeutung / Meaning |
|---|---|---|---|
| `clId` | `CL-nn-nn` | ja | stabile Checklisten-ID |
| `applicability` | enum | ja | `Applicable`, `N/A`, `Open` |
| `implementation` | enum | ja | `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed` |
| `rationale` | LocalizedText | ja | konkrete Entscheidung, kein Platzhalter |
| `evidence` | EvidenceReference[] | ja | darf bei nicht positiver Aussage leer sein, wenn Ziel benannt ist |
| `ownerRole` | string | ja | Maßnahme und Pflege |
| `reviewerRole` | string | ja | unabhängig vom Autor/Agenten |
| `residualRisk` | RiskRecord | ja | auch `None evidenced` braucht Begründung |
| `reevaluation` | Reevaluation | ja | Trigger und gegebenenfalls Termin |
| `nextAction` | LocalizedText | ja | kleinste nächste Maßnahme oder `None` mit Grund |
| `priority` | enum/null | bedingt | Pflicht bei Open/Partly/Not Fulfilled/Not Assessed |
| `targetDateOrTrigger` | string/null | bedingt | ISO-Datum oder präziser Trigger |
| `expectedEvidence` | LocalizedText/null | bedingt | erwarteter Abschlussnachweis |
| `findingId` | string/null | bedingt | Pflicht, falls diese Zeile eine Änderung autorisieren soll |

**Zulässige Statusbeziehungen / Allowed State Relations**:

- `N/A` → ausschließlich `Not Assessed`.
- `Fulfilled` → mindestens eine existierende, zum Commit passende EvidenceReference.
- `Applicable` + `Partly Fulfilled|Not Fulfilled` → offene Maßnahme und Risiko.
- `Open` → keine Produktänderungsautorität; Owner, Trigger und Evidenzziel.
- Eine Zeile autorisiert eine Nicht-VM-Änderung erst mit vollständigem Finding.

## 3. LocalizedText

```text
LocalizedText
├── de: nicht leer, Deutsch zuerst, CEFR B2
└── en: nicht leer, semantisch synchron, CEFR B2
```

Maschinenwerte, Pfade, IDs und Befehle werden nicht künstlich übersetzt.
Essenzielle Bedeutung darf nicht nur durch Reihenfolge, Farbe oder Layout
entstehen.

## 4. EvidenceReference

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `evidenceId` | string | innerhalb des Dokuments eindeutig |
| `kind` | enum | `File`, `CommandLog`, `TestResult`, `WorkflowRun`, `Artifact`, `Decision`, `ExternalReview` |
| `pathOrUri` | string | Repositorypfad bevorzugt; externe URI nur ohne Secrets |
| `sha256` | SHA-256/null | Pflicht für dateibasierte positive Evidence |
| `commit` | full hex/null | Pflicht für Repository-/CI-positive Evidence |
| `command` | string/null | exakter Befehl für CommandLog/TestResult |
| `exitCode` | int/null | `0` bei positivem Nachweis, definierter Nonzero bei Rot-Evidence |
| `runnerOrPlatform` | string[] | z. B. `macOS`, `PowerShell 7`, `ubuntu-latest`, `.NET SDK 10.0.x` |
| `observedAt` | UTC timestamp | tatsächliche Beobachtung, nicht Planzeit |
| `result` | enum | `Pass`, `ExpectedFail`, `Fail`, `Open`, `N/A` |
| `proofBoundary` | LocalizedText | was der Nachweis beweist und was nicht |

## 5. Finding

| Feld / Field | Typ / Type | Regel / Rule |
|---|---|---|
| `findingId` | string | stabil `FND-<AREA>-nnn` |
| `clIds` | string[] | mindestens eine ID aus AssessmentDocument |
| `applicability` | const | `Applicable` |
| `implementation` | enum | `Partly Fulfilled` oder `Not Fulfilled` |
| `risk` | RiskRecord | benannter Asset-/Missbrauchsweg |
| `redTest` | RedGreenEvidence | vor dem ersten Produkt-/Workflow-Edit vollständig |
| `smallestChange` | ChangeSet | exakte Dateien und Ausschlüsse |
| `regressionProof` | string[] | exakte unveränderte Tests/Gates |
| `ownerRole` | string | Repositoryrolle |
| `reviewerRole` | string | getrennte Rolle |
| `state` | enum | `Proposed`, `Authorised`, `Implemented`, `Verified`, `Open`, `FollowUp`, `Rejected` |

**Zustandsübergänge / State transitions**:

```text
Proposed -> Authorised -> Implemented -> Verified
    |             |             |
    +-> Rejected  +-> Open      +-> Open
    +-> FollowUp
```

Nur `Authorised` darf den ersten Edit außerhalb der zwei VM-Härtungen starten.
`Verified` verlangt grüne unveränderte Tests und Regression.

## 6. RedGreenEvidence

| Feld / Field | Regel / Rule |
|---|---|
| `testPath` | exakte Test-/Validator-Datei |
| `command` | exakter selektiver Befehl |
| `expectedRedCause` | nur der fachlich erwartete Fehler |
| `redExitCode` | nonzero; Timeout nur als Harness-Schutz markiert |
| `testSourceSha256` | identisch zwischen Rot und Grün |
| `greenExitCode` | `0` |
| `regressionCommands` | relevante Suite, Golden, Coverage, A11Y, Supply Chain |

Unbezogene Compiler-, Restore-, Netzwerk- oder Toolfehler gelten nicht als Rot.

## 7. ChangeSet

| Feld / Field | Bedeutung / Meaning |
|---|---|
| `includedPaths` | exakte, minimal notwendige Dateien |
| `excludedPaths` | explizite Nicht-Ziele |
| `whyMinimal` | warum weniger Dateien den Befund nicht beheben |
| `architectureImpact` | `None`, `Security`, `General`, `Both` plus Evidenzpfad |
| `documentationImpact` | `UpdateRequired`, `NoUpdateRequired`, `GeneratedUpdate`, `FollowUp` |

## 8. RiskRecord

| Feld / Field | Typ / Type |
|---|---|
| `asset` | LocalizedText |
| `threat` | LocalizedText |
| `stride` | array of `S,T,R,I,D,E` |
| `cia` | array of `Confidentiality,Integrity,Availability` |
| `capec` | string[]; Pflicht für relevante hohe Wege |
| `likelihood` | `Low`, `Medium`, `High` |
| `impact` | `Low`, `Medium`, `High`, `Critical` |
| `severity` | `Low`, `Medium`, `High`, `Critical` |
| `mitigations` | `LocalizedText` oder `EvidenceReference[]`; mindestens eine konkrete Maßnahme oder begründete offene Maßnahme |
| `residualSeverity` | enum |
| `acceptance` | null oder Maintainer-Entscheidung mit Ablaufdatum |

Kritische/hohe offene Risiken blockieren. Der Agent darf keine Acceptance
erzeugen.

## 9. VmExecutionPolicy

Öffentlicher Vertrag in `VirtualMachineOptions`:

| Eigenschaft / Property | Typ | Default | Validierung |
|---|---:|---:|---|
| `StackSize` | int | `500` | `3..1_000_000` vor Addition oder Allokation |
| `InstructionBudget` | int | `1_000_000` | `>0` vor Ausführung |
| `EnableStoreTrace` | bool | `false` | unverändert |
| `Language` | string | `de` | bestehender Kulturvertrag |
| `Messages` | ResourceManager? | `null` | bestehender DI-Vertrag |

Interner Ausführungszustand:

| Feld / Field | Initial | Regel / Rule |
|---|---:|---|
| `executedInstructions` | `0` | monoton, niemals größer als Budget |
| `nextInstructionAllowed` | `true` | `executedInstructions < InstructionBudget` |
| `budgetDiagnostic` | null | entsteht vor `N+1`, terminal |
| `configurationDiagnostic` | null | entsteht vor Stackallokation, terminal |

Batch-Ergebnis bleibt `VmExecutionResult`. Step-Initialisierung muss bei
ungültiger Konfiguration einen sicheren terminalen Zustand bereitstellen;
`Step()` darf danach keine Instruktion ausführen oder weitere Diagnose
duplizieren.

## 10. GateRequirement und GateEvidence

`gate-requirements.json` besitzt pro stabiler Gate-ID:

- `applicability`: nur `Applicable` oder `N/A`;
- `requiredScope`;
- `exactCommands`;
- `requiredCommandTokens`;
- `requiredRunnerOrPlatformTokens`;
- `evidencePath`;
- `rationale` und `reevaluationTrigger`.

Das gesamte Dokument MUSS zusätzlich gegen
`contracts/gate-requirements.schema.json` bestehen. `Applicable` verlangt
mindestens einen tatsächlich ausführbaren Befehl sowie nichtleere Befehls- und
Runner-/Plattformtokens. `N/A` verlangt leere Ausführungslisten, eine konkrete
Begründung und einen Wiedervorlageauslöser. Slash-Kommandonamen wie
`speckit.analyze` sind Phasenbezeichner, keine Shellbefehle; ihre Evidence
entsteht über die ausführbaren Phase-Result-Validatoren.

Spätere GateEvidence bindet zusätzlich Requirements-Hash, exakten HEAD,
Exitcode, Loghash und Ergebnis. Jede Applicable-Gate-ID braucht mindestens einen
passenden Eintrag; jede N/A-ID braucht Begründung und Trigger statt erfundener
Ausführung.

## 11. SupplyChainRecord

Die maschinenlesbare Instanz liegt in
`docs/security/supply-chain-evidence.json`; die gleichnamige Markdown-Datei ist
die text-first Sicht.

| Feld / Field | Regel / Rule |
|---|---|
| `evaluatedCommit` | exakter vollständiger Feature-HEAD |
| `artifactSet` | tatsächlicher `_site`-/Release-Kandidat, nicht TestResults |
| `artifactManifest` | normalisierte, pfadsortierte Liste aus relativem Pfad, Länge und Datei-SHA-256; keine Zeitstempel |
| `artifactSha256` | SHA-256 der normalisierten Artefaktmanifest-Datei oder Provider-Digest des exakt gleichen veröffentlichten Satzes |
| `sbomPath` | validiertes CycloneDX-JSON |
| `sbomSha256` | SHA-256 |
| `generator` | Name, Version, Quelle, Lizenz, Pin |
| `dependencyScan` | direkter/transitiver CVE- und Outdated-Stand |
| `licenceReview` | Ergebnis plus offene Entscheidungen |
| `vexState` | `NotRequiredNoKnownFinding` oder referenzierter VEX-Record |
| `slsaClaim` | nur nachgewiesener Ist-Stand plus Ziel |
| `provenance` | null oder verifizierte Attestation |
| `scorecardDecision` | Stand, Scope, Restrisiko, Trigger |

## 11a. AsvsVerificationDocument

Die lesbare Sicht bleibt `docs/security/asvs-verification.md`. Die
maschinenlesbare Quelle `docs/security/asvs-verification.json` enthält die
gegen die offizielle, auf `v5.0.0` gepinnte ASVS-JSON-Quelle geprüften exakt 70
Level-1-IDs:

| Feld / Field | Regel / Rule |
|---|---|
| `schemaVersion` | exakt `1.0` |
| `scope` | exakt `pl0c --api` einschließlich Start/Konfiguration und statischer Auslieferung |
| `sourceUri` | gepinnte offizielle `OWASP/ASVS@v5.0.0`-Flat-JSON-Quelle |
| `sourceSha256` | SHA-256 der tatsächlich geladenen offiziellen Quelle |
| `evaluatedCommit` | exakter vollständiger Feature-HEAD |
| `items` | exakt 70 eindeutige `v5.0.0-<req_id>`-Einträge in offizieller Reihenfolge |
| `applicability` | nur `Applicable` oder `N/A` |
| `implementation` | bei `Applicable` ausschließlich `Fulfilled`; bei `N/A` `Not Assessed` |
| `rationale`/`evidence` | nicht leer; positive Evidence ist HEAD-gebunden |
| `openCriticalOrHigh` | exakt `0` am Abschluss |

Der Gate-Validator lädt die offizielle Quelle read-only, vergleicht Hash,
Menge und Reihenfolge und prüft danach die Statusrelationen. Eine bloße Suche
nach den Texten „Level 1“ oder „Fulfilled“ ist kein Vollständigkeitsnachweis.

## 12. SerialWriterLease

Keine Laufzeit-Lockdatei wird ins Repository aufgenommen. Tasks erzwingen
logische Single-Writer-Leases:

| `resourceId` | Writer | Freigabe / Release |
|---|---|---|
| `assessment-evidence` | Evidence task | nach Schema-, Hash- und Linkprüfung |
| `ide-version` | Version task | nach Versionierungscommit und genau einem Build/Test plus Logeintrag |
| `project-statistics` | Statistics task | nach Renderer und `current: true` |
| `baseline-compendium` | Generator task | nach Bash/PowerShell-Parität und Check-Modus |

Ein fehlgeschlagener Writer setzt den Lauf auf `Blocked`; ein zweiter Task darf
nicht parallel übernehmen oder einen teilgeschriebenen Stand fortsetzen.

## 13. VersionBuildEvidence

Der vorläufige `Minor` ist der read-only ermittelte nächste GitHub-PR-Slot
`72`, nicht Feature `004`. Er wird vor dem ersten Versionierungscommit und vor
PR-Erzeugung erneut geprüft. Jede Build-/Testzeile enthält:

| Feld / Field | Regel / Rule |
|---|---|
| `invocationId` | eindeutige fortlaufende lokale ID |
| `prMinor` | aktuell read-only bestätigte kanonische PR-Nummer |
| `headSha` | voller Commit, auf dem der Befehl lief |
| `commitCountPatch` | exakt `git rev-list --count <headSha>` |
| `manualBuild` | gegenüber der vorherigen Zeile um eins erhöht |
| `version` | `1.<prMinor>.<commitCountPatch>.<manualBuild>` |
| `command` | genau ein `dotnet build` oder `dotnet test` |
| `startedAt`/`completedAt` | UTC-Zeitpunkte |
| `exitCode` | erwarteter Red-Nonzero oder Green/Regression `0` |
| `logSha256` | Hash des text-first Logs ohne Secrets |

Vor dem Aufruf werden `Version`, `AssemblyVersion` und `FileVersion` auf den
Wert gesetzt und als lokaler Commit abgeschlossen. Danach läuft genau der eine
Befehl auf sauberem HEAD. Eine PR-Slot-Kollision oder ein weiterer Commit
ändert `Minor` beziehungsweise `Patch` und erzwingt einen neuen finalen
Build-/Testnachweis; ein nachträglicher ungetesteter Versionsedit ist ungültig.
Für den finalen Kandidaten ist dieser eine Befehl der vollständige
Release-`dotnet test`-Aufruf mit implizitem Build und Coverage. Frühere
selektive Rot-/Grün-Aufrufe bleiben hashgebundene TDD-Evidence, sind aber kein
Ersatz für den einen finalen exact-HEAD-Gesamtlauf. Provider-Checks konsumieren
den bereits versionierten Head unverändert und übernehmen den lokalen
Version-Writer nicht.
