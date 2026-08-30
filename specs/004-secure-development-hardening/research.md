# Forschungsentscheidungen: Secure-Development-Härtung / Research Decisions: Secure Development Hardening

## R-001 — Evidence-first vor Produktänderung / Evidence First Before Product Change

**Entscheidung / Decision**: Die zwölf kanonischen Checklisten werden zuerst
auf exakt 157 eindeutige IDs gebunden. Danach erhält jede ID getrennte Werte für
Anwendbarkeit und Umsetzung sowie alle Pflichtfelder aus FR-001. Dateiexistenz,
ein grüner Scan oder eine allgemeine Standardsentscheidung ist keine
Erfüllungsaussage.

**Begründung / Rationale**: Die vorhandenen neun Security-Dateien sind fast
vollständig als Stub markiert. Ohne diese Reihenfolge würde der Lauf vorhandene
Pfade mit wirksamen Kontrollen verwechseln und aus 157 Prüfpunkten pauschal 157
Produktänderungen ableiten.

**Verworfene Alternative / Rejected Alternative**: Direkt alle in Spec-Scope
genannten Code-, CI- und UI-Flächen härten. Das verletzt FR-027/FR-028 und macht
Risiko, Test und kleinste Maßnahme nicht nachvollziehbar.

## R-002 — Maschinenlesbare Matrix plus text-first Index / Machine-readable Matrix Plus Text-first Index

**Entscheidung / Decision**: `assessment.json` ist die validierbare Quelle der
157 Projektbewertungen. `README.md`, `findings.md` und `residual-risks.md` sind
zugängliche, zweisprachige Sichten. Das JSON-Schema liegt feature-lokal unter
`contracts/assessment-record.schema.json`.

**Begründung / Rationale**: JSON kann Eindeutigkeit, Statuswerte, Pflichtfelder,
auflösbare Pfade und CL-12-Regeln reproduzierbar prüfen. Der Markdown-Index
bleibt für Screenreader, Braillezeile, Textbrowser und Auszubildende verständlich.

**Verworfene Alternative / Rejected Alternative**: Nur eine 157-zeilige
Markdown-Tabelle. Sie ist lesbar, aber Pflichtfeld- und Typfehler sind unnötig
schwer maschinell zu erkennen.

## R-003 — Budgettyp und Zählpunkt / Budget Type and Counting Point

**Entscheidung / Decision**: `VirtualMachineOptions` erhält
`int InstructionBudget = 1_000_000`. Der Zähler beginnt pro Lauf/Initialisierung
bei null. Eine Instruktion zählt nach erfolgreicher Konfigurations-, Budget- und
Pointerprüfung unmittelbar vor ihrer Ausführung. Bei `executed == N` wird vor
Auswahl/Ausführung von `N+1` diagnostiziert.

**Begründung / Rationale**: `int` passt zum vorhandenen Optionsmodell und zur
festen Produktgrenze. Die Prüfung vor Ausführung macht `N`/`N+1` exakt und
gleich für Batch und Step. Ein externer Zeitgeber wäre lastabhängig und würde
keine reproduzierbare Semantik bieten.

**Verworfene Alternativen / Rejected Alternatives**:

- `TimeSpan` oder Cancellation allein: nicht deterministisch und kein genauer
  Grenzbeweis.
- `long`: für den vereinbarten Standard unnötig und vergrößert nur die API.
- Zählen nach Ausführung: kann Instruktion `N+1` bereits Nebenwirkungen erlauben.

## R-004 — Vorvalidierung vor Allokation / Validation Before Allocation

**Entscheidung / Decision**: Budget und Stackgröße werden durch einen
gemeinsamen internen Validator geprüft, bevor Batch-VM oder Step-VM ein Array
anlegt. Zulässig ist `3 <= StackSize <= 1_000_000`. Die Untergrenze folgt aus
den drei Zellen des historischen Start-Aktivierungsrahmens. Die Obergrenze
verhindert den `StackSize + 1`-Überlauf bei `int.MaxValue` und begrenzt die
einzelne Stackallokation auf rund vier MiB. Nichtpositive Budgets und Werte
außerhalb der Stackgrenzen ergeben stabile `VmDiagnostic`-Werte.

**Begründung / Rationale**: Heute erfolgt `new int[StackSize + 1]` vor jeder
Prüfung; negative Werte können Ausnahmen, `0..2` Indexfehler und
`int.MaxValue` einen Additionsüberlauf verursachen. Eine reine Untergrenze
würde außerdem weiterhin speichererschöpfende Konfigurationen zulassen. Die
fachliche Mindestgröße folgt direkt aus `stack[1..3]`; die Obergrenze ist eine
deterministische Defense-in-Depth-Produktgrenze, keine Betriebssystemgarantie.

**Verworfene Alternative / Rejected Alternative**: Konstruktor- oder
`ArgumentOutOfRangeException`. Das Repository sammelt Laufzeitdiagnosen und darf
internen Exception-Zustand nicht an Nutzerflächen geben.

## R-005 — Gleiche Semantik, keine große VM-Zusammenlegung / Shared Semantics, No Broad VM Merge

**Entscheidung / Decision**: Ein kleiner interner Options-/Diagnosehelfer darf
geteilt werden; die Ausführungsloops bleiben in `VirtualMachine` und
`SteppableVirtualMachine`. Beide Tests binden denselben Diagnosecode und dieselbe
Grenze.

**Begründung / Rationale**: Eine vollständige VM-Engine-Zusammenlegung wäre eine
weit größere Architekturänderung als die autorisierten Härtungen und erhöhte das
historische Regressionsrisiko.

## R-006 — Diagnosevertrag und Lokalisierung / Diagnostic Contract and Localisation

**Entscheidung / Decision**: Neue Optionen und Diagnosen erhalten vollständige
öffentliche XML-Dokumentation sowie deutsche und englische RESX-Einträge. Die
Step-VM darf für die neuen Fälle keine rohen Exception-Nachrichten verwenden.
Bestehende, nicht berührte Step-Diagnosen werden nur nach einem gesonderten
Befund geändert.

**Begründung / Rationale**: Die Batch-VM besitzt bereits lokalisierte Ressourcen,
die Step-VM teilweise harte englische Texte. FR-028 erlaubt keine pauschale
L10N-Modernisierung. Der neue Vertrag muss dennoch in beiden Wegen stabil und
zweisprachig sein.

## R-007 — Repräsentative TDD-Scheibe / Representative TDD Slice

**Entscheidung / Decision**: Der rote Vertikalschnitt verwendet eine
Ein-Instruktions-Endlosschleife und kleines `N=2`. Separat werden Budget `0` und
`-1` sowie Stack `0`, `1`, `2` geprüft. Batch- und Step-Tests bleiben zwischen
Rot und Grün unverändert.

**Begründung / Rationale**: Kleine Werte liefern schnelle, reproduzierbare
Evidence. Ein Test mit Standardmillion wäre langsam und beweist die Off-by-one-
Grenze schlechter. Der rote Lauf erhält ein äußeres Test-Timeout nur als
Harness-Schutz; das Timeout ist nicht die Produktlösung.

## R-008 — Architektur- und Threat-Evidence / Architecture and Threat Evidence

**Entscheidung / Decision**: Das Budget wird als allgemeines ADR und S-ADR
dokumentiert. Threat Model und arc42 Security beschreiben Assets, Flows und
Grenzen für Source, P-Code, VM, CLI, Datei-I/O, IDE und lokalen HTTP-Server.
STRIDE ist die Basis, CIA bewertet Auswirkungen und CAPEC wird für hohe
Missbrauchswege referenziert.

**Begründung / Rationale**: Die Änderung betrifft Runtime-Verhalten,
Ressourcenbegrenzung, Fehlerpfade und eine Trust Boundary. Eine reine
Codekommentierung erfüllt Prinzip XIII/XX nicht.

## R-009 — ASVS-Scope bleibt eng / ASVS Scope Remains Narrow

**Entscheidung / Decision**: ASVS 5.0.0 Level 1 wird vollständig ID-genau auf
`pl0c --api` abgebildet. Authentifizierung, Session, Mehrbenutzerrollen und
fachliche Datenänderung werden begründet `N/A`; Bindung, Methoden, statische
Dateigrenzen, Fehlerantworten, Header und Pfade werden geprüft. GitHub Pages ist
eine Lieferfläche, nicht Teil dieses Produktscopes.

**Begründung / Rationale**: `Program.cs` bindet einen lokalen statischen Server,
aber keine Fach-API. Ein vollständiges L1-Inventar verhindert stilles Auslassen,
ohne einen unangemessenen höheren ASVS-Level zu behaupten.

## R-010 — SBOM-Werkzeug und Provenienz / SBOM Tool and Provenance

**Entscheidung / Decision**: CycloneDX .NET 6.2.0 ist der am 2026-08-30 gegen
die offizielle Releasequelle bestätigte geplante, erst nach
Finding- und Dependency-Gate zu pinnende lokale Generator. Der geplante Befehl
ist `dotnet-CycloneDX TinyPl0.sln -o <evidence-dir> --output-format Json
--spec-version 1.7`. Der tatsächlich veröffentlichte Pages-/Release-Satz erhält
eine Hash-Zuordnung. GitHub-Attestations werden nur nach `FND-SC-001`, mit
minimalen Permissions und voll gepinnter Action ergänzt.

**Begründung / Rationale**: Das offizielle CycloneDX-.NET-Projekt unterstützt
.NET 10, Lösungen, JSON und CycloneDX 1.7. Die offizielle GitHub-Dokumentation
beschreibt SBOM- und Build-Attestations sowie `gh attestation verify`. Tags
allein sind für Supply-Chain-Reproduzierbarkeit nicht ausreichend; der Workflow-
Review verlangt vollständige Commit-SHAs.

**Quellen / Sources**:

- [CycloneDX .NET README](https://github.com/CycloneDX/cyclonedx-dotnet/blob/master/README.md)
- [CycloneDX .NET Releases](https://github.com/CycloneDX/cyclonedx-dotnet/releases)
- [GitHub: Artifact attestations](https://docs.github.com/en/actions/how-tos/secure-your-work/use-artifact-attestations/use-artifact-attestations)
- [GitHub: Export an SBOM](https://docs.github.com/en/code-security/how-tos/secure-your-supply-chain/establish-provenance-and-integrity/export-dependencies-as-sbom)

**Verworfene Alternative / Rejected Alternative**: Eine ungepinnte Marketplace-
Action oder ein global installiertes „latest“-Tool. Das wäre nicht
reproduzierbar und widerspräche der eigenen Supply-Chain-Schranke.

## R-011 — VEX und SLSA ohne Überbehauptung / VEX and SLSA Without Overclaiming

**Entscheidung / Decision**: Ein VEX-Datensatz wird nur für einen bekannten,
bewerteten Fund erzeugt. Ohne Fund dokumentiert die Evidenz den Scanstand,
Trigger und Owner. SLSA wird als Ziel und nachgewiesener Ist-Stand getrennt;
vor einer realen Attestation wird kein Level über den vorhandenen Nachweis
behauptet.

**Begründung / Rationale**: Ein leeres VEX kann eine Prüfung vortäuschen. Ein
grüner Build ist keine Provenienz. Beide Aussagen müssen an Artefakt, Commit und
Workflowkonfiguration gebunden sein.

## R-012 — Baseline-Generator nur nach Befund / Baseline Generator Only After Finding

**Entscheidung / Decision**: Das bekannte Versions-/Generatorproblem wird als
`FND-BASELINE-001` vorbenannt, aber erst nach der 157-ID-Klassifikation
autorisiert. Bei Bestätigung ist PowerShell 7 die Engine; Bash ist ein
funktionsgleicher, streng gequoteter Adapter. Beide teilen Check-, Dry-run- und
Exit-Semantik. Der Sammelband wird nie direkt editiert.

**Begründung / Rationale**: Das Betriebssystem ist macOS und `pwsh` ist
verfügbar; die Repository-Regel bevorzugt PowerShell. Ein gemeinsamer Engine-
Pfad vermeidet zwei unabhängige Generatorimplementierungen und erfüllt dennoch
die Bash-/PowerShell-Bedienflächen aus FR-019.

## R-013 — Coverage-Floor und Ziel getrennt / Separate Coverage Floor and Goal

**Entscheidung / Decision**: Gesamtlinie unter `70%` oder unter `70,23%`
blockiert. `70,23%..79,99%` besteht den Mindestwert mit sichtbarem
`TargetOpen`; `>=80%` ist `TargetMet`. Geänderte VM-Flächen benötigen
`>=85%` Branch; KI-generierter geänderter Code zusätzlich `>=80%` Linie und
Branch.

**Begründung / Rationale**: Damit wird das vorhandene Projekt nicht durch
fachfremde Tests künstlich auf 80% gehoben, während sicherheitskritische neue
Logik strengere lokale Evidenz erhält.

## R-014 — DocFX und A11Y / DocFX and Accessibility

**Entscheidung / Decision**: Das neue öffentliche Optionsfeld löst vollständige
XML-Dokumentation, DocFX und repräsentative axe-/`lynx`-Prüfung aus. Geänderte
Markdown-/Diagnosetexte bleiben DE zuerst, EN danach, CEFR B2 und text-first.
CLI-/IDE-Tastaturtests werden nur bei tatsächlicher UI-/Fehlerpfadänderung
ausgeführt, die wiederum einen Befund braucht.

Die lokale Standardinstallation meldet Node 26, während der bindende
A11Y-Pfad Node 24 LTS verlangt. Nach `FND-A11Y-001` wird deshalb kein globales
Node verwendet, sondern ein eingecheckter, lockfile-gebundener Harness unter
`tests/a11y/`; `.github/workflows/docs-pages.yml` richtet Node 24 explizit ein.
Der gleiche `npm ci`-/Playwright-Aufruf läuft lokal mit einer verwalteten
Node-24-Umgebung und remote auf `ubuntu-latest`. `lynx` bleibt ein getrennter
Textbrowser-Nachweis.

## R-015 — Serialisierte Writer / Serialized Writers

**Entscheidung / Decision**: Assessment/Gate-Evidence, IDE-Version und Statistik
haben getrennte Single-Writer-Tasks. JSON wird in einer temporären Datei
validiert und atomar ersetzt. Vor jedem Build/Test werden die drei IDE-
Versionsfelder gemeinsam aktualisiert. `Minor` ist die kanonische PR-Nummer;
GitHub zeigte read-only am 2026-08-30 PR `#71` als höchste vergebene Nummer,
also ist `72` nur der vorläufige nächste Slot und nicht Feature `004`.
`Patch` entspricht beim ausgeführten Build/Test exakt
`git rev-list --count HEAD`; `Build` steigt vor jedem Aufruf. Der Writercommit
liegt deshalb vor dem jeweiligen Build/Test. Statistik wird erst nach finaler
Inventur einmal ergänzt und danach reproduzierbar gerendert.

**Begründung / Rationale**: Diese Dateien bündeln konkurrierende Ergebnisse.
Parallele Appends würden Hashes, Commitzähler, Buildzähler und chronologische
Reihenfolge unzuverlässig machen. Ein Build auf einem uncommitteten
Versionsstand könnte außerdem keine Evidence für einen exakten HEAD liefern.

## R-016 — Generator-, Workflow- und Governanceänderungen bleiben bedingt / Conditional Non-VM Changes

**Entscheidung / Decision**: Die Planung nennt exakte Kandidatendateien, gibt
sie aber nicht pauschal frei. Genau `FND-BASELINE-001`, `FND-SC-001`,
`FND-CVD-001`, `FND-GITIGNORE-001`, `FND-A11Y-001` und `FND-GOV-001` können
nach vollständiger Einzel-Evidence autorisiert werden. `FND-HTTP-001` und alle
weiteren Befunde bleiben in diesem Lauf `Open` oder `FollowUp` und eröffnen
keinen siebten Implementierungszweig. Plan Review, Tasks und Analyze prüfen
diese Schranke erneut.

**Begründung / Rationale**: So ist der technische Weg vollständig beschrieben,
ohne die Evidence-first-Anforderung in eine Vorabgenehmigung umzudeuten.

## R-017 — ASVS-Quelle und vollständige L1-Menge / ASVS Source and Complete L1 Set

**Entscheidung / Decision**: Die ASVS-Matrix wird gegen die offizielle,
versionsgebundene JSON-Quelle
`OWASP/ASVS@v5.0.0/5.0/docs_en/OWASP_Application_Security_Verification_Standard_5.0.0_en.flat.json`
geprüft. ASVS 5.0.0 besitzt 345 Anforderungen, davon exakt 70 auf Level 1.
Die Evidence speichert Quell-URI, beobachteten Hash, alle 70
`v5.0.0-<ID>`-Schlüssel und deren vollständige `Applicable`-/`N/A`-Zuordnung.

**Begründung / Rationale**: Eine reine Suche nach den Texttokens „Level 1“ und
„Fulfilled“ beweist weder Vollständigkeit noch Versionsbindung. Die offizielle
[OWASP-Änderungsbeschreibung](https://github.com/OWASP/ASVS/blob/v5.0.0/5.0/en/0x05-For-Users-Of-4.0.md)
nennt 70 L1-Anforderungen; die maschinenlesbare Quelle ermöglicht eine
reproduzierbare Mengenprüfung, ohne eine frei erfundene Compliance-Aussage.

## R-018 — Plan-Result bleibt historisch / Plan Result Remains Historical

**Entscheidung / Decision**: Das bereits akzeptierte `plan.result.json` und
sein Run-State-Hash werden nicht umgeschrieben. Die Plan-Review-Phase darf
minimale Planungsremediation vornehmen und bindet den finalen Zustand durch
`plan-review.md`, dessen Artefakthashes und `plan-review.result.json`.
Die Tasks-Phase bindet ihren damals akzeptierten Taskstand. Analyze darf die
letzte minimale Konsistenzremediation an Planungs- und Taskartefakten
vornehmen und bindet diesen finalen pre-implementation Stand durch
`analyze-report.md` und `analyze.result.json`. Keine spätere Phase versucht,
einen historischen Payload-Hash nachträglich als aktuellen Hash auszugeben.

**Begründung / Rationale**: Ein Umschreiben des abgeschlossenen
Phasenergebnisses würde die historische Evidence und den unveränderten
Run-State-Hash auseinanderziehen. Der Review ist die kausal richtige Phase für
Korrekturen und deren neue Hashbindung.
