# Bedingungen

Bedingungen steuern den Programmfluss mit `if ... then` und Vergleichsoperatoren.

## Regeln

- Jede Bedingung liefert 0 (false) oder 1 (true) in der VM.
- Vergleichsoperatoren: =, #, <, <=, >, >=

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

## Erklaerung

- Bedingungen pruefen Relationen und steuern den Kontrollfluss.
- In PL/0 gibt es kein `else`; alternative Zweige werden mit weiteren `if` gebaut.
## Siehe auch

- [Anhang: Primzahltest](../../../appendix/primzahltest.md)
- [Anhang: Zahlenvergleich](../../../appendix/zahlenvergleich.md)

