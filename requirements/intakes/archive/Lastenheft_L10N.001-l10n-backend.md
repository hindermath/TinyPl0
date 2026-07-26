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
> Lies dir von @Lastenheft_L10N.001-l10n-backend.md die Abschnitte 1., 2., 3., 3.1, 3.2 durch. In
  dieser Spezifizierung soll nur die src/Pl0.Ide angepasst werden.

### 3.3 Technischer Standard
* **Zeichenkodierung:** Alle Sprachdateien müssen in UTF-8 kodiert sein.
* **Ressourcen-Management:** Verwendung von Standard-Mechanismen des .Net Core Frameworks zur Speicherung der Strings.

> Lies dir vom @Lastenheft_L10N.001-l10n-backend.md Abschnitt 3.2 durch. In dem Plan sollen nur die        
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

---

## 6. Implementierungsprotokoll — Feature 001-l10n-backend

**Branch:** `001-l10n-backend` | **Durchgeführt:** 2026-03-01 bis 2026-03-05 | **Status:** Abgeschlossen ✅

Dieses Kapitel dokumentiert den tatsächlichen Verlauf der Implementierung mit allen Problemen, Lösungen und Erkenntnissen. Es richtet sich an Auszubildende, die verstehen möchten, wie ein reales Feature von der Spezifikation bis zum grünen Test entsteht — einschliesslich der Fehler auf dem Weg dorthin.

---

### 6.1 Umgesetzte Architektur

Die Implementierung folgte exakt dem in `specs/001-l10n-backend/` dokumentierten Plan. Das Ergebnis ist eine vollständige L10N-Infrastruktur auf Basis von Standard-.NET-10-Mitteln:

```
src/Pl0.Core/Resources/
    Pl0CoreMessages.resx        ← 37 deutsche Basistexte (Invariant-Kultur)
    Pl0CoreMessages.cs          ← Accessor-Klasse mit ResourceManager-Property
    Pl0CoreMessages.en.resx     ← 37 englische Texte → SDK kompiliert zu en/Pl0.Core.resources.dll

src/Pl0.Vm/Resources/
    Pl0VmMessages.resx          ← 13 deutsche Basistexte
    Pl0VmMessages.cs            ← Accessor-Klasse
    Pl0VmMessages.en.resx       ← 13 englische Texte → en/Pl0.Vm.resources.dll

src/Pl0.Cli/Resources/
    Pl0CliMessages.resx         ← 25 deutsche Basistexte
    Pl0CliMessages.cs           ← Accessor-Klasse
    Pl0CliMessages.en.resx      ← 25 englische Texte → en/Pl0.Cli.resources.dll

tests/Pl0.Tests/Resources/
    Pl0CoreMessages.resx        ← DE-Fallback für Test-ResourceManager (Pflicht!)
    Pl0CoreMessages.se.resx     ← Schwedische Dummy-Texte (SC-004)
    Pl0VmMessages.resx / .se.resx
    Pl0CliMessages.resx / .se.resx
```

**Sprachübergabe durch den Stack:**

```
CLI-Argument --lang en
    → CliOptionsParser.ParseLanguageCode("en")
        → CultureInfo.GetCultureInfo("en", predefinedOnly: true) — Validierung
    → CompilerOptions { Language = "en" }
        → Pl0Lexer / Pl0Parser: rm.GetString(key, culture)
    → VirtualMachineOptions { Language = "en" }
        → VirtualMachine: _rm.GetString(key, _culture)
```

---

### 6.2 Phasenverlauf und Ergebnisse

#### Phase 1 — Setup (T001–T006)
Verzeichnisse und Gerüst-Dateien angelegt. Kein nennenswertes Problem.

#### Phase 2 — Foundation (T007–T009)
`CompilerOptions`, `VirtualMachineOptions` und `CliOptionsParser` um optionale `Language`- und `Messages`-Parameter erweitert. Wichtig: Alle bestehenden Aufrufe ohne diese Parameter bleiben gültig (nicht-breaking change).

#### Phase 3 — User Story 1: CLI-Ausgabe (T010–T016)
`CliHelpPrinter` und `CliOptionsParser` auf `ResourceManager`-Lookups umgestellt. Erste L10n-Tests für alle 25 CLI-Keys geschrieben und zum Laufen gebracht.

#### Phase 4 — User Story 2: Compiler-Diagnosen (T017–T025)
`Pl0Lexer` (5 Fehlertexte) und `Pl0Parser` (32 Fehlertexte) auf `ResourceManager`-Lookups umgestellt. Testdaten-Programme für alle Fehlerfälle in `tests/data/pl0/l10n/` angelegt.

#### Phase 5 — User Story 3: VM-Laufzeitfehler (T026–T032)
`VirtualMachine` auf instanz-basierte `ResourceManager`- und `CultureInfo`-Felder umgestellt. Alle 13 VM-Fehlertexte lokalisiert.

#### Phase 6 — User Story 4: Erweiterbarkeit (T033–T037)
Schwedische Dummy-`.resx`-Dateien als Test-Fixtures angelegt. Drei Extensibility-Tests beweisen, dass neue Sprachen ohne Quellcode-Änderung eingebunden werden können.

#### Phase 7 — Polish (T038–T041)
Release-Build, vollständiger Regressionstest, Terminologie-Audit und Quickstart-Smoke-Tests.

**Endergebnis:** 248 Tests — 0 Fehler.

---

### 6.3 Aufgetretene Probleme und Lösungen

Auf dem Weg zur grünen Test-Suite traten vier nicht-triviale Probleme auf. Sie sind hier ausführlich dokumentiert, weil sie typische Fallstricke bei der .NET-Lokalisierung darstellen.

---

#### Problem 1: Falscher ResourceManager-Manifest-Name

**Symptom:** `MissingManifestResourceException` beim Aufruf von `rm.GetString(key)` zur Laufzeit, obwohl die `.resx`-Datei im Projekt vorhanden ist.

**Ursache:** Der Manifest-Name einer eingebetteten Ressource (`EmbeddedResource`) wird vom MSBuild-Task `CreateCSharpManifestResourceName` berechnet. Wenn im selben Verzeichnis eine `.cs`-Datei mit demselben Basisnamen existiert (z. B. `Pl0CoreMessages.cs` neben `Pl0CoreMessages.resx`), verwendet MSBuild den **Namespace und den Klassennamen aus der `.cs`-Datei** als Manifest-Namen — nicht den Dateipfad.

**Konkret:** Die `.resx`-Datei liegt in `src/Pl0.Core/Resources/Pl0CoreMessages.resx`. Intuitiv erwartet man den Manifest-Namen `Pl0.Core.Resources.Pl0CoreMessages`. Tatsächlich liest MSBuild die Klasse `Pl0CoreMessages` im Namespace `Pl0.Core` aus der `.cs`-Datei und erzeugt den Manifest-Namen `Pl0.Core.Pl0CoreMessages`.

**Diagnose:**
```bash
strings bin/Debug/net10.0/Pl0.Core.dll | grep CoreMessages
# Ausgabe: Pl0.Core.Pl0CoreMessages.resources  ← kein "Resources." dazwischen
```

**Lösung:** In allen drei Accessor-Klassen den korrekten Manifest-Namen verwenden:
```csharp
// FALSCH (intuitiv, aber falsch):
new ResourceManager("Pl0.Core.Resources.Pl0CoreMessages", typeof(Pl0CoreMessages).Assembly)

// RICHTIG (tatsächlicher MSBuild-Output):
new ResourceManager("Pl0.Core.Pl0CoreMessages", typeof(Pl0CoreMessages).Assembly)
```

**Lernpunkt:** Der Manifest-Name einer `.resx`-Datei ist nicht trivial ableitbar. Bei Problemen hilft `strings` (Unix) oder `ILSpy` (Windows), um den tatsächlich eingebetteten Namen zu inspizieren. MSBuild-Verbose-Output (`-v:diag`) zeigt den Berechnungsweg.

---

#### Problem 2: Schwedische Satellite Assembly nicht gefunden

**Symptom:** `MissingManifestResourceException` in den Schwedisch-Tests (SC-004), obwohl `Pl0CoreMessages.se.resx` korrekt als `EmbeddedResource` eingebunden war.

**Ursache:** Das .NET-`ResourceManager`-System benötigt für Satellite-Assembly-Lookups zwingend eine **Basis-Ressource** (Invariant-Kultur) in der Hauptassembly. Ohne diese Basis weigert sich der `ResourceManager`, überhaupt nach Satellite-Assemblies zu suchen — er hat keinen Ankerpunkt für die Fallback-Kette.

Das Test-Projekt `Pl0.Tests` hatte keine Basis-`.resx`-Dateien (ohne Kulturcode-Suffix), nur die schwedischen `.se.resx`-Dateien.

**Lösung:** Basis-`.resx`-Dateien mit deutschen Fallback-Texten für das Testprojekt anlegen:
```
tests/Pl0.Tests/Resources/Pl0CoreMessages.resx   ← DE-Fallback, damit ResourceManager arbeitet
tests/Pl0.Tests/Resources/Pl0VmMessages.resx
tests/Pl0.Tests/Resources/Pl0CliMessages.resx
```

**Lernpunkt:** Die Fallback-Kette des `ResourceManager` ist:
```
Angeforderte Kultur (z. B. "se")
    → Neutrale Kultur (z. B. "se") als Satellite Assembly
    → Invariant-Kultur = Basis-Ressource in der Hauptassembly  ← PFLICHT
```
Fehlt die Basis-Ressource, schlägt der gesamte Lookup fehl, selbst wenn die spezifischere Satellite Assembly vorhanden ist.

---

#### Problem 3: VM-Test `Vm_InvalidBasePointer_En` schlug fehl

**Symptom:** Der Test erwartete die Fehlermeldung `"Invalid base pointer while resolving level: 0."`, erhielt aber `"Invalid LOD access at stack index 0."`.

**Ursache:** Missverständnis des VM-Initialisierungszustands. Der VM-Stack wird mit `stack[1]=0, stack[2]=0, stack[3]=0` initialisiert, `b=1` (Base Pointer), `t=0` (Top of Stack).

Bei `new Instruction(Opcode.Lod, Level: 1, Argument: 0)`:
- `ResolveBase(b=1, level=1)` liest `stack[1]=0` (Static Link), dekrementiert Level auf 0, bricht ab, gibt 0 zurück — **kein Fehler**
- Danach: Stack-Index = resolvedBase (0) + Argument (0) = 0 → `"Invalid LOD access at stack index 0."` — **falscher Fehler**

Für `Level=2`:
- Erste Traversierung: liest `stack[1]=0`, gibt 0 als neues Base zurück
- Zweite Traversierung: `0 < 1` → **Ungültiger Basiszeiger** → korrekte Fehlermeldung

**Lösung:** `Level: 1` → `Level: 2` im Test.

**Lernpunkt:** Um interne VM-Fehler per direktem P-Code zu provozieren, muss man den exakten Ausführungszustand (Stack-Belegung, Register-Werte) kennen und den P-Code so konstruieren, dass genau der gewünschte Codepfad erreicht wird. Manchmal muss man dafür den Quellcode der VM Schritt für Schritt durchdenken.

---

#### Problem 4: Platform-inkonsistente Kulturvalidierung

**Symptom:** Der Test `UnknownLanguage_FallsBackToGerman_WithStderrWarning` schlug auf macOS fehl: `CultureNotFoundException` wurde für den Code `"xx"` nie geworfen.

**Ursache:** Auf macOS und Linux verwendet .NET 10 standardmässig den **ICU-Globalisierungsmodus** (International Components for Unicode). ICU akzeptiert nahezu jeden wohlgeformten BCP-47-String, auch `"xx"` oder `"123"`, ohne eine Ausnahme zu werfen. Auf Windows hingegen verwendet .NET den NLS-Modus, der strikter validiert.

**Lösung:** `CultureInfo.GetCultureInfo(code, predefinedOnly: true)` anstelle von `CultureInfo.GetCultureInfo(code)`. Der Parameter `predefinedOnly: true` erzwingt plattformunabhängig, dass nur bekannte, vordefinierte Kulturen akzeptiert werden.

```csharp
// VORHER (nur auf Windows zuverlässig):
CultureInfo.GetCultureInfo(code);

// NACHHER (plattformkonsistent):
CultureInfo.GetCultureInfo(code, predefinedOnly: true);
```

**Nebeneffekt:** Codes wie `"fr"` (Französisch), `"sv"` (Schwedisch), `"ja"` (Japanisch) werden jetzt als gültig akzeptiert, auch wenn keine Satellite Assembly für diese Sprache existiert — der ResourceManager fällt dann auf Deutsch zurück. Nur wirklich unbekannte Codes wie `"xx"` oder `"notalang"` lösen die Warnung aus.

**Lernpunkt:** .NET-Globalisierungsverhalten ist betriebssystemabhängig. Wer plattformübergreifenden Code schreibt, muss testen — und bei Kulturvalidierung explizit den Modus angeben.

---

### 6.4 Terminologie-Audit (T040)

Alle 75 englischen Ressourcen-Keys wurden gegen die normative Terminologietabelle in `data-model.md §7` und NFR-001 (B2 CEFR) geprüft.

**Ergebnis: Keine Abweichungen.**

Eine spec-interne Inkonsistenz wurde dokumentiert:
- `data-model.md §4` (normative EN-Texte) gibt `"Stack overflow on INT."` vor
- `data-model.md §7.2` (VM-Opcode-Namen) listet `Int` (gemischte Schreibung)
- Die Implementierung folgt §4, da dieser die konkreten EN-Texte normativ vorschreibt

Alle PL/0-Schlüsselwörter erscheinen in Grossbuchstaben (CONST, VAR, PROCEDURE, END, CALL, THEN, DO). Alle Domänenbegriffe (identifier, variable, statement, expression, relational operator, …) werden einheitlich verwendet.

---

### 6.5 Quickstart-Smoke-Tests (T041)

Alle acht Smoke-Tests aus `specs/001-l10n-backend/quickstart.md` wurden manuell ausgeführt.

| § | Szenario | Erwartung | Ergebnis |
|---|----------|-----------|----------|
| 1 | `--lang en --help` | Englischer Hilfe-Text | ✅ PASS |
| 2 | `--help` (Standard) | Deutscher Hilfe-Text | ✅ PASS |
| 3 | `--lang en run ... --errmsg` | `"Undeclared identifier."` | ✅ PASS |
| 4 | `run ... --errmsg` (Standard) | `"Nicht deklarierter Bezeichner."` | ✅ PASS |
| 5 | `--lang en run divzero.pl0` | `"Division by zero."` | ✅ PASS |
| 6 | `--lang en compile + run-pcode` | `"Division by zero."` aus .pcode | ✅ PASS |
| 7 | `--lang xx` (ungültig) | Warnung auf stderr + Deutsch | ✅ PASS |
| 8 | Neue Sprache `sv` per .resx | `"Odefinierad identifierare."` | ✅ PASS |

**Hinweis zu §3/§4:** Die Fehlermeldung erscheint nur mit `--errmsg`. Ohne diesen Schalter zeigt der CLI nur den kurzen Fehlercode und die Position (`Error 11 at 1:9.`). Dies ist das bestehende Design des CLI — die Lokalisierung greift nur für die lange Fehlermeldung.

**Hinweis zu §7:** Der Quickstart-Test verwendete `--lang fr` (Französisch) als „unbekannten" Code. Da Französisch eine gültige Kultur ist (`predefinedOnly: true` akzeptiert sie), wird keine Warnung erzeugt. Der Test wurde mit `--lang xx` durchgeführt, das korrekt eine Warnung auslöst.

**Hinweis zu §8:** Der Quickstart schlug `--lang test` vor. Da `test` keine vordefinierte Kultur ist, fällt der CLI auf Deutsch zurück. Für den Smoke-Test wurde `sv` (Schwedisch) als gültige Kultur mit temporärer `Pl0CoreMessages.sv.resx` verwendet. Nach dem Test wurde die temporäre Datei wieder entfernt.

---

### 6.6 Abschliessende Testergebnisse

```
dotnet test
Ergebnis: 248 Tests bestanden, 0 Fehler, 0 übersprungen

dotnet build --configuration Release
Ergebnis: Satellite Assemblies erzeugt:
    bin/Release/net10.0/en/Pl0.Core.resources.dll
    bin/Release/net10.0/en/Pl0.Vm.resources.dll
    bin/Release/net10.0/en/Pl0.Cli.resources.dll
```

Alle Abnahmekriterien (SC-001 bis SC-005) erfüllt.

---

### 6.7 Lernzusammenfassung für Auszubildende

Dieses Feature demonstriert mehrere wichtige Konzepte der professionellen Software-Entwicklung:

**1. Standardmechanismen vor Eigenentwicklung**
Lokalisierung in .NET benötigt kein zusätzliches NuGet-Paket. Das SDK kompiliert `.resx`-Dateien automatisch zu Satellite Assemblies und der `ResourceManager` übernimmt Fallback und Kultur-Auflösung. Erst verstehen, was das Framework bietet — dann entscheiden.

**2. Nicht-breaking API-Erweiterung**
Neue Parameter wurden als optionale Parameter mit Defaultwerten ans Ende des Primary Constructors gesetzt. Alle bestehenden Aufrufe (`new CompilerOptions(dialect)`) funktionieren ohne Änderung weiter. So bleibt die API stabil, während neue Funktionalität hinzukommt.

**3. Dependency Injection für Testbarkeit**
`ResourceManager` und `TextWriter` werden in alle relevanten Klassen injiziert. Tests können dadurch eigene ResourceManager-Instanzen übergeben (z. B. mit schwedischen Dummy-Texten) oder `Console.Error` durch einen `StringWriter` ersetzen, um Warnungen zu prüfen. Ohne DI wären diese Tests nicht möglich.

**4. Manifest-Namen sind nicht intuitiv**
MSBuild berechnet Ressourcen-Manifest-Namen nach eigenen Regeln. Wer einen `ResourceManager` per logischem Namen konstruiert, muss den tatsächlichen Namen kennen — nicht den, den er erwartet. `strings assembly.dll | grep ResourceName` ist ein nützliches Diagnosewerkzeug.

**5. Plattformunterschiede bei Globalisierung**
.NET verhält sich unter Windows (NLS) und macOS/Linux (ICU) unterschiedlich bei der Kulturvalidierung. `predefinedOnly: true` ist der plattformkonsistente Weg. Wer nur auf Windows testet, kann Bugs einbauen, die erst auf dem CI-Server (Linux) sichtbar werden.

**6. P-Code-Debugging erfordert VM-Kenntnisse**
Um gezielt VM-Fehlerzustände zu provozieren, muss man die VM von innen kennen: Wie ist der Stack initialisiert? Was tut `ResolveBase`? Nur wer das weiss, kann den richtigen P-Code-Schnipsel schreiben, der genau den gewünschten Codepfad trifft.

**7. Spezifikation vor Implementierung zahlt sich aus**
Das gesamte Feature wurde zuerst in `specs/001-l10n-backend/` vollständig spezifiziert (spec.md, plan.md, data-model.md, contracts/, research.md, tasks.md), bevor eine Zeile Produktionscode geschrieben wurde. Die drei aufgetretenen Bugs waren schnell zu lösen, weil der Plan klar war und nur die technischen Details angepasst werden mussten — nicht der gesamte Ansatz.
