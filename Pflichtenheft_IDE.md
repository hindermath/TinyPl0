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
    - Zusaetzlich enthaelt der Einstellungsdialog einen Schalter fuer die Anzeige im Code-Fenster:
      - `P-Code` (numerische Darstellung),
      - `Assembler` (mnemonische Darstellung).
      - Default fuer die Code-Anzeige ist `Assembler`.
      Die Auswahl wirkt sich auf die naechste Anzeige nach erfolgreicher Kompilierung aus.

9. `PF-IDE-006` Kompilierung:
   - Quellcode kann direkt aus der IDE ueber `Pl0.Core` kompiliert werden.
   - Diese Anforderung ist umgesetzt und wird als erledigt betrachtet.

10. `PF-IDE-007` Kompilierdialog (entfaellt):
    - Ein separater Kompilierdialog wird nicht mehr benoetigt.
    - Der Ergebnisstatus nach der Kompilierung wird im Meldungsfenster angezeigt.
    - Diese Anforderung wird als erledigt betrachtet, da die notwendige Rueckmeldung bereits durch das Meldungsfenster abgedeckt ist.

11. `PF-IDE-008` Fehleranzeige:
    - Compilerdiagnosen werden gesammelt und in einem Meldungsfenster angezeigt.

12. `PF-IDE-009` P-Code-Fenster:
    - Der erzeugte P-Code wird in einem separaten Fenster dargestellt.
    - Jede Codezeile erhaelt eine feste, vierstellige Zeilennummer im Format `0000:`.
    - Die Darstellung kann zwischen `P-Code` (numerisch) und `Assembler` (mnemonisch) umgeschaltet werden.
    - Der Fenstertitel wird entsprechend der gewaehlten Darstellung angepasst (`P-Code` bzw. `Assembler-Code`).

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
    - Voraussetzung: Die VM in `Pl0.Vm` muss um eine Schritt-Ausfuehrungsschnittstelle erweitert werden. Die aktuelle `VirtualMachine.Run()` laeuft atomar bis zum Ende; fuer Schritt-Debugging muss der VM-Zustand (`P`, `B`, `T`, Stack) zwischen Einzelschritten zugaenglich sein. Die Umsetzung erfolgt als Teilaufgabe `PF-IDE-014a` (siehe unten).
    - Die IDE bietet eine Moeglichkeit, eine laufende P-Code-Ausfuehrung abzubrechen (z. B. ueber Tastatur-Shortcut oder Schaltflaeche), um auch bei Endlosschleifen bedienbar zu bleiben.
    - Nach einem Abbruch bleibt der letzte VM-Zustand (`P`, `B`, `T`, Stack) zur Analyse in der Debug-Ansicht sichtbar.

    **Teilaufgabe `PF-IDE-014a`: VM-Erweiterung fuer Schritt-Ausfuehrung**

    Gewaehlter Ansatz: **Zustandsbehaftete VM mit `Step()`-Methode** (statt Observer-Callback).

    Begruendung: Fuer ein didaktisches Projekt mit TUI-Debugging ist eine explizite `Step()`-Methode der klarere Ansatz. Die IDE ruft bei jedem Klick auf „Step" eine Methode auf und liest anschliessend den aktuellen VM-Zustand — ohne Thread-Synchronisation zwischen VM-Schleife und Terminal.Gui.

    Neue Typen in `Pl0.Vm`:

    | Typ                          | Art      | Beschreibung                                                                                           |
    |------------------------------|----------|--------------------------------------------------------------------------------------------------------|
    | `VmState`                    | `record` | Zustandsschnappschuss: `P`, `B`, `T`, `int[] Stack`, `Instruction? CurrentInstruction`                 |
    | `VmStepStatus`               | `enum`   | Ausfuehrungsstatus: `Running`, `Halted`, `Error`                                                       |
    | `VmStepResult`               | `record` | Ergebnis eines Einzelschritts: `VmState State`, `VmStepStatus Status`, `IReadOnlyList<VmDiagnostic> Diagnostics` |
    | `SteppableVirtualMachine`    | `class`  | Zustandsbehaftete VM mit `Initialize()`, `Step()`, `State`-Property und `IsRunning`-Property           |

    Schnittstellenvertrag `SteppableVirtualMachine`:
    - `Initialize(IReadOnlyList<Instruction> program, IPl0Io? io = null, VirtualMachineOptions? options = null)`: Setzt Register (`P=0`, `B=1`, `T=0`), alloziert Stack und bereitet die Ausfuehrung vor.
    - `Step() -> VmStepResult`: Fuehrt genau eine Instruktion aus und gibt den resultierenden Zustand zurueck. Bei bereits beendeter Ausfuehrung liefert erneuter Aufruf `VmStepStatus.Halted`.
    - `State -> VmState` (Property): Aktueller VM-Zustand, jederzeit abfragbar.
    - `IsRunning -> bool` (Property): `true`, solange die Ausfuehrung nicht beendet oder fehlerhaft ist.

    Abwaertskompatibilitaet:
    - Die bestehende `VirtualMachine` mit `Run()` bleibt unveraendert. CLI und nicht-Debug-Betrieb der IDE verwenden weiterhin `Run()`.
    - Alle bestehenden `VirtualMachineTests` muessen ohne Aenderung bestehen bleiben.
    - Optional kann `VirtualMachine.Run()` spaeter intern auf `SteppableVirtualMachine` delegieren, um Code-Dopplung zu reduzieren; dies ist kein Muss fuer die erste Umsetzung.

    Abbruch-Verhalten:
    - Kein expliziter `Abort()`-Mechanismus noetig: Die IDE ruft bei Abbruch einfach kein weiteres `Step()` auf und verwirft die Instanz.
    - Der letzte `VmState` bleibt ueber die `State`-Property fuer die Analyse in der Debug-Ansicht verfuegbar (siehe `AK-026`).

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
4. IDE zeigt den Ergebnisstatus im Meldungsfenster und schreibt Diagnosen ebenfalls dort hinein.
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
- `AK-005`: Nach jedem Kompiliervorgang wird der Ergebnisstatus im Meldungsfenster angezeigt (kein separater Dialog erforderlich).
- `AK-006`: Das Code-Fenster zeigt nach erfolgreicher Kompilierung die gewaehlte Darstellung (`P-Code` oder `Assembler`) inklusive vierstelliger Zeilennummern im Format `0000:`.
- `AK-007`: P-Code-Ausfuehrung liefert sichtbare Ergebnisanzeige.
- `AK-008`: Datei laden/speichern funktioniert fuer `.pl0`-Dateien ueber die Standard-Dialoge von `Terminal.Gui`.
- `AK-009`: Formatierfunktion veraendert Quelltext deterministisch.
- `AK-010`: "Kompilieren und Ausfuehren" funktioniert in einem Schritt.
- `AK-011`: Schritt-Debugging zeigt pro Schritt Register- und Stackzustand; der aktuelle Ausfuehrungspunkt ist ueber den Instruktionszeiger (`P`) im P-Code nachvollziehbar.
- `AK-031`: `SteppableVirtualMachine` in `Pl0.Vm` stellt `Initialize()`, `Step()`, `State` und `IsRunning` bereit; `Step()` fuehrt genau eine Instruktion aus und liefert `VmStepResult` mit `VmState`, `VmStepStatus` und `Diagnostics`.
- `AK-032`: Die bestehende `VirtualMachine.Run()` und alle zugehoerigen Tests bleiben durch die Erweiterung unveraendert und bestehen weiterhin.
- `AK-033`: `SteppableVirtualMachine` liefert nach Programmende (`P == 0` oder Fehler) bei erneutem `Step()`-Aufruf den Status `Halted` bzw. `Error` und fuehrt keine weitere Instruktion aus.
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
| `TC-IDE-004` | Kompilierung eines gueltigen Programms liefert Erfolg und zeigt den Ergebnisstatus im Meldungsfenster (ohne separaten Kompilierdialog).                                                                       | `PF-IDE-006`, `PF-IDE-008`                             |
| `TC-IDE-005` | Kompilierung eines fehlerhaften Programms zeigt Diagnosen im Meldungsfenster.                                                                                                                                  | `PF-IDE-008`                                           |
| `TC-IDE-006` | Nach erfolgreicher Kompilierung wird die gewaehlte Code-Darstellung (`P-Code`/`Assembler`) im Code-Fenster angezeigt; jede Zeile beginnt mit einer vierstelligen Zeilennummer im Format `0000:`.              | `PF-IDE-009`, `PF-IDE-018`                             |
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
| `TC-IDE-031` | `SteppableVirtualMachine.Initialize()` setzt Register auf Startwerte (`P=0`, `B=1`, `T=0`) und `IsRunning` auf `true`.                                                                                           | `PF-IDE-014a`                                          |
| `TC-IDE-032` | `SteppableVirtualMachine.Step()` fuehrt genau eine Instruktion aus; nach dem Schritt sind `P`, `B`, `T` und Stack im zurueckgegebenen `VmStepResult` korrekt aktualisiert.                                        | `PF-IDE-014a`                                          |
| `TC-IDE-033` | Nach Programmende (`P == 0`) liefert `Step()` den Status `Halted` und fuehrt keine weitere Instruktion aus; `IsRunning` ist `false`.                                                                               | `PF-IDE-014a`                                          |
| `TC-IDE-034` | Bei einem Laufzeitfehler (z. B. Division durch Null) liefert `Step()` den Status `Error` mit entsprechender `VmDiagnostic`; der letzte gueltige `VmState` bleibt ueber die `State`-Property abrufbar.              | `PF-IDE-014a`                                          |
| `TC-IDE-035` | Die bestehenden `VirtualMachineTests` bestehen nach Einfuehrung von `SteppableVirtualMachine` weiterhin unveraendert.                                                                                              | `PF-IDE-014a`                                          |

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
| `R-002` | Schritt-Debugging erfordert eine zustandsbehaftete VM-Erweiterung (`SteppableVirtualMachine`). | Umsetzung als Teilaufgabe `PF-IDE-014a` mit dedizierter Klasse und eigenem Testset (`TC-IDE-031`–`TC-IDE-035`). Die bestehende `VirtualMachine.Run()` bleibt unveraendert; alle bestehenden VM-Tests muessen weiterhin bestehen (`TC-IDE-035`). |
| `R-003` | Eine robuste Quelltextformatierung fuer alle Grenzfaelle kann iterative Nachschaerfung benoetigen. | Mindestumfang (Einrueckung, Spacing, Zeilenumbrueche) priorisieren; Grenzfaelle ueber Testfaelle absichern und bei Bedarf iterativ erweitern. |
| `R-004` | Der `_site`-Ordner fehlt im Repository und muss durch `docfx build` erzeugt werden. | Klarer Hinweisdialog in der IDE bei fehlendem `_site` (siehe PF-IDE-022). Build-Dokumentation um `docfx build`-Schritt ergaenzen. |

Annahmen:
- Das bestehende Compiler- und VM-Verhalten bleibt fachlich unveraendert.
- IDE-Dokumentationsinhalte koennen aus vorhandenen Repository-Dokumenten kuratiert werden.

## 12. Arbeitsprotokoll (fortlaufend)

Dieser Abschnitt dokumentiert ab diesem Arbeitsschritt fortlaufend den Dialogverlauf mit Datum/Uhrzeit und, falls vorhanden, der zugehoerigen Git-Commit-Referenz.

1. Benutzer (2026-02-20 22:31:43 CET): Bitte nach jedem Arbeitsschritt einen Abschnitt am Ende mit Dialog, Zeitstempel und ggf. Git-Commit dokumentieren (angelehnt an `Lastenheft_IDE.md`).
2. Agent (2026-02-20 22:31:43 CET): `Pflichtenheft_IDE.md` vollstaendig gelesen und das gewuenschte fortlaufende Protokoll im Pflichtenheft angelegt (kein separater Commit-Link).
3. Benutzer (2026-02-20 22:37:32 CET): Abschnitt 5.1 Nr. 1 umsetzen und dabei die Abnahmekriterien (Abschnitt 8) sowie Testzuordnung (Abschnitt 9) beruecksichtigen.
4. Agent (2026-02-20 22:37:32 CET): `PF-IDE-001` umgesetzt: neues Projekt `src/Pl0.Ide` erstellt, in `TinyPl0.sln` eingebunden, `ArchitectureGuardTests` fuer `Pl0.Ide`-Projekt- und Paketabhaengigkeiten erweitert, Build sowie Architekturtests erfolgreich ausgefuehrt (kein separater Commit-Link).
5. Benutzer (2026-02-20 22:42:14 CET): Hinweis, dass ein Commit erstellt wurde, mit Bitte um Nachdokumentation im Arbeitsprotokoll.
6. Agent (2026-02-20 22:42:14 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/7ee8dc3de090c991c779398072ed5430e0664f17) (`feat: add Pl0.Ide project with basic setup, integrate into solution, and extend architecture tests`, Commit-Zeit: `2026-02-20 22:41:23 +01:00`).
7. Benutzer (2026-02-20 22:47:15 CET): Bitte als naechsten Schritt direkt 5.1 Nr. 2 umsetzen und wieder im Arbeitsprotokoll dokumentieren.
8. Agent (2026-02-20 22:47:15 CET): `PF-IDE-002` umgesetzt: instanzbasierter `Terminal.Gui`-v2-Startpfad mit `IApplication` (`Init`/`Run<IdeMainView>`/`Shutdown`+`Dispose`) in `src/Pl0.Ide/Program.cs`; minimales Hauptlayout (`IdeMainView`) mit Fenstern fuer Quellcode, P-Code und Meldungen erstellt; Tests fuer v2-Backend und Kernlayout hinzugefuegt (`IdeBootstrapTests`), Testprojekt um `Pl0.Ide`-Referenz erweitert und `ArchitectureGuardTests` entsprechend angepasst; Build und relevante Tests erfolgreich (kein separater Commit-Link).
9. Benutzer (2026-02-20 22:52:09 CET): Hinweis, dass der Git-Commit erfolgt ist, mit Bitte um Dokumentation.
10. Agent (2026-02-20 22:52:09 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/ccaaf73149a9bce9d8e0c9595b10aaefb779df09) (`feat: implement Terminal.Gui v2 integration in Pl0.Ide with core UI layout and tests`, Commit-Zeit: `2026-02-20T22:51:39+01:00`).
11. Benutzer (2026-02-20 22:57:26 CET): Bitte als naechsten Schritt direkt 5.1 Nr. 3 umsetzen und wieder genauso dokumentieren.
12. Agent (2026-02-20 22:57:26 CET): `PF-IDE-003` umgesetzt: Turbo-Pascal-orientiertes Look-and-Feel erweitert durch klassische Menueleiste (`Datei`, `Bearbeiten`, `Kompilieren`, `Ausfuehren`, `Debug`, `Hilfe`) im Hauptlayout (`IdeMainView`) sowie Theme-Aktivierung ueber `ThemeManager` mit Basis `TurboPascal 5` (fehlertolerant) in `IdeLookAndFeel`; Startpfad aktualisiert (`Program`) und IDE-Bootstrap-Tests fuer Menuestruktur sowie Theme-Konfiguration ergaenzt; Build und relevante Tests erfolgreich (kein separater Commit-Link).
13. Benutzer (2026-02-20 23:00:32 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
14. Agent (2026-02-20 23:00:32 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/98c7acbdde8610666263752f19517c78c74199c0) (`feat: integrate Turbo Pascal theme and menu bar into Pl0.Ide, expand tests for look and feel`, Commit-Zeit: `2026-02-20T23:00:00+01:00`).
15. Benutzer (2026-02-20 23:04:15 CET): Bitte als naechsten Schritt direkt 5.1 Nr. 4 umsetzen und wieder genauso dokumentieren.
16. Agent (2026-02-20 23:04:15 CET): `PF-IDE-004` umgesetzt: das Quellcodefenster wurde als dediziertes Editorfenster erweitert (`TextView` als editierbarer Mehrzeilen-Editor mit `Dim.Fill()` im Fenster `Quellcode`), inklusive interner Referenz `SourceEditor` in `IdeMainView`; automatisierter Test ergaenzt, der den dedizierten Editorbereich im Quellcodefenster verifiziert; Build und relevante Tests erfolgreich (kein separater Commit-Link).
17. Benutzer (2026-02-20 23:09:27 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
18. Agent (2026-02-20 23:09:27 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/d834cdd3c3adcd84524b0a84175508cdbb91f4b8) (`feat: add dedicated source code editor to "Quellcode" window in Pl0.Ide with tests`, Commit-Zeit: `2026-02-20T23:08:49+01:00`).
19. Benutzer (2026-02-20 23:13:40 CET): Bitte als naechsten Schritt direkt 5.1 Nr. 5 umsetzen und wieder genauso dokumentieren.
20. Agent (2026-02-20 23:13:40 CET): `PF-IDE-005` umgesetzt: PL/0-Syntaxhervorhebung im dedizierten Quellcodeeditor implementiert (`Pl0SourceEditorView`) auf Basis des bestehenden `Pl0Lexer` aus `Pl0.Core`; Schluesselwoerter, Zahlen und Operatoren werden tokenbasiert klassifiziert und ueber das v2-Scheme-/Theming-Modell (`ColorScheme`/`SetAttribute`) in `OnDrawNormalColor` eingefaerbt; Integration in `IdeMainView` erfolgt und automatisierte Tests fuer Highlighting (inkl. Negativfall fuer Identifier) hinzugefuegt; Build und relevante Tests erfolgreich (kein separater Commit-Link).
21. Benutzer (2026-02-20 23:20:07 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
22. Agent (2026-02-20 23:20:07 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/3adbd78f28bbbe9f4f2bdf439487375adbe21b34) (`feat: add PL/0 syntax highlighting to source code editor with tests`, Commit-Zeit: `2026-02-20T23:19:28+01:00`).
23. Benutzer (2026-02-20 23:24:48 CET): Bitte als naechsten Schritt direkt 5.1 Nr. 6 umsetzen und wieder genauso dokumentieren.
24. Agent (2026-02-20 23:24:48 CET): `PF-IDE-006` umgesetzt: direkte Kompilierung aus der IDE ueber `Pl0.Core` eingebunden (`Pl0Compiler`-Integration in `IdeMainView`), ausloesbar ueber Menuepunkt `Kompilieren -> Build`; Compile-Ergebnis wird intern gespeichert (`LastCompilationResult`) und in P-Code-/Meldungsfenster geschrieben (P-Code bei Erfolg, formatierte Diagnosen bei Fehlern); automatisierte Tests fuer erfolgreichen und fehlerhaften Compile aus dem Editor ergaenzt; Build und relevante Tests erfolgreich (kein separater Commit-Link).
25. Benutzer (2026-02-20 23:40:34 CET): Fehler beim Aufruf von `Build` gemeldet (`Terminal.Gui.NotInitializedException` aus `Application.AddIdle` ueber `MenuBar.Run`/`Menu.Run`).
26. Agent (2026-02-20 23:40:34 CET): Fix umgesetzt: statisches `Terminal.Gui`-Application-Gateway in `src/Pl0.Ide/Program.cs` vor `Init()` explizit an die laufende `IApplication`-Instanz gebunden (`ApplicationImpl.ChangeInstance(app)`) und nach `Shutdown` wiederhergestellt; dadurch funktionieren Menueaktionen (`Build`) ohne `NotInitializedException`. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
27. Benutzer (2026-02-20 23:56:48 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
28. Agent (2026-02-20 23:56:48 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/16c2919b75788afe3665a1dcbab39e7f54e1ec76) (`feat: add compilation feature to IDE with menu integration and output views`, Commit-Zeit: `2026-02-20T23:56:09+01:00`).
29. Benutzer (2026-02-21 14:07:56 CET): Korrektur, dass zuvor inhaltlich `PF-IDE-009`/Kompilierung vorgezogen wurde; Bitte jetzt explizit 5.1 Nr. 6 `PF-IDE-011 Dateioperationen` umsetzen.
30. Agent (2026-02-21 14:07:56 CET): `PF-IDE-011` umgesetzt: Dateioperationen im Menue `Datei` von Platzhaltern auf echte Funktionen umgestellt (`Neu`, `Oeffnen`, `Speichern`) und ueber Standard-Dialoge von `Terminal.Gui` (`OpenDialog`, `SaveDialog`) angebunden; Laden/Speichern auf `.pl0` ausgelegt (inkl. Endungsnormalisierung beim Speichern), aktueller Dateipfad wird im View gehalten und Statusmeldungen werden im Meldungsfenster ausgegeben. Testabdeckung fuer `TC-IDE-008` ergaenzt (`SaveSourceFile_Writes_Source_Lossless_To_Pl0_File`, `OpenSourceFile_Loads_Source_Lossless_From_Pl0_File`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
31. Benutzer (2026-02-21 14:28:26 CET): Fehler gemeldet, dass im Datei-Speichern-Dialog bei Klick auf Save bzw. Enter keine Aktion erfolgt; zusaetzlich Bitte um Pruefung des Datei-Oeffnen-Dialogs fuer korrektes Oeffnen von `.pl0`-Dateien.
32. Agent (2026-02-21 14:28:26 CET): Dialogverhalten korrigiert: die blockierende Dateityp-Filterung (`AllowedTypes`) in `OpenDialog`/`SaveDialog` entfernt, damit Bestaetigung im Speichern-Dialog (`Save`/Enter) zuverlaessig ausgeloest wird; `.pl0`-Regel bleibt in der IDE-Fachlogik erhalten (`Save`: Endungsnormalisierung auf `.pl0`; `Open`: nur `.pl0`, sonst Hinweis). Oeffnen-Funktionalitaet explizit abgesichert durch neue Tests fuer Gross-/Kleinschreibung (`.PL0` wird akzeptiert) und Ablehnung nicht erlaubter Endungen (`.txt`), plus erfolgreiche Verifikation via `dotnet build TinyPl0.sln` und `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
33. Benutzer (2026-02-21 14:33:58 CET): Rueckmeldung, dass Speichern weiterhin nicht in die im Dialog eingegebene Zieldatei schreibt; Bitte um erneute Korrektur.
34. Agent (2026-02-21 14:33:58 CET): Pfadauflosung in der Dialog-Adapterlogik nachgebessert: `Open/Save` verwenden jetzt primaer den durch den `FileDialog` gelieferten `Path` und nur als Fallback `FileName`/Dateiliste; relative Rueckgaben werden robust auf einen vollqualifizierten Zielpfad aufgeloest (bevorzugt relativ zum bekannten Basispfad, sonst `GetFullPath`). Dadurch wird die tatsaechlich im Dialog ausgewaehlte/eingegebene Datei als Ziel verwendet. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
35. Benutzer (2026-02-21 14:42:21 CET): Wunsch, dass in den Fenstertiteln von `Quellcode` und `P-Code` nach Doppelpunkt ausschliesslich der Dateiname angezeigt wird, sobald eine Datei geoeffnet oder gespeichert wurde.
36. Agent (2026-02-21 14:42:21 CET): Fenstertitel-Logik umgesetzt: `Quellcode`- und `P-Code`-Fenster werden nach erfolgreichem Oeffnen/Speichern auf `"<Fenstername>: <Dateiname>"` gesetzt (ohne Pfad), bei neuer/leerem Dokument auf Basistitel zurueckgesetzt. Umsetzung in `IdeMainView` ueber zentrale Methode `UpdateDocumentTitles`; Testabdeckung ergaenzt fuer Titel nach Speichern/Oeffnen inklusive Nachweis "nur Dateiname". Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
37. Benutzer (2026-02-21 14:47:23 CET): Anforderung, dass in beiden Datei-Dialogen die Filteroptionen `.pl0` und `Alle Dateien` angeboten werden, mit `.pl0` als Default.
38. Agent (2026-02-21 14:47:23 CET): Filteroptionen in `OpenDialog` und `SaveDialog` erweitert: jeweils zwei Typen (`PL/0 (*.pl0)` zuerst als Default ueber Reihenfolge, plus `Alle Dateien`), wobei der Speichern-Filter zusaetzlich Dateinamen ohne Endung akzeptiert, damit die bestehende Endungsnormalisierung auf `.pl0` weiterhin greift. Testabdeckung fuer Dialog-Filterlogik ergaenzt (`CreateOpenAllowedTypes`, `CreateSaveAllowedTypes`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
39. Benutzer (2026-02-21 15:19:03 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
40. Agent (2026-02-21 15:19:03 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/1bc67a2260dc1631c81ef0dc45fb139ee52c9ea2) (`feat: add support for file operations in Pl0 IDE with dialogs and tests`, Commit-Zeit: `2026-02-21T15:18:32+01:00`).
41. Benutzer (2026-02-21 15:25:21 CET): Bitte 5.1 Nr. 7 `PF-IDE-012 Quelltextformatierung` umsetzen und dokumentieren.
42. Agent (2026-02-21 15:25:21 CET): `PF-IDE-012` umgesetzt: neue tokenbasierte Formatierungskomponente `Pl0SourceFormatter` in `Pl0.Ide` implementiert (deterministisch/idempotent), inklusive Einrueckung nach `begin`/`end` und Prozedurblock, Normalisierung von Operator-Spacing sowie Trennzeichen-/Zeilenumbruchregeln nach `,`, `;` und `.`; Menueaktion `Bearbeiten -> Formatieren` in `IdeMainView` von Platzhalter auf echte Funktion umgestellt, inkl. Rueckmeldung im Meldungsfenster. Testabdeckung fuer `TC-IDE-009` und `TC-IDE-029` ergaenzt (`FormatSource_Normalizes_Indentation_Spacing_And_LineBreaks`, `FormatSource_Is_Idempotent`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
43. Benutzer (2026-02-21 15:37:46 CET): Hinweis auf Fehlformatierung: die letzte Zeile vor `end;` bzw. `end.` braucht kein abschliessendes `;`; Referenz auf `@fmt_wrong.pl0`.
44. Agent (2026-02-21 15:37:46 CET): Regression in der End-Block-Formatierung korrigiert: `end` wird nun auch ohne vorheriges `;` auf neue, korrekt eingerueckte Zeile gesetzt (kein erzwungenes Semikolon). Umsetzung in `Pl0SourceFormatter` durch gezielten Zeilenumbruch vor `TokenKind.End`, sofern nicht bereits Zeilenstart; Regressionstest `FormatSource_Does_Not_Require_Semicolon_Before_End` hinzugefuegt. Verifikation erfolgreich mit `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests"` sowie `dotnet build TinyPl0.sln` und `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
45. Benutzer (2026-02-21 15:42:20 CET): Hinweis, dass die Zeile `procedure <Name>;` ebenfalls zum korrespondierenden `begin/end`-Block korrekt eingerueckt sein muss.
46. Agent (2026-02-21 15:42:20 CET): Formatierungsregel erweitert: analog zu `end` wird vor `procedure` bei Bedarf ein Zeilenumbruch erzwungen, damit die Prozedur-Headerzeile sauber auf Blockebene eingerueckt wird (ohne zusaetzliches `;`). Regressionstest `FormatSource_Indents_Procedure_Header_In_Containing_Begin_End_Block` ergaenzt. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
47. Benutzer (2026-02-21 15:48:05 CET): Rueckmeldung, dass `procedure` noch nicht auf gleicher Ebene wie sein korrespondierender `begin/end`-Block eingerueckt wird.
48. Agent (2026-02-21 15:48:05 CET): Einrueckungslogik fuer Prozedurbloecke final angepasst: die zusaetzliche Einrueckung nach `procedure <Name>;` entfernt, sodass `procedure`, zugehoeriges `begin` und `end` auf derselben Blockebene stehen. Entsprechend Erwartungswerte in den Formatierungs- und Idempotenztests aktualisiert, inkl. verschachteltem Prozedurfall. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
49. Benutzer (2026-02-21 17:00:33 CET): Hinweis, dass Verschachtelungsebenen gemaess EBNF fuer `const`/`var`/`procedure` innerhalb von Prozedurbloecken sowie im zugehoerigen `begin/end`-Teil korrekt beruecksichtigt werden muessen; Verweis auf `fmt.pl0`.
50. Agent (2026-02-21 17:00:33 CET): Formatierungslogik auf EBNF-orientierte Blockverschachtelung korrigiert: nach `procedure <Name>;` wird die Blockeinrueckung wieder erhoeht, und am korrespondierenden Blockabschluss (Semikolon nach dem inneren Block) wieder reduziert; dadurch werden deklarativer Blockteil (`const`/`var`/verschachtelte `procedure`) und Statement-Teil (`begin/end`) in verschachtelten Prozeduren konsistent auf der jeweils korrekten Ebene ausgegeben. Betroffene Erwartungswerte in bestehenden Formatierungstests aktualisiert und neuer Regressionstest `FormatSource_Respects_Ebnf_Block_Nesting_For_Procedure_Declarations` hinzugefuegt (angelehnt an `fmt.pl0`). Verifikation erfolgreich mit `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests"` sowie `dotnet build TinyPl0.sln` und `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
51. Benutzer (2026-02-21 17:15:32 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
52. Agent (2026-02-21 17:15:32 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/e5e7ab6e829b190a9c2fc05ea9f5c0e0e0f0064a) (`feat: add source code formatting feature with EBNF-consistent indentation and tests`, Commit-Zeit: `2026-02-21T17:14:56+01:00`).
53. Benutzer (2026-02-21 17:25:27 CET): Bitte Abschnitt 5.1 Nr. 8 `PF-IDE-018 Compiler-Einstellungsdialog` ausfuehren.
54. Agent (2026-02-21 17:25:27 CET): `PF-IDE-018` umgesetzt: neuer Compiler-Einstellungsdialog in `Pl0.Ide` mit Konfiguration fuer `Dialect`, `MaxLevel`, `MaxAddress`, `MaxIdentifierLength`, `MaxSymbolCount`, `MaxCodeLength` sowie abgeleitetem, nicht frei editierbarem `MaxNumberDigits` (`Classic=14`, `Extended=10`); Wertebereichsvalidierung (`0..10`, `127..32767`, `1..32`, `10..5000`, `50..10000`) integriert, inklusive Reset-Funktion auf Standardwerte (`CompilerOptions.Default` mit dialektabhaengiger `MaxNumberDigits`-Ableitung), Menueanbindung ueber `Kompilieren -> Einstellungen` und Uebernahme fuer den naechsten Kompiliervorgang. Testabdeckung fuer `TC-IDE-014` und `TC-IDE-028` ergaenzt (`OpenCompilerSettingsDialog_Changes_Options_For_Next_Compile`, `OpenCompilerSettingsDialog_Rejects_Invalid_Values`, `CompilerSettingsState_ResetToDefaults_Uses_CompilerOptionsDefault_And_Derived_MaxNumberDigits`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
55. Benutzer (2026-02-21 17:34:18 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
56. Agent (2026-02-21 17:34:18 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/14de10ad04b0173f46b65675da19b034fa366939) (`feat: add compiler settings dialog with validation and tests`, Commit-Zeit: `2026-02-21T17:33:26+01:00`).
57. Benutzer (2026-02-21 17:41:04 CET): `PF-IDE-006` als bereits erledigt markieren und `PF-IDE-007` als nicht mehr benoetigt/erledigt dokumentieren; anschliessend Commit ausfuehren und ebenfalls dokumentieren.
58. Agent (2026-02-21 17:41:04 CET): Pflichtenheft angepasst: `PF-IDE-006` explizit als erledigt markiert, `PF-IDE-007` als entfallen dokumentiert (Rueckmeldung ueber Meldungsfenster), Prozessschritt 7.4, `AK-005` und `TC-IDE-004` konsistent auf Meldungsfenster statt Kompilierdialog umgestellt; Commit im gleichen Arbeitsschritt ausgefuehrt (Commit-Details siehe Git-Historie/Antwort).
59. Benutzer (2026-02-21 17:45:55 CET): Bitte `PF-IDE-008 Fehleranzeige` pruefen; falls noch nicht umgesetzt, entsprechend umsetzen.
60. Agent (2026-02-21 17:45:55 CET): `PF-IDE-008` geprueft: Funktionalitaet ist bereits umgesetzt. `Pl0Compiler` sammelt Diagnosen in einer Liste (`CompilationResult.Diagnostics`), und `IdeMainView` schreibt diese gesammelt in das Meldungsfenster (`FormatDiagnostics`). Nachweis mit fehlerhaftem Programm (`error_multiple_errors_recovery`) ergab mehrere Diagnosen (`E11`, `E24`, `E17`, `E9`, `E99`). Zusaetzlich Regressionstest ergaenzt (`CompileSource_Shows_All_Collected_Diagnostics_In_Messages_Window`) und erfolgreich verifiziert mit `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests"` (kein separater Commit-Link).
61. Benutzer (2026-02-21 17:52:13 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
62. Agent (2026-02-21 17:52:13 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/dc78c390d6c3346bee704dcb9d1fa16919a5e549) (`test: add regression test for diagnostics display in messages window`, Commit-Zeit: `2026-02-21T17:51:53+01:00`).
63. Benutzer (2026-02-21 18:01:26 CET): Fuer `PF-IDE-009` zwei Ergaenzungen angefordert: vierstellige Zeilennummern (`0000:`) vor jeder Codezeile sowie ein Schalter im Einstellungsdialog fuer `P-Code` vs. `Assembler` inkl. angepasstem Fenstertitel.
64. Agent (2026-02-21 18:01:26 CET): Umsetzung erfolgt: Code-Fenster zeigt jetzt nummerierte Zeilen (`D4` + `:`), Darstellung ist ueber den Einstellungsdialog zwischen numerischem `P-Code` (`ToCod`) und mnemonischem `Assembler` (`ToAsm`) umschaltbar; der Fenstertitel wechselt entsprechend (`P-Code`/`Assembler-Code`) und wird auch bei geoeffneter Datei korrekt mit Dateinamen fortgefuehrt. Testabdeckung erweitert (`CompileSource_Can_Switch_Between_PCode_And_Assembler_Display`, `WindowTitles_Use_Code_Display_Mode_After_File_Open`) sowie bestehende Tests angepasst; Verifikation erfolgreich mit `dotnet build TinyPl0.sln` und `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
65. Benutzer (2026-02-21 18:10:22 CET): Zwei Korrekturen angefordert: Zeilennummerierung soll bei `0000` starten und im Einstellungsdialog soll `Assembler` als Default gesetzt sein.
66. Agent (2026-02-21 18:10:22 CET): Korrektur umgesetzt: Zeilennummern starten nun bei `0000` (statt `0001`), Default-Anzeigemodus ist `Assembler` (Initialzustand sowie `Zuruecksetzen` im Einstellungsdialog). Titel-/Anzeige-Tests entsprechend angepasst und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
67. Benutzer (2026-02-21 18:16:46 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
68. Agent (2026-02-21 18:16:46 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/87611be827651c0e0a8daad4516b8a887eb8128e) (`feat: add switchable P-Code and Assembler display with line numbering in the IDE`, Commit-Zeit: `2026-02-21T18:16:16+01:00`).
69. Benutzer (2026-02-21 18:21:03 CET): Bitte Abschnitt 5.1 Nr. 13 `PF-IDE-021 P-Code-Export (Emit-Modi)` umsetzen.
70. Agent (2026-02-21 18:21:03 CET): `PF-IDE-021` umgesetzt: Exportfunktion im Menue `Kompilieren` ergaenzt (`Export Asm`, `Export Cod`), Export nur nach erfolgreicher Kompilierung erlaubt, Serialisierung ueber `PCodeSerializer.ToAsm()` bzw. `PCodeSerializer.ToCod()`, und bei `Cod` wird die Zielendung strikt auf `.cod` normalisiert. Die IDE bleibt dabei ohne `Pl0.Cli`-Abhaengigkeit (eigener `IdeEmitMode`). Dialogservice um dedizierten Export-Dialog erweitert (inkl. Dateitypfilter fuer `asm`/`cod`). Testabdeckung fuer `TC-IDE-015` ergaenzt (`ExportCompiledCode_Requires_Successful_Compilation`, `ExportCompiledCode_Writes_Asm_Output`, `ExportCompiledCode_Cod_Uses_Cod_Extension_Only`, `FileDialogFilters_For_Export_Expose_Asm_And_Cod`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
71. Benutzer (2026-02-21 18:29:34 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
72. Agent (2026-02-21 18:29:34 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/41e5388c5cf6cc1906e91f2f2461a8d8f065cf7d) (`feat: add export functionality for compiled code in ASM and COD formats`, Commit-Zeit: `2026-02-21T18:29:00+01:00`).
73. Benutzer (2026-02-21 22:47:42 CET): Bitte Abschnitt 5.1 Nr. 14 `PF-IDE-010 P-Code-Ausfuehrung` umsetzen und dokumentieren.
74. Agent (2026-02-21 22:47:42 CET): `PF-IDE-010` umgesetzt: Menueaktion `Ausfuehren -> Run` auf echte P-Code-Ausfuehrung umgestellt (`RunCompiledCode`), neues Ausgabefenster `Ausgabe` im Hauptlayout ergaenzt und VM-Lauf via `VirtualMachine` an die letzte erfolgreiche Kompilierung gebunden; eigene IDE-I/O-Integration implementiert (`IdeRuntimeIo`) mit Eingabe ueber IDE-Dialog (`TerminalGuiIdeRuntimeDialogService`) und Ausgabe ueber das Ausgabefenster statt Konsole. Laufzeitdiagnosen werden bei Fehlern im Meldungsfenster angezeigt (`R<Code>`). Testabdeckung fuer `AK-007`/`AK-025` sowie `TC-IDE-007`/`TC-IDE-026` ergaenzt (`RunCompiledCode_Requires_Successful_Compilation`, `RunCompiledCode_Executes_Program_And_Writes_Runtime_Output`, `RunCompiledCode_Uses_Ide_Input_Dialog_For_Question_Operator`, `RunCompiledCode_Shows_Runtime_Diagnostics_On_Error`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
75. Benutzer (2026-02-21 22:54:45 CET): Hinweis, dass der Git-Commit fuer den letzten Arbeitsschritt erfolgt ist, mit Bitte um Dokumentation.
76. Agent (2026-02-21 22:54:45 CET): Commit-Referenz fuer den vorigen Arbeitsschritt nachgetragen: [Commit](https://github.com/hindermath/TinyPl0/commit/68894db08a1f4ad027d963bda3cfea9617c92b1c) (`feat: enable P-Code execution with runtime diagnostics and output`, Commit-Zeit: `2026-02-21T22:54:12+01:00`).
77. Benutzer (2026-02-21 22:57:54 CET): Bitte Abschnitt 5.1 Nr. 15 `PF-IDE-013 Kombi-Aktion` umsetzen und dokumentieren.
78. Agent (2026-02-21 22:57:54 CET): `PF-IDE-013` umgesetzt: dedizierte Kombi-Aktion `Kompilieren und Run` in der Menuegruppe `Ausfuehren` ergaenzt; neue IDE-Operation `CompileAndRun()` fuehrt Kompilierung und anschliessend (nur bei erfolgreicher Kompilierung) die P-Code-Ausfuehrung in einem Schritt aus. Damit bleibt das Verhalten konsistent zu `PF-IDE-006`/`PF-IDE-010` (Compilerdiagnosen im Meldungsfenster, Laufzeitausgabe im Ausgabefenster). Testabdeckung fuer `AK-010` und `TC-IDE-010` ergaenzt (`CompileAndRun_Compiles_And_Executes_In_One_Step`) und erfolgreich verifiziert mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
79. Benutzer mit Zeitstempel 2026-02-21 23:01:33 CET
80. Agent mit Commit-Referenz: 82d585f9ffa2441ad5c143d1c03f5408dab6cf6e (feat: implement "Compile and Run" combined action in IDE, Commit-Zeit 2026-02-21T23:01:11+01 (line 0))
81. Benutzer (2026-02-21 23:15:00 CET): Bitte fuer `PF-IDE-014` die im Pflichtenheft vorgesehene VM-Erweiterung als eigene Teilaufgabe planen, bevor etwas geschrieben wird.
82. Agent (2026-02-21 23:15:00 CET): Vorschlag fuer VM-Erweiterung erarbeitet: Analyse der bestehenden `VirtualMachine.Run()` (atomar, alle Register lokal), Vergleich zweier Ansaetze (A: Observer-Callback `IVmObserver`, B: zustandsbehaftete VM mit `Step()`-Methode). Empfehlung fuer Ansatz B (natuerliche Passung fuer TUI-Debugging: Button-Klick → `Step()` → Zustand ablesen). Vorschlag dem Benutzer vorgelegt.
83. Benutzer (2026-02-21 23:18:00 CET): Ansatz B genehmigt, bitte als Teilaufgabe ins Pflichtenheft uebernehmen.
84. Agent (2026-02-21 23:18:00 CET): Teilaufgabe `PF-IDE-014a` im Pflichtenheft dokumentiert: Anforderung `PF-IDE-014` um detaillierten Teilaufgabenblock ergaenzt (neue Typen `VmState`, `VmStepStatus`, `VmStepResult`, `SteppableVirtualMachine` mit `Initialize()`/`Step()`/`State`/`IsRunning`); Abnahmekriterien `AK-031`–`AK-033` hinzugefuegt; Testfaelle `TC-IDE-031`–`TC-IDE-035` in Abschnitt 9 ergaenzt; Risiko `R-002` auf Ansatz B konkretisiert. Keine Code-Aenderungen, nur Pflichtenheft-Aktualisierung (kein separater Commit-Link).
85. Benutzer (2026-02-22 00:12:30 CET): Bitte Abschnitt 5.1 Nr. 16 `PF-IDE-014 Debugging` inklusive Teilaufgabe `PF-IDE-014a` umsetzen und dokumentieren.
86. Agent (2026-02-22 00:12:30 CET): `PF-IDE-014` und `PF-IDE-014a` umgesetzt: in `Pl0.Vm` wurden `SteppableVirtualMachine`, `VmState`, `VmStepStatus` und `VmStepResult` eingefuehrt (`Initialize()`/`Step()`/`State`/`IsRunning`) fuer echte Einzelschritt-Ausfuehrung; in `Pl0.Ide` wurde das Debug-Menue von Platzhaltern auf echte Aktionen umgestellt (`Step`, `Abbrechen`), ein eigenes Debug-Fenster fuer Register- und Stackanzeige ergaenzt, sowie eine P-Code-Markierung des aktuellen Instruktionszeigers (`P`) im Codefenster implementiert. Der Abbruch beendet die Debug-Session ohne Verlust des zuletzt sichtbaren VM-Zustands. Testabdeckung ergaenzt fuer `TC-IDE-011`/`TC-IDE-027` (`StepDebug_Updates_Registers_Stack_And_Pointer_In_Code_Window`, `AbortDebug_Stops_Debug_Session_And_Leaves_Last_State_Visible`) sowie `TC-IDE-031`–`TC-IDE-034` (`SteppableVirtualMachineTests`). Nachweis erfolgreich mit `dotnet build TinyPl0.sln` und `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~SteppableVirtualMachineTests|FullyQualifiedName~VirtualMachineTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
87. Benutzer (2026-02-22 00:24:43 CET): Bitte das Debug-Fenster rechts neben dem Assembler/P-Code-Fenster platzieren.
88. Agent (2026-02-22 00:24:43 CET): Layout angepasst: Debug-Fenster aus der unteren Fensterreihe in die obere Reihe verschoben und direkt rechts neben dem Assembler/P-Code-Fenster positioniert; untere Reihe auf Meldungen + Ausgabe konsolidiert. Umsetzung in `IdeMainView` (Top-Layout: `Quellcode` | `Assembler/P-Code` | `Debug`; Bottom-Layout: `Meldungen` | `Ausgabe`). Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
89. Benutzer (2026-02-22 00:31:54 CET): Hinweis, dass die Stackanzeige bei `000` statt `001` beginnen soll.
90. Agent (2026-02-22 00:31:54 CET): Debug-Stackdarstellung korrigiert: sichtbare Stack-Indizes starten jetzt bei `000` (UI-Mapping: interner VM-Index `1` wird als `000` angezeigt), ohne Aenderung der VM-internen Speicheradressierung. Regressionstest in `IdeBootstrapTests` erweitert (`StepDebug_Updates_Registers_Stack_And_Pointer_In_Code_Window` prueft auf `"[000]"`). Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
91. Benutzer (2026-02-22 00:48:22 CET): Bitte `B=X` gelb und `T=Y` blau einfaerben sowie gleichfarbige `>>`-Indikatoren an den Stack-Positionen von Base- und Stack-Pointer anzeigen.
92. Agent (2026-02-22 00:48:22 CET): Debug-Anzeige farbig erweitert: neue `IdeDebugView` mit tokenbasierter Highlight-Map eingefuehrt; Registerwerte `B=<Zahl>` werden gelb (`BrightYellow`), `T=<Zahl>` blau (`BrightBlue`) gerendert. In den Stack-Zeilen werden Pointer-Indikatoren als `B>>` (gelb) und `T>>` (blau) angezeigt; bei Abwesenheit bleibt je ein Leerindikator. `IdeMainView.RenderDebugState` wurde auf strukturierte Ausgabe mit Highlight-Spans umgestellt (`BuildRegisterLine`, `BuildStackLine`). Testabdeckung erweitert (`StepDebug_Updates_Registers_Stack_And_Pointer_In_Code_Window` und neuer Test `StepDebug_Highlights_Base_And_Stack_Pointers_In_Requested_Colors` ueber `IdeDebugView.GetHighlightKindAt`). Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
93. Benutzer (2026-02-22 00:53:33 CET): Bitte im Debug-Fenster `P` zu `IP`, `B` zu `BP` und `T` zu `SP` umbenennen.
94. Agent (2026-02-22 00:53:33 CET): Debug-Benennung vereinheitlicht: Registerzeile im Debug-Fenster nutzt jetzt `IP=<Wert>`, `BP=<Wert>`, `SP=<Wert>`; Stack-Indikatoren wurden passend auf `BP>>` und `SP>>` umgestellt. Highlight-Logik bleibt unveraendert (BP gelb, SP blau) und ist weiterhin testabgedeckt. Betroffene Assertions in `IdeBootstrapTests` auf neue Labels angepasst. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
95. Benutzer (2026-02-22 00:57:42 CET): Hinweis, dass die Werte `BP` und `SP` im Debug-Fenster ebenfalls bei `0` starten sollen, passend zur Stack-Anzeige ab `[000]`.
96. Agent (2026-02-22 00:57:42 CET): Registeranzeige auf 0-basierte Sichtwerte umgestellt: `BP` und `SP` werden jetzt als `max(0, internerIndex-1)` angezeigt und stimmen damit mit der Stack-Zeilenzaehlung (`[000]` ab internem Index `1`) ueberein. Pointer-Indikatoren/Highlighting bleiben unveraendert korrekt auf den internen Stackpositionen. Testanpassung in `StepDebug_Highlights_Base_And_Stack_Pointers_In_Requested_Colors` auf die 0-basierten Registertokens erfolgt. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
97. Benutzer (2026-02-22 01:06:35 CET): Hinweis, dass die Assembler-Instruktion im Debug-Fenster komplett in Kleinbuchstaben wie im Code-Fenster angezeigt werden soll.
98. Agent (2026-02-22 01:06:35 CET): Debug-Instruktionsanzeige vereinheitlicht: `FormatCurrentInstruction` rendert die Mnemonik jetzt per `ToLowerInvariant()` (z. B. `lit`, `jmp`, `opr`) statt gemischter Enum-Schreibweise. Dadurch ist die Darstellung im Debug-Fenster konsistent mit `PCodeSerializer.ToAsm()` im Code-Fenster. Regressionstest in `StepDebug_Updates_Registers_Stack_And_Pointer_In_Code_Window` ergaenzt (Mnemonik muss lowercase sein). Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
99. Benutzer (2026-02-22 01:11:01 CET): Bei Umstellung der Anzeige auf `P-Code` soll im Debug-Fenster bei `Instruktion:` dieselbe Darstellung wie im Instruktionsfenster erscheinen.
100. Agent (2026-02-22 01:11:01 CET): Modusabhaengige Debug-Instruktionsanzeige umgesetzt: `FormatCurrentInstruction` beruecksichtigt jetzt `IdeCodeDisplayMode` (`Assembler` -> mnemonisch lowercase, `P-Code` -> numerischer Opcode), sodass `Instruktion:` im Debug-Fenster mit der gewaehlten Darstellung des Instruktionsfensters uebereinstimmt. Regressionstest ergaenzt (`StepDebug_Shows_Current_Instruction_In_PCode_Mode_Consistent_With_Code_Window`): im `P-Code`-Modus wird verifiziert, dass der Instruktions-Token in der Debugzeile numerisch ist und dem markierten `>>`-Eintrag im Codefenster entspricht. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
101. Benutzer (2026-02-22 01:15:32 CET): Rueckmeldung, dass beim Umschalten von `Assembler` auf `P-Code` das Debug-Fenster noch nicht wie das Instruktionsfenster aktualisiert wird; Bitte um Korrektur.
102. Agent (2026-02-22 01:15:32 CET): Synchronisationsfehler korrigiert: `IdeMainView` speichert jetzt den letzten Debug-Schritt (`lastDebugStepResult`) und rendert diesen in `OpenCompilerSettingsDialog()` nach einem Anzeigewechsel erneut, sodass `Instruktion:` im Debug-Fenster sofort zum neuen Code-Anzeigemodus passt. Zusaetzlich wird der gespeicherte Debugzustand bei Neu/Oeffnen/Kompilieren zurueckgesetzt. Neuer Regressionstest (`OpenCompilerSettingsDialog_ReRenders_DebugInstruction_To_Selected_CodeDisplayMode`) verifiziert den Wechsel von mnemonisch zu numerisch ohne weiteren `Step()`-Aufruf. Verifikation erfolgreich mit `dotnet build TinyPl0.sln` sowie `dotnet test tests/Pl0.Tests/Pl0.Tests.csproj --filter "FullyQualifiedName~IdeBootstrapTests|FullyQualifiedName~ArchitectureGuardTests"` (kein separater Commit-Link).
