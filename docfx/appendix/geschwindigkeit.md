# Anhang: Geschwindigkeit

Berechnet Strecke s = v * t.

## Programm

```pl0
var v, t, s;
begin
  ? v;
  ? t;
  s := v * t;
  ! s
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Strecke = Geschwindigkeit * Zeit.
- Einheit konsistent halten.
## Beispiel

Eingabe:

```
50 3
```

Ausgabe:

```
150
```
## Testfaelle

- 0 10 -> 0
- 80 2 -> 160

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






