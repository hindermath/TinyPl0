# Implementierungsplan: Sandbox-gestützte sichere Entwicklung / Implementation Plan: Sandbox-Supported Secure Development

**Branch**: `codex/005-sandbox-secure-development` | **Datum / Date**: 2026-08-30 | **Spezifikation / Spec**: [spec.md](spec.md)
**Input**: Geklärte Spezifikation, [Klärungsbericht](clarification-report.md) und bestandene [Sandbox-Governance-Checkliste](checklists/sandbox-governance.md)

## Zusammenfassung / Summary

Der Lauf erstellt eine Feature-lokale, zweiachsige Bewertung der zwölf CL-12-Prüfpunkte, eine Mount- und Schreibgrenzenmatrix, eine Arbeitsortmatrix sowie eine klare Nutzungsentscheidung für TinyPl0. Er beobachtet ausschließlich den unveränderten Sandbox-Commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`; nicht übernommene Änderungen im separaten Sandbox-Checkout zählen nicht.

*The run creates a feature-local two-axis assessment of all twelve CL-12 items, a mount and write-boundary matrix, a work-location matrix, and a clear TinyPl0 usage decision. It observes only immutable Sandbox commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`; uncommitted changes in the separate Sandbox checkout do not count.*

Die technische Ausgangslage unterstützt .NET 10, getrennte Tool-/Build-Volumes, Non-Root-Ausführung, `no-new-privileges`, entfernte Linux-Capabilities, gepinnte Werkzeuge und SBOM-Erzeugung. Der aktuelle Stand besitzt jedoch nur einen Freigabeentwurf, freien Egress ohne aktuelle Annahme und viele beschreibbare Projektwurzeln. Deshalb gilt: **keine reguläre oder autonome TinyPl0-Schreibnutzung**. Ein späterer, menschlich genehmigter Read/Build/Test-Pilot ist möglich, wenn ein exakter Image-Digest, ein dedizierter TinyPl0-Mount, read-only beziehungsweise leere Nebenwurzeln, Secret-Trennung und ein erfolgreicher Baseline-Lauf nachgewiesen werden.

## Technischer Kontext / Technical Context

**Sprache/Version / Language/Version**: C# 14/.NET 10 als Produktkontext; Markdown und JSON für Feature-Evidenz
**Primäre Abhängigkeiten / Primary Dependencies**: vorhandene Spec-Kit-Presets, Git, PowerShell 7, bestehende Repository-Validatoren; Sandbox-Referenz mit Podman Compose und .NET-10-SDK read-only
**Speicherung / Storage**: versionierte Feature-Markdown-/JSON-Dateien; keine Laufzeitdatenbank; keine Secret-, Profil-, Cache- oder Sitzungsdaten
**Tests / Testing**: Schema-/Markdown-/Hash-/Scope-/Secret-/Privatpfad- und Delivery-Validierung; Produkt-Build/Test `N/A`, solange kein Produkt- oder API-Artefakt geändert wird
**Zielplattform / Target Platform**: TinyPl0 auf macOS/Windows/Linux und bestehender Linux-CI; Referenz-Sandbox als Podman-Container; dieser Lauf startet oder verändert sie nicht
**Projekttyp / Project Type**: Dokumentations- und Governance-Bewertung für Compiler, VM, CLI und Terminal-IDE
**Leistungsziel / Performance Goal**: `N/A`; keine Produktlaufzeit wird geändert. Neubewertung bei technischem Sandbox-Pilot oder Produktcode.
**Einschränkungen / Constraints**: keine Produktlogik, kein Sandbox-Image/-Repo, keine automatische `docs/security/`-Pflege, keine Secrets, keine privaten Pfade, kein Folgefeature, keine Parallelisierung
**Umfang / Scale**: 12 CL-12-Zeilen, 12 stabile Akzeptanz-Gates, eine Nutzungsentscheidung, eine Mount-Matrix, eine Arbeitsortmatrix und vollständige Open-Folgeeinträge

## Constitution Check

*Gate vor Phase 0 und nach Phase 1 erneut geprüft. / Gate before Phase 0 and rechecked after Phase 1.*

| Prüfpunkt / Checkpoint | Entscheidung | Plan und Evidenz / Plan and evidence |
|---|---|---|
| Branch und PR | `Pass` | Ausschließlich `codex/005-sandbox-secure-development`; Lieferung später per `MergeAndSync` nach Exact-Head-Gates und unabhängiger Approval. |
| Level-2 Registry | `Pass` | TinyPl0-Zeile bindet .NET 10/C# 14, Build/Test/Coverage/Golden, DE/EN-A11Y, Statistik 80/125 und Agentenflächen. |
| MSL und Secure Coding | `Applicable` | C# ist MSL. Die Bewertung prüft weiterhin Pfad-, Prozess-, Secret-, Dependency-, Logging- und Eingabegrenzen; keine Codeänderung. |
| Schichten/Produktarchitektur | `N/A` | Modulabhängigkeiten, APIs, Runtime und Deployment bleiben unverändert. Trigger: spätere technische Härtung. |
| Entwicklungsarchitektur | `Applicable` | Host↔Container, Bind-Mounts, Agent-/Build-Volumes, Secrets, Egress und Git/CI werden als Trust Boundaries in `sandbox-assessment.md` bewertet. |
| STRIDE/CIA/CAPEC | `Applicable` | Feature-lokale Risiken für Manipulation, Offenlegung, Nachweisbarkeit und Rechteausweitung; kein Produkt-Threat-Model-Update. |
| S-ADR/arc42/allgemeine ADR | `N/A` | Keine dauerhafte Architektur- oder Betriebsfreigabe wird beschlossen. Trigger: genehmigte reguläre Sandbox-Nutzung oder technische Änderung. |
| NIST SSDF/CWE Top 25 | `Applicable` | Immer für Level 2; abgebildet in Matrix, Entscheidungen und Folgeaktionen. |
| ASVS | `N/A` | Kein Web/API/HTTP/Auth-Scope wird geändert. Trigger: entsprechender Produktscope. |
| SBOM/SLSA/OpenSSF | `Applicable` | Produkt- und Image-Evidenz werden getrennt; bestehende spätere Zielpfade unter `docs/security/` nur benennen. |
| VEX | `Open` | Nur bei bekanntem Fund; Owner Security-/Release-Review, vor nächstem Release, Trigger Scan-/Dependency-Befund. |
| AI-SBOM | `N/A` | KI bleibt Entwicklungswerkzeug, nicht Produktkomponente. |
| Zero Trust/C3A/C5 | `N/A` | Kein verteilter TinyPl0-Dienst und keine Cloud-Produktarchitektur. Providerzugriff der Entwicklungsumgebung bleibt CL-12/Egress-Thema. |
| SAMM | `Applicable` | Langlebiges Projekt; Sandbox-Reife als Verbesserung nennen, bestehende Datei nicht automatisch ändern. |
| Regulatorik | `N/A` | Keine neue Marktbereitstellung, regulierte Kundenrolle, KI-Produktkomponente oder Finanz-ICT-Lieferkette. |
| Security-Dokumente | `Pass mit Grenze` | `docs/security/` wird read-only geprüft. Spätere Ziele: Threat Model, Security Checklist, Dependency Audit, Supply Chain, SAMM und Zero Trust/Cloud/Regulatory notes. |
| TDD | `N/A` für Produktcode | Kein Produktverhalten. Ersatz: unveränderter Dokumentvertrag schlägt vor Erstellung der zwei Ergebnisdateien fehl und besteht danach; Trigger: Produktänderung. |
| Coverage | `N/A` | Keine Code- oder Teständerung. Der bestehende Gate wird nicht neu beansprucht; Trigger: Produktcode. |
| XML/DocFX/A11Y-HTML | `N/A` | Keine API/XML-/DocFX-Änderung. Feature-Markdown erhält semantischen, text-first DE/EN-Review. |
| Per-invocation versioning | `Applicable bei Aufruf` | Kein lokaler `dotnet build/test` geplant. Vor jedem unerwartet erforderlichen Aufruf erst Build-Zähler erhöhen und Version committen. |
| A11Y/Lernende | `Applicable` | DE zuerst/EN danach, CEFR B2, semantische Überschriften, beschreibende Links, Textstatus, keine farb- oder bildabhängige Aussage. |
| Cross-platform scripting | `N/A` | Kein neues/ändertes Skript, keine Manpage/Cmdlet-/Dry-run-Pflicht. Trigger: spätere Sandbox-Automation in TinyPl0. |
| Agent parity | `N/A` | Keine shared Guidance, Templates, Constitution oder Routing-Regel. Alle fünf gepflegten Flächen bleiben unverändert. |
| Statistiken | `Applicable` | Nach abgeschlossener Implementierungsphase genau ein chronologischer Eintrag mit Baselines 80/125; die versionierte Renderer-Konfiguration erhält den neuen Phasenslot. |
| Security-first | `Pass` | Keine Secret-Datei wird gelesen oder getrackt. Delivery-Scan betrachtet nur beabsichtigte öffentliche Dateien und Dateinamen. |
| Dokumentationswirkung | `UpdateRequired` | Nur Feature-Artefakte plus Statistik; Leserpfad und Sprachpartner wie in Spec. Keine DocFX-/Produktnavigation, kein Home-Sync. |
| Acht Presets | `Pass` | Alle acht installierten Presets gelten; `parallel-autonomous-run-governance` ist nur Prüfkontext, keine Kampagnenbefugnis. |

## Phase 0: Recherche / Research

[research.md](research.md) bindet folgende Entscheidungen:

1. Sandbox-Referenz nur über exakten Commit und versionierte Dateien; keine Working-Tree-Aussage.
2. Technische Eignung für .NET 10 ist plausibel, aber ein TinyPl0-Lauf auf akzeptiertem Image bleibt `Open`.
3. Formelle Freigabe ist am Referenzcommit `Entwurf, Freigabe ausstehend`.
4. Aktuelle Default-Mounts und Codex-Writable-Roots sind breiter als ein TinyPl0-only-Arbeitsauftrag; autonomes Schreiben ist daher nicht freigegeben.
5. Freier Compose-Egress besitzt keinen ausgefüllten aktuellen Annahmezeitraum; Regularbetrieb bleibt offen.
6. Sicherer Minimalnutzen ist ein zukünftiger, menschlich freigegebener, befehlsgebundener Read/Build/Test-Pilot. Vollständige Agenten-Schreibarbeit erfordert eine getrennte Härtung oder Betriebsentscheidung.

## Phase 1: Design und Verträge / Design and Contracts

- [data-model.md](data-model.md) definiert CL-12-Zeile, Entscheidung, Mount-Grenze, Arbeitsort und Follow-up.
- [contracts/sandbox-assessment-contract.md](contracts/sandbox-assessment-contract.md) legt Pflichtfelder, Statusbeziehungen und Vollständigkeitsregeln fest.
- [contracts/gate-requirements.schema.json](contracts/gate-requirements.schema.json) validiert die maschinenlesbaren Liefergates.
- [gate-requirements.json](gate-requirements.json) bindet Inputs, 12/12-Bewertung, Scope, Secret-/Privatpfad-Schutz, A11Y, Version, Exact-Head-Review und Closeout vor Implementierung.
- [quickstart.md](quickstart.md) zeigt einen text-first Reviewweg ohne Sandbox- oder Secret-Zugriff.

## Implementierungsstrategie / Implementation Strategy

### Vertikaler Dokumentationsschnitt / Documentation Vertical Slice

1. **Red**: Der im Vertrag beschriebene, read-only Dateikontrakt erwartet `sandbox-assessment.md` und `evidence-matrix.md`; beide fehlen vor Implementierung und der Check endet erwartbar nonzero. Dies ist Dokumentvertrags-, nicht Produkt-TDD-Evidenz.
2. **Green**: Beide Dateien werden als kleinster Delivery-Schnitt erstellt. Derselbe Vertrag prüft 12 eindeutige CL-IDs, Statuswerte, Pflichtfelder, Entscheidung, Mount-/Arbeitsort-Matrix und Open-Folgefelder.
3. **Review**: Separate Text-/A11Y-, Secret-/Privatpfad-, Scope- und Linkprüfungen schließen den Schnitt ab.

### Bewertungsreihenfolge / Assessment Order

1. Identität, Hashes, Referenzcommit und Scope einfrieren.
2. CL-12-01 bis CL-12-12 in kanonischer Reihenfolge bewerten.
3. Mount- und Schreibgrenze mit symbolischen Pfaden dokumentieren.
4. Sandbox/Lokal/CI-Arbeitsorte und Beweisgrenzen festlegen.
5. Standard-, Architektur-, A11Y-, Supply-Chain- und Governance-Mapping ergänzen.
6. Nutzungsentscheidung und priorisierte Folgearbeit aus denselben Zeilen ableiten.
7. Evidence-Matrix, Checklisten, Statistik und Delivery-Set validieren.

## Geplante Nutzungsentscheidung / Planned Usage Decision

| Arbeitsart | Aktuelle Entscheidung | Bedingung / Boundary |
|---|---|---|
| Read-only Inspektion versionierter TinyPl0-Dateien | `Conditional Pilot` | Menschliche Pilotfreigabe, exakte Image-Identität, dedizierter Projekt-Mount, keine Secrets, keine privaten Profile. |
| Direkte Build/Test/Docs-Befehle ohne Agentenschreibzugriff | `Open` | Vorherige Bedingungen plus tatsächliche Baseline-Ausführung und getrennte Build-Artefakte. |
| Agentische Analyse in read-only Modus | `Open` | Systemweite Anforderungen müssen read-only erzwingen; Provider-/Toolfreigabe und Audit-Evidenz aktuell. |
| Agentische TinyPl0-Schreibarbeit | `Not Ready` | Erst minimaler Writable-Root-Vertrag und formelle Freigabe; Sandbox- oder Betriebsänderung ist separate Aufgabe. |
| Commit/Push/PR/Merge aus Sandbox | `N/A` für Pilot | Remote-Lieferung bleibt beim autorisierten TinyPl0-Orchestrator und benötigt unabhängiges Review. |
| Secret-/Token-Nutzung im Repository | `Prohibited` | Secret Store oder geschützte, untracked Injektion; kein Wert in Prompt, Log oder Datei. |

## Projektstruktur / Project Structure

```text
specs/005-sandbox-secure-development/
├── autonomous-run-evidence.md
├── autonomous-run-state.json
├── checklists/
│   ├── requirements.md
│   └── sandbox-governance.md
├── clarification-report.md
├── contracts/
│   ├── gate-requirements.schema.json
│   └── sandbox-assessment-contract.md
├── data-model.md
├── evidence-matrix.md              # implementation output
├── gate-requirements.json
├── plan.md
├── quickstart.md
├── research.md
├── sandbox-assessment.md            # implementation output
├── spec.md
└── tasks.md                         # generated in Tasks phase

docs/project-statistics.md            # mandatory post-implementation ledger update
docs/project-statistics.config.json    # mandatory generated phase-curve configuration
src/Pl0.Ide/Pl0.Ide.csproj           # version metadata only at governed commit boundary
```

**Structure Decision**: Alle fachlichen Ergebnisse bleiben Feature-lokal. Bestehende Produkt-, Architektur-, Accessibility- und Security-Dokumente sind read-only. Nur Statistikledger samt versionierter Renderer-Konfiguration und die verpflichtende IDE-Versionmetadaten-Ausrichtung liegen außerhalb des Feature-Verzeichnisses.

## Validierung und Konvergenz / Validation and Convergence

- Plan Review darf nur Plan-/Vertragsfehler beheben, keine Ergebnisdateien vorwegnehmen.
- Tasks müssen jede Spec-Anforderung und jedes Gate auf genau eine prüfbare Aufgabe abbilden.
- Analyze endet nur mit null offenen Critical/High/Medium-Konsistenzbefunden.
- Implementation endet mit 12/12 CL-12-Zeilen, null fehlenden Pflichtfeldern, null Secret-/Privatpfad-Funden und null unbeauftragten Produkt-/Sandbox-/`docs/security/`-Änderungen.
- Ein fehlender menschlicher Nachweis bleibt `Open`; er blockiert die dokumentarische Feature-Abnahme nicht, solange die Nutzungsentscheidung dadurch nicht positiv überzeichnet wird.
- Remote-Lieferung benötigt aktuelle Exact-Head-Checks, null offene Review-Threads und unabhängige Approval. Admin Bypass kann nur einen danach verbliebenen Plattform-Policy-Blocker behandeln.

## Autonomous Delivery Plan

- Run-State und strukturierte Phasenergebnisse werden an jeder Grenze validiert.
- Die beabsichtigte Delivery-Menge wird vor Commit und Push explizit aufgelistet; Index und fremde Änderungen bleiben unangetastet.
- `gate-requirements.json` ist vor Implementierung reviewbar und unveränderlich gebunden; spätere Änderungen erzwingen erneutes Analyze.
- PreMerge-Evidenz wird auf dem exakten PR-Head temporär erzeugt. PostMerge-Evidenz wird erst kausal nach dem tatsächlichen Merge erzeugt.
- Nach Merge folgt ein separater Closeout-Branch: Intake byte-identisch archivieren, Manifest/Receipt fortschreiben, Status prüfen, main synchronisieren und Retrospektive abschließen.
- Sicherer Stopp: nur an Phasen-, Commit-, Push-, PR-, Merge- oder Closeout-Grenzen. Ab 2026-08-31 04:30 Europe/Berlin wird kein neuer Lauf gestartet; spätestens 05:30 wird sicher gestoppt.

## Complexity Tracking

Keine Constitution-Verletzung und keine Komplexitätsausnahme. Die bewusste Abweichung vom Standard-Evidenzort `docs/security/` ist keine strukturelle Ausnahme, sondern ein ausdrücklich bindendes Nicht-Ziel dieses Intakes; Feature-lokale Spec-Kit-Evidenz ist für diesen Bewertungsstand ausreichend und benennt spätere Zielpfade.
