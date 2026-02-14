# Anhang: Paritaetstest

Prueft gerade/ungerade.

## Programm

```pl0
var x, even;
begin
  ? x;
  even := 0;
  if (x / 2) * 2 = x then
    even := 1;
  ! even
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Gerade/ungerade ueber Division.
- Ausgabe: 1 = gerade, 0 = ungerade.
## Beispiel

Eingabe:

```
8
```

Ausgabe:

```
1
```
## Testfaelle

- 3 -> 0
- 10 -> 1

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






