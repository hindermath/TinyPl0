# [PROJECT NAME] Development Guidelines

Auto-generated from all feature plans. Last updated: [DATE]

## Active Technologies

[EXTRACTED FROM ALL PLAN.MD FILES]

## Project Structure

```text
[ACTUAL STRUCTURE FROM PLANS]
```

## Commands

[ONLY COMMANDS FOR ACTIVE TECHNOLOGIES]

## Code Style

[LANGUAGE-SPECIFIC, ONLY FOR LANGUAGES IN USE]

## Recent Changes

[LAST 3 FEATURES AND WHAT THEY ADDED]


## Spec-Kit-Modell-Routing / Spec Kit Model Routing

- Modellwahl ist operative Agenten-Routing-Guidance, keine Feature-Anforderung. Modellnamen nicht in `spec.md`, `plan.md`, `tasks.md` oder einzelne Feature-Specs schreiben; diese Artefakte muessen reproduzierbar bleiben, auch wenn Modellnamen wechseln oder ein anderer KI-Agent verwendet wird.
- Der jeweilige Agent soll diese Empfehlungen auf seine aktuell verfuegbaren Modelle abbilden; keine feste Anbieter- oder Modellbindung ableiten.
- Fuer Spec-Kit-Spezifikation, Klaerung, Planung, Tasks und Analyse (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; je nach Agent auch `/speckit.specify` usw.) das staerkste verfuegbare Frontier-Reasoning-/Coding-Modell bevorzugen.
- Fuer vollstaendige, lang laufende `/speckit-implement`-Laeufe das staerkste verfuegbare Long-Running-Agent-Modell bevorzugen; das Frontier-Modell nutzen, wenn maximale Urteilsguete wichtiger ist als Laufzeitstabilitaet.
- Fuer fokussierte Reviews oder CI-Fixes ein coding-optimiertes Modell bevorzugen.
- Fuer triviale Bereinigung, Formatierung oder risikoarme mechanische Edits ist ein schnelles kleines Coding-Modell akzeptabel.

*Model choice is operational agent-routing guidance, not a feature requirement. Do not pin model names in `spec.md`, `plan.md`, `tasks.md`, or individual feature specs; those artifacts must stay reproducible even when model names change or another AI agent is used. Each agent should map these recommendations to its currently available models; do not derive a fixed vendor or model requirement. For Spec-Kit specification, clarification, planning, task generation, and analysis (`/speckit-specify`, `/speckit-clarify`, `/speckit-plan`, `/speckit-tasks`, `/speckit-analyze`; or `/speckit.specify` etc. depending on the agent surface), prefer the strongest available frontier reasoning/coding model. For complete long-running `/speckit-implement` runs, prefer the strongest available long-running agent model; use the frontier model when maximum judgment quality is more important than runtime stability. For focused review or CI fixes, prefer a coding-optimized model. For trivial cleanup, formatting, or low-risk mechanical edits, a fast small coding model is acceptable.*

## Spec-Kit Governance Presets

If this project installs governance presets, keep this section synchronized
with `.specify/presets/` and generated agent command files. Registered Level-0,
Level-1, and Level-2 repositories default to all eight home-baseline presets
unless a justified exception is documented: `security-governance`, `architecture-governance`,
`isaqb-architecture-governance`, `a11y-governance`,
`cross-platform-governance`, `agent-parity-governance`,
`autonomous-run-governance`, and `parallel-autonomous-run-governance`.
`architecture-governance` includes conditional BSI C3A cloud-autonomy and BSI
C5 cloud-compliance assurance evidence for cloud-service selection and
provider-dependent deployments. `security-governance` includes regulatory
applicability screening for NIS2, CRA, EU AI Act, and DORA with explicit N/A
rationale for private training projects when no regulated scope exists.
Installing either autonomous preset starts no run and grants no remote, merge,
bypass, cancellation, secret, or provider authority. Complete autonomous and
parallel autonomous runs require explicit delegation. Parallel campaigns use
separate worktrees and at most three concurrently active workers.

## Didaktische und sprachliche Klarheit / Pedagogical and Linguistic Clarity

- Neue oder geänderte lernendenseitige Inhalte stehen Deutsch zuerst und
  Englisch danach auf CEFR B2 und bleiben text-first; WCAG 2.2 Level AA gilt,
  soweit die Kriterien anwendbar sind.
- Extern öffentliche APIs erhalten vollständige, fachlich anwendbare
  XML-Dokumentation. CS1591 darf nicht global oder projektweit unterdrückt
  werden; lokale, private und generierte Flächen bleiben ausgeschlossen.
- API-Signatur- oder XML-Kommentaränderungen verlangen DocFX und eine
  textorientierte A11Y-Prüfung im selben Arbeitsgegenstand.
- Nicht-triviale Logik wird auf kurze zweisprachige Warum-Kommentare geprüft.
  Neue Funktionen und Fehlerkorrekturen belegen TDD Rot, Grün und Regression;
  reine Text-/Governance-Arbeit dokumentiert ein begründetes `N/A` mit Trigger.

*Learner-facing content is German-first/English-second at CEFR B2 and remains
text-first under WCAG 2.2 AA where applicable. Externally public APIs receive
complete applicable XML documentation without global or project-wide CS1591
suppression. API or XML changes require DocFX and text-oriented accessibility
evidence. Review non-trivial logic for bilingual why-comments, and record TDD
red, green, and regression evidence or a reasoned text/governance `N/A`.*

<!-- MANUAL ADDITIONS START -->
<!-- MANUAL ADDITIONS END -->
