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
