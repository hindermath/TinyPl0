# Aufgaben: Sandbox-gestützte sichere Entwicklung / Tasks: Sandbox-Supported Secure Development

**Eingabe / Input**: Akzeptierter Intake sowie `spec.md`,
`clarification-report.md`, `checklists/`, `plan.md`, `plan-review.md`,
`research.md`, `data-model.md`, `quickstart.md`, `contracts/` und
`gate-requirements.json` unter `specs/005-sandbox-secure-development/`.

**Feature / Feature**: `005-sandbox-secure-development`
**Branch / Branch**: `codex/005-sandbox-secure-development`
**Run-ID / Run ID**: `91e9fb51-2e69-4eab-85b7-cb28ec23749d`
**Liefermodus / Delivery mode**: `MergeAndSync`

## Format und bindende Arbeitsregeln / Format and Binding Work Rules

- Jede Aufgabe verwendet `- [ ] Tnnn [USn] Beschreibung` und nennt die
  betroffenen Repository-Pfade. Dieser Lauf ist strikt seriell; deshalb gibt es
  keinen `[P]`-Marker. / Every task names repository paths and runs serially;
  no task uses the `[P]` marker.
- Der Lauf bewertet und dokumentiert. Produktcode, das Sandbox-Repository,
  Sandbox-Image und -Konfiguration sowie bestehende Dateien unter
  `docs/security/` bleiben unverändert. / This run assesses and documents;
  product code, the Sandbox repository, image and configuration, and existing
  `docs/security/` files remain unchanged.
- Der unveränderliche Beobachtungsstand ist
  `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`. Private Hostpfade und nicht
  übernommene Sandbox-Änderungen dürfen nicht in versionierte Evidence gelangen.
- Produkt-TDD, Coverage, XML/DocFX, Script-Parität und Agent-Parität sind für
  diesen Dokumentationsscope begründet `N/A`. Der unveränderte Dokumentvertrag
  liefert stattdessen ein erwartetes Rot und anschließend Grün. / Product TDD,
  coverage, XML/DocFX, script parity, and agent parity are reasoned `N/A`; the
  unchanged document contract supplies red and green evidence.
- Vor jedem unerwartet nötigen `dotnet build` oder `dotnet test` muss zuerst der
  IDE-Buildzähler erhöht, der vollständige Versionswert commitfähig ausgerichtet
  und die Invocation im Ledger benannt werden. Im geplanten Lauf findet kein
  solcher Produktaufruf statt.
- Remote-Evidence gilt nur auf dem exakten PR-Head. Eine unabhängige
  `APPROVED`-Review ist Pflicht; `COMMENTED`, nicht verfügbar oder Admin-Bypass
  ersetzen sie nicht. Der autorisierte Bypass darf erst danach einen verbleibenden
  Plattform-Policy-Blocker eng begrenzt behandeln.

---

## Phase 1: Setup und unveränderliche Bindung / Setup and Immutable Binding

**Ziel / Goal**: Identität, Scope, Referenzstand, Phasenergebnisse und Gates
vor der Ergebnisarbeit fail-closed binden. / Bind identity, scope, reference,
phase results, and gates before result authoring.

- [X] T001 [US1] Prüfe read-only Betriebssystem, `pwsh`, Branch, HEAD und vollständigen Worktreestatus; protokolliere die Ergebnisse und den erlaubten Dateisatz in `specs/005-sandbox-secure-development/autonomous-run-evidence.md`. (Trace: FR-001, FR-012; SBX-G001, SBX-G007)
- [X] T002 [US1] Validere `specs/005-sandbox-secure-development/autonomous-run-state.json` und die akzeptierten Intake-, Review-, Request- und Manifest-Hashes gegen die benannten Quelldateien; stoppe bei Drift. (Trace: FR-001, FR-010, FR-011; SBX-G001; Machine gate: SBX-IDENTITY-GATE-001)
- [X] T003 [US1] Binde Specify-, Clarify-, Checklist-, Plan-, Plan-Review- und Tasks-Ergebnisse über gültige semantische Resultate im Runtime-Verzeichnis der Run-ID und erfasse ihre SHA-256-Werte im Ledger. (Trace: FR-001; SBX-G001, SBX-G010)
- [X] T004 [US1] Validere `specs/005-sandbox-secure-development/gate-requirements.json` gegen `contracts/gate-requirements.schema.json`; verlange 13 eindeutige Gates, ausführbare Applicable-Befehle und vollständige `N/A`-Begründung/Trigger. (Trace: FR-001, FR-014; SBX-G010)
- [X] T005 [US1] Prüfe Serienmanifest, Ready-Review und Binding Intake read-only; bestätige Feature 005 als einzigen Laufgegenstand und starte keinen Folge-Intake. (Trace: FR-001, FR-011, FR-017; SBX-G001, SBX-G012)
- [X] T006 [US1] Prüfe den Sandbox-Commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0` read-only über versionierte Dateien und dokumentiere in `autonomous-run-evidence.md`, dass Working-Tree-Änderungen und private Checkout-Pfade ausgeschlossen sind. (Trace: FR-010, SC-006; SBX-G005, SBX-G008; Machine gate: SBX-REFERENCE-GATE-002)
- [X] T007 [US1] Ermittle mit `git merge-base`, `git diff`, `git ls-files --others --exclude-standard` und `git status` die beabsichtigte Delivery-Menge ohne Staging; schließe Produktcode, Sandbox-, `docs/security/`-, Secret-, Profil-, Cache- und Sitzungsdaten aus. (Trace: FR-012, FR-013, FR-016; SBX-G007, SBX-G008)
- [X] T008 [US1] Führe den unveränderten Dokumentvertrag aus `specs/005-sandbox-secure-development/plan.md` vor Erstellung von `sandbox-assessment.md` und `evidence-matrix.md` aus; verlange Nonzero ausschließlich wegen der zwei fehlenden Ergebnisdateien und protokolliere Befehl, Exitcode und Vertragshash im Ledger. (Trace: FR-002–FR-014; SBX-G002–SBX-G006, SBX-G010)

**Checkpoint / Checkpoint**: T001–T008 müssen bestanden sein; der erwartete
Dokumentvertrag ist das einzige fachliche Rot. / T001–T008 must pass; the
expected document-contract failure is the only functional red.

---

## Phase 2: User Story 1 – Sichere Nutzungsentscheidung / Safe Usage Decision (Priorität / Priority: P1)

**Ziel / Goal**: Eine nachvollziehbare, nicht überzeichnete Entscheidung für
alle zwölf CL-12-Kontrollen. / Deliver a traceable, non-overstated decision for
all twelve CL-12 controls.

**Unabhängiger Test / Independent test**: `sandbox-assessment.md` enthält
CL-12-01 bis CL-12-12 genau einmal, mit getrenntem Anwendbarkeits- und
Umsetzungsstatus sowie allen Pflichtfeldern.

- [X] T009 [US1] Erstelle die DE-zuerst/EN-danach-Struktur von `specs/005-sandbox-secure-development/sandbox-assessment.md` mit Lesepfad, Begriffsdefinitionen, Referenzstand, Scope-Grenze und den Pflichtfeldlabels aus `contracts/sandbox-assessment-contract.md`. (Trace: FR-002, FR-003, FR-010, FR-015; SBX-G002, SBX-G009)
- [X] T010 [US1] Bewerte CL-12-01 formelle Freigabe in `sandbox-assessment.md`; halte den Referenzstatus als `Open`/nicht erfüllt fest und nenne menschliche Freigabe, Datum, Ablauf und Evidence als Folgegrenze. (Trace: FR-002, FR-003, FR-009, FR-014; SBX-G002, SBX-G005)
- [X] T011 [US1] Bewerte CL-12-02 Mounts und Writable Roots in `sandbox-assessment.md`; trenne vorhandene Containergrenzen von den für TinyPl0 zu breiten Schreibwurzeln. (Trace: FR-002–FR-005; SBX-G002, SBX-G003)
- [X] T012 [US1] Bewerte CL-12-03 Agenten-/Build-Speicher in `sandbox-assessment.md`; dokumentiere getrennte Volumes sowie die Grenze, dass Repository-Zustand nicht in Toolprofilen oder Caches liegen darf. (Trace: FR-002, FR-003, FR-005; SBX-G002, SBX-G003)
- [X] T013 [US1] Bewerte CL-12-04 Secret- und Providerzugriff in `sandbox-assessment.md`; untersage Repository-, Prompt-, Log-, Profil- und Home-Mount-Offenlegung und behaupte keine ungetestete Secret-Injektion. (Trace: FR-002, FR-003, FR-005, FR-016; SBX-G002, SBX-G008)
- [X] T014 [US1] Bewerte CL-12-05 unveränderliche Image-/Toolchain-Identität in `sandbox-assessment.md`; unterscheide gepinnte Beschreibungen, akzeptierten Image-Digest und tatsächlich ausgeführte TinyPl0-Evidence. (Trace: FR-002, FR-003, FR-007, FR-009, FR-010; SBX-G002, SBX-G005)
- [X] T015 [US1] Bewerte CL-12-06 Spec-Kit-/Projektartefakte in `sandbox-assessment.md` anhand der aktuellen Feature-, Preset- und Run-State-Evidence, ohne eine Sandbox-Ausführung zu erfinden. (Trace: FR-002, FR-003, FR-013; SBX-G002, SBX-G006)
- [X] T016 [US1] Bewerte CL-12-07 Review und Vier-Augen-Prinzip in `sandbox-assessment.md`; trenne aktuellen Dokumentreview, spätere Exact-Head-Approval und menschliche Sandbox-Betriebsfreigabe. (Trace: FR-002, FR-003, FR-009, FR-014; SBX-G002, SBX-G011)
- [X] T017 [US1] Bewerte CL-12-08 Auditierbarkeit in `sandbox-assessment.md`; ordne vorhandene Run-State-/Feature-Evidence und fehlende akzeptierte technische Sandbox-Lauf-ID getrennt ein. (Trace: FR-002, FR-003, FR-007, FR-010; SBX-G002, SBX-G010)
- [X] T018 [US1] Bewerte CL-12-09 Isolation und Datenklassen in `sandbox-assessment.md`; erfasse Non-Root, `no-new-privileges`, entfernte Capabilities und offene formelle Schutzklassifikation ohne Betriebsfreigabe. (Trace: FR-002, FR-003, FR-005, CR-003; SBX-G002, SBX-G003, SBX-G006)
- [X] T019 [US1] Bewerte CL-12-10 Egress in `sandbox-assessment.md`; markiere freien Compose-Egress ohne aktuelle menschliche Annahme als offen und nenne eine fail-safe Netzwerkentscheidung. (Trace: FR-002, FR-003, FR-009; SBX-G002, SBX-G005)
- [X] T020 [US1] Bewerte CL-12-11 Gültigkeit und Wiederholungsprüfung in `sandbox-assessment.md`; dokumentiere fehlende Freigabe-/Ablaufwerte und verbindliche Revalidierungs-Trigger. (Trace: FR-002, FR-003, FR-014; SBX-G002, SBX-G005)
- [X] T021 [US1] Bewerte CL-12-12 Governance-Presets in `sandbox-assessment.md`; nenne alle acht installierten Versionen, Effective-Resolution-Evidence und eine Wiedervorlage für Katalog-/Versionsdrift. (Trace: FR-002, FR-003, CR-001; SBX-G002, SBX-G006)
- [X] T022 [US1] Ergänze in `sandbox-assessment.md` die symbolische Mount-/Schreibmatrix für `<TinyPl0-repository>`, getrennte Build-/Audit-Speicher und verbotene Home-, Profil-, Keychain-, SSH-, GPG-, Cloud-CLI- und Token-Bereiche. (Trace: FR-004, FR-005, SC-004–SC-006; SBX-G003, SBX-G008)
- [X] T023 [US1] Leite in `sandbox-assessment.md` die Nutzungsentscheidung ab: reguläre/autonome Schreibarbeit `Not Ready`, ein späterer Read/Build/Test-Pilot nur `Conditional/Open`, Remote-Lieferung ausschließlich durch den autorisierten TinyPl0-Orchestrator. (Trace: FR-007, FR-009, FR-012; SC-004, SC-007; SBX-G005)

**Checkpoint / Checkpoint**: Die Entscheidung darf verfügbare Technik,
TinyPl0-Ausführung und menschliche Freigabe nicht gleichsetzen.

---

## Phase 3: User Story 2 – Verständlicher Arbeitsort / Understandable Work Location (Priorität / Priority: P2)

**Ziel / Goal**: Jede Arbeitsart erhält einen sicheren Ort, Voraussetzungen,
Schreibziele, Evidence und Rückfallweg. / Give each work type a safe location,
prerequisites, write targets, evidence, and fallback.

**Unabhängiger Test / Independent test**: Lernende können Build, Test,
Dokumentation, A11Y, Smoke, Providerzugriff und Review in höchstens fünf Minuten
einem sicheren Arbeitsort zuordnen.

- [X] T024 [US2] Ergänze in `sandbox-assessment.md` eine DE/EN-Arbeitsortmatrix mit den Spalten Arbeit, bevorzugter Ort, Voraussetzung, erlaubtes Schreibziel, Nachweisgrenze, Stop-Bedingung und sicherer Rückfallweg. (Trace: FR-006, FR-015; SBX-G004, SBX-G009)
- [X] T025 [US2] Ordne Restore und Build in `sandbox-assessment.md` Sandbox/Lokal/CI zu; kennzeichne die .NET-10-Fähigkeit im Referenzstand als plausibel und einen TinyPl0-Sandbox-Lauf bis zum Pilot als `Open`. (Trace: FR-006, FR-007; SBX-G004, SBX-G005)
- [X] T026 [US2] Ordne Unit-/Integrations-/Golden-Tests und Coverage in `sandbox-assessment.md` zu; beschreibe getrennte Build-Ausgaben und warum dieser Dokumentationslauf keine Produktresultate neu beansprucht. (Trace: FR-006, FR-007; SBX-G004, SBX-G010)
- [X] T027 [US2] Ordne DocFX-, Playwright/axe- und `lynx`-Prüfung in `sandbox-assessment.md` zu; markiere sie für diesen Lauf als nicht ausgelöst und nenne den Trigger einer API-/DocFX-/Navigationsänderung. (Trace: FR-006, FR-014, CR-004; SBX-G004, SBX-G009)
- [X] T028 [US2] Ordne Sandbox-Smoke-Checks und Image-Identitätsprüfung in `sandbox-assessment.md` zu; verlange einen akzeptierten Digest und protokollierte TinyPl0-Baseline statt bloßer Dockerfile-/Compose-Aussage. (Trace: FR-006, FR-007, FR-009; SBX-G004, SBX-G005)
- [X] T029 [US2] Ordne Provider-/Netzwerkzugriff in `sandbox-assessment.md` zu; begrenze ihn auf genehmigte Ziele, getrennte Secret-Injektion und eine aktuelle Egress-Entscheidung mit lokalem/CI-Rückfallweg. (Trace: FR-005, FR-006, FR-009; SBX-G004, SBX-G008)
- [X] T030 [US2] Ordne Git-, Commit-, Push-, PR-, Review- und Merge-Arbeit in `sandbox-assessment.md` zu; halte den Sandbox-Pilot frei von Remote-Lieferberechtigung und nenne Exact-Head-/Vier-Augen-Gates. (Trace: FR-006, FR-009, FR-017; SBX-G004, SBX-G011, SBX-G012)
- [X] T031 [US2] Trenne in `sandbox-assessment.md` TinyPl0-SBOM/Dependency/VEX/Provenienz/OpenSSF-Evidence von Image-SBOM/Scan/VEX/Provenienz und nenne die späteren Zielpfade nur, ohne `docs/security/` zu ändern. (Trace: FR-008, FR-012; SBX-G006, SBX-G007)
- [X] T032 [US2] Dokumentiere in `sandbox-assessment.md` NIST SSDF, CWE Top 25, STRIDE/CIA/CAPEC, Least Privilege, Fail-Safe Defaults und Defense in Depth als Applicable-Evidence für die Entwicklungsgrenzen. (Trace: CR-002, CR-003; SBX-G006)
- [X] T033 [US2] Dokumentiere in `sandbox-assessment.md` ASVS, AI-SBOM, Zero Trust, C3A/C5 und Regulatorik mit den begründeten `N/A`-Triggern sowie VEX und technische Sandbox-Evidence als `Open`, wo Nachweise fehlen. (Trace: FR-008, FR-014; SBX-G006)
- [X] T034 [US2] Prüfe die Arbeitsortmatrix gegen `spec.md`, `plan.md`, `quickstart.md` und `contracts/sandbox-assessment-contract.md`; entferne Widersprüche, implizite Vorkenntnis und rein visuelle Statussignale. (Trace: FR-006, FR-015; SC-004; SBX-G004, SBX-G009)

---

## Phase 4: User Story 3 – Auditfähige Folgearbeit / Audit-Ready Follow-Up (Priorität / Priority: P3)

**Ziel / Goal**: Alle offenen Punkte bleiben außerhalb dieses Scopes, sind aber
mit Owner, Risiko, Termin, Evidence und Trigger handlungsfähig beschrieben.

**Unabhängiger Test / Independent test**: Jede `Open`- oder nicht erfüllte
Zeile besitzt genau eine benannte Folgeaufgabe mit allen Pflichtfeldern.

- [X] T035 [US3] Ergänze in `sandbox-assessment.md` eine stabile FUP-SBX-Liste für formelle Pilot-/Betriebsfreigabe und Ablaufprüfung mit Owner-Rolle, Risiko, Priorität, nächster Maßnahme, Zieltermin, erwarteter Evidence und Trigger. (Trace: FR-003, FR-014; SC-003; SBX-G005)
- [X] T036 [US3] Ergänze FUP-SBX-Einträge für dedizierten TinyPl0-Mount, minimale Writable Roots, read-only/leere Nebenwurzeln und getrennte Build-/Audit-Speicher; ändere keine Sandbox-Konfiguration. (Trace: FR-004, FR-005, FR-012, FR-014; SBX-G003, SBX-G007)
- [X] T037 [US3] Ergänze FUP-SBX-Einträge für exakten Image-Digest, Toolchain-Smoke und tatsächlichen TinyPl0-Restore/Build/Test/Coverage/Docs/A11Y-Baselinelauf auf akzeptiertem Stand. (Trace: FR-006, FR-007, FR-009, FR-014; SBX-G004, SBX-G005)
- [X] T038 [US3] Ergänze FUP-SBX-Einträge für Secret-Injektion, Providerinventar, aktuelle Egress-Annahme und Stop-on-Exposure-Evidence; erfasse keine Secretwerte. (Trace: FR-005, FR-009, FR-014, FR-016; SBX-G005, SBX-G008)
- [X] T039 [US3] Ergänze FUP-SBX-Einträge für getrennte Produkt-/Image-SBOM, Dependency-Scan, VEX, SLSA-Provenienz, OpenSSF und wiederkehrende Preset-/Sandbox-Revalidierung. (Trace: FR-008, FR-014; SBX-G006)
- [X] T040 [US3] Erstelle `specs/005-sandbox-secure-development/evidence-matrix.md` DE-zuerst/EN-danach und mappe FR-001–FR-017, CR-001–CR-005, SC-001–SC-007 und SBX-G001–SBX-G012 auf konkrete Evidence oder FUP-SBX-Ziele. (Trace: FR-013–FR-015; SBX-G002, SBX-G006, SBX-G009)
- [X] T041 [US3] Ergänze in `evidence-matrix.md` alle zwölf CL-12-Zeilen, die Standard-/Preset-Entscheidungen, Mount-/Arbeitsort-Nachweise und die getrennten Produkt-/Image-Evidence-Pfade ohne private Hostpfade. (Trace: FR-002–FR-011, FR-016; SBX-G002–SBX-G008)
- [X] T042 [US3] Prüfe `sandbox-assessment.md` und `evidence-matrix.md` auf 100 Prozent Open-Follow-up-Abdeckung; kein offener Punkt darf automatisch umgesetzt, akzeptiert oder als nächstes Feature gestartet werden. (Trace: FR-012, FR-014, FR-017; SC-003; SBX-G007, SBX-G012)

---

## Phase 5: Konvergenz, Qualität und lokaler Kandidat / Convergence, Quality, and Local Candidate

**Ziel / Goal**: Den kleinsten Delivery-Schnitt gegen alle anwendbaren Gates
prüfen, ohne Produkt- oder Sandbox-Arbeit vorzutäuschen.

- [X] T043 [US1] Führe den unveränderten Dokumentvertrag aus T008 grün aus; verlange zwei Ergebnisdateien, 12 eindeutige CL-IDs, gültige Statuswerte, alle Pflichtfelder, Nutzungsentscheidung, Mount-/Arbeitsortmatrix und vollständige Open-Follow-ups. (Trace: FR-002–FR-014; SBX-G002–SBX-G006, SBX-G010; Machine gate: SBX-DOCUMENT-REDGREEN-GATE-003)
- [X] T044 [US1] Führe die CL-12- und Evidence-Matrix-Validatoren aus `gate-requirements.json` pro Abschnitt aus; stoppe bei globaler Scheinabdeckung, Doppel-ID, fehlender Evidence oder widersprüchlichem Status. (Trace: FR-002, FR-003, FR-013, FR-014; SBX-G002, SBX-G010; Machine gate: SBX-CL12-GATE-004)
- [X] T045 [US2] Führe Mount-/Writable-Root- und Arbeitsort-Validatoren aus `gate-requirements.json` aus; verlange symbolische Pfade, verbotene Nachbarbereiche und alle geforderten Arbeitsarten. (Trace: FR-004–FR-006; SBX-G003, SBX-G004; Machine gate: SBX-BOUNDARY-GATE-005)
- [X] T046 [US2] Führe den Standards-/Preset-Validator aus `gate-requirements.json` aus; bestätige acht Presets sowie vollständige Applicable/`N/A`/Open-Entscheidungen ohne stille Auslassung. (Trace: FR-008, CR-001–CR-004, SC-002; SBX-G006; Machine gate: SBX-STANDARDS-GATE-006)
- [X] T047 [US1] Prüfe alle geänderten Markdown-Dateien auf Deutsch-zuerst/Englisch-danach, CEFR B2, erklärte Fachbegriffe, semantische Überschriften/Tabellen, getaggte Codeblöcke und vollständig textuelle Status-/Entscheidungsinformation. (Trace: FR-015, CR-004; SC-004; SBX-G009; Machine gate: SBX-A11Y-GATE-009)
- [X] T048 [US1] Erfasse begründetes `N/A` mit Trigger für Produkt-TDD/Coverage, öffentliche API/XML/DocFX, Didaktik-Kommentare, Script-/Manpage-/Cmdlet-Parität, Produktarchitektur-/S-ADR-/arc42-Änderungen und Agent-Guidance-Parität in `autonomous-run-evidence.md`. (Trace: FR-012, CR-002–CR-005; SBX-G006, SBX-G007, SBX-G010; Machine gate: SBX-PRODUCT-BUILD-GATE-010)
- [X] T049 [US1] Führe vor jedem Build-/Test-Verzicht eine Abhängigkeitssuche nach Validatoren aus, die Feature-Pfade, Statusmarker oder Schemas lesen; führe jeden betroffenen Dokument-/JSON-Validator aus und dokumentiere, warum kein `dotnet`-Aufruf nötig ist. (Trace: FR-013; SBX-G010)
- [X] T050 [US1] Prüfe mit `git diff` und `git status`, dass `src/` außer geplanter IDE-Versionsmetadaten, `tests/`, Sandbox-Dateien, bestehende `docs/security/`, Agentflächen, Constitution und Templates unverändert bleiben. (Trace: FR-012; SC-006; SBX-G007; Machine gate: SBX-SCOPE-GATE-007)
- [X] T051 [US1] Führe Secret- und Privatpfad-Prüfung ausschließlich über die beabsichtigte Delivery-Menge aus; lese keine Secretdatei und verlange null Funde, null konkrete Homepfade und null Profil-/Cache-/Sessionartefakte. (Trace: FR-016; SC-005; SBX-G008; Machine gate: SBX-SECRET-PATH-GATE-008)
- [X] T052 [US1] Aktualisiere `docs/project-statistics.md` und den neuen Phasenslot in `docs/project-statistics.config.json` genau einmal für die abgeschlossene Implementierungsphase: chronologischer Eintrag, beobachtetes Fenster, Produktions-/Test-/Dokumentationszeilen, Pakete, Baselines 80/125, 21,5 Arbeitstage und 7,8 Stunden; erhalte `## Gesamtstatistik` als letzten Top-Level-Abschnitt und validiere die text-first Diagramme mit dem Renderer. (Trace: CR-005; SBX-G010)
- [X] T053 [US1] Revalidiere read-only den nächsten eindeutigen PR-Slot; richte `Version`, `AssemblyVersion` und `FileVersion` in `src/Pl0.Ide/Pl0.Ide.csproj` auf `1.<PR>.<Commitcount nach Commit>.<unveränderter Build>` aus, weil kein Build/Test ausgeführt wurde. (Trace: CR-001; SBX-G010; Machine gate: SBX-VERSION-DELIVERY-GATE-011)
- [X] T054 [US1] Markiere T001–T053 in `tasks.md` aus konkreter Evidence abgeschlossen, aktualisiere Run-State und Ledger auf Implementation-Abschluss und validere beide ohne historische Resultate umzuschreiben. (Trace: FR-001, FR-013; SBX-G010)
- [X] T055 [US1] Stage nur die beabsichtigten Feature-, Statistikledger-/Renderer-Konfigurations- und Versionspfade; führe `git diff --cached --check`, Pfadabgleich, Scope-, Secret-/Privatpfad- und Gate-Validierung gegen den exakten Kandidaten aus und bewahre fremde Änderungen. (Trace: FR-012, FR-016; SBX-G007–SBX-G010)

---

## Phase 6: MergeAndSync-Lieferung / MergeAndSync Delivery

**Ziel / Goal**: Produktlieferung auf exakt einem Head mit technischer Evidence
und unabhängiger menschlicher Approval abschließen.

- [ ] T056 [US1] Committe den exakten Kandidaten auf `codex/005-sandbox-secure-development`; prüfe danach Branch-Commitcount und identische drei IDE-Versionsfelder, ohne Produkt-Build/Test nachzuholen. (Trace: FR-013, FR-016; SBX-G010)
- [ ] T057 [US1] Pushe ausschließlich den Feature-Branch, erstelle oder aktualisiere genau einen PR nach `main`, erfasse URL, PR-Nummer, Base/Head und Delivery-Scope in `autonomous-run-evidence.md` und korrigiere bei PR-Slot-Abweichung Version/Patch in einem neuen konsistenten Commit. (Trace: FR-001, FR-013; SBX-G011)
- [ ] T058 [US1] Warte auf die Required Checks des exakten PR-Heads; ordne jeden technischen Gate-Befehl dem Workflow, Job und Runner zu und behandle fehlenden technischen Scope als Blocker, nicht als Bypass-Fall. (Trace: FR-016; SBX-G010, SBX-G011)
- [ ] T059 [US1] Erzeuge temporäre, nicht zu committende Exact-Head-Gate-Evidence aus `gate-requirements.json`; validere Head, Commands, Runner, Primary/Supplemental-Zuordnung und null stale/unowned Rows. (Trace: FR-016; SBX-G011)
- [ ] T060 [US1] Prüfe auf dem unveränderten Head null offene Review-Threads und mindestens eine unabhängige `APPROVED`-Entscheidung; `COMMENTED`, unavailable, Maintainer-Kommentar oder Admin-Bypass zählen nicht. (Trace: FR-009; SBX-G011; Machine gate: SBX-REMOTE-REVIEW-GATE-012)
- [ ] T061 [US1] Merge den PR nur nach T058–T060; nutze Admin-Bypass ausschließlich für einen dann verbleibenden eng begrenzten Branch-Policy-Blocker und protokolliere Grund, Scope und exakten Head. (Trace: FR-017; SBX-G011, SBX-G012)
- [ ] T062 [US1] Synchronisiere lokales `main` mit `origin/main`, bestätige Merge-SHA, gelöschten/geschlossenen Feature-Branch-Status und einen sauberen Delivery-Grenzpunkt; starte noch keinen Folge-Intake. (Trace: FR-017; SBX-G012)

---

## Phase 7: Kausaler Serien-Closeout und Retrospektive / Causal Series Closeout and Retrospective

**Ziel / Goal**: Erst nach Produktmerge den Intake byte-identisch archivieren,
die Serie fortschreiben und denselben autonomen Lauf terminal beenden.

- [ ] T063 [US3] Erstelle von synchronem `main` einen neuen sauberen Closeout-Branch; prüfe erneut Intake-, Review-, Request-, Manifest- und Produkt-Merge-Identität und binde den Closeout an Run-ID und Merge-SHA. (Trace: FR-011, FR-017; SBX-G001, SBX-G012)
- [ ] T064 [US3] Archiviere `requirements/intakes/active/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` byte-identisch unter dem branchgestempelten Pfad in `requirements/intakes/archive/`; verifiziere Quell-/Zielhash vor Lifecycle-Änderung. (Trace: FR-010, FR-011, FR-017; SBX-G012)
- [ ] T065 [US3] Aktualisiere ausschließlich Manifest, Receipt/Lifecycle und gegebenenfalls die kanonische Next-Intake-Anzeige gemäß `speckit-intake-series-update`; erhalte Zielreihenfolge, Abhängigkeiten, andere Hashes und Intake-Inhalte. (Trace: FR-017; SBX-G012)
- [ ] T066 [US3] Führe `speckit-intake-series-status` aus und verlange konsistente Archive, Hashes, abgeschlossene Feature-005-Linie und genau den seriell nächsten Eligible- oder begründet blockierten Zielstatus, ohne ihn auszuführen. (Trace: FR-017; SBX-G012; Machine gate: SBX-CLOSEOUT-GATE-013)
- [ ] T067 [US3] Aktualisiere Closeout-Statistik und IDE-Version am Commit-Grenzpunkt nach denselben Repository-Regeln, stage nur kausale Closeout-Pfade und validere Diff, Scope, Secret-/Privatpfad und byte-identisches Archiv. (Trace: FR-016, FR-017, CR-005; SBX-G008, SBX-G010, SBX-G012)
- [ ] T068 [US3] Committe, pushe, prüfe, lasse den Closeout-PR unabhängig genehmigen und merge/synchronisiere ihn im autorisierten `MergeAndSync`-Modus; Admin-Bypass bleibt auf eine nach technischer Evidence und Approval verbleibende Plattformpolicy begrenzt. (Trace: FR-017; SBX-G011, SBX-G012)
- [ ] T069 [US3] Erstelle die autonome Retrospektive, setze `autonomous-run-state.json` erst nach Produktmerge und Closeout auf `Completed`, validere terminale Evidence und melde den nächsten Serienstatus, ohne im selben Lauf ein Folgefeature zu starten. (Trace: FR-001, FR-017; SBX-G012)

---

## Abhängigkeiten und Ausführungsreihenfolge / Dependencies and Execution Order

- Phase 1 blockiert alle User Stories. / Phase 1 blocks all user stories.
- US1 (Phase 2) muss vor US2 und US3 abgeschlossen sein, weil Entscheidung und
  CL-12-Zeilen die Arbeitsort- und Follow-up-Sichten speisen.
- US2 (Phase 3) muss vor US3 abgeschlossen sein; die Evidence-Matrix übernimmt
  nur bereits entschiedene Arbeitsgrenzen.
- Phase 5 beginnt erst nach allen drei User Stories. Es gibt keine parallelen
  Tasks und keinen zweiten Writer für Assessment, Matrix, Statistik, Version,
  Run-State oder Delivery-Evidence.
- Phase 6 setzt einen validierten lokalen Kandidaten voraus. T060 ist ein harter
  menschlicher Gate; ohne echte unabhängige Approval wird nicht gemergt.
- Phase 7 beginnt kausal erst nach T062. Der nächste Eligible-Intake wird nur
  gemeldet; ein neuer autonomer Lauf braucht eine neue Run-ID und eigene
  Preflight-Grenze.
