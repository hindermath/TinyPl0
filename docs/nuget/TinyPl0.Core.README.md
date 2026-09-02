# TinyPl0.Core

## Deutsch

`TinyPl0.Core` übersetzt PL/0-Quelltext in typisierten P-Code. Das Paket zielt
auf .NET 10 und besitzt keine Produkt-Laufzeitabhängigkeit. Prüfe vor der
Ausführung immer `CompilationResult.Success` und zeige Diagnosen textuell an.

```csharp
using Pl0.Core;

CompilationResult result = new Pl0Compiler().Compile("begin end.");
if (!result.Success)
    foreach (CompilerDiagnostic diagnostic in result.Diagnostics)
        Console.Error.WriteLine($"{diagnostic.Code}: {diagnostic.Message}");
```

## English

`TinyPl0.Core` compiles PL/0 source into typed P-Code. The package targets
.NET 10 and has no product runtime dependency. Always check
`CompilationResult.Success` before execution and expose diagnostics as text.
