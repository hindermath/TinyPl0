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
