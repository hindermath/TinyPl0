# Lastenheft: Secure-Development-Hardening

**Repository:** TinyPl0  
**Dokumenttyp:** Spec-Kit Intake / Lastenheft  
**Status:** vorbereitet fuer separaten Spec-Kit-Haertungslauf  
**Stand:** 2026-06-17

## 1. Zweck

Dieses Lastenheft beschreibt den Eingangsumfang fuer einen spaeteren Spec-Kit-Haertungslauf. Ziel ist zu pruefen, ob TinyPl0 den Vorgaben aus `docs/secure-development/`, der Projekt-Constitution und den installierten Governance-Presets genuegt, und wo Nachweise oder Haertungen noch fehlen.

Das Lastenheft selbst nimmt keine Umsetzung vor. Es erzeugt nur den verbindlichen Pruef- und Dokumentationsrahmen fuer den spaeteren Lauf.

## 2. Ausgangslage

TinyPl0 enthaelt einen Compiler, eine virtuelle Maschine, CLI-Werkzeuge und eine Terminal.Gui-basierte IDE fuer PL/0- und p-code-nahe Ausbildungsarbeit. Das Projekt ist didaktisch, aber sicherheitsrelevant, weil Parser, Dateieingaben, CLI-Parameter, Assemblierung, VM-Ausfuehrung, Ausgabeformate und Dokumentationsartefakte belastbar geprueft werden muessen.

Die wiederverwendbare sichere-Entwicklung-Basis liegt in `docs/secure-development/`. Projektspezifische Sicherheitsnachweise gehoeren weiterhin nach `docs/security/` oder in die jeweils erzeugten Spec-Kit-Artefakte.

## 3. Zielbild des spaeteren Haertungslaufs

Der spaetere Spec-Kit-Lauf soll alle relevanten Pruefpunkte klassifizieren und dokumentieren:

- `Applicable`: der Pruefpunkt gilt fuer TinyPl0 und muss mit Evidenzpfad, Entscheidung und Ergebnis dokumentiert werden.
- `N/A`: der Pruefpunkt gilt nicht fuer diesen Lauf und muss mit kurzer Begruendung dokumentiert werden.
- `Open`: der Pruefpunkt gilt, ist aber noch nicht ausreichend erfuellt; Folgeaufgabe, Risiko oder technische Haertung ist zu dokumentieren.

Nicht anwendbare Punkte duerfen nicht stillschweigend ausgelassen werden.

## 4. Pruefgrundlagen

Der spaetere Lauf muss mindestens diese Grundlagen beruecksichtigen:

- `docs/secure-development/Richtlinie_Sichere-Entwicklung.md`
- `docs/secure-development/checklisten/`
- `docs/secure-development/Checklistensammelband_Sichere-Entwicklung.md`
- `constitution.md` und `.specify/memory/constitution.md`
- `docs/security/` als Standard-Evidenzpfad fuer projektspezifische Nachweise
- installierte Governance-Presets:
  - security-governance
  - architecture-governance
  - a11y-governance
  - agent-parity-governance
  - cross-platform-governance
  - isaqb-architecture-governance

## 5. Scope

Im spaeteren Spec-Kit-Lauf sollen insbesondere geprueft werden:

- Compiler- und Parser-Grenzen, Eingabevalidierung und Fehlermeldungen
- VM- und p-code-Ausfuehrung, inklusive Abbruchpfade und deterministischem Verhalten
- CLI-Optionen, Dateipfade, Ein-/Ausgabe und Exit-Codes
- Terminal.Gui-IDE, Tastaturbedienung, A11Y und robuste UI-Fehlerpfade
- Golden-Code- und Golden-Output-Paritaet
- Build-, Test- und CI-Verhalten
- Dependency- und Supply-Chain-Nachweise
- Dokumentation, Beispielprogramme und Ausbildungsartefakte
- Agentenflaechen, Spec-Kit-Artefakte und Governance-Presets

## 6. Abgrenzung

Dieses Lastenheft loest keinen Haertungslauf aus und aendert keine Produktionslogik. Es werden keine Tests, keine Security-Evidenzen in `docs/security/`, keine Spec-Kit-Feature-Branches und keine Implementierungsartefakte erzeugt. Diese Schritte folgen separat durch den spaeteren Spec-Kit-Lauf.

## 7. Mindestanforderungen an den spaeteren Spec-Kit-Lauf

1. Relevante Checklisten aus `docs/secure-development/` auswaehlen und jede Auswahl begruenden.
2. Alle Pruefpunkte als `Applicable`, `N/A` oder `Open` dokumentieren.
3. Fuer `Applicable`-Punkte konkrete Evidenzpfade in `docs/security/`, Testdateien, Codeverweisen oder Spec-Kit-Artefakten benennen.
4. Fuer `N/A`-Punkte eine kurze technische oder fachliche Begruendung erfassen.
5. Fuer `Open`-Punkte Risiko, Folgeaktion und Prioritaet festhalten.
6. Secure Coding und Secure Architecture gemeinsam bewerten; MSL-Status ersetzt keine sichere API-, I/O-, Auth-, Crypto-, Logging- oder Dependency-Pruefung.
7. CLI-, Parser-, VM- und Dateisystemgrenzen als Trust Boundaries behandeln.
8. A11Y- und didaktische Kommentar-Governance fuer IDE, CLI-Ausgaben und Dokumentation pruefen.
9. Supply-Chain-, SBOM-, AI-SBOM-, C3A-/C5- und regulatorische Punkte nur anwenden, wenn sie fuer TinyPl0 fachlich greifen; sonst als `N/A` mit Begruendung erfassen.
10. Am Ende eine nachvollziehbare Ergebnisuebersicht mit offenen Risiken, akzeptierten Restrisiken und Folgeaufgaben erstellen.

## 8. Erwartete Ergebnisartefakte des spaeteren Laufs

Der spaetere Spec-Kit-Lauf soll mindestens folgende Ergebnisarten erzeugen oder aktualisieren:

| Artefakt | Erwartung |
|---|---|
| Spec-Kit `spec.md` | Haertungsziel, Scope, Nicht-Ziele und Pruefgrundlagen dokumentiert |
| Spec-Kit `plan.md` | Pruefstrategie, Governance-Presets und Evidenzpfade dokumentiert |
| Spec-Kit `tasks.md` | Konkrete Pruef-, Dokumentations- und Haertungsaufgaben ableitbar |
| `docs/security/` | Projektspezifische Nachweise oder begruendete `N/A`-Eintraege |
| Tests/CI | Nur falls aus dem Haertungslauf erforderlich, mit klarer Begruendung |
| Abschlussnotiz | Ergebnis, offene Punkte und Restrisiken auditfaehig zusammengefasst |

## 9. Akzeptanzkriterien fuer den spaeteren Haertungslauf

- Alle relevanten Punkte aus `docs/secure-development/` sind sichtbar behandelt.
- Kein relevanter Sicherheitsstandard aus Constitution oder Governance-Presets wurde stillschweigend ausgelassen.
- Jeder ausgelassene Punkt ist als `N/A` begruendet.
- Jeder offene Punkt ist als `Open` mit Folgeaktion dokumentiert.
- Jede positive Aussage zur Einhaltung verweist auf konkrete Evidenz.
- TinyPl0 bleibt nach moeglichen spaeteren Haertungen baubar und testbar.

---

## Spec-Kit-Intake-Reife / Spec Kit Intake Readiness

Dieses Lastenheft ist als Eingabedatei fuer einen spaeteren `/speckit-specify`-Lauf vorgesehen. Vor dem Start muss der aktuelle Repository-Stand geprueft werden, damit bereits erledigte oder ueberholte Punkte nicht erneut umgesetzt werden.

*This requirements document is intended as input for a later `/speckit-specify` run. Before starting, check the current repository state so already completed or superseded items are not implemented again.*

Der spaetere Lauf muss mindestens klassifizieren:

- `Applicable`: gilt fuer diesen Lauf und braucht Umsetzung oder Evidenz.
- `AlreadySatisfied`: ist im aktuellen Stand bereits nachweisbar erledigt.
- `N/A`: gilt fuer diesen Lauf nicht und braucht eine kurze Begruendung.
- `Open`: gilt, ist aber noch nicht ausreichend geklaert oder belegt.
- `FollowUp`: fachlich relevant, aber nicht Teil dieses Laufs.

## Kopierbarer `/speckit-specify`-Prompt / Copyable `/speckit-specify` Prompt

```text
/speckit-specify Nutze Lastenheft_Secure-Development-Hardening.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen Secure-Development-Hardening-Lauf im Repository TinyPl0.

Ziel: Pruefe das Lastenheft gegen den aktuellen Repository-Stand und erstelle eine belastbare Spec-Kit-Spezifikation, die fuer Auszubildende, Entwickler*innen, Reviewer und KI-Agenten nachvollziehbar ist.

Pflichtpunkte:
- Lies dieses Lastenheft vollstaendig und uebernehme vorhandene Anforderungen, Scope-Grenzen, Reihenfolgehinweise und Akzeptanzkriterien.
- Pruefe, welche Punkte bereits umgesetzt, ueberholt oder noch offen sind.
- Klassifiziere Anforderungen als `Applicable`, `AlreadySatisfied`, `N/A`, `Open` oder `FollowUp`.
- Plane nur `Applicable`-Punkte fuer diesen Lauf.
- Dokumentiere fuer `N/A` und `FollowUp` jeweils eine kurze Begruendung.
- Beachte `constitution.md`, `.specify/memory/constitution.md`, AGENTS/CLAUDE/GEMINI/Copilot-Guidance, installierte Spec-Kit-Presets, Secure-Development-Basis, A11Y-Regeln, CEFR-B2-Verstaendlichkeit und didaktische Kommentar-Governance.
- Starte keinen weiteren Lastenheft-Lauf und kombiniere mehrere Lastenhefte nur, wenn die Kopplung fachlich begruendet und dokumentiert ist.

Erzeuge eine Spezifikation mit Scope, Nicht-Zielen, Anforderungen, Abhaengigkeiten, Akzeptanzkriterien, Risiken, Teststrategie, Evidenzpfaden und offenen Folgepunkten.
```
