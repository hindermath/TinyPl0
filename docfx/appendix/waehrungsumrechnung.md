# Anhang: Waehrungsumrechnung

Umrechnung mit festem Kurs.

## Programm

```pl0
const kurs = 110;
var eur, jpy;
begin
  ? eur;
  jpy := eur * kurs;
  ! jpy
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Fester Kurs als Konstante.
- Multiplikation ohne Rundung.
## Beispiel

Eingabe:

```
10
```

Ausgabe:

```
1100
```
## Testfaelle

- 1 -> 110
- 0 -> 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/waehrungsumrechnung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






