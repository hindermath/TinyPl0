# Projektstatistik TinyPl0

Stand: 2026-09-02 (autonomer Lauf 006: Embeddable VM und NuGet)

## Zweck und Pflege

Diese Datei ist das fortlaufende Statistik-Register fuer TinyPl0. Sie wird
nach jeder abgeschlossenen Spec-Kit-Implementierungsphase, nach jeder
agentischen Aenderung am Repository und auf explizite Anforderung
fortgeschrieben.

## Methodik

- Quellen: Git-Historie, sichtbare Branch-Referenzen und aktueller Dateistand.
- Ausgeschlossen: `.codex/`, `_site/`, `api/`, `bin/`, `obj/` sowie sonstige
  generierte Artefakte.
- Produktionscode: `src/**/*.cs`
- Testcode: `tests/**/*.cs`
- Dokumentation: Markdown-Dateien in Repository-Wurzel, `docs/`, `specs/`,
  `.github/`, `.specify/`, `examples/` und `docfx/`.
- Die konservative Handarbeits-Basis in dieser Datei zaehlt Produktionscode,
  Testcode und Dokumentation gemeinsam als manuell zu erstellenden Umfang.
- Die konservative Handarbeits-Basis folgt dem Beitrag
  [Adapt or Disappear: How AI Turned a 2-Year Project into a 1-Week Sprint](https://www.holgerscode.com/blog/2026/02/23/adapt-or-disappear-how-ai-turned-a-2-year-project-into-a-1-week-sprint/#a-note-on-the-orm-29000-lines-you-never-have-to-write):
  maximal 80 manuell erstellte Zeilen pro Arbeitstag fuer einen erfahrenen
  Entwickler.
- Umrechnung in Zeitraeume:
  durchschnittlich 21.5 Arbeitstage pro Monat (Mittel aus 21-22 Arbeitstagen);
  unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr bis einschliesslich 2026 und
  31 Urlaubstagen pro Jahr ab 2027 (jeweils 5-Tage-Woche) ergeben sich
  `21.5 * 12 - 30 = 228` produktive Arbeitstage pro Jahr fuer Zeitraeume bis
  2026 bzw. `21.5 * 12 - 31 = 227` produktive Arbeitstage pro Jahr ab 2027.
- TVoeD-Stundenbasis in dieser Datei:
  `7.8 Stunden` bzw. `7 Stunden 48 Minuten` pro Arbeitstag fuer zusaetzliche
  Stundenumrechnungen.
- Abgeleitete Formeln in dieser Datei:
  Einzelentwickler `((Produktionscode + Testcode + Dokumentation) / 80)`;
  3er-Team `Einzelentwickler / 3 * 1.2` mit 20 % Koordinationsaufschlag.
- Zusatzannahmen fuer die erfahrungsadjustierte Thorsten-Referenz:
  - Allgemeiner Expertenaufschlag `* 1.25`, weil Thorsten seit Februar 1985
    mehr als 40 Jahre Softwareentwicklungspraxis einbringt und seit 2001 mit
    .NET/C# arbeitet.
  - Legacy-Portierungsaufschlag `* 1.25`, weil TinyPl0 als Pascal-basierte
    Portierung von 10 bis 15 Jahren Turbo-Pascal-/Legacy-Erfahrung profitiert.
  - Daraus ergibt sich fuer TinyPl0 eine erfahrungsadjustierte Solo-Referenz
    von `80 * 1.25 * 1.25 = 125` manuell erstellten Zeilen pro Arbeitstag.
- Beschleunigungsfaktoren vergleichen Referenz-Arbeitstage mit sichtbaren
  `Git-Aktivtagen`. Sie sind als repo-weiter Output-zu-Aktivtag-Indikator zu
  lesen und nicht als exakte Zeiterfassung.

## Erfahrungsprofil und Beschleunigungsmodell

- Referenzprofil fuer die erfahrungsadjustierte Zweitrechnung:
  - mehr als 40 Jahre Softwareentwicklung seit Februar 1985
  - langjaehrige .NET-/C#-Praxis seit 2001
  - 10 bis 15 Jahre Turbo-Pascal-/Legacy-Portierungserfahrung
- Neben der konservativen 80-Zeilen-Referenz fuehrt TinyPl0 daher eine zweite
  Thorsten-Solo-Referenz mit `125 Zeilen/Arbeitstag`.
- Die Beschleunigungsfaktoren beantworten die repo-weite Verdichtungsfrage
  gegenueber einer klassischen Portierung mit demselben fachlichen Zielbild.

## Gesamtstand des Repositories

| Kennzahl | Wert |
|---|---:|
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-05-05 |
| Git-Commits gesamt | 252 |
| Autoren laut Git | 2 |
| Git-Aktivtage | 43 |
| Produktionscode aktuell | 88 Dateien / 6950 Zeilen |
| Testcode aktuell | 22 Dateien / 3536 Zeilen |
| Dokumentation aktuell | 562 Dateien / 36625 Zeilen |
| Davon Spec-Kit-Artefakte | 155 Dateien / 11436 Zeilen |
| Davon Governance/Agent-Dateien | 5 Dateien / 2263 Zeilen |
| Gesamtbasis fuer Handschaetzung (inkl. Dokumentation) | 47111 Zeilen |
| Erfahrener Entwickler, konservative Untergrenze | 588.9 Arbeitstage |
| Erfahrener Entwickler, konservative Untergrenze in Stunden | 4593.3 Stunden (588.9 * 7.8) |
| Erfahrener Entwickler, brutto | 27.4 Arbeitsmonate (21.5 Tage/Monat) |
| Erfahrener Entwickler, TVoeD-Annahme | 31.0 Kalendermonate bzw. 2.6 Jahre |
| Thorsten solo, erfahrungsadjustierte Untergrenze | 376.9 Arbeitstage |
| Thorsten solo, erfahrungsadjustierte Untergrenze in Stunden | 2939.8 Stunden (376.9 * 7.8) |
| Thorsten solo, brutto | 17.5 Arbeitsmonate (21.5 Tage/Monat) |
| Thorsten solo, TVoeD-Annahme | 19.8 Kalendermonate bzw. 1.7 Jahre |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 235.6 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), TVoeD-Annahme | 12.4 Kalendermonate |
| Repo-weiter Beschleunigungsfaktor vs. konservative Referenz | 13.7x (588.9 / 43 Git-Aktivtage) |
| Repo-weiter Beschleunigungsfaktor vs. Thorsten-Referenz | 8.8x (376.9 / 43 Git-Aktivtage) |

## Branch-Ueberblick

| Branch/Ref | Letzte sichtbare Aktivitaet | Einordnung |
|---|---|---|
| `002-vm-inc-compat` | 2026-03-27 | Aktiver Feature-Branch fuer die SPEC-Kit-Spezifikation zur `Inc`-/`Int`-VM-Kompatibilitaet |
| `codex/constitution-preset-sync` | 2026-05-05 | Arbeitsbranch fuer die Governance-Pruefung nach Installation der sechs neuen Spec-Kit-Presets |
| `main` | 2026-03-15 | Integrationsbranch |
| `origin/main` | 2026-03-15 | Remote-Tracking-Branch |

Hinweis: Im aktuellen Git-Stand sind keine laenger lebenden Feature-Branches
mehr sichtbar. Die folgenden Phasen wurden deshalb aus der Commit-Historie
rekonstruiert.

## Rekonstruierte Phasen und grundlegende Arbeiten

### 0. Compiler-Basis

- Status: abgeschlossen und in `main` enthalten
- Beobachtbarer Zeitraum: 2026-02-08 bis 2026-02-10
- Commit-Bild: 19 Commits an 3 Git-Aktivtagen
- Grundlegende Arbeiten: Grundgeruest, Lexer, Parser, Symboltabelle,
  Codegenerator, VM, CLI, Katalogtests, Architekturtests und erste
  Pflichtenheft-Abdeckung
- Git-Aenderungsvolumen netto:
  - Produktionscode: 2297
  - Testcode: 1313
  - Dokumentation: 1698
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 5308 Zeilen netto gesamt
  - 66.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 3.1 Arbeitsmonate brutto bzw. 3.5 TVoeD-Kalendermonate
  - 26.5 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    1.4 TVoeD-Kalendermonaten

### 1. Dokumentations- und API-Welle

- Status: abgeschlossen und in `main` enthalten
- Beobachtbarer Zeitraum: 2026-02-11 bis 2026-02-19
- Commit-Bild: 78 Commits an 7 Git-Aktivtagen
- Grundlegende Arbeiten: DocFX, API-Referenzen, Handbuecher, Beispiele,
  Lasten-/Pflichtenhefte, XML-Dokumentation und CLI-/Web-Help-Dokumentation
- Git-Aenderungsvolumen netto:
  - Produktionscode: 1066
  - Testcode: 44
  - Dokumentation: 11366
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 12476 Zeilen netto gesamt
  - 156.0 Arbeitstage fuer einen erfahrenen Entwickler
  - 7.3 Arbeitsmonate brutto bzw. 8.2 TVoeD-Kalendermonate
  - 62.4 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    3.3 TVoeD-Kalendermonaten

### 2. IDE-Implementierungswelle

- Status: abgeschlossen und in `main` enthalten
- Beobachtbarer Zeitraum: 2026-02-20 bis 2026-02-22
- Commit-Bild: 42 Commits an 3 Git-Aktivtagen
- Grundlegende Arbeiten: `Pl0.Ide`, Terminal.Gui-v2-Integration, Editor,
  Syntax-Highlighting, Kompilieren/Ausfuehren, Exporte, Debug-Ansicht,
  Register-/Stack-Visualisierung und zugehoerige Tests
- Git-Aenderungsvolumen netto:
  - Produktionscode: 3073
  - Testcode: 1474
  - Dokumentation: 578
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 5125 Zeilen netto gesamt
  - 64.1 Arbeitstage fuer einen erfahrenen Entwickler
  - 3.0 Arbeitsmonate brutto bzw. 3.4 TVoeD-Kalendermonate
  - 25.6 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    1.3 TVoeD-Kalendermonaten

### 3. Governance, Spec-Kit und `001-l10n-backend`

- Status: abgeschlossen und in `main` enthalten
- Beobachtbarer Zeitraum: 2026-03-01 bis 2026-03-06
- Commit-Bild: 15 Commits an 3 Git-Aktivtagen
- Grundlegende Arbeiten: Agent-Dateien, Constitution, Spec-Kit-Templates,
  L10N-Backend-Featurephase, Dokumentationsrichtlinien und Test-/DocFX-Regeln
- Git-Aenderungsvolumen netto:
  - Produktionscode: 295
  - Testcode: 653
  - Dokumentation: 7731
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 8679 Zeilen netto gesamt
  - 108.5 Arbeitstage fuer einen erfahrenen Entwickler
  - 5.0 Arbeitsmonate brutto bzw. 5.7 TVoeD-Kalendermonate
  - 43.4 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    2.3 TVoeD-Kalendermonaten

### 4. Pages-, CI- und DocFX-Haertung

- Status: abgeschlossen und in `main` enthalten
- Beobachtbarer Zeitraum: 2026-03-14 bis 2026-03-15
- Commit-Bild: 21 Commits an 2 Git-Aktivtagen
- Grundlegende Arbeiten: GitHub-Pages-Pipeline, Node-24-Anpassungen,
  Smoke-Tests fuer Pages, DocFX-Bereinigung und Build-Haertung
- Git-Aenderungsvolumen netto:
  - Produktionscode: 0
  - Testcode: 0
  - Dokumentation: 14
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 14 Zeilen netto gesamt
  - 0.2 Arbeitstage fuer einen erfahrenen Entwickler
  - 0.0 Arbeitsmonate brutto bzw. 0.0 TVoeD-Kalendermonate
  - 0.1 Arbeitstage fuer ein 3er-Team (+20 % Koordination), entsprechend ca.
    0.0 TVoeD-Kalendermonaten
- Einordnung: vor allem CI-/Operations- und Prozesshaertung; die
  Zeilen-Schaetzung bleibt hier nur ein grober Naeherungswert.

### 5. Branch `002-vm-inc-compat` - SPEC-Kit-Spezifikation und Implementierung fuer `Inc`/`Int`

- Status: Implementierung und Verifikation im Arbeitsbaum des Feature-Branches
  abgeschlossen; noch nicht in `main`
- Beobachtbarer Zeitraum: 2026-03-27 bis 2026-03-27
- Commit-Bild: noch nicht in `main`, aktueller Aenderungssatz vor dem ersten Branch-Commit
- Grundlegende Arbeiten: Lastenheft fuer die historische `Inc`-Alias-Anforderung
  in eine SPEC-Kit-taugliche Feature-Beschreibung ueberfuehrt, `spec.md` fuer
  die VM-Kompatibilitaet erstellt, Qualitaets-Checkliste fuer die
  Planungsfreigabe angelegt, die Planungsartefakte (`plan.md`, `research.md`,
  `data-model.md`, `quickstart.md`, `contracts/pcode-text-contract.md`)
  erzeugt, die Planungs-Checkliste abgearbeitet, `tasks.md` fuer die
  Implementierungsphase erstellt und der Codex-Agent-Kontext auf den Branch
  aktualisiert
- Git-/Arbeitsbaum-Aenderungsvolumen fuer den aktuellen Aenderungssatz:
  - Produktionscode: 0 Zeilen
  - Testcode: 0 Zeilen
  - Dokumentation: 1557 Zeilen in bearbeiteten oder neu angelegten
    Markdown-Dateien inklusive Governance- und Statistik-Fortschreibung
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 1557 Zeilen netto gesamt
  - 19.5 Arbeitstage fuer einen erfahrenen Entwickler
  - 152.0 Stunden auf TVoeD-Basis (`19.5 * 7.8`)
  - 0.9 Arbeitsmonate brutto bzw. 1.0 TVoeD-Kalendermonate
- Thorsten-Solo-Referenz:
  - 12.5 Arbeitstage
  - 97.2 Stunden auf TVoeD-Basis (`12.5 * 7.8`)
  - 0.6 Arbeitsmonate brutto bzw. 0.7 TVoeD-Kalendermonate
- Blended Repository Speedup gegen sichtbare 1 Git-Aktivtag fuer diesen
  Aenderungssatz:
  - 19.5x gegen die konservative 80-Zeilen-Referenz
  - 12.5x gegen die Thorsten-Solo-Referenz mit 125 Zeilen pro Arbeitstag
- Planungsfortschritt innerhalb desselben Branch-Fensters:
  - Nach der Clarify-Runde wurden `plan.md`, `research.md`, `data-model.md`,
    `quickstart.md` und ein Vertragsartefakt unter `contracts/` angelegt bzw.
    fortgeschrieben.
  - Dadurch ist der Branch nicht mehr nur spezifiziert, sondern fuer die
    nachfolgende Task-Zerlegung technisch und organisatorisch vorbereitet.
  - Mit `checklists/planning-quality.md` wurde die Planungsqualitaet formell
    gegen die aktuellen Artefakte geprueft und dokumentiert.
  - Mit `tasks.md` liegt jetzt die vollstaendige, story-orientierte
    Implementierungszerlegung fuer die naechste Spec-Kit-Phase vor.
  - Nach einer nachgelagerten `speckit-analyze`-Pruefung wurden `plan.md` und
    `tasks.md` noch einmal gegen Constitution-, DocFX- und
    Statistikpflege-Luecken nachgeschaerft.
- Implementierungs- und Verifikationsfortschritt innerhalb desselben
  Branch-Fensters:
  - `src/Pl0.Core/PCodeSerializer.cs` akzeptiert jetzt ausschliesslich den
    historischen Alias `inc` zusaetzlich zu `int`, ohne die kanonische Ausgabe
    `int` oder numerisches Opcode-Parsing zu aendern.
  - `tests/Pl0.Tests/PCodeSerializerTests.cs` wurde um Baseline-,
    Alias-, Laufzeitgleichheits-, Kanonform- und Near-Miss-Regressionsfaelle
    erweitert; der gezielte Lauf ergab danach 19/19 gruene Tests.
  - `docs/VM_INSTRUCTION_SET.md` erklaert jetzt zweisprachig die Beziehung
    zwischen historischem `Inc`, internem `Int` und kanonischem `int`; der
    kuratierte Einstieg in `docfx/curated/vm-instruction-set.md` blieb
    unveraendert auf dieselbe Quelle verdrahtet.
  - Vor dieser Ledger-Fortschreibung betraegt der Netto-Aenderungssatz fuer die
    eigentliche Feature-Implementierung `+4` Produktionscode-Zeilen,
    `+92` Testcode-Zeilen und `+16` Dokumentationszeilen; in
    `specs/002-vm-inc-compat/tasks.md` wurden zusaetzlich 14 offene Aufgaben
    auf erledigt gesetzt, was netto 0 Dokumentationszeilen ergibt.
  - Daraus folgen fuer die manuelle Referenz `112 / 80 = 1.4`
    Arbeitstage bzw. `1.4 * 7.8 = 10.9` Stunden auf TVoeD-Basis sowie fuer die
    Thorsten-Solo-Referenz `112 / 125 = 0.9` Arbeitstage bzw.
    `0.9 * 7.8 = 7.0` Stunden.
  - Gegen sichtbare 1 Git-Aktivtag in diesem Branch-Fenster ergibt sich fuer
    diesen Implementierungssatz ein blended repository speedup von `1.4x`
    gegen die konservative 80-Zeilen-Referenz und `0.9x` gegen die
    Thorsten-Solo-Referenz.
  - Die Abschlussvalidierung verlief erfolgreich mit `docfx docfx.json`
    ohne Warnungen oder Fehler sowie `dotnet test` mit 264/264 gruenen Tests.
    Der `docfx`-Lauf regenerierte ausserdem ausgeschlossene `api/*.yml`-
    Artefakte, die gemaess Methodik nicht in die Handarbeitsbasis eingehen.

### 6. Constitution-/Preset-Synchronisierung nach sechs neuen Spec-Kit-Presets

- Status: im Arbeitsbranch `codex/constitution-preset-sync` fuer Commit und
  Push vorbereitet; noch nicht in `main`
- Beobachtbarer Zeitraum: 2026-05-05 bis 2026-05-05
- Commit-Bild: aktueller Governance-Aenderungssatz vor dem Branch-Commit
- Grundlegende Arbeiten: Pruefung der bestehenden Verfassung gegen die sechs
  neu installierten Presets (`a11y-governance`, `agent-parity-governance`,
  `architecture-governance`, `cross-platform-governance`,
  `isaqb-architecture-governance`, `security-governance`), Ergaenzung eines
  neuen allgemeinen Architektur-Grundsatzes in
  `.specify/memory/constitution.md`, Synchronisierung der Spec-Kit-Templates
  und Command-Templates sowie gleichlautende Nachpflege in `AGENTS.md`,
  `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md`.
- Git-/Arbeitsbaum-Aenderungsvolumen fuer den aktuellen Aenderungssatz vor
  dieser Ledger-Fortschreibung:
  - Produktionscode: 0 Zeilen
  - Testcode: 0 Zeilen
  - Dokumentation: 162 Zeilen netto in Governance-, Template- und
    Agent-Dateien
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 162 Zeilen netto gesamt
  - 2.0 Arbeitstage fuer einen erfahrenen Entwickler
  - 15.8 Stunden auf TVoeD-Basis (`2.0 * 7.8`)
  - 0.1 Arbeitsmonate brutto bzw. 0.1 TVoeD-Kalendermonate
- Thorsten-Solo-Referenz:
  - 1.3 Arbeitstage
  - 10.1 Stunden auf TVoeD-Basis (`1.3 * 7.8`)
  - 0.1 Arbeitsmonate brutto bzw. 0.1 TVoeD-Kalendermonate
- Blended Repository Speedup gegen sichtbare 1 Git-Aktivtag fuer diesen
  Aenderungssatz:
  - 2.0x gegen die konservative 80-Zeilen-Referenz
  - 1.3x gegen die Thorsten-Solo-Referenz mit 125 Zeilen pro Arbeitstag
- Vor dem Commit-/Push-Schritt wurden zusaetzlich die Versionsfelder in
  `src/Pl0.Ide/Pl0.Ide.csproj` auf den neuen Commit-Zielstand synchronisiert;
  diese Projektmetadatenpflege liegt ausserhalb der reinen
  Produktionscode-/Testcode-/Markdown-Taxonomie.

## Einordnung der KI-/Spec-Kit-Wirkung

- Die beobachtbare manuelle Gesamtbasis liegt bereits bei 29573 Zeilen
  (Produktionscode + Tests + Dokumentation).
- Selbst mit der konservativen Obergrenze von 80 manuell erstellten Zeilen pro
  Arbeitstag ergibt sich bereits eine Untergrenze von 369.7
  Entwickler-Arbeitstagen.
- Unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr entspricht das fuer einen
  erfahrenen Entwickler ca. 19.5 Kalendermonaten bzw. 1.6 Arbeitsjahren; fuer
  ein 3er-Team mit 20 % Koordinationsaufschlag ca. 7.8 Kalendermonaten.
- Unter Einbezug von Thorstens Erfahrungsprofil sinkt die klassische
  Solo-Referenz fuer TinyPl0 auf ca. 236.6 Arbeitstage bzw.
  12.5 TVoeD-Kalendermonate.
- Gegen die sichtbaren 20 Git-Aktivtage ergibt sich damit ein repo-weiter
  Beschleunigungsfaktor von ca. 18.5x gegen die konservative Referenz und
  ca. 11.8x gegen die erfahrungsadjustierte Thorsten-Referenz.
- Die Historie zeigt eine massive Verdichtung durch agentische KI und
  spec-getriebene Arbeit: hoher Output in Code, Tests, Spezifikation,
  Lehrdokumentation und Infrastruktur innerhalb weniger Wochen.

## Fortschreibungsprotokoll

| Datum | Ausloeser | Eintrag |
|---|---|---|
| 2026-03-21 | Erstanlage | Basisstatistik fuer TinyPl0 angelegt; sichtbare Branch-Referenzen ausgewertet, historische Phasen aus Commits rekonstruiert, Constitution, Templates und Agent-Dateien auf Pflegepflicht synchronisiert. |
| 2026-03-22 | Methodik-Update fuer Handarbeits-Schaetzung | Die Statistik rechnet Handarbeit jetzt auf Basis von Produktionscode, Testcode und Dokumentation gemeinsam; zusaetzlich werden Monatswerte auf Basis von 21.5 Arbeitstagen pro Monat sowie TVoeD-Kalenderwerte mit 30 Urlaubstagen pro Jahr ausgewiesen. |
| 2026-03-22 | Governance-Synchronisierung zur Statistiklogik | Constitution sowie die gemeinsamen Agent-Hinweise (`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`) wurden auf die neue Statistiklogik synchronisiert: Handarbeits-Schaetzung umfasst nun Code, Tests und Dokumentation gemeinsam; Monats- und TVoeD-Annahmen muessen explizit genannt werden. |
| 2026-03-22 | GitHub-Codex-Spec-Kit-Skills installiert | Die lokale Codex-Skill-Struktur `.agents/skills/` mit den neun `speckit-*`-Skills wurde aus TuiVision in TinyPl0 uebernommen, damit die Spec-Kit-Kommandos auch in diesem Repository direkt als Skills verfuegbar sind. |
| 2026-03-25 | Erfahrungsadjustierte Beschleunigungsrechnung erweitert | Die Statistik fuehrt jetzt zusaetzlich zur konservativen 80-Zeilen-Referenz eine explizite Thorsten-Solo-Referenz mit Legacy-Portierungsaufschlag; dieselbe Methodik wurde in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` synchronisiert. |
| 2026-03-25 | TVoeD-Stundenbasis ergänzt | Die Statistik weist zusaetzlich Stundenwerte auf Basis von `7,8 Stunden` bzw. `7 Stunden 48 Minuten` pro Arbeitstag aus; dieselbe Umrechnungsregel wurde in die gemeinsamen Agent-Dateien aufgenommen. |
| 2026-03-27 | TVoeD-Urlaubsregel ab 2027 nachgezogen | Die Statistik- und Agentenmethodik wurde auf die neue Stichtagsregel umgestellt: 30 Urlaubstage pro Jahr gelten nur bis einschliesslich 2026, ab dem Kalenderjahr 2027 werden unter TVoeD-Annahme 31 Urlaubstage bei unveraenderter 5-Tage-Woche verwendet. |
| 2026-03-27 | Agentische Spezifikationsarbeit auf `002-vm-inc-compat` | Lastenheft fuer die historische `Inc`-/`Int`-Kompatibilitaet zu einer SPEC-Kit-tauglichen Feature-Beschreibung umgebaut, `specs/002-vm-inc-compat/spec.md` und die Qualitaets-Checkliste angelegt, Branch-Einordnung und Handschaetzung fuer den Aenderungssatz fortgeschrieben. |
| 2026-03-27 | Clarify-Runde fuer `002-vm-inc-compat` | Die Spezifikation wurde in mehreren Klarstellungsschritten an die reale TinyPl0-P-Code-Logik angepasst: Scope auf textuelle VM-/P-Code-Artefakte eingegrenzt, bestehende case-insensitive Eingabelogik fuer Mnemonics uebernommen, textuelle Ausgabe auf das aktuelle lowercase-Mnemonic `int` statt auf eine neue Schreibweise festgelegt und die Terminologie zwischen historischem `Inc`, internem `Int` und textuellem `int` bereinigt. |
| 2026-03-27 | Governance-Update zur IDE-Versionslogik | Die gemeinsame Agent-Governance (`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.specify/memory/constitution.md`) wurde so angepasst, dass fuer `Pl0.Ide` die Spec-Kit-Feature-/Branch-Nummer kuenftig sofort als kanonische PR-Nummer fuer `Minor` gilt, auch wenn die GitHub-PR noch nicht existiert. |
| 2026-03-27 | Sortierung des Fortschreibungsprotokolls vereinheitlicht | Die Eintraege im Fortschreibungsprotokoll wurden auf strikt chronologische Reihenfolge gebracht: aeltester Eintrag oben, juengster und zuletzt eingetragener Eintrag unten. Dieselbe Regel wurde in der gemeinsamen Agent-Governance fuer dieses Repository festgeschrieben. |
| 2026-03-27 | Planungsphase fuer `002-vm-inc-compat` | Auf Basis der geklaerten Spezifikation wurden die Spec-Kit-Planungsartefakte `plan.md`, `research.md`, `data-model.md`, `quickstart.md` und `contracts/pcode-text-contract.md` erzeugt, die Implementierungsschneise auf `PCodeSerializer`, Serializer-Tests und VM-Dokumentation eingegrenzt, der Codex-Agent-Kontext in `AGENTS.md` aktualisiert sowie die Statistik fuer den erweiterten agentischen Dokumentationssatz fortgeschrieben. |
| 2026-03-27 | Review- und Task-Phase fuer `002-vm-inc-compat` | Die Planungsartefakte wurden mit `checklists/planning-quality.md` formal geprueft und punktuell nachgeschaerft; anschliessend wurde `specs/002-vm-inc-compat/tasks.md` mit 14 umsetzbaren, story-orientierten Tasks fuer `PCodeSerializer`, `PCodeSerializerTests`, VM-Dokumentation, `docfx` und Statistikpflege erstellt. |
| 2026-03-27 | Analyse-Remediation fuer `002-vm-inc-compat` | Nach `speckit-analyze` wurden `specs/002-vm-inc-compat/plan.md` und `specs/002-vm-inc-compat/tasks.md` im Arbeitsbaum nachgeschaerft: DE-/EN-B2-Pflichten jetzt explizit, `docfx/curated/vm-instruction-set.md` als Touchpoint im Plan sichtbar, DocFX-Reihenfolge gegen spaete Doku-Aenderungen abgesichert und Statistikpflege auf die Implementierungsphase umformuliert; Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: 0 Produktionscode-Zeilen, 0 Testcode-Zeilen, +9 Dokumentationszeilen aus dem aktuellen Arbeitsbaum, konservative Handarbeits-Untergrenze 0.1 Arbeitstage bzw. 0.8 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.1 Arbeitstage bzw. 0.6 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat. |
| 2026-03-27 | Formale Status-Nachschaerfung fuer `002-vm-inc-compat` | `specs/002-vm-inc-compat/spec.md` wurde nach abgeschlossener Analyse- und Remediation-Runde von `Draft` auf `Ready for Implementation` gesetzt, damit Spezifikation, Plan und Tasks denselben Reifegrad signalisieren; Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: 0 Produktionscode-Zeilen, 0 Testcode-Zeilen, +1 Dokumentationszeile im Arbeitsbaum, konservative Handarbeits-Untergrenze 0.0 Arbeitstage bzw. 0.1 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.0 Arbeitstage bzw. 0.1 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat. |
| 2026-03-27 | Commit-/Push-Vorbereitung fuer `002-vm-inc-compat` | Vor dem angeforderten Commit/Pull-Request-Branch-Push wurden in `src/Pl0.Ide/Pl0.Ide.csproj` die Versionsfelder gemaess Governance auf `1.2.200.11` synchronisiert; dieser Schritt betrifft ein projektmetadatenbezogenes Build-Artefakt ausserhalb der Produktionscode-/Testcode-/Markdown-Taxonomie, plus diese Ledger-Fortschreibung mit +1 Dokumentationszeile im Arbeitsbaum, konservative Handarbeits-Untergrenze 0.0 Arbeitstage bzw. 0.1 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.0 Arbeitstage bzw. 0.1 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat. |
| 2026-03-27 | Implementierungs- und Verifikationsphase fuer `002-vm-inc-compat` | `src/Pl0.Core/PCodeSerializer.cs` akzeptiert jetzt den historischen Alias `inc` fuer `Opcode.Int`, `tests/Pl0.Tests/PCodeSerializerTests.cs` deckt Baseline-, Alias-, Laufzeitgleichheits-, Kanonform- und Near-Miss-Faelle ab, `docs/VM_INSTRUCTION_SET.md` erklaert die Bruecke `Inc` / `Int` / `int` zweisprachig, `specs/002-vm-inc-compat/tasks.md` wurde vollstaendig abgehakt und die Abschlussvalidierung lief mit 19/19 gezielten Serializer-Tests, `docfx docfx.json` ohne Warnungen/Fehler sowie `dotnet test` mit 264/264 gruenen Tests; Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: +4 Produktionscode-Zeilen, +92 Testcode-Zeilen, +16 Dokumentationszeilen netto, zusaetzlich 14/14 Task-Haken in `tasks.md` ohne Nettoeffekt, konservative Handarbeits-Untergrenze 1.4 Arbeitstage bzw. 10.9 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.9 Arbeitstage bzw. 7.0 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat; der DocFX-Lauf regenerierte ausserdem ausgeschlossene `api/*.yml`-Artefakte ausserhalb der Handarbeitsbasis. |
| 2026-03-28 | Lastenheft-Benennungsregel fuer implementierte Feature-Branches | Das umgesetzte Lastenheft wurde von `Lastenheft_VM_INC_OpCode.md` nach `Lastenheft_VM_INC_OpCode.002-vm-inc-compat.md` umbenannt, die Referenzen in `specs/002-vm-inc-compat/` nachgezogen und die neue Governance-Regel in `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` sowie `.github/copilot-instructions.md` festgeschrieben: Sobald ein dedizierter Feature-Branch die Anforderungen eines Lastenhefts umgesetzt hat, traegt die Lastenheft-Datei den Branch-Suffix zur dauerhaften Rueckverfolgbarkeit; Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: 0 Produktionscode-Zeilen, 0 Testcode-Zeilen, +9 Dokumentationszeilen netto im Arbeitsbaum zuzueglich einer Dateiumbenennung, konservative Handarbeits-Untergrenze 0.1 Arbeitstage bzw. 0.7 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.1 Arbeitstage bzw. 0.5 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat. |
| 2026-03-28 | Lastenheft-Benennungsregel rueckwirkend auf `001-l10n-backend` angewendet | Das bereits umgesetzte L10N-Lastenheft wurde von `Lastenheft_L10N.md` nach `Lastenheft_L10N.001-l10n-backend.md` umbenannt, die Verweise in `specs/001-l10n-backend/` und im Dokument selbst auf den neuen Dateinamen umgestellt und damit die neue Traceability-Regel auch fuer den ersten Spec-Kit-Feature-Branch rueckwirkend konsistent angewendet; Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: 0 Produktionscode-Zeilen, 0 Testcode-Zeilen, +6 Dokumentationszeilen netto im Arbeitsbaum zuzueglich einer Dateiumbenennung, konservative Handarbeits-Untergrenze 0.1 Arbeitstage bzw. 0.5 Stunden auf TVoeD-Basis, Thorsten-Solo-Referenz 0.0 Arbeitstage bzw. 0.3 Stunden, Monatsannahme weiterhin 21.5 Arbeitstage brutto bzw. 19.0 TVoeD-produktive Tage pro Kalendermonat. |
| 2026-03-30 | Inklusions-Leitsatz und DocFX-A11y-Baseline verankert | `README.md`, `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` wurden um den Leitsatz `Programmierung #include<everyone>` ergaenzt. Fuer TinyPl0 bedeutet das: Lernmaterialien, Handbuchtexte und erzeugte DocFX-/API-Dokumentation muessen fuer Braille-Zeile, Screenreader und Textbrowser nutzbar bleiben. Zusaetzlich ist fuer DocFX-basierte HTML-Dokumentation WCAG 2.2 AA als praktische Baseline vermerkt; nach jedem DocFX-Neubau soll ein textorientierter A11y-Review mit Playwright/axe und `lynx` erfolgen. Diese Runde war reine Governance-/Doku-Arbeit mit `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und ca. `+47` Dokumentationszeilen netto. Konservative Manualreferenz: 80 Zeilen/Tag = `0.6` Tage (ca. `4.7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0.4` Tage (ca. `3.0` Stunden); sichtbares Arbeitsfenster: 1 kurze Agentensitzung am 2026-03-30. |
| 2026-03-30 | Bilinguale Abschlusspruefung und A11Y-Gate in Pflichtenheften verankert | `Pflichtenheft_PL0_CSharp_DotNet10.md`, `Pflichtenheft_PL0_Dokumentation.md` und `Pflichtenheft_IDE.md` nennen jetzt ausdruecklich die Abschlusspruefpunkte fuer Bilingualitaet, CEFR-B2-Niveau und A11Y. Zusaetzlich ist fuer grosse normative Dokumente eine synchron gepflegte `.EN.md`-Parallelfassung als zulaessige, uebersichtlichere Lieferform dokumentiert. Fuer DocFX-basierte HTML-Dokumentation ist die A11Y-Pflicht mit WCAG 2.2 AA, textorientiertem Review nach `docfx` sowie Nutzbarkeit fuer Braille-Zeile, Screenreader und Textbrowser Teil der Abnahme. Diese Runde war reine Dokumentationsarbeit mit `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und ca. `+10` Dokumentationszeilen netto. Konservative Manualreferenz: 80 Zeilen/Tag = `0.1` Tage (ca. `1.0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0.1` Tage (ca. `0.6` Stunden); sichtbares Arbeitsfenster: 1 kurze Agentensitzung am 2026-03-30. |
| 2026-03-30 | Parent-Guidance bewusst auf repo-uebergreifende Regeln begrenzt | In den lokalen Guidance-Dateien von `TinyPl0` ist jetzt ausdruecklich vermerkt, dass `/Users/thorstenhindermann/RiderProjects/AGENTS.md` nur gemeinsame Basisregeln fuer mehrere Repositories traegt. Repository-spezifische Build-, Test-, Workflow-, Architektur- und Feature-Vorgaben bleiben bewusst in `TinyPl0` selbst und sind dort die spezifischere Autoritaet. Diese Runde war reine Dokumentationsarbeit mit `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und ca. `+10` Dokumentationszeilen netto. Konservative Manualreferenz: 80 Zeilen/Tag = `0.1` Tage (ca. `1.0` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0.1` Tage (ca. `0.6` Stunden); sichtbares Arbeitsfenster: 1 kurze Agentensitzung am 2026-03-30. |
| 2026-03-30 | Gemeinsame Governance- und Statistik-Baseline mit TuiVision abgeglichen | `AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md`, `README.md`, die drei Pflichtenhefte und `docs/project-statistics.md` wurden gegen die repo-uebergreifende Oberbasis aus `/Users/thorstenhindermann/RiderProjects/AGENTS.md`, die Merknote `project-statistics-tail-block.md` und den gemeinsamen Governance-/Doku-Stand von `TuiVision` abgeglichen. Nachgezogen wurden nur gemeinsame Regeln: `.EN.md` fuer grosse normative Dokumente bei kanonischer deutscher Fassung, formale Abschlusskriterien fuer DE-first/EN-second auf CEFR-B2 und fuer den dokumentierten A11Y-Nachweis nach `Programmierung #include<everyone>`, die DocFX-Folgepruefung mit Playwright/axe und `lynx`, text-first-A11Y fuer Braille-Zeile, Screenreader und Textbrowser sowie der finale `## Gesamtstatistik`-Block mit ASCII-Diagrammen. Zusaetzlich wurden fuer den anstehenden Branch-Commit die IDE-Versionsfelder in `src/Pl0.Ide/Pl0.Ide.csproj` auf `1.2.209.14` synchronisiert und der IDE-Worklog in `Pflichtenheft_IDE.md` fortgeschrieben. Diese Runde war reine Governance-/Doku-Arbeit mit `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und ca. `+135` Dokumentationszeilen netto zuzueglich Projektmetadatenpflege ausserhalb der Markdown-Taxonomie. Konservative Manualreferenz: 80 Zeilen/Tag = `1.7` Tage (ca. `13.2` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1.1` Tage (ca. `8.4` Stunden); sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-03-30. |
| 2026-05-05 | Constitution- und Preset-Sync nach Sechs-Preset-Integration | Die vorhandene `.specify/memory/constitution.md` wurde gegen die sechs neu installierten Spec-Kit-Presets geprueft. Ergebnis: A11Y-, Agent-Parity-, Cross-Platform-, Security- und Secure-Architecture-Governance waren bereits weitgehend gespiegelt; nachgezogen werden musste der allgemeine iSAQB-/arc42-Architektur-Grundsatz samt Evidenzpfad `docs/architecture/`. Dafuer wurden Constitution, Spec-Kit-Templates, Command-Templates sowie `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` synchron aktualisiert. Vor dem angeforderten Commit/Pull wurden ausserdem die IDE-Versionsfelder auf den neuen Zielstand `1.2.252.14` angehoben. Aenderungsumfang dieser Runde vor dieser Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+162` Dokumentationszeilen netto zuzueglich Projektmetadatenpflege ausserhalb der Markdown-Taxonomie. Konservative Manualreferenz: `2.0` Tage bzw. `15.8` Stunden; Thorsten-Solo-Referenz: `1.3` Tage bzw. `10.1` Stunden; sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-05-05. |
| 2026-06-05 | Didaktische Inline-Code-Kommentar-Haertung vorbereitet | `Lastenheft_Quellcode_Doku.md` wurde zum Specify-ready Intake fuer eine moderate Inline-Kommentar-Haertung ausgebaut. Der Lauf soll Compiler-, VM-, CLI-, IDE- und Test-Helfer-Flows pruefen, ohne Runtime-Verhalten zu aendern oder jede Methode zu kommentieren. `AGENTS.md`, `CLAUDE.md`, `GEMINI.md` und `.github/copilot-instructions.md` halten nun fest, dass neue oder geaenderte nicht-triviale Logik auf didaktischen Kommentarbedarf geprueft wird und Kommentare Warum, Trade-off, Randbedingung, historische Abweichung oder Proof-Grenze erklaeren muessen. Die IDE-Versionsfelder wurden fuer diesen Bot-Commit auf `1.2.262.14` fortgeschrieben. Validierung: Doku-/Guidance-Suchcheck und `git diff --check`; keine Build-/Test-/DocFX-Ausfuehrung, weil nur Lastenheft, Guidance, Statistik und Versionsmetadaten geaendert wurden. |
| 2026-06-18 | Claude-Code-Review fuer Release-Please-Bot freigegeben | Die `action_required` GitHub-Actions-Runs fuer Release-Please-PR `#29` wurden per `gh run rerun` neu gestartet. CI, Gitleaks, Agent-Secret-Scan, Homogeneity Check und Docs Pages liefen danach erfolgreich; der einzige echte Fehler lag im Workflow `Claude Code Review`, weil `anthropics/claude-code-action@v1` den ausloesenden `github-actions[bot]` ohne Allowlist ablehnt. `.github/workflows/claude-code-review.yml` erlaubt jetzt explizit `github-actions[bot]`, ohne `'*'` fuer alle Bots zu setzen. Aenderungsumfang vor dieser Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+1` Workflow-Konfigurationszeile, zusaetzlich Projektmetadatenpflege in `src/Pl0.Ide/Pl0.Ide.csproj` auf `1.2.269.14` und IDE-Worklog-Fortschreibung. Validierung: GitHub-Run-Logs/PR-Checks, Review-Thread-Pruefung ohne offene Kommentare und `git diff --check`; kein lokaler `dotnet build`/`dotnet test`, damit der Buildzaehler nicht ohne fachlichen Build-Lauf erhoeht wird. |
| 2026-06-19 | Lastenheft-Abarbeitungsreihenfolge fuer spaetere Spec-Kit-Laeufe | `Lastenheft_Abarbeitungsreihenfolge.md` wurde als sichtbarer Root-Arbeitsvorrat angelegt. Es ordnet alle vorhandenen Lastenhefte in aktive Spec-Kit-Reihenfolge, erledigte Referenzdokumente und bewusst blockierte Hochrisiko-Themen ein. Die aktive Reihenfolge startet mit Governance-/Security-/Kommentar- und Dokumentationslaeufen, fuehrt danach IDE-L10N/A11Y, Options-, VM-CLI- und IDE-PAsm/PCod-Arbeit und stellt Optimierung sowie CLR-Backend hinter ein explizites Architektur-/Governance-Gate. Aenderungsumfang vor dieser Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+74` Dokumentationszeilen netto, zusaetzlich Projektmetadatenpflege in `src/Pl0.Ide/Pl0.Ide.csproj` auf `1.2.270.14` und IDE-Worklog-Fortschreibung. Validierung: vollstaendige Lastenheft-Sichtung, Spec-Kit-Artefaktabgleich, YAML-/Build-unabhaengiger Dokumentationscheck via `git diff --check`; kein lokaler `dotnet build`/`dotnet test`, weil nur Dokumentation und Versionsmetadaten geaendert wurden. |
| 2026-06-19 | Lastenheft-Dateiname `CSharp` fuer PR-Review korrigiert | Die Copilot-Review-Anmerkung in PR `#32` wurde aufgegriffen: Die falsch geschriebene PL0-CSharp-Datei wurde per Git-Rename nach `Lastenheft_PL0_CSharp_DotNet10.md` umbenannt und die sichtbare Referenz in `Lastenheft_Abarbeitungsreihenfolge.md` aktualisiert. Aenderungsumfang vor dieser Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+2` Dokumentationszeilen netto zuzueglich einer Dateiumbenennung, ausserdem Projektmetadatenpflege in `src/Pl0.Ide/Pl0.Ide.csproj` auf `1.2.271.14` und IDE-Worklog-Fortschreibung. Validierung: repo-weite Altname-Suche, Git-Dateiliste, `git diff --check` und erneute PR-Check-Pruefung; kein lokaler `dotnet build`/`dotnet test`, weil nur Dokumentation und Versionsmetadaten geaendert wurden. |
| 2026-07-23 | Intake Authoring und Review | 13 aktive Alt-Intakes hashgebunden adoptiert und einen neuen IDE-L10N-Intake aus dem archivierten Feature-001-Restscope abgeleitet; 14/14 einzeln und als Serie `Ready`, ohne Produktcodeänderung. |
| 2026-07-26 | Requirements- und Intake-Bestand abgeglichen | Ein reproduzierbarer read-only Audit klassifiziert 14 aktive Intakes, zwei abgeschlossene Intakes, drei historische Referenz-Lastenhefte und drei Pflichtenheft-Baselines. Elf Intakes bleiben offen, Constitution ist teilweise erfuellt, Optimierung und CLR bleiben durch geltende Architekturregeln blockiert. Der getrennte Folge-PR soll die Baselines einfrieren und den IDE-Arbeitsverlauf aus `Pflichtenheft_IDE.md` herausloesen; kein Spec-Kit-Feature und keine Produkt-, API-, Abhaengigkeits- oder Laufzeitaenderung wurden gestartet. |
| 2026-07-26 | Requirements- und Intake-Struktur konsolidiert | Drei Pflichtenhefte wurden hashgebunden als read-only Baselines eingefroren. Vierzehn aktive, zwei abgeschlossene und drei historische Referenz-Intakes liegen nun getrennt; eine kanonische Serie, supersedierende Receipts und config-gesteuerte Bash-/PowerShell-/CI-Validatoren ersetzen den Root-Dateibestand. Künftige IDE-Einträge stehen in `docs/ide-worklog.md`. Constitution ist genau der eine `Eligible` Intake; Optimierung und CLR bleiben blockiert. |
| 2026-08-29 | Embeddable VM und NuGet-Pakete als Intake und Serienziel aufgenommen | Auf Branch `codex/embeddable-vm-nuget-intake` wurde der genehmigte TinyPl0-Intake fuer die Bibliothekspakete `TinyPl0.Core` und `TinyPl0.Vm` angelegt und an Position 10 hinter VM/CLI sowie vor der IDE-Erweiterung in die kanonische Serie aufgenommen. Gefordert sind eine begrenzte normale und schrittweise VM-Ausfuehrung, Cancellation, strukturierte Ergebnisse, isoliertes I/O und die direkte, versionsgleiche Veroeffentlichung beider stabiler Pakete auf NuGet.org samt Supply-Chain-Evidenz; lokaler Package-Bau und eine spaetere externe Veroeffentlichung bleiben getrennte Autoritaeten. Vorgaengermanifest und -receipt wurden byteidentisch archiviert, der Nachfolger supersediert sie mit erhaltener Lineage; Governance-Renderer, Alignment-Kardinalitaet und Coverage-Nachweis wurden auf 15 Ziele und zehn Gates nachgezogen. Aenderungsumfang vor dieser Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+705 / -53` Dokumentations-, Governance- und Automationszeilen einschliesslich unveraenderbarer Archivevidenz. Konservative Manualreferenz: 80 Zeilen/Tag = `8.8` Tage (ca. `68.7` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `5.6` Tage (ca. `44.0` Stunden); bei einem sichtbaren Aktivtag entspricht dies einem blended repository speedup von `8.8x` bzw. `5.6x`. Sichtbares Arbeitsfenster: 1 Agentensitzung am 2026-08-29. Validierung: Intake- und Serienvalidatoren in Bash und PowerShell, Gesamt-Alignment, JSON-Parsing, Hash-/Archivpruefung und `git diff --check`; kein `dotnet build`/`dotnet test`, weil keine Produkt-, Test-, API- oder Laufzeitaenderung erfolgte. |
| 2026-08-29 | Aktualisierte TinyPl0-Serie mit 15 Zielen formal reviewed | Der schema-1.1-Serienreview `a6c1acb6-b75e-4875-a968-e5afb90bb289` prueft alle 15 aktuellen Zielhashes, fuenf Wurzeln, zehn verbindliche Kanten, die interne VM/CLI-zu-Paket-zu-IDE-Uebergabe, den externen TinyCalc-Handoff und die Delivery-Authority-Grenzen. Ergebnis ist `NeedsRemediation`: Finding `IR001` bewertet mit `Medium`, dass der lernendenorientierte Embeddable-VM-/NuGet-Intake fortgeschrittene Begriffe wie SemVer, CancellationToken, SBOM, VEX, SLSA, STRIDE, CAPEC und Provenance nicht bei erster Verwendung auf CEFR-B2-Niveau erklaert. Es wurden keine Risiken akzeptiert und keine offenen Fragen erzeugt. Der fruehere 14-Ziele-Review wird durch ID und Artefakthashes explizit supersediert. Aenderungsumfang dieses Review-Artefaktsatzes vor der Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+173 / -29` Dokumentations-, Governance- und Automationszeilen. Konservative Manualreferenz: 80 Zeilen/Tag = `2.2` Tage (ca. `16.9` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `1.4` Tage (ca. `10.8` Stunden); bei einem sichtbaren Aktivtag entspricht dies einem blended repository speedup von `2.2x` bzw. `1.4x`. Sichtbares Arbeitsfenster: dieselbe Agentensitzung am 2026-08-29. Validierung: Review-Validatoren in Bash und PowerShell, vollstaendiges Requirements-Alignment in beiden Varianten, Generator-Reproduktion, JSON-Pruefung und `git diff --check`; kein Produkt-Build, Testlauf, DocFX-Lauf oder Intake-Ausfuehrungsstart. |
| 2026-08-29 | Review-Finding IR001 repariert und Serie vollstaendig neu geprueft | Der ausdruecklich autorisierte `speckit-intake-repair`-Lauf ergaenzte im Embeddable-VM-/NuGet-Intake einen deutsch-/englischsprachigen CEFR-B2-Begriffsblock fuer Hostvertrag, Run/Step-Paritaet, SemVer, CancellationToken, SBOM, VEX, Provenance/SLSA, STRIDE/CAPEC und OpenSSF Scorecard. Scope, Nicht-Ziele, Anforderungen, Abnahmeschwellen, Reihenfolge, Gates und Delivery Authority blieben unveraendert. Receipt, Manifest, Coverage und Review-Hashes wurden nachgezogen; der vollstaendige Nachfolgereview `78435231-e579-486f-8d80-8192781c127d` umfasst erneut 15 Ziele und schliesst mit `Ready`, null Findings, null akzeptierten Risiken und null offenen Fragen. Das `NeedsRemediation`-Ergebnis bleibt mit ID, Hashes, Finding und Reparaturautoritaet als Supersession-Evidenz erhalten. Aenderungsumfang dieses Reparatur- und Re-Review-Satzes gegen den unmittelbar vorherigen Arbeitsstand: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+65` Dokumentations-, Governance- und Automationszeilen netto. Konservative Manualreferenz: 80 Zeilen/Tag = `0.8` Tage (ca. `6.3` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0.5` Tage (ca. `4.1` Stunden); bei einem sichtbaren Aktivtag entspricht dies einem blended repository speedup von `0.8x` bzw. `0.5x`. Sichtbares Arbeitsfenster: dieselbe Agentensitzung am 2026-08-29. Validierung: Authoring- und Review-Validatoren in Bash und PowerShell, vollstaendiges Requirements-Alignment, Generator-Reproduktion, JSON-, Hash-, Statistik- und `git diff --check`-Pruefung; keine Implementierung, kein Build/Test/DocFX und keine Remote- oder NuGet-Veroeffentlichung. |
| 2026-08-29 | MergeAndSync-Lieferung fuer Embeddable-VM-/NuGet-Intake vorbereitet | Fuer den fokussierten PR wurde eine bilinguale Beschreibung mit Problem, Loesung, Risiken und Testplan angelegt. Sie dokumentiert ausdruecklich, dass dieser Liefergegenstand nur Intake- und Governance-Artefakte umfasst, weder Host-API noch Pakete implementiert oder veroeffentlicht und keine NuGet-Publikationsautoritaet erteilt. Zusaetzlicher Aenderungsumfang vor dieser Ledger-Fortschreibung: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+61` Dokumentationszeilen. Konservative Manualreferenz: 80 Zeilen/Tag = `0.8` Tage (ca. `5.9` Stunden); Thorsten-Solo-Referenz: 125 Zeilen/Tag = `0.5` Tage (ca. `3.8` Stunden); sichtbares Arbeitsfenster: dieselbe Agentensitzung am 2026-08-29. Der Nutzer hat Commit, Push und `MergeAndSync` mit Admin-Bypass fuer diesen PR ausdruecklich autorisiert. |
| 2026-08-29 | Autonomer Constitution-Lauf `codex/003-constitution-change` | Der bindende Constitution-Intake wurde in einem vollständigen Spec-Kit-Lauf spezifiziert, geplant, analysiert und umgesetzt. Die Arbeitspakete umfassen das TinyPl0-Addendum für didaktische und sprachliche Klarheit, atomare Agenten-/Template-Governance, die aktive CS1591-Schranke mit Rot-Grün-Guard, ergänzte öffentliche XML-Verträge, DocFX-Regeneration, dauerhafte WCAG-2.2-AA-Remediation, Node-24-/Playwright-/axe- und Lynx-Evidence, Coverage-Remediation, NIST-SSDF-/CWE- und Dependency-Review sowie diese Statistikfortschreibung. Umfang vor diesem Ledger-Eintrag: `+24 / -15` Produktionscode-Zeilen, `+78 / -0` Testcode-Zeilen und `+4883 / -124` manuell zugeordnete Dokumentations-, Governance-, Konfigurations- und Evidence-Zeilen; zusätzlich regenerierte DocFX `+715 / -645` getrackte API-YAML-Zeilen, die nicht in die Handarbeitsbasis eingehen. Die `4985` hinzugefügten manuell zugeordneten Zeilen entsprechen bei 80 Zeilen/Arbeitstag `62.3` Tagen bzw. `486.0` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `39.9` Tagen bzw. `311.1` Stunden, jeweils mit `7.8` Stunden pro Tag. Bei einem sichtbaren Aktivtag ist dies ein blended repository speedup von `62.3x` bzw. `39.9x`, keine Stoppuhrmessung. Sichtbares Arbeitsfenster: eine autonome Agentensitzung am 2026-08-29. Validierung: Release-Build ohne Warnung/Fehler, 266/266 Tests, Coverage `70.23 %`, DocFX ohne Fehler, drei axe-Seiten ohne Verstoß, drei nicht leere Lynx-Dumps, keine bekannte anfällige NuGet-Abhängigkeit sowie Governance-, Scope- und Secure-Diff-Prüfung. |
| 2026-08-29 | Kausaler Post-Merge-Abschluss fuer `003-constitution-change` | Nach PR `#68` wurde der gemergte Head kausal abgeschlossen: lokales und Remote-`main` wurden auf Merge `4873a358` synchronisiert, schema-2.0-PreMerge-/PostMerge-Evidenz validiert, das unveraenderte Constitution-Lastenheft nach `requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md` verschoben, die kanonische Intake-Serie mit byteidentischem Vorgaengerarchiv fortgeschrieben und eine evidenzgebundene Retrospektive samt portablem Handoff erstellt. Der Folge-Intake ist nur `Eligible` und wurde nicht gestartet. Der erste PR-#69-Lauf deckte zudem eine plattformuebergreifende Aktivziel-Annahme im Generator auf; Renderer, Alignment-Validator und die paarigen PowerShell-/Bash-Einstiege unterscheiden nun aktive Ziele von abgeschlossenen Archivzielen, ohne einen neuen Intake-Review zu behaupten. Die zugehoerigen Negativ-Fixtures wurden an die neue Lifecycle-Semantik angepasst. Umfang vor diesem Ledger-Eintrag: `+3 / -3` Projektmetadatenzeilen, `+3 / -3` Testcode-Zeilen und `+532 / -144` Dokumentations-, Governance-, Automations-, Archiv- und Evidence-Zeilen; die `538` hinzugefuegten manuell zugeordneten Zeilen entsprechen bei 80 Zeilen/Arbeitstag `6.7` Tagen beziehungsweise `52.5` Stunden und bei 125 Zeilen/Arbeitstag `4.3` Tagen beziehungsweise `33.6` Stunden. Bei einem sichtbaren Aktivtag entspricht dies einem blended repository speedup von `6.7x` beziehungsweise `4.3x`, keiner Stoppuhrmessung. Validierung: Serienmanifest und Receipt in PowerShell und Bash, Renderer/Alignment in beiden Einstiegspfaden, acht positive/negative Alignment-Fixtures, terminaler Run-State 73/73, PostMerge-Gate-Validator, Release-Build mit 0 Warnungen/0 Fehlern und 266/266 Tests; IDE-Version fuer den sechsstufigen Closeout-Kandidaten `1.3.440.32`. |
| 2026-08-29 | Next-Intake-Aussage nach Constitution-Abschluss repariert | Der schlanke Requirements-Index nennt nun synchron auf Deutsch und Englisch `Lastenheft_Secure-Development-Hardening.md` statt des bereits archivierten Constitution-Intakes als bevorzugtes naechstes Ziel. Manifest, Receipt-Lineage, Reihenfolge, Zielhashes, Lifecycle, Archive und Intake-Inhalte blieben unveraendert; kein Intake wurde gestartet. Umfang vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+5 / -5` Requirements-Dokumentationszeilen, `+3 / -3` IDE-Versionsmetadatenzeilen und `+7 / -0` Statistik-Konfigurationszeilen. Die `15` hinzugefuegten manuell zugeordneten Zeilen entsprechen bei 80 Zeilen/Arbeitstag `0.2` Tagen beziehungsweise `1.5` Stunden und bei 125 Zeilen/Arbeitstag `0.1` Tagen beziehungsweise `0.9` Stunden; dies ist bei einem sichtbaren Aktivtag ein blended repository speedup von `0.2x` beziehungsweise `0.1x`, keine Stoppuhrmessung. Sichtbares Arbeitsfenster: eine kurze Agentensitzung am 2026-08-29. Validierung: Schema-2.0-Alignment, Serienmanifest, Receipt, Pfad- und Hashbindung jeweils ueber PowerShell und Bash sowie `git diff --check`; kein `dotnet build` oder `dotnet test`, weil weder Produktcode noch Tests, APIs oder Laufzeitverhalten geaendert wurden. Finale PR-#70-Branch-Version: `1.70.443.32`. |
| 2026-08-29 | Aktuelle 15-Ziele-Serie nach Constitution-Archivierung neu reviewed | Der Series-Review `357ed01f-f120-4634-8596-45e7baffa17d` bindet alle 15 aktuellen Manifestziele einschliesslich des archivierten, abgeschlossenen Constitution-Ziels neu an ihre normalisierten Hashes. Fuenf Wurzeln, zehn Abhaengigkeiten, Reihenfolge, Governance, Lernenden- und A11Y-Policy, NIST-SSDF-/CWE-Sicherheitsrahmen, Evidenz und Delivery-Authority-Grenzen wurden vollstaendig geprueft. Ergebnis: `Ready`, null Findings, null akzeptierte Risiken und null offene Fragen. Das stale Ergebnis `78435231-e579-486f-8d80-8192781c127d` bleibt mit unveraenderten Hashes als Supersession-Evidenz archiviert; Manifest, Receipt, Lifecycle, Reihenfolge, Archive und Intake-Inhalte wurden nicht geaendert und kein Intake gestartet. Umfang vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen, `+192 / -82` Review-, Governance-, Automations- und Evidence-Zeilen, `+3 / -3` IDE-Versionsmetadatenzeilen und `+7 / -0` Statistik-Konfigurationszeilen. Die `202` hinzugefuegten manuell zugeordneten Zeilen entsprechen bei 80 Zeilen/Arbeitstag `2.5` Tagen beziehungsweise `19.7` Stunden und bei 125 Zeilen/Arbeitstag `1.6` Tagen beziehungsweise `12.6` Stunden; dies ist bei einem sichtbaren Aktivtag ein blended repository speedup von `2.5x` beziehungsweise `1.6x`, keine Stoppuhrmessung. Sichtbares Arbeitsfenster: eine kurze Agentensitzung am 2026-08-29. Validierung: Review und vollstaendiges Requirements-Alignment jeweils in PowerShell und Bash, acht positive/negative Alignment-Fixtures, Generator-Reproduktion, JSON-, Hashschutz- und `git diff --check`-Pruefung; kein `dotnet build` oder `dotnet test`, weil kein Produktcode, Testcode, API- oder Laufzeitverhalten geaendert wurde. Finale PR-#71-Branch-Version: `1.71.446.32`. |
| 2026-08-30 | Secure-Development-Hardening 004 bis zur T084-Grenze umgesetzt | Der autonome Feature-Lauf hat die 157-Punkte-Sicherheitsbewertung, Architektur-, Bedrohungs-, ASVS-, Abhaengigkeits-, SBOM-, CVD-, Baseline-, Gitignore- und WCAG-2.2-AA-Evidenz sowie die begrenzte VM-Instruktionsbudget-Option umgesetzt. Die sechs bedingten Pakete und die VM-Rot-Gruen-Nachweise sind abgeschlossen; der finale Lieferzyklus ab T085 bleibt absichtlich offen. Umfang vor diesem Ledger-Eintrag: `+235 / -20` Produktionscode-Zeilen, `+489 / -0` Testcode-Zeilen und `+25097 / -170` Dokumentations-, Governance-, Automations-, Konfigurations-, Ressourcen- und Evidence-Zeilen. Regenerierte API-YAML umfasst zusaetzlich `+466 / -134` Zeilen und ist aus der Handarbeitsbasis ausgeschlossen. Die `25821` manuell zugeordneten hinzugefuegten Zeilen entsprechen bei 80 Zeilen/Arbeitstag `322.8` Tagen beziehungsweise `2517.5` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `206.6` Tagen beziehungsweise `1611.2` Stunden, jeweils mit `7.8` Stunden pro Tag. Bei einem sichtbaren Aktivtag am 2026-08-30 entspricht dies einem blended repository speedup von `322.8x` beziehungsweise `206.6x`, keiner Stoppuhrmessung. Validierung: VM-Rotnachweis, anschliessend `2/2` Options- und `2/2` VM-Budgettests sowie `88/88` Lokalisierungstests; DocFX zuvor ohne Fehler mit sieben vorbestehenden Linkwarnungen; akzeptierter Hostlauf mit `3/3` Chromium-Seiten, null axe-Verstoessen und zwei tokengeprueften Lynx-Dumps; keine gemeldete npm-Schwachstelle; Assessment-, Dokumentationsauswirkungs- und Statistikvalidatoren. Der vollstaendige finale Build-/Test-/Coverage-Zyklus ist erst ab T090 vorgesehen. English: The same evidence set reaches the intentional T084 boundary without claiming the later delivery, full-suite, remote, or merge gates. |
| 2026-08-30 | Sandbox-gestützte Secure-Development-Bewertung 005 umgesetzt | Der strikt serielle autonome Lauf bewertet `absdd-image-sandbox` am unveränderlichen Referenzcommit für TinyPl0, ohne Produktcode, Tests, Sandbox-Image/-Konfiguration oder bestehende `docs/security/`-Evidence zu ändern. Zwölf CL-12-Punkte, symbolische Mount-/Schreibgrenzen, die Sandbox/Lokal/CI-/HumanOnly-Arbeitsortmatrix, getrennte Produkt-/Image-Lieferkettenevidenz und acht vollständige FUP-SBX-Folgeaufgaben sind dokumentiert. Ergebnis: reguläre/autonome Sandbox-Schreibarbeit `Not Ready`; ein späterer Read/Build/Test-Pilot bleibt `Conditional/Open` und braucht menschliche Freigabe, exakten Image-Digest, minimalen TinyPl0-Mount, Secret-Trennung, aktuelle Egress-Entscheidung und reale Baseline-Evidence. Umfang dieses Implementierungsschnitts: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+2446 / -0` Dokumentations-, Governance-, Konfigurations- und Evidence-Zeilen netto einschließlich Statistikphase; Versionsmetadaten folgen ohne Nettozeilenwirkung. Die `2446` Zeilen entsprechen bei 80 Zeilen/Arbeitstag `30.6` Tagen beziehungsweise `238.5` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `19.6` Tagen beziehungsweise `152.6` Stunden, jeweils mit `7.8` Stunden pro Tag. Das sind bei 21.5 Arbeitstagen pro Monat etwa `1.4` beziehungsweise `0.9` Arbeitsmonate; unter der für 2026 geltenden TVoeD-Annahme bleiben 30 Urlaubstage pro Jahr berücksichtigt. Bei einem sichtbaren Aktivtag am 2026-08-30 entspricht dies einem blended repository speedup von `30.6x` beziehungsweise `19.6x`, keiner Stoppuhrmessung. Validierung: 4/4 akzeptierte Input-Hashes, 7/7 vorhandene Phasenresultate, Manifest/Receipt/Ready-Review, erwartetes Dokumentvertrag-Rot und unverändertes Grün, 12/12 CL-Abschnitte, 13/13 Maschinen-Gates, 8/8 Follow-ups, Scope-, Secret-/Privatpfad-, DE/EN-B2- und text-first-Prüfung. Kein `dotnet build`, `dotnet test`, Coverage-, DocFX-, axe- oder `lynx`-Lauf wurde gestartet, weil keine ausführbare Produkt-/API-/HTML-Fläche geändert wurde. English: The documentation-only slice records the same conservative Not Ready/Conditional Pilot boundary and does not claim runtime or approval evidence. |
| 2026-08-30 | Autonomen Sandbox-Lauf 005 kausal abgeschlossen | Nach dem Produktmerge von PR 75 wurde der bindende Sandbox-Intake byte-identisch archiviert, die 15-Ziel-Serie mit 5 Wurzeln und 10 Abhängigkeiten fortgeschrieben und Run 005 mit 69/69 Aufgaben, Retrospektive und portablem Handoff terminalisiert. Der nächste Intake `Lastenheft_Quellcode_Doku.md` ist nur als `Eligible` ausgewiesen und wurde nicht gestartet. Umfang des Closeouts: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+449 / -107` Dokumentations-, Governance-, Konfigurations- und Evidence-Zeilen, netto `342` Zeilen einschließlich Statistikphase; reine IDE-Versionsmetadaten haben keine Nettozeilenwirkung. Die `342` Nettozeilen entsprechen bei 80 Zeilen/Arbeitstag `4.3` Tagen beziehungsweise `33.3` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `2.7` Tagen beziehungsweise `21.3` Stunden, jeweils mit `7.8` Stunden pro Tag. Das sind bei 21.5 Arbeitstagen pro Monat etwa `0.2` beziehungsweise `0.1` Arbeitsmonate; für 2026 gelten 30 Urlaubstage pro Jahr. Bei einem sichtbaren Aktivtag am 2026-08-30 entspricht dies einem blended repository speedup von `4.3x` beziehungsweise `2.7x`, keiner Stoppuhrmessung. Validierung: identischer Intake-Hash, PowerShell-/Bash-Manifest- und Receipt-Prüfung, Schema-2.0-Governance, Renderer, Alignment, terminaler Run-State sowie Head-, Check-, Thread-, Merge- und Sync-Evidence. Kein Produkt-Build/Test oder Folgefeature wurde gestartet. English: The causal closeout archives the unchanged intake, advances only the series lifecycle, completes run 005, and deliberately starts no next run. |
| 2026-08-30 | Embeddable-VM-/NuGet-Intake auf Rang 4 vorgezogen | Auf ausdrücklichen Nutzerwunsch ist `Lastenheft_Embeddable-VM-und-NuGet.md` nun direkt nach dem abgeschlossenen Sandbox-Intake das einzige `Eligible`-Ziel. Quellcode-Dokumentation und die bisherigen Ränge 4 bis 9 rücken um je einen Platz nach hinten; VM-CLI bleibt eine bindende Voraussetzung der späteren IDE-Erweiterung, blockiert den vorgezogenen Intake aber nicht mehr. Der frühere Intake, sein Receipt, Manifest und Serien-Receipt sowie der bisherige Review-Artefaktsatz wurden byte-identisch archiviert; das alte Review ist wegen Zielhash-, Reihenfolge- und Graphänderung ausdrücklich `NeedsRemediation`. Umfang: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+1282 / -135` Dokumentations-, Governance-, Konfigurations-, Automations- und Evidence-Zeilen, netto `1147` Zeilen einschließlich Statistikphase; reine IDE-Versionsmetadaten haben keine Nettozeilenwirkung. Die `1147` Nettozeilen entsprechen bei 80 Zeilen/Arbeitstag `14.3` Tagen beziehungsweise `111.8` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `9.2` Tagen beziehungsweise `71.6` Stunden, jeweils mit `7.8` Stunden pro Tag. Das sind bei 21.5 Arbeitstagen pro Monat etwa `0.7` beziehungsweise `0.4` Arbeitsmonate; für 2026 gelten 30 Urlaubstage pro Jahr. Bei einem sichtbaren Aktivtag am 2026-08-30 entspricht dies einem blended repository speedup von `14.3x` beziehungsweise `9.2x`, keiner Stoppuhrmessung. Validierung: Intake-Authoring-, Serienmanifest-, Serien-Receipt-, Schema-2.0-, Renderer- und Gesamt-Alignment-Prüfungen jeweils portabel in PowerShell und Bash, JSON-, Hash- und byteidentische Archivprüfung. Kein Intake, Produkt-Build, Testlauf, DocFX-Lauf oder Folgefeature wurde gestartet. English: The authorized order update makes Embeddable VM/NuGet the sole eligible target, preserves the later VM-CLI-to-IDE gate, invalidates the stale review, and deliberately executes no intake. |
| 2026-08-30 | NuGet-Veröffentlichungsregeln im Rang-4-Intake verbindlich ergänzt | Der Nutzer hat die zuvor gegen offizielle NuGet- und PowerShell-Hinweise geprüften Veröffentlichungsinformationen ausdrücklich zur Einarbeitung freigegeben. Das Intake bindet nun `dotnet pack`/`dotnet nuget push` gegen die HTTPS-V3-Quelle, OIDC Trusted Publishing als CI-Vorzugsweg, eine eng begrenzte und kurzlebige API-Key-Ausnahme, maskierte manuelle Eingabe mit sicherer Prozessbereinigung, unveränderliche Paket-ID-/Versionskombinationen sowie einen fail-closed Abgleich statt pauschalem Erfolg durch `--skip-duplicate`. Rang 4, 15 Ziele, fünf Wurzeln, elf Kanten, Lifecycle und `LocalImplementation` bleiben unverändert; es wurde weder ein Secret verarbeitet noch ein Paket veröffentlicht oder Intake gestartet. Der unmittelbare Intake-, Receipt-, Manifest-, Serien-Receipt- und Review-Vorgänger bleibt byte-identisch archiviert, der Review weiterhin ausdrücklich `NeedsRemediation`. Umfang: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+1288 / -86` Dokumentations-, Governance-, Konfigurations-, Automations- und Evidence-Zeilen, netto `1202` Zeilen einschließlich Statistikphase; reine IDE-Versionsmetadaten haben keine Nettozeilenwirkung. Die `1202` Nettozeilen entsprechen bei 80 Zeilen/Arbeitstag `15.0` Tagen beziehungsweise `117.2` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `9.6` Tagen beziehungsweise `75.0` Stunden, jeweils mit `7.8` Stunden pro Tag. Das sind bei 21.5 Arbeitstagen pro Monat etwa `0.7` beziehungsweise `0.4` Arbeitsmonate; für 2026 gelten 30 Urlaubstage pro Jahr. Bei einem sichtbaren Aktivtag am 2026-08-30 entspricht dies einem blended repository speedup von `15.0x` beziehungsweise `9.6x`, keiner Stoppuhrmessung. Validierung: Intake-Authoring-, Manifest-, Receipt-, Renderer- und Alignment-Prüfungen in PowerShell, Bash und Node, acht Negativ-Fixtures, JSON-, Hash-, Archiv- und Secret-Prüfung. Kein Produkt-Build, Testlauf, DocFX-Lauf, Intake-Review oder Veröffentlichungsschritt wurde gestartet. English: The intake now binds OIDC-first NuGet publishing, a constrained fallback, immutable versions, and fail-closed duplicate handling without granting publication authority. |
| 2026-09-01 | Aktuelle 15-Ziele-Serie nach NuGet-Policy-Update neu reviewed | Der vollständige schema-1.1-Serienreview `a182589d-3149-4de7-a1d8-c24cefc28cbf` bindet alle 15 aktuellen Zielhashes und Git-Blobs, fünf Wurzeln sowie elf eindeutige azyklische Abhängigkeiten. Der auf Rang 4 vorgezogene Embeddable-VM-/NuGet-Intake erfüllt nach der ergänzten OIDC-, API-Key-, Versions- und Duplicate-Policy auch die Lernenden-, CEFR-B2-, A11Y-, NIST-SSDF-, CWE- und Supply-Chain-Grenzen. Ergebnis: `Ready`, null Findings, null akzeptierte Risiken, null offene Fragen und null Worker; `Eligible` und `LocalImplementation` erteilen weiterhin keine Remote-, Merge-, Bypass-, Secret-, Provider- oder NuGet-Publikationsrechte. Der invalidierte Vorgängerreview bleibt mit seinen normalisierten Artefakthashes und Finding `IR900` als Supersession-Evidenz erhalten. Umfang vor diesem Ledger-Eintrag: `0` Produktionscode-Zeilen, `0` Testcode-Zeilen und `+190 / -147` Review-, Governance- und Evidence-Zeilen, netto `43` Zeilen. Die `43` Nettozeilen entsprechen bei 80 Zeilen/Arbeitstag `0.5` Tagen beziehungsweise `4.2` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `0.3` Tagen beziehungsweise `2.7` Stunden, jeweils mit `7.8` Stunden pro Tag; sichtbares Arbeitsfenster: eine Agentensitzung am 2026-09-01. Validierung: Schema-2.0-Konfiguration, Manifest, Serien-Receipt, 12 aktuelle Authoring-Receipts, drei archivierte Lifecycle-Ziele, Review-Resultat und Requirements-Alignment portabel in PowerShell und Bash sowie JSON-, Hash-, Supersession- und `git diff --check`-Prüfung. Kein Intake, Produkt-Build, Testlauf, DocFX-Lauf oder Remote-Schritt wurde gestartet. English: The current series review is ready, preserves the invalidated predecessor's audit meaning, and grants no execution or publication authority. |
| 2026-09-02 | Autonomer Lauf 006 für Embeddable VM und NuGet lokal umgesetzt | Branch `codex/006-embeddable-vm-nuget`, sichtbares Arbeitsfenster ein Git-Aktivtag am 2026-09-02. Ein gemeinsamer VM-Ausführungskern stellt Run/Step-Parität, Abbruch-, Budget-, Fehler- und Snapshot-Verträge bereit; Core und VM werden als exakt gekoppelte `0.4.0`-Pakete mit Symbolen, XML-Dokumentation, OIDC-Release-Pipeline, Lockfiles, SBOM/VEX/Provenance, TinyCalc-Handoff sowie DE-first/EN-second-B2- und WCAG-2.2-AA-Nachweisen vorbereitet. Umfang vor diesem Ledger-Eintrag: Produktionscode `+723 / -1175 = -452`, Tests `+468 / -0 = +468`, Dokumentation und Spec-Kit-Evidenz `+3621 / -1 = +3620`; generierte `api/*.yml`, Skripte und Konfiguration bleiben außerhalb der Handarbeitsbasis. Die Netto-Gesamtbasis von `3636` Zeilen entspricht bei 80 Zeilen/Arbeitstag `45.5` Tagen beziehungsweise `354.5` Stunden und bei der Thorsten-Solo-Basis von 125 Zeilen/Arbeitstag `29.1` Tagen beziehungsweise `226.9` Stunden, jeweils mit `7.8` Stunden pro Tag. Das sind bei 21.5 Arbeitstagen pro Monat etwa `2.1` beziehungsweise `1.4` Arbeitsmonate; für 2026 gelten 30 Urlaubstage. Gegen einen sichtbaren Aktivtag ergibt sich ein blended repository speedup von `45.5x` beziehungsweise `29.1x`, keine Stoppuhrmessung. Lokale Evidenz: warnungsfreier Release-Build, `307/307` Tests, `71.73%` Gesamtlinien-, `96.50%` Kernel-Linien- und `85.05%` Kernel-Branch-Abdeckung, vier Paketdateien, exakte VM-Core-Version, frischer Consumer, DocFX ohne Fehler, vier axe-Seiten ohne Verstöße, Lynx-Tokenpfad, PowerShell-Parser und keine bekannten anfälligen NuGet-Abhängigkeiten. Die Remote-, Review-, Merge- und Veröffentlichungsgates folgen erst am exakten PR-Head. English: Run 006 implements the shared VM engine, paired packages, release supply chain, learner documentation, and local evidence while leaving exact-head remote delivery gates for the PR stage. |

## Statistikprofil-1-Archiv / Statistics Profile 1 Archive
- Stand 2026-05-05: `88` Produktionsdateien mit `6950` Zeilen, `22` Testdateien mit `3536` Zeilen und `562` Dokumentationsdateien mit `36625` Zeilen.
- Die beobachtbare Gesamtbasis fuer die manuelle Referenz liegt bei `47111` Zeilen. Das entspricht konservativ `588.9` Arbeitstagen fuer einen erfahrenen Entwickler oder `376.9` Arbeitstagen fuer die Thorsten-Solo-Referenz; sichtbar dokumentiert sind `43` Git-Aktivtage.
- Repo-weite Beschleunigungsfaktoren aus dem aktuellen Ledger: `13.7x` gegen die konservative 80-Zeilen-Referenz und `8.8x` gegen die Thorsten-Solo-Referenz.
- Phasenkuerzel in den Diagrammen: `0` = Compiler-Basis, `1` = Doku/API, `2` = IDE, `3` = Governance/L10N, `4` = Pages/CI/DocFX, `5` = Branch `002-vm-inc-compat`, `6` = Constitution-/Preset-Sync.

Diese Schlusssektion fasst den aktuellen Repository-Stand bewusst textfreundlich zusammen. Die ASCII-Diagramme bleiben grob und ohne Farbcode, damit sie auf Braille-Zeile, mit Screenreadern und in Textbrowsern gut lesbar bleiben.

This closing section summarizes the current repository state in a deliberately text-first form. The ASCII charts stay approximate and color-free so they remain readable on Braille displays, with screen readers, and in text browsers.

```text
Aktueller Artefaktmix im Repository (Zeilen)
Produktion | #####                    |  6950
Tests      | ###                      |  3536
Doku       | ######################## | 36625
```

Dieser Block zeigt, wie stark TinyPl0 inzwischen von Dokumentation, Lehrmaterial und Governance-Artefakten gepraegt ist. Das ist fuer ein Lern- und Referenzprojekt erwartbar: Code, Tests und Dokumentation wachsen hier bewusst zusammen.

This block shows how strongly TinyPl0 is now shaped by documentation, teaching material, and governance artifacts. That is expected for a learning and reference project: code, tests, and documentation intentionally grow together here.

```text
Dokumentiertes Volumen je Phase/Branch (Zeilen)
0 Comp  | ##########               |  5308
1 Doku  | ######################## | 12476
2 IDE   | ##########               |  5125
3 Gov   | #################        |  8679
4 CI    | #                        |    14
5 002   | ###                      |  1557
6 Sync  | #                        |   162
```

Dieses Diagramm zeigt die grossen Lieferpakete ueber die bisher dokumentierten Phasen. Man sieht sofort, dass die Doku/API-Welle und die spaetere Governance-/L10N-Phase fuer TinyPl0 besonders viel sichtbaren Umfang gebracht haben.

This chart shows the major delivery packages across the phases documented so far. It immediately reveals that the documentation/API wave and the later governance/L10N phase contributed especially large visible scope for TinyPl0.

```text
Dokumentierte Beschleunigungsfaktoren
Repo80  | ###########              | 13.7x
Repo125 | #######                  |  8.8x
0 Comp  | ###############          | 22.1x
1 Doku  | ###############          | 22.3x
2 IDE   | ##############           | 21.4x
3 Gov   | ######################## | 36.2x
4 CI    | #                        |  0.1x
5 002   | #############            | 19.5x
6 Sync  | #                        |  2.0x
```

Hier werden keine Stoppuhrzeiten gemessen. Verglichen wird die dokumentierte Lieferdichte gegen zwei manuelle Referenzen. Hohe Werte bedeuten also: viel sichtbarer Output in wenigen belegten Aktivtagen.

This chart does not measure stopwatch time. It compares documented delivery density against two manual references. High values therefore mean a lot of visible output within only a few evidenced active days.

```text
Vergleich dokumentierter Gesamtaufwand / sichtbares KI-Lieferfenster
Erfahren    | ######################## | 588.9 d
Thorsten    | ###############          | 376.9 d
KI sichtbar | ##                       |  43.0 d
```

Dieser Vergleich macht die grobe Groessenordnung sichtbar: Zwischen klassischer Handarbeit und dem im Repository sichtbaren Lieferfenster liegt eine deutliche Verdichtung. Genau diese Verdichtung beschreibt das Ledger als blended repository speedup.

This comparison makes the scale visible at a glance: there is clear compression between classical manual effort and the delivery window visible in the repository. That documented compression is what the ledger labels as blended repository speedup.

Wenn man einen Verlauf ueber die X-Achse sehen will, helfen X/Y-Diagramme
zusaetzlich. Hier steht die X-Achse fuer die dokumentierten Phasen oder
Branches. Die X-Positionen nutzen feste Slots: jede Phase behaelt ihren Platz,
auch wenn einzelne Datenpunkte fehlen. Wenn weitere Spec-Kit-Laeufe die Achse
zu breit machen, wird sie in Bloecke wie `0..15`, `16..31` und `32..47`
geteilt; jeder Block bekommt eine eigene Achsenlinie und eigene X-Labels. Die
Y-Achse zeigt je nach Diagramm Zeilen oder Beschleunigungsfaktoren. Die
Sternpunkte bleiben grob gebinnt: Sie sollen Trends sichtbar machen, nicht
mathematische Genauigkeit auf Plotter-Niveau liefern.

If readers want to see progression across the X-axis, X/Y charts help as a
second view. Here the X-axis stands for the documented phases or branches. The
X positions use fixed slots: each phase keeps its place, even when individual
data points are missing. When future Spec-Kit runs make the axis too wide, split
it into blocks such as `0..15`, `16..31`, and `32..47`; each block gets its own
axis line and X labels. Depending on the chart, the Y-axis shows lines or
acceleration factors. The star markers stay roughly binned: they reveal trends,
not plotter-level mathematical precision.

```text
X/Y-Diagramm: dokumentiertes Phasenvolumen (X = Phase/Branch, Y = Zeilen)
12500 |     *                      |
10000 |                            |
 7500 |             *              |
 5000 | *       *                  |
 2500 |                     *      |
    0 |                 *       *  |
      +----------------------------+
       0   1   2   3   4   5   6
```

Als zweite Ansicht zeigt diese X/Y-Kurve denselben Verlauf ueber die Phasenachse. So werden Spruenge zwischen benachbarten Paketen leichter lesbar als in einer reinen Liste.

As a second view, this X/Y curve shows the same progression across the phase axis. That makes jumps between neighboring packages easier to read than in a plain list.

```text
X/Y-Diagramm: dokumentierte Beschleunigungsfaktoren (X = Phase/Branch, Y = Faktor)
 40 |             *              |
 30 |                            |
 20 | *   *   *           *      |
 10 |                            |
  0 |                 *       *  |
    +----------------------------+
     0   1   2   3   4   5   6
```

Diese zweite X/Y-Ansicht zeigt die dokumentierten Beschleunigungsfaktoren je Phase. Fehlende oder sehr kleine Punkte sind nicht automatisch schlecht; sie bedeuten hier nur, dass ein Paket klein war oder dass die sichtbare Aktivtag-Basis kaum Verdichtung zeigt.

This second X/Y view shows the documented acceleration factors per phase. Missing or very small points are not automatically bad; here they only mean that a package was small or that the visible active-day base shows little compression.

## Gesamtstatistik / Overall Statistics

<!-- project-statistics-v2:begin -->

Profil 2 verwendet Git-getrackte Textdateien und sichtbare Git-Aktivitaet. Die Werte beschreiben Lieferdichte, keine persoenliche Arbeitszeit.

*Profile 2 uses Git-tracked text files and visible Git activity. The values describe delivery density, not personal working time.*

| Kennzahl / Metric | Wert / Value |
|---|---:|
| Textbasis / Text base | 269780 lines |
| Textdateien / Text files | 1976 |
| Beobachtbarer Zeitraum / Observable period | 2025-09-07..2026-09-02 |
| Aktivtage / Active days | 89 |
| Relevante Commits / Relevant commits | 354 |
| Zeilen je Aktivtag / Lines per active day | 3031.2 |
| Peak-Tag im Fenster / Peak day in window | 2026-02-14 / 177480 |
| Peak-Woche im Fenster / Peak week in window | 2026-02-08 / 186065 |
| Laengste Serie / Longest streak | 9 days |
| Speedup vs. 80 lines/day | 37.9x |
| Speedup vs. 125 lines/day | 24.2x |
| Methodik / Methodology | v2; source `16c82fa2233d` |

### Artefaktmix / Artifact Mix

```text
Produktiv / Production          [#...................]   2.6% | 7060
Tests                           [#...................]   4.8% | 13074
Dokumentation / Documentation   [###########.........]  54.1% | 145956
Skripte / Scripts               [##..................]   8.4% | 22620
Konfiguration / Configuration   [######..............]  28.3% | 76338
Daten und Medien / Data and media [....................]   0.0% | 0
Sonstiger Text / Other text     [#...................]   1.8% | 4732
```

Die Balken teilen die aktuelle getrackte Textbasis in stabile Kategorien. Prozent und Zeilenwert sind die genaue, textorientierte Aussage.

*The bars split the current tracked text base into stable categories. Percentages and line counts provide the exact text-first result.*

### Tagesaktivitaet / Daily Activity

```text
Wochen / Weeks 01..26 | 2025-09-07..2026-03-07
So/Su  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 4 4 4 4
Mo/Mo  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 2 0 0
Di/Tu  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 1 0 0
Mi/We  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 1 2 0 0
Do/Th  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 3 0 4
Fr/Fr  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 3 0 4
Sa/Sa  0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 4 4 0 0
```

```text
Wochen / Weeks 27..52 | 2026-03-08..2026-09-05
So/Su  2 1 4 0 0 2 0 0 0 0 0 0 1 0 4 0 0 0 1 4 4 1 4 4 0 4
Mo/Mo  4 1 0 2 0 1 4 0 4 0 0 0 0 0 0 0 3 1 4 4 0 0 0 2 0 0
Di/Tu  0 0 0 2 0 0 0 0 2 0 0 3 0 0 0 0 2 0 3 4 4 0 0 0 0 2
Mi/We  0 1 1 0 0 0 3 0 2 0 0 0 2 0 4 0 2 0 0 2 4 0 0 0 0 4
Do/Th  0 0 0 0 0 0 0 4 0 0 0 1 1 4 1 0 0 0 1 4 0 0 4 0 0 -
Fr/Fr  0 0 4 4 0 2 4 0 0 0 4 2 2 0 3 2 3 4 4 4 1 0 0 0 0 -
Sa/Sa  2 0 2 0 0 0 0 0 0 0 0 0 0 0 4 0 4 4 0 4 2 0 2 0 4 -
```

DE: 0 = keine Aenderung; 1 = 1..79; 2 = 80..399; 3 = 400..1599; 4 = 1600+ geaenderte Textzeilen; - = noch nicht abgelaufen.

*EN: 0 = no change; 1 = 1..79; 2 = 80..399; 3 = 400..1599; 4 = 1600+ changed text lines; - = not elapsed.*

### Wochenvolumen / Weekly Volume

```text
Wochen / Weeks 01..26 | 2025-09-07..2026-03-07
  cap 200000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      166667 | . . . . . . . . . . . . . . . . . . . . . . # . . .
      133333 | . . . . . . . . . . . . . . . . . . . . . . # . . .
      100000 | . . . . . . . . . . . . . . . . . . . . . . # . . .
       66667 | . . . . . . . . . . . . . . . . . . . . . . # . . .
       33333 | . . . . . . . . . . . . . . . . . . . . . . # # . #
           0 +-----------------------------------------------------
```

```text
Wochen / Weeks 27..52 | 2026-03-08..2026-09-05
  cap 100000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
       83333 | . . . . . . . . . . . . . . . . . . . . . . . . . .
       66667 | . . . . . . . . . . . . . . . . . . . . . . . . . .
       50000 | . . . . . . . . . . . . . . . . . . . . . . . . . #
       33333 | . . . . . . . . . . . . . . . . . . . # . . . . . #
       16667 | . . . . . . . . . . . . . . # . . . . # # . . . . #
           0 +-----------------------------------------------------
```

Das Wochenvolumen zeigt Additionen plus Loeschungen. Es ist Aenderungsaktivitaet, nicht die aktuelle Groesse des Repositories.

*Weekly volume shows additions plus deletions. It represents change activity, not the current repository size.*

### Kumulative Entwicklung / Cumulative Development

```text
Wochen / Weeks 01..26 | 2025-09-07..2026-03-07
  cap 500000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      416667 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      333333 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      250000 | . . . . . . . . . . . . . . . . . . . . . . . . . #
      166667 | . . . . . . . . . . . . . . . . . . . . . . # # # #
       83333 | . . . . . . . . . . . . . . . . . . . . . . # # # #
           0 +-----------------------------------------------------
```

```text
Wochen / Weeks 27..52 | 2026-03-08..2026-09-05
 cap 1000000 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      833333 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      666667 | . . . . . . . . . . . . . . . . . . . . . . . . . .
      500000 | . . . . . . . . . . . . . . . . . . . . . . . . . #
      333333 | . . . . . . . . . . # # # # # # # # # # # # # # # #
      166667 | # # # # # # # # # # # # # # # # # # # # # # # # # #
           0 +-----------------------------------------------------
```

Die kumulative Kurve summiert nur das Brutto-Aenderungsvolumen im Fenster. Sie darf nicht als aktuelle Codebasis gelesen werden.

*The cumulative curve sums gross change volume within the window only. It must not be read as the current code base.*

### Phasenvolumen / Phase Volume

```text
Slots 0..15
   cap 50000 | . . . . . . . . . . . . . . . .
       41667 | . . . . . . . . . . . . . . . .
       33333 | . . . . . . . . . . . . . . . .
       25000 | . . . . . . . . . . . # . . . .
       16667 | . . . . . . . . . . . # . . . .
        8333 | . # . # . . . . . . . # . . . .
           0 +---------------------------------
             00 01 02 03 04 05 06 07 08 09 10 11 12 13 14 15
```

```text
Slots 16..18
    cap 5000 | . . .
        4167 | . . .
        3333 | . . #
        2500 | . . #
        1667 | . . #
         833 | # # #
           0 +-------
             16 17 18
```

| Slot | Phase | Nettozeilen / Net lines |
|---:|---|---:|
| 0 | Compiler / Compiler | 5308 |
| 1 | Dokumentation / Documentation | 12476 |
| 2 | IDE / IDE | 5125 |
| 3 | Governance / Governance | 8679 |
| 4 | CI / CI | 14 |
| 5 | 002 / 002 | 1557 |
| 6 | Synchronisierung / Synchronization | 162 |
| 7 | Constitution 003 / Constitution 003 | 4846 |
| 8 | 003 Abschluss / 003 closeout | 388 |
| 9 | Next-Intake-Indexreparatur / Next-intake index repair | 7 |
| 10 | Aktueller Serienreview / Current series review | 110 |
| 11 | Secure Development 004 / Secure Development 004 | 25631 |
| 12 | 004 Abschluss / 004 closeout | 836 |
| 13 | Serienreview nach 004 / Series review after 004 | 141 |
| 14 | Sandbox-Bewertung 005 / Sandbox assessment 005 | 2446 |
| 15 | 005 Abschluss / 005 closeout | 342 |
| 16 | Embeddable VM/NuGet vorgezogen / Embeddable VM/NuGet prioritized | 1147 |
| 17 | NuGet-Veröffentlichungsregeln / NuGet publishing policy | 1202 |
| 18 | Embeddable VM/NuGet 006 / Embeddable VM/NuGet 006 | 3636 |

Die festen Slots halten den Phasenvergleich auch bei fehlenden oder spaeter ergaenzten Werten stabil.

*Stable slots keep the phase comparison consistent when values are missing or added later.*

### Beschleunigungsfaktoren / Acceleration Factors

```text
Scale: 0..50x
80 lines/day       [###############.....] 37.9x
125 lines/day      [##########..........] 24.2x
```

Die Faktoren vergleichen sichtbare Lieferdichte mit den dokumentierten manuellen Referenzen. Sie messen keine Arbeitszeit.

*The factors compare visible delivery density with documented manual references. They do not measure working time.*

### Durchsatzvergleich / Throughput Comparison

```text
Scale: 0..5000 lines/day
Experienced manual [#...................] 80
Thorsten solo      [#...................] 125
Visible repository [############........] 3031.2
```

Die gemeinsame Skala vergleicht Referenzen und sichtbare Lieferdichte. Sie schreibt die Git-Aktivitaet keiner Person oder KI pauschal zu.

*The common scale compares references with visible delivery density. It does not attribute Git activity to a person or AI by default.*

### Textalternative / Text Alternative

DE: Das Fenster beginnt am 2025-09-07 und endet am 2026-09-02. Es enthaelt 89 aktive und 272 inaktive vergangene Tage. Peak-Tag: 2026-02-14 / 177480. Peak-Woche: 2026-02-08 / 186065. Laengste Serie: 9 Tage (2026-02-14..2026-02-22).

*EN: The window starts on 2025-09-07 and ends on 2026-09-02. It contains 89 active and 272 inactive elapsed days. Peak day: 2026-02-14 / 177480. Peak week: 2026-02-08 / 186065. Longest streak: 9 days (2026-02-14..2026-02-22).*

| Monat / Month | Geaenderte Textzeilen / Changed text lines |
|---|---:|
| 2025-10 | 0 |
| 2025-11 | 0 |
| 2025-12 | 0 |
| 2026-01 | 0 |
| 2026-02 | 241788 |
| 2026-03 | 63449 |
| 2026-04 | 17036 |
| 2026-05 | 12323 |
| 2026-06 | 37650 |
| 2026-07 | 78966 |
| 2026-08 | 55167 |
| 2026-09 | 16759 |

<!-- project-statistics-v2:end -->
