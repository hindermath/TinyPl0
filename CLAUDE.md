# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TinyPl0 is a C# .NET 10 port of the historical PL/0 compiler (originally in Pascal). It's a pedagogical reference for compiler construction targeting German vocational IT training (Fachinformatiker-Ausbildung). All documentation and comments are in German.

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
```

## Architecture

Three-module layered architecture with enforced dependencies (`ArchitectureGuardTests`):

```
Pl0.Cli  --> Pl0.Core  (compilation)
Pl0.Cli  --> Pl0.Vm    (execution)
Pl0.Core --> (no dependencies)
Pl0.Vm   --> Pl0.Core  (for Instruction, Opcode types)
```

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

## Key Files

- `Pflichtenheft_PL0_CSharp_DotNet10.md` — main requirements specification
- `docs/ARCHITECTURE.md` — Pascal-to-C# mapping and module overview
- `docs/LANGUAGE_EBNF.md` — formal grammar for both dialects
- `docs/VM_INSTRUCTION_SET.md` — P-Code instruction set reference
- `docs/TRACEABILITY_MATRIX.md` — requirements-to-test mapping
- `tests/data/expected/catalog/cases.json` — 41 mandatory test cases
- `pl0c.pas` — historical Pascal reference source

## Code Conventions

- C# 12+ features: collection expressions `[]`, primary constructors, `record` for immutable data
- Test classes: `public sealed class`, suffix `Tests`, use xUnit `[Fact]`
- Namespaces: all in `Pl0.*`
- 4-space indentation, opening brace on same line

**Error handling — critical:** Do NOT throw exceptions during compilation. All errors go into `CompilerDiagnostic` / `LexerDiagnostic`. Check `CompilationResult.Success` (true when `Diagnostics.Count == 0`) before execution.

## Dialect Handling

- `classic`: Strict Pascal compatibility, no `?`/`!` I/O statements, identifiers must start with lowercase
- `extended`: Adds `? ident` (input) and `! expression` (output)

Use `CompilerOptions.Dialect` when modifying parser/lexer. Classic mode must stay strictly compatible with `pl0c.pas`.

## Historical Quirks (Preserve)

- `[` maps to `<=`, `]` maps to `>=`
- Max identifier length: 10 characters; max number digits: 14; max nesting: 3 levels
- Only integer type; no procedure parameters or return values

## Constraints

- No language extensions beyond the PL/0 definition
- No compiler optimizations (no peephole, SSA, etc.)
- VM target only — no JIT or IL backend
- Code prioritizes clarity over performance (pedagogical project)
