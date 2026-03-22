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
- `Minor` = current PR number
- `Patch` = current commit count in that PR branch (after committing the current change)
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

- C# 12+ features: collection expressions `[]`, primary constructors, `record` for immutable data
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

- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Each update must capture branch or phase, observable work window, production/test/documentation line counts, main work packages, and the conservative manual baseline of 80 code lines per day for an experienced developer.
