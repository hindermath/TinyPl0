# Anhang: Wechselgeld

Greedy-Wechselgeld fuer feste Muenzen.

## Programm

```pl0
var betrag, c, n;
begin
  ? betrag;
  c := betrag / 50;
  betrag := betrag - c * 50;
  n := betrag / 20;
  betrag := betrag - n * 20;
  ! c;
  ! n
end.
```

[📥 Programm herunterladen](../../examples/appendix/wechselgeld/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Greedy fuer 50 und 20 Einheiten.
- Rest wird nicht weiter aufgeteilt.
## Beispiel

Eingabe:

```
130
```

Ausgabe:

```
2 1
```
## Testfaelle

- 70 -> 1 1
- 40 -> 0 2

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/wechselgeld/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/wechselgeld/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/wechselgeld/program.pl0 --list-code --wopcod
```






