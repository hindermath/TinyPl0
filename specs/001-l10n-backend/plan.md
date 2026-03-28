# Implementation Plan: L10N Backend (Pl0.Core / Pl0.Vm / Pl0.Cli)

**Branch**: `001-l10n-backend` | **Date**: 2026-03-04 (aktualisiert) | **Spec**: [spec.md](spec.md)
**Input**: Feature specification from `/specs/001-l10n-backend/spec.md`

## Summary

Dieses Feature implementiert zwei aufeinander aufbauende Schichten gemäss
Lastenheft_L10N.001-l10n-backend.md Glossar Abschnitt 4:

- **I18N (Internationalization)**: Technische Vorbereitung der Software für
  mehrsprachige Unterstützung — `.resx`-Infrastruktur, `ResourceManager`,
  BCP-47-Sprachcodes, Fallback-Kette, Erweiterbarkeit ohne Code-Änderungen.
- **L10N (Localization)**: Konkrete Anpassung für Deutsch (Standard) und
  Englisch (erste Zielsprache) — Übersetzung aller ~75 Diagnosetexte und
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
**Scale/Scope**: ~75 lokalisierbare Strings; 3 Module; 2 Sprachen initial

## I18N-Betrachtung (Lastenheft_L10N.001-l10n-backend.md §4, Punkt 2)

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
| III. Testgetriebene Qualität | ✅ Pass | Bestehende Tests prüfen keine Diagnosetexte → keine Regressionen (validiert 2026-03-04, spec.md Assumptions); neue `L10nTests`-Klasse deckt alle ~75 Keys einzeln ab, via `--lang`-Parameter, mit `BufferedPl0Io` für VM-Tests |
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
└── L10nTests.cs                     NEU     — Dedizierte L10N/I18N-Testklasse (alle ~75 Keys)

tests/data/pl0/l10n/                 NEU     — Testdaten-Programme für L10nTests
├── undeclared_ident.pl0             NEU     — Parser_E11_UndeclaredIdent auslösen
├── assign_to_const.pl0              NEU     — Parser_E12_AssignToConst auslösen
├── division_by_zero.pl0             NEU     — Vm_E206_DivisionByZero auslösen
├── number_too_large.pl0             NEU     — Lexer_E30_NumberTooLarge auslösen
└── ...                              NEU     — je 1 Programm pro Fehlercode

tests/Pl0.Tests/Resources/           NEU     — Test-Fixtures für Erweiterbarkeitstest SC-004
├── Pl0CoreMessages.se.resx          NEU     — Schwedische Dummy-Texte (Core, nicht in Produktions-Build)
├── Pl0VmMessages.se.resx            NEU     — Schwedische Dummy-Texte (Vm, nicht in Produktions-Build)
└── Pl0CliMessages.se.resx           NEU     — Schwedische Dummy-Texte (Cli, nicht in Produktions-Build)
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

1. `CompilerOptions.cs`: Zwei neue optionale Parameter am Ende des record primary constructors
   (nicht-breaking):
   - `string Language = "de"` — BCP-47-Sprachcode
   - `ResourceManager? Messages = null` — Default: `Pl0CoreMessages.ResourceManager` (FR-008)

2. `Resources/Pl0CoreMessages.resx` anlegen:
   - Build-Action: `EmbeddedResource`; Custom Tool: `ResXFileCodeGenerator`
   - Namespace: `Pl0.Core`; Klasse: `Pl0CoreMessages`; Access: `internal`
   - Alle ~37 Lexer- und Parser-Fehlertexte auf Deutsch, **exakt gemäss**
     `data-model.md §3` (normative Vorgabe, spec.md Clarifications Runde 7, Q5)

3. `Resources/Pl0CoreMessages.en.resx` anlegen:
   - Gleiche Keys; Werte aus bestehenden Inline-Strings in `Pl0Lexer.cs`/`Pl0Parser.cs`
     übernehmen und an NFR-002 (Sentence case: PL/0-Keywords grossschreiben, z. B.
     `"call must..."` → `"CALL must..."`) sowie Terminologietabelle §7 anpassen
     (spec.md Clarifications Runde 7, Q4)

4. `Pl0Lexer.cs` und `Pl0Parser.cs`:
   - `ResourceManager rm = _options.Messages ?? Pl0CoreMessages.ResourceManager`
   - `CultureInfo culture = CultureInfo.GetCultureInfo(_options.Language)`
   - Alle String-Literals ersetzen durch `rm.GetString("Key", culture)`
   - Formatierte Strings: `string.Format(culture, template, args)`

#### Pl0.Vm

1. `VirtualMachineOptions.cs`: Analog — `string Language = "de"` und
   `ResourceManager? Messages = null` (Default: `Pl0VmMessages.ResourceManager`).
2. `Resources/Pl0VmMessages.resx` + `.en.resx` — 13 VM-Fehlertexte.
3. `VirtualMachine.cs`: `_rm` und `_culture` im Konstruktor aus Options ableiten;
   alle Fehlertexte über `_rm.GetString(key, _culture)`.

#### Pl0.Cli

1. `CliOptionsParser.cs`:
   - Optionaler Konstruktor-Parameter `TextWriter errorOutput = null` (Default: `Console.Error`) — FR-009
   - Optionaler Konstruktor-Parameter `ResourceManager? cliMessages = null` (Default: `Pl0CliMessages.ResourceManager`) — FR-008
   - `--lang <code>` parsen; ungültiger Code → `errorOutput.WriteLine(warnung)` + Fallback `"de"`
   - Eigene Parse-Fehlertexte über injiziertem `cliMessages`-ResourceManager
2. `CliHelpPrinter.cs`: Hilfe-Texte via injiziertem ResourceManager + `CultureInfo`.
3. `Program.cs`: Language aus Parse-Ergebnis → `CompilerOptions` + `VirtualMachineOptions`
   (beide mit Default-`Messages = null`).

#### Pl0.Tests — neue `L10nTests.cs`

**Test-Infrastruktur-Anforderungen** (aus spec.md Assumptions + SC-002):
- Alle Tests für Lexer-, Parser- und PL/0-ausloesbare VM-Keys verwenden den **`--lang`-Parameter** via `CliOptionsParser` — nicht direkt `CompilerOptions.Language`
- VM-Fehlertexte, die durch PL/0-Programme ausloesbar sind, werden über **`BufferedPl0Io`** (simuliertes I/O) geprüft, da `ConsolePl0Io` nicht fangbar ist
- VM-interne Fehler (nur durch ungültige P-Code-Sequenzen ausloesbar: `IPOutOfRange`, `UnsupportedOpcode`, `InvalidLodIndex`, `InvalidStoIndex`, `StackOverflowCallFrame`, `StackOverflowInt`, `UnsupportedOpr`, `InvalidBasePointer`, `StackOverflow`, `StackUnderflow`) werden direkt via `VirtualMachine` mit `VirtualMachineOptions { Language = "en" }` getestet — kein CLI-Pfad (spec.md Clarifications Runde 6)
- `stderr`-Ausgaben (Fallback-Warnung) werden über einen `StringWriter` gefangen, der an `CliOptionsParser(errorOutput: writer)` übergeben wird — `IPl0Io` bleibt unverändert
- Testdaten-`.pl0`-Programme liegen in `tests/data/pl0/l10n/`

**Lexer-Keys** (5 Testmethoden — alle Keys einzeln):
- `Lexer_NumberTooManyDigits_En_ContainsEnglishMessage()`
- `Lexer_NumberTooLarge_En_ContainsEnglishMessage()`
- `Lexer_IdentifierTooLong_En_ContainsEnglishMessage()`
- `Lexer_UnexpectedColon_En_ContainsEnglishMessage()`
- `Lexer_UnexpectedChar_En_ContainsEnglishMessage()`

**Parser-Keys** (32 Testmethoden — alle Keys einzeln, vollständig aus Quellcode enumeriert):
- `Parser_UseEqualNotAssign_En_ContainsEnglishMessage()`
- `Parser_NumberAfterEquals_En_ContainsEnglishMessage()`
- `Parser_EqualAfterIdent_En_ContainsEnglishMessage()`
- `Parser_IdentAfterConst_En_ContainsEnglishMessage()`
- `Parser_IdentAfterVar_En_ContainsEnglishMessage()`
- `Parser_IdentAfterProc_En_ContainsEnglishMessage()`
- `Parser_IdentAfterInput_En_ContainsEnglishMessage()`
- `Parser_SemiOrComma_En_ContainsEnglishMessage()`
- `Parser_PeriodExpected_En_ContainsEnglishMessage()`
- `Parser_UndeclaredIdent_En_ContainsEnglishMessage()`
- `Parser_AssignToConst_En_ContainsEnglishMessage()`
- `Parser_InputTargetMustBeVar_En_ContainsEnglishMessage()`
- `Parser_AssignOpExpected_En_ContainsEnglishMessage()`
- `Parser_CallNeedsIdent_En_ContainsEnglishMessage()`
- `Parser_CallConstOrVar_En_ContainsEnglishMessage()`
- `Parser_ThenExpected_En_ContainsEnglishMessage()`
- `Parser_SemiOrEndExpected_En_ContainsEnglishMessage()`
- `Parser_DoExpected_En_ContainsEnglishMessage()`
- `Parser_IncorrectSymbol_En_ContainsEnglishMessage()`
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
- `Parser_UnexpectedTermination_En_ContainsEnglishMessage()`

**VM-Keys via `CliOptionsParser` + `BufferedPl0Io`** (PL/0-ausloesbar, 3 Testmethoden):
- `Vm_DivisionByZero_En_ContainsEnglishMessage()` — `division_by_zero.pl0`
- `Vm_EndOfInput_En_ContainsEnglishMessage()` — `end_of_input.pl0` (Leseoperation ohne Input)
- `Vm_InputFormatError_En_ContainsEnglishMessage()` — `input_format_error.pl0` (nicht-ganzzahliger Input)

**VM-Keys via direkter `VirtualMachine`-Konstruktion** (nur durch ungültige P-Code ausloesbar, 10 Testmethoden):
  P-Code wird als `Instruction[]` direkt im Testcode konstruiert — keine externen Fixture-Dateien
  (spec.md Clarifications Runde 6, Q2).
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

**CLI-Keys — Err/Status** (8 Testmethoden — alle Keys einzeln):
- `Cli_Err_UnexpectedPositional_En_ContainsEnglishMessage()`
- `Cli_Err_MissingValueForOut_En_ContainsEnglishMessage()`
- `Cli_Err_NoEmitMode_En_ContainsEnglishMessage()`
- `Cli_Err_UnknownSwitch_En_ContainsEnglishMessage()`
- `Cli_Err_ConflictingEmitModes_En_ContainsEnglishMessage()`
- `Cli_Err_UnknownLanguage_En_ContainsEnglishMessage()`
- `Cli_Status_CompileSuccess_En_ContainsEnglishMessage()`
- `Cli_Status_CompileError_En_ContainsEnglishMessage()`

**CLI-Keys — Help** (17 Testmethoden — alle Keys einzeln, via `--lang en --help`):
- `Cli_Help_UsageHeader_En_ContainsEnglishText()`
- `Cli_Help_HelpLine_En_ContainsEnglishText()`
- `Cli_Help_CompileLine_En_ContainsEnglishText()`
- `Cli_Help_RunLine_En_ContainsEnglishText()`
- `Cli_Help_RunPcodeLine_En_ContainsEnglishText()`
- `Cli_Help_RunPcodeDirectLine_En_ContainsEnglishText()`
- `Cli_Help_ApiLine_En_ContainsEnglishText()`
- `Cli_Help_LegacyLine_En_ContainsEnglishText()`
- `Cli_Help_SwitchesHeader_En_ContainsEnglishText()`
- `Cli_Help_SwitchErrmsg_En_ContainsEnglishText()`
- `Cli_Help_SwitchWopcod_En_ContainsEnglishText()`
- `Cli_Help_SwitchListCode_En_ContainsEnglishText()`
- `Cli_Help_SwitchApi_En_ContainsEnglishText()`
- `Cli_Help_SwitchConly_En_ContainsEnglishText()`
- `Cli_Help_SwitchEmitFormat_En_ContainsEnglishText()`
- `Cli_Help_SwitchEmit_En_ContainsEnglishText()`
- `Cli_Help_SwitchOut_En_ContainsEnglishText()`
- `Cli_Help_SwitchLang_En_ContainsEnglishText()` — SC-005: --lang in help output

**Deutsch-Baseline** (Stichprobe — stellt sicher dass DE ohne `--lang` funktioniert):
- `CompilerDiagnostic_De_ContainsGermanMessage()`
- `VmDiagnostic_De_ContainsGermanMessage()`

**Fallback-Ketten-Tests** (konkrete I/O-Paare aus spec.md FR-006/FR-009):
- `LangEnUs_FallsBackToEn_WhenEnUsResxMissing()` — `--lang en-US` → EN-Text erscheint
- `LangFr_FallsBackToGerman_WhenFrResxMissing()` — `--lang fr` → DE-Fallback-Text erscheint
- `EmptyLanguage_FallsBackToGerman()` — `--lang ""` → DE
- `UnknownLanguage_FallsBackToGerman_WithStderrWarning()` — `--lang xx` → DE + `stderr`-Warnung enthält `"xx"`

**Erweiterbarkeitstest SC-004** (automatisiert, Dummy-Sprache Schwedisch, via ResourceManager-Injection):
- `NewLocale_Se_InjectedResourceManager_Core_ReturnsSwedishMessage()` — Swedish `ResourceManager` (EmbeddedResource aus `tests/Pl0.Tests/Resources/Pl0CoreMessages.se.resx`) via `CompilerOptions.Messages` injiziert; Core-Key auf Schwedisch prüfen
- `NewLocale_Se_InjectedResourceManager_Vm_ReturnsSwedishMessage()` — analog via `VirtualMachineOptions.Messages`
- `NewLocale_Se_InjectedResourceManager_Cli_ReturnsSwedishMessage()` — analog via `CliOptionsParser(cliMessages: swResourceManager)`

### Constitution Check (Post-Design): alle 6 Prinzipien ✅

## Complexity Tracking

*Keine Constitution-Violations — Abschnitt entfällt.*
