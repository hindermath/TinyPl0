# Anhang: Modulo durch Subtraktion

Berechnet a mod b.

## Programm

```pl0
var a, b;
begin
  ? a;
  ? b;
  while a >= b do
    a := a - b;
  ! a
end.
```

[📥 Programm herunterladen](../../examples/appendix/modulo-durch-subtraktion/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Wiederholtes Subtrahieren liefert den Rest.
- Ergebnis entspricht a mod b.
## Beispiel

Eingabe:

```
17 5
```

Ausgabe:

```
2
```
## Testfaelle

- 8 3 -> 2
- 7 7 -> 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/modulo-durch-subtraktion/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






