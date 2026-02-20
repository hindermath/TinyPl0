# Lastenheft IDE

## Anforderungen

- ein neuesProjekt 'src/Pl0.Ide' erstellen
- die IDE soll eine GUI haben, die auf dem Nuget-Paket Terminal.GUI Version 1.9.x basiert
- die IDE soll eine Syntax-Hervorhebung haben für die Schlüsselwort der Sprache Pl0
- der Pl0-Compiler soll in die IDE integriert sein, so dass eingegebener Quellcode direkt aus der IDE übersetzt werden kann
- wenn die Übersetzung erfolgt, wird das in einem Dialog angezeigt.
- die IDE soll eine Fehler-Meldungsfunktion haben, wenn der Compiler fehler gefunden hat. Diese werden in einem Meldungsfenster angezeigt.
- Es soll ein Fenster geben, in dem der Quellcode angezeigt wird.
- In einem weiteren Fenster wird der P-Code angezeigt
- Es soll die Möglichkeit geben, den P-Code auszuführen und das Ergebnis darzustellen
- Es soll die Möglichkeit geben, den Quellcode zu speichern und zu laden
- Es soll die Möglichkeit geben, den Quellcode zu formatieren
- Es soll die Möglichkeit geben, den Quellcode zu kompilieren und den P-Code auszuführen
- Es soll die Möglichkeit geben, den Quellcode zu debuggen. (u.a. auch ein Debugger integrieren und den P-Code Schritt für Schritt auszuführen, der Stack und die Register sollen bei jedem Schritt angezeigt werden)
- Die IDE soll angelehnt sein an die Turbo Pascal IDE für DOS
- Die IDE soll eine Hilfe-Funktion haben, die die Benutzer bei der Entwicklung unterstützt.
- Die IDE soll eine integrierte Dokumentation haben, die die Benutzer bei der Entwicklung unterstützt.

## Beispiel: Agentic-AI-Dialog (für Fachinformatiker-Auszubildende)

Dieser Abschnitt dokumentiert unseren Dialog als praxisnahes Beispiel für
iterative Anforderungsarbeit mit Agentic AI.

### Dialogverlauf (chronologisch, inhaltlich vollständig)

1. Benutzer: Bitte ein Pflichtenheft `Pflichtenheft_IDE.md` zum Lastenheft erstellen.
2. Agent: Lastenheft analysiert, bestehende Pflichtenheft-Struktur im Repository als Vorlage genutzt und neues Pflichtenheft erstellt ([Commit](https://github.com/hindermath/TinyPl0/commit/16d4242)).
3. Benutzer: Bitte Tests als Pflicht ergänzen.
4. Agent: Testanforderung in funktionale Anforderungen, Abnahmekriterien und Liefergegenstände aufgenommen ([Commit](https://github.com/hindermath/TinyPl0/commit/279a4c7)).
5. Benutzer: Bitte konkrete Testfall-IDs (`TC-IDE-001` ff.) mit Zuordnung ergänzen.
6. Agent: Traceability-Matrix mit Testfall-IDs und Zuordnung auf `PF-IDE-*` ergänzt ([Commit](https://github.com/hindermath/TinyPl0/commit/279a4c7)).
7. Benutzer: Nachfrage, ob das Pflichtenheft verständlich ist.
8. Agent: Verständnis bestätigt, keine offenen Fragen (kein separater Commit-Link).
9. Benutzer: Hinweis, dass später selbst gelesen wird.
10. Agent: Bereitschaft für weitere Änderungen signalisiert (kein separater Commit-Link).
11. Benutzer: Bitte prüfen, ob eigene Ergänzungen in Abschnitt 5.1 konsistent sind; bei Bedarf anpassen.
12. Agent: Konsistenzprüfung durchgeführt und Abnahmekriterien/Testfälle an die neuen 5.1-Details angepasst ([Commit](https://github.com/hindermath/TinyPl0/commit/279a4c7)).
13. Benutzer: Compiler-Steuerung aus `src/Pl0.Core/CompilerOptions.cs` soll per Einstellungsdialog konfigurierbar sein; sinnvolle Wertebereiche vorschlagen.
14. Agent: Optionen analysiert und Wertebereiche als neue Anforderung ergänzt, inkl. AK/TC ([Commit](https://github.com/hindermath/TinyPl0/commit/20afe91)).
15. Benutzer: `MaxNumberDigits` soll von `Dialect` abhängen (10 technisch, 14 historisch).
16. Agent: Regel umgesetzt: `Classic=14`, `Extended=10`; freie Eingabe für `MaxNumberDigits` entfernt; AK/TC angepasst ([Commit](https://github.com/hindermath/TinyPl0/commit/20afe91)).
17. Benutzer: Bitte dieselbe Regel auch im Prozessablauf sichtbar machen.
18. Agent: Prozessablauf um expliziten Dialekt-/`MaxNumberDigits`-Schritt erweitert ([Commit](https://github.com/hindermath/TinyPl0/commit/9cc2b17)).
19. Benutzer: Zusätzlich P-Code-Speicherung in mnemonischer Form oder Maschinencode; Orientierung an `CompilerCliOptions.cs` und `EmitMode.cs`; Vorschlag gewünscht.
20. Agent: Vorschlag gemacht (Exportdialog mit `Asm`/`Cod`, Nutzung `PCodeSerializer`, klare UX/Teststrategie) (kein separater Commit-Link).
21. Benutzer: Für `Cod` nur Endung `.cod`; bitte direkt als Anforderungen/AK/TC eintragen.
22. Agent: `PF-IDE-021` + `AK-015` + `TC-IDE-015` ergänzt; Prozessablauf erweitert ([Commit](https://github.com/hindermath/TinyPl0/commit/edfab74)).
23. Benutzer: Kann-Optionen aus Abschnitt 5.2 sollen Pflicht in 5.1 werden.
24. Agent: `PF-IDE-019` und `PF-IDE-020` nach 5.1 verschoben; 5.2 entfernt; passende AK/TC ergänzt ([Commit](https://github.com/hindermath/TinyPl0/commit/a403e3b)).
25. Benutzer: Hilfe-Menüpunkt für Web-Hilfe aus `_site` via Kestrel, mit Start/Stop-Toggle, URL-Hinweisdialog und ohne UI-Glitches; Orientierung an `--api` aus `src/Pl0.Cli`.
26. Agent: `PF-IDE-022` ergänzt, inklusive Toggle-Verhalten, Fehlerdialoge, Log-Isolation, `NF-007`, zusätzliche AK/TC ([Commit](https://github.com/hindermath/TinyPl0/commit/df0189d)).
27. Benutzer: Konkrete Portvorgabe ergänzen: `localhost:5000` mit Fallback bei Belegung.
28. Agent: Portregel ergänzt: Standard `5000`, Fallback `5001..5099`, sonst Fehlerdialog; `AK-020` und `TC-IDE-021` ergänzt ([Commit](https://github.com/hindermath/TinyPl0/commit/d1d5d32)).
29. Benutzer: Bitte gesamten Dialog als Beispielabschnitt in `Lastenheft_IDE.md` aufnehmen.
30. Agent: Dieser Abschnitt wurde eingefügt ([Commit](https://github.com/hindermath/TinyPl0/commit/b6e3e6d)).
31. Benutzer: Bitte für `PF-IDE-016` („Integrierte Dokumentation“) einen detaillierten Vorschlag machen.
32. Agent: Detaillierten Vorschlag geliefert (interne Dokuansicht, TOC, Suche, Offline-Nutzung, F1-Kontext, Abgrenzung zu Web-Hilfe) (kein separater Commit-Link).
33. Benutzer: Bitte direkt als ersetzbaren Textblock für Abschnitt 5.1 plus passende AK-/TC-Ergänzungen formulieren.
34. Agent: `PF-IDE-016` detailliert in `Pflichtenheft_IDE.md` ersetzt und passende Abnahmekriterien sowie Testfälle ergänzt ([Commit](https://github.com/hindermath/TinyPl0/commit/f458748)).
35. Benutzer: Zuerst den Vorschlag für Priorisierung der Dokuquellen zeigen.
36. Agent: Priorisierungs-Vorschlag gezeigt (hoch/mittel/ergänzend, Startseite, Fallback-Regel) (kein separater Commit-Link).
37. Benutzer: Bitte diesen Vorschlag direkt in `Pflichtenheft_IDE.md` eintragen.
38. Agent: Priorisierung der Dokuquellen in `PF-IDE-016` eingetragen und um `AK-023` sowie `TC-IDE-024` ergänzt ([Commit](https://github.com/hindermath/TinyPl0/commit/c097fb4)).
39. Benutzer: Wenn möglich, hinter Agenten-Antworten die URLs der eingecheckten GitHub-Commits ergänzen.
40. Agent: Commit-URLs hinter den Agenten-Antworten ergänzt und transparente Markierung für Antworten ohne separaten Commit gesetzt ([Commit](COMMIT_A_URL)).
41. Benutzer: Bitte die Reihenfolge aller `PF-IDE-*` in Abschnitt 5.1 prüfen und eine neue Reihenfolge zur Ansicht zeigen.
42. Agent: Neue, workflow-orientierte Reihenfolge vorgeschlagen (Anzeige zur Abnahme) (kein separater Commit-Link).
43. Benutzer: Bitte die neue Reihenfolge direkt einpflegen, Querverweise in AK/TC bei Bedarf nachziehen und fehlende Dialoge im Lastenheft mit Commit-URLs ergänzen.
44. Agent: Reihenfolge in Abschnitt 5.1 umgesetzt und fehlende Dialogeinträge im Lastenheft ergänzt ([Commit](COMMIT_A_URL)).

### Didaktischer Nutzen

- Zeigt, wie Anforderungen iterativ präzisiert werden.
- Zeigt die Kopplung von Anforderungen, Abnahmekriterien und Testfällen (Traceability).
- Zeigt, wie technische Randbedingungen früh als verbindliche Regeln dokumentiert werden.
- Zeigt, wie Agentic AI als Co-Engineer arbeitet: analysieren, anpassen, verifizieren, nachziehen.
