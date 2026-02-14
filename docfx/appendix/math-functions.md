# Anhang: Mathematische Funktionen

Hinweis: PL/0 arbeitet nur mit Integern. Die folgenden Beispiele nutzen einfache
Festkomma-Naeherungen und sind didaktisch gedacht.

## sin (naehereungsweise)

```pl0
const scale = 1000, six = 6;
var x, x2, x3, sinx;
begin
  ? x;          /* x in Milliradian */
  x2 := (x * x) / scale;
  x3 := (x2 * x) / scale;
  sinx := x - (x3 / six);
  ! sinx
end.
```

## cos (naehereungsweise)

```pl0
const scale = 1000, two = 2;
var x, x2, cosx;
begin
  ? x;
  x2 := (x * x) / scale;
  cosx := scale - (x2 / two);
  ! cosx
end.
```

## tan (naehereungsweise)

```pl0
var sinx, cosx;
begin
  ? sinx;
  ? cosx;
  if cosx = 0 then
    ! 0
  else
    ! (sinx / cosx)
end.
```

## Kreisberechnung (Flaeche)

```pl0
const pi = 3141, scale = 1000;
var r, area;
begin
  ? r;
  area := (pi * r * r) / scale;
  ! area
end.
```
