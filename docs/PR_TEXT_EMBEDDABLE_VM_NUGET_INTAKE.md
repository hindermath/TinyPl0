# PR: Add the embeddable VM and NuGet delivery intake

## Problem / Problem

TinyPl0 exposes useful compiler and VM projects, but it has no approved,
traceable delivery contract for safe embedding or stable public NuGet
packages. TinyCalc therefore cannot depend on a verified package boundary.

*TinyPl0 besitzt nutzbare Compiler- und VM-Projekte, aber noch keinen
genehmigten und rueckverfolgbaren Liefervertrag fuer die sichere Einbettung
und stabile oeffentliche NuGet-Pakete. TinyCalc kann deshalb noch nicht auf
eine gepruefte Paketgrenze aufbauen.*

## Lösung / Solution

- Add the embeddable VM and NuGet intake at rank 10, after VM/CLI and before
  the IDE extension.
- Define a safe host API for bounded run and step execution, cancellation,
  structured results, diagnostics, and isolated I/O.
- Require version-matched stable `TinyPl0.Core` and `TinyPl0.Vm` packages plus
  SBOM, VEX, provenance/SLSA, and consumer-contract evidence.
- Keep local package creation separate from remote NuGet publication
  authority.
- Add bilingual CEFR-B2 first-use explanations for host, runtime, security,
  and supply-chain terminology.
- Preserve predecessor manifests and receipts with hash-bound supersession
  evidence and complete the 15-target review with status `Ready`.

*Der Intake wird an Rang 10 zwischen VM/CLI und IDE-Erweiterung eingeordnet.
Lokaler Paketbau und spaetere NuGet-Veroeffentlichung bleiben getrennte
Autoritaeten.*

## Risiken / Risks

This PR changes requirements and governance only. It does not implement the
host API, build or publish packages, modify product code, or grant NuGet
publication authority. Main risks are API drift, inconsistent run/step
semantics, resource exhaustion, and supply-chain substitution; the intake
requires contract tests, limits, cancellation, pinned versions, and release
evidence.

*Dieser PR aendert nur Anforderungen und Governance. Contract-Tests,
Ressourcenlimits, Abbruch, feste Versionen und Liefernachweise begrenzen die
dokumentierten API-, Laufzeit- und Lieferkettenrisiken.*

## Testplan / Test Plan

- Intake-governance configuration validation in Bash and PowerShell
- Complete requirements/intake alignment in Bash and PowerShell
- Intake-review result validation in Bash and PowerShell
- Deterministic governance-renderer and statistics-renderer drift checks
- Request, target, archive, and supersession hash verification
- Agent secret scan and `git diff --check`

No product build, runtime test, package publication, or DocFX regeneration is
required because no product, test, API, runtime, package, or DocFX content
changes.

*Kein Produkt-Build, Laufzeittest, Paket-Upload oder DocFX-Neubau ist
erforderlich, weil Produktcode, Tests, APIs, Laufzeit, Pakete und
DocFX-Inhalte unveraendert bleiben.*
