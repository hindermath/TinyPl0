<!--
=== SYNC IMPACT REPORT ===
Version change: 1.2.0 → 1.3.0

Bump rationale:
- MINOR: Expanded the statistical documentation policy so manual-effort
  estimates now include production code, test code, and documentation
  together, and added explicit month and TVoeD calendar assumptions.

Modified principles:
- Keine

Added sections:
- Keine

Removed sections:
- Keine

Templates requiring updates:
- .specify/templates/plan-template.md ⚠ pending review
- .specify/templates/spec-template.md ✅ reviewed; no change required
- .specify/templates/tasks-template.md ⚠ pending review
- .specify/templates/commands/*.md ⚠ pending (Verzeichnis nicht vorhanden)

Runtime guidance docs requiring updates:
- AGENTS.md ✅ updated
- CLAUDE.md ✅ updated
- GEMINI.md ✅ updated
- .github/copilot-instructions.md ✅ updated
- docs/project-statistics.md ✅ updated

Follow-up TODOs:
- Plan- und Task-Templates bei weiterer Propagierung auf denselben Wortlaut pruefen.
=== END SYNC IMPACT REPORT ===
-->

# TinyPl0 Constitution

## Core Principles

### I. Didaktische und sprachliche Klarheit (Pedagogical and Linguistic Clarity)

Code MUST prioritize readability and educational value over performance or cleverness.
Projekttexte MUST be bilingual with German first and English second for all didactic
content (code comments, API docs, guides, examples, and learner-facing explanations).
Both language blocks MUST target CEFR/GER level B2 readability.

All public and internal source-level APIs MUST use complete XML documentation where
applicable (`<summary>`, `<param>`, `<returns>`, `<exception>`; `<remarks>` and
`<example>` when instructive). Missing XML documentation on public members MUST be
handled as a build error (CS1591 MUST NOT be globally suppressed).

Comments MUST explain why (decision, trade-off, constraint), not only what.
At didactically relevant points, additional line or block comments MUST be added in
German and English to make reasoning explicit for trainees.

**Rationale**: TinyPl0 is a teaching artefact for mixed-language training cohorts.
Clear bilingual B2 documentation is required so both native and non-native trainees
can follow the full compilation pipeline without hidden assumptions.

### II. Historische Kompatibilität (Historical Compatibility)

The C# implementation MUST faithfully reproduce the observable behaviour of the Pascal
reference source (`pl0c.pas`) for all programs within the `classic` dialect.
Historical quirks (e.g. `[` -> `<=`, `]` -> `>=` mapping; max identifier length 10;
max nesting 3 levels; only integer type; no procedure parameters) MUST be preserved
without "fixing" them. Any deliberate deviation from the reference MUST be documented
explicitly (e.g. `?`/`!` I/O extensions in `extended` dialect).

No language extensions beyond the consolidated PL/0 definition are permitted.
No compiler optimisations (peephole, SSA, etc.). The VM target is P-Code only;
no JIT or IL backend.

**Rationale**: The project documents the migration from Pascal to C#.
Behavioural fidelity is the primary correctness criterion.

### III. Testgetriebene und nachweisbare Qualität (Test-Driven and Verifiable Quality)

Unit tests for new behaviour MUST follow Red-Green-Refactor where feasible:
first failing test, then implementation, then cleanup.

Every language rule from the consolidated EBNF and every VM opcode MUST be covered by
at least one entry in `tests/data/expected/traceability/matrix.json`. All mandatory
catalog cases in `tests/data/expected/catalog/cases.json` MUST pass on every build.
Golden-master P-Code artefacts (`tests/data/expected/code/`) MUST be regenerated via
`./scripts/update-golden-code.sh` after any intentional code-generation change and
MUST NOT be edited by hand.

New test cases MUST follow the four-step procedure: (1) add `.pl0` source,
(2) add catalog entry, (3) generate golden artefact, (4) update traceability matrix.
Skipping any step is a process violation.

**Rationale**: Deterministic and traceable tests are required to preserve behavioural
compatibility and to teach correct TDD workflows.

### IV. Strikte Modularchitektur (Strict Module Architecture)

Dependency rules are enforced by `ArchitectureGuardTests` and MUST NOT be violated:

```
Pl0.Cli  -> Pl0.Core, Pl0.Vm
Pl0.Vm   -> Pl0.Core
Pl0.Core -> (no project dependencies)
Pl0.Ide  -> Pl0.Core, Pl0.Vm, Terminal.Gui  (Terminal.Gui is the ONLY permitted NuGet package)
```

Circular dependencies are forbidden. `Pl0.Ide` MUST use the instance-based
Terminal.Gui v2 lifecycle (`Application.Create().Init()` / `app.Run<T>()` /
`app.Dispose()`). Static `Application` calls from Terminal.Gui v1 MUST NOT be used.

**Rationale**: Clear module boundaries keep each layer independently testable and
prevent monolithic coupling from re-emerging.

### V. Fehlerdiagnose statt Ausnahmen (Diagnostics Over Exceptions)

The compiler pipeline MUST NOT throw exceptions to signal compilation errors.
All errors MUST be collected as `CompilerDiagnostic` / `LexerDiagnostic` entries.
`CompilationResult.Success` is `true` only when `Diagnostics.Count == 0`.
Callers MUST check `Success` before invoking the VM.

Runtime errors in the VM (e.g. division by zero) MUST produce a defined diagnostic
or error code rather than an unhandled exception reaching the CLI surface.

**Rationale**: Structured diagnostics allow multi-error reporting, deterministic tests,
and clean CLI exit codes.

### VI. Git-Workflow (Git Workflow)

`main` is a protected branch. No commits or pushes directly to `main` are permitted.
Every change MUST be delivered via pull request.

The `Pl0.Ide` project version (`Major.Minor.Patch.Build` in `Pl0.Ide.csproj`) MUST
be aligned before each commit: `Minor` = current PR number, `Patch` = commit count in
PR branch, `Build` = incremented build counter. `Version`, `AssemblyVersion`, and
`FileVersion` MUST be kept in sync.

**Rationale**: Traceable history and deterministic versioning are required for
teaching and reproducible releases.

## Technology Constraints

- **Platform**: .NET 10 / C# 14
- **Test framework**: xUnit with `[Fact]`; test classes are `public sealed class`
  with suffix `Tests`
- **TUI framework**: Terminal.Gui 2.x (Pl0.Ide only)
- **Documentation tooling**: DocFX with `modern` template
- **Coverage tool**: `XPlat Code Coverage` via `dotnet test --collect`
- **CI gate**: `dotnet restore` -> `dotnet build` -> `dotnet test` MUST stay green
- **Pl0.Ide package policy**: No additional NuGet packages beyond `Terminal.Gui`
- **Documentation quality gate**: CS1591 MUST remain active for public APIs; missing
  public XML docs are build-breaking

## Development Workflow

1. **Branch first**: create a feature branch before any code change.
2. **Read before edit**: understand existing code before modifying behaviour.
3. **Test first where applicable**: create/adjust tests and ensure the failing state
   is observable before implementation.
4. **Catalog + golden + matrix**: for every new `.pl0` test, follow the four-step
   process from Principle III.
5. **Documentation parity**: when API signatures or XML comments change, regenerate
   DocFX output in the same commit/PR.
6. **DocFX execution rule**: whenever documentation is changed, run external `docfx`
   from repository root (`docfx.json` in project root).
7. **No over-engineering**: implement only required scope.
8. **Commit via PR**: open PR for each logical work unit; never push to `main`.

### Statistical Documentation

`docs/project-statistics.md` is the mandatory, living statistical ledger for the
repository. It MUST be updated whenever one of the following happens:

1. A Spec-Kit implementation phase is completed or materially re-scoped.
2. An agent-driven work package changes repository content (code, tests, specs,
   plans, tasks, governance, or operational docs).
3. A contributor explicitly requests a statistics refresh.

Every update MUST record, at minimum:

- branch or phase identifier and current status,
- observable git-based work window (first and last date, commit days where possible),
- current or change-based counts for production code, test code, and
  documentation,
- the main work packages or delivered artefacts,
- whether the numbers come from committed history, the working tree, or both,
- a conservative manual-effort baseline using **80 manually created lines per
  workday** for an experienced developer across production code, test code, and
  documentation,
- when time spans are derived, the assumptions for monthly conversion
  (21-22 workdays, typically 21.5) and, if used, TVoeD-style annual leave
  assumptions such as 30 vacation days per year.

Manual-effort estimates for a small team MAY be derived from that baseline, but
the formula and assumptions MUST be stated explicitly.

## Governance

This constitution supersedes all other informal practices and conventions for TinyPl0.
Amendments require:

1. A proposed change described in a PR with rationale.
2. Version bump using semantic versioning:
   - **MAJOR**: removal/redefinition of principles or incompatible governance change.
   - **MINOR**: new principle/section or material expansion of guidance.
   - **PATCH**: wording clarification, typo fix, non-semantic refinement.
3. Update of `LAST_AMENDED_DATE` to merge date.
4. Consistency review of dependent templates and runtime guidance files.

Compliance reviews in PRs MUST verify adherence to principles I-VI, including
bilingual B2 documentation quality and XML documentation completeness.
Missing required documentation MUST be completed before merge.

Use `CLAUDE.md`, `GEMINI.md`, `AGENTS.md`, and
`.github/copilot-instructions.md` as runtime agent-specific guidance files.
Use `docs/project-statistics.md` for the living project-statistics ledger and
manual-effort baseline tracking.

**Version**: 1.3.0 | **Ratified**: 2026-02-19 | **Last Amended**: 2026-03-22
