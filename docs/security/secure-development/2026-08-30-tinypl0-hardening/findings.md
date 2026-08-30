# Befunde und Änderungsautorisierung / Findings and Change Authorisation

## Prüfregel / Review rule

Deutsch: Diese Datei erlaubt genau sechs bedingte Pakete. Jede Autorisierung
benötigt `Applicable` plus `Not Fulfilled` oder `Partly Fulfilled` in
`assessment.json`, einen konkreten Missbrauchsweg, den unveränderbaren
Validatorvertrag, den kleinsten Dateisatz, Grün/Regression, einen Maintainer als
Owner und eine getrennte Security-Review-Rolle. Ein anderer Befund erzeugt in
diesem Lauf keine Änderungserlaubnis.

English: This file permits exactly six conditional packages. Each authorisation
requires the assessment status, concrete risk, immutable validator contract,
smallest file set, green/regression path, maintainer owner, and a separate
security-review role. No other finding grants change authority in this run.

Die unveränderbare Validatorbasis ist `plan.md` mit SHA-256
`8b9108361cf3d7adb03d202b471b90982cf6b48b2053d6e379cb2510d3a5a71b`
und `gate-requirements.json` mit SHA-256
`ab1524b4d3b546fc44cf94513b7ce7600b7de20f7c4508faba4d8c505fb5c96a`.

## FND-BASELINE-001 — Implemented, local verification passed

- Checkpoints: `CL-10-17`, BASE-004; `Applicable` / `Not Fulfilled`.
- Risk: stale versions and missing generators can make a drifting control set
  appear current; integrity/audit severity `High`, residual `Low` after parity.
- Red expectation: the unchanged baseline gate must fail only because manifest
  versions and the PowerShell/Bash/help paths are missing or stale.
- Red observed: exit `10`; stderr identified baseline drift and exactly the
  three missing generator/help paths. No tool, network, or timeout error.
- Smallest files: `docs/secure-development/baseline-manifest.json`,
  `scripts/build-secure-development-docs.ps1`,
  `scripts/build-secure-development-docs.sh`,
  `docs/man/build-secure-development-docs.1.md`, and
  `.github/workflows/powershell-analysis.yml`.
- Green/regression: the same validator, PowerShell/Bash check and dry-run,
  157-ID order/hash parity; remote OS parity remains later evidence.
- Owner/reviewer: TinyPl0 maintainer / independent security reviewer.
- Current state: local PowerShell/Bash parity, 157-ID order, help, strict-mode,
  dry-run, and candidate hash checks passed. The provider OS matrix remains
  future remote evidence under T099 and is not claimed here.

## FND-SC-001 — Implemented, local SBOM verification passed

- Checkpoints: `CL-05-01`, `CL-05-02`, `CL-05-04`, `CL-05-11`;
  `Applicable` / `Not Fulfilled`.
- Risk: distributable Pages/release artefacts lack a bound machine-readable
  inventory and provenance; supply-chain severity `High`.
- Red expectation: the unchanged supply-chain gate fails only on missing pinned
  CycloneDX 6.2.0 tool, SBOM/artifact hashes, VEX/SLSA state, or action pins.
- Red observed: exit `11`; stderr identified the missing tool manifest and
  SBOM/artifact/VEX/SLSA tokens. No restore or network action was attempted.
- Smallest files: `.config/dotnet-tools.json` and
  `.github/workflows/docs-pages.yml`; `.github/workflows/release-please.yml`
  stays read-only.
- Green/regression: same validator, local CycloneDX JSON, dependency/licence
  review, artefact hash, and truthful VEX/SLSA claims.
- Owner/reviewer: TinyPl0 maintainer / independent supply-chain reviewer.
- Current state: official `CycloneDX` 6.2.0 restored and produced a valid
  CycloneDX 1.7 JSON with 47 components. Full action-SHA validation passed.
  Final site-artifact binding follows T063; remote attestation is not claimed.

## FND-CVD-001 — Implemented, local verification passed

- Checkpoints: `CL-06-01`, `CL-06-02`; `Applicable` / `Not Fulfilled`.
- Risk: reporters lack a discoverable safe disclosure path; severity `High`.
- Red expectation: the unchanged CVD gate fails only on missing policy,
  RFC-9116 fields, or publishable `.well-known` path.
- Red observed: exit `12`; stderr named the two absent CVD artefacts.
- Smallest files: `.github/SECURITY.md`, `docfx/.well-known/security.txt`, and
  `docfx.json`.
- Green/regression: same validator, expiry/link smoke, DocFX, axe and lynx.
- Owner/reviewer: TinyPl0 maintainer / independent security reviewer.
- Current state: the unchanged CVD gate, RFC-9116 expiry check, and DocFX
  publication-path/link smoke passed. Final rendered-page checks follow the
  single T075–T082 documentation cycle.

## FND-GITIGNORE-001 — Verified

- Checkpoint: `CL-10-07`; `Applicable` / `Not Fulfilled`.
- Risk: new root files can be trackable by default; disclosure severity `High`.
- Red expectation: a read-only validator checks literal root deny patterns and
  synthetic credential/agent-state names with `git check-ignore`; it reads no
  secret or private state.
- Red observed: exit `13`; only synthetic sentinel names and `.gitignore`
  patterns were inspected. No secret or private agent file was opened.
- Smallest files: `.gitignore` only.
- Green/regression: same sentinels ignored; every tracked path remains visible.
- Owner/reviewer: TinyPl0 maintainer / independent security reviewer.
- Current state: all 1,858 tracked paths remain visible, no tracked path is
  ignored, and all synthetic sentinels are denied. No secret content or private
  agent state was read.

## FND-A11Y-001 — Implemented, inventory verification passed

- Checkpoints: `CL-08-12`, `CL-10-09`; `Applicable` / `Not Fulfilled`.
- Risk: changed public API pages lack executable axe/text-browser proof;
  accessibility severity `High`.
- Red expectation: the read-only inventory fails only because Node 24,
  lockfile-bound Playwright/axe, three named pages, and a separate lynx path are
  absent. Global Node 26 is not accepted as red or green evidence.
- Red observed: exit `14`; stderr listed Node 24, npm/axe/lynx/API-page and
  lockfile inventory gaps. The global Node binary was not executed as evidence.
- Smallest files: `tests/a11y/package.json`, `tests/a11y/package-lock.json`,
  `tests/a11y/docfx-a11y.spec.mjs`, and `.github/workflows/docs-pages.yml`.
- Green/regression: managed Node 24, `npm ci`, axe, separate lynx, safe cleanup.
- Owner/reviewer: TinyPl0 maintainer / independent accessibility reviewer.
- Current state: genuine Node 24.20.0, npm 11.19.0, offline `npm ci`, exact
  Playwright/axe pins and licences, three listed tests, Chromium availability,
  workflow inventory, and an offline audit with zero findings passed. The
  single rendered-page run follows T078.

## FND-GOV-001 — N/A / Non-trigger

- Checkpoint: `CL-09-13`; `Applicable` / `Fulfilled` after repository review.
- The unchanged semantic validator exited `0`. All five maintained agent files
  already use the canonical PR number, exact branch commit count, and a build
  increment before every build/test. No `scripts/templates/*.tmpl` file carries
  the stale rule.
- No red condition exists, so this package is not authorised and none of
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, or
  `.github/agents/copilot-instructions.md` may be edited by this finding.
- Owner/reviewer: TinyPl0 maintainer / independent governance reviewer.

## Outside the six / Außerhalb der sechs

`FND-HTTP-001` is `Open` without edit authority. The current `localhost:5000`
path receives read-only ASVS review. Every new finding is `Open|FollowUp`; a
Critical or High result blocks this phase instead of opening a seventh package.
