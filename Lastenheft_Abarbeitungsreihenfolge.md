# Lastenheft-Abarbeitungsreihenfolge fuer Spec-Kit-Laeufe

Stand: 2026-06-19

## Zweck

Deutsch:
Dieses Dokument ist der sichtbare Arbeitsvorrat fuer spaetere Spec-Kit-Laeufe
aus den vorhandenen Lastenheften. Es trennt erledigte Referenzdokumente,
naechste aktive Laeufe und bewusst zurueckgestellte Themen. Neue Spec-Kit-Laeufe
sollen die Reihenfolge hier verwenden, sofern der Benutzer keine andere
Prioritaet vorgibt.

English:
This document is the visible backlog for later Spec Kit runs based on the
existing requirements documents. It separates completed reference documents,
next active runs, and deliberately deferred topics. New Spec Kit runs should use
this order unless the user gives a different priority.

## Entscheidungsregeln

- Erst Querschnitt und Governance klaeren, dann neue Funktionen bauen.
- Bereits umgesetzte Lastenhefte bleiben Referenz und werden nicht erneut als
  Feature gestartet.
- IDE-nahe Sprach- und A11Y-Arbeit kommt vor neuen IDE-Funktionen, damit neue
  Texte, Shortcuts und Statusmeldungen nicht nachtraeglich umgebaut werden
  muessen.
- CLI-/Options-Arbeit kommt vor VM-CLI, Optimierung und CLR-Backend, weil diese
  Funktionen gemeinsame Schalter, Grenzen und Ausgabeformen brauchen.
- Optimierung und CLR-Backend sind aktuell nur mit einem vorgelagerten
  Architektur-/Governance-Entscheid erlaubt, weil die Projektregeln derzeit
  "keine Compiler-Optimierungen" und "VM target only" festhalten.

## Aktive Abarbeitungsreihenfolge

| Reihenfolge | Vorgeschlagener Spec-Kit-Branch | Lastenheft | Ziel des naechsten Laufs | Grund fuer diese Position |
|---:|---|---|---|---|
| 1 | `003-constitution-reconcile` | `Lastenheft_Constitution_Change.md` | Offene oder bereits uebernommene Governance-Regeln zu B2, XML-Doku, DocFX und TDD gegen `constitution.md`, `.specify/memory/constitution.md` und die aktuellen Agentenregeln abgleichen; Ergebnis kann auch "keine Umsetzung mehr noetig" sein. | Diese Regeln entscheiden, welche Doku-, Test- und Sprachpflichten fuer alle spaeteren Laeufe gelten. |
| 2 | `004-secure-development-hardening` | `Lastenheft_Secure-Development-Hardening.md` | Sicherheits- und Architektur-Governance fuer Compiler, VM, CLI, IDE, CI und Dokumentation klassifizieren; Evidenzpfade und offene Risiken dokumentieren. | Der Lauf ist querschnittlich und sollte vor neuen Feature-Oberflaechen erfolgen. |
| 3 | `014-sandbox-secure-development` | `Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` | Pruefen, wie TinyPl0 sicher, nachvollziehbar und ausbildungsgeeignet in oder mit `absdd-image-sandbox` bearbeitet werden kann. | Der Sandbox-Bezug sollte nach der allgemeinen Sicherheits-Governance und vor weiterer IDE-/Compiler-Arbeit geklaert werden. |
| 4 | `005-didactic-inline-comments` | `Lastenheft_Quellcode_Doku.md` | Nicht-triviale Compiler-, VM-, CLI-, IDE- und Test-Flows auf didaktischen Kommentarbedarf pruefen und nur sinnvolle Warum-Kommentare ergaenzen. | Der Lauf aendert kein Verhalten und verbessert die Lesbarkeit vor groesseren technischen Umbauten. |
| 5 | `006-docfx-english-site` | `Lastenheft_Dokumentation_EN.md` | DocFX-Strategie fuer eine englische Dokumentationsoberflaeche spezifizieren; Sprachumschaltung, Quellstruktur und A11Y-Nachweise klaeren. | Dokumentationssprache und Struktur sollten feststehen, bevor A11Y- und IDE-Hilfe-Texte erweitert werden. |
| 6 | `007-ide-l10n` | `Lastenheft_L10N.001-l10n-backend.md` | Nur den noch offenen IDE-L10N-Teil aus den Abschnitten 1 bis 3.2 spezifizieren; Core/VM/CLI-L10N bleibt als erledigte Referenz erhalten. | Die IDE braucht eine saubere Ressourcenbasis, bevor neue StatusBar-, A11Y- und Editor-Texte entstehen. |
| 7 | `008-ide-a11y` | `Lastenheft_A11Y_IDE.md` | Terminal.Gui-IDE auf sichtbare Textbestaetigungen, Shortcut-StatusBar, Tastaturbedienung, Kontrast und DocFX-A11Y-Pruefung ausrichten. | Baut auf IDE-L10N und Dokumentationsstrategie auf und ist ein Qualitaets-Gate vor weiteren IDE-Funktionen. |
| 8 | `009-compiler-options-parameters` | `Lastenheft_Options_Als_Parameter.md` | Compiler- und CLI-Optionen fuer Dialekt, Limits, Diagnose-/Visualisierungsmodi und Schalterstruktur sauber spezifizieren. | Gemeinsame Options- und Diagnosebasis fuer VM-CLI, IDE-Erweiterung, Optimierung und moegliche Backends. |
| 9 | `010-vm-cli` | `Lastenheft_VM_CLI.md` | Eigenen VM-CLI-Scope pruefen: P-Assembler/P-Code-Eingaben, Step/Verbose/Stack/Language-Optionen und Projektgrenzen zu `Pl0.Cli`. | Nutzt die vorher geklaerte Optionsbasis und schafft eine bessere Grundlage fuer PAsm-/PCod-Arbeit. |
| 10 | `011-ide-pasm-pcod` | `Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md` | IDE-Modi fuer P-Assembler- und P-Code-Dateien, Erkennung, Formatierung, Validierung und Ausfuehrungs-/Build-Flows spezifizieren. | Baut auf VM-/P-Code-CLI-Entscheidungen und IDE-A11Y/L10N-Regeln auf. |
| 11 | `012-pcode-optimization` | `Lastenheft_PL0_Optimierung.md` | Nur nach explizitem Architekturentscheid: Optimierungsziele, Nicht-Ziele, Schalter und Nachweisgrenzen fuer P-Code-Optimierung spezifizieren. | Derzeit durch Projektregel "keine Compiler-Optimierungen" blockiert; erst nach Governance-/ADR-Entscheid starten. |
| 12 | `013-clr-backend` | `Lastenheft_CLR_Assembly.md` | Nur nach explizitem Architekturentscheid: CLR-/CIL-Backend, `--clr=inmem`, `--clr=dll`, Sicherheitsgrenzen und Lehrnutzen spezifizieren. | Derzeit durch Projektregel "VM target only - no JIT or IL backend" blockiert und technisch hochriskant. |

## Referenz- und Abschlussliste

| Lastenheft | Einordnung | Naechste Verwendung |
|---|---|---|
| `Lastenheft_PL0_CSharp_DotNet10.md` | Basis-Portierung ist im Produktstand enthalten. | Als historische Ausgangsbasis und Traceability-Quelle verwenden, nicht als neuen Spec-Kit-Lauf starten. |
| `Lastenheft_PL0_Dokumentation.md` | Grosse Teile sind durch DocFX, Guides, API-Doku und Pages-Arbeit umgesetzt. | Als Referenz fuer `006-docfx-english-site` und A11Y-Dokupruefung verwenden. |
| `Lastenheft_IDE.md` | Core-IDE ist umgesetzt und umfangreich im Pflichtenheft protokolliert. | Als Referenz fuer `007-ide-l10n`, `008-ide-a11y` und `011-ide-pasm-pcod` verwenden. |
| `Lastenheft_L10N.001-l10n-backend.md` | Backend-L10N fuer Core/VM/CLI ist abgeschlossen; IDE-Teil bleibt offen. | Nur IDE-Teil in `007-ide-l10n` weiterfuehren. |
| `Lastenheft_VM_INC_OpCode.002-vm-inc-compat.md` | Feature `002-vm-inc-compat` ist umgesetzt. | Als Muster fuer abgeschlossene Lastenhefte mit Branch-Suffix behalten. |

## Pflegehinweise fuer spaetere Laeufe

- Vor jedem neuen Spec-Kit-Lauf dieses Dokument lesen und den ersten nicht
  erledigten, nicht blockierten Eintrag waehlen.
- Wenn ein Lauf abgeschlossen ist, Status und naechste Verwendung hier
  aktualisieren.
- Wenn ein dedizierter Feature-Branch die Anforderungen eines Lastenhefts
  umgesetzt hat, die Lastenheft-Datei gemaess Repository-Regel mit dem
  Feature-Branch-Suffix umbenennen.
- Wenn ein Lauf neue oder geaenderte oeffentliche APIs, XML-Kommentare oder
  DocFX-Inhalte betrifft, die DocFX- und A11Y-Nachweispfade aus `AGENTS.md`
  einplanen.
- Wenn ein Lauf Architektur, Security oder Governance beruehrt, die passende
  Evidenz in `docs/architecture/`, `docs/security/` oder den Spec-Kit-Artefakten
  fuehren.


## Spec-Kit-Intake-Regel / Spec Kit Intake Rule

- Diese Datei ist ein Ordnungsdokument und selbst kein Spec-Kit-Intake.
- Aktive Lastenhefte ohne Feature-Branch-Suffix koennen als Intake dienen, wenn sie Scope, Nicht-Ziele, Anforderungen, Akzeptanzkriterien und einen kopierbaren `/speckit-specify`-Prompt enthalten.
- Lastenhefte mit Feature-Branch-Suffix wie `.001-*` oder `.009-*` gelten als historisch oder abgeschlossen und werden nicht erneut gestartet.
- Vor jedem neuen Lauf wird zuerst der aktuelle Repository-Stand geprueft; erledigte Punkte werden als `AlreadySatisfied` oder `N/A` dokumentiert, nicht neu implementiert.

- This file is an ordering document and not itself a Spec Kit intake.
- Active Lastenhefte without a feature-branch suffix can be used as intake when they include scope, non-goals, requirements, acceptance criteria, and a copyable `/speckit-specify` prompt.
- Lastenhefte with a feature-branch suffix such as `.001-*` or `.009-*` are historical or completed and are not started again.
- Before every new run, first check the current repository state; completed items are documented as `AlreadySatisfied` or `N/A`, not reimplemented.


<!-- secure-development-hardening-order:start -->
## Automatisch ermittelte Lastenheft-Reihenfolge / Automatically Detected Requirements Order

Diese Tabelle wird aus `Lastenheft*.md` im Repository-Root erzeugt. Sie ist eine Vorbereitung fuer spaetere Spec-Kit-Laeufe und startet selbst keinen Lauf. Manuelle Projektentscheidungen ausserhalb dieses markierten Abschnitts bleiben erhalten.

*This table is generated from `Lastenheft*.md` in the repository root. It prepares later Spec Kit runs and does not start a run. Manual project decisions outside this marked section remain preserved.*

| Rang | Lastenheft | Gruppe | Status |
|---:|---|---|---|
| 1 | `Lastenheft_Constitution_Change.md` | Governance/Baseline | aktiv / active |
| 2 | `Lastenheft_CLR_Assembly.md` | Kernlogik/Runtime | aktiv / active |
| 3 | `Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md` | Kernlogik/Runtime | aktiv / active |
| 4 | `Lastenheft_PL0_CSharp_DotNet10.md` | Kernlogik/Runtime | aktiv / active |
| 5 | `Lastenheft_PL0_Dokumentation.md` | Kernlogik/Runtime | aktiv / active |
| 6 | `Lastenheft_PL0_Optimierung.md` | Kernlogik/Runtime | aktiv / active |
| 7 | `Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md` | Kernlogik/Runtime | aktiv / active |
| 8 | `Lastenheft_VM_CLI.md` | Kernlogik/Runtime | aktiv / active |
| 9 | `Lastenheft_VM_INC_OpCode.002-vm-inc-compat.md` | Kernlogik/Runtime | archiviert oder abgeschlossen / archived or completed |
| 10 | `Lastenheft_A11Y_IDE.md` | UI/A11Y/Dokumentation | aktiv / active |
| 11 | `Lastenheft_Dokumentation_EN.md` | UI/A11Y/Dokumentation | aktiv / active |
| 12 | `Lastenheft_IDE.md` | UI/A11Y/Dokumentation | aktiv / active |
| 13 | `Lastenheft_L10N.001-l10n-backend.md` | UI/A11Y/Dokumentation | archiviert oder abgeschlossen / archived or completed |
| 14 | `Lastenheft_RL-SE-Checklist-Selbstpruefung.md` | RL-SE-/Checklist-Selbstpruefung | aktiv / active |
| 15 | `Lastenheft_Secure-Development-Hardening.md` | Secure-Development-Hardening | aktiv / active |
| 16 | `Lastenheft_Options_Als_Parameter.md` | Weitere Anforderungen | aktiv / active |
| 17 | `Lastenheft_Quellcode_Doku.md` | Weitere Anforderungen | aktiv / active |
<!-- secure-development-hardening-order:end -->
