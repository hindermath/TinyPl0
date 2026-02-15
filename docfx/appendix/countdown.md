# Anhang: Countdown

Zaehlt von n nach 0.

## Programm

```pl0
var n;
begin
  ? n;
  while n >= 0 do
  begin
    ! n;
    n := n - 1
  end
end.
```

[📥 Programm herunterladen](../../examples/appendix/countdown/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Zaehlt von n bis 0 inklusive.
- N sollte nicht negativ sein.
## Beispiel

Eingabe:

```
3
```

Ausgabe:

```
3 2 1 0
```
## Testfaelle

- 0 -> 0
- 2 -> 2 1 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/countdown/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/countdown/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/countdown/program.pl0 --list-code --wopcod
```






