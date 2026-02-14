# Prozeduren

Prozeduren kapseln wiederverwendbare Ablaufe. PL/0 kennt keine Parameter,
aber Prozeduren koennen auf Variablen in aeusseren Bloecken zugreifen.

## Regeln

- Prozeduren werden mit `procedure <name>;` eingefuehrt.
- Parameter und Rueckgabewerte gibt es nicht.

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

## Erklaerung

- Prozeduren haben keinen Parameter, greifen aber auf aeussere Variablen zu.
- Das mittlere Beispiel erhoeht einen Wert mehrfach.
- Das ausfuehrliche Beispiel zeigt einen lokalen Zaehler innerhalb der Prozedur.
## Siehe auch

- [Anhang: Kgt](../../../appendix/kgt.md)
- [Anhang: Ggt](../../../appendix/ggt.md)

