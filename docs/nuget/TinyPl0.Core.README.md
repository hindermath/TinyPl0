# TinyPl0.Core

## Deutsch

`TinyPl0.Core` ist der didaktische Compilerkern von TinyPl0 für .NET 10. Das
Paket überträgt die Ideen des historischen PL/0-Compilers in eine klar
gegliederte C#-API. Es liest PL/0-Quelltext, prüft ihn und erzeugt typisierten
P-Code für die virtuelle Maschine von TinyPl0.

Das Paket hat keine Produkt-Laufzeitabhängigkeit. Für die Ausführung des
erzeugten P-Codes installierst du zusätzlich `TinyPl0.Vm` in derselben stabilen
Version.

### Für wen ist das Paket gedacht?

- Lernende können die Schritte eines kleinen Compilers nachvollziehen.
- Lehrende können Lexer, Parser, Symboltabelle und Codeerzeugung erklären.
- Host-Entwickler können PL/0-Quelltext in eigene .NET-Anwendungen einbetten.
- Werkzeugentwickler können Diagnosen und P-Code strukturiert weiterverarbeiten.

### Was ist enthalten?

- `Pl0Compiler` als einfacher Einstieg für die vollständige Übersetzung
- `Pl0Lexer` und `Pl0Parser` für einzelne Lern- und Werkzeugschritte
- `CompilerOptions` für Dialekt, Diagnosesprache und historische Grenzen
- `CompilationResult` mit `Instructions`, `Diagnostics` und `Success`
- typisierte `Instruction`- und `Opcode`-Werte für den erzeugten P-Code
- Quellpositionen und stabile Diagnosecodes für textuelle Fehlermeldungen

### Was ist bewusst nicht enthalten?

- keine virtuelle Maschine; sie befindet sich im Paket `TinyPl0.Vm`
- keine Kommandozeile und keine Benutzeroberfläche
- kein JIT- oder .NET-IL-Backend
- keine Optimierungen wie SSA- oder Peephole-Optimierung
- keine Spracherweiterungen außerhalb der dokumentierten PL/0-Dialekte

Der enge Umfang hält den historischen Compileraufbau sichtbar und eignet sich
für Unterricht, Experimente und kleine eingebettete Werkzeuge.

### Installation

Installiere das Paket mit der .NET-CLI:

```console
dotnet add package TinyPl0.Core
```

### Schnellstart: PL/0 kompilieren

Das folgende vollständige Beispiel kompiliert ein kleines Programm im
erweiterten Dialekt. Es prüft zuerst das Ergebnis und verarbeitet erst danach
den P-Code.

```csharp
using Pl0.Core;

string source = """
    const answer = 42;
    begin
      ! answer
    end.
    """;

CompilerOptions options = new(Pl0Dialect.Extended, Language: "de");
CompilationResult result = new Pl0Compiler().Compile(source, options);

if (!result.Success)
{
    foreach (CompilerDiagnostic diagnostic in result.Diagnostics)
    {
        Console.Error.WriteLine(
            $"{diagnostic.Code} bei {diagnostic.Position}: {diagnostic.Message}");
    }

    return;
}

foreach (Instruction instruction in result.Instructions)
{
    Console.WriteLine(instruction);
}
```

Der typische Datenfluss ist:

```text
PL/0-Quelltext
  -> Pl0Compiler
  -> CompilationResult
     -> Instructions: typisierter P-Code
     -> Diagnostics: Fehler mit Code, Text und Quellposition
```

### Dialekte

- `Pl0Dialect.Classic` bildet den historischen Dialekt ohne Ein- und Ausgabeanweisungen ab.
- `Pl0Dialect.Extended` ergänzt `? ident` für Eingaben und `! expr` für Ausgaben.
- `CompilerOptions.Default` verwendet den erweiterten Dialekt.

Wähle den Dialekt ausdrücklich, wenn dein Host ein festes Sprachprofil
anbietet. So bleibt das Verhalten auch bei späteren Änderungen gut erkennbar.

### Diagnosen und sichere Weiterverarbeitung

PL/0-Quellfehler werden in `CompilationResult.Diagnostics` gesammelt. Zeige
mindestens Diagnosecode und Meldung als Text an. Eine Quellposition hilft
zusätzlich beim Auffinden des Fehlers.

Führe `Instructions` nur aus, wenn `CompilationResult.Success` den Wert `true`
hat. Ein Ergebnis mit Diagnosen ist kein ausführbares Programm. Für einen
englischen Diagnosetext setzt du `CompilerOptions.Language` auf `"en"`.

Wenn Eingaben nicht vertrauenswürdig sind, begrenze zusätzlich die Werte in
`CompilerOptions`, zum Beispiel die maximale Codelänge und die Größe der
Symboltabelle. Diese Compilergrenzen ersetzen nicht die Laufzeitgrenzen von
`TinyPl0.Vm`.

### Weiterführende Dokumentation

- [TinyPl0-Architektur und Zuordnung zum Pascal-Original](https://github.com/hindermath/TinyPl0/blob/main/docs/ARCHITECTURE.md)
- [Formale Grammatik der unterstützten PL/0-Dialekte](https://github.com/hindermath/TinyPl0/blob/main/docs/LANGUAGE_EBNF.md)
- [TinyPl0.Core API-Referenz](https://hindermath.github.io/TinyPl0/api/Pl0.Core.html)
- [TinyPl0-Dokumentationsportal](https://hindermath.github.io/TinyPl0/)
- [Quellcode und Issue-Tracker](https://github.com/hindermath/TinyPl0)

## English

`TinyPl0.Core` is the educational compiler core of TinyPl0 for .NET 10. The
package transfers the ideas of the historical PL/0 compiler into a clearly
structured C# API. It reads and validates PL/0 source text and generates typed
P-Code for the TinyPl0 virtual machine.

The package has no product runtime dependency. To execute the generated
P-Code, also install `TinyPl0.Vm` at the same stable version.

### Who is this package for?

- Learners can follow the stages of a small compiler.
- Teachers can explain the lexer, parser, symbol table, and code generation.
- Host developers can embed PL/0 compilation in their own .NET applications.
- Tool developers can process diagnostics and P-Code as structured data.

### What is included?

- `Pl0Compiler` as a simple entry point for complete compilation
- `Pl0Lexer` and `Pl0Parser` for individual learning and tooling stages
- `CompilerOptions` for the dialect, diagnostic language, and historical limits
- `CompilationResult` with `Instructions`, `Diagnostics`, and `Success`
- typed `Instruction` and `Opcode` values for the generated P-Code
- source positions and stable diagnostic codes for text error messages

### What is intentionally not included?

- no virtual machine; it is provided by the `TinyPl0.Vm` package
- no command-line tool and no user interface
- no JIT or .NET IL backend
- no optimizations such as SSA or peephole optimization
- no language extensions outside the documented PL/0 dialects

This narrow scope keeps the historical compiler structure visible. It is
suitable for teaching, experiments, and small embedded tools.

### Installation

Install the package with the .NET CLI:

```console
dotnet add package TinyPl0.Core
```

### Quick start: compile PL/0

The following complete example compiles a small program in the extended
dialect. It checks the result first and processes the P-Code only after a
successful compilation.

```csharp
using Pl0.Core;

string source = """
    const answer = 42;
    begin
      ! answer
    end.
    """;

CompilerOptions options = new(Pl0Dialect.Extended, Language: "en");
CompilationResult result = new Pl0Compiler().Compile(source, options);

if (!result.Success)
{
    foreach (CompilerDiagnostic diagnostic in result.Diagnostics)
    {
        Console.Error.WriteLine(
            $"{diagnostic.Code} at {diagnostic.Position}: {diagnostic.Message}");
    }

    return;
}

foreach (Instruction instruction in result.Instructions)
{
    Console.WriteLine(instruction);
}
```

The typical data flow is:

```text
PL/0 source text
  -> Pl0Compiler
  -> CompilationResult
     -> Instructions: typed P-Code
     -> Diagnostics: errors with a code, message, and source position
```

### Dialects

- `Pl0Dialect.Classic` represents the historical dialect without input and output statements.
- `Pl0Dialect.Extended` adds `? ident` for input and `! expr` for output.
- `CompilerOptions.Default` uses the extended dialect.

Select the dialect explicitly when your host provides a fixed language
profile. This keeps the behavior easy to identify when the application is
maintained later.

### Diagnostics and safe processing

PL/0 source errors are collected in `CompilationResult.Diagnostics`. Expose at
least the diagnostic code and message as text. A source position also helps
users locate the error.

Execute `Instructions` only when `CompilationResult.Success` is `true`. A
result with diagnostics is not an executable program. Set
`CompilerOptions.Language` to `"de"` for German diagnostic text.

For untrusted input, also restrict the values in `CompilerOptions`, such as the
maximum code length and symbol table size. These compiler limits do not replace
the runtime limits provided by `TinyPl0.Vm`.

### Further documentation

- [TinyPl0 architecture and mapping to the Pascal original](https://github.com/hindermath/TinyPl0/blob/main/docs/ARCHITECTURE.md)
- [Formal grammar of the supported PL/0 dialects](https://github.com/hindermath/TinyPl0/blob/main/docs/LANGUAGE_EBNF.md)
- [TinyPl0.Core API reference](https://hindermath.github.io/TinyPl0/api/Pl0.Core.html)
- [TinyPl0 documentation portal](https://hindermath.github.io/TinyPl0/)
- [Source code and issue tracker](https://github.com/hindermath/TinyPl0)
