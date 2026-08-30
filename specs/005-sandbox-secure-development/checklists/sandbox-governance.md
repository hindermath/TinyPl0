# Sandbox Governance Requirements Checklist: Sandbox-Supported Secure Development

**Purpose**: Test whether the feature requirements are complete, clear, consistent, measurable, and audit-ready for a secure Sandbox decision
**Created**: 2026-08-30
**Feature**: [spec.md](../spec.md)
**Depth**: Standard formal PR-review gate
**Audience**: Author, security reviewer, project owner, and learner-facing documentation reviewer

**Note**: This checklist tests the written requirements, not the later implementation or operating environment.

## Requirement Completeness

- [x] CHK001 Are all twelve CL-12 IDs required with both applicability and implementation status axes? [Completeness, Spec §FR-002]
- [x] CHK002 Are every CL-12 row's learning stage, role, rationale, evidence, residual risk, trigger, and next-action fields specified? [Completeness, Spec §FR-003]
- [x] CHK003 Are mount source, target, purpose, rights, and forbidden neighboring areas all required without a private path? [Completeness, Spec §FR-004]
- [x] CHK004 Are agent write boundaries, build storage, tool state, profiles, caches, sessions, and secrets all addressed? [Completeness, Spec §FR-005]
- [x] CHK005 Are build, test, coverage, documentation, accessibility, golden update, smoke check, and review work locations all included? [Completeness, Spec §FR-006]
- [x] CHK006 Are TinyPl0 and Sandbox-image supply-chain evidence explicitly separated? [Completeness, Spec §FR-008]
- [x] CHK007 Are formal approval, immutable image identity, mounts, secret injection, egress, and project execution all required for regular use? [Completeness, Spec §FR-009]
- [x] CHK008 Are feature-local results and later `docs/security/` destinations both defined without authorizing automatic population? [Completeness, Spec §FR-012–FR-013]

## Requirement Clarity

- [x] CHK009 Is the difference between technical plausibility, evidenced TinyPl0 execution, and human operating approval unambiguous? [Clarity, Spec §FR-007 and SC-007]
- [x] CHK010 Is “conditionally usable as a pilot” defined by explicit prerequisites rather than a vague security claim? [Clarity, Spec §FR-009]
- [x] CHK011 Is the accepted Sandbox observation bound to one immutable commit while excluding separate uncommitted work? [Clarity, Spec §FR-010]
- [x] CHK012 Is the predecessor's archived location reconciled with the historical active-path wording? [Clarity, Spec §FR-011]
- [x] CHK013 Are `N/A` and `Open` requirements distinguished by rationale, owner, risk, target date, evidence, and re-evaluation rules? [Clarity, Spec §FR-014]
- [x] CHK014 Is Admin Bypass explicitly bounded as neither review nor Approval? [Clarity, Spec §Autonomous-run-Anwendbarkeit]

## Requirement Consistency

- [x] CHK015 Do the documentation-only requirements remain consistent with the prohibition on product, Sandbox, and `docs/security/` hardening? [Consistency, Spec §FR-012–FR-013]
- [x] CHK016 Does the feature-local evidence plan align with CL-12's allowance for complete Spec-Kit evidence while leaving human approval separate? [Consistency, Spec §FR-002–FR-003 and User Story 3]
- [x] CHK017 Do the MSL decision and secure-coding boundary consistently state that C# memory safety does not replace secure APIs and I/O? [Consistency, Spec §CR-002–CR-003]
- [x] CHK018 Are standards decisions consistent with the declared non-Web, non-AI-product, non-cloud product scope? [Consistency, Spec §Security Standards Applicability]
- [x] CHK019 Are cross-platform and agent-parity `N/A` decisions consistent with the absence of script and shared-guidance changes? [Consistency, Spec §Cross-Platform and §Agent-Parität]

## Acceptance Criteria Quality

- [x] CHK020 Can CL-12 completeness be objectively measured as exactly 12 of 12 unique assessments? [Measurability, Spec §SC-001]
- [x] CHK021 Can silent-omission prevention be measured across every named standards and governance domain? [Measurability, Spec §SC-002]
- [x] CHK022 Can every open item be assessed against a complete six-part owner/risk/action/date/evidence/trigger contract? [Measurability, Spec §SC-003]
- [x] CHK023 Are zero-secret, zero-private-path, and zero-out-of-scope-change outcomes objectively stated? [Measurability, Spec §SC-005–SC-006]

## Scenario Coverage

- [x] CHK024 Are the primary decision, daily-work, and audit-follow-up scenarios independently testable? [Coverage, Spec §User Stories 1–3]
- [x] CHK025 Are alternate local and CI fallbacks specified when accepted Sandbox execution is unavailable? [Coverage, Spec §User Story 2]
- [x] CHK026 Are human-only approval and four-eyes review paths kept separate from automated evidence? [Coverage, Spec §User Story 3 and §SBX-G011]
- [x] CHK027 Is causal post-merge archive and series progression specified without starting a follow-up feature? [Coverage, Spec §FR-017]

## Edge Case Coverage

- [x] CHK028 Are dirty reference worktrees, missing image identity, expired approval, and missing data classification addressed? [Edge Case, Spec §Randfälle]
- [x] CHK029 Are forbidden Home, keychain, browser, SSH/GPG, cloud-profile, and token mounts addressed with a stop outcome? [Edge Case, Spec §Randfälle and §FR-005]
- [x] CHK030 Is described-but-unexecuted toolchain support required to remain `Open` rather than pass? [Edge Case, Spec §FR-007]
- [x] CHK031 Is free Egress without current human acceptance kept distinct from an accepted network control? [Edge Case, Spec §Randfälle and §FR-009]

## Non-Functional and Governance Requirements

- [x] CHK032 Are DE-first/EN-second, CEFR B2, semantic Markdown, screen-reader, Braille, and text-browser requirements documented? [Coverage, Spec §FR-015 and §Barrierefreiheit]
- [x] CHK033 Are NIST SSDF and CWE Top 25 unconditional while conditional standards have reasons and triggers? [Coverage, Spec §CR-003 and §Security Standards Applicability]
- [x] CHK034 Are STRIDE/CIA/CAPEC trust boundaries and data classes specified without claiming a product architecture change? [Coverage, Spec §Architecture Applicability]
- [x] CHK035 Are all eight governance presets named with versions and without implicit execution or provider authority? [Traceability, Spec §Governance-Presets]
- [x] CHK036 Is exactly one documentation-impact decision defined with audience, reader path, canonical source, owner, navigation, class, language, platforms, distribution, Home sync, evidence, and trigger? [Completeness, Spec §Dokumentationswirkung]

## Dependencies and Assumptions

- [x] CHK037 Is the immutable Sandbox commit identified as an observation dependency rather than a delivered TinyPl0 artifact? [Dependency, Spec §FR-010 and §Assumptions]
- [x] CHK038 Is the current Ready review identified as authority for the predecessor's archive relocation and the active intake? [Dependency, Spec §FR-011]
- [x] CHK039 Are symbolic paths required as the replacement for concrete private host locations? [Assumption, Spec §FR-004 and §Assumptions]
- [x] CHK040 Is later technical hardening explicitly dependent on a separate, current authorization? [Dependency, Spec §FR-012 and §Assumptions]

## Notes

- All 40 items passed against the current specification; no requirement-quality gap remains.
- The Spec-Kit prerequisite helper's `codex/` branch-path derivation does not change checklist content because `.specify/feature.json`, the run-state, and the phase request identify the same feature directory.
- Implementation evidence is intentionally not credited by this checklist.
