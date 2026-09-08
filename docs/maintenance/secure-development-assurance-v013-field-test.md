# Secure Development Assurance v0.1.3 – TinyPl0-Feldtest

## Ergebnis und Scope / Result and Scope

**Empfehlung: `ReleaseAccepted`.** TinyPl0 bestätigt das unveränderte Preset
`secure-development-assurance-governance` v0.1.3 für den dokumentierten
projektbezogenen Feldtest. Die Empfehlung gilt ausschließlich für die
Funktionsfähigkeit des Presets im nichtkommerziellen Ausbildungs- und
Beispielprojekt. Sie ist weder Produktfreigabe noch Pilot-, Projekt-, Risiko-,
C5-, Konformitäts- oder Zertifizierungsentscheidung.

*Recommendation: `ReleaseAccepted`. TinyPl0 confirms the unchanged
`secure-development-assurance-governance` v0.1.3 preset for this documented
project field test. The recommendation covers preset behavior in a
non-commercial training and example project only. It is not a product release,
pilot, project, risk, C5, conformity, or certification decision.*

Test-Owner und technischer Reviewer ist `@hindermath`. Geprüft wurde der
Development-Kontext
`docs/security/secure-development/2026-08-30-tinypl0-hardening`. Produktcode,
Produkt-API, Runtime, Abhängigkeiten und Bildartefakte wurden nicht geändert;
ausschließlich die vorgeschriebenen IDE-Commit-Versionsmetadaten werden mit den
Liefercommits fortgeschrieben.

## Paket- und Umgebungsbindung / Package and Environment Binding

| Feld / Field | Nachweis / Evidence |
|---|---|
| Release | `v0.1.3`, Pre-Release |
| Tag-Commit | `0d03aa9ebe8f74a26e331815bca5609fb48d7a14` |
| Tag-ZIP | `https://github.com/hindermath/spec-kit-preset-secure-development-assurance-governance/archive/refs/tags/v0.1.3.zip` |
| ZIP SHA-256 | `9023b442b4d82e25bee5a7fe9b73efb7f591a4f265f54061ae6e4a56b9b5c75f` |
| Spec Kit | `0.12.8` |
| Security Governance | `0.6.2`, Priorität 10 |
| Assurance-Preset | `0.1.3`, Priorität 15 |
| Preset-Profil | 13 Presets, exakt |
| Host | macOS 26.6.2, Apple Silicon |
| Shells | GNU Bash 3.2.57; PowerShell 7.6.5; jq 1.7.1 |
| Delivery | [TinyPl0 PR #92](https://github.com/hindermath/TinyPl0/pull/92); geprüfter Evidence-Commit `ee09d2008e01024a60be118def05d1aca8b32f0a` |

## Technische Prüfung / Technical Validation

| Test | Ergebnis | Exitcode |
|---|---|---:|
| Release-ZIP erneut laden und SHA-256 prüfen | bestanden | 0 |
| 13-Preset-`CheckOnly`, Bash | bestanden | 0 |
| 13-Preset-`CheckOnly`, PowerShell | bestanden | 0 |
| `preset list`, `preset info` für Security und Assurance | bestanden | 0 |
| Resolve `secure-development-evidence-contract`; `specify check` | bestanden | 0 |
| Positive Statusprüfung, Bash und PowerShell | `Ready`, fachlich gleich | 0 |
| Vier Einzelreviews je Shell | alle `Ready` | 0 |
| Roh-Hash-Snapshot vor/nach Status und Reviews | 7 von 7 Evidence-Dateien unverändert | 0 |
| Vertrags-, Negativ-, LF-/CRLF-/BOM- und Shell-Paritätstest | bestanden | 0 |
| Acht erzeugte Agenten-/Command-Flächen aus dem Tag-ZIP | bestanden; fehlende Evidence blockiert jeweils geregelt | 0 |
| Temporäre Komposition: 13, Disable, Enable, Remove, gültige 12, Reinstall | bestanden | 0 |
| Bewertungsmatrix | 157 eindeutige IDs; 11 erfüllt, 94 teilweise erfüllt, 52 N/A | 0 |

Die Preset-Negativsuite prüft die im Feldtest-Runbook geforderten
Blockierfälle, einschließlich Hash-, Manifest-, Checklisten-, Versions-,
Sammelband-, Review-, Risiko-, Security-Abhängigkeits-, Runbook-,
Image-Impact- und unzulässiger C5-/Zertifizierungsbehauptungen. Die
synthetischen Fehler enden erwartungsgemäß mit Exitcode 2; der Gesamttest endet
mit Exitcode 0. Der C5-Negativfall ist ein Fail-closed-Sicherheitstest und
keine C5-Prüfung des Projekts.

*The preset negative suite covers the runbook's required blockers, including
hash, manifest, checklist, version, compendium, review, risk, security
dependency, runbook, image-impact, and invalid C5/certification claims. Each
synthetic defect returns exit code 2 as required; the complete regression test
returns 0. The C5 negative case tests fail-closed behavior and is not a C5
assessment of the project.*

## Fachliche Grenzen und Wiedervorlage / Decision Boundaries and Review Dates

- Alle vier Assurance-Gates sind `Ready`; `technicalValidation` ist
  `Fulfilled`.
- `pilotAuthorization`, `projectAcceptance` und `generalRelease` bleiben
  ausdrücklich `Open`.
- Alle Gate-Evidence verwendet `reviewDue=2027-09-08` als technische
  Jahreswiedervorlage, nicht als automatischen Freigabetermin.
- C5 ist für den aktuellen nichtkommerziellen Ausbildungs- und Beispielscope
  `N/A`; GitHub, CI, NuGet und Artefakthosting sind Entwicklungs- und
  Lieferinfrastruktur, keine Produkt-Cloud-Runtime.
- CRA und formale Produktkonformität sind für diesen Scope `N/A`.
  Regulatorische Wiedervorlage ist `2026-12-31`, früher bei kommerzieller
  Nutzung, Marktbereitstellung, Kundenübergabe, Supportvertrag oder geänderter
  Hersteller-/Steward-Rolle.
- Es wurde kein Risiko akzeptiert. Restrisiko bleibt eine unbemerkte Änderung
  von Produkt-, Delivery- oder wirtschaftlichem Scope.

## Abweichungen, Findings und Abschluss / Deviations, Findings, and Closeout

Zwischen Bash und PowerShell besteht keine fachliche Ergebnisabweichung.
Zwischen LF-, CRLF- und UTF-8-BOM-Fixtures besteht keine semantische
Abweichung; rohe Hash-Snapshots erkennen Byteänderungen weiterhin. Der bekannte
Spec-Kit-CLI-Hinweis zu möglichen verwaisten Claude-Skills nach `preset remove`
bleibt eine dokumentierte CLI-Grenze; im isolierten Codex-Profil trat kein
verwaister Claude-Pfad auf.

Offene Punkte dieses Projektfeldtests: keine. Die Community-Einreichung
`github/spec-kit#4455`, die Zusammenführung der fünf Projektberichte und eine
spätere zentrale v0.1.3-Preset-Abnahme sind ausdrücklich außerhalb dieses
Projekturteils und werden abgewartet.

*There is no substantive Bash/PowerShell or line-ending variance. Raw snapshots
still detect byte changes. The documented Spec Kit CLI removal limitation
remains, although no orphaned Claude path appeared in this isolated Codex-only
composition. This project field test has no open finding. Community issue
`github/spec-kit#4455`, consolidation of all five project reports, and the later
central v0.1.3 preset decision remain outside this project verdict.*

## Dokumentationsauswirkung / Documentation Impact

`UpdateRequired`. Owner ist Thorsten Hindermann. Kanonische Quelle ist dieser
Feldbericht; die aktualisierten Security-Entscheidungen und Evidence sind dessen
Nachweise. Zielgruppen sind Maintainer, technische Reviewer und Lernende.
Leserpfad: v0.1.3-Adoption → Feldbericht → Security-Dokumente →
maschinenlesbare Evidence. Die Änderung ist `sourceOnly`, zweisprachig,
textorientiert und benötigt keinen Home-Sync. Neu zu bewerten ist bei Preset-,
Baseline-, Produkt-, Delivery- oder Scopeänderung.
