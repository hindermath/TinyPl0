# Anhang: Multiplikation durch Addition

Multipliziert a*b durch Wiederholung.

## Programm

```pl0
var a, b, i, p;
begin
  ? a;
  ? b;
  p := 0;
  i := 0;
  while i < b do
  begin
    p := p + a;
    i := i + 1
  end;
  ! p
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Wiederholtes Addieren.
- b bestimmt die Anzahl der Schritte.
## Beispiel

Eingabe:

```
6 7
```

Ausgabe:

```
42
```
## Testfaelle

- 0 9 -> 0
- 4 3 -> 12

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






