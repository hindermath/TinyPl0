# Autonomous Run Evidence: 005 Sandbox-Supported Secure Development

## Identity and Authority

| Field | Value |
|---|---|
| Feature | `005-sandbox-secure-development` |
| Binding intake | `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` |
| Accepted intake SHA-256 | `628f869c9df39329949b73457bd56d4345f467ef38d453f257887d07b8f58735` |
| Accepted review | `8804ad13-41b4-4feb-a10d-26d2f55333e6`, `Ready` |
| Delivery mode | `MergeAndSync` |
| Bypass authority | Explicitly authorized, narrowly limited, and never a substitute for review or Approval |
| Secret authority | None; secrets, private profiles, caches, and host paths must not be read into or exposed by evidence |
| Evidence owner | TinyPl0 project owner and autonomous coordinator |
| Run-state path | `specs/005-sandbox-secure-development/autonomous-run-state.json` |
| Run-state status | `Active` |

## Accepted Boundaries

- Exactly one intake and one feature are active in this run.
- Product code, the Sandbox image and repository, and existing `docs/security/` evidence are outside the change scope.
- The read-only Sandbox observation uses commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`. Uncommitted work in that separate repository is excluded from TinyPl0 evidence.
- The predecessor is preserved at `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md`; the current Ready series review resolves the historical path relocation.
- Feature-local assessment and evidence-matrix files may name later evidence destinations, but must not claim that a human approval, image validation, provider approval, or technical hardening already occurred.

## Resolved Project Policy

- TinyPl0 uses memory-safe C# 14 on .NET 10, while secure file, dependency, process, logging, and input boundaries remain mandatory.
- NIST SSDF and CWE Top 25 apply. SBOM, SLSA, OpenSSF, SAMM, and sandbox STRIDE/CIA/CAPEC evaluation apply within the documented scope.
- ASVS, AI-SBOM, Zero Trust, BSI cloud assurance, and regulatory frameworks are currently reasoned `N/A` with explicit re-evaluation triggers.
- Learner-facing evidence is German-first/English-second, approximately CEFR B2, text-first, and reviewed against applicable WCAG 2.2 AA criteria.
- No new script-shaped tool is in scope, and no shared agent-guidance surface is changed.

## Model Routing

| Phase group | Role | Local profile | Preflight |
|---|---|---|---|
| Specify through Analyze | `frontier-reasoning` | `codex-frontier-auto` | Aligned |
| Implement | `long-running-implementation` | `codex-implementation-auto` | Aligned |
| Retrospective | `coding-review` | `codex-review-auto` | Aligned |

Model identifiers are stored only as runtime evidence in the run-state. They are not feature requirements.

## Scope and Convergence

| Gate | State | Evidence or disposition |
|---|---|---|
| Preflight | Pass | Clean synchronized base, current Ready review, eligible binding intake, explicit authority, aligned routing, and safe stop window |
| Specify | Pass | `spec.md`, requirements checklist 32/32, structured result `c9013098…d95d227b` |
| Clarify | Pass | Zero questions, zero material ambiguities, report `4c1db92f…17ae74a2` |
| Checklists | Pass | Requirements 32/32 plus Sandbox governance 40/40; result `fa7dc9f8…17a98081` |
| Plan | Pass | Seven artifacts, 13 unique schema-valid gates; result `0d6dc923…d47620bc` |
| Plan review | Pass | 3 High, 1 Medium, 1 Low resolved; 0 open Critical/High/Medium; result `46c2aedc…21c62d69` |
| Tasks | Pass | 69 strictly serial tasks, no parallel marker, complete user-story/gate/delivery/closeout coverage; result `616a3e39…1d395dc0` |
| Analyze | Pass | 2 High candidate/scope and 2 Medium traceability findings resolved; 0 open Critical/High/Medium; result `a5a3079e…ffecfa74` |
| Implementation | Open | Documentation-only assessment package pending |

## Sandbox Observation Boundary

The stable Sandbox reference provides a .NET 10 base image, a non-root service user, explicit bind mounts, named agent/build volumes, `no-new-privileges`, dropped capabilities, pinned tool versions, a documented free-egress risk decision, and SBOM tooling. These facts make TinyPl0 work technically plausible. They do not prove current human approval, accepted image identity, TinyPl0 execution, current egress acceptance, or clean adoption of the separate repository's uncommitted work. The starting disposition is therefore **conditional pilot use with Open approval and execution evidence**.

## Implemented Assessment Slice

- `sandbox-assessment.md` contains CL-12-01..12 exactly once, in canonical
  order, with both status axes, learning stage, owner/reviewer, bilingual
  rationale, evidence, residual risk, trigger, and next action.
- The usage decision is `Not Ready` for regular/autonomous Sandbox write work.
  A later read/build/test use remains `Conditional Pilot` and currently `Open`.
- The symbolic mount contract allows only the TinyPl0 project plus separate
  build/audit/agent-state storage. Home, profiles, keys, credentials, other
  projects, and private tool state are `NotMounted` or `Denied`.
- The work-location matrix covers restore, build, test, coverage, DocFX, axe,
  `lynx`, Golden updates, Sandbox smoke, agent read/write, provider access,
  Git delivery, CI, and human review.
- `evidence-matrix.md` maps 17 FRs, 5 CRs, 7 SCs, 12 CL-12 items, 12 stable
  gates, 13 machine gates, security standards, eight presets, and eight
  complete FUP-SBX follow-ups.

## Reasoned N/A and Read-Only Dispositions

| Area | Decision | Rationale | Re-evaluation trigger |
|---|---|---|---|
| Product TDD and coverage | `N/A` | No product, test, API, runtime, or compiler output changes; the unchanged document contract supplies red and green evidence. | Any product or executable-test change. |
| `dotnet build` / `dotnet test` | `N/A` | No executable dependency consumes the two feature-local Markdown outputs; running product tests would not validate their contract. | Validator dependency search finds an executable consumer or scope changes. |
| Public API, XML and DocFX | `N/A` | No public API, XML comment, DocFX content, navigation, or generated HTML changes. | API/XML/DocFX/navigation change. |
| Didactic code comments | `N/A` | No program logic changed. | New or changed non-trivial logic. |
| Script, man page, Cmdlet and dry-run parity | `N/A` | No script-shaped tool is added, changed, or removed. | A later Sandbox automation script enters TinyPl0 scope. |
| Product architecture, arc42 and S-ADR | `N/A` | Product structure and deployment are unchanged, and no permanent Sandbox architecture is approved. | Technical hardening or a lasting operating decision. |
| Shared agent parity | `N/A` | No shared guidance, template, constitution, or model-routing rule changes. | Any shared rule change. |
| Existing `docs/security/` evidence | `ReadOnly` | The intake explicitly forbids automatic filling or hardening; later targets are named only. | A separate authorised evidence/hardening feature. |

## Validation

| Invocation | Trigger | Explicit root | Exit | Result and proof boundary |
|---|---|---|---:|---|
| `specify preset resolve spec-template` | Specify preflight | TinyPl0 repository root | 0 | Eight-layer composition resolved; top layer security-governance 0.6.2 |
| `validate-autonomous-phase-result.ps1` | Specify completion | TinyPl0 repository root | 0 | Phase, task counts, gates, and spec payload hash validated |
| `check-prerequisites.ps1 -Json -PathsOnly` | Clarify preflight | TinyPl0 repository root | 0 | Known `codex/` branch-path mismatch recorded; explicit feature selector governs; no retry |
| `validate-autonomous-phase-result.ps1` | Clarify completion | TinyPl0 repository root | 0 | Zero-question report and payload hash validated |
| `check-prerequisites.ps1 -Json` | Checklist preflight | TinyPl0 repository root | 1 | Known `codex/` branch-path mismatch; explicit feature selector governed; no retry |
| `validate-autonomous-phase-result.ps1` | Checklist completion | TinyPl0 repository root | 0 | 40/40 requirement-quality items and payload hash validated |
| `Test-Json -SchemaFile` plus semantic checks | Plan | TinyPl0 repository root | 0 | Schema 2.0, 13 unique gates, Applicable/N/A command contracts passed |
| `validate-autonomous-phase-result.ps1` | Plan completion | TinyPl0 repository root | 0 | Seven planning/design outputs and plan payload hash validated |
| Plan cross-artifact and gate review | Plan review | TinyPl0 repository root | 0 | 17/17 FRs, 12/12 stable gates, 13/13 unique machine gates; all material findings resolved |
| `validate-autonomous-phase-result.ps1` | Plan-review completion | TinyPl0 repository root | 0 | Accepted review report and payload hash validated |
| `validate-autonomous-phase-result.ps1` | Tasks completion | TinyPl0 repository root | 0 | 69/69 task-generation checks and tasks payload hash validated |
| Cross-artifact coverage and schema checks | Analyze | TinyPl0 repository root | 0 | 69 tasks; FR 17/17, CR 5/5, SC 7/7, CL-12 12/12, stable gates 12/12, machine gates 13/13 |
| `validate-autonomous-phase-result.ps1` | Analyze completion | TinyPl0 repository root | 0 | Four resolved findings, zero open Critical/High/Medium, and current report hash validated |
| `git diff --check` | Initial candidate | TinyPl0 repository root | 0 | No whitespace errors in current feature files |
| `check-prerequisites.sh --json --require-tasks --include-tasks` | Implement preflight | TinyPl0 repository root | 0 | Explicit feature path and all required design/task documents resolved |
| Checklist inventory | Implement preflight | TinyPl0 repository root | 0 | Requirements 22/22 and Sandbox governance 40/40 complete |
| Run-state, accepted-input and routed-result checks | T001–T004 | TinyPl0 repository root | 0 | Active Implement stage, 4/4 accepted hashes, 7/7 immutable completed-result files, 13 schema-valid gates |
| Intake-series manifest and receipt validators | T005 | TinyPl0 repository root | 0 | Active 15-target series; binding Sandbox intake remains declared Eligible and review remains Ready |
| Immutable Sandbox reference check | T006 | Symbolic external Sandbox root | 0 | Exact commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`; external working tree excluded |
| Unstaged delivery-set inventory | T007 | TinyPl0 repository root | 0 | Only `.specify/feature.json` and the feature directory currently changed; index untouched |
| Document contract before implementation | T008 red | TinyPl0 repository root | 1 expected | Failed only because `sandbox-assessment.md` and `evidence-matrix.md` did not yet exist |
| Document contract after implementation | T043 green | TinyPl0 repository root | 0 | Both required result files exist; unchanged red command now passes |
| Per-section CL-12 relation validator | T044 | TinyPl0 repository root | 0 | 12 unique ordered sections, all required labels, and FUP-SBX relation for every open/incomplete status |
| Mount and work-location validator | T045 | TinyPl0 repository root | 0 | Symbolic TinyPl0 source, `ReadOnly`, `ReadWrite`, `NotMounted`, Build, Test, CI, and HumanOnly present |
| Standards and preset validator | T046 | TinyPl0 repository root | 0 | Required NIST/CWE/supply-chain/A11Y terms and governance presets present |
| Markdown text-first and code-block check | T047 | TinyPl0 repository root | 0 | No untagged fenced code block; DE/EN semantic review completed |
| Added-content private-path gate | H-001/T051 | TinyPl0 repository root | 0 / 1 expected / 0 | Current candidate passes; a temporary synthetic private-path sentinel fails; candidate passes again after sentinel removal |
| Executable-validator dependency search | T049 | TinyPl0 repository root | 0 | No external executable consumes the two feature outputs; affected document, schema, state, scope, statistics, and delivery validators were executed |
| Scope validator | T050 | TinyPl0 repository root | 0 | 22 candidate paths fit the feature, statistics ledger/configuration, and IDE-version allowlist; no product, test, Sandbox, workflow, agent-guidance, or existing security-evidence path changed |
| Scoped gitleaks scans | T051 | Intended public delivery files only | 0 | Five intended path groups scanned with redaction; zero leaks found |
| Statistics renderer | T052 | TinyPl0 repository root | 0 | New slot 14, net 2446 lines, generated Profile 2 block current, `## Gesamtstatistik` remains final |
| PR-slot and IDE version precommit check | T053 | TinyPl0 repository root | 0 | Highest assigned PR 74, reserved slot 75, predicted commit count 476, three fields `1.75.476.42`, no Build increment |
| Exact staged candidate | T055 | TinyPl0 repository root | 0 | Exactly 22 intended paths; state, inputs, Analyze payload, schema, CL-12, boundaries, standards, A11Y, scope, scoped Gitleaks, added-content private-path, statistics, delivery-set, version, and `git diff --cached --check` gates pass |

## Delivery Candidate Integrity

- Intended paths currently consist of `.specify/feature.json` and `specs/005-sandbox-secure-development/`.
- Existing product, security evidence, archived predecessor, active intake, and series artifacts are read-only until their explicitly ordered lifecycle boundary.
- Ignored routing results remain local process evidence and are not part of the delivery candidate.
- Exact delivery validation, secret scan, private-path scan, staged-candidate review, and index preservation remain Open until implementation closeout.

## Remote Delivery

| Item | Result | Evidence |
|---|---|---|
| Push | Open | No remote mutation during Specify |
| Pull request | Open | Created only after local gates and governed version alignment |
| Required checks | Open | Exact-head evidence required |
| Independent review | Open | A genuine Approval on unchanged head is required |
| Admin bypass | Open | May address only a concrete remaining platform-policy blocker after all evidence; never a review substitute |
| Merge | Open | Requires revalidated authority and exact-head gates |
| Default-branch sync | Open | Required after merge |
| Causal closeout | Required | Archive intake byte-identically and update series only after product merge |

## Resume and Follow-up

- Checkpoint commit: `24612a34dd04cfc42cb02df9d675fe6a93dfe716`.
- Last passing gate: Analyze resolved two High candidate/scope and two Medium traceability findings with zero open Critical/High/Medium findings.
- Next exact action: execute T001–T008, including the expected document-contract red, before authoring assessment outputs.
- Stop boundary: do not start a new autonomous run at or after 04:30 Europe/Berlin on 2026-08-31; stop safely no later than 05:30.
- Residual risk: formal Sandbox approval, exact image identity, Egress acceptance, and actual TinyPl0 Sandbox execution are currently Open.
- Follow-up boundary: technical Sandbox or TinyPl0 hardening is a separate, explicitly authorized feature.
