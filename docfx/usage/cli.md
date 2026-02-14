# Nutzung der CLI

## Commands

- `compile`: PL/0 nach P-Code kompilieren.
- `run`: PL/0 direkt ausfuehren.
- `run-pcode`: P-Code-Datei ausfuehren.

## Typische Optionen

- `--out`: Zielpfad fuer P-Code.
- `--list-code`: P-Code-Liste ausgeben.
- `--emit`: Emissionsmodus fuer Diagnosen.

## Beispiele

```bash
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_io_q_bang_relops.pl0
```
