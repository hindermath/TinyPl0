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
| `Parser_E09_PeriodExpected` | — | `"Punkt erwartet."` |
| `Parser_E11_UndeclaredIdent` | — | `"Nicht deklarierter Bezeichner."` |
| `Parser_E12_AssignToConst` | — | `"Zuweisung an Konstante oder Prozedur nicht erlaubt."` |
| `Parser_E12_InputTargetMustBeVar` | — | `"Eingabeziel muss eine Variable sein."` |
| `Parser_E14_CallNeedsIdent` | — | `"'call' muss von einem Bezeichner gefolgt werden."` |
| `Parser_E15_CallConstOrVar` | — | `"Aufruf von Konstante oder Variable sinnlos."` |
| `Parser_E16_ThenExpected` | — | `"'then' erwartet."` |
| `Parser_E17_SemiOrEndExpected` | — | `"Semikolon oder 'end' erwartet."` |
| `Parser_E18_DoExpected` | — | `"'do' erwartet."` |
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
| *(weitere Parser-Expect-Meldungen)* | … | … |

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
| `Cli_Help_Usage` | — | `"Verwendung: ..."` |
| *(weitere Hilfe-Texte)* | … | … |

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

### Beziehungsübersicht

```
Pl0.Cli (CliOptionsParser)
  ├── parst --lang → string Language
  ├── erstellt CompilerOptions { Language }  →  Pl0.Core (Pl0Lexer, Pl0Parser)
  │                                               └── Pl0CoreMessages[Language]
  └── erstellt VirtualMachineOptions { Language } → Pl0.Vm (VirtualMachine)
                                                      └── Pl0VmMessages[Language]
```
