# Installation und Nutzung

## Installation

1. .NET SDK 10 installieren.
2. Repository klonen.
3. `dotnet build` ausfuehren.

## Nutzung

- CLI: `dotnet run --project src/Pl0.Cli -- <command> <datei.pl0>`
- Unterstuetzte Commands: `compile`, `run`, `run-pcode`

## Beispiel

```bash
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_const_var_assignment.pl0
```
