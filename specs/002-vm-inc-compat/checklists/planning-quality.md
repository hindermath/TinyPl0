# Planning Quality Checklist: VM Compatibility for Historical `Inc` Opcode

**Purpose**: Formales Gate zur Qualitaetspruefung von `plan.md` und den zugehoerigen Planungsartefakten vor `/speckit.tasks` oder PR-Review
**Created**: 2026-03-27
**Feature**: [spec.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/spec.md), [plan.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/plan.md), [research.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/research.md), [data-model.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/data-model.md), [quickstart.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/quickstart.md), [pcode-text-contract.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/contracts/pcode-text-contract.md)

**Note**: Diese Checkliste prueft die Qualitaet, Vollstaendigkeit und Widerspruchsfreiheit der Planungsdokumente selbst. Sie prueft nicht, ob die spaetere Implementierung funktioniert.

## Requirement Completeness

- [x] CHK001 Sind alle vom Plan behaupteten Eingriffsstellen als konkrete Dateien oder Artefaktgruppen benannt, sodass kein Bearbeitungsort nur implizit bleibt? [Completeness, Plan §Project Structure]
  Durchfuehrungshinweis: Vergleiche `Summary`, `Project Structure` und `Phase 1` in [plan.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/plan.md) und markiere jede genannte Aenderung ohne klaren Dateipfad.
- [x] CHK002 Ist der Scope der Alias-Kompatibilitaet in allen Planungsartefakten vollstaendig auf textuelle VM-/P-Code-Artefakte begrenzt und nirgends stillschweigend erweitert? [Completeness, Spec §FR-001, Plan §Summary, Research §Decision 1]
  Durchfuehrungshinweis: Suche in allen Artefakten nach Aussagen zu Parser, VM, CLI und Dokumentation und pruefe, ob irgendwo unbeabsichtigt weitere Ebenen als betroffen dargestellt werden.
- [x] CHK003 Sind die fuer die Implementierung relevanten Ausgabepfade vollstaendig dokumentiert, insbesondere die Beibehaltung von `int` als kanonischer Ausgabeform? [Completeness, Spec §FR-007, Research §Decision 2, Contract §Opcode-Vertrag]
  Durchfuehrungshinweis: Pruefe, ob Eingabe-Alias, interne Abbildung und Ausgabeform in Plan, Data Model und Contract jeweils explizit vorkommen.
- [x] CHK004 Sind die erwarteten Verifikationsartefakte fuer Code, Tests und Doku vollstaendig beschrieben, einschliesslich `docfx` als Pflicht bei geaenderter VM-Dokumentation? [Completeness, Plan §Constitution Check, Quickstart §Empfohlene Verifikation]
  Durchfuehrungshinweis: Gleiche `Constitution Check`, `Quickstart` und den genannten Doku-Ort miteinander ab und notiere fehlende oder nur angedeutete Nachweise.

## Requirement Clarity

- [x] CHK005 Ist eindeutig formuliert, was mit "textuellen VM-/P-Code-Artefakten" gemeint ist, ohne dass Leser zwischen `.pcode`, Listing und anderer Eingabeform raten muessen? [Clarity, Spec §Key Entities, Data Model §4, Contract §Zweck]
  Durchfuehrungshinweis: Lies die Begriffsdefinitionen nebeneinander und pruefe, ob dieselbe Artefaktklasse ueberall mit denselben Merkmalen beschrieben wird.
- [x] CHK006 Ist die Abgrenzung zwischen historischem `Inc`, internem `Opcode.Int`/`Int` und kanonischem `int` praezise genug, um Missverstaendnisse bei Autor und Reviewer auszuschliessen? [Clarity, Spec §FR-005, Data Model §1-2, Research §Decision 4]
  Durchfuehrungshinweis: Pruefe, ob jede Schreibweise genau eine Rolle erhaelt und keine Passage zwei Rollen zugleich andeutet.
- [x] CHK007 Ist die Formulierung zur bestehenden Fehler- und Ausnahmebehandlung klar genug, um zwischen "neue gueltige Eingabe" und "geaenderter Fehlerpolitik" sauber zu unterscheiden? [Clarity, Plan §Constitution Check, Contract §Fehlervertrag]
  Durchfuehrungshinweis: Lies die Passagen zu `FormatException`, Invalid-Faellen und Diagnostics over Exceptions und pruefe auf sprachliche Vermischung von Parser-Validierung und neuer Fachlogik.
- [x] CHK008 Ist der Umfang der "gezielten Regressionstests" konkret genug beschrieben, damit daraus spaeter eindeutig Aufgaben abgeleitet werden koennen? [Clarity, Plan §Phase 1, Research §Decision 3, Data Model §Testrelevante Ableitungen]
  Durchfuehrungshinweis: Pruefe, ob Positiv-, Gleichwertigkeits- und Negativfaelle jeweils explizit benannt sind und nicht nur allgemein als "Tests" auftauchen.

## Requirement Consistency

- [x] CHK009 Stimmen Scope-Aussagen in [spec.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/spec.md), [plan.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/plan.md) und [research.md](/Users/thorstenhindermann/RiderProjects/TinyPl0/specs/002-vm-inc-compat/research.md) ueberein, insbesondere zur Nicht-Betroffenheit von PL/0-Quellparser und VM-Semantik? [Consistency, Spec §FR-001-008, Plan §Summary, Research §Decision 1]
  Durchfuehrungshinweis: Markiere jede Stelle, an der der betroffene Layer anders beschrieben wird als in den anderen Artefakten.
- [x] CHK010 Sind die Aussagen zur kanonischen Ausgabe `int` zwischen Spec, Research, Data Model, Contract und Quickstart widerspruchsfrei? [Consistency, Spec §FR-007, Research §Decision 2, Data Model §Intern nach Ausgabe, Contract §Opcode-Vertrag, Quickstart §3]
  Durchfuehrungshinweis: Suche gezielt nach `Inc`, `Int` und `int` und vergleiche die zugeschriebene Ausgabe- oder Eingaberolle.
- [x] CHK011 Sind die ausgeschlossenen Folgearbeiten zu Katalog, Golden Files und Traceability Matrix zwischen Constitution Check, Research und Quickstart konsistent begruendet? [Consistency, Plan §Constitution Check, Research §Decision 3, Quickstart §Empfohlene Verifikation]
  Durchfuehrungshinweis: Pruefe, ob dieselbe Begruendung ueberall auf "keine PL/0-Codegenerierungs- oder Sprachregel-Aenderung" basiert und nicht an anderer Stelle implizit neue Testartefakte fordert.
- [x] CHK012 Passen die Strukturangaben im Plan zu den tatsaechlich erzeugten Planungsdateien, oder verweist der Plan auf nicht vorhandene bzw. nicht eingeplante Dokumente? [Consistency, Plan §Project Structure, FEATURE_DIR]
  Durchfuehrungshinweis: Vergleiche die Dateiliste in `Project Structure` mit dem aktuellen Verzeichnis `specs/002-vm-inc-compat/`.

## Acceptance Criteria Quality

- [x] CHK013 Sind die Planungsartefakte klar genug mit den Success Criteria verknuepft, damit spaetere Tasks auf messbare Ergebnisse statt auf nur narrative Ziele ausgerichtet werden koennen? [Acceptance Criteria, Spec §SC-001-005, Plan §Phase 1]
  Durchfuehrungshinweis: Pruefe fuer jedes `SC-*`, in welchem Planungsartefakt dessen Nachweisweg beschrieben wird, und markiere Luecken.
- [x] CHK014 Ist eindeutig beschrieben, welche Dokumentstelle die Gleichwertigkeit von `Inc` und `Int` fachlich beweist und welche Stelle nur die technische Umsetzung vorbereitet? [Measurability, Spec §SC-001, Data Model §3, Contract §Beispiel]
  Durchfuehrungshinweis: Unterscheide zwischen "Anforderung", "Modell", "Vertrag" und "Smoke-Check" und pruefe, ob diese Rollen dokumentarisch sauber getrennt sind.
- [x] CHK015 Ist der Nachweis fuer die Doku-Anforderung aus `SC-004` konkret genug beschrieben, um spaeter objektiv reviewbar zu sein? [Measurability, Spec §SC-004, Research §Decision 4, Plan §Constitution Check]
  Durchfuehrungshinweis: Suche nach der expliziten Ziel-Dokumentation und pruefe, ob klar ist, welche Aussage dort nach Implementierung enthalten sein muss.
- [x] CHK016 Ist das Quickstart-Dokument als Verifikationsbeschreibung klar genug, um aus jedem Schritt ein spaeteres Review-Kriterium oder Task-Akzeptanzkriterium ableiten zu koennen? [Acceptance Criteria, Quickstart §1-4]
  Durchfuehrungshinweis: Pruefe jeden Quickstart-Schritt darauf, ob Eingabe, erwartete Aussage und Zweck eindeutig beschrieben sind.

## Scenario Coverage

- [x] CHK017 Decken die Planungsartefakte alle im Spec definierten Szenarioklassen ab: Primaerfall, Rueckwaertskompatibilitaet, Dokumentationsabgleich und Invalid-Faelle? [Coverage, Spec §User Stories, Spec §Edge Cases, Plan §Phase 1]
  Durchfuehrungshinweis: Ordne jede User Story und jeden Edge Case mindestens einem Planungsartefakt zu; fehlende Zuordnungen sind Luecken.
- [x] CHK018 Sind gemischte Schreibweisen und Case-Varianten in den Planungsartefakten explizit genug abgedeckt, statt nur allgemein als "case-insensitive" behauptet zu werden? [Coverage, Spec §FR-008, Data Model §2, Contract §Opcode-Vertrag]
  Durchfuehrungshinweis: Pruefe, ob konkrete Beispielschreibweisen wie `Inc`, `inc` und `INT` dokumentiert sind und ob ihr Status eindeutig ist.
- [x] CHK019 Ist der Negativraum ausreichend beschrieben, also welche aehnlichen, aber ungueltigen Mnemonics weiterhin ausserhalb des Features bleiben? [Coverage, Spec §FR-006, Data Model §Fehlerfaelle, Contract §Fehlervertrag]
  Durchfuehrungshinweis: Suche nach expliziten Gegenbeispielen und pruefe, ob diese in Modell und Vertrag konsistent auftauchen.
- [x] CHK020 Ist das Dokumentationsszenario fuer Lernende und Maintainer ausreichend beschrieben, oder bleibt offen, wie die Begriffsbruecke `Inc` / `Int` / `int` fachlich vermittelt werden soll? [Coverage, Spec §User Story 2, Research §Decision 4]
  Durchfuehrungshinweis: Pruefe, ob nicht nur "Doku aendern" genannt wird, sondern auch welcher Begriffsabgleich dort geleistet werden muss.

## Dependencies & Assumptions

- [x] CHK021 Sind die tragenden Annahmen aus der Spezifikation in den Planungsartefakten entweder bestaetigt oder als pruefpflichtige Voraussetzung kenntlich gemacht? [Assumption, Spec §Assumptions, Plan §Technical Context, Research §Findings]
  Durchfuehrungshinweis: Vergleiche jede Annahme aus `spec.md` mit Plan und Research und markiere stillschweigend uebernommene Annahmen ohne Rueckbindung.
- [x] CHK022 Ist die Abhaengigkeit von der bestehenden case-insensitive Mnemonic-Logik als dokumentierte Voraussetzung und nicht nur als unausgesprochene Vorannahme festgehalten? [Dependency, Spec §FR-008, Research §Decision 1, Data Model §Validierungsregeln]
  Durchfuehrungshinweis: Suche nach Stellen, in denen die bestehende Parsing-Regel als Basis benannt wird, und pruefe, ob daraus kein unbelegter "magischer" Kontext wird.
- [x] CHK023 Ist die `docfx`-Pflicht als Abhaengigkeit der Doku-Aenderung ausreichend in Plan und Quickstart verankert, sodass sie spaeter nicht als optional missverstanden wird? [Dependency, Plan §Constitution Check, Quickstart §Empfohlene Verifikation]
  Durchfuehrungshinweis: Pruefe, ob `docfx` einmal als Gate und einmal als Verifikationsschritt erscheint; falls nur einmal, fehlt Redundanz fuer ein formales Gate.
- [x] CHK024 Ist die Statistik- und Governance-Fortschreibung als bewusste Nebenbedingung dokumentiert, ohne den fachlichen Feature-Scope zu verwischen? [Dependency, Plan §Constitution Check, docs/project-statistics.md, AGENTS.md]
  Durchfuehrungshinweis: Pruefe, ob Prozesspflichten klar als Begleitarbeit erscheinen und nicht als fachliche Anforderung an die `Inc`-/`Int`-Kompatibilitaet.

## Ambiguities & Conflicts

- [x] CHK025 Bleibt irgendwo unklar, ob sich "Int" auf den Enum-Namen, auf textuelle Eingabe oder auf eine kanonische Ausgabeform bezieht? [Ambiguity, Spec §FR-005-007, Data Model §1-2]
  Durchfuehrungshinweis: Suche isoliert nach `Int` und pruefe je Fundstelle, ob der Dokumentkontext die Rolle eindeutig macht.
- [x] CHK026 Gibt es Spannungen zwischen formaler Freigabestrenge und der Aussage "eng begrenzte Parser-Aenderung", die spaetere Aufgaben zu klein oder zu gross schneiden koennten? [Conflict, Plan §Technical Context, Plan §Phase 1, Quickstart §Empfohlene Verifikation]
  Durchfuehrungshinweis: Vergleiche den beschriebenen Minimalumfang mit den geforderten Nachweisen fuer Test, Doku und Prozesspflege.
- [x] CHK027 Ist eindeutig, ob der Vertrag `pcode-text-contract.md` normativ oder nur erlaeuternd sein soll, und passt diese Rolle zu den uebrigen Artefakten? [Ambiguity, Contract §Zweck, Plan §Phase 1]
  Durchfuehrungshinweis: Pruefe, ob der Vertrag als verbindliche Schnittstellenbeschreibung verwendet wird oder nur Beispiele wiederholt; unklare Doppelrolle ist ein Review-Risiko.
- [x] CHK028 Sind offene Folgefragen fuer `/speckit.tasks` bereits ausreichend ausgeraeumt, oder bleiben Dokumentluecken, die zu nicht entscheidbaren Aufgaben fuehren wuerden? [Gap, Plan §Phase 1, Spec §Success Criteria, Quickstart §Empfohlene Verifikation]
  Durchfuehrungshinweis: Lies den Plan aus der Perspektive einer Task-Zerlegung und markiere jede Stelle, an der noch geraten werden muesste, welches konkrete Deliverable gefordert ist.

## Notes

- Einsatzprofil: Autor vor `/speckit.tasks` und PR-Reviewer als formales Gate.
- Fokus: Vollstaendigkeit, Widerspruchsfreiheit, Messbarkeit und Task-Reife der Planungsartefakte.
- Durchfuehrungshinweise sind bewusst knapp gehalten und sollen die Review-Durchfuehrung vereinheitlichen.
- Review-Durchgang am 2026-03-27 abgeschlossen; `plan.md`, `research.md`, `quickstart.md` und `contracts/pcode-text-contract.md` wurden im Zuge des Durchgangs nachgeschaerft.
