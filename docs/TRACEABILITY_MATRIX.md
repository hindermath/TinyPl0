# TinyPl0 Traceability-Matrix

Diese Matrix bildet die Coverage-Gate-Anforderung aus dem Pflichtenheft ab:
- jede Sprachregel aus Abschnitt 4.1.1
- jede VM-Regel aus Abschnitt 4.3

muss mindestens einem Pflichttestfall zugeordnet sein.

## Quelle der Zuordnung
- Maschinenlesbare Matrix:
  - `../tests/data/expected/traceability/matrix.json`
- Referenzkatalog der Pflichttestfaelle:
  - `../tests/data/expected/catalog/cases.json`

## Automatischer Gate-Test
- [`TraceabilityMatrixTests.cs`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/TraceabilityMatrixTests.cs)

Der Test validiert:
1. Vollstaendigkeit aller geforderten Sprachregeln.
2. Vollstaendigkeit aller geforderten VM-Regeln.
3. Jede Regel verweist auf mindestens einen katalogisierten Pflichttestfall.

## Secure-VM-Härtung / Secure VM Hardening

| Vertrag / Contract | Batch-Beweis / Batch proof | Step-Beweis / Step proof | Ergänzende Evidence / Supporting evidence |
|---|---|---|---|
| Genau `N`, Stopp vor `N+1` | `VirtualMachineTests.Instruction_Budget_*` | `SteppableVirtualMachineTests.Instruction_Budget_*` | `VM-TDD-GATE-001`, identische Rot-/Grün-Testquellhashes |
| Stack `3..1_000_000`, Budget `>0`, Prüfung vor Allokation | `VirtualMachineTests.Invalid_Options_*` | `SteppableVirtualMachineTests.Invalid_Options_*` | `VM-CONFIGURATION-GATE-001`, Diagnosen 207/208 |
| Vier-Parameter-Quellkompatibilität und DE/EN | `L10nTests.VirtualMachineOptions_Four_Parameter_*` | gleicher öffentlicher Optionsvertrag / same public options contract | `L10nTests.Vm_Instruction_Budget_*` |

Deutsch: Die Härtung ändert keine Opcode-, OPR- oder PL/0-Semantik und keine
Golden-Datei. Die drei Testdateien bilden Budget, Optionsgrenzen, Batch-/Step-
Parität, Diagnose und den bisherigen Vier-Parameter-Aufruf ab. Die vollständige
Katalog-, Golden- und Traceability-Suite bleibt dem späteren exakten
Kandidaten-HEAD vorbehalten.

English: The hardening changes no opcode, OPR, PL/0, or golden semantics. The
three test files cover the budget, option limits, batch/step parity,
diagnostics, and the existing four-parameter call. The complete catalogue,
golden, and traceability suite remains reserved for the later exact candidate
HEAD.

## Feature 006: Einbettbare VM und NuGet / Embeddable VM and NuGet

| Anforderungen / Requirements | Test oder Evidence / Test or evidence |
|---|---|
| FR-001–FR-006: gemeinsamer Kernel, Grenzen, Cancellation, Results | VirtualMachineParityTests, VmExecutionSession, ADR 0002 |
| FR-007–FR-009: I/O-Grenze, sichere Diagnosen, Parität | VirtualMachineParityTests.Run_And_Step_Have_Parity_For_Input_And_Host_Io_Faults |
| FR-010–FR-013: Paketidentität, Inhalt, exakte Abhängigkeit, Consumer | Test-NuGetPackages.ps1, package-validation.json, PackageWorkflowContractTests |
| FR-014–FR-020: Unveränderlichkeit, OIDC, drei OS, Teilrelease | release-please.yml, ci.yml, S-ADR 0002 |
| FR-021–FR-023: öffentlicher Consumer, Supply Chain, Traceability | New-NuGetReleaseEvidence.ps1, SPDX/OpenVEX/Provenienz, diese Matrix |
| FR-024–FR-026: Handoff und Closeout-Grenze | T041–T050 bleiben außerhalb dieser lokalen Phase; TinyCalc-Handoff ist vorbereitet |
| CR-001–CR-003: Architektur und sichere Grenzen | ARCHITECTURE.md, threat-model.md, arc42-security.md |
| CR-004–CR-006: DE/EN B2, XML, WCAG | HOSTING_VM.md, Paket-READMEs, DocFX/axe/lynx-Gate |
| CR-007–CR-013: Pakete, Evidence, Standards, Statistik | csproj-Metadaten, Release-Evidence, Security-Dokumente, project-statistics.md |
| SC-001–SC-004 | Paritäts-, Cancellation-, Budget- und Idempotenztests |
| SC-005–SC-008 | CI-Matrix, Consumer, Paar-Hashes, sichere Fehler |
| SC-009–SC-012 | DocFX-A11Y, Doku/Statistik, fail-closed Release und Handoff |

Deutsch: Lokale Evidence behauptet keine Remote-CI, unabhängige Freigabe oder
öffentliche Veröffentlichung. Diese Zustände werden erst durch T041–T050
kausal belegt. / English: Local evidence does not claim remote CI, independent
approval, or public publication. Tasks T041–T050 establish those later states.
