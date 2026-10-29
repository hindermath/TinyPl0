# TinyPl0 VM-Befehlssatz

## Instruktionsformat
Jede Instruktion besteht aus:
- `op`: Opcode
- `l`: Lexikalische Level-Differenz
- `a`: Argument (Adresse oder Untercode)

Kodiert in C# als:
- [Instruction.cs](https://github.com/hindermath/TinyPl0/blob/main/src/Pl0.Core/Instruction.cs)
- [Opcode.cs](https://github.com/hindermath/TinyPl0/blob/main/src/Pl0.Core/Opcode.cs)

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

### Klarstellung zu `Inc`, `Int` und `int`

Deutsch:
Historische P-Code-Artefakte schreiben die Stack-Reservierungsoperation oft als
`Inc`. TinyPl0 liest diese Eingabe ebenfalls ein und bildet sie intern auf
denselben Opcode `Int` ab. In TinyPl0-Listings, `ToAsm()`-Ausgaben und der
gepflegten Dokumentation bleibt die kanonische Textform trotzdem `int`, damit
aktuelles Lernmaterial und bestehende Artefakte konsistent bleiben.

English:
Historical P-Code artifacts often write the stack-reservation operation as
`Inc`. TinyPl0 accepts that input as well and maps it internally to the same
`Int` opcode. In TinyPl0 listings, `ToAsm()` output, and the maintained
documentation, the canonical text form still stays `int` so current teaching
material and existing artifacts remain consistent.

## OPR-Untercodes
| Untercode | Bedeutung |
|---:|---|
| 0 | Return (Frame verlassen) |
| 1 | Vorzeichenwechsel (`-x`) |
| 2 | Addition |
| 3 | Subtraktion |
| 4 | Multiplikation |
| 5 | Vorzeichenbehaftete Integer-Division mit Abschneiden gegen null |
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
- [VirtualMachine.cs](https://github.com/hindermath/TinyPl0/blob/main/src/Pl0.Vm/VirtualMachine.cs)

## Registermodell
- `P`: Program Counter
- `B`: Basiszeiger (aktueller Aktivierungsrahmen)
- `T`: Stack-Top

Statische Kette (`base(l)`) wird ueber `ResolveBase` aufgeloest.

## Definierte Laufzeitdiagnosen
| Code | Bedeutung |
|---:|---|
| 206 | Division durch 0 |
| 207 | Ungültiges oder ausgeschöpftes Instruktionsbudget |
| 208 | Ungültige Stackgröße außerhalb `3..1_000_000` |
| 212 | Nicht darstellbare Integer-Division `-2147483648 / -1` |
| 98 | EOF bei Integer-Eingabe |
| 97 | Ungueltiges Integer-Format bei Eingabe |
| 99 | Sonstiger VM-Laufzeitfehler (z. B. Stack-/Pointerfehler) |

OPR 5 prüft Division durch null und den einzigen in `Int32` nicht
darstellbaren Quotienten vor einer Stackänderung. Beide Fälle enden als
kontrollierter `ArithmeticFault`; es verlässt keine CLR-Ausnahme die VM-Grenze.
Alle anderen Quotienten behalten die C#-Semantik mit Abschneiden gegen null.

*OPR 5 checks division by zero and the only quotient that `Int32` cannot
represent before mutating the stack. Both cases terminate as controlled
`ArithmeticFault` results; no CLR exception escapes the VM boundary. All other
quotients retain C# truncation-towards-zero semantics.*

OPR 0 prüft vor einer Registeränderung den aktuellen Aufrufrahmen, die
Rücksprungadresse und den wiederherzustellenden dynamischen Link. Die
Root-Sentinel-Kombination `B=1` mit Rücksprungadresse `0` beendet regulär;
der danach nicht mehr dereferenzierte Link bleibt mit historischem,
handgeschriebenem P-Code kompatibel. Beschädigte Nicht-Root-Rahmen enden als
`StackFault` mit Diagnose 99; Batch und Step geben keine CLR-Indexausnahme an
den Host weiter. Eine außerhalb des Programms liegende, aber ansonsten
lesbare Rücksprungadresse bleibt wie bisher ein kontrollierter `RuntimeFault`
an der nächsten Instruktionsgrenze.

*OPR 0 validates the current call frame, return address, and restored dynamic
link before mutating registers. The root-sentinel combination `B=1` with
return address `0` halts normally; its link is no longer dereferenced and
remains compatible with historical handwritten P-Code. Corrupted non-root
frames end as a controlled `StackFault` with diagnostic 99; neither batch nor
step exposes a CLR index exception to the host. A readable return address
outside the program continues to become a controlled `RuntimeFault` at the
next instruction boundary.*

## Ressourcenbudget und Optionsgrenzen / Resource Budget and Option Limits

Deutsch: `VirtualMachineOptions` besitzt als letzten, optionalen Parameter
`InstructionBudget` mit dem Standardwert `1_000_000`. Gültig sind ein Budget
größer als null und eine Stackgröße von `3` bis `1_000_000`. Beide VM-Wege
prüfen diese Werte vor Addition, Speicherallokation und Ausführung. Ungültige
Werte erzeugen kontrollierte Diagnosen statt Konfigurations-Exceptions.

Eine erfolgreich ausgewählte Instruktion verbraucht genau eine Budgeteinheit.
Nach genau `N` Instruktionen stoppt die VM vor der Auswahl und vor möglichen
Nebenwirkungen von Instruktion `N+1`. Batch- und Step-Ausführung verwenden
denselben Zählpunkt und dieselben Diagnosecodes. Ein Step-Fehler ist terminal;
wiederholtes `Step()` fügt keine zweite Budgetdiagnose hinzu. Das Budget zählt
Instruktionen. Es verspricht keine maximale Laufzeit und ersetzt keine
Betriebssystem- oder Agentensandbox.

English: `VirtualMachineOptions` adds the optional final parameter
`InstructionBudget`, defaulting to `1_000_000`. A valid budget is greater than
zero, and a valid stack size is from `3` through `1_000_000`. Both VM paths
validate these values before arithmetic, allocation, and execution. Invalid
values produce controlled diagnostics rather than configuration exceptions.

One successfully selected instruction consumes one budget unit. After exactly
`N` instructions, the VM stops before selecting or causing side effects from
instruction `N+1`. Batch and step execution use the same counting point and
diagnostic codes. A step failure is terminal and repeated `Step()` calls do not
duplicate the budget diagnostic. The budget counts instructions; it is not a
wall-clock or operating-system isolation guarantee.
## Hostgrenzen / Host Boundaries

Deutsch: Vor dem ersten Dispatch prüft die VM Programmlänge, jeden Opcode,
lexikalische Ebenen 0 bis 3, die OPR-Allowlist, nichtnegative Stackargumente
sowie Sprung- und Aufrufziele. Vor jedem weiteren Dispatch gilt die Reihenfolge
terminaler Cache, Cancellation, Instruktionsbudget und erst danach Zählung plus
Ausführung. Ein fehlgeschlagener begonnener Dispatch zählt einmal.

*English: Before the first dispatch, the VM validates program length, every
opcode, lexical levels 0 through 3, the OPR allowlist, non-negative stack
arguments, and jump/call targets. Each later boundary checks cached terminal
state, cancellation, and budget before counting and dispatching. A started
dispatch that fails counts once.*
