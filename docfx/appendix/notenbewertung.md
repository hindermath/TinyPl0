# Anhang: Notenbewertung

Punkte -> Note (vereinfacht).

## Programm

```pl0
var p, n;
begin
  ? p;
  n := 6;
  if p >= 90 then
    n := 1;
  if p >= 75 then
    n := 2;
  if p >= 60 then
    n := 3;
  if p >= 45 then
    n := 4;
  if p >= 30 then
    n := 5;
  ! n
end.
```

[📥 Programm herunterladen](../../examples/appendix/notenbewertung/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Einfache Staffelung in Noten 1-6.
- Grenzen sind didaktisch gewaehlt.
## Beispiel

Eingabe:

```
78
```

Ausgabe:

```
2
```
## Testfaelle

- 95 -> 1
- 30 -> 5

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/notenbewertung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/notenbewertung/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/notenbewertung/program.pl0 --list-code --wopcod
```






