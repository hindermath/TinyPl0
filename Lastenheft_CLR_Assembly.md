# Lastenheft CLR Assembly

# Implementierungsphase: Code-Generierung (`--clr`)
* Integration eines neuen Backends, das statt des PL/0-Zwischencodes direkt CIL (Common Intermediate Language) erzeugt.
* Nutzung von `System.Reflection.Emit` zur Erstellung einer Assembly entweder im Speicher oder als Assenbly-Datei mit der Dateiendung`.dll`.

| Parameter/Switch | Beschreibung                                                                                                                 | 
|:----------------:|------------------------------------------------------------------------------------------------------------------------------|
|   --clr=inmem    | Erzeugt eine ausführbare .Net Assembly im Speicher und führt diese aus.                                                      |
|   `--clr=dll`    | Erzeugt eine ausführbare .Net Assembly-Datei mit der Datei-Endung '.dll', die mit dem Befehl 'dotnet' ausgeführt werden kann |

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
/speckit-specify Nutze Lastenheft_CLR_Assembly.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen CLR-Backend-Entscheidungslauf im Repository TinyPl0.

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
