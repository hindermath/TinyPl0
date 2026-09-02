<#
.SYNOPSIS
Erzeugt lokale, commit- und versionsgebundene NuGet-Release-Evidenz.

Creates local commit- and version-bound NuGet release evidence.
#>
[CmdletBinding()]
param(
    [ValidateNotNullOrEmpty()][string]$PackageDirectory = 'artifacts/packages',
    [ValidateNotNullOrEmpty()][string]$OutputDirectory = 'artifacts/release-evidence'
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$RepositoryRoot = Split-Path -Parent $PSScriptRoot
$PackagePath = [IO.Path]::GetFullPath((Join-Path $RepositoryRoot $PackageDirectory))
$OutputPath = [IO.Path]::GetFullPath((Join-Path $RepositoryRoot $OutputDirectory))
if (-not (Test-Path -LiteralPath $PackagePath -PathType Container)) { throw "Package directory not found: $PackagePath" }
$ValidationPath = Join-Path $PackagePath 'package-validation.json'
if (-not (Test-Path -LiteralPath $ValidationPath -PathType Leaf)) { throw 'package-validation.json is required.' }
$Validation = Get-Content -LiteralPath $ValidationPath -Raw -Encoding utf8 | ConvertFrom-Json
$Version = [string]$Validation.packageVersion
$Expected = @("TinyPl0.Core.$Version.nupkg", "TinyPl0.Core.$Version.snupkg", "TinyPl0.Vm.$Version.nupkg", "TinyPl0.Vm.$Version.snupkg")
$Files = foreach ($Name in $Expected) {
    $Path = Join-Path $PackagePath $Name
    if (-not (Test-Path -LiteralPath $Path -PathType Leaf)) { throw "Missing release file: $Name" }
    [ordered]@{ name = $Name; sha256 = (Get-FileHash $Path -Algorithm SHA256).Hash.ToLowerInvariant(); length = (Get-Item $Path).Length }
}
$Commit = (& git -C $RepositoryRoot rev-parse HEAD).Trim()
if ($LASTEXITCODE -ne 0 -or $Commit -notmatch '^[0-9a-f]{40}$') { throw 'Unable to resolve the source commit.' }
New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
$Timestamp = [DateTimeOffset]::UtcNow.ToString('o')
$Namespace = "https://github.com/hindermath/TinyPl0/releases/$Version/spdx-$Commit"
$Spdx = [ordered]@{
    spdxVersion = 'SPDX-2.3'; dataLicense = 'CC0-1.0'; SPDXID = 'SPDXRef-DOCUMENT'
    name = "TinyPl0 NuGet $Version"; documentNamespace = $Namespace
    creationInfo = [ordered]@{ created = $Timestamp; creators = @('Tool: New-NuGetReleaseEvidence.ps1') }
    packages = @($Files | ForEach-Object { [ordered]@{
        name = $_.name; SPDXID = ('SPDXRef-' + ($_.name -replace '[^A-Za-z0-9.-]', '-'))
        versionInfo = $Version; downloadLocation = 'NOASSERTION'; filesAnalyzed = $false
        checksums = @([ordered]@{ algorithm = 'SHA256'; checksumValue = $_.sha256 })
        licenseConcluded = 'MIT'; licenseDeclared = 'MIT'; copyrightText = 'NOASSERTION'
    } })
}
$Vex = [ordered]@{
    '@context' = 'https://openvex.dev/ns/v0.2.0'; '@id' = "$Namespace/vex"
    author = 'TinyPl0 maintainers'; role = 'Document Creator'; timestamp = $Timestamp
    version = 1; tooling = 'New-NuGetReleaseEvidence.ps1'; statements = @()
}
$Provenance = [ordered]@{
    '_type' = 'https://in-toto.io/Statement/v1'
    subject = @($Files | ForEach-Object { [ordered]@{ name = $_.name; digest = [ordered]@{ sha256 = $_.sha256 } } })
    predicateType = 'https://slsa.dev/provenance/v1'
    predicate = [ordered]@{
        buildDefinition = [ordered]@{ buildType = 'https://github.com/hindermath/TinyPl0/local-nuget-build/v1'; externalParameters = [ordered]@{ version = $Version; commit = $Commit } }
        runDetails = [ordered]@{ builder = [ordered]@{ id = 'local:New-NuGetReleaseEvidence.ps1' }; metadata = [ordered]@{ invocationId = [guid]::NewGuid().ToString(); startedOn = $Timestamp } }
    }
}
$Spdx | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath (Join-Path $OutputPath 'sbom.spdx.json') -Encoding utf8NoBOM
$Vex | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath (Join-Path $OutputPath 'vex.openvex.json') -Encoding utf8NoBOM
$Provenance | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath (Join-Path $OutputPath 'provenance.intoto.jsonl') -Encoding utf8NoBOM
$Manifest = [ordered]@{
    schemaVersion = '1.0'; version = $Version; commit = $Commit; generatedAt = $Timestamp
    packages = @($Files); sbom = 'sbom.spdx.json'; vex = 'vex.openvex.json'
    provenance = 'provenance.intoto.jsonl'; providerAttestation = 'PendingRemotePublication'
}
$Manifest | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath (Join-Path $OutputPath 'release-evidence-validation.json') -Encoding utf8NoBOM
Write-Host "PASS: SPDX 2.3, OpenVEX and local provenance evidence created at $OutputPath"
