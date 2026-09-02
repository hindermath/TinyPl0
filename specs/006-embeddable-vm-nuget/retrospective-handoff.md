# Portable Retrospektiven-Übergabe / Portable Retrospective Handoff

Diese Übergabe fasst wiederverwendbare Regeln aus Run
`a01cd5bd-fa86-49f1-b074-cb59a9c24862` zusammen. Sie erteilt keine
Berechtigung für Commit, Push, Review, Merge, Bypass, Provider- oder
Secret-Nutzung in einem anderen Repository. / This handoff grants no authority
to commit, push, review, merge, bypass, use providers, or access secrets in
another repository.

## Geförderte Regeln / Promoted Rules

1. Bei Registry-signierten ZIP-Paketen werden Registry-Signatur, vollständiger
   Quell-/Public-Hash und die bytegenaue Gleichheit aller nicht ergänzten
   Einträge getrennt geprüft. / For registry-signed ZIP packages, verify the
   registry signature, record separate source/public hashes, and compare every
   non-added entry byte-for-byte.
2. Ein Recovery-Workflow lädt ein unveränderliches Ursprungsartefakt mit
   minimalen Leserechten und schließt Build sowie Publish strukturell aus. / A
   recovery workflow downloads an immutable source artifact with minimal read
   permissions and structurally excludes build and publish jobs.
3. Beweisrelevante PowerShell-Downloads verwenden getrennte Statusprüfung,
   begrenzten Retry mit Fehlerbehandlung, `.partial`-Datei und atomare
   Übernahme. / Evidence downloads use a separate status probe, bounded retry
   with error handling, a partial file, and atomic promotion.
4. Registry-normalisierte Identifikatoren und unveränderliche lokale
   Artefaktnamen bleiben getrennte Variablen. / Registry-normalized identifiers
   and immutable local artifact names remain separate variables.

## Evidenz / Evidence

- `specs/006-embeddable-vm-nuget/autonomous-run-retrospective.md`
- `specs/006-embeddable-vm-nuget/evidence/pre-merge-gates.json`
- `specs/006-embeddable-vm-nuget/evidence/post-merge-gates.json`
- `specs/006-embeddable-vm-nuget/autonomous-run-evidence.md`
- Recovery-Run `https://github.com/hindermath/TinyPl0/actions/runs/33687547664`
