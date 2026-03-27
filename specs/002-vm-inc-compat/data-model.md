# Data Model: VM Compatibility for Historical `Inc` Opcode

## Entitaeten und Begriffe

### 1. `Opcode` / Stack-Reservierungsoperation

**Bestehend**:
```text
Opcode.Int = 5
```

**Bedeutung**:
- Reserviert `a` Stack-Zellen in der VM.
- Wird von TinyPl0 intern als `Opcode.Int` repraesentiert.
- Wird in kanonischen textuellen Ausgaben als `int` geschrieben.

**Neue Klarstellung**:
- Historisches Eingabe-Mnemonic `Inc` wird bei textueller P-Code-Verarbeitung
  auf denselben internen Wert `Opcode.Int` abgebildet.
- Die Alias-Beziehung gilt nur fuer die Eingabeseite textueller
  VM-/P-Code-Artefakte.

---

### 2. `Stack Reservation Opcode Name`

Diese konzeptionelle Entitaet beschreibt den textuellen Namen, der in einer
P-Code-Zeile an Opcode-Position 1 steht.

```text
StackReservationOpcodeName
  InputForms:
    - "int"
    - "Int"
    - "INT"
    - "inc"
    - "Inc"
    - "INC"
    - weitere Mischformen gemaess case-insensitive Parsing
  CanonicalOutputForm:
    - "int"
  InternalMapping:
    - Opcode.Int
```

**Constraints**:
- Parsing bleibt case-insensitive fuer Mnemonics.
- Nur `Inc`/`inc` und `Int`/`int` sind fuer diese Operation gueltige
  alphabetische Eingabeformen.
- Aehnliche, aber ungueltige Namen wie `incc`, `inta` oder `imc` muessen weiter
  als Fehler abgewiesen werden.

---

### 3. `Instruction`

**Bestehend**:
```text
Instruction
  Op       : Opcode
  Level    : int
  Argument : int
```

**Beziehung zum Feature**:
- Beim Parsen von `Inc 0 4` entsteht dieselbe `Instruction`-Instanz wie bei
  `Int 0 4` oder `int 0 4`:

```text
new Instruction(Opcode.Int, 0, 4)
```

**Unveraenderte Invarianten**:
- `Level` und `Argument` folgen weiterhin denselben Integer-Parse-Regeln.
- Der Alias aendert nichts an der VM-Ausfuehrung, nur an der Token-Zuordnung
  fuer `Op`.

---

### 4. `Textual VM/P-Code Artifact`

Ein textuelles Artefakt besteht aus null oder mehr Zeilen im bekannten Format:

```text
<opcode> <level> <argument>
```

Beispiel mit historischem Alias:

```text
Inc 0 4
Lit 0 1
Opr 0 0
```

Beispiel in TinyPl0-Kanonschreibweise:

```text
int 0 4
lit 0 1
opr 0 0
```

**Validierungsregeln**:
- Zeilen mit drei Teilen bleiben Pflicht.
- Numerische Opcodes bleiben unveraendert gueltig.
- Alphabetische Opcodes werden case-insensitive geparst.
- `Inc` wird wie `Int` auf `Opcode.Int` abgebildet.

---

## Beziehungsuebersicht

```text
Textual VM/P-Code Artifact
  -> tokenisiert erste Spalte als StackReservationOpcodeName
  -> PCodeSerializer.ParseOpcode()
  -> mappt "inc"/"int" auf Opcode.Int
  -> erzeugt Instruction(Op = Opcode.Int, Level, Argument)
  -> VirtualMachine fuehrt identisch aus
  -> PCodeSerializer.ToAsm() serialisiert wieder als "int"
```

## Zustands- und Transformationsregeln

### Eingabe nach intern

| Eingabetoken | Interner Wert |
|--------------|---------------|
| `Inc` | `Opcode.Int` |
| `inc` | `Opcode.Int` |
| `INT` | `Opcode.Int` |
| `int` | `Opcode.Int` |

### Intern nach Ausgabe

| Interner Wert | Textuelle Ausgabe |
|---------------|-------------------|
| `Opcode.Int` | `int` |

### Fehlerfaelle

| Eingabetoken | Erwartetes Verhalten |
|--------------|----------------------|
| `incc` | `FormatException` fuer unbekannten Mnemonic-Opcode |
| `inta` | `FormatException` fuer unbekannten Mnemonic-Opcode |
| `5x` | `FormatException` fuer unbekannten Mnemonic- oder numerischen Opcode |

## Testrelevante Ableitungen

- Alias-Gleichwertigkeit laesst sich ueber Gleichheit der erzeugten
  `Instruction`-Sequenzen pruefen.
- Rueckwaertskompatibilitaet fuer `Int`/`int` laesst sich ueber bestehende
  Roundtrip-Tests absichern.
- Kanonische Ausgabe laesst sich ueber `ToAsm()` auf aus `Inc` geparsten
  Instruktionen pruefen.
