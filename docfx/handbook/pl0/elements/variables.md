# Variablen

Variablen werden mit `var` deklariert und koennen spaeter per `:=` zugewiesen werden.

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
