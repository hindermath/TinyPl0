# Anhang: Countdown

Zaehlt von n nach 0.

## Programm

```pl0
var n;
begin
  ? n;
  while n >= 0 do
  begin
    ! n;
    n := n - 1
  end
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Zaehlt von n bis 0 inklusive.
- N sollte nicht negativ sein.
## Beispiel

Eingabe:

```
3
```

Ausgabe:

```
3 2 1 0
```
## Testfaelle

- 0 -> 0
- 2 -> 2 1 0

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






