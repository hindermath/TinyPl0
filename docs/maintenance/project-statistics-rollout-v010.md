# Statistik-Rollout v0.1.0 / Statistics rollout v0.1.0

## Auftrag und Status / Authority and status

TinyPl0 ist der erste von sechs Kandidaten im
[zentralen Rollout #302](https://github.com/hindermath/home-baseline/issues/302).
Der Auftrag vom 2026-09-19 umfasst Installation, getrennte Erstmessung,
native Pruefungen und PR-Vorbereitung. Thorsten Hindermann (@hindermath)
erteilt die fachliche Abnahme und Lieferfreigabe gesondert vor MergeAndSync.
Kein Admin-Bypass ist vorab genehmigt. Weitere Kandidaten folgen erst nach
Abnahme und Lieferung dieser Referenz. Keine neue Produktfreigabe.

TinyPl0 is the first of six rollout targets. Installation, separate initial
measurement, native checks and PR preparation are authorized. Human acceptance
and delivery approval by Thorsten Hindermann precede MergeAndSync; admin
bypass is not pre-authorized. Other targets follow this accepted delivery.

## Installation und Erhaltung / Installation and preservation

- Ausgangscommit: `fe7b59c71012e19023b7476dd1a8af3eb6a50c53`.
- Profil: `project-statistics-fourteen-governance-presets`; bestehende 13
  Presets unveraendert, Statistik v0.1.0 aktiviert mit Prioritaet 90.
- [Quellenbindung und dateiweise Paketpruefung](project-statistics-installation-v010.json).
- [Regulaeres Release v0.1.0](https://github.com/hindermath/spec-kit-preset-project-statistics-governance/releases/tag/v0.1.0).
- Paketcommit `7e824ca8de11212aefdc5b05d7d05637f5343dab`; Tag-Objekt
  `fe5243020993787065c469b263f7198f9c345a23`; ZIP-SHA-256
  `d8ad7d5eef920f50b629121b64ba8123c22ec4f6dadd14da5cd826d1c50f420a`.
- Alle 26 installierten Paketdateien einschliesslich versteckter Dateien bytegleich zum geprueften Tag-ZIP;
  alle 13 bisherigen Registry-Eintraege und bereits getrackten
  Preset-/Agenten-/Command-Dateien unmittelbar nach Installation unveraendert.
- Bash- und PowerShell-CheckOnly bestaetigen exakt 14 Presets, Exit 0.
- OpenCode meldet den bestehenden Legacy-Commandpfad; keine Migration.
- Die operative lokale Registry wird erst nach Lieferung umgestellt.

The receipt binds the package and central matrix. All 26 installed files match
the verified archive; thirteen registry entries and tracked integration files
were preserved before the separately documented integration edits. Both shell
checks confirm the exact fourteen-preset matrix. Keep the existing OpenCode
path and defer the operational registry change until delivery. No package
source, global defaults, existing assurance evidence or historical acceptance
is modified. GitHub release immutability is not claimed.

## Messung und Grenzen / Measurement and boundaries

Der neue Kontext liegt unter `docs/project-statistics/`; die bisherige
Profil-2-Statistik bleibt kanonisch. Init verlangt bereits einen sauberen
Arbeitsbaum: der erste Vorschauversuch nach Installation blockierte korrekt
mit Exit 2 und ohne Kontexterzeugung. Deshalb wird die Installation separat
committet, bevor Init und Konfigurationspruefung folgen. Messungen starten erst
nach dem anschliessenden Konfigurations-/Workflow-Commit. Referenzmodelle
bleiben aus; UTC und 52 Wochen sind die neuen Kontextdefaults.

The new context is separate; existing Profile 2 stays authoritative. Init
correctly rejected the uncommitted installation with exit 2 and no context
creation. Commit installation first, initialize and review configuration next,
then commit configuration/workflow before measuring. Disable reference models;
use UTC and 52 weeks. Reproducibility and freshness are different results.

## Konfiguration und Pruefpfad / Configuration and proof path

Installation und Kontexterzeugung sind getrennt. Die Konfiguration uebernimmt
die vier vorhandenen Ausschluesse fuer Release-/IDE-Metadaten, keine alten
manuellen Phasensummen und keine Referenzmodelle. Profil 2 schliesst den neuen
Kontext aus. Alle fuenf Agentenflaechen und die README verlinken denselben
[Bedienpfad](../project-statistics/README.md). Das neue Pruefskript besitzt
zweisprachige Hilfe und eine Manpage; ein lokaler Skriptkatalog existiert hier
nicht. Es wird nur diese projektlokale Pruefung ergaenzt, keine Flottenwartung.

Der installierte Lifecycle-Test bestand lokal; die Statistik-Suite bestand
mit 67 Assertions unter PowerShell 7.6.6/macOS. Der neue CI-Workflow prueft
Linux und Windows am exakten PR-Head, ohne Snapshot-Regenerierung. Bestehende
Produkt-, Dokumentations-/A11Y- und Governance-Workflows bleiben aktiv.
Die reale Messung und deren CI-Ergebnisse werden nach dem Inhaltscommit im
Kontext beziehungsweise PR dokumentiert, nicht als vorweggenommene Abnahme.

Configuration preserves four legacy release/version exclusions, omits authored
phase totals and disables references. Five agent surfaces and README share the
usage path. The new local proof script has bilingual help and a manual; this
repository has no local script catalog. The installed lifecycle and all 67
Unix assertions passed locally. CI checks Linux and Windows on the exact PR
head without regenerating snapshots; existing product, docs/A11Y and governance
workflows stay active. Actual measurement/CI results follow the content commit
in the context/PR; they do not preempt human acceptance.

## Dokumentationsauswirkung / Documentation impact

`UpdateRequired`; Owner: Thorsten Hindermann. Zielgruppen: Lernende ab dem
ersten Ausbildungsjahr, Maintainer und Reviewer. Leserpfad: README -> neuer
Kontext -> dieser Integrationsnachweis -> Quellen/CI. Die vorhandene
Profil-2-Konfiguration und historische Ledger-Eintraege bleiben erhalten.
Die gemeinsame Bedienregel wird in allen fuenf Agentenflaechen gepflegt.
Dokumentklasse ActiveSemantic, DE zuerst/EN danach, text-first Tabellen,
keine Farbabhaengigkeit. Projektlokale Integration; kein Home-Runtime-Sync.
Re-Evaluation bei Paket-, Methoden-, Quellen-, Profil- oder Kontextwechsel.

`UpdateRequired`, owned by Thorsten Hindermann. README leads learners and
maintainers to the separate context and source/CI evidence. Preserve authored
history and synchronize five agent surfaces. Bilingual active semantic docs,
text-first accessibility, project-local distribution, no Home Runtime sync.

NIST SSDF/CWE Top 25 gelten fuer Quellen-, Prozess- und Dateigrenzen. Das
unveraenderte Preset ist die einzige Produktquelle; dessen Release-SBOM bleibt
referenzierbar. ASVS, AI-Runtime/AI-SBOM und C5 fuer diesen lokalen
Ausbildungshelfer N/A; kein neuer VEX-Befund und keine SLSA-Level-Behauptung.
Keine Architektur-, Runtime- oder Produkt-API-Aenderung. Bestehende
Constitutions und Environment Registry bleiben inhaltlich passend.
IDE-Metadaten werden entsprechend der bisherigen Maintenance-Fortschreibung
pro Commit erhoeht; das Verhalten bleibt unveraendert. Die vorhandene
Drei-Plattform-Produkt-CI bleibt Pflichtnachweis. Keine lokale .NET-Ausfuehrung
und kein NuGet-Release in diesem Integrationsschritt.

Apply SSDF/CWE review to provenance, process and file boundaries. Reuse the
unchanged preset's release SBOM. Web ASVS, AI runtime and C5 are not applicable
to this local training-tool integration; no VEX disposition or SLSA level is
claimed. Preserve product behavior and existing constitutions. Advance only
mandatory IDE commit metadata; retain the existing three-platform product CI.
