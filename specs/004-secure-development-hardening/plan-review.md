# Plan-Review: Secure-Development-Härtung / Secure Development Hardening

**Feature / Feature**: `specs/004-secure-development-hardening`

**Lauf / Run**: `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7`

**Phase / Phase**: `plan-review`

**Datum / Date**: 2026-08-30

**Geprüfter Git-HEAD / Reviewed Git HEAD**:
`8cce89e09ef624e9875d1ca86ea2c878ce8cdd54`

**Ergebnis / Conclusion**: **Pass**

## Geprüfter Umfang und Autorität / Reviewed Scope and Authority

Der unterbrochene unabhängige Plan Review wurde ausschließlich mit den
übernommenen Resume-Eingaben fortgesetzt: `spec.md`,
`clarification-report.md`, `checklists/requirements.md`, `plan.md`,
`research.md`, `data-model.md`, `quickstart.md`, `contracts/` und
`gate-requirements.json`. `autonomous-run-state.json`, Intake und
Serienartefakte wurden nur gelesen. Der Review erzeugte dieses Dokument und
das vorgeschriebene strukturierte Phasenergebnis. Es wurden weder `tasks.md`
noch Produktcode erzeugt oder geändert; es gab keinen Build, Test, Commit,
Push, Merge, Intake-/Serien-/Run-State-Edit und keinen Start eines weiteren
Features.

*The interrupted independent plan review resumed only with the declared owned
planning inputs. Run state, intake, and series artefacts were read-only. The
review produced this report and the required structured phase result. It did
not create tasks, edit product code, build, test, commit, push, merge, change
intake/series/run state, or start another feature.*

## Aufgabenbeweis / Task Evidence

| Aufgabe / Task | Ergebnis / Result | Evidenz / Evidence |
|---|---|---|
| PLAN-REVIEW-TASK-001 — Drift, Intake, Scope und aktuelle Realität prüfen | Completed | Run-State-Validator erfolgreich; vier akzeptierte Byte-Hashes unverändert; Branch und HEAD stimmen; 28 eindeutige FRs und 14 eindeutige SCs; kein `tasks.md`. |
| PLAN-REVIEW-TASK-002 — alle Critical-, High- und Medium-Befunde minimal beheben | Completed | Zehn Befunde wurden in den übernommenen Planungsartefakten behoben; keine Produkt- oder Governance-Implementierung erfolgte. |
| PLAN-REVIEW-TASK-003 — Gates, Hashbindung und Remote-Annahmen erneut prüfen | Completed | Gate-JSON besteht sein Draft-2020-12-Schema, 31 Gate-IDs sind eindeutig, Pflicht-Token sind befehlsseitig geschlossen, historischer Plan-Result-Dateihash stimmt, PR-Slot wurde read-only erneut geprüft. |

## Befundzahlen / Finding Counts

| Schwere / Severity | Gefunden / Found | Behoben / Resolved | Offen / Open |
|---|---:|---:|---:|
| Critical | 1 | 1 | 0 |
| High | 3 | 3 | 0 |
| Medium | 6 | 6 | 0 |
| Low | 0 | 0 | 0 |
| **Gesamt / Total** | **10** | **10** | **0** |

## Befunde und Behebung / Findings and Remediation

| ID | Schwere / Severity | Befund / Finding | Behebung und Status / Remediation and status |
|---|---|---|---|
| C-001 | Critical | Mehrere `Applicable`-Gates hatten leere Begründungen; `gate-requirements.json` war gegen den eigenen Vertrag schema-ungültig und durfte keine autonome Erfüllung belegen. / Applicable gates had empty rationales and the requirements document failed its own schema. | Begründungen, Trigger und ausführbare Evidenz ergänzt; Schema-, Eindeutigkeits- und Applicability-Prüfung ist grün. **Behoben / Resolved.** |
| H-001 | High | Quickstart und Versionsgate leiteten IDE-`Minor` aus Feature `004` als `4` ab. / IDE Minor was derived from feature 004. | Dynamische read-only PR-Ermittlung festgelegt: vorhandene Feature-PR-Nummer, sonst höchste vergebene Nummer plus eins. Aktuell ist `#71` am höchsten und Slot `72` frei; Feature `004` ist nie die Minor-Quelle. `Patch` ist der finale Branch-Commitcount, `Build` steigt vor jedem Build/Test. **Behoben / Resolved.** |
| H-002 | High | ASVS- und A11Y-Gates konnten Vollständigkeit beziehungsweise reproduzierbare Remote-Evidence nicht beweisen. / ASVS and accessibility gates were not complete or remotely reproducible. | Offizielle gepinnte ASVS-5.0.0-JSON-Quelle, Quellhash, exakt 70 L1-IDs und HEAD-Bindung ergänzt. Der A11Y-Pfad besitzt ein Lockfile-basiertes Node-24-/Playwright-/axe-Design, Loopback-Lebenszyklus und `lynx`-Prüfung. **Behoben / Resolved.** |
| H-003 | High | Bedingte Workflow-, Supply-Chain-, Baseline- und A11Y-Dateiflächen widersprachen sich; dadurch war evidence-first-Autorisierung nicht fail-closed. / Conditional file surfaces conflicted with evidence-first authorisation. | Sechs konkrete Finding-Pakete, rote Vorbedingungen, kleinste Dateisätze und Ausschlüsse synchronisiert. `release-please.yml` bleibt read-only; Baseline-Parität bindet die Drei-OS-Matrix; A11Y bindet nur den lokalen Harness und Pages-Workflow. **Behoben / Resolved.** |
| M-001 | Medium | Der VM-Vertrag nannte nur die Stack-Untergrenze und ließ Überlauf-/Großallokation sowie bestehende positionale API-Nutzung offen. / VM bounds and positional API compatibility were incomplete. | `3 <= StackSize <= 1_000_000`, Vorvalidierung vor Addition/Allokation, `int.MaxValue`-Tests und der neue, am Ende angehängte Budgetparameter sind in Plan, Modell, Vertrag und Rot-Grün-Schnitt gebunden. **Behoben / Resolved.** |
| M-002 | Medium | Lokale Node-Version 26, geforderte Node-24-LTS-Evidence und macOS-/Linux-/Windows-Parität waren nicht zusammengeführt. / Local and remote tool/platform assumptions diverged. | Keine globale Toolannahme mehr: Node 24 wird im gepinnten Workflow gewählt, `npm ci` nutzt das Lockfile; der Baseline-Pfad erhält Remote-Evidence auf allen drei Betriebssystemen. **Behoben / Resolved.** |
| M-003 | Medium | Pflicht-Token für Lizenz/Lockfile, SBOM/VEX/SLSA, Baseline-Hilfe/Strict-Mode, fünf Agentenflächen, Statistik und PostMerge fehlten in den exakten Befehlen. / Required proof tokens were absent from exact commands. | Jeder Pflicht-Token ist jetzt durch eine konkrete read-only oder spätere autorisierte Prüfzeile abgedeckt; Applicable-/N/A-Semantik bleibt schema-valid. **Behoben / Resolved.** |
| M-004 | Medium | Die 28 FRs und 14 SCs hatten keine kompakte, tasks-fähige Zuordnung zu Planpaketen, Pfaden und Gates. / Requirements lacked task-ready traceability. | `plan.md` enthält jetzt sechs abgegrenzte Zuordnungsgruppen mit primären Dateiflächen, Gates und fortgeltender Einzel-Finding-Schranke. **Behoben / Resolved.** |
| M-005 | Medium | Der aktive Spec-Abschnitt beschrieb noch die abgeschlossene Clarify-Autorität statt des unterbrochenen Plan Reviews. / The active-run section still described Clarify authority. | Phase, zulässige Remediation, Ergebnisbindung, read-only PR-Beobachtung und verbotene Aktionen auf den aktuellen Resume-Scope aktualisiert. **Behoben / Resolved.** |
| M-006 | Medium | Quickstart und Evidenzvertrag versuchten den historischen `plan.result.json`-Payloadhash gegen den remedierten Plan erneut zu validieren; das muss nach erlaubter Review-Remediation scheitern. / Historical plan evidence was incorrectly treated as a current plan hash. | Historischer Ergebnis-Dateihash bleibt unverändert an den Run-State gebunden; `plan-review.result.json` ist die kausale Bindung des remedierten Zustands. Delivery-Evidence prüft zusätzlich den exakten Feature-Diff und verbotene Intake-/Serienpfade. **Behoben / Resolved.** |

## Intake-, Scope- und Realitätsabgleich / Intake, Scope, and Reality Check

- Alle 16 Intake-Positionen bleiben klassifiziert. IR-001 bis IR-014 sind
  planbar, IR-015 ist bereits erfüllt und IR-016 bleibt Follow-up. Kein späterer
  Intake und keine Sandbox-Härtung wurden vorgezogen.
- 28/28 FRs (`FR-001` bis `FR-028`) und 14/14 SCs (`SC-001` bis `SC-014`)
  sind eindeutig und einer tasks-fähigen Plan-/Gate-Gruppe zugeordnet.
- Evidence-first bleibt bindend: Außer den zwei ausdrücklich geplanten
  VM-Härtungen darf kein Produkt-, UI-, CI-, Workflow- oder Guidance-Edit ohne
  reproduzierbares Finding, rotes Signal und kleinsten Dateisatz beginnen.
- Die tatsächlichen Modulabhängigkeiten bleiben korrekt:
  `Pl0.Core` ohne Projektabhängigkeit; `Pl0.Vm -> Pl0.Core`;
  `Pl0.Cli/Pl0.Ide -> Pl0.Core + Pl0.Vm`; Tests dürfen alle vier referenzieren.
- Beide VM-Wege allokieren aktuell `StackSize + 1` vor Validierung und besitzen
  kein Instruktionsbudget. Damit ist die kleine gemeinsame Options-/Budget-
  Härtung realistisch und verletzt keine PL/0-, Dialekt- oder P-Code-Semantik.
- Das aktuelle IDE-Projekt steht auf `1.71.446.32`, während der geprüfte HEAD
  Commitcount `447` beträgt. Weil diese Phase weder baut noch testet, wurde die
  Version nicht verändert. Der spätere serialisierte Writer muss vor jedem
  Build/Test alle drei Felder auf den dann gültigen Wert setzen und committen.
- `docs-pages.yml` verwendet aktuell tag-basierte Actions und besitzt keinen
  axe-/`lynx`-Pfad; die Ergänzung bleibt hinter `FND-SC-001` beziehungsweise
  `FND-A11Y-001`. `release-please.yml` ist bereits voll-SHA-gepinnt und bleibt
  außerhalb des Änderungssatzes.

*The accepted intake remains faithful and later intakes stay excluded. All 28
FRs and 14 SCs map to taskable plan/gate groups. Current module dependencies
match repository policy. Both VM paths presently allocate before validating
the configured stack and have no instruction budget, so the bounded VM change
is feasible without changing PL/0 semantics. Conditional workflow and
governance work remains closed until its own evidence-first finding.*

## Security, Privacy, A11Y und Plattformen / Security, Privacy, Accessibility, and Platforms

- NIST SSDF und CWE Top 25 gelten immer. C#/.NET Secure Coding, STRIDE/CIA,
  CAPEC, arc42/iSAQB, S-ADR, Dependency Review und Supply-Chain-Evidence sind
  ausdrücklich gebunden.
- OWASP ASVS 5.0.0 Level 1 gilt nur für den engen lokalen `pl0c --api`-Scope.
  SBOM gilt für den tatsächlichen distributierbaren Artefaktsatz; VEX entsteht
  nur bei bekanntem Fund, und SLSA darf nicht über die Evidence hinaus behauptet
  werden. CycloneDX .NET `6.2.0` ist als zu prüfender Tool-Pin geplant.
- AI-SBOM, Zero Trust, Produktkryptografie/DPIA, NIS2, EU AI Act und DORA
  bleiben mit konkreten Wiedervorlage-Triggern `N/A`; dies ist keine stille
  Auslassung. Es werden keine personenbezogenen Daten, Secrets oder Telemetrie
  eingeführt.
- WCAG 2.2 AA, text-first, DE zuerst/EN danach auf B2-Niveau, vollständige
  XML-Dokumentation, DocFX, axe und `lynx` haben getrennte, fail-closed Gates.
  Visuelle Bedeutung allein ist unzulässig.
- PowerShell 7 ist auf macOS vorhanden. Bash bleibt der Wrapperpfad. Node 26
  ist lokal vorhanden, wird aber nicht als Node-24-Evidence missverstanden.
  Remote-Parität verlangt explizite macOS-, Linux-/Ubuntu- und Windows-Logs.
- Neue NuGet-, npm-, Action- oder .NET-Tool-Abhängigkeiten bleiben bis zu
  Quellen-, Wartungs-, Lizenz-, Lockfile- und CVE-Prüfung gesperrt. Actions in
  den geplanten Lieferworkflows müssen vollständige Commit-SHAs tragen.

## Gate- und Hashnachweis / Gate and Hash Evidence

- `validate-autonomous-run-state.ps1`: Pass für Run, Feature, `PlanReview`,
  `Active`, Tasks `0/0`.
- Vier akzeptierte Intake-/Serienartefakte: Byte-SHA-256 stimmt exakt mit dem
  Run-State überein.
- Historisches `plan.result.json`: Dateihash
  `7ad616e7c60bbb91b05cc8896d32f409c69d2ca898f09d3283c04515ec5c87ca`
  stimmt mit dem Run-State. Sein früherer Payloadhash bleibt absichtlich
  historisch; er wird nicht umgeschrieben.
- `gate-requirements.json`: JSON-Syntax und Feature-Schema 1.0/Draft 2020-12
  bestanden; 31 eindeutige Gates; `Applicable` besitzt Befehle, Befehls- und
  Plattformtokens sowie Begründung; `N/A` besitzt leere Ausführungslisten,
  Begründung und Trigger; kein Pflicht-Befehlstoken fehlt.
- GitHub read-only, erneut am 2026-08-30 unmittelbar vor dem Bericht:
  höchste vergebene PR `#71`, keine PR für
  `codex/004-secure-development-hardening`, vorläufiger nächster Slot `#72`.
  Dies ist eine veränderliche Reservierungsannahme und wird vor der späteren
  Versionierung/PR-Abgabe erneut geprüft.
- Keine Build-/Testausführung erfolgte, daher wurde der manuelle IDE-`Build`
  nicht inkrementiert. HEAD blieb unverändert und `tasks.md` existiert nicht.

### Normalisierte SHA-256 der geprüften Resume-Eingaben / Normalized SHA-256 of Reviewed Resume Inputs

| Artefakt / Artefact | SHA-256 |
|---|---|
| `spec.md` | `e954207426cca04bce55d26f78fb59cf0a1f03d4afead8d1f8a2e0a00a1e2219` |
| `clarification-report.md` | `c2b3a85f17f24b323e68834c63396dc9b7fa27746e83805eca8247b151b2869a` |
| `checklists/requirements.md` | `81b2565e7ae30f59f1a0088df0d5b4a176b85fa90c1463e97719def1cc8105de` |
| `plan.md` | `424db6e9cb14c3de31c2dd01fa85dfc62dabc4fdd41cb367962efb9e88c8f245` |
| `research.md` | `3663f4dd00de794f0b4e5da15061d79b93fd1a697a824dd7c81fd73514307155` |
| `data-model.md` | `6d6db66da4f64ec6ec86b0a43e4067bb14a93de1e2a1f674392ddfd5d23e0967` |
| `quickstart.md` | `c4603fef0771262434de8f5482be08d850b25b98c4e76e1977f7b7db00d7f2d2` |
| `contracts/assessment-record.schema.json` | `70289e39d0d0bb4e0172f0cceade2fffaa6055014b80a0105ad5cd88a2ee22de` |
| `contracts/evidence-contract.md` | `6509c397b24ff9cdb5dfc590651effc113f73f6c0eec9b59eeeac37acac60104` |
| `contracts/gate-requirements.schema.json` | `e2185118df1aaf4df5dfa20a8dd9d52f6726e4bb4da97c561a74f950ad681c2e` |
| `contracts/vm-hardening-contract.md` | `5b514f2b5256c391fef95c3f747cdd98f50e57410fb005770630db010e7d615b` |
| `gate-requirements.json` | `9a4473b03e8f1b27e15917b819571dff731e4860ec75d408a6e5e2576aeccde2` |

## Restrisiko / Residual Risk

Das verbleibende Risiko liegt ausschließlich in späteren, ausdrücklich noch
nicht ausgeführten Arbeiten: Providerstatus und PR-Slot können driften;
Finding-Validatoren müssen ihre erwarteten roten Signale erst erzeugen;
Abhängigkeiten und Actions müssen vor Aufnahme geprüft werden; Remote-, A11Y-,
Coverage-, Security-, Review- und PostMerge-Evidence muss am exakten späteren
HEAD entstehen. Jede solche Abweichung löst das dokumentierte Revalidation-
oder Stop-Gate aus. Sie ist keine offene Planinkonsistenz und erteilt keine
Umsetzungsberechtigung.

*Residual risk is limited to explicitly unexecuted later work: provider state
may drift, findings still need their expected red evidence, dependencies must
be reviewed before adoption, and remote/accessibility/coverage/security/review
evidence must bind the later exact HEAD. Every drift has a fail-closed trigger;
none grants implementation authority.*

## Schlussfolgerung / Conclusion

**Pass.** Nach minimaler Remediation bestehen **0 offene Critical-, 0 offene
High- und 0 offene Medium-Befunde**. Intake- und Scope-Treue, evidence-first-
Autorisierung, technische Machbarkeit, Modulabhängigkeiten, aktuelle Code- und
Workflow-Realität, Security/Privacy, A11Y/Lernpolitik, Cross-Platform-Parität,
Dependency-/Toolrisiken, tasks-fähige Dateiflächen und schema-gültige,
remote-beweisbare Gates sind für diesen Planungsstand konsistent. Dieses
Ergebnis schließt nur `plan-review` ab; es erstellt keine Tasks und startet
keine weitere Phase.

*Pass. Zero Critical, High, or Medium findings remain. Intake/scope fidelity,
evidence-first authorisation, feasibility, module boundaries, current reality,
security/privacy, accessibility/learner policy, cross-platform parity,
dependency/tool risks, task-sized surfaces, and schema-valid remotely provable
gates are consistent for this planning state. This result completes only
plan-review and starts no later phase.*
