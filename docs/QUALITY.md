# Qualität und Testabdeckung

## Ziel
Abdeckung der Kernpfade aus Lexer, Parser/Codegenerator, VM und CLI.

## Testebenen
- Unit: Parser-/Lexer-/CLI-Optionen und VM-Operationen.
- Golden: Referenzvergleich von Token- und P-Code-Streams.
- End-to-End: `source -> pcode -> vm`.

## Kernpfad-Matrix
| Bereich | Tests |
|---|---|
| CLI-Switches und Subcommands | [`CliOptionsParserTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/CliOptionsParserTests.cs) |
| Lexer + Positionstracking | [`LexerTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/LexerTests.cs), [`LexerGoldenTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/LexerGoldenTests.cs) |
| Parser + Codegen + Dialektregeln | [`ParserGoldenTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/ParserGoldenTests.cs), [`ParserDiagnosticsTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/ParserDiagnosticsTests.cs) |
| VM-Laufzeit + Fehlerfälle | [`VirtualMachineTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/VirtualMachineTests.cs) |
| P-Code Datei-Roundtrip und E2E | [`PCodeSerializerTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/PCodeSerializerTests.cs) |
| Katalogpflichtfaelle 8.2 + Golden-Code | [`CatalogCasesTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/CatalogCasesTests.cs) |
| Traceability Coverage-Gate (4.1.1 + 4.3) | [`TraceabilityMatrixTests`](https://github.com/hindermath/TinyPl0/blob/main/tests/Pl0.Tests/TraceabilityMatrixTests.cs) |

## Traceability-Matrix
- Dokumentation:
  - [Traceability Matrix](TRACEABILITY_MATRIX.md)

## Lokale Qualitätskommandos
```bash
dotnet restore
dotnet build TinyPl0.sln --configuration Release
dotnet test TinyPl0.sln --configuration Release --no-build
dotnet test TinyPl0.sln --configuration Release --collect:"XPlat Code Coverage"
```

## CI
Der Workflow führt Build, Tests und Coverage-Collection aus.

## Manuelles Golden-Update
- Script für Maintainer-Workflow:
  - `../scripts/update-golden-code.sh`
