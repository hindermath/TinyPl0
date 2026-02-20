# Pflichtenheft: TinyPl0 IDE (Terminal.Gui)

## 1. Ziel und Zweck
Ziel ist die Entwicklung einer textbasierten IDE fuer TinyPl0 als neues Projekt `src/Pl0.Ide`.
Die IDE soll sich an der Bedienlogik der Turbo-Pascal-DOS-IDE orientieren und den gesamten
Arbeitsablauf abdecken: Quellcode bearbeiten, kompilieren, P-Code anzeigen, ausfuehren und debuggen.

## 2. Ausgangssituation
TinyPl0 stellt bereits folgende Bausteine bereit:
- `Pl0.Core` fuer Lexer, Parser, Semantik und P-Code-Erzeugung
- `Pl0.Vm` fuer die Ausfuehrung von P-Code
- `Pl0.Cli` fuer Kommandozeilen-Bedienung

Es existiert bisher keine integrierte grafische (TUI-)Entwicklungsumgebung.

## 3. Projektabgrenzung
- Kein neuer Sprachumfang fuer PL/0.
- Keine Compiler-Optimierungen.
- Kein Ersatz der bestehenden CLI, sondern eine zusaetzliche Bedienoberflaeche.
- Fokus auf didaktisch nachvollziehbare Funktionen fuer Ausbildung und Demonstration.

## 4. Produktdefinition und Architektur

### 4.1 Zielsystem
- Neues .NET-10-Projekt: `src/Pl0.Ide`
- GUI-Bibliothek: `Terminal.Gui` in Version `2.x` (empfohlen fuer neue Projekte; instanzbasierte Architektur mit `IApplication`)
- Laufzeitplattform: Windows, macOS, Linux (Konsole/Terminal)

### 4.2 Modulabhaengigkeiten
- `Pl0.Ide -> Pl0.Core`
- `Pl0.Ide -> Pl0.Vm`
- `Pl0.Ide -> Terminal.Gui`

Die bestehenden Abhaengigkeitsregeln der vorhandenen Module bleiben unveraendert.

### 4.3 Hauptkomponenten der IDE
- Editorfenster fuer PL/0-Quelltext
- P-Code-Fenster fuer generierten Code
- Meldungsfenster fuer Compilerdiagnosen und Laufzeitmeldungen
- Ausgabefenster/Dialoge fuer Kompilier- und Laufzeitergebnisse
- Debug-Fenster fuer Register (`P`, `B`, `T`) und Stack-Inhalt
- Hilfe-/Dokumentationsfenster

## 5. Funktionale Anforderungen

### 5.1 Muss-Anforderungen

> **Hinweis zur Nummerierung:** Die `PF-IDE-*`-Kennungen sind nicht fortlaufend, da sie in der historischen Reihenfolge ihrer Erstellung vergeben wurden (siehe Dialogverlauf im Lastenheft). Die laufende Nummer (1–22) gibt die workflow-orientierte Lesereihenfolge an. Die Anforderungs-IDs bleiben stabil fuer Rueckverfolgbarkeit.

1. `PF-IDE-001` Projektstruktur:
   - Ein neues Projekt `src/Pl0.Ide` ist in der Solution eingebunden und baubar.
   - Die bestehenden `ArchitectureGuardTests` werden um die erlaubten Abhaengigkeiten von `Pl0.Ide` erweitert (`Pl0.Core`, `Pl0.Vm`, `Terminal.Gui`).

2. `PF-IDE-002` GUI-Basis:
   - Die Oberflaeche basiert auf NuGet-Paket `Terminal.Gui` `2.x`.
   - Der Application-Lifecycle folgt dem instanzbasierten v2-Muster (`Application.Create().Init()` / `app.Run<T>()` / `app.Dispose()`). Statische `Application`-Aufrufe aus v1 werden nicht verwendet.

3. `PF-IDE-003` Look-and-Feel:
   - Layout und Menuefuehrung orientieren sich an der Turbo-Pascal-IDE (DOS-Stil).
   - Als Basis dient das in `Terminal.Gui` v2 eingebaute Theme `TurboPascal 5` (aktivierbar ueber `ThemeManager`). Bei Bedarf kann das Theme angepasst oder erweitert werden.

4. `PF-IDE-004` Quellcodefenster:
   - Ein dediziertes Fenster zur Anzeige und Bearbeitung von PL/0-Quellcode ist vorhanden.

5. `PF-IDE-005` Syntax-Hervorhebung:
   - Schluesselwoerter von PL/0 werden im Editor farblich hervorgehoben.
   - Der Farbstil richtet sich nach den Konventionen von Turbo-Pascal und nutzt das v2-Scheme-/Theming-System (`Scheme`, `ThemeManager`).
   - Auch Zahlen, Operatoren werden entsprechend formatiert.

6. `PF-IDE-011` Dateioperationen:
    - Quellcode kann gespeichert und geladen werden.
    - Dazu werden die Standard-Dialoge von Terminal.GUI genutzt.

7. `PF-IDE-012` Quelltextformatierung:
    - Eine Funktion zur Formatierung des PL/0-Quellcodes ist vorhanden.
    - Mindestumfang der Formatierung:
      - Einrueckung gemaess Verschachtelungstiefe (`BEGIN`/`END`, `PROCEDURE`-Bloecke)
      - Normalisierung von Leerzeichen um Operatoren und nach Trennzeichen (`,`, `;`)
      - Konsistente Zeilenumbrueche nach Anweisungsenden (`;`, `.`)
    - Die Formatierung ist idempotent: mehrfaches Anwenden liefert dasselbe Ergebnis.

8. `PF-IDE-018` Compiler-Einstellungsdialog:
    - Die Compiler-Steuerung aus `src/Pl0.Core/CompilerOptions.cs` ist in der IDE ueber einen Einstellungsdialog konfigurierbar.
    - Konfigurierbar sind mindestens:
      - `Dialect`
      - `MaxLevel`
      - `MaxAddress`
      - `MaxIdentifierLength`
      - `MaxSymbolCount`
      - `MaxCodeLength`
      - `MaxNumberDigits` (abhaengig von `Dialect`, nicht frei editierbar)
    - Vorgeschlagene Wertebereiche fuer den Dialog:

      | Option                | Typ                   | Vorgeschlagener Bereich                | Default                                 |
      |-----------------------|-----------------------|----------------------------------------|-----------------------------------------|
      | `Dialect`             | Auswahl               | `Classic`, `Extended`                  | `Extended`                              |
      | `MaxLevel`            | Ganzzahl              | `0..10` (Schrittweite `1`)             | `3`                                     |
      | `MaxAddress`          | Ganzzahl              | `127..32767` (Schrittweite `1`)        | `2047`                                  |
      | `MaxIdentifierLength` | Ganzzahl              | `1..32` (Schrittweite `1`)             | `10`                                    |
      | `MaxNumberDigits`     | Ganzzahl (abgeleitet) | `10` oder `14` abhaengig von `Dialect` | `14` bei `Classic`, `10` bei `Extended` |
      | `MaxSymbolCount`      | Ganzzahl              | `10..5000` (Schrittweite `10`)         | `100`                                   |
      | `MaxCodeLength`       | Ganzzahl              | `50..10000` (Schrittweite `50`)        | `200`                                   |

    - Werte ausserhalb des Bereichs werden im Dialog validiert und nicht uebernommen.
    - Sonderregel `MaxNumberDigits`:
      - Bei `Dialect = Classic` wird `MaxNumberDigits = 14` gesetzt (historische Kompatibilitaet).
      - Bei `Dialect = Extended` wird `MaxNumberDigits = 10` gesetzt (technisch passend zu `int`).
    - Der Einstellungsdialog bietet eine Funktion zum Zuruecksetzen auf die Standardwerte (`CompilerOptions.Default`).
    - Geaenderte Werte werden fuer den naechsten Kompiliervorgang verwendet.

9. `PF-IDE-006` Kompilierung:
   - Quellcode kann direkt aus der IDE ueber `Pl0.Core` kompiliert werden.

10. `PF-IDE-007` Kompilierdialog:
    - Nach Kompilierung wird ein Dialog mit Ergebnisstatus angezeigt (Erfolg/Fehler).

11. `PF-IDE-008` Fehleranzeige:
    - Compilerdiagnosen werden gesammelt und in einem Meldungsfenster angezeigt.

12. `PF-IDE-009` P-Code-Fenster:
    - Der erzeugte P-Code wird in einem separaten Fenster dargestellt.

13. `PF-IDE-021` P-Code-Export (Emit-Modi):
    - Die IDE bietet eine Exportfunktion fuer den erzeugten P-Code.
    - Die Export-Modi orientieren sich fachlich an `src/Pl0.Cli/Cli/EmitMode.cs`:
      - `Asm` (mnemonische Darstellung)
      - `Cod` (numerischer Maschinen-Code)
    - Die Serialisierung nutzt `PCodeSerializer.ToAsm()` und `PCodeSerializer.ToCod()` aus `Pl0.Core`. Die Referenz auf `EmitMode.cs` und `CompilerCliOptions.cs` dient nur als fachliche Orientierung, nicht als Code-Abhaengigkeit — `Pl0.Ide` darf nicht von `Pl0.Cli` abhaengen (siehe 4.2).
    - Fuer `Cod` wird ausschliesslich die Dateiendung `.cod` verwendet.
    - Der Export ist nur nach erfolgreicher Kompilierung moeglich.

14. `PF-IDE-010` P-Code-Ausfuehrung:
    - Der erzeugte P-Code kann aus der IDE heraus ueber `Pl0.Vm` ausgefuehrt werden.
    - Laufzeitausgaben werden sichtbar dargestellt.
    - Die IDE stellt eine eigene `IPl0Io`-Implementierung bereit, die Laufzeiteingaben (`?`) ueber einen Eingabedialog erfasst und Ausgaben (`!`) im Ausgabefenster darstellt. Die bestehende konsolenbasierte `ConsolePl0Io` wird nicht verwendet.

15. `PF-IDE-013` Kombi-Aktion:
    - Eine Aktion "Kompilieren und Ausfuehren" ist vorhanden. Auch hier Orientierung an der Turbo Pascal IDE für DOS.

16. `PF-IDE-014` Debugging:
    - Schrittweise P-Code-Ausfuehrung (Step) ist moeglich. Hierbei wird der Instruktion-Pointer (`P`) aktualisiert und auch sichtbar im Inhalt des Stacks nachvollziehbar gezeigt, wo im P-Code der aktuelle Ausführungspunkt liegt.
    - Nach jedem Schritt werden Stack und Register (`P`, `B`, `T`) aktualisiert angezeigt.
    - Voraussetzung: Die VM in `Pl0.Vm` muss um eine Schritt-Ausfuehrungsschnittstelle erweitert werden (z. B. `Step()`-Methode oder `IVmObserver`-Callback). Die aktuelle `VirtualMachine.Run()` laeuft atomar bis zum Ende; fuer Schritt-Debugging muss der VM-Zustand (`P`, `B`, `T`, Stack) zwischen Einzelschritten zugaenglich sein. Diese Erweiterung ist als eigene Teilaufgabe zu planen.
    - Die IDE bietet eine Moeglichkeit, eine laufende P-Code-Ausfuehrung abzubrechen (z. B. ueber Tastatur-Shortcut oder Schaltflaeche), um auch bei Endlosschleifen bedienbar zu bleiben.
    - Nach einem Abbruch bleibt der letzte VM-Zustand (`P`, `B`, `T`, Stack) zur Analyse in der Debug-Ansicht sichtbar.

17. `PF-IDE-015` Hilfe:
    - Eine Hilfe-Funktion mit Bedienhinweisen der IDE ist in der IDE verfuegbar (z. B. Tastaturbelegung, Menuefunktionen, Kurzanleitung).
    - Eine Hilfe-Funktion fuer die Sprache PL/0 ist in der IDE verfuegbar (Kurzreferenz der Syntax und Schluesselwoerter).
    - Abgrenzung: `PF-IDE-015` beschreibt kompakte, in der IDE eingebettete Hilfetexte. Die ausfuehrliche Dokumentation mit Navigation und Suche wird durch `PF-IDE-016` abgedeckt.

18. `PF-IDE-016` Integrierte Dokumentation:
    - Die integrierte Dokumentation ist direkt in der IDE nutzbar, ohne externen Browser.
    - Aufruf ueber den Menuepunkt `Hilfe -> Integrierte Dokumentation`.
    - Die Ansicht besteht mindestens aus:
      - einer Kapitel-/TOC-Navigation,
      - einem Inhaltsbereich fuer Markdown-Inhalte,
      - einer lokalen Volltextsuche ueber die eingebundenen Dokumente.
    - Als Dokumentationsquellen werden lokale Repository-Dateien verwendet, insbesondere:
      - `docs/LANGUAGE_EBNF.md`
      - `docs/VM_INSTRUCTION_SET.md`
      - `docs/ARCHITECTURE.md`
      - `docs/TRACEABILITY_MATRIX.md`
    - Priorisierung der Dokuquellen (Anzeige und Navigation):
      1. Prioritaet hoch (Kernsprache und Semantik):
         - `docs/LANGUAGE_EBNF.md`
         - `docs/VM_INSTRUCTION_SET.md`
         - `docs/ARCHITECTURE.md`
      2. Prioritaet mittel (Qualitaet und Nachvollziehbarkeit):
         - `docs/TRACEABILITY_MATRIX.md`
         - `docs/QUALITY.md` (falls vorhanden)
      3. Prioritaet ergaenzend (Projekt- und Bedienkontext):
         - `README.md`
         - `Pflichtenheft_PL0_CSharp_DotNet10.md`
    - Startseite der integrierten Dokumentation ist standardmaessig `docs/LANGUAGE_EBNF.md`.
    - Fallback-Regel:
      - Wenn die Startseite oder eine hoeher priorisierte Datei fehlt, wird automatisch die naechste verfuegbare Datei gemaess Prioritaet geoeffnet.
      - Wenn keine Dokumentationsdatei verfuegbar ist, wird ein Hinweisdialog angezeigt.
    - Die Funktion ist offline nutzbar (kein Internet erforderlich).
    - Die IDE merkt sich die zuletzt geoeffnete Dokumentationsseite.
    - Kontexthilfe (z. B. ueber `F1`) oeffnet passende Dokumentationskapitel.
    - Bei fehlenden oder nicht lesbaren Dokumentationsdateien zeigt die IDE einen klaren Hinweisdialog und bleibt bedienbar.
    - Abgrenzung: `PF-IDE-016` beschreibt die interne IDE-Dokumentationsansicht; `PF-IDE-022` beschreibt den optionalen Webserver-Start fuer die Browseransicht.

19. `PF-IDE-022` Web-Hilfe ueber Kestrel (Hilfe-Menue):
    - Im Hilfe-Bereich der IDE gibt es einen Menuepunkt zum Starten/Stoppen der Web-Hilfe aus dem Ordner `_site`.
    - Die Umsetzung orientiert sich am vorhandenen `--api`-Verhalten in `src/Pl0.Cli`.
    - Der Menuepunkt arbeitet als Toggle:
      - erster Aufruf startet den Kestrel-Webserver,
      - naechster Aufruf stoppt den laufenden Kestrel-Webserver wieder.
    - Nach dem Start zeigt die IDE einen Hinweis-Dialog mit der konkreten URL, unter der die Hilfe erreichbar ist.
    - Der Server ist auf `localhost` gebunden; die angezeigte URL muss dem tatsaechlich verwendeten Endpoint entsprechen.
    - Technische Portvorgabe:
      - Standard: Start auf `http://localhost:5000`.
      - Fallback: Falls Port `5000` belegt ist, wird automatisch der naechste freie Port im Bereich `5001..5099` verwendet.
      - Falls kein Port im Fallback-Bereich verfuegbar ist, wird ein Fehlerdialog angezeigt und kein Webserver gestartet.
    - Voraussetzung: Der Ordner `_site` muss vor Nutzung der Web-Hilfe durch `docfx build` erzeugt worden sein. Er wird nicht im Repository versioniert.
    - Pfadauflösung: `_site` wird relativ zum Executable-Verzeichnis der IDE aufgeloest (analog zum bestehenden CLI-Verhalten), da die statischen Inhalte beim Build/Publish dorthin kopiert werden.
    - Falls `_site` nicht vorhanden ist oder der Start fehlschlaegt, zeigt die IDE eine klare Fehlermeldung im Dialog mit dem Hinweis, dass `docfx build` ausgefuehrt werden muss.
    - Kestrel-Konsolenausgaben duerfen die IDE-Ansicht nicht stoeren (keine UI-Glitches durch direkte Ausgabe auf das IDE-Terminal).

20. `PF-IDE-019` Tastatur-Shortcuts:
    - Hauptfunktionen der IDE sind ueber Tastatur-Shortcuts erreichbar.
    - Die Belegung orientiert sich an klassischen IDE-Mustern (z. B. Build, Run, Step).

21. `PF-IDE-020` Persistenz:
    - Die IDE speichert mindestens die zuletzt geoeffnete Datei und Fenstergroessen.
    - Die gespeicherten Werte werden beim naechsten Start wiederhergestellt.
    - Speicherort: Die Einstellungen werden als JSON-Datei im plattformspezifischen Benutzerverzeichnis abgelegt (z. B. `~/.config/TinyPl0/settings.json` unter Linux/macOS, `%APPDATA%\TinyPl0\settings.json` unter Windows).
    - Beim ersten Start oder bei fehlender/defekter Konfigurationsdatei startet die IDE mit Standardwerten.

22. `PF-IDE-017` Testfaelle:
    - Fuer die IDE-Funktionen werden automatisierte Tests erstellt (xUnit).
    - Die Tests decken mindestens die Kernablaeufe ab: Laden/Speichern, Kompilieren, Diagnostikanzeige,
      P-Code-Anzeige, Ausfuehren sowie Schritt-Debugging mit Register-/Stackaktualisierung.
    - Die Tests sind in `dotnet test` integriert und laufen reproduzierbar.

## 6. Nicht-funktionale Anforderungen
- `NF-001` Stabilitaet: Fehler im Quelltext duerfen nicht zum Absturz der IDE fuehren.
- `NF-002` Diagnostikmodell: Compilerfehler werden als Diagnosen angezeigt, nicht per ungefangener Exception.
- `NF-003` Bedienbarkeit: Hauptfunktionen sind vollstaendig per Tastatur erreichbar.
- `NF-004` Performance: Interaktive Reaktion bei Standardbeispielen ohne merkliche Verzoegerung.
- `NF-005` Wartbarkeit: Klare Trennung von UI, Compiler-/VM-Adapter und Anwendungslogik. Die IDE folgt einem ViewModel-/Adapter-Pattern, das UI-Logik von `Terminal.Gui`-Abhaengigkeiten trennt und automatisierte Tests ohne laufende TUI ermoeglicht. Die instanzbasierte `IApplication`-Architektur von v2 unterstuetzt dies: `IApplication` ist mockbar, mehrere Instanzen koennen parallel in Tests existieren, und der FakeDriver ermoeglicht Headless-Tests ohne Terminal.
- `NF-006` Didaktik: Oberflaeche und Meldungen sind fuer Lernende nachvollziehbar formuliert.
- `NF-007` Hintergrunddienste wie der Hilfe-Webserver laufen ohne stoerende Konsolenausgaben in der IDE-Ansicht.

## 7. Bedien- und Prozessablauf (Soll)
1. Benutzer erstellt/oeffnet eine `.pl0`-Datei im Editorfenster.
2. Benutzer oeffnet bei Bedarf den Compiler-Einstellungsdialog; bei Dialektwechsel setzt die IDE `MaxNumberDigits` automatisch (`Classic=14`, `Extended=10`).
3. Benutzer startet Kompilierung.
4. IDE zeigt Dialogstatus und schreibt Diagnosen ins Meldungsfenster.
5. Bei Erfolg wird P-Code im P-Code-Fenster aktualisiert.
6. Benutzer waehlt:
   - Ausfuehren (Gesamtlauf) — Laufzeiteingaben (`?`) werden ueber Eingabedialog erfasst, Ausgaben (`!`) im Ausgabefenster angezeigt, oder
   - Debuggen (Schrittbetrieb mit Register-/Stackanzeige).
   - Bei Bedarf kann eine laufende Ausfuehrung abgebrochen werden.
7. Benutzer kann den P-Code optional exportieren (`Asm` oder `Cod`; fuer `Cod` mit Dateiendung `.cod`).
8. Benutzer kann im Hilfe-Menue die Web-Hilfe aus `_site` starten/stoppen; beim Start wird die erreichbare URL im Hinweis-Dialog angezeigt.
9. Ergebnisse und Meldungen bleiben nachvollziehbar einsehbar.

## 8. Abnahmekriterien
- `AK-001`: `src/Pl0.Ide` ist in `TinyPl0.sln` eingebunden und baut erfolgreich.
- `AK-002`: IDE startet in Terminalumgebung mit sichtbarem Hauptlayout.
- `AK-003`: PL/0-Syntaxelemente (Schluesselwoerter, Zahlen, Operatoren) werden im Editor hervorgehoben; der Farbstil orientiert sich an Turbo Pascal.
- `AK-004`: Kompilierung aus IDE erzeugt entweder P-Code oder Diagnosen.
- `AK-005`: Kompilierdialog erscheint nach jedem Kompiliervorgang.
- `AK-006`: P-Code wird in separatem Fenster angezeigt.
- `AK-007`: P-Code-Ausfuehrung liefert sichtbare Ergebnisanzeige.
- `AK-008`: Datei laden/speichern funktioniert fuer `.pl0`-Dateien ueber die Standard-Dialoge von `Terminal.Gui`.
- `AK-009`: Formatierfunktion veraendert Quelltext deterministisch.
- `AK-010`: "Kompilieren und Ausfuehren" funktioniert in einem Schritt.
- `AK-011`: Schritt-Debugging zeigt pro Schritt Register- und Stackzustand; der aktuelle Ausfuehrungspunkt ist ueber den Instruktionszeiger (`P`) im P-Code nachvollziehbar.
- `AK-012`: Hilfe zur IDE-Bedienung, Hilfe zur Sprache PL/0 und integrierte Dokumentation sind aus der IDE heraus aufrufbar.
- `AK-013`: Automatisierte IDE-Tests (xUnit) sind vorhanden und mit `dotnet test` erfolgreich ausfuehrbar.
- `AK-014`: Ein Einstellungsdialog erlaubt das Setzen aller `CompilerOptions`-Werte innerhalb der definierten Bereiche; `MaxNumberDigits` wird dabei regelbasiert aus `Dialect` auf `10` oder `14` gesetzt. Die Werte werden beim naechsten Kompilieren wirksam.
- `AK-015`: Der P-Code-Export unterstuetzt `Asm` und `Cod`; bei `Cod` wird ausschliesslich die Dateiendung `.cod` verwendet.
- `AK-016`: Fuer Kernfunktionen (mindestens Build, Run, Step) sind Tastatur-Shortcuts verfuegbar.
- `AK-017`: Zuletzt geoeffnete Datei und Fenstergroessen werden ueber Neustarts hinweg wiederhergestellt.
- `AK-018`: Ein Hilfe-Menuepunkt startet die Web-Hilfe aus `_site` per Kestrel, zeigt die konkrete URL im Hinweis-Dialog und stoppt den Server beim naechsten Aufruf.
- `AK-019`: Kestrel-Ausgaben verursachen keine sichtbaren UI-Stoerungen in der IDE-Ansicht.
- `AK-020`: Die Web-Hilfe startet bevorzugt auf `http://localhost:5000`; bei belegtem Port erfolgt automatischer Fallback auf den naechsten freien Port aus `5001..5099`.
- `AK-021`: Die integrierte Dokumentationsansicht bietet TOC-Navigation, Inhaltsbereich und lokale Volltextsuche ueber die definierten lokalen Dokuquellen.
- `AK-022`: Die integrierte Dokumentation ist offline nutzbar, merkt sich die zuletzt geoeffnete Seite und zeigt bei fehlenden Doku-Dateien einen Hinweisdialog ohne Absturz.
- `AK-023`: Die integrierte Dokumentation startet standardmaessig mit `docs/LANGUAGE_EBNF.md`; falls nicht verfuegbar, wird gemaess definierter Priorisierung auf die naechste verfuegbare Quelle gewechselt.
- `AK-024`: Die `ArchitectureGuardTests` validieren, dass `Pl0.Ide` ausschliesslich von `Pl0.Core`, `Pl0.Vm` und `Terminal.Gui` abhaengt — nicht von `Pl0.Cli`.
- `AK-025`: Die IDE stellt eine eigene `IPl0Io`-Implementierung bereit; Laufzeiteingaben (`?`) werden ueber einen Eingabedialog erfasst, Ausgaben (`!`) im Ausgabefenster dargestellt.
- `AK-026`: Eine laufende P-Code-Ausfuehrung kann aus der IDE heraus abgebrochen werden (z. B. ueber Shortcut oder Schaltflaeche); der letzte VM-Zustand bleibt danach zur Analyse sichtbar.
- `AK-027`: Der Einstellungsdialog bietet eine Funktion zum Zuruecksetzen aller Werte auf `CompilerOptions.Default`.
- `AK-028`: Die Quelltextformatierung ist idempotent und deckt mindestens Einrueckung, Operator-Spacing und Zeilenumbrueche ab.
- `AK-029`: Bei fehlendem `_site` (relativ zum Executable-Verzeichnis) zeigt die IDE einen Hinweisdialog mit dem Verweis auf `docfx build`.
- `AK-030`: Persistenz-Einstellungen werden als JSON-Datei im plattformspezifischen Benutzerverzeichnis gespeichert; bei fehlender Datei startet die IDE mit Standardwerten.

## 9. Testfaelle und Anforderungszuordnung

Die folgenden Testfaelle sind als automatisierte xUnit-Tests umzusetzen.

| Testfall-ID  | Kurzbeschreibung                                                                                                                                                                                               | Zuordnung Anforderungen                                |
|--------------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|--------------------------------------------------------|
| `TC-IDE-001` | Projekt `Pl0.Ide` ist in Solution eingebunden und buildet erfolgreich.                                                                                                                                         | `PF-IDE-001`                                           |
| `TC-IDE-002` | IDE startet mit Hauptlayout (Editor, P-Code, Meldungen).                                                                                                                                                       | `PF-IDE-002`, `PF-IDE-003`, `PF-IDE-004`, `PF-IDE-009` |
| `TC-IDE-003` | PL/0-Syntaxelemente (Schluesselwoerter, Zahlen, Operatoren) werden im Editor im Turbo-Pascal-orientierten Farbstil hervorgehoben.                                                                              | `PF-IDE-005`                                           |
| `TC-IDE-004` | Kompilierung eines gueltigen Programms liefert Erfolg und zeigt Kompilierdialog.                                                                                                                               | `PF-IDE-006`, `PF-IDE-007`                             |
| `TC-IDE-005` | Kompilierung eines fehlerhaften Programms zeigt Diagnosen im Meldungsfenster.                                                                                                                                  | `PF-IDE-008`                                           |
| `TC-IDE-006` | Nach erfolgreicher Kompilierung wird P-Code im P-Code-Fenster angezeigt.                                                                                                                                       | `PF-IDE-009`                                           |
| `TC-IDE-007` | Ausfuehrung von P-Code liefert sichtbare Laufzeitausgabe.                                                                                                                                                      | `PF-IDE-010`                                           |
| `TC-IDE-008` | Laden und Speichern von `.pl0`-Dateien funktioniert verlustfrei ueber die Standard-Dialoge von `Terminal.Gui`.                                                                                                 | `PF-IDE-011`                                           |
| `TC-IDE-009` | Formatieren eines Quelltexts liefert deterministisches Ergebnis.                                                                                                                                               | `PF-IDE-012`                                           |
| `TC-IDE-010` | Aktion "Kompilieren und Ausfuehren" durchlaeuft beide Schritte erfolgreich.                                                                                                                                    | `PF-IDE-013`                                           |
| `TC-IDE-011` | Schritt-Debugging aktualisiert pro Step Register (`P`, `B`, `T`) und Stack; der aktuelle Ausfuehrungspunkt ist im P-Code ueber `P` sichtbar.                                                                   | `PF-IDE-014`                                           |
| `TC-IDE-012` | Hilfe zur IDE-Bedienung, Hilfe zur Sprache PL/0 und integrierte Dokumentation sind ueber die IDE aufrufbar; die integrierte Doku oeffnet als interne IDE-Ansicht.                                              | `PF-IDE-015`, `PF-IDE-016`                             |
| `TC-IDE-013` | Gesamte IDE-Test-Suite laeuft erfolgreich mit `dotnet test`.                                                                                                                                                   | `PF-IDE-017`                                           |
| `TC-IDE-014` | Einstellungsdialog validiert Wertebereiche fuer `CompilerOptions`, setzt `MaxNumberDigits` dialektabhaengig (`Classic=14`, `Extended=10`) und uebergibt die gesetzten Werte an den naechsten Kompiliervorgang. | `PF-IDE-018`                                           |
| `TC-IDE-015` | Exportfunktion schreibt P-Code in den Modi `Asm` und `Cod`; fuer `Cod` wird ausschliesslich `.cod` akzeptiert/verwendet.                                                                                       | `PF-IDE-021`                                           |
| `TC-IDE-016` | Kernfunktionen (mindestens Build, Run, Step) sind ueber definierte Tastatur-Shortcuts ausloesbar.                                                                                                              | `PF-IDE-019`                                           |
| `TC-IDE-017` | Zuletzt geoeffnete Datei und Fenstergroessen werden gespeichert und beim Neustart wiederhergestellt.                                                                                                           | `PF-IDE-020`                                           |
| `TC-IDE-018` | Hilfe-Menuepunkt startet Kestrel fuer `_site`, zeigt einen Hinweis-Dialog mit der tatsaechlichen URL und stoppt den Server beim zweiten Aufruf.                                                                | `PF-IDE-022`                                           |
| `TC-IDE-019` | Bei fehlendem `_site` (relativ zum Executable-Verzeichnis) oder Startfehler wird ein Fehlerdialog angezeigt; die IDE bleibt bedienbar.                                                                          | `PF-IDE-022`                                           |
| `TC-IDE-020` | Beim Betrieb der Web-Hilfe treten keine stoerenden Konsolenausgaben/Renderartefakte in der IDE-Ansicht auf. *(Halbautomatisch: pruefbar durch Verifikation, dass `Console.Out`/`Console.Error` waehrend des Serverbetriebs ueber Log-Isolation umgeleitet werden; visuelle Pruefung ergaenzend manuell.)* | `PF-IDE-022`, `NF-007`                                 |
| `TC-IDE-021` | Ist `localhost:5000` belegt, startet die Web-Hilfe automatisch auf dem naechsten freien Port im Bereich `5001..5099`, und der Hinweis-Dialog zeigt die tatsaechlich verwendete URL.                            | `PF-IDE-022`                                           |
| `TC-IDE-022` | Integrierte Dokumentation zeigt TOC-Navigation, Inhaltsbereich und lokale Volltextsuche ueber die konfigurierten Dokuquellen; Auswahl eines Treffers oeffnet den passenden Inhalt.                             | `PF-IDE-016`                                           |
| `TC-IDE-023` | Integrierte Dokumentation ist offline nutzbar, stellt die zuletzt geoeffnete Seite nach Neustart wieder her und zeigt bei fehlenden Doku-Dateien einen Hinweisdialog bei weiterhin bedienbarer IDE.            | `PF-IDE-016`                                           |
| `TC-IDE-024` | Start der integrierten Dokumentation oeffnet `docs/LANGUAGE_EBNF.md`; bei fehlender Datei wird automatisch die naechste verfuegbare Quelle gemaess Priorisierung geoeffnet.                                      | `PF-IDE-016`                                           |
| `TC-IDE-025` | `ArchitectureGuardTests` validieren, dass `Pl0.Ide` nur von `Pl0.Core`, `Pl0.Vm` und `Terminal.Gui` abhaengt — keine Referenz auf `Pl0.Cli`.                                                                   | `PF-IDE-001`, `PF-IDE-021`                             |
| `TC-IDE-026` | Laufzeiteingaben (`?`) werden ueber einen IDE-Eingabedialog erfasst; Ausgaben (`!`) erscheinen im Ausgabefenster. Die konsolenbasierte `ConsolePl0Io` wird nicht verwendet.                                     | `PF-IDE-010`                                           |
| `TC-IDE-027` | Eine laufende P-Code-Ausfuehrung (inkl. Endlosschleife) kann ueber Shortcut oder Schaltflaeche abgebrochen werden; die IDE bleibt danach bedienbar und der letzte VM-Zustand bleibt in der Debug-Ansicht sichtbar. | `PF-IDE-014`                                           |
| `TC-IDE-028` | Der Einstellungsdialog setzt alle Werte bei Klick auf Zuruecksetzen auf `CompilerOptions.Default` zurueck.                                                                                                       | `PF-IDE-018`                                           |
| `TC-IDE-029` | Die Quelltextformatierung ist idempotent: zweimaliges Anwenden auf denselben Quelltext liefert identisches Ergebnis. Einrueckung, Spacing und Zeilenumbrueche werden normalisiert.                                | `PF-IDE-012`                                           |
| `TC-IDE-030` | Persistenz-Einstellungen werden im plattformspezifischen Benutzerverzeichnis als JSON-Datei gespeichert und beim Neustart korrekt geladen; bei fehlender Datei startet die IDE mit Standardwerten.               | `PF-IDE-020`                                           |

Hinweise zur Umsetzung:
- Tests mit UI-Bezug sollen ueber testbare ViewModel-/Controller-Logik und Adapter abstrahiert werden. Die instanzbasierte `IApplication` von `Terminal.Gui` v2 ermoeglicht isolierte Tests mit FakeDriver ohne reales Terminal.
- Dateizugriffe und VM-I/O sind in Tests ueber Test-Doubles (z. B. In-Memory) zu kapseln.
- Jeder Testfall ist eindeutig auf mindestens eine `PF-IDE-*`-Anforderung rueckfuehrbar.

## 10. Liefergegenstaende
- Neues Projekt `src/Pl0.Ide` inkl. Projektdatei
- Einbindung in `TinyPl0.sln`
- IDE-Oberflaeche (Editor, P-Code, Meldungen, Debug, Hilfe)
- Schnittstellenanbindung an `Pl0.Core` und `Pl0.Vm`
- Tests fuer die IDE-Kernfunktionen (xUnit, in Testlauf integriert)
- Dokumentation der Bedienung (deutsch, im Repository)

## 11. Risiken und Annahmen

| Risiko | Beschreibung | Gegenmassnahme |
|--------|-------------|----------------|
| `R-001` | `Terminal.Gui` v2 befindet sich im Alpha-Status; Breaking Changes sind vor dem Beta-Release moeglich. Plattformspezifische Terminal-Eigenheiten koennen auftreten. | Fruehe Tests auf allen drei Zielplattformen (Windows, macOS, Linux). NuGet-Paketversion pinnen und bei Breaking Changes gezielt aktualisieren. v2 wird offiziell fuer neue Projekte empfohlen; v1 erhaelt nur noch kritische Bugfixes. |
| `R-002` | Schritt-Debugging erfordert Erweiterungen an VM-Observability-Schnittstellen. | VM-Erweiterung (`Step()`/`IVmObserver`) als eigene, fruehe Teilaufgabe planen (siehe PF-IDE-014). Bestehende VM-Tests muessen weiterhin bestehen. |
| `R-003` | Eine robuste Quelltextformatierung fuer alle Grenzfaelle kann iterative Nachschaerfung benoetigen. | Mindestumfang (Einrueckung, Spacing, Zeilenumbrueche) priorisieren; Grenzfaelle ueber Testfaelle absichern und bei Bedarf iterativ erweitern. |
| `R-004` | Der `_site`-Ordner fehlt im Repository und muss durch `docfx build` erzeugt werden. | Klarer Hinweisdialog in der IDE bei fehlendem `_site` (siehe PF-IDE-022). Build-Dokumentation um `docfx build`-Schritt ergaenzen. |

Annahmen:
- Das bestehende Compiler- und VM-Verhalten bleibt fachlich unveraendert.
- IDE-Dokumentationsinhalte koennen aus vorhandenen Repository-Dokumenten kuratiert werden.
