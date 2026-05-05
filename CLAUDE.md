# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TinyPl0 is a C# .NET 10 port of the historical PL/0 compiler (originally in Pascal). It's a pedagogical reference for compiler construction targeting German vocational IT training (Fachinformatiker-Ausbildung). Learner-facing documentation and comments are bilingual (German first, English second) with CEFR/GER B2 readability.

## Build, Test, and Run Commands

```bash
# Restore, build, test
dotnet restore
dotnet build
dotnet test

# Run specific test class or method
dotnet test --filter "FullyQualifiedName~Pl0.Tests.LexerTests"
dotnet test --filter "FullyQualifiedName~Pl0.Tests.LexerTests.Tokenizes_Simple_Assignment"

# Build Release
dotnet build --configuration Release

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Update golden master artifacts after intentional changes (requires jq)
./scripts/update-golden-code.sh
```

### CLI Usage

```bash
dotnet run --project src/Pl0.Cli -- compile <file.pl0> --out <file.pcode>
dotnet run --project src/Pl0.Cli -- run <file.pl0>
dotnet run --project src/Pl0.Cli -- run-pcode <file.pcode>
dotnet run --project src/Pl0.Cli -- run <file.pl0> --list-code --wopcod
dotnet run --project src/Pl0.Cli -- run <file.pl0> --conly        # compile only, skip VM
dotnet run --project src/Pl0.Cli -- run <file.pl0> --emit asm     # emit assembly to STDOUT
dotnet run --project src/Pl0.Cli -- run <file.pl0> --errmsg       # show long error messages
dotnet run --project src/Pl0.Cli -- --api                         # start embedded docs server

# Start the TUI IDE
dotnet run --project src/Pl0.Ide
```

## Architecture

Four-module layered architecture with enforced dependencies (`ArchitectureGuardTests`):

```
Pl0.Cli  --> Pl0.Core  (compilation)
Pl0.Cli  --> Pl0.Vm    (execution)
Pl0.Core --> (no dependencies)
Pl0.Vm   --> Pl0.Core  (for Instruction, Opcode types)
Pl0.Ide  --> Pl0.Core
Pl0.Ide  --> Pl0.Vm
Pl0.Ide  --> Terminal.Gui  (only allowed external NuGet package for Pl0.Ide)
```

`ArchitectureGuardTests` also verifies that `Pl0.Ide.csproj` references only `Terminal.Gui` as a NuGet package. Do not violate these dependency rules.

**Compiler pipeline:**
```
.pl0 source → Pl0Lexer → tokens → Pl0Parser → P-Code Instructions + Diagnostics
                                                     ↓
                                            PCodeSerializer → .pcode file (optional)
                                                     ↓
                                            VirtualMachine → execution
```

**Pascal-to-C# mapping:**

| Pascal | C# |
|--------|-----|
| `getsym/getch` | `Pl0Lexer` |
| `block/statement/condition/expression` | `Pl0Parser` |
| `enter/position/table` | `SymbolTable` + `SymbolEntry` |
| `gen` | `Pl0Parser.Emit()` |
| `interpret` | `VirtualMachine.Run()` |
| `base(l)` | `VirtualMachine.ResolveBase()` |

**VM registers:** `P` (Program Counter), `B` (Base Pointer), `T` (Top of Stack). Activation records reserve three cells: Static Link, Dynamic Link, Return Address.

**8 opcodes:** `Lit`, `Opr`, `Lod`, `Sto`, `Cal`, `Int`, `Jmp`, `Jpc`. See `docs/VM_INSTRUCTION_SET.md`.

**I/O abstraction:** `IPl0Io` interface with two implementations — `ConsolePl0Io` (default, for CLI/IDE) and `BufferedPl0Io` (for tests, provides pre-set input and captures output).

**Step debugging:** `SteppableVirtualMachine` wraps `VirtualMachine` and exposes `Step()` / `VmState` for the IDE debug view.

### Pl0.Ide — TUI IDE

`Pl0.Ide` is a Terminal.Gui v2.x application modelled after the Turbo Pascal DOS IDE. It uses the instance-based v2 lifecycle (`Application.Create().Init()` / `app.Run<T>()` / `app.Dispose()`). Static `Application` calls from v1 must not be used.

**IDE components:** source editor, P-Code viewer, assembler viewer, runtime output window, debug window (registers P/B/T + stack), diagnostics window.

**IDE Version Scheme** — `<Version>` in `src/Pl0.Ide/Pl0.Ide.csproj` follows `Major.Minor.Patch.Build`:
- `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`002` -> `2`) and used immediately even before a GitHub PR exists
- `Patch` = current commit count in that feature/PR branch (after committing the current change)
- `Build` = manual build counter incremented before every `dotnet build` or `dotnet test`

Align `Version`, `AssemblyVersion`, and `FileVersion` in `Pl0.Ide.csproj` whenever a commit is created or the PR branch is updated, before pushing.

**IDE Worklog** — After any IDE-related work, append a new entry to the worklog at the bottom of `Pflichtenheft_IDE.md`.

## Key Files

- `Pflichtenheft_PL0_CSharp_DotNet10.md` — main requirements specification
- `Pflichtenheft_IDE.md` — IDE requirements and worklog
- `docs/ARCHITECTURE.md` — Pascal-to-C# mapping and module overview
- `docs/LANGUAGE_EBNF.md` — formal grammar for both dialects
- `docs/VM_INSTRUCTION_SET.md` — P-Code instruction set reference
- `docs/TRACEABILITY_MATRIX.md` — requirements-to-test mapping
- `docs/QUALITY.md` — code quality and coverage metrics
- `tests/data/expected/catalog/cases.json` — 41 mandatory test cases
- `pl0c.pas` — historical Pascal reference source

## Code Conventions

- C# 14 features: collection expressions `[]`, primary constructors, `record` for immutable data
- Test classes: `public sealed class`, suffix `Tests`, use xUnit `[Fact]`
- Namespaces: all in `Pl0.*`
- 4-space indentation, opening brace on same line
- XML doc comments are required for changed APIs and must be complete where applicable (`<summary>`, `<param>`, `<returns>`, `<exception>`; `<remarks>`/`<example>` when useful)
- Missing public XML docs are build-breaking (CS1591 must not be globally suppressed); run `docfx` from repo root when API docs/signatures change

**Error handling — critical:** Do NOT throw exceptions during compilation. All errors go into `CompilerDiagnostic` / `LexerDiagnostic`. Check `CompilationResult.Success` (true when `Diagnostics.Count == 0`) before execution.

## Adding New Test Cases

To add a catalog test case, touch **all four** of these in order:

1. **Add the `.pl0` source file** to `tests/data/pl0/valid/` or `tests/data/pl0/invalid/`.
2. **Add a catalog entry** in `tests/data/expected/catalog/cases.json`:
   ```json
   {
     "name": "my_case.pl0",
     "group": "valid",
     "folder": "valid",
     "dialect": "extended",
     "compileSuccess": true,
     "run": true,
     "input": [],
     "expectedOutput": [42]
   }
   ```
   Key fields: `group` (`valid`/`invalid`/`compat`/`dialect`/`limits`/`runtime/io-edge`), `dialect` (`extended`/`classic`), `compileSuccess`, `run`, `runtimeSuccess` (default true), `input`/`expectedOutput` (integer lists), `expectedCompileCodes`/`expectedRuntimeCodes` (error code lists), `ioBehavior` (`buffered`/`eof`/`formatError`), `storeTrace`. Limit overrides: `maxLevel`, `maxAddress`, `maxIdentifierLength`, `maxNumberDigits`, `maxSymbolCount`, `maxCodeLength`.
3. **Generate the golden P-Code artifact** (only for `compileSuccess: true` cases):
   ```bash
   ./scripts/update-golden-code.sh   # requires jq
   ```
   This writes `tests/data/expected/code/<case-name>.pcode.txt`.
4. **Update the traceability matrix** at `tests/data/expected/traceability/matrix.json` if the new case covers a language rule or VM opcode not yet listed there. `TraceabilityMatrixTests` enforces that every rule in the matrix references at least one catalog case.

Golden artifact locations:
- `tests/data/expected/code/` — P-Code assembly for each compile-success case
- `tests/data/expected/lexer/` — token streams for lexer golden tests
- `tests/data/expected/traceability/matrix.json` — rule-to-case coverage map

## Dialect Handling

- `classic`: Strict Pascal compatibility, no `?`/`!` I/O statements, identifiers must start with lowercase
- `extended`: Adds `? ident` (input) and `! expression` (output)

Use `CompilerOptions.Dialect` when modifying parser/lexer. Classic mode must stay strictly compatible with `pl0c.pas`.

## Historical Quirks (Preserve)

- `[` maps to `<=`, `]` maps to `>=`
- Max identifier length: 10 characters; max number digits: 14; max nesting: 3 levels
- Only integer type; no procedure parameters or return values

## Git Workflow

- `main` is protected — no direct commits or pushes; all changes via pull request.
- Create a new branch for each change before starting work.
- Once a dedicated feature branch has implemented a Lastenheft, rename that file to `Lastenheft_<topic>.<feature-branch>.md` so the delivered scope stays traceable.

## Constraints

- No language extensions beyond the PL/0 definition
- No compiler optimizations (no peephole, SSA, etc.)
- VM target only — no JIT or IL backend
- Code prioritizes clarity over performance (pedagogical project)


## Gemeinsame Governance-Ergaenzung / Shared Governance Addendum

- Alle nutzerseitigen Artefakte muessen barrierefrei gedacht und geprueft werden: CLI-Ausgaben, Dokumentation, HTML, UI und generierte Templates; WCAG 2.2 Level AA ist die Standard-Basis, sobald die Kriterien auf das Artefakt anwendbar sind.
- All user-facing artefacts must be designed and reviewed for accessibility: CLI output, documentation, HTML, UI, and generated templates; WCAG 2.2 Level AA is the default baseline wherever the criteria apply.

- Fuer C#/.NET-Repositories gilt standardmaessig eine Thorsten-Solo-Basis von `125` Zeilen/Arbeitstag, sofern das Repo keinen abweichenden, begruendeten Wert dokumentiert.
- The default Thorsten-solo baseline for C#/.NET repositories is `125` lines/workday unless the repository documents a justified deviation.

## Active Technologies
- C# 14 / .NET 10 + .NET SDK (`.resx`, `ResourceManager`, `CultureInfo`) — kein zusätzliches NuGet-Pake (001-l10n-backend)
- Satellite Assemblies (automatisch vom SDK aus `.resx` generiert) (001-l10n-backend)

## Recent Changes
- 001-l10n-backend: Added C# 14 / .NET 10 + .NET SDK (`.resx`, `ResourceManager`, `CultureInfo`) — kein zusätzliches NuGet-Pake

## Project Statistics

- When shared AI-agent guidance, workflow conventions, or statistics methodology changes, review and update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md` together when they are affected.
- Shared guidance must not be updated in only one of these files; any intentional agent-specific divergence must be documented in the same change.
- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; no later top-level section should follow it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that show at least the current artifact mix, the documented branch/phase curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window.
- Keep each short CEFR-B2 explanation directly adjacent to its matching ASCII diagram group so apprentices do not need to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- Each update must capture branch or phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 manually created lines per workday across code, tests, and documentation, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal-derived port.
- When effort is converted into months, use explicit assumptions such as 21.5 workdays per month and, if applicable, 30 vacation days per year through calendar year 2026 and 31 vacation days per year from calendar year 2027 onward under a TVoeD-style 5-day-week calendar.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Inclusion & Accessibility

- **`Programmierung #include<everyone>`** — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI) mit Deutsch und Englisch als Arbeitssprachen sowie an sehbehinderte Lernende, die mit Braille-Displays, Screen-Readern oder Textbrowsern arbeiten. Barrierefreiheit ist kein Nice-to-have, sondern Pflichtanforderung. Learner-facing guides, statistics, and generated HTML/API documentation must stay usable on Braille displays, with screen readers, and in text browsers.
- Treat WCAG 2.2 conformance level AA as the practical baseline for generated HTML documentation.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- If `docfx` output is regenerated, follow it with a text-oriented accessibility review, preferably with Playwright + `@axe-core/playwright` and a `lynx` cross-check.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Treat every successful `docfx` regeneration as incomplete until the matching text-oriented A11Y review has also been handled in the same work item; keep the `lynx` text-browser path visible next to the Playwright/axe review.
- Recommended A11y toolchain for DocFX-based repos: Node 24 LTS, `npm`, Playwright, `@axe-core/playwright`, and `lynx`.

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
<!-- claude-init-done -->

## Level-2-Umgebungsregister / Level-2 Environment Registry

- Die zentrale `constitution.md` enthält das verbindliche Level-2 Project Environment Registry.
- Spec-Kit-Pläne und Claude-Arbeit in Level-2-Projekten müssen die passende Registry-Zeile als verbindlichen Kontext für Runtime, Build/Test, A11Y, Statistik und Agentenflächen verwenden.
- Änderungen an einer Level-2-Runtime, Toolchain oder Statistik-Basis müssen `constitution.md`, `.specify/memory/constitution.md` und betroffene KI-Agenten-Dateien gemeinsam prüfen.

*The central `constitution.md` contains the binding Level-2 Project Environment Registry. Spec-Kit plans and Claude work in Level-2 projects must use the matching registry row as binding context for runtime, build/test, A11Y, statistics, and agent surfaces. Changes to Level-2 runtime, toolchain, or statistics baselines require a joint review of `constitution.md`, `.specify/memory/constitution.md`, and affected AI-agent files.*
## Memory-Safe Languages (MSL) / Speichersichere Sprachen

- Level-2-Projekte SOLLEN eine speichersichere Sprache (Memory-Safe Language, MSL) als primäre Laufzeit verwenden, wenn die Zielplattform es erlaubt.
- Verbindliche MSL-Erlaubnisliste, Regeln und Begründungspflicht: siehe `constitution.md`, Prinzip XI.
- MSL-Kurzliste: Rust, Swift, C#, F#, Java, Kotlin, Scala, Go, Dart, Python, Ruby, JavaScript, TypeScript, Haskell, OCaml, Erlang, Elixir, Ada, SPARK.
- **Nicht** MSL (Begründung im Level-2-`constitution.md` erforderlich): C, C++, klassisches Objective-C, Assembly, `cc65`-C89, Zig (pre-1.0), Nim (manual), D ohne GC.
- In Nicht-MSL-Repositories (z. B. `C64Projects/cc65`) die im Level-2-`constitution.md` hinterlegte Begründung im Plan- und Task-Kontext erwähnen.
- `speckit.constitution` und `speckit.specify` SOLLEN bei Nicht-MSL-Primärsprache einen **nicht blockierenden** Hinweis ausgeben (Tooling-Aufgabe, separate Umsetzung).
- Änderungen an dieser Empfehlung erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*Level-2 projects SHOULD use a memory-safe language (MSL) as their primary runtime when the target platform allows. Authoritative rules: `constitution.md`, Principle XI. MSL short list: Rust, Swift, C#/F#, Java/Kotlin/Scala, Go, Dart, Python, Ruby, JavaScript/TypeScript, Haskell, OCaml, Erlang/Elixir, Ada/SPARK. Non-MSL languages (C, C++, Assembly, `cc65`, Zig pre-1.0, …) require a documented justification in the Level-2 `constitution.md`. In non-MSL repositories (e.g. `C64Projects/cc65`), surface the documented justification in plans and tasks. `speckit.constitution` and `speckit.specify` SHOULD emit a non-blocking advisory warning when the primary language is not an MSL — tracked as a separate tooling task. Changes to this recommendation require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sichere Code-Erzeugung / Secure Code Generation (ISO 27001/27002 A.8.28)

- KI-generierter Code MUSS den etablierten Secure-Coding-Best-Practices der Zielsprache und des Frameworks folgen. LLMs erzeugen nicht zuverlässig sicheren Code; explizite Durchsetzung ist erforderlich.
- Verbindliche Regeln und sprachspezifische Anforderungen: siehe `constitution.md`, Prinzip XII.
- Sprachspezifische Kurzregeln:
  - **C / C89**: Bounds-Checking, kein `gets()`, kein ungeprüftes `sprintf()`/`strcpy()`, CERT C.
  - **C# / .NET**: parametrisierte Queries, Output-Encoding gegen XSS, Anti-Forgery-Tokens, sichere Deserialisierung, Microsoft Secure Coding Guidelines.
  - **SQL**: nur parametrisierte Statements, kein dynamisches SQL aus nicht vertrauenswürdigem Input.
  - **Bash**: Variable in Anführungszeichen (`"$var"`), kein `eval` auf nicht vertrauenswürdigem Input, `--` End-of-Options.
  - **PowerShell**: `Set-StrictMode -Version Latest`, validierte Parameter, kein `Invoke-Expression` auf nicht vertrauenswürdigem Input.
- Kryptografie: aktuelle Algorithmen (AES-256, RSA >= 3072, SHA-256+, Ed25519); veraltete (MD5, SHA-1 für Signaturen, DES, RC4) nur mit expliziter Risikobegründung.
- Fehlerbehandlung darf keine internen Zustände, Stack-Traces oder Verbindungszeichenketten an Endbenutzer preisgeben.
- Hinzugefügte Abhängigkeiten müssen aktiv gepflegt sein und dürfen keine bekannten kritischen CVEs aufweisen.
- Code-Reviews MÜSSEN eine Sicherheitsperspektive für Eingabeverarbeitung, Authentifizierung, Autorisierung, Kryptografie und Datei-/Netzwerk-I/O enthalten.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated code MUST follow the secure-coding best practices of the target language and framework. Authoritative rules: `constitution.md`, Principle XII. Language-specific short rules: C/C89 — bounds checking, no `gets()`, CERT C; C#/.NET — parameterised queries, output encoding, anti-forgery tokens, Microsoft Secure Coding Guidelines; SQL — parameterised statements only; Bash — quoted variables, no `eval` on untrusted input, `--` sentinel; PowerShell — `Set-StrictMode`, no `Invoke-Expression` on untrusted input. Cryptography: use current algorithms (AES-256, SHA-256+, Ed25519); deprecated (MD5, SHA-1 for signatures, DES, RC4) only with explicit risk acknowledgement. Error handling must not expose internals. Dependencies must have no known critical CVEs. Code reviews must include a security perspective for input handling, auth, crypto, and I/O. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sichere Software-Architektur / Secure Software Architecture (ISO 27001/27002 A.8.27)

- KI-generierte und menschlich geschriebene Software-Architektur MUSS etablierten sicheren Architekturprinzipien folgen. Sicherer Code (Prinzip XII) ohne sichere Architektur reicht nicht aus — beide Ebenen müssen zusammenwirken.
- Verbindliche Regeln und sprachspezifische Architekturvorgaben: siehe `constitution.md`, Prinzip XIII.
- Verbindliche Architekturprinzipien:
  - **Trust Boundaries**: Explizite Vertrauensgrenzen definieren; alle Eingaben an Vertrauensgrenzen validieren und bereinigen.
  - **Defense in Depth**: Mindestens zwei unabhängige Sicherheitsschichten für kritische Assets.
  - **Least Privilege**: Jede Komponente, jeder Dienst und Prozess arbeitet mit minimalen Berechtigungen.
  - **Fail-Safe Defaults**: Zugriff standardmäßig verweigern, explizit gewähren; Fehlerpfade fallen in sicheren Zustand zurück.
  - **Angriffsfläche reduzieren**: Ungenutzte Endpunkte, Dienste und Debug-Funktionen deaktivieren oder entfernen.
  - **Separation of Concerns**: Authentifizierung, Autorisierung, Logging und Eingabevalidierung als Cross-Cutting Concerns implementieren, nicht ad-hoc verstreuen.
  - **Sichere Konfiguration**: Secrets in plattformgeeigneten Secret-Stores (z. B. Azure Key Vault, macOS Keychain), nie im Quellcode oder in Git-tracked Config-Dateien.
  - **Supply-Chain-Sicherheit**: Abhängigkeiten aus verifizierten Registries; Lock-Files committen; verwundbare Abhängigkeiten vor Release ersetzen.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*AI-generated and human-written software architecture MUST follow secure-architecture principles. Authoritative rules: `constitution.md`, Principle XIII. Core principles: trust boundaries (validate all input at system boundaries), defense in depth (at least two independent security layers), least privilege (minimum required permissions), fail-safe defaults (deny by default), attack surface reduction (disable unused features), separation of concerns (auth/logging/validation as cross-cutting concerns), secure configuration (secrets in secret stores, never in code or Git), supply-chain security (verified registries, lock files, no known-vulnerable dependencies). Principles XII + XIII together form the complete secure-development approach: XII = tactical code-level security, XIII = strategic architecture-level security. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*

## Allgemeine Software-Architektur / General Software Architecture (iSAQB / arc42)

- Software-Architektur MUSS als explizites Design-Artefakt behandelt werden, wenn Änderungen Struktur, Schnittstellen, Quality Attributes, Laufzeitverhalten, Deployment oder technische Schulden berühren.
- Verbindliche Regeln und Evidenzpfade: siehe `constitution.md`, Prinzip XX.
- Architekturarbeit SOLL iSAQB/CPSA-F-Methodik und leichtgewichtige arc42-kompatible Dokumentation verwenden.
- Architekturrelevante Entscheidungen MÜSSEN als ADRs dokumentiert werden; allgemeine ADRs standardmäßig in `docs/architecture/adr/`.
- Systemkontext, Building Blocks, Runtime-Sichten, Deployment-Zwänge, Qualitäts-Szenarien sowie Architektur-Risiken oder akzeptierte Trade-offs MÜSSEN dokumentiert werden, wenn sie das Design materiell beeinflussen.
- Allgemeine Architektur-Evidenz liegt standardmäßig unter `docs/architecture/`; Sicherheitsarchitektur verbleibt unter `docs/security/`.
- Änderungen an dieser Regel erfordern ein gemeinsames Update in `constitution.md`, `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.

*Software architecture MUST be treated as an explicit design artefact whenever changes affect structure, interfaces, quality attributes, runtime behavior, deployment, or technical debt. Authoritative rules: `constitution.md`, Principle XX. Architecture work SHOULD use iSAQB/CPSA-F discipline and lightweight arc42-compatible documentation. Architecturally significant decisions MUST be recorded as ADRs, defaulting to `docs/architecture/adr/`. Context, building blocks, runtime/deployment views, quality scenarios, and architecture risks or trade-offs MUST be documented when they materially affect the design. General architecture evidence defaults to `docs/architecture/`; security architecture remains under `docs/security/`. Changes require a joint update across `constitution.md`, `.specify/memory/constitution.md`, and all four agent guidance files.*
## Sicherheitsdokumentation / Security Documentation (XII–XVIII Extensions)

- Jedes Level-2-Projekt MUSS die folgenden Sicherheitsdokumente pflegen, basierend auf den Templates in `.specify/templates/`:
  - **Bedrohungsmodell / Threat Model** (`threat-model-template.md`) — STRIDE-Methodik, Trust Boundaries, Risikobewertung, CAPEC-Referenzen (Prinzip XIII + XVII)
  - **Security Architecture Decision Records (S-ADR)** (`adr-template.md`) — architektonische Sicherheitsentscheidungen mit Compliance-Nachweis (Prinzip XIII)
  - **arc42 Section 8 Sicherheits-Querschnittskonzepte** (`arc42-security-template.md`) — Authentifizierung, Autorisierung, Verschlüsselung, Eingabevalidierung, Fehlerbehandlung, Logging, Abhängigkeiten, Deployment (Prinzip XIII)
  - **Sicherheits-Checkliste / Security Checklist** (`security-checklist-template.md`) — sprachspezifische Code-Review-Checkliste (Prinzip XII)
  - **Abhängigkeits-Audit / Dependency Audit** (`dependency-audit-template.md`) — CVE-Tracking, Lizenz-Compliance, Supply-Chain-Sicherheit (Prinzip XII)
  - **Sicherheits-Qualitätsszenarien / Security Quality Scenarios** (`security-quality-scenarios-template.md`) — iSAQB CPSA-F Qualitätsszenario-Methodik (Prinzip XII + XIII, SHOULD)
  - **ASVS-Verifikation / ASVS Verification** (`asvs-verification-template.md`) — OWASP ASVS Level, Scope und Evidenz (Prinzip XV, Web-/API-Projekte MUST)
  - **Supply-Chain-Evidenz / Supply Chain Evidence** (`supply-chain-evidence-template.md`) — SBOM, VEX, SLSA, OpenSSF Scorecard (Prinzip XVI, releasefähige Projekte MUST)
  - **Zero-Trust-Anwendbarkeit / Zero Trust Applicability** (`zero-trust-applicability-template.md`) — NIST SP 800-207-Bewertung (Prinzip XVIII, verteilte Systeme SHOULD)
  - **SAMM-Bewertung / SAMM Assessment** (`samm-assessment-template.md`) — OWASP SAMM Reifegrad und Verbesserungsplan (Prinzip XVIII, langlebige Projekte SHOULD)
- Projektspezifische Instanzen werden in `docs/security/` gepflegt; S-ADRs als einzelne Dateien in `docs/security/adr/`.

*Every Level-2 project MUST maintain security documents based on templates in `.specify/templates/`: threat model (STRIDE+CAPEC), S-ADRs, arc42 Section 8 security concepts, security checklist, dependency audit, security quality scenarios (SHOULD), ASVS verification (web/API MUST), supply-chain evidence (release-capable MUST), Zero Trust applicability note (distributed systems SHOULD), and SAMM assessment (long-lived projects SHOULD). Project-specific instances live in `docs/security/`; S-ADRs in `docs/security/adr/`. See `constitution.md`, Principles XII–XVIII for authoritative requirements.*

## Sicherheitsstandards & Anwendbarkeit / Security Standards & Applicability

- Vor jeder Level-2-Aufgabe die anwendbaren Sicherheitsstandards aus `constitution.md`, Prinzipien XIV-XVIII bestimmen und explizit benennen.
- `NIST SSDF` und `CWE Top 25` gelten immer für Level-2-Arbeit.
- `OWASP ASVS` gilt für Web-, API-, HTTP- und authentifizierte Dienste; der gewählte ASVS-Level muss benannt werden.
- `SBOM` gilt für releasefähige oder verteilbare Artefakte; `VEX`, wenn bekannte Schwachstellen in ausgelieferten oder geprüften Komponenten bewertet werden müssen.
- `SLSA` gilt als Soll-Vorgabe für CI/CD- oder veröffentlichte Artefakte; `Zero Trust` ist für verteilte, servicebasierte, cloudnahe oder remote-verwaltete Systeme explizit zu prüfen.
- `CAPEC` soll in Bedrohungsmodellen für die risikoreichsten Angriffswege verwendet werden; `OWASP SAMM` soll für langlebige Projekte/Workspaces in Verbesserungspläne einfließen.
- `OWASP Cheat Sheet Series`, `OWASP Proactive Controls` und bei öffentlichen OSS-Repositories oder kritischen Abhängigkeiten `OpenSSF Scorecard` sind als ergänzende Referenzen zu berücksichtigen.
- Nichtanwendbarkeit immer als `N/A` mit kurzer Begründung dokumentieren; keine stillschweigende Auslassung.

*At the start of every Level-2 task, determine and name the applicable security standards from `constitution.md`, Principles XIV-XVIII. `NIST SSDF` and `CWE Top 25` always apply. `OWASP ASVS` applies to web/API/HTTP/auth-bearing services; `SBOM` applies to releasable or distributable artefacts; `VEX` applies when known vulnerabilities in shipped/evaluated components need a disposition statement. `SLSA` is the target model for CI/CD and published artefacts; `Zero Trust` must be explicitly evaluated for distributed, service-based, cloud, or remotely managed systems. `CAPEC`, `OWASP SAMM`, `OWASP Cheat Sheet Series`, `OWASP Proactive Controls`, and `OpenSSF Scorecard` are supporting references where relevant. Record non-applicability as `N/A` with justification rather than omitting it silently.*

## Agentischer Security-Workflow / Agentic Security Workflow

- In `spec.md`, `plan.md` und `tasks.md` die anwendbaren Standards samt Evidenzpfad festhalten.
- Bei Bedrohungsmodellen `STRIDE` als Basis und bei risikoreichen Flows zusätzlich relevante `CAPEC`-Patterns verwenden.
- Bei Web/API-Features den `ASVS`-Level und den Verifikationsumfang in `docs/security/` oder gleichwertiger Projektdokumentation ablegen.
- Bei Release-/Artefakt-Arbeit `SBOM`, `VEX`, Provenance/SLSA-Nachweise und gegebenenfalls `OpenSSF Scorecard` in Release- oder Sicherheitsdokumentation einplanen.
- Bei Architekturänderungen `Zero Trust`-Anwendbarkeit und bei langlebigen Projekten `SAMM`-Folgeaktionen prüfen.
- Default-Evidenzpfad: `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, `docs/security/samm-assessment.md`; Abweichungen nur mit lokal dokumentierter Begründung.

*Capture the applicable standards and the evidence path in `spec.md`, `plan.md`, and `tasks.md`. Use `STRIDE` as the base for threat modeling and add relevant `CAPEC` patterns for the highest-risk flows. For web/API work, record the chosen `ASVS` level and verification scope in `docs/security/` or equivalent project documentation. For release and artefact work, plan `SBOM`, `VEX`, provenance/SLSA evidence, and `OpenSSF Scorecard` review where applicable. For architectural changes, evaluate `Zero Trust`; for long-lived projects, consider `OWASP SAMM` follow-up actions. The default evidence path is `docs/security/asvs-verification.md`, `docs/security/supply-chain-evidence.md`, `docs/security/zero-trust-applicability.md`, and `docs/security/samm-assessment.md`, unless the repository documents a justified equivalent location.*

## Agentischer Architektur-Workflow / Agentic Architecture Workflow

- In `spec.md`, `plan.md` und `tasks.md` festhalten, ob Systemkontext, Schnittstellen, Building Blocks, Laufzeitverhalten, Deployment oder technische Schulden betroffen sind.
- Bei architekturrelevanten Änderungen passende Evidenz unter `docs/architecture/` planen und pflegen: Context View, Building-Block View, Runtime View, Deployment View, Quality Scenarios, ADRs sowie Architektur-Risiken.
- Für architekturell signifikante Entscheidungen ADRs in `docs/architecture/adr/` anlegen oder aktualisieren.
- Qualitätsanforderungen als konkrete Szenarien formulieren statt als unscharfe Adjektive.
- Wenn Sicherheitsarchitektur betroffen ist, die Evidenzpfade unter `docs/security/` zusätzlich gemeinsam mit der allgemeinen Architektur-Evidenz aktualisieren.

*In `spec.md`, `plan.md`, and `tasks.md`, record whether system context, interfaces, building blocks, runtime behavior, deployment, or technical debt are affected. For architecture-relevant changes, plan and maintain the matching evidence under `docs/architecture/`: context, building-block, runtime, deployment, quality-scenario, ADR, and architecture-risk artefacts. Create or update ADRs in `docs/architecture/adr/` for significant decisions. Express quality requirements as concrete scenarios rather than vague adjectives. If security architecture is affected, update the `docs/security/` evidence path together with the general architecture evidence.*
## Hinweise / Notes

- Diese Datei ergaenzt die projektspezifische Dokumentation mit agentischen Arbeitsregeln.
- This file complements the project-specific documentation with agent-oriented working rules.

<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->
