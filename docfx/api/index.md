# API-Referenz

Willkommen zur API-Referenz von **TinyPl0**. Die API-Referenz wird aus XML-Kommentaren generiert. Für Schulungszwecke sind auch interne und private Member enthalten, um tiefe Einblicke in die Implementierung zu ermöglichen. Die Zugriffsebene wird in der DocFX-Ansicht entsprechend gekennzeichnet.

## Überblick der Module

TinyPl0 ist in drei Module unterteilt, die zusammen einen vollständigen Compiler und eine virtuelle Maschine bilden:

---

### 🔧 Pl0.Core – Compiler & Codegenerierung

Das Herzstück von TinyPl0. Dieses Modul enthält:
- **Lexer:** Tokenisierung des Quellcodes mit Positionsinformationen
- **Parser:** Syntaxanalyse mit Symbol- und Scoperesolution
- **Compiler:** P-Code-Generierung
- **Symboltabelle:** Verwaltung von Variablen, Konstanten und Prozeduren

**Wichtigste Klassen:**
- [Pl0Compiler](xref:Pl0.Core.Pl0Compiler) – Haupteinstiegspunkt für die Kompilierung
- [Pl0Lexer](xref:Pl0.Core.Pl0Lexer) – Lexikalische Analyse
- [Pl0Parser](xref:Pl0.Core.Pl0Parser) – Syntaxanalyse
- [SymbolTable](xref:Pl0.Core.SymbolTable) – Symbol- und Scope-Verwaltung

[📖 Detaillierte Core-API-Referenz](core-reference.md)

---

### 💻 Pl0.Cli – Kommandozeilen-Interface

Das Eingabe-Frontende. Dieses Modul stellt bereit:
- Kommandozeilen-Parser für Pascal-kompatible Compiler-Switches
- Befehlsdefinitionen (`compile`, `run`, `run-pcode`)
- P-Code-Listing-Ausgabe für didaktische Zwecke
- Fehler- und Warnungsausgabe

**Wichtigste Klassen:**
- [CliCommand](xref:Pl0.Cli.Cli.CliCommand) – Kommandodefinition
- [CliOptionsParser](xref:Pl0.Cli.Cli.CliOptionsParser) – Options-Parser
- [CompilerCliOptions](xref:Pl0.Cli.Cli.CompilerCliOptions) – Compiler-Optionen-Struktur
- [CliHelpPrinter](xref:Pl0.Cli.Cli.CliHelpPrinter) – Hilfeausgabe

[📖 Detaillierte CLI-API-Referenz](cli-reference.md)

---

### 🏃 Pl0.Vm – Virtuelle Maschine

Der Laufzeit-Engine. Dieses Modul implementiert:
- Stack-basierte virtuelle Maschine für P-Code-Instruktionen
- I/O-Abstraktionen (Console, gepuffert)
- Laufzeit-Fehlerbehandlung und Diagnosen
- Ergebnisobjekte mit Ausführungsstatistiken

**Wichtigste Klassen:**
- [VirtualMachine](xref:Pl0.Vm.VirtualMachine) – Hauptinterpreter
- [VmExecutionResult](xref:Pl0.Vm.VmExecutionResult) – Ausführungsergebnis
- [IPl0Io](xref:Pl0.Vm.IPl0Io) – I/O-Abstraktion
- [ConsolePl0Io](xref:Pl0.Vm.ConsolePl0Io) – Konsolenimplementierung

[📖 Detaillierte VM-API-Referenz](vm-reference.md)

---

## Namespaces

Vollständige Namespace-Übersicht:

- [Namespace Pl0.Core](xref:Pl0.Core)
- [Namespace Pl0.Cli.Cli](xref:Pl0.Cli.Cli)
- [Namespace Pl0.Vm](xref:Pl0.Vm)

---

## Tipps für die Nutzung

> [!TIP]
> Beginnen Sie mit [Pl0Compiler](xref:Pl0.Core.Pl0Compiler) in der Core-API, um zu verstehen, wie der Compiler aufgebaut ist. Die Methode `Compile()` zeigt den Ablauf vom Quellcode zum P-Code.

> [!NOTE]
> Der Quellcode ist auf GitHub verfügbar. Nutzen Sie die API-Referenz zusammen mit dem Quellcode, um die Implementierung zu durchschauen.

---

### Direkt zur vollständigen API-Referenz

Wenn Sie ohne Umwege in die generierte API-Navigation wechseln möchten, nutzen Sie den folgenden Link:

- [Zur vollständigen API-Referenz](xref:Pl0.Core)

