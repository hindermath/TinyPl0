# Compiler-Options als Parameter

## Beschreibung
In diesem Abschnitt werden die Compiler-Optionen beschrieben, die als weitere Parameter übergeben werden können. Diese Optionen können zur Steuerung des Compilierens und der Ausführung des Programms verwendet werden.


## Verfügbare neue Parameter/Switches
Für jeden Switch der Tabelle soll einen einzelnen Implementierungsabschnitt/-phase enthalten, in dem detailiert vorgeschlagen wird, wie dieser umgesetzt werden soll.

| Parameter/Switch | Standard | Maximal | Beschreibung                                                                                                                 |
|:-----------------|:--------:|:-------:|------------------------------------------------------------------------------------------------------------------------------|
| `--symtable`     |          |         | Gibt die Symboltabelle während des Compiliervorgangs aus.                                                                    |
| `--tracelog`     |          |         | Erzeugt ein Trace-Log der Tokenisierung (Scanner).                                                                           |
| `--ast`          |          |         | Zeigt den generierten Abstrakten Syntaxbaum (AST) an.                                                                        |
| `--dialect`      | Extended | Classic | Ausgewählter Sprachdialekt (Standard: Extended).                                                                             |
| `--maxlvl`       |    3     |    8    | Maximale Schachtelungstiefe von Blöcken (Standard: 3). (Maximal: 8)                                                          |
| `--maxadr`       |   2047   |  4096   | Maximale Adresse/Wert für Literale (Standard: 2047). (Maximal: 8192)                                                         |
| `--maxidentlen`  |    10    |   32    | Maximale Länge von Bezeichnern (Standard: 10). (Maximal: 32)                                                                 |
| `--maxnumdigits` |    14    |   20    | Maximale Anzahl an Ziffern in numerischen Literalen (Standard: 14). (Maximal: 20)                                            |
| `--maxsymcnt`    |   100    |   512   | Maximale Anzahl an Symbolen in der Tabelle (Standard: 100). (Maximal: 512)                                                   |
| `--maxcodelen`   |   200    |  8192   | Maximale Anzahl an generierten Befehlen (Standard: 200).                                                                     |

### Implementierungsphasen der Standard-Switches

#### Phase: Debugging & Visualisierung (`--symtable`, `--tracelog`, `--ast`)
* **`--symtable`**: Nach dem Parsing-Vorgang wird der Inhalt der internen Symboltabelle formatiert auf der Konsole ausgegeben (Name, Typ, Level, Adresse).
* **`--tracelog`**: Der Scanner wird so erweitert, dass jedes erkannte Token (Typ und Wert) sofort nach der Identifikation protokolliert wird.
* **`--ast`**: Implementierung einer Tree-Traversal Logik, die den erzeugten AST in einer hierarchischen Textform oder als JSON-Struktur darstellt.

#### Phase: Code-Generierung (`--clr`)
* Integration eines neuen Backends, das statt des PL/0-Zwischencodes direkt CIL (Common Intermediate Language) erzeugt.
* Nutzung von `System.Reflection.Emit` zur Erstellung der `.dll`.

#### Phase: Sprach-Konfiguration (`--dialect`)
* Steuerung der Schlüsselwort-Erkennung im Scanner. Im "Classic"-Modus werden Erweiterungen wie `WHILE` oder `IF-THEN-ELSE` auf den ursprünglichen Wirth-Standard eingeschränkt.

#### Phase: Kapazitäts-Limits (`--max...`)
* Die fest kodierten Konstanten im Compiler werden durch eine zentrale `Config`-Klasse ersetzt, die initial mit den Standardwerten geladen und durch die Kommandozeilenparameter überschrieben wird. Bei Überschreitung der Maximalwerte erfolgt ein Abbruch mit Fehlermeldung.

### Compiler-Switches Optimierung
Diese Switches und Ihre Ausgestaltung sind für die Optimierung des Compilers und die Steuerung der Optimierungsabläufe gedacht. Sie ermöglichen die Aktivierung oder Deaktivierung von verschiedenen Optimierungstechniken und stellen eine flexibele Möglichkeit zur Steuerung der Compiler-Optimierung bereit.
Diese werden in einem separaten Arbeitsschritt unabhängig von den in der oberen Tabelle beschriebenen Switches umgesetzt.

| Switch            | Bedeutung                       | Hinweis/Beispiel                                     | Verweis                                     |
|:------------------|:--------------------------------|:-----------------------------------------------------|:--------------------------------------------|
| `--opt=fold`      | Constant Folding                | z. B. konstante Ausdrücke zur Compile-Zeit auswerten | Details siehe Lastenheft_PL0_Optimierung.md |
| `--opt=dce`       | Dead Code Elimination           | nicht erreichbaren/wirkungslosen Code entfernen      | Details siehe Lastenheft_PL0_Optimierung.md |
| `--opt=alg`       | Algebraische Vereinfachungen    | z. B. `x + 0`, `x * 1` vereinfachen                  | Details siehe Lastenheft_PL0_Optimierung.md |
| `--opt=reg`       | Register-Optimierung            | effizientere Nutzung der (virtuellen) Register       | Details siehe Lastenheft_PL0_Optimierung.md |
| `--opt=all`       | alle Optimierungen aktivieren   | Sammelschalter                                       | Details siehe Lastenheft_PL0_Optimierung.md |
| `--no-opt`        | alle Optimierungen deaktivieren | überschreibt ggf. gesetzte `--opt=...`               | Details siehe Lastenheft_PL0_Optimierung.md |
| `--no-opt=<name>` | eine Optimierung deaktivieren   | z. B. `--no-opt=fold`                                | Details siehe Lastenheft_PL0_Optimierung.md |
