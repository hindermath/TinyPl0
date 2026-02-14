# Anhang: Rabattberechnung

Berechnet Preis nach Rabatt in Prozent.

## Programm

```pl0
var preis, rabatt, neu;
begin
  ? preis;
  ? rabatt;
  neu := preis - (preis * rabatt) / 100;
  ! neu
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Preis minus Prozentanteil.
- Ergebnis ist ganzzahlig.
## Beispiel

Eingabe:

```
200 10
```

Ausgabe:

```
180
```
## Testfaelle

- 100 0 -> 100
- 50 20 -> 40

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






