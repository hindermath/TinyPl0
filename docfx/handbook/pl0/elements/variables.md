# Variablen

Variablen werden mit `var` deklariert und koennen spaeter per `:=` zugewiesen werden.

## Regeln

- Variablen muessen vor ihrer Verwendung deklariert werden.
- Werte sind immer Integer.

## Kurz

```pl0
var x;
begin
  x := 1;
  ! x
end.
```

## Mittel

```pl0
var x, y;
begin
  x := 3;
  y := x * 2;
  ! y
end.
```

## Ausfuehrlich

```pl0
var a, b, c;
begin
  a := 5;
  b := 7;
  c := a * b + 3;
  ! a;
  ! b;
  ! c
end.
```

## Erklaerung

- Variablen muessen vor der ersten Zuweisung deklariert sein.
- Die Beispiele zeigen einfache Berechnungen und Ausgabe.
- Im ausfuehrlichen Beispiel werden mehrere Variablen kombiniert.
## Siehe auch

- [Anhang: Summe 1 Bis N](../../../appendix/summe-1-bis-n.md)
- [Anhang: Mittelwert](../../../appendix/mittelwert.md)

