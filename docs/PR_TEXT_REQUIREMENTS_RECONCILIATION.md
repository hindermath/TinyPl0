# PR: Reconcile TinyPl0 requirements and intake inventory

## Problem / Problem

TinyPl0 keeps three Pflichtenhefte, 20 root Lastenhefte, a curated order, and 14
authoring receipts without one machine-readable lifecycle inventory.
`Pflichtenheft_IDE.md` also mixes its normative baseline with an operational
agent worklog.

## Lösung / Solution

Add a read-only, reproducible reconciliation report. It classifies 14 active
intakes, two completed intakes, three historical reference intakes, and three
immutable requirements baselines. A separate migration proposal defines the
future canonical layout without applying it.

## Risiken / Risks

This PR changes only audit evidence and its deterministic generator. It does
not move requirements, alter current order, start Spec Kit, or touch product
code, APIs, dependencies, projects, or runtime behavior.

## Testplan / Test Plan

- `node scripts/reconcile-requirements-intakes.mjs`
- `git diff --check`
- `specify check`
