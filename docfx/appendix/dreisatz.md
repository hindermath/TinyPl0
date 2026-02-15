# Anhang: Dreisatz

Berechnet x aus a:b = c:x.

## Programm

```pl0
var a, b, c, x;
begin
  ? a;
  ? b;
  ? c;
  x := (b * c) / a;
  ! x
end.
```

[📥 Programm herunterladen](../../examples/appendix/dreisatz/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Direkte Proportion a:b = c:x.
- a darf nicht 0 sein.
## Beispiel

Eingabe:

```
2 10 6
```

Ausgabe:

```
30
```
## Testfaelle

- 4 20 5 -> 25
- 1 3 2 -> 6

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/dreisatz/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






