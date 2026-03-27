# Projektstatistik TinyPl0

Stand: 2026-03-27

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
  unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr ergeben sich
  `21.5 * 12 - 30 = 228` produktive Arbeitstage pro Jahr bzw.
  durchschnittlich 19.0 produktive Tage pro Kalendermonat.
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
| Beobachtbarer Projektzeitraum | 2026-02-08 bis 2026-03-15 |
| Git-Commits gesamt | 186 |
| Autoren laut Git | 2 |
| Git-Aktivtage | 20 |
| Produktionscode aktuell | 58 Dateien / 6536 Zeilen |
| Testcode aktuell | 15 Dateien / 3351 Zeilen |
| Dokumentation aktuell | 359 Dateien / 16097 Zeilen |
| Davon Spec-Kit-Artefakte | 12 Dateien / 2212 Zeilen |
| Davon Governance/Agent-Dateien | 5 Dateien / 933 Zeilen |
| Gesamtbasis fuer Handschaetzung (inkl. Dokumentation) | 25984 Zeilen |
| Erfahrener Entwickler, konservative Untergrenze | 324.8 Arbeitstage |
| Erfahrener Entwickler, konservative Untergrenze in Stunden | 2533.4 Stunden (324.8 * 7.8) |
| Erfahrener Entwickler, brutto | 15.1 Arbeitsmonate (21.5 Tage/Monat) |
| Erfahrener Entwickler, TVoeD-Annahme | 17.1 Kalendermonate bzw. 1.4 Jahre |
| Thorsten solo, erfahrungsadjustierte Untergrenze | 207.9 Arbeitstage |
| Thorsten solo, erfahrungsadjustierte Untergrenze in Stunden | 1621.6 Stunden (207.9 * 7.8) |
| Thorsten solo, brutto | 9.7 Arbeitsmonate (21.5 Tage/Monat) |
| Thorsten solo, TVoeD-Annahme | 10.9 Kalendermonate bzw. 0.9 Jahre |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 129.9 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), TVoeD-Annahme | 6.8 Kalendermonate |
| Repo-weiter Beschleunigungsfaktor vs. konservative Referenz | 16.2x (324.8 / 20 Git-Aktivtage) |
| Repo-weiter Beschleunigungsfaktor vs. Thorsten-Referenz | 10.4x (207.9 / 20 Git-Aktivtage) |

## Branch-Ueberblick

| Branch/Ref | Letzte sichtbare Aktivitaet | Einordnung |
|---|---|---|
| `002-vm-inc-compat` | 2026-03-27 | Aktiver Feature-Branch fuer die SPEC-Kit-Spezifikation zur `Inc`-/`Int`-VM-Kompatibilitaet |
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

### 5. Branch `002-vm-inc-compat` - SPEC-Kit-Spezifikation fuer `Inc`/`Int`

- Status: in Arbeit auf Feature-Branch `002-vm-inc-compat`
- Beobachtbarer Zeitraum: 2026-03-27 bis 2026-03-27
- Commit-Bild: noch nicht in `main`, aktueller Aenderungssatz vor dem ersten Branch-Commit
- Grundlegende Arbeiten: Lastenheft fuer die historische `Inc`-Alias-Anforderung
  in eine SPEC-Kit-taugliche Feature-Beschreibung ueberfuehrt, `spec.md` fuer
  die VM-Kompatibilitaet erstellt und Qualitaets-Checkliste fuer die
  Planungsfreigabe angelegt
- Git-/Arbeitsbaum-Aenderungsvolumen fuer den aktuellen Aenderungssatz:
  - Produktionscode: 0 Zeilen
  - Testcode: 0 Zeilen
  - Dokumentation: 268 Zeilen in bearbeiteten oder neu angelegten Markdown-Dateien
- Konservative Handarbeits-Basis fuer Code und Dokumentation:
  - 268 Zeilen netto gesamt
  - 3.4 Arbeitstage fuer einen erfahrenen Entwickler
  - 26.1 Stunden auf TVoeD-Basis (`3.4 * 7.8`)
  - 0.2 Arbeitsmonate brutto bzw. 0.2 TVoeD-Kalendermonate
- Thorsten-Solo-Referenz:
  - 2.1 Arbeitstage
  - 16.7 Stunden auf TVoeD-Basis (`2.1 * 7.8`)
  - 0.1 Arbeitsmonate brutto bzw. 0.1 TVoeD-Kalendermonate
- Blended Repository Speedup gegen sichtbare 1 Git-Aktivtag fuer diesen
  Aenderungssatz:
  - 3.4x gegen die konservative 80-Zeilen-Referenz
  - 2.1x gegen die Thorsten-Solo-Referenz mit 125 Zeilen pro Arbeitstag

## Einordnung der KI-/Spec-Kit-Wirkung

- Die beobachtbare manuelle Gesamtbasis liegt bereits bei 25984 Zeilen
  (Produktionscode + Tests + Dokumentation).
- Selbst mit der konservativen Obergrenze von 80 manuell erstellten Zeilen pro
  Arbeitstag ergibt sich bereits eine Untergrenze von 324.8
  Entwickler-Arbeitstagen.
- Unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr entspricht das fuer einen
  erfahrenen Entwickler ca. 17.1 Kalendermonaten bzw. 1.4 Arbeitsjahren; fuer
  ein 3er-Team mit 20 % Koordinationsaufschlag ca. 6.8 Kalendermonaten.
- Unter Einbezug von Thorstens Erfahrungsprofil sinkt die klassische
  Solo-Referenz fuer TinyPl0 auf ca. 207.9 Arbeitstage bzw.
  10.9 TVoeD-Kalendermonate.
- Gegen die sichtbaren 20 Git-Aktivtage ergibt sich damit ein repo-weiter
  Beschleunigungsfaktor von ca. 16.2x gegen die konservative Referenz und
  ca. 10.4x gegen die erfahrungsadjustierte Thorsten-Referenz.
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
| 2026-03-27 | Agentische Spezifikationsarbeit auf `002-vm-inc-compat` | Lastenheft fuer die historische `Inc`-/`Int`-Kompatibilitaet zu einer SPEC-Kit-tauglichen Feature-Beschreibung umgebaut, `specs/002-vm-inc-compat/spec.md` und die Qualitaets-Checkliste angelegt, Branch-Einordnung und Handschaetzung fuer den Aenderungssatz fortgeschrieben. |
| 2026-03-27 | Clarify-Runde fuer `002-vm-inc-compat` | Die Spezifikation wurde in mehreren Klarstellungsschritten an die reale TinyPl0-P-Code-Logik angepasst: Scope auf textuelle VM-/P-Code-Artefakte eingegrenzt, bestehende case-insensitive Eingabelogik fuer Mnemonics uebernommen, textuelle Ausgabe auf das aktuelle lowercase-Mnemonic `int` statt auf eine neue Schreibweise festgelegt und die Terminologie zwischen historischem `Inc`, internem `Int` und textuellem `int` bereinigt. |
| 2026-03-27 | Governance-Update zur IDE-Versionslogik | Die gemeinsame Agent-Governance (`AGENTS.md`, `CLAUDE.md`, `GEMINI.md`, `.github/copilot-instructions.md` und `.specify/memory/constitution.md`) wurde so angepasst, dass fuer `Pl0.Ide` die Spec-Kit-Feature-/Branch-Nummer kuenftig sofort als kanonische PR-Nummer fuer `Minor` gilt, auch wenn die GitHub-PR noch nicht existiert. |
| 2026-03-27 | Sortierung des Fortschreibungsprotokolls vereinheitlicht | Die Eintraege im Fortschreibungsprotokoll wurden auf strikt chronologische Reihenfolge gebracht: aeltester Eintrag oben, juengster und zuletzt eingetragener Eintrag unten. Dieselbe Regel wurde in der gemeinsamen Agent-Governance fuer dieses Repository festgeschrieben. |
