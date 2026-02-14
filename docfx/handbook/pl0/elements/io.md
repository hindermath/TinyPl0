# Ein- und Ausgabe

Im erweiterten Dialekt stehen `? ident` (Eingabe) und `! expression` (Ausgabe) zur Verfuegung.

## Regeln

- Ein-/Ausgabe ist nur im Dialekt `extended` erlaubt.
- `?` liest einen Integer, `!` gibt einen Integer aus.

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

## Erklaerung

- `?` liest genau einen Integer von der Eingabe.
- `!` gibt einen Integer aus.
- Im ausfuehrlichen Beispiel wird das Maximum zweier Eingaben ausgegeben.
## Siehe auch

- [Anhang: Min Max](../../../appendix/min-max.md)
- [Anhang: Statistik](../../../appendix/statistik.md)

