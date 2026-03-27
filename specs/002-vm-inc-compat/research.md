# Research: VM Compatibility for Historical `Inc` Opcode

## Findings

### Decision 1: Alias nur in textueller P-Code-Eingabe akzeptieren

**Decision**: Die neue Kompatibilitaet wird ausschliesslich in der
textuellen P-Code-Einleselogik umgesetzt, konkret in
`Pl0.Core/PCodeSerializer.ParseOpcode`.

**Rationale**:
- Die Spezifikation grenzt den Scope explizit auf textuelle VM-/P-Code-Artefakte
  ein.
- Die eigentliche VM arbeitet bereits korrekt mit `Opcode.Int`; es gibt keinen
  Grund, die Opcode-Enum, den Parser fuer PL/0-Quelltext oder die VM-Semantik zu
  aendern.
- Die bestehende Methode normalisiert Mnemonics bereits per
  `ToLowerInvariant()`, daher passt `inc` als zusaetzlicher Alias ohne
  Sonderpfad zur aktuellen Architektur.

**Alternatives considered**:
- *Enum oder Opcode-Namen auf `Inc` umbenennen*: wuerde kanonische Ausgabe und
  bestehende Dokumentation breiter destabilisieren.
- *Alias auch in Compiler-/PL0-Parser-Schichten einfuehren*: ausserhalb des
  spezifizierten Scopes, da dort keine textuellen VM-Mnemonics verarbeitet
  werden.

---

### Decision 2: Kanonische Ausgabe bleibt `int`

**Decision**: `PCodeSerializer.ToAsm`, CLI-Listings und sonstige textuelle
TinyPl0-Ausgaben behalten `int` als kanonische Schreibweise bei.

**Rationale**:
- Die Spezifikation fordert explizit, dass `Inc` nur als Eingabe-Alias gilt,
  waehrend TinyPl0 seine bestehende Ausgabeform beibehaelt.
- `ToMnemonic` mappt `Opcode.Int` bereits auf `int`, und die CLI nutzt
  `instruction.Op.ToString().ToLowerInvariant()`, was konsistent dieselbe Form
  liefert.
- So entsteht keine Migrationspflicht fuer bestehende TinyPl0-Dokumente,
  Golden-Artefakte oder Lernmaterialien, die bereits `int` verwenden.

**Alternatives considered**:
- *Ausgabe je nach Eingangstext auf `Inc` spiegeln*: waere zustandsbehaftet,
  wuerde Roundtrip-Verhalten verkomplizieren und widerspricht der geklaerten
  Spezifikation.
- *Kanonische Ausgabe auf `Inc` umstellen*: bricht existierende TinyPl0-Artefakte
  und widerspricht der Rueckwaertskompatibilitaet zu aktuellem Material.

---

### Decision 3: Regressionen ueber gezielte Serializer-Tests absichern

**Decision**: Die Verifikation erfolgt ueber fokussierte xUnit-Tests in
`tests/Pl0.Tests/PCodeSerializerTests.cs`.

**Rationale**:
- Die Aenderung sitzt vollstaendig in der Textserialisierung/-deserialisierung;
  dort liegt auch die passende Testnaht.
- Bereits vorhandene Tests decken Roundtrip und End-to-End ueber `PCodeSerializer`
  ab und lassen sich ohne neue Test-Infrastruktur erweitern.
- Positive Alias-Tests, Gleichwertigkeits-Tests und Negativtests genuegen, um
  die fachliche Wirkung belastbar abzudecken.

**Alternatives considered**:
- *Nur CLI-End-to-End testen*: waere langsamer, indirekter und weniger praezise
  bei Fehlerlokalisierung.
- *Neue Katalog- oder Golden-Artefakte*: fuer dieses Feature nicht notwendig,
  da keine PL/0-Codegenerierung und keine VM-Semantik geaendert werden.

---

### Decision 4: Maintained VM documentation an einer Stelle scharfstellen

**Decision**: `docs/VM_INSTRUCTION_SET.md` wird als normative Maintainer- und
Lehrdokumentation fuer die Begriffsbeziehung `Inc` / `Int` / `int` aktualisiert.

**Rationale**:
- Die Datei dokumentiert bereits die Opcode-Tabelle und ist der naheliegendste
  Ort fuer die geforderte Klarstellung.
- Eine einzelne, gut platzierte Doku-Aenderung reduziert das Risiko
  widerspruechlicher Erklaerungen.
- Wegen der Repo-Regeln loest die Doku-Aenderung zugleich den geplanten
  `docfx`-Regenerationsschritt aus.

**Alternatives considered**:
- *Nur Lastenheft/Pflichtenheft anpassen*: weniger nah an der taeglichen
  Referenzdokumentation fuer VM-Mnemonics.
- *Mehrere Doku-Dateien parallel anfassen*: hoehere Konsistenzlast ohne
  zusaetzlichen Erkenntnisgewinn.

---

### Decision 5: Spezifikationsannahmen als bestaetigte Planungsgrundlage behandeln

**Decision**: Die Annahmen aus `spec.md` werden fuer die Planung als bestaetigte
Arbeitsgrundlage uebernommen, weil sie in Research, Data Model und Contract
jeweils wieder aufgegriffen und nicht widersprochen werden.

**Rationale**:
- Die Scope-Annahme "nur textuelle VM-/P-Code-Artefakte" wird durch Decision 1,
  den Contract und das Data Model konsistent getragen.
- Die Annahme zur kanonischen Ausgabe `int` wird in Decision 2, im Contract und
  im Quickstart ausdruecklich bestaetigt.
- Die Annahme zur bestehenden case-insensitive Mnemonic-Logik wird in Decision 1
  und im Data Model als bestehende Abhaengigkeit dokumentiert statt nur
  vorausgesetzt.

**Alternatives considered**:
- *Annahmen unkommentiert in den Plan uebernehmen*: wuerde Task-Autoren spaeter
  zwingen, Scope und Beweisweg erneut zu erraten.
- *Annahmen vor der Task-Phase erneut oeffnen*: aktuell nicht notwendig, weil
  keine ungeloeste Widerspruchsstelle zwischen Spec und Planungsartefakten
  erkennbar ist.
