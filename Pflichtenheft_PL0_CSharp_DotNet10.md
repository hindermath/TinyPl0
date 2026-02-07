# Pflichtenheft: Portierung PL/0-Compiler von Pascal nach C#/.NET 10

## 1. Ziel und Zweck
Ziel ist die funktionsgleiche Portierung des in `PL0.md` dokumentierten PL/0-Beispielcompilers (inklusive Laufzeit/Interpreter) von Pascal nach C# auf .NET 10.

Das Ergebnis soll ein didaktisch klarer, wartbarer Referenzcompiler sein, der:
- die definierte PL/0-Syntax akzeptiert,
- P-Code gemäß dem dokumentierten Befehlssatz erzeugt,
- diesen P-Code in einer virtuellen Maschine ausführt.

## 2. Ausgangslage (Ist-Analyse aus `PL0.md`)
Referenzquelle: `/Users/thorstenhindermann/Codex/TinyPl0/PL0.md`

Technische Kernelemente:
- EBNF der Sprache (Programm, Block, Statements, Bedingungen, Ausdrücke).
- Ein monolithischer Pascal-Compiler mit folgenden Teilsystemen:
  - Lexer (`getsym`/`getch`)
  - Rekursiver Abstiegparser (`block`, `statement`, `condition`, `expression`, `term`, `factor`)
  - Symboltabelle (`table`, `enter`, `position`)
  - Codegenerator (`gen`) für P-Code
  - Interpreter/VM (`interpret`) für `lit, opr, lod, sto, cal, int, jmp, jpc`
- Sprach-/Laufzeitgrenzen:
  - nur `integer`
  - keine Parameter, keine Funktionsrückgabewerte
  - verschachtelte Prozeduren über lexikalische Ebenen
  - Stack-basierte Laufzeit mit Registern `P`, `B`, `T`, `I`

### 2.1 Ergänzende Grammatik-Referenz (ANTLR)
Zusätzliche Referenzquelle:
- [ANTLR PL/0 Grammar (`pl0.g4`)](https://github.com/antlr/grammars-v4/blob/master/pl0/pl0.g4)

Auswertung für den Sollumfang:
- Die ANTLR-Grammatik enthält explizit Statement-Formen für
  - Eingabe: `? ident`
  - Ausgabe: `! expression`
- Damit werden `qstmt`/`bangstmt` als reguläre Sprachfeatures in den Zielumfang aufgenommen.

### 2.2 Analyse der konkreten Beispiel-Implementierung (Pascal)
Die vorliegende Beispielimplementierung ist nicht nur Spezifikation, sondern konkrete Referenz mit einigen historischen Eigenheiten. Diese Punkte sind für die Portierung verbindlich zu berücksichtigen:

1. Gesamtaufbau als Monolith:
- Compiler und VM sind in einem Programm kombiniert.
- Ablauf: Initialisierung -> Parsen/Codegenerierung -> bei `err=0` direkte Interpretation.

2. Harte Systemgrenzen und feste Arrays:
- `txmax=100` (Symboltabellenlänge), `cxmax=200` (Codegröße), `stacksize=500`, `levmax=3`.
- Die C#-Portierung soll diese Grenzen mindestens konfigurierbar anbieten; Standardwerte an der Referenz orientieren.

3. Lexer-Verhalten:
- Identifikatoren: nur beginnend mit `a..z`; Verarbeitung ist auf Kleinbuchstaben ausgelegt.
- Maximale Identifikatorlänge `al=10`, Zahlbreite `nmax=14`.
- Reserved-Word-Erkennung erfolgt über sortierte Tabellen + binäre Suche.
- Einzelzeichentabelle `ssym` enthält historische Abbildungen; insbesondere `[` -> `<=` und `]` -> `>=` in der gezeigten Quelle.

4. Parser-/Grammatik-Abweichung:
- Die EBNF nennt `? ident` und `! expression` im `statement`.
- Die konkrete Pascal-Implementierung implementiert diese beiden Formen nicht.
- Für die C#-Portierung wird dies als gezielte, dokumentierte Erweiterung aufgenommen (ANTLR-kompatibler Modus), bei optionalem Classic-Kompatibilitätsmodus.

5. Block-/Codegen-Mechanik:
- `block` reserviert mit `dx:=3` die drei Verwaltungszellen im Stackframe.
- Zu Blockbeginn wird `jmp` mit Platzhalter erzeugt, später auf den Statement-Start zurückgepatcht.
- Prozeduradressen und lexikalische Level werden in der Symboltabelle geführt; Auflösung erfolgt über `lev-level`.

6. Symboltabelle:
- Lineare Rückwärtssuche mit Sentinel (`table[0].name := id`) in `position`.
- Kinds: `constant`, `varible`, `proc` (historische Schreibweise `varible` ist im Quelltext bewusst beizubehalten oder sauber zu mappen).

7. Interpreter-Semantik:
- Aktivierungsrahmen mit Static Link, Dynamic Link, Return Address (`s[t+1..t+3]` bei `cal`).
- `base(l)` folgt Static-Link-Kette.
- `opr 0 0` stellt `t`, `p`, `b` aus dem aktuellen Frame wieder her.

8. Beobachtbare Nebeneffekte der Referenz:
- `sto` führt `writeln(s[t])` aus; dadurch entsteht Ausgabe bei Zuweisungen.
- Die VM-Beschreibung sagt zwar "kein I/O", die Beispielimplementierung hat aber diesen didaktischen Output.
- Das Portierungsziel muss diesen Punkt explizit entscheiden (kompatibler Debug-Output per Option empfohlen).

9. Fehlerbehandlung:
- Diagnosen über numerische Fehlercodes (`error(n)`), Recovery über `test(...)`.
- Abbruchpfad teilweise per `goto 99`.
- In C# soll das Verhalten funktional erhalten bleiben, aber technisch als strukturierte Diagnostics ohne `goto`.

## 3. Produktdefinition (Soll)

### 3.1 Muss-Ziele
- Funktionsäquivalente Umsetzung des Originalverhaltens.
- Kompilierung und Ausführung eines PL/0-Programms in einem CLI-Tool.
- Beibehaltung des didaktischen Compileraufbaus (sichtbare Phasen).
- Reproduzierbare Tests mit Referenzprogrammen.
- Nachweis der Verhaltenskompatibilität zur konkreten Pascal-Beispielimplementierung (nicht nur zur EBNF).

### 3.2 Abgrenzung (nicht im Scope)
- Keine Sprach-Erweiterungen über die konsolidierte PL/0-Definition (`PL0.md` + ANTLR-Referenz) hinaus.
- Keine Optimierungen (Peephole, SSA etc.).
- Kein JIT/IL-Backend; Ziel bleibt die PL/0-P-Code-VM.

## 4. Fachliche Anforderungen

### 4.1 Sprachumfang (PL/0)
Umzusetzen gemäß konsolidierter EBNF aus `PL0.md` plus ANTLR-Referenz:
- Deklarationen: `const`, `var`, `procedure`
- Statements: Zuweisung, `call`, `? ident`, `! expression`, `begin...end`, `if...then`, `while...do`
- Bedingungen: `odd`, Relationen `= # < <= > >=`
- Arithmetik: unär `+/-`, binär `+ - * /`

Hinweis zur historischen Notation:
- In Wirth-Varianten werden `<=` und `>=` teils über spezielle Einzelsymbole codiert; die C#-Portierung soll benutzerseitig die erwartete Schreibweise `<=` und `>=` akzeptieren und intern eindeutig mappen.

Referenzpräzisierung:
- Kein `else` im Kernumfang.
- `?` und `!` sind als zu implementierende Sprachmerkmale verbindlich (über ANTLR-Referenz abgesichert), trotz fehlender Umsetzung im historischen Pascal-Beispiel.

### 4.1.1 Konsolidierte EBNF (Soll)
```ebnf
program   = block "." .
block     = [ "const" ident "=" number { "," ident "=" number } ";" ]
            [ "var" ident { "," ident } ";" ]
            { "procedure" ident ";" block ";" }
            statement .
statement = [ ident ":=" expression
            | "call" ident
            | "?" ident
            | "!" expression
            | "begin" statement { ";" statement } "end"
            | "if" condition "then" statement
            | "while" condition "do" statement ] .
condition = "odd" expression
          | expression ( "=" | "#" | "<" | "<=" | ">" | ">=" ) expression .
expression= [ "+" | "-" ] term { ( "+" | "-" ) term } .
term      = factor { ( "*" | "/" ) factor } .
factor    = ident | number | "(" expression ")" .
```

### 4.2 Compilerphasen
- Lexikalische Analyse:
  - Tokenisierung von Identifikatoren, Zahlen, Schlüsselwörtern, Operatoren und Trennzeichen.
  - Erkennung der Terminale `?` und `!` als eigene Tokens.
  - Fehler bei ungültigen Zeichenfolgen.
- Syntaxanalyse:
  - Rekursiver Abstieg nach der EBNF.
  - Panic-Mode/Follow-Set-basierte Fehlererholung analog `test(...)`.
- Semantik:
  - Symbolauflösung über Block-/Levelgrenzen.
  - Prüfung von Deklaration vor Benutzung.
  - Typ-/Objektprüfungen gemäß Original (`const`, `var`, `proc`).
  - Für `? ident`: Ziel muss veränderbar (`var`) sein.
  - Für `! expression`: Ausdruck muss vollständig auswertbar sein.
- Codegenerierung:
  - Erzeugung eines linearen Instruktionsarrays.
  - Backpatching von Sprüngen (`jmp`, `jpc`).
  - Erzeugung von I/O-Operationen für `?` und `!` im Extended-Modus.

### 4.3 Laufzeit (VM)
Pflicht ist die Implementierung der Instruktionen:
- `LIT`, `OPR`, `LOD`, `STO`, `CAL`, `INT`, `JMP`, `JPC`
- `OPR`-Unterfunktionen:
  - `0` Return
  - `1` Negation
  - `2` Add
  - `3` Sub
  - `4` Mul
  - `5` Div
  - `6` Odd
  - `8` Eq
  - `9` Neq
  - `10` Lt
  - `11` Ge
  - `12` Gt
  - `13` Le

Semantik:
- Stackframe-Layout mit Static Link, Dynamic Link, Return Address wie im Pascal-Original.
- Lexikalische Ebenenauflösung über `base(l)`.
- Optionaler "Store-Trace"-Modus zur Reproduktion des Referenz-Outputs bei `sto`.

I/O-Erweiterung (verbindlich):
- Für `? ident` und `! expression` wird ein Runtime-I/O-Adapter definiert (`IPl0Io`), der Integer liest/schreibt.
- VM/Runtime muss diese Operationen deterministisch testbar machen (stdin/stdout sowie testbare In-Memory-Implementierung).

### 4.4 Kompatibilitätsanforderungen zur Referenzimplementierung
- Die C#-Implementierung muss für definierte Referenzprogramme dieselbe Instruktionsfolge (Opcode/L/A) erzeugen oder eine dokumentiert äquivalente Folge.
- Für verschachtelte Prozeduren müssen Static-Link-Auflösung und Variable-Zugriffe identisches Verhalten zeigen.
- Fehlerfälle (z. B. undeclared identifier, falsches Zuweisungsziel, fehlendes `then`/`do`) müssen reproduzierbar diagnostiziert werden.
- Historische Limits (`levmax`, `cxmax`, `txmax`) sollen als konfigurierbare Guardrails vorhanden sein und bei Überschreitung klare Fehler melden.
- Zwei Dialekte sind vorzusehen:
  - `classic`: strikt kompatibel zur Pascal-Referenz ohne `?`/`!`
  - `extended`: konsolidierte Grammatik inkl. `?`/`!`

## 5. Nicht-funktionale Anforderungen
- Plattform: .NET 10, Sprache C# (aktueller Sprachstandard kompatibel zu .NET 10 SDK).
- Ausführung als Konsole (Windows/macOS/Linux).
- Lesbarkeit vor Mikro-Performance.
- Deterministische Tests in CI.
- Robuste Fehlermeldungen mit Quellpositionsangaben (Zeile/Spalte).

## 6. Technische Zielarchitektur (C#)

### 6.1 Module
- `Pl0.Core`
  - `Lexer`
  - `Parser`
  - `SymbolTable`
  - `CodeGenerator`
  - `Instruction`/`Opcode`/`ObjectKind`/`Symbol`
- `Pl0.Vm`
  - `VirtualMachine`
  - `StackFrame`-Hilfen / `BaseResolver`
- `Pl0.Cli`
  - Datei laden
  - Kompilieren
  - optional Listen des Codes
  - Ausführen

### 6.2 Datenmodelle (Soll-Mapping)
- Pascal `symbol` -> C# `enum TokenKind`
- Pascal `object` -> C# `enum SymbolKind`
- Pascal `instruction` -> C# `readonly record struct Instruction(Opcode Op, int L, int A)`
- Pascal `table[...]` -> C# `List<SymbolEntry>` + ggf. Scope-Indexe
- Pascal globale Zustände -> kapselnde Klassen mit explizitem Zustand

### 6.3 Fehlerbehandlung
- Ersetze `goto 99` durch Exceptions/Result-Objekte.
- Sammeln mehrerer Diagnosemeldungen (Severity, Code, Position, Text).

### 6.4 Explizite Pascal->C# Mapping-Tabelle (pro Prozedur/Funktion)
| Pascal-Prozedur/Funktion | Zweck in der Referenz | C# Ziel (Vorschlag) | Portierungsregeln |
|---|---|---|---|
| `error(n)` | Fehlermeldung/Fehlerzähler | `DiagnosticBag.Report(code, location, message)` | Numerische Codes beibehalten; Text zentralisieren. |
| `getch` (lokal in `getsym`) | Zeicheneinlesung/Zeilenpuffer | `Lexer.ReadChar()` | Zeile/Spalte exakt pflegen; EOF als eigener Pfad. |
| `getsym` | Lexikalische Analyse, nächstes Token | `Lexer.NextToken()` | Reserved-Words + Symbolmapping kompatibel halten; historische Operatorabbildung dokumentieren. |
| `gen(x,y,z)` | Instruktion emittieren | `CodeGenerator.Emit(op, l, a)` | Kapazitätsgrenzen prüfen; Index rückgeben für Backpatching. |
| `test(s1,s2,n)` | Panic-Mode Recovery | `Parser.ExpectOrSync(expected, sync, errorCode)` | Follow-Set-basierte Erholung wie Referenz. |
| `block(lev,tx,fsys)` | Deklarationen, verschachtelte Blöcke, Codegen-Root | `Parser.ParseBlock(context)` + `CodeGenerator` | `dx:=3`, initiales `jmp` und Backpatching identisch umsetzen. |
| `enter(k)` (lokal in `block`) | Symbol eintragen | `SymbolTable.Enter(entry)` | Level/Adresse/Kind wie Original führen; Grenzprüfungen zentral. |
| `position(id)` (lokal in `block`) | Symbolsuche (rückwärts) | `SymbolTable.Lookup(name, scope)` | Sichtbarkeitsregeln identisch; Sentinel-Trick nicht erforderlich. |
| `constdeclaration` | `const`-Deklarationen parsen | `Parser.ParseConstDeclaration()` | Fehlercodes bei `=`/`:=`/Zahl kompatibel abbilden. |
| `vardeclaration` | `var`-Deklarationen parsen | `Parser.ParseVarDeclaration()` | Adressvergabe über Data-Index (`dx`) beibehalten. |
| `listcode` | Instruktionslisting | `CodeListingWriter.Write(program)` | CLI-Option `--list-code`; Format stabil halten. |
| `statement(fsys)` | Statement-Dispatcher | `Parser.ParseStatement(syncSet)` | Zuweisung, `call`, `begin/end`, `if`, `while` kompatibel; kein `else` im Kernmodus. |
| `qstmt` (ANTLR) | Eingabestatement `? ident` | `Parser.ParseReadStatement()` + `CodeGenerator.EmitRead(...)` | Nur `var` als Ziel zulassen; Dialekt `extended` erforderlich. |
| `bangstmt` (ANTLR) | Ausgabestatement `! expression` | `Parser.ParseWriteStatement()` + `CodeGenerator.EmitWrite(...)` | Ausdruck auswerten, Ergebnis über Runtime-I/O ausgeben. |
| `condition(fsys)` | Bedingungen/Relationen | `Parser.ParseCondition(syncSet)` + `CodeGenerator` | `odd` und Relationen auf `OPR`-Subcodes mappen. |
| `expression(fsys)` | `+/-` Präzedenzebene | `Parser.ParseExpression(syncSet)` | Unäres Minus via `OPR 0 1` wie Referenz. |
| `term(fsys)` | `* /` Präzedenzebene | `Parser.ParseTerm(syncSet)` | Multiplikation/Division auf `OPR 0 4/5`. |
| `factor(fsys)` | Ident/Number/Klammer | `Parser.ParseFactor(syncSet)` | `const`->`LIT`, `var`->`LOD`, `proc` als Fehler. |
| `interpret` | VM-Hauptschleife | `VirtualMachine.Run(program)` | Register `P,B,T` und Dispatch über Opcode exakt spiegeln. |
| `base(l)` (lokal in `interpret`) | Static-Link-Auflösung | `VirtualMachine.ResolveBase(level)` | Static-Link-Kette identisch traversieren. |
| `main program` (Initialisierung) | Tabellenaufbau, Start der Pipeline | `Program.cs` + `CompilerPipeline` | Initialisierung explizit und testbar machen; kein globaler Zustand. |

Ergänzende Implementierungsregel:
- Für jede gemappte Prozedur/Funktion ist mindestens ein fokussierter Unit- oder Integrationstest bereitzustellen, der das Referenzverhalten nachweist.

## 7. Migrationsplan (Umsetzungsphasen)

### Phase 0: Vorbereitung
- git Repository anlegen und initialisieren.
- README mit Zielsetzung, Referenzverhalten und Build-Anleitung erstellen.
- .gitignore für C#-Projekte einrichten. Dabei von gitignore.io generieren lassen, um typische C#-, .NET-, JetBrains- und Visual Studio-Dateien auszuschließen.
- das Repo auf github mit dem URL https://github.com/hindermath/TinyPl0.git pushen

### Phase 1: Projektgrundlage
- .NET-10-Solution mit drei Projekten (`Core`, `Vm`, `Cli`) anlegen.
- Build, Testprojekt, CI-Template einrichten.
- Enumerationen/Records für Tokens, Symbole, Instruktionen definieren.

Ergebnis:
- Kompilierbare Projektstruktur ohne Fachlogik.

### Phase 2: Lexer
- Zeicheneinlesung mit Zeilen-/Spaltenzählung.
- Reserved Words und Symboltabellen wie Original abbilden.
- Tokenstream mit End-of-File-Token.

Ergebnis:
- Golden-Tests für Tokenisierung.

### Phase 3: Parser + Symboltabelle + Codegenerator
- Rekursiven Abstieg 1:1 semantisch portieren.
- `block`, `statement`, `condition`, `expression`, `term`, `factor` umsetzen.
- Backpatching und Level-/Adresslogik implementieren.
- Fehlercodes/Folgesets definieren.
- `qstmt`/`bangstmt` im `extended`-Dialekt implementieren; Dialektumschaltung testen.

Ergebnis:
- Aus PL/0-Quellen entsteht korrektes P-Code-Programm.

### Phase 4: VM/Interpreter
- Register `P,B,T,I`, Stack, `base(l)` implementieren.
- Alle `OPR`-Subcodes und Instruktionen ausführen.
- Verhalten für Return/Call-Frames verifizieren.
- Store-Ausgabeverhalten als konfigurierbaren Kompatibilitätsmodus umsetzen und testen.
- I/O-Adapter für `?`/`!` anbinden und mit In-Memory-Testdouble validieren.

Ergebnis:
- P-Code läuft deterministisch und kompatibel zum Referenzverhalten.

### Phase 5: CLI und End-to-End
- CLI-Befehle, z. B.:
  - `compile <file.pl0> --out <file.pcode>`
  - `run <file.pl0>` oder `run-pcode <file.pcode>`
  - `--list-code` für didaktische Ausgabe.
- End-to-End-Tests mit Referenzprogrammen.

Ergebnis:
- Nutzbares Lehr-/Demo-Tool.

### Phase 6: Qualität und Dokumentation
- README (Sprache, Einschränkungen, Beispiele).
- Architekturdiagramm und Mapping-Tabelle Pascal->C#.
- Testabdeckung für Kernpfade.

Ergebnis:
- Abnahmefähiges, dokumentiertes Beispielsystem.

## 8. Test- und Abnahmekonzept

### 8.1 Testarten
- Unit-Tests:
  - Lexer-Tokens
  - Parserproduktionen
  - Symboltabellenauflösung
  - VM-Instruktionen
- Integrationstests:
  - Kompilieren von PL/0-Beispielen zu erwarteten Instruktionsfolgen
- End-to-End:
  - PL/0-Quelle -> P-Code -> VM-Ausführung -> erwarteter Stack-/Ausgabewert
- Kompatibilitätstests:
  - Golden-Master-Vergleich gegen Referenz-Instruktionslisten
  - Tests für historische Spezialfälle (`[`/`]` Mapping, Levelauflösung, Fehler-Recovery)

### 8.2 Testdatenkatalog: `.pl0`-Beispielprogramme (Sprachfeatures)
Die Sprachfeatures sind über feste Referenzprogramme abzudecken. Diese Programme werden versioniert im Repository gehalten und automatisiert ausgeführt.

Vorgeschlagene Struktur:
- `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/pl0/valid/`
- `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/pl0/invalid/`
- `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/expected/`

Pflichtfälle `valid`:
1. `feature_const_var_assignment.pl0`
- Deckt `const`, `var`, Zuweisung, arithmetische Grundoperationen ab.
- Erwartung: erfolgreiche Kompilierung; stabile Instruktionsfolge; deterministischer Endzustand.

2. `feature_begin_end_sequence.pl0`
- Deckt Statement-Sequenzen und Blockbildung ab.
- Erwartung: korrekte Reihenfolge der generierten `STO`/`OPR`-Operationen.

3. `feature_if_then_relops.pl0`
- Deckt `if ... then` mit `= # < <= > >=` ab.
- Erwartung: pro Relation korrekte `OPR`-Subcode-Generierung (`8..13`) und korrektes `JPC`-Verhalten.

4. `feature_while_do.pl0`
- Deckt Schleifen-Backpatching (`JPC`, `JMP`) ab.
- Erwartung: korrekte Sprungadressen und terminierende Ausführung für Testfall.

5. `feature_odd_condition.pl0`
- Deckt `odd expression` ab.
- Erwartung: `OPR 0 6` wird erzeugt; boolesches Verhalten (0/1) korrekt.

6. `feature_procedure_call_nested_levels.pl0`
- Deckt Prozedurdeklaration, Aufruf, verschachtelte Ebenen, `LOD/STO` mit Leveldifferenz ab.
- Erwartung: korrekte `CAL l,a`-Parameter und korrekte `base(l)`-Auflösung.

7. `feature_unary_minus_precedence.pl0`
- Deckt unäres Minus sowie Operatorpräzedenz (`term` vor `expression`) ab.
- Erwartung: `OPR 0 1` und korrekte Auswertungsreihenfolge.

8. `feature_parentheses.pl0`
- Deckt Klammerausdrücke ab.
- Erwartung: Ergebnis unterscheidet sich nachweisbar von ungeklammerter Variante.

9. `feature_input_qstmt.pl0`
- Deckt Eingabe über `? ident` ab.
- Erwartung: eingelesener Integer wird korrekt in Zielvariable gespeichert und weiterverarbeitet.

10. `feature_output_bangstmt.pl0`
- Deckt Ausgabe über `! expression` ab.
- Erwartung: Ausdruck wird korrekt ausgewertet und als Integer-Ausgabe ausgegeben.

11. `feature_io_roundtrip_q_bang.pl0`
- Deckt Kombination aus `?` und `!` in einem Ablauf ab.
- Erwartung: bei definierter Eingabesequenz entsteht exakt die erwartete Ausgabesequenz.

Pflichtfälle `invalid`:
1. `error_undeclared_identifier.pl0`
- Erwartung: Diagnose "nicht deklariert" (kompatibel zu Fehlerfall `position=0`).

2. `error_assign_to_const.pl0`
- Erwartung: Diagnose für unzulässiges Zuweisungsziel.

3. `error_missing_then.pl0`
- Erwartung: Diagnose für fehlendes `then` plus Recovery ohne Prozessabbruch.

4. `error_missing_do.pl0`
- Erwartung: Diagnose für fehlendes `do` plus Recovery.

5. `error_bad_factor.pl0`
- Erwartung: Diagnose für ungültigen Faktor/fehlende Klammer.

6. `error_qstmt_non_variable.pl0`
- Erwartung: Diagnose, wenn `?` auf `const`/`procedure` angewendet wird.

7. `error_bangstmt_missing_expression.pl0`
- Erwartung: Diagnose bei unvollständigem `!`-Statement.

Historische Kompatibilitätsfälle:
1. `compat_relop_brackets.pl0`
- Nutzt historische Schreibweise für `<=`/`>=`-Mapping.
- Erwartung: definierte, dokumentierte Behandlung im Kompatibilitätsmodus.

2. `compat_store_trace_output.pl0`
- Erwartung: bei aktiviertem Store-Trace identische Ausgabecharakteristik zur Referenz (`sto`-Ausgabe), sonst still.

Pflichtfälle `dialect`:
1. `dialect_classic_reject_qstmt.pl0`
- Erwartung: im Modus `classic` Diagnose, dass `?` nicht erlaubt ist.

2. `dialect_classic_reject_bangstmt.pl0`
- Erwartung: im Modus `classic` Diagnose, dass `!` nicht erlaubt ist.

3. `dialect_extended_accept_qstmt.pl0`
- Erwartung: im Modus `extended` erfolgreiche Kompilierung und korrekte I/O-Ausführung von `?`.

4. `dialect_extended_accept_bangstmt.pl0`
- Erwartung: im Modus `extended` erfolgreiche Kompilierung und korrekte Ausgabe von `!`.

Pflichtfälle `limits`:
1. `limit_identifier_max_length_ok.pl0`
- Erwartung: Identifier mit maximal erlaubter Länge wird akzeptiert.

2. `limit_identifier_too_long.pl0`
- Erwartung: Diagnose bei zu langem Identifier.

3. `limit_number_max_digits_ok.pl0`
- Erwartung: Zahl mit maximal erlaubter Stellenzahl wird akzeptiert.

4. `limit_number_too_long.pl0`
- Erwartung: Diagnose bei Überschreitung der zulässigen Ziffernanzahl.

5. `limit_amax_exceeded.pl0`
- Erwartung: Diagnose, wenn Literal/Konstante den erlaubten Wertebereich überschreitet.

6. `limit_levmax_exceeded.pl0`
- Erwartung: Diagnose bei zu tiefer Prozedurverschachtelung.

7. `limit_cxmax_exceeded.pl0`
- Erwartung: Diagnose bei Überschreitung der maximalen Codegröße.

8. `limit_txmax_exceeded.pl0`
- Erwartung: Diagnose bei Überschreitung der Symboltabellengröße.

Zusätzliche Pflichtfälle `invalid/semantics`:
1. `error_duplicate_identifier_same_scope.pl0`
- Erwartung: Diagnose bei doppelter Deklaration im selben Scope.

2. `error_call_non_procedure.pl0`
- Erwartung: Diagnose, wenn `call` auf `const` oder `var` angewendet wird.

3. `error_missing_end.pl0`
- Erwartung: Diagnose für fehlendes `end` mit Recovery.

4. `error_missing_period.pl0`
- Erwartung: Diagnose für fehlenden Programmabschluss `.`.

5. `error_multiple_errors_recovery.pl0`
- Erwartung: mehrere Diagnosen in einem Lauf; kein früher Abbruch nach erstem Fehler.

Zusätzliche Pflichtfälle `runtime/io-edge`:
1. `runtime_division_by_zero.pl0`
- Erwartung: definierte Laufzeitdiagnose (oder definierter Fehlercode) statt undefiniertem Verhalten.

2. `io_input_negative_and_whitespace.pl0`
- Erwartung: negative Integer und führende/folgende Whitespaces werden korrekt verarbeitet.

3. `io_input_non_integer.pl0`
- Erwartung: definierte Eingabediagnose bei nicht-numerischem Wert.

4. `io_input_eof_before_value.pl0`
- Erwartung: definierte Eingabediagnose bei EOF ohne verfügbaren Integer-Wert.

### 8.3 Testorakel und Artefakte
Für jeden `valid`-Fall werden drei Orakel gepflegt:
1. Parse-/Compile-Erfolg ohne Fehlerdiagnosen.
2. Erwartete Instruktionsliste als Golden-Master, z. B.:
- `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/expected/<testname>.pcode.txt`
3. Erwarteter Laufzeiteffekt:
- finaler Stack-Snapshot oder
- erwartete Store-Trace-Ausgabe (wenn Modus aktiv) oder
- erwartete `stdout`-Sequenz für `!`-Statements bei definierter `stdin`-Sequenz.

Für `invalid`-Fälle:
1. Mindestens eine erwartete Diagnose (Code + Position + Kernaussage).
2. Kein ungefangener Laufzeitabbruch im Compilerprozess.

Für `runtime/io-edge`-Fälle:
1. Definierte Laufzeitdiagnose oder definierter Fehlercode.
2. Keine unkontrollierte Exception bis in die CLI-Oberfläche.
3. Deterministisches Verhalten (gleiches Ergebnis bei gleichem Input).

### 8.4 Einbindung in die Testpipeline
- Bei jedem Build laufen mindestens:
1. Lexer-/Parser-Unit-Tests
2. Compile-Golden-Tests mit allen `valid`-Beispielen
3. Diagnose-Tests mit allen `invalid`-Beispielen
4. VM-End-to-End-Tests für ausgewählte Kernfälle (`while`, `procedure`, `odd`)
5. I/O-End-to-End-Tests für `?`/`!` mit deterministischen In-Memory-Streams
6. Dialekt-Tests (`classic` vs. `extended`) für Zulässigkeit von `?`/`!`
7. Limit-Tests (`levmax`, `cxmax`, `txmax`, `nmax`, `amax`)
8. Runtime-Fehlertests (Division durch 0, Input-Fehler, EOF)

- Optionaler Schalter für Golden-Update (nur manuell in Maintainer-Workflow), damit Soll-Änderungen kontrolliert nachvollzogen werden können.
- Coverage-Gate: jede Sprachregel aus Abschnitt 4.1.1 und jede VM-Regel aus Abschnitt 4.3 muss mindestens einem Pflichttest zugeordnet sein (Traceability-Matrix).

### 8.5 Abnahmekriterien
- Alle Muss-Features aus Abschnitt 4 sind implementiert.
- Alle Instruktionen aus Abschnitt 4.3 laufen gemäß Spezifikation.
- Fehlerhafte Programme liefern verständliche Diagnosen statt Absturz.
- Referenz-Beispielprogramme laufen erfolgreich.
- Alle Pflichtfälle aus Abschnitt 8.2 (`valid` und `invalid`) laufen im CI stabil durch.
- Alle Pflichtfallgruppen (`valid`, `invalid`, `compat`, `dialect`, `limits`, `runtime/io-edge`) laufen im CI stabil durch.

## 9. Risiken und Gegenmaßnahmen
- Risiko: Semantikabweichungen bei Level-/Frame-Handling.
  - Maßnahme: gezielte Tests für verschachtelte Prozeduren und `base(l)`.
- Risiko: Unterschiedliche Integer-/Divisionseigenschaften.
  - Maßnahme: explizite Tests für Division, Negation, Vergleichsoperatoren.
- Risiko: Zu enge Kopplung wie im Pascal-Monolith.
  - Maßnahme: klare Modulgrenzen und zustandsarme APIs.

## 10. Liefergegenstände
- C#/.NET-10-Quellcode (Compiler, VM, CLI)
- Testprojekt mit automatisierten Tests
- Dokumentation:
  - Sprachumfang/EBNF
  - VM-Befehlssatz
  - Build- und Nutzungsanleitung
  - Migrations-/Architekturhinweise

## 11. Priorisierte Backlog-Items (Startreihenfolge)
1. Projektskelett + Kern-Datentypen
2. Lexer mit Positionstracking
3. Parser (Expressions/Statements) + Fehlererholung
4. Symboltabelle mit Scope/Level
5. Codegenerator + Backpatching
6. VM-Interpreter vollständig
7. CLI + End-to-End-Tests
8. Dokumentation + Stabilisierung
