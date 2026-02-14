# Pflichtenheft: Dokumentation TinyPl0 (DocFX)

## 1. Ziel

Ziel ist die Erstellung einer umfassenden, didaktisch ausgerichteten Dokumentation
fuer TinyPl0 mit DocFX (Template: `modern`). Die Dokumentation deckt Projektuebersicht,
Bedienung, PL0-Handbuch, P-Code-Handbuch, API-Referenz und Anhang mit Beispielprogrammen ab.

## 2. Ausgangssituation

Das Repository enthaelt Quellcode fuer Compiler und VM sowie bestehende technische
Dokumente im Ordner `docs/`. Die Dokumentation soll konsolidiert und fuer die Ausbildung
aufbereitet werden.

## 3. Projektabgrenzung

- Keine Aenderung des fachlichen Funktionsumfangs des Compilers/VM.
- Fokus auf Dokumentationsstruktur, Inhalte und DocFX-Konfiguration.

## 4. Anforderungen

### 4.1 Funktionale Anforderungen

1. DocFX-Konfiguration
   - Eine `docfx.json` wird im Repository angelegt.
   - Verwendung des Templates `modern`.

2. Inhaltsstruktur
   - Einstiegskapitel mit Ueberblick, Schnellstart, Installation und Support.
   - Architektur- und Projektuebersicht fuer Pl0.CLI, Pl0.Core, Pl0.Vm.
   - Bedienung der CLI inklusive Beispiele.
   - PL0-Handbuch mit Sprachelementen und Beispielen in drei Lernstufen (kurz, mittel, ausfuehrlich).
   - P-Code-Handbuch inkl. Instruktions-Referenz und Beispielen.
   - Tutorial zur direkten P-Code-Programmierung fuer Auszubildende.
   - Anhang mit separaten Seiten: Fibonacci, KGT, GGT, mathematische Funktionen (sin, cos, tan, Kreisberechnung etc.).

3. API-Referenz
   - Die API-Referenz wird aus XML-Kommentaren generiert.
   - Alle Funktionen, Methoden, Klassen und Attribute erhalten XML-Kommentare, auch interne/private.

4. Integration bestehender Dokumente
   - Inhalte aus `docs/` werden kuratiert und in die Gesamtdokumentation integriert.

5. Lizenz und Support
   - README enthaelt: "Lizenz: MIT - siehe LICENSE."
   - Support/Feedback ueber Issues im GitHub-Repository.

### 4.2 Nicht-funktionale Anforderungen

- Didaktische Aufbereitung mit klarer Struktur und praxisnahen Beispielen.
- Lesbarkeit und Nachvollziehbarkeit fuer Ausbildungszwecke.
- Konsistente Navigationsstruktur in DocFX.

## 5. Liefergegenstaende

- `docfx.json`
- DocFX-Inhaltsstruktur (Markdown, TOCs)
- Kuratierte Kapitel aus `docs/`
- Anhangsseiten mit Beispielprogrammen
- Aktualisiertes `README.md`

## 6. Abnahmekriterien

- DocFX laesst sich mit dem Template `modern` bauen.
- Alle geforderten Kapitel sind vorhanden und erreichbar.
- `docs/`-Inhalte sind integriert und kuratiert.
- README enthaelt die Lizenzzeile.
- PL0- und P-Code-Handbuch enthalten Beispiele auf drei Lernstufen bzw. Instruktionsbeispiele.

## 7. Risiken und Annahmen

- XML-Kommentare muessen ggf. im Code nachgezogen werden.
- Umfangreiche Beispielprogramme koennen iterative Erweiterung erfordern.

