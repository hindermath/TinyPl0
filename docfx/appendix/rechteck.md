# Anhang: Rechteck Umfang und Flaeche

Berechnet Umfang und Flaeche eines Rechtecks.

## Programm

```pl0
var a, b, u, f;
begin
  ? a;
  ? b;
  u := 2 * (a + b);
  f := a * b;
  ! u;
  ! f
end.
```

[📥 Programm herunterladen](../../examples/appendix/rechteck/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Umfang = 2*(a+b).
- Flaeche = a*b.
## Beispiel

Eingabe:

```
3 4
```

Ausgabe:

```
14 12
```
## Testfaelle

- 2 3 -> 10 6
- 5 5 -> 20 25

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/rechteck/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/rechteck/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/rechteck/program.pl0 --list-code --wopcod
```






