# TinyPl0

## Zielsetzung
Dieses Repository dient der Portierung des historischen PL/0-Beispielcompilers (Pascal) nach C# auf .NET 10.

Die fachliche und technische Zieldefinition steht im Pflichtenheft:
- `/Users/thorstenhindermann/Codex/TinyPl0/Pflichtenheft_PL0_CSharp_DotNet10.md`

## Referenzverhalten
Die Portierung orientiert sich an zwei klaren Dialekten:

1. `classic`
- Kompatibel zum vorliegenden Pascal-Referenzcode.
- Fokus auf historische Semantik und VM-Verhalten.

2. `extended`
- Konsolidierte EBNF inkl. `? ident` (Input) und `! expression` (Output) auf Basis der ANTLR-PL/0-Grammatik.

Referenzquellen im Repository:
- `/Users/thorstenhindermann/Codex/TinyPl0/PL0.md`
- `/Users/thorstenhindermann/Codex/TinyPl0/pl0c.pas`

## Sprachumfang und Einschränkungen
- Datentyp: nur `integer`.
- Prozeduren sind unterstützt, Parameter/Funktionsrückgaben nicht.
- Dialekte:
1. `classic`: ohne `?`/`!`.
2. `extended`: mit Eingabe `? ident` und Ausgabe `! expression`.

## Architektur und Qualität
- Architekturdiagramm und Pascal->C# Mapping:
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/ARCHITECTURE.md`
- Sprachumfang/EBNF:
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/LANGUAGE_EBNF.md`
- VM-Befehlssatz:
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/VM_INSTRUCTION_SET.md`
- Traceability-Matrix (Regel -> Pflichttests):
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/TRACEABILITY_MATRIX.md`
- Qualitäts- und Coverage-Übersicht:
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/QUALITY.md`

## Build und Ausführung

### Aktueller Stand (Phase 5)
Die .NET-10-Solution ist angelegt:
- `TinyPl0.sln`
- `src/Pl0.Core`
- `src/Pl0.Vm`
- `src/Pl0.Cli`
- `tests/Pl0.Tests`

Zusätzlich sind umgesetzt:
- `CliOptionsParser` für die Pascal-kompatiblen Compiler-Switches.
- Ein erster Lexer (`Pl0Lexer`) mit Zeilen-/Spalten-Tracking.
- Parser + Symboltabelle + Codegenerator (`Pl0Parser`/`Pl0Compiler`).
- VM/Interpreter (`VirtualMachine`) inklusive Stack-Maschine und I/O-Adapter (`ConsolePl0Io`, `BufferedPl0Io`).
- CLI-Subcommands: `compile`, `run`, `run-pcode` (inkl. `--out`, `--list-code`, `--emit`).
- P-Code Serialisierung/Deserialisierung (`PCodeSerializer`) für Datei-Workflow.
- End-to-End-Tests für `source -> pcode -> vm` in `tests`.
- Vollständiger 8.2-Testdatenkatalog (41 Pflicht-`.pl0`-Fälle) inkl. erwarteter Artefakte:
  - `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/expected/catalog/cases.json`

### Pascal-Referenz (optional)
Voraussetzung: FreePascal (`fpc`) oder Delphi-kompatibler Compiler.

Beispiel:
```bash
fpc pl0c.pas
./pl0c
```

### .NET-10-Befehle
```bash
dotnet restore
dotnet build
dotnet test
```

### CLI-Beispiele
```bash
# Kompilieren nach .pcode
dotnet run --project src/Pl0.Cli -- compile tests/data/pl0/valid/feature_const_var_assignment.pl0 --out /tmp/example.pcode
```
```bash
# PL/0 Quelltext direkt ausführen
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_io_q_bang_relops.pl0
```
```bash
# Vorhandene .pcode Datei ausführen
dotnet run --project src/Pl0.Cli -- run-pcode tests/data/expected/code/feature_io_q_bang_relops.pcode.txt
```
```bash
# Code-Liste ausgeben (didaktisch)
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_const_var_assignment.pl0 --list-code --wopcod
```

## Repository-Status
- Git-Repository ist initialisiert.
- `.gitignore` für .NET/C#/Visual Studio/JetBrains ist vorhanden.
- Phase 0 bis Phase 5 (CLI + End-to-End) sind gemäß Pflichtenheft umgesetzt.
- Phase 6 (Qualität + Dokumentation) ist gestartet und enthält Architektur-/Qualitätsdoku sowie erweiterte Kernpfadtests.
