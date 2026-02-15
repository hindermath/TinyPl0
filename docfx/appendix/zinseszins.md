# Anhang: Zinseszins

Berechnet Endkapital nach n Jahren.

## Programm

```pl0
var k, p, n, i;
begin
  ? k;
  ? p;
  ? n;
  i := 0;
  while i < n do
  begin
    k := k + (k * p) / 100;
    i := i + 1
  end;
  ! k
end.
```

[📥 Programm herunterladen](../../examples/appendix/zinseszins/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Iterative Verzinsung pro Jahr.
- p als Prozentwert verwenden.
## Beispiel

Eingabe:

```
1000 5 2
```

Ausgabe:

```
1102
```
## Testfaelle

- 1000 10 1 -> 1100
- 1000 10 2 -> 1210

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zinseszins/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zinseszins/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zinseszins/program.pl0 --list-code --wopcod
```






