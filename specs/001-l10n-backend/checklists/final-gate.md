# Final PR Gate: L10N Backend

**Purpose**: Konsolidiertes PR-Merge-Gate — fasst alle offenen Anforderungslücken aus drei
Checklist-Runden zusammen. Ein Item hier entspricht einer **Kategorie** von Anforderungen;
die Detailprüfung erfolgt in den verlinkten Checklisten. Alle Items MÜSSEN Pass sein vor Merge.
**Created**: 2026-03-03
**Feature**: [spec.md](../spec.md) | [plan.md](../plan.md) | [data-model.md](../data-model.md)
**Vorläufer**:
- [english-translations.md](english-translations.md) — Runde 1 (35 Items)
- [english-translations-remediation.md](english-translations-remediation.md) — Runde 2 (26 Items)
- [spec-updates.md](spec-updates.md) — Runde 3A: Artefakt-Updates (30 Items)
- [translations-content.md](translations-content.md) — Runde 3B: Übersetzungsinhalt (35 Items)

---

## Gate 1 — Spec-Vollständigkeit

- [x] G001 — Sind **alle Remediation-Items** aus `spec-updates.md` (CHK001–CHK030) mit Pass
  bewertet, d. h. alle Spec-Artefakte (`spec.md`, `data-model.md`, `plan.md`, `quickstart.md`)
  wurden entsprechend der „Nein\"-Befunde aus Runde 1 und 2 aktualisiert?
  [Gate, Completeness, Spec-Updates §A1–B4] - BEFUND: "PASS — alle 30 Items in spec-updates.md mit PASS (oder N/A für CHK020) bewertet; alle Artefakte aktualisiert."
- [x] G002 — Enthält `spec.md §FR-003` und `§FR-004` eine **explizite, messbare Definition**
  von „semantischer Äquivalenz\", die ein Reviewer ohne Deutschkenntnisse anwenden kann?
  [Gate, Clarity, spec-updates.md CHK001–CHK003] - BEFUND: "PASS — FR-003/FR-004: gleiche Bedeutung, gleicher Fehlercode-Kontext, gleiche Platzhalter-Semantik; pruefbar ohne Deutschkenntnisse via Terminologietabelle §7 und Fehlercode."
- [x] G003 — Ist eine **Großschreibungskonvention** (Sentence case) für englische Fehlertexte
  in `spec.md` oder `data-model.md` dokumentiert und gilt explizit für alle drei Module?
  [Gate, Consistency, spec-updates.md CHK004–CHK006] - BEFUND: "PASS — spec.md NFR-002 definiert Sentence case für Pl0.Core, Pl0.Vm und Pl0.Cli; PL/0-Keywords und VM-Opcodes als Ausnahmen explizit dokumentiert."
- [x] G004 — Hat `data-model.md` **englische Beispielspalten** für alle Keys mit Platzhaltern
  (`{0}`, `{1}`) in allen drei Modulen erhalten?
  [Gate, Completeness, spec-updates.md CHK007–CHK010] - BEFUND: "PASS — data-model.md §3/§4/§5 um 'Beispiel (en)'-Spalte ergänzt (2026-03-04); alle Keys einschliesslich Platzhalter-Keys mit EN-Texten versehen."
- [x] G005 — Ist eine **Terminologietabelle** für konsistente englische Begriffe als normatives
  Anforderungsartefakt definiert und deckt alle Begriffe aus Core- und Vm-Fehlertexten ab?
  [Gate, Consistency, spec-updates.md CHK015–CHK017] - BEFUND: "PASS — data-model.md §7 (Terminologietabelle): §7.1 PL/0-Keywords, §7.2 VM-Opcodes, §7.3 22 Domänen-Terme; Status: Normatives Artefakt (NFR-003); Abweichungen blockieren PR-Merge."

---

## Gate 2 — Übersetzungsinhalt

- [x] G006 — Sind **alle 5 Lexer-Keys** mit englischen Anforderungstexten und korrekten
  Platzhalter-Spezifikationen versehen?
  [Gate, Completeness, translations-content.md CHK001–CHK005] - BEFUND: "PASS — alle fünf Lexer-Keys mit EN-Texten in data-model.md §3 spezifiziert; Platzhalter-Reihenfolge identisch zur DE-Vorlage."
- [x] G007 — Sind **alle explizit gelisteten Parser-Keys** (CHK006–CHK015) sowie die
  bisher unter `*(weitere Parser-Expect-Meldungen)*` zusammengefassten Keys vollständig
  mit englischen Anforderungstexten spezifiziert?
  [Gate, Completeness, translations-content.md CHK006–CHK016] - BEFUND: "PASS — alle 32 Parser-Keys mit EN-Texten spezifiziert; Placeholder '*(weitere Parser-Expect-Meldungen)*' entfernt; alle 10 fehlenden Keys aus Quellcode-Analyse ergänzt."
- [x] G008 — Sind **alle 13 VM-Keys** mit englischen Anforderungstexten spezifiziert, unter
  Beibehaltung der VM-Opcode-Namen (`LOD`, `STO`, `OPR` etc.) als unveränderte Terme?
  [Gate, Completeness, translations-content.md CHK017–CHK023] - BEFUND: "PASS — alle 13 VM-Keys mit EN-Texten spezifiziert; LOD, STO, INT, OPR als Assembly-Mnemonics unverändert; 'instruction pointer' und 'base pointer' über §7.3 normiert."
- [x] G009 — Sind **alle CLI-Keys** (6 Err + 2 Status + Help-Texte) mit vollständigen englischen
  Anforderungstexten versehen — kein offener `"..."` Platzhalter in `data-model.md §5`?
  [Gate, Completeness, translations-content.md CHK024–CHK028] - BEFUND: "PASS — alle 25 CLI-Keys (6 Err + 2 Status + 17 Help) mit EN-Texten in data-model.md §5 spezifiziert; kein offener Platzhalter."
- [x] G010 — Sind **alle englischen Anforderungstexte** über alle ~75 Keys hinweg konsistent
  in Großschreibung (Sentence case), Interpunktion (abschließender Punkt) und Terminologie?
  [Gate, Consistency, translations-content.md CHK029–CHK035] - BEFUND: "PASS — alle CHK029–CHK035 in translations-content.md mit PASS bewertet; Sentence case, Pflichtpunkt, Terminologietabelle §7 und PL/0-Keyword-Großschreibung konsistent eingehalten."

---

## Gate 3 — Test-Anforderungen

- [x] G011 — Ist die Anforderung, dass `L10nTests` **alle ~75 englischen Keys einzeln** testet,
  explizit in `spec.md §SC-002/SC-003` dokumentiert?
  [Gate, Completeness, spec-updates.md CHK025] - BEFUND: "PASS — spec.md §SC-002: 'verifizieren alle ~75 englischen Keys einzeln in Pl0.Tests/L10nTests.cs'; plan.md listet alle 75 Testmethoden namentlich."
- [x] G012 — Ist die Anforderung, dass `L10nTests` via **`--lang`-Parameter** (nicht direkt
  über `CompilerOptions.Language`) testet, in `plan.md §Phase-1` dokumentiert?
  [Gate, Clarity, spec-updates.md CHK027] - BEFUND: "PASS — plan.md §Test-Infrastruktur: 'Alle Tests für Lexer-, Parser- und PL/0-ausloesbare VM-Keys verwenden den --lang-Parameter via CliOptionsParser — nicht direkt CompilerOptions.Language'."
- [x] G013 — Ist die Anforderung, dass VM-Fehlertexte über **`BufferedPl0Io`** (simuliertes I/O)
  geprüft werden, als Anforderung festgelegt — mit Begründung gegenüber `ConsolePl0Io`?
  [Gate, Clarity, spec-updates.md CHK011–CHK012] - BEFUND: "PASS — spec.md Assumptions: 'ConsolePl0Io in Tests nicht fangbar'; SC-002 und plan.md §Test-Infrastruktur legen BufferedPl0Io als Anforderung fest."
- [x] G014 — Sind **Fallback-Ketten-Tests** (`en-US` → `en` → `de`; `xx` → `de` + `stderr`)
  mit konkreten Eingabe/Ausgabe-Paaren als Akzeptanzkriterien in `spec.md §FR-006/FR-009`
  dokumentiert? [Gate, Measurability, spec-updates.md CHK026] - BEFUND: "PASS — spec.md §FR-006 mit drei konkreten I/O-Paaren (en-US→EN-Text, fr→DE-Fallback, leer→DE); FR-009: '--lang xx → stderr enthält Warnung mit xx; stdout enthält deutschen Text'."
- [x] G015 — Ist die **`stderr`-Capture-Anforderung** für den Fallback-Warnungs-Test als
  testbare Anforderung (nicht Implementierungsdetail) in der Spec festgelegt?
  [Gate, Clarity, spec-updates.md CHK028] - BEFUND: "PASS — spec.md §FR-009: 'CliOptionsParser MUSS einen optionalen TextWriter errorOutput-Parameter akzeptieren... dies ist Anforderung, nicht Implementierungsdetail.'"
- [x] G016 — Hat `spec.md §SC-004` (Erweiterbarkeitstest) die Dummy-Sprache **`--lang se`**
  (Schwedisch), Stichprobe aus allen Modulen und ist als automatisierbar oder manuell deklariert?
  [Gate, Clarity, spec-updates.md CHK029–CHK030] - BEFUND: "PASS — spec.md §SC-004: '--lang se', alle drei Module (Core/Vm/Cli), 'automatisierten Test in L10nTests.cs'; plan.md: drei Testmethoden NewLocale_Se_*."
- [x] G017 — Sind **konkrete `.pl0`-Testprogramme** für `L10nTests` (welche Fehlerfälle, welcher
  Ablageort im Repository) spezifiziert?
  [Gate, Completeness, spec-updates.md CHK024] - BEFUND: "PASS — plan.md §Source-Code-Structure: undeclared_ident.pl0, assign_to_const.pl0, division_by_zero.pl0, number_too_large.pl0 etc. in tests/data/pl0/l10n/; spec.md SC-004 bestätigt Ablageort."
- [x] G018 — Sind **konkrete erwartete englische Texte** (z. B. genauen String für
  `Parser_E11_UndeclaredIdent`) als normative Akzeptanzkriterien in `spec.md §US-2` oder
  `quickstart.md` dokumentiert?
  [Gate, Measurability, spec-updates.md CHK023] - BEFUND: "PASS — data-model.md §3 enthält normative EN-Texte für alle Keys (z.B. 'Undeclared identifier.' für Parser_E11); quickstart.md §3 zeigt konkreten EN-String als erwartete Ausgabe."

---

## Gate 4 — Nicht-funktionale Anforderungen

- [x] G019 — Ist eine **NFR für B2-Sprachniveau** (CEFR) für alle englischen Fehlertexte in
  `spec.md` als messbares Kriterium mit KI-Agenten-Review dokumentiert?
  [Gate, Non-Functional, spec-updates.md CHK013–CHK014] - BEFUND: "PASS — spec.md NFR-001: B2-CEFR für alle englischen Fehlertexte; 'Pruefung: PR-Review durch KI-Agenten'; Reviewer-Verantwortlichkeit §3 Punkt 3 bestätigt."
- [x] G020 — Ist die **UTF-8-Anforderung** für englische `.resx`-Dateien explizit in
  `spec.md §FR-005` dokumentiert (keine ASCII-only-Einschränkung)?
  [Gate, Non-Functional, spec-updates.md CHK018] - BEFUND: "PASS — spec.md §FR-005: 'Alle .resx-Dateien (Deutsch und Englisch) MUESSEN UTF-8-kodiert sein; eine ASCII-only-Einschraenkung gilt nicht.'"

---

## Gate 5 — Prozess und Verantwortlichkeit

- [x] G021 — Ist die **Reviewer-Verantwortlichkeit** für englische Übersetzungsqualität in
  `spec.md` dokumentiert — PR-Reviewer prüft und verantwortet Korrektheit?
  [Gate, Process, spec-updates.md CHK021] - BEFUND: "PASS — spec.md Abschnitt 'Reviewer-Verantwortlichkeit': 'Der PR-Reviewer ist verantwortlich für die inhaltliche Korrektheit aller englischen Uebersetzungen.'"
- [x] G022 — Sind **Reviewer-Kriterien** (Terminologietabelle, Großschreibungskonvention,
  B2-Niveau, Interpunktionsregel) ausreichend definiert für objektiven Review ohne Subjektivität?
  [Gate, Measurability, spec-updates.md CHK022] - BEFUND: "PASS — spec.md Reviewer-Verantwortlichkeit: 5 konkrete Kriterien (Terminologietabelle §7, Sentence case NFR-002, B2-Niveau NFR-001, Interpunktion, semantische Äquivalenz FR-003/004)."
- [x] G023 — Wurde die Annahme „bestehende Tests prüfen keine Diagnosetexte\" durch
  **Quellcode-Prüfung validiert** und als Fact (nicht Assumption) in der Spec dokumentiert?
  [Gate, Assumption, spec-updates.md CHK019–CHK020] - BEFUND: "PASS — spec.md Assumptions: 'Test-Annahme (validiert, 2026-03-04)' mit Detailbefund; CompilationDiagnosticsTests.cs konstruiert manuell; CatalogCasesTests.cs nur Codes; IdeBootstrapTests.cs ausser Scope."

---

## Gate 6 — Konsistenz und Keine Regressionen

- [x] G024 — Sind `spec.md`, `data-model.md`, `plan.md`, `quickstart.md` und
  `contracts/cli-contract.md` **untereinander konsistent** — keine widersprüchlichen Anforderungen
  zwischen den Artefakten? [Gate, Consistency, Cross-Artifact] - BEFUND: "PASS — Cross-Artifact-Prüfung: Schlüsselzahl (~75) konsistent in spec.md/plan.md/data-model.md (37+13+25=75); --lang se, tests/data/pl0/l10n/, ResourceManager-Injection konsistent; keine Widersprüche gefunden."
- [x] G025 — Ist `data-model.md §3` um alle bisher unvollständig spezifizierten
  `*(weitere Parser-Expect-Meldungen)*` vollständig erweitert — kein offener Platzhalter mehr?
  [Gate, Completeness, translations-content.md CHK016, english-translations.md CHK003] - BEFUND: "PASS — Placeholder '*(weitere Parser-Expect-Meldungen)*' in Runde 7 entfernt; alle 32 Parser-Keys vollständig mit DE- und EN-Texten in §3 aufgelistet."
- [x] G026 — Ist `data-model.md §5` um alle bisher unvollständig spezifizierten
  `*(weitere Hilfe-Texte)*` vollständig erweitert — kein offener Platzhalter mehr?
  [Gate, Completeness, translations-content.md CHK028, english-translations.md CHK010] - BEFUND: "PASS — Placeholder '*(weitere Hilfe-Texte)*' in Runde 7 durch 17 explizite Cli_Help_*-Keys ersetzt; alle mit DE- und EN-Texten in §5 spezifiziert."

---

## Gate-Status-Übersicht

| Gate | Thema | Items | Status |
|------|-------|-------|--------|
| Gate 1 | Spec-Vollständigkeit | G001–G005 | ✅ Pass |
| Gate 2 | Übersetzungsinhalt | G006–G010 | ✅ Pass |
| Gate 3 | Test-Anforderungen | G011–G018 | ✅ Pass |
| Gate 4 | Nicht-funktionale Anforderungen | G019–G020 | ✅ Pass |
| Gate 5 | Prozess & Verantwortlichkeit | G021–G023 | ✅ Pass |
| Gate 6 | Konsistenz & Keine Regressionen | G024–G026 | ✅ Pass |
| **Gesamt** | | **26 Items** | **✅ Bereit für Merge** |

---

## Merge-Kriterium

> **Alle 26 Gate-Items MÜSSEN Pass (✅) sein.**
> Kein partieller Merge zulässig. Offene Items blockieren die Implementation (Phase 2).

**Befund (2026-03-04): Alle 26 Gate-Items PASS — Spec-Artefakte bereit für Implementation.**

## Notes

- Befunde inline dokumentieren: `- [ ] G001 — BEFUND: "…"`
- Gate-Status-Tabelle bei Abschluss aktualisieren: ⬜ Offen → ✅ Pass oder ❌ Fail
- Detailprüfung immer in den verlinkten Checklisten; dieses Dokument ist das Entscheidungsdokument
- Reihenfolge der Bearbeitung: Gate 1 → Gate 5 → Gate 2 → Gate 3 → Gate 4 → Gate 6
