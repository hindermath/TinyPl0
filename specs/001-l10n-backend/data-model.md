# Data Model: L10N Backend (001-l10n-backend)

## Entitäten und Änderungen

### 1. `CompilerOptions` — erweitertes Record (Pl0.Core)

**Bestehend** (unveränderlich):
```
CompilerOptions
  Dialect            : Pl0Dialect
  MaxLevel           : int = 3
  MaxAddress         : int = 2047
  MaxIdentifierLength: int = 10
  MaxNumberDigits    : int = 14
  MaxSymbolCount     : int = 100
  MaxCodeLength      : int = 200
  EnableIoStatements : bool (computed)
```

**Neu** (additives Feld, optionaler Parameter mit Default):
```
  Language           : string = "de"   // BCP-47-Sprachcode
```

Constraints:
- Defaultwert `"de"` → Deutsch (Standardsprache gemäss Lastenheft 3.1)
- Wert wird nicht validiert — Validierung erfolgt in `Pl0.Cli` beim Parsen
- Bestehende Aufrufe `new CompilerOptions(dialect)` bleiben unverändert gültig

---

### 2. `VirtualMachineOptions` — erweitertes Record (Pl0.Vm)

**Bestehend**:
```
VirtualMachineOptions
  StackSize        : int  = 500
  EnableStoreTrace : bool = false
```

**Neu**:
```
  Language         : string = "de"   // BCP-47-Sprachcode
```

Constraints: identisch zu `CompilerOptions.Language`.

---

### 3. `Pl0CoreMessages` — Ressourcenklasse (Pl0.Core)

Autogenerierte strongly-typed Designer-Klasse aus `Pl0CoreMessages.resx`:

| Key | Platzhalter | Beispiel (de) |
|-----|-------------|---------------|
| `Lexer_E30_NumberTooManyDigits` | `{0}` = Lexem, `{1}` = MaxDigits | `"Zahl hat zu viele Ziffern: '{0}'. Max: {1}."` |
| `Lexer_E30_NumberTooLarge` | `{0}` = Lexem | `"Zahl zu groß: '{0}'."` |
| `Lexer_E33_IdentifierTooLong` | `{0}` = Lexem, `{1}` = MaxLen | `"Bezeichner zu lang: '{0}'. Max: {1}."` |
| `Lexer_E99_UnexpectedColon` | — | `"Unerwartetes ':'; erwartet ':='."`  |
| `Lexer_E99_UnexpectedChar` | `{0}` = Zeichen | `"Unerwartetes Zeichen '{0}'."` |
| `Parser_E01_UseEqualNotAssign` | — | `"Benutze '=' statt ':='."`  |
| `Parser_E02_NumberAfterEquals` | — | `"Auf '=' muss eine Zahl folgen."` |
| `Parser_E03_EqualAfterIdent` | — | `"Auf den Bezeichner muss '=' folgen."` |
| `Parser_E04_IdentAfterConst` | — | `"Auf 'const' muss ein Bezeichner folgen."` |
| `Parser_E04_IdentAfterVar` | — | `"Auf 'var' muss ein Bezeichner folgen."` |
| `Parser_E04_IdentAfterProc` | — | `"Auf 'procedure' muss ein Bezeichner folgen."` |
| `Parser_E04_IdentAfterInput` | — | `"Nach '?' wird ein Bezeichner erwartet."` |
| `Parser_E05_SemiOrComma` | — | `"Semikolon oder Komma fehlt."` |
| `Parser_E09_PeriodExpected` | — | `"Punkt erwartet."` |
| `Parser_E11_UndeclaredIdent` | — | `"Nicht deklarierter Bezeichner."` |
| `Parser_E12_AssignToConst` | — | `"Zuweisung an Konstante oder Prozedur nicht erlaubt."` |
| `Parser_E12_InputTargetMustBeVar` | — | `"Eingabeziel muss eine Variable sein."` |
| `Parser_E13_AssignOpExpected` | — | `"Zuweisungsoperator ':=' erwartet."` |
| `Parser_E14_CallNeedsIdent` | — | `"'call' muss von einem Bezeichner gefolgt werden."` |
| `Parser_E15_CallConstOrVar` | — | `"Aufruf von Konstante oder Variable sinnlos."` |
| `Parser_E16_ThenExpected` | — | `"'then' erwartet."` |
| `Parser_E17_SemiOrEndExpected` | — | `"Semikolon oder 'end' erwartet."` |
| `Parser_E18_DoExpected` | — | `"'do' erwartet."` |
| `Parser_E19_IncorrectSymbol` | — | `"Falsches Symbol nach Anweisung."` |
| `Parser_E19_InputNotInClassic` | — | `"Eingabe '?' im classic-Modus nicht verfügbar."` |
| `Parser_E19_OutputNotInClassic` | — | `"Ausgabe '!' im classic-Modus nicht verfügbar."` |
| `Parser_E20_RelOpExpected` | — | `"Vergleichsoperator erwartet."` |
| `Parser_E21_ProcInExpr` | — | `"Prozedurbezeichner in Ausdruck nicht erlaubt."` |
| `Parser_E22_RightParenMissing` | — | `"Schließende Klammer fehlt."` |
| `Parser_E24_BadExprStart` | — | `"Ausdruck kann nicht mit diesem Symbol beginnen."` |
| `Parser_E30_NumberTooLarge` | — | `"Diese Zahl ist zu groß."` |
| `Parser_E32_NestingTooDeep` | — | `"Maximale Blockschachtelungstiefe überschritten."` |
| `Parser_E34_SymbolTableOverflow` | `{0}` = Max | `"Symboltabelle voll (max {0})."` |
| `Parser_E35_ProgramTooLong` | `{0}` = Max | `"Programm zu lang (max {0} Befehle)."` |
| `Parser_E98_UnexpectedEndOfInput` | — | `"Programm unvollständig: unerwartetes Eingabeende."` |
| `Parser_E99_InvalidLexLevel` | — | `"Ungültige lexikalische Ebenenreferenz."` |
| `Parser_E99_UnexpectedTermination` | — | `"Unerwartetes Programmende."` |

---

### 4. `Pl0VmMessages` — Ressourcenklasse (Pl0.Vm)

| Key | Platzhalter | Beispiel (de) |
|-----|-------------|---------------|
| `Vm_E99_IPOutOfRange` | `{0}` = PC-Wert | `"Befehlszeiger außerhalb des Bereichs: {0}."` |
| `Vm_E99_InvalidLodIndex` | `{0}` = Index | `"Ungültiger LOD-Zugriff bei Stack-Index {0}."` |
| `Vm_E99_InvalidStoIndex` | `{0}` = Index | `"Ungültiger STO-Zugriff bei Stack-Index {0}."` |
| `Vm_E99_StackOverflowCallFrame` | — | `"Stack-Überlauf beim Erstellen eines Aufrufrahmens."` |
| `Vm_E99_StackOverflowInt` | — | `"Stack-Überlauf bei INT."` |
| `Vm_E99_UnsupportedOpcode` | `{0}` = Opcode | `"Nicht unterstützter Opcode: {0}."` |
| `Vm_E99_UnsupportedOpr` | `{0}` = OPR-Code | `"Nicht unterstützter OPR-Code: {0}."` |
| `Vm_E99_InvalidBasePointer` | `{0}` = Adresse | `"Ungültiger Basiszeiger beim Auflösen der Ebene: {0}."` |
| `Vm_E99_StackOverflow` | — | `"Stack-Überlauf."` |
| `Vm_E99_StackUnderflow` | — | `"Stack-Unterlauf."` |
| `Vm_E98_EndOfInput` | — | `"Eingabeende während des Lesens."` |
| `Vm_E97_InputFormatError` | — | `"Eingabeformat ungültig."` |
| `Vm_E206_DivisionByZero` | — | `"Division durch null."` |

---

### 5. `Pl0CliMessages` — Ressourcenklasse (Pl0.Cli)

| Key | Platzhalter | Beispiel (de) |
|-----|-------------|---------------|
| `Cli_Err_UnexpectedPositional` | `{0}` = Argument | `"Unerwartetes Argument: '{0}'."` |
| `Cli_Err_MissingValueForOut` | — | `"Fehlender Wert für '--out'."` |
| `Cli_Err_NoEmitMode` | — | `"Kein Ausgabemodus angegeben. Nutze '--emit asm' oder '--emit cod'."` |
| `Cli_Err_UnknownSwitch` | `{0}` = Switch | `"Unbekannter Schalter: '{0}'."` |
| `Cli_Err_ConflictingEmitModes` | — | `"Widersprüchliche Ausgabemodi. Nur einen von 'asm' oder 'cod' angeben."` |
| `Cli_Err_UnknownLanguage` | `{0}` = Code | `"Unbekannter Sprachcode '{0}', verwende Fallback 'de'."` |
| `Cli_Status_CompileSuccess` | — | `"Kompilierung erfolgreich."` |
| `Cli_Status_CompileError` | — | `"Kompilierung fehlgeschlagen."` |
| `Cli_Help_UsageHeader` | — | `"Verwendung:"` |
| `Cli_Help_HelpLine` | `{0}` = Programmname | `"{0} [-\|/]h \| [-\|/]? \| --help"` |
| `Cli_Help_CompileLine` | `{0}` = Programmname | `"{0} compile <datei.pl0> [--out <datei.pcode>] [--emit asm\|cod] [--list-code]"` |
| `Cli_Help_RunLine` | `{0}` = Programmname | `"{0} run <datei.pl0> [--emit asm\|cod] [--list-code]"` |
| `Cli_Help_RunPcodeLine` | `{0}` = Programmname | `"{0} run-pcode <datei.pcode> [--list-code]"` |
| `Cli_Help_RunPcodeDirectLine` | `{0}` = Programmname | `"{0} <datei.pcode> \| <datei.cod> [P-Code direkt ausführen]"` |
| `Cli_Help_ApiLine` | `{0}` = Programmname | `"{0} --api"` |
| `Cli_Help_LegacyLine` | `{0}` = Programmname | `"{0} <datei.pl0> [Legacy-Modus; kompilieren und ausführen]"` |
| `Cli_Help_SwitchesHeader` | — | `"Schalter:"` |
| `Cli_Help_SwitchErrmsg` | — | `"  [-\|/]errmsg \| --errmsg                lange Compiler-Fehlermeldungen anzeigen"` |
| `Cli_Help_SwitchWopcod` | — | `"  [-\|/]wopcod \| --wopcod                numerische Opcodes in Listing-Ausgabe"` |
| `Cli_Help_SwitchListCode` | — | `"  --list-code                            erzeugten/geladenen Code-Listing ausgeben"` |
| `Cli_Help_SwitchApi` | — | `"  --api                                  Webserver zur Dokumentation starten"` |
| `Cli_Help_SwitchConly` | — | `"  [-\|/]conly \| --conly \| --compile-only  nur kompilieren, VM nicht starten"` |
| `Cli_Help_SwitchEmitFormat` | — | `"  [-\|/]emit {[-\|/]asm \| [-\|/]cod}"` |
| `Cli_Help_SwitchEmit` | — | `"  --emit asm\|cod                         PL/0-Code auf STDOUT ausgeben"` |
| `Cli_Help_SwitchOut` | — | `"  --out <datei>                           Ausgabepfad für 'compile'-Befehl"` |
| `Cli_Help_SwitchLang` | — | `"  --lang <code>                           Ausgabesprache setzen (z. B. de, en)"` |

---

### 6. Fallback-Kette (Laufzeitverhalten)

```
Anfrage mit Language = "en-US"
  → ResourceManager sucht Pl0CoreMessages.en-US.resx  (nicht vorhanden)
  → fallback: Pl0CoreMessages.en.resx                  (vorhanden → liefert EN-Text)

Anfrage mit Language = "fr"
  → ResourceManager sucht Pl0CoreMessages.fr.resx      (nicht vorhanden)
  → fallback: Pl0CoreMessages.resx (Invariant = DE)    (liefert DE-Text)

Anfrage mit Language = "xx" (ungültig)
  → CultureInfo.GetCultureInfo("xx") wirft CultureNotFoundException
  → Pl0.Cli fängt Exception → Warnung auf stderr
  → Language wird auf "de" gesetzt
```

---

### 7. Terminologietabelle — Normative englische Begriffe

**Status**: Normatives Artefakt (NFR-003). Alle englischen Texte MÜSSEN diese Tabelle einhalten.
PR-Reviewer prüft alle Keys gegen diese Tabelle vor Merge.

#### 7.1 PL/0-Schlüsselwörter

In englischen Fehlertexten erscheinen PL/0-Schlüsselwörter **stets in Großbuchstaben** (NFR-002),
auch wenn sie als Quellcode-Elemente zitiert werden:

| Schlüsselwort | Form in EN-Text | Beispiel |
|---------------|-----------------|---------|
| `const` | `CONST` | `"CONST must be followed by an identifier."` |
| `var` | `VAR` | `"VAR must be followed by an identifier."` |
| `procedure` | `PROCEDURE` | `"PROCEDURE must be followed by an identifier."` |
| `begin` | `BEGIN` | — (kein eigener Fehlertext) |
| `end` | `END` | `"Semicolon or END expected."` |
| `call` | `CALL` | `"CALL must be followed by an identifier."` |
| `odd` | `ODD` | — |
| `if` | `IF` | — |
| `then` | `THEN` | `"THEN expected."` |
| `while` | `WHILE` | — |
| `do` | `DO` | `"DO expected."` |

#### 7.2 VM-Opcode-Namen

VM-Opcodes erscheinen in Fehlertexten **exakt wie definiert** (NFR-002):

| Opcode | Form in EN-Text |
|--------|-----------------|
| `Lit` | `Lit` |
| `Opr` | `Opr` |
| `Lod` | `Lod` |
| `Sto` | `Sto` |
| `Cal` | `Cal` |
| `Int` | `Int` |
| `Jmp` | `Jmp` |
| `Jpc` | `Jpc` |

#### 7.3 Domänen-Terminologie

| Kanonischer Begriff (EN) | Zu vermeiden | Kontext |
|--------------------------|--------------|---------|
| `identifier` | name, ident, symbol | Bezeichner in PL/0-Quellcode (CONST/VAR/PROCEDURE-Namen) |
| `variable` | var | Eine per VAR deklarierte Variable |
| `constant` | const | Eine per CONST deklarierte Konstante |
| `procedure` | proc, function, subroutine | Eine per PROCEDURE deklarierte Prozedur |
| `statement` | instruction, command | Eine PL/0-Anweisung |
| `expression` | expr | Arithmetischer oder boolescher Ausdruck |
| `operator` | op | Arithmetischer oder Vergleichsoperator |
| `relational operator` | comparison operator, rel-op | Vergleichsoperator (`=`, `<>`, `<`, `>`, `[`, `]`) |
| `assignment` | assign, assignment statement | `:=`-Zuweisung |
| `assignment operator` | assign op, `:=` operator | Das Symbol `:=` |
| `parenthesis` / `parentheses` | bracket, paren | `(` und `)` Begrenzer |
| `period` | dot, full stop | `.` am Programmende |
| `semicolon` | semi | `;` Trenner |
| `comma` | — | `,` Trenner |
| `symbol table` | symbol list, name table | Interne Compilerdatenstruktur |
| `nesting level` | nesting depth, block depth | Lexikalische Blocktiefe (max. 3) |
| `call frame` | activation record, stack frame | VM-Prozeduraufruf-Rahmen |
| `instruction pointer` | program counter, PC | VM-Register P |
| `base pointer` | base register | VM-Register B |
| `stack` | operand stack | VM-Ausführungsstapel |
| `input` | read, input statement | `?`-Eingabeanweisung |
| `output` | write, print, output statement | `!`-Ausgabeanweisung |

---

### Beziehungsübersicht

```
Pl0.Cli (CliOptionsParser)
  ├── parst --lang → string Language
  ├── erstellt CompilerOptions { Language }  →  Pl0.Core (Pl0Lexer, Pl0Parser)
  │                                               └── Pl0CoreMessages[Language]
  └── erstellt VirtualMachineOptions { Language } → Pl0.Vm (VirtualMachine)
                                                      └── Pl0VmMessages[Language]
```
