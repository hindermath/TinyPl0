# Lokalisierung

## 1. Einleitung
Dieses Dokument beschreibt die Anforderungen an die Lokalisierung (L10N) für das Projekt TinyPl0. Ziel ist es, die Software für verschiedene Sprachräume (initial Deutsch und Englisch) anpassbar zu machen.

## 2. Zielsetzungen
* Trennung von Programmlogik und sprachspezifischen Ressourcen.
* Unterstützung von Deutsch (de-DE) und Englisch (en-US).
* Einfache Erweiterbarkeit um weitere Sprachen.
* Pl0.Cli soll die Lokalisierung unterstützen und einen Sprachenparameter erhalten.
* In der Pl0.Ide soll die Sprache in den Einstellungen einstellbar sein.

## 3. Anforderungen

### 3.1 Unterstützte Sprachen
* **Deutsch (Standard):** Alle Ausgaben und Fehlermeldungen in deutscher Sprache.
* **Englisch:** Alle Ausgaben und Fehlermeldungen in englischer Sprache.

### 3.2 Lokalisierte Komponenten
Folgende Elemente müssen lokalisiert werden:
* Fehlermeldungen des Compilers/Interpreters.
* Hilfe-Texte der Kommandozeile (CLI).
* Statusmeldungen während des Kompiliervorgangs.
* Das Interface der IDE (Pl0.Ide).
* 
-> Lies dir von @Lastenheft_L10N.md die Abschnitte 1., 2., 3., 3.1, 3.2 durch. In
  dieser Spezifizierung soll nur die src/Pl0.Ide angepasst werden.

### 3.3 Technischer Standard
* **Zeichenkodierung:** Alle Sprachdateien müssen in UTF-8 kodiert sein.
* **Ressourcen-Management:** Verwendung von Standard-Mechanismen des .Net Core Frameworks zur Speicherung der Strings.

-> Lies dir vom @Lastenheft_L10N.md Abschnitt 3.2 durch. In dem Plan sollen nur die        
Standard .Net 10/C# 14 Mechanismen genutzt werden, wie in dem Abschnitt beschrieben. Falls .Net 10/C#  
14 neuere Mechanismen bieten, können die genutzt werden.

## 4. Glossar
* **L10N (Localization):** Anpassung der Software an einen spezifischen Zielmarkt.
* **I18N (Internationalization):** Vorbereitung der Software, um mehrere Sprachen technisch zu unterstützen.

---

## 5. Umsetzungsdokumentation: Feature 001-l10n-backend (Pl0.Core / Pl0.Vm / Pl0.Cli)

**Branch:** `001-l10n-backend` | **Erarbeitet:** 2026-03-01 bis 2026-03-05 | **Status:** Bereit zur Implementierung

Dieses Kapitel dokumentiert die vollständige Spezifikations- und Planungskonversion für die L10N-Backend-Implementierung. Scope dieses Features ist ausschliesslich `Pl0.Core`, `Pl0.Vm` und `Pl0.Cli` — `Pl0.Ide` wird in einem separaten Feature spezifiziert.

---

### 5.1 Scope-Abgrenzung

| Modul | In Scope | Lokalisierte Inhalte |
|-------|----------|---------------------|
| `Pl0.Core` | ✅ | 5 Lexer-Fehlertexte, 32 Parser-Fehlertexte |
| `Pl0.Vm` | ✅ | 13 VM-Laufzeit-Fehlertexte |
| `Pl0.Cli` | ✅ | 17 Hilfe-Texte, 6 Fehler-Meldungen, 2 Status-Meldungen |
| `Pl0.Ide` | ❌ | Separates Feature |

Sprachneutral (nicht lokalisiert): P-Code-Listings (`--list-code`, `--emit asm`, `--wopcod`), Docs-Server (`--api`), PL/0-Schlüsselwörter, numerische Fehlercodes.

**Gesamtumfang:** ~75 lokalisierbare Strings in zwei Sprachen (Deutsch als Standard, Englisch als erste Zielsprache).

---

### 5.2 Architekturentscheidungen (Phase 0: Research)

Alle Entscheidungen sind in `specs/001-l10n-backend/research.md` vollständig begründet.

| Entscheidung | Gewählte Lösung | Begründung |
|-------------|-----------------|------------|
| Lokalisierungsmechanismus | `.resx`-Dateien + `ResourceManager` + strongly-typed Designer-Klassen (`ResXFileCodeGenerator`) | Standard .NET SDK; kein zusätzliches NuGet-Paket; Satellite-Assembly-Fallback eingebaut |
| Sprachübergabe | Explizites `Language`-Feld in `CompilerOptions` und `VirtualMachineOptions` | Kein globaler Zustand; jeder Kompilierlauf unabhängig testbar |
| Ressourcen-Key-Schema | `<Modul>_E<Code>_<KurzerName>` (z. B. `Parser_E11_UndeclaredIdent`) | Fehlercode im Key ermöglicht schnelle Diagnose-Zuordnung |
| Fallback-Kette | SDK-eingebaut: `en-US` → `en` → Invariant (= Deutsch) | Keine eigene Fallback-Logik notwendig |
| Ungültiger Sprachcode | `CultureInfo.GetCultureInfo()` Exception abfangen → Warnung auf `stderr` → Fallback `"de"` | Programm läuft weiter; `stdout` bleibt sauber |
| Erweiterbarkeit | `ResourceManager?`-Injection in alle drei Options-Records | Neue Sprache durch .resx-Datei ohne Quellcode-Änderung |

---

### 5.3 Technischer Vertrag (Phase 1: Design)

Die vollständige API-Spezifikation ist in `specs/001-l10n-backend/contracts/cli-contract.md` dokumentiert.

#### Neuer CLI-Parameter

```
dotnet run --project src/Pl0.Cli -- [--lang <BCP-47-Code>] <Befehl> [Optionen] [Dateien]
```

| Parameter | Typ | Default | Gültige Werte |
|-----------|-----|---------|---------------|
| `--lang` | string (BCP-47) | `de` | `de`, `de-DE`, `en`, `en-US` |

#### Geänderte öffentliche APIs

```csharp
// Pl0.Core — nicht-breaking (optionale Parameter)
public sealed record CompilerOptions(
    Pl0Dialect Dialect,
    // ... bestehende Parameter ...
    string Language = "de",          // NEU
    ResourceManager? Messages = null  // NEU
)

// Pl0.Vm — nicht-breaking
public sealed record VirtualMachineOptions(
    int StackSize = 500,
    bool EnableStoreTrace = false,
    string Language = "de",          // NEU
    ResourceManager? Messages = null  // NEU
)

// Pl0.Cli — neue optionale Konstruktor-Parameter
CliOptionsParser(
    TextWriter? errorOutput = null,      // NEU — für stderr-Capture in Tests
    ResourceManager? cliMessages = null  // NEU — für ResourceManager-Injection
)
```

Alle bestehenden Aufrufe ohne die neuen Parameter bleiben gültig. Numerische Fehlercodes (`96`, `97`, `98`, `99`, `206`) bleiben unverändert.

---

### 5.4 Ressourcen-Dateistruktur

```
src/Pl0.Core/Resources/
    Pl0CoreMessages.resx        # Deutsch (Invariant/Standard) — 37 Keys
    Pl0CoreMessages.en.resx     # Englisch — 37 Keys
    Pl0CoreMessages.Designer.cs # Autogeneriert vom SDK

src/Pl0.Vm/Resources/
    Pl0VmMessages.resx          # Deutsch — 13 Keys
    Pl0VmMessages.en.resx       # Englisch — 13 Keys
    Pl0VmMessages.Designer.cs   # Autogeneriert vom SDK

src/Pl0.Cli/Resources/
    Pl0CliMessages.resx         # Deutsch — 25 Keys
    Pl0CliMessages.en.resx      # Englisch — 25 Keys
    Pl0CliMessages.Designer.cs  # Autogeneriert vom SDK

tests/Pl0.Tests/Resources/      # Nur Test-Fixtures, kein Produktions-Build-Einfluss
    Pl0CoreMessages.se.resx     # Schwedische Dummy-Texte (SC-004 Erweiterbarkeitstest)
    Pl0VmMessages.se.resx
    Pl0CliMessages.se.resx
```

Die normativen deutschen und englischen Texte für alle ~75 Keys sind vollständig in `specs/001-l10n-backend/data-model.md §3/§4/§5` dokumentiert.

---

### 5.5 Anforderungen (Functional Requirements)

| ID | Anforderung |
|----|------------|
| FR-001 | `Pl0.Cli` akzeptiert optionalen Parameter `--lang <BCP-47-Code>` |
| FR-002 | Alle Benutzermeldungen von `Pl0.Cli` erscheinen in der gewählten Sprache |
| FR-003 | Alle Compiler-Diagnosetexte (`Pl0.Core`) in Deutsch und Englisch verfügbar |
| FR-004 | Alle VM-Laufzeit-Fehlertexte (`Pl0.Vm`) in Deutsch und Englisch verfügbar |
| FR-005 | Strings in `.resx`-Dateien abgelegt; alle Dateien UTF-8-kodiert |
| FR-006 | Fehlende Übersetzungen fallen automatisch auf Deutsch zurück |
| FR-007 | Numerische Fehlercodes bleiben unverändert |
| FR-008 | System ohne Quellcode-Änderung um weitere Sprachen erweiterbar (`ResourceManager`-Injection) |
| FR-009 | Unbekannter Sprachcode → Warnung auf `stderr` + Fallback Deutsch; kein Abbruch |
| FR-010 | Alle bestehenden CLI-Switches bleiben unverändert |
| FR-011 | `--lang` gilt für alle Befehle (`compile`, `run`, `run-pcode`) |

| ID | Nicht-funktionale Anforderung |
|----|------------------------------|
| NFR-001 | Englische Fehlertexte auf CEFR-Niveau B2 formuliert |
| NFR-002 | Sentence case: nur erstes Wort und Eigennamen grossgeschrieben; PL/0-Schlüsselwörter stets in Grossbuchstaben |
| NFR-003 | Konsistente Terminologie gemäss normativer Tabelle in `data-model.md §7` |

---

### 5.6 Abnahmekriterien (Success Criteria)

| ID | Kriterium |
|----|----------|
| SC-001 | Alle bestehenden Tests (`dotnet test`) laufen nach der Änderung fehlerfrei — keine Regressionen |
| SC-002 | Neue `L10nTests.cs` verifiziert alle ~75 englischen Keys einzeln; Fallback-Kette und `stderr`-Warnung durch konkrete Ein-/Ausgabe-Paare getestet |
| SC-003 | Jeder Fehlercode in `matrix.json` hat einen korrespondierenden Eintrag in `Pl0CoreMessages.resx` und `Pl0CoreMessages.en.resx` |
| SC-004 | Automatisierter Erweiterbarkeitstest: Schwedische Dummy-`.resx`-Dateien als `EmbeddedResource` in `Pl0.Tests`; Test injiziert `ResourceManager` und prüft schwedische Texte — kein Einfluss auf Produktions-Build |
| SC-005 | Parameter `--lang` erscheint in `--help`-Ausgabe beider Sprachen mit Beschreibung und Beispiel |

---

### 5.7 Implementierungsplan — Phasen und Tasks

Die vollständige Task-Liste ist in `specs/001-l10n-backend/tasks.md` dokumentiert (41 Tasks, T001–T041).

| Phase | Inhalt | Tasks | Blockiert durch |
|-------|--------|-------|-----------------|
| 1 — Setup | Verzeichnisstruktur, `L10nTests.cs`-Gerüst | T001–T006 | — |
| 2 — Foundation | `CompilerOptions`, `VirtualMachineOptions`, `CliOptionsParser` erweitern | T007–T009 | Phase 1 |
| 3 — US1 (P1) | CLI-Ausgabe auf EN/DE: `Pl0CliMessages.resx` + `CliHelpPrinter` + `CliOptionsParser` | T010–T016 | Phase 2 |
| 4 — US2 (P1) | Compiler-Diagnosen auf EN/DE: `Pl0CoreMessages.resx` + Lexer/Parser-Refactoring | T017–T025 | Phase 2 |
| 5 — US3 (P2) | VM-Laufzeitfehler auf EN/DE: `Pl0VmMessages.resx` + `VirtualMachine`-Refactoring | T026–T032 | Phase 2 |
| 6 — US4 (P3) | Erweiterbarkeit: Schwedische Dummy-Fixtures + Extensibility-Tests | T033–T037 | Phase 2 |
| 7 — Polish | Release-Build, Regressionstest, NFR-Review, Smoke-Tests | T038–T041 | Phase 3–6 |

**MVP-Scope (Phase 1–3):** Nach Abschluss von Phase 3 ist `--lang en --help` vollständig funktional und demonstrierbar.

---

### 5.8 Qualitätssicherung der Spezifikation

Die Spezifikationsartefakte (`spec.md`, `plan.md`, `tasks.md`) wurden mit dem `/speckit.analyze`-Werkzeug in mehreren Iterationen auf Konsistenz, Vollständigkeit und Übereinstimmung mit der Projekt-Constitution geprüft. Ergebnis der finalen Analyse:

| Metrik | Ergebnis |
|--------|----------|
| Anforderungsabdeckung | 100 % (19/19 Requirements mit ≥1 Task) |
| CRITICAL Issues | 0 |
| HIGH Issues | 0 |
| MEDIUM Issues | 0 |
| LOW Issues | 0 |
| Constitution-Verstösse | 0 |

Alle sechs Constitution-Prinzipien (Didaktische Klarheit, Historische Kompatibilität, Testgetriebene Qualität, Strikte Modularchitektur, Fehlerdiagnose statt Ausnahmen, Git-Workflow) wurden eingehalten.
