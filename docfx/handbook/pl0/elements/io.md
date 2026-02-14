# Ein- und Ausgabe

Im erweiterten Dialekt stehen `? ident` (Eingabe) und `! expression` (Ausgabe) zur Verfuegung.

## Kurz

```pl0
var x;
begin
  ? x;
  ! x
end.
```

## Mittel

```pl0
var x;
begin
  ? x;
  x := x + 1;
  ! x
end.
```

## Ausfuehrlich

```pl0
var x, y;
var max;
begin
  ? x;
  ? y;
  if x > y then
    max := x;
  if y >= x then
    max := y;
  ! max
end.
```
