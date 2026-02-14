# Instruktions-Referenz

Dieses Kapitel beschreibt alle P-Code-Elemente, die die VM versteht.

## Grundform

```
OPCODE LEVEL ARG
```

- `OPCODE`: Mnemonic (z. B. LIT, LOD)
- `LEVEL`: Lexikalische Ebene (statisch verschachtelte Prozeduren)
- `ARG`: Argument oder Adresse

## Instruktionen

### LIT

Laedt eine Konstante auf den Stack.

### LOD

Laedt eine Variable von einer (LEVEL, ARG)-Adresse auf den Stack.

### STO

Speichert den Wert vom Stack in eine Variable an (LEVEL, ARG).

### CAL

Ruft eine Prozedur an der Zieladresse auf und legt einen neuen Stack-Frame an.

### INT

Reserviert Speicher auf dem Stack fuer lokale Variablen.

### JMP

Unbedingter Sprung zur Zieladresse.

### JPC

Bedingter Sprung zur Zieladresse, wenn der Stack-Top 0 (false) ist.

### OPR

Fuehrt Operationen aus. Die Bedeutung von `ARG` ist:

- 0: Return (Rueckkehr aus Prozedur)
- 1: Negation
- 2: Addition
- 3: Subtraktion
- 4: Multiplikation
- 5: Division (Integer)
- 6: odd-Test
- 8: Gleichheit
- 9: Ungleichheit
- 10: Kleiner
- 11: Groesser gleich
- 12: Groesser
- 13: Kleiner gleich
- 14: Read (Eingabe)
- 15: Write (Ausgabe)

Die vollstaendige Referenz wird zusaetzlich aus dem VM-Instruction-Set kuratiert eingebunden.
