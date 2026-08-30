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
