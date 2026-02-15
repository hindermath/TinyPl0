# Anhang: Beispiele ausfuehren

Die Beispiele liegen als PL/0-Quelltext in der Dokumentation.
Zum Ausfuehren kopiere den Code in eine Datei und nutze die CLI.

## Schritte

1. Datei anlegen, z. B. `program.pl0`.
2. Programm einfuegen und mit `.` abschliessen.
3. Ausfuehren: `dotnet run --project src/Pl0.Cli -- run program.pl0`

## Beispiele aus dem Repository ausfuehren

Wenn du das Repository ausgecheckt hast, kannst du die Beispiele direkt aus dem Ordner `examples/appendix` starten.

Beispiel BMI:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/bmi/program.pl0
```

## Tipp

Mit `--list-code` kannst du die generierte P-Code-Liste ausgeben. Fuege noch `--wopcod` hinzu, um zusaetzlich den Op-Code anzuzeigen.

P-Code anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/bmi/program.pl0 --list-code
```

P-Code mit Op-Codes anzeigen:

```bash
dotnet run --project src/Pl0.Cli -- examples/appendix/bmi/program.pl0 --list-code --wopcod
```
