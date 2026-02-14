# Anhang: Uhrzeitdifferenz

Differenz zweier Zeiten in Minuten.

## Programm

```pl0
var h1, m1, h2, m2, t1, t2, d;
begin
  ? h1;
  ? m1;
  ? h2;
  ? m2;
  t1 := h1 * 60 + m1;
  t2 := h2 * 60 + m2;
  d := t2 - t1;
  if d < 0 then
    d := -d;
  ! d
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Berechnet Differenz in Minuten.
- Ergebnis ist absoluter Wert.
## Beispiel

Eingabe:

```
9 30 11 0
```

Ausgabe:

```
90
```
## Testfaelle

- 0 0 0 0 -> 0
- 10 0 11 30 -> 90

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






