# Regulatorische Anwendbarkeit / Regulatory Applicability

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
