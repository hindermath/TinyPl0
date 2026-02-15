# Anhang: Zeitumrechnung

Sekunden zu hh:mm:ss (ganzzahlig).

## Programm

```pl0
var s, h, m;
begin
  ? s;
  h := s / 3600;
  s := s - h * 3600;
  m := s / 60;
  s := s - m * 60;
  ! h;
  ! m;
  ! s
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Sekunden in h/m/s zerlegt.
- Ergebnisse einzeln ausgegeben.
## Beispiel

Eingabe:

```
3661
```

Ausgabe:

```
1 1 1
```
## Testfaelle

- 60 -> 0 1 0
- 59 -> 0 0 59

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/zeitumrechnung/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






