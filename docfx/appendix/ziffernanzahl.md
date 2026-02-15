# Anhang: Ziffernanzahl

Zaehlt die Ziffern einer Zahl.

## Programm

```pl0
var n, c;
begin
  ? n;
  if n = 0 then
    c := 1;
  if n # 0 then
  begin
    c := 0;
    while n > 0 do
    begin
      n := n / 10;
      c := c + 1
    end
  end;
  ! c
end.
```

[📥 Programm herunterladen](../../examples/appendix/ziffernanzahl/program.pl0)

## Erklaerung

- Eingaben werden mit `?` gelesen.
- Ausgaben erfolgen ueber `!`.
- Alle Berechnungen sind ganzzahlig.
## Details

- Sonderfall n=0 ergibt 1 Ziffer.
- Schleife zaehlt durch Division.
## Beispiel

Eingabe:

```
1005
```

Ausgabe:

```
4
```
## Testfaelle

- 0 -> 1
- 42 -> 2

## Ausfuehrung

Beispiel:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/ziffernanzahl/program.pl0
```

Tipp: Fuege `--list-code` hinzu, um den P-Code zu sehen.






