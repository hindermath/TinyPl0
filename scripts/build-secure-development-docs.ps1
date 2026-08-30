[CmdletBinding(SupportsShouldProcess, DefaultParameterSetName = 'Build')]
param(
    [Parameter(ParameterSetName = 'Check')]
    [switch] $Check,

    [Parameter()]
    [ValidateNotNullOrEmpty()]
    [string] $Repo = (Split-Path -Parent $PSScriptRoot)
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Build-SecureDevelopmentDocs {
    <#
    .SYNOPSIS
    Prüft oder erzeugt den Secure-Development-Sammelband. / Checks or builds the secure-development compendium.
    .DESCRIPTION
    DE: Liest ausschließlich die zwölf kanonischen Checklisten, prüft Manifest,
    Versionen, 157 eindeutige IDs und Reihenfolge und ersetzt die Ausgabe atomar.
    EN: Reads only the twelve canonical checklists, validates manifest, versions,
    157 unique IDs and order, and replaces output atomically.
    #>
    [CmdletBinding(SupportsShouldProcess)]
    param(
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string] $RepositoryRoot,

        [Parameter()]
        [switch] $CheckOnly
    )

    $root = (Resolve-Path -LiteralPath $RepositoryRoot).Path
    $docsRoot = Join-Path $root 'docs/secure-development'
    $manifestPath = Join-Path $docsRoot 'baseline-manifest.json'
    $compendiumPath = Join-Path $docsRoot 'Checklistensammelband_Sichere-Entwicklung.md'
    $manifest = Get-Content -LiteralPath $manifestPath -Raw -Encoding UTF8 | ConvertFrom-Json
    if ($manifest.baselineVersion -ne '3.2.0' -or $manifest.guideline.version -ne '3.2.0' -or $manifest.compendium.version -ne '2.2.0') {
        throw 'Secure-development manifest versions are not aligned to 3.2.0/2.2.0.'
    }

    $files = @($manifest.checklists | ForEach-Object { Join-Path $docsRoot $_.path })
    if ($files.Count -ne 12) { throw 'Expected twelve canonical checklist files.' }
    $sets = foreach ($path in $files) {
        if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing checklist: $path" }
        $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
        $version = [regex]::Match($text, '(?m)^\*\*Version / Version:\*\*\s+([^\r\n]+)$').Groups[1].Value.Trim()
        $entry = @($manifest.checklists | Where-Object { (Join-Path $docsRoot $_.path) -eq $path })[0]
        if ($version -ne $entry.version) { throw "Version drift: $path expected $($entry.version), found $version" }
        $ids = @([regex]::Matches($text, '(?m)^#### (CL-[0-9]{2}-[0-9]{2}):') | ForEach-Object { $_.Groups[1].Value })
        # DE: Links der Quelldatei sind relativ zu checkliste/. Im Sammelband
        # liegen sie eine Ebene höher. EN: Source links are relative to the
        # checklist folder and are rebased for the compendium one level above.
        $embedded = $text.TrimEnd()
        $embedded = $embedded.Replace('(../Richtlinie_Sichere-Entwicklung.md)', '(Richtlinie_Sichere-Entwicklung.md)')
        $embedded = $embedded.Replace('(../../../constitution.md)', '(../../constitution.md)')
        $embedded = $embedded.Replace('(../README.md)', '(README.md)')
        $embedded = $embedded.Replace('(../mitgeltende-dokumente/', '(mitgeltende-dokumente/')
        $embedded = $embedded.Replace('(CL_04_Bedrohungsmodellierung.md)', '(checklisten/CL_04_Bedrohungsmodellierung.md)')
        [pscustomobject]@{ Text = $embedded; Ids = $ids }
    }
    $all = [string[]]@($sets.Ids)
    if ($all.Count -ne 157 -or @($all | Sort-Object -Unique).Count -ne 157) { throw 'Expected 157 unique checklist IDs.' }
    $book = [string[]]@([regex]::Matches((Get-Content -LiteralPath $compendiumPath -Raw -Encoding UTF8), '(?m)^#### (CL-[0-9]{2}-[0-9]{2}):') | ForEach-Object { $_.Groups[1].Value })
    if (-not [Linq.Enumerable]::SequenceEqual($all, $book)) { throw 'Compendium ID order differs from canonical sources.' }

    $header = @'
# Checklistensammelband Sichere Entwicklung / Secure Development Checklist Compendium

> **GENERATED FILE / GENERIERTE DATEI:** Nicht manuell bearbeiten. Der Sammelband wird aus den zwölf Dateien unter `checklisten/` erzeugt. / Do not edit manually. This compendium is generated from the twelve files under `checklisten/`.

**Baseline-Version / Baseline version:** 3.2.0
**Dokumentversion / Document version:** 2.2.0
**Stand / Date:** 2026-07-19
**Quelle / Source:** `baseline-manifest.json` und / and `checklisten/`

## Zweck / Purpose

**DE:** Dieser Sammelband führt die zwölf kanonischen Checklisten für sichere Entwicklung in unveränderter Reihenfolge zusammen. Er ist eine vollständige Audit- und Review-Sicht. Für gezielte Prüfungen werden die Einzelchecklisten verwendet.

**EN:** This compendium combines the twelve canonical secure-development checklists in unchanged order. It is the complete audit and review view. Use the individual checklists for focused reviews.

## Einheitliches Statusmodell / Unified Status Model

Jeder Prüfpunkt verwendet zwei getrennte Statusachsen. / Every review item uses two separate status axes.

| Achse / Axis | Zulässige Werte / Allowed values |
|---|---|
| Anwendbarkeit / Applicability | `Applicable`, `N/A`, `Open` |
| Umsetzung / Implementation | `Fulfilled`, `Partly Fulfilled`, `Not Fulfilled`, `Not Assessed` |

`N/A` braucht immer eine kurze Begründung. `Open`, `Partly Fulfilled`, `Not Fulfilled` und `Not Assessed` brauchen eine Folgeaufgabe, verantwortliche Rolle und einen Zieltermin. / `N/A` always needs a short rationale. `Open`, `Partly Fulfilled`, `Not Fulfilled`, and `Not Assessed` need a follow-up action, responsible role, and target date.

## Nachweisinstanzen / Evidence Instances

**DE:** Diese Datei ist eine Vorlage, kein ausgefüllter Projektnachweis. Ausgefüllte Nachweise werden unter `docs/security/secure-development/<datum>-<scope>/` abgelegt und nennen Projekt, Scope, Prüfdatum, Baseline-Version, verantwortliche Person, Reviewer, Evidenzpfade, Restrisiken und Neubewertungs-Trigger.

**EN:** This file is a template, not completed project evidence. Completed evidence is stored under `docs/security/secure-development/<date>-<scope>/` and names project, scope, review date, baseline version, responsible person, reviewer, evidence paths, residual risks, and re-evaluation triggers.

## Kapitelüberblick / Chapter Overview

- [CL-01 Standards-Anwendbarkeit](checklisten/CL_01_Standards-Anwendbarkeit.md)
- [CL-02 Sichere Softwarearchitektur](checklisten/CL_02_Sichere-Softwarearchitektur.md)
- [CL-03 Krypto-Mindestvorgaben](checklisten/CL_03_Krypto-Mindestvorgaben.md)
- [CL-04 Bedrohungsmodellierung](checklisten/CL_04_Bedrohungsmodellierung.md)
- [CL-05 Lieferkette und Build-Integrität](checklisten/CL_05_Lieferkette-Build-Integritaet.md)
- [CL-06 Schwachstellenoffenlegung](checklisten/CL_06_Schwachstellenoffenlegung.md)
- [CL-07 CRA-Anwendbarkeit](checklisten/CL_07_CRA-Anwendbarkeit.md)
- [CL-08 Sicherheits-Code-Review](checklisten/CL_08_Sicherheits-Code-Review.md)
- [CL-09 KI-Codeerzeugung](checklisten/CL_09_KI-Codeerzeugung.md)
- [CL-10 Sichere Entwicklungsumgebung](checklisten/CL_10_Sichere-Entwicklungsumgebung.md)
- [CL-11 Datenschutz-Folgenabschätzung](checklisten/CL_11_Datenschutz-Folgenabschaetzung.md)
- [CL-12 Agentische KI-Sandbox](checklisten/CL_12_Agentische-KI-Sandbox.md)
'@
    $footer = @'


---

## Versionshistorie / Version History

| Version | Datum / Date | Änderung / Change |
|---|---|---|
| 2.2.0 | 2026-07-19 | Aus den zwölf kanonischen Einzelchecklisten der sicheren-Entwicklung-Basis 3.2.0 erzeugt; einheitliches zweiachsiges Statusmodell und klare Trennung zwischen Vorlage und Projektnachweis. / Generated from the twelve canonical individual checklists of secure-development baseline 3.2.0; unified two-axis status model and clear separation between template and project evidence. |
'@
    $candidate = $header.TrimStart() + "`n`n---`n`n" + (($sets.Text) -join "`n`n---`n`n") + $footer + "`n"
    $candidateBytes = [Text.Encoding]::UTF8.GetBytes($candidate)
    $candidateHash = [Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($candidateBytes)).ToLowerInvariant()
    if ($CheckOnly) {
        Write-Output "PASS: baseline=3.2.0; compendium=2.2.0; files=12; total=157; unique=157; candidateSha256=$candidateHash"
        return
    }

    if ($PSCmdlet.ShouldProcess($compendiumPath, 'Atomically replace generated compendium')) {
        $temporary = Join-Path ([IO.Path]::GetTempPath()) ('tinypl0-secure-development-' + [guid]::NewGuid().ToString('N') + '.md')
        try {
            [IO.File]::WriteAllText($temporary, $candidate, [Text.UTF8Encoding]::new($false))
            Move-Item -LiteralPath $temporary -Destination $compendiumPath -Force
        } finally {
            if (Test-Path -LiteralPath $temporary) { Remove-Item -LiteralPath $temporary -Force }
        }
        Write-Output "PASS: generated compendiumSha256=$candidateHash"
    }
}

if ($MyInvocation.InvocationName -ne '.') {
    Build-SecureDevelopmentDocs -RepositoryRoot $Repo -CheckOnly:$Check -WhatIf:$WhatIfPreference
}
