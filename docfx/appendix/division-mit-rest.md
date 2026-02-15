# Anhang: Ganzzahl-Division mit Rest

Berechnet Quotient und Rest.

## Programm

```pl0
var a, b, q, r;
begin
  ? a;
  ? b;
  q := a / b;
  r := a - q * b;
  ! q;
  ! r
end.
```

[📥 Programm herunterladen](../../examples/appendix/division-mit-rest/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Rest wird als a - q*b berechnet.
- b darf nicht 0 sein.
## Beispiel

Eingabe:

```
17 5
```

Ausgabe:

```
3 2
```
## Testfaelle

- 20 4 -> 5 0
- 9 2 -> 4 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/division-mit-rest/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/division-mit-rest/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/division-mit-rest/program.pl0 --list-code --wopcod
```






