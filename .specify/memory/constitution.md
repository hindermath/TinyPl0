<!--
=== SYNC IMPACT REPORT ===
Version change: [template] → 1.0.0
Constitution created from scratch (first fill of template).

Modified principles: N/A (first authoring)
Added sections: Core Principles (I–VI), Technology Constraints, Development Workflow, Governance
Removed sections: N/A

Templates requiring updates:
- .specify/templates/plan-template.md  ✅ reviewed – Constitution Check section present; no updates required
- .specify/templates/spec-template.md  ✅ reviewed – scope/requirements alignment confirmed
- .specify/templates/tasks-template.md ✅ reviewed – task structure aligns with principles
- .specify/templates/checklist-template.md ✅ reviewed – no constitution references to update

Follow-up TODOs:
- RATIFICATION_DATE set to 2026-02-19 (inferred from first commit in Lastenheft_IDE.md dialogue history;
  verify against earliest Git tag if desired).
=== END SYNC IMPACT REPORT ===
-->

# TinyPl0 Constitution

## Core Principles

### I. Didaktische Klarheit (Pedagogical Clarity)

Code MUST prioritize readability and educational value over performance or cleverness.
All source code, comments, and documentation MUST be written in German (the project's target audience
is German-speaking Fachinformatiker trainees). Every compiler phase (Lexer, Parser, Symbol Table,
Code Generator, VM) MUST remain clearly separated and independently comprehensible.
Implementation shortcuts that obscure the learning path are forbidden.

**Rationale**: TinyPl0 is a teaching artefact. A trainee reading the code must be able to trace
the full compilation pipeline step-by-step without expert guidance.

### II. Historische Kompatibilität (Historical Compatibility)

The C# implementation MUST faithfully reproduce the observable behaviour of the Pascal reference
source (`pl0c.pas`) for all programs within the `classic` dialect. Historical quirks (e.g. `[`→`<=`,
`]`→`>=` mapping; max identifier length 10; max nesting 3 levels; only integer type; no procedure
parameters) MUST be preserved without "fixing" them. Any deliberate deviation from the reference MUST
be documented explicitly (e.g. `?`/`!` I/O extensions in `extended` dialect).

No language extensions beyond the consolidated PL/0 definition (PL0.md + ANTLR reference) are
permitted. No compiler optimisations (peephole, SSA, etc.). The VM target is P-Code only — no JIT
or IL back-end.

**Rationale**: The project documents the migration from Pascal to C#. Behavioural fidelity is
the primary correctness criterion; the golden-master tests enforce it.

### III. Testgetriebene Qualität (Test-Driven Quality)

Every language rule from the consolidated EBNF and every VM opcode MUST be covered by at least one
entry in `tests/data/expected/traceability/matrix.json`. All 41+ mandatory catalog cases in
`tests/data/expected/catalog/cases.json` MUST pass on every build. Golden-master P-Code artefacts
(`tests/data/expected/code/`) MUST be regenerated via `./scripts/update-golden-code.sh` after any
intentional code-generation change — never edited by hand.

New test cases MUST follow the four-step procedure: (1) add `.pl0` source, (2) add catalog entry,
(3) generate golden artefact, (4) update traceability matrix. Skipping any step is a build violation.

**Rationale**: Deterministic, reproducible tests are the only way to guarantee behavioural
compatibility across refactoring iterations and new features.

### IV. Strikte Modularchitektur (Strict Module Architecture)

Dependency rules are enforced by `ArchitectureGuardTests` and MUST NOT be violated:

```
Pl0.Cli  → Pl0.Core, Pl0.Vm
Pl0.Vm   → Pl0.Core
Pl0.Core → (no project dependencies)
Pl0.Ide  → Pl0.Core, Pl0.Vm, Terminal.Gui  (Terminal.Gui is the ONLY permitted NuGet package)
```

Circular dependencies are forbidden. `Pl0.Ide` MUST use the instance-based Terminal.Gui v2 lifecycle
(`Application.Create().Init()` / `app.Run<T>()` / `app.Dispose()`). Static `Application` calls from
Terminal.Gui v1 MUST NOT be used.

**Rationale**: Clear module boundaries keep each layer independently testable and prevent the
monolithic coupling of the original Pascal source from re-emerging in the C# port.

### V. Fehlerdiagnose statt Ausnahmen (Diagnostics Over Exceptions)

The compiler pipeline MUST NOT throw exceptions to signal compilation errors. All errors MUST be
collected as `CompilerDiagnostic` / `LexerDiagnostic` entries. `CompilationResult.Success` is
`true` only when `Diagnostics.Count == 0`. Callers MUST check `Success` before invoking the VM.

Runtime errors in the VM (e.g. division by zero) MUST produce a defined diagnostic or error code
rather than an unhandled exception reaching the CLI surface.

**Rationale**: Structured diagnostics allow multi-error reporting, deterministic tests, and
clean CLI exit codes — essential for both the teaching context and the CI pipeline.

### VI. Git-Workflow (Git Workflow)

`main` is a protected branch. No commits or pushes directly to `main` are permitted. Every change
MUST be delivered via a pull request. The `Pl0.Ide` project version (`Major.Minor.Patch.Build` in
`Pl0.Ide.csproj`) MUST be aligned before each commit: `Minor` = current PR number, `Patch` = commit
count in the PR branch, `Build` = incremented build counter. `Version`, `AssemblyVersion`, and
`FileVersion` MUST be kept in sync.

**Rationale**: Transparent history and versioning allow trainees to trace each feature increment
back to its requirement, reinforcing the Requirements Engineering teaching goal.

## Technology Constraints

- **Platform**: .NET 10 / C# 14 (collection expressions, primary constructors, `record` types)
- **Test framework**: xUnit with `[Fact]` attributes; test classes are `public sealed class` with
  suffix `Tests`
- **TUI framework**: Terminal.Gui 2.x (Pl0.Ide only)
- **Documentation**: DocFX with `modern` template; API reference generated from XML doc comments
- **CI**: `dotnet restore` → `dotnet build` → `dotnet test` pipeline MUST remain green
- **No additional NuGet packages** may be added to `Pl0.Ide` beyond `Terminal.Gui`
- **Coverage tool**: `XPlat Code Coverage` via `dotnet test --collect`

All public APIs (classes, methods, properties, fields) MUST carry XML doc comments
(`<summary>`, `<param>`, `<returns>`). Internal/private members MAY carry doc comments for
pedagogical clarity.

## Development Workflow

1. **Branch first**: create a feature branch before any code change.
2. **Read before edit**: understand existing code before proposing modifications; do not add
   untargeted refactoring or cleanup alongside functional changes.
3. **Fail before green**: when adding a new test case, verify it fails before writing the
   implementation.
4. **Catalog + golden + matrix**: for every new `.pl0` test, follow the four-step procedure
   (see Principle III).
5. **No over-engineering**: implement exactly what is required; no additional abstractions,
   feature flags, or backwards-compatibility shims unless explicitly requested.
6. **Error handling at boundaries only**: validate at system boundaries (user input, CLI args,
   file I/O). Trust internal invariants; do not duplicate validation inside the pipeline.
7. **Commit via PR**: after completing a logical unit of work, open a PR; never push directly
   to `main`.

## Governance

This constitution supersedes all other informal practices and conventions for the TinyPl0 project.
Amendments require:

1. A proposed change described in a PR (with rationale).
2. Version bump following semantic versioning:
   - **MAJOR**: removal or redefinition of a principle, or incompatible governance change.
   - **MINOR**: new principle or section added, or material expansion of guidance.
   - **PATCH**: wording clarification, typo fix, or non-semantic refinement.
3. Update of `LAST_AMENDED_DATE` to the merge date.
4. Review of dependent templates (plan, spec, tasks, checklist) for consistency.

All PRs and code reviews MUST verify compliance with principles I–VI before merge.
Use `CLAUDE.md` or `AGENTS.md` or `.github/copilot-instructions.md` depends of the used agent-assisted work session  as the runtime development guidance file for agent-assisted work sessions.

**Version**: 1.0.0 | **Ratified**: 2026-02-19 | **Last Amended**: 2026-03-01
