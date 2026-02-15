# Anhang: Quadrat Umfang und Flaeche

Berechnet Umfang und Flaeche eines Quadrats.

## Programm

```pl0
var a, u, f;
begin
  ? a;
  u := 4 * a;
  f := a * a;
  ! u;
  ! f
end.
```

[📥 Programm herunterladen](../../examples/appendix/quadrat/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Umfang = 4*a.
- Flaeche = a*a.
## Beispiel

Eingabe:

```
5
```

Ausgabe:

```
20 25
```
## Testfaelle

- 1 -> 4 1
- 3 -> 12 9

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadrat/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadrat/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadrat/program.pl0 --list-code --wopcod
```






