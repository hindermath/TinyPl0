<!-- intake-authoring:begin -->
# Lastenheft: Barrierefreiheit der TinyPl0 IDE (A11Y)

**Dokument-Status:** Entwurf
**Erstellt:** 2026-03-31
**Empfohlener Durchführungszeitraum:** Nach Abschluss der laufenden IDE-Vollständigkeitsarbeiten
(alle `PF-IDE-*` Anforderungen grün); parallel zu oder direkt nach dem nächsten Dokumentations-PR.
**Grundlage:** `docs/tui-a11y-assessment.md` im RiderProjects-Workspace

---

## Ausgangslage

Die TinyPl0 IDE nutzt **Terminal.Gui 2.0.0** (instanzbasiertes Lifecycle-Modell).
Sie ist vollständig per Tastatur navigierbar. Screen-Reader (NVDA, VoiceOver, Orca)
können den sichtbaren Terminal-Text lesen, erkennen aber keine semantischen UI-Rollen
(Button, Dialog, Menü). Keyboard-Shortcuts sind in Menü-Einträgen sichtbar, fehlen
jedoch als persistente Legende im Sichtbereich während der Arbeit.

Weiterhin gibt es Feedback, das ausschließlich visuell (Farbe, Fokus-Highlight) ohne
begleitenden Text vermittelt wird. Das verstößt gegen das Projektprinzip
`Programmierung #include<everyone>`.

---

## Anforderungen

### R-A11Y-IDE-01: StatusBar mit vollständiger Shortcut-Legende

Die IDE muss eine `Terminal.Gui StatusBar`-Leiste mit den wichtigsten
Tastenkombinationen im unteren Bildschirmbereich dauerhaft anzeigen.
Die Leiste muss kontextabhängig aktualisiert werden (Editor-Fokus,
P-Code-Fenster-Fokus, Debug-Modus).

Mindestinhalt in jeder Ansicht:

| Kontext | Mindest-Shortcuts |
|---------|------------------|
| Editor aktiv | F1 Hilfe, Strg+S Speichern, Strg+O Öffnen, F5 Kompilieren, F9 Ausführen |
| Kompilierung aktiv | Esc Abbrechen |
| Debug-Modus | F7 Schritt, F8 Weiter, F4 Haltepunkt, Esc Beenden |
| P-Code-Ansicht | Strg+W Schließen |

### R-A11Y-IDE-02: Text-Bestätigung nach jeder Aktion

Nach jeder benutzerinitierten Aktion (Speichern, Öffnen, Kompilieren, Ausführen,
Konfigurationsänderung) muss eine Statusmeldung in der Statuszeile oder im
Ausgabefenster als **sichtbarer Text** erscheinen — zusätzlich zu jedem Dialog.
Screen-Reader lesen sichtbaren Text; reine Farb- oder Fokusänderungen reichen nicht.

Beispiele:
- Speichern → `„Datei gespeichert: <Dateiname.pl0>"`
- Kompilierung erfolgreich → `„Kompilierung erfolgreich. <N> Byte P-Code."`
- Kompilierung mit Fehlern → `„Fehler: <Meldung> in Zeile <N>"`

### R-A11Y-IDE-03: Fehlermeldungen auch als Text im Ausgabefenster

Compiler-Fehlermeldungen und Laufzeitfehler müssen immer sowohl als
`MessageBox.ErrorQuery` (Dialog) **als auch** als Textzeile im Ausgabefenster
erscheinen. Ein Nutzer, der Dialoge per Screen-Reader nicht vollständig erfasst,
kann so die Fehler nachlesen.

### R-A11Y-IDE-04: Farbkontrast und High-Contrast-Modus

Alle genutzten `ColorScheme`-Definitionen in Terminal.Gui müssen einen
Mindest-Kontrast von **4,5:1** (WCAG 2.2 AA, Normaltext) einhalten.
Ein optionaler High-Contrast-Modus (z. B. schwarzer Hintergrund, weißer Text,
gelbe Akzente) soll als konfigurierbare Option eingeführt werden.
Konfiguration: über den bestehenden Einstellungsdialog (`PF-IDE-013` ff.).

### R-A11Y-IDE-05: Tastaturnavigation ohne Maus vollständig möglich

Die gesamte IDE muss ohne Mausinteraktion nutzbar sein.
Alle Dialoge, Menüs, Fenster und Eingabefelder müssen per Tab/Shift+Tab,
Pfeiltasten und Enter vollständig erreichbar und bedienbar sein.
Dies ist zu verifizieren: kein Steuerelement darf den Fokus via Tab
nicht erreichen (außer bewusst tabstopp-freien Statuselementen).

### R-A11Y-IDE-06: A11Y-Tests für Dokumentations-HTML (Playwright+axe)

Die mit `docfx` generierte Dokumentation der TinyPl0 IDE muss durch
automatisierte Playwright-Tests mit `@axe-core/playwright` auf
WCAG 2.2 AA-Konformität geprüft werden — analog zu TuiVision (`tests/web-a11y/`).
Zusätzlich: Sichtbarkeitscheck mit `lynx` als Text-Browser-Validierung.
Diese Tests sind in die CI/CD-Pipeline (`ci.yml`) zu integrieren.

### R-A11Y-IDE-07: Prozessbasierte Keyboard-Integrationstests (optional, mittelfristig)

Als ergänzender Ansatz (kein Pflichtziel dieses Lastenhefte-Zyklus, aber
vorzusehen) sollen Integrationstests entwickelt werden, die die IDE als
Prozess starten, Tastatureingaben über stdin simulieren und die stdout-Ausgabe
auf erwartete Textmuster prüfen. Diese Tests verifizieren die Keyboard-Navigation
maschinenlesbar ohne einen Screen-Reader zu benötigen.

---

## Nicht im Scope

- Vollständige Screen-Reader-Semantik (erfordert UI-Automation-Integration,
  die Terminal.Gui nicht anbietet)
- Sprachausgabe direkt aus der IDE
- Mausunterstützung als primärer Eingabekanal
- Änderungen am Compiler-Kern oder VM

---

## Akzeptanzkriterien

| ID | Kriterium |
|----|-----------|
| AK-A11Y-IDE-01 | StatusBar zeigt kontextabhängige Shortcut-Legende in allen Hauptansichten |
| AK-A11Y-IDE-02 | Jede Aktion (Speichern, Kompilieren, Ausführen) erzeugt sichtbaren Statustext |
| AK-A11Y-IDE-03 | Fehlermeldungen erscheinen sowohl im Dialog als auch im Ausgabefenster |
| AK-A11Y-IDE-04 | Alle Farbschemata erreichen WCAG 2.2 AA Kontrast (4,5:1) |
| AK-A11Y-IDE-05 | Vollständige Tab-Navigation durch alle Steuerelemente per Keyboard-Test verifiziert |
| AK-A11Y-IDE-06 | Playwright+axe-Tests für DocFX-HTML liefern keine serious/critical Violations |
| AK-A11Y-IDE-07 | lynx kann die generierte Dokumentationsstartseite vollständig als Text darstellen |

---

## Beispiel: Agentic-AI-Dialog (Platzhalter für spätere Durchführung)

Dieser Abschnitt wird während der Umsetzung dieses Lastenhefte mit Agentic-AI
plus Spec-Kit/SDD befüllt — analog zum didaktischen Dialog in `Lastenheft_IDE.md`.
Jeder Umsetzungsschritt wird mit Commit-URL und Zeitstempel dokumentiert.

---

## Hinweis für Lernende

**Deutsch:** Barrierefreiheit in Terminal-Anwendungen bedeutet vor allem:
Alle Informationen als lesbaren Text ausgeben, Tastaturnavigation lückenlos
sicherstellen und Dokumentation maschinenlesbar halten. Das ist nicht schwer —
es muss nur von Anfang an mitgedacht werden.

**English:** Accessibility in terminal applications primarily means: output all
information as readable text, ensure complete keyboard navigation, and keep
documentation machine-readable. It is not hard — it just needs to be part of
the design from the beginning.

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
Ersetzter Alt-Prompt: speckit-specify Nutze Lastenheft_A11Y_IDE.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen Barrierefreiheits- und Nutzbarkeitslauf im Repository TinyPl0.

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
$speckit-specify Use Lastenheft_A11Y_IDE.md as the binding intake. Preserve its scope, non-goals, ordering, governance, evidence, and acceptance criteria. Create or update only the matching feature specification. Do not implement, commit, push, create a pull request, merge, or start another feature.
```

### Autonomous

<!-- spec-kit-command-id: speckit.autonomous -->
```text
$speckit-autonomous Execute one complete autonomous Spec Kit run using Lastenheft_A11Y_IDE.md as the binding intake. Delivery mode: LocalImplementation. Preserve all scope, ordering, security, accessibility, evidence, and acceptance boundaries. Do not push, create or merge a pull request, use bypass authority, expose secrets, or start a follow-up feature.
```
<!-- intake-authoring:end -->
