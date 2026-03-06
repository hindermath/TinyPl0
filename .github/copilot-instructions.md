# TinyPl0 - Copilot Instructions

This project is a port of the historical PL/0 compiler (originally in Pascal) to C# on .NET 10. It serves as a pedagogical reference for compiler construction in the German vocational training context (Fachinformatiker-Ausbildung).

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

## Architecture and Key Concepts

### Module Dependencies (Must Follow)
```
Pl0.Cli --> Pl0.Core
Pl0.Cli --> Pl0.Vm
Pl0.Core --> (no dependencies on other modules)
Pl0.Vm --> Pl0.Core (for Instruction, Opcode)
Pl0.Ide --> Pl0.Core
Pl0.Ide --> Pl0.Vm
Pl0.Ide --> Terminal.Gui (only allowed external package for Pl0.Ide)
```

**Important:** This layered architecture is enforced by `ArchitectureGuardTests`. Do not introduce circular dependencies or violate these rules. `ArchitectureGuardTests` also verifies that `Pl0.Ide.csproj` only references `Terminal.Gui` as a NuGet package.

### Data Flow
1. `.pl0` source → `Pl0Lexer` → tokens
2. tokens → `Pl0Parser` → P-Code `Instruction[]` + diagnostics
3. P-Code → `PCodeSerializer` → `.pcode` file (optional)
4. P-Code → `VirtualMachine` → execution on integer stack

### Pascal to C# Mapping

The original Pascal implementation serves as the behavioral reference. Key mappings:

| Pascal Reference | C# Implementation |
|------------------|-------------------|
| `getsym/getch` | `Pl0Lexer` |
| `block/statement/condition/expression` | `Pl0Parser` |
| `enter/position/table` | `SymbolTable` + `SymbolEntry` |
| `gen` | `Pl0Parser.Emit()` |
| `interpret` | `VirtualMachine.Run()` |
| `base(l)` | `VirtualMachine.ResolveBase()` |

### Stack-Based VM Semantics

The VM uses three registers:
- **P** (Program Counter): Next instruction to execute
- **B** (Base Pointer): Points to the current activation record's base
- **T** (Top of Stack): Points to the top of the stack

Activation records use three reserved cells: Static Link, Dynamic Link, Return Address.

**8 opcodes:** `Lit`, `Opr`, `Lod`, `Sto`, `Cal`, `Int`, `Jmp`, `Jpc`. See `docs/VM_INSTRUCTION_SET.md` for full reference.

### I/O Abstraction

The VM uses the `IPl0Io` interface for all input/output:
- `ConsolePl0Io`: Default implementation used by CLI and IDE (reads/writes to console)
- `BufferedPl0Io`: Used in tests — accepts pre-set integer input and captures integer output for assertions

Inject `IPl0Io` when constructing `VirtualMachine`; never read from `Console` directly inside the VM or parser.

### Step Debugging

`SteppableVirtualMachine` (in `Pl0.Vm`) wraps `VirtualMachine` and exposes `Step()` and a `VmState` snapshot (registers P/B/T + stack slice). The IDE debug view uses this to single-step through P-Code execution.

## Code Conventions and Patterns

### Language and Documentation
- Learner-facing documentation and explanatory comments are bilingual: German first, English second.
- Both language blocks should target CEFR/GER B2 readability for mixed-language trainee cohorts.
- For changed APIs, provide complete XML docs where applicable (`<summary>`, `<param>`, `<returns>`, `<exception>`; optional `<remarks>`/`<example>` when instructive).
- Public XML documentation gaps are build-breaking (CS1591 must not be globally suppressed).
- If API signatures or XML docs change, run `docfx` from repository root (`docfx.json`) in the same commit/PR.

### Error Handling
- **Do NOT throw exceptions during compilation.** Collect all errors in diagnostics instead.
- Use `CompilerDiagnostic` (in `Pl0.Core`) to report parser/compiler errors.
- Use `LexerDiagnostic` for lexical analysis errors.
- The `CompilationResult` contains both `Instructions` and `Diagnostics`.
- Check `CompilationResult.Success` (true when `Diagnostics.Count == 0`) before execution.

### Naming Conventions
- **Classes, methods, properties:** PascalCase
- **Local variables, parameters:** camelCase
- **Test classes:** Suffix with `Tests` (e.g., `LexerTests`)
- **Test classes:** Marked as `public sealed class`

### C# Style
- Use C# 12+ features: collection expressions `[]`, primary constructors
- Use `record` for immutable data types (e.g., `Pl0Token`, `TextPosition`)
- Use `List<T>` for mutable collections

### Testing Requirements
- Use **xUnit** for all tests.
- New features or bug fixes MUST include corresponding test cases in `tests/Pl0.Tests`.
- The project maintains high traceability between requirements and tests (see `docs/TRACEABILITY_MATRIX.md`).
- Test data catalog is at `tests/data/expected/catalog/cases.json` with 41 mandatory test cases.

### Adding New Test Cases

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

### Historical Quirks (Preserve These)
- **Relational operators:** `[` maps to `<=` and `]` maps to `>=` (historical compatibility)
- **Identifier rules:** Must start with `a-z` (lowercase only in classic mode)
- **Max identifier length:** 10 characters (configurable, default from Pascal reference)
- **Max number digits:** 14 digits (configurable)
- **Lexical levels:** Maximum nesting depth 3 (default from Pascal)
- **No parameters or return values** for procedures (PL/0 limitation)
- **Only integer type** supported

### Dialect Handling
When modifying parser or lexer:
- Check if the feature applies to `classic`, `extended`, or both.
- Use `CompilerOptions.Dialect` to control behavior.
- Classic mode must remain strictly compatible with the Pascal reference source (`pl0c.pas`).

## Git Workflow

- `main` is protected — never commit or push directly to `main`.
- For every change, create a new branch first. Agent/bot branch naming: `codex/<topic>`.
- Open a pull request to merge into `main`.

## Key Documentation Files

- `Pflichtenheft_PL0_CSharp_DotNet10.md`: Requirements and technical specification
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
- `Minor` = current PR number
- `Patch` = current commit count in that PR branch (after committing the current change)
- `Build` = manual build counter incremented before every `dotnet build` or `dotnet test`

Whenever a commit is created or the PR branch is updated, align `Version`, `AssemblyVersion`, and `FileVersion` in `Pl0.Ide.csproj` to this scheme before pushing.

### IDE Worklog

After any IDE-related work, append a new entry to the worklog at the bottom of `Pflichtenheft_IDE.md`.

## Important Notes

- **Language:** All documentation and comments in German (target audience: German vocational students).
- **Pedagogy:** The code prioritizes clarity and didactic value over performance optimizations.
- **No language extensions:** Do not add features beyond the consolidated PL/0 definition.
- **No optimizations:** No peephole optimization, SSA, or similar compiler optimizations.
- **VM target only:** No JIT or IL backend; always target the P-Code VM.
