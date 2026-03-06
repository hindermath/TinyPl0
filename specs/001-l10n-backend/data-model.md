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

| Key | Platzhalter | Beispiel (de) | Beispiel (en) |
|-----|-------------|---------------|---------------|
| `Lexer_E30_NumberTooManyDigits` | `{0}` = Lexem, `{1}` = MaxDigits | `"Zahl hat zu viele Ziffern: '{0}'. Max: {1}."` | `"Number has too many digits: '{0}'. Max: {1}."` |
| `Lexer_E30_NumberTooLarge` | `{0}` = Lexem | `"Zahl zu groß: '{0}'."` | `"Number is too large: '{0}'."` |
| `Lexer_E33_IdentifierTooLong` | `{0}` = Lexem, `{1}` = MaxLen | `"Bezeichner zu lang: '{0}'. Max: {1}."` | `"Identifier too long: '{0}'. Max: {1}."` |
| `Lexer_E99_UnexpectedColon` | — | `"Unerwartetes ':'; erwartet ':='."` | `"Unexpected ':'; expected ':='."` |
| `Lexer_E99_UnexpectedChar` | `{0}` = Zeichen | `"Unerwartetes Zeichen '{0}'."` | `"Unexpected character '{0}'."` |
| `Parser_E01_UseEqualNotAssign` | — | `"Benutze '=' statt ':='."` | `"Use '=' instead of ':='."` |
| `Parser_E02_NumberAfterEquals` | — | `"Auf '=' muss eine Zahl folgen."` | `"'=' must be followed by a number."` |
| `Parser_E03_EqualAfterIdent` | — | `"Auf den Bezeichner muss '=' folgen."` | `"Identifier must be followed by '='."` |
| `Parser_E04_IdentAfterConst` | — | `"Auf 'const' muss ein Bezeichner folgen."` | `"CONST must be followed by an identifier."` |
| `Parser_E04_IdentAfterVar` | — | `"Auf 'var' muss ein Bezeichner folgen."` | `"VAR must be followed by an identifier."` |
| `Parser_E04_IdentAfterProc` | — | `"Auf 'procedure' muss ein Bezeichner folgen."` | `"PROCEDURE must be followed by an identifier."` |
| `Parser_E04_IdentAfterInput` | — | `"Nach '?' wird ein Bezeichner erwartet."` | `"An identifier must follow '?'."` |
| `Parser_E05_SemiOrComma` | — | `"Semikolon oder Komma fehlt."` | `"Semicolon or comma missing."` |
| `Parser_E09_PeriodExpected` | — | `"Punkt erwartet."` | `"Period expected."` |
| `Parser_E11_UndeclaredIdent` | — | `"Nicht deklarierter Bezeichner."` | `"Undeclared identifier."` |
| `Parser_E12_AssignToConst` | — | `"Zuweisung an Konstante oder Prozedur nicht erlaubt."` | `"Assignment to CONST or PROCEDURE not allowed."` |
| `Parser_E12_InputTargetMustBeVar` | — | `"Eingabeziel muss eine Variable sein."` | `"Input target must be a variable."` |
| `Parser_E13_AssignOpExpected` | — | `"Zuweisungsoperator ':=' erwartet."` | `"Assignment operator ':=' expected."` |
| `Parser_E14_CallNeedsIdent` | — | `"'call' muss von einem Bezeichner gefolgt werden."` | `"CALL must be followed by an identifier."` |
| `Parser_E15_CallConstOrVar` | — | `"Aufruf von Konstante oder Variable sinnlos."` | `"Calling a constant or variable is not meaningful."` |
| `Parser_E16_ThenExpected` | — | `"'then' erwartet."` | `"THEN expected."` |
| `Parser_E17_SemiOrEndExpected` | — | `"Semikolon oder 'end' erwartet."` | `"Semicolon or END expected."` |
| `Parser_E18_DoExpected` | — | `"'do' erwartet."` | `"DO expected."` |
| `Parser_E19_IncorrectSymbol` | — | `"Falsches Symbol nach Anweisung."` | `"Incorrect symbol after statement."` |
| `Parser_E19_InputNotInClassic` | — | `"Eingabe '?' im classic-Modus nicht verfügbar."` | `"Input '?' is not available in classic mode."` |
| `Parser_E19_OutputNotInClassic` | — | `"Ausgabe '!' im classic-Modus nicht verfügbar."` | `"Output '!' is not available in classic mode."` |
| `Parser_E20_RelOpExpected` | — | `"Vergleichsoperator erwartet."` | `"Relational operator expected."` |
| `Parser_E21_ProcInExpr` | — | `"Prozedurbezeichner in Ausdruck nicht erlaubt."` | `"Procedure identifier not allowed in expression."` |
| `Parser_E22_RightParenMissing` | — | `"Schließende Klammer fehlt."` | `"Closing parenthesis missing."` |
| `Parser_E24_BadExprStart` | — | `"Ausdruck kann nicht mit diesem Symbol beginnen."` | `"Expression cannot start with this symbol."` |
| `Parser_E30_NumberTooLarge` | — | `"Diese Zahl ist zu groß."` | `"This number is too large."` |
| `Parser_E32_NestingTooDeep` | — | `"Maximale Blockschachtelungstiefe überschritten."` | `"Maximum nesting level exceeded."` |
| `Parser_E34_SymbolTableOverflow` | `{0}` = Max | `"Symboltabelle voll (max {0})."` | `"Symbol table full (max {0})."` |
| `Parser_E35_ProgramTooLong` | `{0}` = Max | `"Programm zu lang (max {0} Befehle)."` | `"Program too long (max {0} instructions)."` |
| `Parser_E98_UnexpectedEndOfInput` | — | `"Programm unvollständig: unerwartetes Eingabeende."` | `"Program incomplete: unexpected end of input."` |
| `Parser_E99_InvalidLexLevel` | — | `"Ungültige lexikalische Ebenenreferenz."` | `"Invalid lexical level reference."` |
| `Parser_E99_UnexpectedTermination` | — | `"Unerwartetes Programmende."` | `"Unexpected end of program."` |

---

### 4. `Pl0VmMessages` — Ressourcenklasse (Pl0.Vm)

| Key | Platzhalter | Beispiel (de) | Beispiel (en) |
|-----|-------------|---------------|---------------|
| `Vm_E99_IPOutOfRange` | `{0}` = PC-Wert | `"Befehlszeiger außerhalb des Bereichs: {0}."` | `"Instruction pointer out of range: {0}."` |
| `Vm_E99_InvalidLodIndex` | `{0}` = Index | `"Ungültiger LOD-Zugriff bei Stack-Index {0}."` | `"Invalid LOD access at stack index {0}."` |
| `Vm_E99_InvalidStoIndex` | `{0}` = Index | `"Ungültiger STO-Zugriff bei Stack-Index {0}."` | `"Invalid STO access at stack index {0}."` |
| `Vm_E99_StackOverflowCallFrame` | — | `"Stack-Überlauf beim Erstellen eines Aufrufrahmens."` | `"Stack overflow while creating call frame."` |
| `Vm_E99_StackOverflowInt` | — | `"Stack-Überlauf bei INT."` | `"Stack overflow on INT."` |
| `Vm_E99_UnsupportedOpcode` | `{0}` = Opcode | `"Nicht unterstützter Opcode: {0}."` | `"Unsupported opcode: {0}."` |
| `Vm_E99_UnsupportedOpr` | `{0}` = OPR-Code | `"Nicht unterstützter OPR-Code: {0}."` | `"Unsupported OPR code: {0}."` |
| `Vm_E99_InvalidBasePointer` | `{0}` = Adresse | `"Ungültiger Basiszeiger beim Auflösen der Ebene: {0}."` | `"Invalid base pointer while resolving level: {0}."` |
| `Vm_E99_StackOverflow` | — | `"Stack-Überlauf."` | `"Stack overflow."` |
| `Vm_E99_StackUnderflow` | — | `"Stack-Unterlauf."` | `"Stack underflow."` |
| `Vm_E98_EndOfInput` | — | `"Eingabeende während des Lesens."` | `"End of input while reading."` |
| `Vm_E97_InputFormatError` | — | `"Eingabeformat ungültig."` | `"Invalid input format."` |
| `Vm_E206_DivisionByZero` | — | `"Division durch null."` | `"Division by zero."` |

---

### 5. `Pl0CliMessages` — Ressourcenklasse (Pl0.Cli)

| Key | Platzhalter | Beispiel (de) | Beispiel (en) |
|-----|-------------|---------------|---------------|
| `Cli_Err_UnexpectedPositional` | `{0}` = Argument | `"Unerwartetes Argument: '{0}'."` | `"Unexpected argument: '{0}'."` |
| `Cli_Err_MissingValueForOut` | — | `"Fehlender Wert für '--out'."` | `"Missing value for '--out'."` |
| `Cli_Err_NoEmitMode` | — | `"Kein Ausgabemodus angegeben. Nutze '--emit asm' oder '--emit cod'."` | `"No emit mode specified. Use '--emit asm' or '--emit cod'."` |
| `Cli_Err_UnknownSwitch` | `{0}` = Switch | `"Unbekannter Schalter: '{0}'."` | `"Unknown switch: '{0}'."` |
| `Cli_Err_ConflictingEmitModes` | — | `"Widersprüchliche Ausgabemodi. Nur einen von 'asm' oder 'cod' angeben."` | `"Conflicting emit modes. Specify only one of 'asm' or 'cod'."` |
| `Cli_Err_UnknownLanguage` | `{0}` = Code | `"Unbekannter Sprachcode '{0}', verwende Fallback 'de'."` | `"Unknown language code '{0}', using fallback 'de'."` |
| `Cli_Status_CompileSuccess` | — | `"Kompilierung erfolgreich."` | `"Compilation successful."` |
| `Cli_Status_CompileError` | — | `"Kompilierung fehlgeschlagen."` | `"Compilation failed."` |
| `Cli_Help_UsageHeader` | — | `"Verwendung:"` | `"Usage:"` |
| `Cli_Help_HelpLine` | `{0}` = Programmname | `"{0} [-\|/]h \| [-\|/]? \| --help"` | `"{0} [-\|/]h \| [-\|/]? \| --help"` |
| `Cli_Help_CompileLine` | `{0}` = Programmname | `"{0} compile <datei.pl0> [--out <datei.pcode>] [--emit asm\|cod] [--list-code]"` | `"{0} compile <file.pl0> [--out <file.pcode>] [--emit asm\|cod] [--list-code]"` |
| `Cli_Help_RunLine` | `{0}` = Programmname | `"{0} run <datei.pl0> [--emit asm\|cod] [--list-code]"` | `"{0} run <file.pl0> [--emit asm\|cod] [--list-code]"` |
| `Cli_Help_RunPcodeLine` | `{0}` = Programmname | `"{0} run-pcode <datei.pcode> [--list-code]"` | `"{0} run-pcode <file.pcode> [--list-code]"` |
| `Cli_Help_RunPcodeDirectLine` | `{0}` = Programmname | `"{0} <datei.pcode> \| <datei.cod> [P-Code direkt ausführen]"` | `"{0} <file.pcode> \| <file.cod> [run P-Code directly]"` |
| `Cli_Help_ApiLine` | `{0}` = Programmname | `"{0} --api"` | `"{0} --api"` |
| `Cli_Help_LegacyLine` | `{0}` = Programmname | `"{0} <datei.pl0> [Legacy-Modus; kompilieren und ausführen]"` | `"{0} <file.pl0> [legacy mode; compile and run]"` |
| `Cli_Help_SwitchesHeader` | — | `"Schalter:"` | `"Switches:"` |
| `Cli_Help_SwitchErrmsg` | — | `"  [-\|/]errmsg \| --errmsg                lange Compiler-Fehlermeldungen anzeigen"` | `"  [-\|/]errmsg \| --errmsg                show long compiler error messages"` |
| `Cli_Help_SwitchWopcod` | — | `"  [-\|/]wopcod \| --wopcod                numerische Opcodes in Listing-Ausgabe"` | `"  [-\|/]wopcod \| --wopcod                numeric opcodes in listing output"` |
| `Cli_Help_SwitchListCode` | — | `"  --list-code                            erzeugten/geladenen Code-Listing ausgeben"` | `"  --list-code                            output generated/loaded code listing"` |
| `Cli_Help_SwitchApi` | — | `"  --api                                  Webserver zur Dokumentation starten"` | `"  --api                                  start documentation web server"` |
| `Cli_Help_SwitchConly` | — | `"  [-\|/]conly \| --conly \| --compile-only  nur kompilieren, VM nicht starten"` | `"  [-\|/]conly \| --conly \| --compile-only  compile only, do not run VM"` |
| `Cli_Help_SwitchEmitFormat` | — | `"  [-\|/]emit {[-\|/]asm \| [-\|/]cod}"` | `"  [-\|/]emit {[-\|/]asm \| [-\|/]cod}"` |
| `Cli_Help_SwitchEmit` | — | `"  --emit asm\|cod                         PL/0-Code auf STDOUT ausgeben"` | `"  --emit asm\|cod                         output PL/0 code to STDOUT"` |
| `Cli_Help_SwitchOut` | — | `"  --out <datei>                           Ausgabepfad für 'compile'-Befehl"` | `"  --out <file>                            output path for 'compile' command"` |
| `Cli_Help_SwitchLang` | — | `"  --lang <code>                           Ausgabesprache setzen (z. B. de, en)"` | `"  --lang <code>                           set output language (e.g. de, en)"` |

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
