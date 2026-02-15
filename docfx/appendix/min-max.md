# Anhang: Minimum und Maximum

Bestimmt Minimum und Maximum aus N Zahlen.

## Programm

```pl0
var n, i, x, min, max;
begin
  ? n;
  ? x;
  min := x;
  max := x;
  i := 1;
  while i < n do
  begin
    ? x;
    if x < min then
      min := x;
    if x > max then
      max := x;
    i := i + 1
  end;
  ! min;
  ! max
end.
```

[📥 Programm herunterladen](../../examples/appendix/min-max/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Erstes Element initialisiert min/max.
- Danach Vergleich in der Schleife.
## Beispiel

Eingabe:

```
5 3 9 2 8 6
```

Ausgabe:

```
2 9
```
## Testfaelle

- 3 5 5 5 -> 5 5
- 4 -1 2 0 9 -> -1 9

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/min-max/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






