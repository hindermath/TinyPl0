# Anhang: Kreis Umfang und Flaeche

Berechnet Umfang und Flaeche mit Festkomma.

## Programm

```pl0
const pi = 3141, scale = 1000;
var r, u, f;
begin
  ? r;
  u := (2 * pi * r) / scale;
  f := (pi * r * r) / scale;
  ! u;
  ! f
end.
```

[📥 Programm herunterladen](../../examples/appendix/kreis/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- pi als Festkomma (3141/1000).
- Ergebnisse sind Naeherungen.
## Beispiel

Eingabe:

```
10
```

Ausgabe:

```
62 314
```
## Testfaelle

- 1 -> 6 3
- 2 -> 12 12

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/kreis/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






