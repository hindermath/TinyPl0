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

- **Documentation:** Follow the specifications in `Pflichtenheft_PL0_CSharp_DotNet10.md`. Extensive documentation is available in the `docs/` and `docfx/` directories.
- **Code Style:** Adhere to standard C# naming conventions (PascalCase for classes/methods, camelCase for local variables).
- **Testing:**
  - Use `xUnit` for unit and integration tests.
  - New features or bug fixes should include corresponding test cases in `tests/Pl0.Tests`.
  - The project maintains a high standard of traceability between requirements and tests (see `docs/TRACEABILITY_MATRIX.md`).
- **Error Handling:** Use the `DiagnosticBag` in `Pl0.Core` to collect errors instead of throwing exceptions during compilation.
- **VM Semantics:** The VM uses a stack-based architecture with registers `P` (Program Counter), `B` (Base Pointer), and `T` (Top of Stack).

## Key Files
- `TinyPl0.sln`: Main solution file.
- `Pflichtenheft_PL0_CSharp_DotNet10.md`: Main requirements and technical specification.
- `docs/ARCHITECTURE.md`: High-level system architecture and mapping from Pascal to C#.
- `docs/LANGUAGE_EBNF.md`: Formal grammar of the supported PL/0 dialects.
- `docs/VM_INSTRUCTION_SET.md`: Detailed description of the P-Code instruction set.
- `pl0c.pas`: Historical Pascal reference source.
