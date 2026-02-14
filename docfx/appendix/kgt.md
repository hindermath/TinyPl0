# Anhang: KGT

Dieses Beispiel berechnet das kleinste gemeinsame Vielfache (KGT) zweier Zahlen.

## Programm

```pl0
var a, b, x, y;
procedure gcd;
var t;
begin
  while y # 0 do
  begin
    t := x;
    x := y;
    y := t - (t / y) * y
  end
end;
begin
  ? a;
  ? b;
  x := a;
  y := b;
  call gcd;
  ! (a / x) * b
end.
```

## Erklaerung

- Der groesste gemeinsame Teiler (GGT) wird mit dem Euklidischen Algorithmus ermittelt.
- Aus GGT wird das KGT berechnet: `KGT = (a / GGT) * b`.
## Details

- KGT aus GGT abgeleitet: (a/GGT)*b.
- Nutzt Euklidischen Algorithmus.
## Beispiel

Eingabe:

```
12 18
```

Ausgabe:

```
36
```
## Testfaelle

- 6 8 -> 24
- 7 3 -> 21

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






