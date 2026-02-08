# TinyPl0 VM-Befehlssatz

## Instruktionsformat
Jede Instruktion besteht aus:
- `op`: Opcode
- `l`: Lexikalische Level-Differenz
- `a`: Argument (Adresse oder Untercode)

Kodiert in C# als:
- `/Users/thorstenhindermann/Codex/TinyPl0/src/Pl0.Core/Instruction.cs`
- `/Users/thorstenhindermann/Codex/TinyPl0/src/Pl0.Core/Opcode.cs`

## Opcodes
| Opcode | Wert | Bedeutung |
|---|---:|---|
| `lit` | 0 | Konstante `a` auf Stack laden |
| `opr` | 1 | ALU-/Kontrolloperation nach Untercode `a` |
| `lod` | 2 | Variable aus statischer Tiefe `l`, Offset `a` laden |
| `sto` | 3 | Obersten Stackwert in statische Tiefe `l`, Offset `a` speichern |
| `cal` | 4 | Prozeduraufruf mit statischem Link |
| `int` | 5 | Stack um `a` Zellen erweitern |
| `jmp` | 6 | Unbedingter Sprung zu `a` |
| `jpc` | 7 | Bedingter Sprung zu `a` bei `0` auf Stack |

## OPR-Untercodes
| Untercode | Bedeutung |
|---:|---|
| 0 | Return (Frame verlassen) |
| 1 | Vorzeichenwechsel (`-x`) |
| 2 | Addition |
| 3 | Subtraktion |
| 4 | Multiplikation |
| 5 | Division |
| 6 | `odd`-Test |
| 8 | Gleichheit (`=`) |
| 9 | Ungleichheit (`#`) |
| 10 | Kleiner (`<`) |
| 11 | Groesser-gleich (`>=`) |
| 12 | Groesser (`>`) |
| 13 | Kleiner-gleich (`<=`) |
| 14 | Integer-Eingabe (`?`) |
| 15 | Integer-Ausgabe (`!`) |

Implementierung:
- `/Users/thorstenhindermann/Codex/TinyPl0/src/Pl0.Vm/VirtualMachine.cs`

## Registermodell
- `P`: Program Counter
- `B`: Basiszeiger (aktueller Aktivierungsrahmen)
- `T`: Stack-Top

Statische Kette (`base(l)`) wird ueber `ResolveBase` aufgeloest.

## Definierte Laufzeitdiagnosen
| Code | Bedeutung |
|---:|---|
| 206 | Division durch 0 |
| 98 | EOF bei Integer-Eingabe |
| 97 | Ungueltiges Integer-Format bei Eingabe |
| 99 | Sonstiger VM-Laufzeitfehler (z. B. Stack-/Pointerfehler) |
