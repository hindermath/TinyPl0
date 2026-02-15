# Anhang: Fakultaet

Berechnet n! iterativ.

## Programm

```pl0
var n, i, f;
begin
  ? n;
  f := 1;
  i := 2;
  while i <= n do
  begin
    f := f * i;
    i := i + 1
  end;
  ! f
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Laufvariable i startet bei 2, Ergebnis in f.
- Fuer n=0 bleibt f=1.
## Beispiel

Eingabe:

```
5
```

Ausgabe:

```
120
```
## Testfaelle

- 0 -> 1
- 6 -> 720

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/fakultaet/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






