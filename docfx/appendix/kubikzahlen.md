# Anhang: Kubikzahlen

Gibt n^3 fuer 1..N aus.

## Programm

```pl0
var n, i;
begin
  ? n;
  i := 1;
  while i <= n do
  begin
    ! (i * i * i);
    i := i + 1
  end
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Ausgabe von i^3 fuer 1..N.
- Eignet sich fuer Tabellen.
## Beispiel

Eingabe:

```
3
```

Ausgabe:

```
1 8 27
```
## Testfaelle

- 1 -> 1
- 2 -> 1 8

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/kubikzahlen/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






