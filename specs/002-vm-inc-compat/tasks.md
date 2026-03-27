# Tasks: VM Compatibility for Historical `Inc` Opcode

**Input**: Design documents from `/specs/002-vm-inc-compat/`
**Prerequisites**: plan.md ✅ | spec.md ✅ | research.md ✅ | data-model.md ✅ | contracts/ ✅ | quickstart.md ✅

**Tests**: Explizit erforderlich, weil das Feature ein bestehendes Text-Parsing erweitert und laut Repo-Regeln jede Verhaltensaenderung mit Tests abgesichert werden muss.

**Organization**: Phasen nach User Story; die gemeinsame Serializer-Baseline in Phase 2 blockiert alle Story-Phasen.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Kann parallel ausgefuehrt werden (unterschiedliche Dateien, keine offenen Abhaengigkeiten)
- **[Story]**: Welche User Story (US1-US3)
- Exakte Dateipfade in jeder Beschreibung

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Bestehende Touchpoints und wiederverwendbare Teststruktur fuer das kleine Feature sauber vorbereiten

- [ ] T001 Review der aktuellen Touchpoints in `src/Pl0.Core/PCodeSerializer.cs`, `tests/Pl0.Tests/PCodeSerializerTests.cs`, `docs/VM_INSTRUCTION_SET.md` und `docfx/curated/vm-instruction-set.md` durchfuehren und den Edit-Fokus auf diese Dateien begrenzen
- [ ] T002 Gemeinsame Hilfsdaten und Assertion-Helfer fuer historische, kanonische und ungueltige Mnemonic-Faelle in `tests/Pl0.Tests/PCodeSerializerTests.cs` vorbereiten

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Bestehende `Int`-Semantik und Kanonform als geschuetzte Ausgangsbasis festhalten

**⚠️ CRITICAL**: Keine User-Story-Arbeit beginnt, bevor die Ausgangsbasis fuer Parsing und Ausgabe abgesichert ist

- [ ] T003 Fail-first Baseline-Regressionsfaelle fuer bestehendes `Int`-Parsing und kanonische `int`-Ausgabe in `tests/Pl0.Tests/PCodeSerializerTests.cs` ergaenzen

**Checkpoint**: Die aktuelle TinyPl0-Basis fuer `Int` ist testseitig eingefroren; Story-Arbeit kann beginnen

---

## Phase 3: User Story 1 - Execute historical teaching artifacts without manual opcode renaming (Priority: P1) 🎯 MVP

**Goal**: Historische textuelle P-Code-Artefakte mit `Inc` sollen ohne Umbenennung akzeptiert und aequivalent zu `Int` verarbeitet werden

**Independent Test**: `tests/Pl0.Tests/PCodeSerializerTests.cs` weist nach, dass `Inc`-, `inc`- und gemischte Schreibweisen dieselbe `Instruction`-Sequenz und dasselbe beobachtbare Laufzeitverhalten wie `Int` ergeben

### Tests for User Story 1

- [ ] T004 [US1] Fail-first Alias- und Laufzeitgleichheits-Tests fuer historische `Inc`-/Mischschreibweisen in `tests/Pl0.Tests/PCodeSerializerTests.cs` ergaenzen

### Implementation for User Story 1

- [ ] T005 [US1] Historische `Inc`-Alias-Erkennung in `src/Pl0.Core/PCodeSerializer.cs` implementieren, ohne numerisches Opcode-Parsing zu aendern; falls die didaktisch relevante Alias-Stelle einen Kommentar braucht, dort einen begruendenden DE-/EN-Kommentar mit Deutsch zuerst und Englisch danach ergaenzen
- [ ] T006 [US1] `tests/Pl0.Tests/PCodeSerializerTests.cs` auf den akzeptierten historischen Artefaktpfad fuer `PCodeSerializer.Parse(...)` und VM-Ausfuehrung komplettieren

**Checkpoint**: User Story 1 ist vollstaendig und unabhaengig testbar

---

## Phase 4: User Story 2 - Understand the relationship between historical and current notation (Priority: P2)

**Goal**: Die gepflegte VM-Dokumentation soll `Inc`, internes `Int` und textuelles `int` widerspruchsfrei erklaeren

**Independent Test**: Ein Reviewer kann in `docs/VM_INSTRUCTION_SET.md` direkt nachvollziehen, dass historisches `Inc`, internes `Int` und kanonisches TinyPl0-`int` dieselbe Stack-Reservierungsoperation meinen

### Implementation for User Story 2

- [ ] T007 [P] [US2] Bilingualen Klarstellungstext in `docs/VM_INSTRUCTION_SET.md` fuer `Inc` / `Int` / `int` mit Deutsch zuerst, Englisch danach und B2-Lesbarkeit ergaenzen und den Include-Pfad in `docfx/curated/vm-instruction-set.md` als gepflegten Doku-Einstieg unveraendert mitverwenden

**Checkpoint**: User Story 2 ist unabhaengig reviewbar

---

## Phase 5: User Story 3 - Preserve current TinyPl0 compatibility for `Int` artifacts (Priority: P3)

**Goal**: Bestehende `Int`-Artefakte und die kanonische TinyPl0-Ausgabe `int` bleiben unveraendert, waehrend weiterhin nur echte Alias-Schreibweisen akzeptiert werden

**Independent Test**: `tests/Pl0.Tests/PCodeSerializerTests.cs` zeigt, dass bestehende `Int`-Faelle gruen bleiben, `Inc` weiterhin zu `int` serialisiert wird und aehnliche Ungueltig-Schreibweisen abgewiesen werden

### Tests for User Story 3

- [ ] T008 [P] [US3] Fail-first Regressionstests fuer bestehende `Int`-Eingaben, kanonische `int`-Ausgabe aus `Inc`-Eingaben und ungueltige Near-Miss-Mnemonics in `tests/Pl0.Tests/PCodeSerializerTests.cs` ergaenzen

### Implementation for User Story 3

- [ ] T009 [US3] `src/Pl0.Core/PCodeSerializer.cs` so nachschaerfen, dass ausschliesslich `inc` als Alias zu `Opcode.Int` hinzukommt, `ToAsm()` bei `int` bleibt und ungueltige Mnemonics weiter dieselben Formatfehler ausloesen

**Checkpoint**: Alle drei User Stories sind vollstaendig und unabhaengig testbar

---

## Phase 6: Polish & Cross-Cutting Concerns

**Purpose**: Abschluss-Validierung, Doku-Generierung und Statistikpflege

- [ ] T010 [P] `docs/project-statistics.md` um die abgeschlossene Implementierungs- und Verifikationsphase fuer `002-vm-inc-compat` fortschreiben, inklusive geaenderter Code-, Test- und Dokumentationsumfaenge des Feature-Aenderungssatzes
- [ ] T011 `tests/Pl0.Tests/PCodeSerializerTests.cs` ueber `dotnet test --filter "FullyQualifiedName~Pl0.Tests.PCodeSerializerTests"` gezielt ausfuehren und verbleibende Alias-/Regressionsthemen beheben
- [ ] T012 `specs/002-vm-inc-compat/quickstart.md` manuell durchgehen und noetige Formulierungs- oder Nachweisnachschaerfungen in `docs/VM_INSTRUCTION_SET.md` bzw. `tests/Pl0.Tests/PCodeSerializerTests.cs` vor dem finalen DocFX-Lauf nachziehen
- [ ] T013 `docfx docfx.json` aus dem Repository-Root fuer `docs/VM_INSTRUCTION_SET.md` und `docfx/curated/vm-instruction-set.md` erst nach Abschluss aller Doku-Nachschaerfungen ausfuehren
- [ ] T014 `TinyPl0.sln` ueber `dotnet test` vollstaendig ausfuehren und bestaetigen, dass ausserhalb der Serializer-Tests keine Regression entsteht

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: Keine Abhaengigkeiten - sofort startbar
- **Foundational (Phase 2)**: Haengt von Setup ab - blockiert alle User Stories
- **User Story 1 (Phase 3)**: Haengt von Phase 2 ab - MVP
- **User Story 2 (Phase 4)**: Haengt von Phase 2 ab - kann nach US1 oder parallel dazu bearbeitet werden, sobald der Scope stabil ist
- **User Story 3 (Phase 5)**: Haengt von Phase 2 und praktisch von US1 ab, weil die Alias-Implementierung zuerst stehen muss
- **Polish (Phase 6)**: Haengt von allen gewaehlten User Stories ab

### User Story Dependencies

- **US1 (P1)**: Startet nach Phase 2 und liefert das eigentliche neue Verhalten
- **US2 (P2)**: Kann nach Phase 2 parallel zu spaeteren Testarbeiten laufen, weil die Doku in `docs/VM_INSTRUCTION_SET.md` von `src/Pl0.Core/PCodeSerializer.cs` getrennt ist
- **US3 (P3)**: Baut auf der in US1 eingefuehrten Alias-Unterstuetzung auf und haertet Rueckwaertskompatibilitaet sowie Negativfaelle ab

### Within Each User Story

- Tests werden vor der jeweils zugehoerigen Verhaltensaenderung oder Nachschaerfung geschrieben
- Parser-Aenderungen in `src/Pl0.Core/PCodeSerializer.cs` folgen erst nach den fail-first Faellen in `tests/Pl0.Tests/PCodeSerializerTests.cs`
- Dokumentation wird nach gefestigter Terminologie aktualisiert, aber vor `docfx`

### Parallel Opportunities

- T001 und T002 koennen nacheinander schlank erledigt werden; Phase 1 ist klein
- Nach T005 kann T007 parallel zu T008 laufen (`docs/VM_INSTRUCTION_SET.md` vs. `tests/Pl0.Tests/PCodeSerializerTests.cs`)
- In der Polish-Phase koennen T010 und T011 parallel laufen; T013 folgt erst nach T012

---

## Parallel Example: User Story 2 + User Story 3

```bash
# Nach abgeschlossener Alias-Implementierung aus US1:
Task: T007 - Update docs/VM_INSTRUCTION_SET.md and rely on docfx/curated/vm-instruction-set.md include path
Task: T008 - Extend tests/Pl0.Tests/PCodeSerializerTests.cs with Int-regression, canonical-output, and invalid-mnemonic cases
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Phase 1 abschliessen
2. Phase 2 abschliessen
3. Phase 3 abschliessen
4. **STOP and VALIDATE**: Nur `tests/Pl0.Tests/PCodeSerializerTests.cs` gezielt laufen lassen
5. Historische `Inc`-Artefakte koennen danach bereits ohne manuelle Umbenennung eingelesen werden

### Incremental Delivery

1. Setup + Foundational -> gemeinsame Basis geschuetzt
2. US1 -> historischer Alias funktioniert
3. US2 -> Terminologie in der Doku wird nachvollziehbar
4. US3 -> Rueckwaertskompatibilitaet und Negativfaelle sind abgesichert
5. Polish -> Statistik, `docfx`, gezielte und volle Testlaeufe

### Single-Developer Strategy

1. T001-T003 fuer Basis und fail-first Ausgangspunkt
2. T004-T006 fuer das MVP
3. T007 fuer die gepflegte Doku
4. T008-T009 fuer Rueckwaertskompatibilitaet und Negativraum
5. T010-T014 fuer Abschlussvalidierung

---

## Notes

- [P] bedeutet: unterschiedliche Dateien, keine offene Abhaengigkeit auf unvollstaendige Vorarbeit
- Dieses Feature braucht keine neuen Projekte, keine neuen NuGet-Pakete und keine Architekturverschiebung
- Oeffentliche API-Signaturen sollen unveraendert bleiben; falls sich das beim Implementieren aendert, muessen XML-Dokumentation und CS1591-Regel mitgezogen werden
- Lernerrelevante Doku und didaktisch relevante Kommentare muessen fuer dieses Feature zweisprachig bleiben: Deutsch zuerst, Englisch danach, jeweils auf B2-Niveau
- `docfx` bleibt Pflicht, sobald `docs/VM_INSTRUCTION_SET.md` geaendert wurde, und laeuft erst nach der letzten Doku-Nachschaerfung
- `docs/project-statistics.md` ist fuer diese abgeschlossene Spec-Kit-Phase erneut zu aktualisieren
