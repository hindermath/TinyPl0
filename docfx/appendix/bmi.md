# Anhang: BMI

BMI in Festkomma (gewicht/(groesse^2)).

## Programm

```pl0
const scale = 100;
var w, h, bmi;
begin
  ? w;
  ? h;
  bmi := (w * scale) / ((h * h) / scale);
  ! bmi
end.
```

[📥 Programm herunterladen](../../examples/appendix/bmi/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Festkomma mit scale=100.
- Groesse in cm, Gewicht in kg.
## Beispiel

Eingabe:

```
80 180
```

Ausgabe:

```
24
```
## Testfaelle

- 60 170 -> 20
- 90 180 -> 27

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/bmi/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/bmi/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/bmi/program.pl0 --list-code --wopcod
```






