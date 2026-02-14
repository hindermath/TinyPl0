# Konstanten

Konstanten werden mit `const` deklariert und koennen im weiteren Programmverlauf
nicht geaendert werden.

## Kurz

```pl0
const pi = 3;
var r;
begin
  r := pi;
  ! r
end.
```

## Mittel

```pl0
const limit = 10, step = 2;
var x;
begin
  x := 0;
  while x < limit do
  begin
    ! x;
    x := x + step
  end
end.
```

## Ausfuehrlich

```pl0
const base = 2, max = 16;
var value;
procedure show;
begin
  ! value
end;
begin
  value := base;
  while value <= max do
  begin
    call show;
    value := value * base
  end
end.
```
