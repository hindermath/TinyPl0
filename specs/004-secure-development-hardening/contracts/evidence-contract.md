# Evidenz- und Gate-Vertrag / Evidence and Gate Contract

## Zweck / Purpose

Dieser Vertrag trennt Plan, Befund, Änderung und Beweis. Er erteilt keine
Implementierungs-, Remote- oder Merge-Berechtigung. Die maschinenlesbaren
Delivery-Gates stehen in `../gate-requirements.json`.

## Phasenidentität / Phase Identity

| Feld / Field | Wert / Value |
|---|---|
| Feature | `specs/004-secure-development-hardening` |
| Branch | `codex/004-secure-development-hardening` |
| Run-ID | `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7` |
| Plan-Result | `.specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/plan.result.json` |
| Plan-Payload | `specs/004-secure-development-hardening/plan.md` |
| Runner/Plattform | `Codex`, `frontier-reasoning`, `codex-frontier-auto`, `gpt-5.6-sol`, `high`, `macOS`, `PowerShell 7` |

Die vier akzeptierten Input-Hashes werden read-only gegen
`autonomous-run-state.json` geprüft. Intake, Serie und Run-State werden nicht
editiert.

## Aufgabenbeweis der Planphase / Plan-phase Task Evidence

Die Planphase besitzt sechs abgeschlossene Artefaktaufgaben:

1. `plan.md` mit Constitution Check, Architektur, exakten Dateien, TDD,
   Coverage, Supply Chain und Single-Writer-Reihenfolge.
2. `research.md` mit aufgelösten technischen Entscheidungen.
3. `data-model.md` mit Status-, Finding-, VM-, Risiko- und Gate-Modell.
4. `quickstart.md` mit ausführbaren Prüfwegen.
5. `contracts/` mit Assessment-, VM- und Evidence-Vertrag.
6. `gate-requirements.json` mit allen vor Implementierung erklärten Gates.

`Completed` ist nur zulässig, wenn alle sechs existieren, JSON syntaktisch
gültig ist und keine Planplatzhalter offen sind. Das bereits akzeptierte
Plan-Ergebnis bleibt als historische Evidence unverändert: Sein Dateihash MUSS
dem Run-State entsprechen. Plan Review bindet den damals unabhängig geprüften
Planungsstand. Weil Analyze minimale letzte Planungs-/Taskremediation
ausdrücklich erlaubt, bindet erst das validierte `analyze.result.json` den
finalen pre-implementation Stand; kein Vorgängerresultat wird umgeschrieben.

*Completion requires all six artefacts, valid JSON, and no unresolved planning
placeholders. Accepted predecessor results remain immutable historical
evidence. Analyze is the causal binding for its explicitly authorised final
planning/task remediation.*

## Befundschranke / Finding Gate

Außer den zwei VM-Härtungen darf kein Code-, UI-, CI- oder Workflow-Edit
beginnen, bevor ein Finding folgende Beweise besitzt:

- konkrete CL-ID aus der validierten 157er Matrix;
- `Applicable` und `Partly Fulfilled` oder `Not Fulfilled`;
- Asset, Missbrauchsweg, Auswirkung, Schwere, Restrisiko;
- exakter roter Test/Validator mit nur erwarteter Fehlerursache;
- kleinster exakter Dateisatz und begründete Ausschlüsse;
- unveränderter Test grün sowie passende Regression;
- Owner und unabhängiger Reviewer.

`Open`, `N/A`, `AlreadySatisfied` und `FollowUp` autorisieren keine Änderung.
Eine kritische/hohe Risikoakzeptanz benötigt eine schriftliche, befristete
Maintainer-Entscheidung und kompensierende Kontrolle.

## Evidence-Qualität / Evidence Quality

Positive Evidence muss:

1. existieren und auflösbar sein;
2. den geprüften vollständigen Commit/Workflow-Head nennen;
3. Befehl, Exitcode, Runner/Plattform und Zeitpunkt enthalten;
4. bei Dateien einen normalisierten SHA-256 tragen;
5. Beweisgrenze und Nichtaussage erklären;
6. von einer getrennten Rolle reviewbar sein.

Ein grüner Build beweist keine A11Y, kein Threat Model und keine Lieferkette.
Ein existierender Stub beweist keine erfüllte Kontrolle. Ein Scan ohne Fund
beweist nur den geprüften Scope und Zeitpunkt.

## Rot-Grün-Regression / Red-Green-Regression

- Rot: fachlich erwarteter Nonzero-Exit; unbezogener Restore-, Compiler-,
  Netzwerk- oder Toolfehler ist ungültig.
- Grün: derselbe Testpfad und derselbe Testquellhash, Exit `0`.
- Regression: betroffene Einheit plus Gesamtsuite, Golden/Traceability,
  Coverage und je nach Fläche A11Y/DocFX/Supply Chain.
- Logs werden text-first in `autonomous-run-evidence.md` oder einem dort
  referenzierten hashgebundenen Artefakt zusammengefasst; rohe sensible Logs
  werden nicht eingecheckt.

## 157-ID-Gate / 157-ID Gate

Der Validator muss mindestens prüfen:

- zwölf kanonische Dateien mit Counts `12/13/15/10/13/11/12/13/17/17/12/12`;
- Summe und eindeutige Menge `157`;
- identische ID-Menge und Reihenfolge im Sammelband;
- `assessment.json` gegen `assessment-record.schema.json`;
- 157 eindeutige Assessment-IDs;
- genau zwölf CL-12-Zeilen als `N/A`/`Not Assessed`;
- jede `Fulfilled`-Zeile mit mindestens einer existierenden Evidence;
- Zusatzfelder für Open/Partly/Not Fulfilled/Not Assessed;
- keine Follow-up-Ausführung und keine still ausgelassene ID.

## Architektur- und Security-Gate / Architecture and Security Gate

Vor VM-Grün müssen Threat Model, arc42 Security, Quality Scenarios,
allgemeines ADR und S-ADR mindestens den Ressourcenmissbrauch und die
Vorvalidierung beschreiben. Vor einer anderen Finding-Änderung müssen die
passenden Trust-/Runtime-/Deployment- und Risikoentscheidungen ergänzt sein.

## Supply-Chain-Gate / Supply-chain Gate

- Direct/transitive NuGet, Quellen, veraltet, CVEs, Lizenzen und Lockstatus.
- CycloneDX-Tool nur gepinnt und nach eigener Prüfung.
- SBOM-JSON validiert und an konkreten Artefakt-Hash gebunden.
- VEX nur bei bekanntem Fund; Status aus FR-013.
- Kritische/hohe offene Findings blockieren.
- SLSA-Ist und Ziel getrennt; keine unbelegte Level-Aussage.
- CI-Action nur vollständiger SHA und minimale Permissions.
- AI-SBOM bleibt `N/A`, solange KI nur Development Tool ist.

## A11Y-, Sprache- und Dokumentationsgate / Accessibility, Language, and Documentation Gate

Geänderte Lern-/Nutzertexte sind DE zuerst, EN danach, CEFR B2 und text-first.
Öffentliche API-/XML-Änderungen verlangen DocFX plus repräsentative axe- und
`lynx`-Evidence. CLI-/IDE-Änderungen verlangen farbunabhängige Ursache, Status,
nächste Aktion und gegebenenfalls Tastatur-/Fokusprüfung. Kritische oder
schwerwiegende A11Y-Befunde blockieren.

## Serialisierte Writer / Serialized Writers

Evidence, IDE-Version und Statistik dürfen nicht parallel geschrieben werden.
Jeder Writer protokolliert Eingabehash, Ausgabehash und Abschluss, bevor der
nächste abhängige Task beginnt:

1. Evidence writer: temp JSON → Schema/ID/Link-Check → atomarer Replace →
   Markdown-Ableitung.
2. Version writer: alle drei Felder → pro frühem TDD-Aufruf genau ein
   Build/Test; für den finalen Kandidaten genau ein Versionscommit und ein
   vollständiger Release-/Coverage-Testaufruf → Log → Freigabe.
3. Statistics writer: finale Zeilenzählung → ein chronologischer Eintrag →
   Renderer → `current: true`.
4. Generator writer, falls Finding bestätigt: zwölf Quellen → temporärer
   Sammelband → Check → atomarer Replace; nie direkter Edit.

## Gate-Evidence-Format / Gate Evidence Format

Spätere Implementierung verwendet den vorhandenen Validator:

```powershell
pwsh -NoProfile -Command '& ./.specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.ps1 -Requirements specs/004-secure-development-hardening/gate-requirements.json -Evidence /private/tmp/tinypl0-004-premerge-gate-evidence.json -Head (git rev-parse HEAD)'
```

Die Evidence-Datei bleibt temporär bis zum kausalen Closeout. Jede
`Applicable`-Gate-ID braucht einen passenden Eintrag; jede `N/A`-ID braucht
Begründung und Wiedervorlage.

## Phasenergebnisse / Phase Results

```powershell
pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; $phase = @($state.routing.phases | Where-Object phaseId -eq "plan"); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical plan phase is not Completed" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical plan result hash drift" }; "PASS: immutable historical plan result"'
pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; foreach ($phaseId in @("specify", "plan-review", "tasks")) { $phase = @($state.routing.phases | Where-Object phaseId -eq $phaseId); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical phase is not Completed: $phaseId" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical phase-result hash drift: $phaseId" } }; "PASS: immutable historical Specify, Plan Review, and Tasks results"'
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/clarify.result.json -PhaseId clarify -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/checklist.result.json -PhaseId checklist -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/analyze.result.json -PhaseId analyze -ExitCode 0
```

Plan Review und Analyze bestehen nur ohne offene Critical-, High- oder
Medium-Konsistenzbefunde. Diese Befehle ändern den Run-State nicht.
Specify, Plan, Plan Review und Tasks bleiben unveränderliche historische
Ergebnisdateien; Analyze bindet die finalen minimalen Planungs-/Taskdeltas.
