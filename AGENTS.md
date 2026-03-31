# AGENTS.md - TinyPl0

This file provides guidance for agents working in this repository.

## Project Overview

TinyPl0 is a C# .NET 10 port of the historical PL/0 compiler (originally in Pascal). It's a pedagogical reference for compiler construction.

**Architecture:**
- `Pl0.Core`: Lexer, Parser, Symbol Table, Code Generator
- `Pl0.Vm`: Stack-based virtual machine for P-Code
- `Pl0.Cli`: Command-line interface
- `Pl0.Ide`: Terminal.Gui-based IDE for compile/run/debug workflows

## Build, Test, and Run Commands

### Build and Test
```bash
# Restore dependencies
dotnet restore

# Build entire solution
dotnet build

# Build Release
dotnet build --configuration Release

# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "FullyQualifiedName~Pl0.Tests.LexerTests"

# Run single test method
dotnet test --filter "FullyQualifiedName~Pl0.Tests.LexerTests.Tokenizes_Simple_Assignment"
```

### CLI Usage
```bash
# Compile PL/0 to P-Code
dotnet run --project src/Pl0.Cli -- compile <file.pl0> --out <file.pcode>

# Compile and run immediately
dotnet run --project src/Pl0.Cli -- run <file.pl0>

# Execute pre-compiled P-Code
dotnet run --project src/Pl0.Cli -- run-pcode <file.pcode>

# Show P-Code listing
dotnet run --project src/Pl0.Cli -- run <file.pl0> --list-code --wopcod
```

### Golden Master Tests
Update golden artifacts after intentional changes:
```bash
./scripts/update-golden-code.sh
```

## Git Workflow (Mandatory)

- `main` is protected: do not commit or push directly to `main`.
- For every change, create a new clean branch first.
- Open a pull request to merge changes into `main`.
- Agent branch naming convention: `codex/<topic>`.
- When a dedicated feature branch has implemented the requirements of a Lastenheft, rename that file to `Lastenheft_<Thema>.<feature-branch>.md` so the fulfilled requirement scope remains traceable in the repository.
- When interacting with GitHub, prefer the GitHub CLI `gh` whenever it can handle the task.
- When syncing the local repository with GitHub, prefer `gh repo sync --branch <branch>`; the user is already authenticated via `gh`.

## Code Style Guidelines

### General
- Use C# 14 features (collection expressions, primary constructors)
- Use `public sealed class` for test classes
- All code in `Pl0.*` namespaces

### Naming Conventions
- Classes, methods, properties: PascalCase
- Local variables, parameters: camelCase
- Test classes: Suffix with `Tests` (e.g., `LexerTests`)
- File names: Match class name (e.g., `Pl0Lexer.cs`)

### Imports
- Use implicit usings where available
- Explicit `using` for project-specific types
- No unnecessary aliases

### Types
- Use explicit types for clarity in complex cases
- Use `record` for immutable data (e.g., `Pl0Token`, `TextPosition`)
- Use `List<T>` for collections, prefer collection expressions `[]`

### Formatting
- 4 spaces indentation
- Opening brace on same line
- One space after keywords (`if (`, `for (`)
- No space after method name before parenthesis

### Error Handling
- **DO NOT throw exceptions during compilation**
- Collect errors in `CompilerDiagnostic` / `LexerDiagnostic` (using `DiagnosticBag` in `Pl0.Core`)
- Use `CompilationResult` which contains `Instructions` and `Diagnostics`
- Check `CompilationResult.Success` before execution

### Documentation
- Bilingual learner-facing documentation/comments are mandatory: German block first, English block second, both at CEFR/GER B2 readability.
- Large normative documents such as `Pflichtenheft*.md` and `Lastenheft*.md` may use a synchronized English sidecar with suffix `.EN.md`; the German version remains canonical unless explicitly marked otherwise.
- Follow `Programmierung #include<everyone>`: guides, statistics, examples, and generated API documentation must remain usable in text-first assistive setups such as Braille displays, screen readers, and text browsers.
- Prefer semantic headings, lists, tables, and ASCII/text-first diagrams; do not encode essential meaning only through color, layout, or pointer-only affordances.
- Treat bilingual CEFR-B2 delivery and the documented A11Y proof path as formal completion criteria for learner-facing documentation and active requirement artifacts.
- XML doc comments are mandatory for changed APIs and must be complete where applicable (`<summary>`, `<param>`, `<returns>`, `<exception>`; `<remarks>`/`<example>` when instructive).
- Missing XML documentation on public API members is a build error; CS1591 must not be globally suppressed.
- If API signatures or XML comments change, regenerate docs with `docfx` (repository root `docfx.json`) in the same commit/PR.

## Module Dependencies (Must Follow)

```
Pl0.Core --> (no dependencies)
Pl0.Vm --> Pl0.Core (for Instruction, Opcode)
Pl0.Cli --> Pl0.Core
Pl0.Cli --> Pl0.Vm
Pl0.Ide --> Pl0.Core
Pl0.Ide --> Pl0.Vm
Pl0.Tests --> Pl0.Core, Pl0.Vm, Pl0.Cli, Pl0.Ide
```

Enforced by `ArchitectureGuardTests` - do not violate these rules.

## Testing Requirements

- Use **xUnit** with `[Fact]` attribute
- New features/bug fixes MUST include test cases in `tests/Pl0.Tests`
- Test data: `tests/data/expected/catalog/cases.json` (41 mandatory cases)
- See `docs/TRACEABILITY_MATRIX.md` for requirement-to-test mapping

## Historical Quirks (Preserve)

- `[` maps to `<=`, `]` maps to `>=` (historical compatibility)
- Identifiers must start with lowercase letter in classic mode
- Max identifier length: 10 characters
- Max number digits: 14
- Max lexical nesting: 3 levels
- Only integer type supported

## Dialect Handling

- `classic`: Original Pascal reference (no I/O statements)
- `extended`: Includes `? ident` (input) and `! expr` (output)
- Check `CompilerOptions.Dialect` when modifying parser/lexer

## Virtual Machine Semantics
- Stack-based architecture.
- Registers: `P` (Program Counter), `B` (Base Pointer), and `T` (Top of Stack).

## Key Files

- `TinyPl0.sln`: Main solution file.
- `Pflichtenheft_PL0_CSharp_DotNet10.md`: Main requirements and technical specification.
- `docs/ARCHITECTURE.md`: High-level system architecture and mapping from Pascal to C#.
- `docs/LANGUAGE_EBNF.md`: Formal grammar of the supported PL/0 dialects.
- `docs/VM_INSTRUCTION_SET.md`: Detailed description of the P-Code instruction set.
- `docs/TRACEABILITY_MATRIX.md`: Requirements↔tests mapping.
- `pl0c.pas`: Historical Pascal reference source.

## Important Notes

- Learner-facing documentation/comments are bilingual (DE first, EN second) with B2 readability target.
- Code prioritizes clarity over performance.
- No language extensions beyond PL/0 definition.
- No compiler optimizations (no peephole, SSA, etc.).
- VM target only - no JIT or IL backend.
- Prerequisites: .NET 10 SDK.
- For `Pl0.Ide`, `<Version>` in `src/Pl0.Ide/Pl0.Ide.csproj` follows `Major.Minor.Patch.Build` with these fixed semantics: `Minor` = current Spec-Kit feature/branch number, interpreted numerically as the canonical PR number for versioning (`002` -> `2`) and used immediately even before a GitHub PR exists; `Patch` = current commit count in that feature/PR branch (after committing the current change); `Build` = manual build counter incremented by the bot before every `dotnet build` or `dotnet test`.
- Whenever the bot creates a commit or updates a PR branch, it must automatically align the IDE version fields (`Version`, `AssemblyVersion`, `FileVersion`) to this rule before pushing.
- Keep `Pflichtenheft_IDE.md` worklog up to date by appending new IDE-related work steps at the end.

## Copilot Instructions

This project is a port of the historical PL/0 compiler (originally in Pascal) to C# on .NET 10. It serves as a pedagogical reference for compiler construction in the German vocational training context (Fachinformatiker-Ausbildung).

The original Pascal implementation serves as the behavioral reference. Key mappings:
- `getsym/getch` → `Pl0Lexer`
- `block/statement/condition/expression` → `Pl0Parser`
- `enter/position/table` → `SymbolTable` + `SymbolEntry`
- `gen` → `Pl0Parser.Emit()`
- `interpret` → `VirtualMachine.Run()`
- `base(l)` → `VirtualMachine.ResolveBase()`

The VM uses three registers:
- **P** (Program Counter): Next instruction to execute
- **B** (Base Pointer): Points to the current activation record's base
- **T** (Top of Stack): Points to the top of the stack

Activation records use three reserved cells: Static Link, Dynamic Link, Return Address.

8 opcodes: `Lit`, `Opr`, `Lod`, `Sto`, `Cal`, `Int`, `Jmp`, `Jpc`. See `docs/VM_INSTRUCTION_SET.md` for full reference.

## Project Statistics

- When shared AI-agent guidance, workflow conventions, or statistics methodology changes, review and update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md` together when they are affected.
- Shared guidance must not be updated in only one of these files; any intentional agent-specific divergence must be documented in the same change.
- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Within the `## Fortschreibungsprotokoll` table, keep entries in strict chronological order: oldest entry at the top, newest and most recently added entry at the bottom; entries with the same date keep their insertion order.
- Keep a final top-level `## Gesamtstatistik` block as the last section of `docs/project-statistics.md`; do not place any later top-level section after it.
- Inside that final `## Gesamtstatistik` block, maintain compact ASCII-only trend diagrams that show at least the current artifact mix, the documented branch/phase curves, the documented acceleration factors from agentic-AI plus Spec-Kit/SDD support, and a direct comparison between experienced-developer effort, Thorsten-solo effort, and the visible AI-assisted delivery window.
- Keep each short CEFR-B2 explanation directly adjacent to its matching ASCII diagram group so apprentices do not have to scroll between explanation and diagram.
- When the data benefits from progression across an X-axis, add simple ASCII X/Y charts as a second visualization layer; keep them approximate, readable in plain Markdown, and explained in CEFR-B2 language.
- Keep the statistics section plain-text friendly for Braille displays, screen readers, and text browsers; the ASCII diagrams and their explanations must stay understandable without relying on color or visual layout alone.
- Each update must record branch or phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 manually created lines per workday across code, tests, and documentation, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal-derived port.
- When effort is converted into months, use explicit assumptions such as 21.5 workdays per month and, if applicable, 30 vacation days per year through calendar year 2026 and 31 vacation days per year from calendar year 2027 onward under a TVoeD-style 5-day-week calendar.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.

## Active Technologies
- C# 14 / .NET 10 + .NET SDK, bestehende Module `Pl0.Core`, `Pl0.Vm`, `Pl0.Cli`, xUnit (002-vm-inc-compat)
- Textuelle `.pcode`-/Listing-Artefakte als Einlese-Schnittstelle fuer historische `Inc`-/`Int`-Kompatibilitaet (002-vm-inc-compat)

## Recent Changes
- 002-vm-inc-compat: Planungsartefakte fuer die historische `Inc`-/`Int`-Alias-Kompatibilitaet und den Implementierungsfokus auf `PCodeSerializer`, Tests und VM-Dokumentation hinzugefuegt

## Inclusion & Accessibility

- Follow `Programmierung #include<everyone>`: learner-facing guides, statistics, and generated HTML/API documentation must stay usable on Braille displays, with screen readers, and in text browsers.
- Treat WCAG 2.2 conformance level AA as the practical baseline for generated HTML documentation.
- Use WCAG 2.2 AA as the concrete review baseline for generated HTML documentation, especially for page language, bypass blocks, keyboard focus visibility, non-text contrast, and readable landmark structure.
- If `docfx` output is regenerated, follow it with a text-oriented accessibility review, preferably with Playwright + `@axe-core/playwright` and a `lynx` cross-check.
- When DocFX content, documentation navigation, or API presentation changes, validate representative `_site/` pages through a text-oriented review path, preferably with a local Playwright accessibility snapshot.
- Treat every successful `docfx` regeneration as incomplete until the matching text-oriented A11Y review has also been handled in the same work item; keep the `lynx` text-browser path visible next to the Playwright/axe review.
- Recommended A11y toolchain for DocFX-based repos: Node 24 LTS, `npm`, Playwright, `@axe-core/playwright`, and `lynx`.

## Shared Parent Guidance

- The shared parent file `/Users/thorstenhindermann/RiderProjects/AGENTS.md` intentionally stores only repo-spanning baseline rules.
- Keep repository-specific build, test, workflow, architecture, and feature guidance in this repository's own files; when both layers exist, the repository-local files are the more specific authority.
