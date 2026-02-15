# Anhang: Abstand zweier Zahlen

Berechnet |a-b|.

## Programm

```pl0
var a, b, d;
begin
  ? a;
  ? b;
  d := a - b;
  if d < 0 then
    d := -d;
  ! d
end.
```

[📥 Programm herunterladen](../../examples/appendix/abstand/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Betrag der Differenz.
- Nutzt Bedingung fuer negatives Ergebnis.
## Beispiel

Eingabe:

```
5 12
```

Ausgabe:

```
7
```
## Testfaelle

- 10 3 -> 7
- -2 4 -> 6

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/abstand/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/abstand/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/abstand/program.pl0 --list-code --wopcod
```






