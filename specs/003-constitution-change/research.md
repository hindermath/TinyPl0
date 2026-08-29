# Technische Recherche / Technical Research

**Feature**: `003-constitution-change`
**Datum / Date**: 2026-08-29

## Ergebnis / Outcome

Alle technischen Fragen sind entschieden. Es bleibt kein offener
Klärungsmarker und kein offenes Design-Risiko. Die Recherche begrenzt die spätere Umsetzung
auf vorhandene Governance-, Build-, Test-, DocFX- und Statistikflächen.

*All technical questions are resolved. The implementation remains bounded to
existing governance, build, test, DocFX, and statistics surfaces.*

## Entscheidung 1: Security-First bleibt Prinzip I / Security-First Remains Principle I

**Entscheidung / Decision**: Der neue Titel „Didaktische und sprachliche
Klarheit / Pedagogical and Linguistic Clarity“ wird als klar abgegrenzter
TinyPl0-Abschnitt im vorhandenen Level-2-Addendum ergänzt. Prinzip I wird weder
umbenannt noch inhaltlich geändert.

**Begründung / Rationale**: `constitution.md` und
`.specify/memory/constitution.md` sind derzeit bytegleich und führen
Security-First als nicht verhandelbares Prinzip I. Prinzip X erlaubt und
verlangt projektspezifische Level-2-Ergänzungen, solange sie gemeinsame Regeln
nicht schwächen.

**Verworfene Alternative / Rejected alternative**: Den Intake-Titel als neues
Prinzip I einzusetzen würde die akzeptierte Security-First-Basis ersetzen und
FR-001 verletzen.

## Entscheidung 2: Vier statt drei CS1591-Flächen / Four CS1591 Surfaces, Not Three

**Entscheidung / Decision**: Die Umsetzung entfernt
`<NoWarn>$(NoWarn);1591</NoWarn>` aus:

- `src/Pl0.Core/Pl0.Core.csproj`
- `src/Pl0.Vm/Pl0.Vm.csproj`
- `src/Pl0.Cli/Pl0.Cli.csproj`
- `src/Pl0.Ide/Pl0.Ide.csproj`

**Begründung / Rationale**: Die aktuelle Repository-Inventur findet die
Unterdrückung in allen vier Produktprojekten. Die dreifache Zahl in einem
Spezifikationsbeispiel ist damit veraltet, während FR-002 ausdrücklich jede
öffentliche Produkt-API und jede projektweite Unterdrückung bindet. Die
technische Korrektur erweitert den fachlichen Scope nicht.

*The repository currently suppresses CS1591 in all four product projects. The
functional requirement already covers every product API, so treating all four
projects is a correction of the technical inventory, not scope expansion.*

## Entscheidung 3: Öffentliche API-Fläche wird compiler- und semantikgeführt geprüft / Public API Review Uses Compiler and Semantic Evidence

**Entscheidung / Decision**:

1. Der neue Guard prüft `GenerateDocumentationFile=true` und keine
   projektweite `1591`-Unterdrückung.
2. Der Release-Build mit aktiver Warnung bestimmt die extern sichtbare
   Mindestfläche.
3. Ein semantischer Review prüft anwendbare `<param>`, `<returns>` und
   `<exception>`-Elemente, die CS1591 allein nicht beweist.
4. Es werden keine erfundenen Elemente und keine XML-Kommentare für lokale
   Variablen erzeugt.

**Bestandsbefund / Current finding**: Alle lexikalisch extern sichtbaren
Deklarationen in Core, VM und CLI haben bereits einen unmittelbar zugeordneten
XML-Kommentaranker. Die gefundenen undokumentierten `public`-Mitglieder in
`Pl0.Ide` liegen in internen Typen; `Pl0.Ide` besitzt aktuell keine extern
öffentliche Top-Level-Deklaration. Trotzdem muss das Projekt die Unterdrückung
verlieren, damit spätere neue öffentliche APIs nicht unbemerkt bleiben.

**Scope-Grenze / Scope boundary**: Neue oder korrigierte XML-Texte folgen den
aktuellen DE-first/EN-second- und CEFR-B2-Regeln. Eine pauschale Übersetzung
aller bestehenden englischen XML-Kommentare bleibt beim akzeptierten späteren
Dokumentations-Intake.

## Entscheidung 4: TDD wird als ausführbarer Konfigurationsschutz sichtbar / TDD Uses an Executable Configuration Guard

**Entscheidung / Decision**: `ArchitectureGuardTests` erhält einen Test mit
einem fachlich eindeutigen Namen wie
`Product_Projects_Enable_Public_Xml_Documentation_Warnings`.

- **Rot / Red**: Der Test scheitert an den vier vorhandenen Unterdrückungen.
- **Grün / Green**: Nach Entfernung der Unterdrückungen und Schließen aller
  Dokumentationslücken bestehen Guard und Build.
- **Aufräumen/Regression / Refactor and regression**: Gemeinsame XML-Lesehilfe
  des Tests bleibt klein; danach laufen Gesamtsuite und Coverage.

**Begründung / Rationale**: Ein reines TDD-`N/A` wäre zulässig, aber unnötig:
Die Build-Governance hat einen klaren maschinenlesbaren Vorher-/Nachher-Zustand.
Der Test schützt zusätzlich vor späterer erneuter Unterdrückung.

## Entscheidung 5: Preset-Konfiguration ist Quelle der Wahrheit / Executable Preset Matrix Is Authoritative

**Entscheidung / Decision**: Folgende Werte aus
`scripts/config/spec-kit-governance-presets.json` werden in allen betroffenen
beschreibenden Flächen verwendet:

| Preset | Version | Priorität / Priority |
|---|---:|---:|
| `security-governance` | `v0.6.2` | 10 |
| `architecture-governance` | `v0.5.2` | 20 |
| `isaqb-architecture-governance` | `v0.2.2` | 30 |
| `a11y-governance` | `v0.4.3` | 40 |
| `cross-platform-governance` | `v0.2.2` | 50 |
| `agent-parity-governance` | `v0.4.2` | 60 |
| `autonomous-run-governance` | `v0.4.1` | 70 |
| `parallel-autonomous-run-governance` | `v0.2.6` | 80 |

**Bestandsbefund / Current finding**: Constitution, README und die fünf
gepflegten Agentenflächen enthalten ältere Versionen. Die vier Agenten-
Bootstrap-Templates enthalten die aktuellen Werte bereits. Diese Templates
werden nur geändert, wenn die neue didaktische/XML-/TDD-Regel sie fachlich
betrifft; reine Versions-No-op-Edits sind ausgeschlossen.

Die Matrix beschreibt das verbindliche Standardprofil, nicht die Obergrenze
aller lokal registrierten Presets. Separat verwaltete optionale Presets dürfen
koexistieren, solange jeder Standard-Eintrag mit ID, Version, Priorität und
Aktivstatus unverändert nachweisbar bleibt. / The matrix defines the mandatory
standard profile rather than an upper limit for locally registered presets.
Separately governed optional presets may coexist when every standard entry
still matches by ID, version, priority, and enabled state.

## Entscheidung 6: DocFX-Ausgabe und A11Y-Evidenz / DocFX Output and Accessibility Evidence

**Entscheidung / Decision**:

- `docfx docfx.json` regeneriert die getrackten Dateien unter `api/` und die
  ignorierte Prüfwebsite `_site/`.
- Ein lokaler HTTP-Server liefert `_site/` nur über `127.0.0.1` aus.
- Ein temporäres Verzeichnis außerhalb des Repositories enthält exakt gepinnt
  `@playwright/test` 1.62.1 und `@axe-core/playwright` 4.13.0. Es ist nicht Teil
  des Delivery-Sets und wird nach Evidenzaufnahme verworfen.
- Der axe-Lauf prüft Startseite, `Pl0Compiler` und `VirtualMachine`; jede
  gemeldete Verletzung blockiert.
- `lynx -dump` liefert einen unabhängigen textorientierten Nachweis derselben
  Seiten.

**Begründung / Rationale**: Der vorhandene Docs-Workflow baut DocFX und führt
einen HTTP-Smoke-Test aus, enthält aber keinen Playwright/axe- oder
`lynx`-Schritt. Ein temporärer Audit-Harness erfüllt den akzeptierten Nachweis,
ohne Skript-, Workflow- oder dauerhafte Dependency-Arbeit in den Scope zu
ziehen.

**Verifizierte Paketquellen / Verified package sources**:

- [@playwright/test 1.62.1 on npm](https://www.npmjs.com/package/%40playwright/test?activeTab=versions)
- [@axe-core/playwright 4.13.0 on npm](https://www.npmjs.com/package/%40axe-core/playwright?activeTab=versions)

## Entscheidung 7: Coverage-Gate / Coverage Gate

**Entscheidung / Decision**: Die bestehende xUnit-Suite erzeugt Cobertura mit
`--collect:"XPlat Code Coverage"`. Ein PowerShell-7-Schritt liest `line-rate`
aus `TestResults/**/coverage.cobertura.xml`.

- `< 0.70`: blockiert.
- `0.70` bis `< 0.80`: Mindestgate bestanden, Zielabweichung im Ledger.
- `>= 0.80`: Mindestgate und Ziel erreicht.

Da keine Produktlogik geändert wird, entsteht keine künstliche Testmenge nur
zur Erhöhung der Prozentzahl.

## Entscheidung 8: NuGet und Supply Chain / NuGet and Supply Chain

**Entscheidung / Decision**: Es wird kein PackageReference geändert. Der Plan
führt `dotnet list TinyPl0.sln package --outdated --include-transitive` und
`dotnet list TinyPl0.sln package --vulnerable --include-transitive` als Review
aus. Ein kritischer Fund stoppt die Umsetzung und verlangt neue Autorisierung,
statt still den Feature-Scope um Paketupdates zu erweitern.

*No package reference changes. Outdated and vulnerability reports are evidence;
a critical finding blocks and requires separate authority.*

SBOM, VEX, SLSA, OpenSSF Scorecard und AI-SBOM bleiben für diesen Feature-
Arbeitsgegenstand begründet `N/A`; bestehende Release-Pflichten werden nicht
aufgehoben.

## Entscheidung 9: Architektur- und Security-Dokumente / Architecture and Security Documents

**Entscheidung / Decision**: Keine neue Datei unter `docs/architecture/` oder
`docs/security/` und kein ADR/S-ADR. Es ändern sich weder Building Blocks,
Schnittstellen, Laufzeitverhalten, Deployment, Datenfluss noch Trust Boundary.
NIST SSDF und CWE Top 25 bleiben als Implementierungs-/Review-Linse
`Applicable`; ihre Feature-Evidenz liegt im autonomen Ledger und in der
Gate-Evidence.

## Entscheidung 10: IDE-Version / IDE Version

**Entscheidung / Decision**: Die drei Felder `Version`, `AssemblyVersion` und
`FileVersion` bleiben identisch.

- Major: `1`.
- Minor: `3` für Feature `003`.
- Patch: vollständiger `git rev-list --count HEAD` des Commits, der die
  Änderung enthält; vor dem ersten Commit wird der nächste Wert vorausberechnet.
- Build: vor jedem `dotnet build` und jedem `dotnet test` manuell um eins
  erhöhen.

Der aktuelle Wert `1.2.275.14` ist kein Zielwert für dieses Feature.

## Entscheidung 11: Projektstatistik / Project Statistics

**Entscheidung / Decision**: Nach der Agenten-Implementierung genau einen neuen
Eintrag am Ende des `Fortschreibungsprotokoll` ergänzen. Er nennt Branch/Phase,
sichtbares Arbeitsfenster, Produktions-/Test-/Dokumentationszeilen,
Arbeitspakete und die Basen 80/125. Danach den Profil-2-Block rendern und mit
`-CheckOnly` verifizieren; `## Gesamtstatistik / Overall Statistics` bleibt der
letzte Top-Level-Abschnitt.

## Nicht gewählte Arbeit / Work Deliberately Not Selected

- keine PL/0-, Compiler-, VM-, CLI- oder IDE-Funktion;
- keine öffentliche API-Signatur;
- keine Komplettübersetzung bestehender Kommentare/Dokumente;
- keine XML-Dokumentation lokaler Variablen;
- kein Repository-Automationsskript, Cmdlet, Workflow oder keine Manpage;
  browserseitige DocFX-A11Y-Anpassung bleibt JavaScript-/A11Y-Scope ohne
  plattformübergreifendes Befehlsgegenstück;
- keine Dependency-, Release-, Cloud-, Auth-, Netzwerk- oder Datenänderung;
- kein Start des nächsten Intakes.

*No product behavior, broad legacy translation, local-variable XML docs,
repository automation, dependencies, release/cloud/auth/network/data changes,
or next intake.*
