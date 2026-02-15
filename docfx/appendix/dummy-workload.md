# Anhang: Schleifenzaehler

Einfacher Zaehler fuer Laufzeit.

## Programm

```pl0
var i, n;
begin
  ? n;
  i := 0;
  while i < n do
    i := i + 1;
  ! i
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Dient zur Demonstration von Laufzeit/Schleifen.
- Ausgabe ist der Endzaehler.
## Beispiel

Eingabe:

```
5
```

Ausgabe:

```
5
```
## Testfaelle

- 0 -> 0
- 3 -> 3

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/dummy-workload/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






