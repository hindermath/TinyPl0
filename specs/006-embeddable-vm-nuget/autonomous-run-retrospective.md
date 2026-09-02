# Retrospektive des autonomen Laufs / Autonomous Run Retrospective

## Laufidentität / Run Identity

| Feld / Field | Wert / Value |
|---|---|
| Feature | `006-embeddable-vm-nuget` |
| Run-ID | `a01cd5bd-fa86-49f1-b074-cb59a9c24862` |
| Produkt-PR und Merge | PR `#79`; `6a886aad0a5d63d53f8352b5bd22972cb265a934` |
| Release-PR und Merge | PR `#33`; `ff68fabd5a44d754dc50cdaac167f97ef2676a87` |
| Recovery-Head und Merge | `3b746643f0be3e026660addb83c900be110c2d34`; PR `#80`; `baeca77a313d5acd4928531e4fba5e332ddef706` |
| Öffentliche Evidenz / Public evidence | Run `33687547664`; `TinyPl0.Core` und `TinyPl0.Vm` `0.4.0`; kein erneuter Push |
| Delivery Mode | `MergeAndSync`; Admin-Bypass nur für den ausgefallenen Claude-Policy-Check |

## Beobachtungen / Observations

| ID | Beobachtung / Observation | Artefaktart / Artifact kind | Projektausschlüsse / Project exclusions | Portable Zielregel / Portable target rule | Vorkommen / Occurrences | Konfidenz / Confidence | Berechtigungs- und Evidenzrisiko / Permission and evidence risk | Reproduzierbarer Test / Reproducible test | Entscheidung / Decision |
|---|---|---|---|---|---:|---|---|---|---|
| AR-006-01 | NuGet.org ergänzt bei der Veröffentlichung eine Repository-Signatur. Deshalb unterscheiden sich vollständiger Quell- und Public-Hash trotz inhaltlich korrektem Paket. / NuGet.org adds a repository signature, so whole-file source and public hashes differ even when package content is correct. | evidence structure | Konkrete Paket-IDs, Versionen, Hashes und NuGet.org-URLs bleiben projektspezifisch. | Bei Registry-signierten ZIP-Paketen die Registry-Signatur kryptografisch prüfen, Quell- und Public-Hash getrennt erfassen und alle nicht von der Registry ergänzten Einträge bytegenau vergleichen. Eine pauschale Hashgleichheit ist kein gültiges Gate. | 1 | High / Hoch | Hoch: falsche Hashgleichheit meldet korrekte Pakete als fehlerhaft; ein zu lockerer Vergleich könnte echte Manipulation übersehen. | Signiere eine Test-`nupkg` nachträglich, verifiziere den geänderten Gesamt-Hash und identische Nicht-Signatur-Einträge; jede andere Eintragsänderung muss scheitern. | `Promote` |
| AR-006-02 | Ein Release-Recovery darf vorhandene Artefakte nachweisen, aber niemals erneut veröffentlichen. Der Dispatch lud das unveränderliche Artefakt per Ursprungs-Run-ID und übersprang Release, Build und Publish. / Release recovery may verify existing artifacts but must never republish them. | workflow/runbook | GitHub-Run-ID, Workflowname, Environment und Paketversion sind ausgeschlossen. | Ein Recovery-Einstieg braucht validierte Version plus immutable Artefakt-Identität, minimale Read-Permissions und strukturelle Job-Gates, die alle Erzeugungs- und Publish-Jobs ausschließen. `--skip-duplicate` ist kein Ersatz für diese Trennung. | 1 | High / Hoch | Hoch: ein falsch gekoppelter Recovery-Pfad kann unveränderliche Versionen erneut pushen oder andere Artefakte prüfen. | Dispatch gegen ein Fixture-Release starten; nur Verify darf laufen. Build oder Publish im Jobgraph muss den Test scheitern lassen. | `Promote` |
| AR-006-03 | `Invoke-WebRequest -OutFile` liefert ohne `-PassThru` kein Response-Objekt. Transiente HEAD-/GET-Fehler müssen innerhalb des begrenzten Retry-Loops behandelt und Downloads atomar über `.partial` übernommen werden. / PowerShell download semantics require an explicit status probe and in-loop transient error handling. | command/script requirement | Konkrete URLs, Retry-Anzahl und Dateinamen bleiben projektspezifisch. | Für beweisrelevante PowerShell-Downloads Status getrennt prüfen, GET in eine temporäre Datei schreiben, Fehler innerhalb einer begrenzten Backoff-Schleife behandeln, Teilreste löschen und erst nach Erfolg atomar umbenennen. | 1 | High / Hoch | Mittel: ohne diese Regel entstehen flüchtige Fehlalarme oder unvollständige Beweisdateien. | Lokalen HTTP-Server die ersten Aufrufe abbrechen und danach eine gültige Datei liefern lassen; nur der vollständige atomare Download darf akzeptiert werden. | `Promote` |
| AR-006-04 | SemVer wird für die NuGet-Flat-Container-URL kleingeschrieben, der lokale Artefaktpfad muss aber die ursprüngliche Schreibweise behalten. / The public flat-container URL is lowercase while the local artifact path must preserve the source version casing. | script requirement | Version `0.4.0` und Paketnamen sind ausgeschlossen. | Externe kanonische Identifikatoren und lokale unveränderliche Artefaktnamen als getrennte Variablen führen; Normalisierung an einer Trust Boundary darf nie rückwirkend die lokale Evidence-Adresse verändern. | 1 | High / Hoch | Mittel: vermischte Normalisierung kann bei Prerelease-Versionen die falsche oder keine Datei prüfen. | Prerelease-Fixture mit Großbuchstaben verwenden; URL muss normalisiert, lokaler Dateipfad unverändert aufgelöst werden. | `Promote` |

Korrektheits-, Sicherheits-, Berechtigungs- und Evidence-Integritätsfehler dürfen
nach einem deterministischen Vorkommen gefördert werden. Reine
Effizienzpräferenzen brauchen mindestens zwei unabhängige Feldbeobachtungen. /
Correctness, security, permission, and evidence-integrity defects may be
promoted after one deterministic occurrence. Efficiency preferences require
at least two independent field observations.

## Ergebnis / Outcome

- **Geförderte Regeln / Promoted rules**: AR-006-01 bis AR-006-04.
- **Weiter beobachten / Observe again**: keine / none.
- **Projektspezifisch verworfen / Rejected project details**: Paket-IDs,
  Version, Hashes, Run-, PR-, Commit-, Workflow- und Environment-Identitäten.
- **Geänderte Flächen / Changed surfaces**: Retrospektive, portable Übergabe,
  terminaler Run-State, Gate-Evidence, Intake-/Serien-Lineage, Statistik und
  Versionsmetadaten; keine weitere Produktlogik und kein Folgefeature.
- **Validierung / Validation**: 50/50 Tasks, PreMerge/PostMerge-Schema 2.0,
  erfolgreicher Verify-only-Run, byte-identischer Intake-Hash, PowerShell- und
  Bash-Serienvalidatoren sowie Renderer-/Alignment-Prüfung.
- **Nächste Feldschranke / Next field gate**: die vier portablen Regeln in
  einem synthetischen Paket-Registry-Projekt prüfen; kein Folge-Intake in
  diesem Lauf.
