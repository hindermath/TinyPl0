# SAMM-Assessment / SAMM Assessment: TinyPl0

**Feature / Phase**: `004-secure-development-hardening` / implement
**Datum / Date**: 2026-08-30
**Owner / Review**: TinyPl0-Maintainer / unabhängige Security-Review
**Rhythmus / Cadence**: je Release und vierteljährlich / per release and quarterly

Deutsch: Die Werte sind eine interne OWASP-SAMM-Selbsteinschätzung von 1 bis 3,
keine Zertifizierung. English: Scores are an internal OWASP SAMM self-
assessment from 1 through 3, not a certification.

| Funktion / Function | Praxis / Practice | Ist / Current | Ziel / Target | Evidence / Grenze |
|---|---|---:|---:|---|
| Governance | Strategy & Metrics | 1 | 2 | Assessment/Statistik vorhanden; Trend- und Releasekennzahlen ausbauen |
| Governance | Policy & Compliance | 2 | 2 | Constitution, 157-ID-Matrix, Regulatory/CRA |
| Design | Threat Assessment | 2 | 2 | STRIDE/CIA/CAPEC und Restrisiken |
| Design | Secure Architecture | 2 | 2 | arc42, ADR/S-ADR, Trust Boundaries |
| Implementation | Secure Build | 1 | 2 | Pins/SBOM lokal; Provider-Provenienz bleibt offen |
| Implementation | Defect Management | 2 | 2 | Finding-Rot→Grün, Owner, Trigger |
| Verification | Requirements-driven Testing | 2 | 2 | VM-TDD; finaler exact-head Lauf später |
| Verification | Security Testing | 1 | 2 | Dependency/A11Y lokal; Automatisierung ausbauen |
| Operations | Incident Management | 1 | 2 | CVD/security.txt neu; reale Übung fehlt |
| Operations | Environment Management | 1 | 2 | lokale Toolpins; Provider-Assurance begrenzt |

## Priorisierte Verbesserungen / Prioritized Improvements

| ID | Gap / Lücke | Aktion / Action | Priorität | Owner | Termin/Trigger und Evidence-Ziel |
|---|---|---|---|---|---|
| SAMM-001 | Provider-Provenienz offen | Exact-head Attestation nur bei realer Veröffentlichung erfassen | High | Maintainer | nächster Release; Attestation + Artefakthash |
| SAMM-002 | Keine CVD-Übung | Meldungsannahme und Reaktion als Tabletop prüfen | Medium | Security owner | 2026-11-30; Übungsprotokoll ohne vertrauliche Daten |
| SAMM-003 | Dependency-Folge manuell | Renovate/Dependabot oder gleichwertig entscheiden | Medium | Maintainer | 2026-11-30; Policy und erster geprüfter Update-PR |
| SAMM-004 | Security-Metriken jung | Findings und Gate-Wiederholungen je Release fortschreiben | Low | Maintainer | nächster Release; Statistik-/Retrospektiveneintrag |

Wiedervorlage bei Release, neuem Trust Boundary oder spätestens 2026-11-30. / Re-evaluate on release, a new trust boundary, or by 2026-11-30.
