# Quickstart für die spätere Umsetzung / Quickstart for Later Implementation

## Zweck / Purpose

Diese Anleitung ist **kein Auftrag zur sofortigen Implementierung**. Sie darf
erst nach bestandenem Plan-Review, `/speckit.tasks`, Analyze-Konvergenz und
erneuter Autoritätsprüfung verwendet werden. / This guide is **not authority to
implement now**. Use it only after plan review, tasks, analyze convergence, and
authority revalidation.

Die exakten vollständigen Befehle und Erfolgsregeln stehen im
[Evidenzvertrag](contracts/evidence-contract.md). / Exact full commands and
success rules are in the evidence contract.

## 1. Voraussetzungen und Zustand / Prerequisites and State

```text
git branch --show-current
git status --short
dotnet --version
pwsh -NoProfile -Command '$PSVersionTable.PSVersion.ToString()'
docfx --version
node --version
lynx --version
```

Erwartung / Expected:

- Branch `codex/003-constitution-change`;
- valider aktiver Run `064927e0-8389-4692-a53c-f1ce79e6043d`;
- .NET SDK `10.0.x`, PowerShell 7, DocFX;
- Node `v24.x` für den A11Y-Lauf und verfügbares `lynx`;
- keine unbekannte Änderung im beabsichtigten Delivery-Set.

Wenn Node nicht mit `v24.` beginnt, wird der A11Y-Schritt als `Blocked`
behandelt, bis ein passender Runner bereitsteht. Es wird nicht still mit einer
anderen Hauptversion weitergearbeitet. / If Node is not v24, fail closed until
a matching runner is available.

Run-Zustand prüfen / Validate run state:

```powershell
pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.ps1 -State specs/003-constitution-change/autonomous-run-state.json
```

## 2. Evidence vor dem ersten Edit / Evidence Before the First Edit

1. `specs/003-constitution-change/autonomous-run-evidence.md` aus dem
   installierten Template erzeugen.
2. `gate-requirements.json` und dessen normalisierten SHA-256 in das Ledger
   übernehmen.
3. Die in `tasks.md` festgelegte unversionierte Delivery-Liste gegen den realen
   Status bestätigen.
4. Keine Datei außerhalb der akzeptierten Feature-Flächen aufnehmen.

*Create the ledger, bind the normalized gate-requirements hash, confirm the
exact intended untracked set, and preserve unrelated work.*

## 3. Governance zuerst / Governance First

In einer atomaren Änderung:

1. TinyPl0-Level-2-Addendum ergänzen; Security-First-Prinzip I unverändert
   lassen.
2. Constitution-SemVer als MINOR erhöhen und Spiegel bytegleich halten.
3. Fünf Agentenflächen um dieselbe didaktische, XML-, TDD- und A11Y-Regel
   ergänzen.
4. README und dokumentierte Preset-Matrizen auf die ausführbare Konfiguration
   ausrichten.
5. Betroffene Spec-Kit- und Bootstrap-Templates synchronisieren; nicht
   betroffene Templates nur als geprüft dokumentieren.

Erste lokale Nachweise / First local evidence:

```powershell
# CMD-02 aus contracts/evidence-contract.md prueft die acht Standard-Presets
# und meldet separat verwaltete optionale Presets transparent.
# CMD-02 from contracts/evidence-contract.md validates the standard eight and
# reports separately governed optional presets transparently.
pwsh -NoProfile -File scripts/check-homogeneity.ps1 -TargetDir . -Json -DryRun -NoPatch
```

## 4. IDE-Version vor Build/Test / IDE Version Before Build/Test

Vor dem ersten `dotnet build` oder `dotnet test`:

1. Major `1`, Minor `3` setzen.
2. Patch als vorausberechneten vollständigen Commit-Zähler für den kommenden
   Feature-Commit setzen.
3. Build gegenüber dem aktuellen vierten Feld um eins erhöhen.
4. `Version`, `AssemblyVersion` und `FileVersion` identisch setzen.

Vor **jedem weiteren** Build-/Testaufruf nur den Buildzähler erneut erhöhen.
Nach dem Commit muss Patch exakt `git rev-list --count HEAD` entsprechen.

*Use `1.3.<containing-commit-count>.<manual-build-counter>` and keep all three
version fields identical.*

## 5. TDD Rot / TDD Red

Zuerst ausschließlich den neuen Guard-Test ergänzen. Noch keine `.csproj`-
Unterdrückung entfernen. Dann nach dem Versions-Inkrement:

```text
dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.ArchitectureGuardTests.Product_Projects_Enable_Public_Xml_Documentation_Warnings"
```

Erwartung: nicht nuller Exit, weil vier Produktprojekte `1591` unterdrücken.
Assertion und Exitcode im Ledger erfassen. Ein Kompilierfehler außerhalb dieser
Assertion ist kein gültiges Rot. / A failure unrelated to the intended
assertion is not valid red evidence.

## 6. Grün: Dokumentationsschranke / Green: Documentation Gate

1. `1591` aus `NoWarn` in Core, VM, CLI und IDE entfernen.
2. Öffentliche extern sichtbare APIs prüfen.
3. Fehlende anwendbare XML-Elemente ergänzen. Keine erfundenen Returns oder
   Exceptions; keine lokalen Variablen dokumentieren.
4. Geänderte neue Texte DE zuerst, EN danach auf CEFR B2 schreiben. Bestehende
   Altbestandsübersetzung nicht vorziehen.
5. Nach erneutem Buildzähler-Inkrement denselben Guard-Test ausführen; er muss
   nun Exit 0 liefern.

## 7. Regression und Coverage / Regression and Coverage

Jeder Build-/Testaufruf erhält vorher seinen Buildzähler-Inkrement.

```text
dotnet restore TinyPl0.sln
dotnet build TinyPl0.sln --configuration Release --no-restore
dotnet test TinyPl0.sln --configuration Release
pwsh -NoProfile -Command 'if (Test-Path -LiteralPath /private/tmp/tinypl0-003-constitution-change-064927e0-coverage) { throw "Coverage evidence path already exists" }'
dotnet test TinyPl0.sln --configuration Release --collect:"XPlat Code Coverage" --results-directory /private/tmp/tinypl0-003-constitution-change-064927e0-coverage
```

Danach den Coverage-Parser aus CMD-06 verwenden. Unter 70 % ist blockierend;
70–79,99 % wird als bestandener Mindestwert mit offenem 80-%-Ziel dokumentiert.

## 8. DocFX und textorientierte A11Y / DocFX and Text-Oriented Accessibility

```text
docfx docfx.json
python3 -m http.server 8080 --bind 127.0.0.1 --directory _site
```

In einer zweiten Session CMD-10 und CMD-11 ausführen:

- Playwright/axe mit Node 24 LTS und exakt gepinnten temporären Paketen;
- `lynx -dump` für Startseite, `Pl0Compiler` und `VirtualMachine`;
- danach den lokalen Server beenden;
- temporäre Pfade nur nach exakter Pfadprüfung entfernen.

*Run DocFX, audit three representative pages with Playwright/axe and `lynx`,
then stop the loopback server. Generated `_site/` is evidence, not delivery.*

## 9. Security, Dependencies und Architektur / Security, Dependencies, and Architecture

```text
dotnet list TinyPl0.sln package --outdated --include-transitive
dotnet list TinyPl0.sln package --vulnerable --include-transitive
git diff --check
git diff --name-only main...HEAD
```

Review-Ergebnis / Review outcome:

- NIST SSDF und CWE Top 25: `Applicable`, geprüft;
- C#-MSL und sichere .NET-Regeln: `Applicable`;
- keine neue Eingabe-, Datei-, Netzwerk-, Auth-, Crypto- oder Fehlergrenze;
- Architektur, ADR/S-ADR, ASVS, SBOM/VEX/SLSA, AI-SBOM, Cloud/Regulierung:
  begründet `N/A` nach `gate-requirements.json`;
- kritischer Vulnerability-Fund oder unerwarteter Scope: stoppen und neue
  Autorisierung einholen.

## 10. Statistik und finale lokale Prüfung / Statistics and Final Local Check

1. Genau einen neuen chronologischen Fortschreibungseintrag ergänzen.
2. Produktions-, Test- und Dokumentationszeilen sowie 80/125-Basen nennen.
3. Profil 2 rendern und read-only prüfen.

```powershell
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo .
pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly -Json
pwsh -NoProfile -File scripts/check-homogeneity.ps1 -TargetDir . -Json -DryRun -NoPatch
```

## 11. Delivery-Grenze / Delivery Boundary

Vor Commit/Push den exakten Delivery-Set-Validator, `git diff --cached --check`
und `git status --short` aus CMD-15 verwenden. Danach die temporäre Schema-2.0-
PreMerge-Evidence für den exakten Head mit CMD-16 validieren.

Remote-Aktionen, PR, Merge, Sync und PostMerge sind nur in den späteren dafür
autorisierten Phasen erlaubt. Diese Quickstart-Anleitung selbst verleiht keine
Remote- oder Merge-Berechtigung.

*Validate the immutable delivery candidate and exact-head gate evidence before
remote delivery. This guide grants no commit, push, PR, merge, or sync
authority by itself.*
