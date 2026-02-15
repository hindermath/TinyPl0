# Anhang: Geometrische Folge

Gibt eine geometrische Folge aus.

## Programm

```pl0
var a, q, n, i, x;
begin
  ? a;
  ? q;
  ? n;
  i := 0;
  x := a;
  while i < n do
  begin
    ! x;
    x := x * q;
    i := i + 1
  end
end.
```

[📥 Programm herunterladen](../../examples/appendix/geometrische-folge/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Startwert a und Faktor q.
- Gibt n Werte aus.
## Beispiel

Eingabe:

```
2 3 4
```

Ausgabe:

```
2 6 18 54
```
## Testfaelle

- 1 2 3 -> 1 2 4
- 3 3 2 -> 3 9

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/geometrische-folge/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/geometrische-folge/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/geometrische-folge/program.pl0 --list-code --wopcod
```






