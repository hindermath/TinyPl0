# Lastenheft zur VM-Kompatibilität von OpCode `Inc`

## Anforderung
Im Original PL/0-Compiler von Nilaus Wirth wurde die OpCode `Inc` implementiert, um den Stack um X Elemente zu erhöhen. Diese Operation ist fundamental für die Ausführung von PL/0 P-Code-Programmen und muss mit der VM kompatibel sein, um die Ausführung von PL/0-Programmen zu ermöglichen.
Aus der ersten Umsetzung in DELPHI gab es Inkompatibilitäten mit der `Inc`-Funktion in Turbo Pascal/DELPHI.
In dieser Version der VM soll die `Inc`-Operation korrekt implementiert werden, um die Ausführung von PL/0-Programmen zu ermöglichen. Aber der `Ìnc` OpCode soll gleichwertig parallel zum `Ìnt` OpCode implementiert werden.
`Ìnc` und `Ìnt` OpCodes sollen also die gleiche Operation sein, um die Ausführung von PL/0-Programmen zu ermöglichen. Sowohl historische PL/0-Programme als auch aktuell erstellte Programme aus dem DELPHI-Port und dem aktuell vorloegenden .Net/C#-Port.

## Technische Umsetzung
Zu prüfen ware vor der Implementierung, ob der einzufügenden `Inc` OpCode mit einem Schlüsselwort in C# kollidiert und die Aufnahme dieses OpCodes in der VM verhindert. 