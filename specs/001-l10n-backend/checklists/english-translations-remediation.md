# English Translations Remediation Checklist: L10N Backend

**Purpose**: Zweiter Review-Durchlauf — (A) prüft ob die Spec-Nachbesserungen für die
„Nein"-Befunde aus `english-translations.md` vollständig und klar formuliert wurden;
(B) bewertet die Anforderungsqualität der geplanten `L10nTests`-Testklasse.
**Created**: 2026-03-03
**Feature**: [spec.md](../spec.md) | [plan.md](../plan.md) | Vorläufer: [english-translations.md](english-translations.md)

---

## Teil A — Remediation der „Nein"-Befunde

### A1: Semantische Äquivalenz (Befund CHK012)

- [x] CHK001 — Wurde FR-003 und/oder FR-004 in der Spec um eine explizite, überprüfbare
  Definition von „semantischer Äquivalenz" (z. B. „gleiche Bedeutung, gleicher Fehlercode-Kontext,
  gleiche Platzhalter-Semantik") ergänzt? [Remediation, Clarity, Spec §FR-003/FR-004] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK002 — Ist die Definition von „semantischer Äquivalenz" so formuliert, dass ein
  Reviewer ohne Deutschkenntnisse die Äquivalenz objektiv beurteilen kann? [Measurability, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

### A2: Großschreibungskonventionen (Befund CHK015)

- [x] CHK003 — Wurde eine Anforderung für Großschreibungskonventionen englischer Fehlertexte
  (z. B. „Sentence case: nur erstes Wort und Eigennamen") in der Spec oder im data-model.md
  ergänzt? [Remediation, Clarity, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK004 — Gilt die Großschreibungskonvention einheitlich für alle drei Module
  (Pl0.Core, Pl0.Vm, Pl0.Cli), und ist dies explizit dokumentiert? [Consistency, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

### A3: Englische Platzhalter-Beispiele (Befund CHK017)

- [x] CHK005 — Wurde data-model.md um eine englische Beispielspalte für alle Keys mit
  Platzhaltern (`{0}`, `{1}`) ergänzt, sodass Platzhalter-Reihenfolge und -Typ prüfbar sind?
  [Remediation, Completeness, Data-Model §3/§4] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK006 — Sind die englischen Beispieltexte in data-model.md konsistent mit den
  definierten Großschreibungs- und Interpunktionskonventionen (CHK003, CHK016)?
  [Consistency, Data-Model] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

### A4: BufferedPl0Io-Tests (Befund CHK029)

- [x] CHK007 — Ist die Anforderung, dass `L10nTests` die englischen VM-Fehlertexte über
  `BufferedPl0Io` (simuliertes I/O) prüfen müssen, explizit in der Spec (FR oder SC) oder
  im plan.md dokumentiert? [Remediation, Completeness, Spec §SC-002, Plan §Phase-1] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK008 — Ist die Wahl von `BufferedPl0Io` gegenüber `ConsolePl0Io` für L10N-Tests
  begründet und als Anforderung — nicht als Implementierungsentscheidung — festgelegt?
  [Clarity, Assumption] - BEFUND: "Nein, nicht klar definiert, bitte prüfen und ergänzen/erstellen"

### A5: Verständlichkeitsniveau B2 (Befund CHK030)

- [x] CHK009 — Wurde eine nicht-funktionale Anforderung für englische Fehlertexte auf
  Sprachniveau B2 (CEFR) als messbares Kriterium in der Spec ergänzt? [Remediation, Non-Functional, Gap] - BEFUND: "Nein, nicht klar definiert, bitte prüfen und ergänzen/erstellen"
- [x] CHK010 — Ist definiert, wie das B2-Niveau im PR-Review überprüft wird (z. B. durch
  den Reviewer, Referenzliste einfacher englischer Begriffe, oder automatisches Tool)?
  [Measurability, Gap]  - BEFUND: "Nein, durch automatisches -> KI-Agent"

### A6: Konsistente englische Terminologie (Befund CHK031)

- [x] CHK011 — Wurde eine Terminologietabelle oder ein Glossar für konsistente englische
  Begriffe (z. B. `identifier`, `variable`, `procedure`, `symbol`, `statement`) als
  Anforderungsartefakt in data-model.md oder als eigene Datei definiert? [Remediation, Clarity, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK012 — Deckt die Terminologietabelle alle Begriffe ab, die in Pl0.Core- und
  Pl0.Vm-Fehlertexten vorkommen, sodass keine Synonyme (z. B. „identifier" vs. „name")
  unkontrolliert eingeführt werden können? [Completeness, Consistency, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

### A7: UTF-8-Kompatibilität (Befund CHK032)

- [x] CHK013 — Wurde FR-005 oder eine neue Anforderung um die explizite Aussage ergänzt,
  dass englische `.resx`-Dateien UTF-8-kodiert sein müssen und keine ASCII-only-Einschränkung gilt?
  [Remediation, Completeness, Spec §FR-005, Lastenheft §3.3] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

### A8: Validierung der Test-Annahme (Befund CHK033)

- [x] CHK014 — Ist die Annahme „bestehende Tests prüfen keine Diagnosetexte" durch eine
  explizite Prüfung des Test-Quellcodes validiert und das Ergebnis (Fact, nicht Assumption)
  in der Spec dokumentiert? [Remediation, Assumption, Spec §Clarifications §Q3] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK015 — Falls die Validierung Diagnosetexte in bestehenden Tests findet: Ist dokumentiert,
  welche Tests angepasst werden müssen und nach welchem Kriterium (explizites `--lang de`)
  [Coverage, Exception Flow, Gap] - BEFUND: "Nein, explizites `--lang de` und `--lang en`"

### A9: Reviewer-Verantwortlichkeit (Befund CHK035)

- [x] CHK016 — Ist dokumentiert, dass der PR-Reviewer die inhaltliche Korrektheit der
  englischen Übersetzungen prüft und verantwortet — und nicht der Entwickler allein?
  [Remediation, Assumption, Gap] - BEFUND: "Nein, nicht klar definiert, bitte prüfen und ergänzen/erstellen"
- [x] CHK017 — Sind die Reviewer-Kriterien für „korrekte" englische Übersetzungen ausreichend
  definiert (z. B. Verweis auf Terminologietabelle, Großschreibungskonvention, B2-Niveau),
  sodass der Review ohne Subjektivität durchgeführt werden kann? [Measurability, Gap] - BEFUND: "Nein, nicht klar definiert, bitte prüfen und ergänzen/erstellen"

---

## Teil B — Anforderungsqualität der `L10nTests`-Klasse

### B1: Konkrete Testdaten und erwartete Texte

- [x] CHK018 — Sind konkrete erwartete englische Texte (z. B. der genaue String für
  `Parser_E11_UndeclaredIdent` auf Englisch) als Akzeptanzkriterien in der Spec, im plan.md
  oder im quickstart.md dokumentiert, sodass `L10nTests` ohne Spekulation implementiert
  werden kann? [Completeness, Measurability, Spec §US-2, §SC-002] - BEFUND: "Nein, bitte ergänzen"
- [x] CHK019 — Ist spezifiziert, welche Testdaten-`.pl0`-Programme für `L10nTests` benötigt
  werden (z. B. Programm mit undeclared identifier, Programm mit Division-by-zero), und
  wo diese im Repository abgelegt werden sollen? [Completeness, Gap, quickstart.md] - BEFUND: "Nein, bitte prüfen ergänzen/erstellen"

### B2: Testumfang und Abdeckungsstrategie

- [x] CHK020 — Ist spezifiziert, ob `L10nTests` alle ~50 englischen Keys einzeln testen
  muss oder ob eine repräsentative Stichprobe (z. B. min. 1 Key je Modul und Fehlerkategorie)
  ausreicht? [Clarity, Spec §SC-002/SC-003, Gap] - BEFUND: "Ja, alle Keys einzeln testen"
- [x] CHK021 — Sind die Anforderungen an die Fallback-Ketten-Tests (en-US → en → de,
  unbekannte Sprache → de) mit konkreten Eingabe/Ausgabe-Paaren als Akzeptanzkriterien
  formuliert? [Completeness, Spec §FR-006, §FR-009, Data-Model §6] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

### B3: Test-Infrastruktur-Anforderungen

- [x] CHK022 — Ist die Anforderung, dass `L10nTests` über `CliOptionsParser` mit dem
  `--lang`-Parameter oder direkt über `CompilerOptions.Language` testen sollen, explizit
  dokumentiert — und nicht der Implementierungsentscheidung überlassen? [Clarity, Plan §Phase-1, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen, dass mit dem --land-Parameter geprüft wird"
- [x] CHK023 — Ist spezifiziert, wie `stderr`-Ausgaben (Fallback-Warnung bei unbekanntem
  Sprachcode) in `L10nTests` fangbar sein müssen — als testbare Anforderung, nicht als
  Implementierungsdetail? [Clarity, Spec §FR-009, Gap] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"
- [x] CHK024 — Ist die Verortung der neuen Tests (`Pl0.Tests/L10nTests.cs`) als
  Anforderung dokumentiert, sodass dies keine offene Implementierungsentscheidung bleibt?
  [Completeness, Plan §Phase-1] - BEFUND: "Ja, Verortung ist klar"

### B4: Erweiterbarkeitstest

- [x] CHK025 — Sind die Anforderungen für den Test `NewLocale_LoadsFromResxWithoutCodeChange`
  (SC-004) ausreichend präzise: Welche Dummy-Sprache? Welche Keys? Welches Minimalverhalten
  muss gezeigt werden? [Clarity, Measurability, Spec §SC-004] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen. Dummy-Sprache `--lang se`, Stichprobe Keys von allen Modulen, Fehlermeldunge in Schwedisch"
- [x] CHK026 — Ist der Erweiterbarkeitstest (SC-004) so formuliert, dass er als PR-Gate
  ohne manuelle Eingriffe (keine neue `.resx`-Datei im CI anlegen) automatisiert werden kann,
  oder ist er explizit als manueller Smoketest deklariert? [Clarity, Spec §SC-004] - BEFUND: "Nein, bitte prüfen und ergänzen/erstellen"

---

## Notes

- Befunde inline dokumentieren: `- [x] CHK001 — BEFUND: "…"`
- Items aus Teil A erfordern Spec-Aktualisierungen vor Merge
- Items aus Teil B erfordern Präzisierungen in plan.md / quickstart.md
- Alle Items MÜSSEN Pass sein als ergänzendes PR-Review-Gate
