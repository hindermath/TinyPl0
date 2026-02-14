# Anhang: Zins (einfach)

Berechnet einfachen Jahreszins.

## Programm

```pl0
var k, p, z;
begin
  ? k;
  ? p;
  z := (k * p) / 100;
  ! z
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Einfache Verzinsung, kein Zinseszins.
- p als Prozentwert verwenden.
## Beispiel

Eingabe:

```
1000 5
```

Ausgabe:

```
50
```
## Testfaelle

- 200 10 -> 20
- 1500 3 -> 45

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






