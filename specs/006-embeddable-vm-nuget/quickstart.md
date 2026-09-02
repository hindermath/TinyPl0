# Schnellstart für die spätere Umsetzung / Quickstart for Later Implementation

**Wichtig / Important**: Dieses Dokument plant die spätere Arbeit. In der
Planphase werden die Befehle nicht ausgeführt. Jeder lokale Build/Test folgt
vorher dem serialisierten IDE-Versionsvertrag. / This document plans later
work. Commands are not executed during planning. Every local build/test first
follows the serialized IDE-version contract.

## 1. Vorbedingungen / Preconditions

- Branch `codex/006-embeddable-vm-nuget`, aktiver Intake und akzeptierte Hashes
  erneut prüfen. / Revalidate branch, active intake, and accepted hashes.
- Plan Review, `/speckit.tasks` und `/speckit.analyze` müssen vollständig sein.
  / Plan review, tasks, and analysis must be complete.
- .NET 10 SDK, PowerShell 7, DocFX, Node 24, Playwright/axe, `lynx`, CycloneDX
  und Gitleaks aus den gebundenen Versionen verwenden. / Use the bound tool
  versions.
- Keine Secrets lesen, anfordern oder protokollieren; keine Provideraktion vor
  den Remote-Gates. / Do not read, request, or log secrets; do not perform a
  provider action before remote gates.

## 2. TDD-Reihenfolge / TDD sequence

1. Rot: vorab abgebrochener Token für Run und initialisierten Step erwartet
   `Cancelled`, Zähler `0`, kein I/O und idempotente Folge-Steps. / Red:
   pre-cancelled Run and initialized Step expect Cancelled, count zero, no I/O,
   and idempotent later steps.
2. Grün: Token, Reason, Sessionhülle und Terminalcache minimal ergänzen. / Green:
   minimally add token, reason, session shell, and terminal cache.
3. Rot/Grün: Paritätstest hinzufügen; Opcode-/OPR-Dispatch genau einmal in
   `VmExecutionSession.ExecuteNext()` verschieben. / Add parity test and move
   dispatch exactly once into the session.
4. Rot/Grün: Validierungspräzedenz, positive/negative Grenzwerte, ungültigen
   P-Code, Zählpunkt und alle terminalen Gründe ergänzen. / Add validation
   precedence, boundaries, invalid P-Code, counting point, and terminal reasons.
5. Bestehende VM-, CLI-, IDE-, Compiler-, Golden-, L10N- und Architekturtests
   regressieren. / Regress existing test surfaces.

Geplante fokussierte Befehle / Planned focused commands:

```powershell
dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --configuration Release --no-build --filter "FullyQualifiedName~VmCancellationTests|FullyQualifiedName~VmRunStepParityTests"
dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --configuration Release --no-build --filter "FullyQualifiedName~VmExecutionBoundaryTests"
dotnet test TinyPl0.sln --configuration Release --no-build --collect:"XPlat Code Coverage"
```

## 3. Lokales Paketpaar / Local package pair

Nach erfolgreicher Laufzeitimplementierung wird genau eine Testversion aus
`eng/TinyPl0.PackageVersion.props` verwendet. Core und VM werden getrennt
gepackt, aber gemeinsam geprüft. / After runtime implementation, one test
version from the props file is used. Core and VM are packed separately but
verified as one set.

```powershell
dotnet restore TinyPl0.sln --locked-mode
dotnet pack src/Pl0.Core/Pl0.Core.csproj --configuration Release --no-restore --output artifacts/packages
dotnet pack src/Pl0.Vm/Pl0.Vm.csproj --configuration Release --no-restore --output artifacts/packages
dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --configuration Release --no-build --filter "FullyQualifiedName~NuGetPackageContractTests"
```

Die Prüfung verwirft fehlende README/XML/Symbole/SourceLink-Metadaten,
abweichende Versionen, weitere Runtime-Abhängigkeiten oder eine VM→Core-Range
ungleich `[version]`. / Verification rejects missing package assets, mismatched
versions, extra runtime dependencies, or a VM-to-Core range other than the
exact version.

## 4. Sauberer Consumer / Clean consumer

Der Testconsumer besitzt keine `ProjectReference`. Für lokale Prüfung verwendet
er ausschließlich den erzeugten temporären Feed und einen neuen Paketcache; für
das Delivery-Gate ausschließlich NuGet.org. / The test consumer has no project
reference. Local proof uses only the generated temporary feed and a fresh cache;
delivery proof uses only NuGet.org.

```powershell
$consumerCache = Join-Path ([System.IO.Path]::GetTempPath()) ("tinypl0-nuget-" + [guid]::NewGuid())
$env:NUGET_PACKAGES = $consumerCache
dotnet restore tests/consumers/TinyPl0.PackageConsumer/TinyPl0.PackageConsumer.csproj --configfile tests/consumers/TinyPl0.PackageConsumer/NuGet.local.config --no-cache --force
dotnet run --project tests/consumers/TinyPl0.PackageConsumer/TinyPl0.PackageConsumer.csproj --configuration Release --no-restore
```

Der Consumer kompiliert PL/0, führt dasselbe Programm mit `Run` und `Step` aus
und vergleicht Reason, Zähler und Ausgabe. Der spätere öffentliche Lauf wird
auf `macos-15`, `ubuntu-24.04` und `windows-2025` wiederholt. / The consumer
compiles PL/0, runs the same program through Run and Step, and compares reason,
count, and output. The later public run repeats on all three runners.

## 5. Hostbeispiel / Host example

```csharp
CancellationTokenSource cancellation = new(TimeSpan.FromSeconds(2));
VirtualMachineOptions options = new(
    StackSize: 500,
    InstructionBudget: 1_000_000,
    MaximumProgramLength: 10_000);

VmExecutionResult run = VirtualMachine.Run(
    compilation.Instructions,
    io,
    options,
    cancellation.Token);

SteppableVirtualMachine machine = new();
machine.Initialize(compilation.Instructions, io, options, cancellation.Token);
VmStepResult step;
do
{
    step = machine.Step();
}
while (step.CompletionReason == VmCompletionReason.Running);
```

Produktionscode prüft `compilation.Success`, behandelt jeden Completion Reason
und setzt eigene Zeit-/Prozessgrenzen, falls nicht vertrauenswürdiger Code eine
stärkere Isolation braucht. Die VM selbst ist keine Betriebssystem-Sandbox. /
Production code checks compilation success, handles every completion reason,
and supplies stronger process/time isolation for untrusted code. The VM itself
is not an operating-system sandbox.

## 6. Dokumentation, Sicherheit und Statistik / Docs, security, and statistics

```powershell
dotnet tool run docfx docfx.json
$env:MANAGE_DOCS_SERVER = "1"
npm --prefix tests/a11y test -- --project=chromium
lynx -dump _site/api/Pl0.Vm.VirtualMachine.html
dotnet package list TinyPl0.sln --vulnerable --include-transitive
pwsh -NoLogo -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly
```

Die manuelle Textprüfung dokumentiert Seitensprache, Skip-Link, Landmarken,
Fokus, Nicht-Text-Kontrast und verständliche lineare Lesereihenfolge. Security-
Evidence ordnet Threat Model, S-ADRs, SBOM, VEX, Attestation und Restrisiken
demselben Commit zu. / Manual text review records language, bypass link,
landmarks, focus, non-text contrast, and linear reading order. Security evidence
binds threat model, S-ADRs, SBOM, VEX, attestation, and residual risks to the
same commit.

## 7. Remote- und Abschlussgrenze / Remote and closeout boundary

Remote Review, OIDC-Policy-Prüfung, Publish, öffentlicher Restore, Merge, Sync,
Handoff und Intake-Closeout sind getrennte spätere Gates. Fehlende OIDC-
Evidence, `Partial`, `Conflict`, unbekannter 409, offener Review oder
Exact-Head-Drift blockiert. Es wird kein Folgefeature gestartet. / Remote
review, OIDC policy proof, publication, public restore, merge, sync, handoff,
and intake closeout are separate later gates. Missing OIDC evidence, partial or
conflicting state, unknown 409, open review, or exact-head drift blocks. No
follow-up feature is started.
