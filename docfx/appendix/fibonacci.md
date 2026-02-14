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

## Erklaerung

- `a` und `b` halten die aktuellen Werte.
- `temp` speichert die Zwischensumme.
- Die Schleife gibt die ersten 10 Werte aus.
