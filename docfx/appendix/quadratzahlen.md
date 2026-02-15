# Anhang: Quadratzahlen

Gibt n^2 fuer 1..N aus.

## Programm

```pl0
var n, i;
begin
  ? n;
  i := 1;
  while i <= n do
  begin
    ! (i * i);
    i := i + 1
  end
end.
```

[📥 Programm herunterladen](../../examples/appendix/quadratzahlen/program.pl0)

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
4
```

Ausgabe:

```
1 4 9 16
```
## Testfaelle

- 1 -> 1
- 2 -> 1 4

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadratzahlen/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadratzahlen/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadratzahlen/program.pl0 --list-code --wopcod
```






