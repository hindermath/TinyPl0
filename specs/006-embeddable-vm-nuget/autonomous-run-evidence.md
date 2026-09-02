# Autonomous Run Evidence: Embeddable VM and NuGet Packages

## Identity and Authority

| Field | Value |
|---|---|
| Feature | `006-embeddable-vm-nuget` |
| Binding intake | `requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md` |
| Delivery mode | `MergeAndSync` |
| Provider authority | Current user request explicitly authorizes package publication; credentials must remain undisclosed and OIDC is preferred. |
| Admin bypass | Explicitly authorized only for repository-policy delivery after technical, risk, evidence, and exact-head gates pass. |
| Run-state path | `specs/006-embeddable-vm-nuget/autonomous-run-state.json` |
| Run-state status | `Active`; local implementation complete, remote delivery pending |

## Frozen Inputs

| Path | SHA-256 |
|---|---|
| `requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md` | `a6e752dcc372c26626cf40cc0b1fb1da1a195a895f51129b87dc0920310b64d5` |
| `requirements/intakes/series/tinypl0-delivery/intake-review-result.json` | `09d26eb8f267b92ce21ad9acaa0d316d29e7b51d893c8e3eed7910e7199cfea2` |
| `requirements/intakes/series/tinypl0-delivery/intake-review-request.json` | `b23706568d8c66a62ca6df0dfd506378166a5d8108bf3012d30ec2802a3b7e04` |
| `requirements/intakes/series/tinypl0-delivery/manifest.json` | `c73a65227e91123ccf017b03720695ad1c21b5910eb966a79a824069c8ff0a17` |

## Standards Applicability

- Applicable: NIST SSDF, CWE Top 25, C#/.NET secure coding, STRIDE/CAPEC,
  SBOM, VEX, SLSA/provenance, OpenSSF Scorecard, supply-chain review,
  WCAG 2.2 AA for applicable generated HTML, and text-first A11Y evidence.
- `OWASP ASVS`: N/A because the deliverables are local .NET libraries without
  a web, HTTP, authentication, or API service.
- `Zero Trust`: N/A for the in-process library execution boundary; the OIDC
  publisher identity remains a supply-chain trust boundary.
- `AI-SBOM`: N/A because AI is development tooling only and no model, dataset,
  inference service, or AI runtime is shipped.

## Model Routing

The active preset matrix resolves reasoning phases to `frontier-reasoning`,
implementation to `long-running-implementation`, and retrospective to
`coding-review`. The local catalog is aligned at SHA-256
`7db176ac6bc263526ad6cd67cce9715123393cff0f8774977691b3aa6c04bbfc`.

## Scope and Convergence

| Gate | State | Evidence or disposition |
|---|---|---|
| Preflight | Pass | Clean synchronized main baseline, current Ready review, sole Eligible target, and accepted hashes validated. |
| Specify | Pass | Binding intake preserved in `spec.md`; requirements checklist passes. |
| Clarify | Pass | Zero material ambiguities and zero user questions. |
| Checklists | Pass | Pre-implementation and planning dispositions are complete. |
| Plan review | Pass | Zero Critical, High, or Medium findings; two Low items were assigned before implementation. |
| Tasks and Analyze | Pass | Fifty dependency-ordered tasks and a cross-artifact analysis with no unresolved finding. |
| Implementation | Pass locally | T001-T040 complete; delivery and closeout T041-T050 remain open. |

## Local Validation Snapshot

| Evidence | Result |
|---|---|
| Release build | Pass, 0 warnings and 0 errors |
| Regression and coverage | 307/307 tests pass; overall line rate `71.73%`, overall branch rate `74.12%`; shared kernel line rate `96.50%`, branch rate `85.05%` |
| Run/Step contract | Normal halt, arithmetic, stack, input, host I/O, invalid P-Code, cancellation, budget, idempotence and compatibility cases pass |
| Package pair | `TinyPl0.Core.0.4.0` and `TinyPl0.Vm.0.4.0`, `.nupkg` plus `.snupkg`; VM dependency exactly `[0.4.0]` |
| Independent consumer | Clean local-feed restore, build and execution pass without project references |
| Supply chain | SPDX 2.3, OpenVEX, in-toto/SLSA-style provenance input and four SHA-256 package records generated |
| Dependencies | No known vulnerable NuGet package reported against the explicit public source |
| Documentation | Pinned DocFX 2.78.5 completes with 0 errors and 7 pre-existing link warnings |
| Accessibility | Four representative pages pass Playwright/axe with zero violations; matching Lynx token paths pass |
| Static checks | Changed C# files format-clean; both PowerShell files parse; workflow YAML parses; `git diff --check` passes |

The gate contract was corrected once before delivery evidence was frozen: test
class names, local package-consumer paths, pinned DocFX invocation, and the
feature-merge-before-release lifecycle now name the implemented commands.
Required scope, thresholds, OIDC-only publication, fail-closed behavior, and
acceptance boundaries did not weaken. Every affected local gate was rerun
after this correction; no earlier remote or merge claim was retained.

## Validation and Delivery

The gate contract was declared before the first implementation edit. The
temporary schema-2.0 exact-head `PreMerge` snapshot will be generated only
after the PR candidate is stable, so recording it cannot invalidate its own
head binding. Package publication must fail closed when the authorized
OIDC/provider route, immutable version, paired package identity, or public
consumer evidence is unavailable. No secret value may enter source, command
arguments, logs, evidence, or chat output. Admin bypass remains limited to the
repository-policy merge step and cannot replace a technical, review, risk, or
evidence gate.

## Resume Boundary

- Checkpoint commit: `9f39b2afb90aca406c2a59591f137f64e05b8d82`
- Last passing gate: local implementation and validation through T040.
- Next exact action: freeze the intended delivery set, align the commit-target
  IDE version, commit, push, and open the single feature PR.
- Follow-up feature: forbidden by the current request.
