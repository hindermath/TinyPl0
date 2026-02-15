# AGENTS.md - TinyPl0

This file provides guidance for Junie working in this repository.

## Project Overview

TinyPl0 is a C# .NET 10 port of the historical PL/0 compiler (originally in Pascal). It's a pedagogical reference for compiler construction.

**Architecture:**
- `Pl0.Core`: Lexer, Parser, Symbol Table, Code Generator
- `Pl0.Vm`: Stack-based virtual machine for P-Code
- `Pl0.Cli`: Command-line interface

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
- XML doc comments for public APIs (`<summary>`, `<param>`)
- Target audience: German vocational students (German comments in docs)

## Module Dependencies (Must Follow)

```
Pl0.Cli --> Pl0.Core
Pl0.Cli --> Pl0.Vm
Pl0.Core --> (no dependencies)
Pl0.Vm --> Pl0.Core (for Instruction, Opcode)
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

- All user-facing documentation in German.
- Code prioritizes clarity over performance.
- No language extensions beyond PL/0 definition.
- No compiler optimizations (no peephole, SSA, etc.).
- VM target only - no JIT or IL backend.
- Prerequisites: .NET 10 SDK.
