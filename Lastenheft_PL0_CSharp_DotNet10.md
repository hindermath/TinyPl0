# Lastenheft: PL/0-Portierung nach C#/.NET 10

## 1. Zielbild (aus Anwendersicht)
Ziel ist ein didaktischer, nachvollziehbarer Beispiel-Compiler, der den historischen PL/0-Ansatz von Pascal nach C#/.NET 10 überführt und für Ausbildung, Lehre und Übungen nutzbar macht.

Das Produkt soll:
- PL/0-Programme einlesen, prüfen und in P-Code übersetzen,
- den erzeugten P-Code in einer VM ausführen,
- den kompletten Lernweg von Sprache, Compilerbau und Laufzeit transparent machen,
- den Übergang von Anforderung (Lastenheft) zu Umsetzung (Pflichtenheft) beispielhaft dokumentieren.

## 2. Ausgangslage und Idee
Ausgangspunkt war die vorhandene Referenzdokumentation in `PL0.md` und der Pascal-Compiler in `pl0c.pas`.

Der fachliche Impuls war:
- die historische Referenz nicht nur nachzubauen,
- sondern sie in eine moderne .NET-10-Lernumgebung zu übertragen,
- inklusive Testbarkeit, CI und nachvollziehbarer Entwicklungsphasen.

## 3. Entstehung der Anforderungen (Werdegang der Konversation)
Die Anforderungen wurden schrittweise in der Konversation konkretisiert und geschärft:

1. Analyseauftrag:
- Aus `PL0.md`, EBNF, Pascal-Quelltext und Zusatzinformationen sollte zunächst ein belastbarer Umsetzungsplan/Pflichtenheft entstehen.

2. Vertiefung der Referenzanalyse:
- Nicht nur die Theorie (EBNF), sondern ausdrücklich die konkrete Pascal-Beispielimplementierung sollte in die Zieldefinition einfließen.

3. Methodische Nachvollziehbarkeit:
- Es wurde eine explizite Pascal->C# Mapping-Tabelle pro zentraler Prozedur/Funktion gefordert (`getsym`, `block`, `interpret`, usw.).

4. Qualitäts- und Testfokus:
- Das Pflichtenheft sollte mit realen `.pl0`-Beispieldaten erweitert werden,
- inklusive Sprachfeature-Tests und späterer Vollständigkeitsprüfung der Testabdeckung.

5. Sprachpräzisierung über ANTLR:
- Das EBNF sollte mithilfe der ANTLR-Referenz (`pl0.g4`) geschärft werden,
- insbesondere bzgl. `?` (Input) und `!` (Output).

6. Umsetzungssteuerung in Phasen:
- Die im Pflichtenheft beschriebenen Phasen wurden nacheinander in der Implementierung umgesetzt:
  - Phase 0: Projektvorbereitung,
  - Phase 1: Projektbasis + CLI-Switch-Basis,
  - Phase 2: Lexer,
  - Phase 3: Parser/Symboltabelle/Codegenerator,
  - Phase 4: VM,
  - Phase 5: CLI + End-to-End,
  - Phase 6: Qualität + Dokumentation.

7. Kontinuierliche Integrationsanforderung:
- Nach jeder wesentlichen Phase wurde Commit und Push auf `main` verlangt.

8. Nachweisorientierte Abnahmefragen:
- Mehrfach wurde abgefragt, ob die Testarten, Testdatenkataloge, Abnahmekriterien und Liefergegenstände vollständig erfüllt sind.
- Daraus entstanden zusätzliche konkrete Nachbesserungen (fehlende Testfälle, Artefakte, Architektur-Guard-Test, Traceability/Coverage-Gates, CLI-Kantenfälle).

## 4. Fachliche Kernanforderungen (Lasten)
Aus der Konversation ergaben sich folgende Muss-Anforderungen:

- Portierung des PL/0-Beispielcompilers von Pascal nach C#/.NET 10.
- Unterstützung der PL/0-Kernsprache inklusive `const`, `var`, `procedure`, `call`, `if/then`, `while/do`, Ausdrucksregeln.
- Unterstützung der I/O-Erweiterung mit `? ident` und `! expression` im erweiterten Dialekt.
- Kompatible VM mit P-Code-Instruktionssatz (`lit`, `opr`, `lod`, `sto`, `cal`, `int`, `jmp`, `jpc`).
- Historische Grenzen/Guardrails als prüfbare, konfigurierbare Limits.
- CLI mit Pascal-nahen Switches/Aliassen.
- Reproduzierbare Testdatenbasis mit valid/invalid/dialect/limits/runtime-io-edge.
- Nachvollziehbare Dokumentation für Ausbildungskontext.

## 5. Nicht-funktionale Anforderungen
- Plattform: .NET 10.
- Didaktische Lesbarkeit vor Optimierung.
- Deterministische Tests und CI-Lauffähigkeit.
- Klare Diagnosen und saubere Fehlerbehandlung statt Prozessabsturz.

## 6. Übergang zum Pflichtenheft
Aus diesen Lasten wurde das Dokument
`/Users/thorstenhindermann/Codex/TinyPl0/Pflichtenheft_PL0_CSharp_DotNet10.md`
abgeleitet und iterativ erweitert.

Der Übergang folgte dem Muster:
- Lasten (Was/Wozu) ->
- technische Spezifikation (Wie) ->
- phasenweise Implementierung ->
- testgetriebene Absicherung ->
- Abnahme gegen definierte Kriterien.

## 7. Ausbildungsnutzen
Dieses Lastenheft dokumentiert nicht nur das Zielsystem, sondern auch den Lern- und Entscheidungsweg:
- wie aus einer anfänglichen Idee konkrete Anforderungen werden,
- wie diese in ein Pflichtenheft übersetzt werden,
- und wie Umsetzung, Tests und Qualitätssicherung an den Anforderungen rückgekoppelt werden.

Damit dient das Projekt als durchgängiges Ausbildungsbeispiel für:
- Requirements Engineering,
- Compilerbau,
- Teststrategie,
- iterative Produktabnahme.

## 8. Zugehörige Referenzdokumente
- Fachliche Ausgangsbasis:
  - `/Users/thorstenhindermann/Codex/TinyPl0/PL0.md`
  - `/Users/thorstenhindermann/Codex/TinyPl0/pl0c.pas`
- Technische Spezifikation:
  - `/Users/thorstenhindermann/Codex/TinyPl0/Pflichtenheft_PL0_CSharp_DotNet10.md`
- Architektur/Qualität:
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/ARCHITECTURE.md`
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/QUALITY.md`
  - `/Users/thorstenhindermann/Codex/TinyPl0/docs/TRACEABILITY_MATRIX.md`

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
/speckit-specify Nutze Lastenheft_PL0_CSharp_DotNet10.md als verbindliche Eingabedatei. Erstelle die Feature-Spezifikation fuer einen projektspezifischen Spec-Kit-Lauf im Repository TinyPl0.

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
