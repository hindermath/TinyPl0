# TinyPl0 Secure-Development-Prüfung 2026-08-30

## Deutsche Fassung

Diese Prüfinstanz bindet die zwölf kanonischen Checklisten an den Feature-Stand
`004-secure-development-hardening`. Der ausführbare Inventarbefehl aus
`quickstart.md` endete mit Exitcode 0.

| Datei | IDs |
|---|---:|
| CL-01 Standards-Anwendbarkeit | 12 |
| CL-02 Sichere Softwarearchitektur | 13 |
| CL-03 Krypto-Mindestvorgaben | 15 |
| CL-04 Bedrohungsmodellierung | 10 |
| CL-05 Lieferkette und Build-Integrität | 13 |
| CL-06 Schwachstellenoffenlegung | 11 |
| CL-07 CRA-Anwendbarkeit | 12 |
| CL-08 Sicherheits-Code-Review | 13 |
| CL-09 KI-Codeerzeugung | 17 |
| CL-10 Sichere Entwicklungsumgebung | 17 |
| CL-11 Datenschutz-Folgenabschätzung | 12 |
| CL-12 Agentische KI-Sandbox | 12 |
| **Gesamt** | **157** |

Alle 157 IDs sind eindeutig. Ihre Reihenfolge entspricht byteunabhängig der
Reihenfolge im erzeugten Sammelband. Das Manifest wurde mit SHA-256
`82449d57f2e072cb93e0066e7e1eee112219c9836cd51acd75027ec8436ec916`
geprüft. Es meldet Baseline `3.1.0`, Richtlinie `3.1.0`, Sammelband `2.1.0`
und CL-09/CL-12 `2.1.0`. Die beobachteten kanonischen CL-09- und CL-12-Dateien
sowie der Sammelband enthalten bereits `2.2.0`. Diese Drift ist als
`FND-BASELINE-001` erfasst und nicht als erfüllte Parität ausgegeben.

Die maschinenlesbare Quelle ist [assessment.json](assessment.json). Die
lesbaren Entscheidungen stehen in [findings.md](findings.md) und
[residual-risks.md](residual-risks.md). Alle CL-12-Zeilen sind `N/A` und
`Not Assessed`; der getrennte Sandbox-Intake wurde weder gelesen noch gestartet.

## English version

This assessment binds the twelve canonical checklists to feature
`004-secure-development-hardening`. The executable inventory command passed
with counts `12/13/15/10/13/11/12/13/17/17/12/12`, 157 total and unique IDs,
and ordered compendium parity. The manifest/version drift is recorded as
`FND-BASELINE-001`; it is not reported as fulfilled parity. The JSON assessment
is canonical, while the Markdown views remain readable in text browsers and
assistive technology. All CL-12 rows are `N/A` and `Not Assessed`, and the
separate sandbox intake remains unstarted.
