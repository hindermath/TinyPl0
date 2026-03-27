# Implementation Plan: VM Compatibility for Historical `Inc` Opcode

**Branch**: `002-vm-inc-compat` | **Date**: 2026-03-27 | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/002-vm-inc-compat/spec.md`

## Summary

Dieses Feature erweitert die textuelle P-Code-Einleselogik von TinyPl0 so, dass
historische Artefakte mit dem Mnemonic `Inc` denselben Opcode `Opcode.Int`
erzeugen wie heutige TinyPl0-Artefakte mit `int` oder `Int`. Die kanonische
Ausgabe bleibt unveraendert auf `int`, sodass die Aenderung rein
eingabeseitig-kompatibel bleibt. Die technische Umsetzung konzentriert sich auf
`PCodeSerializer.ParseOpcode`, gezielte Regressionstests in
`PCodeSerializerTests` sowie eine dokumentierte Begriffsklaerung in der
VM-Befehlsdokumentation.

## Technical Context

**Language/Version**: C# 14 / .NET 10  
**Primary Dependencies**: .NET SDK, bestehende Module `Pl0.Core`, `Pl0.Vm`, `Pl0.Cli`, xUnit  
**Storage**: N/A; betroffen sind textuelle `.pcode`-/Listing-Artefakte als Dateiinhalte  
**Testing**: xUnit, insbesondere `tests/Pl0.Tests/PCodeSerializerTests.cs`; optional kompletter `dotnet test` Lauf vor Merge  
**Target Platform**: Windows, macOS und Linux fuer CLI-/VM-Ausfuehrung  
**Project Type**: Compiler-/VM-/CLI-Toolchain mit Dokumentation  
**Performance Goals**: Kein messbarer Rueckschritt; Alias-Erkennung bleibt O(1) je Opcode-Token  
**Constraints**: Keine Modulgrenzen aendern; bestehende `Int`-Artefakte muessen unveraendert bleiben; kanonische Textausgabe bleibt `int`; keine neue Ausnahme- oder Diagnosepolitik; lernerrelevante Erlaeuterungen in Doku und didaktisch relevanten Kommentaren bleiben zweisprachig (DE zuerst, EN danach) auf B2-Niveau  
**Scale/Scope**: Eine eng begrenzte Parser-Aenderung, gezielte Testergaenzungen, eine VM-Doku-Aktualisierung und Statistik-Fortschreibung

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Prinzip | Status | Begruendung |
|---------|--------|-------------|
| I. Didaktische Klarheit | PASS | Lernrelevante Doku wird zweisprachig gepflegt; die historische zu aktueller Terminologie wird explizit erklaert und der Alias-Mapping-Punkt kann bei Bedarf mit didaktischem DE-/EN-Kommentar markiert werden. |
| I. XML-Dokumentation / CS1591 | PASS | Voraussichtlich keine oeffentlichen API-Signaturen betroffen; falls sich das beim Implementieren aendert, bleiben XML-Doku und CS1591-Regel verbindlich. |
| II. Historische Kompatibilitaet | PASS | Das Feature ist explizit eine historische Alias-Kompatibilitaet ohne Semantik-Aenderung. |
| III. Testgetriebene Qualitaet | PASS | Der Plan sieht fail-first Tests fuer `Inc`-Parsing, Alias-Gleichwertigkeit und Invalid-Faelle vor. |
| III. Katalog / Golden / Matrix | PASS | Kein neuer PL/0-Sprachfall und keine Codegenerierungs-Aenderung; daher sind Katalog-, Golden- und Traceability-Artefakte nach aktuellem Stand nicht zu erweitern. |
| IV. Strikte Modularchitektur | PASS | Die Aenderung bleibt in `Pl0.Core` plus Tests und Doku; keine neuen Abhaengigkeiten. |
| V. Diagnostics over Exceptions | PASS | `PCodeSerializer` behaelt seine bestehende FormatException-basierte Textparse-Schnittstelle; es werden nur zusaetzliche gueltige Alias-Tokens akzeptiert. |
| VI. Branch / PR / Versionierung | PASS | Arbeit erfolgt auf Feature-Branch `002-vm-inc-compat`; kein Direkt-Commit auf `main`. |
| Development Workflow / DocFX | PASS | Da `docs/VM_INSTRUCTION_SET.md` geaendert werden soll, ist ein `docfx`-Lauf im Implementierungs-Task einzuplanen. |
| Statistikpflege | PASS | `docs/project-statistics.md` wird bereits in dieser Planungsphase fortgeschrieben. |

**Gate-Ergebnis: PASS**

## Project Structure

### Documentation (this feature)

```text
specs/002-vm-inc-compat/
|-- plan.md                    # Dieses Dokument
|-- research.md                # Phase 0 output
|-- data-model.md              # Phase 1 output
|-- quickstart.md              # Phase 1 output
|-- contracts/
|   `-- pcode-text-contract.md # Textuelle Opcode-/Alias-Schnittstelle
`-- tasks.md                   # Phase 2 output (/speckit.tasks)
```

### Source Code (repository root)

```text
src/Pl0.Core/
`-- PCodeSerializer.cs               AENDERN  - akzeptiert `Inc` als Alias fuer `Opcode.Int`

tests/Pl0.Tests/
`-- PCodeSerializerTests.cs          AENDERN  - neue Alias- und Regressions-Tests

docs/
|-- VM_INSTRUCTION_SET.md            AENDERN  - historische Alias-Beziehung dokumentieren
`-- project-statistics.md            AENDERN  - Planungsphase und Aenderungssatz fortschreiben

docfx/curated/
`-- vm-instruction-set.md            PRUEFEN   - kuratierter Einstieg bleibt auf die gepflegte VM-Doku verdrahtet

specs/002-vm-inc-compat/
|-- plan.md                          AENDERN  - dieser Plan
|-- research.md                      NEU
|-- data-model.md                    NEU
|-- quickstart.md                    NEU
`-- contracts/
    `-- pcode-text-contract.md       NEU
```

**Structure Decision**: Die Umsetzung bleibt im bestehenden Multi-Projekt-Layout
des Repositories. Produktionscode, Tests und Doku werden nur an den bereits
zustaendigen Stellen erweitert; es wird kein neuer Projekttyp, kein neues
Paket und kein neues Modul eingefuehrt.

## Phase 0: Research

**Status**: Completed - siehe [research.md](research.md)

Kernentscheidungen:
1. Alias-Akzeptanz erfolgt ausschliesslich in der textuellen P-Code-Eingabe.
2. Die kanonische Ausgabe bleibt `int`; `ToAsm`, CLI-Listing und enum-basierte
   Anzeige werden nicht auf `Inc` umgestellt.
3. Regressionen werden ueber gezielte Serializer-Tests und Doku-Abgleich
   abgesichert.

## Phase 1: Design & Contracts

**Status**: Completed

Artefakte:
- [data-model.md](data-model.md) - Begriffe, Entitaeten und Regelbeziehungen
- [contracts/pcode-text-contract.md](contracts/pcode-text-contract.md) - Eingabe-/Ausgabevertrag fuer textuelle P-Code-Artefakte
- [quickstart.md](quickstart.md) - Verifikationsablauf nach Implementierung

Implementierungsfokus:
1. `PCodeSerializer.ParseOpcode` erweitert die bestehende
   case-insensitive Mnemonic-Zuordnung um `inc` -> `Opcode.Int`.
2. `PCodeSerializerTests` deckt `Inc`, `INT`, Mischschreibweisen und weiterhin
   ungueltige Opcode-Namen sowie die kanonische Rueckserialisierung auf `int`
   ab.
3. `docs/VM_INSTRUCTION_SET.md` benennt `Inc` explizit als historische
   Eingabeform derselben Stack-Reservierungsoperation und stellt klar, dass
   TinyPl0 intern `Int` und textuell kanonisch `int` verwendet. Die
   Erlaeuterung bleibt zweisprachig mit Deutsch zuerst und Englisch danach auf
   B2-Niveau.
4. Falls die Alias-Stelle in `src/Pl0.Core/PCodeSerializer.cs` erlaeuternde
   Kommentare braucht, werden diese didaktisch begruendend und zweisprachig
   (DE zuerst, EN danach) direkt am Mapping angebracht.

### Success-Criteria-Traceability

| Success Criterion | Planungsartefakt / Nachweisweg |
|-------------------|--------------------------------|
| `SC-001` Gleiches Laufzeitverhalten fuer `Inc`/`Int` | `plan.md` Implementierungsfokus 1-2, `data-model.md` Testrelevante Ableitungen, spaetere xUnit-Regressionstests |
| `SC-002` Bestehende `Int`-Artefakte bleiben stabil | `research.md` Decision 2-3, `quickstart.md` Vergleich `Inc` vs. `Int`, bestehende Serializer-Roundtrips |
| `SC-003` Mindestens ein historisches `Inc`-Artefakt laeuft | `quickstart.md` Schritt 1, `contract` gueltige historische Eingabe |
| `SC-004` Doku erklaert `Inc` / `Int` / `int` widerspruchsfrei | Implementierungsfokus 3, `research.md` Decision 4, `quickstart.md` Dokumentations-Review |
| `SC-005` Kanonische Ausgabe bleibt `int` | `research.md` Decision 2, `data-model.md` Intern nach Ausgabe, `contract` Ausgabeform, spaeterer Serializer-Test |

## Post-Design Constitution Check

| Pruefung | Status | Ergebnis |
|----------|--------|----------|
| Historische Semantik bleibt unveraendert | PASS | Alias wird auf denselben Opcode gemappt; VM-Ausfuehrung bleibt identisch. |
| Architektur bleibt unveraendert | PASS | Nur `Pl0.Core`, Tests und Doku betroffen. |
| Doku- und DocFX-Pflicht eingeplant | PASS | Doku-Datei und kuratierter DocFX-Einstieg sind identifiziert; `docfx` folgt erst nach allen moeglichen Doku-Nachschaerfungen. |
| Test- und Regressionspfad ausreichend | PASS | Positiv-, Gleichwertigkeits- und Negativfaelle sind im Testplan enthalten. |
| Statistikpflege eingeplant | PASS | Planungsphase wird in `docs/project-statistics.md` dokumentiert. |

## Complexity Tracking

Keine Constitution-Verletzungen oder Ausnahmebegruendungen erforderlich.
