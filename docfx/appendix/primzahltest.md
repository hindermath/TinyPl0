# Anhang: Primzahltest

Testet, ob eine Zahl prim ist.

## Programm

```pl0
var n, i, isPrime;
begin
  ? n;
  if n < 2 then
    isPrime := 0;
  if n >= 2 then
  begin
    isPrime := 1;
    i := 2;
    while i * i <= n do
    begin
      if (n / i) * i = n then
        isPrime := 0;
      i := i + 1
    end
  end;
  ! isPrime
end.
```

[📥 Programm herunterladen](../../examples/appendix/primzahltest/program.pl0)

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
7
```

Ausgabe:

```
1
```
## Testfaelle

- 2 -> 1
- 9 -> 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/primzahltest/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






