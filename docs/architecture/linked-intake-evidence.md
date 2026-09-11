# Architektur verlinkter Intake-Evidence / Linked Intake Evidence Architecture

## Kontext und Datenfluss / Context and data flow

```text
Kanonisches tinypl0-delivery-Manifest
  + manifestgebundene Intake-Dateien
  + vorhandene specs/*/autonomous-run-state.json
  -> UTF-8-, Schema-, Pfad-, Hash-, Graph- und Proof-Validierung
  -> eine typisierte Fünf-Felder-Projektion
  -> Root-Ansicht + Series-Ansicht mit relativem Linkkontext
  -> Check oder vorbereitete Mehrdateien-Publikation mit Rollback und Generationsmarker
```

Die kanonische fachliche Identität bleibt das Manifest. Beide Ansichten
enthalten dieselben Positionen, Statuswerte, vollständigen Intake-Dateinamen,
direkten eingehenden Kanten und Featurezustände. Nur die relativen Linkziele
unterscheiden sich wegen ihrer verschiedenen Verzeichnisse. Fehlender
Feature-Abschluss wird ausdrücklich als Fallback dargestellt und nicht
erraten. Beide Ansichten tragen denselben SHA-256-Generationsmarker. Dadurch
erkennen Leser und der nächste Check einen durch Prozess- oder Rechnerabbruch
unterbrochenen Mehrdateien-Replace; ein erneuter Write repariert ihn aus den
kanonischen Quellen. Bei im Prozess erkannten Fehlern stellt der Renderer die
vorherigen Dateien wieder her.

*The manifest remains the canonical functional identity. Both views contain
the same positions, status values, complete intake filenames, direct incoming
edges, and feature states. Only relative link targets differ because the files
live in different directories. Missing feature completion is shown explicitly
as a fallback and is never inferred. Both views carry the same SHA-256
generation marker, so readers and the next check detect an interrupted
multi-file replace; rerunning write repairs it from canonical sources. Errors
caught in-process restore the previous files.*

## Qualitätsziele / Quality goals

| Ziel | Szenario | Messbares Ergebnis |
|---|---|---|
| Integrität | Quelle, Hash, Pfad, Kante oder Proof ist ungültig. | Stabiler `LIE001`–`LIE011`-Blocker; kein Output wird verändert. |
| Determinismus | Unveränderte Eingaben werden erneut verarbeitet. | Beide Dateien bleiben bytegleich; `writes=0`. |
| Semantische Parität | Root- und Series-Kontext benötigen verschiedene relative Links. | Normalisierte fünf Felder sind identisch; nur das relative Ziel ist kontextabhängig. |
| Wiederherstellbarkeit | Publication scheitert im Prozess oder wird extern abgebrochen. | Erkannte Fehler stellen Altstände wieder her; ein gemeinsamer Generationsmarker macht einen extern unterbrochenen Replace erkennbar und reparierbar. |
| Produktisolation | Der Governance-Renderer wird erweitert. | Kein Produkt-, Compiler-, VM-, Golden-, API-, Paket- oder Dependency-Diff. |
| Portabilität | Derselbe Node-Lauf wird unter Linux und Windows geprüft. | Exact-head-Proofs binden Kommando, Runner, Exitcode, Hashes und Write-Count. |

## Entscheidung und Kommentarbedarf / Decision and comment needs

Die Erweiterung bleibt im vorhandenen Standardbibliothek-ESM-Renderer und in
seinem bestehenden Alignment-Test. Es entsteht weder ein neuer Produktbaustein
noch eine externe Schnittstelle oder Deploymentänderung. Ein ADR/S-ADR und ein
Produktarchitekturdiagramm sind deshalb `N/A / Not Assessed`; neu zu bewerten
bei neuer Komponente, öffentlicher API, Runtime, Dependency, Deployment- oder
Trust-Boundary-Änderung.

Nicht offensichtliche Grenzen werden durch sprechende Hilfsfunktionen,
diagnostische Codes und Tests erklärt. Zusätzliche Inline-Kommentare würden
hier überwiegend das offensichtliche Was wiederholen und sind daher nicht
erforderlich. Owner ist der TinyPl0 Repository Owner; Reviewer ist die
Feature-032 Architecture-Rolle. Re-Evaluation bei Quellen-, Komponenten-,
Schnittstellen-, Runtime-, Deployment-, Lifecycle- oder Transaktionsänderung.

*The extension stays in the existing standard-library ESM renderer and its
alignment test. It introduces no product building block, external interface,
or deployment change. Naming, diagnostic codes, and tests explain the
non-obvious boundaries without comments that merely restate the code. The
owner is the TinyPl0 Repository Owner; the reviewer is the Feature 032
architecture role. Re-evaluate when sources, components, interfaces, runtime,
deployment, lifecycle, or transaction behavior change.*

## Documentation Impact: `GeneratedUpdate`

| Feld | Entscheidung |
|---|---|
| Kanonische Quelle | `requirements/intakes/series/tinypl0-delivery/manifest.json` und vorhandene terminale Featurezustände |
| Owner / Reviewer | TinyPl0 Repository Owner / Feature-032 Documentation Reviewer |
| Zielgruppen und Leserpfad | Lernende, Maintainer und Reviewer: Repository-Root → Reihenfolge → vollständiger Intake oder Feature-Nachweis |
| Navigation und Dokumentklasse | Zwei verlinkte Markdown-Referenzansichten; Security-, Architektur- und A11Y-Begleitnachweise unter `docs/` |
| Sprachpartner | Deutsch zuerst, unmittelbar gefolgt von Englisch; technische Literale bleiben identisch |
| Plattform-/Beispielnachweis | Lokale macOS-Fixtures; exact-head Linux-/Windows-Node-Proof folgt im Delivery-Checkpoint |
| Distribution / Home Sync | Repositorylokale Source- und Dokumentationsänderung; kein Home Sync |
| Evidence | Paar-Fixtures, `LIE001`–`LIE011`, zweiter Write mit null Diff sowie späterer lokaler und nativer Gate-Record |
| Re-Evaluation | Manifest-, Feld-, Link-, Sprach-, Zielgruppen-, Plattform-, Renderer- oder Distributionsänderung |

*Decision: `GeneratedUpdate`. The canonical manifest and terminal feature
states generate two repository-local views for learners, maintainers, and
reviewers. German precedes English, no Home Sync is needed, and local plus
exact-head platform evidence validates the result. Re-evaluate whenever its
source, fields, links, language, audience, platform, renderer, or distribution
changes.*
