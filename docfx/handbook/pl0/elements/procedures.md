# Prozeduren

Prozeduren kapseln wiederverwendbare Ablaufe. PL/0 kennt keine Parameter,
aber Prozeduren koennen auf Variablen in aeusseren Bloecken zugreifen.

## Kurz

```pl0
var x;
procedure show;
begin
  ! x
end;
begin
  x := 1;
  call show
end.
```

## Mittel

```pl0
var x;
procedure inc;
begin
  x := x + 1
end;
begin
  x := 0;
  call inc;
  call inc;
  ! x
end.
```

## Ausfuehrlich

```pl0
var x;
procedure loop;
var i;
begin
  i := 0;
  while i < 3 do
  begin
    x := x + 1;
    i := i + 1
  end
end;
begin
  x := 5;
  call loop;
  ! x
end.
```
