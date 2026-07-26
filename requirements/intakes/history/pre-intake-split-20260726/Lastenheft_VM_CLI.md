<!-- intake-authoring:begin -->
# Lastenheft VM CLI

## 1. Einleitung
- Dieses Dokument beschreibt die Funktionalitäten und Anforderungen für die VM CLI (Virtual Machine Command Line Interface) des Projekts.

## 2. CLI Parameter

## 2.CLI Parameter
- Die VM CLI unterstützt verschiedene Parameter, um die Interaktion mit den virtuellen Maschinen zu steuern.
- es gibt die folgenden Parameter
  - Hilfe mit `--help` und `-h`
  - P-Assembler-Datei mit `-a` oder `--pasm`
  - P-Code-Datei mit `-c` oder `--pcode`
  - 
## 2.2 Optionale Parameter
- Die  VM CLI unterstützt die folgenden optionalen Parameter 
  - Debug-Modus mit `-s` oder `--step`
  - Verbose-Ausgabe mit `-v` oder `--verbose`
  - Stack-Size mit `-t` oder `--stack`
  - Sprache mit `-l` oder `--language`
  
## 3. Funktionalitäten
- Die VM CLI unterstützt verschiedene Funktionalitäten, um virtuelle Maschinen zu steuern.
  - Ausführen von P-Assembler-Dateien
  - Ausführen von P-Code-Dateien
  - Debugging mit Schritt-für-Schritt-Ausführung
  - Verbose-Ausgabe für Debugging
  - Stack-Size-Einstellung für virtuelle Maschinen
  - Spracheinstellung für virtuelle Maschinen
  - Hilfe mit `-h` oder `--help`
  
## 4. Projektstruktur
- Ein eigenständiges Projekt unter src/ mit dem Projekt-Namen Pl0.Vm.Cli
- Abhängigkeiten von Pl0.Vm und dessen Abhängigkeiten

---

## Spec-Kit-Intake-Reife / Spec Kit Intake Readiness

Dieses Lastenheft ist als Eingabedatei fuer einen spaeteren `/speckit-specify`-Lauf vorgesehen. Vor dem Start muss der aktuelle Repository-Stand geprueft werden, damit bereits erledigte oder ueberholte Punkte nicht erneut umgesetzt werden.

*This requirements document is intended as input for a later `/speckit-specify` run. Before starting, check the current repository state so already completed or superseded items are not implemented again.*

Der spaetere Lauf muss mindestens klassifizieren:

- `Applicable`: gilt fuer diesen Lauf und braucht Umsetzung oder Evidenz.
- `AlreadySatisfied`: ist im aktuellen Stand bereits nachweisbar erledigt.
- `N/A`: gilt fuer diesen Lauf nicht und braucht eine kurze Begruendung.
- `Open`: gilt, ist aber noch nicht ausreichend geklaert oder belegt.
- `FollowUp`: fachlich relevant, aber nicht Teil dieses Laufs.

## Kopierbarer `/speckit-specify`-Prompt / Copyable `/speckit-specify` Prompt

```text
Ersetzter Alt-Prompt: speckit-specify Nutze Lastenheft_VM_CLI.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen VM-CLI-Lauf im Repository TinyPl0.

Ziel: Pruefe das Lastenheft gegen den aktuellen Repository-Stand und erstelle eine belastbare Spec-Kit-Spezifikation, die fuer Auszubildende, Entwickler*innen, Reviewer und KI-Agenten nachvollziehbar ist.

Pflichtpunkte:
- Lies dieses Lastenheft vollstaendig und uebernehme vorhandene Anforderungen, Scope-Grenzen, Reihenfolgehinweise und Akzeptanzkriterien.
- Pruefe, welche Punkte bereits umgesetzt, ueberholt oder noch offen sind.
- Klassifiziere Anforderungen als `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp`.
- Plane nur `Applicable`-Punkte fuer diesen Lauf.
- Dokumentiere fuer `N/A` und `FollowUp` jeweils eine kurze Begruendung.
- Beachte `constitution.md`, `.specify/memory/constitution.md`, AGENTS/CLAUDE/GEMINI/Copilot-Guidance, installierte Spec-Kit-Presets, Secure-Development-Basis, A11Y-Regeln, CEFR-B2-Verstaendlichkeit und didaktische Kommentar-Governance.
- Starte keinen weiteren Lastenheft-Lauf und kombiniere mehrere Lastenhefte nur, wenn die Kopplung fachlich begruendet und dokumentiert ist.

Erzeuge eine Spezifikation mit Scope, Nicht-Zielen, Anforderungen, Abhaengigkeiten, Akzeptanzkriterien, Risiken, Teststrategie, Evidenzpfaden und offenen Folgepunkten.
```
<!-- intake-authoring:prompts -->
## Kopierbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

Die folgenden Alternativen starten keinen Lauf automatisch. Der autonome
Prompt ist auf `LocalImplementation` begrenzt und erteilt keine Remote-,
PR-, Merge-, Bypass-, Secret- oder Provider-Berechtigung.

*The alternatives below do not start a run automatically. The autonomous
prompt is limited to `LocalImplementation` and grants no remote,
pull-request, merge, bypass, secret, or provider authority.*

### Specify

<!-- spec-kit-command-id: speckit.specify -->
```text
$speckit-specify Use Lastenheft_VM_CLI.md as the binding intake. Preserve its scope, non-goals, ordering, governance, evidence, and acceptance criteria. Create or update only the matching feature specification. Do not implement, commit, push, create a pull request, merge, or start another feature.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run using Lastenheft_VM_CLI.md as the binding intake. Delivery mode: LocalImplementation. Preserve all scope, ordering, security, accessibility, evidence, and acceptance boundaries. Do not push, create or merge a pull request, use bypass authority, expose secrets, or start a follow-up feature.
```
<!-- intake-authoring:end -->
