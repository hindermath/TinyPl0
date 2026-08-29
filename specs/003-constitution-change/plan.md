# Implementierungsplan: Constitution-Abgleich / Implementation Plan: Constitution Alignment

**Branch**: `codex/003-constitution-change` | **Datum / Date**: 2026-08-29 | **Spezifikation / Spec**: [spec.md](spec.md)
**Eingabe / Input**: akzeptierte Spezifikation, abgeschlossener Klärungsbericht und beide bestandenen Checklisten in `specs/003-constitution-change/` / accepted specification, completed clarification report, and both passing checklists in `specs/003-constitution-change/`

## Zusammenfassung / Summary

Dieses Feature richtet die TinyPl0-Governance aus, ohne das gemeinsame
Security-First-Prinzip I zu ersetzen. Der neue projektlokale Abschnitt
„Didaktische und sprachliche Klarheit / Pedagogical and Linguistic Clarity“
wird atomar in Constitution, Spiegel, gepflegten Agentenflächen und betroffenen
Templates geführt. Eine neue rote Architektur-Guard-Prüfung belegt zunächst die
vorhandene CS1591-Unterdrückung. Danach werden die Unterdrückungen in allen vier
Produktprojekten entfernt, öffentliche XML-Dokumentation wird semantisch
vollständig gemacht, der Build wird grün, DocFX wird neu erzeugt und die
HTML-Ausgabe mit Playwright/axe sowie `lynx` textorientiert geprüft. Preset-
Matrix, Projektstatistik und IDE-Version folgen ihren bestehenden Verträgen.

*This feature aligns TinyPl0 governance without replacing shared Security-First
Principle I. The project-local pedagogical section is propagated atomically
across the constitution pair, maintained agent guidance, and affected
templates. A new failing architecture guard first proves the existing CS1591
suppression; all four product projects then remove that suppression, public XML
documentation is completed, the build turns green, DocFX is regenerated, and
representative HTML is reviewed with Playwright/axe and `lynx`. Preset parity,
project statistics, and IDE versioning follow their existing contracts.*

## Technischer Kontext / Technical Context

**Sprache/Version / Language/Version**: C# 14 auf .NET 10 (`net10.0`), Markdown, XML-MSBuild-Projektdateien, JSON-Spezifikationsartefakte / C# 14 on .NET 10, Markdown, XML MSBuild project files, JSON specification artefacts
**Primäre Abhängigkeiten / Primary Dependencies**: .NET SDK 10.0.x, xUnit, Coverlet Collector, DocFX, Terminal.Gui 2.0.0; nur temporäre A11Y-Prüfwerkzeuge `@playwright/test` 1.62.1 und `@axe-core/playwright` 4.13.0 außerhalb des Delivery-Sets / temporary A11Y audit tools only outside the delivery set
**Speicherung / Storage**: dateibasierte Governance-, Quell-, XML-/YAML-DocFX- und Evidence-Artefakte; keine Laufzeitdatenbank und kein neues Serialisierungsformat / file-based governance, source, DocFX XML/YAML, and evidence artefacts; no runtime database or new serialization format
**Tests / Testing**: xUnit in `tests/Pl0.Tests`, MSBuild-Dokumentationswarnung CS1591, vollständiger Release-Build, Gesamttestsuite, XPlat Code Coverage, DocFX, Playwright/axe, `lynx`, PowerShell-Validatoren / xUnit, CS1591 compiler gate, full Release build and test suite, coverage, DocFX, accessibility, and PowerShell validators
**Zielplattform / Target Platform**: lokale Referenz macOS mit PowerShell 7; CI-Gegenprüfung auf GitHub `ubuntu-latest` mit .NET 10.0.x; Inhalte und MSBuild-Regeln bleiben plattformneutral / local macOS reference with PowerShell 7 and CI counter-check on GitHub `ubuntu-latest`
**Projekttyp / Project Type**: mehrteiliges Compiler-/VM-/CLI-/Terminal-IDE-Repository mit Governance- und Dokumentationsänderung / multi-project compiler, VM, CLI, and terminal IDE repository with governance/documentation change
**Leistungsziele / Performance Goals**: keine Laufzeitleistungsänderung; bestehende Test- und DocFX-Laufzeiten dürfen nur durch die zusätzlichen Dokumentationsprüfungen proportional steigen / no runtime performance change
**Einschränkungen / Constraints**: keine PL/0-Semantik, keine öffentliche API-Signatur, keine Abhängigkeit, kein Skript, keine Manpage, keine Trust Boundary und kein späterer Intake werden geändert / no PL/0 semantics, public API signature, dependency, script, man page, trust boundary, or later intake changes
**Umfang / Scale/Scope**: zwei Constitution-Dateien, fünf gepflegte Agentenflächen, gezielt betroffene Templates, vier Produkt-`.csproj`, öffentliche API-Dokumentation in Core/VM/CLI, ein Guard-Test, erzeugte `api/`-Metadaten, Statistik und IDE-Version / two constitutions, five agent surfaces, affected templates, four product projects, public API docs, one guard test, generated API metadata, statistics, and IDE version

## Constitution Check

*Schranke vor Phase 0 und erneut nach Phase 1 / Gate before Phase 0 and re-check after Phase 1.*

### Verbindlicher Level-2-Kontext / Binding Level-2 Context

Der Eintrag `RiderProjects/TinyPl0` in `constitution.md` ist bindend:
.NET 10/C# 14, `dotnet restore/build/test`, xUnit und Coverage, DocFX mit
textorientierter A11Y-Prüfung, manuelle Basis `80`, Thorsten-Solo-Basis `125`
Zeilen/Arbeitstag und die gepflegten Agentenflächen. C# ist nach Prinzip XI
eine speichersichere Sprache. Es gibt keine Nicht-MSL-Ausnahme.

*The `RiderProjects/TinyPl0` registry row binds .NET 10/C# 14, restore/build/test,
xUnit and coverage, DocFX accessibility review, the 80/125 statistics baselines,
and maintained agent surfaces. C# is on the Principle XI memory-safe allow-list.*

### Gates und Entscheidungen / Gates and Decisions

| Prüfpunkt / Checkpoint | Entscheidung / Decision | Plan und Evidenz / Plan and evidence |
|---|---|---|
| Branch und PR / Branch and PR | `Pass` | Arbeit bleibt auf `codex/003-constitution-change`; kein Direkt-Commit auf `main`; spätere Lieferung nur über PR und den autorisierten `MergeAndSync`-Ablauf. / Work stays on the feature branch and later delivery uses the authorized PR flow. |
| Toolchain | `Pass` | `net10.0`, .NET SDK 10.0.x und C# 14 bleiben unverändert; CI nutzt `ubuntu-latest`. / Toolchain remains unchanged. |
| Architekturgrenzen / Layer boundaries | `Pass` | `Pl0.Core -> none`, `Pl0.Vm -> Core`, `Cli/Ide -> Core+Vm`, Tests -> Produktprojekte bleiben unverändert; `ArchitectureGuardTests` schützt zusätzlich die Doku-Build-Governance. / Module dependencies remain unchanged. |
| Allgemeine Architektur / General architecture | `N/A` | Keine Struktur, Schnittstelle, Runtime, Deployment-Sicht oder Quality-Attribute-Architektur ändert sich; keine Datei unter `docs/architecture/` und kein ADR. Wiedervorlage bei Struktur-, Schnittstellen-, Runtime- oder Deployment-Änderung. / No architecture work product is triggered. |
| Sichere Architektur / Secure architecture | `N/A` | Keine Trust Boundary, kein Datenfluss, kein Privileg und kein externes System ändert sich; STRIDE/CIA, CAPEC, S-ADR, arc42 Security, Zero Trust und SAMM bleiben unverändert. Wiedervorlage bei entsprechendem Scope. / No trust or flow boundary changes. |
| Secure Development | `Applicable` | NIST SSDF und CWE Top 25 gelten. Review prüft sichere MSBuild-Konfiguration, keine Fehlerunterdrückung, keine geheimen Dateien, keine neue I/O-/Auth-/Crypto-Fläche und keine internen Fehlerdetails. / NIST SSDF and CWE Top 25 apply to configuration and documentation review. |
| Security-Dokumente / Security documents | `N/A` | Vorhandene Dateien unter `docs/security/` bleiben gültig; keine neue Bedrohung oder Abhängigkeit. Feature-lokale Evidenz reicht. Wiedervorlage bei Trust-, Risiko-, Release- oder Dependency-Änderung. / Existing security evidence remains sufficient. |
| ASVS | `N/A` | Kein Web/API/HTTP/Auth-Scope; Wiedervorlage bei einem solchen Scope. / No web/API/HTTP/auth scope. |
| Supply Chain | `N/A` für SBOM/VEX/SLSA/Scorecard / for release evidence | Keine Release-, Provenienz-, CVE- oder Abhängigkeitsänderung. Die vorhandene NuGet-Lage wird dennoch mit `dotnet list ... --outdated` und `--vulnerable` geprüft. Wiedervorlage bei Release, CVE, Dependency oder Pipeline-Änderung. |
| AI-SBOM | `N/A` | KI ist nur Entwicklungswerkzeug, kein Produktbestandteil. Wiedervorlage bei Modell, Datensatz, Inferenzdienst oder KI-Runtime im Produkt. / AI is development tooling only. |
| Cloud/Regulierung | `N/A` | BSI C3A/C5, NIS2, CRA, EU AI Act und DORA werden mangels Cloud-, Provider- oder reguliertem Scope nicht ausgelöst. / No cloud/provider/regulatory trigger. |
| Presets | `Applicable` | Alle acht Presets des installierten Standardprofils gelten. `scripts/config/spec-kit-governance-presets.json` ist die ausführbare Quelle; dokumentierte Versionen werden auf `v0.6.2`, `v0.5.2`, `v0.2.2`, `v0.4.3`, `v0.2.2`, `v0.4.2`, `v0.4.1`, `v0.2.6` ausgerichtet. Separat verwaltete optionale Presets sind zulässig und werden nur auf konfliktfreie Koexistenz geprüft. / All eight standard-profile presets apply; separately governed optional presets may coexist without replacing or conflicting with a standard entry. |
| Security-First | `Pass` | Prinzip I bleibt Titel, Inhalt und Priorität nach unverändert; die didaktische Regel kommt ausschließlich in das TinyPl0-Level-2-Addendum. / Principle I remains untouched; the new rule is project-local. |
| Zweisprachigkeit / Bilingual delivery | `Applicable` | Neue Governance-, Plan-, Template- und Evidence-Texte sind DE zuerst, EN danach, CEFR B2; bestehende Altbestandsübersetzung bleibt beim späteren Intake. / New text is bilingual; wholesale legacy translation remains out of scope. |
| Lernendenbasis / Learner baseline | `Applicable` | Fachbegriffe werden bei erster Nutzung erklärt; kein Spec-Kit-Vorwissen; Zustände, Abhängigkeiten und nächste sichere Aktion sind textuell vollständig. / First-year readers need no hidden prior knowledge. |
| XML + DocFX | `Applicable` | Alle vier Produktprojekte verlieren `NoWarn ... 1591`; externe öffentliche APIs erhalten anwendbare `<summary>`, `<param>`, `<returns>` und `<exception>`; DocFX erzeugt `api/` neu und `_site/` wird geprüft. |
| TDD | `Applicable` | Rot: neuer Guard-Test weist die vier Unterdrückungen nach. Grün: Unterdrückungen entfernen und Dokumentations-Build schließen. Regression: Guard, Build und Gesamttestsuite. Produktlogik bleibt unverändert. / A configuration guard provides red-green-regression evidence. |
| Coverage | `Applicable` | Gesamtsuite mit XPlat Code Coverage; Mindestwert `>=70%`, Ziel `>=80%`. Unter 70 % blockiert; 70–79,99 % besteht mit dokumentierter Zielabweichung. / Minimum and target are distinct. |
| NuGet | `Applicable` als Review / as review | Keine Paketänderung. Outdated- und Vulnerability-Ausgabe wird dokumentiert; bestehende Pins bleiben unverändert, sofern kein kritischer Fund eine neue Autorisierung verlangt. / No package update is planned. |
| Serialisierung/Daten / Serialization/data | `N/A` | Keine Datenentität, Schema-, `.pcode`- oder JSON-Semantik ändert sich. Wiedervorlage bei Daten- oder Serializeränderung. |
| Skriptparität / Script parity | `N/A` | Keine `.sh`-/`.ps1`-/Cmdlet-/Manpage-Änderung. Wiedervorlage bei jeder Skriptänderung. |
| A11Y | `Applicable` | Semantisches Markdown sowie repräsentative DocFX-Seiten werden text-first geprüft; WCAG 2.2 AA, Playwright/axe und `lynx` sind eigenständige Nachweise. |
| Statistik | `Applicable` | `docs/project-statistics.md` erhält genau einen chronologisch letzten Ledger-Eintrag; Profil 2 bleibt letzter Top-Level-Block und wird reproduzierbar geprüft. |
| Agentenparität | `Applicable` | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, beide Copilot-Flächen und betroffene Templates werden als eine atomare Regelmenge geprüft. |

### Dokumentationsauswirkung / Documentation Impact

**Entscheidung / Decision**: `UpdateRequired` (genau eine Entscheidung / exactly one decision)

- **Zielgruppen / Audiences**: Auszubildende ab Lehrjahr 1, Lehrende,
  Entwicklerinnen und Entwickler, Reviewer und KI-Agenten.
- **Leserpfad / Reader path**: Constitution/Agenten-Einstieg → didaktische,
  XML- und TDD-Regel → Build-/DocFX-/A11Y-Nachweis → nächste sichere Aufgabe.
- **Quelle und Owner / Source and owner**: `constitution.md`, Repository-
  Maintainer; Preset-Versionen aus `scripts/config/spec-kit-governance-presets.json`.
- **Betroffene Klassen / Affected classes**: normative Governance,
  Agenten-Guidance, Templates, öffentliche API-Dokumentation, generierte DocFX-
  Metadaten und Statistik.
- **Ableitungen / Derivations**: `.specify/memory/constitution.md` ist Spiegel;
  `api/**/*.yml` wird durch DocFX erzeugt; `_site/` bleibt ignoriertes
  Prüfartefakt.
- **Navigation / Navigation**: neuer Abschnitt im vorhandenen Level-2-Addendum;
  keine neue Website-Hauptnavigation.
- **Sprachpartner / Language partner**: kurze Inhalte inline DE→EN; kein neuer
  `.EN.md`-Sidecar.
- **Plattformnachweis / Platform proof**: macOS/PowerShell 7 lokal und
  `ubuntu-latest`/.NET 10 CI; keine plattformspezifische Produktlogik.
- **Distribution/Home-Sync**: repository-lokal `sourceOnly`; Home-Sync `N/A`,
  Wiedervorlage bei einem neuen Home-Runtime-Vertrag.
- **Evidenz / Evidence**: `contracts/evidence-contract.md`, autonomes Ledger,
  Gate-JSON, Build/Test/Coverage, DocFX/A11Y, Homogenität, Statistik und PR.

### Re-Check nach Phase 1 / Post-Design Re-check

`Pass`. `research.md`, `data-model.md`, `quickstart.md`,
`contracts/evidence-contract.md` und `gate-requirements.json` erweitern weder
Scope noch Architektur. Jede anwendbare Schranke hat einen exakten Befehl und
Runner-/Plattform-Tokens; jedes `N/A` besitzt Begründung und Wiedervorlage.

*Pass. The design artefacts do not expand scope or architecture. Every
applicable gate has an exact command and runner/platform tokens; every N/A has
a rationale and re-evaluation trigger.*

## Umsetzungsdesign / Implementation Design

### Phase 0: Bestandsentscheidungen / Inventory Decisions

1. Akzeptierte Hashes, Run-Zustand und strukturierte Vorgängergebnisse bleiben
   Eingangsschranke.
2. `constitution.md` und `.specify/memory/constitution.md` sind derzeit
   bytegleich. Security-First bleibt unverändert; nur das Level-2-Addendum wird
   semantisch erweitert.
3. Technische Inventur findet CS1591-Unterdrückung in `Pl0.Core`, `Pl0.Vm`,
   `Pl0.Cli` und `Pl0.Ide`. Die Spezifikationsformulierung „drei“ wird nicht als
   Scope-Grenze gelesen, weil FR-002 ausdrücklich alle Produktprojekte bindet.
4. Alle extern sichtbaren Deklarationen in Core/VM/CLI besitzen bereits einen
   XML-Kommentaranker; die Umsetzung prüft zusätzlich anwendbare Parameter,
   Rückgaben und Ausnahmen. `Pl0.Ide` besitzt derzeit keine extern öffentliche
   Top-Level-API, verliert aber ebenfalls die Unterdrückung.
5. Die ausführbare Preset-Matrix ist aktuell; die dokumentierten Matrizen in
   Constitution, README und Agentenflächen sind veraltet und werden an die
   Konfiguration angeglichen. Vorlagen mit bereits aktuellen Versionen werden
   nicht künstlich geändert, erhalten aber die neue didaktische/TDD-Regel,
   wenn sie Agenten- oder Task-Guidance erzeugen.

### Phase 1: Governance und Parität / Governance and Parity

1. Im TinyPl0-Level-2-Addendum den neuen Abschnitt ergänzen, ohne Prinzip I zu
   ändern; Constitution-Version als `MINOR` erhöhen und Amendierungsdatum nach
   dem bestehenden SemVer-Vertrag setzen.
2. Den widersprüchlichen Final-Polish-Satz zur Lastenheft-Umbenennung
   klarstellen: Bei einem manifestgebundenen aktiven Intake bleibt der
   akzeptierte Pfad während Implementierung und `MergeAndSync` unverändert;
   erst die vollständig gemergte Implementierung löst eine separat autorisierte
   Post-Merge-Archivierung aus. Danach den vollständigen Text und die aktuelle
   Acht-Preset-Matrix bytegleich in den Constitution-Spiegel übertragen. / Clarify
   that a manifest-bound active intake keeps its accepted path through
   implementation and `MergeAndSync`; only fully merged implementation triggers
   a separately authorized post-merge archival rename, then synchronize the
   complete constitution mirror byte-for-byte.
3. Die fünf gepflegten Agentenflächen semantisch gleich ergänzen: vollständige
   öffentliche XML-Dokumentation, keine CS1591-Unterdrückung, didaktische
   Warum-Kommentare und beobachtbare TDD-Evidenz oder begründetes `N/A`.
4. README-Matrix und betroffene Template-Quellen synchronisieren. Absichtliche
   projektspezifische Formulierungsunterschiede werden in der Paritätsprüfung
   genannt; es ist keine unbegründete Abweichung geplant.

### Phase 2: Rot-Grün-Regression für die Build-Governance / Red-Green-Regression for Build Governance

1. Vor jedem `dotnet build` oder `dotnet test` die drei IDE-Versionsfelder
   angleichen: Major bleibt `1`, Minor wird `3`, Patch ist der für den nächsten
   Commit erwartete vollständige Branch-Commit-Zähler, Build wird pro Aufruf um
   eins erhöht. Nach dem Commit wird Patch gegen `git rev-list --count HEAD`
   geprüft.
2. **Rot**: `ArchitectureGuardTests` erhält einen fokussierten Test, der für
   alle vier Produkt-`.csproj` `GenerateDocumentationFile=true` und das Fehlen
   von `1591` in `NoWarn` fordert. Derselbe gefilterte Test muss vor der
   Projektdateiänderung erwartbar fehlschlagen; Exitcode und Assertion werden
   im Ledger festgehalten.
3. **Grün**: `$(NoWarn);1591` aus allen vier Produktprojekten entfernen. Den
   öffentlichen API-Bestand semantisch prüfen und nur fehlende anwendbare
   XML-Elemente ergänzen; keine erfundenen Exceptions oder Returns und keine
   Vollübersetzung des Altbestands.
4. **Regression**: gefilterten Guard, Release-Build, Gesamtsuite und Coverage
   ausführen. Es gibt keine Änderung an Compiler-, VM-, CLI- oder IDE-Verhalten.

### Phase 3: Dokumentation, A11Y und Abschluss-Evidenz / Documentation, A11Y, and Closeout Evidence

1. `docfx docfx.json` aus dem Repository-Root ausführen; getrackte `api/`-
   Metadaten aufnehmen, ignoriertes `_site/` nur als Prüfoutput verwenden.
2. Auf einem Node-24-LTS-Runner den temporären, exakt gepinnten
   Playwright/axe-Harness außerhalb des Repositories ausführen. Mindestens
   `_site/index.html`, `_site/api/Pl0.Core.Pl0Compiler.html` und
   `_site/api/Pl0.Vm.VirtualMachine.html` prüfen; jede axe-Verletzung blockiert.
3. Dieselben repräsentativen Seiten mit `lynx -dump` prüfen und verständliche
   Überschriften/API-Texte als Textnachweis speichern.
4. Preset-Check, Homogenitätsprüfung, NuGet-Review, sichere .NET-/CWE-Prüfung,
   Statistik-Renderer und Delivery-Set-Validator ausführen.
5. Remote-, Merge- und Closeout-Schritte bleiben spätere, separat autorisierte
   Phasen des vorhandenen `MergeAndSync`-Laufs.

## Projektstruktur / Project Structure

### Planungsartefakte dieses Features / Planning Artefacts

```text
specs/003-constitution-change/
├── spec.md
├── clarification-report.md
├── checklists/
│   ├── requirements.md
│   └── autonomous-readiness.md
├── plan.md
├── research.md
├── data-model.md
├── quickstart.md
├── gate-requirements.json
├── contracts/
│   └── evidence-contract.md
└── tasks.md                         # Erst durch /speckit.tasks / only later
```

### Geplanter Implementierungsumfang / Planned Implementation Surface

```text
constitution.md
.specify/memory/constitution.md
README.md

AGENTS.md
CLAUDE.md
GEMINI.md
.github/copilot-instructions.md
.github/agents/copilot-instructions.md

.specify/templates/
├── agent-file-template.md
├── plan-template.md
├── tasks-template.md
└── commands/
    ├── plan.md
    └── tasks.md

scripts/templates/
├── AGENTS.md.tmpl
├── CLAUDE.md.tmpl
├── GEMINI.md.tmpl
├── copilot-instructions.tmpl       # Quelle fuer beide Copilot-Flaechen
└── speckit-workflow-section.md

src/
├── Pl0.Core/
│   ├── Pl0.Core.csproj
│   └── **/*.cs                     # Nur anwendbare öffentliche XML-Lücken
├── Pl0.Vm/
│   ├── Pl0.Vm.csproj
│   └── **/*.cs
├── Pl0.Cli/
│   ├── Pl0.Cli.csproj
│   └── **/*.cs
└── Pl0.Ide/
    ├── Pl0.Ide.csproj              # CS1591 + Version/Assembly/FileVersion
    └── **/*.cs                     # Nur falls extern öffentliche Lücke gefunden

tests/Pl0.Tests/ArchitectureGuardTests.cs
api/**/*.yml
api/.manifest
docs/project-statistics.md
```

**Strukturentscheidung / Structure Decision**: Die bestehende Modulstruktur
bleibt unverändert. Governance wird an ihren vorhandenen Quellen gepflegt; der
einzige neue ausführbare Test bleibt im bestehenden Architektur-Guard. Es gibt
kein neues Produktmodul, keinen Service, kein Datenmodell und keinen
Architekturpfad.

*The existing module structure remains unchanged. Governance stays in its
current source locations and the only new executable test belongs to the
existing architecture guard.*

## Traceability und Abnahme / Traceability and Acceptance

| Anforderungen / Requirements | Umsetzungsblock / Implementation block | Primäre Evidenz / Primary evidence |
|---|---|---|
| FR-001 | Constitution/Addendum | bytegleicher Spiegel, Security-First-Prüfung |
| FR-002, FR-005 | Guard, vier `.csproj`, XML, DocFX/A11Y | Test, Build, DocFX, axe, `lynx` |
| FR-003 | Logik-Diff-Prüfung | `N/A`, solange keine nicht-triviale Logik geändert wird; Trigger im Gate-JSON |
| FR-004 | Agenten- und Task-Guidance, Guard-TDD | Rot-/Grün-/Regression-Ledger |
| FR-006, FR-007 | Agenten, Templates, Matrix | Preset-Check und Homogenitätsprüfung |
| FR-008 | Statistik | Renderer + CheckOnly |
| FR-009 | Scope-Review | Delivery-Set und Analyze |
| SC-001–SC-007 | Gesamtabschluss | Evidence Contract und Gate-Evidence |

## Komplexitätsverfolgung / Complexity Tracking

Keine Constitution-Verletzung und keine genehmigungspflichtige zusätzliche
Komplexität. Die temporäre A11Y-Toolchain liegt außerhalb des Delivery-Sets,
ist exakt gepinnt und erzeugt keine neue Produkt- oder Repository-Abhängigkeit.

*No constitution violation or additional architecture is required. The
temporary accessibility toolchain is pinned, outside the delivery set, and does
not become a product or repository dependency.*
