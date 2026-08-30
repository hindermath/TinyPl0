# Feature-Spezifikation: Secure-Development-Härtung / Feature Specification: Secure Development Hardening

**Feature-Branch / Feature Branch**: `codex/004-secure-development-hardening`

**Erstellt / Created**: 2026-08-30

**Status / Status**: Geklärt; bereit für die dedizierte Checklistenphase / Clarified; ready for the dedicated checklist phase

**Verbindliche Eingabe / Binding Input**: `requirements/intakes/active/Lastenheft_Secure-Development-Hardening.md`

**Feature-Verzeichnis / Feature Directory**: `specs/004-secure-development-hardening`

## Ziel und Nutzen / Goal and Value

TinyPl0 soll einen nachvollziehbaren, risikobasierten Härtungsstand erhalten.
Der Lauf prüft Compiler, Parser, P-Code-Verarbeitung, virtuelle Maschine (VM),
Kommandozeile (CLI), Terminal-IDE, Build, Veröffentlichung, Dokumentation und
Agentenflächen gemeinsam. Positive Aussagen gelten nur mit einem konkreten
Nachweis. Nicht anwendbare Punkte werden begründet und offene Punkte erhalten
eine verantwortliche Rolle, eine Folgeaktion und einen Auslöser für die nächste
Prüfung.

*TinyPl0 shall gain a traceable, risk-based hardening state. The run reviews the
compiler, parser, P-Code processing, virtual machine (VM), command line (CLI),
terminal IDE, build, publishing, documentation, and agent surfaces together.
Positive claims are valid only with concrete evidence. Non-applicable items are
justified, and open items receive an owner role, a follow-up action, and a
re-evaluation trigger.*

Die Spezifikation richtet sich an Auszubildende ab dem ersten Ausbildungsjahr,
Lehrende, Entwicklerinnen und Entwickler, Reviewer sowie KI-Agenten. Lern- und
Governance-Inhalte stehen Deutsch zuerst und Englisch danach, zielen auf CEFR B2
und bleiben text-first. WCAG 2.2 Level AA ist die Prüfbasis, soweit die Kriterien
auf das jeweilige Artefakt anwendbar sind.

*The specification targets apprentices from their first training year,
teachers, developers, reviewers, and AI agents. Learning and governance content
is German-first and English-second at CEFR B2 and remains text-first. WCAG 2.2
Level AA is the review baseline wherever its criteria apply to the artefact.*

## Scope / Scope

Zum Feature gehören:

- eine vollständige, zweiachsige Prüfung der relevanten Punkte aus der
  Secure-Development-Basis: Anwendbarkeit und Umsetzungsstand werden getrennt
  bewertet;
- die Härtung der bestehenden Vertrauensgrenzen für PL/0-Quelltext,
  textuelles P-Code, CLI-Parameter, Dateipfade, Ein-/Ausgabe, VM-Ausführung,
  IDE-Dateidialoge und den lokalen HTTP-Dokumentationsserver;
- deterministische, begrenzte und verständliche Fehler- und Abbruchpfade für
  ungültige, beschädigte, übergroße oder nicht terminierende Eingaben;
- die Prüfung von Golden-Code- und Golden-Output-Parität sowie aller
  historischen PL/0-Eigenheiten;
- projektspezifische Sicherheits-, Architektur-, Lieferketten-,
  Schwachstellen-, A11Y- und Reifegradnachweise;
- die Prüfung von Abhängigkeiten, Build-Integrität, CI, GitHub Pages und
  Release-Automation;
- eine belastbare Entscheidung zu ASVS, SBOM, VEX, SLSA, AI-SBOM, Zero Trust,
  SAMM, CRA, BSI C3A und BSI C5;
- die Wiederherstellung der prüfbaren Konsistenz von Secure-Development-
  Manifest, zwölf Einzelchecklisten und erzeugtem Sammelband;
- TDD-, Test-, Coverage-, Dokumentations-, Statistik- und Abschlussnachweise
  für tatsächlich geänderte Flächen.

*The feature covers a complete two-axis review of the relevant secure-
development checkpoints; hardening of source, P-Code, CLI, file, I/O, VM, IDE,
and local HTTP trust boundaries; deterministic and bounded failures; golden and
historical compatibility; project-specific security, architecture, supply-
chain, vulnerability, accessibility, and maturity evidence; dependency, CI,
Pages, and release review; explicit standards decisions; restoration of the
secure-development baseline's verifiable consistency; and TDD, test, coverage,
documentation, statistics, and closeout evidence for affected surfaces.*

## Nicht-Ziele / Non-Goals

- Keine neue PL/0-Spracherweiterung, kein Optimierungsverfahren und kein neuer
  Backend-Typ.
- Keine Änderung historischer Eigenheiten, Grenzwerte oder Golden-Artefakte,
  außer ein sicherheitsbedingter Unterschied wird ausdrücklich als notwendige
  Härtung begründet und mit Regressionsevidenz freigegeben.
- Keine vollständige Übersetzung aller vorhandenen Dokumente, Kommentare oder
  IDE-Texte. Die geordneten Folge-Intakes zur Quellcode-Dokumentation,
  englischen Dokumentation, IDE-Lokalisierung und IDE-A11Y bleiben eigenständig.
- Keine Umsetzung der agentischen Sandbox-Härtung. Sie gehört zum unmittelbar
  folgenden, derzeit blockierten Intake.
- Keine Einführung von Authentifizierung, Benutzerverwaltung, Cloud-Runtime,
  Telemetrie, personenbezogener Datenverarbeitung oder eigener Kryptografie.
- Keine ISO-27001-Zertifizierung und keine rechtsverbindliche Hersteller- oder
  CRA-Konformitätserklärung. Der Lauf liefert eine projektspezifische,
  überprüfbare Anwendbarkeitsentscheidung.
- Kein Zugriff auf Secrets oder private Geräte-/Kontokonfigurationen.
  Endpunktbezogene Kontrollen werden nur anhand repositoryfähiger Evidenz
  bewertet und ansonsten als Folgepunkt abgegrenzt.
- In der aktuellen Clarify-Phase keine Implementierung, kein Commit, Push,
  Pull Request, Merge, Bypass, Provider-Änderung und kein Start eines weiteren
  Features.
- Keine Änderung am aktiven Intake, am Serienmanifest oder an
  `autonomous-run-state.json`; diese Datei gehört dem Wrapper.

*There is no PL/0 language extension, optimisation, new backend, broad legacy
translation, sandbox implementation, authentication, cloud runtime, telemetry,
personal-data processing, custom cryptography, certification claim, secret or
private device access, implementation in this phase, remote action, intake or
series edit, run-state edit, or additional feature start.*

## Reihenfolge und Abhängigkeiten / Ordering and Dependencies

1. Branch, autonome Run-Identität sowie die vier akzeptierten Artefakt-Hashes
   bilden die unveränderliche Eingangsschranke.
2. Zuerst werden Secure-Development-Baseline, die lokal bestätigten 157
   eindeutigen Checklisten-IDs, vorhandene Evidenz und aktuelle Produktgrenzen
   inventarisiert. Die Zahl ist ein Bestandsmerkmal der zwölf kanonischen
   Checklisten, keine Vorabentscheidung für 157 Produktänderungen.
3. Danach werden Anwendbarkeit, Umsetzungsstand, Risiko, Owner, Reviewer,
   Evidenz, Wiedervorlage und nächste Maßnahme festgehalten.
4. Bedrohungsmodell, Architektur- und Qualitätsentscheidungen gehen jeder
   sicherheitsrelevanten Code-, Workflow- oder Dokumentationsänderung voraus.
5. Produktänderungen folgen TDD Rot → Grün → Regression; danach werden
   Build, Tests, Golden-Parität, Coverage, A11Y und Lieferkette geprüft.
6. Erst wenn jede positive Aussage auf Evidenz zeigt und jeder offene Punkt
   abgegrenzt ist, darf der Härtungslauf abgeschlossen werden.
7. Der nächste Intake bleibt bis zu einem getrennten, autorisierten Lauf
   blockiert.

Die verbindliche Phasenfolge lautet `Specify → Clarify → Checklist → Plan →
Plan Review → Tasks → Analyze → Implement`. Die in Specify erzeugte
Spezifikations-Qualitätscheckliste ist Eingangsevidenz. Nach jeder Clarify-
Änderung muss die dedizierte Checklist-Phase sie vor Plan erneut gegen den
aktuellen Spec-Hash prüfen.

*The accepted identity and hashes form the immutable entry gate. Inventory and
classification precede architecture and threat decisions. Those decisions
precede changes. Product changes follow red, green, and regression evidence,
then full validation. Closeout requires evidence for every positive claim and
a bounded disposition for every open item. The binding phase order is Specify,
Clarify, Checklist, Plan, Plan Review, Tasks, Analyze, and Implement. The
Specify checklist is revalidated after clarification and before planning. The
next intake remains separate.*

## Nutzerszenarien und Tests / User Scenarios and Testing

### User Story 1 - Auditfähigen Sicherheitsstand verstehen / Understand the Audit-Ready Security State (Priorität / Priority: P1)

Als Auszubildende, entwickelnde oder prüfende Person möchte ich jeden relevanten
Sicherheitsprüfpunkt mit Status, Begründung, Evidenz und nächster Aktion sehen,
damit ich den Projektstand ohne verborgenes Sicherheitswissen nachvollziehen
kann.

*As an apprentice, developer, or reviewer, I want every relevant security
checkpoint to show status, rationale, evidence, and the next action so that I
can understand the project state without hidden security knowledge.*

**Warum diese Priorität / Why this priority**: Ohne vollständige Einstufung
können Stubs oder fehlende Nachweise irrtümlich als erfüllte Kontrolle gelten.

**Unabhängiger Test / Independent Test**: Alle 157 im aktuellen Snapshot
bestätigten CL-IDs sowie alle Standard- und Preset-Entscheidungen werden auf
genau einen zulässigen Status, die Pflichtfelder und einen auflösbaren
Evidenzpfad geprüft.

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** ein relevanter Prüfpunkt, **wenn / when** eine prüfende
   Person die Ergebnismatrix liest, **dann / then** findet sie Anwendbarkeit,
   Umsetzung, Begründung, Evidenz, Owner, Reviewer, Restrisiko,
   Wiedervorlage und nächste Maßnahme.
2. **Gegeben / Given** ein nicht anwendbarer Punkt, **wenn / when** er geprüft
   wird, **dann / then** ist er als `N/A` mit `Not Assessed`, Begründung und
   Wiedervorlage sichtbar und nicht still ausgelassen.
3. **Gegeben / Given** eine positive Aussage, **wenn / when** der benannte
   Nachweis fehlt oder nicht zum geprüften Stand gehört, **dann / then** bleibt
   die Aussage offen und darf nicht als erfüllt gelten.

### User Story 2 - Unvertrauenswürdige Programme sicher verarbeiten / Process Untrusted Programs Safely (Priorität / Priority: P1)

Als lernende oder nutzende Person möchte ich fehlerhafte PL/0-, P-Code- und
Dateieingaben ausprobieren können, ohne dass TinyPl0 hängen bleibt, unkontrolliert
Ressourcen verbraucht oder interne Details ausgibt.

*As a learner or user, I want to try malformed PL/0, P-Code, and file input
without TinyPl0 hanging, consuming resources without bounds, or exposing
internal details.*

**Warum diese Priorität / Why this priority**: Parser, Dateiverarbeitung und
VM-Ausführung sind die wichtigsten Vertrauensgrenzen des Produkts.

**Unabhängiger Test / Independent Test**: Ein reproduzierbarer Positiv-,
Grenzwert-, Negativ- und Missbrauchskorpus wird über Compiler, P-Code-Lader,
VM, CLI und IDE ausgeführt. Für die VM gilt ein deterministisches
Instruktionsbudget: Standard `1_000_000` ausgeführte Instruktionen; Tests dürfen
ein kleineres positives Budget setzen und prüfen exakt die Grenze `N`/`N+1`.

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** beschädigtes oder außerhalb der Grenzen liegendes
   P-Code, **wenn / when** es geladen oder ausgeführt wird, **dann / then**
   endet die Verarbeitung kontrolliert mit einer verständlichen Diagnose und
   ohne unkontrollierte Ausnahme.
2. **Gegeben / Given** ein gültiges, aber nicht terminierendes Programm,
   **wenn / when** das dokumentierte Ausführungsbudget erreicht wird,
   **dann / then** bricht die VM deterministisch ab und meldet den Grund.
3. **Gegeben / Given** ein nicht lesbarer oder nicht beschreibbarer Pfad,
   **wenn / when** CLI oder IDE darauf zugreift, **dann / then** bleibt die
   Anwendung bedienbar und zeigt keine Stack-Traces, Verbindungsdaten oder
   unnötigen internen Zustand.

### User Story 3 - Verteilbare Artefakte nachvollziehen / Trace Distributable Artefacts (Priorität / Priority: P1)

Als nutzende oder prüfende Person möchte ich wissen, welche Komponenten in
einem Release stecken, wie es gebaut wurde und wie bekannte Schwachstellen
bewertet werden, damit ich das Artefakt verantwortbar einsetzen kann.

*As a user or reviewer, I want to know which components are in a release, how
it was built, and how known vulnerabilities are assessed so that I can use the
artefact responsibly.*

**Warum diese Priorität / Why this priority**: TinyPl0 ist ein öffentliches
MIT-Projekt mit CI, Pages und Release-Automation, besitzt aber noch keine
ausgefüllte Lieferkettenevidenz.

**Unabhängiger Test / Independent Test**: Ein Kandidaten-Release wird gegen
Komponenteninventar, Schwachstellenstatus, Provenienz, Lizenzprüfung,
verifizierte Quellen und Veröffentlichungsschranken geprüft.

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** ein Release-Kandidat, **wenn / when** die
   Lieferkettenprüfung läuft, **dann / then** besitzt der tatsächlich zur
   Veröffentlichung bestimmte Artefaktsatz ein validiertes, maschinenlesbares
   SBOM und einen nachvollziehbaren Build-Herkunftsnachweis. CI-Testartefakte
   sind kein Release; dieser Lauf muss keinen Release veröffentlichen.
2. **Gegeben / Given** eine bekannte Schwachstelle in einer geprüften oder
   ausgelieferten Komponente, **wenn / when** die Freigabe bewertet wird,
   **dann / then** liegt ein VEX-Status vor und kritische oder hohe offene
   Befunde blockieren die Freigabe.
3. **Gegeben / Given** eine externe Meldung, **wenn / when** eine Person den
   veröffentlichten Sicherheitskontakt nutzt, **dann / then** findet sie eine
   barrierearme, zweisprachige Melde- und Reaktionsbeschreibung.

### User Story 4 - Härtung barrierearm lernen und prüfen / Learn and Review Hardening Accessibly (Priorität / Priority: P2)

Als Auszubildende oder Person mit assistiver Technik möchte ich Sicherheits-
und Fehlersituationen über Tastatur und Text verstehen können, damit Farbe,
Layout oder Maus keine Zugangsvoraussetzung sind.

*As an apprentice or assistive-technology user, I want to understand security
and error situations through keyboard and text so that colour, layout, or a
mouse is not a prerequisite.*

**Warum diese Priorität / Why this priority**: CLI, Terminal-IDE, Markdown und
generiertes HTML sind nutzerseitige Lernflächen und unterliegen dem Prinzip
`Programmierung #include<everyone>`.

**Unabhängiger Test / Independent Test**: Repräsentative CLI-, IDE- und
Dokumentationspfade werden per Tastatur, Textausgabe, semantischer Markdown-
Prüfung und, bei HTML, mit axe sowie `lynx` geprüft.

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** eine neue oder geänderte Diagnose, **wenn / when** sie
   ohne Farbe und visuelle Position gelesen wird, **dann / then** bleiben
   Ursache, Status und nächste Aktion vollständig verständlich.
2. **Gegeben / Given** ein kritischer IDE-Fehlerpfad, **wenn / when** er nur per
   Tastatur bedient wird, **dann / then** kann die nutzende Person ihn erkennen,
   schließen und sicher fortsetzen oder beenden.
3. **Gegeben / Given** geänderte HTML-Dokumentation, **wenn / when** sie geprüft
   wird, **dann / then** bestehen der automatisierte WCAG-2.2-AA-Pfad und die
   textbrowserorientierte Gegenprüfung.

### Grenzfälle / Edge Cases

- Leere Dateien, nur aus Leerraum bestehende Eingaben, gemischte Zeilenenden,
  ungültiges UTF-8, sehr lange Einzelzeilen und Eingaben genau an jeder
  dokumentierten Grenze.
- Numerische Überläufe, negative Ebenen oder Adressen, unbekannte Opcodes,
  ungültige OPR-Untercodes, beschädigte Sprungziele, Stack-Unterlauf,
  Stack-Überlauf, ungültige Basiszeiger und Rekursion ohne Terminierung.
- Ungültige VM-Konfigurationen, etwa ein Stack, der den Startzustand nicht
  aufnehmen kann, dürfen nicht selbst einen unkontrollierten Fehler auslösen.
- Existierende Dateien, symbolische Links, fehlende Verzeichnisse, verweigerte
  Rechte, schreibgeschützte Ziele und Pfade mit ungewöhnlichen Namen.
- Der lokale Dokumentationsserver darf nicht versehentlich an externe
  Schnittstellen binden, außerhalb seines Dokumentwurzelpfads lesen oder
  unbereinigte Dateinamen in Header übernehmen.
- Ein Scan ohne bekannte Schwachstelle erzeugt einen nachvollziehbaren
  „keine bekannte Bewertung nötig“-Stand; er wird nicht still übersprungen.
- Eine regulatorische Bewertung darf aus öffentlicher MIT-Verteilung nicht
  automatisch eine kommerzielle Herstellerrolle ableiten.
- Private Endpunktkontrollen ohne repositoryfähige Evidenz bleiben
  `FollowUp`; sie dürfen nicht erfunden oder als erfüllt behauptet werden.
- Ein bestehender Sicherheitsevidenz-Stub zählt als vorhandener Pfad, aber
  nicht als erfüllter Sicherheitsnachweis.
- Eine Härtung darf die 41 Pflichtfälle, Golden-Artefakte, historischen
  Operator-Aliasse oder Dialektgrenzen nicht unbeabsichtigt verändern.

## Ist-Zustand und nachweisbare Ausgangslage / Current State and Evidenced Baseline

| ID | Beobachtung / Observation | Einstufung / Classification | Evidenz und Bedeutung / Evidence and meaning |
|---|---|---|---|
| BASE-001 | C# 14 auf .NET 10 ist die primäre Laufzeit und steht auf der MSL-Erlaubnisliste. / C# 14 on .NET 10 is the primary runtime and is on the MSL allow-list. | `AlreadySatisfied` | `constitution.md`, `.specify/memory/constitution.md`, Projektdateien. MSL ersetzt keine sichere API-, I/O- oder Dependency-Prüfung. / MSL does not replace secure API, I/O, or dependency review. |
| BASE-002 | Die kanonische Constitution und ihr Memory-Spiegel sind bytegleich. / The canonical constitution and memory mirror are byte-identical. | `AlreadySatisfied` | Read-only comparison on 2026-08-30. |
| BASE-003 | Alle zwölf kanonischen Checklisten und 157 eindeutigen CL-IDs sind vorhanden; der Sammelband enthält dieselbe ID-Menge. / All twelve canonical checklists and 157 unique CL IDs exist; the compendium contains the same ID set. | `AlreadySatisfied` | Read-only-Zählung am 2026-08-30: CL-01..CL-12 = `12/13/15/10/13/11/12/13/17/17/12/12`; Summe und eindeutige Menge jeweils `157`, Mengendifferenz zum Sammelband `0`. / Read-only count and set comparison. |
| BASE-004 | Das Baseline-Manifest nennt ältere Versionen als Richtlinie, Sammelband sowie CL-09 und CL-12; die dokumentierten Generatoren fehlen. / The baseline manifest names older versions than the guideline, compendium, CL-09, and CL-12; the documented generators are missing. | `Applicable` | `baseline-manifest.json` nennt 3.1.0/2.1.0; aktuelle Dokumente nennen 3.2.0/2.2.0. `scripts/build-secure-development-docs.*` ist nicht vorhanden. |
| BASE-005 | `docs/security/` besitzt die vorgeschriebenen Standardpfade, fast alle sind aber ausdrücklich Stubs. / `docs/security/` has the required standard paths, but almost all are explicit stubs. | `Applicable` | `docs/security/README.md` und neun Stub-Dateien. Der Pfad ist erfüllt, die Aussagekraft nicht. / The path exists; the evidence does not. |
| BASE-006 | Compiler, Parser, VM, CLI und IDE besitzen umfangreiche Positiv-, Negativ-, Golden-, Traceability- und L10N-Tests. / Compiler, parser, VM, CLI, and IDE have extensive positive, negative, golden, traceability, and localisation tests. | `AlreadySatisfied` | `tests/Pl0.Tests/`, 41 Pflichtfälle in `tests/data/expected/catalog/cases.json`. |
| BASE-007 | Mehrere Grenzfälle sind bereits abgefangen, aber Ausführungsbudget, ungültige VM-Optionen und vollständige Datei-I/O-Fehlerpfade sind nicht durchgängig belegt. / Several boundaries are already guarded, but execution budgets, invalid VM options, and complete file-I/O failures are not consistently evidenced. | `Applicable` | `VirtualMachine`, `SteppableVirtualMachine`, `PCodeSerializer`, CLI- und IDE-Dateipfade. |
| BASE-008 | Die CLI enthält einen lokalen, nicht authentifizierten HTTP-Server für statische Dokumentation. / The CLI includes a local unauthenticated HTTP server for static documentation. | `Applicable` | `src/Pl0.Cli/Program.cs`, Option `--api`; ASVS 5.0.0 Level 1 gilt für diesen begrenzten Scope. |
| BASE-009 | Das öffentliche MIT-Repository besitzt CI, GitHub Pages und Release-Automation. / The public MIT repository has CI, GitHub Pages, and release automation. | `Applicable` | `.github/workflows/`, `release-please-config.json`, öffentlicher `origin`. SBOM, VEX, SLSA, Scorecard, CRA-Screening und begrenzte C3A/C5-Prüfung greifen. |
| BASE-010 | Secret-Scanning und grundlegende CI-Prüfungen sind vorhanden. / Secret scanning and basic CI checks exist. | `AlreadySatisfied` | Gitleaks, Agent Secret Scan, Build/Test/Coverage workflows. Sie sind Eingangsevidenz, keine vollständige Lieferkettenfreigabe. / They are entry evidence, not a complete supply-chain approval. |
| BASE-011 | Der autonome Lauf und die aktuelle Intake-Review sind aktiv und akzeptiert; der passende Branch existiert. / The autonomous run and current intake review are active and accepted; the matching branch exists. | `AlreadySatisfied` | `autonomous-run-state.json`, `autonomous-run-evidence.md`, Serienreview `Ready`. |
| BASE-012 | Nachfolgende Sandbox-, Dokumentations-, L10N-, A11Y- und Produkt-Intakes sind getrennt und überwiegend blockiert. / Later sandbox, documentation, localisation, accessibility, and product intakes are separate and mostly blocked. | `FollowUp` | `requirements/intakes/series/tinypl0-delivery/manifest.json`; sie werden nicht vorgezogen. / They are not pulled forward. |

## Intake-Abgleich / Intake Reconciliation

Nur `Applicable` erzeugt Anforderungen dieses Features. `AlreadySatisfied`
bleibt belegte Ausgangslage. `N/A` besitzt Begründung und Wiedervorlage.
`Open` wäre anwendbar, aber noch nicht entscheidbar. `FollowUp` bleibt außerhalb
dieses Laufs. Lokal auffindbare Fakten werden ohne Nutzerfrage geklärt; nach
diesem Bericht bleibt keine materielle Planungsunklarheit offen.

*Only `Applicable` creates feature requirements. `AlreadySatisfied` remains
evidenced baseline. `N/A` has rationale and re-evaluation. `Open` would mean
applicable but undecided. `FollowUp` stays outside this run. No material
clarification question remains after this clarification phase.*

| ID | Bindende Intake-Anforderung / Binding intake requirement | Einstufung / Classification | Begründung und Evidenzgrenze / Rationale and evidence boundary |
|---|---|---|---|
| IR-001 | Relevante Checklisten auswählen und Auswahl begründen. / Select and justify relevant checklists. | `Applicable` | Die Auswahl steht in der Checklistenmatrix unten. |
| IR-002 | Alle Prüfpunkte sichtbar klassifizieren. / Classify all checkpoints visibly. | `Applicable` | Die Umsetzung muss alle 157 IDs und zusätzliche Governance-Punkte zweiachsig erfassen. |
| IR-003 | Für anwendbare Punkte konkrete Evidenzpfade nennen. / Name concrete evidence paths for applicable items. | `Applicable` | Standardpfad ist `docs/security/`; Code-, Test-, CI- und Feature-Nachweise dürfen ergänzen. |
| IR-004 | `N/A` kurz begründen. / Briefly justify `N/A`. | `Applicable` | Stille Auslassung ist eine Abnahmeverletzung. |
| IR-005 | Offene Punkte mit Risiko, Aktion und Priorität festhalten. / Record open items with risk, action, and priority. | `Applicable` | Zusätzlich sind Owner, Reviewer, Restrisiko, Termin oder Trigger und erwartete Evidenz erforderlich. |
| IR-006 | Secure Coding und Secure Architecture gemeinsam bewerten. / Assess secure coding and secure architecture together. | `Applicable` | NIST SSDF, CWE Top 25, STRIDE/CAPEC, arc42 und S-ADR bilden den gemeinsamen Prüfpfad. |
| IR-007 | CLI-, Parser-, VM- und Dateigrenzen als Trust Boundaries behandeln. / Treat CLI, parser, VM, and file boundaries as trust boundaries. | `Applicable` | Der aktuelle Code besitzt diese Eingänge und braucht vollständige Bedrohungs- und Testevidenz. |
| IR-008 | A11Y und didaktische Kommentar-Governance prüfen. / Review accessibility and didactic-comment governance. | `Applicable` | Nutzerflächen und geänderte nicht-triviale Logik sind betroffen. |
| IR-009 | Lieferketten-, AI-SBOM-, C3A/C5- und regulatorische Punkte fachlich entscheiden. / Decide supply-chain, AI-SBOM, C3A/C5, and regulatory points by project relevance. | `Applicable` | Einzelentscheidungen stehen in der Standardsmatrix; AI-SBOM ist dort begründet `N/A`. |
| IR-010 | Ergebnis, offene Risiken, akzeptierte Restrisiken und Folgen auditfähig zusammenfassen. / Summarise results, open risks, accepted residual risks, and follow-ups auditably. | `Applicable` | Abschlussnotiz und ausgefüllte Evidenzmatrix sind Pflicht. |
| IR-011 | Spec, Plan und Tasks als getrennte Spec-Kit-Artefakte erzeugen. / Create separate Spec Kit spec, plan, and tasks artefacts. | `Applicable` | Specify erzeugte Spec und erste Qualitätscheckliste; Clarify erzeugt Bericht und minimale Spec-Korrekturen; die dedizierte Checklist-Phase revalidiert danach `checklists/requirements.md`. Plan und Tasks folgen getrennt. |
| IR-012 | Projektspezifische Sicherheitsnachweise aktualisieren. / Update project-specific security evidence. | `Applicable` | Stubs dürfen nicht als Erfüllung gelten. |
| IR-013 | Tests und CI nur aus konkreten Härtungsbefunden ändern. / Change tests and CI only from concrete hardening findings. | `Applicable` | Änderungen brauchen Risiko- und Anforderungsbezug; pauschale Tool-Aufrüstung ist ausgeschlossen. |
| IR-014 | TinyPl0 bleibt baubar und testbar. / TinyPl0 remains buildable and testable. | `Applicable` | Vollständige Regression und Golden-Parität sind Abschlussgates. |
| IR-015 | Das Intake selbst löst keine Umsetzung aus. / The intake itself does not perform implementation. | `AlreadySatisfied` | Der aktuelle autonome Lauf besitzt getrennte, explizite Autorität und Feature-Identität. |
| IR-016 | Spätere Intakes nicht kombinieren oder starten. / Do not combine or start later intakes. | `FollowUp` | Die Serienreihenfolge bleibt unverändert; dieser Lauf dokumentiert nur Übergabegrenzen. |

## Auswahl der Secure-Development-Checklisten / Secure Development Checklist Selection

| Checkliste / Checklist | Einstufung / Classification | Scope-Entscheidung / Scope decision |
|---|---|---|
| CL-01 Standards-Anwendbarkeit | `Applicable` | NIST SSDF und CWE Top 25 gelten immer; alle bedingten Standards werden explizit entschieden. |
| CL-02 Sichere Softwarearchitektur | `Applicable` | Bestehende Trust Boundaries, lokaler HTTP-Fluss, Fehlerpfade, Lieferkette und Provider-Abhängigkeiten werden geprüft. |
| CL-03 Krypto-Mindestvorgaben | `N/A` | TinyPl0 führt keine Produktkryptografie, Schlüssel, Passwörter oder TLS-Verbindung ein. Wiedervorlage bei Krypto-, Auth- oder Secret-Scope. |
| CL-04 Bedrohungsmodellierung | `Applicable` | STRIDE/CIA und CAPEC gelten für Parser-, P-Code-, VM-, Datei-, CLI-, IDE- und HTTP-Grenzen. |
| CL-05 Lieferkette und Build-Integrität | `Applicable` | Öffentliche Releases, NuGet-Abhängigkeiten, Actions, Pages und fehlende SBOM-/VEX-/Provenienz-Evidenz liegen im Scope. |
| CL-06 Schwachstellenoffenlegung | `Applicable` | Öffentliches, releasefähiges OSS benötigt einen auffindbaren CVD-Pfad und nachvollziehbare Reaktion. |
| CL-07 CRA-Anwendbarkeit | `Applicable` | Öffentliche digitale Artefakte und Release-Automation lösen eine dokumentierte CRA-Rollen- und Scope-Prüfung aus; formale Pflichten werden nicht vorweggenommen. |
| CL-08 Sicherheits-Code-Review | `Applicable` | C#/.NET, Datei-/HTTP-I/O, Eingabevalidierung, Fehler, Abhängigkeiten und Tests sind Kernscope. |
| CL-09 KI-Codeerzeugung | `Applicable` | KI ist Entwicklungswerkzeug; Human Review, Paket-, Lizenz-, Test-, Datenschutz- und Auditregeln gelten. AI-SBOM bleibt separat `N/A`. |
| CL-10 Sichere Entwicklungsumgebung | `Applicable` | Repositoryfähige CI-, Branch-, Secret-, Build-, Backup- und Cross-Platform-Kontrollen gelten. Private Geräte- und Kontokontrollen bleiben `FollowUp`, wenn keine sichere Evidenz vorliegt. |
| CL-11 Datenschutz-Folgenabschätzung | `N/A` | Produkt und Härtung verarbeiten keine personenbezogenen Daten und führen keine Telemetrie ein. Wiedervorlage bei neuen Datenkategorien, Logs, Konten oder externen Empfängern. |
| CL-12 Agentische KI-Sandbox | `FollowUp` | Die Laufgrenzen des aktuellen autonomen Runs sind `AlreadySatisfied`; die eigentliche Sandbox-Härtung gehört zum nächsten bindenden Intake und wird nicht vorgezogen. |

Die zwölf IDs aus CL-12 bleiben aus Vollständigkeitsgründen in der 157-ID-
Prüfinstanz. Für dieses Feature erhalten sie `N/A`/`Not Assessed` mit Verweis
auf den blockierten Folge-Intake; sie erzeugen keine Sandbox-, Mount-, Host-,
Netzwerk- oder Agentenproduktarbeit.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Der Lauf MUSS für alle 157 im aktuellen Repository-Snapshot
  nachgewiesenen CL-IDs genau eine
  Anwendbarkeit (`Applicable`, `N/A` oder `Open`) und genau einen
  Umsetzungsstand (`Fulfilled`, `Partly Fulfilled`, `Not Fulfilled` oder
  `Not Assessed`) erfassen. Jede Zeile MUSS genau diese zehn Pflichtspalten
  besitzen: CL-ID, Anwendbarkeit, Umsetzungsstand, Begründung, Evidenzpfad,
  Owner, Reviewer, Restrisiko, Wiedervorlage und nächste Maßnahme. Bei `Open`,
  `Partly Fulfilled`, `Not Fulfilled` oder `Not Assessed` sind zusätzlich
  Priorität, Zieltermin oder auslösender Termin und erwartete Evidenz Pflicht.
  Die zwölf CL-12-IDs sind in diesem Feature `N/A`/`Not Assessed` und verweisen
  ausschließlich auf den getrennten Sandbox-Folge-Intake.

- **FR-002**: Die vollständige projektspezifische Prüfinstanz MUSS unter
  `docs/security/secure-development/2026-08-30-tinypl0-hardening/` liegen.
  Wiederverwendbare Vorlagen unter `docs/secure-development/` DÜRFEN nicht mit
  projektspezifischen Ergebnissen überschrieben werden.

- **FR-003**: `docs/security/threat-model.md` und
  `docs/security/arc42-security.md` MÜSSEN die Werte, Datenflüsse und
  Vertrauensgrenzen für PL/0-Quelltext, P-Code, VM, CLI, Datei-I/O, IDE und
  lokalen HTTP-Dokumentationsserver abbilden. STRIDE ist die Basis; relevante
  risikoreiche Wege erhalten CAPEC-Referenzen und bewertete Restrisiken.

- **FR-004**: Architektonisch bedeutsame Sicherheitsentscheidungen MÜSSEN als
  einzelne S-ADRs unter `docs/security/adr/` dokumentiert werden. Allgemeine
  Kontext-, Runtime-, Deployment-, Qualitäts- oder Schuldenänderungen MÜSSEN
  außerdem im passenden Pfad unter `docs/architecture/` nachvollziehbar sein.

- **FR-005**: Compiler und Parser MÜSSEN jede dokumentierte gültige Grenze
  akzeptieren und übergroße, ungültige oder unvollständige Eingaben
  deterministisch als Diagnosen ablehnen. Kompilierungsfehler DÜRFEN nicht als
  unkontrollierte Ausnahmen an Nutzerinnen oder Nutzer gelangen.

- **FR-006**: P-Code-Deserialisierung und beide VM-Ausführungswege MÜSSEN
  ungültige Opcodes, Untercodes, Ebenen, Adressen, Sprungziele, Stackzugriffe,
  Konfigurationen und Ressourcenbudgets kontrolliert behandeln. Gültige, aber
  nicht terminierende Programme MÜSSEN vor Ausführung der Instruktion `N+1`
  mit einer stabilen Diagnose abbrechen. `N` ist ein positives,
  konfigurierbares Instruktionsbudget; der Produktstandard ist `1_000_000`.
  Nichtpositive Budgets und ungültige Stackgrößen werden vor Allokation oder
  Ausführung diagnostiziert. Normale und schrittweise VM zählen gleich.

- **FR-007**: CLI- und IDE-Dateioperationen MÜSSEN fehlende, unlesbare,
  nicht beschreibbare, ungeeignete oder widersprüchliche Pfade fail-safe
  behandeln. Nutzerseitige Meldungen MÜSSEN stabilen Exit-/Fehlerstatus und
  nächste Aktion nennen und DÜRFEN keine Stack-Traces, Secrets oder unnötigen
  internen Zustand offenlegen.

- **FR-008**: Der lokale HTTP-Dokumentationsserver MUSS gegen den vollständigen
  OWASP-ASVS-5.0.0-Level-1-Katalog gemappt werden. Jede L1-ID erhält
  `Applicable` oder ein begründetes `N/A`; `Applicable` gilt nur für Kontrollen,
  die durch `pl0c --api`, seine Start-/Konfigurationspfade oder die statische
  Dateiauslieferung berührt werden. Authentifizierung, Sitzungen,
  Mehrbenutzer-Autorisierung und fachliche Datenänderungen bleiben für diesen
  Server begründet `N/A`. Alle anwendbaren IDs MÜSSEN erfüllt sein. Der Server
  MUSS standardmäßig nur lokal erreichbar sein, ausschließlich beabsichtigte
  Dokumente ausliefern, sichere Fehlerantworten verwenden und aus Dateinamen
  oder Pfaden keine unsicheren Header erzeugen. GitHub Pages ist eine getrennte
  Veröffentlichungsfläche und nicht Teil dieses ASVS-Produktscopes.

- **FR-009**: Geänderte IDE-Fehlerpfade MÜSSEN per Tastatur bedienbar bleiben;
  Fokus, Status, Ursache und sichere Folgeaktion DÜRFEN nicht nur über Farbe,
  Layout oder Mausinteraktion vermittelt werden.

- **FR-010**: Härtungen DÜRFEN PL/0-Semantik, Dialekte, historische Eigenheiten,
  acht Opcodes, 41 Pflichtfälle und Golden-Ausgaben nicht unbeabsichtigt
  verändern. Jede beabsichtigte sicherheitsbedingte Abweichung braucht eine
  ausdrücklich akzeptierte Anforderung, Risikoentscheidung und aktualisierte
  Traceability-Evidenz.

- **FR-011**: `docs/security/security-checklist.md`,
  `dependency-audit.md` und `security-quality-scenarios.md` MÜSSEN mit
  TinyPl0-spezifischen Inhalten befüllt werden. Sicherheitsrelevante Szenarien
  MÜSSEN Quelle, Auslöser, Umgebung, Reaktion und messbares Ergebnis enthalten.

- **FR-012**: Für jeden nach Abschluss dieses Features tatsächlich
  veröffentlichten Artefaktsatz MUSS der Release-/Pages-Pfad ein validiertes,
  maschinenlesbares SBOM erzeugen und zuordnen. Der Feature-Lauf MUSS den lokal
  und in CI reproduzierbaren Erzeugungs-, Validierungs- und
  Veröffentlichungsanschlusspfad nachweisen, aber keinen Release auslösen und
  keine historischen Releases nachträglich ändern. CI-Test- und Coverage-
  Uploads sind keine veröffentlichten Artefaktsätze. `docs/security/supply-chain-evidence.md` MUSS Abhängigkeiten,
  Lizenzen, Quellen, Lock-Status, CVE-Prüfung, SBOM, VEX, Build-Provenienz,
  SLSA-Ziel und OpenSSF-Scorecard-Entscheidung zusammenführen.

- **FR-013**: Bekannte Schwachstellen in ausgelieferten oder bewerteten
  Komponenten MÜSSEN einen VEX-Status `affected`, `not affected`, `mitigated` oder
  `under investigation` erhalten. Offene kritische oder hohe Befunde MÜSSEN
  Release und Abschluss blockieren. Eine Risikoakzeptanz darf nur die
  Repository-Maintainer-Rolle schriftlich, befristet und mit kompensierender
  Kontrolle erteilen; der ausführende Agent darf sie weder erfinden noch selbst
  genehmigen.

- **FR-014**: Das öffentliche Repository MUSS einen auffindbaren,
  barrierearmen, DE-zuerst/EN-danach formulierten Prozess für koordinierte
  Schwachstellenmeldung, Triage, Reaktionsziele, Advisories und Lessons Learned
  besitzen. Eine veröffentlichte `security.txt`-Darstellung MUSS mit diesem
  Prozess übereinstimmen, soweit die öffentliche Dokumentationsfläche sie
  ausliefern kann.

- **FR-015**: `docs/security/cra-applicability.md` und
  `docs/security/regulatory-applicability.md` MÜSSEN die öffentliche
  OSS-Verteilung, mögliche kommerzielle Tätigkeit, Hersteller-/Steward-Rolle,
  Produkttyp, Zeitpunkte und Folgepflichten prüfen. NIS2, EU AI Act und DORA
  MÜSSEN getrennte Entscheidungen erhalten.

- **FR-016**: `docs/security/cloud-autonomy-applicability.md` und
  `docs/security/cloud-compliance-assurance.md` MÜSSEN BSI C3A und BSI C5 für
  GitHub Actions, Pages, Release- und Artefakt-Hosting im Entwicklungs- und
  Veröffentlichungsprozess bewerten. Der lokale TinyPl0-Produktbetrieb bleibt
  ausdrücklich außerhalb eines Cloud-Runtime-Claims.

- **FR-017**: `docs/security/samm-assessment.md` MUSS einen aktuellen,
  priorisierten OWASP-SAMM-Stand mit Ownern, Zielterminen und erwarteter
  Evidenz enthalten. Nur in diesem Lauf `Applicable` eingestufte Verbesserungen
  dürfen als Umsetzung geplant werden.

- **FR-018**: Das Secure-Development-Baseline-Manifest MUSS mit Richtlinie,
  zwölf Einzelchecklisten und Sammelband übereinstimmen. Die 157 IDs und ihre
  Reihenfolge MÜSSEN durch die dokumentierten Bash- und PowerShell-Prüfwege
  reproduzierbar validiert werden; der Sammelband darf nicht direkt bearbeitet
  werden.

- **FR-019**: Falls der fehlende Baseline-Generator in diesem Lauf ergänzt
  wird, MÜSSEN `scripts/build-secure-development-docs.sh` und
  `scripts/build-secure-development-docs.ps1` funktionsgleich sein. Die
  PowerShell-Fläche MUSS den freigegebenen Cmdlet-Namen
  `Build-SecureDevelopmentDocs`, DE-zuerst/EN-danach-Hilfe und `-WhatIf`
  anbieten; Bash MUSS `--dry-run` und eine Manpage unter
  `docs/man/build-secure-development-docs.1.md` anbieten. Beide MÜSSEN einen
  read-only Check-Modus besitzen.

- **FR-020**: KI-Nutzung in diesem Lauf MUSS als Entwicklungswerkzeug
  dokumentiert werden. Menschliches Review, Vier-Augen-Prüfung kritischer
  Logik, Paket-/CVE-/Lizenzprüfung, Datenschutz in Prompts, Tests und Auditspur
  gelten. AI-SBOM MUSS als `N/A` mit Wiedervorlage dokumentiert werden, solange
  kein KI-Modell, Datensatz, Inferenzdienst oder KI-Runtime-Bestandteil
  ausgeliefert oder betrieben wird.

- **FR-021**: Neue oder geänderte Produktlogik MUSS beobachtbare TDD-Evidenz
  für Rot, Grün und Regression besitzen. KI-erzeugte sicherheitsrelevante
  Logik MUSS mindestens 80 % Zeilen- und 80 % Branch-Coverage erreichen;
  sonstige sicherheitskritische Module MÜSSEN mindestens 85 % Branch-Coverage
  erreichen. Für das Gesamtprojekt ist der zuletzt belegte Stand `70,23 %`.
  Abschlussgate sind mindestens `70 %` Zeilen-Coverage und keine Absenkung
  gegenüber diesem belegten Ausgangswert; `80 %` bleibt das verbindlich
  dokumentierte Ziel. Eine verbleibende Lücke zu 80 % wird als `Open` mit
  Owner, Zieltermin/Trigger und Evidenzziel geführt und erzwingt keine
  fachfremde Testausweitung in diesem Feature.

- **FR-022**: Geänderte nutzer- oder lernendenseitige Inhalte MÜSSEN
  Deutsch zuerst und Englisch danach auf CEFR B2 liefern, Fachbegriffe beim
  ersten Auftreten erklären und text-first bleiben. Relevante A11Y-Evidenz
  MUSS unter `docs/accessibility/secure-development-hardening.md` oder einem
  dort verlinkten gleichwertigen Pfad liegen.

- **FR-023**: Geänderte öffentliche APIs MÜSSEN vollständige anwendbare
  XML-Dokumentation erhalten. Änderungen an API-Signaturen oder XML-Kommentaren
  MÜSSEN im selben Arbeitsgegenstand DocFX sowie den vorhandenen
  Playwright/axe- und `lynx`-orientierten A11Y-Pfad erfolgreich durchlaufen.

- **FR-024**: Gemeinsame Governance- oder Agentenregeln MÜSSEN atomar in
  `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`,
  `.github/agents/copilot-instructions.md`, betroffenen Constitution-
  Spiegeln und Templates geprüft und bei Bedarf aktualisiert werden.
  Absichtliche Abweichungen MÜSSEN im selben Nachweis begründet werden.

- **FR-025**: Die installierten Governance-Presets MÜSSEN in wirksamer
  Prioritätsreihenfolge geprüft werden. Intake-Review, Sequencing, Model Routing
  und der aktuelle Autonomous Run gelten als Eingangsevidenz; Parallel-
  Autonomous ist `N/A`, weil keine Kampagne gestartet wird.

- **FR-026**: Der Abschluss MUSS eine text-first Ergebnisübersicht mit
  erfüllten Kontrollen, offenen Risiken, befristet akzeptierten Restrisiken,
  Folgepunkten und exakten Evidenzpfaden enthalten. `docs/project-statistics.md`
  MUSS nach der agentengetriebenen Repository-Änderung nach Profil 2
  fortgeschrieben werden.

- **FR-027**: Dieses Feature DARF keine als `AlreadySatisfied`, `N/A`, `Open`
  oder `FollowUp` klassifizierte Position als Implementierungsarbeit behandeln,
  keinen Folge-Intake starten und keine aktive Intake- oder Serienartefaktdatei
  ändern.

- **FR-028**: Verbindliche Arbeit dieses Features ist zuerst die Prüfung und
  projektspezifische Evidenz. Vorab als Produktlogik begründet sind nur das
  Instruktionsbudget und die fail-safe Validierung der VM-Konfiguration aus
  FR-006. Weitere Code-, UI-, CI- oder Workflow-Änderungen sind nur zulässig,
  wenn die 157-ID-Prüfung einen `Applicable`-Punkt mit `Partly Fulfilled` oder
  `Not Fulfilled`, Risiko, Test und kleinstmöglicher Maßnahme belegt. Eine
  Standardsentscheidung allein autorisiert keine Produktänderung; Sandbox-
  Arbeit bleibt stets ausgeschlossen.

### Constitution-Anforderungen / Constitution Requirements

- **CR-001**: Der Level-2-Registry-Eintrag für `RiderProjects/TinyPl0` ist
  verbindlicher Kontext: .NET 10/C# 14, `dotnet restore/build/test`, xUnit,
  Coverage, Golden-Artefakte, DocFX/A11Y, Statistikbasen 80/125 und die
  gepflegten Agentenflächen.
- **CR-002**: Alle nutzerseitigen Artefakte benennen ihren A11Y-Prüfpfad;
  WCAG 2.2 Level AA gilt, soweit anwendbar, und jede wesentliche Information
  bleibt textuell verfügbar.
- **CR-003**: Lern- und Governance-Inhalte stehen DE zuerst, EN danach auf
  CEFR B2, erklären Fachbegriffe und setzen keine Spec-Kit-Erfahrung voraus.
- **CR-004**: Statistik und Agentenflächen sind `UpdateRequired`, wenn die
  Umsetzung sie berührt; FR-024 und FR-026 definieren die Parität.
- **CR-005**: C# 14/.NET 10 ist speichersicher und ohne Hardwarezwang gewählt.
  Sichere .NET-Regeln bleiben für Eingaben, I/O, HTTP, Fehler und Dependencies
  anwendbar.
- **CR-006**: NIST SSDF und CWE Top 25 sind immer `Applicable`. Alle weiteren
  Standards besitzen unten eine ausdrückliche Entscheidung.
- **CR-007**: OWASP ASVS 5.0.0 Level 1 ist für den lokalen statischen HTTP-
  Dokumentationsserver `Applicable`; der vollständige L1-Katalog wird auf
  exakte IDs gemappt. Nicht berührte Authentifizierungs-, Sitzungs-,
  Mehrbenutzer- und Datenänderungskontrollen werden einzeln begründet `N/A`.
- **CR-008**: SBOM und SLSA sind wegen releasefähiger, CI-erstellter Artefakte
  `Applicable`; VEX gilt für bekannte Schwachstellen in geprüften oder
  ausgelieferten Komponenten.
- **CR-009**: KI ist nur Entwicklungswerkzeug. AI-SBOM ist `N/A` mit
  Wiedervorlage bei KI-Runtime-/Produktbestandteilen.
- **CR-010**: CAPEC ist für risikoreiche Pfade `Applicable`; Zero Trust ist
  wegen des lokalen, nicht verteilten Produkts `N/A` und wird bei Remote-,
  Cloud-Runtime-, Identitäts- oder Mehrgeräte-Scope neu bewertet.
- **CR-011**: Standard-Evidenz liegt unter `docs/security/`; Architektur- und
  A11Y-Nachweise ergänzen unter `docs/architecture/` und
  `docs/accessibility/`. Abweichungen müssen im jeweiligen Index verlinkt sein.
- **CR-012**: Alle aktiven Presets werden berücksichtigt. Für die bindende
  Standardmatrix gelten Security, Architecture, iSAQB, A11Y, Cross-Platform,
  Agent Parity, Autonomous und Parallel Autonomous; zusätzliche aktive Intake-
  und Routing-Presets werden als Eingangsgates behandelt.
- **CR-013**: Es gibt genau eine Dokumentationsauswirkungsentscheidung:
  `UpdateRequired`, vollständig beschrieben im entsprechenden Abschnitt.

## Standards-, Architektur- und Governance-Anwendbarkeit / Standards, Architecture, and Governance Applicability

`Not Fulfilled` oder `Not Assessed` beschreibt hier die belegte Ausgangslage,
nicht den Erfolg von Specify oder Clarify. Evidenzerzeuger ist die ausführende
Autor- oder Agentenrolle. Owner ist die für Maßnahme und Pflege verantwortliche
Repository-Rolle. Reviewer ist eine vom Evidenzerzeuger getrennte prüfende
Rolle. CI-/Toolnachweise müssen an Commit und Konfiguration gebunden sein.
Rechtliche, providerbezogene oder kritische/hohe Risikoakzeptanz gehört
ausschließlich der Repository-Maintainer-Rolle; fehlt diese Entscheidung,
bleibt der Punkt offen und die betroffene Freigabe blockiert. Für `Open` muss
der Projektnachweis zusätzlich Priorität, Zieltermin oder Trigger und erwartete
Evidenz nennen.

*`Not Fulfilled` or `Not Assessed` describes the evidenced baseline, not the
success of the specify phase. The owner is the implementing role and the
reviewer is an independent reviewer. Later `Open` evidence also records
priority, target date, and expected evidence.*

| Prüfpunkt / Checkpoint | Anwendbarkeit / Applicability | Umsetzung / Implementation | Entscheidung, Evidenz und Restrisiko / Decision, evidence, and residual risk | Wiedervorlage / Re-evaluation |
|---|---|---|---|---|
| NIST SSDF SP 800-218 | `Applicable` | `Partly Fulfilled` | Für Level 2 immer Pflicht; CI, Tests, Secret-Scans und diese Spec decken Teile ab. Vollständige Prepare/Protect/Produce/Respond-Evidenz fehlt. | In Plan, Tasks, Implementierung, Release und Abschluss. |
| CWE Top 25 | `Applicable` | `Partly Fulfilled` | Bestehende Grenzwerttests decken Teile ab; vollständige Zuordnung von Eingabe-, Pfad-, Ressourcen- und Fehlerbefunden fehlt. | Bei jedem Sicherheitsreview und jeder Codeänderung. |
| OWASP ASVS 5.0.0 Level 1 | `Applicable` | `Not Fulfilled` | Vollständiges L1-ID-Mapping für den durch `pl0c --api` erreichbaren statischen Loopback-Server; keine ausgefüllte ASVS-Matrix. Nicht berührte L1-IDs werden nicht still ausgelassen, sondern begründet `N/A`. | Bei jeder `--api`-, HTTP-, Docs-Server- oder Bindungsänderung. |
| SBOM | `Applicable` | `Not Fulfilled` | Veröffentlichbare Release-/Pages-Artefaktsätze, aber kein maschinenlesbarer Release-Nachweis. Dieser Lauf belegt Erzeugung und Anschluss, veröffentlicht selbst nichts. | Für jeden tatsächlichen Release-Kandidaten und Dependency-Wechsel. |
| VEX | `Applicable` | `Not Fulfilled` | VEX-Prozess für bekannte Funde fehlt; ein VEX-Datensatz entsteht nur bei einem Fund. | Bei jedem CVE-Fund und Release-Review. |
| SLSA v1.2 | `Applicable` | `Partly Fulfilled` | Automatisierte GitHub-Builds existieren; Provenienz und erklärtes Zielniveau fehlen. Ziel mindestens Build L1, öffentlich konsumierte Artefakte langfristig L2. | Bei Pipeline-, Runner-, Action- oder Releaseänderung. |
| AI-SBOM | `N/A` | `Not Assessed` | KI wird nur als Entwicklungswerkzeug genutzt; keine KI-Komponente wird ausgeliefert oder betrieben. | Bei Modell, Datensatz, Inferenzdienst oder KI-Runtime im Produkt. |
| OpenSSF Scorecard | `Applicable` | `Not Assessed` | Öffentliches OSS-Repository und externe Actions/Packages. | Vor Release und Aufnahme einer hochwirksamen Dependency. |
| OWASP SAMM | `Applicable` | `Not Fulfilled` | Langlebiges Level-2-Projekt; vorhandene Datei ist Stub. | Mindestens jährlich und nach wesentlichem Security-Lauf. |
| CRA | `Applicable` | `Not Assessed` | Öffentliche digitale Artefakte und Releases verlangen eine dokumentierte Rollen-/Scope-Prüfung. Kommerzielle Tätigkeit wird nicht angenommen. | Bei Vertriebs-, Monetarisierungs-, Hersteller-, Steward- oder Übergabemodelländerung. |
| NIS2 | `N/A` | `Not Assessed` | Kein Nachweis einer wesentlichen/wichtigen Einrichtung oder regulierten Lieferbeziehung im Feature-Scope. | Bei Betreiber-, Kunden- oder Lieferkettenänderung. |
| EU AI Act | `N/A` | `Not Assessed` | Keine KI-Runtime und kein KI-Produkt. | Bei Produkt-KI oder Modellbereitstellung. |
| DORA | `N/A` | `Not Assessed` | Kein Finanzunternehmen und keine ICT-Dienstleistung für einen regulierten Finanz-Scope belegt. | Bei entsprechendem Kunden-/Betriebsmodell. |
| C#/.NET MSL und Secure Coding | `Applicable` | `Partly Fulfilled` | Speichersicher; unvollständige Evidenz für Datei-, HTTP-, Ressourcen- und Ausnahmegrenzen. | Bei jeder geänderten Logik und jedem Review. |
| STRIDE/CIA und CAPEC | `Applicable` | `Not Fulfilled` | Mehrere reale Trust Boundaries, aber Threat Model ist Stub. | Bei jeder Grenz-, Datenfluss- oder Deploymentänderung. |
| S-ADR und arc42 Security | `Applicable` | `Not Fulfilled` | Sicherheitsarchitektur ist materieller Scope; vorhandene arc42-Datei ist Stub. | Bei jeder bedeutsamen Entscheidung oder akzeptiertem Trade-off. |
| iSAQB/arc42 allgemeine Architektur | `Applicable` | `Partly Fulfilled` | `docs/ARCHITECTURE.md` existiert; Härtung kann Runtime, Qualitätsmerkmale und technische Schulden ändern. | Wenn ein Befund Struktur, Interface, Runtime oder Deployment verändert. |
| NIST Zero Trust SP 800-207 | `N/A` | `Not Assessed` | TinyPl0 läuft lokal; der Docs-Server bindet lokal und besitzt keine föderierte Identität oder Remote-Verwaltung. | Bei verteiltem, Cloud-Runtime-, Remote-, Mehrgeräte- oder Identitätsscope. |
| BSI C3A | `Applicable` | `Not Assessed` | GitHub Actions, Pages und Artefakt-/Release-Hosting sind Provider-Abhängigkeiten im Lieferprozess, nicht im lokalen Produktruntime. | Bei Provider-, Hosting-, Exit- oder Portabilitätsänderung. |
| BSI C5 | `Applicable` | `Not Assessed` | Cloud-Assurance, Shared Responsibility und Betriebsnachweise der genannten GitHub-Flächen sind zu bewerten. | Bei Provider-/Assurance- oder Betriebsmodelländerung. |
| Produktkryptografie / Product cryptography | `N/A` | `Not Assessed` | Keine Schlüssel, Passwörter, Signatur- oder Verschlüsselungsfunktion im Produkt-Scope. | Bei Krypto-, Auth- oder Secret-Scope. |
| DPIA / DSGVO Art. 35 | `N/A` | `Not Assessed` | Keine personenbezogene Produktverarbeitung oder Telemetrie. | Bei personenbezogenen Daten, Konten, Logs, Empfängern oder Profiling. |
| WCAG 2.2 AA und text-first | `Applicable` | `Partly Fulfilled` | Bestehende A11Y-Regeln und Docs-Pipeline; Security- und Fehlerpfade noch nicht vollständig belegt. | Bei jeder Nutzer-, IDE-, CLI-, HTML- oder Templateänderung. |
| DE zuerst, EN danach, CEFR B2 | `Applicable` | `Partly Fulfilled` | Governance fordert es; vorhandene Laufzeittexte sind nicht pauschal Teil dieser Härtung. Geänderte Flächen müssen es erfüllen. | Bei jeder geänderten Nutzer- oder Lernfläche. |
| Didaktische Inline-Kommentare | `Applicable` | `Not Assessed` | Spätere nicht-triviale Härtungslogik braucht einen Warum-Kommentar-Review; Specify und Clarify ändern keinen Code. | Bei jeder nicht-trivialen Logikänderung. |
| Cross-Platform-Skriptparität | `Applicable` | `Not Fulfilled` | README fordert `build-secure-development-docs.sh/.ps1`; beide fehlen. FR-019 bindet Paar, Hilfe, Manpage und Dry-run/WhatIf. | Bei jeder Script- oder Cmdlet-Änderung. |
| Agenten- und Template-Parität | `Applicable` | `Partly Fulfilled` | Fünf gepflegte Agentenflächen sind vorhanden; Änderungen müssen semantisch synchron bleiben. | Bei gemeinsamer Guidance-, Template- oder Routingregel. |
| Autonomous Run | `Applicable` | `Partly Fulfilled` | Aktiver, akzeptierter Lauf in Clarify mit enger Phasenautorität; spätere Gates bleiben offen. | An jeder Phasengrenze und nach Unterbrechung. |
| Parallel Autonomous | `N/A` | `Not Assessed` | Keine Kampagne, Worker-Delegation oder Konsolidierung wird gestartet. | Nur bei ausdrücklich autorisierter Kampagne. |

## Aktive Presets und Status / Active Presets and Status

| Preset | Einstufung / Classification | Entscheidung / Decision |
|---|---|---|
| `security-governance` 0.6.2 | `Applicable` | Standards, MSL, Secure Coding, Lieferkette und Regulierung bestimmen Anforderungen und Evidenz. |
| `architecture-governance` 0.5.2 | `Applicable` | Threat Model, S-ADR, arc42 Security, SAMM, C3A und C5 greifen. |
| `isaqb-architecture-governance` 0.2.2 | `Applicable` | Qualitätsmerkmale, Runtime-/Deployment-Sicht, Risiken und ADR-Gate greifen bei materiellen Änderungen. |
| `a11y-governance` 0.4.3 | `Applicable` | CLI, IDE, Markdown, HTML und Kommentare sind Nutzer-/Lernflächen. |
| `cross-platform-governance` 0.2.2 | `Applicable` | Der dokumentierte Baseline-Generator fehlt und braucht die gebundene Paar-/Hilfestruktur. |
| `agent-parity-governance` 0.4.2 | `Applicable` | Gemeinsame Regeln werden auf allen fünf gepflegten Flächen und betroffenen Templates geprüft. |
| `model-routing-governance` 0.1.4 | `AlreadySatisfied` | Phase ist providerneutral einer validierten lokalen Rolle und einem Runner-Profil zugeordnet; Produktanforderungen nennen kein Modell. |
| `intake-authoring-governance` 0.3.1 | `AlreadySatisfied` | Bindender Intake ist akzeptiert; er wird nicht editiert oder neu verfasst. |
| `intake-review-governance` 0.2.1 | `AlreadySatisfied` | Serienreview ist `Ready` und hashgebunden. |
| `intake-sequencing-governance` 0.2.3 | `AlreadySatisfied` | `004` ist `Eligible`; nachfolgende Ziele bleiben getrennt. |
| `autonomous-run-governance` 0.4.1 | `Applicable` | Run-Zustand, Phasenergebnis, Scope, Stopp/Resume und Evidenzgrenzen gelten. |
| `parallel-autonomous-run-governance` 0.2.6 | `N/A` | Keine parallele Kampagne wird gestartet; Wiedervorlage nur bei ausdrücklicher Kampagnenautorität. |

## Autonomous-Run-Anwendbarkeit / Autonomous-run Applicability

- **Liefermodus und Autorität / Delivery mode and authority**: Der gespeicherte
  Gesamtlauf nennt `MergeAndSync`. Die aktuelle Phase ist enger autorisiert:
  Wiederaufnahme ausschließlich des unabhängigen Plan Reviews, minimale
  Remediation der ausdrücklich übernommenen Planungsartefakte,
  `plan-review.md` und das strukturierte Plan-Review-Phasenergebnis; keine
  Tasks-, Implementierungs-, Intake-, Serien-, Run-State-, Commit-, Push-,
  Merge- oder Folgefeature-Aktion.
- **Feature-Identität / Feature identity**:
  `specs/004-secure-development-hardening`, Branch
  `codex/004-secure-development-hardening`, Run-ID
  `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7`.
- **Akzeptierte Eingaben / Accepted inputs**:
  Intake `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de`,
  Review-Ergebnis `acdcf2dcb7411be6fa3389cf642748fcb1225e9bcbcf32e6bad8a76da54314fe`,
  Review-Anfrage `49cddf9ce3391048a12fc4314f1ef2cdf4c500de73956623875a916cde1f3c50`
  und Serienmanifest `1ca91db4ec4970c45a7c27b8623d03c29f52c9295305f8ee7d574b23d3f6cadf`.
- **Scope-Grenze / Scope boundary**: Genau dieser Intake. Folge-Intakes,
  Secrets, private Endpunkte, Provider-Administration und nicht ausdrücklich
  notwendige Produktfeatures bleiben ausgeschlossen.
- **Result-Semantik / Result semantics**: Das Feature ändert weder Delivery-
  Set-Validierung noch Phase-Result-Schema. Plan Review verwendet Schema 1.0
  und den normalisierten Hash von `plan-review.md`; das historische
  `plan.result.json` bleibt unverändert und wird über seinen Run-State-Dateihash
  gebunden.
- **Mutable Validation Tokens**: Für Plan Review werden keine veränderlichen
  Autoritäts-, Review-, Merge- oder Bypass-Tokens konsumiert. Die read-only
  Beobachtung des nächsten kanonischen PR-Slots wird unmittelbar vor Abgabe
  erneut gelesen und erteilt keine Provideraktion.
- **Kausaler Abschluss / Causal closeout**: `N/A` für Plan Review. Der
  Gesamtlauf bewertet Push, Review, Merge, Main-Sync und Post-Merge-Evidenz erst
  an der dafür autorisierten Grenze.
- **Stopp und Wiederaufnahme / Stop and resume**: Bewusster Stopp erfolgt nur
  an einer sicheren Phasengrenze und benötigt explizites Resume. Nach
  unerwarteter Unterbrechung werden Branch, Hashes, Scope, Run-State, Routing
  und vorhandene Artefakte vollständig neu validiert.
- **Retrospektive / Retrospective**: Übertragbare Erkenntnisse werden erst in
  der vorgesehenen Retrospektive festgehalten; daraus startet kein Follow-up.

### Akzeptanz-Schranken / Acceptance Gates

| Gate-ID | Status | Erforderliche Evidenz / Required evidence | Prüfweg / Validation path | Wiedervorlage / Re-evaluation |
|---|---|---|---|---|
| `SPEC-GATE-001` | `Applicable` | Branch, Run-ID und vier akzeptierte normalisierte Hashes stimmen. | Read-only PowerShell-7-/Hashprüfung auf macOS. | Vor jeder Phasenfortsetzung. |
| `SPEC-GATE-002` | `Applicable` | Alle Intake-, Checklisten-, Standard- und Preset-Punkte besitzen genau eine erlaubte Einstufung; nur `Applicable` erzeugt FR-Arbeit. | Markdown- und Traceability-Review. | In Clarify und Analyze. |
| `SPEC-GATE-003` | `Applicable` | Die in Specify erzeugte `checklists/requirements.md` ist Eingangsevidenz; nach Clarify revalidiert die dedizierte Checklist-Phase sie gegen den aktuellen Spec-Hash, bevor Plan beginnt. | Semantische Textprüfung und Phasenreihenfolge im Run-State. | Nach jeder Clarify-Änderung. |
| `SPEC-GATE-004` | `Applicable` | Scope, Nicht-Ziele, Reihenfolge, Risiken, Tests, Evidenz und Follow-ups sind vollständig; Intake, Serie und Run-State blieben unverändert. | Read-only Git-Diff und Hashvergleich. | Vor Phasenabschluss. |
| `SPEC-GATE-005` | `N/A` | Keine Runner-, Delivery- oder Parallel-Kampagnensemantik wird geändert. | Kein Ausführungstoken. | Bei entsprechender Scope-Änderung. |
| `SPEC-GATE-006` | `Applicable` | Das bereits validierte Specify-Ergebnis bindet den damaligen `spec.md`-Hash und zwei abgeschlossene Specify-Artefaktaufgaben. Spätere Clarify-Änderungen werden durch das getrennte Clarify-Ergebnis gebunden und schreiben den Vorgängernachweis nicht um. | `validate-autonomous-phase-result.ps1`. | Specify historisch abgeschlossen; Clarify separat validieren. |

## Dokumentationsauswirkung / Documentation Impact

**Entscheidung / Decision**: `UpdateRequired`

- **Zielgruppen / Audiences**: Auszubildende ab Lehrjahr 1, Lehrende,
  Entwicklerinnen und Entwickler, Reviewer, Maintainer, Security-Rollen,
  Nutzende der CLI/IDE und KI-Agenten.
- **Leserpfade / Reader paths**: README oder Sicherheitsindex →
  Anwendbarkeitsmatrix → Threat Model/Architektur → Code-/Test-/Supply-Chain-
  Evidenz → Ergebnis, Restrisiko und nächste Aktion.
- **Dokumentfamilien / Documentation families**: Spec-Kit-Artefakte,
  `docs/security/`, `docs/architecture/`, `docs/accessibility/`, öffentliche
  Vulnerability-Guidance, API-/Nutzer-Dokumentation, Agentenflächen und
  Projektstatistik.
- **Kanonische Quelle und Owner / Canonical source and owner**: Bindender Scope
  aus dem aktiven Intake; Sicherheitsanforderungen aus `constitution.md` und
  `docs/secure-development/`; projektspezifische Evidenz beim TinyPl0-
  Maintainer mit unabhängigem Security-/PR-Review.
- **Navigation / Navigation impact**: `docs/security/README.md` muss alle
  tatsächlich verwendeten Standard- und neuen Evidenzpfade verlinken. Neue
  öffentliche Meldeinformation muss aus README oder veröffentlichter
  Dokumentation auffindbar sein.
- **Dokumentklasse / Document class**: Normative Feature-Spezifikation,
  projektspezifische Sicherheits-/Architekturevidenz, öffentliche
  Nutzerinformation und maschinenlesbare Release-Evidenz.
- **Sprachstrategie und Partner / Language strategy and partner**: kurze
  Inhalte inline DE zuerst, EN danach. Große normative Dateien dürfen eine
  synchronisierte `.EN.md`-Partnerdatei verwenden; die deutsche Fassung bleibt
  kanonisch, sofern nichts anderes markiert ist.
- **Plattform- und Beispielnachweis / Platform and example proof**: C#/.NET auf
  macOS, Linux und Windows nach Registry-Vertrag; Bash/PowerShell-Parität für
  Scriptflächen; CLI/IDE-Beispiele sowie DocFX/axe/`lynx` für betroffene
  Nutzerpfade.
- **Distribution / Distribution class**: `public` für Security-Kontakt,
  Nutzer-/Release- und Supply-Chain-Information; `sourceOnly` für interne
  Feature-, Review- und detaillierte Audit-Matrizen.
- **Home-Sync / Home sync**: `false`; dieses Feature ändert
  projektspezifische Evidenz. Eine tatsächlich gemeinsame Governanceänderung
  löst stattdessen den atomaren Paritäts- und Home-Baseline-Prüfpfad aus.
- **Evidenz / Evidence**: diese Spec und Checkliste, ausgefüllte Security-
  Dokumente, Architektur-/A11Y-Nachweise, Tests/Coverage, SBOM/VEX/Provenienz,
  CI- und Release-Nachweise sowie `docs/project-statistics.md`.
- **Owner / Owner**: TinyPl0-Repository-Maintainer; unabhängiger Reviewer für
  Security-, Architektur- und Release-Aussagen.
- **Wiedervorlage / Re-evaluation trigger**: jede Änderung an Nutzerfläche,
  Runtime, Trust Boundary, Dependency, CI/Release, Provider, Regulierung,
  Agenten-Governance oder Dokumentnavigation.

*The decision is `UpdateRequired`. It covers learner, user, maintainer,
reviewer, security, and agent paths across feature, security, architecture,
accessibility, public vulnerability, API/user, release, and statistics
documentation. German remains first, public and source-only distribution are
distinguished, project-local work has no automatic Home sync, and every
material change triggers a renewed impact review.*

## Annahmen / Assumptions

- TinyPl0 bleibt ein öffentliches MIT-Ausbildungs- und Referenzprojekt mit
  releasefähigen Artefakten; kommerzielle Tätigkeit oder formale
  Herstellerpflichten werden nicht ohne Evidenz angenommen.
- Der HTTP-Scope bleibt der lokale statische Dokumentationsserver. ASVS Level 1
  ist dafür angemessen. Der vollständige L1-Katalog wird auf den begrenzten
  `--api`-Scope gemappt; nicht berührte IDs erhalten ein begründetes `N/A`.
  Eine externe oder authentifizierte Bereitstellung würde eine neue Level-
  Entscheidung auslösen.
- GitHub Actions, Pages und Release-Hosting sind Provider-Abhängigkeiten des
  Lieferprozesses, aber keine Cloud-Runtime des lokalen Produkts.
- Es gibt keine Produktkonten, keine personenbezogene Telemetrie, keine
  Produktkryptografie und keine ausgelieferte KI-Komponente.
- `docs/secure-development/checklisten/` ist die kanonische Quelle;
  der Sammelband ist erzeugt und wird nicht direkt geändert.
- Bestehende Tests und Security-Scans sind gültige Eingangsevidenz, ersetzen
  aber weder Threat Model noch Dependency-, ASVS- oder Supply-Chain-Nachweis.
- Ein späterer Plan darf konkrete technische Mittel auswählen, aber Scope,
  Standardsentscheidungen, historische Kompatibilität und Abnahmegrenzen
  dieser Spec nicht erweitern oder abschwächen.

## Abhängigkeiten / Dependencies

- Akzeptierter Intake, aktuelle Serienreview und unveränderte vier Hashes.
- TinyPl0 Level-2-Registry-Eintrag und Constitution v1.17.0 samt synchronem
  Memory-Spiegel.
- Secure-Development-Richtlinie 3.2.0, zwölf Einzelchecklisten, 157 CL-IDs,
  Sammelband und mitgeltende Mapping-Datei.
- Bestehende Module `Pl0.Core`, `Pl0.Vm`, `Pl0.Cli`, `Pl0.Ide`, xUnit-Suite,
  Golden-Katalog, DocFX und GitHub-Workflows.
- Aktive Presets in der lokal registrierten Prioritätsreihenfolge.
- Für rechtliche oder providerbezogene Aussagen eine zuständige menschliche
  Rolle; der Agent darf fehlende Geschäfts- oder Vertragsfakten nicht erfinden.

## Risiken / Risks

- Die vollständige Prüfung von 157 Punkten kann Scheingenauigkeit erzeugen.
  Pflichtfelder, Evidenzprüfung und unabhängiger Review begrenzen dieses Risiko.
- Ressourcenlimits können historische oder didaktische Programme unbeabsichtigt
  ablehnen. Grenzwert-, Golden- und Kompatibilitätstests müssen deshalb vor und
  nach jeder Änderung identisch nachvollziehbar sein.
- Ein generischer Fehlertext kann sicher sein, aber Lernwert verlieren. Die
  Diagnose muss daher Ursache und nächste Aktion erklären, ohne Interna
  offenzulegen.
- GitHub-Provider-Evidenz kann zeit- oder kontogebunden sein. Nicht
  repositoryfähig belegbare Punkte bleiben `Open` oder `FollowUp`; sie werden
  nicht als erfüllt behauptet.
- CRA-, C3A- und C5-Bewertungen können ohne Geschäfts-, Vertrags- oder
  Assurance-Unterlagen nur begrenzt abschließen. Die Spezifikation fordert eine
  dokumentierte Entscheidung, keine erfundene Konformität.
- Neue Security-Tools oder Dependencies können selbst Lieferkettenrisiken
  erzeugen. Bestehende, gepflegte und verifizierte Werkzeuge sind zu bevorzugen;
  jede Ergänzung braucht CVE-, Lizenz- und Quellenprüfung.
- Umfassende Dokumentationsänderungen können in spätere Intakes hineinreichen.
  Nur härtungsbedingte Nutzer- und Evidenztexte gehören hierher; Vollübersetzung
  und allgemeine A11Y-/L10N-Modernisierung bleiben Follow-up.
- Ein grüner Build beweist weder Sicherheit noch A11Y. Threat-, Negativ-,
  Coverage-, HTML-/Textbrowser- und Lieferkettenprüfungen bleiben unabhängige
  Gates.

## Test- und Evidenzstrategie / Test and Evidence Strategy

1. Vor jeder Phase Branch, Run-ID, State-Schema und die vier akzeptierten
   normalisierten Hashes read-only validieren.
2. Manifest, Richtlinien-/Checklisten-Versionen, 157 eindeutige IDs,
   Reihenfolge und Sammelband-Inhalt über beide Plattformpfade prüfen.
3. Alle 157 Punkte zweiachsig ausfüllen und jede positive Aussage gegen den
   tatsächlich existierenden, zum geprüften Stand gehörenden Nachweis prüfen.
4. Threat Model und Security-Qualitätsszenarien aus den realen Datenflüssen
   ableiten; höchste Risiken mit STRIDE, CIA und CAPEC abdecken.
5. Für jede Produktänderung zuerst einen fehlenden oder fehlschlagenden
   Sicherheits-/Grenztest zeigen, dann die kleinste grüne Änderung und danach
   die vollständige Regression ausführen.
6. Positiv-, Grenzwert-, Negativ- und Missbrauchstests für Lexer/Parser,
   P-Code, beide VM-Wege, CLI, Datei-I/O, IDE und HTTP-Dokumentation ausführen.
   Die VM führt höchstens das positive Budget `N` aus und diagnostiziert vor
   `N+1`; Standard `N = 1_000_000`. Batch- und Step-VM müssen dieselbe Grenze
   und Diagnose liefern. Tests verwenden kleine explizite `N`-Werte, damit die
   Grenzprüfung schnell und reproduzierbar bleibt.
7. Vollständige xUnit-Suite, 41 Pflichtfälle, Traceability und Golden-Artefakte
   prüfen. Golden-Dateien nur nach einer ausdrücklich akzeptierten
   Verhaltensänderung aktualisieren.
8. Coverage je betroffenem Modul messen: Gesamtlinie mindestens 70 % und nicht
   unter den belegten Ausgangswert 70,23 % absenken; 80 % bleibt dokumentiertes
   Ziel. Sicherheitskritische geänderte Module erreichen mindestens 85 %
   Branch-Coverage, KI-erzeugter geänderter Code mindestens 80 % Linie und
   Branch.
9. Den vollständigen ASVS-5.0.0-Level-1-Katalog für den lokalen HTTP-Scope auf
   exakte `Applicable`-/`N/A`-IDs abbilden und alle anwendbaren IDs prüfen;
   externe Bindung, Pfadüberschreitung, Header-Injektion, unerwartete Methoden,
   Fehlerantworten und statische Dateigrenzen negativ testen.
10. CLI- und IDE-Fehlerpfade tastatur- und textorientiert prüfen. Geänderte
    HTML-Ausgabe benötigt DocFX, Playwright/axe und `lynx`; schwerwiegende oder
    kritische A11Y-Befunde blockieren den Abschluss.
11. Dependencies, Lizenzen, Registry-Quellen und CVEs prüfen; SBOM erzeugen und
    validieren, VEX-Funde zuordnen, Provenienz/SLSA und Scorecard dokumentieren.
12. Security-Kontakt, CVD, regulatorische Entscheidungen, C3A/C5, SAMM,
    Agentenparität, Statistik und Abschlussnotiz gegen die Spec abgleichen.

## Konkrete Evidenzpfade / Concrete Evidence Paths

| Evidenz / Evidence | Pfad / Path |
|---|---|
| Vollständige 157-Punkte-Prüfinstanz / Full 157-item assessment | `docs/security/secure-development/2026-08-30-tinypl0-hardening/` |
| Sicherheitsindex / Security index | `docs/security/README.md` |
| Threat Model | `docs/security/threat-model.md` |
| Security Checklist | `docs/security/security-checklist.md` |
| arc42 Security | `docs/security/arc42-security.md` |
| Dependency Audit | `docs/security/dependency-audit.md` |
| Security Quality Scenarios | `docs/security/security-quality-scenarios.md` |
| ASVS 5.0.0 Level 1 | `docs/security/asvs-verification.md` |
| SBOM/VEX/SLSA/Scorecard | `docs/security/supply-chain-evidence.md` und Release-Artefakte / and release artefacts |
| CRA und Regulierung / CRA and regulation | `docs/security/cra-applicability.md`, `docs/security/regulatory-applicability.md` |
| BSI C3A/C5 | `docs/security/cloud-autonomy-applicability.md`, `docs/security/cloud-compliance-assurance.md` |
| Zero Trust `N/A` | `docs/security/zero-trust-applicability.md` |
| SAMM | `docs/security/samm-assessment.md` |
| S-ADRs | `docs/security/adr/` |
| Allgemeine Architektur / General architecture | `docs/ARCHITECTURE.md`, `docs/architecture/` |
| A11Y | `docs/accessibility/secure-development-hardening.md` |
| CVD / Vulnerability disclosure | `.github/SECURITY.md` und veröffentlichte `security.txt`-Evidenz / and published `security.txt` evidence |
| Produkt- und Regressionstests / Product and regression tests | `tests/Pl0.Tests/`, `tests/data/expected/`, `docs/TRACEABILITY_MATRIX.md` |
| CI/Release | `.github/workflows/`, Release-Nachweise / release evidence |
| Baseline-Parität / Baseline parity | `docs/secure-development/baseline-manifest.json`, `scripts/build-secure-development-docs.sh`, `scripts/build-secure-development-docs.ps1`, `docs/man/build-secure-development-docs.1.md` |
| Projektstatistik / Project statistics | `docs/project-statistics.md` |
| Feature-Abschluss / Feature closeout | `specs/004-secure-development-hardening/` |

## Follow-ups / Follow-ups

- Sandbox-Isolation, Mounts, Netzwerkfreigabe und Agenten-Host-Härtung bleiben
  beim geordneten Intake
  `Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`.
- Vollständige Quellcode-Kommentierung, englische Dokumentation, IDE-L10N und
  umfassende IDE-A11Y bleiben bei ihren jeweiligen späteren Intakes.
- Private Geräte-/Kontokontrollen aus CL-10 bleiben außerhalb des Repositories,
  bis eine zuständige Person eine sichere Evidenzform und Autorität festlegt.
- Ungeklärte kommerzielle Tätigkeit, Hersteller-/Steward-Rolle oder
  Provider-Vertragsdetails bleiben als verantwortete Folgepunkte in der
  regulatorischen beziehungsweise C3A/C5-Evidenz; sie starten kein Feature.
- Jeder Befund außerhalb des akzeptierten Scope wird nur mit Risiko, Owner,
  Priorität, Zieltermin, Evidenzziel und Wiedervorlage protokolliert.

## Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: 157 von 157 im Snapshot bestätigte CL-IDs besitzen genau eine
  Anwendbarkeit, einen Umsetzungsstand und alle zehn in FR-001 benannten
  Pflichtspalten; unvollständige/offene Zustände besitzen zusätzlich Priorität,
  Zieltermin oder Trigger und Evidenzziel. 0 relevante Punkte sind still
  ausgelassen; alle zwölf CL-12-IDs bleiben begründet außerhalb der Umsetzung.
- **SC-002**: 100 % der `Applicable`-Punkte zeigen auf existierende Evidenz
  oder sind bis zur Umsetzung ausdrücklich `Not Fulfilled`; 0 positive
  Erfüllungsaussagen besitzen einen fehlenden oder veralteten Nachweis.
- **SC-003**: Alle identifizierten Trust Boundaries besitzen mindestens ein
  Positiv-, Grenzwert-, Negativ- und Missbrauchsszenario sowie dokumentierte
  STRIDE/CIA-Bewertung; hohe Risiken besitzen passende CAPEC-Referenzen.
- **SC-004**: 100 % des adversarialen Compiler-, P-Code-, VM-, CLI-, Datei-,
  IDE- und HTTP-Testkorpus enden ohne unkontrollierte Ausnahme, Hänger oder
  Offenlegung interner Geheimnisse. Beide VM-Wege führen höchstens `N`
  Instruktionen aus und melden vor `N+1` dieselbe stabile Budgetdiagnose.
- **SC-005**: Alle 41 Pflichtfälle, vollständige Regression, Traceability und
  Golden-Parität bestehen; 0 unbeabsichtigte PL/0- oder P-Code-
  Verhaltensänderungen bleiben.
- **SC-006**: Gesamt-Zeilen-Coverage beträgt mindestens 70 % und nicht weniger
  als der belegte Ausgangswert 70,23 %; 80 % bleibt Ziel und eine Restlücke ist
  auditfähig `Open`. Betroffene sicherheitskritische Module erreichen
  mindestens 85 % Branch-Coverage; KI-erzeugter geänderter Code erreicht
  mindestens 80 % Linie und Branch.
- **SC-007**: 100 % der ASVS-5.0.0-Level-1-IDs sind für den begrenzten lokalen
  HTTP-Scope exakt als `Applicable` oder begründet `N/A` gemappt; 100 % der
  anwendbaren IDs sind erfüllt und 0 kritische oder hohe offene HTTP-Befunde
  bleiben beim Abschluss.
- **SC-008**: Der reproduzierbare SBOM-Erzeugungs-, Validierungs- und
  Veröffentlichungsanschlusspfad deckt 100 % des tatsächlich vorgesehenen
  Release-/Pages-Artefaktsatzes ab; ein Release ist kein Abschlusskriterium.
  Jede bekannte Schwachstelle besitzt einen VEX-Status; 0 kritische oder hohe
  nicht akzeptierte Dependency-Befunde passieren das Release-Gate.
- **SC-009**: NIST SSDF, CWE Top 25, ASVS, SBOM, VEX, SLSA, AI-SBOM, Zero
  Trust, SAMM, CRA, BSI C3A und BSI C5 besitzen jeweils genau eine
  nachvollziehbare Entscheidung und Wiedervorlage.
- **SC-010**: Manifest, Richtlinie, zwölf Einzelchecklisten und Sammelband
  stimmen in Version, 157 IDs, Reihenfolge und Inhalt überein; Bash- und
  PowerShell-Check liefern dasselbe erfolgreiche Ergebnis.
- **SC-011**: Für geänderte CLI-, IDE-, Markdown- und HTML-Flächen gibt es 0
  kritische oder schwerwiegende A11Y-Befunde; alle wesentlichen Zustände,
  Ursachen und nächsten Aktionen bleiben ohne Farbe, Layout oder Maus
  vollständig verständlich.
- **SC-012**: 100 % der geänderten lernendenseitigen Inhalte sind DE zuerst,
  EN danach, auf CEFR B2 und text-first; geänderte öffentliche APIs besitzen
  vollständige XML-Dokumentation und den erforderlichen DocFX-/axe-/`lynx`-
  Nachweis.
- **SC-013**: Alle aktiven Presets und fünf gepflegten Agentenflächen sind
  semantisch geprüft; 0 unbegründete Guidance- oder Template-Abweichungen
  bleiben.
- **SC-014**: Der Abschluss enthält genau eine priorisierte Liste offener oder
  akzeptierter Restrisiken mit Owner, Zieltermin/Trigger und Evidenzziel; 0
  Folge-Intakes wurden gestartet oder in diesen Scope gezogen.

*All 157 checklist IDs are classified and evidence-bound without pulling the
CL-12 sandbox follow-up into implementation. Trust boundaries have threat and
adversarial coverage; the no-regression coverage floor and changed-security-
module thresholds, golden compatibility, complete ASVS L1 mapping for the
bounded local HTTP scope, reproducible release SBOM/VEX handling, standards
decisions, baseline parity, accessibility, bilingual learning content, agent
parity, and residual-risk closeout meet the stated thresholds. No follow-up
intake is started.*
