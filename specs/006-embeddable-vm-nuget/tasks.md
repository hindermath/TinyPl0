# Tasks: Embeddable VM and NuGet Packages

## Phase 1 — Contract tests and shared engine

- [x] T001 Add `VmCompletionReason` and append non-breaking step statuses.
- [x] T002 Extend execution and step results while preserving existing constructors and members.
- [x] T003 Add cancellation-aware overloads while preserving existing `Run`, `Initialize`, and `Step` entry points.
- [x] T004 Add program-length and instruction-argument trust-boundary validation before allocation.
- [x] T005 Make `SteppableVirtualMachine` the sole instruction decoder and batch `Run` its loop adapter.
- [x] T006 Make terminal state, reason, diagnostics, count and snapshot idempotent.
- [x] T007 Convert host I/O failures to safe structured diagnostics without stack traces.
- [x] T008 Add Run/Step parity tests for success and halt.
- [x] T009 Add parity tests for division, stack, input EOF/format and invalid P-Code.
- [x] T010 Add cancellation-before-start and cancellation-during-run parity tests.
- [x] T011 Add budget N/N+1 and repeated-terminal parity tests.
- [x] T012 Update CLI/IDE consumers for appended statuses without changing user semantics.

## Phase 2 — Packages and independent consumer

- [x] T013 Add complete package metadata, README, XML docs, source/symbol settings to Core.
- [x] T014 Add matching metadata and exact same-version Core dependency to VM.
- [x] T015 Add package README content in DE-first/EN-second B2 form.
- [x] T016 Create `scripts/Test-NuGetPackages.ps1` with strict parameters and no secret handling.
- [x] T017 Pack both `.nupkg` and `.snupkg` files and inspect IDs, version, contents and dependencies.
- [x] T018 Restore/build/run a temporary .NET 10 consumer from a clean local feed without project references.
- [x] T019 Add package-validation tests or script assertions for forbidden IDE/Terminal.Gui/TinyCalc dependencies.
- [x] T020 Add the package-consumer gate to the Linux/Windows/macOS CI matrix.

## Phase 3 — Security and release supply chain

- [x] T021 Update threat model for P-Code, I/O, resource and publisher trust boundaries.
- [x] T022 Add security ADR for the shared engine and OIDC-only publication in this run.
- [x] T023 Update arc42 security, security checklist and quality scenarios.
- [x] T024 Refresh dependency audit and regulatory applicability dispositions.
- [x] T025 Create `scripts/New-NuGetReleaseEvidence.ps1` for SPDX SBOM, VEX, provenance and paired hashes.
- [x] T026 Validate generated release evidence against both exact package files and one commit/version.
- [x] T027 Add public release-evidence template and TinyCalc handoff path.
- [x] T028 Resolve and pin the official `NuGet/login` v1 commit SHA.
- [x] T029 Split Release Please and publish jobs with least-privilege OIDC permissions.
- [x] T030 Add paired ID/version availability preflight and fail-closed partial-publication handling.
- [x] T031 Pack/push both files with Release Please version and no `--skip-duplicate`.
- [x] T032 Add public NuGet.org clean-consumer restore with bounded retry.
- [x] T033 Upload package, SBOM, VEX, provenance and validation artifacts without exposing credentials.

## Phase 4 — Documentation, A11Y and traceability

- [x] T034 Update VM/API/architecture documentation and examples DE-first/EN-second.
- [x] T035 Complete XML docs for every changed public member and regenerate DocFX.
- [x] T036 Run Playwright/axe and lynx text-path checks for representative pages.
- [x] T037 Update `docs/TRACEABILITY_MATRIX.md` for FR/AC to tests and evidence.
- [x] T038 Update project statistics and IDE worklog using existing methodology.

## Phase 5 — Validation and delivery

- [x] T039 Increment governed IDE build before each local build/test and run Release build/test/coverage.
- [x] T040 Run parity, packaging, consumer, security, dependency and evidence gates.
- [x] T041 Freeze and validate the intended delivery set; align IDE version after PR allocation.
- [x] T042 Commit, push and open exactly one feature PR.
- [ ] T043 Converge exact-head checks, independent approval and review threads.
- [ ] T044 Generate and validate temporary schema-2.0 PreMerge evidence for the exact head.
- [ ] T045 Merge with narrowly authorized Admin Bypass only if every prior gate passes, then sync main.
- [ ] T046 Merge the causal Release Please release PR as release closeout, not a follow-up feature.
- [ ] T047 Verify OIDC publication of both public packages and public consumer restore; record URLs and hashes.
- [ ] T048 Archive the intake byte-identically and update manifest/receipt lineage in one causal closeout.
- [ ] T049 Complete PostMerge evidence, final run validation and retrospective.
- [ ] T050 Stop without starting another feature.
