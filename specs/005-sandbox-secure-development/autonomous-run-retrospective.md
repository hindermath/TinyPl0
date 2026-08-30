# Retrospektive des autonomen Laufs / Autonomous Run Retrospective

## Laufidentität / Run Identity

| Feld / Field | Wert / Value |
|---|---|
| Feature | `005-sandbox-secure-development` |
| Run-ID | `91e9fb51-2e69-4eab-85b7-cb28ec23749d` |
| Geprüfter Head / Reviewed head | `8d1a69f44d3ae0a36f3d59c3499e129dbcab7ff6` |
| Produkt-Merge-Commit | `25614e87ce74512491e9d7406a7a07a1e331cf20` |
| Remote-Ergebnis / Remote result | PR `#75`; 27 technische Checks erfolgreich, 2 erwartet übersprungen, 1 externer Reviewer-Lauf mit Providerfehler; null offene Review-Threads; ausdrückliche Owner-Genehmigung und eng begrenzter Admin-Bypass |
| Fachliches Ergebnis / Feature result | Sandbox-Schreibarbeit `Not Ready`; späterer Read/Build/Test-Pilot `Conditional/Open`; keine Produkt-, Test-, Sandbox- oder bestehende Security-Evidence geändert |

## Beobachtungen / Observations

| ID | Beobachtung / Observation | Artefaktart / Artifact kind | Projektausschlüsse / Project exclusions | Portable Zielregel / Portable target rule | Vorkommen / Occurrences | Sicherheit / Confidence | Berechtigungs- und Evidenzrisiko / Permission and evidence risk | Reproduzierbarer Test / Reproducible test | Entscheidung / Decision |
|---|---|---|---|---|---:|---|---|---|---|
| AR-005-01 | Der Projekt-Owner genehmigte den unveränderten Head ausdrücklich und hob für diesen Lernendenlauf die unabhängige Review-Anforderung auf. GitHub speicherte weiterhin keine formale Self-`APPROVED`-Review. / The project owner explicitly approved the unchanged head and waived independent review for this learner-focused run, while GitHub still stored no formal self-approval. | permission rule / Berechtigungsregel | Konkrete Provider-, Account-, PR-, Kommentar- und Commit-Identitäten bleiben projektspezifisch. | Eine projektspezifische Owner-Ausnahme muss Head-gebunden, ausdrücklich und wahrheitsgetreu als Ausnahme dokumentiert sein. Sie darf technische, Risiko-, Secret- oder Evidence-Gates nicht ersetzen; ein Bypass darf nur die verbleibende Provider-Policy überbrücken. | 3 | High / Hoch | Hoch: eine unklare Ausnahme könnte als unabhängige Review oder als allgemeine Bypass-Vollmacht missverstanden werden. | Simuliere einen unveränderten und einen nach Genehmigung geänderten Head; nur der unveränderte Head darf nach vollständigen Technikgates die eng benannte Policy-Schranke passieren. | `Promote` |
| AR-005-02 | Der lokale Modellkatalog änderte erneut nur seinen Hash; Rollen, Modelle und Reasoning-Stufen blieben identisch. Dies ist die zweite unabhängige Beobachtung nach AR-004-06. / The local model catalog again changed only its hash while roles, models, and reasoning levels remained identical. | runbook / Runbook | Lokale Katalogpfade, konkrete Modelle und Harness-Versionen sind ausgeschlossen. | Reine Katalogdrift darf nur dann als nicht materiell gelten, wenn jede Rolle, Modellklasse und Reasoning-Stufe unverändert und verfügbar bleibt. Vor einer neuen gerouteten Phase ist ein atomarer Refresh unter lokaler Autorität nötig; ein rein skriptbasierter Closeout darf ohne Klassenwechsel fortfahren. | 2 | High / Hoch | Mittel: eine zu lockere Prüfung könnte einen Provider- oder Modellklassenwechsel übersehen. | Ändere in einer Fixture nur den Kataloghash und prüfe Rollenparität; jede Rollen-, Modell- oder Reasoning-Abweichung muss blockieren. | `Promote` |
| AR-005-03 | Der Schema-2.0-Governance-Validator nahm ohne expliziten Modus ein strikt aktives Verzeichnis an, obwohl eine laufende Serie abgeschlossene Ziele absichtlich im Archiv hält. `SeriesManifest` stellte die Lifecycle-Sicht fail-closed her. | evidence structure / Evidenzstruktur | TinyPl0-Pfade, Zielnamen, IDs und Zielanzahl sind ausgeschlossen. | Eine Serie mit archivierten `Completed`-Zielen muss das Manifest als Inventarquelle deklarieren; aktive Dateien allein dürfen die historische Serienlinie nicht definieren. Hashes, Reihenfolge und genau ein bevorzugtes `Eligible`-Ziel bleiben Pflicht. | 1 | High / Hoch | Mittel: ein zu weiter Manifestmodus könnte verwaiste aktive Intakes übersehen; ein separater Alignment-Check muss deshalb erhalten bleiben. | Baue eine Fixture mit aktivem, abgeschlossen archiviertem und blockiertem Ziel; `DirectoryStrict` muss den Archivfall ablehnen, `SeriesManifest` plus Alignment muss ihn akzeptieren und Hashdrift weiterhin stoppen. | `Promote` |
| AR-005-04 | Die generische Prerequisite-Auflösung leitete aus dem vorgeschriebenen `codex/`-Branchpräfix zeitweise einen falschen Featurepfad ab; der explizite Feature-Selektor war korrekt. | script requirement / Skriptanforderung | Branch-, Feature- und Repository-Namen sind ausgeschlossen. | Branchpräfixe und Featureverzeichnisse dürfen nicht durch bloßes Abschneiden oder Voranstellen gleichgesetzt werden. Ein expliziter, validierter Feature-Selektor hat Vorrang; Mehrdeutigkeit blockiert. | 1 | Medium / Mittel | Niedrig bis mittel: ein falscher Pfad kann den Lauf unnötig stoppen oder fremde Artefakte auswählen. | Prüfe Fixtures mit `codex/<feature>`, direktem Featurebranch und zwei gleichnamigen Kandidaten; nur die eindeutige explizite Auswahl darf passieren. | `ObserveAgain` |

Korrektheits-, Sicherheits-, Berechtigungs- und Evidence-Integritätsfehler dürfen
nach einem deterministischen Vorkommen gefördert werden. Reine
Effizienzpräferenzen brauchen mindestens zwei unabhängige Feldbeobachtungen. /
Correctness, security, permission, and evidence-integrity defects may be
promoted after one deterministic occurrence. Efficiency preferences require
at least two independent field observations.

## Ergebnis / Outcome

- **Geförderte Regeln / Promoted rules**: AR-005-01 bis AR-005-03.
- **Weiter beobachten / Observe again**: AR-005-04.
- **Projektspezifisch verworfen / Rejected project details**: konkrete
  GitHub-, Workflow-, Kommentar-, Pfad-, Modell-, PR- und Commit-Identitäten.
- **Geänderte Flächen / Changed surfaces**: Retrospektive, portable Übergabe,
  terminaler Run-State, Intake-/Serien-Lineage, Governance-Renderer,
  Closeout-Evidence, Statistik und Versionsmetadaten; keine Produktlogik,
  Tests, öffentliche API, Sandbox-Konfiguration oder bestehende
  `docs/security/`-Evidence.
- **Validierung / Validation**: byte-identischer Intake-Hash, 69/69 Tasks,
  PowerShell-/Bash-Serienvalidatoren, Schema-2.0-Governance, Renderer,
  Alignment, PR-Head-/Check-/Thread-Fakten, Produktmerge und Main-Sync.
- **Nächste Feldschranke / Next field gate**: Branchpräfix-Auflösung in einem
  zweiten unabhängigen Repository beobachten; keinen Folge-Intake starten.
