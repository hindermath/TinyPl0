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

## Build und Ausführung

### Aktueller Stand (Phase 0)
Aktuell sind Spezifikation, Pflichtenheft und Pascal-Referenzcode vorhanden. Die .NET-10-Solution wird in Phase 1 angelegt.

### Pascal-Referenz (optional)
Voraussetzung: FreePascal (`fpc`) oder Delphi-kompatibler Compiler.

Beispiel:
```bash
fpc pl0c.pas
./pl0c
```

### Geplante .NET-10-Befehle (ab Phase 1)
```bash
dotnet restore
dotnet build
dotnet test
```

## Repository-Status
- Git-Repository ist initialisiert.
- `.gitignore` für .NET/C#/Visual Studio/JetBrains ist vorhanden.
- Phase 0 gemäß Pflichtenheft ist damit umgesetzt.
