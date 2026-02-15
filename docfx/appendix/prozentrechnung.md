# Anhang: Prozentrechnung

Berechnet Prozentwert p% von grundwert g.

## Programm

```pl0
var g, p, w;
begin
  ? g;
  ? p;
  w := (g * p) / 100;
  ! w
end.
```

[📥 Programm herunterladen](../../examples/appendix/prozentrechnung/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Prozentwert = Grundwert * Prozent / 100.
- Ergebnis ist ganzzahlig.
## Beispiel

Eingabe:

```
200 15
```

Ausgabe:

```
30
```
## Testfaelle

- 100 50 -> 50
- 80 25 -> 20

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/prozentrechnung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/prozentrechnung/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/prozentrechnung/program.pl0 --list-code --wopcod
```






