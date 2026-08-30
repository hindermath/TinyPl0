# Nachweis-Matrix / Evidence Matrix

**Feature / Feature**: `005-sandbox-secure-development`
**Bewertungsdatum / Assessment date**: 2026-08-30
**Owner**: TinyPl0-Projektverantwortung
**Reviewer**: unabhängige menschliche Feature-/Security-Prüfung

## Verwendung / How to Use This Matrix

**DE:** Die Matrix verbindet jede Anforderung mit einem lesbaren Nachweis oder
einer offenen `FUP-SBX`-Folgeaufgabe. `Pass` bedeutet nur, dass der
Dokumentvertrag erfüllt ist. `Open` bedeutet, dass technische oder menschliche
Evidence noch fehlt. `N/A` bedeutet nicht anwendbar und nennt immer einen
Neubewertungs-Trigger. Kein Status hängt von Farbe oder Tabellenposition ab.

**EN:** The matrix connects every requirement to readable evidence or an open
`FUP-SBX` follow-up. `Pass` means only that the document contract is met.
`Open` means technical or human evidence is still missing. `N/A` means not
applicable and always includes a re-evaluation trigger. No status depends on
colour or table position.

## Funktionale Anforderungen / Functional Requirements

| ID | Entscheidung / Decision | Primäre Evidence / Primary evidence | Prüfung oder Folgeweg / Check or follow-up |
|---|---|---|---|
| FR-001 | `Pass` | [spec.md](spec.md), [tasks.md](tasks.md), Run-ID in [autonomous-run-state.json](autonomous-run-state.json) | Ein Intake, ein Feature; nächster Intake wird nicht in diesem Lauf gestartet. |
| FR-002 | `Pass` | [sandbox-assessment.md](sandbox-assessment.md), Abschnitte CL-12-01..12 | T043/T044 verlangen 12 eindeutige Abschnitte und getrennte Statusachsen. |
| FR-003 | `Pass` | Pflichtfeldtabellen je CL-12-Zeile | Owner, Reviewer, Lernstufe, Rationale, Evidence, Risiko, Trigger und Next action vorhanden. |
| FR-004 | `Pass` | Mount- und Schreibgrenzen in [sandbox-assessment.md](sandbox-assessment.md) | Symbolische Quellen; keine konkrete private Hostquelle. |
| FR-005 | `Pass` | Mount-Matrix und Stop-Bedingungen | TinyPl0-only-Zielvertrag; Profile, Caches, Sitzungen und Secrets getrennt oder `NotMounted`. |
| FR-006 | `Pass` | Arbeitsort-Matrix in [sandbox-assessment.md](sandbox-assessment.md) | Build, Test, Coverage, Docs, A11Y, Golden, Smoke, Provider, Git und Review abgedeckt. |
| FR-007 | `Pass` | Kurzentscheidung, CL-12-05/08 und Arbeitsort-Matrix | Beschriebene .NET-10-Fähigkeit bleibt von realer TinyPl0-Ausführung getrennt; FUP-SBX-004. |
| FR-008 | `Pass` | Produkt- und Image-Lieferkettentabelle | TinyPl0- und Image-Dependency/SBOM/Scan/VEX/SLSA/OpenSSF/Review getrennt; FUP-SBX-008. |
| FR-009 | `Pass` | Kurzentscheidung und CL-12-01/02/04/05/10/11 | Schreibarbeit `Not Ready`; Pilot `Conditional/Open`; FUP-SBX-001..007. |
| FR-010 | `Pass` | Metadaten und Beweisgrenze in [sandbox-assessment.md](sandbox-assessment.md) | Exakter Beobachtungscommit; Working-Tree-Änderungen ausgeschlossen. |
| FR-011 | `Pass` | [spec.md](spec.md), [research.md](research.md), akzeptierte Serienreview | Vorgänger bleibt byte-identisch im Archiv; aktuelle Review löst den historischen Pfad auf. |
| FR-012 | `Pass` | Scope-Vertrag, [tasks.md](tasks.md), T050 | Kein Produktcode, Sandbox-Edit oder automatisches `docs/security/`-Update. |
| FR-013 | `Pass` | Feature-Verzeichnis und diese Matrix | Ergebnisdateien liegen feature-lokal; nur Statistikledger/Renderer-Konfiguration und IDE-Version liegen am Governance-Grenzpunkt außerhalb. |
| FR-014 | `Pass` | CL-12-Tabellen und FUP-SBX-001..008 | Jede offene/unvollständige Zeile besitzt Owner, Risiko, Aktion, Termin, Evidence und Trigger. |
| FR-015 | `Pass` | alle Nutzertexte im Feature | Deutsch zuerst, Englisch danach, CEFR-B2-Ziel und text-first; T047 prüft. |
| FR-016 | `Open` bis Delivery-Kandidat | [autonomous-run-evidence.md](autonomous-run-evidence.md), T051/T055 | Secret-/Privatpfad-Check wird auf der beabsichtigten Delivery-Menge ausgeführt. |
| FR-017 | `Open` bis Post-Merge | T063–T069 in [tasks.md](tasks.md) | Byte-identisches Archiv und Serienfortschreibung erst kausal nach Produktmerge. |

## Verfassungs- und Erfolgskriterien / Constitution and Success Criteria

| ID | Entscheidung / Decision | Evidence oder Prüfschritt / Evidence or check |
|---|---|---|
| CR-001 | `Pass` | [plan.md](plan.md) bindet .NET 10/C# 14, Baselines, A11Y, Statistik und Agentenflächen; Produktaufrufe sind in diesem Dokumentationsscope `N/A`. |
| CR-002 | `Pass` | C#/.NET ist MSL; [sandbox-assessment.md](sandbox-assessment.md) erhält sichere Pfad-, Prozess-, Secret-, Netzwerk- und Dependency-Grenzen. |
| CR-003 | `Pass` | NIST SSDF, CWE Top 25, Least Privilege, Fail-Safe Defaults und Defense in Depth sind gemappt. |
| CR-004 | `Pass` | DE/EN, semantische Tabellen, textuelle Statuswerte, getaggte Codeblöcke und WCAG-2.2-AA-orientierter Review. |
| CR-005 | `Open` bis T052 | `docs/project-statistics.md` wird genau nach Implementierungsabschluss aktualisiert; Shared Guidance bleibt unverändert. |
| SC-001 | `Pass` | 12/12 eindeutige CL-12-Abschnitte mit Pflichtfeldern. |
| SC-002 | `Pass` | Standards- und Preset-Matrix unten enthält für jeden Bereich `Applicable`, `N/A` oder `Open`. |
| SC-003 | `Pass` | Alle offenen/unvollständigen Zeilen referenzieren FUP-SBX-001..008 mit vollständigen Feldern. |
| SC-004 | `Pass` | Kurzentscheidung, Mount-Matrix und drei Stop-Bedingungen stehen am Anfang von [sandbox-assessment.md](sandbox-assessment.md). |
| SC-005 | `Open` bis T051/T055 | Delivery-Scan verlangt null Secrets, konkrete private Hostpfade, Profile, Caches und Sitzungsdaten. |
| SC-006 | `Open` bis T050/T055 | Scope-Check verlangt null unbeauftragte Produkt-, Sandbox- und bestehende `docs/security/`-Änderungen. |
| SC-007 | `Pass` | Kurzentscheidung trennt technische Verfügbarkeit, TinyPl0-Ausführung und menschliche Freigabe. |

## CL-12-Nachweis / CL-12 Evidence

| ID | Anwendbarkeit / Applicability | Umsetzung / Implementation | Primäre Evidence / Primary evidence | Folgeweg / Follow-up |
|---|---|---|---|---|
| CL-12-01 | `Open` | `Not Fulfilled` | Freigabeentwurf am Beobachtungscommit; [research.md](research.md) | FUP-SBX-001 |
| CL-12-02 | `Open` | `Partly Fulfilled` | explizite, aber zu breite Mount-/Writable-Root-Basis; Mount-Matrix | FUP-SBX-002 |
| CL-12-03 | `Applicable` | `Fulfilled` | getrennte Agent-/Build-Speicher und saubere Delivery-Grenze | `N/A`; Trigger bei Speicher-/Profiländerung |
| CL-12-04 | `Open` | `Partly Fulfilled` | Secret-Verbot vorhanden; genehmigte Injektion nicht ausgeführt | FUP-SBX-003 |
| CL-12-05 | `Open` | `Partly Fulfilled` | gepinnte Beschreibungen; Digest/Inventar/Baseline fehlen | FUP-SBX-004 |
| CL-12-06 | `Applicable` | `Fulfilled` | Spec/Plan/Tasks/Analyze, Run-State und acht Presets | `N/A`; Trigger bei Spec-Kit-/Presetänderung |
| CL-12-07 | `Open` | `Not Assessed` | Delivery- und Pilot-Approval stehen aus | FUP-SBX-006 |
| CL-12-08 | `Open` | `Partly Fulfilled` | Run-ID/Hashes vorhanden; technischer Pilotdatensatz fehlt | FUP-SBX-004 |
| CL-12-09 | `Open` | `Partly Fulfilled` | Non-Root, `no-new-privileges`, Capability-Drop; Schutzklasse offen | FUP-SBX-005 |
| CL-12-10 | `Open` | `Partly Fulfilled` | freier Compose-Egress beschrieben; aktuelle Annahme fehlt | FUP-SBX-007 |
| CL-12-11 | `Open` | `Not Fulfilled` | kein gültiges Freigabe-/Ablaufdatum | FUP-SBX-001 |
| CL-12-12 | `Applicable` | `Partly Fulfilled` | acht Presets mit Version/Priorität/Resolution; Quartalsreview offen | FUP-SBX-008 |

## Sicherheitsstandards / Security Standards

| Standard oder Bereich / Standard or area | Entscheidung / Decision | Evidence, Grenze und Trigger / Evidence, boundary, and trigger |
|---|---|---|
| NIST SSDF SP 800-218 | `Applicable` | Schutz von Quellen, Identität, Build-Ausgaben und Review in [sandbox-assessment.md](sandbox-assessment.md); neu bei Workflow-/Sandbox-Wechsel. |
| CWE Top 25 | `Applicable` | Pfadmanipulation, sensitive Daten, Ressourcen und übermäßige Rechte sind explizit berücksichtigt; neu bei Schnittstellenänderung. |
| OWASP ASVS 5.0.0 | `N/A` | Kein Web/API/HTTP/Auth-Produktscope; Trigger ist ein entsprechendes Feature. |
| SBOM | `Applicable` | Releasefähiges TinyPl0 und Image brauchen getrennte SBOMs; FUP-SBX-008. |
| VEX | `Open` | Erst bei bekanntem Produkt- oder Imagefund erforderlich; Trigger ist ein Scan-/Dependency-Befund. |
| AI-SBOM | `N/A` | KI ist Entwicklungswerkzeug, kein Produktbestandteil; Trigger ist KI im ausgelieferten/betriebenen System. |
| SLSA v1.2 | `Applicable` | Produkt- und Image-Provenienz getrennt; Ist-Niveau darf nur aus realer CI-/Build-Evidence stammen. |
| OpenSSF Scorecard | `Applicable` | Öffentliches TinyPl0-Repository; kein Ersatz für Image-Scan oder Sandbox-Freigabe. |
| OWASP SAMM | `Applicable` | TinyPl0 ist langlebig; offene Sandbox-Reife liegt in FUP-SBX-001..008. |
| STRIDE, CIA und CAPEC | `Applicable` | Host↔Container, Mounts, Secrets, Egress und Git/CI sind Entwicklungs-Trust-Boundaries. |
| NIST Zero Trust SP 800-207 | `N/A` | Kein verteilter Produktdienst; Trigger ist Cloud-/Remote-/Identitätsföderationsscope. |
| BSI C3A / BSI C5 | `N/A` | Kein Cloud-Dienst als Produktabhängigkeit; Trigger ist verbindliche Cloud-/Providerarchitektur. |
| NIS2, CRA, EU AI Act und DORA | `N/A` | Keine neue Markt-, Kunden-, KI-Produkt- oder Finanz-ICT-Rolle; Trigger ist entsprechender Delivery-/Betriebskontext. |
| WCAG 2.2 Level AA | `Applicable` | Feature-Markdown ist DE/EN, semantisch und text-first; HTML-Prüfung erst bei DocFX-/Navigationsänderung. |
| Produktarchitektur, arc42 und S-ADR | `N/A` | Keine Produkt- oder dauerhafte Sandbox-Architektur wird geändert/genehmigt; Trigger ist technische Härtung oder Betriebsfreigabe. |
| Sicherheits-Qualitätsszenario | `Applicable` | Agent darf nur den genehmigten Projektbereich ändern und stoppt bei Secret oder unerwartetem Schreibpfad. |

## Governance-Presets / Governance Presets

| Preset | Version | Priorität / Priority | Entscheidung / Decision | Evidence und Trigger / Evidence and trigger |
|---|---:|---:|---|---|
| `security-governance` | 0.6.2 | 10 | `Applicable` | installiert und in Spec/Plan/Tasks gemappt; FUP-SBX-008 bei Versions-/Katalogänderung |
| `architecture-governance` | 0.5.2 | 20 | `Applicable` | Trust-Boundary- und Sicherheitsarchitekturprüfung; keine Produkt-S-ADR-Autorität |
| `isaqb-architecture-governance` | 0.2.2 | 30 | `Applicable` | Qualitäts- und Risikoprüfung; Produktarchitektur `N/A` begründet |
| `a11y-governance` | 0.4.3 | 40 | `Applicable` | DE/EN, CEFR B2, WCAG 2.2 und text-first für Feature-Artefakte |
| `cross-platform-governance` | 0.2.2 | 50 | `N/A` für Scriptänderung | kein neues/ändertes Script; Trigger ist Sandbox-Automation im Repository |
| `agent-parity-governance` | 0.4.2 | 60 | `N/A` für Guidance-Änderung | keine Shared-Guidance-/Template-/Routing-Regel geändert; Trigger ist Regeländerung |
| `autonomous-run-governance` | 0.4.1 | 70 | `Applicable` | Run-State, Resultate, Evidence, Exact-Head- und Closeout-Vertrag |
| `parallel-autonomous-run-governance` | 0.2.6 | 80 | `N/A` für Ausführung | nur Prüfkontext; dieser Lauf bleibt strikt seriell; Trigger ist separate Kampagnenautorität |

## Mount-, Arbeitsort- und Lieferkettenabdeckung / Boundary and Supply-Chain Coverage

| Bereich / Area | Evidence | Entscheidung / Decision | Offener Nachweis / Open proof |
|---|---|---|---|
| TinyPl0-Mount | Mount-Matrix in [sandbox-assessment.md](sandbox-assessment.md) | Ziel `ReadOnly`, später enges `ReadWrite` | FUP-SBX-002 |
| Build-/Audit-Speicher | getrennte symbolische Volumes | `ReadWrite` nur für befehlsgebundenen Pilot | FUP-SBX-002/004 |
| Home/Profile/Keychain/SSH/GPG/Cloud-CLI | `NotMounted`, Agent `Denied` | `Prohibited` | negativer Grenztest FUP-SBX-002/003 |
| Restore/Build/Test/Coverage | bevorzugt `Local`/`CI`, Sandbox `Open` | kein neuer Sandbox-Claim | FUP-SBX-004 |
| Docs/A11Y/Golden | `N/A` für diesen Lauf, Trigger dokumentiert | kein Produkt-/HTML-Edit | FUP-SBX-004 bei Pilot |
| Provider/Secrets/Egress | `HumanOnly`, Werte nie in Evidence | `Open` | FUP-SBX-003/007 |
| Commit/Push/PR/Merge | autorisierter lokaler Orchestrator plus `CI` und Human Review | Sandbox-Pilot `Prohibited` | Exact-Head-Gates T058–T061 |
| TinyPl0 Dependencies/SBOM/VEX/SLSA/OpenSSF | Produktpfade nur als spätere Ziele benannt | getrennt vom Image | FUP-SBX-008 |
| Image Dependencies/SBOM/Scan/VEX/Provenienz | Digestgebundene Sandbox-Evidence erforderlich | `Open` | FUP-SBX-004/008 |

## Stabile Akzeptanzgates / Stable Acceptance Gates

| Gate | Zustand / State | Evidence oder Ausführung / Evidence or execution |
|---|---|---|
| SBX-G001 Intake-Identität | `Pass` | 4/4 akzeptierte Hashes und Ready-Review in [autonomous-run-evidence.md](autonomous-run-evidence.md) |
| SBX-G002 CL-12-Vollständigkeit | `Pass` | 12/12 Abschnitte in [sandbox-assessment.md](sandbox-assessment.md); T044 maschinell |
| SBX-G003 Mount-/Schreibgrenze | `Pass` als Dokumentvertrag | symbolische Matrix und FUP-SBX-002; technische Umsetzung bleibt `Open` |
| SBX-G004 Arbeitsort-Matrix | `Pass` | alle geforderten Arbeitsarten, Voraussetzungen, Rückfallwege und Beweisgrenzen |
| SBX-G005 Nutzungsentscheidung | `Pass` | `Not Ready` und `Conditional/Open` ohne `Approved`-Überzeichnung |
| SBX-G006 Standards | `Pass` | Standards- und Preset-Tabellen vollständig |
| SBX-G007 Scope-Grenze | `Open` bis Kandidat | T050/T055; kein Produkt-/Sandbox-/Security-Dokument-Edit zulässig |
| SBX-G008 Secret-/Privatpfad-Schutz | `Open` bis Kandidat | T051/T055; Delivery-Dateien allein werden gescannt |
| SBX-G009 A11Y/Sprachqualität | `Open` bis T047 | DE/EN, B2, Text-first, Semantik und Codeblock-Tags |
| SBX-G010 Lokale Validierung | `Open` bis T043–T055 | Dokument-, Schema-, Scope- und Delivery-Gates; Produktbuild `N/A` |
| SBX-G011 Exact-Head-Review | `Open` bis PR | Checks, Threads und echte unabhängige `APPROVED`-Entscheidung |
| SBX-G012 Merge/Sync/Closeout | `Open` bis Post-Merge | T061–T069; Intake erst kausal nach Produktmerge archivieren |

## Maschinenlesbare Gate-Zuordnung / Machine Gate Mapping

| Gate-ID | Primärer Task / Primary task | Current state |
|---|---|---|
| `SBX-IDENTITY-GATE-001` | T002 | `Pass` |
| `SBX-REFERENCE-GATE-002` | T006 | `Pass` |
| `SBX-DOCUMENT-REDGREEN-GATE-003` | T043 | Red `Pass`; Green offen / open |
| `SBX-CL12-GATE-004` | T044 | Struktur vorhanden; Ausführung offen / execution open |
| `SBX-BOUNDARY-GATE-005` | T045 | Struktur vorhanden; Ausführung offen / execution open |
| `SBX-STANDARDS-GATE-006` | T046 | Struktur vorhanden; Ausführung offen / execution open |
| `SBX-SCOPE-GATE-007` | T050 | `Open` bis Kandidat |
| `SBX-SECRET-PATH-GATE-008` | T051 | `Open` bis Kandidat |
| `SBX-A11Y-GATE-009` | T047 | `Open` bis Review |
| `SBX-PRODUCT-BUILD-GATE-010` | T048 | `N/A`, Trigger: Produkt-/API-/Teständerung |
| `SBX-VERSION-DELIVERY-GATE-011` | T053 | `Open` bis Commit-Grenze |
| `SBX-REMOTE-REVIEW-GATE-012` | T060 | `Open` bis PR-Head/Approval |
| `SBX-CLOSEOUT-GATE-013` | T066 | `Open` bis kausaler Post-Merge-Closeout |

## Follow-up-Abdeckung / Follow-Up Coverage

| Follow-up | Abgedeckte Quellen / Covered sources | Vollständigkeit / Completeness |
|---|---|---|
| FUP-SBX-001 | CL-12-01, CL-12-11 | Owner, Risiko, P0, Aktion, 2026-09-15, Evidence, Trigger und Scope-Grenze vorhanden |
| FUP-SBX-002 | CL-12-02, Mount-/Writable-Root-Vertrag | vollständig / complete |
| FUP-SBX-003 | CL-12-04, Secret-/Providergrenze | vollständig / complete |
| FUP-SBX-004 | CL-12-05, CL-12-08, Baseline/SBOM | vollständig / complete |
| FUP-SBX-005 | CL-12-09, Isolation/Trust Boundaries | vollständig / complete |
| FUP-SBX-006 | CL-12-07, unabhängige Review | vollständig / complete |
| FUP-SBX-007 | CL-12-10, Egress | vollständig / complete |
| FUP-SBX-008 | CL-12-12, Presets und getrennte Supply Chain | vollständig / complete |

## Schlussstatus / Final Matrix Status

Die Dokumentbewertung ist vollständig, wenn T043–T051 grün sind. Das macht die
Sandbox nicht automatisch betriebsbereit. Technische Pilot-Evidence bleibt
FUP-SBX-Arbeit unter neuer, ausdrücklicher Autorität.

*The document assessment is complete when T043–T051 pass. This does not make
the Sandbox operationally approved. Technical pilot evidence remains FUP-SBX
work under new explicit authority.*
