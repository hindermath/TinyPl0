# Anhang: Umkehrung einer Zahl

Kehrt die Ziffern einer Zahl um.

## Programm

```pl0
var n, r, d;
begin
  ? n;
  r := 0;
  while n > 0 do
  begin
    d := n - (n / 10) * 10;
    r := r * 10 + d;
    n := n / 10
  end;
  ! r
end.
```

[📥 Programm herunterladen](../../examples/appendix/zahl-umkehrung/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Reversiert Ziffern durch Mod/Division.
- Ergebnis ist die umgekehrte Zahl.
## Beispiel

Eingabe:

```
12340
```

Ausgabe:

```
4321
```
## Testfaelle

- 100 -> 1
- 907 -> 709

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zahl-umkehrung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zahl-umkehrung/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zahl-umkehrung/program.pl0 --list-code --wopcod
```






