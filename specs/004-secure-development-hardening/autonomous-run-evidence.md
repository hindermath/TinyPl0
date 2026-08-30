# Autonomous Run Evidence: 004 Secure Development Hardening

## Identity and Authority

| Field | Value |
|---|---|
| Feature | `004-secure-development-hardening` |
| Binding intake | Accepted at `requirements/intakes/active/Lastenheft_Secure-Development-Hardening.md`; byte-preserved terminal archive at `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md` |
| Accepted intake SHA-256 | `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de` |
| Accepted review | `357ed01f-f120-4634-8596-45e7baffa17d`, `Ready` |
| Delivery mode | `MergeAndSync` |
| Bypass authority | Admin bypass explicitly authorized for this run by the current user request |
| Secret authority | None; secrets must not be read, changed, or exposed |
| Evidence owner | TinyPl0 repository owner and autonomous coordinator |
| Run-state path | `specs/004-secure-development-hardening/autonomous-run-state.json` |
| Run-state status | `Completed` after causal closeout |

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
| Tasks | Pass | `tasks.md` contains and completes T001–T110 with conditional non-trigger evidence where declared. |
| Analyze | Pass | Result `43b49122…aa9ac`, payload `0132d038…4f93b`; 1 Critical, 2 High, and 5 Medium findings resolved, 0 open. |
| Implementation | Pass | Exact candidate `1526e64e34371e89aac6d4e6a6e41b5286270a36`; 275/275 tests; complete local, remote, merge, PostMerge, lifecycle, and retrospective evidence through T110. |

## Validation and Delivery Integrity

Every invoked validator will record its explicit repository root, exit status, expected output, and proof boundary here before delivery. The intended delivery set will be checked read-only before each commit. Exact-head `PreMerge` evidence will remain temporary; causal `PostMerge` evidence will be created only after the actual merge.

## Remote Delivery and Closeout

| Item | Result | Evidence |
|---|---|---|
| Push | Completed | Remote feature head `1526e64e34371e89aac6d4e6a6e41b5286270a36` |
| Pull request | Completed | `https://github.com/hindermath/TinyPl0/pull/72` |
| Required checks | Completed | No provider-required status checks; all acceptance-mapped CI, security, docs, A11Y, baseline, and platform jobs passed on the exact head. |
| Actionable threads | Completed | Gitleaks false-positive thread resolved; final open-thread count `0`; human Owner approval `issuecomment-5469201251`. |
| Admin bypass | Authorized and used | Consumed only for the formal one-approval Self-Review policy after complete technical evidence and explicit human approval. |
| Merge and main synchronization | Completed | Merge `e37acee1792911c0b0c2c2115edefe4bcd22f613`; local `main == origin/main == merge`; local and remote feature branches deleted. |
| Post-merge actions | Completed | Schema-2.0 PostMerge evidence, terminal state, retrospective, byte-preserved intake archive, successor series and next-intake index. |

## Resume and Follow-up

- Product checkpoint: reviewed feature head
  `1526e64e34371e89aac6d4e6a6e41b5286270a36`; merge checkpoint
  `e37acee1792911c0b0c2c2115edefe4bcd22f613`.
- Last passing gate: schema-2.0 PostMerge hash
  `f64e2c4be74d13594a711af49e3e3058ce64ddf88b6fa2f145de8abc5c5645af`
  plus validated successor series.
- Next exact action: `$speckit-intake-series-status`; downstream execution
  remains separate and uses the user's current serial-run authority.
- Stop boundary: no new autonomous run starts at or after 04:30 Europe/Berlin on 2026-08-31; stop safely no later than 05:30.
- Residual risk: the 80-% overall coverage target remains `TargetOpen` with
  maintainer ownership; all mandatory thresholds passed.
- Out-of-scope follow-up: `FND-HTTP-001` remains separate; no scope expansion
  occurred in this run.

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
Retrospective. This current freeze supersedes the pre-implementation status
snapshot; missing later proof remains `Open` or `Pending` and is not a pass.

- Assessment/Findings: `Pass` through T072; 157/157 rows, findings, and
  residual risks are reconciled. Exact-head delivery reevaluation remains T092–T103.
- Architecture/Threats: `Pass` through T060 with STRIDE/CIA/CAPEC, arc42,
  general ADR, S-ADR, quality scenarios, risks, and trade-offs.
- VM Red/Green: `Pass` for the accepted T049/T050/T055–T057 invocations and
  T058–T061 reconciliation. The final exact-head full suite remains T087–T091.
- Six Conditional Packages: `Pass` for five authorised red-to-green packages
  and the evidenced `FND-GOV-001` non-trigger; no seventh package was opened.
- Security/ASVS and Dependencies/Supply Chain: `Pass` for the local T062–T074
  evidence. Provider/exact-head supply-chain reevaluation remains T097–T103.
- DocFX/A11Y: `Pass` for T075–T082 with Node 24, axe, separate Lynx, cleanup,
  generated API semantics, and documentation-impact evidence. Remote exact-head
  evidence remains T097–T098.
- Statistics: `Pass` and `CURRENT` through T083–T084; it is frozen.
- Version/Build: accepted early TDD invocations end at `1.72.453.38`.
  Orchestrator alignment to `1.72.454.38` and the T085 commit are `Blocked` in
  this routed phase; final exact-head version/build/coverage remain T086–T093.
- Coverage/Golden and final Delivery Set: `Pending` for T087–T093 and
  T101–T103; no final candidate claim is made here.
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
- Historical sandbox boundary: no genuine Node 24 runtime was visible inside
  the routed sandbox and npm registry resolution failed there. Node 26 was
  correctly rejected, and no lockfile or gate evidence was fabricated. This
  local limitation is resolved for T077–T080 by the accepted and read-only
  revalidated host evidence recorded below; it is not a current gate blocker.
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
- Invocation `VM-GREEN-OPTIONS-01` ran on clean commitcount/Patch `452`, version `1.72.452.37`. The unchanged invalid-options filter completed restore/build and passed exactly 2/2 tests in 15 ms, covering both VM paths and all declared StackSize/InstructionBudget bounds. Before T057, the governed version advances to `1.72.453.38`.
- Invocation `VM-GREEN-L10N-01` ran on clean commitcount/Patch `453`, version `1.72.453.38`. The exact `Pl0.Tests.L10nTests` filter completed restore/build and passed 88/88 tests in 32 ms, including German/English VM messages and four-parameter source compatibility.

## Bounded Implement Resume — T058–T076

This resume consumed the accepted T049/T050/T055–T057 evidence without
repeating a build or test. No `dotnet build`, `dotnet test`, IDE version edit,
commit, push, pull request, intake/series mutation, or remote delivery action
occurred in this bounded phase.

### T058–T061 — Secure VM review and evidence reconciliation

- The bounded VM diff was reviewed against NIST SSDF, CWE-400/CWE-770, the
  C#/.NET rules, and `contracts/vm-hardening-contract.md`. The implementation
  keeps batch and step execution separate, counts instructions rather than
  wall-clock time, adds no CLI/IDE option, validates both limits before stack
  allocation, and exposes only localized diagnostics 207/208.
- Documentation, traceability, architecture, both ADRs, and the threat model
  now describe only the implemented budget/stack boundary. Opcode, OPR,
  deployment, module graph, PL/0 semantics, and golden files are unchanged.
- Accepted source hashes remain identical to the red/green evidence:
  `VirtualMachineTests.cs` `0427a8be7da87b9ef51d761772a503c5843d33af3c18d2b9ed9fe6850410e94b`,
  `SteppableVirtualMachineTests.cs` `9c2ebaab022050e3a2552044910ac59eabb96aaab36696abd40f307aff6a4715`,
  and `L10nTests.cs` `1ab60fcc3a105387eed0d14f4a8b90189c4395115a163299b9092e751691a2b5`.

### T062–T074 — Security, ASVS, supply chain, and boundary review

- The prepared, secret-scanned package inventories were consumed offline.
  Outdated log SHA-256 is
  `6a74e4d3bcc347e1941afa6cfe7c54e4803a44645f7ffbf06a2a4dbbe07412e6`;
  vulnerable log SHA-256 is
  `18053740bd61ffd6e6ce709b03898f29b25f795e864f63d1565b002660978e73`.
  All five projects report no known vulnerable package. Available updates are
  recorded, but no package was changed and no private feed was contacted.
- The CycloneDX 1.7 SBOM has 47 components and SHA-256
  `46e930c23bb224f091f91e346525813b8a859504b195f0b91b1ed81f1783a899`.
  The final ordinal manifest binds 1,385 DocFX files with SHA-256
  `feb58953a86cb31696d7ec4c934b0ed99f6489f4e418c1e34bb19acc5e6d0000`.
  The self-referential evidence page and dependent search index are named
  exclusions. VEX is `NotRequiredNoKnownFinding`; no SLSA level, provenance,
  provider attestation, or Scorecard result is overclaimed.
- The pinned official ASVS input matched SHA-256
  `8201b20eec2908c3380ac600c91c8ba746346fbb808859366abb232027532311`
  and contained exactly 70 Level-1 requirements. The generated JSON preserves
  official order, maps all 70 IDs, binds exact HEAD, and records
  `openCriticalOrHigh: 0`; its current SHA-256 is
  `413a2dd2163b8652796b4df201f471c317c5ad92d507026f2598fb83d7c8b995`.
- `src/Pl0.Cli/Program.cs` remained unchanged during the read-only HTTP review.
  The fixed loopback/static-file scope has no new Critical or High finding;
  `FND-HTTP-001` remains a Medium follow-up without edit authority.
- CRA/regulatory, C3A/C5, SAMM, Zero Trust, MSL, development-tool AI,
  AI-SBOM, product cryptography, DPIA, NIS2, EU AI Act, DORA, serial-run, and
  sandbox boundaries have explicit owners and reevaluation triggers. The six
  N/A gates remain reasoned and non-executing.
- Assessment schema/order, 157 unique IDs, exact-HEAD evidence, reviewer
  separation, and CL-12 boundary passed. Security placeholder, NIST SSDF/CWE,
  ASVS pinned-source, offline dependency, action-SHA, supply-chain, and legal/
  provider boundary checks passed. The bounded independent reconciliation
  found no new Critical/High result; the known A11Y finding remains explicit
  for the later rendered-page gate. Current assessment SHA-256 is
  `e632d0bdf382085132ca45bc246db6f8e454d49807aabaa595423ab4b58032d3`.

### T075–T076 — XML inventory and DocFX generation

- The three changed public VM surfaces have applicable bilingual `summary`,
  `param`, `returns`, and `exception` documentation. No CS1591 suppression or
  signature drift was found. Private/local/generated members are excluded from
  the public contract; the internal shared validator is hidden from generated
  public navigation.
- A complete `docfx docfx.json` generation succeeded earlier in this bounded
  phase with zero errors. The final metadata refresh with `--noRestore` later
  hit a local named-pipe timeout and is retained as failed infrastructure, not
  pass evidence. The already generated metadata was then rendered twice with
  `docfx build docfx.json`; both builds succeeded with zero errors and seven
  pre-existing invalid-link warnings. Final build log SHA-256 is
  `1dccc0943b6b7803e3e33d5aabca78ec802d2fe81ed201075cfd37d6066c90c2`.
  Only the task-authorised API derivatives remain in the tracked set.

### T077–T080 — Accepted host DocFX accessibility cycle

- The authorised host used Node `v24.20.0`, npm `11.19.0`, and Playwright
  `1.62.1`. `npm --prefix tests/a11y ci` exited `0`, installed six packages,
  audited seven packages, and reported zero vulnerabilities.
- The controlled harness ran once with `MANAGE_DOCS_SERVER=1`, bound only to
  `127.0.0.1:8080`, and executed the required command
  `npm --prefix tests/a11y test -- --project=chromium`. Exit was `0`: exactly
  `3/3` pages passed in `3.9` seconds and every page has zero axe violations.
- `/private/tmp/tinypl0-004-axe.json` was revalidated read-only at 563 bytes
  with SHA-256
  `b01856d34bacb4215a958b0382add5ea44a87e2d1e6e77a2e2457adb45a8f23b`.
  `/private/tmp/tinypl0-004-a11y.log` was revalidated at 715 bytes with SHA-256
  `c7e1438448b0217f8da02beed0a35a5056e37efe2b06be4b611913dbd00eee66`.
- The separate VirtualMachineOptions Lynx dump is 3,980 bytes, contains
  `VirtualMachineOptions`, `InstructionBudget`, and `StackSize`, and has
  SHA-256 `c5277e1ffa5ada8965bc76024afaec356ea10144a5d604312e0a894efce65797`.
  The VirtualMachine dump is 5,795 bytes, contains `VirtualMachine`, `Run`, and
  `CultureNotFoundException`, and has SHA-256
  `063a98a71a1cf02c92e8305e5cc3ee44c8b80ece2706552f90c0e2d1a5aaeb82`.
- No listener existed before the accepted cycle, and no listener, child
  process, or owned server remained on TCP 8080 afterward. `_site`, runtime
  downloads, and the four named temporary evidence files remain outside the
  tracked delivery set.

### T081–T082 — API semantics, accessibility, and documentation impact

- The generated YAML preserves `VirtualMachineOptions` as the public options
  type. Its fifth optional constructor parameter is `InstructionBudget =
  1000000`; the prior four positional parameters retain their order and
  defaults. The generated descriptions preserve stack range `3..1,000,000`,
  positive instruction-budget semantics, instruction rather than time
  counting, and controlled diagnostic behavior in `Run` and `Initialize`.
- Public source inventory remains `VirtualMachineOptions`, `VirtualMachine`
  with `Run`, and `SteppableVirtualMachine` with `State`, `IsRunning`,
  `Initialize`, and `Step`; no unexpected public type or method was generated.
  Private/internal DocFX metadata is not counted as a public API addition.
- `docs/accessibility/secure-development-hardening.md` records the DE-first/
  EN-second WCAG 2.2 AA text-first review, the four accepted host hashes,
  Markdown/diagnostic/CVD/script/help scope, CLI/IDE non-trigger, and the
  keyboard/focus re-evaluation boundary.
- The assessment schema/157-ID/unique-evidence/CL-12 validator exited `0`.
  Exactly one `UpdateRequired` entry in
  `docs/documentation-impact/feature-004-secure-development-hardening.json`
  passed `scripts/validate-documentation-impact.ps1` with exit `0`.
- The Markdown text-first review also removed one stray NUL byte and replaced
  two unevaluated display expressions in `docs/security/asvs-verification.md`
  with the already proven source hash and evaluated commit. The file is now
  strict UTF-8 text; no ASVS status or product behavior changed.

### T083–T084 — Statistics writer and deterministic renderer

- The sole Statistics writer added one chronologically last feature entry and
  Profile 2 slot `11`. Before the ledger entry, the feature diff contains
  `+235 / -20` production lines, `+489 / -0` test lines, and
  `+25097 / -170` documentation, governance, automation, configuration,
  resources, and evidence lines; generated API YAML is reported separately
  and excluded from the manual reference basis.
- The entry uses manual references `80` and `125` lines per workday, `7.8`
  hours per day, one visible active day, and labels both comparisons as
  `blended repository speedup`, not stopwatch time.
- `pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo .` ran
  exactly once and exited `0`. The immediately following
  `-CheckOnly -Json` validation exited `0` with `status: CURRENT` and
  `changed: false`; fixed phase slots, block splitting, exact values, ASCII-only
  charts, and adjacent German/English text alternatives are current.

## T085 Traceability Freeze — FR-001–FR-028 and SC-001–SC-014

`Lokal belegt / Local pass` means that the accepted T009–T084 evidence answers
the requirement at this freeze. `Später erneut / Later reevaluation` names an
exact-head, provider, merge, or closeout boundary that is deliberately not
claimed. Commands written as `GATE/Cn` resolve verbatim in the 31-gate command
catalogue below. Read-only reviews are declared explicitly and were not
re-executed during this documentation-only freeze.

| ID | Tasks | Current evidence paths | Command or read-only review action | Exit / error channel | Current state |
|---|---|---|---|---|---|
| FR-001 | T009–T012, T072; later T102–T103 | `docs/security/secure-development/2026-08-30-tinypl0-hardening/assessment.json`, `findings.md`, `residual-risks.md` | `ASSESSMENT-157-GATE-001/C1–C2`; independent row review | `0`; corrected helper-only null-pipeline error was not gate evidence | Lokal belegt; exact-head gate evidence later |
| FR-002 | T009–T012, T071, T085 | feature assessment directory and `docs/security/README.md` | Read-only path/scope review; `ASSESSMENT-157-GATE-001/C2` | `0`, clean | Lokal belegt |
| FR-003 | T021–T027, T060, T074; later T092 | `docs/security/threat-model.md`, `arc42-security.md`, `security-quality-scenarios.md` | `ARCHITECTURE-THREAT-GATE-001/C1`; independent boundary review | `0`, clean | Lokal belegt; final diff review later |
| FR-004 | T022–T027, T060, T074; later T092 | `docs/security/adr/0001-vm-resource-budget.md`, `docs/architecture/adr/0001-vm-resource-budget.md`, `docs/architecture/secure-development-hardening.md` | `ARCHITECTURE-THREAT-GATE-001/C1` | `0`, clean | Lokal belegt; final diff review later |
| FR-005 | T021; later T090, T092 | assessment, threat model, existing compiler diagnostic tests | Read-only compiler/parser boundary review; final `BUILD-TEST-GOLDEN-GATE-001/C1` | Review accepted; final command not executed | Existing boundary belegt; exact-head regression pending |
| FR-006 | T047–T061, T081; later T087–T090 | VM sources, three accepted test files, VM section above, generated API YAML | `VM-TDD-GATE-001/C1–C2`; `VM-CONFIGURATION-GATE-001/C1–C3` | expected red `1`; green `0`; no accepted infrastructure error | VM slice belegt; full suite pending |
| FR-007 | T021; later T090, T092 | assessment, threat model, CLI/IDE negative-boundary review | Read-only CLI/IDE file/error review; final `BUILD-TEST-GOLDEN-GATE-001/C1` | Review accepted; final command not executed | Existing boundary belegt; exact-head regression pending |
| FR-008 | T064–T065, T077; later T090, T098 | `docs/security/asvs-verification.json`, `.md`, HTTP review, A11Y evidence | `ASVS-L1-GATE-001/C1`; loopback/static-root read-only review | `0`, clean; remote command not executed | 70-ID local mapping belegt; remote exact-head later |
| FR-009 | T082; later T092 | `docs/accessibility/secure-development-hardening.md` | Read-only IDE-impact non-trigger and keyboard/focus reevaluation review | No execution; no changed IDE error path | N/A non-trigger belegt; final review later |
| FR-010 | T048, T052–T061, T081; later T087–T090 | VM/L10N logs, test hashes, generated API YAML, traceability docs | VM gate commands; final `BUILD-TEST-GOLDEN-GATE-001/C1` | filtered green `0`; final 41/golden suite not executed | Compatibility slice belegt; full exact-head gate pending |
| FR-011 | T021–T027, T064, T071, T073 | checklist, dependency audit, quality scenarios, security index | `SECURITY-EVIDENCE-GATE-001/C1–C2` | `0`, clean | Lokal belegt |
| FR-012 | T016, T031–T033, T062–T063; later T097–T098 | `docs/security/supply-chain-evidence.json`, `.md`, dependency audit, SBOM | `SUPPLY-CHAIN-SBOM-VEX-SLSA-GATE-001/C1–C5` | local `0`; provider publication path not executed | Local reproducibility belegt; provider evidence pending |
| FR-013 | T007, T016, T031–T033, T062–T074; later T098, T100, T104 | dependency audit, supply-chain evidence, findings/residual risks | dependency and supply-chain gate commands; independent review | `0`; zero known Critical/High; no risk acceptance invented | Local gate belegt; review/provider boundary pending |
| FR-014 | T017, T034–T035, T070–T073, T082; later T098 | `.github/SECURITY.md`, `docfx/.well-known/security.txt`, security/accessibility indexes | `CVD-SECURITY-TXT-GATE-001/C1`; text-first review | `0`, clean | Local CVD belegt; published exact-head smoke later |
| FR-015 | T066, T071, T073–T074 | `docs/security/cra-applicability.md`, `regulatory-applicability.md` | Read-only legal/applicability review under `SECURITY-EVIDENCE-GATE-001/C1` | `0`; business-role open points retained | Dokumentationspflicht belegt |
| FR-016 | T067, T071, T073–T074 | `docs/security/cloud-autonomy-applicability.md`, `cloud-compliance-assurance.md` | Read-only C3A/C5/provider-boundary review | `0`; provider assertions bounded | Dokumentationspflicht belegt |
| FR-017 | T068, T071, T073–T074 | `docs/security/samm-assessment.md` | `SECURITY-EVIDENCE-GATE-001/C1`; prioritized-practice review | `0`, clean | Lokal belegt |
| FR-018 | T009, T015, T028–T030; later T099 | baseline manifest, 12 checklists, compendium, generator scripts | `ASSESSMENT-157-GATE-001/C1`; `BASELINE-GENERATOR-PARITY-GATE-001/C1–C6` | red `10` expected, local green `0`; remote matrix not executed | Local parity belegt; cross-platform exact-head pending |
| FR-019 | T015, T028–T030; later T099 | PowerShell/Bash generators and man page | `BASELINE-GENERATOR-PARITY-GATE-001/C1–C6` | local `0`; provider matrix not executed | Local parity belegt; remote matrix pending |
| FR-020 | T001, T005, T012, T018, T025–T026, T031, T036–T045, T062, T080; later T092 | supply-chain, regulatory, assessment, findings, A11Y cleanup evidence | Read-only AI-tool/privacy/review decision; N/A action for `AI-SBOM-GATE-001` | `0` for applicable reviews; AI-SBOM not executed | Lokal belegt; final secure review later |
| FR-021 | T013, T047–T061; later T088–T092, T101 | accepted VM red/green logs and test-source hashes | VM gate commands; final `COVERAGE-GATE-001/C1` | red `1`, green `0`; final coverage not executed | TDD belegt; thresholds pending exact-head coverage |
| FR-022 | T019, T034–T040, T045, T054, T071, T078–T082 | `docs/accessibility/secure-development-hardening.md`, axe JSON/log, Lynx dumps | `XML-DOC-DOCFX-A11Y-GATE-001/C2–C6`; text-first review | `0`; 3/3, zero axe violations; Lynx non-empty | Lokal belegt; remote exact-head later |
| FR-023 | T019, T038–T040, T051, T057, T075–T082; later T087, T090, T097–T098 | VM XML sources, generated `api/Pl0.Vm*.yml`, DocFX/A11Y logs | `XML-DOC-DOCFX-A11Y-GATE-001/C1–C6` | successful DocFX/axe `0`; one noRestore timeout rejected as infrastructure | Local API documentation belegt; remote exact-head pending |
| FR-024 | T020, T041–T042; later T089, T093, T095, T101 | homogeneity evidence, unchanged five agent surfaces, constitution mirrors | `AGENT-PRESET-PARITY-GATE-001/C1–C3` | `0`; `FND-GOV-001` non-trigger | Lokal belegt; final version/diff review later |
| FR-025 | T001, T003, T020, T041–T042; later T097 | gate contract, preset inventory, run-state/routing evidence | `GATE-REQUIREMENTS-SCHEMA-GATE-001/C1`; agent parity review | `0`, clean | Lokal belegt; provider mapping later |
| FR-026 | T005, T012, T045, T066–T074, T083–T085; later T107–T109 | security index, residual risks, this ledger, `docs/project-statistics.md` | `STATISTICS-GATE-001/C1–C3`; read-only result-summary freeze | `0`, `CURRENT`; terminal closeout not executed | Local summary/statistics belegt; closeout pending |
| FR-027 | T001, T004, T006–T008, T013–T014, T037, T044–T046, T061, T080; later T086, T092–T110 | run state, findings, delivery-boundary ledger, intake references | Read-only scope review; later delivery/remote gate commands | accepted local review; later commands not executed | Scope preserved; final delivery and closeout pending |
| FR-028 | T004, T008, T013–T020, T028–T046, T061, T065; later T086, T092–T103, T110 | assessment, findings, HTTP follow-up, six package evidence | `FINDING-AUTHORIZATION-GATE-001/C1–C3`; read-only no-seventh-package review | red `10–14` as expected, non-trigger/green `0`; final diff later | Evidence-first boundary belegt; exact-head review pending |
| SC-001 | T009–T012, T072 | assessment, findings, residual risks | `ASSESSMENT-157-GATE-001/C1–C2` | `0`, 157/157 | Lokal belegt |
| SC-002 | T003, T011–T014, T043–T046, T071–T074; later T100 | assessment/evidence paths and independent review | assessment/security gate commands; later PR review | `0`; provider review not executed | Local evidence integrity belegt; PR review pending |
| SC-003 | T021–T027, T060, T065, T074 | threat model, arc42, scenarios, ADRs | `ARCHITECTURE-THREAT-GATE-001/C1`; independent review | `0`, clean | Lokal belegt |
| SC-004 | T047–T061; later T088, T090 | VM tests/logs and source hashes | VM gate commands; final full suite | red `1`, green `0`; full suite not executed | Representative VM boundary belegt; full adversarial suite pending |
| SC-005 | T048, T052–T061, T081; later T087–T090 | VM/L10N logs, API YAML, traceability | VM gates; final build/test/golden command | filtered `0`; full suite not executed | Local slice belegt; final 41/golden parity pending |
| SC-006 | T047–T061; later T088–T091, T101 | VM TDD evidence; future Cobertura directory | VM gates; `COVERAGE-GATE-001/C1` later | TDD `0`; coverage command not executed | Coverage threshold pending |
| SC-007 | T064–T065, T073, T077; later T098 | ASVS JSON/MD and loopback evidence | `ASVS-L1-GATE-001/C1` | `0`, 70 IDs, 0 Critical/High | Local ASVS belegt; remote boundary later |
| SC-008 | T016, T031–T033, T062–T063; later T097–T098 | SBOM, dependency and supply-chain evidence | supply-chain/dependency gate commands | local `0`; remote artefact evidence not executed | Local path belegt; provider gate pending |
| SC-009 | T001, T003, T016–T018, T021–T026, T031–T036, T062–T074; later T097, T100 | complete `docs/security/` decision set | security gates plus six declared N/A read-only actions | applicable local checks `0`; N/A not executed | Decision coverage belegt; provider/review pending |
| SC-010 | T009, T015, T028–T030; later T099 | baseline manifest/checklists/compendium/generators | assessment inventory and baseline parity commands | local `0`; remote matrix not executed | Local parity belegt; cross-platform pending |
| SC-011 | T017, T019, T034–T040, T077–T082; later T097–T098 | A11Y evidence, CVD, rendered docs | XML/DocFX/A11Y and CVD commands | `0`; 0 axe violations, separate Lynx pass | Local A11Y belegt; remote exact-head pending |
| SC-012 | T017, T019, T034–T040, T051, T054, T057–T059, T071, T075–T084; later T087–T090, T097–T098 | XML/API, A11Y, docs, statistics | XML/DocFX/A11Y plus Statistics commands | `0`; final suite/provider not executed | Local learner/API evidence belegt; later gates pending |
| SC-013 | T001, T020, T041–T042; later T089, T093, T095, T101 | agent/preset parity evidence and IDE version ledger | agent parity commands; later IDE version command | parity `0`; final version command not executed | Parity belegt; final serial version pending |
| SC-014 | T001, T004–T008, T012–T013, T018, T036–T046, T061–T074, T080, T083–T085; later T086, T092–T110 | residual risks, scope ledger, statistics, this freeze | read-only scope/risk freeze; later delivery/closeout commands | local review `0`; remote/closeout not executed | Prioritized local list belegt; terminal closeout pending |

## T085 Gate Matrix — 31 Stable Gate IDs

Diese Freeze-Matrix ist noch keine Schema-2.0-PreMerge-Evidence. Sie weist jeder
Gate-ID genau einen `Primary`-Task-Owner zu; Support-Tasks erzeugen keine zweite
Primary-Ownership. Erst T102 schreibt später genau eine ausführungsgebundene
Primary-Zeile je Gate in die temporäre PreMerge-Evidence. N/A-Gates bleiben
begründete, nicht ausgeführte Entscheidungen. / This freeze is not schema-2.0
PreMerge evidence. It assigns exactly one primary task owner per gate; support
tasks do not create another primary owner. T102 later writes exactly one
execution-bound Primary row per gate. N/A gates remain reasoned and unexecuted.

In der Spalte „Exact command/action“ verweist `GATE/Cn` eindeutig auf den
unmittelbar folgenden wörtlichen Katalog. Dieser Verweis und der Katalog bilden
zusammen den exakten Befehl der Zeile. / The exact-command cell uniquely resolves
to the verbatim catalogue immediately below.

| Gate ID | State | Primary task | Support tasks | Current evidence path(s) | Exact command/action | Exit / error channel | Later boundary / reevaluation |
|---|---|---|---|---|---|---|---|
| `PLAN-GATE-001` | Accepted historical Pass | Primary: T002 | — | .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/plan.result.json | PLAN-GATE-001/C1 (verbatim below) | 0; stdout PASS; stderr clean | Any plan.md change or planning artefact/gate drift. |
| `PLAN-REVIEW-GATE-001` | Accepted historical Pass | Primary: T004 | T002 | specs/004-secure-development-hardening/plan-review.md and .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/plan-review.result.json | PLAN-REVIEW-GATE-001/C1 (verbatim below) | 0; stdout PASS; stderr clean | Any accepted specification, plan, contract, or gate requirement change. |
| `GATE-REQUIREMENTS-SCHEMA-GATE-001` | Accepted Pass | Primary: T003 | — | specs/004-secure-development-hardening/contracts/gate-requirements.schema.json, specs/004-secure-development-hardening/gate-requirements.json, and validation log | GATE-REQUIREMENTS-SCHEMA-GATE-001/C1 (verbatim below) | corrected schema check 0; helper-only field-name error excluded | Any gate requirement, gate schema, validator, command, token, runner, platform, or evidence-path change. |
| `TASKS-ANALYZE-GATE-001` | Accepted Pass | Primary: T004 | T001,T005 | specs/004-secure-development-hardening/tasks.md, specs/004-secure-development-hardening/analyze-report.md, and routed phase results | TASKS-ANALYZE-GATE-001/C1–C3 (verbatim below) | all three commands 0; semantic result and state clean | Any task, finding authorization, delivery gate, or analyze finding change. |
| `IDENTITY-INPUT-GATE-001` | Accepted at local boundary; reevaluation pending | Primary: T002 | T001,T086 | specs/004-secure-development-hardening/autonomous-run-state.json and validation log | IDENTITY-INPUT-GATE-001/C1–C2 (verbatim below) | 0; accepted hashes clean | Every phase boundary, interruption, resume, or accepted input drift. |
| `ASSESSMENT-157-GATE-001` | Local Pass; exact-head reevaluation pending | Primary: T011 | T009,T010,T012,T072 | docs/security/secure-development/2026-08-30-tinypl0-hardening/assessment.json and validation log | ASSESSMENT-157-GATE-001/C1–C2 (verbatim below) | 0; 157/157; corrected helper-only null-pipeline error excluded | Any checklist, manifest, compendium, assessment, or evidence hash change. |
| `FINDING-AUTHORIZATION-GATE-001` | Local Pass; final diff reevaluation pending | Primary: T013 | T006,T014–T020,T036–T046,T061,T065,T086,T092 | docs/security/secure-development/2026-08-30-tinypl0-hardening/findings.md and assessment.json | FINDING-AUTHORIZATION-GATE-001/C1–C3 (verbatim below) | validator 0; conditional red exits 10–14 were expected evidence | Before the first edit for every non-VM finding and whenever its file set or test changes. |
| `ARCHITECTURE-THREAT-GATE-001` | Local Pass | Primary: T027 | T021–T026,T060 | docs/security/threat-model.md, docs/security/arc42-security.md, docs/security/security-quality-scenarios.md, and docs/architecture/ | ARCHITECTURE-THREAT-GATE-001/C1 (verbatim below) | 0; stdout PASS; stderr clean | Any trust boundary, interface, runtime, deployment, resource policy, or accepted architecture trade-off change. |
| `VM-TDD-GATE-001` | Accepted red→green Pass; exact-head reconciliation pending | Primary: T055 | T048–T054,T058,T061,T088 | specs/004-secure-development-hardening/autonomous-run-evidence.md VM TDD section | VM-TDD-GATE-001/C1–C2 (verbatim below) | red 1 expected; green 0; interrupted restore excluded | Any instruction-counting, halt, error-state, or budget diagnostic change. |
| `VM-CONFIGURATION-GATE-001` | Accepted red→green/L10N Pass; exact-head reconciliation pending | Primary: T056 | T048–T057,T061,T088 | VM test results, resource review, and specs/004-secure-development-hardening/autonomous-run-evidence.md | VM-CONFIGURATION-GATE-001/C1–C3 (verbatim below) | red 1 expected; options green 0; L10N 0 | Any VirtualMachineOptions, stack-layout, validation-order, or diagnostic-resource change. |
| `BUILD-TEST-GOLDEN-GATE-001` | Pending | Primary: T090 | T059,T087 | Current: this ledger and `gate-requirements.json`; declared later: local build/test ledger and GitHub Actions CI build-test job | BUILD-TEST-GOLDEN-GATE-001/C1 (verbatim below) | not executed at this freeze; no exit/error channel | Any source, test, project, dependency, golden, or traceability change. |
| `COVERAGE-GATE-001` | Pending | Primary: T091 | T090 | Current: this ledger and `tasks.md`; declared later: `/private/tmp/tinypl0-004-abaa7b81-coverage` and coverage section | COVERAGE-GATE-001/C1 (verbatim below) | not executed; coverage directory is not claimed | Any source/test change or coverage baseline recalculation. |
| `XML-DOC-DOCFX-A11Y-GATE-001` | Local Pass; provider exact-head pending | Primary: T082 | T019,T035,T038–T040,T051,T057,T075–T081,T087,T098 | Current: `tests/a11y/package-lock.json`, DocFX log, generated API YAML, temporary `_site`, axe JSON, Lynx output, and `docs/accessibility/secure-development-hardening.md`; declared later: GitHub Docs Pages exact-head log | XML-DOC-DOCFX-A11Y-GATE-001/C1–C6 (verbatim below) | successful DocFX/npm/axe 0; Lynx non-empty; noRestore timeout rejected | Any public API/XML comment, DocFX content/navigation, diagnostic text, or generated HTML change. |
| `SECURITY-EVIDENCE-GATE-001` | Local Pass; final secure diff review pending | Primary: T074 | T012,T025–T026,T045,T054,T058–T059,T066–T073,T082,T092 | docs/security/ and docs/security/secure-development/2026-08-30-tinypl0-hardening/ | SECURITY-EVIDENCE-GATE-001/C1–C3 (verbatim below) | 0; no open Critical/High; stderr clean | Any security standard, risk, dependency, provider, regulatory, or product-boundary change. |
| `ASVS-L1-GATE-001` | Local Pass | Primary: T073 | T064–T065,T077 | docs/security/asvs-verification.json, docs/security/asvs-verification.md, official OWASP/ASVS v5.0.0 source hash, and any finding-bound HTTP test log | ASVS-L1-GATE-001/C1 (verbatim below) | 0; 70 exact IDs; openCriticalOrHigh 0 | Any --api, HTTP binding, method, header, static-file root, path, error-response, authentication, or deployment change. |
| `DEPENDENCY-REVIEW-GATE-001` | Local Pass | Primary: T073 | T031,T040,T062 | docs/security/dependency-audit.md and dependency command logs | DEPENDENCY-REVIEW-GATE-001/C1–C3 (verbatim below) | 0; no known vulnerable package; private sources not contacted | Any package/tool/action/version/source/lock-file change or new vulnerability. |
| `SUPPLY-CHAIN-SBOM-VEX-SLSA-GATE-001` | Local Pass; provider evidence pending | Primary: T063 | T016,T032–T033,T098 | Current: `docs/security/supply-chain-evidence.md`, dependency audit, pinned `.config/dotnet-tools.json`, and SBOM; declared later: CI logs | SUPPLY-CHAIN-SBOM-VEX-SLSA-GATE-001/C1–C5 (verbatim below) | local commands 0; provider/attestation channel not executed | Any release/Pages artifact, dependency, workflow, action, runner, CVE, VEX, provenance, or SLSA claim change. |
| `BASELINE-GENERATOR-PARITY-GATE-001` | Local Pass; cross-platform provider matrix pending | Primary: T030 | T009,T015,T028–T029,T099 | Current: baseline manifest, generators, man page, and `.github/workflows/powershell-analysis.yml`; declared later: exact-head macOS/Linux/Windows logs | BASELINE-GENERATOR-PARITY-GATE-001/C1–C6 (verbatim below) | red 10 expected; local green 0; remote channel not executed | Any secure-development guideline, checklist, manifest, compendium, generator, help, or platform change. |
| `CVD-SECURITY-TXT-GATE-001` | Local Pass; published-doc smoke pending | Primary: T070 | T017,T034–T035 | Current: `.github/SECURITY.md`, `docfx/.well-known/security.txt`, and `docs/security/README.md`; declared later: published-docs smoke evidence | CVD-SECURITY-TXT-GATE-001/C1 (verbatim below) | red 12 expected; local green 0 | Any security contact, response target, advisory process, public docs path, or security.txt expiry change. |
| `AGENT-PRESET-PARITY-GATE-001` | Pass via evidenced governance non-trigger | Primary: T020 | T041–T042 | homogeneity JSON, preset inventory, constitution comparison, and any FND-GOV-001 record | AGENT-PRESET-PARITY-GATE-001/C1–C3 (verbatim below) | 0; five surfaces and mirrors unchanged | Any shared guidance, constitution, template, preset version/order, or agent-surface change. |
| `STATISTICS-GATE-001` | Pass and CURRENT | Primary: T084 | T083 | docs/project-statistics.md and renderer JSON output | STATISTICS-GATE-001/C1–C3 (verbatim below) | render 0; CheckOnly JSON 0; changed false | Any final delivery-set, line-count, work-window, phase-slot, or statistics-method change. |
| `IDE-VERSION-SERIAL-GATE-001` | Blocked at remaining T085 orchestrator portion | Primary: T089 | T047,T049–T050,T055–T057,T085–T086,T090,T093,T095,T101 | src/Pl0.Ide/Pl0.Ide.csproj and per-invocation version/build ledger | IDE-VERSION-SERIAL-GATE-001/C1 (verbatim below) | early gates accepted through 1.72.453.38; 1.72.454.38/commit not executed here | Before every dotnet build/test and before each commit or PR branch update. |
| `DELIVERY-EVIDENCE-GATE-001` | Blocked at T085 commit; final exact-head evidence pending | Primary: T103 | T005–T006,T037,T046,T080,T085,T093,T101–T102 | Current: this ledger and `gate-requirements.json`; declared later: `/private/tmp/tinypl0-004-premerge-gate-evidence.json` | DELIVERY-EVIDENCE-GATE-001/C1–C4 (verbatim below) | documentation freeze only; no delivery validator/Git command executed here | Any file, finding, gate, evidence, HEAD, or delivery-scope change. |
| `REMOTE-REVIEW-GATE-001` | Pending | Primary: T100 | T007,T094–T099,T101–T104 | Current: `gate-requirements.json` and T094–T104; declared later: GitHub PR checks/review metadata | REMOTE-REVIEW-GATE-001/C1–C2 (verbatim below) | not executed; no provider channel claimed | Every pushed head, workflow rerun, review change, or provider-state change. |
| `MERGE-CLOSEOUT-GATE-001` | Pending | Primary: T108 | T007,T094,T104–T107,T109–T110 | Current: run state, this ledger, and T104–T110; declared later: post-merge evidence, synchronized main, retrospective, and terminal run state | MERGE-CLOSEOUT-GATE-001/C1–C4 (verbatim below) | not executed; no merge/closeout channel claimed | Any merge authority, reviewed head, branch policy, post-merge action, or closeout-state change. |
| `AI-SBOM-GATE-001` | N/A documented; not executed | Primary: T069 | T073 | docs/security/supply-chain-evidence.md | N/A — no execution; read-only disposition in declared evidence path | N/A: no process, exit code, stdout, or stderr | A model, dataset, inference service, infrastructure, or AI runtime becomes a product or operated component. |
| `ZERO-TRUST-GATE-001` | N/A documented; not executed | Primary: T068 | T073 | docs/security/zero-trust-applicability.md | N/A — no execution; read-only disposition in declared evidence path | N/A: no process, exit code, stdout, or stderr | Remote, cloud-runtime, multi-device, service, federated identity, or remote-management scope. |
| `PRODUCT-CRYPTO-DPIA-GATE-001` | N/A documented; not executed | Primary: T069 | T073 | docs/security/regulatory-applicability.md and assessment rows for CL-03/CL-11 | N/A — no execution; read-only disposition in declared evidence path | N/A: no process, exit code, stdout, or stderr | Any cryptography, authentication, secret, account, telemetry, personal-data, profiling, recipient, or retention scope. |
| `NIS2-AIACT-DORA-GATE-001` | N/A documented; not executed | Primary: T066 | T073 | docs/security/regulatory-applicability.md | N/A — no execution; read-only disposition in declared evidence path | N/A: no process, exit code, stdout, or stderr | Operator/customer/supply relationship, AI product, financial-sector service, or regulated business model change. |
| `PARALLEL-AUTONOMOUS-GATE-001` | N/A documented; not executed | Primary: T008 | T073,T110 | specs/004-secure-development-hardening/plan.md | N/A — no execution; read-only disposition in declared evidence path | N/A: no process, exit code, stdout, or stderr | Explicit user authority for a parallel autonomous campaign. |
| `SANDBOX-HARDENING-GATE-001` | N/A documented; not executed | Primary: T010 | T008,T073,T110 | assessment.json CL-12 rows and the separate sandbox follow-up intake reference | N/A — no execution; read-only disposition in declared evidence path | N/A: no process, exit code, stdout, or stderr | A separate accepted and explicitly authorised sandbox-hardening run after this feature's hard completion gate. |

### Verbatim exact-command and read-only-action catalogue

Die Befehle sind ausschließlich der akzeptierte Vertrag; sie wurden in diesem
T085-Dokumentationslauf nicht erneut ausgeführt. / These commands are the
accepted contract only; they were not re-executed in this T085 documentation
run.

#### PLAN-GATE-001

- C1:

      pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; $phase = @($state.routing.phases | Where-Object phaseId -eq "plan"); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical plan phase is not Completed" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical plan result hash drift" }; "PASS: immutable historical plan result"'

#### PLAN-REVIEW-GATE-001

- C1:

      pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; $phase = @($state.routing.phases | Where-Object phaseId -eq "plan-review"); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical plan-review phase is not Completed" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical plan-review result hash drift" }; "PASS: immutable historical plan-review result"'

#### GATE-REQUIREMENTS-SCHEMA-GATE-001

- C1:

      pwsh -NoProfile -Command '$json = Get-Content -LiteralPath specs/004-secure-development-hardening/gate-requirements.json -Raw -Encoding UTF8; if (-not ($json | Test-Json -SchemaFile specs/004-secure-development-hardening/contracts/gate-requirements.schema.json)) { throw "Gate requirements schema failed" }; $data = $json | ConvertFrom-Json; if (@($data.gates.gateId | Sort-Object -Unique).Count -ne @($data.gates).Count) { throw "Duplicate gateId" }; "PASS: gate requirements schema and unique IDs"'

#### TASKS-ANALYZE-GATE-001

- C1:

      pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; $phase = @($state.routing.phases | Where-Object phaseId -eq "tasks"); if ($phase.Count -ne 1 -or $phase[0].status -ne "Completed") { throw "Historical tasks phase is not Completed" }; $actual = (Get-FileHash -LiteralPath $phase[0].resultPath -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $phase[0].resultSha256) { throw "Historical tasks result hash drift" }; "PASS: immutable historical tasks result"'
- C2:

      pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-phase-result.ps1 -Repo . -Result .specify/runtime/autonomous-routing/abaa7b81-fd2c-47e7-8d59-87a852a3b2e7/analyze.result.json -PhaseId analyze -ExitCode 0
- C3:

      pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.ps1 -State specs/004-secure-development-hardening/autonomous-run-state.json

#### IDENTITY-INPUT-GATE-001

- C1:

      pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.ps1 -State specs/004-secure-development-hardening/autonomous-run-state.json
- C2:

      pwsh -NoProfile -Command '$state = Get-Content -LiteralPath specs/004-secure-development-hardening/autonomous-run-state.json -Raw -Encoding UTF8 | ConvertFrom-Json; if ($state.runId -ne "abaa7b81-fd2c-47e7-8d59-87a852a3b2e7" -or $state.featurePath -ne "specs/004-secure-development-hardening" -or $state.branch -ne "codex/004-secure-development-hardening") { throw "Feature identity mismatch" }; foreach ($item in $state.acceptedArtifacts) { $actual = (Get-FileHash -LiteralPath $item.path -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $item.sha256) { throw "Accepted input drift: $($item.path)" } }; "PASS: identity and accepted byte hashes"'

#### ASSESSMENT-157-GATE-001

- C1:

      pwsh -NoProfile -Command '$files = @(Get-ChildItem -LiteralPath docs/secure-development/checklisten -Filter "CL_*.md" | Sort-Object Name); if ($files.Count -ne 12) { throw "Expected 12 canonical checklists" }; $sets = foreach ($file in $files) { $ids = @([regex]::Matches((Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8), "(?m)^#### (CL-[0-9]{2}-[0-9]{2}):") | ForEach-Object { $_.Groups[1].Value }); [pscustomobject]@{ Name = $file.Name; Count = $ids.Count; Ids = $ids } }; $counts = @($sets | ForEach-Object { $_.Count }); $all = [string[]]@($sets.Ids); $unique = @($all | Sort-Object -Unique); $book = [string[]]@([regex]::Matches((Get-Content -LiteralPath docs/secure-development/Checklistensammelband_Sichere-Entwicklung.md -Raw -Encoding UTF8), "(?m)^#### (CL-[0-9]{2}-[0-9]{2}):") | ForEach-Object { $_.Groups[1].Value }); if (($counts -join "/") -ne "12/13/15/10/13/11/12/13/17/17/12/12" -or $all.Count -ne 157 -or $unique.Count -ne 157 -or -not [Linq.Enumerable]::SequenceEqual($all, $book)) { throw "Checklist inventory or ordered compendium parity failed" }; "PASS: counts=$($counts -join "/"); total=157; unique=157; ordered compendium parity"'
- C2:

      pwsh -NoProfile -Command '$path = "docs/security/secure-development/2026-08-30-tinypl0-hardening/assessment.json"; $json = Get-Content -LiteralPath $path -Raw -Encoding UTF8; if (-not ($json | Test-Json -SchemaFile specs/004-secure-development-hardening/contracts/assessment-record.schema.json)) { throw "Assessment schema failed" }; $data = $json | ConvertFrom-Json; $files = @(Get-ChildItem -LiteralPath docs/secure-development/checklisten -Filter "CL_*.md" | Sort-Object Name); $canonical = [string[]]@($files | ForEach-Object { [regex]::Matches((Get-Content -LiteralPath $_.FullName -Raw -Encoding UTF8), "(?m)^#### (CL-[0-9]{2}-[0-9]{2}):") | ForEach-Object { $_.Groups[1].Value } }); $ids = [string[]]@($data.items.clId); if ($ids.Count -ne 157 -or @($ids | Sort-Object -Unique).Count -ne 157 -or -not [Linq.Enumerable]::SequenceEqual($canonical, $ids)) { throw "Assessment canonical ID order failed" }; if ($data.evaluatedCommit -ne (git rev-parse HEAD)) { throw "Assessment commit is not exact HEAD" }; $cl12 = @($data.items | Where-Object { $_.clId -like "CL-12-*" }); if ($cl12.Count -ne 12 -or @($cl12 | Where-Object { $_.applicability -ne "N/A" -or $_.implementation -ne "Not Assessed" }).Count -ne 0) { throw "CL-12 boundary failed" }; $badReview = @($data.items | Where-Object { $_.ownerRole -eq $_.reviewerRole }); if ($badReview) { throw "Owner and reviewer role must differ" }; $evidence = @($data.items.evidence); $evidenceIds = @($evidence.evidenceId); if (@($evidenceIds | Sort-Object -Unique).Count -ne $evidenceIds.Count) { throw "Duplicate evidenceId" }; $badPass = @($data.items | Where-Object { $_.implementation -eq "Fulfilled" -and @($_.evidence | Where-Object { $_.result -eq "Pass" -and $_.commit -eq $data.evaluatedCommit }).Count -eq 0 }); if ($badPass) { throw "Fulfilled row lacks exact-HEAD Pass evidence" }; "PASS: assessment schema, canonical 157 order, exact HEAD evidence, reviewer separation, CL-12 boundary"'

#### FINDING-AUTHORIZATION-GATE-001

- C1:

      pwsh -NoProfile -Command '$assessment = Get-Content -LiteralPath docs/security/secure-development/2026-08-30-tinypl0-hardening/assessment.json -Raw -Encoding UTF8 | ConvertFrom-Json; $allowed = @("FND-BASELINE-001", "FND-SC-001", "FND-CVD-001", "FND-GITIGNORE-001", "FND-A11Y-001", "FND-GOV-001"); $named = @($assessment.items | Where-Object { -not [string]::IsNullOrWhiteSpace([string]$_.findingId) }); $unexpected = @($named | Where-Object { $_.findingId -notin $allowed }); if ($unexpected.Count) { throw "Finding outside exact six-package authorisation boundary" }; $bad = @($named | Where-Object { $_.applicability -ne "Applicable" -or $_.implementation -notin @("Partly Fulfilled", "Not Fulfilled") }); if ($bad.Count) { throw "Finding authorisation state invalid" }; "PASS: exact six-package boundary and finding status precondition"'
- C2:

      pwsh -NoProfile -Command '$base = git merge-base main HEAD; $changed = @(git diff --name-only $base HEAD); $findings = Get-Content -LiteralPath docs/security/secure-development/2026-08-30-tinypl0-hardening/findings.md -Raw -Encoding UTF8; $unbound = @($changed | Where-Object { $_ -match "^(src/Pl0\.(Core|Cli|Ide)/|\.github/workflows/|scripts/|\.gitignore$|AGENTS\.md$|CLAUDE\.md$|GEMINI\.md$|\.github/(copilot-instructions|agents/copilot-instructions)\.md$)" -and $findings -notmatch [regex]::Escape("`$_`") }); if ($unbound) { throw "Changed path lacks finding binding: $($unbound -join ', ')" }; "PASS: finding-bound changed paths at exact HEAD"'
- C3:

      git status --short --untracked-files=all

#### ARCHITECTURE-THREAT-GATE-001

- C1:

      pwsh -NoProfile -Command '$required = @("docs/security/threat-model.md", "docs/security/arc42-security.md", "docs/security/security-quality-scenarios.md", "docs/architecture/secure-development-hardening.md", "docs/architecture/adr/0001-vm-resource-budget.md", "docs/security/adr/0001-vm-resource-budget.md"); $combined = ""; foreach ($path in $required) { if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing architecture evidence: $path" }; $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8; if ($text -match "Stub|To be populated|Zu befuellen|TBD") { throw "Unresolved architecture placeholder: $path" }; $combined += $text }; foreach ($token in @("STRIDE", "CIA", "CAPEC", "arc42", "iSAQB")) { if ($combined -notmatch [regex]::Escape($token)) { throw "Architecture evidence lacks $token" } }; "PASS: architecture and threat evidence; STRIDE; CIA; CAPEC; arc42; iSAQB"'

#### VM-TDD-GATE-001

- C1:

      dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.VirtualMachineTests.Instruction_Budget_Stops_Before_N_Plus_One|FullyQualifiedName~Pl0.Tests.SteppableVirtualMachineTests.Instruction_Budget_Stops_Before_N_Plus_One"
- C2:

      pwsh -NoProfile -Command '$paths = @("tests/Pl0.Tests/VirtualMachineTests.cs", "tests/Pl0.Tests/SteppableVirtualMachineTests.cs"); foreach ($path in $paths) { $hash = (Get-FileHash -LiteralPath $path -Algorithm SHA256).Hash.ToLowerInvariant(); "testSourceSha256 $path $hash" }; "PASS: N+1 red/green source binding"'

#### VM-CONFIGURATION-GATE-001

- C1:

      dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.VirtualMachineTests.Invalid_Options_Return_Diagnostic_Before_Allocation|FullyQualifiedName~Pl0.Tests.SteppableVirtualMachineTests.Invalid_Options_Return_Diagnostic_Before_Allocation"
- C2:

      dotnet test TinyPl0.sln --configuration Release --filter "FullyQualifiedName~Pl0.Tests.L10nTests"
- C3:

      pwsh -NoProfile -Command '$text = (Get-Content -LiteralPath tests/Pl0.Tests/VirtualMachineTests.cs -Raw -Encoding UTF8) + (Get-Content -LiteralPath tests/Pl0.Tests/SteppableVirtualMachineTests.cs -Raw -Encoding UTF8); foreach ($token in @("InstructionBudget", "StackSize", "1_000_001", "int.MaxValue")) { if ($text -notmatch [regex]::Escape($token)) { throw "VM configuration tests lack $token" } }; "PASS: InstructionBudget and StackSize validation bounds"'

#### BUILD-TEST-GOLDEN-GATE-001

- C1:

      pwsh -NoProfile -Command '& dotnet restore TinyPl0.sln; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; & dotnet test TinyPl0.sln --configuration Release --no-restore --collect:"XPlat Code Coverage" --results-directory /private/tmp/tinypl0-004-abaa7b81-coverage; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; "PASS: one exact-HEAD Release full suite; 41 mandatory cases; VirtualMachineTests; SteppableVirtualMachineTests; L10nTests; CatalogCasesTests; LexerGoldenTests; ParserGoldenTests; TraceabilityMatrixTests; ArchitectureGuardTests; no golden regeneration"'

#### COVERAGE-GATE-001

- C1:

      pwsh -NoProfile -Command '$files = @(Get-ChildItem -LiteralPath /private/tmp/tinypl0-004-abaa7b81-coverage -Recurse -Filter coverage.cobertura.xml); if (-not $files) { throw "No coverage file from XPlat Code Coverage" }; $docs = @($files | ForEach-Object { [xml](Get-Content -LiteralPath $_.FullName -Raw -Encoding UTF8) }); $overall = ($docs | ForEach-Object { [double]$_.coverage."line-rate" } | Measure-Object -Minimum).Minimum; if ($overall -lt 0.7023) { throw "Coverage below 70.23% baseline: $overall" }; $vm = @($docs.coverage.packages.package.classes.class | Where-Object { $_.filename -like "*src/Pl0.Vm/*" }); if (-not $vm) { throw "No VM coverage entries" }; $branch = ($vm | Measure-Object -Property branch-rate -Minimum).Minimum; if ([double]$branch -lt 0.85) { throw "Changed VM branch coverage below 85%: $branch" }; $target = if ($overall -ge 0.80) { "TargetMet" } else { "TargetOpen" }; "PASS: XPlat Code Coverage; overall=$overall; floor=0.7023; target=$target; vmBranch=$branch"'

#### XML-DOC-DOCFX-A11Y-GATE-001

- C1:

      docfx docfx.json
- C2:

      npm --prefix tests/a11y ci
- C3:

      npm --prefix tests/a11y test -- --project=chromium
- C4:

      pwsh -NoProfile -Command '$lock = Get-Content -LiteralPath tests/a11y/package-lock.json -Raw -Encoding UTF8; if ($lock -notmatch "@axe-core/playwright" -or $lock -notmatch "playwright") { throw "Pinned axe/Playwright dependencies missing" }; "PASS: axe dependency lock"'
- C5:

      lynx -dump -nolist http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachineOptions.html
- C6:

      lynx -dump -nolist http://127.0.0.1:8080/api/Pl0.Vm.VirtualMachine.html

#### SECURITY-EVIDENCE-GATE-001

- C1:

      pwsh -NoProfile -Command '$required = @("docs/security/threat-model.md", "docs/security/arc42-security.md", "docs/security/security-checklist.md", "docs/security/security-quality-scenarios.md", "docs/security/dependency-audit.md", "docs/security/asvs-verification.md", "docs/security/supply-chain-evidence.md", "docs/security/zero-trust-applicability.md", "docs/security/samm-assessment.md", "docs/security/cra-applicability.md", "docs/security/regulatory-applicability.md", "docs/security/cloud-autonomy-applicability.md", "docs/security/cloud-compliance-assurance.md"); foreach ($path in $required) { if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing evidence: $path" }; $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8; if ($text -match "Stub|To be populated|Zu befuellen|TBD") { throw "Unresolved evidence placeholder: $path" } }; "PASS: security evidence paths"'
- C2:

      pwsh -NoProfile -Command '$text = (Get-Content -LiteralPath docs/security/security-checklist.md -Raw -Encoding UTF8) + (Get-Content -LiteralPath docs/security/dependency-audit.md -Raw -Encoding UTF8); foreach ($token in @("NIST SSDF", "CWE Top 25")) { if ($text -notmatch [regex]::Escape($token)) { throw "Security evidence lacks $token" } }; "PASS: NIST SSDF and CWE Top 25 evidence"'
- C3:

      git diff --check

#### ASVS-L1-GATE-001

- C1:

      pwsh -NoProfile -Command '$uri = "https://raw.githubusercontent.com/OWASP/ASVS/v5.0.0/5.0/docs_en/OWASP_Application_Security_Verification_Standard_5.0.0_en.flat.json"; $temp = [IO.Path]::GetTempFileName(); try { Invoke-WebRequest -Uri $uri -OutFile $temp; $sourceHash = (Get-FileHash -LiteralPath $temp -Algorithm SHA256).Hash.ToLowerInvariant(); $official = Get-Content -LiteralPath $temp -Raw -Encoding UTF8 | ConvertFrom-Json; $expected = [string[]]@($official.requirements | Where-Object L -eq "1" | ForEach-Object { "v5.0.0-$($_.req_id)" }); $data = Get-Content -LiteralPath docs/security/asvs-verification.json -Raw -Encoding UTF8 | ConvertFrom-Json; $actual = [string[]]@($data.items.id); if ($data.scope -ne "pl0c --api" -or $data.sourceUri -ne $uri -or $data.sourceSha256 -ne $sourceHash -or $data.evaluatedCommit -ne (git rev-parse HEAD) -or $expected.Count -ne 70 -or $actual.Count -ne 70 -or @($actual | Sort-Object -Unique).Count -ne 70 -or -not [Linq.Enumerable]::SequenceEqual($expected, $actual)) { throw "ASVS 5.0.0 Level 1 source, exact HEAD, count, or order mismatch" }; $bad = @($data.items | Where-Object { $_.applicability -notin @("Applicable", "N/A") -or ($_.applicability -eq "Applicable" -and $_.implementation -ne "Fulfilled") -or ($_.applicability -eq "N/A" -and $_.implementation -ne "Not Assessed") -or [string]::IsNullOrWhiteSpace([string]$_.rationale) }); if ($bad.Count -or [int]$data.openCriticalOrHigh -ne 0) { throw "ASVS applicability, implementation, rationale, or risk gate failed" }; "PASS: OWASP ASVS 5.0.0 Level 1; 70 exact IDs; Applicable/N/A; Fulfilled; exact HEAD" } finally { Remove-Item -LiteralPath $temp -Force -ErrorAction SilentlyContinue }'

#### DEPENDENCY-REVIEW-GATE-001

- C1:

      dotnet list TinyPl0.sln package --outdated --include-transitive
- C2:

      dotnet list TinyPl0.sln package --vulnerable --include-transitive
- C3:

      pwsh -NoProfile -Command '$text = Get-Content -LiteralPath docs/security/dependency-audit.md -Raw -Encoding UTF8; foreach ($token in @("licence", "lock-file")) { if ($text -notmatch [regex]::Escape($token)) { throw "Missing dependency-audit evidence: $token" } }; "PASS: licence and lock-file evidence"'

#### SUPPLY-CHAIN-SBOM-VEX-SLSA-GATE-001

- C1:

      dotnet tool restore
- C2:

      dotnet tool run dotnet-CycloneDX TinyPl0.sln -o /private/tmp/tinypl0-004-sbom --output-format Json --spec-version 1.7
- C3:

      pwsh -NoProfile -Command '$bom = @(Get-ChildItem -LiteralPath /private/tmp/tinypl0-004-sbom -Filter *.json); if ($bom.Count -ne 1) { throw "Expected one CycloneDX JSON" }; $data = Get-Content -LiteralPath $bom[0].FullName -Raw -Encoding UTF8 | ConvertFrom-Json; if ([string]$data.bomFormat -ne "CycloneDX" -or -not $data.specVersion -or -not $data.components) { throw "Invalid SBOM content" }; Get-FileHash -LiteralPath $bom[0].FullName -Algorithm SHA256'
- C4:

      pwsh -NoProfile -Command '$path = "docs/security/supply-chain-evidence.json"; $data = Get-Content -LiteralPath $path -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 100; $head = git rev-parse HEAD; if ($LASTEXITCODE -ne 0 -or $data.evaluatedCommit -ne $head) { throw "Supply-chain evidence is stale" }; if ($data.generator.name -ne "CycloneDX .NET" -or $data.generator.version -ne "6.2.0") { throw "CycloneDX .NET 6.2.0 pin missing" }; foreach ($field in @("artifactSha256", "sbomSha256")) { if ([string]$data.$field -notmatch "^[0-9a-f]{64}$") { throw "Invalid $field" } }; if (-not $data.vexState -or -not $data.slsaClaim) { throw "VEX or SLSA assessment missing" }; "PASS: artifactSha256, VEX, SLSA, CycloneDX .NET 6.2.0"'
- C5:

      pwsh -NoProfile -Command '$workflows = @(Get-Item -LiteralPath .github/workflows/docs-pages.yml, .github/workflows/release-please.yml); foreach ($workflow in $workflows) { $text = Get-Content -LiteralPath $workflow.FullName -Raw -Encoding UTF8; $refs = @([regex]::Matches($text, "uses:\s+[^\s]+@([^\s#]+)") | ForEach-Object { $_.Groups[1].Value }); $unpinned = @($refs | Where-Object { $_ -notmatch "^[0-9a-f]{40}$" }); if ($unpinned) { throw "Unpinned action in $($workflow.Name): $($unpinned -join ',')" } }; "PASS: planned supply-chain workflows use full action SHA pins"'

#### BASELINE-GENERATOR-PARITY-GATE-001

- C1:

      pwsh -NoProfile -File scripts/build-secure-development-docs.ps1 -Check
- C2:

      pwsh -NoProfile -File scripts/build-secure-development-docs.ps1 -WhatIf
- C3:

      pwsh -NoProfile -Command '. ./scripts/build-secure-development-docs.ps1; $cmd = Get-Command Build-SecureDevelopmentDocs -ErrorAction Stop; if (-not $cmd.Parameters.ContainsKey("WhatIf")) { throw "Cmdlet lacks WhatIf" }; Get-Help Build-SecureDevelopmentDocs -Full'
- C4:

      bash scripts/build-secure-development-docs.sh --check
- C5:

      bash scripts/build-secure-development-docs.sh --dry-run
- C6:

      pwsh -NoProfile -Command '$man = "docs/man/build-secure-development-docs.1.md"; if (-not (Test-Path -LiteralPath $man -PathType Leaf)) { throw "Missing $man" }; $ps = Get-Content -LiteralPath scripts/build-secure-development-docs.ps1 -Raw -Encoding UTF8; $sh = Get-Content -LiteralPath scripts/build-secure-development-docs.sh -Raw -Encoding UTF8; if ($ps -notmatch "Set-StrictMode -Version Latest") { throw "PowerShell strict mode missing" }; if ($sh -notmatch "set -euo pipefail") { throw "Bash strict mode missing" }; "PASS: docs/man/build-secure-development-docs.1.md; Set-StrictMode -Version Latest; set -euo pipefail"'

#### CVD-SECURITY-TXT-GATE-001

- C1:

      pwsh -NoProfile -Command '$required = @(".github/SECURITY.md", "docfx/.well-known/security.txt"); foreach ($path in $required) { if (-not (Test-Path -LiteralPath $path -PathType Leaf)) { throw "Missing CVD artefact: $path" } }; $policy = Get-Content -LiteralPath .github/SECURITY.md -Raw -Encoding UTF8; $txt = Get-Content -LiteralPath docfx/.well-known/security.txt -Raw -Encoding UTF8; foreach ($token in @("Contact:", "Expires:", "Preferred-Languages:", "Canonical:")) { if ($txt -notmatch [regex]::Escape($token)) { throw "security.txt missing $token" } }; if ($policy -notmatch "DE" -or $policy -notmatch "EN") { throw "CVD policy lacks bilingual markers" }; "PASS: CVD and security.txt"'

#### AGENT-PRESET-PARITY-GATE-001

- C1:

      pwsh -NoProfile -File scripts/check-homogeneity.ps1 -TargetDir . -Json -DryRun -NoPatch
- C2:

      pwsh -NoProfile -Command '$a = Get-Content -LiteralPath constitution.md -Raw -Encoding UTF8; $b = Get-Content -LiteralPath .specify/memory/constitution.md -Raw -Encoding UTF8; if ($a -cne $b) { throw "Constitution mirror differs" }; "PASS: constitution mirror"'
- C3:

      pwsh -NoProfile -Command '$files = @("AGENTS.md", "CLAUDE.md", "GEMINI.md", ".github/copilot-instructions.md", ".github/agents/copilot-instructions.md"); foreach ($file in $files) { $text = Get-Content -LiteralPath $file -Raw -Encoding UTF8; if ($text -match "Minor = current Spec-Kit feature/branch number" -or $text -notmatch "canonical PR") { throw "Stale IDE version rule in $file" } }; "PASS: AGENTS.md; CLAUDE.md; GEMINI.md; .github/copilot-instructions.md; .github/agents/copilot-instructions.md"'

#### STATISTICS-GATE-001

- C1:

      pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo .
- C2:

      pwsh -NoProfile -File scripts/render-project-statistics.ps1 -Repo . -CheckOnly -Json
- C3:

      pwsh -NoProfile -Command '$text = Get-Content -LiteralPath docs/project-statistics.md -Raw -Encoding UTF8; foreach ($token in @("80", "125", "7.8", "blended repository speedup", "current: true")) { if ($text -notmatch [regex]::Escape($token)) { throw "Missing statistics evidence: $token" } }; if ($text -notmatch "(?s)## Gesamtstatistik\s.*\z") { throw "Gesamtstatistik is not final" }; "PASS: 80, 125, 7.8, blended repository speedup, current: true"'

#### IDE-VERSION-SERIAL-GATE-001

- C1:

      pwsh -NoProfile -Command '$repo = "hindermath/TinyPl0"; $branch = "codex/004-secure-development-hardening"; $head = (git rev-parse HEAD).Trim(); $current = @(gh pr list --repo $repo --head $branch --state all --limit 100 --json number,headRefOid | ConvertFrom-Json); if ($current.Count -gt 1) { throw "Multiple PRs use the feature branch" }; if ($current.Count -eq 1) { if ($current[0].headRefOid -ne $head) { throw "Feature PR is not at exact HEAD" }; $expectedMinor = [int]$current[0].number } else { $all = @(gh pr list --repo $repo --state all --limit 1000 --json number | ConvertFrom-Json); $expectedMinor = 1 + [int](($all.number | Measure-Object -Maximum).Maximum) }; $xml = [xml](Get-Content -LiteralPath src/Pl0.Ide/Pl0.Ide.csproj -Raw -Encoding UTF8); $v = [version]([string]$xml.Project.PropertyGroup.Version); $patch = [int](git rev-list --count HEAD); if ($v -ne [version]([string]$xml.Project.PropertyGroup.AssemblyVersion) -or $v -ne [version]([string]$xml.Project.PropertyGroup.FileVersion)) { throw "IDE version fields differ" }; if ($v.Major -ne 1 -or $v.Minor -ne $expectedMinor -or $v.Build -ne $patch) { throw "IDE version does not match canonical PR number and exact-HEAD commit count" }; "PASS: canonical PR number=$expectedMinor; exact HEAD=$head; version=$($v.ToString(4)); serialized version writer"'

#### DELIVERY-EVIDENCE-GATE-001

- C1:

      pwsh -NoProfile -Command '& ./.specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.ps1 -Requirements specs/004-secure-development-hardening/gate-requirements.json -Evidence /private/tmp/tinypl0-004-premerge-gate-evidence.json -Head (git rev-parse HEAD)'
- C2:

      pwsh -NoProfile -Command '$base = (git merge-base origin/main HEAD).Trim(); $changed = @(git diff --name-only $base HEAD --); if (-not $changed) { throw "Feature delivery diff is empty" }; $forbidden = @($changed | Where-Object { $_ -match "^requirements/intakes/(active|series)/" }); if ($forbidden) { throw "Forbidden intake or series delivery path: $($forbidden -join ', ')" }; $dirty = @(git status --porcelain=v1 --untracked-files=all); if ($dirty) { throw "Delivery HEAD is not clean: $($dirty -join '; ')" }; "PASS: exact feature delivery diff at HEAD; paths=$($changed.Count); base=$base"'
- C3:

      git diff --check
- C4:

      git status --short

#### REMOTE-REVIEW-GATE-001

- C1:

      gh pr checks --required
- C2:

      gh pr view --json number,url,headRefOid,reviewDecision,statusCheckRollup,reviews,mergeStateStatus

#### MERGE-CLOSEOUT-GATE-001

- C1:

      gh pr merge --merge --delete-branch
- C2:

      gh repo sync --branch main
- C3:

      pwsh -NoProfile -File .specify/presets/autonomous-run-governance/scripts/validate-autonomous-run-state.ps1 -State specs/004-secure-development-hardening/autonomous-run-state.json
- C4:

      pwsh -NoProfile -Command '$evidence = "/private/tmp/tinypl0-004-postmerge-gate-evidence.json"; $data = Get-Content -LiteralPath $evidence -Raw -Encoding UTF8 | ConvertFrom-Json -Depth 100; if ($data.snapshotType -ne "PostMerge") { throw "Expected PostMerge evidence" }; & ./.specify/presets/autonomous-run-governance/scripts/validate-autonomous-gate-evidence.ps1 -Requirements specs/004-secure-development-hardening/gate-requirements.json -Evidence $evidence -Head $data.reviewedHead -MergeCommit $data.mergeCommit; if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }; "PASS: PostMerge"'

#### AI-SBOM-GATE-001

- Declared action: N/A — no command and no execution. Read-only disposition: docs/security/supply-chain-evidence.md.
- Reevaluation: A model, dataset, inference service, infrastructure, or AI runtime becomes a product or operated component.

#### ZERO-TRUST-GATE-001

- Declared action: N/A — no command and no execution. Read-only disposition: docs/security/zero-trust-applicability.md.
- Reevaluation: Remote, cloud-runtime, multi-device, service, federated identity, or remote-management scope.

#### PRODUCT-CRYPTO-DPIA-GATE-001

- Declared action: N/A — no command and no execution. Read-only disposition: docs/security/regulatory-applicability.md and assessment rows for CL-03/CL-11.
- Reevaluation: Any cryptography, authentication, secret, account, telemetry, personal-data, profiling, recipient, or retention scope.

#### NIS2-AIACT-DORA-GATE-001

- Declared action: N/A — no command and no execution. Read-only disposition: docs/security/regulatory-applicability.md.
- Reevaluation: Operator/customer/supply relationship, AI product, financial-sector service, or regulated business model change.

#### PARALLEL-AUTONOMOUS-GATE-001

- Declared action: N/A — no command and no execution. Read-only disposition: specs/004-secure-development-hardening/plan.md.
- Reevaluation: Explicit user authority for a parallel autonomous campaign.

#### SANDBOX-HARDENING-GATE-001

- Declared action: N/A — no command and no execution. Read-only disposition: assessment.json CL-12 rows and the separate sandbox follow-up intake reference.
- Reevaluation: A separate accepted and explicitly authorised sandbox-hardening run after this feature's hard completion gate.

## Intentional safe boundary inside T085

- T001–T084 are complete; T085–T110 remain unchecked. The documentation
  portion of the T085 evidence freeze is complete, but the orchestrator still
  owns version alignment to `1.72.454.38`, the evidence-freeze commit, and the
  atomic T085 checkbox. This routed phase result is therefore `Blocked` with
  `84/110`, not terminally `Completed`.
- No additional A11Y cycle, npm, Playwright, Chromium, Lynx, DocFX,
  `dotnet build`, or `dotnet test` command was run during this continuation.
  No IDE version edit, commit, push, pull request, intake/series/lifecycle edit,
  or next-feature action occurred.

## Final Exact-Head, Remote and Closeout Evidence — T086–T110

### Final candidate and local proof

- The canonical feature PR slot remained `72`. The exact reviewed candidate is
  `1526e64e34371e89aac6d4e6a6e41b5286270a36` with IDE version
  `1.72.464.42`; all three version fields are identical and Patch `464` equals
  the exact-head commit count.
- One final Release suite passed `275/275`, with zero failures and zero skips.
  It included the 41 mandatory catalogue cases, VM, localisation, Golden,
  traceability, and architecture guards without Golden regeneration.
- Cobertura evidence reports 70.88 % overall line coverage against the
  mandatory 70.23 % floor. Changed VM lines are 61/61 (100 %); changed VM
  branches are 21/22 (95.45 %). The separate 80-% overall goal remains
  `TargetOpen` with maintainer ownership and reevaluation on the next coverage
  or source change.
- The final security diff scan ID
  `c82bebcb-42d1-42a7-9564-66ffca86210a` covered 18/18 surfaces and produced
  zero reportable findings. `git diff --check`, delivery paths, and the clean
  exact-head worktree passed.

### Provider and human review proof

- CI, Agent Secret Scan, Gitleaks, Homogeneity, Maintenance TUI, PowerShell
  Static Analysis, all PowerShell/Bash baseline platforms, and Docs Pages
  completed successfully on the exact head. PR-only Pages deployment and smoke
  jobs were correctly skipped; they do not claim publication.
- The optional Claude reviewer failed twice in the external model step without
  comments, findings, or an approval. It was not retried again and was never
  counted as success.
- The Gitleaks `generic-api-key` report at the harmless
  `PRODUCT-CRYPTO-DPIA-GATE-001` prose rationale was independently classified
  as a false positive. The exact full-line allowlist from commit `29516b1` and
  a full redacted 17-commit scan proved `no leaks found`; no duplicate
  `.gitleaksignore` suppression was added. The PR thread was answered and
  resolved.
- Repository Owner `hindermath` explicitly approved the unchanged head in
  `https://github.com/hindermath/TinyPl0/pull/72#issuecomment-5469201251` at
  `2026-08-30T14:15:50Z`. This is the independent human decision required by
  T100. GitHub could not store author self-approval as formal `APPROVED`.

### Schema-2.0 evidence and merge decision

- PreMerge snapshot `e3eba5cc-4859-435f-84ff-29198f7f91a0` contains exactly
  31 Primary rows: 25 `Applicable` Pass and six reasoned non-executing `N/A`.
  Its normalized hash is
  `b7302d0112e787a8ded3d6389c33353c2fd09a821294274af755537ece21f90e`;
  it remained temporary and made no merge claim.
- Ruleset `13093926` required one approving review and exposed RepositoryRole
  `5` with `always` bypass. The recorded decision was `AuthorizedRequired`:
  all technical, risk, thread, Head, and human-review evidence was complete;
  only the provider's formal Self-Review policy remained.
- `gh pr merge 72 --merge --delete-branch --admin` merged only reviewed head
  `1526e64e34371e89aac6d4e6a6e41b5286270a36` at
  `2026-08-30T14:23:07Z`. Merge commit is
  `e37acee1792911c0b0c2c2115edefe4bcd22f613`; admin bypass was consumed only
  for that policy bit.
- `gh repo sync` was attempted and correctly reported that the repository is
  not a fork. The lossless local fallback fetched `origin/main` and
  fast-forwarded local `main`; local main, `origin/main`, and the PR merge
  commit then matched exactly. Local and remote feature branches were deleted.
- PostMerge snapshot `a3f3a026-91bd-4f18-99a2-de4726cd31f9` binds the accepted
  PreMerge hash, reviewed head, merge commit, sync, cleanup, and no product
  delta. It validates with normalized hash
  `f64e2c4be74d13594a711af49e3e3058ce64ddf88b6fa2f145de8abc5c5645af`
  and `mergeAuthorized: true`.

### Causal lifecycle and retrospective

- The fulfilled intake moved byte-for-byte to
  `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md`;
  its normalized SHA-256 remains
  `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de`.
- Prior manifest and receipt were archived byte-identically under
  `requirements/intakes/series-archive/tinypl0-delivery/20260830T142658Z/`.
  The successor manifest keeps 15 targets, five roots, ten binding edges and
  all ordering/evidence paths. Target 004 is `Completed`; only the next serial
  Sandbox target is declared `Eligible`.
- PowerShell and Bash manifest/receipt validators passed on the successor.
  `Pflichtenheft.md` and both order views name the Sandbox intake consistently.
- The delivery-set validator now preserves exactly two semantic Markdown
  hard-break spaces while continuing to reject stray or excessive trailing
  whitespace; positive and negative PowerShell/Bash fixtures pass.
- `autonomous-run-retrospective.md` and `retrospective-handoff.md` classify seven
  observations, promote only portable deterministic rules, and grant no
  downstream authority. No product logic, public API, secret, Sandbox content,
  or follow-up feature was changed in this closeout.
