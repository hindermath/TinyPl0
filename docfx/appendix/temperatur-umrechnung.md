# Anhang: Temperaturumrechnung

Celsius in Fahrenheit (Integer).

## Programm

```pl0
var c, f;
begin
  ? c;
  f := (c * 9) / 5 + 32;
  ! f
end.
```

[📥 Programm herunterladen](../../examples/appendix/temperatur-umrechnung/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Formel F = C*9/5 + 32.
- Ganzzahlige Rundung durch Division.
## Beispiel

Eingabe:

```
0
```

Ausgabe:

```
32
```
## Testfaelle

- 100 -> 212
- -40 -> -40

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/temperatur-umrechnung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/temperatur-umrechnung/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/temperatur-umrechnung/program.pl0 --list-code --wopcod
```






