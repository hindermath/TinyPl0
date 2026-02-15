# Anhang: Summe gerade/ungerade

Summiert gerade und ungerade Zahlen bis N.

## Programm

```pl0
var n, i, sEven, sOdd;
begin
  ? n;
  sEven := 0;
  sOdd := 0;
  i := 1;
  while i <= n do
  begin
    if (i / 2) * 2 = i then
      sEven := sEven + i;
    if (i / 2) * 2 # i then
      sOdd := sOdd + i;
    i := i + 1
  end;
  ! sEven;
  ! sOdd
end.
```

[📥 Programm herunterladen](../../examples/appendix/summe-gerade-ungerade/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Gerade pruefung per (i/2)*2.
- Ausgabe: zuerst gerade, dann ungerade Summe.
## Beispiel

Eingabe:

```
7
```

Ausgabe:

```
12 16
```
## Testfaelle

- 1 -> 0 1
- 4 -> 6 4

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/summe-gerade-ungerade/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






