# Regulatorische Anwendbarkeit / Regulatory Applicability

## Feature 006 / Feature 006

- NIST SSDF und CWE Top 25: Applicable; Evidence in VM tests, scripts, review.
- CRA: Open; Owner ist der Maintainer, Wiedervorlage vor öffentlichem Release.
- NIS2 und DORA: N/A für das private Ausbildungsbibliotheks-Feature ohne
  Betreiber-/Finanzdienstscope; Trigger ist ein entsprechender Betrieb.
- EU AI Act und AI-SBOM: N/A, weil KI nur Entwicklungswerkzeug ist.
- OWASP ASVS: N/A, weil kein Web-, HTTP-, Auth- oder API-Dienst geliefert wird.
- Zero Trust, BSI C3A und BSI C5: N/A für die In-Process-Bibliothek; die
  OIDC-Publishergrenze wird als Supply-Chain-Trust-Boundary behandelt.
- SLSA: Zielmodell für Provider-Provenienz; lokal wird keine Stufe behauptet.
- OpenSSF Scorecard und SAMM: Applicable als Remote-Review beziehungsweise
  langfristiger Verbesserungsplan.

*English: NIST SSDF and CWE apply. CRA remains open for maintainer review.
NIS2, DORA, the EU AI Act, AI-SBOM, ASVS, Zero Trust, C3A, and C5 are reasoned
N/A at feature scope with re-evaluation triggers. SLSA is the provider target;
Scorecard and SAMM remain applicable follow-up evidence.*

**Feature**: `004-secure-development-hardening`
**Stand / Date**: 2026-08-30
**Owner / Review**: TinyPl0-Maintainer / unabhängige Rechtsgrenzen-Review

## Entscheidungen / Decisions

| Regelwerk / Topic | Entscheidung / Decision | Begründung / Rationale | Wiedervorlage / Trigger |
|---|---|---|---|
| NIS2 | `N/A` | Kein belegter Betreiber einer wesentlichen oder wichtigen Einrichtung und kein entsprechender Dienst im Produktscope. / No evidenced essential or important entity or service. | Betreiber-, Kunden- oder Sektorrolle ändert sich. |
| EU AI Act | `N/A` | KI wird nur als Entwicklungswerkzeug genutzt; TinyPl0 liefert kein Modell, KI-System oder Inferenzdienst. / AI is development tooling only; no model or AI system ships. | Modell, Agent, Datensatz oder Inferenz wird Produktbestandteil. |
| DORA | `N/A` | Keine Finanzentität und kein belegter ICT-Drittdienst für eine Finanzentität. / No financial entity or evidenced ICT service relationship. | Finanzkunde oder regulierter ICT-Vertrag. |
| Produktkryptografie | `N/A` | Keine Schlüssel, Signaturen, Verschlüsselungs- oder Passwortfunktion im Produkt. / No keys, signatures, encryption, or password feature. | Authentifizierung, Secrets, Signatur oder Verschlüsselung. |
| DPIA / DSGVO Art. 35 | `N/A` | Keine Konten, Telemetrie, Profilbildung oder Verarbeitung personenbezogener Daten im Produktscope. / No accounts, telemetry, profiling, or personal-data processing. | Personenbezug, Empfänger, Aufbewahrung oder Profiling entsteht. |

Deutsch: `N/A` bedeutet hier eine begründete Scope-Entscheidung, keine Aussage
über Organisationen, die TinyPl0 später einsetzen. Offene Geschäftsrollen aus
der CRA-Prüfung gehören dem Maintainer und haben keine agentisch erfundene
Risikoakzeptanz. English: `N/A` is a reasoned scope decision, not a claim about
future users. CRA business roles remain maintainer decisions without invented
risk acceptance.
