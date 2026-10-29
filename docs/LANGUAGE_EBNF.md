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

## Implementierungsgrenzen / Implementation limits

Die EBNF selbst ist rekursiv und nennt keine endliche Verschachtelungstiefe.
TinyPl0 begrenzt jedoch die gleichzeitig aktive Syntaxverschachtelung auf
`10`. Gezählt werden strukturierte Anweisungen (`begin`, `if`, `while`) und
geklammerte Ausdrücke. Tiefe `10` ist gültig; Tiefe `11` endet kontrolliert mit
Diagnose E36. Iterative Operator- und Semikolonfolgen erhöhen diese Tiefe nicht.

Die historische lexikalische Blockgrenze bleibt standardmäßig `3` und kann
über `CompilerOptions.MaxLevel` bis zur harten Obergrenze `10` angehoben
werden. Eine Überschreitung endet mit E32. Das Pascal-Original verwendete
ebenfalls `levmax = 3`, besaß aber keine eigene Grenze für rekursive
Anweisungen oder Klammerausdrücke. Sein VM-Datenspeicher von 500 Zellen ist
keine Parser-Stackgrenze.

*The EBNF is recursive and does not define a finite nesting depth. TinyPl0
limits simultaneously active structured statements (`begin`, `if`, `while`)
and parenthesized expressions to depth `10`. Depth `10` is accepted; depth
`11` returns diagnostic E36. Iterative operator and semicolon sequences do not
increase this depth. The historical lexical default remains `3` and
`CompilerOptions.MaxLevel` may raise it only to the hard ceiling `10`; E32
reports violations. The Pascal original used `levmax = 3` but did not bound
statement or expression recursion. Its 500-cell VM data store was unrelated to
the parser host stack.*

## Hinweise zur Kompatibilität
- `[` und `]` werden als historische Relationen fuer `<=` und `>=` akzeptiert.
- `?` und `!` sind im `classic`-Modus absichtlich nicht erlaubt.
- Der Sprachkern bleibt bewusst klein: Integer, keine Parameter, keine Rueckgabewerte.

## Referenzen
- [PL0 Referenz](../PL0.md)
- [Pflichtenheft C#](https://github.com/hindermath/TinyPl0/blob/main/requirements/baseline/Pflichtenheft_PL0_CSharp_DotNet10.pre-intake-split.2026-07-26.md)
