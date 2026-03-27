# Feature Specification: VM Compatibility for Historical `Inc` Opcode

**Feature Branch**: `[002-vm-inc-compat]`  
**Created**: 2026-03-27  
**Status**: Draft  
**Input**: User description: "Erstelle aus der Datei Lastenheft_VM_INC_OpCode.md bitte eine Spezifikation."

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Execute historical teaching artifacts without manual opcode renaming (Priority: P1)

As a learner, teacher, or maintainer working with historical PL/0 material, I want TinyPl0 to
accept the historical stack-reservation name `Inc`, so that I can run or compare older artifacts
without rewriting them to the current `Int` spelling first.

**Why this priority**: This is the direct business value of the feature. Without it, historical
reference artifacts still require manual adaptation before they can be used in TinyPl0.

**Independent Test**: Can be fully tested by loading two otherwise equivalent artifacts, one using
`Inc` and one using `Int`, and verifying that both complete with the same observable result.

**Acceptance Scenarios**:

1. **Given** a valid TinyPl0-compatible execution artifact that uses `Inc` for stack reservation,
   **When** the artifact is processed and executed, **Then** TinyPl0 accepts it as valid and
   completes execution without requiring manual renaming to `Int`.
2. **Given** two equivalent artifacts that differ only in the use of `Inc` versus `Int`,
   **When** both are executed with the same input, **Then** they produce the same observable
   runtime outcome.

---

### User Story 2 - Understand the relationship between historical and current notation (Priority: P2)

As a learner or maintainer comparing historical references with TinyPl0 documentation, I want the
system documentation to explain the relationship between `Inc` and `Int`, so that I can map both
notations confidently and without ambiguity.

**Why this priority**: TinyPl0 is a pedagogical project. Clear terminology is necessary for
teaching, porting, and traceability across Pascal, Delphi, and current TinyPl0 materials.

**Independent Test**: Can be fully tested by reviewing the maintained VM-related documentation and
confirming that the equivalence between `Inc` and `Int` is stated clearly and consistently.

**Acceptance Scenarios**:

1. **Given** a reader consulting TinyPl0 VM instruction documentation, **When** they look up stack
   reservation, **Then** they can see that `Inc` and `Int` refer to the same operation.
2. **Given** teaching material that mentions the historical name `Inc`, **When** it is compared
   with TinyPl0 reference documentation, **Then** the reader can reconcile both notations without
   conflicting definitions.

---

### User Story 3 - Preserve current TinyPl0 compatibility for `Int` artifacts (Priority: P3)

As a maintainer of current TinyPl0 materials, I want existing artifacts that use `Int` to remain
valid and unchanged in behavior, so that the compatibility improvement does not break current
teaching assets, tests, or workflows.

**Why this priority**: The feature adds compatibility, not a new semantic variant. Existing
artifacts and current terminology must remain stable.

**Independent Test**: Can be fully tested by reusing existing `Int`-based artifacts and confirming
that they still work without edits and without changed observable behavior.

**Acceptance Scenarios**:

1. **Given** an existing TinyPl0 artifact that uses `Int`, **When** it is processed after this
   feature is introduced, **Then** it remains valid and behaves exactly as before.
2. **Given** current documentation and workflows that already use `Int`, **When** the feature is
   delivered, **Then** they remain usable without forced migration to a different term.

---

### Edge Cases

- What happens when a teaching artifact mixes `Inc` and `Int` references in the same material?
  TinyPl0 must still communicate that both names refer to the same stack-reservation action.
- How does the system handle an invalid opcode name that resembles `Inc` or `Int` but is neither?
  It must continue to reject the invalid name rather than broadening acceptance beyond the defined
  compatibility scope.
- What happens when a historical-style artifact uses `Inc` in situations that currently trigger
  normal stack-reservation validation or diagnostics? It must receive the same observable treatment
  as the equivalent `Int` form.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST accept the historical stack-reservation name `Inc` anywhere the
  current stack-reservation name `Int` is accepted in supported TinyPl0 execution artifacts.
- **FR-002**: The system MUST continue to accept `Int` without requiring any changes to current
  TinyPl0 artifacts, documentation consumers, or workflows.
- **FR-003**: The system MUST treat `Inc` and `Int` as the same stack-reservation operation with
  identical observable runtime effect when used in otherwise equivalent artifacts.
- **FR-004**: The system MUST preserve the current observable behavior of existing `Int`-based
  artifacts after the compatibility feature is introduced.
- **FR-005**: The system MUST state in maintained VM-related documentation that `Inc` is the
  historical name and `Int` is the currently used TinyPl0 name for the same operation.
- **FR-006**: The system MUST continue to reject unsupported opcode names that are neither `Inc`
  nor `Int`.

### Documentation & Didactic Requirements *(mandatory for TinyPl0)*

- **DR-001**: Learner-facing documentation and comments MUST be bilingual, with German block first
  and English block second.
- **DR-002**: Both language blocks MUST target CEFR/GER B2 readability.
- **DR-003**: Changed or added APIs MUST include complete XML documentation where applicable
  (`<summary>`, `<param>`, `<returns>`, `<exception>`; `<remarks>`/`<example>` when useful).
- **DR-004**: Public API XML documentation gaps MUST be treated as build errors
  (CS1591 MUST NOT be globally suppressed).
- **DR-005**: If API signatures or XML docs change, the related work MUST include a `docfx` run
  from repository root and regeneration of documentation artifacts.

### Key Entities *(include if feature involves data)*

- **Stack Reservation Opcode Name**: The textual name used in an execution artifact to express the
  stack-reservation instruction. Valid names in scope are the historical `Inc` form and the
  current `Int` form.
- **Execution Artifact**: A TinyPl0-compatible artifact, such as a listing or comparable
  executable representation, that can contain stack-reservation instructions.
- **VM Instruction Reference**: Maintained project documentation that explains the meaning of VM
  instructions for learners, teachers, and maintainers.

### Assumptions

- Historical PL/0-oriented materials that use `Inc` intend the same stack-reservation concept that
  current TinyPl0 materials express with `Int`.
- Current TinyPl0 behavior around stack reservation is already correct and should be preserved
  rather than redefined by this feature.
- Existing TinyPl0-facing outputs and learning materials may continue to prefer `Int` as the
  current notation as long as the equivalence to `Inc` is clearly documented.

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: In acceptance coverage for this feature, 100% of paired artifacts that differ only by
  `Inc` versus `Int` produce the same observable execution result under the same input.
- **SC-002**: 100% of pre-existing covered TinyPl0 artifacts that use `Int` remain valid without
  requiring content changes.
- **SC-003**: At least one historical-style artifact using `Inc` can be processed and executed in
  TinyPl0 without manual opcode renaming.
- **SC-004**: Maintained VM-related reference documentation contains a clear, non-conflicting
  explanation that `Inc` and `Int` represent the same stack-reservation operation.
