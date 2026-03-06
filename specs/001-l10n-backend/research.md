# Research: L10N Backend (001-l10n-backend)

## I18N vs. L10N — Abgrenzung (Lastenheft §4)

Das Lastenheft definiert im Glossar zwei Begriffe:
- **L10N**: Anpassung für einen spezifischen Zielmarkt (DE + EN)
- **I18N**: Technische Vorbereitung für mehrsprachige Unterstützung (Infrastruktur)

Dieser Plan implementiert **beide Schichten**: Die `.resx`-Architektur ist die
I18N-Schicht; die deutschen und englischen Übersetzungen sind die L10N-Schicht.

Für einen Integer-only-Compiler entfallen typische I18N-Komplexitäten:
keine locale-abhängige Zahlenformatierung, keine Pluralformen, kein RTL, keine
Datumsausgaben, keine locale-sensitive Zeichensortierung (PL/0-Bezeichner ASCII-only).

## Findings

### Decision 1: Lokalisierungsmechanismus — `.resx` + `ResourceManager`

**Decision**: Standardmechanismus des .NET SDK: `.resx`-Ressourcendateien mit
autogenerierten strongly-typed Designer-Klassen (`ResXFileCodeGenerator`) und
`ResourceManager` zur Laufzeitauflösung.

**Rationale**:
- Entspricht exakt Lastenheft 3.3 („Standard-Mechanismen des .NET Core Frameworks").
- Keine zusätzlichen NuGet-Pakete erforderlich (Architecture Guard bleibt grün).
- Satellite-Assembly-Modell des .NET SDK ermöglicht neue Sprachen durch Hinzufügen
  einer einzigen `.resx`-Datei pro Modul — ohne Quellcodeänderung (FR-008).
- Fallback-Kette `en-US` → `en` → invariant (= Deutsch) ist im SDK eingebaut
  und erfüllt FR-006 ohne eigenen Code.
- Strongly-typed Designer-Klassen vermeiden Tippfehler in Ressourcen-Keys (Didaktik).

**Alternatives considered**:
- *JSON-Ressourcen + eigener Loader*: Kein Mehrwert gegenüber `.resx`; nicht
  der .NET-Standard; erfordert zusätzlichen Code.
- *Ambient CultureInfo (Thread.CurrentCulture)*: Globaler Zustand; in Tests
  schwer zu isolieren; in der Klärungsrunde explizit abgelehnt (Q1 → Option B).
- *IStringLocalizer (Microsoft.Extensions.Localization)*: Nur im
  ASP.NET-Kontext sinnvoll; erfordert DI-Container; über-engineered für ein
  CLI/Compiler-Projekt.

---

### Decision 2: Sprachübergabe — Explizites Feld in Options-Records

**Decision**: `CompilerOptions` erhält ein neues Feld `string Language = "de"`;
`VirtualMachineOptions` erhält ein neues Feld `string Language = "de"`.
`Pl0.Cli` leitet den geparsten BCP-47-Code an beide weiter.

**Rationale**:
- `CompilerOptions` ist bereits ein `sealed record`; ein zusätzliches Property
  mit Defaultwert ist eine nicht-breaking Änderung (bestehende `new(Pl0Dialect.Extended)`-
  Aufrufe bleiben gültig, da der Parameter optional ist).
- Kein globaler Zustand; jeder Kompilierlauf ist unabhängig testbar.
- Konsistent mit bestehenden Optionsklassen des Projekts.

**Alternatives considered**:
- *CultureInfo statt string*: Würde eine Abhängigkeit von `System.Globalization`
  in den öffentlichen Typ einführen; BCP-47-String ist minimaler und flexibler.
- *Separate `LocalizationOptions`-Klasse*: Unnötige Abstraktion für nur ein Feld.

---

### Decision 3: Ressourcen-Key-Schema

**Decision**: Stabile, präfixierte Keys nach Muster `<Modul>_<Kontext>_<Fehlercode>`:
- Lexer: `Lexer_E30_NumberTooManyDigits`, `Lexer_E33_IdentifierTooLong`, …
- Parser: `Parser_E01_UseEqualNotAssign`, `Parser_E09_PeriodExpected`, …
- VM: `Vm_E206_DivisionByZero`, `Vm_E99_StackOverflow`, …
- CLI: `Cli_Help_Usage`, `Cli_Err_UnknownSwitch`, …

Formatierungsplatzhalter bleiben als `{0}`, `{1}` (string.Format-Stil), da
strongly-typed Designer-Klassen `string.Format` intern aufrufen.

**Rationale**: Fehlercode im Key ermöglicht schnelle Zuordnung Diagnose → Text
(didaktisch wertvoll). Stabile Schlüssel erfüllen FR-007 (Codes unveränderlich).

---

### Decision 4: Ressourcen-Dateistruktur

```
src/Pl0.Core/Resources/
    Pl0CoreMessages.resx        # Deutsch (Standardsprache / Invariant)
    Pl0CoreMessages.en.resx     # Englisch
src/Pl0.Vm/Resources/
    Pl0VmMessages.resx
    Pl0VmMessages.en.resx
src/Pl0.Cli/Resources/
    Pl0CliMessages.resx
    Pl0CliMessages.en.resx
```

SDK generiert `Pl0CoreMessages.Designer.cs` etc. automatisch bei Build.

---

### Decision 5: Fallback-Mechanismus

.NETResourceManager-Fallback out-of-the-box:
1. Exakter Code `de-DE` → sucht Satellite Assembly `de-DE`
2. → fallback auf `de`
3. → fallback auf Invariant (= Hauptassembly, Deutsch)

Für `--lang xx` (unbekannter Code): `CultureInfo.GetCultureInfo(code)` kann
eine `CultureNotFoundException` werfen → in `Pl0.Cli` abgefangen → Warnung
auf `stderr` + Fallback auf `CultureInfo.GetCultureInfo("de")`.

---

### Decision 6: Scope der lokalisierten Strings

**In Scope** (~50 Strings):
- Pl0Lexer: 5 Diagnosetexte
- Pl0Parser: ~25 Diagnosetexte
- VirtualMachine: ~13 Laufzeit-Fehlertexte
- CliOptionsParser: 5 Parse-Fehlertexte
- CliHelpPrinter: Hilfe-Text (Usage, Befehlsbeschreibungen)
- Program.cs (Pl0.Cli): Statusmeldungen (Kompilierung erfolgreich etc.)

**Out of Scope** (sprachneutral):
- P-Code-Listings (`--list-code`, `--emit asm`, `--wopcod`)
- Docs-Server (`--api`)
- PL/0-Schlüsselwörter, Token-Namen
- Kommentare im Quellcode (Deutsch, gemäss Constitution)

---

### Bestandsaufnahme: Bestehende Tests

- `ParserDiagnosticsTests` und `VirtualMachineTests` prüfen Fehler-Codes,
  **nicht** Diagnosetexte → keine Änderungen notwendig (SC-001 erfüllbar).
- `CliOptionsParserTests` prüft `CliDiagnostic.Code` und Feldinhalte,
  **nicht** freie Texte → ebenfalls unkritisch.
- Neue Testklasse `L10nTests` in `Pl0.Tests` für L10N-spezifische Assertions.
