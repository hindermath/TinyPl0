# TinyPl0 Sprachumfang und EBNF

## Dialekte
- `classic`: orientiert am historischen PL/0 ohne `?` und `!`.
- `extended`: enthält zusätzlich Eingabe `? ident` und Ausgabe `! expression`.

## Konsolidierte EBNF
```ebnf
program    = block "." ;

block      = [ "const" ident "=" number { "," ident "=" number } ";" ]
             [ "var" ident { "," ident } ";" ]
             { "procedure" ident ";" block ";" }
             statement ;

statement  = [ ident ":=" expression
             | "call" ident
             | "?" ident
             | "!" expression
             | "begin" statement { ";" statement } "end"
             | "if" condition "then" statement
             | "while" condition "do" statement ] ;

condition  = "odd" expression
           | expression relop expression ;

relop      = "=" | "#" | "<" | "<=" | ">" | ">=" | "[" | "]" ;

expression = [ "+" | "-" ] term { ( "+" | "-" ) term } ;

term       = factor { ( "*" | "/" ) factor } ;

factor     = ident | number | "(" expression ")" ;
```

## Hinweise zur Kompatibilität
- `[` und `]` werden als historische Relationen fuer `<=` und `>=` akzeptiert.
- `?` und `!` sind im `classic`-Modus absichtlich nicht erlaubt.
- Der Sprachkern bleibt bewusst klein: Integer, keine Parameter, keine Rueckgabewerte.

## Referenzen
- [PL0 Referenz](../PL0.md)
- [Pflichtenheft C#](../Pflichtenheft_PL0_CSharp_DotNet10.md)
