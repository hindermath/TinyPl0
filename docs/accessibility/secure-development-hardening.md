# Barrierefreiheit: Secure-Development-Härtung

## Deutsch

### Umfang und Ergebnis

Die Prüfung verwendet WCAG 2.2 Level AA als praktische Basis. Sie umfasst die
geänderten Markdown-Texte, VM-Diagnosen, CVD-Informationen, Skript- und
Hilfetexte sowie die erzeugten DocFX-Seiten. Bedeutung wird durch Überschriften,
Listen, Tabellen, Statuswörter und beschreibende Links vermittelt. Farbe,
Zeigerbedienung oder räumliche Anordnung sind keine alleinigen Informationsträger.

Die Nutzer- und Lerntexte stehen Deutsch zuerst und Englisch danach. Die Sätze
bleiben auf B2-Niveau, nennen Ursache, Grenze und nächste sichere Aktion. Die
neuen VM-Diagnosen 207 und 208 sind lokalisiert, enthalten keine Stack-Traces
oder internen Pfade und bleiben in textbasierten Ausgaben verständlich.

### Ausführbare HTML- und Textbrowser-Evidence

Der autorisierte Host-Zyklus verwendete Node `v24.20.0`, npm `11.19.0` und
Playwright `1.62.1`. `npm --prefix tests/a11y ci` endete mit Exit `0`,
installierte sechs Pakete, prüfte sieben Pakete und meldete null Schwachstellen.
Der kontrollierte Harness band einmalig nur an `127.0.0.1:8080` und führte
`npm --prefix tests/a11y test -- --project=chromium` aus. Genau drei von drei
Seiten bestanden in 3,9 Sekunden; jede Seite besitzt null axe-Verletzungen:

- `/index.html`
- `/api/Pl0.Vm.VirtualMachineOptions.html`
- `/api/Pl0.Vm.VirtualMachine.html`

Das axe-JSON hat SHA-256
`b01856d34bacb4215a958b0382add5ea44a87e2d1e6e77a2e2457adb45a8f23b`.
Das Testlog hat SHA-256
`c7e1438448b0217f8da02beed0a35a5056e37efe2b06be4b611913dbd00eee66`.

Der getrennte Lynx-Pfad bestätigt eine verständliche, farbunabhängige
Textreihenfolge. Der 3.980 Byte große Options-Dump enthält
`VirtualMachineOptions`, `InstructionBudget` und `StackSize`; sein SHA-256 ist
`c5277e1ffa5ada8965bc76024afaec356ea10144a5d604312e0a894efce65797`.
Der 5.795 Byte große VM-Dump enthält `VirtualMachine`, `Run` und
`CultureNotFoundException`; sein SHA-256 ist
`063a98a71a1cf02c92e8305e5cc3ee44c8b80ece2706552f90c0e2d1a5aaeb82`.
Vor und nach dem Zyklus bestand kein fremder Listener. Danach blieb kein
Listener, Kindprozess oder eigener Server auf TCP 8080 aktiv.

### Weitere Flächen und Wiedervorlage

- Markdown und Security-Evidence sind semantisch gegliedert und text-first.
- CVD-Informationen nennen Kontakt, Scope, Reaktion und einen sicheren Meldeweg.
- PowerShell-, Bash-, Manpage- und Hilfetexte besitzen verständliche
  DE-/EN-Pfade; ihre bereits abgeschlossenen Paketvalidatoren bleiben getrennte
  Nachweise.
- CLI und IDE erhielten keine neue Bedienfläche. Deshalb wurden keine neuen
  Tastatur-, Fokus- oder Farbdialogtests ausgelöst.
- Eine Änderung an CLI/IDE, DocFX-Theme, Navigation, Fokusdarstellung,
  API-Vertrag oder Browser-Harness löst die Tastatur-/Fokus- und A11Y-Prüfung
  erneut aus. Die Remote-Prüfung am exakten PR-Head bleibt eine spätere
  Liefergrenze.

Die Dokumentationsauswirkung ist genau `UpdateRequired`. Die
maschinenlesbare Entscheidung liegt unter
`docs/documentation-impact/feature-004-secure-development-hardening.json`;
Home-Sync bleibt `false`.

## English

### Scope and result

The review uses WCAG 2.2 Level AA as its practical baseline. It covers changed
Markdown, VM diagnostics, coordinated-vulnerability-disclosure information,
script and help text, and generated DocFX pages. Headings, lists, tables,
status words, and descriptive links carry meaning. Colour, pointer use, and
spatial layout are never the only information channels.

User and learner text keeps German first and English second at B2 level. The
new VM diagnostics 207 and 208 are localized, disclose no stack trace or
internal path, and remain understandable in text output.

### Executable HTML and text-browser evidence

The authorised host cycle used Node `v24.20.0`, npm `11.19.0`, and Playwright
`1.62.1`. The pinned install exited `0`, and the controlled loopback harness
passed exactly three of three pages in 3.9 seconds. Every page has zero axe
violations. The axe JSON and test-log hashes are recorded above.

The independent Lynx path produced two non-empty dumps. Their required option,
budget, stack, run, and exception tokens prove a meaningful text order without
relying on colour. No listener, child process, or owned server remained on TCP
8080 after cleanup.

### Other surfaces and re-evaluation

Markdown, diagnostics, CVD, script, manpage, and help surfaces remain
text-first. The CLI and IDE gained no new interaction, so this change did not
trigger new keyboard, focus, or colour-dialog tests. Any later CLI/IDE, DocFX
theme, navigation, focus, API, or harness change triggers renewed keyboard,
focus, and accessibility review. Exact-PR-head remote evidence remains a later
delivery gate. The single documentation-impact decision is `UpdateRequired`,
with project-local distribution and no Home sync.
