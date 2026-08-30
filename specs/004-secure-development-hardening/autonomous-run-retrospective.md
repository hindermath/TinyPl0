# Retrospektive des autonomen Laufs / Autonomous Run Retrospective

## Laufidentität / Run Identity

| Feld / Field | Wert / Value |
|---|---|
| Feature | `004-secure-development-hardening` |
| Run-ID | `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7` |
| Geprüfter Head / Reviewed head | `1526e64e34371e89aac6d4e6a6e41b5286270a36` |
| Merge-Commit | `e37acee1792911c0b0c2c2115edefe4bcd22f613` |
| Liefernachweis / Delivery evidence | PreMerge `b7302d0112e787a8ded3d6389c33353c2fd09a821294274af755537ece21f90e`; PostMerge `f64e2c4be74d13594a711af49e3e3058ce64ddf88b6fa2f145de8abc5c5645af` |
| Remote-Ergebnis / Remote result | PR `#72`; menschliche Owner-Approval auf dem exakten Head; Admin-Bypass nur für die formale Self-Review-Policy; Merge und Main-Sync abgeschlossen |
| Produktvalidierung / Product validation | 275/275 Tests; Gesamt-Coverage 70,88 %; geänderte VM-Zeilen 100 %; geänderte VM-Branches 95,45 %; finaler Security-Diff-Scan 18/18 Flächen und 0 Findings |

## Beobachtungen / Observations

| ID | Beobachtung / Observation | Artefaktart / Artifact kind | Projektausschlüsse / Project exclusions | Portable Zielregel / Portable target rule | Vorkommen / Occurrences | Sicherheit / Confidence | Berechtigungs- und Evidenzrisiko / Permission and evidence risk | Reproduzierbarer Test / Reproducible test | Entscheidung / Decision |
|---|---|---|---|---|---:|---|---|---|---|
| AR-004-01 | Ein harmloser vollständiger Erklärungssatz löste `generic-api-key` aus. Eine vorhandene genaue Ausnahme und ein vollständiger redigierter History-Scan schlossen den Befund; eine zweite Fingerprint-Ausnahme wäre redundant gewesen. / Harmless explanatory prose triggered `generic-api-key`; one exact exception plus a full redacted history scan closed it. | evidence structure / Evidenzstruktur | Konkreter Satz, Pfad, Regel, Commit und Fingerprint bleiben TinyPl0-spezifisch. | Verwende genau eine möglichst enge False-Positive-Ausnahme. Belege sie mit Quellsemantik und vollständigem redigiertem Scan; stapel keine redundanten Ausnahmen. / Use one narrow false-positive suppression, prove it from source semantics and a full redacted scan, and do not stack duplicates. | 1 | High / Hoch | Mittel: zu breite oder doppelte Ausnahmen können echte Treffer verdecken. | Erzeuge in einem temporären Repository harmlose prose-basierte und echte Token-Treffer; nur die konkrete harmlose Fundstelle darf verschwinden. | `Promote` |
| AR-004-02 | Die Statistik wurde durch verpflichtende Versionscommits wiederholt stale, bis die reine IDE-Versionsmetadatei aus der Statistikquelle ausgeschlossen wurde. Dies bestätigt die offene Beobachtung AR-003 aus Run 003 ein zweites Mal. / Required version-only commits repeatedly made statistics stale until pure version metadata was excluded. | runbook / Runbook | TinyPl0-Phasen, Diagramme und der konkrete Projektpfad sind ausgeschlossen. | Ein deterministischer Statistik-Renderer darf nach zwei unabhängigen Beobachtungen reine Statistik- und Versionsmetadaten aus seiner fachlichen Source-Revision ausnehmen; danach muss `CheckOnly` auf dem endgültigen Commit grün bleiben. | 2 | High / Hoch | Niedrig: falsche Ausschlüsse könnten fachliche Änderungen ausblenden. | Nutze ein temporäres Repository mit einem Fachcommit, Statistikcommit und Versionscommit; der fachliche Hash bleibt stabil, eine Produktdatei muss ihn ändern. | `Promote` |
| AR-004-03 | Der textbrowserbasierte Docs-Test startete vor dem lokalen Server und fiel sporadisch aus. Eine begrenzte Loopback-Readiness-Schleife mit Fehlerlog und bestehendem Cleanup schloss den Race fail-closed. | script requirement / Skriptanforderung | Port, konkrete Seite, Workflow- und Jobnamen sind ausgeschlossen. | Lokale Dokumentations-Smoke-Tests müssen vor dem Clientzugriff begrenzt auf Readiness warten, bei Ablauf fehlschlagen, Serverlogs zeigen und den Prozess immer aufräumen. | 1 | High / Hoch | Mittel: ein unbegrenztes Warten versteckt Hänger; fehlendes Cleanup hinterlässt Prozesse. | Starte in einem temporären Test einen verzögerten Loopback-Server; prüfe Erfolg innerhalb der Grenze, Timeoutfehler und Cleanup in beiden Pfaden. | `Promote` |
| AR-004-04 | Eine PowerShell-Zeilenprüfung erkannte Windows-CRLF nicht wie macOS/Linux-LF. Eine optionale `\r`-Grenze stellte die Plattformparität her. | script requirement / Skriptanforderung | Konkreter Baseline-Text und Workflowmatrix sind ausgeschlossen. | Textprüfungen für plattformübergreifende Dateien müssen LF und CRLF explizit gleich behandeln und in mindestens einem Windows- und einem Unix-Pfad getestet werden. | 1 | High / Hoch | Niedrig: falsche Zeilengrenzen erzeugen Provider-Fehler, nicht Produktfehler. | Schreibe identische UTF-8-Fixtures einmal mit LF und einmal mit CRLF; beide müssen dieselbe fachliche Erkennung liefern. | `Promote` |
| AR-004-05 | Ein automatisierter Reviewer war nicht verfügbar und GitHub konnte die Genehmigung des PR-Owners nicht als Self-`APPROVED` speichern. Erst eine explizite menschliche Owner-Entscheidung auf dem unveränderten Head erlaubte, ausschließlich die formale Policy per Admin zu überbrücken. Dies bestätigt Run 003 erneut. | permission rule / Berechtigungsregel | Anbieter, Account, PR-, Ruleset- und Run-IDs sind ausgeschlossen. | Fehlende Reviewer sind nie Zustimmung. Ein Bypass darf erst nach expliziter menschlicher Genehmigung, vollständiger technischer Evidence und erneuter Head-/Policy-Prüfung genau die verbleibende Provider-Policy überbrücken. | 2 | High / Hoch | Hoch: ein weiter Bypass kann Review-, Risiko- oder Technikgates unzulässig ersetzen. | Verwende ein geschütztes temporäres Repository: ohne menschliche Entscheidung muss der Merge stoppen; mit Head-gebundener Entscheidung darf nur die simulierte Policy-Schranke fallen. | `Promote` |
| AR-004-06 | Der lokale Modellkatalog änderte seinen Hash, während alle vier Rollen dieselben verfügbaren Modelle und Reasoning-Stufen behielten. Ein lokaler Refresh stellte `Aligned` wieder her. | runbook / Runbook | Harness-Version, Modelle und lokale Pfade sind ausgeschlossen. | Katalogdrift ohne Rollenänderung darf nach expliziter lokaler Autorität atomar aktualisiert werden; eine zweite unabhängige Beobachtung ist nötig, bevor dies als allgemeine automatische Effizienzregel gilt. | 1 | Medium / Mittel | Mittel: ungeprüfter Refresh könnte Provider oder Modellklasse wechseln. | Ändere in einer Fixture nur den Kataloghash bei identischer eindeutiger Rollenabbildung und verlange `Aligned`; Modell- oder Providerwechsel muss blockieren. | `ObserveAgain` |
| AR-004-07 | Der Delivery-Set-Validator behandelte zwei semantische Markdown-Endleerzeichen wie zufälligen Whitespace und konnte deshalb den byte-identischen Intake-Archivpfad nicht validieren. Die Regel erlaubt jetzt ausschließlich genau zwei Endleerzeichen in `.md`/`.markdown`; ein einzelnes oder mindestens drei bleiben Fehler. | script requirement / Skriptanforderung | Konkreter Intake, Archivpfad und Textinhalt sind ausgeschlossen. | Whitespace-Prüfungen müssen dokumentierte Dateiformat-Semantik erhalten und zugleich eng fail-closed bleiben; Ausnahmen benötigen Positiv- und Negativtests in allen unterstützten Shellpfaden. | 1 | High / Hoch | Mittel: eine zu breite Ausnahme könnte echte Formatfehler verdecken. | Prüfe in einem temporären Repository zwei Endleerzeichen in Markdown als Erfolg sowie ein Endleerzeichen in Text und mindestens drei in Markdown als Fehler; PowerShell und Bash müssen gleich entscheiden. | `Promote` |

Korrektheits-, Sicherheits-, Berechtigungs- und Evidence-Integritätsfehler dürfen
nach einem deterministischen Vorkommen gefördert werden. Reine
Effizienzpräferenzen brauchen mindestens zwei unabhängige Feldbeobachtungen. /
Correctness, security, permission, and evidence-integrity defects may be
promoted after one deterministic occurrence. Efficiency preferences require
at least two independent field observations.

## Ergebnis / Outcome

- **Geförderte Regeln / Promoted rules**: AR-004-01 bis AR-004-05 sowie
  AR-004-07.
- **Weiter beobachten / Observe again**: AR-004-06.
- **Projektspezifisch verworfen / Rejected project details**: konkrete
  GitHub-, Gitleaks-, Workflow-, Port-, Pfad-, Modell- und PR-Identitäten.
- **Geänderte Flächen / Changed surfaces**: Retrospektive, portable Übergabe,
  terminaler Run-State, Intake-/Serien-Lineage, Closeout-Evidence,
  Versionsmetadaten und Statistik; keine Produktlogik oder öffentliche API.
- **Validierung / Validation**: Schema-2.0 PreMerge/PostMerge, PR-/Merge-/Sync-
  Fakten, 110/110 Tasks, vier terminale Closeout-Felder, PowerShell-/Bash-
  Serienvalidatoren und finaler Schema-1.1-Run-State.
- **Nächste Feldschranke / Next field gate**: AR-004-06 in einem zweiten
  unabhängigen Repository beobachten; keine automatische Modellklassenänderung.
