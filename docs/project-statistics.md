# Projektstatistik TinyPl0

Stand: 2026-03-22

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
- Abgeleitete Formeln in dieser Datei:
  Einzelentwickler `((Produktionscode + Testcode + Dokumentation) / 80)`;
  3er-Team `Einzelentwickler / 3 * 1.2` mit 20 % Koordinationsaufschlag.

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
| Erfahrener Entwickler, brutto | 15.1 Arbeitsmonate (21.5 Tage/Monat) |
| Erfahrener Entwickler, TVoeD-Annahme | 17.1 Kalendermonate bzw. 1.4 Jahre |
| Kleines Team (3 Personen, +20 % Koordination), Untergrenze | 129.9 Arbeitstage |
| Kleines Team (3 Personen, +20 % Koordination), TVoeD-Annahme | 6.8 Kalendermonate |

## Branch-Ueberblick

| Branch/Ref | Letzte sichtbare Aktivitaet | Einordnung |
|---|---|---|
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

## Einordnung der KI-/Spec-Kit-Wirkung

- Die beobachtbare manuelle Gesamtbasis liegt bereits bei 25984 Zeilen
  (Produktionscode + Tests + Dokumentation).
- Selbst mit der konservativen Obergrenze von 80 manuell erstellten Zeilen pro
  Arbeitstag ergibt sich bereits eine Untergrenze von 324.8
  Entwickler-Arbeitstagen.
- Unter TVoeD-Annahme mit 30 Urlaubstagen pro Jahr entspricht das fuer einen
  erfahrenen Entwickler ca. 17.1 Kalendermonaten bzw. 1.4 Arbeitsjahren; fuer
  ein 3er-Team mit 20 % Koordinationsaufschlag ca. 6.8 Kalendermonaten.
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
