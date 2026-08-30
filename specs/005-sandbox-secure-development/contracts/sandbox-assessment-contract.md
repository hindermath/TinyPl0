# Sandbox Assessment Contract

## Vertragszweck / Contract Purpose

Der Vertrag definiert die prüfbare Mindestform von `sandbox-assessment.md` und `evidence-matrix.md`. Er prüft die Dokumente, nicht die technische Sandbox. / This contract defines the verifiable minimum shape of `sandbox-assessment.md` and `evidence-matrix.md`. It checks the documents, not the technical Sandbox.

## Pflichtartefakte / Required Artefacts

1. `specs/005-sandbox-secure-development/sandbox-assessment.md`
2. `specs/005-sandbox-secure-development/evidence-matrix.md`
3. `specs/005-sandbox-secure-development/checklists/sandbox-governance.md`
4. `specs/005-sandbox-secure-development/autonomous-run-evidence.md`

## Bewertungsvertrag / Assessment Contract

- Genau zwölf Überschriften der Form `### CL-12-NN` identifizieren `CL-12-01` bis `CL-12-12` in aufsteigender Reihenfolge.
- Jede ID besitzt Anwendbarkeit, Umsetzungsstatus, Lernstufe, Owner, DE-/EN-Begründung, Evidenz, Restrisiko und Neubewertungs-Trigger.
- `Open`, `Not Assessed`, `Partly Fulfilled` und `Not Fulfilled` verweisen auf eine `FUP-SBX-NNN`-Folgeaufgabe.
- `N/A` besitzt eine technische oder fachliche Begründung und einen Trigger.
- `Fulfilled` ist nur mit existierender, überprüfbarer Evidenz zulässig.
- Die Entscheidung unterscheidet `Not Ready`, `Conditional Pilot` und `Approved`. Dieser autonome Lauf darf `Approved` nicht selbst erteilen.

## Mount- und Schreibgrenzenvertrag / Mount and Write-Boundary Contract

- Alle Hostquellen sind symbolisch, beispielsweise `<TinyPl0-repository>`.
- Die Matrix enthält mindestens: TinyPl0-Quelle, getrenntes Build-Volume, getrennten Audit-Ausgang und nicht gemountete Secret-/Profilkategorien.
- Andere Projektfamilien, Home, Desktop, Downloads, Browserprofile, SSH/GPG, OS-Keychain und Cloud-CLI-Zustand sind `NotMounted` oder `Denied`.
- Ein TinyPl0-Write-Status darf nur positiv sein, wenn technische Writable-Roots auf diesen Scope begrenzt sind.

## Arbeitsortvertrag / Work-Location Contract

Die Matrix behandelt mindestens:

- Restore/Build/Test;
- Coverage;
- DocFX und textorientierte A11Y-Prüfung;
- Golden-Update;
- Agentenanalyse und Agentenschreiben;
- Secret-/Provideranmeldung;
- Commit/Push/PR/Merge und menschliches Review.

Jede Zeile nennt Ort, Status, Voraussetzungen, Rückfallweg und Beweisgrenze.

## Sicherheits- und Datenschutzvertrag / Security and Privacy Contract

- Keine Zeile enthält einen absoluten Benutzer-Hostpfad.
- Keine Datei enthält Secret-Werte, private Endpunkte, Token, Cookies, Agentenprofile, Cache- oder Sitzungsinhalte.
- Der Scan darf nur die Delivery-Dateien lesen. Fremde Secret-Dateien oder nicht freigegebene Profile werden nicht geöffnet.
- Referenzen auf Secrets verwenden Kategorien oder symbolische Speicherwege.

## A11Y- und Sprachvertrag / A11Y and Language Contract

- Nutzerseitige Abschnitte bieten Deutsch zuerst und Englisch danach.
- Status und Entscheidungen sind als Text verständlich; Farbe oder Layout ist nicht erforderlich.
- Tabellen besitzen beschreibende Spaltenüberschriften.
- Codeblöcke haben Sprachkennzeichen.
- Technische Begriffe werden bei erster Verwendung erklärt oder auf das Glossar verwiesen.

## Scope-Vertrag / Scope Contract

Gegen den Feature-Baseline-Commit dürfen nur folgende Flächen geändert sein:

- `.specify/feature.json`;
- `specs/005-sandbox-secure-development/**`;
- `docs/project-statistics.md` nach Implementierungsabschluss;
- `docs/project-statistics.config.json` nur für den neuen, renderergeprüften Phasenslot;
- `src/Pl0.Ide/Pl0.Ide.csproj` ausschließlich für verpflichtende Versionsmetadaten.

Änderungen an Produktcode, Tests, Workflows, Sandbox-Repository, Sandbox-Image oder bestehenden Dateien unter `docs/security/` sind unzulässig.

## Red/Green-Evidenz / Red/Green Evidence

- Red: Vor Erstellung fehlen `sandbox-assessment.md` und `evidence-matrix.md`; der unveränderte Vertrag meldet ausschließlich diese fehlenden Ergebnisartefakte.
- Green: Nach Erstellung erfüllt derselbe Vertrag alle oben genannten Strukturregeln.
- Die Red-/Green-Prüfung ist keine Behauptung über Sandbox-Runtime, Produktcode oder Freigabe.
