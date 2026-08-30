# Klärungsbericht / Clarification Report

**Datum / Date**: 2026-08-30

**Feature / Feature**: `004-secure-development-hardening`

**Geprüfte Spezifikation / Reviewed specification**:
`specs/004-secure-development-hardening/spec.md`

**Bindender Intake / Binding intake**:
`requirements/intakes/active/Lastenheft_Secure-Development-Hardening.md`

## Ergebnis / Result

Die materielle Klärung ist abgeschlossen. Es wurden null Nutzerfragen gestellt,
weil alle entscheidenden Fakten aus Intake, Serienmanifest, Review, Run-State,
Constitution, Secure-Development-Basis, Code, Workflows und vorhandener Evidenz
ermittelt werden konnten. Die Spezifikation wurde nur dort geändert, wo eine
Planungs- oder Abnahmeentscheidung sonst mehrdeutig geblieben wäre.

*Material clarification is complete. Zero user questions were asked because
all decisive facts were discoverable from the intake, series manifest, review,
run state, constitution, secure-development baseline, code, workflows, and
existing evidence. The specification was changed only where planning or
acceptance would otherwise have remained ambiguous.*

Es bleibt keine materielle Planungsunklarheit und kein zurückgestellter
Klärungspunkt. Die nächste erlaubte Phase ist die dedizierte Checklist-Phase,
nicht unmittelbar Plan und nicht der Sandbox-Folge-Intake.

*No material planning ambiguity or deferred clarification remains. The next
permitted phase is the dedicated checklist phase, not planning directly and
not the sandbox follow-up intake.*

## Gebundene Eingaben und Laufzustand / Bound Inputs and Run State

| Nachweis / Evidence | Ergebnis / Result |
|---|---|
| Branch | `codex/004-secure-development-hardening` bestätigt / confirmed |
| Run-ID | `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7` |
| Phase | `Clarify`, Status `Active`; `clarify` war `Running` beim Phasenstart / was `Running` at phase start |
| Intake SHA-256 | `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de` bestätigt / confirmed |
| Review-Ergebnis SHA-256 | `acdcf2dcb7411be6fa3389cf642748fcb1225e9bcbcf32e6bad8a76da54314fe` bestätigt / confirmed |
| Review-Anfrage SHA-256 | `49cddf9ce3391048a12fc4314f1ef2cdf4c500de73956623875a916cde1f3c50` bestätigt / confirmed |
| Serienmanifest SHA-256 | `1ca91db4ec4970c45a7c27b8623d03c29f52c9295305f8ee7d574b23d3f6cadf` bestätigt / confirmed |
| Run-State-Validator | `PASS`, Feature `specs/004-secure-development-hardening`, Stage `Clarify`, Status `Active`, Tasks `0/0` |

Das Prerequisite-Skript wurde wie vorgeschrieben einmal ausgeführt. Wegen des
Slash im Branchnamen meldete es den nicht vorhandenen Pfad
`specs/codex/004-secure-development-hardening`. Der explizite Feature-Pfad aus
`.specify/feature.json`, dem autonomen Run-State und dem Phasenauftrag stimmt
dagegen überein und ist maßgeblich. Das Skript wurde nicht erneut ausgeführt.

*The prerequisite script was run once as required. Because the branch name
contains a slash, it reported the non-existent path
`specs/codex/004-secure-development-hardening`. The explicit feature path in
`.specify/feature.json`, the autonomous run state, and the phase request agree
and therefore govern. The script was not rerun.*

## Aufgelöste Punkte / Resolved Points

| Thema / Topic | Lokale Evidenz / Local evidence | Verbindliche Klärung / Binding clarification |
|---|---|---|
| 157 Checklisten-IDs | Zwölf kanonische Dateien enthalten eindeutige ID-Anzahlen `12/13/15/10/13/11/12/13/17/17/12/12`; Summe `157`. Der Sammelband besitzt dieselbe eindeutige Menge; Mengendifferenz `0`. | `157` ist ein belegtes Snapshot-Bestandsmerkmal, keine Schätzung und keine Vorabfreigabe für 157 Produktänderungen. Jede ID wird klassifiziert. |
| Pflichtfelder der 157-ID-Matrix | Die alte Spec nannte sieben Metadatenfelder, später aber „acht Pflichtfelder“. | FR-001 definiert nun zehn Pflichtspalten einschließlich CL-ID und beider Statusachsen; unvollständige Zustände brauchen zusätzlich Priorität, Termin/Trigger und Evidenzziel. |
| CL-12 und nächster Intake | Das Serienmanifest führt `Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` direkt danach als `Blocked` mit bindendem `HardCompletionGate`. | Alle zwölf CL-12-IDs bleiben zur Vollständigkeit in der Matrix, sind hier aber `N/A`/`Not Assessed` und verweisen auf den getrennten Folge-Intake. Keine Sandbox-, Mount-, Host-, Netzwerk- oder Agentenhärtung wird vorgezogen. |
| Zeitpunkt der Requirements-Checkliste | `checklists/requirements.md` existiert als Specify-Qualitätsevidenz; der Run-State ordnet eine eigene `checklist`-Phase nach `clarify` und vor `plan` an. | Die vorhandene Checkliste ist Eingangsevidenz. Nach den Spec-Korrekturen muss die dedizierte Checklist-Phase sie gegen den neuen Spec-Stand revalidieren; erst danach darf Plan beginnen. |
| Phasenreihenfolge | Run-State: `specify → clarify → checklist → plan → plan-review → tasks → analyze → implement`. | Die Spec nennt diese Reihenfolge jetzt ausdrücklich. Clarify erzeugt nur minimale Spec-Korrekturen, diesen Bericht und das strukturierte Phasenergebnis. |
| ASVS-Scope | `src/Pl0.Cli/Program.cs` bindet `pl0c --api` an `http://localhost:5000` und liefert statische Dateien aus. Es gibt keine Authentifizierung, Sitzung, Mehrbenutzerrolle oder fachliche Datenänderung. | ASVS 5.0.0 L1 gilt für den begrenzten `--api`-Server. Der vollständige L1-Katalog wird ID-genau auf `Applicable` oder begründet `N/A` gemappt; alle anwendbaren IDs müssen erfüllt sein. GitHub Pages ist nicht Teil dieses ASVS-Produktscopes. |
| Release und SBOM | Release Please, CI-Artefakt-Upload und GitHub Pages existieren; ein maschinenlesbarer SBOM-Nachweis fehlt. CI-Test-/Coverage-Artefakte sind keine Veröffentlichung. | Der Lauf muss einen reproduzierbaren Erzeugungs-, Validierungs- und Veröffentlichungsanschlusspfad für den tatsächlich vorgesehenen Release-/Pages-Artefaktsatz belegen. Er muss keinen Release auslösen und keine historischen Releases nacharbeiten. VEX entsteht bei bekannten Funden. |
| Coverage-Schwellen | Letzte Repository-Evidenz: `70,23 %` Gesamt-Zeilen-Coverage. Spec-Kit-Templates verwenden `>=70 %` als Minimum und `>=80 %` als Ziel; die Secure-Development-Basis fordert höhere Schwellen für geänderte sicherheitsrelevante Flächen. | Abschluss: mindestens 70 %, keine Absenkung gegenüber dem belegten Wert 70,23 %. 80 % bleibt auditfähiges Ziel; eine Lücke bleibt `Open` statt fachfremde Tests zu erzwingen. Geänderte sicherheitskritische Module: mindestens 85 % Branch; KI-erzeugter geänderter Code: mindestens 80 % Linie und Branch. |
| Ausführungsressourcen | `VirtualMachineOptions` besitzt `StackSize = 500`, aber kein Instruktionsbudget; normale und schrittweise VM können nicht terminierende Programme ausführen. | Positives konfigurierbares Instruktionsbudget, Produktstandard `1_000_000`; Abbruch vor Instruktion `N+1`. Nichtpositive Budgets und ungültige Stackgrößen werden vor Allokation/Ausführung diagnostiziert. Beide VM-Wege zählen gleich; Tests prüfen kleine explizite `N` an der Grenze. Keine wall-clock-basierte Sicherheitsbehauptung. |
| Produktlogik gegenüber Evidenzarbeit | Der Intake verlangt zuerst Prüfung und projektspezifische Evidenz; Änderungen an Tests/CI nur aus konkreten Befunden. | Vorab begründete Produktarbeit ist auf Instruktionsbudget und fail-safe VM-Konfigurationsprüfung begrenzt. Weitere Code-, UI-, CI- oder Workflow-Änderungen brauchen einen nachgewiesenen `Applicable`-Befund mit `Partly Fulfilled`/`Not Fulfilled`, Risiko, Test und kleinster Maßnahme. |
| Evidenz- und Risiko-Ownership | Vorher waren Owner und Reviewer nur allgemein benannt. | Autor/Agent erzeugt Evidenz; die Repository-Rolle besitzt Maßnahme und Pflege; eine getrennte Reviewer-Rolle prüft. CI-/Toolbelege werden an Commit und Konfiguration gebunden. Rechtliche, providerbezogene sowie kritische/hohe Risikoakzeptanz darf nur die Maintainer-Rolle schriftlich und befristet erteilen; ohne sie bleibt die Freigabe blockiert. |

## Standards-Anwendbarkeit / Standards Applicability

- `NIST SSDF SP 800-218` und `CWE Top 25`: immer `Applicable` für Level 2.
- `OWASP ASVS 5.0.0 L1`: `Applicable` ausschließlich für den lokal gebundenen,
  statischen `pl0c --api`-HTTP-Scope mit vollständigem L1-ID-Mapping.
- `SBOM`: `Applicable` für tatsächlich veröffentlichte Artefaktsätze;
  reproduzierbarer Anschluss ist Feature-Evidenz, Veröffentlichung ist keine
  Clarify- oder Implement-Abnahmevoraussetzung.
- `VEX`: `Applicable`, sobald eine bekannte Schwachstelle in ausgelieferten
  oder bewerteten Komponenten vorliegt.
- `SLSA`: `Applicable` als dokumentiertes Lieferkettenziel; mindestens L1,
  öffentlich konsumierte Artefakte langfristig L2, ohne unbelegte Behauptung.
- `AI-SBOM`: `N/A`, solange KI nur Entwicklungswerkzeug ist.
- `STRIDE`, CIA und risikobezogene `CAPEC`-Referenzen: `Applicable`.
- `OWASP SAMM`: `Applicable` für das langlebige Level-2-Projekt.
- `NIST Zero Trust SP 800-207`: `N/A` für das lokale, nicht verteilte Produkt;
  Wiedervorlage bei Remote-, Cloud-, Identitäts- oder Mehrgeräte-Scope.
- `OpenSSF Scorecard`, CRA-Screening sowie begrenzte BSI-C3A-/C5-
  Providerprüfung: `Applicable` als Evidenz-/Entscheidungsarbeit, nicht als
  Zertifizierungs- oder Cloud-Produktruntime-Behauptung.
- NIS2, EU AI Act, DORA, Produktkryptografie und DPIA: begründet `N/A` mit den
  in der Spec genannten Wiedervorlagen.

*NIST SSDF and CWE Top 25 always apply. ASVS L1 is bounded to the local static
HTTP server and requires complete ID-level mapping. SBOM applies to actual
published artefact sets, VEX to known findings, and SLSA remains an evidenced
supply-chain target. AI-SBOM, Zero Trust, product cryptography, and DPIA are
reasoned N/A for the current product shape. Threat modeling, SAMM, Scorecard,
regulatory screening, and bounded provider assurance remain evidence and
decision work without certification claims.*

## Geänderte Spec-Abschnitte / Specification Sections Changed

- Status, Nicht-Ziele sowie Reihenfolge und Abhängigkeiten
- User Stories 2 und 3
- nachweisbare Ausgangslage und Intake-Abgleich
- Checklisten-Auswahl und funktionale Anforderungen
- Constitution-/Standards-Anwendbarkeit und Evidenz-Ownership
- Autonomous-Run-Anwendbarkeit und Akzeptanz-Schranken
- Annahmen, Test-/Evidenzstrategie und messbare Ergebnisse

Die Änderung fügt keine Produktimplementierung hinzu und ändert weder Intake,
Serienartefakte, Run-State, Requirements-Checkliste noch autonomen
Laufevidenzstand.

*The change adds no product implementation and does not modify the intake,
series artefacts, run state, requirements checklist, or autonomous run evidence
state.*

## Aufgaben- und Gate-Evidenz / Task and Gate Evidence

| ID | Aufgabe oder Gate / Task or gate | Ergebnis / Result | Evidenz / Evidence |
|---|---|---|---|
| `CLARIFY-TASK-001` | Spec gegen Intake, Serie, Run-State, Constitution, Standardsbasis, Code und Repository-Evidenz prüfen. | `Completed` | Dieser Bericht; bestätigte Hashes, 157-ID-Zählung, Code-/Workflow-/Coverage-Befunde. |
| `CLARIFY-TASK-002` | Materielle Unklarheiten minimal in `spec.md` auflösen und Bericht erzeugen. | `Completed` | Geänderte `spec.md`; `clarification-report.md`; keine Implementierungs- oder Governance-State-Änderung. |
| `CLARIFY-GATE-001` | Aktiver Feature-, Branch-, Phasen- und Run-ID-Abgleich. | `Pass` | Run-State-Validator und gebundene Identität. |
| `CLARIFY-GATE-002` | Vier akzeptierte Eingabe-Hashes unverändert. | `Pass` | Bytegenauer SHA-256-Vergleich. |
| `CLARIFY-GATE-003` | 157-ID-Behauptung reproduzierbar und ohne Mengendifferenz. | `Pass` | Zwölf Einzelzählungen, Summe/Eindeutigkeit `157`, Sammelband-Differenz `0`. |
| `CLARIFY-GATE-004` | Scope, Standards, Schwellen, Budgets und Ownership testbar bestimmt. | `Pass` | FR-001, FR-006, FR-008, FR-012, FR-013, FR-021 und FR-028. |
| `CLARIFY-GATE-005` | Sandbox-Folge-Intake bleibt getrennt und blockiert. | `Pass` | CL-12-Entscheidung, FR-001/FR-027/FR-028, Serienmanifest. |
| `CLARIFY-GATE-006` | Phasenfolge und Checklist-Zeitpunkt widerspruchsfrei. | `Pass` | Reihenfolge, IR-011, `SPEC-GATE-003`, Autonomous-Run-Abschnitt. |
| `CLARIFY-GATE-007` | Keine materielle offene, zurückgestellte oder unverifizierte Planungsentscheidung. | `Pass` | Abdeckungsübersicht und Abschluss unten. |

## Abdeckungsübersicht / Coverage Summary

| Kategorie / Category | Status | Kurzbegründung / Rationale |
|---|---|---|
| Funktionsumfang und Verhalten / Functional scope and behavior | `Resolved` | Evidenzarbeit, bedingte Produktarbeit und vorab begrenzte VM-Härtung sind getrennt. |
| Domäne und Datenmodell / Domain and data model | `Clear` | Keine persistente Fachdaten- oder Identitätsdomäne; Status- und Evidenzschema ist in FR-001 vollständig. |
| Interaktion und UX / Interaction and UX | `Clear` | CLI-, IDE-, Text-, Tastatur- und Fehlerpfade sowie A11Y-Gates sind benannt; keine neue UX wird vorweggenommen. |
| Nichtfunktionale Qualitätsmerkmale / Non-functional quality attributes | `Resolved` | Instruktionsbudget, Coverage-Floor/Ziel, ASVS-Scope, A11Y und Sicherheitsgates sind messbar. |
| Integrationen und externe Abhängigkeiten / Integrations and external dependencies | `Resolved` | Release/Pages, SBOM/VEX/SLSA, Providergrenzen und tatsächliche Veröffentlichung sind getrennt. |
| Grenz- und Fehlerfälle / Edge cases and failure handling | `Resolved` | VM-Budgetgrenze `N`/`N+1`, ungültige Optionen und fail-closed Freigaben sind bestimmt. |
| Einschränkungen und Abwägungen / Constraints and trade-offs | `Resolved` | 70,23-%-Baseline versus 80-%-Ziel, kleinste Befundänderung und Sandbox-Abgrenzung sind ausdrücklich entschieden. |
| Terminologie und Konsistenz / Terminology and consistency | `Resolved` | Snapshot-ID, Checklist-Familie, Applicability, Implementation, Owner und Reviewer sind eindeutig getrennt. |
| Abschlusssignale / Completion signals | `Resolved` | Zehn Matrixspalten, vollständiges ASVS-Mapping, SBOM-Anschluss, Coverage- und Risiko-Gates sind prüfbar. |
| Platzhalter und offene Entscheidungen / Placeholders and open decisions | `Clear` | Keine formalen Klärungs-, Aufgaben-, Entscheidungs- oder Deferred-Marker verbleiben. |

## Bereitschaft / Readiness

**Fragen gestellt / Questions asked**: `0`
**Fragen beantwortet / Questions answered**: `0`
**Materielle offene Punkte / Material outstanding items**: `0`
**Zurückgestellte Klärungen / Deferred clarifications**: `0`

Die Klärungsphase ist fachlich abgeschlossen. Als nächste Aktion ist gemäß
Run-State `speckit.checklist` auszuführen; diese Phase muss die vorhandene
`checklists/requirements.md` gegen den geklärten Spec-Stand revalidieren. Erst
danach folgt `speckit.plan`. Kein Folge-Intake wird gestartet.

*Clarification is semantically complete. According to the run state, the next
action is `speckit.checklist`, which must revalidate the existing
`checklists/requirements.md` against the clarified specification. Only then
does `speckit.plan` follow. No follow-up intake is started.*
