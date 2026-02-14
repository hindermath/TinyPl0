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
