# Anhang: Versandkostenstaffel

Einfache Staffelung nach Bestellwert.

## Programm

```pl0
var wert, kosten;
begin
  ? wert;
  kosten := 0;
  if wert < 50 then
    kosten := 5;
  if wert >= 50 then
    kosten := 0;
  ! kosten
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Zwei Stufen: <50 kostet 5, sonst 0.
- Beispiel fuer einfache Regeln.
## Beispiel

Eingabe:

```
40
```

Ausgabe:

```
5
```
## Testfaelle

- 50 -> 0
- 49 -> 5

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/versandkosten/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






