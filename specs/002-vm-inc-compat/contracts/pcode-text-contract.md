# Contract: Textuelle P-Code-Schnittstelle fuer historischen `Inc`-Alias

## Zweck

Dieser Vertrag beschreibt, wie TinyPl0 textuelle VM-/P-Code-Artefakte fuer die
Stack-Reservierungsoperation einliest und wieder ausgibt.

Er ist fuer dieses Feature die normative Beschreibung der textuellen
Schnittstelle rund um die Stack-Reservierungsoperation; Implementierung,
Quickstart und Review sollen nicht davon abweichen.

## Zeilenformat

Jede nicht-leere, nicht-kommentierte Zeile folgt weiterhin dem bekannten Schema:

```text
<opcode> <level> <argument>
```

## Opcode-Vertrag fuer Stack-Reservierung

| Eingabeform | Gueltig | Interne Abbildung | Ausgabeform |
|-------------|---------|-------------------|-------------|
| `int` | ja | `Opcode.Int` | `int` |
| `Int` | ja | `Opcode.Int` | `int` |
| `INT` | ja | `Opcode.Int` | `int` |
| `inc` | ja | `Opcode.Int` | `int` |
| `Inc` | ja | `Opcode.Int` | `int` |
| `INC` | ja | `Opcode.Int` | `int` |

Regel:
- Alphabetische Mnemonics werden case-insensitive geparst.
- Der historische Alias `Inc` wird ausschliesslich als Eingabeform akzeptiert.
- Textuelle TinyPl0-Ausgaben verwenden fuer diese Operation weiterhin nur `int`.

## Beispiel

### Gueltige historische Eingabe

```text
Inc 0 4
Lit 0 5
Opr 0 0
```

### Interne Darstellung

```csharp
new[]
{
    new Instruction(Opcode.Int, 0, 4),
    new Instruction(Opcode.Lit, 0, 5),
    new Instruction(Opcode.Opr, 0, 0),
}
```

### Kanonische TinyPl0-Ausgabe

```text
int 0 4
lit 0 5
opr 0 0
```

## Fehlervertrag

Die Erweiterung weicht die bestehende Validierung nicht auf.

| Beispiel | Erwartetes Verhalten |
|----------|----------------------|
| `Incc 0 4` | Fehler: unbekannter Mnemonic-Opcode |
| `inta 0 4` | Fehler: unbekannter Mnemonic-Opcode |
| `Inc x 4` | Fehler: ungueltiges `level` |
| `Inc 0 y` | Fehler: ungueltiges `argument` |
| `Inc 0` | Fehler: ungueltige P-Code-Zeile |

## Nicht-Ziele

- Keine Aenderung der numerischen Opcode-Schnittstelle.
- Keine Aenderung der VM-Semantik fuer `Opcode.Int`.
- Keine Umbenennung des internen Enum-Werts `Opcode.Int`.
- Keine Umstellung existierender TinyPl0-Ausgaben auf `Inc`.
