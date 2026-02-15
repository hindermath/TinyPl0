# Anhang: Summenquadrat vs. Quadratsumme

Vergleicht (sum)^2 und sum(x^2).

## Programm

```pl0
var n, i, sum, sumsq;
begin
  ? n;
  sum := 0;
  sumsq := 0;
  i := 1;
  while i <= n do
  begin
    sum := sum + i;
    sumsq := sumsq + i * i;
    i := i + 1
  end;
  ! (sum * sum);
  ! sumsq
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Umfang = 4*a.
- Flaeche = a*a.
## Beispiel

Eingabe:

```
3
```

Ausgabe:

```
36 14
```
## Testfaelle

- 1 -> 1 1
- 2 -> 9 5

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/summenquadrat-vs-quadratsumme/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






