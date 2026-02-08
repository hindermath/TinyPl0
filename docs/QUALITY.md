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
| CLI-Switches und Subcommands | `CliOptionsParserTests` |
| Lexer + Positionstracking | `LexerTests`, `LexerGoldenTests` |
| Parser + Codegen + Dialektregeln | `ParserGoldenTests`, `ParserDiagnosticsTests` |
| VM-Laufzeit + Fehlerfälle | `VirtualMachineTests` |
| P-Code Datei-Roundtrip und E2E | `PCodeSerializerTests` |
| Katalogpflichtfaelle 8.2 + Golden-Code | `CatalogCasesTests` |
| Traceability Coverage-Gate (4.1.1 + 4.3) | `TraceabilityMatrixTests` |

## Traceability-Matrix
- Dokumentation:
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/TRACEABILITY_MATRIX.md`
- Datenbasis:
  - `/Users/thorstenhindermann/Codex/TinyPl0/tests/data/expected/traceability/matrix.json`

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
  - `/Users/thorstenhindermann/Codex/TinyPl0/scripts/update-golden-code.sh`
