# Anhang: Summe 1 bis N

Berechnet die Summe 1..N.

## Programm

```pl0
var n, i, sum;
begin
  ? n;
  sum := 0;
  i := 1;
  while i <= n do
  begin
    sum := sum + i;
    i := i + 1
  end;
  ! sum
end.
```

[📥 Programm herunterladen](../../examples/appendix/summe-1-bis-n/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Summation per Schleife; alternative Formel (n*(n+1)/2) moeglich.
- n sollte nicht negativ sein.
## Beispiel

Eingabe:

```
10
```

Ausgabe:

```
55
```
## Testfaelle

- 1 -> 1
- 5 -> 15

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/summe-1-bis-n/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






