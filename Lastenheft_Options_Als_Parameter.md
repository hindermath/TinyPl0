# Compiler-Options als Parameter

## Beschreibung
In diesem Abschnitt werden die Compiler-Optionen beschrieben, die als weitere Parameter übergeben werden können. Diese Optionen können zur Steuerung des Compilierens und der Ausführung des Programms verwendet werden.


## Verfügbare neue Parameter

| Parameter        | Standard | Maximal | Beschreibung                                                                                                                 |
|------------------|----------|---------|------------------------------------------------------------------------------------------------------------------------------|
| `--symtable`     |          |         | Gibt die Symboltabelle während des Compiliervorgangs aus.                                                                    |
| `--tracelog`     |          |         | Erzeugt ein Trace-Log der Tokenisierung (Scanner).                                                                           |
| `--ast`          |          |         | Zeigt den generierten Abstrakten Syntaxbaum (AST) an.                                                                        |
| `--clr`          |          |         | Erzeugt eine ausführbare .Net Assembly-Datei mit der Datei-Endung '.dll', die mit dem Befehl 'dotnet' ausgeführt werden kann |
| `--dialect`      | Extended | Classic | Ausgewählter Sprachdialekt (Standard: Extended).                                                                             |
| `--maxlvl`       | 3        | 8       | Maximale Schachtelungstiefe von Blöcken (Standard: 3). (Maximal: 8)                                                          |
| `--maxadr`       | 2047     | 4096    | Maximale Adresse/Wert für Literale (Standard: 2047). (Maximal: 8192)                                                         |
| `--maxidentlen`  | 10       | 32      | Maximale Länge von Bezeichnern (Standard: 10). (Maximal: 32)                                                                 |
| `--maxnumdigits` | 14       | 20      | Maximale Anzahl an Ziffern in numerischen Literalen (Standard: 14). (Maximal: 20)                                            |
| `--maxsymcnt`    | 100      | 512     | Maximale Anzahl an Symbolen in der Tabelle (Standard: 100). (Maximal: 512)                                                   |
| `--maxcodelen`   | 200      | 8192    | Maximale Anzahl an generierten Befehlen (Standard: 200).                                                                     |
