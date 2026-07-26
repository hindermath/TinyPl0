# PR: Consolidate TinyPl0 requirements and intake governance

## Problem / Problem

Three historical Pflichtenhefte, 20 root Lastenhefte, one order document, and
first-generation receipts mixed immutable baselines, active work, completed
work, and operational IDE history.

## Lösung / Solution

- Freeze all three Pflichtenhefte with their audit hashes.
- Move future IDE entries to `docs/ide-worklog.md`.
- Separate 14 active, two completed, and three historical reference intakes.
- Publish one slim requirements index and one validated intake series.
- Supersede all schema-1.1 receipts with schema-2.0 receipts.
- Add config-driven Bash, PowerShell, Node, negative-fixture, and three-OS CI
  validation.

## Risiken / Risks

Current requirement paths change. Preserved predecessors, supersession
receipts, normalized hashes, and deterministic validation provide traceability.
Product code, APIs, dependencies, projects, and runtime behavior do not change.

## Testplan / Test Plan

- `bash scripts/validate-requirements-intake-alignment.sh`
- `pwsh -NoProfile -File scripts/validate-requirements-intake-alignment.ps1`
- `node scripts/tests/requirements-intake-alignment-tests.mjs`
- `node scripts/reconcile-requirements-intakes.mjs`
- `docfx docfx.json`
- DocFX Playwright/Axe smoke test
- `specify check`
- `git diff --check`
- Agent secret and homogeneity checks
