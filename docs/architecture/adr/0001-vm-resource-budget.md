# ADR-0001: Positives VM-Instruktionsbudget / Positive VM Instruction Budget

**Status**: Accepted
**Date**: 2026-08-30
**Decision roles**: TinyPl0 maintainer / architecture reviewer

## Context

Beide VM-Wege können Endlosschleifen ausführen und allokieren den Stack vor
einer vollständigen Optionsprüfung. Das betrifft Verfügbarkeit, sichere
Fehlerpfade und den didaktischen Vertrag.

## Decision

`VirtualMachineOptions` erhält den letzten Parameter
`InstructionBudget = 1_000_000`. Gültig sind Budget `>0` und Stack
`3..1_000_000`. Ein gemeinsamer Validator läuft vor `StackSize + 1`, Allokation
und Ausführung. Batch und Step zählen unmittelbar vor Ausführung; nach `N`
Instruktionen wird vor `N+1` diagnostiziert.

## Rationale and alternatives

Eine Instruktionszahl ist deterministisch. Timeouts/Cancellation allein sind
lastabhängig; `long` vergrößert ohne Nutzen die API; Zählen nach Ausführung
erlaubt Nebenwirkungen von `N+1`; eine große VM-Zusammenlegung wäre zu breit.

## Consequences

Positive Folge: reproduzierbare Ressourcenpolicy, fail-safe Allokation und
Batch-/Step-Parität. Kosten: neue öffentliche Option, Diagnostics, Tests,
DocFX/A11Y und dauerhafte Paritätstests. Residual risk: keine OS-, CPU-Zeit-
oder Speichersandbox. Related: `docs/security/threat-model.md`,
`docs/security/adr/0001-vm-resource-budget.md`, Feature spec and plan.
