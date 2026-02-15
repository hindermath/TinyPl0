# Beispiele & Tutorials

Willkommen zur Sammlung von **Beispielen und Tutorials** für TinyPl0. Diese Sektion bietet praktische Code-Beispiele für alle Lernstufen – von einfachen Programmen bis zu fortgeschrittenen Compilerbau-Konzepten.

## Struktur der Beispiele

Die Beispiele sind in mehrere Kategorien unterteilt, basierend auf ihrem Lernziel:

---

### 📚 Anhang-Programme

Eine umfangreiche Sammlung von PL/0-Programmen, die klassische Algorithmen und mathematische Konzepte demonstrieren:

- **Fibonacci-Berechnung:** Rekursive Prozeduren und Schleifen
- **Kreisberechnung:** Floating-Point-ähnliche Berechnungen mit Integers
- **Faktorielles:** Iterative und rekursive Ansätze
- **Primzahlen:** Sieb des Eratosthenes
- **Weitere Algorithmen:** Sortieren, Suchen, mathematische Funktionen

Diese Programme eignen sich hervorragend, um die Sprachmöglichkeiten von PL/0 kennenzulernen und gleichzeitig die VM zu testen.

> [!TIP]
> Weitere Anhang-Programme finden Sie im `appendix/`-Verzeichnis.

---

### 🎓 PL/0-Handbuch-Beispiele

Strukturierte Beispiele, die direkt zu den Kapiteln des **PL/0-Sprachreferenz-Handbuchs** korrespondieren:

- **Konstanten und Variablen:** `var`, `const` Deklarationen
- **Arithmetik und Vergleiche:** Operationen und Bedingungen
- **Kontrollfluss:** `if`, `while`, Schleifen
- **Prozeduren:** Deklaration, Aufruf, lokale Variablen
- **Ein- und Ausgabe:** `?` (Input) und `!` (Output) im extended Dialekt

Ideal für Anfänger, um Sprachkonzepte schrittweise zu erlernen.

> [!TIP]
> Weitere PL/0-Handbuch-Beispiele finden Sie im `handbook/pl0/`-Verzeichnis.

Siehe auch: [PL/0-Sprachreferenz](../handbook/pl0/index.md)

---

### ⚙️ P-Code-Handbuch-Beispiele

Detaillierte Beispiele, die die **Zwischendarstellung und P-Code-Instruktionen** erklären:

- **Einfache Instruktionen:** LIT, LOD, STO, ADD, SUB, etc.
- **Kontrollfluss:** JMP, JPC (Sprünge)
- **Prozeduraufrufe:** CALL, RET
- **Stack-Operationen:** Wie wird der Stack manipuliert?

Diese Beispiele zeigen, wie PL/0-Code in P-Code übersetzt wird und welche Instruktionen die VM ausführt.

> [!TIP]
> Weitere P-Code-Handbuch-Beispiele finden Sie im `handbook/pcode/`-Verzeichnis.

Siehe auch: [P-Code & VM Handbuch](../handbook/pcode/index.md)

---

### 🚀 P-Code-Tutorial

Ein schrittweises Tutorial, das den **Compilierungsprozess** von Anfang bis Ende demonstriert:

1. **Quelle → Tokens:** Lexikalische Analyse
2. **Tokens → Syntaxbaum:** Syntaxanalyse
3. **Syntaxbaum → P-Code:** Codegenerierung
4. **P-Code → Ausführung:** Virtuelle Maschine

Perfekt zum Verständnis, wie ein Compiler intern arbeitet.

> [!TIP]
> Weitere P-Code-Tutorial-Beispiele finden Sie im `handbook/tutorial/`-Verzeichnis.

Siehe auch: [P-Code-Tutorial](../handbook/tutorial/index.md)

---

### 💻 CLI-Nutzungs-Beispiele

Praktische Beispiele, die die **Kommandozeilen-Schnittstelle** demonstrieren:

- Kompilierung mit verschiedenen Optionen
- P-Code-Listing-Ausgabe für didaktische Zwecke
- Ausführung von kompilierten Programmen
- Verschiedene I/O-Modi

> [!TIP]
> Weitere CLI-Nutzungs-Beispiele finden Sie im `usage/`-Verzeichnis.

Siehe auch: [CLI-Dokumentation](../usage/cli.md)

---

## Schneller Einstieg

### Ein PL/0-Programm ausführen

```bash
dotnet run --project src/Pl0.Cli -- run examples/appendix/fibonacci.pl0
```

### P-Code-Listing anzeigen

```bash
dotnet run --project src/Pl0.Cli -- run examples/handbook/pl0/variables.pl0 --list-code
```

### Vorkompilierten P-Code ausführen

```bash
dotnet run --project src/Pl0.Cli -- run-pcode examples/handbook/pcode/simple_arithmetic.pcode.txt
```

---

## Empfohlene Lernreihenfolge

1. **Anfänger:** Starten Sie mit den PL/0-Handbuch-Beispielen (einfache Konzepte)
2. **Fortgeschrittene:** Durchlaufen Sie das P-Code-Tutorial (verstehen Sie den Compiler)
3. **Experte:** Analysieren Sie die P-Code-Handbuch-Beispiele (tief technisch)
4. **Praktizierend:** Erkunden Sie die Anhang-Programme (komplexe Algorithmen)

---

## Weitere Ressourcen

- [PL/0-Sprachreferenz](../handbook/pl0/index.md) – Vollständige Sprachreferenz
- [P-Code & VM Handbuch](../handbook/pcode/index.md) – Instruktionsset und VM-Verhalten
- [CLI-Dokumentation](../usage/cli.md) – Kommandozeilen-Optionen und Syntax
- [API-Referenz](../api/index.md) – Entwickler-API für erweiterte Nutzung

---

> [!TIP]
> **Tipp für Ausbildende:** Diese Beispiele können als Ausgangspunkt für eigene Übungsaufgaben verwendet werden. Modifizieren Sie die Programme, erweitern Sie sie mit neuen Funktionen, oder schreiben Sie eigene Programme von Grund auf!

> [!NOTE]
> Alle Beispiele wurden auf Korrektheit überprüft und funktionieren mit dem aktuellen Stand des Compilers und der VM.

