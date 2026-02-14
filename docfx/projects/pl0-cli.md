# Pl0.CLI

## Zweck

Die CLI stellt die Befehle `compile`, `run` und `run-pcode` bereit und verbindet Compiler und VM.

## Wichtige Bestandteile

- Optionen-Parser fuer Pascal-kompatible Compiler-Switches.
- Ausgabe von P-Code-Listen fuer didaktische Zwecke.
- Diagnoseausgaben fuer Fehler und Warnungen.

## Einstieg

```bash
dotnet run --project src/Pl0.Cli -- compile <datei.pl0> --out /tmp/example.pcode
```
