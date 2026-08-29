# Command Template: `/speckit.plan`

Use this command to produce an implementation plan from an approved specification.

## Required Actions

1. Populate technical context with real stack details.
2. Execute the Constitution Check gates explicitly:
   - branching and PR flow
   - .NET 10 + C# 14.0 toolchain alignment
   - general architecture evidence scope (`docs/architecture/`, ADRs, quality scenarios, risks/trade-offs)
   - architecture/layer boundaries
   - bilingual CEFR B2 documentation scope
   - complete applicable XML documentation for externally public APIs, no
     global/project-wide CS1591 suppression, and explicit local/private/generated exclusions
   - DocFX plus representative Playwright/axe and `lynx` evidence when API/XML changes trigger it
   - observable test-first Red-Green-Regression evidence or a reasoned pure text/governance `N/A`
   - one governed version/build-counter increment before every build/test invocation
   - coverage gate (`>=70%` minimum, `>=80%` target)
   - NuGet dependency currency and pinning exceptions
   - serialization/data conventions
3. Document concrete project structure for this feature.
4. Record justified exceptions in Complexity Tracking.

## Validation Checklist

- No gate is left unresolved without rationale.
- Test, coverage, dependency, and documentation impacts are planned before implementation.
- Architecture evidence impacts are planned before implementation.
- Exact evidence paths are declared for public XML, TDD, versioning, DocFX,
  text-oriented accessibility, dependencies, statistics, and fail-closed delivery gates.
