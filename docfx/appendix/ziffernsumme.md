# Anhang: Ziffernsumme

Berechnet die Ziffernsumme einer Zahl.

## Programm

```pl0
var n, sum, digit;
begin
  ? n;
  sum := 0;
  while n > 0 do
  begin
    digit := n - (n / 10) * 10;
    sum := sum + digit;
    n := n / 10
  end;
  ! sum
end.
```

[📥 Programm herunterladen](../../examples/appendix/ziffernsumme/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Mod 10 per n - (n/10)*10.
- Schleife endet bei n=0.
## Beispiel

Eingabe:

```
472
```

Ausgabe:

```
13
```
## Testfaelle

- 0 -> 0
- 999 -> 27

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/ziffernsumme/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






