# TinyPl0

## Zielsetzung
Dieses Repository dient der Portierung des historischen PL/0-Beispielcompilers (Pascal) nach C# auf .NET 10.

Die fachliche und technische Zieldefinition steht im Pflichtenheft:
- `Pflichtenheft_PL0_CSharp_DotNet10.md`

## Lizenz
Lizenz: MIT - siehe LICENSE.

## Referenzverhalten
Die Portierung orientiert sich an zwei klaren Dialekten:

1. `classic`
- Kompatibel zum vorliegenden Pascal-Referenzcode.
- Fokus auf historische Semantik und VM-Verhalten.

2. `extended`
- Konsolidierte EBNF inkl. `? ident` (Input) und `! expression` (Output) auf Basis der ANTLR-PL/0-Grammatik.

Referenzquellen im Repository:
- `PL0.md`
- `pl0c.pas`

## Sprachumfang und Einschränkungen
- Datentyp: nur `integer`.
- Prozeduren sind unterstützt, Parameter/Funktionsrückgaben nicht.
- Dialekte:
1. `classic`: ohne `?`/`!`.
2. `extended`: mit Eingabe `? ident` und Ausgabe `! expression`.

## Architektur und Qualität
- Architekturdiagramm und Pascal->C# Mapping:
  - `docs/ARCHITECTURE.md`
- Sprachumfang/EBNF:
  - `docs/LANGUAGE_EBNF.md`
- VM-Befehlssatz:
  - `docs/VM_INSTRUCTION_SET.md`
- Traceability-Matrix (Regel -> Pflichttests):
  - `docs/TRACEABILITY_MATRIX.md`
- Qualitäts- und Coverage-Übersicht:
  - `docs/QUALITY.md`

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
  - `tests/data/expected/catalog/cases.json`

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

## Entwicklung mit Agentic-AI

Dieses Projekt entstand mit starker Unterstützung durch **Agentic-AI-Technologien**. Die Verwendung von KI-Agenten spielte eine zentrale Rolle bei:

- **Architektur & Design:** Automatisierte Analyse von Anforderungen und Ableitung der Systemarchitektur.
- **Code-Generierung:** Unterstützung bei der Implementierung von Lexer, Parser, Compiler und VM.
- **Testing & Validierung:** Systematische Generierung von Testfällen und Überprüfung der Compliance.
- **Dokumentation:** Automatisierte Erstellung von API-Dokumentation, Handbüchern und Architekturdokumentation.
- **Qualitätssicherung:** Statische Codeanalyse, Fehlerdetection und Optimierungsvorschläge.

### Ziel: Agentic-AI in der Ausbildung

Neben der praktischen Implementierung eines Compilers dient dieses Projekt auch als **Fallstudie** zur Untersuchung, wie Agentic-AI sinnvoll in der **Fachinformatiker-Ausbildung** eingesetzt werden kann:

- **Pädagogischer Mehrwert:** Wie können AI-Agenten Auszubildende beim Erlernen komplexer Compilerbau-Konzepte unterstützen?
- **Produktivität:** Wie beschleunigt die Zusammenarbeit mit AI-Agenten die Entwicklung, ohne die Lerneffekte zu schmälern?
- **Qualität:** Wie trägt Agentic-AI zu besserer Code-Qualität, Dokumentation und Testabdeckung bei?
- **Transparenz:** Wie bleibt die Nachvollziehbarkeit und Validierbarkeit der generierten Artefakte gewährleistet?

Dieses Projekt zeigt, dass Agentic-AI nicht als Ersatz für manuelles Lernen gedacht ist, sondern als **produktive Werkzeug und Lernpartner** für professionelle Softwareentwicklung in der Ausbildung.


