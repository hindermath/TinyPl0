# Beispiele je Instruktion

Dieses Kapitel zeigt fuer jede Instruktion ein einfaches Beispiel. Die Kommentare
erklaeren jeweils die Wirkung auf Stack und Kontrollfluss.

## LIT

```text
LIT 0 5       // push 5
```

## LOD

```text
LOD 0 0       // load variable at level 0, offset 0
```

## STO

```text
LIT 0 7
STO 0 0       // store 7 into variable 0
```

## CAL

```text
CAL 0 10      // call procedure at address 10
```

## INT

```text
INT 0 3       // reserve 3 cells for locals (plus 3 control cells)
```

## JMP

```text
JMP 0 20      // jump to instruction 20
```

## JPC

```text
LIT 0 0
JPC 0 12      // if top is 0, jump to 12
```

## OPR (Auswahl)

### Return (OPR 0 0)
```text
OPR 0 0       // return from procedure
```

### Negation (OPR 0 1)
```text
LIT 0 4
OPR 0 1       // result: -4
```

### Addition (OPR 0 2)
```text
LIT 0 1
LIT 0 2
OPR 0 2       // result: 3
```

### Subtraktion (OPR 0 3)
```text
LIT 0 10
LIT 0 3
OPR 0 3       // result: 7
```

### Multiplikation (OPR 0 4)
```text
LIT 0 6
LIT 0 7
OPR 0 4       // result: 42
```

### Division (OPR 0 5)
```text
LIT 0 10
LIT 0 3
OPR 0 5       // result: 3 (Integer)
```

### odd (OPR 0 6)
```text
LIT 0 5
OPR 0 6       // result: 1
```

### Vergleich (OPR 0 8..13)
```text
LIT 0 2
LIT 0 2
OPR 0 8       // result: 1 (gleich)
```

### Read (OPR 0 14)
```text
OPR 0 14      // read int from input, push on stack
```

### Write (OPR 0 15)
```text
LIT 0 9
OPR 0 15      // write 9 to output
```

## Ausfuehrliches Mini-Programm (Summe)

```text
INT 0 3       // locals: x, y, sum
LIT 0 2
STO 0 0
LIT 0 3
STO 0 1
LOD 0 0
LOD 0 1
OPR 0 2       // x + y
STO 0 2
LOD 0 2
OPR 0 15      // print sum
```
