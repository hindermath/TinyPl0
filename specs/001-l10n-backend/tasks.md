# Tasks: L10N Backend (Pl0.Core / Pl0.Vm / Pl0.Cli)

**Input**: Design documents from `/specs/001-l10n-backend/`
**Prerequisites**: plan.md ✅ | spec.md ✅ | data-model.md ✅ | contracts/ ✅ | research.md ✅ | quickstart.md ✅

**Tests**: Explizit gefordert (SC-002, SC-003, SC-004) — alle ~75 EN-Keys einzeln in `L10nTests.cs`.

**Organization**: Phasen nach User Story; L10N-Infrastruktur (Phase 2) ist harter Blocker für alle Stories.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Kann parallel ausgeführt werden (unterschiedliche Dateien, keine offenen Abhängigkeiten)
- **[Story]**: Welche User Story (US1–US4)
- Exakte Dateipfade in jeder Beschreibung

---

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Verzeichnisstruktur und Test-Klassen-Gerüst anlegen

- [x] T001 Verzeichnis `src/Pl0.Core/Resources/` anlegen (wird von SDK für `.resx`-Dateien benötigt)
- [x] T002 [P] Verzeichnis `src/Pl0.Vm/Resources/` anlegen
- [x] T003 [P] Verzeichnis `src/Pl0.Cli/Resources/` anlegen
- [x] T004 [P] Verzeichnis `tests/data/pl0/l10n/` für L10N-Testprogramme anlegen
- [x] T005 [P] Verzeichnis `tests/Pl0.Tests/Resources/` für Swedish Dummy-Fixtures (SC-004) anlegen
- [x] T006 Leere Testklasse `tests/Pl0.Tests/L10nTests.cs` anlegen (`public sealed class L10nTests`, kein Inhalt außer Namespace und `using`)

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Options-Records und CLI-Parsing erweitern — BLOCKER für alle User Stories

**⚠️ CRITICAL**: Keine User-Story-Arbeit kann beginnen, bis diese Phase abgeschlossen ist

- [x] T007 [P] `src/Pl0.Core/CompilerOptions.cs` erweitern — optionale Parameter `string Language = "de"` und `ResourceManager? Messages = null` am Ende des primary constructors hinzufügen (nicht-breaking; bestehende Aufrufe bleiben gültig; Kommentar auf Deutsch); XML-Dokumentationskommentare (`<param>`) für beide neuen Parameter auf Deutsch ergänzen (Constitution I)
- [x] T008 [P] `src/Pl0.Vm/VirtualMachineOptions.cs` erweitern — analog: optionale Parameter `string Language = "de"` und `ResourceManager? Messages = null` hinzufügen; XML-Dokumentationskommentare (`<param>`) für beide neuen Parameter auf Deutsch ergänzen (Constitution I)
- [x] T009 `src/Pl0.Cli/Cli/CliOptionsParser.cs` erweitern — zwei optionale Konstruktor-Parameter hinzufügen: `TextWriter? errorOutput = null` (Default: `Console.Error`, für FR-009 stderr-Capture) und `ResourceManager? cliMessages = null` (für FR-008 Injection); `--lang <code>` parsen: `CultureInfo.GetCultureInfo(code)` aufrufen; bei `CultureNotFoundException` Warnung auf `errorOutput` schreiben und auf `"de"` zurückfallen; geparsten Code in `ParsedOptions.Language` ablegen; XML-Dokumentationskommentare (`<param>`) für beide neuen Parameter auf Deutsch ergänzen (Constitution I)
  ⚠️ HINWEIS: `Pl0CliMessages` (Designer-Klasse) existiert erst nach T010 (`.resx`-Erstellung + Build). Den `cliMessages`-Parameter vorerst nur mit `null` verwenden; die interne Auflösung `_cliMessages = cliMessages ?? Pl0CliMessages.ResourceManager` im Konstruktor-Body erst in T013 ergänzen, sobald die Designer-Klasse verfügbar ist.

**Checkpoint**: Foundation bereit — User Stories können beginnen

---

## Phase 3: User Story 1 — CLI-Ausgabe in gewählter Sprache (Priority: P1) 🎯 MVP

**Goal**: `Pl0.Cli --lang en --help` zeigt vollständigen Hilfe-Text auf Englisch; ohne `--lang` erscheint Deutsch

**Independent Test**: `dotnet run --project src/Pl0.Cli -- --lang en --help` → Usage-Text vollständig auf Englisch; `--lang de --help` → Deutsch; `--lang xx` → stderr-Warnung + Deutsch

### Implementation für User Story 1

- [x] T010 [US1] `src/Pl0.Cli/Resources/Pl0CliMessages.resx` anlegen — alle 25 deutschen CLI-Strings exakt gemäss `data-model.md §5`; Build-Action `EmbeddedResource`; Custom Tool `ResXFileCodeGenerator`; Namespace `Pl0.Cli`; Klasse `Pl0CliMessages`; Access `internal`
- [x] T011 [P] [US1] `src/Pl0.Cli/Resources/Pl0CliMessages.en.resx` anlegen — alle 25 englischen CLI-Strings exakt gemäss `data-model.md §5 Beispiel (en)`; gleiche Keys; Build-Action `EmbeddedResource`
- [x] T012 [US1] `src/Pl0.Cli/Cli/CliHelpPrinter.cs` refaktorieren — alle 17 `Cli_Help_*`-Hardcoded-Strings durch `ResourceManager`-Lookups ersetzen (`rm.GetString(key, culture)` wobei `rm` und `culture` aus injiziertem `cliMessages` + `Language` abgeleitet); Kommentare auf Deutsch
- [x] T013 [US1] `src/Pl0.Cli/Cli/CliOptionsParser.cs` ergänzen — verbleibende Hardcoded-Strings (`Cli_Err_*`, `Cli_Status_*`) durch ResourceManager-Lookups via `cliMessages` ersetzen; `_cliMessages = cliMessages ?? Pl0CliMessages.ResourceManager` im Konstruktor-Body ergänzen (Pl0CliMessages ist jetzt nach T010 verfügbar); Status-Meldungen in `Program.cs` oder `CliRunner` analog behandeln; `ParsedOptions.Language` in **allen drei Befehlspfaden** (`compile`, `run`, `run-pcode`) an `CompilerOptions(Language:)` und `VirtualMachineOptions(Language:)` weitergeben (FR-011)

### Tests für User Story 1

- [x] T014 [US1] `tests/Pl0.Tests/L10nTests.cs` — 17 Testmethoden für alle `Cli_Help_*`-Keys: `Cli_Help_UsageHeader_En`, `Cli_Help_SwitchesHeader_En`, `Cli_Help_CompileLine_En` usw.; prüfen via `CliOptionsParser(cliMessages: ...) + --lang en + --help`; erwartete EN-Texte aus `data-model.md §5`
- [x] T015 [US1] `tests/Pl0.Tests/L10nTests.cs` — 6 Testmethoden für `Cli_Err_*`-Keys und 2 für `Cli_Status_*`-Keys (8 gesamt): `Cli_Err_UnknownSwitch_En`, `Cli_Err_UnknownLanguage_En`, `Cli_Status_CompileSuccess_En` usw.
- [x] T016 [US1] `tests/Pl0.Tests/L10nTests.cs` — 4 Fallback-Ketten-Tests: `LangEnUs_FallsBackToEn`, `LangFr_FallsBackToGerman`, `EmptyLanguage_FallsBackToGerman`, `UnknownLanguage_FallsBackToGerman_WithStderrWarning` (prüft `StringWriter`-Inhalt auf Sprachcode `"xx"`)

**Checkpoint**: User Story 1 vollständig und unabhängig testbar — `dotnet test --filter L10nTests`

---

## Phase 4: User Story 2 — Compiler-Fehlermeldungen in gewählter Sprache (Priority: P1)

**Goal**: Compiler-Diagnosen (`CompilerDiagnostic`, `LexerDiagnostic`) erscheinen in der per `--lang` gewählten Sprache

**Independent Test**: `dotnet run --project src/Pl0.Cli -- --lang en run tests/data/pl0/l10n/undeclared_ident.pl0` → englischer Diagnosetext `"Undeclared identifier."`; ohne `--lang` → `"Nicht deklarierter Bezeichner."`

### Implementation für User Story 2

- [x] T017 [P] [US2] `src/Pl0.Core/Resources/Pl0CoreMessages.resx` anlegen — alle 37 deutschen Lexer/Parser-Fehlertexte exakt gemäss `data-model.md §3 Beispiel (de)`; Build-Action `EmbeddedResource`; Custom Tool `ResXFileCodeGenerator`; Namespace `Pl0.Core`; Klasse `Pl0CoreMessages`; Access `internal`
- [x] T018 [P] [US2] `src/Pl0.Core/Resources/Pl0CoreMessages.en.resx` anlegen — alle 37 englischen Texte exakt gemäss `data-model.md §3 Beispiel (en)`; gleiche Keys wie `.resx`
- [x] T019 [US2] `src/Pl0.Core/Pl0Lexer.cs` refaktorieren — `ResourceManager rm = _options.Messages ?? Pl0CoreMessages.ResourceManager` und `CultureInfo culture = CultureInfo.GetCultureInfo(_options.Language)` im Lexer-Kontext ableiten; alle 5 Inline-Error-Strings durch `rm.GetString("Key", culture)` ersetzen; Formatierung via `string.Format(culture, template, args)`; Kommentare auf Deutsch
- [x] T020 [US2] `src/Pl0.Core/Pl0Parser.cs` refaktorieren — analog zu T019; alle 32 Inline-Error-Strings der Parser-Diagnosen durch ResourceManager-Lookups ersetzen; `_options.Messages` und `_options.Language` verwenden

### Tests für User Story 2

- [x] T021 [P] [US2] Testdaten-Programme in `tests/data/pl0/l10n/` anlegen für alle 5 Lexer-Fehler: `number_too_many_digits.pl0` (E30), `number_too_large_lexer.pl0` (E30), `ident_too_long.pl0` (E33), `unexpected_colon.pl0` (E99), `unexpected_char.pl0` (E99)
- [x] T022 [P] [US2] Testdaten-Programme in `tests/data/pl0/l10n/` anlegen für Parser-Fehler: `use_equal_not_assign.pl0` (E01), `undeclared_ident.pl0` (E11), `assign_to_const.pl0` (E12), `missing_then.pl0` (E16), `missing_do.pl0` (E18), `period_expected.pl0` (E09), `nesting_too_deep.pl0` (E32), und weitere je Fehlercode gemäss `plan.md §Source-Code-Structure`
- [x] T023 [US2] `tests/Pl0.Tests/L10nTests.cs` — 5 Testmethoden für alle Lexer-Keys (`Lexer_NumberTooManyDigits_En`, `Lexer_NumberTooLarge_En`, `Lexer_IdentifierTooLong_En`, `Lexer_UnexpectedColon_En`, `Lexer_UnexpectedChar_En`); via `CliOptionsParser --lang en`; erwartete Texte aus `data-model.md §3 Beispiel (en)`
- [x] T024 [US2] `tests/Pl0.Tests/L10nTests.cs` — 32 Testmethoden für alle Parser-Keys (vollständige Liste in `plan.md §Parser-Keys`); via `CliOptionsParser --lang en`; je eine Testmethode pro Key; erwartete Texte aus `data-model.md §3 Beispiel (en)`
- [x] T025 [US2] `tests/Pl0.Tests/L10nTests.cs` — DE-Baseline-Tests: `CompilerDiagnostic_De_ContainsGermanMessage` und `LexerDiagnostic_De_ContainsGermanMessage` (prüfen dass DE ohne `--lang` korrekt erscheint)

**Checkpoint**: User Stories 1 und 2 funktionieren unabhängig

---

## Phase 5: User Story 3 — VM-Laufzeitfehler in gewählter Sprache (Priority: P2)

**Goal**: VM-Fehlermeldungen (`VmDiagnostic`) erscheinen in der per `--lang` gewählten Sprache

**Independent Test**: `dotnet run --project src/Pl0.Cli -- --lang en run tests/data/pl0/l10n/division_by_zero.pl0` → `"Division by zero."`; ohne `--lang` → `"Division durch null."`

### Implementation für User Story 3

- [x] T026 [P] [US3] `src/Pl0.Vm/Resources/Pl0VmMessages.resx` anlegen — alle 13 deutschen VM-Fehlertexte exakt gemäss `data-model.md §4 Beispiel (de)`; Build-Action `EmbeddedResource`; Custom Tool `ResXFileCodeGenerator`; Namespace `Pl0.Vm`; Klasse `Pl0VmMessages`; Access `internal`
- [x] T027 [P] [US3] `src/Pl0.Vm/Resources/Pl0VmMessages.en.resx` anlegen — alle 13 englischen Texte exakt gemäss `data-model.md §4 Beispiel (en)`
- [x] T028 [US3] `src/Pl0.Vm/VirtualMachine.cs` refaktorieren — `_rm = options.Messages ?? Pl0VmMessages.ResourceManager` und `_culture = CultureInfo.GetCultureInfo(options.Language)` im Konstruktor ableiten; alle 11 Inline-Fehlertexte aus `throw`-Statements und Diagnose-Erstellungen durch `_rm.GetString(key, _culture)` ersetzen; Formatierung via `string.Format(_culture, template, args)`; E98/E97 (EndOfInput, InputFormatError) werden durch Exception-Catching im VM-Run-Loop lokalisiert: Exception-Message durch `_rm.GetString("Vm_E98_EndOfInput", _culture)` bzw. `_rm.GetString("Vm_E97_InputFormatError", _culture)` ersetzen; Kommentare auf Deutsch

### Tests für User Story 3

- [x] T029 [P] [US3] Testdaten-Programme in `tests/data/pl0/l10n/` anlegen: `division_by_zero.pl0` (E206), `end_of_input.pl0` (E98 — Leseoperation ohne buffered Input), `input_format_error.pl0` (E97 — nicht-ganzzahliger Input)
- [x] T030 [US3] `tests/Pl0.Tests/L10nTests.cs` — 3 Testmethoden für PL/0-auslösbare VM-Keys via `CliOptionsParser --lang en` + `BufferedPl0Io`: `Vm_DivisionByZero_En`, `Vm_EndOfInput_En`, `Vm_InputFormatError_En`; erwartete Texte aus `data-model.md §4 Beispiel (en)`
- [x] T031 [US3] `tests/Pl0.Tests/L10nTests.cs` — 10 Testmethoden für VM-interne Keys via direkter `VirtualMachine`-Konstruktion mit `VirtualMachineOptions { Language = "en" }` und `Instruction[]`-inline-P-Code: `Vm_IPOutOfRange_En`, `Vm_InvalidLodIndex_En`, `Vm_InvalidStoIndex_En`, `Vm_StackOverflowCallFrame_En`, `Vm_StackOverflowInt_En`, `Vm_UnsupportedOpcode_En`, `Vm_UnsupportedOpr_En`, `Vm_InvalidBasePointer_En`, `Vm_StackOverflow_En`, `Vm_StackUnderflow_En`
- [x] T032 [US3] `tests/Pl0.Tests/L10nTests.cs` — DE-Baseline-Test: `VmDiagnostic_De_ContainsGermanMessage` (prüft Division-durch-null auf Deutsch)

**Checkpoint**: User Stories 1, 2 und 3 funktionieren unabhängig

---

## Phase 6: User Story 4 — Neue Sprache ohne Quellcode-Änderung erweiterbar (Priority: P3)

**Goal**: Neue Sprache (Schwedisch als Dummy) durch Hinzufügen von `.resx`-Dateien einbindbar — automatisierter Test beweist die Erweiterbarkeit

**Independent Test**: `dotnet test --filter NewLocale_Se_` → alle 3 Swedish-Extensibility-Tests grün

### Implementation für User Story 4

- [x] T033 [P] [US4] `tests/Pl0.Tests/Resources/Pl0CoreMessages.se.resx` anlegen — schwedische Dummy-Texte für mindestens 1 Core-Key (z. B. `Parser_E11_UndeclaredIdent` → `"Odefinierad identifierare."`); Build-Action `EmbeddedResource` in `Pl0.Tests.csproj`; kein Produktions-Build-Einfluss
- [x] T034 [P] [US4] `tests/Pl0.Tests/Resources/Pl0VmMessages.se.resx` anlegen — schwedischer Dummy-Text für mindestens 1 VM-Key (z. B. `Vm_E206_DivisionByZero` → `"Division med noll."`)
- [x] T035 [P] [US4] `tests/Pl0.Tests/Resources/Pl0CliMessages.se.resx` anlegen — schwedischer Dummy-Text für mindestens 1 CLI-Key (z. B. `Cli_Status_CompileSuccess` → `"Kompileringen lyckades."`)
- [x] T036 [US4] `tests/Pl0.Tests/Pl0.Tests.csproj` anpassen — alle drei `.se.resx`-Dateien als `<EmbeddedResource>` einbinden, damit `ResourceManager`-Lookup in Tests funktioniert

### Tests für User Story 4

- [x] T037 [US4] `tests/Pl0.Tests/L10nTests.cs` — 3 Testmethoden für SC-004 Extensibility: `NewLocale_Se_InjectedResourceManager_Core_ReturnsSwedishMessage`, `NewLocale_Se_InjectedResourceManager_Vm_ReturnsSwedishMessage`, `NewLocale_Se_InjectedResourceManager_Cli_ReturnsSwedishMessage`; jede Methode erstellt `ResourceManager` aus EmbeddedResource in `tests/Pl0.Tests/Resources/` und injiziert ihn via `CompilerOptions.Messages` / `VirtualMachineOptions.Messages` / `CliOptionsParser(cliMessages:)`

**Checkpoint**: Alle vier User Stories vollständig funktional und unabhängig testbar

---

## Phase 7: Polish & Cross-Cutting Concerns

**Purpose**: Abschluss-Validierung und Traceability

- [x] T038 [P] `dotnet build --configuration Release` ausführen und prüfen dass Satellite Assemblies (`de/`, `en/`) in `bin/Release/` erzeugt werden — beweist dass SDK-automatische `.resx`-Kompilierung funktioniert
- [x] T039 `dotnet test` ausführen — alle bestehenden Tests müssen weiterhin grün sein (SC-001: keine Regressionen); alle neuen L10nTests ebenfalls grün
- [x] T040 [P] Alle englischen `.en.resx`-Inhalte aller drei Module gegen `data-model.md §7` (Terminologietabelle) und NFR-001 (B2 CEFR) prüfen; Abweichungen als Review-Kommentar im PR dokumentieren (NFR-001, NFR-003)
- [x] T041 Quickstart-Smoke-Tests aus `quickstart.md` manuell ausführen (§1–§8) — Ende-zu-Ende-Validierung der vollständigen L10N-Kette in echter CLI-Umgebung

---

## Dependencies & Execution Order

### Phase-Abhängigkeiten

- **Setup (Phase 1)**: Keine Abhängigkeiten — sofort startbar
- **Foundational (Phase 2)**: Hängt von Phase 1 ab — **BLOCKER für alle User Stories**
- **User Story 1 (Phase 3)**: Hängt von Phase 2 ab; keine Abhängigkeit von US2/US3/US4
- **User Story 2 (Phase 4)**: Hängt von Phase 2 ab; CliOptionsParser aus Phase 2 (T009) muss vorliegen da Tests via `--lang` testen
- **User Story 3 (Phase 5)**: Hängt von Phase 2 ab; CliOptionsParser aus Phase 2 (T009) muss vorliegen für PL/0-auslösbare VM-Tests
- **User Story 4 (Phase 6)**: Hängt von Phase 2 (T007, T008, T009) ab; benötigt ResourceManager-Injection aus allen drei Modulen
- **Polish (Phase 7)**: Hängt von allen User Stories ab

### User-Story-Abhängigkeiten

- **US1 (P1)**: Kann nach Phase 2 starten — keine Abhängigkeit von anderen Stories
- **US2 (P1)**: Kann nach Phase 2 starten; Tests verwenden `CliOptionsParser --lang` aus T009
- **US3 (P2)**: Kann nach Phase 2 starten; PL/0-auslösbare VM-Tests verwenden `CliOptionsParser --lang` aus T009
- **US4 (P3)**: Kann nach Phase 2 starten; nutzt ResourceManager-Injection aus T007, T008, T009

### Innerhalb jeder User Story

- `.resx`-Dateien (DE + EN) werden vor Quellcode-Refactoring angelegt
- Quellcode-Refactoring (Lexer/Parser/VM/CLI) bevor Tests geschrieben werden (damit Tests die realen Methoden aufrufen können)
- Tests laufen nach vollständiger Implementierung der Story

### Parallele Möglichkeiten

- Alle Setup-Tasks (T001–T006) können parallel laufen
- T007, T008, T009 (Foundational) können parallel laufen
- T010 + T011 (`.resx`-Dateien für US1) parallel
- T017 + T018 (`.resx`-Dateien für US2) parallel; T021 + T022 (Testdaten) parallel
- T026 + T027 (`.resx`-Dateien für US3) parallel; T029 (Testdaten) parallel
- T033 + T034 + T035 (Swedish `.se.resx`-Fixtures für US4) alle drei parallel

---

## Parallel Example: User Story 2

```bash
# Parallel: resx-Dateien anlegen
Task: T017 — src/Pl0.Core/Resources/Pl0CoreMessages.resx (DE)
Task: T018 — src/Pl0.Core/Resources/Pl0CoreMessages.en.resx (EN)

# Parallel: Testdaten anlegen (während resx-Dateien entstehen)
Task: T021 — tests/data/pl0/l10n/ Lexer-Testprogramme
Task: T022 — tests/data/pl0/l10n/ Parser-Testprogramme

# Nach Abschluss von T017/T018: Quellcode-Refactoring
Task: T019 — src/Pl0.Core/Pl0Lexer.cs
Task: T020 — src/Pl0.Core/Pl0Parser.cs
```

---

## Implementation Strategy

### MVP First (User Story 1 Only)

1. Phase 1 abschließen: Setup
2. Phase 2 abschließen: Foundational (CRITICAL)
3. Phase 3 abschließen: User Story 1 (CLI-Ausgabe auf EN/DE)
4. **STOP und VALIDIEREN**: `dotnet run --project src/Pl0.Cli -- --lang en --help`
5. Demo/Review der CLI-Lokalisierung

### Incremental Delivery

1. Setup + Foundational → Foundation bereit
2. US1 (CLI Help + Errors) → `--lang en --help` funktioniert → MVP Demo
3. US2 (Compiler-Diagnosen) → `--lang en run fehler.pl0` zeigt EN-Meldungen
4. US3 (VM-Laufzeitfehler) → `--lang en run divzero.pl0` zeigt EN-VM-Fehler
5. US4 (Erweiterbarkeit) → SC-004 Schwedisch-Test grün → FR-008 nachgewiesen

### Single-Developer Strategy

Mit einem Entwickler sequenziell:

1. Phase 1 + 2 (Setup + Foundation) — ~1h
2. Phase 3 (US1: CLI) — resxAnlegen + Refactoring + Tests
3. Phase 4 (US2: Compiler) — resx + Lexer/Parser-Refactoring + Tests
4. Phase 5 (US3: VM) — resx + VM-Refactoring + Tests
5. Phase 6 (US4: Extensibility) — Swedish Fixtures + Tests
6. Phase 7 (Polish: Smoke-Tests)

---

## Notes

- [P] = unterschiedliche Dateien, keine offenen Abhängigkeiten auf nicht abgeschlossene Tasks
- Alle `.resx`-Inhalte exakt gemäss normativer Vorgabe in `data-model.md §3/§4/§5` (Spalten DE und EN)
- VM-interne Tests (T031): P-Code via `new[] { new Instruction(Opcode.X, ...) }` direkt im Testcode — keine externen Fixture-Dateien
- PL/0-auslösbare VM-Tests (T030): via `CliOptionsParser --lang en` + `BufferedPl0Io` (nicht `ConsolePl0Io`)
- Alle Kommentare im Produktionscode auf Deutsch (Constitution Prinzip I)
- `dotnet test` nach jeder Phase ausführen; bei Regression sofort stoppen und beheben
- Keine neuen NuGet-Pakete; keine Architektur-Grenzenverletzungen; bestehende Exit-Codes stabil
