# Anhang: Werte tauschen

Tauscht zwei Werte.

## Programm

```pl0
var a, b, t;
begin
  ? a;
  ? b;
  t := a;
  a := b;
  b := t;
  ! a;
  ! b
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Tauscht Werte mit Hilfsvariable.
- Zwei Ausgaben zeigen Ergebnis.
## Beispiel

Eingabe:

```
7 9
```

Ausgabe:

```
9 7
```
## Testfaelle

- 1 2 -> 2 1
- -3 7 -> 7 -3

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






