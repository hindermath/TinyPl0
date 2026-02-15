# Anhang: GGT

Dieses Beispiel berechnet den groessten gemeinsamen Teiler (GGT) zweier Zahlen.

## Programm

```pl0
var x, y, t;
begin
  ? x;
  ? y;
  while y # 0 do
  begin
    t := x;
    x := y;
    y := t - (t / y) * y
  end;
  ! x
end.
```

## Erklaerung

- Die Schleife implementiert den Euklidischen Algorithmus.
- Ergebnis ist der Wert von `x`.
## Details

- Euklidischer Algorithmus per wiederholter Subtraktion/Division.
- Ergebnis ist der GGT.
## Beispiel

Eingabe:

```
54 24
```

Ausgabe:

```
6
```
## Testfaelle

- 8 12 -> 4
- 7 3 -> 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/ggt/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






