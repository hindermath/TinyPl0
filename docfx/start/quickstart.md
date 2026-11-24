# Schnellstart

## Voraussetzungen

- .NET 11 SDK gemäß `global.json` / .NET 11 SDK as specified in `global.json`

## Build und Tests

```bash
dotnet build
```

```bash
dotnet test
```

## CLI-Beispiele

```bash
# PL/0 Quelltext ausfuehren
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_io_q_bang_relops.pl0
```

```bash
# Kompilieren nach .pcode
dotnet run --project src/Pl0.Cli -- compile tests/data/pl0/valid/feature_const_var_assignment.pl0 --out /tmp/example.pcode
```
