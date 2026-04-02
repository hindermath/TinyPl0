# TinyPl0

[![Docs](https://img.shields.io/badge/docs-GitHub%20Pages-2ea44f)](https://hindermath.github.io/TinyPl0/)
[![Docs Pages](https://github.com/hindermath/TinyPl0/actions/workflows/docs-pages.yml/badge.svg)](https://github.com/hindermath/TinyPl0/actions/workflows/docs-pages.yml)

## Sprache und Zielgruppe
### DE
Dieses Projekt ist auf die **Fachinformatiker-Ausbildung in Deutschland** ausgerichtet.
Lernrelevante Dokumentation und Kommentare werden **zweisprachig** gepflegt:
zuerst Deutsch, danach Englisch. Beide Textblöcke sollen ein gut verständliches
Sprachniveau **B2 (GER/CEFR)** einhalten, damit auch nicht-muttersprachliche
Auszubildende alle Inhalte sicher nachvollziehen können.

### EN
This project targets **vocational IT training (Fachinformatiker) in Germany**.
Learner-facing documentation and comments are maintained **bilingually**:
German first, English second. Both language blocks are expected to follow
**B2 (CEFR/GER)** readability so non-native trainees can fully understand
compiler construction concepts and project workflows.

## Zielsetzung
Dieses Repository dient der Portierung des historischen PL/0-Beispielcompilers (Pascal) nach C# auf .NET 10.

Die fachliche und technische Zieldefinition steht im Pflichtenheft:
- [Pflichtenheft_PL0_CSharp_DotNet10.md](Pflichtenheft_PL0_CSharp_DotNet10.md)

## Lizenz
Lizenz: MIT - siehe [LICENSE](LICENSE).

## Referenzverhalten
Die Portierung orientiert sich an zwei klaren Dialekten:

1. `classic`
- Kompatibel zum vorliegenden Pascal-Referenzcode.
- Fokus auf historische Semantik und VM-Verhalten.

2. `extended`
- Konsolidierte EBNF inkl. `? ident` (Input) und `! expression` (Output) auf Basis der ANTLR-PL/0-Grammatik.

Referenzquellen im Repository:
- [PL0.md](PL0.md)
- [pl0c.pas](pl0c.pas)

## Sprachumfang und Einschränkungen
- Datentyp: nur `integer`.
- Prozeduren sind unterstützt, Parameter/Funktionsrückgaben nicht.
- Dialekte:
1. `classic`: ohne `?`/`!`.
2. `extended`: mit Eingabe `? ident` und Ausgabe `! expression`.

## Architektur und Qualität
- Architekturdiagramm und Pascal->C# Mapping:
  - `docs/ARCHITECTURE.md`
- Sprachumfang/EBNF:
  - `docs/LANGUAGE_EBNF.md`
- VM-Befehlssatz:
  - `docs/VM_INSTRUCTION_SET.md`
- Traceability-Matrix (Regel -> Pflichttests):
  - `docs/TRACEABILITY_MATRIX.md`
- Qualitäts- und Coverage-Übersicht:
  - `docs/QUALITY.md`
- Veröffentlichtes DocFX-Handbuch über GitHub Pages:
  - `https://hindermath.github.io/TinyPl0/`

## Build und Ausführung

### Versionskonvention (Pl0.Ide)
Fuer `src/Pl0.Ide` gilt die vierteilige Version `Major.Minor.Patch.Build` mit folgender Bedeutung:
- `Minor`: aktuelle PR-Nummer
- `Patch`: Anzahl Commits im aktuellen PR-Branch
- `Build`: manueller Buildzaehler (vor jedem `dotnet build`/`dotnet test` erhoehen)

### Aktueller Stand (CLI + IDE)
Die .NET-10-Solution ist angelegt:
- `TinyPl0.sln`
- `src/Pl0.Core`
- `src/Pl0.Vm`
- `src/Pl0.Cli`
- `src/Pl0.Ide`
- `tests/Pl0.Tests`

Zusätzlich sind umgesetzt:
- `CliOptionsParser` für die Pascal-kompatiblen Compiler-Switches.
- Ein erster Lexer (`Pl0Lexer`) mit Zeilen-/Spalten-Tracking.
- Parser + Symboltabelle + Codegenerator (`Pl0Parser`/`Pl0Compiler`).
- VM/Interpreter (`VirtualMachine`) inklusive Stack-Maschine und I/O-Adapter (`ConsolePl0Io`, `BufferedPl0Io`).
- CLI-Subcommands: `compile`, `run`, `run-pcode` (inkl. `--out`, `--list-code`, `--emit`).
- P-Code Serialisierung/Deserialisierung (`PCodeSerializer`) für Datei-Workflow.
- End-to-End-Tests für `source -> pcode -> vm` in `tests`.
- Terminal-GUI-IDE (`Pl0.Ide`) mit Quellcode-Editor, Kompilieren/Ausführen, Export (`asm`/`cod`) und Schritt-Debugging über `SteppableVirtualMachine`.
- Vollständiger 8.2-Testdatenkatalog (41 Pflicht-`.pl0`-Fälle) inkl. erwarteter Artefakte:
  - `tests/data/expected/catalog/cases.json`

### Pascal-Referenz (optional)
Voraussetzung: FreePascal (`fpc`) oder Delphi-kompatibler Compiler.

Beispiel:
```bash
fpc pl0c.pas
./pl0c
```

### .NET-10-Befehle
```bash
dotnet restore
dotnet build
dotnet test
```

### CLI-Beispiele
```bash
# Kompilieren nach .pcode
dotnet run --project src/Pl0.Cli -- compile tests/data/pl0/valid/feature_const_var_assignment.pl0 --out /tmp/example.pcode
```
```bash
# PL/0 Quelltext direkt ausführen
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_io_q_bang_relops.pl0
```
```bash
# Vorhandene .pcode Datei ausführen
dotnet run --project src/Pl0.Cli -- run-pcode tests/data/expected/code/feature_io_q_bang_relops.pcode.txt
```
```bash
# Code-Liste ausgeben (didaktisch)
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_const_var_assignment.pl0 --list-code --wopcod
```

### IDE starten
```bash
dotnet run --project src/Pl0.Ide
```

## Repository-Status
- Git-Repository ist initialisiert.
- `.gitignore` für .NET/C#/Visual Studio/JetBrains ist vorhanden.
- Core/VM/CLI sind inklusive End-to-End-Workflow (`source -> pcode -> vm`) umgesetzt.
- `Pl0.Ide` ist als eigenes Projekt in die Solution integriert und durch Tests abgesichert.
- Qualitäts- und Architekturprüfungen laufen über die Test-Suite (`ArchitectureGuardTests`, Traceability-/Golden-/E2E-Tests).

## Beitrags-Workflow (Pull Requests)
- Der Branch `main` ist geschützt; direkte Commits/Pushes auf `main` sind nicht erlaubt.
- Für jede Änderung zuerst einen neuen, sauberen Arbeits-Branch erstellen.
- Änderungen ausschließlich per Pull Request nach `main` integrieren.

## Entwicklung mit Agentic-AI

Dieses Projekt entstand mit starker Unterstützung durch **Agentic-AI-Technologien**. Die Verwendung von KI-Agenten spielte eine zentrale Rolle bei:

- **Architektur & Design:** Automatisierte Analyse von Anforderungen und Ableitung der Systemarchitektur.
- **Code-Generierung:** Unterstützung bei der Implementierung von Lexer, Parser, Compiler und VM.
- **Testing & Validierung:** Systematische Generierung von Testfällen und Überprüfung der Compliance.
- **Dokumentation:** Automatisierte Erstellung von API-Dokumentation, Handbüchern und Architekturdokumentation.
- **Qualitätssicherung:** Statische Codeanalyse, Fehlerdetection und Optimierungsvorschläge.

### Ziel: Agentic-AI in der Ausbildung

Neben der praktischen Implementierung eines Compilers dient dieses Projekt auch als **Fallstudie** zur Untersuchung, wie Agentic-AI sinnvoll in der **Fachinformatiker-Ausbildung** eingesetzt werden kann:

- **Pädagogischer Mehrwert:** Wie können AI-Agenten Auszubildende beim Erlernen komplexer Compilerbau-Konzepte unterstützen?
- **Produktivität:** Wie beschleunigt die Zusammenarbeit mit AI-Agenten die Entwicklung, ohne die Lerneffekte zu schmälern?
- **Qualität:** Wie trägt Agentic-AI zu besserer Code-Qualität, Dokumentation und Testabdeckung bei?
- **Transparenz:** Wie bleibt die Nachvollziehbarkeit und Validierbarkeit der generierten Artefakte gewährleistet?

Dieses Projekt zeigt, dass Agentic-AI nicht als Ersatz für manuelles Lernen gedacht ist, sondern als **produktive Werkzeug und Lernpartner** für professionelle Softwareentwicklung in der Ausbildung.

## Inklusion und Barrierefreiheit / Inclusion and Accessibility

- Folge dem Leitsatz `Programmierung #include<everyone>`: Lernmaterialien, Guides und erzeugte HTML-/API-Dokumentation muessen fuer Braille-Zeile, Screenreader und Textbrowser nutzbar bleiben.
- Follow `Programmierung #include<everyone>`: learner-facing material, guides, and generated HTML/API documentation must stay usable on Braille displays, with screen readers, and in text browsers.
- Fuer erzeugte HTML-Dokumentation gilt WCAG 2.2 Konformitaetsstufe AA als praktische Basis.
- For generated HTML documentation, WCAG 2.2 conformance level AA is the practical baseline.
- Nach jedem `docfx`-Neubau soll ein textorientierter A11y-Review folgen, bevorzugt mit Playwright + `@axe-core/playwright` und `lynx`.
- After every `docfx` regeneration, a text-oriented accessibility review should follow, preferably with Playwright + `@axe-core/playwright` and `lynx`.
- Fuer lokale DocFX-A11y-Pruefpfade sollen `Node 24 LTS`, `npm`, Playwright, `@axe-core/playwright` und `lynx` als gepflegte Voraussetzung verfuegbar sein.
- For local DocFX accessibility review paths, keep `Node 24 LTS`, `npm`, Playwright, `@axe-core/playwright`, and `lynx` available as maintained prerequisites.

## Spec-kit-Workflow

Neue Features in diesem Workspace werden nach dem **Specification-Driven Development (SDD)**-Workflow entwickelt.
Der Workflow verwendet das `speckit`-CLI-Tool (GitHub Copilot Skill).

Schritte für ein neues Feature:

1. **Spezifikation erstellen** — `speckit specify "Feature-Name"` → `specs/{branch}/spec.md`
2. **Klärungsfragen** — `speckit clarify` → offene Fragen in `spec.md` beantworten
3. **Implementierungsplan** — `speckit plan` → `specs/{branch}/plan.md`
4. **Aufgabenliste** — `speckit tasks` → `specs/{branch}/tasks.md`
5. **Implementieren** — `speckit implement` → Aufgaben aus `tasks.md` abarbeiten
6. **Validieren** — `bash scripts/check-homogeneity.sh` → Compliance-Score prüfen

Alle Spec-Artefakte werden im Branch-Verzeichnis `specs/{branch}/` gespeichert und versioniert.

---

## Spec-kit Workflow

New features in this workspace are developed following the **Specification-Driven Development (SDD)** workflow.
The workflow uses the `speckit` CLI tool (GitHub Copilot Skill).

Steps for a new feature:

1. **Create specification** — `speckit specify "Feature Name"` → `specs/{branch}/spec.md`
2. **Clarification questions** — `speckit clarify` → answer open questions in `spec.md`
3. **Implementation plan** — `speckit plan` → `specs/{branch}/plan.md`
4. **Task list** — `speckit tasks` → `specs/{branch}/tasks.md`
5. **Implement** — `speckit implement` → work through tasks in `tasks.md`
6. **Validate** — `bash scripts/check-homogeneity.sh` → check compliance score

All spec artefacts are stored and versioned in the branch directory `specs/{branch}/`.

---

## Homogeneity Guardian — Skript-Kurzreferenz / Script Quick Reference

### `scripts/check-homogeneity.sh` / `scripts/check-homogeneity.ps1`

Prüft dieses Projekt auf Compliance (constitution.md, A11Y, Spec-kit, Azubis-Abschnitte, STATS.md).
*Checks this project for compliance (constitution.md, A11Y, Spec-kit, Azubis sections, STATS.md).*

```bash
bash scripts/check-homogeneity.sh

# JSON-Ausgabe für CI/Scripting / JSON output for CI/scripting
bash scripts/check-homogeneity.sh --json
```

```powershell
pwsh scripts/check-homogeneity.ps1
pwsh scripts/check-homogeneity.ps1 -Json
```

---

### `scripts/init-stats.sh` / `scripts/init-stats.ps1`

Schreibt einen Baseline-Eintrag in `STATS.md`. Einmalig nach dem Einrichten ausführen.
*Writes a baseline entry to `STATS.md`. Run once after initial setup.*

```bash
bash scripts/init-stats.sh
```

```powershell
pwsh scripts/init-stats.ps1
```

---

### `scripts/rename-lastenheft.sh` / `scripts/rename-lastenheft.ps1`

Benennt eine Lastenheft-Datei via `git mv` um und committet — fügt Branch-Suffix hinzu.
*Renames a Lastenheft file via `git mv` and commits — adds branch suffix.*

```bash
# Datei umbenennen und committen / Rename and commit
bash scripts/rename-lastenheft.sh Lastenheft_foo.md 002-feature-branch
# Ergebnis / Result: Lastenheft_foo.002-feature-branch.md
```

```powershell
pwsh scripts/rename-lastenheft.ps1 -File Lastenheft_foo.md -Branch 002-feature-branch
```

---

### `scripts/install-hooks.sh` / `scripts/install-hooks.ps1`

Installiert den `pre-push`-Hook nach dem Clonen auf einem neuen Gerät.
*Installs the `pre-push` hook after cloning on a new device.*

```bash
bash scripts/install-hooks.sh
```

```powershell
pwsh scripts/install-hooks.ps1
```

## Für Azubis / For Apprentices

Willkommen! Diese Sektion beschreibt den Einstieg in die Entwicklungsumgebung
für Fachinformatiker-Azubis und andere Einsteiger.

**Voraussetzungen:**

- Git (macOS: `brew install git` / Windows: `winget install Git.Git`)
- PowerShell 7+ (Windows: `winget install Microsoft.PowerShell`)
- ripgrep (macOS: `brew install ripgrep` / Windows: `winget install BurntSushi.ripgrep.MSVC`)
- GitHub CLI (macOS: `brew install gh` / Windows: `winget install GitHub.cli`)

**Ersten Schritt ausführen:**

```bash
# Repository klonen
git clone <repo-url>
cd <projekt-verzeichnis>

# Hooks installieren
bash scripts/install-hooks.sh

# Compliance prüfen
bash scripts/check-homogeneity.sh
```

**Hilfreiche Befehle:**

| Befehl | Beschreibung |
|--------|--------------|
| `bash scripts/check-homogeneity.sh` | Compliance-Bericht anzeigen |
| `bash scripts/init-stats.sh` | Compliance-Baseline in STATS.md schreiben |
| `git log --oneline -10` | Letzte 10 Commits anzeigen |

Bei Fragen: Issue im GitHub-Repository erstellen oder Mentor ansprechen.

---

Welcome! This section describes how to get started with the development
environment for apprentice software developers (Fachinformatiker-Azubis) and
other beginners.

**Prerequisites:**

- Git (macOS: `brew install git` / Windows: `winget install Git.Git`)
- PowerShell 7+ (Windows: `winget install Microsoft.PowerShell`)
- ripgrep (macOS: `brew install ripgrep` / Windows: `winget install BurntSushi.ripgrep.MSVC`)
- GitHub CLI (macOS: `brew install gh` / Windows: `winget install GitHub.cli`)

**First steps:**

```bash
# Clone the repository
git clone <repo-url>
cd <project-directory>

# Install hooks
bash scripts/install-hooks.sh

# Check compliance
bash scripts/check-homogeneity.sh
```

**Useful commands:**

| Command | Description |
|---------|-------------|
| `bash scripts/check-homogeneity.sh` | Show compliance report |
| `bash scripts/init-stats.sh` | Write compliance baseline to STATS.md |
| `git log --oneline -10` | Show last 10 commits |

For questions: open an issue in the GitHub repository or ask your mentor.
