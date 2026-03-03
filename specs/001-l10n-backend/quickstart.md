# Quickstart: L10N Backend verifizieren

## Voraussetzungen

```bash
cd /Users/thorstenhindermann/RiderProjects/TinyPl0
dotnet build
```

## Smoke-Tests nach Implementierung

### 1. Hilfe auf Englisch

```bash
dotnet run --project src/Pl0.Cli -- --lang en --help
```

Erwartung: Usage-Text vollständig auf Englisch.

### 2. Hilfe auf Deutsch (Standard)

```bash
dotnet run --project src/Pl0.Cli -- --help
```

Erwartung: Usage-Text auf Deutsch.

### 3. Compiler-Fehler auf Englisch

Erstelle `tests/data/pl0/invalid/fehler.pl0`:
```pl0
VAR x;
BEGIN
  x := y
END.
```

```bash
dotnet run --project src/Pl0.Cli -- --lang en run tests/data/pl0/invalid/fehler.pl0
```

Erwartung: Englische Diagnosemeldung (z. B. „Undeclared identifier.").

### 4. Compiler-Fehler auf Deutsch (Standard)

```bash
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/invalid/fehler.pl0
```

Erwartung: Deutsche Diagnosemeldung (z. B. „Nicht deklarierter Bezeichner.").

### 5. VM-Laufzeitfehler auf Englisch

Erstelle `tests/data/pl0/valid/divzero.pl0`:
```pl0
VAR x, y;
BEGIN
  x := 1;
  y := 0;
  ! x / y
END.
```

```bash
dotnet run --project src/Pl0.Cli -- --lang en run tests/data/pl0/valid/divzero.pl0
```

Erwartung: Englische VM-Fehlermeldung „Division by zero."

### 6. run-pcode mit Sprachparameter

```bash
dotnet run --project src/Pl0.Cli -- --lang en compile tests/data/pl0/valid/divzero.pl0 --out /tmp/divzero.pcode
dotnet run --project src/Pl0.Cli -- --lang en run-pcode /tmp/divzero.pcode
```

Erwartung: Englische VM-Fehlermeldung.

### 7. Unbekannter Sprachcode — Fallback

```bash
dotnet run --project src/Pl0.Cli -- --lang fr run tests/data/pl0/invalid/fehler.pl0 2>/tmp/warn.txt
cat /tmp/warn.txt
```

Erwartung: `stderr` enthält Warnung über unbekannten Code `fr`; `stdout` enthält
deutsche Fehlermeldung.

### 8. Erweiterbarkeit — Dummy-Sprache

Erstelle `src/Pl0.Core/Resources/Pl0CoreMessages.test.resx` mit einem einzigen
Key (z. B. `Parser_E11_UndeclaredIdent` → `"TEST UNDECLARED"`). Baue das
Projekt. Starte mit `--lang test`:

```bash
dotnet run --project src/Pl0.Cli -- --lang test run tests/data/pl0/invalid/fehler.pl0
```

Erwartung: Diagnosemeldung enthält „TEST UNDECLARED"; alle anderen Keys fallen
auf Deutsch zurück.

## Alle Tests ausführen

```bash
dotnet test
```

Erwartung: Alle bestehenden Tests weiterhin grün; neue L10N-Tests ebenfalls grün.
