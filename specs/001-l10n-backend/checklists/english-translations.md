# English Translations Checklist: L10N Backend

**Purpose**: PR-Review-Gate — validiert die Qualität der englischen Übersetzungsanforderungen
über alle 3 Module (Pl0.Core, Pl0.Vm, Pl0.Cli). Prüft Vollständigkeit, semantische
Äquivalenz zur deutschen Vorlage und Testbarkeit der Anforderungen.
**Created**: 2026-03-03
**Feature**: [spec.md](../spec.md) | [data-model.md](../data-model.md)

---

## Requirement Completeness — Pl0.Core (Lexer + Parser)

- [x] CHK001 — Sind für alle 5 Lexer-Keys (`Lexer_E30_*`, `Lexer_E33_*`, `Lexer_E99_*`) englische Übersetzungsanforderungen im data-model.md hinterlegt? [Completeness, Data-Model §3]
- [x] CHK002 — Sind für alle explizit gelisteten 27 Parser-Keys (`Parser_E01_*` … `Parser_E99_*`) englische Strings gefordert? [Completeness, Data-Model §3]
- [x] CHK003 — Deckt der Eintrag `*(weitere Parser-Expect-Meldungen)*` in data-model.md alle verbleibenden Parser-Fehlertexte ab, oder sind noch unbenannte Keys vorhanden? [Completeness, Gap, Data-Model §3]
- [x] CHK004 — Fordert FR-003 explizit, dass **alle** Compiler-Diagnosetexte (nicht nur die gelisteten Beispiele) auf Englisch verfügbar sein müssen? [Completeness, Spec §FR-003]
- [x] CHK005 — Ist die Anforderung für englische `--errmsg`-Langtexte (lange Fehlerbeschreibungen) getrennt von den Kurztexten spezifiziert, oder gilt FR-003 für beides? [Completeness, Spec §FR-003, US-2 Acceptance Scenario 3]

---

## Requirement Completeness — Pl0.Vm

- [x] CHK006 — Sind alle 13 VM-Fehlermeldungs-Keys (`Vm_E99_*`, `Vm_E98_*`, `Vm_E97_*`, `Vm_E206_*`) im data-model.md mit englischer Übersetzungspflicht dokumentiert? [Completeness, Data-Model §4]
- [x] CHK007 — Fordert FR-004 explizit, dass auch I/O-Laufzeitfehler (`Vm_E98_EndOfInput`, `Vm_E97_InputFormatError`) lokalisiert werden müssen, und nicht nur VM-interne Fehler? [Completeness, Spec §FR-004]
- [x] CHK008 — Ist spezifiziert, ob die englischen VM-Fehlertexte die Platzhalter (`{0}`, `{1}`) in derselben Reihenfolge und mit demselben Typ wie die deutschen Vorlagen enthalten müssen? [Completeness, Clarity, Data-Model §4]

---

## Requirement Completeness — Pl0.Cli

- [x] CHK009 — Sind alle CLI-Error-Keys (`Cli_Err_*`, 6 Einträge) und Status-Keys (`Cli_Status_*`, 2 Einträge) mit englischer Übersetzungspflicht gefordert? [Completeness, Data-Model §5]
- [x] CHK010 — Ist der Umfang von `Cli_Help_Usage` und den `*(weiteren Hilfe-Texten)*` vollständig spezifiziert (welche Unterbefehle, Optionen, Beispiele gehören dazu)? [Completeness, Gap, Data-Model §5]
- [x] CHK011 — Ist die englische Übersetzungspflicht für `Cli_Err_UnknownLanguage` selbst explizit gefordert — also die Warnung, die beim unbekannten Sprachcode auf `stderr` erscheint? [Completeness, Edge Case, Spec §FR-009]

---

## Requirement Clarity — Semantische Äquivalenz

- [x] CHK012 — Ist „semantische Äquivalenz" zwischen deutschen und englischen Texten als Anforderungskriterium definiert? Oder gilt nur strukturelle Entsprechung (gleicher Code, gleiche Platzhalter)? [Clarity, Gap, Spec §FR-003/FR-004] - BEFUND: "Nein, bite prüfen"
- [x] CHK013 — Sind die englischen Texte für PL/0-spezifische Begriffe (`CONST`, `VAR`, `PROCEDURE`, `BEGIN`, `END`, `CALL`, `ODD`) einheitlich definiert — als englische Synonyme oder als unveränderte PL/0-Schlüsselwörter? [Clarity, Ambiguity, Data-Model §3] - BEFUND "Ja, klar definiert"
- [x] CHK014 — Ist klar definiert, ob technische Begriffe wie „LOD", „STO", „INT", „OPR" in den VM-Fehlertexten auf Englisch unverändert bleiben oder übersetzt werden müssen? [Clarity, Ambiguity, Data-Model §4] - BEFUND: "Ja, klar definiert"
- [x] CHK015 — Sind Großschreibungskonventionen für englische Fehlertexte (Satzschreibung vs. Titelschreibung) als Anforderung festgelegt? [Clarity, Gap] - BEFUND: "Nein, nicht klar definiert"
- [x] CHK016 — Ist spezifiziert, ob englische Fehlertexte eine abschließende Interpunktion (Punkt `.`) haben müssen, analog zu den deutschen Vorlagen? [Clarity, Consistency, Data-Model §3/§4] - BEFUND: "Ja, klar definiert"

---

## Requirement Consistency — Abgleich zwischen Modulen und Sprachversionen

- [x] CHK017 — Sind die Platzhalter-Spezifikationen (`{0}`, `{1}`) in data-model.md für alle Keys konsistent zwischen der deutschen und der englischen Anforderungszeile? [Consistency, Data-Model §3/§4/§5] - BEFUND: "Nein, Platzhalter-Spezifikationen nur DE Beispiele, EN-Beispiele fehlen, um es zu überprüfen"
- [x] CHK018 — Stimmen die Key-Namen in data-model.md mit dem im plan.md definierten Key-Schema (`<Modul>_E<Code>_<KurzerName>`) überein — ohne Ausnahmen? [Consistency, Plan §Phase-1, Data-Model] - BEFUND: "Ja, alle Keys stimmen überein"
- [x] CHK019 — Ist die Anforderung, dass numerische Fehlercodes unverändert bleiben (FR-007), konsistent mit allen data-model.md-Einträgen — d.h. kein Key enthält den Code als variablen Platzhalter? [Consistency, Spec §FR-007, Data-Model §3/§4] - BEFUND: "Ja, KEIN Key enthält den Code als variablen Platzhalter"
- [x] CHK020 — Sind die Anforderungen für englische Texte in FR-003 (Pl0.Core) und FR-004 (Pl0.Vm) sprachlich konsistent formuliert, ohne unterschiedliche Qualitätskriterien anzulegen? [Consistency, Spec §FR-003, §FR-004] - BEFUND: "Ja, aber trotzdem prüfen auf konsistente Formulierung"

---

## Acceptance Criteria Quality — Testbarkeit der Anforderungen

- [x] CHK021 — Ist SC-002 (`--lang en` erzeugt englische Diagnosemeldung) messbar ohne Kenntnis des konkreten englischen Textes — oder fehlt eine Referenz auf die erwarteten englischen Strings? [Measurability, Spec §SC-002] - BEFUND: "Ja, Referenz auf `Pl0CoreMessages.en.resx` vorhanden"
- [x] CHK022 — Ist SC-003 (alle Fehlertexte der Traceability-Matrix in beiden Sprachen verfügbar) testbar ohne vollständige englische Textreferenz? Fehlt eine Referenz auf `Pl0CoreMessages.en.resx` als Prüfgröße? [Measurability, Spec §SC-003, Gap] - BEFUND: "Ja, Referenz auf `Pl0CoreMessages.en.resx` vorhanden"
- [x] CHK023 — Definieren die Acceptance Scenarios von US-2 (Compiler-Fehler auf Englisch) konkret, welcher englische Text bei `Parser_E11_UndeclaredIdent` erwartet wird, oder prüfen sie nur die Sprache allgemein? [Measurability, Spec §US-2] - BEFUND: "Ja, konkret"
- [x] CHK024 — Sind die Acceptance Criteria für die englischen VM-Fehlertexte (US-3) testbar ohne vollständige Übersetzungstabelle? [Measurability, Spec §US-3] - BEFUND: "Ja, Referenz auf `Pl0CoreMessages.en.resx` MUSS vorhanden"
- [x] CHK025 — Ist SC-004 (Smoketest Dummy-Sprache) als Anforderung ausreichend präzise, um als PR-Review-Gate-Kriterium zu dienen? [Measurability, Spec §SC-004] - BEFUND: "Vorerst ja"

---

## Scenario Coverage — Fehlende Szenarien

- [x] CHK026 — Sind Anforderungen für den Fall definiert, dass ein englischer Key-Wert leer (`""`) ist — soll der deutsche Fallback greifen oder eine Fehlermeldung erscheinen? [Coverage, Edge Case, Gap] - BEFUND: "Ja, deutsche Fallback soll greifen"
- [x] CHK027 — Ist spezifiziert, wie sich das System verhält, wenn `en-US` angefordert wird, aber nur `en.resx` vorhanden ist (Fallback-Kette)? [Coverage, Data-Model §6, Spec §FR-006] - BEFUND: "Prüfen, Fallback auf `en`-Fehlertexte"
- [x] CHK028 — Sind die Anforderungen für `--lang en-US` (Bindestrich-Variante) explizit von `--lang en` unterschieden, oder gilt FR-001 für beide identisch? [Coverage, Spec §FR-001, contracts/cli-contract.md] - BEFUND: "Beide sollen identisch sein"
- [x] CHK029 — Ist spezifiziert, ob `BufferedPl0Io`-Tests (Tests mit simuliertem I/O) die englischen VM-Fehlertexte prüfen müssen, oder nur `ConsolePl0Io`? [Coverage, Gap] - BEFUND: "Nein, bitte testen mit simuliertem I/O"

---

## Non-Functional Requirements — Qualitätsmerkmale der Übersetzungen

- [X] CHK030 — Ist eine Anforderung für die Verständlichkeit englischer Fehlertexte für nicht-muttersprachliche Lernende (B1/B2-Niveau) definiert? [Coverage, Gap — Ausbildungskontext] - BEFUND: "Nein, Niveau B2 festlegen"
- [x] CHK031 — Sind Anforderungen für konsistente Terminologie zwischen Pl0.Core- und Pl0.Vm-Fehlertexten auf Englisch festgelegt (z. B. „identifier" vs. „variable" vs. „symbol")? [Non-Functional, Gap] - BEFUND: "Nein, bitte konsistente Terminologie festlegen"
- [x] CHK032 — Ist die Anforderung, dass englische Texte UTF-8-kompatibel sein müssen (keine ASCII-only-Einschränkung), explizit dokumentiert? [Non-Functional, Spec §FR-005, Lastenheft §3.3] - BEFUND: "Nein, bitte UTF-8-kompatibilität dokumentieren"

---

## Dependencies & Assumptions

- [x] CHK033 — Ist die Annahme „bestehende Tests prüfen keine Diagnosetexte" dokumentiert und validiert (nicht nur angenommen)? [Assumption, Spec §Assumptions, Clarifications §Q3] - BEFUND: "Nein, nicht dokumentiert und validiert"
- [x] CHK034 — Ist die Abhängigkeit der englischen Übersetzungsqualität von der deutschen Vorlage als Anforderung explizit festgelegt — d.h. muss DE stets vor EN als Referenz verfügbar sein? [Dependency, Gap] - BEFUND: "Ja, DE muss stets vor EN da/vorhanden sein"
- [x] CHK035 — Ist dokumentiert, wer für die inhaltliche Korrektheit der englischen Übersetzungen verantwortlich ist (Entwickler, Reviewer, externer Übersetzer)? [Assumption, Gap] - BEFUND: "Nein, keine Angabe. Das sollte der Reviewer prüfen und durchführen"

---

## Notes

- Check-Items markieren: `- [x] CHK### …`
- Befunde inline dokumentieren: `- [x] CHK012 — BEFUND: "semantische Äquivalenz" nicht explizit definiert → FR-003 ergänzen`
- Alle Items MÜSSEN Pass sein, bevor der PR gemergt wird (formales Review-Gate)
- Items ohne befriedigende Anforderungsformulierung erfordern eine Spec-Aktualisierung
