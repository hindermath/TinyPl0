# ADR 0003: Festes Parser-Verschachtelungsbudget / Fixed Parser Nesting Budget

## Status

Accepted — 2026-10-29

## Deutsch

Nicht vertrauenswürdiger PL/0-Quelltext konnte den rekursiv-absteigenden Parser
über Prozedurblöcke, strukturierte Anweisungen oder Klammerausdrücke bis zum
CLR-Stacküberlauf treiben. Historisches `levmax = 3` deckte nur Prozedurblöcke
ab und meldete Überschreitungen, ohne den Abstieg zu stoppen.

TinyPl0 begrenzt deshalb gleichzeitig aktive strukturierte Anweisungen und
Klammerausdrücke fest auf `10`. E36 beendet den gesamten Parsevorgang genau
einmal und kontrolliert. Die lexikalische Blocktiefe bleibt standardmäßig `3`,
ist über `CompilerOptions.MaxLevel` bis `10` konfigurierbar und stoppt darüber
mit E32. Beide Kontrollen liegen direkt in `Pl0Parser`, damit keine öffentliche
Fassade sie umgehen kann.

Der feste Wert verändert weder den öffentlichen positional-record-Konstruktor
noch dessen `Deconstruct`-Vertrag und kann von einem Host nicht versehentlich
deaktiviert werden. Der Trade-off ist bewusst: Formal rekursive EBNF-Programme
ab Tiefe `11` werden abgewiesen. Der vorhandene Korpus benötigt höchstens zwei
Klammer- und drei `begin`-Ebenen; `10` bewahrt daher einen deutlichen Lehr- und
Experimentierraum. Eine spätere Erhöhung erfordert plattformübergreifende
Stackevidence und eine neue Architekturentscheidung.

## English

Untrusted PL/0 source could drive the recursive-descent parser through
procedure blocks, structured statements, or parenthesized expressions until
the CLR stack overflowed. Historical `levmax = 3` covered procedure blocks only
and reported violations without stopping descent.

TinyPl0 therefore fixes the combined active depth of structured statements and
parenthesized expressions at `10`. E36 terminates the complete parse exactly
once and in a controlled way. Lexical block depth retains default `3`, is
configurable through `CompilerOptions.MaxLevel` up to `10`, and stops with E32
above that ceiling. Enforcement lives directly in `Pl0Parser`, so no public
facade can bypass it.

The fixed value preserves the public positional-record constructor and its
`Deconstruct` contract, and a host cannot accidentally disable the protection.
The accepted trade-off is that formally recursive EBNF programs at depth `11`
are rejected. The existing corpus needs at most two parenthesis levels and
three `begin` levels, leaving substantial teaching and experiment headroom.
Any later increase requires cross-platform stack evidence and a new
architecture decision.
