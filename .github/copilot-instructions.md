# TinyPl0 - Copilot Instructions

This project is a port of the historical PL/0 compiler (originally in Pascal) to C# on .NET 10. It serves as a pedagogical reference for compiler construction in the German vocational training context (Fachinformatiker-Ausbildung).

## Project Overview

**Purpose:** A functional and didactic implementation of the PL/0 compiler and its P-Code virtual machine.

**Architecture:**
- `Pl0.Core`: Lexer, Parser, Symbol Table, and Code Generator
- `Pl0.Vm`: Stack-based virtual machine that executes P-Code instructions
- `Pl0.Cli`: Command-line interface for compiling and running PL/0 programs

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
The project uses Golden Master tests for lexer and parser outputs. To update golden artifacts after intentional changes:
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
```

**Important:** This layered architecture is enforced by `ArchitectureGuardTests`. Do not introduce circular dependencies or violate these rules.

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

## Code Conventions and Patterns

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

## Key Documentation Files

- `Pflichtenheft_PL0_CSharp_DotNet10.md`: Requirements and technical specification
- `docs/ARCHITECTURE.md`: High-level system architecture
- `docs/LANGUAGE_EBNF.md`: Formal grammar of PL/0 dialects
- `docs/VM_INSTRUCTION_SET.md`: P-Code instruction set reference
- `docs/TRACEABILITY_MATRIX.md`: Mapping between requirements and tests
- `docs/QUALITY.md`: Code quality and coverage metrics
- `pl0c.pas`: Historical Pascal reference source

## Important Notes

- **Language:** All documentation and comments in German (target audience: German vocational students).
- **Pedagogy:** The code prioritizes clarity and didactic value over performance optimizations.
- **No language extensions:** Do not add features beyond the consolidated PL/0 definition.
- **No optimizations:** No peephole optimization, SSA, or similar compiler optimizations.
- **VM target only:** No JIT or IL backend; always target the P-Code VM.
