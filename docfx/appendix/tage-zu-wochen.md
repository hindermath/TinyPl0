# Anhang: Tage zu Wochen

Berechnet Wochen und Resttage.

## Programm

```pl0
var t, w, r;
begin
  ? t;
  w := t / 7;
  r := t - w * 7;
  ! w;
  ! r
end.
```

[📥 Programm herunterladen](../../examples/appendix/tage-zu-wochen/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Wochen = t/7, Resttage = t - Wochen*7.
- Beispiele mit kleinen Zahlen.
## Beispiel

Eingabe:

```
17
```

Ausgabe:

```
2 3
```
## Testfaelle

- 7 -> 1 0
- 0 -> 0 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/tage-zu-wochen/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






