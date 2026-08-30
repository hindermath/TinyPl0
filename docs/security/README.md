# Sicherheitsdokumentation / Security Documentation

**Repository**: TinyPl0 (Level 2)
**Feature**: `004-secure-development-hardening`
**Stand / Date**: 2026-08-30
**Home-Sync**: `false`

Deutsch: Dieser text-first Index trennt veröffentlichbare Lern-/Policy-Inhalte
von quellinternen Auditdatensätzen. Statusfarben sind nicht erforderlich;
Links, Pfade und Statuswörter tragen die Bedeutung. English: This text-first
index separates public learning/policy content from source-only audit records.
Links, paths, and status words carry the meaning without colour.

## Öffentliche Evidence / Public Evidence

| Thema / Topic | Dokument / Document | Status |
|---|---|---|
| Bedrohungsmodell / threat model | [threat-model.md](threat-model.md) | Applicable, reconciled |
| arc42 Security | [arc42-security.md](arc42-security.md) | Applicable, reconciled |
| Security Checklist | [security-checklist.md](security-checklist.md) | Applicable; local DocFX/axe/lynx cycle passed |
| Barrierefreiheit / accessibility | [../accessibility/secure-development-hardening.md](../accessibility/secure-development-hardening.md) | WCAG 2.2 AA text-first evidence passed locally |
| Qualitätsszenarien / quality scenarios | [security-quality-scenarios.md](security-quality-scenarios.md) | Applicable |
| Dependency Audit | [dependency-audit.md](dependency-audit.md) | 0 known Critical/High |
| ASVS 5.0.0 L1 | [asvs-verification.md](asvs-verification.md) | 70/70 mapped |
| Supply Chain | [supply-chain-evidence.md](supply-chain-evidence.md) | local SBOM/artifact evidence; provider claims bounded |
| CRA | [cra-applicability.md](cra-applicability.md) | technical readiness; business role Open |
| Regulierung / regulation | [regulatory-applicability.md](regulatory-applicability.md) | reasoned N/A decisions |
| Cloud-Autonomie / cloud autonomy | [cloud-autonomy-applicability.md](cloud-autonomy-applicability.md) | C3A lens Applicable to delivery |
| Cloud-Assurance | [cloud-compliance-assurance.md](cloud-compliance-assurance.md) | C5 lens; no certification claim |
| Zero Trust | [zero-trust-applicability.md](zero-trust-applicability.md) | N/A with trigger |
| SAMM | [samm-assessment.md](samm-assessment.md) | internal maturity snapshot |
| CVD Policy | `.github/SECURITY.md` | public policy source; published through the repository host |
| Security contact | [../../docfx/.well-known/security.txt](../../docfx/.well-known/security.txt) | RFC 9116 publication source |

## Quellinterne Audit-Evidence / Source-only Audit Evidence

| Thema / Topic | Pfad / Path | Zweck / Purpose |
|---|---|---|
| 157-ID-Assessment | `secure-development/2026-08-30-tinypl0-hardening/assessment.json` | machine-readable canonical assessment |
| Findings | [secure-development/2026-08-30-tinypl0-hardening/findings.md](secure-development/2026-08-30-tinypl0-hardening/findings.md) | authorisation and closure history |
| Restrisiken / residual risks | [secure-development/2026-08-30-tinypl0-hardening/residual-risks.md](secure-development/2026-08-30-tinypl0-hardening/residual-risks.md) | owner/trigger/evidence targets |
| ASVS machine record | `asvs-verification.json` | official ordered 70-ID mapping |
| Supply-chain machine record | `supply-chain-evidence.json` | hashes, manifest, VEX/SLSA boundaries |

## Entscheidungen / Decisions

- Allgemeine ADR: [../architecture/adr/0001-vm-resource-budget.md](../architecture/adr/0001-vm-resource-budget.md).
- Security ADR: [adr/0001-vm-resource-budget.md](adr/0001-vm-resource-budget.md).
- Architektur: [../architecture/secure-development-hardening.md](../architecture/secure-development-hardening.md).
- Assessment-Leseeinstieg: [secure-development/2026-08-30-tinypl0-hardening/README.md](secure-development/2026-08-30-tinypl0-hardening/README.md).

Diese Evidence unterstützt interne Audit- und Zertifizierungsvorbereitung. Sie
ersetzt keine externe Prüfung, Provider-Attestation oder Rechtsberatung. / This
evidence supports internal audit preparation and does not replace an external
audit, provider attestation, or legal advice.
