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
- GUI-Bibliothek: `Terminal.Gui` in Version `1.9.x`
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
1. `PF-IDE-001` Projektstruktur:
   - Ein neues Projekt `src/Pl0.Ide` ist in der Solution eingebunden und baubar.

2. `PF-IDE-002` GUI-Basis:
   - Die Oberflaeche basiert auf Nuget-Paket `Terminal.Gui` `1.9.x`.

3. `PF-IDE-003` Look-and-Feel:
   - Layout und Menuefuehrung orientieren sich an der Turbo-Pascal-IDE (DOS-Stil).

4. `PF-IDE-004` Quellcodefenster:
   - Ein dediziertes Fenster zur Anzeige und Bearbeitung von PL/0-Quellcode ist vorhanden.

5. `PF-IDE-005` Syntax-Hervorhebung:
   - Schluesselwoerter von PL/0 werden im Editor farblich hervorgehoben.
   - Der Farbstil richtet sich nach den Konventionen von Turbo-Pascal.
   - Auch Zahlen, Operatoren werden entsprechend formatiert.

6. `PF-IDE-006` Kompilierung:
   - Quellcode kann direkt aus der IDE ueber `Pl0.Core` kompiliert werden.

7. `PF-IDE-007` Kompilierdialog:
   - Nach Kompilierung wird ein Dialog mit Ergebnisstatus angezeigt (Erfolg/Fehler).

8. `PF-IDE-008` Fehleranzeige:
   - Compilerdiagnosen werden gesammelt und in einem Meldungsfenster angezeigt.

9. `PF-IDE-009` P-Code-Fenster:
   - Der erzeugte P-Code wird in einem separaten Fenster dargestellt.

10. `PF-IDE-010` P-Code-Ausfuehrung:
    - Der erzeugte P-Code kann aus der IDE heraus ueber `Pl0.Vm` ausgefuehrt werden.
    - Laufzeitausgaben werden sichtbar dargestellt.

11. `PF-IDE-011` Dateioperationen:
    - Quellcode kann gespeichert und geladen werden.
    - Dazu werden die Standard-Dialoge von Terminal.GUI genutzt.

12. `PF-IDE-012` Quelltextformatierung:
    - Eine Funktion zur Formatierung des PL/0-Quellcodes ist vorhanden.

13. `PF-IDE-013` Kombi-Aktion:
    - Eine Aktion "Kompilieren und Ausfuehren" ist vorhanden. Auch hier Orientierung an der Turbo Pascal IDE für DOS.

14. `PF-IDE-014` Debugging:
    - Schrittweise P-Code-Ausfuehrung (Step) ist moeglich. Hierbei wird der Instruktion-Pointer (`P`) aktualisiert und auch sichtbar im Inhalt des Stacks nachvollziehbar gezeigt, wo im P-Code der aktuelle Ausführungspunkt liegt.
    - Nach jedem Schritt werden Stack und Register (`P`, `B`, `T`) aktualisiert angezeigt. 

15. `PF-IDE-015` Hilfe:
    - Eine Hilfe-Funktion mit Bedienhinweisen der IDE ist in der IDE verfuegbar.
    - Eine Hilfe-Funktion für die Sprache PL0 ist in der IDE verfuegbar.

16. `PF-IDE-016` Integrierte Dokumentation:
    - Eine integrierte Dokumentationsansicht (lokale Inhalte) steht in der IDE bereit.

17. `PF-IDE-017` Testfaelle:
    - Fuer die IDE-Funktionen werden automatisierte Tests erstellt (xUnit).
    - Die Tests decken mindestens die Kernablaeufe ab: Laden/Speichern, Kompilieren, Diagnostikanzeige,
      P-Code-Anzeige, Ausfuehren sowie Schritt-Debugging mit Register-/Stackaktualisierung.
    - Die Tests sind in `dotnet test` integriert und laufen reproduzierbar.

18. `PF-IDE-018` Compiler-Einstellungsdialog:
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

      | Option | Typ | Vorgeschlagener Bereich | Default |
      |---|---|---|---|
      | `Dialect` | Auswahl | `Classic`, `Extended` | `Extended` |
      | `MaxLevel` | Ganzzahl | `0..10` (Schrittweite `1`) | `3` |
      | `MaxAddress` | Ganzzahl | `127..32767` (Schrittweite `1`) | `2047` |
      | `MaxIdentifierLength` | Ganzzahl | `1..32` (Schrittweite `1`) | `10` |
      | `MaxNumberDigits` | Ganzzahl (abgeleitet) | `10` oder `14` abhaengig von `Dialect` | `14` bei `Classic`, `10` bei `Extended` |
      | `MaxSymbolCount` | Ganzzahl | `10..5000` (Schrittweite `10`) | `100` |
      | `MaxCodeLength` | Ganzzahl | `50..10000` (Schrittweite `50`) | `200` |

    - Werte ausserhalb des Bereichs werden im Dialog validiert und nicht uebernommen.
    - Sonderregel `MaxNumberDigits`:
      - Bei `Dialect = Classic` wird `MaxNumberDigits = 14` gesetzt (historische Kompatibilitaet).
      - Bei `Dialect = Extended` wird `MaxNumberDigits = 10` gesetzt (technisch passend zu `int`).
    - Geaenderte Werte werden fuer den naechsten Kompiliervorgang verwendet.

19. `PF-IDE-019` Tastatur-Shortcuts:
    - Hauptfunktionen der IDE sind ueber Tastatur-Shortcuts erreichbar.
    - Die Belegung orientiert sich an klassischen IDE-Mustern (z. B. Build, Run, Step).

20. `PF-IDE-020` Persistenz:
    - Die IDE speichert mindestens die zuletzt geoeffnete Datei und Fenstergroessen.
    - Die gespeicherten Werte werden beim naechsten Start wiederhergestellt.

21. `PF-IDE-021` P-Code-Export (Emit-Modi):
    - Die IDE bietet eine Exportfunktion fuer den erzeugten P-Code.
    - Die Export-Modi orientieren sich an `src/Pl0.Cli/Cli/EmitMode.cs`:
      - `Asm` (mnemonische Darstellung)
      - `Cod` (numerischer Maschinen-Code)
    - Die Exportformate orientieren sich an `src/Pl0.Cli/Cli/CompilerCliOptions.cs` und den bestehenden Core-Serialisierern.
    - Fuer `Cod` wird ausschliesslich die Dateiendung `.cod` verwendet.
    - Der Export ist nur nach erfolgreicher Kompilierung moeglich.

22. `PF-IDE-022` Web-Hilfe ueber Kestrel (Hilfe-Menue):
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
    - Falls `_site` nicht vorhanden ist oder der Start fehlschlaegt, zeigt die IDE eine klare Fehlermeldung im Dialog.
    - Kestrel-Konsolenausgaben duerfen die IDE-Ansicht nicht stoeren (keine UI-Glitches durch direkte Ausgabe auf das IDE-Terminal).

## 6. Nicht-funktionale Anforderungen
- `NF-001` Stabilitaet: Fehler im Quelltext duerfen nicht zum Absturz der IDE fuehren.
- `NF-002` Diagnostikmodell: Compilerfehler werden als Diagnosen angezeigt, nicht per ungefangener Exception.
- `NF-003` Bedienbarkeit: Hauptfunktionen sind vollstaendig per Tastatur erreichbar.
- `NF-004` Performance: Interaktive Reaktion bei Standardbeispielen ohne merkliche Verzoegerung.
- `NF-005` Wartbarkeit: Klare Trennung von UI, Compiler-/VM-Adapter und Anwendungslogik.
- `NF-006` Didaktik: Oberflaeche und Meldungen sind fuer Lernende nachvollziehbar formuliert.
- `NF-007` Hintergrunddienste wie der Hilfe-Webserver laufen ohne stoerende Konsolenausgaben in der IDE-Ansicht.

## 7. Bedien- und Prozessablauf (Soll)
1. Benutzer erstellt/oeffnet eine `.pl0`-Datei im Editorfenster.
2. Benutzer oeffnet bei Bedarf den Compiler-Einstellungsdialog; bei Dialektwechsel setzt die IDE `MaxNumberDigits` automatisch (`Classic=14`, `Extended=10`).
3. Benutzer startet Kompilierung.
4. IDE zeigt Dialogstatus und schreibt Diagnosen ins Meldungsfenster.
5. Bei Erfolg wird P-Code im P-Code-Fenster aktualisiert.
6. Benutzer waehlt:
   - Ausfuehren (Gesamtlauf), oder
   - Debuggen (Schrittbetrieb mit Register-/Stackanzeige).
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

## 9. Testfaelle und Anforderungszuordnung

Die folgenden Testfaelle sind als automatisierte xUnit-Tests umzusetzen.

| Testfall-ID  | Kurzbeschreibung                                                                 | Zuordnung Anforderungen                                |
|--------------|----------------------------------------------------------------------------------|--------------------------------------------------------|
| `TC-IDE-001` | Projekt `Pl0.Ide` ist in Solution eingebunden und buildet erfolgreich.           | `PF-IDE-001`                                           |
| `TC-IDE-002` | IDE startet mit Hauptlayout (Editor, P-Code, Meldungen).                         | `PF-IDE-002`, `PF-IDE-003`, `PF-IDE-004`, `PF-IDE-009` |
| `TC-IDE-003` | PL/0-Syntaxelemente (Schluesselwoerter, Zahlen, Operatoren) werden im Editor im Turbo-Pascal-orientierten Farbstil hervorgehoben. | `PF-IDE-005` |
| `TC-IDE-004` | Kompilierung eines gueltigen Programms liefert Erfolg und zeigt Kompilierdialog. | `PF-IDE-006`, `PF-IDE-007`                             |
| `TC-IDE-005` | Kompilierung eines fehlerhaften Programms zeigt Diagnosen im Meldungsfenster.    | `PF-IDE-008`                                           |
| `TC-IDE-006` | Nach erfolgreicher Kompilierung wird P-Code im P-Code-Fenster angezeigt.         | `PF-IDE-009`                                           |
| `TC-IDE-007` | Ausfuehrung von P-Code liefert sichtbare Laufzeitausgabe.                        | `PF-IDE-010`                                           |
| `TC-IDE-008` | Laden und Speichern von `.pl0`-Dateien funktioniert verlustfrei ueber die Standard-Dialoge von `Terminal.Gui`. | `PF-IDE-011` |
| `TC-IDE-009` | Formatieren eines Quelltexts liefert deterministisches Ergebnis.                 | `PF-IDE-012`                                           |
| `TC-IDE-010` | Aktion "Kompilieren und Ausfuehren" durchlaeuft beide Schritte erfolgreich.      | `PF-IDE-013`                                           |
| `TC-IDE-011` | Schritt-Debugging aktualisiert pro Step Register (`P`, `B`, `T`) und Stack; der aktuelle Ausfuehrungspunkt ist im P-Code ueber `P` sichtbar. | `PF-IDE-014` |
| `TC-IDE-012` | Hilfe zur IDE-Bedienung, Hilfe zur Sprache PL/0 und integrierte Dokumentation sind ueber die IDE aufrufbar. | `PF-IDE-015`, `PF-IDE-016` |
| `TC-IDE-013` | Gesamte IDE-Test-Suite laeuft erfolgreich mit `dotnet test`.                     | `PF-IDE-017`                                           |
| `TC-IDE-014` | Einstellungsdialog validiert Wertebereiche fuer `CompilerOptions`, setzt `MaxNumberDigits` dialektabhaengig (`Classic=14`, `Extended=10`) und uebergibt die gesetzten Werte an den naechsten Kompiliervorgang. | `PF-IDE-018` |
| `TC-IDE-015` | Exportfunktion schreibt P-Code in den Modi `Asm` und `Cod`; fuer `Cod` wird ausschliesslich `.cod` akzeptiert/verwendet. | `PF-IDE-021` |
| `TC-IDE-016` | Kernfunktionen (mindestens Build, Run, Step) sind ueber definierte Tastatur-Shortcuts ausloesbar. | `PF-IDE-019` |
| `TC-IDE-017` | Zuletzt geoeffnete Datei und Fenstergroessen werden gespeichert und beim Neustart wiederhergestellt. | `PF-IDE-020` |
| `TC-IDE-018` | Hilfe-Menuepunkt startet Kestrel fuer `_site`, zeigt einen Hinweis-Dialog mit der tatsaechlichen URL und stoppt den Server beim zweiten Aufruf. | `PF-IDE-022` |
| `TC-IDE-019` | Bei fehlendem `_site` oder Startfehler wird ein Fehlerdialog angezeigt; die IDE bleibt bedienbar. | `PF-IDE-022` |
| `TC-IDE-020` | Beim Betrieb der Web-Hilfe treten keine stoerenden Konsolenausgaben/Renderartefakte in der IDE-Ansicht auf. | `PF-IDE-022`, `NF-007` |
| `TC-IDE-021` | Ist `localhost:5000` belegt, startet die Web-Hilfe automatisch auf dem naechsten freien Port im Bereich `5001..5099`, und der Hinweis-Dialog zeigt die tatsaechlich verwendete URL. | `PF-IDE-022` |

Hinweise zur Umsetzung:
- Tests mit UI-Bezug sollen ueber testbare ViewModel-/Controller-Logik und Adapter abstrahiert werden.
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
- `R-001`: `Terminal.Gui` `1.9.x` kann je nach Plattform unterschiedliche Terminal-Eigenheiten zeigen.
- `R-002`: Schritt-Debugging erfordert ggf. Erweiterungen an VM-Observability-Schnittstellen.
- `R-003`: Eine robuste Quelltextformatierung fuer alle Grenzfaelle kann iterative Nachschaerfung benoetigen.

Annahmen:
- Das bestehende Compiler- und VM-Verhalten bleibt fachlich unveraendert.
- IDE-Dokumentationsinhalte koennen aus vorhandenen Repository-Dokumenten kuratiert werden.
