# Quickstart: VM `Inc`-Kompatibilitaet verifizieren

## Voraussetzungen

```bash
cd /Users/thorstenhindermann/RiderProjects/TinyPl0
dotnet build
```

## Manuelle Smoke-Checks nach Implementierung

### 1. Historisches `Inc` direkt als P-Code ausfuehren

Erstelle `/tmp/inc-demo.pcode`:

```text
Inc 0 4
Lit 0 7
Sto 0 3
Opr 0 0
```

Dann ausfuehren:

```bash
dotnet run --project src/Pl0.Cli -- run-pcode /tmp/inc-demo.pcode
```

Erwartung: Die Datei wird akzeptiert; es gibt keinen Unknown-Opcode-Fehler.

### 2. `Inc` und `Int` auf dieselbe interne Operation abbilden

Erstelle `/tmp/int-demo.pcode`:

```text
Int 0 4
Lit 0 7
Sto 0 3
Opr 0 0
```

Vergleich:

```bash
dotnet run --project src/Pl0.Cli -- run-pcode /tmp/inc-demo.pcode
dotnet run --project src/Pl0.Cli -- run-pcode /tmp/int-demo.pcode
```

Erwartung: Beide Artefakte verhalten sich identisch.

### 3. Kanonische Ausgabe bleibt `int`

```bash
dotnet test --filter "FullyQualifiedName~Pl0.Tests.PCodeSerializerTests"
```

Erwartung: Ein Test bestaetigt, dass aus `Inc` geparste Instruktionen per
`ToAsm()` wieder mit `int` serialisiert werden.

### 4. Ungueltige aehnliche Mnemonics bleiben ungueltig

Erstelle `/tmp/invalid-inc.pcode`:

```text
Incc 0 4
Opr 0 0
```

Dann:

```bash
dotnet run --project src/Pl0.Cli -- run-pcode /tmp/invalid-inc.pcode
```

Erwartung: TinyPl0 lehnt den Opcode weiterhin als unbekannt ab.

### 5. Dokumentation stellt die Begriffsbruecke explizit her

Oeffne `docs/VM_INSTRUCTION_SET.md` nach der Implementierung und suche die
Beschreibung des Stack-Reservierungs-Opcodes.

Erwartung: Die Doku macht explizit kenntlich, dass historisches `Inc`,
internes `Int` und textuelles TinyPl0-`int` dieselbe Operation bezeichnen.

## Empfohlene Verifikation vor Merge

```bash
dotnet test
docfx docfx.json
```

Erwartung:
- Alle Tests bleiben gruen.
- Die Doku-Regeneration laeuft erfolgreich mit aktualisierter
  VM-Instruktionsbeschreibung.
- Die geaenderte VM-Dokumentation ist der normative Review-Ort fuer die
  Begriffsbeziehung `Inc` / `Int` / `int`.
