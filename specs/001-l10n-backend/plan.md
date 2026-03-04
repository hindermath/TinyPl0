# Implementation Plan: L10N Backend (Pl0.Core / Pl0.Vm / Pl0.Cli)

**Branch**: `001-l10n-backend` | **Date**: 2026-03-04 (aktualisiert) | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/001-l10n-backend/spec.md`

## Summary

Dieses Feature implementiert zwei aufeinander aufbauende Schichten gemäss
Lastenheft_L10N.md Glossar Abschnitt 4:

- **I18N (Internationalization)**: Technische Vorbereitung der Software für
  mehrsprachige Unterstützung — `.resx`-Infrastruktur, `ResourceManager`,
  BCP-47-Sprachcodes, Fallback-Kette, Erweiterbarkeit ohne Code-Änderungen.
- **L10N (Localization)**: Konkrete Anpassung für Deutsch (Standard) und
  Englisch (erste Zielsprache) — Übersetzung aller ~50 Diagnosetexte und
  CLI-Meldungen.

Sprachübergabe: explizites `Language`-Feld in `CompilerOptions` und
`VirtualMachineOptions`; Auswahl über `--lang <code>` in `Pl0.Cli`.
Technischer Mechanismus: Standard-.NET-`.resx`-Ressourcendateien mit
strongly-typed Designer-Klassen und `ResourceManager` (kein NuGet-Paket).

## Technical Context

**Language/Version**: C# 14 / .NET 10
**Primary Dependencies**: .NET SDK (`.resx`, `ResourceManager`, `CultureInfo`) — kein zusätzliches NuGet-Paket
**Storage**: Satellite Assemblies (automatisch vom SDK aus `.resx` generiert)
**Testing**: xUnit; bestehende Tests unverändert; neue `L10nTests`-Klasse
**Target Platform**: Windows, macOS, Linux (Konsole/Terminal)
**Project Type**: Compiler + CLI (multi-module)
**Performance Goals**: N/A (String-Lookup-Overhead vernachlässigbar)
**Constraints**: Keine neuen NuGet-Pakete; keine Architektur-Grenzenverletzungen; alle bestehenden Exit-Codes stabil
**Scale/Scope**: ~50 lokalisierbare Strings; 3 Module; 2 Sprachen initial

## I18N-Betrachtung (Lastenheft_L10N.md §4, Punkt 2)

Das Lastenheft definiert I18N als „Vorbereitung der Software, um mehrere Sprachen
technisch zu unterstützen." Folgende I18N-Aspekte wurden geprüft:

| I18N-Aspekt | Relevant? | Behandlung im Plan |
|-------------|-----------|-------------------|
| String-Trennung von Logik | ✅ Ja | `.resx`-Ressourcendateien je Modul |
| BCP-47-Locale-Codes | ✅ Ja | `string Language = "de"` in Options-Records |
| Fallback-Kette | ✅ Ja | SDK-eingebaut: `en-US` → `en` → Invariant (DE) |
| Erweiterbarkeit neue Sprachen | ✅ Ja | Neue `.resx`-Datei ohne Code-Änderung (FR-008) |
| UTF-8-Kodierung | ✅ Ja | `.resx` ist immer UTF-8 im .NET SDK |
| Locale-abhängige Zahlenformatierung | ❌ N/A | PL/0 kennt nur Integer; `int.Parse` ohne Dezimaltrennzeichen |
| Pluralformen | ❌ N/A | Fehlermeldungen ohne Plural-Logik |
| RTL-Textrichtung | ❌ N/A | Terminal-UI, nicht grafisch |
| Datumsformate | ❌ N/A | Compiler hat keine Datums-Ausgaben |
| Zeichensatz / Kollation | ❌ N/A | PL/0-Bezeichner sind ASCII-only |

**Ergebnis**: Die I18N-Infrastruktur ist vollständig durch die gewählte `.resx`-Architektur
abgedeckt. Keine zusätzlichen Anpassungen notwendig. Der Plan adressiert damit
sowohl die I18N-Schicht (Infrastruktur) als auch die L10N-Schicht (DE + EN).

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Prinzip | Status | Begründung |
|---------|--------|-----------|
| I. Didaktische Klarheit | ✅ Pass | `.resx` + Designer-Klassen sind Standard .NET; Ressourcen-Keys mit Fehlercode im Namen fördern Verständlichkeit |
| II. Historische Kompatibilität | ✅ Pass | Keine PL/0-Sprachänderungen; Fehlercodes unveränderlich; nur Texte werden lokalisiert |
| III. Testgetriebene Qualität | ✅ Pass | Bestehende Tests prüfen keine Diagnosetexte → keine Regressionen (zu validieren, spec.md Assumptions); neue `L10nTests`-Klasse deckt alle ~50 Keys einzeln ab, via `--lang`-Parameter, mit `BufferedPl0Io` für VM-Tests |
| IV. Strikte Modularchitektur | ✅ Pass | Keine neuen NuGet-Pakete; Dependency-Regeln unverändert; `.resx` ist SDK-Feature ohne externe Deps |
| V. Fehlerdiagnose statt Ausnahmen | ✅ Pass | `CompilerDiagnostic`/`LexerDiagnostic`/`VmDiagnostic` Strukturen unverändert; nur Message-Strings werden lokalisiert |
| VI. Git-Workflow | ✅ Pass | Feature-Branch `001-l10n-backend` existiert; alle Änderungen über PR |

**Gate-Ergebnis: PASS**

## Project Structure

### Documentation (this feature)

```text
specs/001-l10n-backend/
├── plan.md              # Dieses Dokument
├── research.md          # Phase 0 output — Entscheidungen und Alternativen
├── data-model.md        # Phase 1 output — Entitäten und Ressourcen-Keys
├── quickstart.md        # Phase 1 output — Smoke-Tests nach Implementierung
├── contracts/
│   └── cli-contract.md  # --lang Parameter-Spezifikation + API-Verträge
└── tasks.md             # Phase 2 output (wird von /speckit.tasks erstellt)
```

### Source Code (Änderungen)

```text
src/Pl0.Core/
├── CompilerOptions.cs               ÄNDERN  — Language-Feld hinzufügen
├── Resources/
│   ├── Pl0CoreMessages.resx         NEU     — Deutsche Strings (Invariant/Standard)
│   ├── Pl0CoreMessages.en.resx      NEU     — Englische Strings
│   └── Pl0CoreMessages.Designer.cs  NEU     — Autogeneriert vom SDK
├── Pl0Lexer.cs                      ÄNDERN  — Strings → Pl0CoreMessages[Language]
└── Pl0Parser.cs                     ÄNDERN  — Strings → Pl0CoreMessages[Language]

src/Pl0.Vm/
├── VirtualMachineOptions.cs         ÄNDERN  — Language-Feld hinzufügen
├── Resources/
│   ├── Pl0VmMessages.resx           NEU     — Deutsche Strings
│   ├── Pl0VmMessages.en.resx        NEU     — Englische Strings
│   └── Pl0VmMessages.Designer.cs    NEU     — Autogeneriert vom SDK
└── VirtualMachine.cs                ÄNDERN  — Strings → Pl0VmMessages[Language]

src/Pl0.Cli/
├── Cli/CliOptionsParser.cs          ÄNDERN  — --lang parsen; Strings → Pl0CliMessages
├── Cli/CliHelpPrinter.cs            ÄNDERN  — Strings → Pl0CliMessages
├── Program.cs                       ÄNDERN  — Language an CompilerOptions/VmOptions übergeben
├── Resources/
│   ├── Pl0CliMessages.resx          NEU     — Deutsche Strings
│   ├── Pl0CliMessages.en.resx       NEU     — Englische Strings
│   └── Pl0CliMessages.Designer.cs   NEU     — Autogeneriert vom SDK

tests/Pl0.Tests/
└── L10nTests.cs                     NEU     — Dedizierte L10N/I18N-Testklasse (alle ~50 Keys)

tests/data/pl0/l10n/                 NEU     — Testdaten-Programme für L10nTests
├── undeclared_ident.pl0             NEU     — Parser_E11_UndeclaredIdent auslösen
├── assign_to_const.pl0              NEU     — Parser_E12_AssignToConst auslösen
├── division_by_zero.pl0             NEU     — Vm_E206_DivisionByZero auslösen
├── number_too_large.pl0             NEU     — Lexer_E30_NumberTooLarge auslösen
└── ...                              NEU     — je 1 Programm pro Fehlercode

src/Pl0.Core/Resources/
└── Pl0CoreMessages.se.resx          NEU     — Schwedische Dummy-Texte (Erweiterbarkeitstest SC-004)

src/Pl0.Vm/Resources/
└── Pl0VmMessages.se.resx            NEU     — Schwedische Dummy-Texte (Erweiterbarkeitstest SC-004)

src/Pl0.Cli/Resources/
└── Pl0CliMessages.se.resx           NEU     — Schwedische Dummy-Texte (Erweiterbarkeitstest SC-004)
```

**Structure Decision**: Single-project layout je Modul; Ressourcen unter `Resources/`
-Unterverzeichnis; autogenerierte Designer-Klassen via MSBuild-Integration.

## Phase 0: Research

**Status**: ✅ Abgeschlossen — siehe [research.md](research.md)

### Kernentscheidungen

1. **Mechanismus**: `.resx` + `ResourceManager` + strongly-typed Designer-Klassen
   (Standard .NET SDK, kein NuGet-Paket)
2. **Sprachübergabe**: Explizites `Language`-Feld in `CompilerOptions` und
   `VirtualMachineOptions`
3. **Key-Schema**: `<Modul>_E<Code>_<KurzerName>` (z. B. `Parser_E11_UndeclaredIdent`)
4. **Fallback**: SDK-eingebaut (`en-US` → `en` → Invariant = DE)
5. **Ungültiger Code**: `CultureInfo.GetCultureInfo()` Exception → Warnung `stderr` → Fallback DE

## Phase 1: Design & Contracts

**Status**: ✅ Abgeschlossen

Artefakte:
- [data-model.md](data-model.md) — alle Ressourcen-Keys, Entitäten, Beziehungen, **Terminologietabelle §7**
- [contracts/cli-contract.md](contracts/cli-contract.md) — `--lang`-Parameter-Spec, API-Verträge
- [quickstart.md](quickstart.md) — Smoke-Tests nach Implementierung

**Abhängigkeit für Translation-Review**: `data-model.md §7` (Terminologietabelle) ist normativer
Standard für alle englischen Texte. Sie muss vor Implementierung der `.resx`-Inhalte
vollständig sein. PR-Reviewer prüft alle englischen Keys gegen diese Tabelle.

### Implementierungsdetails pro Modul

#### Pl0.Core

1. `CompilerOptions.cs`: Neues optionales `string Language = "de"` am Ende der
   Parameterliste (record primary constructor — nicht-breaking).

2. `Resources/Pl0CoreMessages.resx` anlegen:
   - Build-Action: `EmbeddedResource`; Custom Tool: `ResXFileCodeGenerator`
   - Namespace: `Pl0.Core`; Klasse: `Pl0CoreMessages`; Access: `internal`
   - Alle ~30 Lexer- und Parser-Fehlertexte auf Deutsch

3. `Resources/Pl0CoreMessages.en.resx` anlegen:
   - Gleiche Keys, englische Werte

4. `Pl0Lexer.cs` und `Pl0Parser.cs`:
   - `CultureInfo culture = CultureInfo.GetCultureInfo(_options.Language)` ableiten
   - Alle String-Literals ersetzen durch `Pl0CoreMessages.ResourceManager.GetString("Key", culture)`
   - Formatierte Strings: `string.Format(culture, template, args)`

#### Pl0.Vm

1. `VirtualMachineOptions.cs`: Analog — `string Language = "de"`.
2. `Resources/Pl0VmMessages.resx` + `.en.resx` — 13 VM-Fehlertexte.
3. `VirtualMachine.cs`: `_culture` im Konstruktor speichern; alle Fehlertexte
   über `Pl0VmMessages.ResourceManager.GetString(key, _culture)`.

#### Pl0.Cli

1. `CliOptionsParser.cs`: `--lang <code>` parsen; ungültiger Code → Warnung `stderr`
   + Fallback `"de"`; eigene Parse-Fehlertexte über `Pl0CliMessages`.
2. `CliHelpPrinter.cs`: Hilfe-Texte via `Pl0CliMessages` + `CultureInfo`.
3. `Program.cs`: Language aus Parse-Ergebnis → `CompilerOptions` + `VirtualMachineOptions`.

#### Pl0.Tests — neue `L10nTests.cs`

**Test-Infrastruktur-Anforderungen** (aus spec.md Assumptions + SC-002):
- Alle Tests verwenden den **`--lang`-Parameter** via `CliOptionsParser` — nicht direkt `CompilerOptions.Language`
- VM-Fehlertexte werden über **`BufferedPl0Io`** (simuliertes I/O) geprüft, da `ConsolePl0Io` nicht fangbar ist
- `stderr`-Ausgaben (Fallback-Warnung) werden über die `IPl0Io`-Schnittstelle gefangen — als Anforderung, nicht Implementierungsdetail
- Testdaten-`.pl0`-Programme liegen in `tests/data/pl0/l10n/`

**Lexer-Keys** (5 Testmethoden — alle Keys einzeln):
- `Lexer_NumberTooManyDigits_En_ContainsEnglishMessage()`
- `Lexer_NumberTooLarge_En_ContainsEnglishMessage()`
- `Lexer_IdentifierTooLong_En_ContainsEnglishMessage()`
- `Lexer_UnexpectedColon_En_ContainsEnglishMessage()`
- `Lexer_UnexpectedChar_En_ContainsEnglishMessage()`

**Parser-Keys** (~27 Testmethoden — alle Keys einzeln):
- `Parser_UseEqualNotAssign_En_ContainsEnglishMessage()`
- `Parser_PeriodExpected_En_ContainsEnglishMessage()`
- `Parser_UndeclaredIdent_En_ContainsEnglishMessage()`
- `Parser_AssignToConst_En_ContainsEnglishMessage()`
- `Parser_InputTargetMustBeVar_En_ContainsEnglishMessage()`
- `Parser_CallNeedsIdent_En_ContainsEnglishMessage()`
- `Parser_CallConstOrVar_En_ContainsEnglishMessage()`
- `Parser_ThenExpected_En_ContainsEnglishMessage()`
- `Parser_SemiOrEndExpected_En_ContainsEnglishMessage()`
- `Parser_DoExpected_En_ContainsEnglishMessage()`
- `Parser_InputNotInClassic_En_ContainsEnglishMessage()`
- `Parser_OutputNotInClassic_En_ContainsEnglishMessage()`
- `Parser_RelOpExpected_En_ContainsEnglishMessage()`
- `Parser_ProcInExpr_En_ContainsEnglishMessage()`
- `Parser_RightParenMissing_En_ContainsEnglishMessage()`
- `Parser_BadExprStart_En_ContainsEnglishMessage()`
- `Parser_NumberTooLarge_En_ContainsEnglishMessage()`
- `Parser_NestingTooDeep_En_ContainsEnglishMessage()`
- `Parser_SymbolTableOverflow_En_ContainsEnglishMessage()`
- `Parser_ProgramTooLong_En_ContainsEnglishMessage()`
- `Parser_UnexpectedEndOfInput_En_ContainsEnglishMessage()`
- `Parser_InvalidLexLevel_En_ContainsEnglishMessage()`
- *(weitere Expect-Methoden je verbleibendem Key aus data-model.md §3)*

**VM-Keys** (13 Testmethoden — alle Keys einzeln, via `BufferedPl0Io`):
- `Vm_IPOutOfRange_En_ContainsEnglishMessage()`
- `Vm_InvalidLodIndex_En_ContainsEnglishMessage()`
- `Vm_InvalidStoIndex_En_ContainsEnglishMessage()`
- `Vm_StackOverflowCallFrame_En_ContainsEnglishMessage()`
- `Vm_StackOverflowInt_En_ContainsEnglishMessage()`
- `Vm_UnsupportedOpcode_En_ContainsEnglishMessage()`
- `Vm_UnsupportedOpr_En_ContainsEnglishMessage()`
- `Vm_InvalidBasePointer_En_ContainsEnglishMessage()`
- `Vm_StackOverflow_En_ContainsEnglishMessage()`
- `Vm_StackUnderflow_En_ContainsEnglishMessage()`
- `Vm_EndOfInput_En_ContainsEnglishMessage()`
- `Vm_InputFormatError_En_ContainsEnglishMessage()`
- `Vm_DivisionByZero_En_ContainsEnglishMessage()`

**CLI-Keys** (~9 Testmethoden):
- `Cli_Help_En_ContainsEnglishText()`
- `Cli_Help_Contains_LangParameter()`
- `Cli_Status_CompileSuccess_En_ContainsEnglishMessage()`
- `Cli_Status_CompileError_En_ContainsEnglishMessage()`
- `Cli_Err_UnknownSwitch_En_ContainsEnglishMessage()`
- `Cli_Err_UnknownLanguage_En_ContainsEnglishMessage()`
- *(weitere Cli_Err_*-Methoden je Key aus data-model.md §5)*

**Deutsch-Baseline** (Stichprobe — stellt sicher dass DE ohne `--lang` funktioniert):
- `CompilerDiagnostic_De_ContainsGermanMessage()`
- `VmDiagnostic_De_ContainsGermanMessage()`

**Fallback-Ketten-Tests** (konkrete I/O-Paare aus spec.md FR-006/FR-009):
- `LangEnUs_FallsBackToEn_WhenEnUsResxMissing()` — `--lang en-US` → EN-Text erscheint
- `LangFr_FallsBackToGerman_WhenFrResxMissing()` — `--lang fr` → DE-Fallback-Text erscheint
- `EmptyLanguage_FallsBackToGerman()` — `--lang ""` → DE
- `UnknownLanguage_FallsBackToGerman_WithStderrWarning()` — `--lang xx` → DE + `stderr`-Warnung enthält `"xx"`

**Erweiterbarkeitstest SC-004** (automatisiert, Dummy-Sprache Schwedisch):
- `NewLocale_Se_LoadsFromResxWithoutCodeChange_Core()` — schwedischer Core-Key erscheint mit `--lang se`
- `NewLocale_Se_LoadsFromResxWithoutCodeChange_Vm()` — schwedischer Vm-Key erscheint mit `--lang se`
- `NewLocale_Se_LoadsFromResxWithoutCodeChange_Cli()` — schwedischer Cli-Key erscheint mit `--lang se`

### Constitution Check (Post-Design): alle 6 Prinzipien ✅

## Complexity Tracking

*Keine Constitution-Violations — Abschnitt entfällt.*
