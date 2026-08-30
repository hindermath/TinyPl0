# Sandbox-Bewertung für TinyPl0 / Sandbox Assessment for TinyPl0

**Feature / Feature**: `005-sandbox-secure-development`
**Bewertungsdatum / Assessment date**: 2026-08-30
**Verantwortliche Rolle / Responsible role**: TinyPl0-Projektverantwortung
**Prüfrolle / Reviewer role**: unabhängige menschliche Security-/Projektprüfung
**Sandbox-Identität / Sandbox identity**: `container-images/absdd-image-sandbox`
**Beobachtungscommit / Observation commit**:
`05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`
**CL-12-Basis / CL-12 baseline**: Version 2.2.0, Baseline 3.2.0

## Kurzentscheidung / Decision Summary

**DE:** Reguläre oder autonome agentische Schreibarbeit an TinyPl0 in der
beobachteten Sandbox ist derzeit **`Not Ready`**. Die Sandbox beschreibt
nützliche technische Schutzmaßnahmen, besitzt am gebundenen Commit aber keine
vollständige menschliche Betriebsfreigabe, keinen akzeptierten Image-Digest,
keine auf TinyPl0 begrenzten Writable Roots, keine aktuelle Egress-Annahme und
keinen ausgeführten TinyPl0-Baselinelauf. Ein späterer, menschlich genehmigter
Read/Build/Test-Pilot kann **`Conditional Pilot`** werden, nachdem FUP-SBX-001
bis FUP-SBX-008 soweit einschlägig nachgewiesen wurden. `Approved` wird in
diesem Lauf nicht erteilt.

**EN:** Regular or autonomous agentic write work on TinyPl0 in the observed
Sandbox is currently **`Not Ready`**. The Sandbox describes useful technical
controls, but the bound commit has no complete human operating approval, no
accepted image digest, no TinyPl0-only writable roots, no current egress
acceptance, and no executed TinyPl0 baseline. A later human-approved
read/build/test pilot may become a **`Conditional Pilot`** after the applicable
FUP-SBX-001..008 evidence exists. This run cannot grant `Approved`.

| Arbeitsmodus / Work mode | Entscheidung / Decision | Aktuell erlaubt / Currently allowed |
|---|---|---|
| Versionierte Sandbox-Artefakte lesen / Read versioned Sandbox artefacts | `Observed` | Ja, read-only am exakten Commit / Yes, read-only at the exact commit |
| Read/Build/Test-Pilot | `Conditional Pilot`, aktuell `Open` | Nein, erst nach menschlicher Pilotfreigabe und technischer Baseline / No, only after human pilot approval and technical baseline |
| Agentische TinyPl0-Schreibarbeit / Agentic TinyPl0 write work | `Not Ready` | Nein / No |
| Commit, Push, PR oder Merge aus der Sandbox | `Prohibited` im Pilot / in the pilot | Nein; beim autorisierten TinyPl0-Orchestrator / No; remains with the authorised TinyPl0 orchestrator |
| Secret-Werte im Repository, Prompt oder Log | `Prohibited` | Nie / Never |

## Begriffe und Beweisgrenze / Terms and Proof Boundary

**DE:** Eine Sandbox ist eine begrenzte Entwicklungsumgebung. Ein *Mount*
bindet einen ausdrücklich gewählten Hostbereich in diese Umgebung ein.
*Writable Root* bedeutet einen Pfad, in den ein Agent schreiben darf. *Egress*
ist ausgehender Netzwerkverkehr. *Evidence* ist ein prüfbarer Nachweis. Eine
beschriebene Fähigkeit ist noch keine ausgeführte TinyPl0-Prüfung, und eine
technische Prüfung ersetzt keine menschliche Freigabe.

**EN:** A Sandbox is a constrained development environment. A *mount* exposes
one explicitly selected host area inside it. A *writable root* is a path an
agent may change. *Egress* is outbound network traffic. *Evidence* is a
reviewable proof. A described capability is not an executed TinyPl0 check, and
a technical check does not replace human approval.

Die Bewertung liest ausschließlich versionierte Dateien am Beobachtungscommit.
Nicht übernommene Änderungen des getrennten Sandbox-Checkouts sind
ausgeschlossen. Alle Hostquellen werden symbolisch benannt; kein konkreter
privater Hostpfad ist Teil dieser Evidence.

*The assessment reads only versioned files at the observation commit.
Uncommitted changes in the separate Sandbox checkout are excluded. Host
sources use symbols; this evidence contains no concrete private host path.*

## Drei Stop-Bedingungen / Three Stop Conditions

1. Ein Secret, privates Profil, Cookie, Token oder Sitzungsinhalt wird sichtbar.
   / A secret, private profile, cookie, token, or session content becomes
   visible.
2. Ein unerwarteter Hostbereich, ein Home-Verzeichnis oder ein anderes Projekt
   ist gemountet oder beschreibbar. / An unexpected host area, home directory,
   or another project is mounted or writable.
3. Commit, Image-Digest, Freigabe, Egress-Entscheidung oder TinyPl0-Baseline
   stimmt nicht mit der genehmigten Pilotakte überein. / The commit, image
   digest, approval, egress decision, or TinyPl0 baseline differs from the
   approved pilot record.

## CL-12-Bewertungen / CL-12 Assessments

### CL-12-01: Initialfreigabe der Sandbox / Initial Sandbox Approval

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Not Fulfilled` |
| Learning stage / Lernstufe | `Grundlage` |
| Responsible role / Verantwortliche Rolle | Projektverantwortung gemeinsam mit Security-/Ausbildungsverantwortung |
| Reviewer role / Prüfrolle | unabhängige menschliche Security- oder Projektprüfung |
| Rationale / Begründung | **DE:** Der Referenzstand enthält nur einen Freigabeentwurf. Verantwortliche Person, technische Identität, genehmigte Mount-Liste, Werkzeuge/Modelle und Ablaufdatum sind nicht vollständig freigegeben. **EN:** The reference contains an approval draft only. Responsible person, technical identity, approved mounts, tools/models, and expiry are not fully approved. |
| Evidence / Evidenz | [research.md](research.md), Beobachtungscommit und [autonomous-run-evidence.md](autonomous-run-evidence.md); Zielnachweis FUP-SBX-001 |
| Residual risk / Restrisiko | Ein verfügbarer Container könnte irrtümlich wie eine genehmigte Betriebsumgebung behandelt werden. / An available container could be mistaken for an approved operating environment. |
| Re-evaluation trigger / Neubewertungs-Trigger | Unterzeichnete Freigabe, geänderte Sandbox-Identität oder spätestens 2026-09-15. / Signed approval, changed Sandbox identity, or 2026-09-15 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-001`; Owner: Projekt- und Security-Verantwortung; Ziel: 2026-09-15; erwartet: signierte Pilotakte mit Digest, Tools/Modellen, Mounts, Egress, Person und Ablauf. / Expected: signed pilot record with digest, tools/models, mounts, egress, person, and expiry. |

### CL-12-02: Begrenzte Host-Mounts / Limited Host Mounts

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Grundlage` |
| Responsible role / Verantwortliche Rolle | Sandbox-Betrieb und TinyPl0-Projektverantwortung |
| Reviewer role / Prüfrolle | unabhängige Security-Prüfung |
| Rationale / Begründung | **DE:** Mounts sind explizit beschrieben, aber der Referenzstand erlaubt mehrere beschreibbare Projektfamilien. Ein TinyPl0-Auftrag braucht genau einen Projekt-Mount und getrennte Build-/Audit-Speicher. **EN:** Mounts are explicit, but the reference permits several writable project families. A TinyPl0 job needs one project mount and separate build/audit storage. |
| Evidence / Evidenz | [research.md](research.md), symbolische Mount-Matrix in diesem Dokument; Zielnachweis FUP-SBX-002 |
| Residual risk / Restrisiko | Ein Agent könnte Nachbarprojekte oder unbeauftragte Hostdaten verändern. / An agent could change neighbouring projects or unauthorised host data. |
| Re-evaluation trigger / Neubewertungs-Trigger | Jede Mount-/Writable-Root-Änderung oder spätestens 2026-09-15. / Any mount or writable-root change, or 2026-09-15 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-002`; Owner: Sandbox-Betrieb; Ziel: 2026-09-15; erwartet: genehmigte technische Mount-Liste und negativer Schreibgrenzentest. / Expected: approved technical mount list and negative write-boundary test. |

### CL-12-03: Trennung von Agentendaten und Projektcode / Separation of Agent Data and Project Code

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Applicable` |
| Implementation status / Umsetzungsstatus | `Fulfilled` |
| Learning stage / Lernstufe | `Grundlage` |
| Responsible role / Verantwortliche Rolle | Sandbox-Betrieb und Repository-Verantwortung |
| Reviewer role / Prüfrolle | Projektprüfung |
| Rationale / Begründung | **DE:** Der stabile Referenzstand beschreibt getrennte Agenten- und Build-Volumes. Die TinyPl0-Delivery-Menge enthält keine Agentenprofile, Caches oder Sitzungsdaten. **EN:** The stable reference describes separate agent and build volumes. The TinyPl0 delivery set contains no agent profiles, caches, or session data. |
| Evidence / Evidenz | [research.md](research.md), [autonomous-run-evidence.md](autonomous-run-evidence.md), Delivery-Set-Inventar T007 |
| Residual risk / Restrisiko | `None` für die dokumentierte Trennungsregel; die reale Pilotkonfiguration wird zusätzlich durch FUP-SBX-002 negativ geprüft. / `None` for the documented separation rule; FUP-SBX-002 will additionally test the actual pilot configuration. |
| Re-evaluation trigger / Neubewertungs-Trigger | Neue Agentenprofile, Volumes, Cachepfade oder Repository-Regeln. / New agent profiles, volumes, cache paths, or repository rules. |
| Next action / Nächste Maßnahme | `N/A`; erfüllt durch vorhandene, überprüfbare Evidence. Bei Konfigurationsänderung erneut prüfen. / Fulfilled by existing reviewable evidence; reassess on configuration change. |

### CL-12-04: Schutz von Geheimnissen / Secrets Protection

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Grundlage` |
| Responsible role / Verantwortliche Rolle | Projektverantwortung und Provider-/Secret-Store-Verantwortung |
| Reviewer role / Prüfrolle | unabhängige Security-Prüfung |
| Rationale / Begründung | **DE:** Repository- und Prompt-Regeln untersagen Secret-Offenlegung. Ein für TinyPl0 genehmigter Injektionsweg und ein Providerinventar wurden auf dem akzeptierten Sandbox-Stand jedoch nicht ausgeführt. **EN:** Repository and prompt rules prohibit secret exposure. However, no TinyPl0-approved injection path and provider inventory were executed on the accepted Sandbox state. |
| Evidence / Evidenz | [quickstart.md](quickstart.md), [autonomous-run-evidence.md](autonomous-run-evidence.md), späterer Delivery-Scan; Zielnachweis FUP-SBX-003 |
| Residual risk / Restrisiko | Ungeeignete Mounts oder Logs könnten Zugangsdaten offenlegen. / Unsuitable mounts or logs could expose credentials. |
| Re-evaluation trigger / Neubewertungs-Trigger | Erster Providerzugriff, neue Credential-Art oder spätestens 2026-09-15. / First provider access, new credential type, or 2026-09-15 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-003`; Owner: Provider-/Secret-Store-Verantwortung; Ziel: 2026-09-15; erwartet: genehmigtes Inventar, untracked Injektionsweg und Stop-on-Exposure-Test ohne Secret-Wert. / Expected: approved inventory, untracked injection path, and stop-on-exposure test without a secret value. |

### CL-12-05: Genehmigte und gepinnte Werkzeuge und Modelle / Approved and Pinned Tools and Models

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Aufbau` |
| Responsible role / Verantwortliche Rolle | Sandbox-Betrieb, KI-/Provider-Verantwortung und Projektverantwortung |
| Reviewer role / Prüfrolle | unabhängige Security-/Supply-Chain-Prüfung |
| Rationale / Begründung | **DE:** Der Referenzstand pinnt Basisimage und Werkzeuge in versionierten Dateien. Es fehlen ein akzeptierter resultierender Image-Digest, das genehmigte Tool-/Modellinventar und eine reale TinyPl0-Versionsausgabe. **EN:** The reference pins its base image and tools in versioned files. An accepted resulting image digest, approved tool/model inventory, and actual TinyPl0 version output are missing. |
| Evidence / Evidenz | [research.md](research.md), Beobachtungscommit; Zielnachweis FUP-SBX-004 |
| Residual risk / Restrisiko | Beschreibung und tatsächlich ausgeführtes Image könnten auseinanderfallen. / The description and the image that actually runs could differ. |
| Re-evaluation trigger / Neubewertungs-Trigger | Image-Neubau, Tool-/Modellwechsel oder spätestens 2026-09-22. / Image rebuild, tool/model change, or 2026-09-22 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-004`; Owner: Sandbox-Betrieb; Ziel: 2026-09-22; erwartet: Image-Digest, gepinnte Inventarliste, Versionsausgaben und TinyPl0-Baselineprotokoll. / Expected: image digest, pinned inventory, version outputs, and TinyPl0 baseline record. |

### CL-12-06: GitHub Spec Kit und Governance-Presets / GitHub Spec Kit and Governance Presets

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Applicable` |
| Implementation status / Umsetzungsstatus | `Fulfilled` |
| Learning stage / Lernstufe | `Aufbau` |
| Responsible role / Verantwortliche Rolle | TinyPl0-Projektverantwortung |
| Reviewer role / Prüfrolle | unabhängige Feature-Prüfung |
| Rationale / Begründung | **DE:** Spec Kit ist initialisiert. Specify, Clarify, Checklist, Plan, Tasks und Analyze besitzen Feature-Artefakte und strukturierte Resultate. Alle acht Presets sind versioniert und wirksam aufgelöst. **EN:** Spec Kit is initialised. Specify, Clarify, Checklist, Plan, Tasks, and Analyze have feature artefacts and structured results. All eight presets are versioned and effectively resolved. |
| Evidence / Evidenz | [spec.md](spec.md), [plan.md](plan.md), [tasks.md](tasks.md), [analyze-report.md](analyze-report.md), [evidence-matrix.md](evidence-matrix.md), `.specify/presets/` |
| Residual risk / Restrisiko | `None` für die aktuelle Feature-Steuerung; Preset-Aktualität wird getrennt unter CL-12-12 bewertet. / `None` for current feature governance; preset currency is assessed separately under CL-12-12. |
| Re-evaluation trigger / Neubewertungs-Trigger | Spec-Kit-, Preset-, Constitution- oder Workflowänderung. / Spec Kit, preset, constitution, or workflow change. |
| Next action / Nächste Maßnahme | `N/A`; aktuelle Feature-Evidence ist vorhanden. / Current feature evidence exists. |

### CL-12-07: Menschliche Prüfung / Human Review

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Not Assessed` |
| Learning stage / Lernstufe | `Grundlage` |
| Responsible role / Verantwortliche Rolle | unabhängige menschliche Reviewer-Rolle |
| Reviewer role / Prüfrolle | eine zweite, vom Autor getrennte Person |
| Rationale / Begründung | **DE:** Der Produkt-PR dieses Laufs ist bei Erstellung der Bewertung noch nicht genehmigt. Außerdem braucht ein späterer Sandbox-Pilot eine eigene menschliche Betriebsentscheidung; Kommentar oder Admin-Bypass genügt nicht. **EN:** The product PR is not yet approved when this assessment is authored. A later Sandbox pilot also needs its own human operating decision; a comment or admin bypass is insufficient. |
| Evidence / Evidenz | Späterer Exact-Head-PR-Nachweis im [autonomous-run-evidence.md](autonomous-run-evidence.md); Pilotziel FUP-SBX-006 |
| Residual risk / Restrisiko | Ohne getrennte Prüfung könnten Grenzen oder positive Aussagen unbemerkt falsch sein. / Without separate review, boundaries or positive claims could be wrong. |
| Re-evaluation trigger / Neubewertungs-Trigger | Unveränderter PR-Head mit `APPROVED` und jede spätere Pilotentscheidung. / Unchanged PR head with `APPROVED` and every later pilot decision. |
| Next action / Nächste Maßnahme | `FUP-SBX-006`; Owner: unabhängige Reviewer-Rolle; Ziel: vor erstem Pilot, spätestens 2026-09-22; erwartet: genehmigte Review-Entscheidung mit Scope und Head/Digest. / Expected: approving review decision with scope and head/digest. |

### CL-12-08: Audit-Spur und Nachvollziehbarkeit / Audit Trail and Traceability

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Aufbau` |
| Responsible role / Verantwortliche Rolle | Projektverantwortung und Sandbox-Betrieb |
| Reviewer role / Prüfrolle | unabhängige Feature-/Betriebsprüfung |
| Rationale / Begründung | **DE:** Run-ID, Intake-Hashes, Feature-Ziel, Beobachtungscommit und lokale Prüfungen sind nachvollziehbar. Für einen echten Sandbox-Pilot fehlen jedoch akzeptierter Image-Digest, Zeitraum und tatsächliches TinyPl0-Laufprotokoll. **EN:** Run ID, intake hashes, feature goal, observation commit, and local checks are traceable. A real Sandbox pilot still lacks an accepted image digest, time window, and actual TinyPl0 execution record. |
| Evidence / Evidenz | [autonomous-run-state.json](autonomous-run-state.json), [autonomous-run-evidence.md](autonomous-run-evidence.md), Zielnachweis FUP-SBX-004 |
| Residual risk / Restrisiko | Eine spätere Ausführung könnte nicht sicher dem geprüften Image zugeordnet werden. / A later execution might not be reliably tied to the reviewed image. |
| Re-evaluation trigger / Neubewertungs-Trigger | Erster Pilot, neue Image-Identität oder geändertes Auditformat. / First pilot, new image identity, or changed audit format. |
| Next action / Nächste Maßnahme | `FUP-SBX-004`; Owner: Sandbox-Betrieb; Ziel: 2026-09-22; erwartet: Digest-gebundener TinyPl0-Laufdatensatz mit Zeitraum, Zweck, Ergebnis und Reviewer. / Expected: digest-bound TinyPl0 execution record with time, purpose, result, and reviewer. |

### CL-12-09: Sandbox-Typologie und Isolationsnachweis / Sandbox Typology and Isolation Evidence

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Vertiefung` |
| Responsible role / Verantwortliche Rolle | Sandbox-Architektur-/Security-Verantwortung |
| Reviewer role / Prüfrolle | unabhängige Security-Architekturprüfung |
| Rationale / Begründung | **DE:** Die Container-Sandbox dokumentiert Non-Root-Ausführung, `no-new-privileges` und entfernte Linux-Capabilities. Die formelle Schutzklassifikation und die Bewertung der breiten Host-Mounts sind noch nicht genehmigt. **EN:** The container Sandbox documents non-root execution, `no-new-privileges`, and dropped Linux capabilities. Its formal protection classification and the assessment of broad host mounts are not approved yet. |
| Evidence / Evidenz | [research.md](research.md), Beobachtungscommit; Zielnachweis FUP-SBX-005 |
| Residual risk / Restrisiko | Container-Isolation allein schützt nicht vor allen Host-Mount- oder Kernelrisiken. / Container isolation alone does not cover every host-mount or kernel risk. |
| Re-evaluation trigger / Neubewertungs-Trigger | Runtime-, Privileg-, Kernel-, Mount- oder Schutzklassenänderung; spätestens 2026-09-22. / Runtime, privilege, kernel, mount, or protection-class change; 2026-09-22 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-005`; Owner: Sandbox-Security-Architektur; Ziel: 2026-09-22; erwartet: typisierte Isolationsbewertung mit Trust Boundaries, Schutzklasse, Negativtests und akzeptiertem Restrisiko. / Expected: typed isolation assessment with trust boundaries, protection class, negative tests, and accepted residual risk. |

### CL-12-10: Netzwerkrestriktion / Network Restriction

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Aufbau` |
| Responsible role / Verantwortliche Rolle | Sandbox-Netzwerk-/Security-Verantwortung |
| Reviewer role / Prüfrolle | unabhängige Security-Prüfung |
| Rationale / Begründung | **DE:** Der Referenzstand dokumentiert freien Compose-Egress und eine getrennte netzwerklose Workspace-Shell. Eine aktuelle, ausgefüllte Risikoannahme mit Zielmenge und Ablauf fehlt. **EN:** The reference documents unrestricted Compose egress and a separate network-disabled workspace shell. A current completed risk acceptance with destinations and expiry is missing. |
| Evidence / Evidenz | [research.md](research.md), Beobachtungscommit; Zielnachweis FUP-SBX-007 |
| Residual risk / Restrisiko | Freier Egress kann Datenabfluss, unbekannte Downloads oder unkontrollierte Providerzugriffe ermöglichen. / Unrestricted egress can enable data leakage, unknown downloads, or uncontrolled provider access. |
| Re-evaluation trigger / Neubewertungs-Trigger | Netzwerkkonfiguration, Provider-/Registry-Wechsel oder spätestens 2026-09-15. / Network configuration, provider/registry change, or 2026-09-15 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-007`; Owner: Netzwerk-/Security-Verantwortung; Ziel: 2026-09-15; erwartet: Allow-List/Proxy oder befristete begründete Offenheit mit Zielen, Ablauf und Negativtest. / Expected: allow-list/proxy or time-limited justified openness with destinations, expiry, and negative test. |

### CL-12-11: Re-Validierungsstand und Lebenszyklus / Re-Validation Status and Lifecycle

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Open` |
| Implementation status / Umsetzungsstatus | `Not Fulfilled` |
| Learning stage / Lernstufe | `Aufbau` |
| Responsible role / Verantwortliche Rolle | Projekt- und Sandbox-Betriebsverantwortung |
| Reviewer role / Prüfrolle | unabhängige Security-/Projektprüfung |
| Rationale / Begründung | **DE:** Ohne gültige Initialfreigabe gibt es kein belastbares Freigabe- oder Ablaufdatum und keinen nachgewiesenen Entzugs-/Wiederanlaufprozess. **EN:** Without valid initial approval, there is no reliable approval or expiry date and no evidenced withdrawal/re-entry process. |
| Evidence / Evidenz | [research.md](research.md); Zielnachweis FUP-SBX-001 |
| Residual risk / Restrisiko | Veraltete Entscheidungen könnten nach Image-, Mount-, Provider- oder Netzwerkänderungen weiterverwendet werden. / Stale decisions could survive image, mount, provider, or network changes. |
| Re-evaluation trigger / Neubewertungs-Trigger | Initialfreigabe, jede benannte Konfigurationsänderung oder spätestens 2026-09-15. / Initial approval, any named configuration change, or 2026-09-15 at the latest. |
| Next action / Nächste Maßnahme | `FUP-SBX-001`; Owner: Projekt-/Sandbox-Verantwortung; Ziel: 2026-09-15; erwartet: Freigabe- und Ablaufdatum, höchstens zwölf Monate Gültigkeit, Ereignisregister sowie Entzugs-/Wiederanlaufregel. / Expected: approval and expiry, no more than twelve months validity, event log, and withdrawal/re-entry rule. |

### CL-12-12: Preset-Aktualisierung und Inhaltsabdeckung / Preset Updates and Content Coverage

| Pflichtfeld / Required field | Bewertung / Assessment |
|---|---|
| Applicability / Anwendbarkeit | `Applicable` |
| Implementation status / Umsetzungsstatus | `Partly Fulfilled` |
| Learning stage / Lernstufe | `Vertiefung` |
| Responsible role / Verantwortliche Rolle | Spec-Kit-/Projekt-Governance-Verantwortung |
| Reviewer role / Prüfrolle | unabhängige Governance-Prüfung |
| Rationale / Begründung | **DE:** Acht Presets mit Versionen, Prioritäten und wirksamer Auflösung sind lokal belegt. Ein eigenständiger quartalsweiser Katalog-/Quellenreview mit Datum und Owner ist für diesen Bewertungsstand nicht vollständig nachgewiesen. **EN:** Eight presets with versions, priorities, and effective resolution are evidenced locally. A separate quarterly catalogue/source review with date and owner is not fully evidenced for this assessment state. |
| Evidence / Evidenz | [spec.md](spec.md), [plan.md](plan.md), [analyze-report.md](analyze-report.md), `.specify/presets/*/preset.yml`; Zielnachweis FUP-SBX-008 |
| Residual risk / Restrisiko | Veraltete Presets oder geänderte Kataloginhalte könnten unbemerkt bleiben. / Outdated presets or changed catalogue content could remain unnoticed. |
| Re-evaluation trigger / Neubewertungs-Trigger | Quartalsende, Preset-/Katalog-/Priority-/Override-Änderung oder 2026-09-30. / Quarter end, preset/catalogue/priority/override change, or 2026-09-30. |
| Next action / Nächste Maßnahme | `FUP-SBX-008`; Owner: Spec-Kit-Governance; Ziel: 2026-09-30; erwartet: datierter `list`/`info`/`resolve`- und Quellenreview mit Versionen, Prioritäten, Mapping-Lücken und Reviewer. / Expected: dated `list`/`info`/`resolve` and source review with versions, priorities, mapping gaps, and reviewer. |

## Mount- und Schreibgrenzen / Mount and Write Boundaries

**DE:** `ReadOnly` erlaubt Lesen, `ReadWrite` erlaubt Schreiben,
`NotMounted` macht den Hostbereich in der Sandbox unsichtbar, und `Denied`
verbietet Agentenzugriff. Positive Schreibrechte unten sind ein Zielvertrag für
einen späteren Pilot, keine Aussage über den beobachteten Ist-Stand.

**EN:** `ReadOnly` permits reading, `ReadWrite` permits writing, `NotMounted`
hides the host area from the Sandbox, and `Denied` prohibits agent access.
Positive write entries below are a target contract for a later pilot, not a
claim about the observed current state.

| Symbolische Quelle / Source symbol | Sandbox-Ziel / Target | Zweck / Purpose | Pilotzugriff / Pilot access | Agentenzugriff / Agent access | Verbotene Nachbarn / Forbidden neighbours | Evidenzziel / Evidence target |
|---|---|---|---|---|---|---|
| `<TinyPl0-repository>` | `/workspace/TinyPl0` | Versionierte Quellen und Feature-Evidence / versioned sources and feature evidence | `ReadOnly`; erst nach FUP-SBX-002 begrenzt `ReadWrite` | `ReadOnly`; Schreiben `Denied` bis Freigabe | andere Projekte, übergeordnete Wurzeln / other projects, parent roots | genehmigte Mount-Liste und negativer Schreibtest |
| `<TinyPl0-build-volume>` | `/work/build/TinyPl0` | `bin`, `obj`, Coverage und temporäre Build-Ausgaben | `ReadWrite` | `ReadWrite` nur für befehlsgebundenen Build/Test-Pilot | Repository, Profile, Secrets | leerer Start, Artefaktmanifest, Bereinigung |
| `<TinyPl0-audit-output>` | `/work/audit/TinyPl0` | nicht versionierte Logs, SBOM und Scan-Ausgaben | `ReadWrite` | `ReadWrite` ohne Secret-Inhalt | Repository und Agentenprofile | Auditmanifest mit Digest und Zeitfenster |
| `<Agent-state-volume>` | `/work/agent-state` | Agentensitzung und Werkzeugcache getrennt vom Projekt | `ReadWrite` | `ReadWrite`, niemals commitfähig | Repository, Audit-Ausgang, Secrets | negativer Git-/Delivery-Set-Test |
| `<Secret-injection>` | kein Dateimount / no file mount | geschützter Secret Store oder kurzlebige Umgebungsinjektion | `NotMounted` | `Denied` für Werte und dauerhafte Ablage | Repository, Prompt, Log, Cache | FUP-SBX-003 ohne echten Secret-Wert |
| `<Home-and-profiles>` | kein Ziel / no target | Home, Desktop, Downloads, Browserprofile, SSH, GPG, Keychain, Cloud-CLI, Cookies, Token | `NotMounted` | `Denied` | alle Kategorien dieser Zeile | negativer Mount-/Pfadtest |
| `<Other-projects>` | kein Ziel / no target | andere Projektfamilien und Arbeitskopien | `NotMounted` | `Denied` | gesamte Nachbarprojekte | negativer Mount-/Schreibtest |

## Arbeitsort-Matrix / Work-Location Matrix

| Arbeit / Work type | Bevorzugter Ort / Preferred location | Aktueller Status / Current status | Voraussetzung / Prerequisite | Erlaubtes Schreibziel / Permitted writes | Rückfallweg / Safe fallback | Beweisgrenze / Evidence boundary |
|---|---|---|---|---|---|---|
| Restore und Build | `Local` oder `CI`; Sandbox später | Sandbox `Open` | .NET 10, akzeptierter Digest, FUP-SBX-001/002/004 | getrenntes Build-Volume | vorhandener lokaler/CI-Build | Lokal/CI beweist keine Sandbox-Fähigkeit |
| Unit-/Integrations-/Golden-`Test` | `Local` oder `CI`; Sandbox später | Sandbox `Open` | gleiche Identität, unveränderte Tests, befehlsgebundener Pilot | getrenntes Build-Volume | vorhandene lokale/CI-Tests | beschriebene Toolchain ist kein TinyPl0-Test |
| Coverage | `CI` oder `Local` | Sandbox `Open` | Collector vorhanden, Ausgaben getrennt | Build-/Audit-Volume | bestehender Coverage-Workflow | dieser Dokumentationslauf beansprucht keinen neuen Coverage-Wert |
| DocFX-Dokumentationsbau | `CI` oder `Local` | `N/A` für diesen Lauf | API/XML-/DocFX-Änderung als Trigger | Build-/Audit-Volume | bestehender DocFX-Pfad | keine Produktdokumentation geändert |
| Playwright/axe und `lynx` A11Y | `CI` oder `Local` | `N/A` für diesen Lauf | erzeugtes HTML oder Navigationsänderung | Audit-Volume | text-first Markdown-Review | Markdown-Prüfung ersetzt keine spätere HTML-Prüfung |
| Golden-Update | `HumanOnly` plus `Local` | `N/A` für diesen Lauf | absichtliche Compilerausgabe-Änderung und Review | nur genehmigte Golden-Dateien | kein Update | kein Produktverhalten geändert |
| Sandbox-Smoke | `Sandbox` | `Open` | Digest, Freigabe, Mount-/Egress-/Secret-Grenze | Build-/Audit-Volume | lokale Toolprüfung | Smoke beweist nur benannte Befehle auf benanntem Image |
| Agentenanalyse `ReadOnly` | `Sandbox` als späterer Pilot | `Conditional Pilot`, aktuell `Open` | Humanfreigabe, Providerinventar, read-only Projekt | Audit-Volume | autorisierte lokale Analyse | keine Schreib- oder Remote-Berechtigung |
| Agentische Schreibarbeit | `Local` im autorisierten Orchestrator | Sandbox `Prohibited` / `Not Ready` | separate technische Härtung und Freigabe | aktuelle TinyPl0-Delivery-Grenze | kein Sandbox-Schreiben | lokaler Lauf beweist keine Sandbox-Freigabe |
| Secret-/Provideranmeldung | `HumanOnly` | `Open` | FUP-SBX-003 und FUP-SBX-007 | keine Repository-Datei | lokaler geschützter Providerweg | keine Secretwerte in Evidence |
| Commit, Push, PR und Merge | `Local` plus `CI`, mit `HumanOnly`-Review | Sandbox-Pilot `Prohibited` | explizite Delivery-Autorität, Exact-Head-Gates, `APPROVED` | Git-Branch/Remote im autorisierten Lauf | sicherer Stopp vor Remote | Admin-Bypass ersetzt keine Approval |
| Menschliches Review | `HumanOnly` | `Open` bis echte Entscheidung | unveränderter Head/Digest und klarer Scope | keine Inhaltsänderung | neue Review nach Head-Änderung | Kommentar oder unavailable ist keine Approval |

## Produkt- und Image-Lieferkette / Product and Image Supply Chain

| Nachweisart / Evidence type | TinyPl0-Produkt / TinyPl0 product | Sandbox-Image | Aktueller Zustand / Current state |
|---|---|---|---|
| Dependency-Audit | spätere TinyPl0-Paketprüfung unter `docs/security/dependency-audit.md` | OS-/Tool-/Containerabhängigkeiten getrennt prüfen | bestehende Projektpfade nur benannt; neuer Pilotnachweis `Open` |
| SBOM | Produkt-/Release-SBOM, nicht Image-SBOM | Image-SBOM mit exaktem Digest | getrennt; FUP-SBX-004/FUP-SBX-008 |
| Scan und VEX | Produktfunde und Betroffenheit | Image-/OS-/Toolfunde und Betroffenheit | VEX nur bei bekanntem Fund; keine positive Neuaussage in diesem Lauf |
| SLSA-Provenienz | TinyPl0-Build-/Release-Provenienz | Image-Build-Provenienz | getrennte Erzeuger und Artefakthashes erforderlich |
| OpenSSF Scorecard | öffentliches TinyPl0-Repository | gegebenenfalls Sandbox-Repository getrennt | kein Ersatz für Image-Scan oder Betriebsfreigabe |
| Review | TinyPl0-PR und Exact-Head-Approval | Digest-/Konfigurations-/Betriebsreview | beide erforderlich, nicht austauschbar |

## Sicherheits- und Architekturzuordnung / Security and Architecture Mapping

- **NIST SSDF**: Protect Software und Produce Well-Secured Software werden durch
  minimalen Mount, getrennte Ausgaben, überprüfbare Identität und Review
  unterstützt. / Minimal mounts, separate outputs, reviewable identity, and
  review support the SSDF practices.
- **CWE Top 25**: besonders relevant sind Pfadmanipulation, Offenlegung
  sensitiver Informationen, unkontrollierter Ressourcenverbrauch und zu hohe
  Berechtigungen. / Path traversal/manipulation, sensitive-data exposure,
  uncontrolled resources, and excessive privilege are most relevant.
- **STRIDE, CIA und CAPEC**: Trust Boundaries sind Host↔Container,
  Repository↔Build-/Audit-Volume, Secret-Injektion, Egress und Git/CI. Höchste
  Risiken sind Manipulation und Information Disclosure über breite Mounts oder
  freien Egress. / Highest risks are tampering and information disclosure via
  broad mounts or unrestricted egress.
- **Defense in Depth**: Non-Root und entfernte Capabilities sind eine Schicht;
  minimale Mounts, Secret-Trennung, Egress-Entscheidung und Review sind weitere
  unabhängige Schichten.
- **Least Privilege und Fail-Safe Defaults**: Nur benannte Pfade und Befehle
  werden erlaubt; fehlende Identität oder Freigabe ergibt `Not Ready`, nicht
  stillschweigende Erlaubnis.

## Offene Folgeaufgaben / Open Follow-Ups

Diese Aufgaben dokumentieren spätere Arbeit. Sie werden in diesem Feature nicht
ausgeführt. / These tasks document later work and are not executed in this
feature.

| ID | Quellen / Sources | Priorität / Priority | Risiko / Risk | Owner | Nächste Maßnahme / Next action | Zieltermin / Due date | Erwartete Evidence / Expected evidence | Trigger und Scope-Grenze / Trigger and scope boundary |
|---|---|---|---|---|---|---|---|---|
| `FUP-SBX-001` | CL-12-01, CL-12-11 | P0 | Nutzung ohne gültige Freigabe und Lebenszyklus | Projekt-/Security-/Sandbox-Verantwortung | Pilotakte entscheiden und Revalidierung/Entzug definieren | 2026-09-15 | signierte Akte mit Digest, Tools/Modellen, Mounts, Egress, Person, Datum, Ablauf | vor jedem Pilot; menschliche Entscheidung, nicht Agentenscope |
| `FUP-SBX-002` | CL-12-02, Mount-Grenze | P0 | unbeauftragte Änderungen an Nachbarbereichen | Sandbox-Betrieb | TinyPl0-only-Mount und minimale Writable Roots konfigurieren und negativ testen | 2026-09-15 | technische Mount-Liste, leere/read-only Nebenwurzeln, Negativtest | Mount-/Runtime-Änderung; separate Sandbox-Aufgabe |
| `FUP-SBX-003` | CL-12-04 | P0 | Secret- oder Profiloffenlegung | Provider-/Secret-Store-Verantwortung | Inventar und kurzlebige Injektion ohne Dateimount prüfen | 2026-09-15 | Freigabe, synthetischer Stop-Test, null persistierte Werte | erster Providerzugriff; keine Secretwerte in diesem Feature |
| `FUP-SBX-004` | CL-12-05, CL-12-08 | P1 | falsche Image-/Tool-Identität oder unbelegte TinyPl0-Fähigkeit | Sandbox-Betrieb und TinyPl0-Projektverantwortung | Digest binden und Restore/Build/Test/Coverage/Docs/A11Y-Smoke ausführen | 2026-09-22 | Digest, Toolversionen, Befehle, Exitcodes, Artefakthashes und Reviewer | Image-/Toolwechsel; technische Pilotaufgabe |
| `FUP-SBX-005` | CL-12-09 | P1 | unzureichende Isolation trotz Containerkontrollen | Sandbox-Security-Architektur | Typologie, Trust Boundaries und Schutzklasse prüfen | 2026-09-22 | Isolationsbericht, Negativtests, akzeptiertes Restrisiko | Runtime-/Kernel-/Mountänderung; kein Produktarchitektur-Edit hier |
| `FUP-SBX-006` | CL-12-07 | P0 | fehlendes Vier-Augen-Prinzip | unabhängige Reviewer-Rolle | Feature-PR und spätere Pilotakte auf unverändertem Head/Digest genehmigen | 2026-09-22 oder vor Pilot | echte `APPROVED`-Entscheidung mit Scope | jede Head-/Digest-Änderung; Bypass/Kommentar unzureichend |
| `FUP-SBX-007` | CL-12-10 | P0 | Datenabfluss oder unkontrollierter Download | Netzwerk-/Security-Verantwortung | Allow-List/Proxy oder befristete offene Egress-Entscheidung prüfen | 2026-09-15 | Ziele, Konfiguration, Ablauf und negativer Verbindungstest | Provider-/Registry-/Netzwerkänderung; separate Sandbox-Aufgabe |
| `FUP-SBX-008` | CL-12-12, Supply Chain | P2 | Preset-/SBOM-/Scan-Evidence veraltet | Spec-Kit- und Supply-Chain-Governance | Presetquellen, Versionen, Mapping sowie Produkt-/Image-Evidence getrennt reviewen | 2026-09-30 | datierter `list`/`info`/`resolve`-Review und getrennte SBOM-/Scan-Ziele | Quartal oder Katalog-/Presetänderung; bestehende `docs/security/` bleiben hier read-only |

## Schlussfolgerung / Conclusion

Die Sandbox ist eine brauchbare technische Ausgangsbasis, aber keine bereits
genehmigte TinyPl0-Betriebsumgebung. Der sichere nächste Schritt ist kein
autonomer Schreiblauf, sondern ein menschlich genehmigter, eng begrenzter
Read/Build/Test-Pilot mit exakter Identität und negativer Grenzprüfung.

*The Sandbox is a useful technical starting point, but not an already approved
TinyPl0 operating environment. The safe next step is not an autonomous write
run; it is a human-approved, tightly bounded read/build/test pilot with exact
identity and negative boundary tests.*
