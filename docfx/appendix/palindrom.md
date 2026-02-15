# Anhang: Palindromtest

Prueft, ob eine Zahl ein Palindrom ist.

## Programm

```pl0
var n, t, r, d;
begin
  ? n;
  t := n;
  r := 0;
  while t > 0 do
  begin
    d := t - (t / 10) * 10;
    r := r * 10 + d;
    t := t / 10
  end;
  if r = n then
    ! 1
  else
    ! 0
end.
```

[📥 Programm herunterladen](../../examples/appendix/palindrom/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Dreht Zahl um und vergleicht.
- Ergebnis: 1 = Palindrom, 0 = nein.
## Beispiel

Eingabe:

```
1221
```

Ausgabe:

```
1
```
## Testfaelle

- 123 -> 0
- 11 -> 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/palindrom/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






