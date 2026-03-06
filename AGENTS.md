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

## Code Style Guidelines

### General
- Use C# 12+ features (collection expressions, primary constructors)
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
- For `Pl0.Ide`, `<Version>` in `src/Pl0.Ide/Pl0.Ide.csproj` follows `Major.Minor.Patch.Build` with these fixed semantics: `Minor` = current PR number, `Patch` = current commit count in that PR branch (after committing the current change), `Build` = manual build counter incremented by the bot before every `dotnet build` or `dotnet test`.
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
