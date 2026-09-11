# Barrierearme verlinkte Intake-Evidence / Accessible Linked Intake Evidence

## Ergebnis und Prüfgrenze / Result and review boundary

Die beiden TinyPl0-Reihenfolgeansichten wurden am 11. September 2026 als
Quelltext und linearer Text geprüft. Jede Datenzeile enthält in stabiler
Reihenfolge Position, ausgeschriebenen Status, vollständigen Intake-Dateinamen,
jede direkte Abhängigkeit mit Art und Bindungswert sowie einen verlinkten oder
ausdrücklichen fehlenden Featurezustand. Keine Information hängt nur von Farbe,
Symbolform oder räumlichem Tabellenverständnis ab.

*The two TinyPl0 order views were reviewed as source and linear text on 11
September 2026. Every data row provides, in stable order, the position,
written status, complete intake filename, every direct dependency with kind
and binding value, and a linked or explicitly absent feature state. No
information depends solely on colour, symbol shape, or spatial table layout.*

Dies ist eine macOS-Quelltext- und Linearisierungsprüfung. Ein konkreter
Screenreader oder eine Braille-Zeile wurde nicht bedient; native Linux- und
Windows-Evidence folgt im Delivery-Checkpoint. Diese Grenzen werden nicht als
bereits erfüllt vorweggenommen.

## WCAG-2.2-AA-Disposition

| Kriterium | Anwendung | Nachweis / Grenze |
|---|---|---|
| 1.3.1 Information und Beziehungen | Eindeutige Überschrift, Einleitung und fünf benannte Felder. | Semantischer Paarvergleich und Fixture-Zeilen; keine HTML-Behauptung. |
| 1.4.1 Farbe | Status, Root, Kantenart, Bindungswert und Fallback stehen als Text. | Keine Farbe oder ANSI-Steuerung erforderlich. |
| 2.1.1 Tastatur | Markdown-Links und statischer Text verlangen keine Zeigeraktion. | Web- oder IDE-Navigation ist nicht Teil dieses Slices. |
| 2.4.4 Linkzweck | Vollständige Intake- und Feature-Namen bilden den Linktext. | Root- und Series-Fixtures prüfen relative Ziele. |
| 2.4.6 Überschriften und Beschriftungen | Deutsch steht vor Englisch; Zweck und Datenquelle sind benannt. | Beide generierten Ansichten verwenden dieselbe Dokumentvorlage. |
| 3.1.2 Sprache von Teilen | Deutscher Haupttext und direkt zugeordneter englischer Partnertext. | Technische Literale und Dateinamen bleiben unverändert. |
| 3.3.1 Fehlererkennung | `LIE001`–`LIE011` unterscheiden stabile Fehlerfamilien. | Negativkatalog und ausführbare Regressionstests. |

Mehrere direkte Kanten werden erst nach sicherer Linkerzeugung mit `<br>`
getrennt. Dateinamen und Statuswerte werden als Daten escaped. Lange Zeilen
bleiben in Textbrowsern umbrechbar; die vollständige Bedeutung bleibt nach
Linearisierung erhalten.

## Agent-Parity- und `.codex`-Disposition

Die Featurefunktion führt keine neue Agenten-, Routing-, Delivery- oder
Bedienregel ein. Deshalb bleiben `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`,
`.github/copilot-instructions.md`, `.github/agents/copilot-instructions.md`
und die vorhandenen `.codex/prompts/` unverändert. Das ist eine bewusste
`NoUpdateRequired`-Entscheidung, keine ausgelassene Spiegelung. Eine Änderung
dieser Flächen wäre nur bei einer neuen gemeinsamen Regel, einem neuen
Spec-Kit-Befehl, einer Prompt-Semantik- oder Routingänderung erforderlich.

*The feature introduces no new agent, routing, delivery, or operating rule.
The shared guidance files and existing `.codex/prompts/` therefore remain
unchanged as an explicit `NoUpdateRequired` decision. Re-evaluate when a
shared rule, Spec Kit command, prompt semantic, or routing behavior changes.*

Der Renderer erzeugt keine Credentials, Providerlogs, absoluten privaten
Pfade oder SQLite-Zustände. Owner ist der TinyPl0 Repository Owner; Reviewer
ist die Feature-032 A11Y-/Agent-Parity-Rolle. Restrisiko sind Unterschiede
realer assistiver Konfigurationen. Re-Evaluation bei Spalten-, Link-, Sprach-,
Diagnose-, HTML-, Medien- oder Interaktionsänderung.
