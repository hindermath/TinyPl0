<#
.SYNOPSIS
Packt und prüft beide TinyPl0-NuGet-Pakete mit einem unabhängigen Consumer.

Packs and validates both TinyPl0 NuGet packages with an independent consumer.
#>
[CmdletBinding()]
param(
    [ValidateSet('Debug', 'Release')][string]$Configuration = 'Release',
    [ValidatePattern('^\d+\.\d+\.\d+(?:-[0-9A-Za-z.-]+)?$')][string]$Version = '0.4.0',
    [ValidateNotNullOrEmpty()][string]$OutputDirectory = 'artifacts/packages'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$RepositoryRoot = Split-Path -Parent $PSScriptRoot
$OutputPath = [IO.Path]::GetFullPath((Join-Path $RepositoryRoot $OutputDirectory))
$TemporaryRoot = Join-Path ([IO.Path]::GetTempPath()) ("tinypl0-package-test-" + [guid]::NewGuid().ToString('N'))

function Invoke-DotNet {
    param([Parameter(Mandatory)][string[]]$Arguments)
    & dotnet @Arguments
    if ($LASTEXITCODE -ne 0) { throw "dotnet failed with exit code $LASTEXITCODE" }
}

function Get-PackageMetadata {
    param([Parameter(Mandatory)][string]$PackagePath)
    $Archive = [IO.Compression.ZipFile]::OpenRead($PackagePath)
    try {
        $Entries = @($Archive.Entries | ForEach-Object FullName | Sort-Object)
        $NuspecEntry = @($Archive.Entries | Where-Object FullName -like '*.nuspec')
        if ($NuspecEntry.Count -ne 1) { throw "Package must contain exactly one nuspec: $PackagePath" }
        $Reader = [IO.StreamReader]::new($NuspecEntry[0].Open())
        try { [xml]$Nuspec = $Reader.ReadToEnd() } finally { $Reader.Dispose() }
        $Namespace = [Xml.XmlNamespaceManager]::new($Nuspec.NameTable)
        $Namespace.AddNamespace('n', $Nuspec.DocumentElement.NamespaceURI)
        $Metadata = $Nuspec.SelectSingleNode('/n:package/n:metadata', $Namespace)
        $Dependencies = @($Nuspec.SelectNodes('/n:package/n:metadata/n:dependencies/n:group/n:dependency', $Namespace))
        return [ordered]@{
            id = [string]$Metadata.id
            version = [string]$Metadata.version
            dependencies = @($Dependencies | ForEach-Object { [ordered]@{ id = [string]$_.id; version = [string]$_.version } })
            entries = $Entries
        }
    } finally { $Archive.Dispose() }
}

try {
    $env:DOTNET_CLI_HOME = Join-Path $TemporaryRoot 'dotnet-home'
    New-Item -ItemType Directory -Path $env:DOTNET_CLI_HOME -Force | Out-Null
    New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
    Get-ChildItem -LiteralPath $OutputPath -File -ErrorAction SilentlyContinue |
        Where-Object Name -Match '^TinyPl0\.(Core|Vm)\.' | Remove-Item -Force

    Invoke-DotNet @('restore', 'TinyPl0.sln', '--locked-mode')

    Invoke-DotNet @('pack', 'src/Pl0.Core/Pl0.Core.csproj', '--configuration', $Configuration,
        '--no-restore', '--output', $OutputPath, "-p:TinyPl0PackageVersion=$Version", "-p:Version=$Version")
    Invoke-DotNet @('pack', 'src/Pl0.Vm/Pl0.Vm.csproj', '--configuration', $Configuration,
        '--no-restore', '--output', $OutputPath, "-p:TinyPl0PackageVersion=$Version", "-p:Version=$Version")

    $ExpectedFiles = @(
        "TinyPl0.Core.$Version.nupkg", "TinyPl0.Core.$Version.snupkg",
        "TinyPl0.Vm.$Version.nupkg", "TinyPl0.Vm.$Version.snupkg"
    )
    foreach ($Name in $ExpectedFiles) {
        if (-not (Test-Path -LiteralPath (Join-Path $OutputPath $Name) -PathType Leaf)) {
            throw "Missing package artifact: $Name"
        }
    }

    $Core = Get-PackageMetadata (Join-Path $OutputPath "TinyPl0.Core.$Version.nupkg")
    $Vm = Get-PackageMetadata (Join-Path $OutputPath "TinyPl0.Vm.$Version.nupkg")
    if ($Core.id -ne 'TinyPl0.Core' -or $Vm.id -ne 'TinyPl0.Vm') { throw 'Unexpected package ID.' }
    if ($Core.version -ne $Version -or $Vm.version -ne $Version) { throw 'Package versions do not match.' }
    if ($Core.dependencies.Count -ne 0) { throw 'TinyPl0.Core must have no runtime package dependency.' }
    if ($Vm.dependencies.Count -ne 1 -or $Vm.dependencies[0].id -ne 'TinyPl0.Core' -or
        $Vm.dependencies[0].version -ne "[$Version]") { throw 'TinyPl0.Vm must depend exactly on matching TinyPl0.Core.' }
    foreach ($Package in @($Core, $Vm)) {
        $AssemblyName = if ($Package.id -eq 'TinyPl0.Core') { 'Pl0.Core' } else { 'Pl0.Vm' }
        if (-not ($Package.entries -contains 'README.md')) { throw "$($Package.id) is missing README.md." }
        if (-not ($Package.entries -contains "lib/net10.0/$AssemblyName.dll")) { throw "$($Package.id) is missing its DLL." }
        if (-not ($Package.entries -contains "lib/net10.0/$AssemblyName.xml")) { throw "$($Package.id) is missing XML documentation." }
        $Text = ($Package.dependencies | ConvertTo-Json -Compress)
        if ($Text -match 'Terminal\.Gui|Pl0\.Ide|TinyCalc') { throw "$($Package.id) contains a forbidden dependency." }
    }

    $Consumer = Join-Path $TemporaryRoot 'consumer'
    New-Item -ItemType Directory -Path $Consumer -Force | Out-Null
    Invoke-DotNet @('new', 'console', '--framework', 'net10.0', '--no-restore', '--output', $Consumer)
    $Project = @"
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings></PropertyGroup>
  <ItemGroup>
    <PackageReference Include="TinyPl0.Core" Version="$Version" />
    <PackageReference Include="TinyPl0.Vm" Version="$Version" />
  </ItemGroup>
</Project>
"@
    Set-Content -LiteralPath (Join-Path $Consumer 'consumer.csproj') -Value $Project -Encoding utf8NoBOM
    $Program = @'
using Pl0.Core;
using Pl0.Vm;
Instruction[] code = [new(Opcode.Lit, 0, 42), new(Opcode.Opr, 0, 15), new(Opcode.Opr, 0, 0)];
var io = new BufferedPl0Io();
VmExecutionResult run = new VirtualMachine().Run(code, io);
var stepVm = new SteppableVirtualMachine();
stepVm.Initialize(code, new BufferedPl0Io());
VmStepResult step;
do { step = stepVm.Step(); } while (step.Status == VmStepStatus.Running);
if (!run.Success || step.Reason != VmCompletionReason.Halted || io.Output.Single() != 42) return 1;
Console.WriteLine("PASS: TinyPl0 package consumer");
return 0;
'@
    Set-Content -LiteralPath (Join-Path $Consumer 'Program.cs') -Value $Program -Encoding utf8NoBOM
    $Config = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration><packageSources><clear /><add key="local" value="$OutputPath" /></packageSources></configuration>
"@
    Set-Content -LiteralPath (Join-Path $Consumer 'NuGet.Config') -Value $Config -Encoding utf8NoBOM
    $Packages = Join-Path $TemporaryRoot 'packages'
    Invoke-DotNet @('restore', (Join-Path $Consumer 'consumer.csproj'), '--configfile',
        (Join-Path $Consumer 'NuGet.Config'), '--packages', $Packages, '--no-cache')
    Invoke-DotNet @('build', (Join-Path $Consumer 'consumer.csproj'), '--configuration', 'Release', '--no-restore')
    Invoke-DotNet @('run', '--project', (Join-Path $Consumer 'consumer.csproj'),
        '--configuration', 'Release', '--no-build')

    $Artifacts = foreach ($Name in $ExpectedFiles) {
        $File = Get-Item -LiteralPath (Join-Path $OutputPath $Name)
        [ordered]@{ file = $Name; length = $File.Length; sha256 = (Get-FileHash $File.FullName -Algorithm SHA256).Hash.ToLowerInvariant() }
    }
    $Evidence = [ordered]@{
        schemaVersion = '1.0'; packageVersion = $Version; configuration = $Configuration
        platform = [Environment]::OSVersion.Platform.ToString(); consumerFramework = 'net10.0'
        packages = @($Artifacts); core = $Core; vm = $Vm; consumer = 'Passed'
    }
    $Evidence | ConvertTo-Json -Depth 12 | Set-Content -LiteralPath (Join-Path $OutputPath 'package-validation.json') -Encoding utf8NoBOM
    Write-Host "PASS: packages and independent consumer validated at $OutputPath"
} finally {
    if (Test-Path -LiteralPath $TemporaryRoot) { Remove-Item -LiteralPath $TemporaryRoot -Recurse -Force }
}
