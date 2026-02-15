# Anhang: Altersberechnung

Berechnet Alter aus Geburtsjahr und aktuellem Jahr.

## Programm

```pl0
var geb, jahr, alter;
begin
  ? geb;
  ? jahr;
  alter := jahr - geb;
  if alter < 0 then
    alter := 0;
  ! alter
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Alter = aktuelles Jahr - Geburtsjahr.
- Negative Werte werden auf 0 gesetzt.
## Beispiel

Eingabe:

```
2000 2025
```

Ausgabe:

```
25
```
## Testfaelle

- 2025 2025 -> 0
- 2010 2025 -> 15

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/altersberechnung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






