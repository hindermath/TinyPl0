# Anhang: Betrag

Berechnet |x|.

## Programm

```pl0
var x;
begin
  ? x;
  if x < 0 then
    x := -x;
  ! x
end.
```

[📥 Programm herunterladen](../../examples/appendix/betrag/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Bedingung prueft negatives Vorzeichen.
- Ergebnis ist immer >= 0.
## Beispiel

Eingabe:

```
-9
```

Ausgabe:

```
9
```
## Testfaelle

- 0 -> 0
- -1 -> 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/betrag/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






