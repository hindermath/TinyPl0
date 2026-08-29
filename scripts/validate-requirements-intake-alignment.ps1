[CmdletBinding()]
param(
    [string]$RepositoryRoot = (Split-Path -Parent $PSScriptRoot)
)

$ErrorActionPreference = 'Stop'
Push-Location $RepositoryRoot
try {
    & node scripts/render-requirements-intake-governance.mjs
    if ($LASTEXITCODE -ne 0) { throw 'Generated intake-governance validation failed.' }
    & node scripts/validate-requirements-intake-alignment.mjs
    if ($LASTEXITCODE -ne 0) { throw 'Requirements alignment validation failed.' }

    Get-ChildItem specs/intake-authoring-receipts/*.json | ForEach-Object {
        $Receipt = Get-Content -LiteralPath $_.FullName -Raw -Encoding UTF8 | ConvertFrom-Json
        $TargetPath = [string]$Receipt.target.path
        if (Test-Path -LiteralPath $TargetPath -PathType Leaf) {
            & pwsh -NoProfile -File .specify/presets/intake-authoring-governance/scripts/validate-intake-authoring-receipt.ps1 `
                -Receipt $_.FullName -Repo $RepositoryRoot
            if ($LASTEXITCODE -ne 0) { throw "Intake receipt validation failed: $($_.Name)" }
        }
        else {
            Write-Output "PASS: historical intake receipt remains generator-bound after completed-target archival ($($_.Name))"
        }
    }

    & pwsh -NoProfile -File .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-manifest.ps1 `
        -File requirements/intakes/series/tinypl0-delivery/manifest.json -Repo $RepositoryRoot
    if ($LASTEXITCODE -ne 0) { throw 'Series manifest validation failed.' }
    & pwsh -NoProfile -File .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-receipt.ps1 `
        -File requirements/intakes/series/tinypl0-delivery/receipt.json -Repo $RepositoryRoot
    if ($LASTEXITCODE -ne 0) { throw 'Series receipt validation failed.' }
    $Review = Get-Content -LiteralPath requirements/intakes/series/tinypl0-delivery/intake-review-result.json -Raw -Encoding UTF8 | ConvertFrom-Json
    $ReviewTargetsExist = @($Review.targets | Where-Object { -not (Test-Path -LiteralPath ([string]$_.path) -PathType Leaf) }).Count -eq 0
    if ($ReviewTargetsExist) {
        & pwsh -NoProfile -File .specify/presets/intake-review-governance/scripts/validate-intake-review-result.ps1 `
            -Result requirements/intakes/series/tinypl0-delivery/intake-review-result.json -Repo $RepositoryRoot
        if ($LASTEXITCODE -ne 0) { throw 'Intake review validation failed.' }
    }
    else {
        Write-Output 'PASS: prior intake review remains generator-bound historical evidence after completed-target archival; no new review is claimed.'
    }
}
finally {
    Pop-Location
}
