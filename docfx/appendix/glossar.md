# Anhang: Glossar

Begriffe aus PL/0, P-Code und der VM.

## Address

Die Speicherposition eines Wertes in der VM.
Siehe auch: [VM-Instruction-Set](../curated/vm-instruction-set.md).

## Argument

Das dritte Feld einer P-Code-Instruktion (OPCODE LEVEL ARG).
Siehe auch: [P-Code-Handbuch](../handbook/pcode/instruction-reference.md).

## Basiszeiger (B)

Zeigt auf den aktuellen Stack-Frame.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/index.md).

## Code-Area

Der Speicherbereich mit den P-Code-Instruktionen.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/index.md).

## Compiler

Uebersetzt PL/0 in P-Code.
Siehe auch: [Architektur](../curated/architecture.md).

## Dialekt

Variante der Sprache, z. B. classic oder extended.
Siehe auch: [PL0-Handbuch](../handbook/pl0/index.md).

## EndOfFile

Token, das das Dateiende markiert.
Siehe auch: [API-Referenz](../api/index.md).

## Instruktionszeiger (P)

Program Counter, zeigt auf die naechste Instruktion.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/index.md).

## Lexer

Zerlegt Quelltext in Token.
Siehe auch: [PL0-Handbuch](../handbook/pl0/index.md).

## Level

Lexikalische Verschachtelungsebene fuer Variablenzugriff.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/instruction-reference.md).

## OPR

Opcode fuer arithmetische/vergleichende Operationen.
Siehe auch: [OPR Referenz](../handbook/pcode/instruction-reference.md).

## Parser

Analysiert Token und erzeugt P-Code.
Siehe auch: [PL0-Handbuch](../handbook/pl0/index.md).

## P-Code

Zwischencode, den die VM ausfuehrt.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/index.md).

## Stack

LIFO-Speicher fuer Werte und lokale Variablen.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/index.md).

## Stack-Top (T)

Zeigt auf das aktuelle Stackende.
Siehe auch: [P-Code-Handbuch](../handbook/pcode/index.md).

## Symboltabelle

Speichert Deklarationen von Konstanten/Variablen/Prozeduren.
Siehe auch: [PL0-Handbuch](../handbook/pl0/index.md).

## Token

Kleinstes syntaktisches Element (z. B. Ident, Number).
Siehe auch: [PL0-Handbuch](../handbook/pl0/syntax.md).

## VM

Virtuelle Maschine zur Ausfuehrung von P-Code.
Siehe auch: [VM-Instruction-Set](../curated/vm-instruction-set.md).
