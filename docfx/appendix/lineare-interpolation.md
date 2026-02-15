# Anhang: Lineare Interpolation

Berechnet y = y0 + (x-x0)*(y1-y0)/(x1-x0).

## Programm

```pl0
var x0, y0, x1, y1, x, y;
begin
  ? x0;
  ? y0;
  ? x1;
  ? y1;
  ? x;
  y := y0 + (x - x0) * (y1 - y0) / (x1 - x0);
  ! y
end.
```

[📥 Programm herunterladen](../../examples/appendix/lineare-interpolation/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Lineare Interpolation zwischen (x0,y0) und (x1,y1).
- x1 darf nicht gleich x0 sein.
## Beispiel

Eingabe:

```
0 0 10 100 5
```

Ausgabe:

```
50
```
## Testfaelle

- 0 0 10 100 0 -> 0
- 0 0 10 100 10 -> 100

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/lineare-interpolation/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/lineare-interpolation/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/lineare-interpolation/program.pl0 --list-code --wopcod
```






