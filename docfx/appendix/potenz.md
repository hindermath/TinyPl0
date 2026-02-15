# Anhang: Potenz

Berechnet a^b durch Wiederholung.

## Programm

```pl0
var a, b, i, p;
begin
  ? a;
  ? b;
  p := 1;
  i := 0;
  while i < b do
  begin
    p := p * a;
    i := i + 1
  end;
  ! p
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Exponent wird durch wiederholte Multiplikation aufgebaut.
- b sollte nicht negativ sein.
## Beispiel

Eingabe:

```
2 8
```

Ausgabe:

```
256
```
## Testfaelle

- 3 0 -> 1
- 2 5 -> 32

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/potenz/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






