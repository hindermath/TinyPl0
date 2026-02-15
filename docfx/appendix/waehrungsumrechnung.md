# Anhang: Waehrungsumrechnung

Umrechnung mit festem Kurs.

## Programm

```pl0
const kurs = 110;
var eur, jpy;
begin
  ? eur;
  jpy := eur * kurs;
  ! jpy
end.
```

[📥 Programm herunterladen](../../examples/appendix/waehrungsumrechnung/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Fester Kurs als Konstante.
- Multiplikation ohne Rundung.
## Beispiel

Eingabe:

```
10
```

Ausgabe:

```
1100
```
## Testfaelle

- 1 -> 110
- 0 -> 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/waehrungsumrechnung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/waehrungsumrechnung/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/waehrungsumrechnung/program.pl0 --list-code --wopcod
```






