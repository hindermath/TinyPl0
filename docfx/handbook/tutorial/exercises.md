# Schritt-fuer-Schritt Uebungen

## Uebung 1: Addition

```text
LIT 0 2
LIT 0 3
OPR 0 2
```

Aufgabe:
- Fuege eine Speicherung in eine Variable hinzu (STO 0 0).
- Gib das Ergebnis mit OPR 0 15 aus.

## Uebung 2: Bedingter Sprung

```text
LIT 0 0
JPC 0 5
LIT 0 1
```

Aufgabe:
- Ergaenze den Code, sodass bei falscher Bedingung ein anderer Wert geladen wird.
- Stelle sicher, dass beide Zweige am Ende das Ergebnis ausgeben.

## Uebung 3: Schleife zaehlen

```text
INT 0 1
LIT 0 0
STO 0 0
LOD 0 0
LIT 0 5
OPR 0 10
JPC 0 12
LOD 0 0
OPR 0 15
LOD 0 0
LIT 0 1
OPR 0 2
STO 0 0
JMP 0 3
OPR 0 0
```

Aufgabe:
- Beschrifte jede Zeile mit einem Kommentar.
- Variiere die obere Grenze der Schleife.
