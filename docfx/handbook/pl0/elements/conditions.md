# Bedingungen

Bedingungen steuern den Programmfluss mit `if ... then` und Vergleichsoperatoren.

## Kurz

```pl0
var x;
begin
  x := 1;
  if x = 1 then
    ! x
end.
```

## Mittel

```pl0
var x;
begin
  x := 5;
  if x > 3 then
    ! x
end.
```

## Ausfuehrlich

```pl0
var a, b;
begin
  a := 4;
  b := 7;
  if a < b then
    ! b
end.
```
