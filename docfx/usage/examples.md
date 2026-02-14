# Beispiele

## Kurz

```pl0
const a = 1;
var x;
begin
  x := a;
  ! x
end.
```

## Mittel

```pl0
const limit = 5;
var x;
begin
  x := 0;
  while x < limit do
  begin
    ! x;
    x := x + 1
  end
end.
```

## Ausfuehrlich

```pl0
const limit = 10;
var x;
procedure show;
begin
  ! x
end;
begin
  x := 0;
  while x < limit do
  begin
    call show;
    x := x + 1
  end
end.
```
