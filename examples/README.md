# Beispiele

Diese Sammlung enthaelt alle PL/0- und P-Code-Beispiele aus Handbuch und Anhang.

## Struktur

- `appendix/` Programme aus dem Anhang
- `handbook/pl0/` Beispiele aus dem PL0-Handbuch
- `handbook/pcode/` Beispiele aus dem P-Code-Handbuch
- `handbook/tutorial/` Beispiele aus dem P-Code-Tutorial
- `usage/` Beispiele aus der CLI-Dokumentation

## Ausfuehren

PL/0:

```bash
dotnet run --project src/Pl0.Cli -- run program.pl0
```

P-Code:

```bash
dotnet run --project src/Pl0.Cli -- run-pcode program.pcode.txt
```
