# Ausführliche Dokumentation

## Zu verwendendes Programm
Für die Erstellung der Dokumentation soll docfx verwendet werden

## Anforderungen
- Es soll eine docfx.json Datei erstellt werden
- Es soll eine Dokumentation mit Hilfe von docfx erstellt werden, die folgendes beinhaltet
  - Übersicht über das vorliegende Projekt TinyPl0 und wie alle Teile des Projekts und dessen Funktionen zusammenhängen und mit einander arbeiten. Es soll ein mermaid-Diagramm erstellt werden, dass zum einen gespeichert wird und zum anderen in der HTML-Dokumentation angezeigt werden soll. Es soll dabei Direkt im Browser via Mermaid (CDN) eingebettet werden.
  - Beschreibung der Funktionen
    - aus den XML-Kommentaren im Quellcode 
    - dazu sollen alle Funktionen, Methoden, Klassen und Attribute mit XML-Kommentaren dokumentiert werden.
    - Ausführliche Beschreibung der 3 Projekte
      - Pl0.CLI
      - Pl0.Core
      - Pl0.Vm
    - Ausführliche Beschreibung der Tests
      - Pl0.Tests
  - Anleitung zur Installation und Nutzung
  - Informationen über die Lizenzierung
    - In dieREADME.md folgendes aufnehmen: Lizenz: MIT – siehe LICENSE.
    - Falls noch nicht vorhanden inder README.md einen Hinweis-Absatz, dass dieses Projekt zu Ausbildungszwecken dient für Auszubildende der Fachinformatik.
  - Kontaktinformationen für Support und Feedback
    - Issue in diesem github Repository erstellen
    - Aber darauf hinweisen, dass es ein Projekt für Auszubildende der Fachinformatik ist. Es soll ein Anreiz der Auszubildenden sein, sich mit dem Projekt zu ubeschäftigen und eigentlich versuchen sollen, Anforderungen selbst oder mit Hilfe Ihrer Ausbildenden zu lösen. 
  - Inhalte aus dem Ordner Docs sollen auch mit in die Dokumentation aufgenommen werden
  - Aus dem Projekt selbst sollen die Arbeitsweise der Sprache PL0 ausführlich als Handbuch erstellt werden mit ausführlichen Code-Beispielen zu allen Elementen und Funktionen der Sprache.
  - Im Anhang sollen weitere Code-Beispiele zu PL0 enthalten sein.
    - Fibonacci
    - KGT, GGT
    - Verschiedenste einfache mathematische Funktionen
      - sin, cos, tan, Kreisberechnung etc.
  - Aus dem Projekt selbst sollen die Arbeitsweise des P-Codes der Sprache PL0 ausführlich als Handbuch erstellt werden mit ausführlichen P-Code-Beispielen zu allen Elementen des P-Code.
    - es soll auch ein Tutorial erstellt werden, wie Auszubildende sich in die direkte P-Code-Programmierung einarbeiten können.
  - 
## Ausführung
- Es soll ein Pflichtenheft in Markdown erstellt werden
  - geeigneten Dateinamen für die Datei erstellen
  - als Markdown-Datei speichern
  - Die Umsetzung in Phasen oder Abschnitten aufteilen

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
/speckit-specify Nutze Lastenheft_PL0_Dokumentation.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen Dokumentations- und DocFX-Lauf im Repository TinyPl0.

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
