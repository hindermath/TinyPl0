# VM-Härtungsvertrag / VM Hardening Contract

## Öffentliche Option / Public Option

`VirtualMachineOptions` erhält genau einen neuen öffentlichen Parameter:

```csharp
int InstructionBudget = 1_000_000
```

Der Parameter wird hinter den vier bestehenden Positionsparametern angehängt.
Damit bleiben vorhandene Positions- und benannte Aufrufe quellkompatibel. Ein
API-Kompatibilitätstest kompiliert und verwendet den bisherigen Vier-Parameter-
Aufruf zusätzlich zu benannten neuen Aufrufen. Neue oder geänderte XML-
Dokumentation steht Deutsch zuerst und Englisch danach, nennt die positive
Wertbedingung und erklärt, dass das Budget Instruktionen und keine Zeit misst.

## Gültige Konfiguration / Valid Configuration

Eine Konfiguration ist genau dann ausführbar, wenn:

```text
3 <= StackSize <= 1_000_000 AND InstructionBudget > 0
```

Die Untergrenze drei folgt aus Static Link, Dynamic Link und Return Address des
Start-Aktivierungsrahmens. Die Prüfung findet vor Arrayallokation,
Registerinitialisierung und Instruktionsausführung statt.

## Ungültige Konfiguration / Invalid Configuration

- `InstructionBudget <= 0`: terminale, lokalisierte Budgetoptionsdiagnose.
- `StackSize < 3`: terminale, lokalisierte Stackoptionsdiagnose.
- `StackSize > 1_000_000`: dieselbe stabile Stackoptionsdiagnose vor Addition
  oder Allokation; dadurch können weder `StackSize + 1` überlaufen noch
  unbegrenzt große VM-Stacks angefordert werden.
- Sind beide ungültig, ist die Diagnose-Reihenfolge stabil und dokumentiert;
  es findet keine Allokation oder Ausführung statt.
- Es wird keine `ArgumentOutOfRangeException`,
  `IndexOutOfRangeException`, `OverflowException`, Stack-Trace oder interne
  Ressourcenkennung an Nutzerinnen oder Nutzer weitergegeben.

Batch-VM gibt ein erfolgloses `VmExecutionResult` zurück. Die Step-VM liefert
nach `Initialize`/erstem beobachtbaren Resultat einen terminalen Error-State;
weitere `Step()`-Aufrufe führen nichts aus und duplizieren die Diagnose nicht.

## Instruktionszählung / Instruction Counting

Für Budget `N` gilt:

1. Der Zähler beginnt bei `0`.
2. Pointer und Konfiguration werden geprüft.
3. Falls `executedInstructions == N`, entsteht die Budgetdiagnose; die nächste
   Instruktion wird weder geladen noch ausgeführt.
4. Andernfalls wird genau eine Instruktion zur Ausführung ausgewählt und der
   Zähler genau einmal erhöht.
5. Eine haltende Instruktion zählt wie jede andere ausgeführte Instruktion.

Damit führt jeder Weg höchstens `N` Instruktionen aus und meldet vor `N+1`.
`Step()` verbraucht pro erfolgreich ausgeführtem Schritt eine Einheit. Aufrufe
nach Halt oder Fehler verbrauchen keine Einheit.

## Diagnosegleichheit / Diagnostic Equivalence

Batch und Step müssen für Budgetüberschreitung und Optionsvalidierung besitzen:

- denselben numerischen Diagnosecode;
- dieselbe semantische Ursache;
- denselben DE-/EN-Ressourcenschlüssel oder eine nachweislich gemeinsame
  Formatierungsfunktion;
- denselben terminalen Erfolgsstatus (`Success == false` beziehungsweise
  `VmStepStatus.Error`).

Der genaue neue Code wird vor Implementierung gegen den bestehenden
Diagnosekatalog auf Kollision geprüft und im Test als benannte Konstante oder
fachliche Assertion gebunden. Code `99` darf nur verwendet werden, wenn die
Review-Evidence belegt, dass ein separater stabiler Code keinen Lern- oder
Automationsnutzen bringt; andernfalls wird ein neuer kollisionsfreier Code
gewählt.

## TDD-Abnahme / TDD Acceptance

| Fall / Case | Erwartung / Expected result |
|---|---|
| Default options | `InstructionBudget == 1_000_000`, Stack `500` |
| Endlosschleife, `N=2` | genau zwei Ausführungen; Fehler vor dritter |
| haltendes Programm mit exakt `N` Instruktionen | erfolgreich |
| haltendes Programm benötigt `N+1` | Budgetfehler, keine `N+1`-Nebenwirkung |
| Budget `0`, `-1` | Diagnose vor Allokation/Ausführung |
| Stack `0`, `1`, `2`, negativ | Diagnose vor Allokation/Indexzugriff |
| Stack `1_000_001`, `int.MaxValue` | Diagnose vor Addition/Allokation |
| Batch versus Step | gleiche Grenze, Code und Sprachsemantik |
| bestehender Vier-Parameter-Aufruf | weiterhin quellkompatibel |
| DE versus EN | verständliche, parameterfreie oder sicher formatierte Texte |
| weiterer Step nach Budgetfehler | stabiler Error-State, keine Ausführung |

Rot und Grün verwenden dieselbe Testquelle. Ein äußeres Timeout schützt nur den
roten Testlauf und ist kein akzeptierter Produktmechanismus.

## Regression und Nicht-Ziele / Regression and Non-goals

- Keine neue CLI- oder IDE-Einstellung für das Budget in diesem Feature.
- Keine Änderung an Opcode-, OPR-, Parser-, Dialekt- oder P-Code-Semantik.
- Keine Änderung an 41 Pflichtfällen oder Golden-Artefakten.
- Keine vollständige Zusammenlegung der beiden VM-Implementierungen.
- Keine wall-clock-, CPU-, Speicher- oder Betriebssystem-Isolationsgarantie.

Abnahme verlangt die beiden VM-Testklassen, L10N-Tests, vollständige Suite,
Golden-/Traceability-Prüfung, Coverage und DocFX/A11Y wegen der öffentlichen
Option.
