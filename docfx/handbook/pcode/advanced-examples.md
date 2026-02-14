# Ausfuehrliche Beispiele

In diesem Kapitel folgen groessere P-Code-Programme mit kurzen Erklaerungen.

## Beispiel 1: Summe von 1 bis N

Berechnet die Summe 1..N und gibt das Ergebnis aus. Lokale Variablen:
`n` (0), `i` (1), `sum` (2).

```text
INT 0 3
OPR 0 14      // read n
STO 0 0
LIT 0 1
STO 0 1
LIT 0 0
STO 0 2
LOD 0 1
LOD 0 0
OPR 0 10      // i < n
JPC 0 18
LOD 0 2
LOD 0 1
OPR 0 2       // sum + i
STO 0 2
LOD 0 1
LIT 0 1
OPR 0 2       // i + 1
STO 0 1
JMP 0 7
LOD 0 2
OPR 0 15      // print sum
OPR 0 0       // return
```

## Beispiel 2: Maximum von zwei Zahlen

```text
INT 0 3
OPR 0 14
STO 0 0
OPR 0 14
STO 0 1
LOD 0 0
LOD 0 1
OPR 0 12      // a > b
JPC 0 12
LOD 0 0
STO 0 2
JMP 0 14
LOD 0 1
STO 0 2
LOD 0 2
OPR 0 15
OPR 0 0
```
