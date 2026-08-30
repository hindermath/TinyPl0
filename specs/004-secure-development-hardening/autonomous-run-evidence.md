# Autonomous Run Evidence: 004 Secure Development Hardening

## Identity and Authority

| Field | Value |
|---|---|
| Feature | `004-secure-development-hardening` |
| Binding intake | `requirements/intakes/active/Lastenheft_Secure-Development-Hardening.md` |
| Accepted intake SHA-256 | `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de` |
| Accepted review | `357ed01f-f120-4634-8596-45e7baffa17d`, `Ready` |
| Delivery mode | `MergeAndSync` |
| Bypass authority | Admin bypass explicitly authorized for this run by the current user request |
| Secret authority | None; secrets must not be read, changed, or exposed |
| Evidence owner | TinyPl0 repository owner and autonomous coordinator |
| Run-state path | `specs/004-secure-development-hardening/autonomous-run-state.json` |
| Run-state status | `Active` |

## Resolved Project Policy

- Audience: apprentices, developers, reviewers, and AI agents working on the pedagogical TinyPl0 compiler.
- Language and readability: German first, English second, both at CEFR B2 for learner-facing material.
- Accessibility: text-first delivery and WCAG 2.2 AA wherever applicable; no essential meaning only in color, layout, or pointer interaction.
- Security baseline: NIST SSDF and CWE Top 25 always apply. Other standards require an explicit `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp` decision.
- Runtime: C# 14 on .NET 10 is memory-safe, but this does not replace secure API, I/O, dependency, error, architecture, or supply-chain review.
- Execution boundary: exactly one intake is implemented. No secret access, external publication beyond the authorized GitHub delivery, or follow-up feature is implicit.

## Model Routing

| Phase group | Role | Local profile | Resolution |
|---|---|---|---|
| Specify through Analyze | `frontier-reasoning` | `codex-frontier-auto` | Aligned after local catalog refresh |
| Implement | `long-running-implementation` | `codex-implementation-auto` | Aligned after local catalog refresh |

Concrete model identifiers remain in the local runner configuration and will be recorded by the run-state wrapper at phase boundaries.

## Scope and Convergence

| Gate | State | Evidence or disposition |
|---|---|---|
| Preflight | Pass | Clean `main`, terminal prior run, current review hash, explicit authority, and aligned routing |
| Specify | Pass | `spec.md`, payload `9d8a600c…e85a607`, phase result `b21ced8b…baea15` |
| Clarify | Pass | Zero questions and no material ambiguity; `clarification-report.md`, phase `29eaf4e3…52e9217` |
| Requirements checklist | Pass | `52/52`, zero material open items; phase `8be14b90…fb6cf8` |
| Plan and plan review | Pass | Resumed review result `e6d8731d…d024cc`; 1 Critical, 3 High, and 6 Medium findings resolved; 0 open at these severities. |
| Tasks | Pass | `tasks.md` contains T001–T110; current post-Analyze hash `5ecde279…926069e`; readiness is 24/48 before implementation. |
| Analyze | Pass | Result `43b49122…aa9ac`, payload `0132d038…4f93b`; 1 Critical, 2 High, and 5 Medium findings resolved, 0 open. |
| Implementation | Open | `tasks.md` and named security evidence |

## Validation and Delivery Integrity

Every invoked validator will record its explicit repository root, exit status, expected output, and proof boundary here before delivery. The intended delivery set will be checked read-only before each commit. Exact-head `PreMerge` evidence will remain temporary; causal `PostMerge` evidence will be created only after the actual merge.

## Remote Delivery and Closeout

| Item | Result | Evidence |
|---|---|---|
| Push | Open | Feature branch after validated delivery set |
| Pull request | Open | Provider URL after publication |
| Required checks | Open | Exact reviewed head |
| Actionable threads | Open | Provider review evidence |
| Admin bypass | Authorized, not yet used | Only for the concrete policy gate after technical evidence passes |
| Merge and main synchronization | Open | Merge commit and equal local/remote default-branch heads |
| Post-merge actions | Open | Manifest-declared lifecycle and final validation |

## Resume and Follow-up

- Checkpoint commit: `8cce89e09ef624e9875d1ca86ea2c878ce8cdd54`
- Last passing gate: current 15-target Series review is `Ready`.
- Next exact action: rerun only `plan-review` in a new routed process, then continue with Tasks.
- Stop boundary: no new autonomous run starts at or after 04:30 Europe/Berlin on 2026-08-31; stop safely no later than 05:30.
- Residual risk: open until classification and validation complete.
- Out-of-scope follow-up: record only in this feature; do not start it implicitly.

## Implementation Ledger — Local Tasks T001–T093

### T001 — Platform and tool preflight

- Observed on `2026-08-30` at checkpoint
  `8cce89e09ef624e9875d1ca86ea2c878ce8cdd54` on branch
  `codex/004-secure-development-hardening`.
- `uname -s`: exit `0`, stdout `Darwin`, no stderr.
- PowerShell: the first shell-interpolated helper invocation exited `1` with a
  parser error because zsh expanded `$PSVersionTable`; the corrected literal
  PowerShell command exited `0` and reported `7.6.5`. The helper error is not
  product or gate evidence.
- `dotnet --version`: exit `0`, `10.0.400`; `docfx --version`: exit `0`,
  `2.78.5`; `lynx --version`: exit `0`, `2.9.3`.
- Global Node/npm checks exited `0` and reported Node `26.7.0` and npm
  `11.19.0`. A managed Node 24 installation was not present. This remains
  `Open` until the authorised A11Y package supplies its lockfile-bound Node 24
  path; the global Node 26 binary is not credited as Node 24 evidence.
- Git branch, HEAD and complete short status all exited `0`. The worktree
  contains only the owned `.specify/feature.json` selector and the accepted
  untracked feature directory.

### T002–T004 — Identity, historical phases, gate contract, and Analyze

- `validate-autonomous-run-state.ps1`: exit `0`; run ID, feature, branch,
  stage `Implement`, status `Active`, and task baseline `0/110` matched.
- The byte-SHA-256 comparison of intake, review result, review request, and
  series manifest exited `0` with `PASS: identity and accepted byte hashes`.
- Historical Specify, Plan, Plan Review, and Tasks result-file hashes matched
  the immutable run-state entries. Semantic Clarify, Checklist, and Analyze
  validation each exited `0`; Analyze payload SHA-256 is
  `0132d0389942d8c718509013141529753abf1dd39cd9d509ae0de7e91564f93b`.
- The first local gate-contract helper used non-existent convenience field
  names and exited nonzero. The corrected schema-driven check used
  `gateId`, `exactCommands`, and `requiredRunnerOrPlatformTokens`, exited `0`,
  and proved JSON Schema 2020-12, 31 unique IDs, 25 executable `Applicable`
  gates, and six reasoned, non-executing `N/A` gates. Requirements byte hash:
  `ab1524b4d3b546fc44cf94513b7ce7600b7de20f7c4508faba4d8c505fb5c96a`.

### T005–T008 — Evidence areas, delivery set, authority, and intake boundary

The serialized implementation sections are: Assessment/Findings;
Architecture/Threats; VM Red/Green; Six Conditional Packages; Security/ASVS;
Dependencies/Supply Chain; DocFX/A11Y; Version/Build; Coverage/Golden;
Statistics; Delivery Set; PreMerge; Review/Admin Bypass; Merge/PostMerge; and
Retrospective. Missing later proof remains `Open` and is not a pass.

- Assessment/Findings: `Open` until T009–T020.
- Architecture/Threats: `Open` until T021–T027.
- VM Red/Green: `Open` until T048–T061.
- Six Conditional Packages: `Open`; no package is authorised before T013 and
  its immutable red/non-trigger evidence.
- Security/ASVS and Dependencies/Supply Chain: `Open` until T062–T074.
- DocFX/A11Y: `Open`; managed Node 24, axe, and lynx evidence is mandatory.
- Version/Build, Coverage/Golden, Statistics, and final Delivery Set: `Open`.
- PreMerge, Review/Admin Bypass, Merge/PostMerge, and Retrospective are outside
  this local implementation phase and remain `Open` for T094–T110.
- `git merge-base main HEAD` is the checkpoint commit. The initial delivery
  set is `.specify/feature.json` plus
  `specs/004-secure-development-hardening/`; the intended later set is limited
  to task-declared paths. Ignored runtime evidence, active intake/series paths,
  and unrelated files are excluded. The index was not changed.
- Delivery mode remains `MergeAndSync`; this phase has no remote authority.
  Admin bypass is narrowed to a later concrete branch-policy blocker after
  complete technical and independent-review evidence and is never a review
  substitute. Secret access remains prohibited.
- The active intake and series manifest were checked read-only. Feature `004`
  is the only active scope. Sandbox hardening and every later intake remain
  separate and unstarted. The next authorised action is T009 only.

### T009–T014 — Canonical assessment and finding authorisation

- The exact Quickstart inventory exited `0`: twelve files, counts
  `12/13/15/10/13/11/12/13/17/17/12/12`, 157 total and unique IDs, and
  ordered compendium parity.
- The baseline manifest input SHA-256 is
  `82449d57f2e072cb93e0066e7e1eee112219c9836cd51acd75027ec8436ec916`.
  Observed CL-09/CL-12 version `2.2.0` differs from manifest `2.1.0`.
- `assessment.json` was generated through a temporary PowerShell 7 writer,
  schema-tested before atomic replacement, and independently rechecked for
  canonical order, 157 IDs, status relations, distinct Owner/Reviewer roles,
  exact paths, and the CL-12 boundary. Its post-review SHA-256 is
  `8c16e248042c53b71f41704a103cf574787849b9dbdb461777e4389e3bc6be63`.
- The first independent-review helper represented an empty positive-evidence
  pipeline as one null object and exited nonzero. The corrected explicit loop
  exited `0`; this helper correction changed no assessment conclusion.
- Exactly five packages were authorised by actual `Applicable` plus
  `Not Fulfilled` evidence: baseline, supply chain, CVD, gitignore, and A11Y.
  `FND-GOV-001` is a truthful non-trigger because its unchanged semantic
  validator passed at the checkpoint. `FND-HTTP-001` remains no-edit `Open`.
- T014 exited `0` and proved no pre-authorisation change under `src/`,
  `.github/workflows/`, `scripts/`, `.gitignore`, or any maintained agent file.

### T015–T020 — Immutable red evidence and one non-trigger

The immutable validator basis is `plan.md`
`8b9108361cf3d7adb03d202b471b90982cf6b48b2053d6e379cb2510d3a5a71b`
and `gate-requirements.json`
`ab1524b4d3b546fc44cf94513b7ce7600b7de20f7c4508faba4d8c505fb5c96a`.

- T015 baseline: expected exit `10`, manifest/version and three path gaps.
- T016 supply chain: expected exit `11`, pinned tool and SBOM/hash/VEX/SLSA
  evidence gaps; no restore/network failure.
- T017 CVD: expected exit `12`, missing policy and `security.txt`.
- T018 gitignore: expected exit `13`, root deny/synthetic sentinel gap; no
  secret content or private agent state was read.
- T019 A11Y: expected exit `14`, managed Node 24/npm/axe/lynx/API-page and
  lockfile inventory gaps.
- T020 governance: exit `0`; non-trigger. The five agent surfaces and templates
  remain unchanged.

## Resume Audit — 2026-08-30T08:19:41Z

- Branch and feature identity match `codex/004-secure-development-hardening` and `.specify/feature.json`.
- All four accepted intake/review/manifest hashes match; review `357ed01f-f120-4634-8596-45e7baffa17d` validates as current `Series` / `Ready` with all 15 targets.
- Constitution mirrors, installed autonomous preset, agent guidance, and checkpoint history show no repository drift. The only tracked worktree change is the owned feature selector; all new files are owned by this run.
- Codex routing is `Aligned`: frontier and implementation roles still resolve to their recorded profiles, models, and reasoning efforts.
- The last completed phase result no longer binds the current `plan.md` because the interrupted review partially remediated it before ending without `plan-review.result.json`. This is an uncertain in-flight operation, not a failed plan decision.
- Current user authority explicitly renews `MergeAndSync` and Admin-Bypass for this and subsequent serial runs. Secret access and out-of-series work remain unauthorized.
- Drift disposition: `NeedsRevalidation`; rerun only `plan-review` in a fresh process. No accepted Specify, Clarify, or Checklist phase is repeated.
- Resume result: the fresh `plan-review` completed at `2026-08-30T08:47:05Z`; result hash `e6d8731de740577b2603094b0a1f2eeda824bfb7853dfecbe756e5b239d024cc`, payload hash `e4203dba842f728c7fdf27e56e354b5d542d267b525a83e425c2b6b2926a5cc1`, and zero open Critical/High/Medium findings.

## Implement Resume Audit — 2026-08-30T10:03:24Z

- The first implementation attempt completed T001–T027 and stopped fail-closed. Result bytes: `bd36e5e2481dc8ab4cff5f5f06eaba774e15a93d4a005b404f2d8709969d74c9`; task payload: `3db6fa7882462026a04f06f4066f386040e02dbe5d68b213cc090efc7fdb3220`.
- Blocker: no genuine Node 24 runtime was visible inside the routed sandbox and npm registry resolution failed there. Node 26 was correctly rejected; no lockfile or gate evidence was fabricated.
- Orchestrator remediation: Homebrew `node@24` 24.20.0 was installed keg-only. `/opt/homebrew/opt/node@24` now resolves to `../Cellar/node@24/24.20.0`; `node --version` reports `v24.20.0`, npm reports `11.19.0`, and `npm ping` returned `PONG`.
- The pinned CycloneDX 6.2.0 version exists, but the interrupted implementation used the wrong NuGet package ID `dotnet-cyclonedx`. The official package ID is `CycloneDX`; only the manifest key was corrected, while version `6.2.0` and command `dotnet-CycloneDX` remain unchanged.
- Accepted intake hashes and the current 15-target Ready review still validate; Codex routing remains Aligned. All dirty paths are owned by T001–T027 and the six authorised finding surfaces; no runtime evidence is part of the delivery set.
- Current user authority again proves `MergeAndSync` plus narrowly scoped Admin-Bypass. Resume disposition: `NeedsRevalidation`; continue only `implement` from T028 and do not repeat T001–T027.

## Git Boundary Resume Audit — 2026-08-30T10:29:00Z

- The second implementation attempt completed T028–T048, including all six conditional package green gates, PR-slot revalidation, and the VM red-test source hashes.
- The routed workspace correctly stopped before T049 because it cannot create `.git/index.lock`; it did not run an unversioned `dotnet test` and performed no remote action.
- The orchestrator holds current explicit commit authority. With HEAD commitcount `447`, the next versioned commit is bound to `1.72.448.33`: canonical provisional PR Minor `72`, resulting Patch `448`, and Build increment `32 -> 33`.
- Next exact action: validate the complete intended delivery set, commit only those paths locally, prove Patch `448`, then run the exact T049 budget red filter.
- Intended delivery validation passed for 16 changed tracked paths and 34 explicitly named untracked paths, with zero unrelated untracked files; the only remediation was removal of prohibited line-end whitespace from owned Markdown artifacts.
- Invocation `VM-RED-BUDGET-01` on `1.72.448.33` is invalid evidence: the command was interrupted while NuGet restore was still running and never reached the VM test. It is retained as a failed infrastructure attempt, not counted as T049 red proof.
- `dotnet restore TinyPl0.sln --ignore-failed-sources --disable-parallel` then completed successfully for all five projects. Before retrying any `dotnet test`, the governed version advances to `1.72.449.34` in the next commit.
- Invocation `VM-RED-BUDGET-02` ran on clean commitcount/Patch `449`, version `1.72.449.34`. The exact `VM-TDD-GATE-001` filter completed restore and build, discovered exactly two tests, and both failed in 12 ms at the expected missing five-parameter `VirtualMachineOptions` constructor (`Assert.NotNull`). Exit `1` is accepted red evidence for the absent instruction-budget contract; no infrastructure, timeout, or unrelated failure remained.
- Before the separate T050 options red test, the governed version advances to `1.72.450.35` in the next commit.
- Invocation `VM-RED-OPTIONS-01` ran on clean commitcount/Patch `450`, version `1.72.450.35`. The exact `VM-CONFIGURATION-GATE-001` invalid-options filter completed restore and build, discovered exactly two tests, and both failed in 13 ms because the five-parameter options constructor is absent. The tests therefore cannot yet reach the required fail-safe StackSize/InstructionBudget validation; exit `1` is accepted red evidence and the three test source files remain unchanged.
- T051–T054 implement the five-parameter options contract, pre-allocation shared validation, identical batch/step N/N+1 counting, terminal non-duplicating Step failure, and localized codes 207/208. No build/test was run in the routed sandbox. The next governed commit is `1.72.451.36` before T055.
- Invocation `VM-GREEN-BUDGET-01` ran on clean commitcount/Patch `451`, version `1.72.451.36`. The unchanged exact budget filter completed restore/build and passed exactly 2/2 tests in 13 ms, proving batch/step N/N+1 behavior without timeout. Before T056, the governed version advances to `1.72.452.37`.
