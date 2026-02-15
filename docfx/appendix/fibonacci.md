# Anhang: Fibonacci

Dieses Beispiel berechnet die Fibonacci-Folge.

## Programm

```pl0
var a, b, i, temp;
begin
  a := 0;
  b := 1;
  i := 0;
  while i < 10 do
  begin
    ! a;
    temp := a + b;
    a := b;
    b := temp;
    i := i + 1
  end
end.
```

[📥 Programm herunterladen](../../examples/appendix/fibonacci/program.pl0)

## Erklaerung

- `a` und `b` halten die aktuellen Werte.
- `temp` speichert die Zwischensumme.
- Die Schleife gibt die ersten 10 Werte aus.
## Details

- Iterative Berechnung mit zwei Arbeitsvariablen.
- Gibt die ersten 10 Werte aus.
## Beispiel

Eingabe:

```
(no input)
```

Ausgabe:

```
0 1 1 2 3 5 8 13 21 34
```
## Testfaelle

- (no input) -> 0 1 1 2 3 5 8 13 21 34
- (no input) -> 0 1 1 2 3 5 8 13 21 34

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/fibonacci/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/fibonacci/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/fibonacci/program.pl0 --list-code --wopcod
```






