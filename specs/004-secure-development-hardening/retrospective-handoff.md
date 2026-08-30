# Portable Retrospektiven-Übergabe / Portable Retrospective Handoff

Diese Übergabe fasst wiederverwendbare Regeln aus Run
`abaa7b81-fd2c-47e7-8d59-87a852a3b2e7` zusammen. Sie erteilt keine
Berechtigung für Commit, Push, Review, Merge, Bypass oder Änderungen in einem
anderen Repository. / This handoff grants no authority to commit, push,
review, merge, bypass, or modify another repository.

## Geförderte Regeln / Promoted Rules

1. Verwende für einen Secret-Scan-Falschpositiv genau eine enge Ausnahme und
   belege sie mit Quellsemantik plus vollständigem redigiertem Scan. Redundante
   Ignore-Einträge werden nicht gestapelt. / Use one narrow false-positive
   suppression and prove it from source semantics plus a full redacted scan.
2. Ein deterministischer Statistik-Renderer darf reine Statistik- und
   Versionsmetadaten aus der fachlichen Source-Revision ausnehmen, wenn zwei
   unabhängige Beobachtungen dies belegen und eine Produktänderung den Hash
   weiterhin ändert. / Pure statistics and version metadata may be excluded
   from the semantic source revision only after two independent observations.
3. Lokale Docs-Smoke-Tests warten begrenzt auf Server-Readiness, schlagen bei
   Timeout fail-closed fehl, zeigen das Serverlog und räumen den Prozess immer
   auf. / Local documentation smoke tests use bounded readiness, fail closed,
   surface the server log, and always clean up.
4. Plattformübergreifende Textprüfungen behandeln LF und CRLF explizit gleich
   und werden auf Windows sowie Unix getestet. / Cross-platform text checks
   treat LF and CRLF equivalently and test both Windows and Unix paths.
5. Fehlende Reviewer sind nie Zustimmung. Ein Admin-Bypass darf nur nach
   expliziter menschlicher Head-Genehmigung und vollständiger technischer
   Evidence die verbleibende Provider-Policy überwinden. / Missing reviewers
   are never approval; bypass may cross only the remaining provider policy
   after explicit human head approval and complete technical evidence.
6. Formatprüfungen unterscheiden eng zwischen semantischen Markdown-Hardbreaks
   mit genau zwei Endleerzeichen und zufälligem Whitespace. Positiv- und
   Negativtests müssen in allen unterstützten Shellpfaden gleich entscheiden. /
   Format validation narrowly distinguishes semantic two-space Markdown hard
   breaks from stray whitespace and proves parity with positive and negative
   tests in every supported shell path.

## Weiter beobachten / Observe Again

- Ein Modellkatalog-Refresh ohne Rollen- oder Modelländerung ist erst einmal
  beobachtet. Vor einer allgemeinen Automatisierung ist eine zweite
  unabhängige Beobachtung nötig. / A catalog-only model-routing refresh needs
  a second independent observation before general automation.

## Evidenz / Evidence

- `specs/004-secure-development-hardening/autonomous-run-retrospective.md`
- `specs/004-secure-development-hardening/autonomous-run-evidence.md`
- PR `https://github.com/hindermath/TinyPl0/pull/72`
- Reviewed Head `1526e64e34371e89aac6d4e6a6e41b5286270a36`
- Merge-Commit `e37acee1792911c0b0c2c2115edefe4bcd22f613`
