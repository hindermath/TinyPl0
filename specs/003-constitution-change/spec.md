# Feature-Spezifikation: Constitution-Abgleich für didaktische und sprachliche Klarheit / Feature Specification: Constitution Alignment for Pedagogical and Linguistic Clarity

**Feature-Branch / Feature Branch**: `codex/003-constitution-change`
**Erstellt / Created**: 2026-08-29
**Status / Status**: Bereit für die Klärungsphase / Ready for clarification
**Verbindliche Eingabe / Binding Input**: `requirements/intakes/active/Lastenheft_Constitution_Change.md`
**Feature-Verzeichnis / Feature Directory**: `specs/003-constitution-change`

## Ziel und Nutzen / Goal and Value

TinyPl0 soll eine widerspruchsfreie, prüfbare Governance für verständliche
Lerninhalte, vollständige öffentliche API-Dokumentation und einen sichtbaren
TDD-Lernweg erhalten. TDD bedeutet **Test Driven Development**: Zuerst zeigt ein
Test den fehlenden oder falschen Zustand rot, danach macht die Umsetzung ihn
grün. Bereits erfüllte Regeln werden nicht neu umgesetzt.

*TinyPl0 shall have consistent and verifiable governance for understandable
learning content, complete public API documentation, and a visible TDD learning
path. TDD means **Test Driven Development**: a test first shows the missing or
wrong state in red, and the implementation then makes it green. Rules that are
already fulfilled will not be implemented again.*

## Scope / Scope

Dieser Lauf gleicht das verbindliche Lastenheft mit dem aktuellen Repository ab
und plant nur als `Applicable` eingestufte Punkte. Zum Feature gehören:

- ein TinyPl0-spezifischer Governance-Abschnitt „Didaktische und sprachliche
  Klarheit / Pedagogical and Linguistic Clarity“, ohne das bestehende
  Security-First-Prinzip I zu ersetzen;
- die vollständige XML-Dokumentation aller öffentlichen C#-APIs und eine
  Build-Schranke, die fehlende öffentliche XML-Dokumentation ablehnt;
- eine angemessene, zweisprachige Warum-Kommentierung für geänderte
  nicht-triviale Logik;
- ein didaktischer TDD-Ablauf für neue Funktionen und Fehlerkorrekturen;
- die synchrone Pflege der Constitution-Spiegel, Agentenflächen, betroffenen
  Templates und Projektstatistik;
- DocFX- und textorientierte A11Y-Nachweise, wenn API-Signaturen oder
  XML-Kommentare geändert werden.

*This run reconciles the binding intake with the current repository and plans
only items classified as `Applicable`. The feature includes a TinyPl0-specific
governance section without replacing the existing Security-First Principle I,
complete public C# API documentation with an enforcing build gate, suitable
bilingual why-comments for changed non-trivial logic, a pedagogical TDD flow,
synchronised constitution mirrors, agent surfaces, affected templates and
project statistics, plus DocFX and text-oriented accessibility evidence when
API signatures or XML comments change.*

## Nicht-Ziele / Non-Goals

- Keine neue Compiler-, VM-, CLI- oder IDE-Funktion und keine Änderung der
  PL/0-Sprachsemantik.
- Keine XML-Dokumentation für lokale Variablen; C# stellt dafür keine
  XML-Dokumentationsfläche bereit.
- Keine pauschale Übersetzung oder Überarbeitung aller vorhandenen
  Quellcodekommentare und Dokumente. Diese Arbeit bleibt bei den späteren,
  geordneten Intakes zur Quellcode- und englischen Dokumentation.
- Keine Änderung oder Neuanlage eines Skripts, Cmdlets oder Manpages.
- Keine Änderung von Abhängigkeiten, Netzwerk-, Cloud-, Authentifizierungs-,
  Autorisierungs- oder Laufzeit-Vertrauensgrenzen.
- In dieser Phase keine Implementierung, kein Commit, Push, Pull Request,
  Merge und kein Start eines weiteren Features.

*There is no new compiler, VM, CLI, or IDE function and no change to PL/0
semantics. Local variables receive no XML documentation because C# provides no
such documentation surface. The feature does not translate or revise every
existing comment and document; later ordered source-documentation and English-
documentation intakes retain that work. It adds no script, Cmdlet, or man page,
changes no dependency or runtime trust boundary, and this phase performs no
implementation, commit, push, pull request, merge, or additional feature run.*

## Reihenfolge und Abhängigkeiten / Ordering and Dependencies

1. Die akzeptierten Intake- und Review-Hashes bleiben unverändert und bilden
   die Eingangs-Schranke.
2. Zuerst werden Governance-Quelle, Spiegel, Templates und Agentenflächen
   inhaltlich ausgerichtet.
3. Danach werden öffentliche XML-Lücken geschlossen und die Build-Schranke
   aktiviert.
4. Wenn XML-Kommentare oder API-Signaturen geändert wurden, folgen DocFX-Aufbau
   und textorientierter A11Y-Nachweis im selben Arbeitsgegenstand.
5. Erst nach erfolgreicher Validierung darf der nächste Intake der Serie
   eigenständig begonnen werden. Dieses Feature startet ihn nicht.

*The accepted intake and review hashes remain the entry gate. Governance
sources, mirrors, templates, and agent surfaces are aligned first. Public XML
gaps and their enforcing build gate follow. Changed XML comments or API
signatures trigger DocFX and text-oriented accessibility evidence in the same
work item. A later series intake may start only as a separate validated run.*

## Nutzerszenarien und Tests / User Scenarios and Testing

### User Story 1 - Verständliche, einheitliche Lernregeln / Understandable and Consistent Learning Rules (Priorität / Priority: P1)

Als Auszubildende, Lehrende oder neue mitwirkende Person möchte ich dieselben
klaren Regeln zu Sprache, Dokumentation und Kommentaren an allen verbindlichen
Einstiegen finden, damit ich TinyPl0 ohne verborgenes Vorwissen nachvollziehen
kann.

*As an apprentice, teacher, or new contributor, I want to find the same clear
language, documentation, and comment rules at every binding entry point so that
I can understand TinyPl0 without hidden prior knowledge.*

**Warum diese Priorität / Why this priority**: Widersprüchliche Governance kann
jede spätere Code- und Dokumentationsarbeit in eine falsche Richtung lenken.

*Conflicting governance can direct every later code and documentation change
in the wrong direction.*

**Unabhängiger Test / Independent Test**: Die kanonische Constitution, ihr
Spiegel, alle gepflegten Agentenflächen und betroffenen Templates werden gegen
dieselbe Regelmenge geprüft.

*The canonical constitution, its mirror, all maintained agent surfaces, and
affected templates are checked against the same rule set.*

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** eine neue mitwirkende Person liest einen verbindlichen
   Einstieg, **wenn / when** sie die Dokumentationsregeln sucht, **dann / then**
   findet sie Deutsch zuerst, Englisch danach, CEFR B2, text-first A11Y,
   vollständige öffentliche XML-Dokumentation und die DocFX-Folgeprüfung ohne
   widersprüchliche Aussage.
2. **Gegeben / Given** die gemeinsame Constitution besitzt bereits das
   Security-First-Prinzip I, **wenn / when** die didaktische Regel ergänzt wird,
   **dann / then** bleibt Security-First unverändert und der TinyPl0-spezifische
   Titel wird im Projekt-Addendum eindeutig geführt.

### User Story 2 - Verlässliche öffentliche API-Dokumentation / Reliable Public API Documentation (Priorität / Priority: P1)

Als lernende oder integrierende Person möchte ich für jeden öffentlichen Typ
und jedes öffentliche Mitglied vollständige API-Erklärungen erhalten, damit
ich Parameter, Rückgaben und mögliche Fehler ohne Lesen der Implementierung
verstehen kann.

*As a learner or integrator, I want complete API explanations for every public
type and member so that I can understand parameters, returns, and possible
errors without reading the implementation.*

**Warum diese Priorität / Why this priority**: Das Repository behauptet bereits,
dass fehlende öffentliche XML-Dokumentation ein Build-Fehler ist; mehrere
Produktprojekte unterdrücken diese Prüfung aktuell dennoch.

*The repository already states that missing public XML documentation is a build
error, but several product projects currently suppress that check.*

**Unabhängiger Test / Independent Test**: Ein sauberer Build wird mit aktiver
Dokumentationsprüfung ausgeführt; keine fehlende öffentliche XML-Dokumentation
darf gemeldet werden.

*A clean build runs with documentation checking enabled and reports no missing
public XML documentation.*

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** ein öffentlicher Typ oder ein öffentliches Mitglied,
   **wenn / when** die API-Dokumentation geprüft wird, **dann / then** sind
   Zusammenfassung, Parameter, Rückgabe und mögliche Ausnahmen dort beschrieben,
   wo sie fachlich anwendbar sind.
2. **Gegeben / Given** eine öffentliche API ohne erforderliche XML-Dokumentation,
   **wenn / when** die Build-Schranke läuft, **dann / then** schlägt sie sichtbar
   fehl und kann nicht global unterdrückt werden.
3. **Gegeben / Given** geänderte XML-Kommentare, **wenn / when** die Änderung
   abgenommen wird, **dann / then** liegen ein erfolgreicher DocFX-Aufbau und ein
   textorientierter A11Y-Nachweis aus demselben Arbeitsgegenstand vor.

### User Story 3 - TDD als sichtbarer Lernweg / TDD as a Visible Learning Path (Priorität / Priority: P2)

Als Auszubildende oder entwickelnde Person möchte ich bei neuen Funktionen und
Fehlerkorrekturen den Weg Rot → Grün → Aufräumen nachvollziehen können, damit
Tests nicht nur Endkontrolle, sondern Teil des Lern- und Entwicklungsprozesses
sind.

*As an apprentice or developer, I want to follow the red → green → refactor path
for new features and bug fixes so that tests are part of learning and
development, not only a final check.*

**Warum diese Priorität / Why this priority**: Der Intake fordert diesen
didaktischen Ablauf, während die aktuelle Projekt-Guidance nur Tests, aber noch
keinen ausdrücklichen TDD-Nachweis verlangt.

*The intake requires this pedagogical flow, while current project guidance
requires tests but does not yet require explicit TDD evidence.*

**Unabhängiger Test / Independent Test**: Die Governance und spätere Aufgaben
nennen für jede betroffene neue Funktion oder Fehlerkorrektur den roten Test,
die grüne Umsetzung und die abschließende Regressionsprüfung oder begründen ein
`N/A`.

*Governance and later tasks name the red test, green implementation, and final
regression check for each affected feature or bug fix, or provide a reasoned
`N/A`.*

**Akzeptanzszenarien / Acceptance Scenarios**:

1. **Gegeben / Given** eine neue Funktion oder Fehlerkorrektur, **wenn / when**
   Aufgaben geplant werden, **dann / then** ist der TDD-Ablauf mit beobachtbarer
   Rot-, Grün- und Regressions-Evidenz enthalten.
2. **Gegeben / Given** eine reine Governance- oder Textänderung ohne sinnvollen
   roten Produkttest, **wenn / when** TDD bewertet wird, **dann / then** wird
   `N/A` mit kurzer Begründung und Wiedervorlage bei der nächsten Logikänderung
   dokumentiert.

### Grenzfälle / Edge Cases

- Eine bereits erfüllte Anforderung bleibt `AlreadySatisfied` und erzeugt keine
  Implementierungsaufgabe.
- Eine veraltete Formulierung darf das aktuelle Security-First-Prinzip I nicht
  umbenennen oder verdrängen; ihr fachlicher Inhalt wird projektlokal bewahrt.
- Ein öffentlicher Wert ohne Parameter, Rückgabe oder dokumentierbare Ausnahme
  benötigt nur die jeweils anwendbaren XML-Elemente.
- Lokale Variablen, private Implementierungsdetails und automatisch erzeugte
  Compilerbestandteile erzeugen keine künstlichen XML-Kommentare.
- Eine reine Markdown-Änderung löst nicht automatisch einen API-Neubau aus.
  Änderungen an API-Signaturen oder XML-Kommentaren tun dies.
- Schlägt DocFX oder die textorientierte A11Y-Prüfung fehl, bleibt das Feature
  unvollständig; ein erfolgreicher Build allein genügt nicht.
- Weichen Agentenflächen oder Templates semantisch voneinander ab, bleibt die
  Paritäts-Schranke offen, auch wenn jede einzelne Datei syntaktisch gültig ist.

*Already satisfied requirements create no implementation task. Outdated wording
must not displace Security-First Principle I. XML elements are required only
where they apply, and local variables, private details, or generated compiler
parts do not receive artificial XML comments. Pure Markdown changes do not
automatically trigger API generation, while API signature or XML-comment
changes do. Failed DocFX, accessibility, or semantic parity evidence keeps the
feature incomplete.*

## Intake-Abgleich / Intake Reconciliation

`Applicable` wird in diesem Feature umgesetzt. `AlreadySatisfied` ist bereits
belegt. `N/A` ist fachlich oder technisch nicht anwendbar. `FollowUp` bleibt
absichtlich bei einem späteren, geordneten Intake. Es gibt keine ungeklärte
`Open`-Anforderung in dieser Spezifikation.

*`Applicable` is implemented in this feature. `AlreadySatisfied` is already
evidenced. `N/A` is not professionally or technically applicable. `FollowUp`
intentionally remains with a later ordered intake. This specification has no
unresolved `Open` requirement.*

| ID | Intake-Anforderung / Intake requirement | Einstufung / Classification | Begründung und Evidenz / Rationale and evidence |
|---|---|---|---|
| IR-001 | Lesbarkeit und Lernwert haben Vorrang; Compilerphasen bleiben verständlich getrennt. / Readability and learning value take priority; compiler phases remain understandable. | `AlreadySatisfied` | `AGENTS.md`, `docs/ARCHITECTURE.md` und die Modulgrenzen enthalten diese Regel bereits. / Existing guidance and module boundaries already carry the rule. |
| IR-002 | Lerntexte stehen Deutsch zuerst und Englisch danach. / Learning text is German first and English second. | `AlreadySatisfied` | Constitution VII/VIII, README und alle gepflegten Agentenflächen enthalten die Reihenfolge. / Constitution VII/VIII, README, and maintained agent surfaces contain the order. |
| IR-003 | Beide Sprachblöcke zielen auf GER/CEFR B2. / Both language blocks target GER/CEFR B2. | `AlreadySatisfied` | Constitution VIII sowie Agenten- und A11Y-Guidance belegen das Ziel. / Constitution VIII and agent/A11Y guidance evidence the target. |
| IR-004 | Der didaktische Grundsatz trägt den Titel „Didaktische und sprachliche Klarheit“. / The pedagogical rule is titled “Pedagogical and Linguistic Clarity”. | `Applicable` | Die gemeinsame Constitution nutzt Prinzip I heute für Security-First. Der Titel wird deshalb sicher im TinyPl0-Level-2-Addendum ergänzt, nicht an Stelle von Security-First. / The shared constitution now uses Principle I for Security-First, so the title is safely added to the TinyPl0 Level-2 addendum instead of replacing Security-First. |
| IR-005 | XML-Kommentare für alle öffentlichen Typen und Mitglieder mit anwendbaren Parametern, Rückgaben und Ausnahmen. / XML comments for every public type and member, including applicable parameters, returns, and exceptions. | `Applicable` | Die Regel ist dokumentiert, aber `Pl0.Core`, `Pl0.Cli` und `Pl0.Ide` unterdrücken CS1591 aktuell. / The rule is documented, but three product projects currently suppress CS1591. |
| IR-006 | XML-Kommentare für lokale Variablen. / XML comments for local variables. | `N/A` | C# unterstützt keine XML-Dokumentation für lokale Variablen. Normale Kommentare bleiben möglich. Wiedervorlage nur bei einer neuen dokumentierbaren API-Fläche. / C# has no XML documentation surface for local variables; normal comments remain possible. Re-evaluate only for a new documentable API surface. |
| IR-007 | Geeignete didaktische Block- oder Zeilenkommentare stehen DE zuerst, EN danach. / Suitable pedagogical block or line comments are DE first, EN second. | `AlreadySatisfied` | Alle Agentenflächen fordern moderate zweisprachige Warum-Kommentare für nicht-triviale Logik. / All agent surfaces already require moderate bilingual why-comments for non-trivial logic. |
| IR-008 | Dokumentation dient als Lernmaterial für Fachinformatiker Anwendungsentwicklung. / Documentation serves as learning material for application-development apprentices. | `AlreadySatisfied` | Constitution VII/VIII, README und Projekt-Guidance nennen Lernende und ersten Ausbildungsjahrgang. / Constitution VII/VIII, README, and project guidance name learners and first-year accessibility. |
| IR-009 | Kommentare erklären Entscheidungen, Abwägungen und Grenzen statt nur den Ablauf. / Comments explain decisions, trade-offs, and constraints rather than only flow. | `AlreadySatisfied` | `AGENTS.md` und die vier Agenten-Guidance-Dateien enthalten die Warum-Regel. / Project and four-agent guidance contain the why-rule. |
| IR-010 | Fehlende öffentliche XML-Dokumentation ist ein Build-Fehler; CS1591 wird nicht global unterdrückt. / Missing public XML documentation is a build error; CS1591 is not globally suppressed. | `Applicable` | Drei Produktprojekte enthalten derzeit `<NoWarn>...1591</NoWarn>`; diese Abweichung muss geschlossen werden. / Three product projects currently contain the suppression and must be corrected. |
| IR-011 | API-/XML-Änderungen erzeugen DocFX-Ausgabe im selben Commit/PR. / API or XML changes regenerate DocFX output in the same commit/PR. | `AlreadySatisfied` | Agenten-Guidance und `docfx.json` enthalten den Prozess; die spätere Umsetzung muss den Lauf nachweisen. / Guidance and configuration already define the process; implementation must provide run evidence. |
| IR-012 | Gemeinsame Laufzeit-Guidance wird über alle Agentenflächen gepflegt. / Shared runtime guidance is maintained across all agent surfaces. | `Applicable` | Die neue projektlokale Regel und TDD-Ergänzung müssen atomar in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, beiden Copilot-Flächen und betroffenen Templates erscheinen. / The new local rule and TDD addition require atomic parity across all maintained surfaces and affected templates. |
| IR-013 | Dokumentationskonformität wird erneut geprüft und fehlende öffentliche Dokumentation ergänzt. / Documentation compliance is rechecked and missing public documentation is completed. | `Applicable` | Die aktuelle CS1591-Unterdrückung verhindert einen belastbaren Nachweis. / Current CS1591 suppression prevents reliable evidence. |
| IR-014 | Jeder allgemeine Prosa-Dokumentationswechsel startet DocFX. / Every general prose documentation change runs DocFX. | `N/A` | Die spätere, präzisere Intake-Regel begrenzt den Pflichtlauf auf API-Signaturen und XML-Kommentare. Wiedervorlage bei einer Änderung der DocFX-Navigation oder API-Präsentation. / The later and more specific intake rule limits the mandatory run to API signatures and XML comments. Re-evaluate when DocFX navigation or API presentation changes. |
| IR-015 | Neue Funktionen und Fehlerkorrekturen zeigen möglichst TDD Rot → Grün → Aufräumen. / New features and fixes should show TDD red → green → refactor where practical. | `Applicable` | Tests sind Pflicht, aber der ausdrückliche Lern- und Evidenzablauf fehlt in der aktuellen Guidance. / Tests are mandatory, but the explicit learning and evidence flow is absent from current guidance. |
| IR-016 | Vorhandene Quellcode- und Dokumentationsbestände werden vollständig zweisprachig nachgearbeitet. / Existing source and documentation are fully remediated bilingually. | `FollowUp` | Die akzeptierte Serie reserviert Vollständigkeitsarbeit für `Lastenheft_Quellcode_Doku.md` und `Lastenheft_Dokumentation_EN.md`; Vorziehen würde die verbindliche Reihenfolge verletzen. / The accepted series reserves full remediation for the later source-documentation and English-documentation intakes. |

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Die kanonische Constitution MUSS im TinyPl0-Level-2-Addendum einen
  klar abgegrenzten Abschnitt „Didaktische und sprachliche Klarheit /
  Pedagogical and Linguistic Clarity“ führen, ohne Security-First-Prinzip I
  umzubenennen, abzuschwächen oder zu ersetzen.

  *The canonical constitution MUST carry a clearly scoped TinyPl0 Level-2
  section named “Pedagogical and Linguistic Clarity” without renaming,
  weakening, or replacing Security-First Principle I.*

- **FR-002**: Jede öffentliche C#-API im Produktscope MUSS vollständige,
  anwendbare XML-Dokumentation besitzen. Die Build-Governance MUSS fehlende
  öffentliche XML-Dokumentation ablehnen; CS1591 DARF nicht global oder
  projektweit unterdrückt werden.

  *Every public C# API in product scope MUST have complete applicable XML
  documentation. Build governance MUST reject missing public XML
  documentation, and CS1591 MUST NOT be suppressed globally or per project.*

- **FR-003**: Geänderte nicht-triviale Logik MUSS auf didaktischen Kommentarwert
  geprüft werden. Notwendige Kommentare erklären Warum, Abwägung, Grenze oder
  historischen Unterschied in Deutsch zuerst und Englisch danach auf CEFR B2.

  *Changed non-trivial logic MUST be reviewed for pedagogical comment value.
  Necessary comments explain the reason, trade-off, boundary, or historical
  difference in German first and English second at CEFR B2.*

- **FR-004**: Governance und spätere Aufgaben MÜSSEN für neue Funktionen und
  Fehlerkorrekturen einen praktikablen TDD-Ablauf mit Rot-, Grün- und
  Regressions-Evidenz verlangen. Für reine Text- oder Governance-Änderungen ist
  ein begründetes `N/A` zulässig.

  *Governance and later tasks MUST require a practical TDD flow with red,
  green, and regression evidence for features and fixes. A reasoned `N/A` is
  allowed for text-only or governance-only changes.*

- **FR-005**: Wenn API-Signaturen oder XML-Kommentare geändert werden, MÜSSEN
  DocFX-Aufbau und die dokumentierte textorientierte A11Y-Prüfung im selben
  Arbeitsgegenstand erfolgreich sein. Fehlende oder fehlerhafte Evidenz
  blockiert den Abschluss.

  *Changed API signatures or XML comments MUST have a successful DocFX build
  and documented text-oriented accessibility review in the same work item.
  Missing or failed evidence blocks completion.*

- **FR-006**: Gemeinsame Regeländerungen MÜSSEN atomar in `constitution.md`,
  `.specify/memory/constitution.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
  `.github/copilot-instructions.md`,
  `.github/agents/copilot-instructions.md`, betroffenen
  `.specify/templates/`-Dateien und betroffenen `scripts/templates/`-Dateien
  geprüft und bei Bedarf aktualisiert werden. Absichtliche Abweichungen MÜSSEN
  in derselben Änderung begründet werden.

  *Shared rule changes MUST be reviewed and, where affected, updated atomically
  across both constitution files, all maintained agent surfaces, and affected
  Spec-Kit and repository templates. Any intentional deviation MUST be
  justified in the same change.*

- **FR-007**: Die in der Constitution dokumentierte Standard-Acht-Preset-Matrix
  MUSS mit `scripts/config/spec-kit-governance-presets.json` übereinstimmen;
  die ausführbare Konfiguration bleibt die Quelle der Wahrheit. Zusätzliche,
  separat verwaltete optionale Presets dürfen im Repository registriert sein,
  sofern sie keinen Standard-Eintrag ersetzen oder widersprüchlich überlagern.

  *The standard eight-preset matrix documented in the constitution MUST match
  the executable governance-preset configuration, which remains the source of
  truth. Separately governed optional presets may also be registered when they
  neither replace nor conflict with a standard entry.*

- **FR-008**: Nach der Agenten-getriebenen Umsetzung MUSS
  `docs/project-statistics.md` gemäß dem vorhandenen Statistikvertrag
  fortgeschrieben werden; die Gesamtstatistik bleibt der letzte
  Top-Level-Abschnitt.

  *After agent-driven implementation, the project statistics ledger MUST be
  updated under its existing contract, with the overall statistics remaining
  the final top-level section.*

- **FR-009**: Dieses Feature DARF keine `AlreadySatisfied`-, `N/A`- oder
  `FollowUp`-Position als Implementierungsarbeit einplanen und DARF keinen
  weiteren Intake starten.

  *This feature MUST NOT plan `AlreadySatisfied`, `N/A`, or `FollowUp` items as
  implementation work and MUST NOT start another intake.*

### Constitution-Anforderungen / Constitution Requirements

- **CR-001**: Der Level-2-Registry-Eintrag für `RiderProjects/TinyPl0` ist der
  verbindliche Kontext: .NET 10/C# 14, `dotnet restore/build/test`, xUnit,
  DocFX/A11Y, Statistikbasen 80 und 125 sowie die gepflegten Agentenflächen.
- **CR-002**: Nutzer- und lernendenseitige Artefakte sind text-first und, soweit
  anwendbar, nach WCAG 2.2 Level AA prüfbar; Zustände und Entscheidungen hängen
  nicht allein von Farbe, Layout oder Diagrammen ab.
- **CR-003**: Lern- und Governance-Inhalte stehen DE zuerst, EN danach auf CEFR
  B2, erklären Fachbegriffe bei erster Verwendung und setzen keine Spec-Kit-
  Erfahrung voraus.
- **CR-004**: Agentenflächen, Templates und Projektstatistik benötigen für die
  neue Regel synchrone Updates; die betroffenen Pfade stehen in FR-006/FR-008.
- **CR-005**: Primäre Sprache ist C# 14 auf .NET 10. C# steht auf der MSL-
  Erlaubnisliste und ist speichersicher; Laufzeit oder Hardware erzwingen keine
  nicht-speichersichere Sprache. Sichere .NET-Programmierregeln bleiben Pflicht.
- **CR-006**: NIST SSDF und CWE Top 25 gelten. Alle bedingten Standards sind in
  der Governance-Matrix unten mit begründetem `N/A` oder `Applicable` erfasst.
- **CR-007**: OWASP ASVS ist `N/A`, weil keine Web-, API-, HTTP- oder
  Authentifizierungsfunktion geändert wird. Wiedervorlage bei einem solchen
  Scope.
- **CR-008**: SBOM, VEX und SLSA sind für diese Governance-/Dokumentationsänderung
  `N/A`, weil weder Release-Inhalt, Abhängigkeiten noch Build-Provenienz geändert
  werden. Die bestehenden Release-Pflichten bleiben unberührt.
- **CR-009**: KI wird nur als Entwicklungswerkzeug eingesetzt und ist kein
  ausgelieferter oder betriebener Bestandteil. AI-SBOM ist daher `N/A`;
  Wiedervorlage bei einem KI-Runtime- oder KI-Produktbestandteil.
- **CR-010**: CAPEC und Zero Trust sind `N/A`, weil keine Vertrauensgrenze und
  kein verteilter oder externer Fluss geändert wird. Wiedervorlage bei einer
  entsprechenden Architekturänderung.
- **CR-011**: Bestehende Standards-Evidenz bleibt unter `docs/security/`. Für
  diesen Lauf genügt die feature-lokale Prüfspur in `spec.md` und
  `checklists/requirements.md`; Cloud-, Regulierungs- und ASVS-Dateien werden
  mangels Trigger nicht geändert.
- **CR-012**: Es gelten alle acht Presets des installierten Standardprofils:
  `security-governance`, `architecture-governance`,
  `isaqb-architecture-governance`, `a11y-governance`,
  `cross-platform-governance`, `agent-parity-governance`,
  `autonomous-run-governance` und `parallel-autonomous-run-governance`.
- **CR-013**: Dokumentationsauswirkung ist genau `UpdateRequired`; die
  vollständige Entscheidung folgt im Abschnitt „Dokumentationsauswirkung“.

*CR-001 through CR-013 bind the TinyPl0 Level-2 registry context, WCAG text-
first delivery, German-first/English-second CEFR-B2 content, agent/template and
statistics parity, C#/.NET memory safety, mandatory NIST SSDF and CWE Top 25,
reasoned conditional-standard decisions, development-tool-only AI use, default
security evidence, all eight standard-profile governance presets, and exactly one
documentation-impact decision.*

## Governance-Anwendbarkeit und Audit-Evidenz / Governance Applicability and Audit Evidence

`Not Fulfilled` bedeutet hier, dass die spätere Implementierung noch aussteht;
es ist kein Ergebnis der aktuellen Specify-Phase. Owner ist die umsetzende
Person oder der ausführende Agent. Reviewer ist der spätere PR-Reviewer.

*`Not Fulfilled` means that later implementation is still pending; it is not a
failure of the current specify phase. The owner is the implementer or executing
agent, and the reviewer is the later pull-request reviewer.*

| Prüfpunkt / Checkpoint | Anwendbarkeit / Applicability | Umsetzung / Implementation | Begründung, Evidenz und Restrisiko / Rationale, evidence, and residual risk | Wiedervorlage und Folge / Re-evaluation and follow-up |
|---|---|---|---|---|
| NIST SSDF | `Applicable` | `Partly Fulfilled` | Für Level 2 immer Pflicht; Spec und Checkliste sind Prepare-Evidenz. Restrisiko: Umsetzungsnachweis fehlt noch. / Always mandatory for Level 2; spec and checklist provide Prepare evidence. | Bei Plan, Tasks, Implementierung und Abschluss erneut prüfen. / Recheck at plan, tasks, implementation, and closeout. |
| CWE Top 25 | `Applicable` | `Partly Fulfilled` | Build- und Dokumentationsänderungen werden sicherheitsorientiert geprüft; keine neue Eingabegrenze. / Build and documentation changes receive security review; no new input boundary. | Bei jeder Code-, Datei- oder Build-Logikänderung erneut prüfen. / Recheck for code, file, or build-logic changes. |
| OWASP ASVS | `N/A` | `Not Assessed` | Kein Web/API/HTTP/Auth-Scope. / No web, API, HTTP, or authentication scope. | Bei neuem Web-/API-/Auth-Scope. / On new web/API/auth scope. |
| SBOM | `N/A` | `Not Assessed` | Keine Änderung ausgelieferter Komponenten oder Abhängigkeiten. / No changed shipped component or dependency. | Vor Release oder bei Abhängigkeitsänderung. / Before release or on dependency change. |
| VEX | `N/A` | `Not Assessed` | Keine bekannte Schwachstelle wird bewertet. / No known vulnerability is evaluated. | Bei CVE-Fund oder Release-Review. / On a CVE finding or release review. |
| AI-SBOM | `N/A` | `Not Assessed` | KI ist nur Entwicklungswerkzeug. / AI is development tooling only. | Bei KI-Runtime, Modell, Datensatz oder Inferenzdienst im Produkt. / On product AI runtime, model, dataset, or inference service. |
| SLSA | `N/A` | `Not Assessed` | Keine CI/CD- oder Provenienzänderung. / No CI/CD or provenance change. | Bei Build-/Publishing-Pipeline-Änderung. / On build or publishing-pipeline change. |
| OpenSSF Scorecard | `N/A` | `Not Assessed` | Keine neue externe Abhängigkeit und kein Release-Scope. / No new external dependency or release scope. | Bei Abhängigkeitsaufnahme oder Release. / On dependency adoption or release. |
| NIS2, CRA, EU AI Act, DORA | `N/A` | `Not Assessed` | Keine Marktbereitstellung, regulierte Dienstleistung, KI-Runtime oder Finanz-ICT-Änderung. / No market placement, regulated service, AI runtime, or financial ICT change. | Bei Änderung dieser Auslöser. / On a change to these triggers. |
| C#-MSL und Secure Coding | `Applicable` | `Partly Fulfilled` | C# 14/.NET 10 ist speichersicher; sichere .NET-Regeln gelten. Kein Produktcode wird in Specify geändert. / C# is memory-safe and secure .NET rules apply; specify changes no product code. | Bei Implementierung und Review erneut prüfen. / Recheck during implementation and review. |
| STRIDE/CIA, CAPEC, Trust Boundaries | `N/A` | `Not Assessed` | Keine Laufzeit-, Datenfluss- oder Vertrauensgrenzenänderung. / No runtime, data-flow, or trust-boundary change. | Bei externem Input, Datei-/Netzwerkfluss oder Privilegänderung. / On external input, file/network flow, or privilege change. |
| S-ADR und arc42 Security | `N/A` | `Not Assessed` | Keine sicherheitsrelevante Architekturentscheidung. / No security-relevant architecture decision. | Bei Architektur- oder Trust-Boundary-Änderung. / On architecture or trust-boundary change. |
| Zero Trust | `N/A` | `Not Assessed` | Kein verteiltes, Cloud-, Remote- oder Mehrgeräte-System betroffen. / No distributed, cloud, remote, or multi-device system is affected. | Bei entsprechendem Betriebsmodell. / On such an operating model. |
| OWASP SAMM | `N/A` | `Not Assessed` | Das Feature ändert keinen Security-Programmprozess; bestehende Bewertung bleibt bestehen. / The feature does not change the security-program process. | Bei Security-Prozess- oder Reifegradänderung. / On security-process or maturity change. |
| BSI C3A / BSI C5 | `N/A` | `Not Assessed` | Keine Cloud- oder Provider-Abhängigkeit wird gewählt. / No cloud or provider dependency is selected. | Bei SaaS/PaaS/IaaS, Hosting oder Managed Service. / On SaaS/PaaS/IaaS, hosting, or managed service. |
| iSAQB/arc42 allgemeine Architektur | `N/A` | `Not Assessed` | Systemkontext, Schnittstellen, Building Blocks, Runtime und Deployment bleiben unverändert. / Context, interfaces, building blocks, runtime, and deployment remain unchanged. | Bei struktureller oder laufzeitbezogener Änderung. / On structural or runtime change. |
| WCAG 2.2 AA und text-first | `Applicable` | `Partly Fulfilled` | Spec und Checkliste sind semantisches Markdown; spätere DocFX-Ausgabe braucht den vorhandenen Playwright/axe- und lynx-Pfad. / Spec and checklist are semantic Markdown; later DocFX output needs the existing Playwright/axe and lynx path. | Bei jeder nutzerseitigen oder generierten Dokumentationsänderung. / On each user-facing or generated documentation change. |
| DE zuerst, EN danach, CEFR B2 | `Applicable` | `Fulfilled` | Diese Spec und Checkliste liefern beide Sprachpfade in der geforderten Reihenfolge. / This spec and checklist provide both language paths in the required order. | Bei jeder Textänderung semantisch prüfen. / Semantically review every text change. |
| Didaktische Inline-Kommentare | `N/A` | `Not Assessed` | Specify ändert keine nicht-triviale C#-Logik. / Specify changes no non-trivial C# logic. | Bei späterer Logikänderung; dann FR-003 anwenden. / On later logic changes, then apply FR-003. |
| `docs/accessibility/` | `N/A` | `Not Assessed` | Kein neuer UI- oder Bedienfluss; die feature-lokale Checkliste ist ausreichende Specify-Evidenz. / No new UI or interaction flow; the feature checklist is sufficient specify evidence. | Wenn DocFX-Navigation, UI oder A11Y-Verhalten geändert wird. / If DocFX navigation, UI, or accessibility behaviour changes. |
| Skript-/Cmdlet-Parität | `N/A` | `Not Assessed` | Kein Repository-Automationsskript, `.sh`/`.ps1`-Paar, Cmdlet, Workflow-Helper oder keine Manpage wird geändert. Browserseitige DocFX-A11Y-Anpassung besitzt kein Befehlsgegenstück und wird unter JavaScript-Secure-Coding und A11Y geprüft. / No repository automation script, paired command, cmdlet, workflow helper, or man page changes. Browser-side DocFX A11Y is reviewed under JavaScript secure coding and accessibility. | Bei einer entsprechenden Automations- oder Manpage-Änderung. / On a matching automation or man-page change. |
| Agentenparität und Templates | `Applicable` | `Not Fulfilled` | FR-006 benennt alle gemeinsam zu prüfenden Flächen; keine absichtliche Abweichung geplant. / FR-006 names all jointly reviewed surfaces; no intentional deviation is planned. | Vor Abschluss Homogenitäts- und Semantikprüfung. / Run homogeneity and semantic review before completion. |
| Audit-Checkliste | `Applicable` | `Fulfilled` | `specs/003-constitution-change/checklists/requirements.md`. Restrisiko: spätere Phasen müssen ihren Status aktualisieren. / The feature checklist is present; later phases must update their evidence. | An jeder Phasengrenze. / At every phase boundary. |

## Autonomous-Run-Anwendbarkeit / Autonomous-run Applicability

- **Liefermodus / Delivery mode**: Der akzeptierte Zustand nennt
  `MergeAndSync`. Die ausdrückliche Autorität dieser Phase ist enger: nur
  Spezifikationsartefakte und das strukturierte Phasenergebnis; kein Commit,
  Push, PR, Merge, Bypass, Secret- oder Provider-Zugriff.
- **Feature-Identität / Feature identity**:
  `specs/003-constitution-change`, Branch
  `codex/003-constitution-change`, Run-ID
  `064927e0-8389-4692-a53c-f1ce79e6043d`.
- **Akzeptierte Eingaben / Accepted inputs**: Binding intake
  `fe796de8ced6daf9cb3f4c890b929f47420a12deac2f37da793c4ea263fc2ff5`,
  Review-Ergebnis
  `3533dbc8a717ade82055dfaac644d30bd8a593858e30e8b5d6a8aab4cb1e11dc`,
  Review-Anfrage
  `1c6ca450b55e6d5b4de11eba7a15ccbcb817ad880e75b60141a98e5c1aecd15c`
  und Serienmanifest
  `5e4ca0a67a221854fef7abb092b7f014433f6dd1e6c0e24b71fc978f5096b3bf`.
- **Delivery-Set-/Result-Semantik / Delivery-set and result semantics**:
  Dieses Feature ändert weder Runner-Schema noch Ergebnissemantik. Die
  Specify-Phase verwendet das vorhandene Schema 1.0 für ihr strukturiertes
  Ergebnis; neue Delivery-Entscheidungen würden Schema 2.0 verlangen.
- **Mutable Validation Tokens**: `N/A`; es gibt keine veränderlichen
  Provider-, Review- oder Merge-Tokens in dieser Phase. Wiedervorlage bei einer
  Remote- oder Provider-Phase.
- **Kausaler Abschluss / Causal closeout**: `N/A` für Specify, weil keine
  Veröffentlichung oder Synchronisation erlaubt ist. Der Gesamtlauf bewertet
  Closeout erst an seiner dafür autorisierten Grenze.
- **Stopp und Wiederaufnahme / Stop and resume**: Bei bewusstem Stopp bleibt der
  Lauf an der sicheren Phasengrenze pausiert und benötigt die ausdrückliche
  Resume-Phase. Nach unerwarteter Unterbrechung müssen Branch, akzeptierte
  Hashes, Scope, Runner-Zustand und vorhandene Artefakte vor Fortsetzung erneut
  geprüft werden.
- **Retrospektive und Follow-up / Retrospective and follow-up**: Übertragbare
  Erkenntnisse dürfen erst in der vorgesehenen Retrospektive festgehalten
  werden. Die späteren Intakes bleiben außerhalb dieses Features.

*The accepted state names `MergeAndSync`, while this phase has narrower
authority: specification artefacts and the structured result only. It may not
commit, publish, review, merge, bypass, access secrets, or administer a
provider. The feature identity and four accepted hashes are fixed. This
feature changes neither delivery-set validation nor result semantics, has no
mutable validation token, and needs no causal closeout in specify. A deliberate
stop requires explicit resume; an unexpected interruption requires full scope,
hash, branch, state, and artefact revalidation.*

### Akzeptanz-Schranken / Acceptance Gates

| Gate-ID | Status | Erforderlicher Scope und Evidenz / Required scope and evidence | Befehls-/Plattform-Token / Command or platform token | Wiedervorlage / Re-evaluation |
|---|---|---|---|---|
| `SPEC-GATE-001` | `Applicable` | Vier akzeptierte normalisierte Hashes stimmen exakt. / Four accepted normalized hashes match exactly. | PowerShell 7 Hash-Prüfung auf macOS; semantisch plattformneutral. / PowerShell 7 hash check on macOS; semantics are platform-neutral. | Vor jeder Phasenfortsetzung. / Before every phase continuation. |
| `SPEC-GATE-002` | `Applicable` | Jede Intake-Anforderung ist genau einer erlaubten Klasse zugeordnet; nur `Applicable` erscheint in FR-001 bis FR-009. / Every intake requirement has one allowed class; only `Applicable` work appears in the functional requirements. | Markdown-Review. | In Clarify und Analyze erneut prüfen. / Recheck in clarify and analyze. |
| `SPEC-GATE-003` | `Applicable` | `spec.md` und `checklists/requirements.md` sind vollständig, DE→EN, CEFR B2, text-first und ohne Klärungsmarker. / Both files are complete, German-first/English-second, CEFR B2, text-first, and contain no clarification marker. | Text-/Markdown-Review. | Bei jeder Textänderung. / On every text change. |
| `SPEC-GATE-004` | `Applicable` | Scope, Nicht-Ziele, Reihenfolge, Governance, Evidenz und Abnahme sind nachvollziehbar; kein anderer Intake wurde gestartet. / Scope, non-goals, order, governance, evidence, and acceptance are traceable; no other intake started. | Git-Diff nur lesend prüfen. / Read-only Git diff review. | Vor Phasenabschluss. / Before phase completion. |
| `SPEC-GATE-005` | `N/A` | Runner-/Delivery-Schemaänderung ist nicht Teil dieses Features. / Runner or delivery-schema change is out of scope. | Kein Token. / No token. | Bei Änderung der autonomen Semantik. / On autonomous-semantics change. |
| `SPEC-GATE-006` | `Applicable` | Das strukturierte Ergebnis nennt `spec.md` und dessen exakten normalisierten SHA-256; der vorhandene Validator akzeptiert das Ergebnis. / The structured result names `spec.md` and its exact normalized SHA-256; the existing validator accepts it. | `validate-autonomous-phase-result.ps1`, PowerShell 7. | Nach letzter Spec-Änderung neu berechnen. / Recompute after the final spec change. |

## Dokumentationsauswirkung / Documentation Impact

**Entscheidung / Decision**: `UpdateRequired`

- **Zielgruppen / Audiences**: Auszubildende ab dem ersten Ausbildungsjahr,
  Lehrende, Entwicklerinnen und Entwickler, Reviewer sowie KI-Agenten.
- **Leserpfade / Reader paths**: Einstieg über Constitution oder Agenten-Guidance
  → Sprach-/A11Y-/XML-Regeln → Build- und DocFX-Nachweis → nächste sichere
  Aktion in Plan oder Tasks.
- **Dokumentfamilien / Documentation families**: normative Governance,
  Agenten-Guidance, Spec-Kit-Templates, generierte API-Dokumentation und
  Projektstatistik.
- **Kanonische Quelle und Owner / Canonical source and owner**:
  `constitution.md`, Repository-Maintainer; ausführbare Preset-Versionen aus
  `scripts/config/spec-kit-governance-presets.json`, Spec-Kit-Maintainer.
- **Navigation / Navigation impact**: Der neue TinyPl0-Abschnitt muss aus dem
  Level-2-Addendum klar auffindbar sein; keine neue Website-Hauptnavigation ist
  erforderlich.
- **Dokumentklasse / Document class**: Level-2-Governance mit erzeugten oder
  gespiegelten Ableitungen.
- **Sprachstrategie und Partner / Language strategy and partner**: kurze Texte
  inline DE zuerst, EN danach; große normative Dokumente dürfen einen
  synchronen `.EN.md`-Partner nutzen. Dieses Feature plant keinen neuen
  Sidecar.
- **Plattform- und Beispielnachweis / Platform and example proof**: semantische
  Markdown-Prüfung auf allen Plattformen; .NET-/DocFX-Nachweis nach dem
  Registry-Vertrag. Keine skriptspezifische Plattformabweichung.
- **Distribution und Home-Sync / Distribution and home sync**: Repository-
  lokaler `sourceOnly`-Governanceinhalt; Home-Sync `N/A`, weil TinyPl0 keinen
  lokalen Home-Runtime-Vertrag dafür besitzt.
- **Evidenz / Evidence**: diese Spezifikation, ihre Qualitätscheckliste,
  spätere Build-/Test-/DocFX-/A11Y-Protokolle, Homogenitätsprüfung und
  `docs/project-statistics.md`.
- **Wiedervorlage / Re-evaluation trigger**: jede Änderung an Constitution,
  öffentlicher API, XML-Kommentaren, DocFX-Navigation, Agentenparität,
  Preset-Matrix oder Statistikmethodik.

*The decision is `UpdateRequired`. It covers first-year apprentices, teachers,
developers, reviewers, and AI agents across normative governance, agent
guidance, Spec-Kit templates, generated API documentation, and statistics. The
canonical sources are the constitution and executable preset configuration.
Inline text is German first and English second. Evidence consists of this spec
and checklist plus later build, test, DocFX, accessibility, parity, and
statistics records. Home sync is not applicable to this repository-local
source-only governance content.*

## Annahmen / Assumptions

- Der akzeptierte Intake und die drei Serien-/Review-Artefakte bleiben während
  dieser Phase inhaltlich unverändert.
- `constitution.md` ist kanonisch;
  `.specify/memory/constitution.md` ist sein synchroner Spiegel.
- Die ausführbare Standard-Acht-Preset-Konfiguration ist verbindlicher als
  veraltete Versionsangaben in beschreibenden Dokumenten; separat verwaltete
  optionale Presets bleiben zulässig.
- Vollständige öffentliche XML-Dokumentation bedeutet „wo anwendbar“; sie
  verlangt keine erfundenen `<returns>`- oder `<exception>`-Abschnitte.
- Ein späterer Plan darf die konkreten Prüfkommandos festlegen, aber Scope und
  Akzeptanz dieser Spezifikation nicht erweitern.

*The accepted intake and review artefacts remain unchanged. The root
constitution is canonical and its memory copy is a synchronized mirror. The
executable standard eight-preset configuration takes precedence over stale
descriptive version text, while separately governed optional presets remain
permitted. Complete public XML documentation means “where applicable” and
does not require invented return or exception sections. A later plan may choose
verification commands but may not expand this specification's scope.*

## Risiken / Risks

- Das Aktivieren der Dokumentations-Schranke kann mehr vorhandene Lücken zeigen
  als aus der aktuellen Unterdrückung sichtbar ist. Dieses Risiko bleibt im
  Feature-Scope, solange nur öffentliche Produkt-APIs betroffen sind.
- Eine breit formulierte XML-Regel könnte zu nutzlosen Kommentaren führen. Die
  Begrenzung auf öffentliche, anwendbare API-Flächen verhindert das.
- Atomare Parität über viele Guidance- und Template-Flächen kann Textdrift
  erzeugen. Semantische Homogenitätsprüfung und ein gemeinsamer Review sind
  deshalb Abnahmeschranken.
- DocFX kann formal erfolgreich sein, obwohl die Ausgabe für Hilfsmittel schwer
  nutzbar ist. Deshalb bleibt die textorientierte A11Y-Prüfung eigenständig.

*Enabling the documentation gate may reveal more existing public API gaps than
the suppression currently exposes. Over-broad XML rules can create useless
comments, so only applicable public API surfaces are covered. Atomic parity can
still drift semantically and therefore needs homogeneity review. A successful
DocFX process does not by itself prove accessibility, so the text-oriented
review remains a separate gate.*

## Test- und Evidenzstrategie / Test and Evidence Strategy

1. Normalisierte Hashes der vier akzeptierten Artefakte vor jeder
   Phasenfortsetzung vergleichen.
2. Intake-Klassifikation auf Vollständigkeit, Einzigkeit und erlaubte Werte
   prüfen; FR-001 bis FR-009 dürfen nur `Applicable`-Arbeit enthalten.
3. Constitution, Spiegel, Agentenflächen und betroffene Templates semantisch
   sowie mit vorhandener Homogenitätsprüfung vergleichen.
4. Build mit aktiver öffentlicher XML-Dokumentationsprüfung ausführen; keine
   projektweite CS1591-Unterdrückung und keine Dokumentationslücke zulassen.
5. Gesamte xUnit-Suite ausführen. Bei späterer Produktlogik gilt TDD Rot → Grün
   → Regression; für die reine Governance-Änderung wird TDD begründet als
   `N/A` geführt.
6. Bei API-/XML-Änderungen DocFX aus dem Repository-Hauptverzeichnis ausführen,
   repräsentative HTML-Seiten mit dem dokumentierten Playwright/axe-Pfad und
   zusätzlich textorientiert mit `lynx` prüfen.
7. Dokumentations-, Security-, A11Y-, Agentenparitäts- und Statistik-Evidenz
   vor Abschluss gegen diese Spec prüfen.

*Verification compares accepted normalized hashes, validates complete and
exclusive intake classification, checks semantic parity across governance
surfaces, builds with the public XML documentation gate active, runs the full
xUnit suite, applies or justifiably marks TDD, and runs DocFX plus the documented
Playwright/axe and lynx accessibility paths when API/XML content changes. Final
review reconciles documentation, security, accessibility, agent parity, and
statistics evidence against this specification.*

## Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: 100 % der 16 Intake-Positionen besitzen genau eine der erlaubten
  Einstufungen und 100 % der geplanten funktionalen Anforderungen stammen aus
  `Applicable`-Positionen.
- **SC-002**: 100 % der gepflegten Constitution-, Agenten- und betroffenen
  Template-Flächen enthalten semantisch dieselben neuen Regeln; es bleibt keine
  unbegründete Abweichung.
- **SC-003**: 100 % der öffentlichen Produkt-APIs bestehen die aktive
  Dokumentations-Schranke; keine produktweite Unterdrückung fehlender
  öffentlicher XML-Dokumentation bleibt bestehen.
- **SC-004**: Bei jeder geänderten API-Signatur oder jedem geänderten
  XML-Kommentar liegen genau ein erfolgreicher DocFX-Nachweis und ein
  erfolgreicher textorientierter A11Y-Nachweis aus demselben Arbeitsgegenstand
  vor.
- **SC-005**: 100 % der betroffenen neuen Funktionen oder Fehlerkorrekturen
  besitzen Rot-, Grün- und Regressions-Evidenz oder ein überprüfbares `N/A` für
  reine Governance-/Textarbeit.
- **SC-006**: Alle bestehenden automatisierten Tests bleiben erfolgreich, und
  die Projektstatistik enthält nach der Umsetzung genau einen neuen,
  chronologisch korrekt eingeordneten Fortschreibungseintrag.
- **SC-007**: Reviewer und Lernende können Scope, Nicht-Ziele, Reihenfolge,
  Status, Evidenz und nächste sichere Aktion vollständig aus Text entnehmen;
  keine wesentliche Entscheidung hängt nur von Farbe, Layout oder visueller
  Position ab.

*All 16 intake items have one allowed classification, and all planned work
comes from `Applicable` items. Every maintained governance surface carries the
same new rules. All public product APIs pass the active documentation gate.
Each applicable API/XML change has successful DocFX and text-oriented
accessibility evidence. Each affected feature or fix has TDD evidence or a
reviewable governance-only `N/A`. Existing tests remain successful, statistics
gain one correctly ordered entry, and all important decisions remain fully
understandable from text alone.*
