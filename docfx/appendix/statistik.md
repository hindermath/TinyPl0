# Anhang: Einfache Statistik

Summe, Mittel, Min, Max.

## Programm

```pl0
var n, i, x, sum, min, max;
begin
  ? n;
  ? x;
  sum := x;
  min := x;
  max := x;
  i := 1;
  while i < n do
  begin
    ? x;
    sum := sum + x;
    if x < min then
      min := x;
    if x > max then
      max := x;
    i := i + 1
  end;
  ! sum;
  ! (sum / n);
  ! min;
  ! max
end.
```

[📥 Programm herunterladen](../../examples/appendix/statistik/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Summe, Mittelwert, Min, Max.
- Mittelwert ist ganzzahlig.
## Beispiel

Eingabe:

```
4 2 8 4 6
```

Ausgabe:

```
20 5 2 8
```
## Testfaelle

- 1 5 -> 5 5 5 5
- 3 1 2 3 -> 6 2 1 3

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/statistik/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






