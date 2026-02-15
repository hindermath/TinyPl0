# Anhang: Quadratische Gleichung

Diskriminante und ganzzahlige Loesung (vereinfachtes Beispiel).

## Programm

```pl0
var a, b, c, d, x;
begin
  ? a;
  ? b;
  ? c;
  d := b * b - 4 * a * c;
  if d < 0 then
    ! 0
  else
  begin
    x := (-b + d) / (2 * a);
    ! x
  end
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Diskriminante d entscheidet ueber Existenz der Loesung.
- Wurzel wird hier vereinfacht als d behandelt (didaktisch).
## Beispiel

Eingabe:

```
1 -3 2
```

Ausgabe:

```
4
```
## Testfaelle

- 1 0 -4 -> 4
- 1 0 4 -> 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/quadratische-gleichung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






