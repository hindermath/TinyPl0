# TinyPl0 Project Context

This project is a port of the historical PL/0 compiler (originally in Pascal) to C# on .NET 10. It serves as a pedagogical reference for compiler construction.

## Project Overview

- **Purpose:** To provide a functional and didactic implementation of the PL/0 compiler and its virtual machine (P-Code VM).
- **Architecture:**
  - `Pl0.Core`: Contains the Lexer, Parser, Symbol Table, and Code Generator.
  - `Pl0.Vm`: A stack-based virtual machine that executes P-Code instructions.
  - `Pl0.Cli`: Command-line interface for compiling and running PL/0 programs.
- **Dialects:**
  - `classic`: Strictly compatible with the original Pascal reference (no I/O statements `?` and `!`).
  - `extended`: Includes `? ident` (input) and `! expression` (output) statements.
- **Key Technologies:** C#, .NET 10, xUnit for testing.

## Building and Running

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Key Commands
- **Restore Dependencies:** `dotnet restore`
- **Build Project:** `dotnet build`
- **Run Tests:** `dotnet test`
- **Run CLI:** `dotnet run --project src/Pl0.Cli -- [subcommand] [args]`
  - `compile <file.pl0> --out <file.pcode>`: Compiles PL/0 source to P-Code.
  - `run <file.pl0>`: Compiles and immediately executes a PL/0 program.
  - `run-pcode <file.pcode>`: Executes a pre-compiled P-Code file.

### Update Golden Artifacts
The project uses Golden Master tests for the lexer and parser. To update these artifacts after intentional changes:
```bash
./scripts/update-golden-code.sh
```

## Development Conventions

- **Language & Didactics:** Learner-facing documentation and comments are bilingual (German first, English second) and should target CEFR/GER B2 readability.
- **XML Documentation:** For changed APIs, provide complete XML docs where applicable (`<summary>`, `<param>`, `<returns>`, `<exception>`; `<remarks>`/`<example>` when useful). Public XML doc gaps are build-breaking (CS1591 must not be globally suppressed).
- **DocFX Regeneration:** If API signatures or XML docs change, run `docfx` from repository root (`docfx.json`) in the same commit/PR.
- **Documentation:** Follow the specifications in `Pflichtenheft_PL0_CSharp_DotNet10.md`. Extensive documentation is available in the `docs/` and `docfx/` directories.
- **Code Style:** Adhere to standard C# naming conventions (PascalCase for classes/methods, camelCase for local variables).
- **Testing:**
  - Use `xUnit` for unit and integration tests.
  - New features or bug fixes should include corresponding test cases in `tests/Pl0.Tests`.
  - The project maintains a high standard of traceability between requirements and tests (see `docs/TRACEABILITY_MATRIX.md`).
- **Error Handling:** Use the `DiagnosticBag` in `Pl0.Core` to collect errors instead of throwing exceptions during compilation.
- **VM Semantics:** The VM uses a stack-based architecture with registers `P` (Program Counter), `B` (Base Pointer), and `T` (Top of Stack).
- **IDE Version Scheme:** `src/Pl0.Ide/Pl0.Ide.csproj` uses `Major.Minor.Patch.Build`. `Minor` is the current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`002` -> `2`) and used immediately even before a GitHub PR exists. `Patch` is the current commit count in that feature/PR branch after committing the current change. `Build` is the manual build counter incremented before every `dotnet build` or `dotnet test`. Keep `Version`, `AssemblyVersion`, and `FileVersion` aligned before pushing.

## Key Files
- `TinyPl0.sln`: Main solution file.
- `Pflichtenheft_PL0_CSharp_DotNet10.md`: Main requirements and technical specification.
- `docs/ARCHITECTURE.md`: High-level system architecture and mapping from Pascal to C#.
- `docs/LANGUAGE_EBNF.md`: Formal grammar of the supported PL/0 dialects.
- `docs/VM_INSTRUCTION_SET.md`: Detailed description of the P-Code instruction set.
- `pl0c.pas`: Historical Pascal reference source.

## Git Workflow

- `main` is protected: do not commit or push directly to `main`.
- Create a new branch for each change before starting work.
- Once a dedicated feature branch has implemented a Lastenheft, rename that file to `Lastenheft_<topic>.<feature-branch>.md` so the delivered scope stays traceable.

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

- Programmierung #include<everyone> — Diese Lernbeispiele richten sich an Azubis (Fachinformatiker AE/SI) mit Deutsch und Englisch als Arbeitssprachen sowie an sehbehinderte Lernende, die mit Braille-Displays, Screen-Readern oder Textbrowsern arbeiten. Barrierefreiheit ist kein Nice-to-have, sondern Pflichtanforderung.
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

## Shared Parent Guidance

- Die gemeinsamen Dateien `/Users/thorstenhindermann/RiderProjects/AGENTS.md` und `/Users/thorstenhindermann/RiderProjects/GEMINI.md` speichern die repo-uebergreifenden Basisregeln.
- Diese Projekt-Datei ist die spezifischere Autoritaet fuer projektspezifische Build-Befehle, Workflows, Architektur und Features.

## Hinweise / Notes

- Diese Datei ergaenzt die projektspezifische Dokumentation mit agentischen Arbeitsregeln.
- This file complements the project-specific documentation with agent-oriented working rules.
