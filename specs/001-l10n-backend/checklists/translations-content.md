# Translations Content Checklist: L10N Backend

**Purpose**: Dritter Review-Durchlauf (Teil B) — prüft die **Anforderungsqualität der konkreten
englischen Übersetzungsinhalte** für alle ~75 Keys über alle drei Module. Jedes Item bewertet,
ob die Anforderungen für den englischen Text eines Keys präzise, konsistent und testbar sind.
**Created**: 2026-03-03
**Feature**: [spec.md](../spec.md) | [data-model.md](../data-model.md)
**Verwandt**: [spec-updates.md](spec-updates.md) — prüft ob Artefakt-Updates erfolgt sind

---

## Pl0.Core — Lexer-Keys (data-model.md §3)

- [x] CHK001 — Ist für `Lexer_E30_NumberTooManyDigits` ein englischer Anforderungstext spezifiziert,
  der beide Platzhalter `{0}` (Lexem) und `{1}` (MaxDigits) in korrekter Reihenfolge enthält?
  [Completeness, Placeholder, Data-Model §3] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Number has too many digits: '{0}'. Max: {1}.' — {0}=Lexem, {1}=MaxDigits, identische Reihenfolge wie DE."
- [x] CHK002 — Ist für `Lexer_E30_NumberTooLarge` ein englischer Anforderungstext mit Platzhalter
  `{0}` (Lexem) spezifiziert? [Completeness, Placeholder, Data-Model §3] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Number is too large: '{0}.' — {0}=Lexem."
- [x] CHK003 — Ist für `Lexer_E33_IdentifierTooLong` ein englischer Anforderungstext mit beiden
  Platzhaltern `{0}` (Lexem) und `{1}` (MaxLen) in korrekter Reihenfolge spezifiziert?
  [Completeness, Placeholder, Data-Model §3] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Identifier too long: '{0}'. Max: {1}.' — {0}=Lexem, {1}=MaxLen."
- [x] CHK004 — Ist für `Lexer_E99_UnexpectedColon` ein englischer Anforderungstext (ohne
  Platzhalter) spezifiziert, der den erwarteten Token `:=` benennt?
  [Completeness, Clarity, Data-Model §3] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Unexpected ':'; expected ':='.' — ':=' explizit benannt."
- [x] CHK005 — Ist für `Lexer_E99_UnexpectedChar` ein englischer Anforderungstext mit Platzhalter
  `{0}` (Zeichen) spezifiziert? [Completeness, Placeholder, Data-Model §3] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Unexpected character '{0}'.' — {0}=Zeichen."

---

## Pl0.Core — Parser-Keys (data-model.md §3)

- [x] CHK006 — Ist für `Parser_E01_UseEqualNotAssign` ein englischer Anforderungstext spezifiziert,
  der die PL/0-Tokens `=` und `:=` unverändert beibehält (nicht übersetzt)?
  [Completeness, Clarity, PL/0-Keywords] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Use '=' instead of ':='.' — '=' und ':=' unverändert."
- [x] CHK007 — Sind für alle Parser-„Expect"-Meldungen (z. B. `Parser_E09_PeriodExpected`,
  `Parser_E16_ThenExpected`, `Parser_E17_SemiOrEndExpected`, `Parser_E18_DoExpected`)
  englische Anforderungstexte spezifiziert? [Completeness, Data-Model §3] - BEFUND: "PASS — data-model.md §3: 'Period expected.', 'THEN expected.', 'Semicolon or END expected.', 'DO expected.' — alle vier Keys spezifiziert."
- [x] CHK008 — Ist für `Parser_E11_UndeclaredIdent` ein konkreter englischer Anforderungstext
  spezifiziert — als normatives Akzeptanzkriterium, nicht nur als Freitext-Anforderung?
  [Completeness, Measurability, Spec §US-2] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Undeclared identifier.' — normativ, exakt als Akzeptanzkriterium nutzbar."
- [x] CHK009 — Ist für `Parser_E12_AssignToConst` ein englischer Anforderungstext spezifiziert,
  der die PL/0-Schlüsselwörter `CONST` und `PROCEDURE` unverändert oder eindeutig referenziert?
  [Completeness, PL/0-Keywords, Data-Model §3] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Assignment to CONST or PROCEDURE not allowed.' — CONST und PROCEDURE in Großbuchstaben."
- [x] CHK010 — Ist für `Parser_E12_InputTargetMustBeVar` ein englischer Anforderungstext
  spezifiziert, der die Terminologie `variable` konsistent mit der Terminologietabelle verwendet?
  [Completeness, Terminology] - BEFUND: "PASS — data-model.md §3 Beispiel (en): 'Input target must be a variable.' — 'variable' gemäss §7.3."
- [x] CHK011 — Sind für alle `Parser_E14_*`, `Parser_E15_*`-Keys englische Anforderungstexte
  spezifiziert, die den PL/0-Schlüsselwort `CALL` korrekt referenzieren?
  [Completeness, PL/0-Keywords, Data-Model §3] - BEFUND: "PASS — data-model.md §3: Parser_E14: 'CALL must be followed by an identifier.'; Parser_E15: 'Calling a constant or variable is not meaningful.' — CALL in Großbuchstaben."
- [x] CHK012 — Sind für `Parser_E19_InputNotInClassic` und `Parser_E19_OutputNotInClassic`
  englische Anforderungstexte spezifiziert, die die PL/0-Symbole `?` und `!` unverändert
  beibehalten? [Completeness, PL/0-Keywords] - BEFUND: "PASS — data-model.md §3: 'Input '?' is not available in classic mode.' und 'Output '!' is not available in classic mode.' — '?' und '!' unverändert."
- [x] CHK013 — Sind für `Parser_E21_ProcInExpr`, `Parser_E22_RightParenMissing`,
  `Parser_E24_BadExprStart` englische Anforderungstexte spezifiziert?
  [Completeness, Data-Model §3] - BEFUND: "PASS — data-model.md §3: 'Procedure identifier not allowed in expression.', 'Closing parenthesis missing.', 'Expression cannot start with this symbol.' — alle drei Keys spezifiziert."
- [x] CHK014 — Sind für `Parser_E34_SymbolTableOverflow` und `Parser_E35_ProgramTooLong`
  englische Anforderungstexte mit Platzhalter `{0}` (Max-Wert) spezifiziert?
  [Completeness, Placeholder, Data-Model §3] - BEFUND: "PASS — data-model.md §3: 'Symbol table full (max {0}).' und 'Program too long (max {0} instructions).' — {0}=Max-Wert."
- [x] CHK015 — Sind für `Parser_E98_UnexpectedEndOfInput`, `Parser_E99_InvalidLexLevel`,
  `Parser_E30_NumberTooLarge`, `Parser_E32_NestingTooDeep`, `Parser_E20_RelOpExpected`
  englische Anforderungstexte spezifiziert? [Completeness, Data-Model §3] - BEFUND: "PASS — data-model.md §3: 'Program incomplete: unexpected end of input.', 'Invalid lexical level reference.', 'This number is too large.', 'Maximum nesting level exceeded.', 'Relational operator expected.' — alle fünf Keys spezifiziert."
- [x] CHK016 — Sind die unter `*(weitere Parser-Expect-Meldungen)*` in `data-model.md §3`
  zusammengefassten Keys **vollständig aufgelistet** mit englischen Anforderungstexten?
  [Completeness, Gap, Data-Model §3] - BEFUND: "PASS — Placeholder '*(weitere Parser-Expect-Meldungen)*' wurde in Runde 7 entfernt; alle 10 fehlenden Parser-Keys (E02, E03, E04×4, E05, E13, E19-3rd, E99-2nd) wurden explizit mit EN-Texten in §3 aufgenommen."

---

## Pl0.Vm — VM-Keys (data-model.md §4)

- [x] CHK017 — Sind für alle drei Stack-Overflow-Keys (`Vm_E99_StackOverflowCallFrame`,
  `Vm_E99_StackOverflowInt`, `Vm_E99_StackOverflow`) englische Anforderungstexte spezifiziert,
  die die VM-Terminologie konsistent verwenden? [Completeness, Terminology, Data-Model §4] - BEFUND: "PASS — data-model.md §4: 'Stack overflow while creating call frame.', 'Stack overflow on INT.', 'Stack overflow.' — 'call frame' (§7.3) und 'INT' (Opcode, §7.2) konsistent."
- [x] CHK018 — Ist für `Vm_E99_IPOutOfRange` ein englischer Anforderungstext mit Platzhalter
  `{0}` (PC-Wert) spezifiziert? Wird der VM-Begriff „program counter" oder „instruction pointer"
  als fester Term in der Terminologietabelle definiert? [Completeness, Terminology, Placeholder] - BEFUND: "PASS — data-model.md §4: 'Instruction pointer out of range: {0}.' mit {0}=PC-Wert; §7.3 definiert 'instruction pointer' als kanonischen Begriff (nicht 'program counter' oder 'PC')."
- [x] CHK019 — Sind für `Vm_E99_InvalidLodIndex` und `Vm_E99_InvalidStoIndex` englische
  Anforderungstexte mit Platzhalter `{0}` (Stack-Index) spezifiziert, die die VM-Opcodes
  `LOD` und `STO` **unverändert** (nicht übersetzt) beibehalten?
  [Completeness, Placeholder, VM-Opcodes] - BEFUND: "PASS — data-model.md §4: 'Invalid LOD access at stack index {0}.' und 'Invalid STO access at stack index {0}.' — LOD und STO unverändert; §7.2 definiert Lod/Sto als Opcode-Namen (die internen Fehlertexte verwenden die Assembly-Mnemonik LOD/STO)."
- [x] CHK020 — Ist für `Vm_E99_UnsupportedOpcode` ein englischer Anforderungstext mit Platzhalter
  `{0}` (Opcode) spezifiziert? Ist definiert, dass Opcode-Namen (`Lit`, `Opr`, `Lod` etc.)
  im englischen Text unverändert bleiben? [Completeness, Placeholder, VM-Opcodes] - BEFUND: "PASS — data-model.md §4: 'Unsupported opcode: {0}.' mit {0}=Opcode; §7.2 definiert Opcode-Formen als unveränderlich in EN-Texten."
- [x] CHK021 — Ist für `Vm_E99_UnsupportedOpr` ein englischer Anforderungstext mit Platzhalter
  `{0}` (OPR-Code) spezifiziert? Ist definiert, dass `OPR` als technischer Begriff unverändert
  bleibt? [Completeness, Placeholder, VM-Opcodes] - BEFUND: "PASS — data-model.md §4: 'Unsupported OPR code: {0}.' mit {0}=OPR-Code; OPR ist Assembly-Mnemonic und bleibt unverändert (Quelle: VirtualMachine.cs Inline-String)."
- [x] CHK022 — Ist für `Vm_E99_InvalidBasePointer` ein englischer Anforderungstext mit
  Platzhalter `{0}` (Adresse) spezifiziert? Wird `base pointer` als fester Term definiert?
  [Completeness, Placeholder, Terminology] - BEFUND: "PASS — data-model.md §4: 'Invalid base pointer while resolving level: {0}.' mit {0}=Adresse; §7.3 definiert 'base pointer' als kanonischen Begriff."
- [x] CHK023 — Sind für `Vm_E99_StackUnderflow`, `Vm_E98_EndOfInput`, `Vm_E97_InputFormatError`
  und `Vm_E206_DivisionByZero` englische Anforderungstexte (ohne Platzhalter) spezifiziert?
  [Completeness, Data-Model §4] - BEFUND: "PASS — data-model.md §4: 'Stack underflow.', 'End of input while reading.', 'Invalid input format.', 'Division by zero.' — alle vier Keys ohne Platzhalter spezifiziert."

---

## Pl0.Cli — CLI-Keys (data-model.md §5)

- [x] CHK024 — Sind für alle sechs `Cli_Err_*`-Keys (`UnexpectedPositional`, `MissingValueForOut`,
  `NoEmitMode`, `UnknownSwitch`, `ConflictingEmitModes`, `UnknownLanguage`) englische
  Anforderungstexte spezifiziert? [Completeness, Data-Model §5] - BEFUND: "PASS — data-model.md §5 enthält EN-Texte für alle sechs Cli_Err_*-Keys."
- [x] CHK025 — Ist für `Cli_Err_UnknownSwitch` ein englischer Anforderungstext mit Platzhalter
  `{0}` (Switch-Name) spezifiziert? [Completeness, Placeholder, Data-Model §5] - BEFUND: "PASS — data-model.md §5: 'Unknown switch: '{0}'.' mit {0}=Switch."
- [x] CHK026 — Ist für `Cli_Err_UnknownLanguage` ein englischer Anforderungstext mit Platzhalter
  `{0}` (Sprachcode) spezifiziert — **der Text selbst ist auf Englisch**, auch wenn er eine
  Fallback-Situation beschreibt? [Completeness, Edge Case, Spec §FR-009] - BEFUND: "PASS — data-model.md §5: 'Unknown language code '{0}', using fallback 'de'.' — Text auf Englisch, {0}=Sprachcode."
- [x] CHK027 — Sind für beide `Cli_Status_*`-Keys (`CompileSuccess`, `CompileError`) englische
  Anforderungstexte spezifiziert? [Completeness, Data-Model §5] - BEFUND: "PASS — data-model.md §5: 'Compilation successful.' und 'Compilation failed.' — beide Keys spezifiziert."
- [x] CHK028 — Sind für `Cli_Help_Usage` und alle unter `*(weitere Hilfe-Texte)*` zusammengefassten
  Keys **vollständige englische Anforderungstexte** spezifiziert — kein offener Platzhalter
  `"..."` ohne konkreten Inhalt? [Completeness, Gap, Data-Model §5] - BEFUND: "PASS — Placeholder '*(weitere Hilfe-Texte)*' in Runde 7 durch 17 explizite Cli_Help_*-Keys ersetzt; alle mit EN-Texten in data-model.md §5 spezifiziert."

---

## Terminologie und Stil — Übergreifend

- [x] CHK029 — Verwenden alle englischen Anforderungstexte für „Bezeichner" konsequent `identifier`
  (nicht `name`, `symbol`, `label`) gemäss Terminologietabelle?
  [Consistency, Terminology] - BEFUND: "PASS — alle Keys mit Bezeichner-Bezug verwenden 'identifier': Parser_E11 'Undeclared identifier.', Parser_E14 'CALL must be followed by an identifier.', Parser_E03 'Identifier must be followed by...', Parser_E04 'an identifier.' — konsistent."
- [x] CHK030 — Verwenden alle englischen Anforderungstexte für „Prozedur" konsequent `procedure`
  (PL/0-Schlüsselwort unverändert) und nicht `function` oder `subroutine`?
  [Consistency, PL/0-Keywords] - BEFUND: "PASS — Parser_E12_AssignToConst: '...CONST or PROCEDURE...'; Parser_E21_ProcInExpr: 'Procedure identifier...' — kein 'function' oder 'subroutine' in EN-Texten."
- [x] CHK031 — Sind alle englischen Anforderungstexte mit einem abschließenden Punkt `.`
  spezifiziert — analog zur deutschen Vorlage und gemäss ET CHK016?
  [Consistency, Style, Data-Model §3/§4/§5] - BEFUND: "PASS — alle EN-Texte in data-model.md §3/§4/§5 enden mit '.'; CLI-Help-Zeilen (reine Befehlsformat-Strings ohne Satznatur) wurden nicht mit Punkt versehen, da sie kein vollständiger Satz sind."
- [x] CHK032 — Beginnen alle englischen Anforderungstexte mit einem Großbuchstaben (Sentence case)
  gemäss der definierten Großschreibungskonvention (spec-updates.md CHK004)?
  [Consistency, Style] - BEFUND: "PASS — alle EN-Anforderungstexte beginnen mit Großbuchstaben; Parser_E04_IdentAfterInput: 'An identifier must follow '?'.' (beginnt mit 'An', nicht mit Sonderzeichen '?')."
- [x] CHK033 — Sind alle englischen Anforderungstexte **auf B2-Sprachniveau** formuliert —
  keine Fachjargon-Wörter, die über das Niveau eines nicht-muttersprachlichen Lernenden hinausgehen,
  soweit PL/0-Terminologie es zulässt? [Non-Functional, B2-Level] - BEFUND: "PASS — EN-Texte verwenden klares, einfaches Vokabular; unvermeidbare PL/0-Fachbegriffe (identifier, opcode, call frame, instruction pointer) sind durch Terminologietabelle §7 normiert und im Lernkontext bekannt."
- [x] CHK034 — Sind englische Anforderungstexte, die PL/0-Schlüsselwörter (`BEGIN`, `END`,
  `CALL`, `IF`, `THEN`, `WHILE`, `DO`, `ODD`, `CONST`, `VAR`) enthalten, konsistent so
  spezifiziert, dass diese Wörter **unverändert und in Großbuchstaben** im Text erscheinen?
  [Consistency, PL/0-Keywords] - BEFUND: "PASS — CONST (Parser_E04, E12), VAR (Parser_E04), PROCEDURE (Parser_E04, E12), CALL (Parser_E14, E15), THEN (Parser_E16), END (Parser_E17), DO (Parser_E18) — alle in Großbuchstaben in EN-Texten."
- [x] CHK035 — Ist für alle englischen Anforderungstexte mit Platzhaltern sichergestellt, dass
  **Platzhalter-Reihenfolge** im EN-Text identisch zur DE-Vorlage ist — keine implizite
  Umstellung durch Grammatikunterschied DE/EN?
  [Consistency, Placeholder, Data-Model §3/§4/§5] - BEFUND: "PASS — alle Platzhalter-Reihenfolgen geprüft: Lexer_E30_NumberTooManyDigits {0},{1} DE/EN identisch; Lexer_E33_IdentifierTooLong {0},{1} DE/EN identisch; alle VM/CLI-Keys single-placeholder, keine Umstellung möglich."

---

## Notes

- Befunde inline dokumentieren: `- [ ] CHK001 — BEFUND: "…"`
- Voraussetzung: `spec-updates.md` CHK007–CHK010 (EN-Beispielspalten in data-model.md) müssen
  Pass sein, damit dieser Checklist vollständig bewertet werden kann
- Items CHK001–CHK028 prüfen **Vollständigkeit** der EN-Anforderungen je Key
- Items CHK029–CHK035 prüfen **konsistenten Stil** über alle Keys hinweg
- Alle Items MÜSSEN Pass sein als PR-Review-Gate vor Merge
