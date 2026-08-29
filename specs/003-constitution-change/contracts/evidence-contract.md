# Evidenzvertrag / Evidence Contract

**Feature**: `003-constitution-change`
**Gate-Anforderungen / Gate requirements**: `specs/003-constitution-change/gate-requirements.json`
**Späteres Ledger / Later ledger**: `specs/003-constitution-change/autonomous-run-evidence.md`

## Zweck / Purpose

Dieser Vertrag legt vor der Implementierung fest, welche Befehle, Plattformen,
Ergebnisse und Artefakte den Plan belegen. Ein Exitcode 0 allein reicht nicht:
Das Ledger muss Befehl, Trigger, Plattform, Exitcode, Fehlerkanal, Resultat und
Evidence-Referenz enthalten. / This contract declares commands, platforms,
results, and artefacts before implementation. Exit zero alone is insufficient;
the ledger records command, trigger, platform, exit, error channel, result, and
evidence reference.

## Evidence-Regeln / Evidence Rules

1. Das Ledger wird nach `autonomous-run-evidence-template.md` angelegt, bevor
   die erste Implementierungsdatei geändert wird.
2. Jeder Befehl wird aus dem Repository-Root ausgeführt.
3. Jeder `dotnet build`- oder `dotnet test`-Aufruf erhält vorher einen eigenen
   manuellen IDE-Buildzähler-Inkrement. Die drei Versionsfelder bleiben gleich.
4. Die Rot-Ausführung ist erwartbar nicht erfolgreich und wird als TDD-
   Vorherzustand erfasst, nicht als bestandenes Delivery-Gate.
5. Die finale Gate-Evidence verwendet Schema 2.0, bindet den exakten geprüften
   Head und den normalisierten Hash von `gate-requirements.json` und besitzt je
   Gate genau eine `Primary`-Zeile.
6. Temporäre Logs und `_site/` bleiben außerhalb des Delivery-Sets. Getrackte
   `api/`-Metadaten werden als erzeugte Ableitung geprüft.
7. Eine fehlende Werkzeugvoraussetzung ist `Blocked`, niemals ein stilles
   `Pass` oder ein nachträglich erfundenes `N/A`.

*Create the ledger before implementation, run from repository root, increment
the IDE build counter before every build/test, record red as expected negative
evidence, bind final schema-2.0 evidence to the exact head and requirements
hash, and fail closed on missing tools.*

## Befehlsverzeichnis / Command Catalogue

### CMD-01 — Constitution und Spiegel / Constitution and Mirror

```powershell
pwsh -NoProfile -Command '$a = Get-Content -LiteralPath constitution.md -Raw -Encoding UTF8; $b = Get-Content -LiteralPath .specify/memory/constitution.md -Raw -Encoding UTF8; if ($a -cne $b) { throw "Constitution mirror differs" }; if ($a -notmatch "### I\. Security-First \(NON-NEGOTIABLE\)") { throw "Security-First Principle I missing" }; if ($a -notmatch "Didaktische und sprachliche Klarheit / Pedagogical and Linguistic Clarity") { throw "TinyPl0 pedagogical addendum missing" }; if ($a -notmatch "manifest-bound active intake" -or $a -notmatch "fully merged" -or $a -notmatch "post-merge archival") { throw "Governed intake archival rule missing" }'
```

Erwartung: Exit 0 auf macOS mit PowerShell 7. / Expected: exit zero on macOS
with PowerShell 7.

### CMD-02 — Standard-Acht-Preset-Matrix / Standard Eight-Preset Matrix

```powershell
pwsh -NoProfile -Command '$ErrorActionPreference = "Stop"; $matrix = Get-Content "scripts/config/spec-kit-governance-presets.json" -Raw | ConvertFrom-Json; $registry = Get-Content ".specify/presets/.registry" -Raw | ConvertFrom-Json; $expected = @($matrix.presets); $expectedIds = @($expected.id); foreach ($preset in $expected) { $property = $registry.presets.PSObject.Properties[[string]$preset.id]; if ($null -eq $property) { throw "Standard-Preset fehlt / missing: $($preset.id)" }; $actual = $property.Value; if (([string]$actual.version).TrimStart("v") -ne ([string]$preset.version).TrimStart("v") -or [int]$actual.priority -ne [int]$preset.priority -or $actual.enabled -ne $true) { throw "Standard-Preset weicht ab / mismatch: $($preset.id)" }; if (-not (Test-Path ".specify/presets/$($preset.id)/preset.yml" -PathType Leaf)) { throw "preset.yml fehlt / missing: $($preset.id)" } }; $optional = @($registry.presets.PSObject.Properties.Name | Where-Object { $_ -notin $expectedIds } | Sort-Object); "PASS: $($expected.Count) Standard-Presets stimmen; optionale Presets / standard presets match; optional presets: $($optional -join ", ")"'
```

Erwartung: Die acht aktiven Standard-Presets stimmen bei ID, Version und
Priorität mit `scripts/config/spec-kit-governance-presets.json` überein.
Separat verwaltete optionale Registry-Einträge werden transparent gemeldet und
blockieren nur bei Kollision mit dem Standardprofil. / The eight active
standard presets match the executable matrix. Separately governed optional
registry entries are reported and block only if they conflict with the
standard profile.

### CMD-03 — Homogenität ohne Mutation / Non-mutating Homogeneity

```powershell
pwsh -NoProfile -File scripts/check-homogeneity.ps1 -TargetDir . -Json -DryRun -NoPatch
```

Erwartung: Exit 0, kein Patch, kein Commit und keine Statistikmutation. Das
JSON und der semantische Review belegen die fünf Agentenflächen und Templates.

### CMD-04 — TDD Rot und Grün / TDD Red and Green

Vor jedem Aufruf IDE-Buildzähler erhöhen. / Increment the IDE build counter
before each invocation.

```text
dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.ArchitectureGuardTests.Product_Projects_Enable_Public_Xml_Documentation_Warnings"
```

- Rot: nicht nuller Exit, Assertion nennt mindestens eine `1591`-Unterdrückung.
- Grün: Exit 0 nach Entfernung aller vier Unterdrückungen.

*Red is the expected failing configuration state; green is the same command
passing after all four project suppressions are removed.*

### CMD-05 — Restore, Build und Gesamttest / Restore, Build, and Full Test

```text
dotnet restore TinyPl0.sln
dotnet build TinyPl0.sln --configuration Release --no-restore
dotnet test TinyPl0.sln --configuration Release
```

Erwartung: jeder Befehl Exit 0; Build ohne CS1591 und ohne fatalen Fehlerkanal.
Build und Test haben jeweils ihren eigenen vorherigen Buildzähler-Inkrement.

### CMD-06 — Coverage mit Mindestwert und Ziel / Coverage Minimum and Target

```text
pwsh -NoProfile -Command 'if (Test-Path -LiteralPath /private/tmp/tinypl0-003-constitution-change-064927e0-coverage) { throw "Coverage evidence path already exists" }'
dotnet test TinyPl0.sln --configuration Release --collect:"XPlat Code Coverage" --results-directory /private/tmp/tinypl0-003-constitution-change-064927e0-coverage
```

```powershell
pwsh -NoProfile -Command '$files = @(Get-ChildItem -LiteralPath /private/tmp/tinypl0-003-constitution-change-064927e0-coverage -Recurse -Filter coverage.cobertura.xml); if ($files.Count -eq 0) { throw "No Cobertura coverage file" }; $rates = @($files | ForEach-Object { $xml = [xml](Get-Content -LiteralPath $_.FullName -Raw -Encoding UTF8); [double]$xml.coverage."line-rate" }); $rate = ($rates | Measure-Object -Minimum).Minimum; if ($rate -lt 0.70) { throw "Coverage below 70 percent: $rate" }; $target = if ($rate -ge 0.80) { "TargetMet" } else { "TargetOpen" }; Write-Output ("line-rate={0:F4};minimum=0.70;target=0.80;targetState={1}" -f $rate, $target)'
```

Erwartung: Mindestwert mindestens 0,70. Ein offenes 0,80-Ziel wird im Ledger
genannt, blockiert aber nicht das verfassungsmäßige Mindestgate.

### CMD-07 — NuGet-Review / NuGet Review

```text
dotnet list TinyPl0.sln package --outdated --include-transitive
dotnet list TinyPl0.sln package --vulnerable --include-transitive
```

Erwartung: keine neue Abhängigkeit; kein bekannter kritischer Fund. Bestehende
Outdated-Funde werden als Bestand dokumentiert und nicht scopewidrig aktualisiert.

### CMD-08 — DocFX

```text
docfx docfx.json
```

Erwartung: Exit 0, aktuelle `api/.manifest`- und `api/**/*.yml`-Ableitungen und
eine vollständige temporäre `_site/`-Website.

### CMD-09 — Lokaler DocFX-Server / Local DocFX Server

```text
python3 -m http.server 8080 --bind 127.0.0.1 --directory _site
```

Erwartung: nur Loopback-Bindung; die Evidence nennt Prozess-ID bzw. Session und
beendet den Server nach CMD-10/CMD-11. / Loopback only; stop after auditing.

### CMD-10 — Playwright/axe auf Node 24 LTS / Playwright/axe on Node 24 LTS

Zuerst muss `node --version` mit `v24.` beginnen. / `node --version` must start
with `v24.`.

```text
audit_dir="$(mktemp -d)"; printf 'audit_dir=%s\n' "$audit_dir"; (cd "$audit_dir" && npm init -y && npm install --save-exact --ignore-scripts @playwright/test@1.62.1 @axe-core/playwright@4.13.0 && npm exec playwright install chromium && node --input-type=module --eval 'import { chromium } from "playwright"; import AxeBuilder from "@axe-core/playwright"; const browser = await chromium.launch({ headless: true }); const urls = ["http://127.0.0.1:8080/index.html", "http://127.0.0.1:8080/api/Pl0.Core.Pl0Compiler.html", "http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachine.html"]; let failed = false; for (const url of urls) { const context = await browser.newContext(); const page = await context.newPage(); await page.goto(url, { waitUntil: "networkidle" }); const result = await new AxeBuilder({ page }).analyze(); console.log(JSON.stringify({ url, violations: result.violations.map(v => ({ id: v.id, impact: v.impact, nodes: v.nodes.length })) })); failed ||= result.violations.length > 0; await context.close(); } await browser.close(); if (failed) process.exit(1);')
```

Erwartung: Exit 0 und für jede Seite `violations: []`. Das temporäre Verzeichnis
liegt außerhalb des Repositories und wird exakt entfernt. / Zero violations;
the exact temporary directory is removed.

### CMD-11 — `lynx`-Textpfad / `lynx` Text Path

```text
lynx -dump -nolist http://127.0.0.1:8080/index.html > /tmp/tinypl0-docfx-index.txt; lynx -dump -nolist http://127.0.0.1:8080/api/Pl0.Core.Pl0Compiler.html > /tmp/tinypl0-docfx-core.txt; lynx -dump -nolist http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachine.html > /tmp/tinypl0-docfx-vm.txt; rg -n "TinyPl0|Pl0Compiler|VirtualMachine" /tmp/tinypl0-docfx-index.txt /tmp/tinypl0-docfx-core.txt /tmp/tinypl0-docfx-vm.txt
```

Erwartung: drei nicht leere Textausgaben und Treffer für die verständlichen
Seiten-/API-Bezeichnungen. Ein visueller Screenshot ersetzt diesen Textpfad
nicht.

### CMD-12 — Statistik / Statistics

Nach dem neuen chronologischen Ledger-Eintrag: / After adding the chronological
ledger entry:

```powershell
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo .
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly -Json
```

Erwartung: Renderer Exit 0, anschließend `current: true`; Gesamtstatistik bleibt
letzter Top-Level-Abschnitt.

### CMD-13 — IDE-Version / IDE Version

```powershell
pwsh -NoProfile -Command '$xml = [xml](Get-Content -LiteralPath src/Pl0.Ide/Pl0.Ide.csproj -Raw -Encoding UTF8); $version = [version]([string]$xml.Project.PropertyGroup.Version); $assembly = [version]([string]$xml.Project.PropertyGroup.AssemblyVersion); $file = [version]([string]$xml.Project.PropertyGroup.FileVersion); $commits = [int](git rev-list --count HEAD); if ($version -ne $assembly -or $version -ne $file) { throw "IDE version fields differ" }; if ($version.Major -ne 1 -or $version.Minor -ne 3 -or $version.Build -ne $commits) { throw "IDE version does not match 1.3.<commit-count>.<build-counter>" }; Write-Output $version.ToString(4)'
```

Hinweis: .NET `Version.Build` entspricht hier dem dritten TinyPl0-Feld
`Patch`; `Version.Revision` entspricht dem manuellen vierten Feld `Build`.

### CMD-14 — Plan-, Review-, Tasks- und Analyze-Ergebnisse / Plan, Review, Tasks, and Analyze Results

```powershell
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/plan.result.json -PhaseId plan -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/plan-review.result.json -PhaseId plan-review -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/tasks.result.json -PhaseId tasks -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/analyze.result.json -PhaseId analyze -ExitCode 0
```

Das Plan-Review besteht nur ohne offene Critical-, High- oder Medium-Befunde;
`plan-review.md` ist der Payload des zweiten Ergebnisses. Der Tasks-Payload
enthält exakt T001–T073; der Analyze-Payload `analyze-report.md` besteht nur
ohne offene Critical-, High- oder Medium-Befunde. / The plan review passes only
with no unresolved Critical, High, or Medium finding. The Tasks payload contains
exactly T001–T073, and the Analyze payload passes only with no unresolved
Critical, High, or Medium finding.

### CMD-15 — Delivery-Set / Delivery Set

```powershell
pwsh -NoProfile -Command '& ./.specify/presets/autonomous-run-governance/scripts/validate-autonomous-delivery-set.ps1 -Repo . -Intended @("specs/003-constitution-change/spec.md", "specs/003-constitution-change/clarification-report.md", "specs/003-constitution-change/checklists/requirements.md", "specs/003-constitution-change/checklists/autonomous-readiness.md", "specs/003-constitution-change/autonomous-run-state.json", "specs/003-constitution-change/plan.md", "specs/003-constitution-change/research.md", "specs/003-constitution-change/data-model.md", "specs/003-constitution-change/quickstart.md", "specs/003-constitution-change/gate-requirements.json", "specs/003-constitution-change/contracts/evidence-contract.md", "specs/003-constitution-change/plan-review.md", "specs/003-constitution-change/tasks.md", "specs/003-constitution-change/analyze-report.md", "specs/003-constitution-change/autonomous-run-evidence.md")'
```

`-Intended` benennt nur die ausdrücklich erlaubten unversionierten Dateien;
geänderte getrackte Dateien ermittelt der Validator selbst. Die Liste ist vor
dem ersten Implementierungs-Edit in `tasks.md` gegen den dann realen
unversionierten Bestand zu bestätigen. Eine Änderung dieser Liste verlangt
Review und einen neuen normalisierten Gate-Requirements-Hash. / `-Intended`
names only explicitly allowed untracked files; the validator discovers changed
tracked files itself. Confirm the list before implementation; changing it
requires review and a new requirements hash.

Danach / Then:

```text
git diff --cached --check
git status --short
```

### CMD-16 — Gate-Evidence / Gate Evidence

```powershell
pwsh -NoProfile -Command '& ./.specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.ps1 -Requirements specs/003-constitution-change/gate-requirements.json -Evidence /tmp/003-constitution-change.premerge-gate-evidence.json -Head (git rev-parse HEAD)'
```

Die temporäre Evidence wird nicht vor dem Merge commitet. / Do not commit the
temporary exact-head evidence before merge.

### CMD-17 — Remote-Konvergenz / Remote Convergence

```text
gh pr checks --required
gh pr view --json number,url,headRefOid,reviewDecision,statusCheckRollup,reviews,mergeStateStatus
```

Nur in der autorisierten Remote-Phase. Fehlender Review oder Check ist kein
Pass. / Authorized remote phase only; missing review/check is not a pass.

### CMD-18 — Merge und Sync / Merge and Sync

```text
gh pr merge --merge --delete-branch
gh repo sync --branch main
```

Danach Run-Zustand, PostMerge-Evidence und alle vier Closeout-Felder validieren.

## Erforderliche Artefakt-Evidenz / Required Artefact Evidence

| Evidence | Pfad oder Referenz / Path or reference | Erfolgsregel / Success rule |
|---|---|---|
| Plan-Phase | `.specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/plan.result.json` | gültiger normalisierter Hash von `plan.md` |
| Plan-Review | `specs/003-constitution-change/plan-review.md` und `.specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/plan-review.result.json` | keine offenen Critical-/High-/Medium-Befunde; gültiger normalisierter Payload-Hash |
| Tasks | `specs/003-constitution-change/tasks.md` und `.specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/tasks.result.json` | exakt 73 eindeutige, abhängigkeitsgeordnete Aufgaben; gültiger normalisierter Payload-Hash |
| Analyze | `specs/003-constitution-change/analyze-report.md` und `.specify/runtime/autonomous-routing/064927e0-8389-4692-a53c-f1ce79e6043d/analyze.result.json` | keine offenen Critical-/High-/Medium-Befunde; gültiger normalisierter Payload-Hash |
| TDD | autonomes Ledger | beobachtetes Rot, Grün und Regression |
| XML-Build | Buildlog/CI-Job | keine CS1591-Unterdrückung, keine CS1591-Warnung |
| Coverage | Cobertura + Ledger | `>=70%`, Zielstatus für `>=80%` |
| DocFX | `api/`, DocFX-Log | Exit 0, Ableitungen aktuell |
| A11Y | axe-JSON-Zeilen + drei `lynx`-Dumps | 0 axe-Verletzungen, verständlicher Text |
| Parität | Preset-/Homogenitätsoutput + Review | alle betroffenen Flächen semantisch gleich |
| Security | Ledger/PR | NIST SSDF + CWE Top 25 geprüft; bedingte Standards begründet N/A |
| Statistik | `docs/project-statistics.md` + CheckOnly | ein neuer Eintrag, Profil 2 aktuell |
| Version | `Pl0.Ide.csproj` + CMD-13 | drei Felder identisch, `1.3.<count>.<counter>` |
| Delivery | temporäre Schema-2.0-Evidence | exakter Head, jede Gate-ID genau einmal Primary |

## Fehler- und Stoppregeln / Failure and Stop Rules

- CS1591, DocFX-, axe-, `lynx`-, Coverage-, Paritäts-, Statistik-, Versions-
  oder Validatorfehler blockieren den Abschluss.
- Ein kritischer NuGet-Vulnerability-Fund blockiert und verlangt neue
  Autorisierung; keine stille Paketänderung.
- Eine unerwartete Produktlogik-, API-Signatur-, Skript-, Workflow-, Trust-
  Boundary- oder Dependency-Änderung stoppt die Implementierung und löst eine
  Scope-/Gate-Neubewertung aus.
- Ein Stop-Request wird am nächsten sicheren Phasenrand behandelt; Fortsetzung
  nur über die vorgesehene Resume-Phase.

*Failures in any applicable gate block completion. Unexpected scope changes or
critical vulnerabilities require re-evaluation rather than silent expansion.*
