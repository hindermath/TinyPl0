# Schleifen

Schleifen werden mit `while ... do` gebildet.

## Kurz

```pl0
var x;
begin
  x := 0;
  while x < 3 do
    x := x + 1;
  ! x
end.
```

## Mittel

```pl0
var x;
begin
  x := 1;
  while x <= 4 do
  begin
    ! x;
    x := x + 1
  end
end.
```

## Ausfuehrlich

```pl0
var x, sum;
begin
  x := 1;
  sum := 0;
  while x <= 5 do
  begin
    sum := sum + x;
    x := x + 1
  end;
  ! sum
end.
```
