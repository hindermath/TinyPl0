# CLI Contract: `--lang` Parameter

## Befehlssyntax

```
dotnet run --project src/Pl0.Cli -- [--lang <code>] <command> [options] [files]
```

`--lang` ist ein globaler, optionaler Parameter der **vor oder nach** dem Unterbefehl
angegeben werden kann. Er gilt für alle Unterbefehle: `compile`, `run`, `run-pcode`.

## Parameter-Spezifikation

| Parameter | Typ | Optional | Default | Beschreibung |
|-----------|-----|----------|---------|--------------|
| `--lang <code>` | string (BCP-47) | ja | `de` | Ausgabesprache für Benutzer-meldungen |

### Gültige Werte

| Wert | Bedeutung |
|------|-----------|
| `de` | Deutsch (Standard) |
| `de-DE` | Deutsch (Deutschland) — Alias für `de` |
| `en` | Englisch |
| `en-US` | Englisch (USA) — Alias für `en` |

### Verhalten bei ungültigem Wert

- Warnung auf `stderr`: `"Unbekannter Sprachcode 'xx', verwende Fallback 'de'."`
- Fallback auf `de`; Programm läuft weiter
- Exit-Code bleibt unverändert (kein Fehler-Exit durch `--lang` allein)

## Auswirkung auf Ausgaben

### Lokalisiert (betroffen von `--lang`)

- `--help` — Hilfe-Text
- Compiler-Diagnosetexte (`CompilerDiagnostic.Message`, `LexerDiagnostic.Message`)
- VM-Laufzeit-Fehlertexte (`VmDiagnostic.Message`)
- Statusmeldungen (z. B. „Kompilierung erfolgreich")
- CLI-Parse-Fehlermeldungen

### Nicht lokalisiert (sprachneutral)

- `--emit asm` — Assembler-Ausgabe (Opcode-Namen)
- `--list-code` — P-Code-Listing
- `--wopcod` — Wopcod-Dump
- `--api` — Docs-Server-Meldungen
- Numerische Fehler-Codes (96, 97, 98, 99, 206)

## Beispiele

```bash
# Hilfe auf Englisch
dotnet run --project src/Pl0.Cli -- --lang en --help

# Kompilierung mit englischen Fehlermeldungen
dotnet run --project src/Pl0.Cli -- --lang en run fehlerhafte.pl0

# Ausführung einer .pcode-Datei mit englischen VM-Meldungen
dotnet run --project src/Pl0.Cli -- --lang en run-pcode programm.pcode

# Unbekannter Sprachcode → Warnung + Fallback Deutsch
dotnet run --project src/Pl0.Cli -- --lang fr run programm.pl0
# stderr: Unbekannter Sprachcode 'fr', verwende Fallback 'de'.
```

## Öffentliche API-Verträge (Pl0.Core / Pl0.Vm)

### `CompilerOptions` (Pl0.Core)

```csharp
public sealed record CompilerOptions(
    Pl0Dialect Dialect,
    int MaxLevel = 3,
    int MaxAddress = 2047,
    int MaxIdentifierLength = 10,
    int MaxNumberDigits = 14,
    int MaxSymbolCount = 100,
    int MaxCodeLength = 200,
    string Language = "de")   // NEU
```

- Bestehende Aufrufe ohne `Language`-Argument bleiben gültig.
- Wert wird nicht intern validiert; `Pl0.Cli` ist für Validierung zuständig.

### `VirtualMachineOptions` (Pl0.Vm)

```csharp
public sealed record VirtualMachineOptions(
    int StackSize = 500,
    bool EnableStoreTrace = false,
    string Language = "de")   // NEU
```

- Bestehende Aufrufe ohne `Language`-Argument bleiben gültig.

### `CompilerDiagnostic` / `LexerDiagnostic` / `VmDiagnostic` — unverändert

Die Record-Strukturen ändern sich nicht. Der `Message`-String wird beim
Erstellen der Diagnose anhand der gewählten Sprache aufgelöst.
Numerische `Code`-Werte bleiben stabil (FR-007).
