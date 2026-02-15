# Anhang: Mittelwert aus N Zahlen

Liest N Zahlen und berechnet den Mittelwert (Integer).

## Programm

```pl0
var n, i, sum, x;
begin
  ? n;
  sum := 0;
  i := 0;
  while i < n do
  begin
    ? x;
    sum := sum + x;
    i := i + 1
  end;
  ! (sum / n)
end.
```

[📥 Programm herunterladen](../../examples/appendix/mittelwert/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Ergebnis ist ganzzahlig gekuerzt.
- n muss groesser als 0 sein.
## Beispiel

Eingabe:

```
4 10 20 30 40
```

Ausgabe:

```
25
```
## Testfaelle

- 1 5 -> 5
- 3 1 2 2 -> 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/mittelwert/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/mittelwert/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/mittelwert/program.pl0 --list-code --wopcod
```






