# Pl0.Cli API – Detaillierte Referenz

## Überblick

Das Modul **Pl0.Cli** stellt die Kommandozeilen-Schnittstelle (CLI) von TinyPl0 bereit. Es verbindet den Compiler (Pl0.Core) und die virtuelle Maschine (Pl0.Vm) und bietet ein benutzerfreundliches Command-Line-Interface.

Die CLI unterstützt mehrere Kommandos:
- `compile` – Kompiliert PL/0-Code zu P-Code
- `run` – Kompiliert und führt PL/0-Code direkt aus
- `run-pcode` – Führt vorkompilierten P-Code aus

---

## Hauptkomponenten

### CliCommand – Kommandodefinition

[`CliCommand`](xref:Pl0.Cli.Cli.CliCommand) definiert ein CLI-Kommando mit:
- **Name:** Der Kommando-Name (z.B. "compile")
- **Beschreibung:** Kurze Erklärung der Funktionalität
- **Optionen:** Kommando-spezifische Parameter
- **Handler:** Die Methode, die ausgeführt wird

---

### CliOptionsParser – Optionen-Analyse

[`CliOptionsParser`](xref:Pl0.Cli.Cli.CliOptionsParser) ist ein flexibler Parser für Kommandozeilen-Argumente. Er:
- Parst Pascal-kompatible Compiler-Switches (z.B. `/out`, `/list`)
- Unterstützt lange und kurze Optionen
- Generiert hilfreiche Fehlermeldungen bei ungültigen Optionen

**Beispiel:**
```csharp
// Parsing von Kommandozeilen-Argumenten
var parser = new CliOptionsParser();
var result = parser.Parse(args);

if (result.HasErrors)
{
    foreach (var error in result.Errors)
    {
        Console.WriteLine(error);
    }
}
```

---

### CompilerCliOptions – Optionen-Struktur

[`CompilerCliOptions`](xref:Pl0.Cli.Cli.CompilerCliOptions) speichert die geparsten Optionen für den `compile`-Kommando:

- **SourceFile:** Eingabe-Dateipath
- **OutputFile:** Ausgabe-P-Code-Datei (optional, mit `/out`)
- **ListingFile:** P-Code-Listing-Datei (optional, mit `/list`)
- **Dialect:** PL/0-Dialekt (Standard oder erweitert)
- **EmitMode:** Art der Ausgabe (Binary oder Text)

**Nutzung:**
```csharp
var options = new CompilerCliOptions
{
    SourceFile = "program.pl0",
    OutputFile = "program.pcode",
    ListingFile = "program.list"
};
```

---

### CliHelpPrinter – Hilfssystem

[`CliHelpPrinter`](xref:Pl0.Cli.Cli.CliHelpPrinter) generiert hilfreiche Ausgaben:
- Allgemeine Hilfe (alle verfügbaren Kommandos)
- Kommando-spezifische Hilfe (Optionen eines Kommandos)

**Beispiel:**
```csharp
var printer = new CliHelpPrinter();
printer.PrintGeneralHelp();
// oder
printer.PrintCommandHelp("compile");
```

---

### CliParseResult – Parsing-Ergebnis

[`CliParseResult`](xref:Pl0.Cli.Cli.CliParseResult) kapselt das Ergebnis des Kommandozeilen-Parsings:
- **Command:** Das erkannte Kommando
- **Options:** Die geparsten Optionen
- **HasErrors:** Fehlerflag
- **Errors:** Liste von Parsing-Fehlern

---

### CompilationDiagnostics – Fehlerbehandlung

[`CompilationDiagnostics`](xref:Pl0.Cli.Cli.CompilationDiagnostics) und [`CliDiagnostic`](xref:Pl0.Cli.Cli.CliDiagnostic) verwalten Fehler- und Warnmeldungen für die Benutzerausgabe.

---

## EmitMode – Ausgabe-Modi

[`EmitMode`](xref:Pl0.Cli.Cli.EmitMode) definiert, wie der P-Code ausgegeben wird:
- **Binary:** Binäres Format (kompakt, maschinenlesbar)
- **Text:** Textformat (menschenlesbar, mit Mnemonics)

---

## Workflow-Beispiele

### Beispiel 1: Kompilieren einer Datei

```csharp
// CLI empfängt: compile program.pl0 /out program.pcode /list program.list
var options = new CompilerCliOptions
{
    SourceFile = "program.pl0",
    OutputFile = "program.pcode",
    ListingFile = "program.list"
};

// Compiler nutzen
var compiler = new Pl0Compiler();
var result = compiler.Compile(File.ReadAllText(options.SourceFile));

// Ergebnis speichern
if (result.Success)
{
    // P-Code serialisieren und speichern...
}
```

### Beispiel 2: Direktes Ausführen von Code

```csharp
// CLI empfängt: run program.pl0
var code = File.ReadAllText("program.pl0");
var compiler = new Pl0Compiler();
var compilationResult = compiler.Compile(code);

if (compilationResult.Success)
{
    var vm = new VirtualMachine(compilationResult.Instructions);
    var vmResult = vm.Execute();
    // Ergebnis ausgeben...
}
```

---

## Verbindung zu anderen Modulen

- **← Pl0.Core:** Nutzt den `Pl0Compiler` zur Kompilierung
- **← Pl0.Vm:** Nutzt die `VirtualMachine` zur Ausführung von P-Code

---

## Weiterführende Ressourcen

- [Pl0.Cli.Cli Namespace API](xref:Pl0.Cli.Cli)
- [Architektur-Übersicht: CLI](../projects/pl0-cli.md)
- [Bedienung & Nutzung](../usage/index.md)

