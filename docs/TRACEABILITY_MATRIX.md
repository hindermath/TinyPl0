# TinyPl0 Traceability-Matrix

Diese Matrix bildet die Coverage-Gate-Anforderung aus dem Pflichtenheft ab:
- jede Sprachregel aus Abschnitt 4.1.1
- jede VM-Regel aus Abschnitt 4.3

muss mindestens einem Pflichttestfall zugeordnet sein.

## Quelle der Zuordnung
- Maschinenlesbare Matrix:
  - `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/expected/traceability/matrix.json`
- Referenzkatalog der Pflichttestfaelle:
  - `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/expected/catalog/cases.json`

## Automatischer Gate-Test
- `/Users/thorstenhindermann/Codex/TinyPl0/tests/Pl0.Tests/TraceabilityMatrixTests.cs`

Der Test validiert:
1. Vollstaendigkeit aller geforderten Sprachregeln.
2. Vollstaendigkeit aller geforderten VM-Regeln.
3. Jede Regel verweist auf mindestens einen katalogisierten Pflichttestfall.
