# Quickstart: Secure-Development-Härtung / Secure Development Hardening

Diese Befehlsfolge ist für die spätere Implementierungsphase. Die Planphase
führt keine Produkt-, Workflow-, Intake-, Serien- oder Run-State-Änderung aus.
Jeder Build/Test erhält vorher einen serialisierten IDE-Buildzähler-Write.

## 1. Identität und Eingangsevidenz / Identity and Input Evidence

```powershell
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.ps1 -State specs/004-secure-development-hardening/autonomous-run-state.json
pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; if ($state.runId -ne "abaa7b81-fd2c-47e7-8d59-87a852a3b2e7" -or $state.featurePath -ne "specs/004-secure-development-hardening" -or $state.branch -ne "codex/004-secure-development-hardening") { throw "Feature identity mismatch" }; foreach ($item in $state.acceptedArtifacts) { $actual = (Get-FileHash -LiteralPath $item.path -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $item.sha256) { throw "Accepted input drift: $($item.path)" } }; "PASS: identity and accepted byte hashes"'
```

Erwartung: Exit `0`; Plan-/Implementierungsphase und vier Hashes stimmen. Der
zweite Befehl ist bytegenau; bereits akzeptierte normalisierte Ergebnisse
werden zusätzlich über ihre Phase-Result-Validatoren gebunden.

## 2. Plan-, Review-, Tasks- und Analyze-Schranke / Planning Convergence Gate

```powershell
pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; $phase = @($state.routing.phases | Where-Object phaseId -eq "plan"); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical plan phase is not Completed" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical plan result hash drift" }; "PASS: immutable historical plan result"'
pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; foreach ($phaseId in @("specify", "plan-review", "tasks")) { $phase = @($state.routing.phases | Where-Object phaseId -eq $phaseId); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical phase is not Completed: $phaseId" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical phase-result hash drift: $phaseId" } }; "PASS: immutable historical Specify, Plan Review, and Tasks results"'
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/clarify.result.json -PhaseId clarify -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/checklist.result.json -PhaseId checklist -ExitCode 0
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/analyze.result.json -PhaseId analyze -ExitCode 0
```

Keine Implementierung vor unveränderten historischen Ergebnisdateien,
aktuellen Clarify-/Checklist-/Analyze-Ergebnissen sowie null offenen Critical-,
High- oder Medium-Konsistenzbefunden. Analyze bindet die minimalen Änderungen
an Planungs- und Taskartefakten, ohne historische Phasenergebnisse
umzuschreiben. / Analyze causally binds final planning/task remediation while
accepted predecessor results remain immutable historical evidence.

## 3. 157 kanonische IDs prüfen / Validate the 157 Canonical IDs

```powershell
pwsh -NoProfile -Command '$files = @(Get-ChildItem -LiteralPath docs/secure-development/checklisten -Filter "CL_*.md" | Sort-Object Name); if ($files.Count -ne 12) { throw "Expected 12 canonical checklists" }; $sets = foreach ($file in $files) { $ids = @([regex]::Matches((Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8), "(?m)^#### (CL-[0-9]{2}-[0-9]{2}):") | ForEach-Object { $_.Groups[1].Value }); [pscustomobject]@{ Name = $file.Name; Count = $ids.Count; Ids = $ids } }; $counts = @($sets | ForEach-Object { $_.Count }); $all = [string[]]@($sets.Ids); $unique = @($all | Sort-Object -Unique); $book = [string[]]@([regex]::Matches((Get-Content -LiteralPath docs/secure-development/Checklistensammelband_Sichere-Entwicklung.md -Raw -Encoding UTF8), "(?m)^#### (CL-[0-9]{2}-[0-9]{2}):") | ForEach-Object { $_.Groups[1].Value }); if (($counts -join "/") -ne "12/13/15/10/13/11/12/13/17/17/12/12" -or $all.Count -ne 157 -or $unique.Count -ne 157 -or -not [Linq.Enumerable]::SequenceEqual($all, $book)) { throw "Checklist inventory or ordered compendium parity failed" }; "PASS: counts=$($counts -join "/"); total=157; unique=157; ordered compendium parity"'
```

Nach Erstellung der Prüfinstanz:

```powershell
pwsh -NoProfile -Command '$json = Get-Content -LiteralPath docs/security/secure-development/2026-08-30-tinypl0-hardening/assessment.json -Raw -Encoding UTF8; if (-not ($json | Test-Json -SchemaFile specs/004-secure-development-hardening/contracts/assessment-record.schema.json)) { throw "Assessment schema failed" }; $data = $json | ConvertFrom-Json; $ids = @($data.items.clId); if (@($ids | Sort-Object -Unique).Count -ne 157) { throw "Assessment IDs are not unique" }; $cl12 = @($data.items | Where-Object { $_.clId -like "CL-12-*" }); if ($cl12.Count -ne 12 -or @($cl12 | Where-Object { $_.applicability -ne "N/A" -or $_.implementation -ne "Not Assessed" }).Count -ne 0) { throw "CL-12 boundary failed" }; "PASS: assessment schema, 157 unique IDs, CL-12 boundary"'
```

## 4. Befundschranke / Finding Gate

Vor jeder Nicht-VM-Änderung:

```powershell
pwsh -NoProfile -Command '$findings = Get-Content -LiteralPath docs/security/secure-development/2026-08-30-tinypl0-hardening/assessment.json -Raw -Encoding UTF8 | ConvertFrom-Json; $bad = @($findings.items | Where-Object { $null -ne $_.findingId -and ($_.applicability -ne "Applicable" -or $_.implementation -notin @("Partly Fulfilled", "Not Fulfilled")) }); if ($bad.Count) { throw "Finding authorisation state invalid" }; "PASS: finding status precondition"'
```

Zusätzlich muss `findings.md` Risiko, roten Test, exakte Dateien, kleinste
Maßnahme, Regression, Owner und Reviewer enthalten. Fehlt ein Feld, bleibt der
Befund `Proposed` und es erfolgt kein Edit.

## 5. Repräsentatives VM-Rot / Representative VM Red

Zuerst nur die neuen Tests ergänzen. Direkt vor jedem folgenden `dotnet test`
erhöht der Version-Writer `Version`, `AssemblyVersion` und `FileVersion`
gemeinsam auf
`1.<read-only bestätigte PR-Nummer>.<Commitzähler>.<nächster-Buildzähler>`.
Der am 2026-08-30 erneut bestätigte Slot ist `72`, niemals Feature `004`.
Vor dem ersten Versionierungscommit und an der Delivery-Grenze wird der Slot
erneut gelesen; existiert die Feature-PR bereits, ist ihre Nummer bindend.

```text
dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.VirtualMachineTests.Instruction_Budget_Stops_Before_N_Plus_One|FullyQualifiedName~Pl0.Tests.SteppableVirtualMachineTests.Instruction_Budget_Stops_Before_N_Plus_One"
```

Erwartung Rot: nur die neue Budgetanforderung scheitert. Ein äußerer Harness-
Timeout schützt vor der heutigen Endlosschleife; Restore-/Compiler-/Toolfehler
zählen nicht. Testquellhash protokollieren.

Separate Options-Rotfälle:

```text
dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.VirtualMachineTests.Invalid_Options_Return_Diagnostic_Before_Allocation|FullyQualifiedName~Pl0.Tests.SteppableVirtualMachineTests.Invalid_Options_Return_Diagnostic_Before_Allocation"
```

## 6. Minimales VM-Grün / Minimal VM Green

Nur diese Produktdateien ändern:

```text
src/Pl0.Vm/VirtualMachineOptions.cs
src/Pl0.Vm/VirtualMachine.cs
src/Pl0.Vm/SteppableVirtualMachine.cs
src/Pl0.Vm/Resources/Pl0VmMessages.resx
src/Pl0.Vm/Resources/Pl0VmMessages.en.resx
```

Danach dieselben selektiven Befehle mit unveränderten Tests ausführen. Erwartung:
Exit `0`, Batch/Step gleiche Grenze und Diagnose; Budget `0/-1` und Stack
`0/1/2/1_000_001/int.MaxValue` ohne Additions-, Allokations- oder
Indexexception. Der bisherige Vier-Parameter-Aufruf von
`VirtualMachineOptions` bleibt quellkompatibel.

## 7. VM-, Katalog- und Golden-Regression / VM, Catalogue, and Golden Regression

Der finale Kandidat erhält genau einen Versionscommit: `Minor` ist die direkt
zuvor read-only revalidierte kanonische PR-Nummer, `Patch` ist der kommende und
nach dem Commit geprüfte Branch-Commitcount, und `Build` wird einmal erhöht.
Danach führt genau ein `dotnet test`-Aufruf auf diesem sauberen HEAD Build,
vollständige Suite und Coverage aus:

```powershell
pwsh -NoProfile -Command '& dotnet restore TinyPl0.sln; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; & dotnet test TinyPl0.sln --configuration Release --no-restore --collect:"XPlat Code Coverage" --results-directory /private/tmp/tinypl0-004-abaa7b81-coverage; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; "PASS: one exact-HEAD Release full suite; 41 mandatory cases; VirtualMachineTests; SteppableVirtualMachineTests; L10nTests; CatalogCasesTests; LexerGoldenTests; ParserGoldenTests; TraceabilityMatrixTests; ArchitectureGuardTests; no golden regeneration"'
```

`scripts/update-golden-code.sh` wird nicht ausgeführt: Der Vertrag erwartet
keine Golden-Änderung. Jede Differenz blockiert und benötigt neue Autorität.

## 8. Coverage / Coverage

```powershell
pwsh -NoProfile -Command '$path = "/private/tmp/tinypl0-004-abaa7b81-coverage"; if (Test-Path -LiteralPath $path) { throw "Coverage path already exists" }'
```

Die Coverage stammt ausschließlich aus dem einen finalen Aufruf in Abschnitt
7; hier wird kein zweiter Build/Test gestartet.

```powershell
pwsh -NoProfile -Command '$files = @(Get-ChildItem -LiteralPath /private/tmp/tinypl0-004-abaa7b81-coverage -Recurse -Filter coverage.cobertura.xml); if (-not $files) { throw "No coverage file" }; $docs = @($files | ForEach-Object { [xml](Get-Content -LiteralPath $_.FullName -Raw -Encoding UTF8) }); $overall = ($docs | ForEach-Object { [double]$_.coverage."line-rate" } | Measure-Object -Minimum).Minimum; if ($overall -lt 0.7023) { throw "Coverage below 70.23% baseline: $overall" }; $vm = @($docs.coverage.packages.package.classes.class | Where-Object { $_.filename -like "*src/Pl0.Vm/*" }); if (-not $vm) { throw "No VM coverage entries" }; $branch = ($vm | Measure-Object -Property branch-rate -Minimum).Minimum; if ([double]$branch -lt 0.85) { throw "Changed VM branch coverage below 85%: $branch" }; $target = if ($overall -ge 0.80) { "TargetMet" } else { "TargetOpen" }; "PASS: overall=$overall; floor=0.7023; target=$target; vmBranch=$branch"'
```

## 9. Dependency-, SBOM-, VEX- und SLSA-Pfad / Dependency, SBOM, VEX, and SLSA Path

Read-only Dependency-Inventur:

```text
dotnet list TinyPl0.sln package --outdated --include-transitive
dotnet list TinyPl0.sln package --vulnerable --include-transitive
```

Nur nach `FND-SC-001` und geprüftem Pin:

```text
dotnet tool restore
dotnet tool run dotnet-CycloneDX TinyPl0.sln -o /private/tmp/tinypl0-004-sbom --output-format Json --spec-version 1.7
```

```powershell
pwsh -NoProfile -Command '$bom = @(Get-ChildItem -LiteralPath /private/tmp/tinypl0-004-sbom -Filter *.json); if ($bom.Count -ne 1) { throw "Expected one CycloneDX JSON" }; $data = Get-Content -LiteralPath $bom[0].FullName -Raw -Encoding UTF8 | ConvertFrom-Json; if ([string]$data.bomFormat -ne "CycloneDX" -or -not $data.specVersion -or -not $data.components) { throw "Invalid SBOM content" }; Get-FileHash -LiteralPath $bom[0].FullName -Algorithm SHA256'
```

Bei bekanntem CVE muss ein zulässiger VEX-Status vorliegen; kritische/hohe
offene Funde blockieren. Ohne Fund wird `NotRequiredNoKnownFinding` mit Scan-
Commit, Zeitpunkt und Wiedervorlage dokumentiert. Eine geplante CI-Attestation
ist nur gültig, wenn der Workflow volle Action-SHAs und minimale Permissions
enthält; `gh attestation verify <artifact> --repo hindermath/TinyPl0` ist der
Remote-Verifikationspfad nach einer tatsächlichen, separat autorisierten
Veröffentlichung.

`docs/security/supply-chain-evidence.json` bindet den exakten Feature-HEAD,
Artefaktmanifest- und SBOM-Hash, CycloneDX-Version/Pin, VEX-Stand sowie den nur
tatsächlich belegten SLSA-/Provenienzstand. Die Markdown-Datei bleibt die
DE-zuerst/EN-danach formulierte text-first Sicht.

## 10. Baseline-Generatorparität / Baseline Generator Parity

Nur nach `FND-BASELINE-001`:

```powershell
pwsh -NoProfile -File scripts/build-secure-development-docs.ps1 -Check
pwsh -NoProfile -File scripts/build-secure-development-docs.ps1 -WhatIf
pwsh -NoProfile -Command '. ./scripts/build-secure-development-docs.ps1; $cmd = Get-Command Build-SecureDevelopmentDocs -ErrorAction Stop; if (-not $cmd.Parameters.ContainsKey("WhatIf")) { throw "Cmdlet lacks WhatIf" }; Get-Help Build-SecureDevelopmentDocs -Full'
```

```text
bash scripts/build-secure-development-docs.sh --check
bash scripts/build-secure-development-docs.sh --dry-run
```

Erwartung: identische ID-Menge, Reihenfolge, Versionen, Exitcodes und
Output-Hashes; PowerShell verwendet StrictMode, Bash `set -euo pipefail` und
gequotete Variablen. Manpage und DE-/EN-Hilfe werden manuell auf macOS/Linux,
PowerShell zusätzlich auf Windows geprüft.

## 11. ASVS, Architektur und Security-Evidence / ASVS, Architecture, and Security Evidence

```powershell
pwsh -NoProfile -Command '$required = @("docs/security/threat-model.md", "docs/security/arc42-security.md", "docs/security/security-checklist.md", "docs/security/security-quality-scenarios.md", "docs/security/dependency-audit.md", "docs/security/asvs-verification.md", "docs/security/supply-chain-evidence.md", "docs/security/zero-trust-applicability.md", "docs/security/samm-assessment.md", "docs/architecture/secure-development-hardening.md", "docs/architecture/adr/0001-vm-resource-budget.md", "docs/security/adr/0001-vm-resource-budget.md"); foreach ($path in $required) { if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing evidence: $path" }; $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8; if ($text -match "Stub|To be populated|Zu befuellen|TBD") { throw "Unresolved evidence placeholder: $path" } }; "PASS: security and architecture evidence paths"'
```

ASVS-Evidence muss alle 70 L1-IDs in
`docs/security/asvs-verification.json` enthalten; jede Zeile ist `Applicable`
oder begründet `N/A`, alle Applicable-Zeilen sind `Fulfilled`, und es bleiben
keine kritischen/hohen HTTP-Funde. Der Validator lädt die offizielle, auf
`v5.0.0` gepinnte Flat-JSON-Quelle read-only, prüft deren SHA-256 und vergleicht
Menge sowie Reihenfolge der 70 IDs. Eine HTTP-Produktänderung braucht
zusätzlich `FND-HTTP-001` und einen roten Test.

## 12. DocFX, axe und lynx / DocFX, axe, and lynx

```text
docfx docfx.json
npm --prefix tests/a11y ci
npm --prefix tests/a11y test -- --project=chromium
```

Der nach `FND-A11Y-001` eingecheckte, lockfile-gebundene Node-24-Harness startet
den Server ausschließlich auf `127.0.0.1`, prüft repräsentativ und beendet den
Prozess auch im Fehlerfall:

```text
http://127.0.0.1:8080/index.html
http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachineOptions.html
http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachine.html
```

Erwartung axe: keine Critical-/Serious-Verletzung und keine neue andere
Verletzung. Textpfad:

```text
lynx -dump -nolist http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachineOptions.html
lynx -dump -nolist http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachine.html
```

## 13. Agentenparität, Statistik und Version / Agent Parity, Statistics, and Version

```powershell
pwsh -NoProfile -File scripts/check-homogeneity.ps1 -TargetDir . -Json -DryRun -NoPatch
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo .
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly -Json
pwsh -NoProfile -Command '$repo = "hindermath/TinyPl0"; $branch = "codex/004-secure-development-hardening"; $head = (git rev-parse HEAD).Trim(); $current = @(gh pr list --repo $repo --head $branch --state all --limit 100 --json number,headRefOid | ConvertFrom-Json); if ($current.Count -gt 1) { throw "Multiple PRs use the feature branch" }; if ($current.Count -eq 1) { if ($current[0].headRefOid -ne $head) { throw "Feature PR is not at exact HEAD" }; $expectedMinor = [int]$current[0].number } else { $all = @(gh pr list --repo $repo --state all --limit 1000 --json number | ConvertFrom-Json); $expectedMinor = 1 + [int](($all.number | Measure-Object -Maximum).Maximum) }; $xml = [xml](Get-Content -LiteralPath src/Pl0.Ide/Pl0.Ide.csproj -Raw -Encoding UTF8); $v = [version]([string]$xml.Project.PropertyGroup.Version); $patch = [int](git rev-list --count HEAD); if ($v -ne [version]([string]$xml.Project.PropertyGroup.AssemblyVersion) -or $v -ne [version]([string]$xml.Project.PropertyGroup.FileVersion)) { throw "IDE version fields differ" }; if ($v.Major -ne 1 -or $v.Minor -ne $expectedMinor -or $v.Build -ne $patch) { throw "IDE version does not match canonical PR number and exact-HEAD commit count" }; "PASS: version=$($v.ToString(4)); canonical-pr=$expectedMinor; head=$head"'
```

Statistik: genau ein neuer chronologisch letzter Eintrag, Basen `80`/`125`,
7,8 Stunden/Tag, blended repository speedup; `## Gesamtstatistik` bleibt letzter
Top-Level-Abschnitt.

## 14. Gate- und Delivery-Evidence / Gate and Delivery Evidence

```powershell
pwsh -NoProfile -Command '& ./.specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.ps1 -Requirements specs/004-secure-development-hardening/gate-requirements.json -Evidence /private/tmp/tinypl0-004-premerge-gate-evidence.json -Head (git rev-parse HEAD)'
git diff --check
git status --short
```

Die temporäre Gate-Evidence wird nicht als kausale Post-Merge-Evidence
ausgegeben. Remote-, Merge- und Closeout-Schritte gehören nicht zu Plan oder
diesem Quickstart ohne die spätere, revalidierte Autorität.
