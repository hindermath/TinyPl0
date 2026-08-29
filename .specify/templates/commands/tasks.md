# Command Template: `/speckit.tasks`

Use this command to generate an executable task list from `plan.md` and `spec.md`.

## Required Actions

1. Organize tasks by user story for independent delivery.
2. Include the corresponding test task before implementation and require an
   observable intended red result, the unchanged test turning green after the
   minimum implementation, and regression evidence. Use a reasoned `N/A` with
   re-evaluation trigger only for pure text/governance work.
3. Include architecture evidence tasks when the feature affects structure, interfaces, runtime behavior, deployment, or quality attributes:
   - `docs/architecture/` updates
   - ADRs in `docs/architecture/adr/`
   - quality-scenario validation
   - architecture risks / technical debt review
4. Include documentation tasks:
   - bilingual updates (German block first, then English)
   - complete applicable XML documentation for externally public APIs
   - removal of global/project-wide CS1591 suppression, excluding local/private/generated surfaces
   - `docfx docfx.json`, representative Playwright/axe, and `lynx` runs when API/XML docs changed
5. Include coverage and dependency tasks:
   - coverage evidence for `>=70%` minimum and `>=80%` target tracking
   - `dotnet list package --outdated` review and update tasks
6. Include PR preparation task (purpose, touched projects, test evidence, config/API impact).
7. Include one version/build-counter task immediately before every governed
   build or test invocation, plus dependency, statistics, and exact fail-closed
   delivery-set/gate tasks with evidence paths.

## Validation Checklist

- Every code change has corresponding tests.
- Documentation and governance tasks are present.
- Task ordering supports incremental, verifiable delivery.
- Coverage and dependency currency tasks are explicitly scheduled.
- Architecture evidence tasks are explicitly scheduled when architecture is in scope.
