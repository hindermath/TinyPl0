# Anhang: Einheitenumrechnung

cm -> m -> km.

## Programm

```pl0
var cm, m, km;
begin
  ? cm;
  m := cm / 100;
  km := m / 1000;
  ! m;
  ! km
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- cm -> m -> km, rein ganzzahlig.
- Rundungen durch Division.
## Beispiel

Eingabe:

```
12345
```

Ausgabe:

```
123 0
```
## Testfaelle

- 100 -> 1 0
- 100000 -> 1000 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






