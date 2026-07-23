<!-- intake-authoring:begin -->
# Lastenheft: IDE-Lokalisierung fuer TinyPl0

**Dokumenttyp:** Spec-Kit Intake / Lastenheft  
**Status:** aktiv, noch nicht gestartet  
**Historische Grundlage:** `Lastenheft_L10N.001-l10n-backend.md`, Abschnitte 1 bis 3.2  
**Scope:** ausschließlich `src/Pl0.Ide`

## 1. Zweck / Purpose

Dieses Lastenheft beschreibt den noch offenen IDE-Anteil der TinyPl0-
Lokalisierung. Die bereits abgeschlossene Lokalisierung von `Pl0.Core`,
`Pl0.Vm` und `Pl0.Cli` bleibt historische Grundlage und wird nicht erneut
implementiert.

*This intake defines the remaining TinyPl0 IDE localization work. The completed
localization of `Pl0.Core`, `Pl0.Vm`, and `Pl0.Cli` remains historical evidence
and is not implemented again.*

## 2. Ausgangslage und Zielbild / Current And Target State

Deutsch und Englisch sind für Backend, VM und CLI bereits über Standard-.NET-
Ressourcen nachgewiesen. Die IDE soll dieselbe Sprachgrenze verwenden:
Deutsch bleibt Standard, Englisch ist die erste zusätzliche Sprache und die
gewählte Sprache ist in den IDE-Einstellungen kontrolliert änderbar.

*German and English are already proven for backend, VM, and CLI through
standard .NET resources. The IDE shall use the same language boundary and
provide a controlled language setting.*

## 3. Scope

- Benutzerseitige Texte, Menüs, Dialoge, Status- und Fehlermeldungen in
  `src/Pl0.Ide`.
- Deutsche Standardressourcen und englische Ressourcen in UTF-8.
- Standardmechanismen von .NET 10 und C# 14 ohne zusätzliche
  Lokalisierungsabhängigkeit.
- Persistenz und Anwendung der IDE-Spracheinstellung.
- Tastatur-, Fokus-, Status- und text-first A11Y-Nachweise für beide Sprachen.
- Tests für Ressourcenabdeckung, Fallback, Umschaltung und fehlende Schlüssel.

## 4. Nicht-Ziele / Non-Goals

- Keine erneute Änderung der abgeschlossenen Core-, VM- oder CLI-Lokalisierung.
- Keine neue dritte Sprache.
- Kein Wechsel des UI-Frameworks und keine allgemeine IDE-Neugestaltung.
- Keine implizite Änderung öffentlicher Compiler- oder VM-Verträge.

## 5. Anforderungen / Requirements

- `IDE-L10N-001`: Alle benutzerseitigen IDE-Texte besitzen stabile
  Ressourcen-IDs.
- `IDE-L10N-002`: Deutsch ist der sichere Standard und Englisch vollständig
  verfügbar.
- `IDE-L10N-003`: Die Spracheinstellung wird validiert; ungültige oder fehlende
  Werte fallen nachvollziehbar auf Deutsch zurück.
- `IDE-L10N-004`: Sprachwechsel beschädigen weder Fokus, Shortcuts,
  Statusmeldungen noch Dialogzustände.
- `IDE-L10N-005`: Fehlende Ressourcen werden durch deterministische Tests
  erkannt.
- `IDE-L10N-006`: Texte bleiben auf CEFR-B2-Niveau verständlich und erfüllen
  anwendbare text-first WCAG-2.2-AA-Grenzen.

## 6. Abhängigkeiten, Risiken und Evidence / Dependencies, Risks And Evidence

Verbindliche Grundlagen sind die Abschnitte 1 bis 3.2 des historischen
L10N-Lastenhefts, die umgesetzten Ressourcenmuster und die aktuelle
Repository-Governance. Risiken sind unvollständige Ressourcenabdeckung,
abgeschnittene Texte, verlorene Fokus-/Shortcut-Signale und unbeabsichtigte
Backend-Regressionen. Evidence umfasst Ressourceninventar, Tests,
Tastatur-/Fokusnachweise und eine klare Abgrenzung zum historischen Feature.

## 7. Akzeptanzkriterien / Acceptance Criteria

- Alle IDE-Texte sind Deutsch und Englisch zugeordnet.
- Ressourcen-, Fallback- und Umschalttests sind grün.
- Fokus, Shortcuts, Statusmeldungen und Dialoge sind in beiden Sprachen
  nachvollziehbar bedienbar.
- Core, VM und CLI bleiben funktional unverändert.
- Dokumentierte Evidence enthält keine offene Scope- oder
  Delivery-Mehrdeutigkeit.

## 8. Annahmen und Ausführungsgrenze / Assumptions And Execution Boundary

Das bestehende .NET-Ressourcenmodell bleibt geeignet. Dieses Lastenheft startet
keinen Spec-Kit-Lauf. Jeder spätere Lauf benötigt eine aktuelle ausdrückliche
Freigabe und darf das archivierte Feature `001-l10n-backend` nicht
reaktivieren.
<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

Die folgenden Alternativen starten keinen Lauf automatisch. Der autonome
Prompt ist auf `LocalImplementation` begrenzt und erteilt keine Remote-,
PR-, Merge-, Bypass-, Secret- oder Provider-Berechtigung.

*The alternatives below do not start a run automatically. The autonomous
prompt is limited to `LocalImplementation` and grants no remote,
pull-request, merge, bypass, secret, or provider authority.*

### Specify

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use Lastenheft_IDE-L10N.md as the binding intake. Preserve its scope, non-goals, ordering, governance, evidence, and acceptance criteria. Create or update only the matching feature specification. Do not implement, commit, push, create a pull request, merge, or start another feature.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run using Lastenheft_IDE-L10N.md as the binding intake. Delivery mode: LocalImplementation. Preserve all scope, ordering, security, accessibility, evidence, and acceptance boundaries. Do not push, create or merge a pull request, use bypass authority, expose secrets, or start a follow-up feature.
```
<!-- intake-authoring:end -->
