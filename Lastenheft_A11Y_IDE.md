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
