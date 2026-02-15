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
