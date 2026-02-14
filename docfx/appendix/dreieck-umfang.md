# Anhang: Dreiecksumfang

Berechnet Umfang eines Dreiecks.

## Programm

```pl0
var a, b, c, u;
begin
  ? a;
  ? b;
  ? c;
  u := a + b + c;
  ! u
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Summe aller drei Seiten.
- Keine Plausibilitaetspruefung.
## Beispiel

Eingabe:

```
3 4 5
```

Ausgabe:

```
12
```
## Testfaelle

- 1 1 1 -> 3
- 5 7 9 -> 21

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






