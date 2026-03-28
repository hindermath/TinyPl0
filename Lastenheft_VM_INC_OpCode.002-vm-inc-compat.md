# Feature-Beschreibung: VM-Kompatibilität für Opcode `Inc`

## Kurzbeschreibung
TinyPl0 soll neben dem heute verwendeten Opcode `Int` auch die historische Schreibweise `Inc`
für die Stack-Reservierung unterstützen. Beide Schreibweisen stehen für dieselbe VM-Operation:
Der Stack wird um die im Argument angegebene Anzahl Zellen erweitert.

Ziel ist eine kompatible Verarbeitung historischer PL/0-Artefakte und von Varianten aus dem
Delphi-Port, ohne das bestehende Verhalten des aktuellen .NET/C#-Ports zu verändern.

## Problem und Nutzen
In historischen PL/0-Quellen und in älteren Portierungen wird die Stack-Reservierung teils mit
`Inc`, teils mit `Int` bezeichnet. TinyPl0 kennt derzeit nur `Int`. Dadurch können Programme,
Listings oder P-Code-Darstellungen aus anderen Referenzen nicht ohne Anpassung übernommen oder
verglichen werden.

Mit dieser Erweiterung soll TinyPl0 beide Schreibweisen verstehen und konsistent ausführen. Das
verbessert die Nachvollziehbarkeit gegenüber der historischen Vorlage und reduziert Reibung beim
Vergleich zwischen Pascal-, Delphi- und .NET-Version.

## Zielbild
- Historische PL/0-Referenzen mit `Inc` können in TinyPl0 fachlich korrekt eingeordnet werden.
- P-Code oder Listings mit `Inc` werden von TinyPl0 als dieselbe Stack-Reservierungsoperation wie
  `Int` behandelt.
- Bereits vorhandene Programme, Tests und Dokumentationen mit `Int` bleiben weiterhin gültig.
- Das Laufzeitverhalten der VM bleibt für die Stack-Reservierung unverändert und kompatibel.

## Fachlicher Umfang
Die Funktion umfasst die fachliche Gleichstellung von `Inc` und `Int` als Bezeichnung derselben
Operation.

Im Scope:
- TinyPl0 erkennt `Inc` als alternative Bezeichnung der bisherigen Stack-Reservierung.
- TinyPl0 führt Programme mit `Inc` und `Int` mit identischer Semantik aus.
- Bestehende Artefakte mit `Int` bleiben lauffähig und behalten ihr bisheriges Verhalten.
- Relevante Dokumentation und Tests werden an die Doppelbezeichnung angepasst.

Nicht im Scope:
- Neue VM-Semantik jenseits der Stack-Reservierung.
- Änderungen am Stackmodell, an Aktivierungsrahmen oder an der Registerlogik.
- Compiler-Erweiterungen außerhalb der Benennung beziehungsweise Erkennung dieser Operation.
- Allgemeine Modernisierung historischer Opcodes über `Inc` und `Int` hinaus.

## Fachliche Anforderungen
- TinyPl0 muss `Inc` als gültige Bezeichnung für die Stack-Reservierung akzeptieren.
- TinyPl0 muss `Int` weiterhin unverändert akzeptieren.
- Beide Schreibweisen müssen zur gleichen VM-Wirkung führen: Der Stack wird um die angegebene
  Anzahl Zellen erweitert.
- Historische oder externe Artefakte mit `Inc` dürfen nicht an der abweichenden Benennung
  scheitern, solange sie fachlich derselben Operation entsprechen.
- Bestehende TinyPl0-Artefakte mit `Int` dürfen durch die Erweiterung kein anderes
  Laufzeitverhalten erhalten.
- Die Systemdokumentation muss klar erklären, dass `Inc` und `Int` in TinyPl0 fachlich
  gleichwertig behandelt werden.

## Annahmen für die Spezifikation
- `Inc` ist in C# kein reserviertes Schlüsselwort und darf daher als Bezeichner verwendet werden.
- `Int` bleibt die bisher etablierte Benennung im aktuellen TinyPl0-Bestand.
- Die historische Referenz und der Delphi-Port beschreiben fachlich dieselbe Stack-Operation,
  auch wenn die Benennung abweicht.

## Akzeptanzhinweise
Die Funktion gilt als fachlich erfüllt, wenn die folgenden Beobachtungen möglich sind:

1. Ein Programm oder P-Code-Artefakt mit `Inc` wird von TinyPl0 akzeptiert und lauffähig
   verarbeitet.
2. Ein fachlich identisches Artefakt mit `Int` zeigt dasselbe Ausführungsverhalten wie mit `Inc`.
3. Bereits vorhandene Tests oder Programme mit `Int` bleiben ohne inhaltliche Änderung gültig.
4. Die VM-Dokumentation beschreibt die Beziehung zwischen `Inc` und `Int` eindeutig und
   widerspruchsfrei.

## Relevante Randfälle
- Ein Artefakt verwendet `Inc`, obwohl TinyPl0 intern bisher nur `Int` kennt.
- Historische Unterlagen und aktuelle TinyPl0-Dokumentation verwenden unterschiedliche
  Bezeichnungen für dieselbe Operation.
- Bestehende Serialisierung, Deserialisierung oder Anzeige von Opcodes darf nicht uneindeutig
  werden.

## Referenzen
- Historische und Delphi-nahe Referenz:
  `https://github.com/hindermath/PL0Delphi?tab=readme-ov-file#pl0-compiler-source-code`
- Lokale Pascal-/Delphi-nahe Referenz im Repository:
  `pl0c.pas`
- Aktuelle TinyPl0-VM-Dokumentation:
  `docs/VM_INSTRUCTION_SET.md`
