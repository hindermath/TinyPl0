# Feature-Spezifikation: Sandbox-gestützte sichere Entwicklung / Sandbox-Supported Secure Development

**Feature Branch**: `codex/005-sandbox-secure-development`
**Created**: 2026-08-30
**Status**: Draft
**Input**: Binding intake `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md`

## Benutzerszenarien und Tests / User Scenarios & Testing

### User Story 1 – Sichere Nutzungsentscheidung / Safe Usage Decision (Priorität: P1)

Als Projektverantwortung möchte ich eine nachvollziehbare Entscheidung, ob und unter welchen Bedingungen TinyPl0 mit der `absdd-image-sandbox` bearbeitet werden darf. Dadurch wird eine vorhandene Sandbox nicht mit einer bereits erteilten Betriebsfreigabe verwechselt.

*As project owner, I want a traceable decision on whether and under which conditions TinyPl0 may be worked on with `absdd-image-sandbox`. This prevents an available sandbox from being mistaken for an approved operating environment.*

**Why this priority**: Ohne technische Identität, Freigabe, begrenzte Mounts, Secret-Schutz und Netzwerkentscheidung darf kein sicherer Agentenlauf behauptet werden. / Without technical identity, approval, limited mounts, secret protection, and a network decision, no safe agent run may be claimed.

**Independent Test**: Eine prüfende Person kann die Entscheidungs- und Evidenzmatrix lesen und für jeden CL-12-Punkt genau einen Anwendbarkeitsstatus, einen Umsetzungsstatus, Evidenz oder eine offene Folgeaktion finden. / A reviewer can read the decision and evidence matrix and find exactly one applicability state, one implementation state, evidence or an open follow-up for every CL-12 item.

**Acceptance Scenarios**:

1. **Given** der unveränderte, versionierte Sandbox-Referenzstand ist bekannt, **when** Mounts, Schreibgrenzen, Secrets, Egress, Toolchains und Freigabe bewertet werden, **then** lautet die Entscheidung entweder freigegeben, bedingt als Pilot nutzbar oder nicht nutzbar und nennt ihre Bedingungen.
2. **Given** ein technischer oder menschlicher Nachweis fehlt, **when** das Ergebnis dokumentiert wird, **then** steht dort `Open` mit Rolle, nächster Maßnahme, Zieltermin und Neubewertungs-Trigger statt einer positiven Behauptung.

---

### User Story 2 – Verständlicher Arbeitsort / Understandable Work Location (Priorität: P2)

Als lernende oder entwickelnde Person möchte ich für Build, Test, Dokumentation, Smoke-Checks, Providerzugriff und Review erkennen, welche Arbeit in der Sandbox, lokal oder in CI stattfinden soll. Dadurch kann ich sicher arbeiten, ohne unnötige Hürden oder unklare Zuständigkeiten.

*As an apprentice or developer, I want to know whether build, test, documentation, smoke checks, provider access, and review belong in the sandbox, locally, or in CI. This lets me work safely without unnecessary barriers or unclear responsibilities.*

**Why this priority**: Die Sicherheitsgrenze muss praktisch nutzbar und für Personen ohne Spec-Kit-Vorerfahrung verständlich sein. / The security boundary must be practical and understandable to people without prior Spec Kit experience.

**Independent Test**: Für jede Arbeitsart nennt die Matrix einen bevorzugten Ausführungsort, Voraussetzungen, erlaubte Schreibziele, erwartete Evidenz und einen sicheren Rückfallweg. / For each work type, the matrix names a preferred execution location, prerequisites, permitted write targets, expected evidence, and a safe fallback.

**Acceptance Scenarios**:

1. **Given** eine Person möchte TinyPl0 bauen oder testen, **when** sie die Arbeitsmatrix liest, **then** erkennt sie die nötige .NET-Basis, die erlaubte Projektgrenze und ob reale Sandbox-Evidenz bereits vorliegt oder noch `Open` ist.
2. **Given** ein Werkzeug oder eine Toolchain fehlt, **when** die Arbeit weiterhin lokal oder in CI sicher möglich ist, **then** beschreibt die Matrix diesen Rückfallweg ohne eine Image-Änderung zu verlangen.

---

### User Story 3 – Auditfähige Folgearbeit / Audit-Ready Follow-Up (Priorität: P3)

Als Reviewer möchte ich alle offenen Sandbox-, Supply-Chain-, Sicherheits- und Freigabepunkte mit Owner und Zielnachweis sehen. Dadurch kann eine spätere technische Härtung als eigener Lauf geplant werden, ohne den aktuellen Scope auszuweiten.

*As a reviewer, I want all open sandbox, supply-chain, security, and approval items to show an owner and target evidence. This allows later technical hardening to be planned as a separate run without expanding the current scope.*

**Why this priority**: Offene Punkte bleiben sichtbar, aber dieser Lauf verändert weder Produktcode noch Sandbox-Image. / Open items remain visible, while this run changes neither product code nor the sandbox image.

**Independent Test**: Jede offene Zeile besitzt Risiko, Priorität, verantwortliche Rolle, nächste Maßnahme, Zieltermin und einen Auslöser zur Neubewertung. / Every open row has risk, priority, responsible role, next action, due date, and a re-evaluation trigger.

**Acceptance Scenarios**:

1. **Given** eine formelle Sandbox-Freigabe kann nur ein Mensch erteilen, **when** die Bewertung abgeschlossen wird, **then** bleibt diese Entscheidung `Open` und wird nicht durch Agenten-, CI- oder Admin-Bypass-Evidenz ersetzt.
2. **Given** eine technische Härtung wäre sinnvoll, **when** sie nicht zur Dokumentationsbewertung dieses Features gehört, **then** wird sie als separate Folgeaufgabe notiert und nicht umgesetzt.

### Randfälle / Edge Cases

- Der Sandbox-Referenz-Checkout enthält nicht übernommene Änderungen. Nur ein benannter, unveränderter Commit darf als Beobachtungsbasis dienen; Arbeitskopie-Änderungen sind keine TinyPl0-Evidenz.
- Image-Digest, Freigabe, Ablaufdatum oder Datenklassifikation fehlen. Die Betriebsentscheidung bleibt `Open` oder auf einen klar begrenzten Pilotbetrieb beschränkt.
- Ein Mount würde ein Home-Verzeichnis, Schlüsselbund, Browserprofil, SSH-/GPG-Agent, Cloud-CLI-Profil oder lokalen Token-Speicher sichtbar machen. Der Mount ist unzulässig und der Lauf muss vor Secret-Zugriff stoppen.
- Eine Toolchain ist im Referenz-Image beschrieben, aber für TinyPl0 nicht auf einem akzeptierten Image-Stand ausgeführt worden. Die Fähigkeit ist plausibel, die Projektausführung bleibt jedoch `Open`.
- Freier Egress ist dokumentiert, aber nicht aktuell menschlich akzeptiert. Netzwerkzugriff darf nicht als vollständig erfüllt bewertet werden.
- Ein lokaler oder CI-Rückfallweg funktioniert, während die Sandbox nicht verfügbar ist. Der Rückfallweg bleibt erlaubt, erzeugt aber keine Sandbox-Evidenz.
- Der fachliche Vorgänger liegt nach seinem Abschluss im Archiv statt am historischen aktiven Pfad. Die aktuelle Serienreview und der byte-identische Archivpfad sind maßgeblich; die aktive Datei wird nicht wiederhergestellt.

## Anforderungen / Requirements

### Funktionale Anforderungen / Functional Requirements

- **FR-001**: Der Lauf MUSS den Binding Intake vollständig und ausschließlich für Feature 005 umsetzen; kein weiterer Intake darf gestartet werden.
- **FR-002**: Der Lauf MUSS alle zwölf Prüfpunkte `CL-12-01` bis `CL-12-12` mit genau einem Wert `Applicable`, `N/A` oder `Open` und genau einem Umsetzungsstatus bewerten.
- **FR-003**: Jede CL-12-Bewertung MUSS Lernstufe, verantwortliche Rolle, Begründung, Evidenzpfad oder Nachweisziel, Restrisiko, Neubewertungs-Trigger sowie bei offenen oder unvollständigen Punkten eine nächste Maßnahme mit Zieltermin enthalten.
- **FR-004**: Der Lauf MUSS eine projektneutrale Mount-Matrix mit symbolischen Pfaden erstellen. Sie nennt Quelle, Sandbox-Ziel, Zweck, Lese-/Schreibrecht und verbotene Nachbarbereiche, aber keinen privaten Host-Pfad.
- **FR-005**: Schreibrechte für Agenten MÜSSEN auf den TinyPl0-Projekt-Mount und ausdrücklich notwendige, getrennte Build- oder Audit-Speicher begrenzt werden. Nutzerprofile, Caches, Sitzungsdaten und Zugangsdaten bleiben außerhalb des Repository-Inhalts.
- **FR-006**: Der Lauf MUSS Build, Test, Coverage, Dokumentationsbau, A11Y-Prüfung, Golden-Update und relevante Smoke-Checks jeweils als Sandbox-, Lokal- oder CI-Arbeit einordnen und Voraussetzungen sowie Evidenzgrenzen nennen.
- **FR-007**: Eine beschriebene Toolchain oder ein vorhandener Befehl DARF NICHT als erfolgreich für TinyPl0 bewertet werden, solange keine passende Ausführung auf einem akzeptierten Sandbox-Stand vorliegt.
- **FR-008**: Der Lauf MUSS SBOM-, Dependency-, Scan-, VEX-, Provenienz-, OpenSSF- und Review-Nachweise für TinyPl0 und für das verwendete Sandbox-Image trennen.
- **FR-009**: Der Lauf MUSS eine klare Nutzungsentscheidung für `absdd-image-sandbox` liefern. Der Ausgangspunkt ist `bedingt als Pilot nutzbar`; eine reguläre Freigabe erfordert mindestens unveränderliche Image-Identität, gültige menschliche Freigabe, genehmigte Mount-Liste, Secret-Injektion außerhalb des Repositories, aktuelle Egress-Entscheidung und erfolgreiche TinyPl0-Baseline-Prüfung.
- **FR-010**: Der Lauf MUSS den Sandbox-Referenzstand `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0` als read-only Beobachtungsbasis kennzeichnen. Nicht übernommene Änderungen des Referenz-Repositories dürfen weder übernommen noch als erfüllt angerechnet werden.
- **FR-011**: Der Lauf MUSS die fachliche Vorgängerlinie zum byte-identischen Archiv `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md` bewahren und die aktuelle Ready-Serienreview als Pfadauflösungsautorität verwenden.
- **FR-012**: Der Lauf DARF keinen Produktcode, kein Sandbox-Image, keine Sandbox-Konfiguration und keine bestehende Datei unter `docs/security/` automatisch härten oder ausfüllen.
- **FR-013**: Die Ergebnisdokumente MÜSSEN in den Feature-Artefakten liegen: `sandbox-assessment.md`, `evidence-matrix.md` sowie die Spec-Kit-Spezifikation, Planung, Aufgaben und Checklisten. Spätere Zielpfade unter `docs/security/` werden nur benannt. Außerhalb des Feature-Verzeichnisses sind nur das Statistikledger mit seiner Renderer-Konfiguration und verpflichtende IDE-Versionsmetadaten zulässig.
- **FR-014**: Alle `N/A`-Entscheidungen MÜSSEN eine konkrete Begründung und einen Neubewertungs-Trigger enthalten. Alle `Open`-Entscheidungen MÜSSEN Owner, Risiko, Folgeaktion, Zieltermin und erwartete Evidenz nennen.
- **FR-015**: Nutzerseitige Ergebnisse MÜSSEN Deutsch zuerst und Englisch danach auf ungefähr CEFR B2 anbieten, Status nicht nur visuell vermitteln und mit Bildschirmleser, Braillezeile und Textbrowser verständlich bleiben.
- **FR-016**: Vor Remote-Lieferung MUSS ein Secret- und Privatpfad-Check der beabsichtigten Delivery-Menge ohne Lesen nicht freigegebener Secret-Dateien erfolgreich sein.
- **FR-017**: Der Lauf MUSS nach erfolgreicher Produktlieferung die kausale Serienfortschreibung durchführen: Intake byte-identisch archivieren, Manifest/Receipt nachvollziehbar fortschreiben, den nächsten Status seriell ermitteln und kein Folgefeature innerhalb desselben Laufs starten.

### Verfassungsanforderungen / Constitution Requirements

- **CR-001**: Verbindlicher Level-2-Kontext ist die TinyPl0-Zeile des Project Environment Registry: .NET 10/C# 14; `dotnet restore/build/test`, Coverage und Golden-Skript; DE-first/EN-second und WCAG-2.2-AA-orientierte Dokumentation; Statistikbasen 80/125; definierte Agentenflächen und verbotener privater `.codex`-Zustand.
- **CR-002**: C# 14/.NET 10 ist eine speichersichere Primärlaufzeit. Das entbindet nicht von sicheren Datei-, Prozess-, Abhängigkeits-, Logging- oder Eingabegrenzen.
- **CR-003**: `NIST SSDF` und `CWE Top 25` sind `Applicable`. Die Bewertung betrachtet besonders Quell-/Build-Schutz, Least Privilege, sichere Defaults, Pfadgrenzen, Secret-Offenlegung und übermäßige Agentenrechte.
- **CR-004**: Nutzerseitige Artefakte verwenden Text als vollständigen Informationskanal. WCAG 2.2 Level AA gilt soweit auf Markdown und später erzeugte HTML-Dokumentation anwendbar.
- **CR-005**: Shared Agent Guidance und Constitution-Templates werden nicht geändert. `docs/project-statistics.md` und die zugehörige versionierte Renderer-Konfiguration werden nach der abgeschlossenen Implementierungsphase gemäß Repository-Regel aktualisiert.

### Anwendbarkeit von Sicherheitsstandards / Security Standards Applicability

| Standard oder Kontrolle | Entscheidung | Begründung, Evidenzziel und Neubewertung |
|---|---|---|
| NIST SSDF SP 800-218 | `Applicable` | Gilt für jedes Level-2-Projekt. Feature-Artefakte ordnen Prepare/Protect/Produce/Respond den Sandbox-Grenzen zu. Neubewertung bei Workflow- oder Sandbox-Wechsel. |
| CWE Top 25 | `Applicable` | Pfadmanipulation, Offenlegung sensitiver Information, unkontrollierte Ressourcen und Rechteausweitung sind für Mounts und Agenten relevant. Neubewertung bei neuer Schnittstelle oder Härtungsaufgabe. |
| OWASP ASVS 5.0.0 | `N/A` | Dieses Feature ändert keinen Web-, API-, HTTP-, Authentifizierungs- oder Autorisierungsdienst. Neubewertung bei entsprechendem Produktscope. |
| SBOM | `Applicable` | TinyPl0 ist releasefähig; Image- und Produkt-SBOM bleiben getrennt. Späterer Zielpfad: `docs/security/supply-chain-evidence.md`. Neubewertung je Release oder Image-Wechsel. |
| VEX | `Open` | Nur bei bekannten Schwachstellen erforderlich. Der aktuelle Lauf erzeugt keinen neuen Scan; spätere Feststellungen werden in der Supply-Chain-Evidenz bewertet. Owner: Security-/Release-Review; Termin: vor nächstem Release. |
| AI-SBOM | `N/A` | KI ist Entwicklungswerkzeug, kein ausgelieferter oder betriebener TinyPl0-Produktbestandteil. Neubewertung bei Aufnahme eines Modells, KI-Dienstes oder einer Inferenzkomponente ins Produkt. |
| SLSA v1.2 | `Applicable` | TinyPl0 besitzt CI-/Release-Artefakte; Provenienz bleibt ein Zielmodell. Späterer Zielpfad: `docs/security/supply-chain-evidence.md`. Neubewertung bei Release-Pipeline-Änderung. |
| OpenSSF Scorecard | `Applicable` | TinyPl0 ist ein öffentliches OSS-Repository. Das Feature benennt die spätere Release-Evidenz, führt aber keine technische Härtung aus. Neubewertung vor Release. |
| OWASP SAMM | `Applicable` | TinyPl0 ist langlebig. Bestehender Zielpfad: `docs/security/samm-assessment.md`; Sandbox-Folgen werden als offene Verbesserung eingeordnet. Neubewertung im regulären Review-Zyklus. |
| STRIDE, CIA und CAPEC | `Applicable` | Host-Mount, Agentenprozess, Tool-Volumes, Secret-Injektion, Netzwerk und CI sind Entwicklungs-Trust-Boundaries. Die Feature-Matrix dokumentiert Risiken; Produkt-Threat-Model bleibt unverändert. |
| NIST Zero Trust SP 800-207 | `N/A` | TinyPl0 bleibt ein lokaler Compiler/VM/CLI/IDE und dieses Feature führt keinen verteilten Produktdienst ein. Neubewertung bei Cloud-, Remote- oder Identitätsföderationsscope. |
| BSI C3A / BSI C5 | `N/A` | Es wird kein Cloud-Service ausgewählt, bereitgestellt oder als Produktabhängigkeit eingeführt. Externe KI-Provider bleiben außerhalb der Produktlaufzeit. Neubewertung bei verbindlicher Cloud-/Provider-Architektur. |
| NIS2, CRA, EU AI Act und DORA | `N/A` | Das Feature ändert weder Marktbereitstellung noch regulierten Betrieb, KI-Produktbestandteil oder Finanzsektor-Lieferkette. Neubewertung bei neuem Vertriebs-, Kunden- oder Betriebskontext. |

### Governance-Presets / Governance Presets

Alle acht installierten Presets gelten als Prüfkontext: `security-governance` 0.6.2, `architecture-governance` 0.5.2, `isaqb-architecture-governance` 0.2.2, `a11y-governance` 0.4.3, `cross-platform-governance` 0.2.2, `agent-parity-governance` 0.4.2, `autonomous-run-governance` 0.4.1 und `parallel-autonomous-run-governance` 0.2.6. Installation erteilt keine Ausführungs-, Provider-, Secret-, Remote- oder Merge-Berechtigung.

### Architektur-Anwendbarkeit / Architecture Applicability

| Bereich | Entscheidung | Begründung und Evidenz |
|---|---|---|
| Produktkontext, Building Blocks und Laufzeit | `N/A` | Compiler, VM, CLI und IDE werden nicht geändert. Neubewertung bei Produktcode- oder Deployment-Änderung. |
| Entwicklungs-Trust-Boundaries | `Applicable` | Host↔Container-Mount, getrennte Tool-/Build-Volumes, Secret-Injektion, Egress sowie Git/CI-Übergabe werden in `sandbox-assessment.md` beschrieben. |
| Datenklassen | `Applicable` | Öffentlicher Quellcode/Dokumentation sind vom internen Toolzustand und von eingeschränkten Secrets klar zu trennen. Produktivdaten und besondere personenbezogene Daten sind nicht zugelassen. |
| Produkt-Threat-Model, arc42 und S-ADR | `N/A` | Es wird keine Produktarchitektur geändert und keine reguläre Sandbox-Freigabe erteilt. Neubewertung, sobald eine dauerhafte Sandbox-Architektur verbindlich eingeführt wird. |
| Sicherheits-Qualitätsszenario | `Applicable` | Ein nachvollziehbares Szenario prüft, dass ein Agent nur den freigegebenen Projektbereich ändern kann und bei sichtbaren Secrets oder unerwarteten Schreibpfaden stoppt. |
| Allgemeine Architektur-Evidenz unter `docs/architecture/` | `N/A` | Feature-lokale Bewertung genügt für die Dokumentationsentscheidung. Neubewertung bei technischer Härtung oder dauerhafter Deployment-Änderung. |

### Barrierefreiheit / Accessibility Applicability

- Betroffene Artefakte sind die neue Feature-Spezifikation, Planung, Aufgaben, Checklisten, Bewertung und Evidenzmatrix; Produkt-CLI, IDE und generiertes HTML bleiben unverändert.
- Deutsch steht zuerst, Englisch danach; Überschriften folgen soweit passend dem Muster `DE / EN`. Zielgruppe sind Auszubildende ab dem ersten Ausbildungsjahr, Entwickler*innen, Reviewer und KI-Agenten ohne vorausgesetzte Spec-Kit-Erfahrung.
- Status, Abhängigkeiten, Entscheidung und nächste Aktion stehen vollständig im Text. Tabellen besitzen semantische Überschriften und Statuswörter; Farbe oder räumliche Lage tragen keine alleinige Bedeutung.
- Codeblöcke erhalten Sprachkennzeichen. Bilder sind nicht geplant. ASCII-Diagramme benötigen eine direkt angrenzende DE/EN-Erklärung.
- Didaktische Inline-Code-Kommentare sind `N/A`, weil keine Programmlogik geändert wird. Neubewertung bei späterer technischer Härtung.
- Eine Änderung unter `docs/accessibility/` ist `N/A`, weil keine Produkt- oder Website-Oberfläche geändert wird. Die Feature-Checkliste ist der Nachweis; Neubewertung bei DocFX-, CLI- oder IDE-Änderung.

### Cross-Platform-Anwendbarkeit / Cross-Platform Applicability

Dieses Feature fügt kein skriptförmiges Werkzeug hinzu, ändert keines und entfernt keines. Bash-/PowerShell-Parität, Manpage, Cmdlet-Name und Dry-Run-Parität sind daher jeweils `N/A`. Neubewertung erfolgt, sobald eine Folgeaufgabe ein Sandbox-Start-, Prüf- oder Exportskript in TinyPl0 einführt. Die Arbeitsmatrix muss vorhandene macOS-, Linux-, Windows- und CI-Pfade dennoch verständlich unterscheiden.

### Agent-Parität / Agent Parity Applicability

Gemeinsame Agentenanweisungen, `.specify/memory/constitution.md`, Projektvorlagen und Model-Routing-Guidance werden nicht geändert; synchrone Agent-Parität ist daher `N/A`. Die gepflegten Flächen `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.github/agents/copilot-instructions.md` bleiben unverändert. Neubewertung bei einer späteren Regeländerung.

### Dokumentationswirkung / Documentation Impact

**Entscheidung: `UpdateRequired`.** Betroffen sind nur die neuen Feature- und Review-Artefakte für Auszubildende, Entwickler*innen, Reviewer und KI-Agenten. Leserpfad: `spec.md` → `plan.md` → `tasks.md` → `sandbox-assessment.md` → `evidence-matrix.md`. Kanonische Quelle und Owner sind der Binding Intake sowie die TinyPl0-Projektverantwortung. Es gibt keine Produktnavigation und keinen DocFX-Navigationseingriff. Dokumentklasse ist Feature-lokale Governance- und Ausbildungsdokumentation; Sprachstrategie ist DE-first/EN-second in derselben Datei. Plattformbeispiele verwenden symbolische Projektpfade und unterscheiden Sandbox, lokal und CI. Verteilungsklasse ist öffentlich repository-tauglich. Home-Sync ist nicht erforderlich. Evidenz liefern die Feature-Checklisten und der Secret-/Privatpfad-Check. Neubewertung erfolgt bei einer späteren technischen Sandbox- oder Produktänderung.

### Autonomous-run-Anwendbarkeit / Autonomous Run Applicability

- Delivery mode ist `MergeAndSync` aufgrund der aktuellen ausdrücklichen Nutzerautorität. Ein eng begrenzter Admin-Bypass darf nur einen konkret belegten Plattform-Policy-Blocker nach vollständigen technischen Gates behandeln; er ist kein Review und keine Approval.
- Akzeptierte Eingaben sind der Binding Intake mit SHA-256 `628f869c9df39329949b73457bd56d4345f467ef38d453f257887d07b8f58735`, die Ready-Serienreview sowie Manifest und Request mit ihren im Run-State festgehaltenen Hashes.
- Keine Secret-, Provider-, Image-Änderungs-, Folgefeature- oder Parallelisierungsbefugnis wird erteilt. Modellbezeichner sind Laufzeitevidenz und keine Feature-Anforderung.
- Der Zustand liegt unter `specs/005-sandbox-secure-development/autonomous-run-state.json`; absichtlicher Stopp erfolgt nur an einer Phasengrenze. Unerwartete Unterbrechung verlangt Drift-, Hash-, Authority- und Remote-Revalidierung vor Resume.
- Mutable Validation Tokens sind für Dokumentationsprüfungen `N/A`; versionierte Build-/Test-Aufrufe dürfen nur nach der Repository-Versionsregel erfolgen.
- Kausaler Closeout ist nach Merge erforderlich, weil der Intake anschließend byte-identisch archiviert und die kanonische Serie fortgeschrieben werden muss.

### Stabile Akzeptanz-Gates / Stable Acceptance Gates

| Gate | Status | Erforderlicher Scope und Nachweis | Neubewertungs-Trigger |
|---|---|---|---|
| `SBX-G001` Intake-Identität | `Applicable` | Binding-Intake-, Review-, Request- und Manifest-Hash stimmen. | Jede Änderung an einem akzeptierten Artefakt. |
| `SBX-G002` CL-12-Vollständigkeit | `Applicable` | 12/12 eindeutige Bewertungen mit Pflichtfeldern. | CL-12-Version oder Bewertung ändert sich. |
| `SBX-G003` Mount-/Schreibgrenze | `Applicable` | Symbolische Mount-Matrix; keine Home-, Profil- oder Secret-Mounts. | Mount- oder Agentenkonfiguration ändert sich. |
| `SBX-G004` Arbeitsort-Matrix | `Applicable` | Build/Test/Docs/A11Y/Smoke/Review sind Sandbox, lokal oder CI zugeordnet. | Toolchain oder CI ändert sich. |
| `SBX-G005` Nutzungsentscheidung | `Applicable` | Bedingung, Restrisiko, Owner und fehlende Freigaben sind klar. | Image, Freigabe oder Datenklasse ändert sich. |
| `SBX-G006` Standards | `Applicable` | Alle Sicherheits-, Architektur-, A11Y- und Governance-Entscheidungen sind sichtbar. | Constitution oder Preset ändert sich. |
| `SBX-G007` Scope-Grenze | `Applicable` | Kein Produktcode, Sandbox-Repo, Sandbox-Image oder `docs/security/`-Nachweis wird automatisch geändert. | Delivery-Menge ändert sich. |
| `SBX-G008` Secret-/Privatpfad-Schutz | `Applicable` | Secret-Scan und Pfadprüfung der Delivery-Menge sind unauffällig. | Jede Delivery-Änderung. |
| `SBX-G009` A11Y/Sprachqualität | `Applicable` | DE/EN, CEFR B2, Text-first und semantische Markdown-Prüfung bestehen. | Nutzerseitiger Text ändert sich. |
| `SBX-G010` Lokale Validierung | `Applicable` | Dokument-, Schema-, Link- und Delivery-Validatoren bestehen; Produkt-Build nur wenn tasks.md ihn verlangt. | Kandidaten-Head ändert sich. |
| `SBX-G011` Exact-Head-Review | `Applicable` | PR-Head, Checks, Threads und unabhängige menschliche Approval sind aktuell. COMMENTED, unavailable oder Bypass zählen nicht. | Head oder Reviewzustand ändert sich. |
| `SBX-G012` Merge/Sync/Closeout | `Applicable` | Autorisierter Merge, synchrones `main`, byte-identisches Intake-Archiv und Serienfortschreibung. | Remote- oder Lifecycle-Zustand ändert sich. |

### Schlüsselentitäten / Key Entities

- **Sandbox-Nutzungsentscheidung / Sandbox Usage Decision**: Ergebnis, Betriebsmodus, Voraussetzungen, Restrisiko, Owner, Ablauf und Neubewertung.
- **CL-12-Bewertung / CL-12 Assessment**: stabile ID, Anwendbarkeit, Umsetzungsstatus, Lernstufe, Rolle, Begründung, Evidenz, Risiko, Trigger und Maßnahme.
- **Mount-Grenze / Mount Boundary**: symbolische Quelle, Sandbox-Ziel, Zweck, Rechte und verbotene Nachbarbereiche.
- **Arbeitsort / Work Location**: Sandbox, lokaler Host oder CI mit Voraussetzung, Rückfallweg und Nachweisgrenze.
- **Folgeaufgabe / Follow-Up**: offene, ausdrücklich nicht umgesetzte Härtung mit Owner, Priorität, Zieltermin und erwarteter Evidenz.

## Erfolgskriterien / Success Criteria

### Messbare Ergebnisse / Measurable Outcomes

- **SC-001**: 12 von 12 CL-12-Prüfpunkten besitzen genau eine vollständige und widerspruchsfreie Bewertung.
- **SC-002**: 100 % der genannten Standards und Governance-Bereiche sind `Applicable`, `N/A` oder `Open`; keine stille Auslassung bleibt.
- **SC-003**: 100 % der offenen Punkte besitzen Owner, Risiko, nächste Maßnahme, Zieltermin, erwartete Evidenz und Neubewertungs-Trigger.
- **SC-004**: Eine neue lernende oder prüfende Person kann innerhalb von fünf Minuten die aktuelle Nutzungsentscheidung, erlaubte Schreibgrenze und drei Stop-Bedingungen aus den Feature-Artefakten bestimmen.
- **SC-005**: Die versionierte Delivery-Menge enthält null Secret-Funde, null konkrete private Host-Pfade und null Agentenprofile, Caches, Sitzungs- oder Zugangsdaten.
- **SC-006**: Produktcode, Sandbox-Image, Sandbox-Repository und bestehende `docs/security/`-Nachweise weisen gegenüber dem Feature-Baseline-Commit null unbeauftragte Änderungen auf.
- **SC-007**: Die endgültige Entscheidung unterscheidet klar zwischen verfügbarer Technik, nachgewiesener TinyPl0-Ausführung und menschlicher Betriebsfreigabe; keine der drei Ebenen wird gleichgesetzt.

## Annahmen / Assumptions

- Der verbindliche Sandbox-Kontext ist der versionierte Referenzstand, nicht seine aktuell veränderte Arbeitskopie.
- Das .NET-10-SDK im Referenz-Image macht TinyPl0-Builds grundsätzlich plausibel; tatsächlicher Erfolg bleibt bis zur akzeptierten Sandbox-Ausführung `Open`.
- Die aktuelle Aufgabe bewertet und dokumentiert. Technische Härtung, Image-Änderungen, formelle Freigaben und Providerentscheidungen folgen nur in getrennten, ausdrücklich autorisierten Aufgaben.
- Symbolische Pfade wie `<TinyPl0-repository>` ersetzen lokale absolute Host-Pfade in allen versionierten Artefakten.
- Die Ready-Serienreview bestätigt, dass der abgeschlossene fachliche Vorgänger byte-identisch im Archiv liegt; sein historischer aktiver Pfad wird nicht wiederhergestellt.
