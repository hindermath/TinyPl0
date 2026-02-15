# Anhang: Zahlenvergleich

Vergleicht a und b.

## Programm

```pl0
var a, b, r;
begin
  ? a;
  ? b;
  r := 0;
  if a < b then
    r := -1;
  if a = b then
    r := 0;
  if a > b then
    r := 1;
  ! r
end.
```

[📥 Programm herunterladen](../../examples/appendix/zahlenvergleich/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Ergebnis: -1 (a<b), 0 (a=b), 1 (a>b).
- Drei getrennte if-Zweige.
## Beispiel

Eingabe:

```
5 9
```

Ausgabe:

```
-1
```
## Testfaelle

- 5 5 -> 0
- 9 4 -> 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zahlenvergleich/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






