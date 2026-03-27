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

## Project Statistics

- When shared AI-agent guidance, workflow conventions, or statistics methodology changes, review and update `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, and `.github/copilot-instructions.md` together when they are affected.
- Shared guidance must not be updated in only one of these files; any intentional agent-specific divergence must be documented in the same change.
- Maintain `docs/project-statistics.md` as the living statistics ledger for the repository.
- Update the file after each completed Spec-Kit implementation phase, after each agent-driven repository change, or when a refresh is explicitly requested.
- Each update must capture branch or phase, observable work window, production/test/documentation line counts, main work packages, the conservative manual baseline of 80 manually created lines per workday across code, tests, and documentation, and the repo-specific Thorsten-Solo comparison baseline of 125 lines per workday for this Pascal-derived port.
- When effort is converted into months, use explicit assumptions such as 21.5 workdays per month and, if applicable, 30 vacation days per year through calendar year 2026 and 31 vacation days per year from calendar year 2027 onward under a TVoeD-style 5-day-week calendar.
- When reporting acceleration, compare both manual references against visible Git active days and label the result as a blended repository speedup rather than a stopwatch measurement.
- When hour values are shown, convert the day-based estimates with the TVoeD working-day baseline of `7.8 hours` (`7h 48m`) per day.
