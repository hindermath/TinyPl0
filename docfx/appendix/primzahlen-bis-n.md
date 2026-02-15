# Anhang: Primzahlen bis N

Gibt Primzahlen bis zu einer oberen Grenze aus.

## Programm

```pl0
var n, i, j, isPrime;
begin
  ? n;
  i := 2;
  while i <= n do
  begin
    isPrime := 1;
    j := 2;
    while j * j <= i do
    begin
      if (i / j) * j = i then
        isPrime := 0;
      j := j + 1
    end;
    if isPrime = 1 then
      ! i;
    i := i + 1
  end
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Schleife testet Teilbarkeit nur bis sqrt(n).
- Ergebnis: 1 = prim, 0 = nicht prim.
## Beispiel

Eingabe:

```
10
```

Ausgabe:

```
2 3 5 7
```
## Testfaelle

- 5 -> 2 3 5
- 1 -> (keine Ausgabe)

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/primzahlen-bis-n/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






