# Spec-Update Tracker: L10N Backend

**Purpose**: Dritter Review-Durchlauf (Teil A) — prüft ob die Spec-Nachbesserungen aus den
„Nein"-Befunden in `english-translations.md` (ET) und `english-translations-remediation.md` (ETR)
**tatsächlich in die Artefakte eingearbeitet wurden**. Jedes Item verweist auf den Ursprungsbefund.
**Created**: 2026-03-03
**Feature**: [spec.md](../spec.md) | [plan.md](../plan.md) | [data-model.md](../data-model.md)
**Vorläufer**: [english-translations-remediation.md](english-translations-remediation.md)

---

## A1: Semantische Äquivalenz (ETR §A1 → ET CHK012)

- [x] CHK001 — Hat `spec.md §FR-003` eine explizite, überprüfbare Definition von „semantischer
  Äquivalenz" erhalten (z. B. gleiche Bedeutung, gleicher Fehlercode-Kontext, gleiche
  Platzhalter-Semantik)? [Remediation-Gate, Spec §FR-003] - BEFUND: "PASS — spec.md §FR-003 definiert semantische Äquivalenz explizit: gleiche Bedeutung, gleicher Fehlercode-Kontext, gleiche Platzhalter-Semantik und gleiche Platzhalter-Reihenfolge."
- [x] CHK002 — Hat `spec.md §FR-004` dieselbe Definition von „semantischer Äquivalenz" erhalten
  wie FR-003, sodass die Anforderung für Core und Vm konsistent formuliert ist?
  [Consistency, Remediation-Gate, Spec §FR-004] - BEFUND: "PASS — spec.md §FR-004 referenziert dieselbe Äquivalenzdefinition wie FR-003 explizit ('gleiche Bedeutung, gleicher Kontext, gleiche Platzhalter-Semantik')."
- [x] CHK003 — Ist die Definition von „semantischer Äquivalenz" so formuliert, dass ein Reviewer
  **ohne Deutschkenntnisse** die Äquivalenz objektiv beurteilen kann — d. h. ohne implizites
  Wissen über den deutschen Original-Text? [Measurability, Clarity] - BEFUND: "PASS — FR-003/FR-004 spezifizieren: 'pruefbar ohne Deutschkenntnisse anhand der Terminologietabelle (data-model.md §7) und des Fehlercodes'."

---

## A2: Großschreibungskonventionen (ETR §A2 → ET CHK015)

- [x] CHK004 — Hat `spec.md` oder `data-model.md` eine **explizite Großschreibungskonvention**
  für englische Fehlertexte erhalten (z. B. „Sentence case: nur erstes Wort und Eigennamen
  großschreiben")? [Remediation-Gate, Clarity, Gap] - BEFUND: "PASS — spec.md NFR-002 definiert Sentence case explizit für alle drei Module."
- [x] CHK005 — Gilt die dokumentierte Großschreibungskonvention **explizit für alle drei Module**
  (Pl0.Core, Pl0.Vm, Pl0.Cli) und ist das im Artefakt festgehalten?
  [Consistency, Completeness] - BEFUND: "PASS — NFR-002: 'Alle englischen Fehlertexte in Pl0.Core, Pl0.Vm und Pl0.Cli MUESSEN Sentence case verwenden.'"
- [x] CHK006 — Ist die Großschreibungskonvention **ohne Widerspruch** zu bestehenden Anforderungen
  (z. B. Interpunktionspflicht mit `.`) formuliert? [Consistency, Spec §FR-003] - BEFUND: "PASS — Textstil-Konventionen-Tabelle in spec.md zeigt Sentence case und Pflichtpunkt ohne Widerspruch."

---

## A3: Englische Platzhalter-Beispiele (ETR §A3 → ET CHK017)

- [x] CHK007 — Hat `data-model.md §3` (Pl0CoreMessages) für **alle Keys mit Platzhaltern** eine
  englische Beispielspalte erhalten, sodass Platzhalter-Reihenfolge und -Typ prüfbar sind?
  [Remediation-Gate, Completeness, Data-Model §3] - BEFUND: "PASS — data-model.md §3 um 'Beispiel (en)'-Spalte für alle Keys ergänzt (2026-03-04); alle Platzhalter-Keys zeigen EN-Beispiele mit korrekter Reihenfolge."
- [x] CHK008 — Hat `data-model.md §4` (Pl0VmMessages) für **alle Keys mit Platzhaltern** eine
  englische Beispielspalte erhalten? [Remediation-Gate, Completeness, Data-Model §4] - BEFUND: "PASS — data-model.md §4 um 'Beispiel (en)'-Spalte ergänzt; alle 5 Platzhalter-Keys zeigen EN-Beispiele."
- [x] CHK009 — Hat `data-model.md §5` (Pl0CliMessages) für **alle Keys mit Platzhaltern** eine
  englische Beispielspalte erhalten? [Completeness, Data-Model §5] - BEFUND: "PASS — data-model.md §5 um 'Beispiel (en)'-Spalte ergänzt; alle Platzhalter-Keys (Cli_Err_*, Cli_Help_*Line) zeigen EN-Beispiele."
- [x] CHK010 — Sind die englischen Beispieltexte in `data-model.md` **konsistent mit den
  definierten Großschreibungs- und Interpunktionskonventionen** aus CHK004 und ET CHK016?
  [Consistency, Data-Model] - BEFUND: "PASS — Alle EN-Beispieltexte verwenden Sentence case und enden mit '.'; PL/0-Keywords (CONST, VAR, PROCEDURE, CALL, THEN, DO, END) sind durchgehend in Großbuchstaben."

---

## A4: BufferedPl0Io-Tests (ETR §A4 → ET CHK029)

- [x] CHK011 — Hat `spec.md §SC-002` oder `plan.md §Phase-1` die explizite Anforderung erhalten,
  dass `L10nTests` englische VM-Fehlertexte über `BufferedPl0Io` (simuliertes I/O) prüfen müssen?
  [Remediation-Gate, Completeness, Spec §SC-002] - BEFUND: "PASS — spec.md §SC-002 spezifiziert BufferedPl0Io explizit für alle PL/0-auslösbaren VM-Keys; plan.md §Test-Infrastruktur ebenso."
- [x] CHK012 — Ist die **Wahl von `BufferedPl0Io` gegenüber `ConsolePl0Io`** begründet und als
  Anforderung — nicht als offene Implementierungsentscheidung — festgelegt?
  [Clarity, Assumption, Plan §Phase-1] - BEFUND: "PASS — Begründung in spec.md Assumptions: 'ConsolePl0Io in Tests nicht fangbar'; als Anforderung in SC-002 und plan.md §Test-Infrastruktur festgelegt."

---

## A5: Verständlichkeitsniveau B2 (ETR §A5 → ET CHK030)

- [x] CHK013 — Hat `spec.md` eine **nicht-funktionale Anforderung** für englische Fehlertexte auf
  Sprachniveau B2 (CEFR) als messbares Kriterium erhalten?
  [Remediation-Gate, Non-Functional, Gap] - BEFUND: "PASS — spec.md NFR-001 definiert B2 CEFR als messbares Kriterium für alle englischen Fehlertexte."
- [x] CHK014 — Ist dokumentiert, dass das B2-Niveau im PR-Review **durch einen KI-Agenten**
  geprüft wird — als Anforderung, nicht als informeller Hinweis?
  [Measurability, Clarity] - BEFUND: "PASS — NFR-001: 'Pruefung: PR-Review durch KI-Agenten'; Reviewer-Verantwortlichkeit §3 bestätigt KI-Agenten-Review als Anforderung."

---

## A6: Konsistente englische Terminologie (ETR §A6 → ET CHK031)

- [x] CHK015 — Wurde eine **Terminologietabelle oder ein Glossar** für konsistente englische
  Begriffe (z. B. `identifier`, `variable`, `procedure`, `symbol`, `statement`) als
  Anforderungsartefakt in `data-model.md` oder als eigene Datei definiert?
  [Remediation-Gate, Clarity, Gap] - BEFUND: "PASS — data-model.md §7 (Terminologietabelle) mit §7.1 PL/0-Keywords, §7.2 VM-Opcodes, §7.3 Domänen-Terminologie als normatives Artefakt erstellt."
- [x] CHK016 — Deckt die Terminologietabelle **alle Begriffe** ab, die in Pl0.Core- und
  Pl0.Vm-Fehlertexten vorkommen, sodass keine unkontrollierten Synonyme entstehen können?
  [Completeness, Consistency] - BEFUND: "PASS — §7.3 enthält 21 Domänen-Terme: identifier, variable, constant, procedure, statement, expression, operator, relational operator, assignment, assignment operator, parenthesis, period, semicolon, comma, symbol table, nesting level, call frame, instruction pointer, base pointer, stack, input, output."
- [x] CHK017 — Ist die Terminologietabelle als **normatives Referenzdokument** für den PR-Review
  gekennzeichnet — d. h. Abweichungen sind explizite Fehler, nicht Geschmackssache?
  [Measurability, Clarity] - BEFUND: "PASS — data-model.md §7 Status: 'Normatives Artefakt (NFR-003). Alle englischen Texte MUESSEN diese Tabelle einhalten. PR-Reviewer prueft alle Keys gegen diese Tabelle vor Merge.'"

---

## A7: UTF-8-Kompatibilität (ETR §A7 → ET CHK032)

- [x] CHK018 — Hat `spec.md §FR-005` die explizite Aussage erhalten, dass englische `.resx`-Dateien
  **UTF-8-kodiert** sein müssen und keine ASCII-only-Einschränkung gilt?
  [Remediation-Gate, Completeness, Spec §FR-005] - BEFUND: "PASS — spec.md §FR-005: 'Alle .resx-Dateien (Deutsch und Englisch) MUESSEN UTF-8-kodiert sein; eine ASCII-only-Einschraenkung gilt nicht.'"

---

## A8: Validierung der Test-Annahme (ETR §A8 → ET CHK033)

- [x] CHK019 — Wurde die Annahme „bestehende Tests prüfen keine Diagnosetexte" durch
  **Quellcode-Prüfung validiert** und das Ergebnis (Fact, nicht Assumption) in der Spec
  dokumentiert? [Remediation-Gate, Assumption, Spec §Clarifications §Q3] - BEFUND: "PASS — Quellcode-Prüfung 2026-03-04: CompilationDiagnosticsTests.cs konstruiert Diagnostics manuell (kein Compilerlauf); CatalogCasesTests.cs prüft nur Codes; IdeBootstrapTests.cs prüft Pl0.Ide (ausser Scope). Spec.md Assumptions als 'validiert, 2026-03-04' markiert."
- [x] CHK020 — Falls die Validierung Diagnosetexte in bestehenden Tests findet: Ist dokumentiert,
  **welche Tests angepasst** werden müssen und nach welchem Kriterium (`--lang de` explizit)?
  [Coverage, Exception Flow] - BEFUND: "N/A — Validierung ergab keine Diagnosetexte in bestehenden Tests (ausser Pl0.Ide, ausser Scope); keine Testanpassungen notwendig."

---

## A9: Reviewer-Verantwortlichkeit (ETR §A9 → ET CHK035)

- [x] CHK021 — Ist in `spec.md` dokumentiert, dass der **PR-Reviewer** die inhaltliche Korrektheit
  der englischen Übersetzungen prüft und verantwortet — nicht der Entwickler allein?
  [Remediation-Gate, Assumption, Gap] - BEFUND: "PASS — spec.md Abschnitt 'Reviewer-Verantwortlichkeit' dokumentiert PR-Reviewer-Verantwortung explizit."
- [x] CHK022 — Sind die **Reviewer-Kriterien** (Verweis auf Terminologietabelle, Großschreibungs-
  konvention, B2-Niveau) ausreichend definiert, sodass der Review ohne Subjektivität möglich ist?
  [Measurability, Clarity] - BEFUND: "PASS — 5 konkrete Reviewer-Kriterien: Terminologietabelle §7, Sentence case (NFR-002), B2-Niveau (NFR-001) mit KI-Agenten, Interpunktion (Pflichtpunkt), semantische Äquivalenz (FR-003/FR-004)."

---

## B1: Konkrete Testdaten (ETR §B1)

- [x] CHK023 — Hat `spec.md §US-2` / `quickstart.md` **konkrete erwartete englische Texte** (z. B.
  genauen String für `Parser_E11_UndeclaredIdent` auf Englisch) als Akzeptanzkriterien erhalten?
  [Remediation-Gate, Completeness, Measurability] - BEFUND: "PASS — data-model.md §3 enthält normative EN-Texte (z.B. 'Undeclared identifier.' für Parser_E11_UndeclaredIdent); quickstart.md §3 zeigt konkretes Beispiel 'Undeclared identifier.' als erwartete Ausgabe."
- [x] CHK024 — Sind die **Test-Daten-`.pl0`-Programme** für `L10nTests` spezifiziert (welche
  Fehlerfälle, welche Programmstruktur) und ihr Repository-Ablageort dokumentiert?
  [Completeness, Plan §Phase-1] - BEFUND: "PASS — plan.md §Source-Code-Structure listet spezifische .pl0-Programme (undeclared_ident.pl0, assign_to_const.pl0, division_by_zero.pl0, number_too_large.pl0 etc.) in tests/data/pl0/l10n/."

---

## B2: Testumfang (ETR §B2)

- [x] CHK025 — Hat `spec.md §SC-002/SC-003` die Anforderung erhalten, dass **alle ~50 englischen
  Keys einzeln** in `L10nTests` getestet werden müssen — nicht nur Stichproben?
  [Completeness, Clarity, Spec §SC-002] - BEFUND: "PASS — spec.md §SC-002: 'verifizieren alle ~75 englischen Keys einzeln in Pl0.Tests/L10nTests.cs'; plan.md listet alle 75 Testmethoden explizit."
- [x] CHK026 — Hat `spec.md §FR-006/FR-009` Fallback-Ketten-Tests mit **konkreten
  Eingabe/Ausgabe-Paaren** als Akzeptanzkriterien erhalten (en-US → en → de; xx → de +
  `stderr`-Warnung)? [Completeness, Measurability] - BEFUND: "PASS — spec.md §FR-006 enthält konkrete I/O-Paare (en-US→EN-Text, fr→DE-Fallback, leer→DE); FR-009 spezifiziert stderr mit Sprachcode 'xx'; plan.md Fallback-Ketten-Tests bestätigen."

---

## B3: Test-Infrastruktur (ETR §B3)

- [x] CHK027 — Hat `plan.md §Phase-1` die Anforderung erhalten, dass `L10nTests` via
  `CliOptionsParser` mit dem **`--lang`-Parameter** testen sollen — nicht direkt über
  `CompilerOptions.Language`? [Remediation-Gate, Clarity, Plan §Phase-1] - BEFUND: "PASS — plan.md §Test-Infrastruktur: 'Alle Tests... verwenden den --lang-Parameter via CliOptionsParser — nicht direkt CompilerOptions.Language'."
- [x] CHK028 — Ist die **`stderr`-Capture-Anforderung** für Fallback-Warnung-Tests als testbare
  Anforderung (nicht als Implementierungsdetail) in der Spec dokumentiert?
  [Clarity, Spec §FR-009] - BEFUND: "PASS — spec.md §FR-009: 'CliOptionsParser MUSS einen optionalen TextWriter errorOutput-Parameter akzeptieren... dies ist Anforderung, nicht Implementierungsdetail.'"

---

## B4: Erweiterbarkeitstest (ETR §B4)

- [x] CHK029 — Hat `spec.md §SC-004` die Dummy-Sprache **`--lang se`** (Schwedisch), eine
  Stichprobe von Keys aus **allen Modulen** und Fehlermeldungen auf Schwedisch als konkrete
  Anforderung erhalten? [Remediation-Gate, Clarity, Measurability] - BEFUND: "PASS — spec.md §SC-004: '--lang se', alle drei Module (Core/Vm/Cli), schwedische Texte über ResourceManager-Injection."
- [x] CHK030 — Ist der Erweiterbarkeitstest (SC-004) entweder als **automatisierbarer Test**
  (`.resx`-Datei im Repository vorhanden) oder als **explizit manueller Smoketest** deklariert —
  nicht unklar gelassen? [Clarity, Spec §SC-004] - BEFUND: "PASS — spec.md §SC-004: 'automatisierten Test in L10nTests.cs'; plan.md listet 3 spezifische Testmethoden (NewLocale_Se_InjectedResourceManager_Core/Vm/Cli)."

---

## Notes

- Befunde inline dokumentieren: `- [ ] CHK001 — BEFUND: "…"`
- Alle Items MÜSSEN Pass sein, bevor die zugehörigen Spec-Artefakte als „bereit für
  Implementation" gelten
- Nicht bestandene Items erfordern eine Aktualisierung von `spec.md`, `data-model.md`,
  `plan.md` oder `quickstart.md`
- Teil A (CHK001–CHK022): höhere Priorität — Vollständigkeit und Messbarkeit der
  Spec-Anforderungen
- Teil B (CHK023–CHK030): Testqualität und Testinfrastruktur-Anforderungen
