# CRA-Anwendbarkeit / CRA Applicability

**Feature**: `004-secure-development-hardening`

**Prüftag / Review date**: `2026-09-08`

**Owner**: Thorsten Hindermann, TinyPl0 project owner

## Entscheidung / Decision

Deutsch: TinyPl0 ist im aktuellen Projektscope ein nichtkommerzieller Lern-
und Beispielcompiler ohne beabsichtigte kommerzielle Nutzung,
Marktbereitstellung, Kundenübergabe oder Supportvertrag. Die CRA-Produkt- und
Konformitätspflichten sind für diesen begrenzten Scope `N/A`. Diese
Scopeentscheidung ist keine Rechtsberatung, Risikoakzeptanz,
Konformitätsbewertung oder Zertifizierung. Die vorhandenen technischen
Sicherheitsmaßnahmen wie CVD, Dependency Review, SBOM, sichere Architektur und
Schwachstellenfolge bleiben unabhängig davon sinnvoll und anwendbar.

English: TinyPl0 is currently a non-commercial teaching and example compiler
with no intended commercial use, market placement, customer handover, or
support contract. CRA product and conformity duties are `N/A` for this bounded
scope. This scope decision is not legal advice, risk acceptance, a conformity
assessment, or certification. Existing technical security practices such as
CVD, dependency review, SBOM, secure architecture, and vulnerability follow-up
remain useful and applicable independently.

| Frage / Question | Stand / State | Trigger und Evidence / Trigger and evidence |
|---|---|---|
| Aktueller wirtschaftlicher Produktscope / current economic product scope | `N/A` | Nichtkommerzielles Ausbildungs- und Beispielprogramm / non-commercial training and example program |
| Hersteller-, Importeur-, Händler- oder Steward-Pflichten / manufacturer, importer, distributor, or steward duties | `N/A` im aktuellen Scope | Neu prüfen, sobald eine solche Rolle, Finanzierung oder Marktbereitstellung entsteht / review when such a role, funding, or market placement appears |
| Formale Konformitäts- oder CE-Entscheidung / formal conformity or CE decision | `N/A` im aktuellen Scope | Keine Behauptung; separate befugte Entscheidung bei Scopeänderung / no claim; separate authorised decision after a scope change |
| Technische Schwachstellenbehandlung / technical vulnerability handling | `Applicable` | `.github/SECURITY.md`, `security.txt`, Dependency- und Supply-Chain-Evidence |

Wiedervorlage ist der `2026-12-31`, früher bei kommerzieller Nutzung,
Marktbereitstellung, Kundenübergabe, Supportvertrag oder geänderter
Hersteller-/Steward-Rolle.

*Re-evaluate on `2026-12-31`, or earlier on commercial use, market placement,
customer handover, a support contract, or a changed manufacturer/steward role.*
