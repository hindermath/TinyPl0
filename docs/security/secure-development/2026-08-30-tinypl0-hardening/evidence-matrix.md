# Evidence-Matrix / Evidence Matrix

## Aktuelle Assurance-Revalidierung / Current Assurance Revalidation

**DE:** Am 2026-09-08 wurde die repository-lokale Secure-Development-Baseline
auf Version 3.2.0 synchronisiert und der technische Evidence-Vertrag des
13. Presets erneut geprüft. Baseline, Delta, Closure und Image Impact sind
Ready; der strengste technische Gesamtstatus ist Ready. Das ist keine
fachliche Neubewertung der 157 Kontrollpunkte. 11 Kontrollen sind als erfüllt,
94 als teilweise erfüllt und 52 als nicht anwendbar dokumentiert.
Pilotfreigabe, Projektabnahme und allgemeine Freigabe bleiben Open.

**EN:** On 2026-09-08, the repository-local secure-development baseline was
synchronized to version 3.2.0 and the thirteenth preset's technical evidence
contract was revalidated. Baseline, delta, closure, and image impact are
Ready; the strictest technical overall status is Ready. This is not a new
domain assessment of the 157 controls. 11 controls are recorded as fulfilled,
94 as partly fulfilled, and 52 as not applicable. Pilot
authorization, project acceptance, and general release remain Open.

**DE:** Aktuelle maschinenlesbare Gates sind baseline.json,
deltas/2026-09-08-assurance-revalidation.json, closure.json und
image-impact.json. Die ersetzten blockierten Migrations-Gates bleiben unter
archive/2026-09-07-assurance-migration/ erhalten. Umfang und ausführbare
Nachweise stehen in assurance-revalidation.md und assurance-validation.json.

**EN:** Current machine-readable gates are baseline.json,
deltas/2026-09-08-assurance-revalidation.json, closure.json, and
image-impact.json. The superseded blocked migration gates remain under
archive/2026-09-07-assurance-migration/. See assurance-revalidation.md and
assurance-validation.json for scope and executable proof.

## Zweck und Grenze / Purpose and Boundary

Dieser am 2026-09-07 erstellte Index erschließt die **vorhandenen historischen**
Kontrollbewertungen. Er ist kein neuer fachlicher Review und bestätigt weder
Aktualität noch Erfüllung der referenzierten Nachweise. Die Statuswerte werden
unverändert aus [assessment.json](assessment.json) übernommen, nicht in Assurance-Gate-Ergebnisse
umgedeutet. Rollen in der Quelle sind keine nachträglich erteilten Freigaben.

*This index, added on 2026-09-07, exposes existing historical assessments. It is
not a new domain review or a freshness/fulfilment confirmation. Source states
are copied literally, never converted into Assurance gate outcomes. Source
roles do not constitute newly granted approvals.*

## Quellenbindung / Source Binding

- Kontext / context: `2026-08-30-tinypl0-hardening`.
- Kanonische Bewertungsquelle / canonical assessment source: [assessment.json](assessment.json).
- SHA-256 der unveränderten Dateibytes / SHA-256 of unchanged file bytes:
  `78429eab1ba1ca5f3764e71bdef9b897da067ad0bec66de775f568a27c2f0b2c`.
- Git-Stand vor Indexergänzung / Git HEAD before adding this index: `a3835067ef3cfad37248f2e5be2329fb2694a289`.
- Umfang / scope: 157 eindeutige Kontroll-IDs / unique control IDs.
- Hash oben ist ein Dateibyte-Hash, keine neue normalisierte Baseline-Bindung.
  / The hash above is a raw file hash, not a new normalized baseline binding.
- Owner, Reviewer, Begründung, Risiken, Fristen, Trigger und Grenzen stehen
  vollständig in der verlinkten Quelle; sie werden nicht neu festgelegt.
  / Owner, reviewer, rationale, risks, deadlines, triggers and boundaries remain
  in the linked source; none is newly assigned by this index.

### Quellenstatus, nicht Assurance-Ergebnis / Source States, Not Assurance Outcomes

- `Applicable / Partly Fulfilled`: 94
- `N/A / Not Assessed`: 52
- `Applicable / Fulfilled`: 11

## Kontrollzuordnung / Control Mapping

Jede Zeile benennt die unveränderte Quellbewertung, vorhandene Evidence-IDs und
den nullbasierten JSON-Array-Pfad in der oben verlinkten Datei. Fehlende
Referenzen bleiben ausdrücklich sichtbar. Verweis-IDs sind keine Prüfung ihrer
Aktualität oder Wirksamkeit. / Each row retains its source state and existing
evidence IDs, with a zero-based JSON array path. Missing references stay visible;
an evidence ID does not prove freshness or effectiveness.

| Kontroll-ID / Control ID | Quellenstatus / Source state | Evidence-ID | JSON-Pfad / JSON path |
|---|---|---|---|
| CL-01-01 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[0]` |
| CL-01-02 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[1]` |
| CL-01-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[2]` |
| CL-01-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[3]` |
| CL-01-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[4]` |
| CL-01-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[5]` |
| CL-01-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[6]` |
| CL-01-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[7]` |
| CL-01-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[8]` |
| CL-01-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[9]` |
| CL-01-11 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[10]` |
| CL-01-12 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[11]` |
| CL-02-01 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[12]` |
| CL-02-02 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[13]` |
| CL-02-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[14]` |
| CL-02-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[15]` |
| CL-02-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[16]` |
| CL-02-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[17]` |
| CL-02-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[18]` |
| CL-02-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[19]` |
| CL-02-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[20]` |
| CL-02-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[21]` |
| CL-02-11 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[22]` |
| CL-02-12 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[23]` |
| CL-02-13 | N/A / Not Assessed | `docs/security/cloud-compliance-assurance.md` | `items[24]` |
| CL-03-01 | N/A / Not Assessed | Keine Referenz / no reference | `items[25]` |
| CL-03-02 | N/A / Not Assessed | Keine Referenz / no reference | `items[26]` |
| CL-03-03 | N/A / Not Assessed | Keine Referenz / no reference | `items[27]` |
| CL-03-04 | N/A / Not Assessed | Keine Referenz / no reference | `items[28]` |
| CL-03-05 | N/A / Not Assessed | Keine Referenz / no reference | `items[29]` |
| CL-03-06 | N/A / Not Assessed | Keine Referenz / no reference | `items[30]` |
| CL-03-07 | N/A / Not Assessed | Keine Referenz / no reference | `items[31]` |
| CL-03-08 | N/A / Not Assessed | Keine Referenz / no reference | `items[32]` |
| CL-03-09 | N/A / Not Assessed | Keine Referenz / no reference | `items[33]` |
| CL-03-10 | N/A / Not Assessed | Keine Referenz / no reference | `items[34]` |
| CL-03-11 | N/A / Not Assessed | Keine Referenz / no reference | `items[35]` |
| CL-03-12 | N/A / Not Assessed | Keine Referenz / no reference | `items[36]` |
| CL-03-13 | N/A / Not Assessed | Keine Referenz / no reference | `items[37]` |
| CL-03-14 | N/A / Not Assessed | Keine Referenz / no reference | `items[38]` |
| CL-03-15 | N/A / Not Assessed | Keine Referenz / no reference | `items[39]` |
| CL-04-01 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[40]` |
| CL-04-02 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[41]` |
| CL-04-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[42]` |
| CL-04-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[43]` |
| CL-04-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[44]` |
| CL-04-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[45]` |
| CL-04-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[46]` |
| CL-04-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[47]` |
| CL-04-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[48]` |
| CL-04-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[49]` |
| CL-05-01 | Applicable / Fulfilled | T072-SC-CL-05-01 | `items[50]` |
| CL-05-02 | Applicable / Fulfilled | T072-SC-CL-05-02 | `items[51]` |
| CL-05-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[52]` |
| CL-05-04 | Applicable / Fulfilled | T072-SC-CL-05-04 | `items[53]` |
| CL-05-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[54]` |
| CL-05-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[55]` |
| CL-05-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[56]` |
| CL-05-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[57]` |
| CL-05-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[58]` |
| CL-05-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[59]` |
| CL-05-11 | Applicable / Fulfilled | T072-SC-CL-05-11 | `items[60]` |
| CL-05-12 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[61]` |
| CL-05-13 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[62]` |
| CL-06-01 | Applicable / Fulfilled | T072-CVD-CL-06-01 | `items[63]` |
| CL-06-02 | Applicable / Fulfilled | T072-CVD-CL-06-02 | `items[64]` |
| CL-06-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[65]` |
| CL-06-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[66]` |
| CL-06-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[67]` |
| CL-06-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[68]` |
| CL-06-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[69]` |
| CL-06-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[70]` |
| CL-06-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[71]` |
| CL-06-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[72]` |
| CL-06-11 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[73]` |
| CL-07-01 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[74]` |
| CL-07-02 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[75]` |
| CL-07-03 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[76]` |
| CL-07-04 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[77]` |
| CL-07-05 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[78]` |
| CL-07-06 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[79]` |
| CL-07-07 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[80]` |
| CL-07-08 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[81]` |
| CL-07-09 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[82]` |
| CL-07-10 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[83]` |
| CL-07-11 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[84]` |
| CL-07-12 | N/A / Not Assessed | `docs/security/cra-applicability.md`; `docs/security/regulatory-applicability.md` | `items[85]` |
| CL-08-01 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[86]` |
| CL-08-02 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[87]` |
| CL-08-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[88]` |
| CL-08-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[89]` |
| CL-08-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[90]` |
| CL-08-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[91]` |
| CL-08-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[92]` |
| CL-08-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[93]` |
| CL-08-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[94]` |
| CL-08-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[95]` |
| CL-08-11 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[96]` |
| CL-08-12 | Applicable / Fulfilled | T072-A11Y-CL-08-12-INVENTORY, T078-A11Y-CL-08-12-HOST | `items[97]` |
| CL-08-13 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[98]` |
| CL-09-01 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[99]` |
| CL-09-02 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[100]` |
| CL-09-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[101]` |
| CL-09-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[102]` |
| CL-09-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[103]` |
| CL-09-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[104]` |
| CL-09-07 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[105]` |
| CL-09-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[106]` |
| CL-09-09 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[107]` |
| CL-09-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[108]` |
| CL-09-11 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[109]` |
| CL-09-12 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[110]` |
| CL-09-13 | Applicable / Fulfilled | EV-CL-09-13-AGENTS, EV-CL-09-13-CLAUDE, EV-CL-09-13-GEMINI, EV-CL-09-13-COPILOT, EV-CL-09-13-COPILOT-AGENT | `items[111]` |
| CL-09-14 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[112]` |
| CL-09-15 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[113]` |
| CL-09-16 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[114]` |
| CL-09-17 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[115]` |
| CL-10-01 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[116]` |
| CL-10-02 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[117]` |
| CL-10-03 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[118]` |
| CL-10-04 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[119]` |
| CL-10-05 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[120]` |
| CL-10-06 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[121]` |
| CL-10-07 | Applicable / Fulfilled | T072-GITIGNORE-CL-10-07 | `items[122]` |
| CL-10-08 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[123]` |
| CL-10-09 | Applicable / Fulfilled | T072-A11Y-CL-10-09-INVENTORY, T080-A11Y-CL-10-09-HOST | `items[124]` |
| CL-10-10 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[125]` |
| CL-10-11 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[126]` |
| CL-10-12 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[127]` |
| CL-10-13 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[128]` |
| CL-10-14 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[129]` |
| CL-10-15 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[130]` |
| CL-10-16 | Applicable / Partly Fulfilled | Keine Referenz / no reference | `items[131]` |
| CL-10-17 | Applicable / Fulfilled | T072-BASELINE-CL-10-17 | `items[132]` |
| CL-11-01 | N/A / Not Assessed | Keine Referenz / no reference | `items[133]` |
| CL-11-02 | N/A / Not Assessed | Keine Referenz / no reference | `items[134]` |
| CL-11-03 | N/A / Not Assessed | Keine Referenz / no reference | `items[135]` |
| CL-11-04 | N/A / Not Assessed | Keine Referenz / no reference | `items[136]` |
| CL-11-05 | N/A / Not Assessed | Keine Referenz / no reference | `items[137]` |
| CL-11-06 | N/A / Not Assessed | Keine Referenz / no reference | `items[138]` |
| CL-11-07 | N/A / Not Assessed | Keine Referenz / no reference | `items[139]` |
| CL-11-08 | N/A / Not Assessed | Keine Referenz / no reference | `items[140]` |
| CL-11-09 | N/A / Not Assessed | Keine Referenz / no reference | `items[141]` |
| CL-11-10 | N/A / Not Assessed | Keine Referenz / no reference | `items[142]` |
| CL-11-11 | N/A / Not Assessed | Keine Referenz / no reference | `items[143]` |
| CL-11-12 | N/A / Not Assessed | Keine Referenz / no reference | `items[144]` |
| CL-12-01 | N/A / Not Assessed | Keine Referenz / no reference | `items[145]` |
| CL-12-02 | N/A / Not Assessed | Keine Referenz / no reference | `items[146]` |
| CL-12-03 | N/A / Not Assessed | Keine Referenz / no reference | `items[147]` |
| CL-12-04 | N/A / Not Assessed | Keine Referenz / no reference | `items[148]` |
| CL-12-05 | N/A / Not Assessed | Keine Referenz / no reference | `items[149]` |
| CL-12-06 | N/A / Not Assessed | Keine Referenz / no reference | `items[150]` |
| CL-12-07 | N/A / Not Assessed | Keine Referenz / no reference | `items[151]` |
| CL-12-08 | N/A / Not Assessed | Keine Referenz / no reference | `items[152]` |
| CL-12-09 | N/A / Not Assessed | Keine Referenz / no reference | `items[153]` |
| CL-12-10 | N/A / Not Assessed | Keine Referenz / no reference | `items[154]` |
| CL-12-11 | N/A / Not Assessed | Keine Referenz / no reference | `items[155]` |
| CL-12-12 | N/A / Not Assessed | Keine Referenz / no reference | `items[156]` |

## Aktuelles Assurance-Ergebnis und nächste Aktion / Current Assurance Result and Next Action

Alle vier Gate-Dateien sind für den exakt gebundenen Development-Kontext
vorhanden und technisch `Ready`; `technicalValidation` ist `Fulfilled`.
`pilotAuthorization`, `projectAcceptance` und `generalRelease` bleiben
ausdrücklich `Open`. Die technische Evidence wird am 2027-09-08 erneut geprüft.
C5 sowie CRA und formale Produktkonformität sind für den gegenwärtigen
nichtkommerziellen Ausbildungs- und Beispielscope `N/A`; die regulatorische
Scopeprüfung wird am 2026-12-31 oder früher bei Marktbereitstellung,
kommerzieller Nutzung, Kundenübergabe, Supportvertrag oder geänderter
Hersteller-/Steward-Rolle wiederholt.

TinyPl0 empfiehlt für diesen begrenzten Projektfeldtest
`ReleaseAccepted` für das unveränderte Preset v0.1.3. Die nächste zentrale
Aktion bleibt ausgesetzt, bis alle fünf Projektberichte vorliegen und
`github/spec-kit#4455` entschieden ist. Daraus entsteht keine Produkt-, C5-,
Konformitäts- oder Zertifizierungsfreigabe.

*All four gate files are technically Ready for the exact development context,
and technical validation is Fulfilled. The three human decisions remain Open.
Technical evidence is due again on 2027-09-08. C5, CRA, and formal product
conformity are N/A for the current non-commercial training and example scope;
regulatory scope is reviewed again on 2026-12-31 or earlier upon a defined
commercialization or role-change trigger. TinyPl0 recommends ReleaseAccepted
for the unchanged v0.1.3 preset in this bounded project field test. Central
acceptance waits for all five reports and github/spec-kit#4455.*

## Leserpfad und Dokumentationsauswirkung / Reader Path and Documentation Impact

- [findings](findings.md)
- [README](README.md)
- [residual-risks](residual-risks.md)

Documentation Impact: `UpdateRequired`. Zielgruppen / audiences: Maintainer,
Security-Reviewer und KI-Agenten. Leserpfad / reader path: Integrationsnachweis →
Matrix → kanonisches JSON → bestehende Detaildokumente → separat genehmigte
nächste Aktion. Owner: Repository-Maintainer (Thorsten Hindermann). Dokumentklasse:
Evidence-Navigation, keine normative Richtlinie. DE/EN im selben Dokument;
textorientierte Tabelle, keine farbabhängige Aussage. Repository-lokal, kein
Home-Sync. Re-Evaluation bei Quellhash-, Scope-, Baseline- oder Vertragsänderung.
Prüfung: 157 eindeutige IDs, quellentreue Werte, Dateihash und read-only Status
unter Bash und PowerShell. / Repository-local evidence navigation; same-document
bilingual text, no colour-only meaning, no Home sync. Reevaluate when the source
hash, scope, baseline or contract changes. Validation checks all 157 IDs, literal
source values, file hash and read-only status under both shells.
