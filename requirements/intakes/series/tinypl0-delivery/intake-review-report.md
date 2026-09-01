# Intake Review: TinyPl0 Delivery Series

> **Status: `Ready`.** Der vollständige Serienreview bindet den aktuellen
> Rang-4-Intake „Einbettbare PL/0-VM und öffentliche NuGet-Pakete“, alle 15
> Zielhashes, fünf Wurzeln und elf Abhängigkeiten. Der durch die
> NuGet-Veröffentlichungsregeln veraltete Review ist ausdrücklich
> supersediert; kein Intake wurde gestartet. / **The complete series review is
> ready.** It binds the current rank-4 intake, all 15 target hashes, five roots,
> and eleven dependencies. It explicitly supersedes the review invalidated by
> the NuGet publishing-policy update and starts no intake.

## Identität / Identity

- Review-ID: `a182589d-3149-4de7-a1d8-c24cefc28cbf`
- Modus: `Series`
- Policy: `tinypl0-delivery-v1`
- Ergebnis: `Ready`
- Repository-Head: `3366400b989532d7f270de532acb03ae6a8ce21f`
- Umfang: 15 Ziele, 5 Wurzeln, 11 bindende Abhängigkeiten, 0 Worker
- Supersedierter Review: `8804ad13-41b4-4feb-a10d-26d2f55333e6`
- Supersession-Evidenz:
  `requirements/intakes/series-archive/tinypl0-delivery/20260901T210304Z-review/superseded-review.json`

*The review uses schema 1.1 and binds its request by repository-relative path
and normalized SHA-256. The prior `NeedsRemediation` meaning remains preserved
through hash-bound supersession evidence.*

## Ziele und Hashes / Targets And Hashes

| Rang | Zustand | Ziel | Normalisierter SHA-256 |
|---:|---|---|---|
| 1 | `Completed` | `requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md` | `fe796de8ced6daf9cb3f4c890b929f47420a12deac2f37da793c4ea263fc2ff5` |
| 2 | `Completed` | `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md` | `18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de` |
| 3 | `Completed` | `requirements/intakes/archive/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md` | `628f869c9df39329949b73457bd56d4345f467ef38d453f257887d07b8f58735` |
| 4 | `Eligible` | `requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md` | `a6e752dcc372c26626cf40cc0b1fb1da1a195a895f51129b87dc0920310b64d5` |
| 5 | `Blocked` | `requirements/intakes/active/Lastenheft_Quellcode_Doku.md` | `5a04868a629453e433da1a733239c948294f9a6462ac11968c806556eefffed2` |
| 6 | `Blocked` | `requirements/intakes/active/Lastenheft_Dokumentation_EN.md` | `0ca2e4d25690699219b9190396ddbb6b4619ef122c962f5accd430d4a5e23068` |
| 7 | `Blocked` | `requirements/intakes/active/Lastenheft_IDE-L10N.md` | `490cc5f62e81a825029567f6d218fd21fea51e03c39a3885bb56154462ca2806` |
| 8 | `Blocked` | `requirements/intakes/active/Lastenheft_A11Y_IDE.md` | `8acb4cbbb5d5c14b13816bcb65b891cbc7d65486581dc95a17c602e9a79d2d48` |
| 9 | `Blocked` | `requirements/intakes/active/Lastenheft_Options_Als_Parameter.md` | `df0e28b4e74683c3eaf1c5bb690d4d5e9a5b20836fe38a7ed06f7adc9d627d19` |
| 10 | `Blocked` | `requirements/intakes/active/Lastenheft_VM_CLI.md` | `dd4b0683ad7733a380078f9fddd6658a1d80713baa2d72a20d76aed8ba8dc1fc` |
| 11 | `Blocked` | `requirements/intakes/active/Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md` | `d273b99f19a8996f7b584940a7b2e3ee291e79dc4e6ce52a7176e0bbb6c3c689` |
| 12 | `Blocked` | `requirements/intakes/active/Lastenheft_PL0_Optimierung.md` | `bb58d3e51ee2f4f17bc6049ab22eea9f3959705ab9d89df7056dad9af7be782a` |
| 13 | `Blocked` | `requirements/intakes/active/Lastenheft_CLR_Assembly.md` | `29ca3739ffa410375adca332a744eec5a650acead08349135483481f476e6e5b` |
| 14 | `Pending` | `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md` | `5cd05da8227cb40f5c9b7b4638486a0377bafc2aed35e072a91c80a0b741637e` |
| 15 | `Pending` | `requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` | `bab44cd08aa1335255a4e29cf5823deb743320d2737ab8eab75b78583c451aec` |

Alle Dateien sind striktes UTF-8 ohne Binärinhalt. Die normalisierten Hashes
stimmen mit dem Manifest überein; für jedes Ziel ist zusätzlich ein Git-Blob
im maschinenlesbaren Ergebnis gebunden.

*Every file is strict UTF-8 text. All normalized hashes match the manifest,
and the machine-readable result also binds an available Git blob for each
target.*

## Review-Abdeckung / Review Coverage

| Bereich | Ergebnis | Evidenz |
|---|---|---|
| Identität, Ziel, Scope und Nicht-Ziele | `Ready` | 15 Intake-Texte und ihre copy-ready Prompts |
| Atomare Anforderungen und messbare Abnahme | `Ready` | Aktuelle Intake-Anforderungen, Abnahmekriterien und Evidence-Pfade |
| Sprache, Lernende und Begriffe | `Ready` | DE zuerst, EN danach, CEFR B2; der Rang-4-Intake erklärt VM-, NuGet-, OIDC- und Supply-Chain-Begriffe bei Erstnutzung |
| Text-First und A11Y | `Ready` | WCAG-2.2-AA-Basis, Tastatur-, Playwright/axe- und Lynx-Nachweise sind im jeweiligen Scope sichtbar |
| Security und Privacy | `Ready` | NIST SSDF und CWE Top 25 gelten; Trust Boundaries, Least Privilege, fail-closed Releasepfad und Secret-Grenzen sind ausdrücklich gebunden |
| Supply Chain | `Ready` | SBOM, VEX, SLSA/Provenance, OpenSSF, OIDC-first und eng begrenzter API-Key-Fallback für die beiden NuGet-Pakete |
| Standards-Anwendbarkeit | `Ready` | ASVS und AI-SBOM sind für den nicht-webbasierten, nicht-KI-ausliefernden Scope begründet `N/A`; regulatorische Anwendbarkeit bleibt als Laufentscheidung sichtbar |
| Plattform | `Ready` | C# 14/.NET 10, portable PowerShell-/Bash-Evidenz und Cross-Platform-Consumer-Tests |
| Reihenfolge und DAG | `Ready` | 15 Ziele genau einmal, 5 Null-Eingangs-Wurzeln, 11 eindeutige vorwärts gerichtete Kanten, azyklisch |
| Handoffs und Zukunftsgrenzen | `Ready` | Sandbox → Pakete; Pakete → Quellcode-Doku; VM-CLI plus Pakete → IDE; TinyCalc bleibt externer Verbraucher |
| Authority | `Ready` | `Eligible` und `LocalImplementation` erteilen keine Remote-, Merge-, Bypass-, Secret-, Provider- oder NuGet-Publikationsrechte |
| Evidenz und Lineage | `Ready` | Schema-2.0-Konfiguration, Manifest, Serien-Receipt, 12 aktuelle Authoring-Receipts und drei hashgleiche archivierte Lifecycle-Ziele |

Die historische Reconciliation-Datei bleibt zeitgebundene Herkunftsevidenz.
Aktuelle Zielpfade, Hashes und Lifecycle-Zustände werden ausschließlich durch
Manifest und Serien-Receipt bestimmt. Dadurch entsteht kein Konflikt zwischen
historischem Audit und aktueller Serie.

*The historical reconciliation file remains dated provenance evidence. The
manifest and series receipt alone define current paths, hashes, and lifecycle
states, so historical evidence does not override current governance.*

## Findings, Risiken und Fragen / Findings, Risks And Questions

- Critical: `0`
- High: `0`
- Medium: `0`
- Low: `0`
- Akzeptierte Risiken / Accepted risks: keine / none
- Offene Fragen / Open questions: keine / none
- Operator-Ausnahmen / Operator exceptions: keine / none

## Entscheidung und nächster Schritt / Decision And Next Action

Der Review ist `Ready`. Das einzige bevorzugte nächste Ziel bleibt
`requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md`. Der
Review selbst startet es nicht und erteilt keine zusätzliche Delivery
Authority.

*The review is ready. The embeddable VM and NuGet intake remains the sole
preferred next target. This review neither starts it nor grants additional
delivery authority.*

Exakter nächster Spec-Kit-Befehl nach ausdrücklicher neuer Ausführungsfreigabe:

```text
$speckit-autonomous Führe genau einen vollständigen autonomen Spec-Kit-Lauf mit requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md als verbindlichem Intake aus. Delivery Mode: LocalImplementation. Implementiere und validiere lokal die sichere Host-API, Run/Step-Parität, Pack-Artefakte und Release-Evidenz, aber stoppe vor jeder Remote- oder NuGet-Veröffentlichung, solange keine aktuelle ausdrückliche Provider- und Secret-Berechtigung vorliegt. Bewahre Scope, Reihenfolge, Security-, Supply-Chain-, A11Y-, Dokumentations- und Evidenzgrenzen. Nicht pushen, keinen Pull Request erstellen oder mergen, keine Pakete veröffentlichen, keinen Bypass nutzen, keine Secrets offenlegen und kein Folgefeature starten.
```
