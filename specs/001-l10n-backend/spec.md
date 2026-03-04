# Feature Specification: L10N Backend (Pl0.Core / Pl0.Vm / Pl0.Cli)

**Feature Branch**: `001-l10n-backend`
**Created**: 2026-03-01
**Status**: Draft — aktualisiert 2026-03-04 (Checklist-Remediation Runde 3)
**Input**: Lastenheft_L10N.md Abschnitte 1–3.2; Scope begrenzt auf Pl0.Core, Pl0.Vm, Pl0.Cli.

## Scope

Dieses Dokument beschreibt die Lokalisierung (L10N) ausschliesslich fuer:
- `src/Pl0.Core` — Fehlermeldungen des Compilers (Lexer, Parser, Semantik, Codegenerator)
- `src/Pl0.Vm` — Laufzeit-Fehlermeldungen der Virtuellen Maschine
- `src/Pl0.Cli` — Hilfe-Texte, Statusmeldungen und CLI-Ausgaben

**Explizit ausgeschlossen**: `src/Pl0.Ide` (wird in einem separaten Feature spezifiziert).

## Assumptions

- Der Standard-Mechanismus der .NET-Plattform (`.resx`-Ressourcendateien) wird fuer
  das String-Management verwendet, gemaess Lastenheft 3.3.
- Deutsch (`de-DE`) ist die Standardsprache; Englisch (`en-US`) ist die erste Zielsprache.
- Die PL/0-Sprache selbst (Schluesselwoerter, Syntax) wird nicht lokalisiert —
  nur die Werkzeug-Ausgaben.
- Alle bestehenden numerischen Fehlercodes bleiben unveraendert; nur Beschreibungstexte
  werden lokalisiert.
- Die Sprachauswahl erfolgt per CLI-Parameter; die gewaehlte Sprache wird als
  expliziter Parameter in `CompilerOptions` (fuer `Pl0.Core`) und in den
  VM-Optionen (fuer `Pl0.Vm`) weitergegeben — kein globaler Zustand, kein
  Ambient-Culture-Ansatz.
- **Test-Infrastruktur**: `L10nTests` pruefen englische VM-Fehlertexte ueber
  `BufferedPl0Io` (simuliertes I/O), da `ConsolePl0Io` in Tests nicht fangbar ist.
  Tests verwenden den `--lang`-Parameter (via `CliOptionsParser`), nicht direkt
  `CompilerOptions.Language`, um den vollstaendigen CLI-Pfad zu verifizieren.
- **EN-Quellbasis**: Die bestehenden englischen Inline-Strings in `Pl0Parser.cs`,
  `Pl0Lexer.cs` und `VirtualMachine.cs` werden als Ausgangsbasis fuer
  `Pl0CoreMessages.en.resx` / `Pl0VmMessages.en.resx` / `Pl0CliMessages.en.resx`
  uebernommen. Sie werden an NFR-002 (Sentence case, insbesondere PL/0-Schluesselwoerter
  wie `CALL`, `THEN`, `DO` in Grossbuchstaben) und data-model.md §7 (Terminologietabelle)
  angepasst. Kein englischer Inhalt wird von Grund auf neu verfasst.
- **Test-Annahme (validiert, 2026-03-04)**: Bestehende Tests pruefen keine
  Diagnosetexte des Compilers oder der VM. Quellcode-Pruefung ergab:
  `CompilationDiagnosticsTests.cs` konstruiert `CompilerDiagnostic`-Objekte
  manuell (kein Compilerlauf — prueft Formatierung, nicht den Lokalisierungspfad);
  `CatalogCasesTests.cs` prueft ausschliesslich Fehlercodes, nicht Texte;
  `IdeBootstrapTests.cs` prueft Pl0.Ide-Strings (explizit ausser L10N-Scope).
  Keine bestehenden Tests muessen angepasst werden.

---

## User Scenarios & Testing

### User Story 1 — CLI-Ausgabe in gewaehlter Sprache (Priority: P1)

Ein Nutzer startet `Pl0.Cli` mit dem Parameter `--lang en` und erhaelt alle
Hilfe-Texte und Statusmeldungen auf Englisch statt auf Deutsch.

**Why this priority**: Direktester Nutzennachweis fuer L10N; alle weiteren Stories
bauen auf demselben Mechanismus auf.

**Independent Test**: `Pl0.Cli --lang en --help` ausfuehren und pruefen, dass
der Usage-Text vollstaendig auf Englisch erscheint.

**Acceptance Scenarios**:

1. **Given** `Pl0.Cli` ist installiert, **When** `--lang en --help` aufgerufen wird,
   **Then** erscheint der gesamte Hilfe-Text auf Englisch.
2. **Given** `Pl0.Cli` ist installiert ohne `--lang`, **When** `--help` aufgerufen wird,
   **Then** erscheint der Hilfe-Text auf Deutsch (Standard).
3. **Given** ein ungueltiger Sprachcode (`--lang xx`), **When** `Pl0.Cli` startet,
   **Then** wird eine Warnung ausgegeben und Deutsch als Fallback verwendet;
   die Ausfuehrung bricht nicht ab.

---

### User Story 2 — Compiler-Fehlermeldungen in gewaehlter Sprache (Priority: P1)

Ein Nutzer kompiliert ein fehlerhaftes PL/0-Programm und erhaelt Diagnosen
(`CompilerDiagnostic`, `LexerDiagnostic`) in der beim CLI-Aufruf gewaelten Sprache.

**Why this priority**: Fehlermeldungen sind der wichtigste Ausgabekanal des Compilers;
ihre Verstaendlichkeit ist zentrales Qualitaetsmerkmal fuer Ausbildungszwecke.

**Independent Test**: `Pl0.Cli run fehlerhafte.pl0 --lang en --errmsg` ausfuehren
und pruefen, dass kurze und lange Diagnosetexte auf Englisch erscheinen.

**Acceptance Scenarios**:

1. **Given** ein PL/0-Programm mit Syntaxfehler, **When** mit `--lang en` kompiliert,
   **Then** enthaelt jede Diagnose einen englischen Beschreibungstext.
2. **Given** dasselbe Programm, **When** ohne `--lang` kompiliert,
   **Then** enthaelt jede Diagnose einen deutschen Text.
3. **Given** `--errmsg` ist aktiv, **When** Fehler auftreten,
   **Then** enthaelt der `Message`-Inhalt lokalisierten Text. Das Kurz-Format
   `"Error {code} at {line}:{col}."` ist sprachneutral (nur Codes und Zahlen)
   und wird **nicht** lokalisiert.

---

### User Story 3 — VM-Laufzeitfehler in gewaehlter Sprache (Priority: P2)

Ein Nutzer fuehrt ein PL/0-Programm aus, das einen Laufzeitfehler ausloest
(z. B. Division durch null), und erhaelt die `VmDiagnostic`-Meldung in der
gewaelten Sprache.

**Why this priority**: Laufzeitfehler sind seltener als Compilerfehler;
der Mechanismus aus P1 wird wiederverwendet.

**Independent Test**: `Pl0.Cli run division_by_zero.pl0 --lang en` ausfuehren
und pruefen, dass die VM-Fehlermeldung auf Englisch erscheint.

**Acceptance Scenarios**:

1. **Given** ein Programm dividiert durch null, **When** mit `--lang en` ausgefuehrt,
   **Then** erscheint eine englische VM-Fehlermeldung mit unveraendertem Exit-Code.
2. **Given** dasselbe Programm, **When** ohne `--lang` ausgefuehrt,
   **Then** erscheint die Meldung auf Deutsch.

---

### User Story 4 — Neue Sprache ohne Quellcode-Aenderung erweiterbar (Priority: P3)

Ein Ausbilder kann eine dritte Sprache (z. B. Franzoesisch) hinzufuegen, indem er
je eine neue Ressourcendatei pro Modul anlegt, ohne Quellcode in `Pl0.Core`,
`Pl0.Vm` oder `Pl0.Cli` zu aendern.

**Why this priority**: Erweiterbarkeit ist langfristig relevant, aber kein MVP-Blocker;
das Design muss sie ermoeglichen.

**Independent Test**: Dummy-Ressourcendatei fuer `fr-FR` anlegen, `--lang fr`
uebergeben, pruefen dass die neuen Texte verwendet werden.

**Acceptance Scenarios**:

1. **Given** neue `.resx`-Dateien fuer `fr-FR` sind vorhanden,
   **When** `--lang fr` uebergeben wird,
   **Then** verwendet die CLI die franzosischen Texte ohne Neukompilierung.
2. **Given** die neue Ressourcendatei ist unvollstaendig,
   **When** ein fehlender Key abgefragt wird,
   **Then** wird der deutsche Fallback-Text verwendet.

---

### Edge Cases

- Unbekannter Sprachcode (`--lang xx`): Warnung auf `stderr` + Fallback Deutsch; kein Abbruch; `stdout` bleibt unbeeintraechtigt.
- Fehlende Ressourcendatei fuer gewaehlte Sprache: Fallback Deutsch; kein unbehandelter Fehler.
- Leerer Sprachcode (`--lang ""`): identisch zu fehlendem `--lang` — Deutsch.
- Alle bestehenden Exit-Codes (96, 97, 98, 99) bleiben unveraendert.

---

## Requirements

### Functional Requirements

- **FR-001**: `Pl0.Cli` MUSS einen optionalen Parameter `--lang <code>` akzeptieren
  (BCP-47-Sprachcode, z. B. `de`, `en`, `de-DE`, `en-US`).
- **FR-002**: Benutzermeldungen von `Pl0.Cli` (Hilfe-Text, Kompilier-Statusmeldungen,
  Fehlertexte) MUESSEN in der gewaelten Sprache erscheinen. Technische Ausgaben
  (`--emit asm`, `--list-code`, `--wopcod`) und der Docs-Server (`--api`) sind
  sprachneutral und werden NICHT lokalisiert.
- **FR-003**: Alle Compiler-Diagnosetexte aus `Pl0.Core` MUESSEN in Deutsch und
  Englisch verfuegbar sein; die Sprachauswahl erfolgt ueber ein neues Feld
  in `CompilerOptions` (BCP-47-Sprachcode). Die deutschen Texte MUESSEN exakt den
  normativen Vorgaben in `data-model.md §3` entsprechen. Englische Texte MUESSEN semantisch
  aequivalent zur deutschen Vorlage sein: gleiche Bedeutung, gleicher
  Fehlercode-Kontext, gleiche Platzhalter-Semantik und gleiche Platzhalter-Reihenfolge.
  Die Aequivalenz MUSS ohne Deutschkenntnisse anhand der Terminologietabelle
  (`data-model.md §7`) und des Fehlercodes pruefbar sein.
- **FR-004**: Alle VM-Laufzeit-Fehlermeldungen aus `Pl0.Vm` MUESSEN in Deutsch und
  Englisch verfuegbar sein; die Sprachauswahl erfolgt ueber ein neues Feld
  in `VirtualMachineOptions`. Die deutschen Texte MUESSEN exakt den normativen
  Vorgaben in `data-model.md §4` entsprechen. Fuer englische Texte gilt dieselbe Anforderung an
  semantische Aequivalenz wie in FR-003 (gleiche Bedeutung, gleicher Kontext,
  gleiche Platzhalter-Semantik; pruefbar ohne Deutschkenntnisse).
- **FR-005**: Sprachspezifische Texte MUESSEN von der Programmlogik getrennt in
  Ressourcendateien abgelegt sein (gemaess Lastenheft 3.3). Alle `.resx`-Dateien
  (Deutsch und Englisch) MUESSEN UTF-8-kodiert sein; eine ASCII-only-Einschraenkung
  gilt nicht.
- **FR-006**: Fehlende Uebersetzungen MUESSEN automatisch auf die deutsche
  Standardressource zurueckfallen (Fallback-Kette). Testbare Akzeptanzkriterien:
  (1) `--lang en-US` → ResourceManager sucht `en-US.resx`, faellt auf `en.resx` zurueck
  → englischer Text erscheint; (2) `--lang fr` → kein `fr.resx` vorhanden → deutscher
  Fallback-Text erscheint; (3) `--lang ""` → identisch zu fehlendem `--lang` → Deutsch.
- **FR-007**: Numerische Fehlercodes DUERFEN sich nicht aendern; Lokalisierung
  betrifft ausschliesslich Beschreibungstexte.
- **FR-008**: Das System MUSS fuer weitere Sprachen erweiterbar sein, ohne
  Quellcode-Aenderungen in `Pl0.Core`, `Pl0.Vm` oder `Pl0.Cli`. Alle drei Module
  unterstuetzen `ResourceManager`-Injection:
  (a) `CompilerOptions` erhaelt optionalen `ResourceManager? Messages = null`
  (Default: `Pl0CoreMessages.ResourceManager`);
  (b) `VirtualMachineOptions` erhaelt optionalen `ResourceManager? Messages = null`
  (Default: `Pl0VmMessages.ResourceManager`);
  (c) `CliOptionsParser` erhaelt optionalen `ResourceManager? cliMessages = null`
  (Default: `Pl0CliMessages.ResourceManager`).
  Tests und Erweiterungen koennen einen eigenen `ResourceManager` injizieren,
  ohne Produktionscode zu aendern.
- **FR-009**: Bei unbekanntem Sprachcode MUSS eine Warnung auf `stderr` ausgegeben
  und Deutsch als Fallback verwendet werden; `stdout` bleibt unbeeintraechtigt,
  der Prozess laeuft weiter. Testbares Akzeptanzkriterium: `--lang xx` → `stderr`
  enthaelt Warnung mit Sprachcode `xx`; `stdout` enthaelt deutschen Text.
  `CliOptionsParser` MUSS einen optionalen `TextWriter errorOutput`-Parameter
  akzeptieren (Default: `Console.Error`); in `L10nTests` wird ein `StringWriter`
  uebergeben, um die Warnung zu pruefen — dies ist Anforderung, nicht
  Implementierungsdetail.
- **FR-010**: Alle bestehenden CLI-Switches MUESSEN unveraendert funktionieren;
  `--lang` ist ein additiver Parameter.
- **FR-011**: `--lang` MUSS fuer alle Befehle gelten (`compile`, `run`, `run-pcode`);
  VM-Fehlermeldungen werden in jedem Ausfuehrungspfad lokalisiert.

### Non-Functional Requirements

- **NFR-001 — Sprachniveau B2 (CEFR)**: Alle englischen Fehlertexte MUESSEN auf CEFR-Niveau B2
  formuliert sein: klare, einfache Saetze, gaengige Vokabeln — soweit PL/0-Terminologie
  (`CONST`, `VAR`, `LOD` etc.) es zulaesst. Pruefung: PR-Review durch KI-Agenten.
- **NFR-002 — Groesschreibungskonvention (Sentence case)**: Alle englischen Fehlertexte in
  Pl0.Core, Pl0.Vm und Pl0.Cli MUESSEN Sentence case verwenden: nur das erste Wort und
  Eigennamen werden grossgeschrieben. PL/0-Schluesselwoerter (`CONST`, `VAR`, `BEGIN`, `END`,
  `CALL`, `ODD`, `PROCEDURE`) bleiben stets in Grossbuchstaben. VM-Opcode-Namen (`LOD`,
  `STO`, `INT`, `OPR`, `Lit`, `Jmp`, `Jpc`, `Cal`) bleiben unveraendert.
- **NFR-003 — Konsistente Terminologie**: Alle englischen Texte MUESSEN die normative
  Terminologietabelle aus `data-model.md §7` verwenden. Abweichungen gelten als Fehler
  und blockieren den PR-Merge.

### Textstil-Konventionen

Die folgenden Konventionen gelten als normative Anforderungen fuer alle englischen Texte
und werden im PR-Review gegen die Terminologietabelle (`data-model.md §7`) geprueft:

| Konvention | Regel |
|------------|-------|
| Grossschreibung | Sentence case (NFR-002) |
| Abschliessende Interpunktion | Pflichtpunkt `.` am Satzende, analog deutschen Vorlagen |
| PL/0-Schluesselwoerter | Unveraendert in Grossbuchstaben: `CONST`, `VAR`, `PROCEDURE`, `BEGIN`, `END`, `CALL`, `ODD` |
| VM-Opcodes | Unveraendert wie definiert: `Lit`, `Opr`, `Lod`, `Sto`, `Cal`, `Int`, `Jmp`, `Jpc` |
| Platzhalter | Reihenfolge `{0}`, `{1}` identisch zur deutschen Vorlage — keine Umstellung |
| Terminologie | Gemaess normativer Tabelle `data-model.md §7` |

### Reviewer-Verantwortlichkeit

Der **PR-Reviewer** ist verantwortlich fuer die inhaltliche Korrektheit aller englischen
Uebersetzungen. Der Entwickler liefert die Uebersetzungen; der Reviewer prueft und
verantwortet die Qualitaet anhand der folgenden Kriterien:

1. **Terminologietabelle** (`data-model.md §7`): Alle Begriffe stimmen mit der normativen
   Tabelle ueberein — keine unkontrollierten Synonyme.
2. **Sentence case** (NFR-002): Grossschreibungskonvention eingehalten.
3. **B2-Niveau** (NFR-001): Texte sind fuer nicht-muttersprachliche Lernende verstaendlich;
   bei Zweifel Pruefung durch KI-Agenten im PR-Review.
4. **Interpunktion**: Jeder Satz schliesst mit `.`.
5. **Semantische Aequivalenz** (FR-003/FR-004): Bedeutung und Kontext stimmen mit
   der deutschen Vorlage ueberein — pruefbar ohne Deutschkenntnisse.

### Key Entities

- **Sprachressource**: Sammlung von Key-Value-Paaren (stabiler Schluessel → lokalisierter
  Text) pro Sprache und Modul.
- **Sprachcode**: BCP-47-konformer Bezeichner; Fallback-Kette: spezifisch (`de-DE`) →
  allgemein (`de`) → Standard (`de`).
- **Diagnosemeldung**: bestehende `CompilerDiagnostic`, `LexerDiagnostic`, `VmDiagnostic`
  — deren Texte werden lokalisiert, Codes nicht.

---

## Clarifications

### Session 2026-03-03 (Runde 2)

- Q: Wie wird die Spracheinstellung von Pl0.Cli an Pl0.Core und Pl0.Vm weitergegeben? → A: Expliziter Parameter — neues Feld in `CompilerOptions` (Pl0.Core) und `VirtualMachineOptions` (Pl0.Vm); kein globaler Zustand.
- Q: Welche CLI-Ausgaben gehoeren zum Lokalisierungsscope? → A: Nur Benutzermeldungen (Hilfe-Text, Statustexte, Fehlertexte); P-Code/Assembler-Output (`--emit asm`, `--list-code`, `--wopcod`) bleibt sprachneutral.
- Q: Verhalten bestehender Katalogtests bei lokalisierten Diagnosetexten? → A: Bestehende Tests pruefen keine Diagnosetexte und bleiben unveraendert; L10N-Coverage wird durch neue dedizierte Tests sichergestellt.
- Q: Gehoert der eingebettete Docs-Server (`--api`) zum Lokalisierungsscope? → A: Nein; `--api` ist ausgeschlossen — nur interaktive CLI-Benutzermeldungen werden lokalisiert.
- Q: Gilt `--lang` auch fuer den `run-pcode`-Befehl? → A: Ja; `--lang` gilt fuer alle Befehle inkl. `run-pcode`.
- Q: Ausgabekanal der Fallback-Warnung bei unbekanntem Sprachcode? → A: `stderr`; `stdout` bleibt sauber fuer maschinenlesbare Ausgaben.

### Session 2026-03-04 (Runde 4)

- Q: Wie soll die `stderr`-Fallback-Warnung (FR-009) in `L10nTests` testbar sein? `IPl0Io` hat keinen stderr-Kanal. → A: `CliOptionsParser` akzeptiert optionalen `TextWriter errorOutput`-Parameter (Default: `Console.Error`); Tests uebergeben `StringWriter`. `IPl0Io` bleibt unveraendert.
- Q: SC-004-Erweiterbarkeitstest — Swedish `.resx` in `tests/` wuerden nicht von `Pl0.Core`-ResourceManager gefunden (Satellite-Assembly-Pfad). Loesung? → A: `CompilerOptions`/`VirtualMachineOptions` erhalten optionalen `ResourceManager? Messages`-Parameter (FR-008); Test injiziert ResourceManager aus Swedish EmbeddedResource in Test-Assembly. Kein schwedischer Inhalt in Produktions-Build.
- Q: Soll `CliOptionsParser` fuer SC-004 analog zu Core/Vm einen injizierbaren `ResourceManager? cliMessages`-Parameter erhalten? → A: Ja — konsistentes Injektionsmuster fuer alle drei Module; FR-008 entsprechend ergaenzt.
- Q: Sollen die schwedischen Dummy-`.resx`-Dateien fuer SC-004 in Produktionscode (`src/`) oder als Test-Fixtures abgelegt werden? → A: Test-Fixtures in `tests/Pl0.Tests/Resources/` — kein Einfluss auf Release-Binary.
- Q: Soll das CLI-Kurz-Format `"Error {code} at {line}:{col}."` lokalisiert werden, oder nur der `Message`-Inhalt (sichtbar mit `--errmsg`)? → A: Kurz-Format ist sprachneutral (nur Codes/Zahlen); lokalisiert wird ausschliesslich `Message`. US-2 Szenario 3 entsprechend praezisiert.

### Session 2026-03-04 (Runde 7)

- Q: Fehlercode E4 wird im Parser an 4 Stellen mit unterschiedlichen Kontextmeldungen verwendet. Sollen diese als separate Resource-Keys oder als ein generischer Key gespeichert werden? → A: 4 separate Keys (`Parser_E04_IdentAfterConst`, `Parser_E04_IdentAfterVar`, `Parser_E04_IdentAfterProc`, `Parser_E04_IdentAfterInput`) — kontextspezifische Meldungen bleiben erhalten. Quellcode-Analyse ergibt ausserdem 10 bisher fehlende Parser-Keys; Gesamtanzahl korrigiert auf ~75 Keys. data-model.md §3 entsprechend vollstaendig ergaenzt.
- Q: `data-model.md §7` (Terminologietabelle) ist als normatives Artefakt referenziert aber nicht vorhanden — soll sie jetzt erstellt oder als Task aufgenommen werden? → A: Jetzt erstellt; Schema und Termini aus Quellcode-Analyse abgeleitet und in data-model.md §7 eingetragen (NFR-003-Grundlage fuer alle Uebersetzungsaufgaben).
- Q: Granularitaet der CLI-Help-Keys — 1 Key pro Zeile, 2-3 Block-Keys, oder 1 Gesamt-Key? → A: 1 Key pro logischer Zeile (~17 CLI-Help-Keys: `Cli_Help_UsageHeader`, `Cli_Help_CompileLine`, `Cli_Help_SwitchLang` etc.); data-model.md §5 entsprechend vollstaendig ergaenzt. Gesamtanzahl korrigiert auf ~75 Keys (5 Lexer + 32 Parser + 13 VM + 8 Err/Status + 17 Help).
- Q: Bestehende englische Inline-Strings in `Pl0Parser.cs` / `Pl0Lexer.cs` / `VirtualMachine.cs` — werden sie als EN-`.resx`-Basis uebernommen oder neu verfasst? → A: Uebernommen als Basis fuer `Pl0CoreMessages.en.resx` / `Pl0VmMessages.en.resx`; angepasst an NFR-002 (Sentence case, PL/0-Schluesselwoerter in Grossbuchstaben) und Terminologietabelle §7. Kein Inhalt wird von Grund auf neu geschrieben.
- Q: Sind die deutschen Texte in `data-model.md §3/§4/§5` normativ (1:1 in `Pl0CoreMessages.resx` uebernehmen) oder illustrativ (Entwickler schreibt selbst)? → A: Normativ — Entwickler uebernimmt die deutschen Texte aus `data-model.md` exakt als `.resx`-Inhalt; Abweichungen blockieren den PR-Merge.

### Session 2026-03-04 (Runde 6)

- Q: VM-interne Fehler (`Vm_E99_IPOutOfRange`, `Vm_E99_UnsupportedOpcode`, `Vm_E99_InvalidLodIndex` etc.) koennen nur durch ungueltige P-Code-Sequenzen ausgeloest werden, nicht durch PL/0-Quellprogramme. Sollen diese Keys via `CliOptionsParser --lang en` oder direkt via `VirtualMachine` mit `VirtualMachineOptions { Language = "en" }` getestet werden? → A: Direkt via `VirtualMachine(options: new VirtualMachineOptions { Language = "en" })` ohne CLI-Pfad; SC-002 entsprechend praezisiert.
- Q: Wie soll der ungueltige P-Code fuer VM-interne Tests bereitgestellt werden? → A: `Instruction[]` direkt im Testcode konstruieren (z. B. `new[] { new Instruction(Opcode.Opr, 0, 99) }`) — keine externen Fixture-Dateien, vollstaendig inline.

### Session 2026-03-03 (Runde 3 — Checklist-Remediation)

- Q: Muss semantische Aequivalenz ohne Deutschkenntnisse pruefbar sein? → A: Ja; Definition in FR-003/FR-004 aufgenommen, Terminologietabelle als normatives Artefakt definiert.
- Q: Welche Grossschreibungskonvention gilt fuer englische Fehlertexte? → A: Sentence case fuer alle drei Module; NFR-002 hinzugefuegt.
- Q: Wie wird B2-Sprachniveau geprueft? → A: Durch KI-Agenten im PR-Review; NFR-001 hinzugefuegt.
- Q: Sollen L10nTests direkt `CompilerOptions.Language` setzen oder den `--lang`-Parameter nutzen? → A: `--lang`-Parameter via `CliOptionsParser`; in SC-002 und Assumptions festgelegt.
- Q: Sollen VM-Fehlertexte in Tests ueber `BufferedPl0Io` oder `ConsolePl0Io` geprueft werden? → A: `BufferedPl0Io`, da `ConsolePl0Io` nicht fangbar; in SC-002 und Assumptions festgelegt.
- Q: Ist der Erweiterbarkeitstest (SC-004) manuell oder automatisiert? → A: Automatisierter Test; Dummy-Sprache Schwedisch (`--lang se`), Keys aller Module; SC-004 entsprechend praezisiert.
- Q: Muessen alle ~75 Keys einzeln getestet werden oder reicht eine Stichprobe? → A: Alle Keys einzeln; SC-002/SC-003 praezisiert.
- Q: UTF-8-Anforderung fuer englische .resx-Dateien explizit? → A: Ja; FR-005 ergaenzt.
- Q: Wer verantwortet englische Uebersetzungsqualitaet? → A: PR-Reviewer; neuer Abschnitt "Reviewer-Verantwortlichkeit" hinzugefuegt.
- Q: Testdaten-Ablageort fuer L10nTests? → A: `tests/data/pl0/l10n/`; in SC-004 festgelegt.

---

## Success Criteria

### Measurable Outcomes

- **SC-001**: Alle bestehenden Tests (`dotnet test`) laufen nach der Aenderung fehlerfrei
  durch — keine Regressionen (bestehende Tests pruefen keine Diagnosetexte und
  bleiben unveraendert).
- **SC-002**: Neue dedizierte L10N-Tests verifizieren **alle ~75 englischen Keys einzeln**
  in `Pl0.Tests/L10nTests.cs`. Tests nutzen den `--lang`-Parameter (via `CliOptionsParser`);
  VM-Fehlertexte, die durch PL/0-Programme ausloesbar sind (z. B. Division durch null,
  End-of-Input), werden ueber `BufferedPl0Io` geprueft. VM-interne Fehler, die nur durch
  ungueltige P-Code-Sequenzen ausloesbar sind (z. B. `Vm_E99_IPOutOfRange`,
  `Vm_E99_UnsupportedOpcode`, `Vm_E99_InvalidLodIndex`), werden direkt ueber `VirtualMachine`
  mit `VirtualMachineOptions { Language = "en" }` getestet — nicht via `CliOptionsParser`.
  Fallback-Kette und `stderr`-Warnung sind ebenfalls durch konkrete Eingabe/Ausgabe-Paare
  verifiziert.
- **SC-003**: Alle Compiler-Fehlertexte der Traceability-Matrix sind in beiden Sprachen
  verfuegbar und key-fuer-key durch die neuen L10N-Tests verifiziert (kein Key ohne
  individuellen Testfall).
- **SC-004**: Eine neue Sprache kann durch Hinzufuegen je einer Ressourcendatei pro Modul
  eingebunden werden — nachgewiesen durch einen **automatisierten Test** in `L10nTests.cs`
  mit Dummy-Sprache Schwedisch (`--lang se`). Mechanismus: Je eine `.resx`-Datei fuer
  Pl0.Core, Pl0.Vm und Pl0.Cli mit schwedischen Dummy-Texten wird als `EmbeddedResource`
  in `tests/Pl0.Tests/Resources/` kompiliert; der Test erstellt daraus einen
  `ResourceManager` und injiziert ihn via `CompilerOptions.Messages` /
  `VirtualMachineOptions.Messages` / `CliOptionsParser(cliMessages:)` (FR-008, alle drei
  Module). Der Test prueft, dass schwedische Texte ausgegeben werden. Kein schwedischer
  Inhalt landet im Produktions-Build.
  Test-Daten `.pl0`-Programme liegen in `tests/data/pl0/l10n/`.
- **SC-005**: Der Parameter `--lang` erscheint in der `--help`-Ausgabe beider Sprachen
  mit Beschreibung und Beispiel.
