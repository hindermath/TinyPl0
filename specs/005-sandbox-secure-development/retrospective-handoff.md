# Portable Retrospektiven-Übergabe / Portable Retrospective Handoff

Diese Übergabe fasst wiederverwendbare Regeln aus Run
`91e9fb51-2e69-4eab-85b7-cb28ec23749d` zusammen. Sie erteilt keine
Berechtigung für Commit, Push, Review, Merge, Bypass oder Änderungen in einem
anderen Repository. / This handoff grants no authority to commit, push,
review, merge, bypass, or modify another repository.

## Geförderte Regeln / Promoted Rules

1. Eine projektspezifische Owner-Ausnahme wird ausdrücklich, Head-gebunden und
   wahrheitsgetreu als Ausnahme dokumentiert. Sie ersetzt keine technischen,
   Risiko-, Secret- oder Evidence-Gates; ein Bypass überwindet nur die eng
   benannte Provider-Policy. / A project-specific owner exception is explicit,
   head-bound, and truthfully recorded as an exception. It replaces no
   technical, risk, secret, or evidence gate.
2. Reine Modellkatalogdrift gilt nur bei vollständiger Parität von Rollen,
   Modellklassen und Reasoning-Stufen als nicht materiell. Vor einer neuen
   gerouteten Phase ist ein autorisierter atomarer Refresh erforderlich. / A
   catalog-only model-routing drift is non-material only when every role,
   model class, and reasoning level remains identical.
3. Serien mit archivierten `Completed`-Zielen verwenden das Manifest als
   deklarierte Inventarquelle und behalten zusätzlich den aktiven
   Alignment-Check. / Series containing archived completed targets declare
   the manifest as inventory source and retain a separate active-alignment
   check.

## Weiter beobachten / Observe Again

- Branchpräfixe wie ein Agenten-Namespace dürfen nicht automatisch als Teil
  des Featureverzeichnisses gelten. Die portable Korrektur braucht eine zweite
  unabhängige Beobachtung. / Agent namespace prefixes must not automatically
  become part of the feature-directory name; a second independent observation
  is required before portable promotion.

## Evidenz / Evidence

- `specs/005-sandbox-secure-development/autonomous-run-retrospective.md`
- `specs/005-sandbox-secure-development/autonomous-run-evidence.md`
- `requirements/intakes/series/tinypl0-delivery/manifest.json`
- PR `https://github.com/hindermath/TinyPl0/pull/75`
- Reviewed Head `8d1a69f44d3ae0a36f3d59c3499e129dbcab7ff6`
- Product merge `25614e87ce74512491e9d7406a7a07a1e331cf20`
