# Anhang: Arithmetische Folge

Gibt eine arithmetische Folge aus.

## Programm

```pl0
var a, d, n, i, x;
begin
  ? a;
  ? d;
  ? n;
  i := 0;
  x := a;
  while i < n do
  begin
    ! x;
    x := x + d;
    i := i + 1
  end
end.
```

[📥 Programm herunterladen](../../examples/appendix/arithmetische-folge/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Startwert a und Schritt d.
- Gibt n Werte aus.
## Beispiel

Eingabe:

```
2 3 4
```

Ausgabe:

```
2 5 8 11
```
## Testfaelle

- 1 1 3 -> 1 2 3
- 5 2 2 -> 5 7

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/arithmetische-folge/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/arithmetische-folge/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/arithmetische-folge/program.pl0 --list-code --wopcod
```






