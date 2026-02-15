# Anhang: Pythagoras

Berechnet c^2 = a^2 + b^2.

## Programm

```pl0
var a, b, c2;
begin
  ? a;
  ? b;
  c2 := a * a + b * b;
  ! c2
end.
```

[📥 Programm herunterladen](../../examples/appendix/pythagoras/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Berechnet c^2, nicht c.
- Fuer c waere eine Wurzel noetig.
## Beispiel

Eingabe:

```
3 4
```

Ausgabe:

```
25
```
## Testfaelle

- 5 12 -> 169
- 8 15 -> 289

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/pythagoras/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/pythagoras/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/pythagoras/program.pl0 --list-code --wopcod
```






