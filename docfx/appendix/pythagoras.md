# Anhang: Pythagoras

Berechnet c^2 = a^2 + b^2.

## Programm

```pl0
var a, b, c2;
begin
  ? a;
  ? b;
  c2 := a * a + b * b;
  ! c2
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Berechnet c^2, nicht c.
- Fuer c waere eine Wurzel noetig.
## Beispiel

Eingabe:

```
3 4
```

Ausgabe:

```
25
```
## Testfaelle

- 5 12 -> 169
- 8 15 -> 289

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






