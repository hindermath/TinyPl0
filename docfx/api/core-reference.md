# Pl0.Core API – Detaillierte Referenz

## Überblick

Das Modul **Pl0.Core** bildet das Herzstück des TinyPl0-Compilers. Es implementiert die klassischen Compiler-Phasen:

1. **Lexikalische Analyse (Lexer):** Umsetzung von Quelltext in Token
2. **Syntaxanalyse (Parser):** Strukturanalyse und Symboltabellen-Verwaltung
3. **Codegenerierung:** Erzeugung von P-Code-Instruktionen

---

## Hauptkomponenten

### Pl0Compiler – Der zentrale Einstiegspunkt

[`Pl0Compiler`](xref:Pl0.Core.Pl0Compiler) ist die Haupt-API für Benutzer des Core-Moduls. Die Klasse orchestriert den gesamten Kompilierungsprozess:

- **Methode `Compile()`:** Übernimmt den Quellcode und gibt ein [`CompilationResult`](xref:Pl0.Core.CompilationResult) zurück.
- **Eigenschaften:** Ermöglichen die Anpassung des Compilers über [`CompilerOptions`](xref:Pl0.Core.CompilerOptions).

**Beispiel-Nutzung:**
```csharp
var compiler = new Pl0Compiler();
var result = compiler.Compile(sourceCode);
if (result.Success)
{
    var instructions = result.Instructions;
    // Instruktionen verwenden
}
```

---

### Pl0Lexer – Lexikalische Analyse

[`Pl0Lexer`](xref:Pl0.Core.Pl0Lexer) teilt den Quelltext in Token auf. Jedes Token wird mit:
- **TokenKind:** Die Art des Tokens (Keyword, Identifier, Number, etc.)
- **Textposition:** Zeile und Spalte für Fehlermeldungen
- **Textwert:** Der tatsächliche Token-Text

Relevante Typen:
- [`Pl0Token`](xref:Pl0.Core.Pl0Token) – Repräsentation eines einzelnen Tokens
- [`TokenKind`](xref:Pl0.Core.TokenKind) – Enumeration der Token-Arten
- [`LexerResult`](xref:Pl0.Core.LexerResult) – Ergebnis der lexikalischen Analyse
- [`LexerDiagnostic`](xref:Pl0.Core.LexerDiagnostic) – Fehlerdiagnosen

---

### Pl0Parser – Syntaxanalyse

[`Pl0Parser`](xref:Pl0.Core.Pl0Parser) implementiert die PL/0-Grammatik und erzeugt ein abstraktes Syntaxbaum-ähnliche Struktur. Der Parser arbeitet eng mit der Symboltabelle zusammen.

Relevante Typen:
- [`SymbolTable`](xref:Pl0.Core.SymbolTable) – Speichert Symbole und deren Scope
- [`SymbolEntry`](xref:Pl0.Core.SymbolEntry) – Ein einzelnes Symbol (Variable, Konstante, Prozedur)
- [`SymbolKind`](xref:Pl0.Core.SymbolKind) – Art des Symbols

---

### Codegenerierung & Instruktionen

Die Codegenerierung erzeugt P-Code-Instruktionen, die von der virtuellen Maschine ausgeführt werden.

Relevante Typen:
- [`Instruction`](xref:Pl0.Core.Instruction) – Eine einzelne P-Code-Instruktion
- [`Opcode`](xref:Pl0.Core.Opcode) – Die Operation (LIT, LOD, STO, etc.)
- [`PCodeSerializer`](xref:Pl0.Core.PCodeSerializer) – Serialisierung von Instruktionen für Speicherung/Transport

**P-Code Instruktionen:**
```csharp
// Beispiele von Opcodes
Opcode.LIT  // Lade Konstante auf Stack
Opcode.LOD  // Lade Variable auf Stack
Opcode.STO  // Speichere Stack-Top in Variable
Opcode.ADD  // Addition
Opcode.JMP  // Unbedingter Sprung
Opcode.JPC  // Bedingter Sprung
```

---

## Fehlerbehandlung

Kompilierungsfehler werden in [`CompilerDiagnostic`](xref:Pl0.Core.CompilerDiagnostic) erfasst und über [`CompilationResult`](xref:Pl0.Core.CompilationResult) zurückgegeben.

**Nutzung:**
```csharp
var result = compiler.Compile(sourceCode);
if (!result.Success)
{
    foreach (var diagnostic in result.Diagnostics)
    {
        Console.WriteLine($"{diagnostic.Message} at {diagnostic.Position}");
    }
}
```

---

## Verbindung zu anderen Modulen

- **→ Pl0.Cli:** Der CLI-Parser nutzt `Pl0Compiler` für die `compile`-Kommando.
- **→ Pl0.Vm:** Die erzeugten P-Code-Instruktionen werden von `VirtualMachine` ausgeführt.

---

## Weiterführende Ressourcen

- [Pl0.Core Namespace API](xref:Pl0.Core)
- [Architektur-Übersicht](../projects/pl0-core.md)
- [P-Code & VM Handbuch](../handbook/pcode/index.md)

