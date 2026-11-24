# TinyPl0 - Copilot Instructions

## Einstieg und Detailregeln / Entry point and detailed rules

DE: Lies die Projektkonstitution und vor der Arbeit die [Detailregeln](../docs/agent-guidance.md)
für deine Aufgabe. Sie ergänzen die verbindlichen Arbeits-, Sicherheits-, Prüf- und Freigaberegeln.

EN: Read the project constitution and the relevant [detailed rules](../docs/agent-guidance.md)
before working. They supplement the binding work, security, verification and approval rules.

This project is a port of the historical PL/0 compiler (originally in Pascal) to C# on .NET 11. It serves as a pedagogical reference for compiler construction in the German vocational training context (Fachinformatiker-Ausbildung).

## Project Overview

**Purpose:** A functional and didactic implementation of the PL/0 compiler and its P-Code virtual machine.

**Architecture:**
- `Pl0.Core`: Lexer, Parser, Symbol Table, and Code Generator
- `Pl0.Vm`: Stack-based virtual machine that executes P-Code instructions
- `Pl0.Cli`: Command-line interface for compiling and running PL/0 programs
- `Pl0.Ide`: Text-based IDE (TUI) using Terminal.Gui v2.x, modelled after the Turbo Pascal DOS IDE

**Supported Dialects:**
- `classic`: Strictly compatible with the original Pascal reference (no I/O statements `?` and `!`)
- `extended`: Includes `? ident` (input) and `! expression` (output) statements

## Build, Test, and Run Commands

### Build and Test
```bash
# Restore dependencies
dotnet restore

# Build the entire solution
dotnet build
dotnet build --configuration Release

# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run a specific test class
dotnet test --filter "FullyQualifiedName~Pl0.Tests.LexerTests"

# Run a single test method
dotnet test --filter "FullyQualifiedName~Pl0.Tests.LexerTests.Tokenizes_Simple_Assignment"
```

### CLI Usage
```bash
# Compile PL/0 source to P-Code
dotnet run --project src/Pl0.Cli -- compile <file.pl0> --out <file.pcode>

# Compile and run PL/0 source immediately
dotnet run --project src/Pl0.Cli -- run <file.pl0>

# Execute a pre-compiled P-Code file
dotnet run --project src/Pl0.Cli -- run-pcode <file.pcode>

# Show P-Code listing with symbolic opcodes
dotnet run --project src/Pl0.Cli -- run <file.pl0> --list-code --wopcod

# Compile only, skip VM execution
dotnet run --project src/Pl0.Cli -- run <file.pl0> --conly

# Emit assembly to STDOUT
dotnet run --project src/Pl0.Cli -- run <file.pl0> --emit asm

# Show long error messages
dotnet run --project src/Pl0.Cli -- run <file.pl0> --errmsg

# Start embedded docs server
dotnet run --project src/Pl0.Cli -- --api
```

### Golden Master Tests
The project uses Golden Master tests for lexer and parser outputs. To update golden artifacts after intentional changes (requires `jq`):
```bash
./scripts/update-golden-code.sh
```

## Git Workflow

- `main` is protected — never commit or push directly to `main`.
- For every change, create a new branch first. Agent/bot branch naming: `codex/<topic>`.
- Open a pull request to merge into `main`.
- Once a dedicated feature branch has implemented a Lastenheft, rename that file to `Lastenheft_<topic>.<feature-branch>.md` so the delivered scope stays traceable.

## Key Documentation Files

- `Pflichtenheft.md`: Canonical requirements index
- `requirements/baseline/Pflichtenheft_PL0_CSharp_DotNet10.pre-intake-split.2026-07-26.md`: Historical requirements and technical specification
- `docs/project-statistics.md`: Living project statistics ledger with code/doc counts and manual-effort baseline

## Agentische Skriptausfuehrung / Agentic Script Execution

- Vor jeder Automationsaufgabe zuerst das Betriebssystem pruefen. Wenn ein passendes PowerShell-7-Skript oder Cmdlet vorhanden ist und `pwsh` verfuegbar ist, diese Variante bevorzugen. Fuer strukturierte lokale Automationen ist C# ueber `.NET` oder `mono` ein zulaessiger zweiter Weg, wenn Typisierung, Dateiformate oder Wiederverwendbarkeit dadurch klar besser werden. Erst wenn PowerShell oder C# nicht sinnvoll passen, die OS-nahe vorhandene Repo-Variante nutzen, auf macOS/Linux typischerweise Bash. Keine neue Sprache nur aus Bequemlichkeit einfuehren, wenn ein bestehendes Repo-Skript denselben Zweck erfuellt.
- Detect the operating system before each automation task. If a matching PowerShell 7 script or cmdlet exists and `pwsh` is available, prefer that variant. For structured local automation, C# via `.NET` or `mono` is an acceptable second option when type safety, file formats, or reuse clearly benefit from it. Only when PowerShell or C# is not a good fit, use the existing OS-native repository variant, typically Bash on macOS/Linux. Do not introduce a new language merely for convenience when an existing repository script already solves the task.

## Key Files

- `docs/ARCHITECTURE.md`: High-level system architecture
- `docs/LANGUAGE_EBNF.md`: Formal grammar of PL/0 dialects
- `docs/VM_INSTRUCTION_SET.md`: P-Code instruction set reference
- `docs/TRACEABILITY_MATRIX.md`: Mapping between requirements and tests
- `docs/QUALITY.md`: Code quality and coverage metrics
- `pl0c.pas`: Historical Pascal reference source

## Pl0.Ide — TUI IDE

`Pl0.Ide` is a Terminal.Gui v2.x application modelled after the Turbo Pascal DOS IDE. It uses the instance-based v2 lifecycle (`Application.Create().Init()` / `app.Run<T>()` / `app.Dispose()`). Static `Application` calls from v1 must not be used.

**IDE components:** source editor, P-Code viewer, assembler viewer, runtime output window, debug window (registers P/B/T + stack), diagnostics window.

### IDE Version Scheme

`<Version>` in `src/Pl0.Ide/Pl0.Ide.csproj` follows `Major.Minor.Patch.Build`:
- `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`002` -> `2`) and used immediately even before a GitHub PR exists
- `Patch` = current commit count in that feature/PR branch (after committing the current change)
- `Build` = manual build counter incremented before every `dotnet build` or `dotnet test`

Whenever a commit is created or the PR branch is updated, align `Version`, `AssemblyVersion`, and `FileVersion` in `Pl0.Ide.csproj` to this scheme before pushing.

### IDE Worklog

After IDE-related work, append a new entry to `docs/ide-worklog.md`; keep the frozen IDE Pflichtenheft baseline read-only.

<!-- requirements-intake-governance:begin -->
## Requirements- und Intake-Governance / Requirements and Intake Governance

- `Pflichtenheft.md` ist der schlanke Einstieg; verbindliche Reihenfolge und Zustände stehen im Serienmanifest unter `requirements/intakes/series/`.
- Aktive, abgeschlossene, ersetzte und optionale Intakes bleiben getrennt. Die Baselines unter `requirements/baseline/` sind read-only.
- Nur `Eligible` bezeichnet den bevorzugten nächsten Intake. Der Zustand erteilt keine Ausführungs-, Remote- oder Merge-Berechtigung.

*`Pflichtenheft.md` is the slim entry point; binding order and states live in the series manifest. Active, completed, superseded, and optional intakes remain separated, and baselines are read-only. `Eligible` grants no execution, remote, or merge authority.*
<!-- requirements-intake-governance:end -->

## Important Notes

- **Language:** Learner-facing documentation and comments are bilingual: German first, English second, both at CEFR/GER B2 readability; large normative documents may use synchronized `.EN.md` sidecars while the German version remains canonical unless explicitly marked otherwise.
- **Pedagogy:** The code prioritizes clarity and didactic value over performance optimizations.
- **No language extensions:** Do not add features beyond the consolidated PL/0 definition.
- **No optimizations:** No peephole optimization, SSA, or similar compiler optimizations.
- **VM target only:** No JIT or IL backend; always target the P-Code VM.

## Inclusion & Accessibility

- Follow `Programmierung #include<everyone>`: Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI), die auf Deutsch und Englisch arbeiten, **sowie** an sehbehinderte Lernende, die Braille-Displays, Screen-Reader oder Textbrowser nutzen. Barrierefreiheit ist Pflichtanforderung, kein Nice-to-have. / *These learning examples target apprentices working in German and English, **and** visually impaired learners using Braille displays, screen readers, or text browsers. Accessibility is mandatory.*
- Treat WCAG 2.2 conformance level AA as the practical baseline for generated HTML documentation.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- If `docfx` output is regenerated, follow it with a text-oriented accessibility review, preferably with Playwright + `@axe-core/playwright` and a `lynx` cross-check.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Treat every successful `docfx` regeneration as incomplete until the matching text-oriented A11Y review has also been handled in the same work item; keep the `lynx` text-browser path visible next to the Playwright/axe review.
- Recommended A11y toolchain for DocFX-based repos: Node 24 LTS, `npm`, Playwright, `@axe-core/playwright`, and `lynx`.

## Gemeinsame Governance-Ergaenzung / Shared Governance Addendum

- Alle nutzerseitigen Artefakte muessen barrierefrei gedacht und geprueft werden: CLI-Ausgaben, Dokumentation, HTML, UI und generierte Templates; WCAG 2.2 Level AA ist die Standard-Basis, sobald die Kriterien auf das Artefakt anwendbar sind.
- All user-facing artefacts must be designed and reviewed for accessibility: CLI output, documentation, HTML, UI, and generated templates; WCAG 2.2 Level AA is the default baseline wherever the criteria apply.

- Fuer C#/.NET-Repositories gilt standardmaessig eine Thorsten-Solo-Basis von `125` Zeilen/Arbeitstag, sofern das Repo keinen abweichenden, begruendeten Wert dokumentiert.
- The default Thorsten-solo baseline for C#/.NET repositories is `125` lines/workday unless the repository documents a justified deviation.

## Level-2-Umgebungsregister / Level-2 Environment Registry

- Die zentrale `constitution.md` enthält das verbindliche Level-2 Project Environment Registry.
- Spec-Kit-Pläne und Copilot-Arbeit in Level-2-Projekten müssen die passende Registry-Zeile als verbindlichen Kontext für Runtime, Build/Test, A11Y, Statistik und Agentenflächen verwenden.
- Änderungen an einer Level-2-Runtime, Toolchain oder Statistik-Basis müssen `constitution.md`, `.specify/memory/constitution.md` und betroffene KI-Agenten-Dateien gemeinsam prüfen.

*The central `constitution.md` contains the binding Level-2 Project Environment Registry. Spec-Kit plans and Copilot work in Level-2 projects must use the matching registry row as binding context for runtime, build/test, A11Y, statistics, and agent surfaces. Changes to Level-2 runtime, toolchain, or statistics baselines require a joint review of `constitution.md`, `.specify/memory/constitution.md`, and affected AI-agent files.*

## Sicherheitsdokumentation / Security Documentation (XII–XVIII Extensions)

- Jedes Level-2-Projekt MUSS die folgenden Sicherheitsdokumente pflegen, basierend auf den Templates in `.specify/templates/`:
  - **Bedrohungsmodell / Threat Model** (`threat-model-template.md`) — STRIDE-Methodik, Trust Boundaries, Risikobewertung, CAPEC-Referenzen (Prinzip XIII + XVII)
  - **Security Architecture Decision Records (S-ADR)** (`adr-template.md`) — architektonische Sicherheitsentscheidungen mit Compliance-Nachweis (Prinzip XIII)
  - **arc42 Section 8 Sicherheits-Querschnittskonzepte** (`arc42-security-template.md`) — Authentifizierung, Autorisierung, Verschlüsselung, Eingabevalidierung, Fehlerbehandlung, Logging, Abhängigkeiten, Deployment (Prinzip XIII)
  - **Sicherheits-Checkliste / Security Checklist** (`security-checklist-template.md`) — sprachspezifische Code-Review-Checkliste (Prinzip XII)
  - **Abhängigkeits-Audit / Dependency Audit** (`dependency-audit-template.md`) — CVE-Tracking, Lizenz-Compliance, Supply-Chain-Sicherheit (Prinzip XII)
  - **Sicherheits-Qualitätsszenarien / Security Quality Scenarios** (`security-quality-scenarios-template.md`) — iSAQB CPSA-F Qualitätsszenario-Methodik (Prinzip XII + XIII, SHOULD)
  - **ASVS-Verifikation / ASVS Verification** (`asvs-verification-template.md`) — OWASP ASVS Level, Scope und Evidenz (Prinzip XV, Web-/API-Projekte MUST)
  - **Supply-Chain-Evidenz / Supply Chain Evidence** (`supply-chain-evidence-template.md`) — SBOM, AI-SBOM, VEX, SLSA, OpenSSF Scorecard (Prinzip XVI, releasefähige Projekte MUST; AI-SBOM nur bei KI-Runtime-/Produktkomponenten)
  - **Zero-Trust-Anwendbarkeit / Zero Trust Applicability** (`zero-trust-applicability-template.md`) — NIST SP 800-207-Bewertung (Prinzip XVIII, verteilte Systeme SHOULD)
  - **SAMM-Bewertung / SAMM Assessment** (`samm-assessment-template.md`) — OWASP SAMM Reifegrad und Verbesserungsplan (Prinzip XVIII, langlebige Projekte SHOULD)
- Projektspezifische Instanzen werden in `docs/security/` gepflegt; S-ADRs als einzelne Dateien in `docs/security/adr/`.

*Every Level-2 project MUST maintain security documents based on templates in `.specify/templates/`: threat model (STRIDE+CAPEC), S-ADRs, arc42 Section 8 security concepts, security checklist, dependency audit, security quality scenarios (SHOULD), ASVS verification (web/API MUST), supply-chain evidence (release-capable MUST; AI-SBOM when AI runtime/product components apply), Zero Trust applicability note (distributed systems SHOULD), and SAMM assessment (long-lived projects SHOULD). Project-specific instances live in `docs/security/`; S-ADRs in `docs/security/adr/`. See `constitution.md`, Principles XII–XVIII for authoritative requirements.*

## Sicherheitsstandards & Anwendbarkeit / Security Standards & Applicability

- Vor jeder Level-2-Aufgabe die anwendbaren Sicherheitsstandards aus `constitution.md`, Prinzipien XIV-XVIII bestimmen und explizit benennen.
- `NIST SSDF` und `CWE Top 25` gelten immer für Level-2-Arbeit.
- `OWASP ASVS` gilt für Web-, API-, HTTP- und authentifizierte Dienste; der gewählte ASVS-Level muss benannt werden.
- `SBOM` gilt für releasefähige oder verteilbare Artefakte; `VEX`, wenn bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten bewertet werden müssen.
- `AI-SBOM` gilt projektartabhängig bei KI-Modellen, KI-Diensten, Trainings-/Embedding-Daten, Inferenz-Infrastruktur oder KI-Runtime-Komponenten im ausgelieferten oder betriebenen System; reine Entwicklungswerkzeug-Nutzung wird als `N/A` mit Toolchain-Begründung dokumentiert.
- `SLSA` gilt als Soll-Vorgabe für CI/CD- oder veröffentlichte Artefakte; `Zero Trust` ist für verteilte, servicebasierte, cloudnahe oder remote-verwaltete Systeme explizit zu prüfen.
- `CAPEC` soll in Bedrohungsmodellen für die risikoreichsten Angriffswege verwendet werden; `OWASP SAMM` soll für langlebige Projekte/Workspaces in Verbesserungspläne einfließen.
- `OWASP Cheat Sheet Series`, `OWASP Proactive Controls` und bei öffentlichen OSS-Repositories oder kritischen Abhängigkeiten `OpenSSF Scorecard` sind als ergänzende Referenzen zu berücksichtigen.
- Nichtanwendbarkeit immer als `N/A` mit kurzer Begründung dokumentieren; keine stillschweigende Auslassung.

*At the start of every Level-2 task, determine and name the applicable security standards from `constitution.md`, Principles XIV-XVIII. `NIST SSDF` and `CWE Top 25` always apply. `OWASP ASVS` applies to web/API/HTTP/auth-bearing services; `SBOM` applies to releasable or distributable artefacts; `AI-SBOM` applies when AI models, AI services, datasets, inference infrastructure, or AI runtime components are part of the released or operated system; `VEX` applies when known vulnerabilities in shipped/evaluated components need a disposition statement. `SLSA` is the target model for CI/CD and published artefacts; `Zero Trust` must be explicitly evaluated for distributed, service-based, cloud, or remotely managed systems. `CAPEC`, `OWASP SAMM`, `OWASP Cheat Sheet Series`, `OWASP Proactive Controls`, and `OpenSSF Scorecard` are supporting references where relevant. Record non-applicability as `N/A` with justification rather than omitting it silently.*

## Agentischer Architektur-Workflow / Agentic Architecture Workflow

- In `spec.md`, `plan.md` und `tasks.md` festhalten, ob Systemkontext, Schnittstellen, Building Blocks, Laufzeitverhalten, Deployment oder technische Schulden betroffen sind.
- Bei architekturrelevanten Änderungen passende Evidenz unter `docs/architecture/` planen und pflegen: Context View, Building-Block View, Runtime View, Deployment View, Quality Scenarios, ADRs sowie Architektur-Risiken.
- Für architekturell signifikante Entscheidungen ADRs in `docs/architecture/adr/` anlegen oder aktualisieren.
- Qualitätsanforderungen als konkrete Szenarien formulieren statt als unscharfe Adjektive.
- Wenn Sicherheitsarchitektur betroffen ist, die Evidenzpfade unter `docs/security/` zusätzlich gemeinsam mit der allgemeinen Architektur-Evidenz aktualisieren.

*In `spec.md`, `plan.md`, and `tasks.md`, record whether system context, interfaces, building blocks, runtime behavior, deployment, or technical debt are affected. For architecture-relevant changes, plan and maintain the matching evidence under `docs/architecture/`: context, building-block, runtime, deployment, quality-scenario, ADR, and architecture-risk artefacts. Create or update ADRs in `docs/architecture/adr/` for significant decisions. Express quality requirements as concrete scenarios rather than vague adjectives. If security architecture is affected, update the `docs/security/` evidence path together with the general architecture evidence.*

## GitHub/GitLab CLI First / GitHub/GitLab CLI zuerst

Für GitHub-Repositories zuerst die authentifizierte `gh` CLI für mögliche Schreibaktionen und Live-Repository-Operationen verwenden, einschließlich PR-/Issue-Kommentaren, PR-Statusprüfungen, Review-Follow-up, Workflow-Prüfung und Merge-/Statusabfragen. GitHub-Connector-Tools hauptsächlich für strukturierte Read-only-Inspektion oder Fälle nutzen, in denen die CLI nicht geeignet ist.

Für GitLab-Repositories die authentifizierte `glab` CLI zuerst für gleichwertige Aktionen verwenden. Bekanntermaßen fehlschlagende Connector-Schreibwege nicht wiederholt versuchen, wenn `gh`/`glab` die Aufgabe direkt erledigen kann.

For GitHub repositories, use the authenticated `gh` CLI first for feasible write actions and live repository operations, including PR/issue comments, PR status checks, review follow-up, workflow inspection, and merge/status queries. Use GitHub connector tools mainly for structured read-only inspection or when the CLI is not suitable.

For GitLab repositories, use the authenticated `glab` CLI first for equivalent actions. Do not repeatedly try connector write paths that are known to fail when `gh`/`glab` can perform the task directly.

<!-- learner-a11y-baseline:start -->
## Hinweise zur Lernenden- und A11Y-Basis / Notes on the Learner and A11Y Baseline

- Verbindliche Zielgruppen ab dem ersten Ausbildungsjahr sind
  Fachinformatiker*innen, Kaufleute für IT-System-Management und Kaufleute für
  Digitalisierungsmanagement.
- Lern-, Bedien-, Governance- und Spec-Kit-Inhalte stehen auf Deutsch zuerst
  und Englisch danach, verwenden ungefähr CEFR B2 und erklären Fachbegriffe
  beim ersten Auftreten.
- Spec-Kit-Erfahrung wird nicht vorausgesetzt. Befehle, Artefakte, Zustände und
  Übergänge werden beim ersten Gebrauch verständlich eingeführt.
- Abhängigkeiten, Zustände und Entscheidungen erhalten eine vollständige
  textorientierte Erklärung; eine ausschließlich visuelle Darstellung genügt
  nicht.
- `Programmierung #include<everyone>` und WCAG 2.2 Level AA gelten als
  verbindliche Prüfbasis, soweit die Kriterien auf das Artefakt anwendbar sind.

*The binding audience starts in the first training year and includes IT
specialist apprentices and both IT management occupations. Learner, usage,
governance, and Spec Kit content is German-first/English-second at about CEFR
B2, explains technical terms at first use, assumes no prior Spec Kit
experience, and never relies on visual-only dependency, state, or decision
information. `Programmierung #include<everyone>` and WCAG 2.2 Level AA are the
review baseline wherever applicable.*
<!-- learner-a11y-baseline:end -->

<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->

<!-- statistics-profile-2-guidance:begin -->
## Statistikprofil 2 / Statistics Profile 2

- Verbindlich sind `docs/project-statistics.config.json` und der markierte Profil-2-Block in `docs/project-statistics.md`; aktualisieren mit `render-project-statistics.*`.
- Profil 2 zeigt exakte KPI, Artefaktmix, 52-Wochen-Aktivitaet, Wochen- und kumulatives Volumen, belastbare Phasen oder Monatsfallback sowie Speedup-Vergleiche.
- Nur ASCII verwenden: Heatmap `0..4`, `-` fuer noch nicht abgelaufene Tage und Gauges `#`/`.`; jedes Textdiagramm bleibt hoechstens 100 Zeichen breit.
- Jede Grafik braucht genaue Zahlen und eine bilinguale CEFR-B2-Textalternative, Deutsch zuerst und Englisch danach.
- Methodik v2 wertet Git-getrackten Text und Bruttoaenderungen aus Nicht-Merge-Commits aus; Ledger, `STATS.md` und Binaerdaten bleiben ausgeschlossen.
- Referenzen dieses Repositories: `80` Zeilen/Arbeitstag konservativ und `125` Zeilen/Arbeitstag Thorsten-Solo. Speedup bleibt Lieferdichte, keine Stoppuhr- oder Personenbewertung.
- Dieser Vertrag ersetzt aeltere Visualisierungsvorgaben; historische Ledger-Eintraege und archivierte Profil-1-Diagramme bleiben unveraendert.
- Gemeinsame Aenderungen werden synchron in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.github/agents/copilot-instructions.md` gepflegt.

*Profile 2 is governed by the JSON configuration and generated marker block. Use ASCII `0..4`, `-`, and `#`/`.`, exact values, German-first bilingual CEFR-B2 alternatives, and a 100-character chart limit. Methodology v2 excludes the ledger, `STATS.md`, and binaries. This repository uses manual references of `80` and `125` lines per workday. Speedup describes delivery density, not stopwatch or personal performance. This contract supersedes older visualization rules while retaining historical entries and archived Profile 1 charts.*
<!-- statistics-profile-2-guidance:end -->

<!-- project-statistics-rollout:begin -->
## Statistik-Preset und Pflege / Statistics preset and maintenance

Das aktuelle Projektprofil ist `project-statistics-fourteen-governance-presets`:
bestehende 13er-Basis plus Statistik v0.1.0 bei Prioritaet 90. Globale Defaults
bleiben erhalten; die operative lokale Zuordnung folgt erst nach Lieferung.
`docs/project-statistics/config.json` steuert den getrennten UTC-/52-Wochen-Kontext.
Profil 2 bleibt kanonisch; Referenzmodelle im neuen Kontext bleiben aus.
Nach abgeschlossenem Feature/Implementierungsabschnitt Inhalte zuerst committen,
Update ausdruecklich beauftragen und vorab im Dry-Run pruefen. Beide Statistiken
aus derselben Inhaltsrevision pflegen, Ausgaben committen und lesend verifizieren.
Keine Personen-, Lernleistungs-, Qualitaets-, Sicherheits- oder KI-Produktivitaetsbewertung.
Bedienung: `docs/project-statistics/README.md`; Quellen und Abnahme:
`docs/maintenance/project-statistics-rollout-v010.md`. Kein automatischer
Spec-Kit-Lauf und keine Commit-/Push-/Merge- oder menschliche Freigabe durch das Preset.

The current profile adds statistics v0.1.0 at priority 90 to the unchanged
thirteen-preset base. Preserve global defaults and defer operational assignment
until delivery. Keep legacy Profile 2 authoritative; use the separate UTC context
with reference models disabled. After feature/implementation completion, commit
content first, preview an expressly authorized update, render both contexts,
commit outputs and verify read-only status. Statistics do not rate people,
learning, quality, security or AI productivity. See the repository-relative
usage and rollout records. Installation grants no feature or delivery authority.
<!-- project-statistics-rollout:end -->

<!-- tinypl0-pedagogy-governance:begin -->
## Didaktische und sprachliche Klarheit / Pedagogical and Linguistic Clarity

- Neue oder geänderte lernendenseitige Inhalte stehen Deutsch zuerst und
  Englisch danach, verwenden CEFR B2 und bleiben text-first. WCAG 2.2 Level AA
  ist die Prüfbasis, soweit die Kriterien anwendbar sind.
- Jede extern öffentliche C#-API erhält vollständige, fachlich anwendbare
  XML-Dokumentation. Lokale Variablen, private Details und generierter Bestand
  bleiben ausgeschlossen; CS1591 darf nicht global oder projektweit
  unterdrückt werden.
- Geänderte API-Signaturen oder XML-Kommentare verlangen im selben
  Arbeitsgegenstand DocFX sowie eine textorientierte A11Y-Prüfung mit
  Playwright/axe und `lynx`.
- Neue oder geänderte nicht-triviale Logik wird auf kurze zweisprachige
  Warum-Kommentare geprüft, die Entscheidungen, Abwägungen oder Nachweisgrenzen
  erklären.
- Neue Funktionen und Fehlerkorrekturen belegen beobachtbar TDD Rot, Grün und
  Regression. Reine Governance- oder Textarbeit erhält ein begründetes `N/A`
  mit Wiedervorlage bei der nächsten Logikänderung.

*New or changed learner-facing content is German-first and English-second at
CEFR B2, remains text-first, and uses WCAG 2.2 Level AA where applicable. Every
externally public C# API receives complete applicable XML documentation; locals,
private details, and generated surfaces stay excluded, and CS1591 is not
suppressed globally or per project. API-signature or XML-comment changes require
DocFX plus Playwright/axe and `lynx` evidence in the same work item. Changed
non-trivial logic is reviewed for concise bilingual why-comments. New features
and fixes record observable TDD red, green, and regression evidence; pure
text/governance work records a reasoned `N/A` and re-evaluation trigger.*
<!-- tinypl0-pedagogy-governance:end -->

<!-- BEGIN spec-kit-diagrams-completion -->
## Mermaid und Spec-Kit-Abschlussbericht / Mermaid and Spec Kit completion report

Neue Lastenhefte enthalten bei hilfreichen Abläufen, Zuständen oder Abhängigkeiten
lesbaren Mermaid-Quelltext im Markdown und eine gleichwertige Textalternative.
Bei einfachen Inhalten die Nichtanwendung kurz begründen. Diagramme bilden die
verbindlichen Text-/Manifestquellen ab; Farbe allein trägt keine Bedeutung.
Nach jedem vollständig abgeschlossenen Spec-Kit-Feature-Lauf den vollständigen
Ergebnisbericht im Chat anzeigen und im Feature-Verzeichnis als
`completion-report.md` versionieren. Einzelne Planungs-/Status-/Review-Kommandos
lösen keinen solchen Bericht aus; blockierte oder pausierte Läufe als
Zwischenbericht kennzeichnen. Vorlage: `.specify/templates/completion-report-template.md`;
Regel: `docs/spec-kit-diagrams-and-completion-reports.md`.
Ergebnis, Tests, Dokumentation, Git-gebundene Umfangszahlen, Verlauf und Restpunkte
belegen. Programmlogik von generierter Evidence, Git-Wandzeit von aktiver
Arbeitszeit und bestandene von ausgefallenen Prüfungen unterscheiden. Finale
Merge-/Sync-Evidence im Chat und bestehenden Closeout-Nachweis ergänzen; keine
zusätzlichen Commits allein für selbstreferenzielle Berichts-/Statistikwerte.
Diese Projektregel und lokale Vorlagen bei Spec-Kit-Updates erhalten.

*New intakes use readable Mermaid Markdown and equivalent text alternatives for
useful workflows, states or dependencies; justify omission for simple content.
Diagrams reflect authoritative text/manifests and never rely on color alone.
After each completed feature run, show the full outcome report in chat and
version completion-report.md in the feature directory using the shared template
and rule above. Individual planning/status/review commands do not trigger it;
paused/blocked runs receive interim reports. Evidence outcomes, tests, docs,
Git-bound counts, delivery history and remaining work. Distinguish code from
volume generated as evidence, elapsed from active time, and passed from failed
checks. Add final merge/sync proof in chat and existing closeout evidence, without
commits solely for self-referential counts or IDs. Preserve local rules/templates
across Spec Kit updates.*
<!-- END spec-kit-diagrams-completion -->
