# Lastenheft: Didaktische Quellcode- und Inline-Kommentar-Haertung

**Dokument-Status:** Spec-Kit-Eingabedatei, bereit fuer `/speckit-specify`
**Aktualisiert:** 2026-06-05
**Betrifft:** `src/Pl0.Core/`, `src/Pl0.Vm/`, `src/Pl0.Cli/`, `src/Pl0.Ide/`, relevante Test-Helfer in `tests/` und betroffene Evidence-/Guide-Oberflaechen.

## 1. Ziel / Goal

Deutsch:
TinyPl0 ist ein didaktischer PL/0-Compiler und eine VM fuer Auszubildende. XML-Kommentare bleiben die primaere API- und DocFX-Erklaerung. Dieses Lastenheft ergaenzt gezielt Code-nahe Kommentare dort, wo Lernende oder Maintainer sonst nur sehen, dass ein Compiler-, VM-, IDE- oder Testablauf funktioniert, aber nicht warum er so gebaut wurde.

English:
TinyPl0 is a didactic PL/0 compiler and VM for trainees. XML comments remain the primary API and DocFX explanation. This requirements document adds focused code-near comments where learners or maintainers would otherwise see that a compiler, VM, IDE, or test flow works, but not why it is built that way.

## 2. Scope

In Scope:
- Compiler-Pipeline: Lexer, Parser, Symboltabelle, Codegenerator, Diagnose-Sammlung;
- VM- und P-Code-Flows: Stack, Spruenge, historische Mnemonics, Fehlergrenzen;
- CLI-/IDE-Bruecken, wenn sie Lernpfade oder Debugging-Verhalten erklaeren;
- Test-Helfer, die rote/gruene TDD- oder Proof-Pfade fuer Auszubildende sichtbar machen;
- vorhandene Kommentare, die im geprueften Bereich veraltet, trivial oder irrefuehrend sind.

Out of Scope:
- keine Verhaltensaenderung am Runtime-Code;
- keine neue Compiler-, VM- oder IDE-Funktion;
- keine flaechenhafte Kommentierung jeder Methode;
- keine DocFX-Regeneration, solange nur `//`- oder `/* */`-Kommentare ohne XML-Kommentar- oder API-Aenderung betroffen sind.

## 3. Kommentar-Intensitaet

- Datei-/Modulkommentar: 1 bis 3 Zeilen, wenn eine fachlich gepflegte Datei noch keinen sinnvollen Einstieg hat.
- Block- oder Inline-Kommentar: 1 bis 3 Zeilen vor nicht-trivialer Logik.
- Mehrzeilig nur bei komplexen Compiler-/VM-/IDE-Flows, historischen Abweichungen, Sicherheits-/A11Y-Randbedingungen oder Test-Proof-Pfaden.
- Keine Kommentare, die nur Namen, Operatoren oder offensichtliche Zuweisungen wiederholen.
- German-first/English-second und CEFR-B2 fuer didaktische Erklaerbloecke; technische Lizenz-, Marker- oder Generatorzeilen bleiben unveraendert.

## 4. Review-Modell

Jede gepruefte Datei oder jeder Flow-Bereich erhaelt in der Feature-Evidence eine Entscheidung:

- `CommentAdequate`: vorhandene Kommentare reichen.
- `CommentNeeded`: nicht-triviale Logik braucht eine kurze didaktische Erklaerung.
- `NoCommentNeeded`: Code ist selbsterklaerend; ein Kommentar waere Rauschen.
- `UpdateExistingComment`: vorhandener Kommentar ist veraltet oder zu ungenau.
- `FollowUpHardening`: beim Review wurde ein echtes Code-, Test- oder Architekturproblem sichtbar, das nicht in diesen Kommentar-Lauf gehoert.

## 5. Akzeptanzkriterien

- Eine Feature-Evidence-Datei dokumentiert gepruefte Dateien oder Flow-Bereiche, Entscheidung, Kommentarbedarf, Aenderung und Follow-up-Grenzen.
- Kommentare erklaeren Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze.
- Triviale Kommentare werden nicht neu eingefuehrt.
- Veraltete Kommentare in geprueften Bereichen werden aktualisiert oder entfernt.
- Agent-Guidance haelt die Regel fuer kuenftige neue oder geaenderte nicht-triviale Logik fest.
- Wenn XML-Kommentare oder API-Signaturen beruehrt werden, gilt der normale DocFX-/A11Y-Nachweispfad.

## 6. Kopierbarer `/speckit-specify`-Prompt

```text
/speckit-specify Nutze Lastenheft_Quellcode_Doku.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen didaktischen Inline-Code-Kommentar-Hardening-Lauf in TinyPl0.

Ziel: Zentrale Compiler-, VM-, CLI-, IDE- und Test-Helfer-Flows muessen fuer Auszubildende und Maintainer besser nachvollziehbar werden. XML-Kommentare bleiben die primaere API-/DocFX-Erklaerung; dieser Lauf ergaenzt nur Code-nahe didaktische Kommentare bei nicht-trivialer Logik.

Wichtig:
- Keine Runtime-Verhaltensaenderung, keine neue Compiler-/VM-/IDE-Funktion und kein globales "jede Methode kommentieren".
- Kommentarintensitaet moderat halten: 1 bis 3 Zeilen fuer Datei-/Modulkommentare oder nicht-triviale Blocks; mehrzeilig nur bei komplexen Flows, historischen Abweichungen, Sicherheits-/A11Y-Randbedingungen oder Test-Proof-Pfaden.
- Kommentare muessen Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze erklaeren, nicht triviales Was.
- German-first/English-second und CEFR-B2 fuer didaktische Erklaerbloecke; technische Lizenz-, Marker- oder Generatorzeilen bleiben unveraendert.
- Review-Modell aufnehmen: `CommentAdequate`, `CommentNeeded`, `NoCommentNeeded`, `UpdateExistingComment`, `FollowUpHardening`.
- Mindestens pruefen: Lexer, Parser, Symboltabelle, Codegenerator, Diagnose-Sammlung, VM-Stack und Spruenge, historische Mnemonic-Abweichungen, CLI-/IDE-Bruecken und TDD-/Proof-Test-Helfer.
- Feature-Evidence anlegen, die gepruefte Dateien oder Flow-Bereiche, Entscheidung, Kommentarbedarf, Aenderung und Follow-up-Grenzen dokumentiert.
- Wenn XML-Kommentare oder API-Signaturen beruehrt werden, gilt der normale DocFX-/A11Y-Nachweispfad; reine `//`- oder `/* */`-Kommentarhaertung loest keinen DocFX-Zwang aus.
```

---

## Spec-Kit-Intake-Reife / Spec Kit Intake Readiness

Dieses Lastenheft enthaelt bereits einen kopierbaren `/speckit-specify`-Prompt. Vor dem Start muss der aktuelle Repository-Stand trotzdem geprueft werden. Bereits erledigte oder branch-suffig archivierte Punkte werden nicht erneut umgesetzt; offene Punkte werden als `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp` klassifiziert.

*This requirements document already contains a copyable `/speckit-specify` prompt. Before starting, still check the current repository state. Completed or branch-suffixed archived items are not implemented again; open items are classified as `Applicable`, `AlreadySatisfied`, `N/A`, `Open`, or `FollowUp`.*
