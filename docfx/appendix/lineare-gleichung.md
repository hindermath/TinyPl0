# Anhang: Lineare Gleichung

Loest ax + b = 0 (ganzzahlig).

## Programm

```pl0
var a, b, x;
begin
  ? a;
  ? b;
  if a = 0 then
    ! 0
  else
  begin
    x := -b / a;
    ! x
  end
end.
```

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Bei a=0 wird 0 ausgegeben (keine/inf. Loesungen).
- x wird ganzzahlig berechnet.
## Beispiel

Eingabe:

```
2 4
```

Ausgabe:

```
-2
```
## Testfaelle

- 0 5 -> 0
- 2 -6 -> 3

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- run example.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






