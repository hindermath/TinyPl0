# Anhang: Division durch Subtraktion

Teilt a/b durch wiederholtes Subtrahieren.

## Programm

```pl0
var a, b, q;
begin
  ? a;
  ? b;
  q := 0;
  while a >= b do
  begin
    a := a - b;
    q := q + 1
  end;
  ! q
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Wiederholtes Subtrahieren bis a < b.
- Ergebnis ist Quotient ohne Rest.
## Beispiel

Eingabe:

```
17 5
```

Ausgabe:

```
3
```
## Testfaelle

- 9 3 -> 3
- 8 3 -> 2

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






