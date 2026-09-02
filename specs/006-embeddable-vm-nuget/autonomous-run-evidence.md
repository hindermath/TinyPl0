# Autonomous Run Evidence: Embeddable VM and NuGet Packages

## Identity and Authority

| Field | Value |
|---|---|
| Feature | `006-embeddable-vm-nuget` |
| Binding intake | `requirements/intakes/archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md` (byte-identical closeout relocation) |
| Delivery mode | `MergeAndSync` |
| Provider authority | Current user request explicitly authorizes package publication; credentials must remain undisclosed and OIDC is preferred. |
| Admin bypass | Explicitly authorized only for repository-policy delivery after technical, risk, evidence, and exact-head gates pass. |
| Run-state path | `specs/006-embeddable-vm-nuget/autonomous-run-state.json` |
| Run-state status | `Completed`; product, release, public verification and causal series closeout complete |

## Frozen Inputs

| Path | SHA-256 |
|---|---|
| `requirements/intakes/archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md` | `a6e752dcc372c26626cf40cc0b1fb1da1a195a895f51129b87dc0920310b64d5` |
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
| Implementation | Pass | T001-T050 complete; product, release, provider verification and closeout are terminal. |

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

## Remote Delivery and Public Evidence

| Item | Result | Evidence |
|---|---|---|
| Feature delivery | Pass | PR [#79](https://github.com/hindermath/TinyPl0/pull/79), merge `6a886aad0a5d63d53f8352b5bd22972cb265a934` |
| Release closeout | Pass | Release PR [#33](https://github.com/hindermath/TinyPl0/pull/33), merge `ff68fabd5a44d754dc50cdaac167f97ef2676a87` |
| OIDC publication | Pass | Release run [33682479577](https://github.com/hindermath/TinyPl0/actions/runs/33682479577); both `.nupkg` and `.snupkg` pairs created and both public package pushes succeeded without a long-lived API key |
| Recovery exact head | Pass | PR [#80](https://github.com/hindermath/TinyPl0/pull/80), head `3b746643f0be3e026660addb83c900be110c2d34`; technical checks pass, zero unresolved review threads, independent Copilot review with zero new comments, explicit unchanged-head Owner approval |
| PreMerge | Pass | Schema 2.0, normalized SHA-256 `b3492f8e399583311768f7d355adf643086a509d3d73cc5374f9aec510c293b8` |
| Narrow bypass and merge | Pass | Only the unavailable Claude review-policy check was bypassed; merge `baeca77a313d5acd4928531e4fba5e332ddef706`; local and remote `main` synchronized |
| Verification-only recovery | Pass | Run [33687547664](https://github.com/hindermath/TinyPl0/actions/runs/33687547664) on the merge commit; `release-please`, `build-release` and `publish-nuget` skipped, only `verify-public` succeeded |
| Public packages | Pass | [TinyPl0.Core 0.4.0](https://www.nuget.org/packages/TinyPl0.Core/0.4.0) and [TinyPl0.Vm 0.4.0](https://www.nuget.org/packages/TinyPl0.Vm/0.4.0) |
| Public integrity | Pass | NuGet.org repository signatures valid; every unsigned ZIP entry matches the immutable source package; evidence artifact SHA-256 `5c62464d3c668de1444636707161a707768ee87c933b4a490199e6ab5b8f5d8b` |
| Clean public consumer | Pass | NuGet.org-only .NET 10 restore compiled PL/0 and proved equal Run/Step completion reason, instruction count and output |
| Secrets | Pass | Trusted Publishing used OIDC; no credential value entered source, arguments, evidence or chat output |

The initial public verification failure was evidence-path failure, not a
publication failure. The immutable package pair was never pushed again.
Repository signing legitimately changed each complete public `.nupkg` hash;
the final proof therefore verifies the repository signature and compares every
unsigned ZIP entry against the source artifact.

## Causal Series Closeout

- The accepted intake moved byte-identically to
  `requirements/intakes/archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md`;
  SHA-256 remains
  `a6e752dcc372c26626cf40cc0b1fb1da1a195a895f51129b87dc0920310b64d5`.
- The predecessor manifest and receipt remain byte-identical below
  `requirements/intakes/series-archive/tinypl0-delivery/20260902T215535Z/`.
- The successor keeps 15 targets, 5 roots and 11 binding dependencies.
  Embeddable VM/NuGet is `Completed`; only
  `requirements/intakes/active/Lastenheft_Quellcode_Doku.md` becomes the
  declared `Eligible` successor. It was not executed.
- Schema-2.0 PostMerge evidence binds the accepted PreMerge hash, reviewed
  head, actual recovery merge, public evidence and lifecycle closeout.

## Terminal Boundary

- Checkpoint commit: recovery merge
  `baeca77a313d5acd4928531e4fba5e332ddef706`.
- Last passing gate: 50/50 tasks, exact-head PreMerge, MergeAndSync, OIDC
  publication, verification-only public proof, byte-identical intake archive,
  successor-series validation, PostMerge evidence and retrospective.
- Next exact action for this autonomous run: `N/A`.
- Follow-up feature: not started; it requires a separate explicit run.
