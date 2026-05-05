# Command Template: `/speckit.specify`

Use this command to create or update a feature specification.

## Required Actions

1. Create a branch for the feature (never work directly on `main`).
2. Fill `spec.md` with prioritized, independently testable user stories.
3. Define measurable outcomes and explicit edge cases.
4. Fill the Constitution Requirements and applicability sections with concrete impacts:
   - general architecture evidence impact (`docs/architecture/`, ADRs, quality scenarios, risks/trade-offs)
   - .NET 10 + C# 14.0 toolchain impact
   - NuGet dependency currency impact
   - coverage thresholds (`>=70%`, target `>=80%`)
   - layering/shared logic placement
   - bilingual documentation impact (German first, English second, CEFR B2)
   - XML documentation and DocFX impact
   - Red-Green-Refactor test impact
   - data contract implications

## Validation Checklist

- Each story can be tested independently.
- Requirements are implementation-agnostic.
- Constitution alignment items are complete and non-empty.
- Toolchain/dependency/coverage constraints are explicit and measurable.
- Architecture applicability and evidence expectations are explicit when the feature affects design structure.
