# 1.Aktuell
## Core Principles

### I. Didaktische Klarheit (Pedagogical Clarity)

Code MUST prioritize readability and educational value over performance or cleverness.
All source code, comments, and documentation MUST be written in German (the project's target audience
is German-speaking Fachinformatiker trainees). Every compiler phase (Lexer, Parser, Symbol Table,
Code Generator, VM) MUST remain clearly separated and independently comprehensible.
Implementation shortcuts that obscure the learning path are forbidden.

**Rationale**: TinyPl0 is a teaching artefact. A trainee reading the code must be able to trace
the full compilation pipeline step-by-step without expert guidance.

# 2. Änderungen
Hinzufügen/Änderungen zu ### I. Didaktische Klarheit (Pedagogical Clarity)
- Texte zusätzlich in Englisch. Der deutsche Textblock zuerst und danach der englische Textblock.
- die deutsche und englischen Texte sollen dem Sprachniveau B2 (nach Gemeinsamer Europäischer Referenzrahmen für Sprachen – abgekürzt GER bzw. international CEFR (Common European Framework of Reference for Languages). ) entsprechen, da auch nicht-muttersprachliche Auszubildenden an diesem Projekt ausgebildet werden und sollen alles in dem Projekt verstehen können.
- I. Didaktische Klarheit (Pedagogical Clarity) ändern zu I. Didaktische und sprachliche Klarheit (Pedagogical and Linguistic Clarity)
- XML-Kommentare sollen für alle Methoden, Klassen, Variablen  etc. im Quellcode verwendet werden.
- An geeigneten Stellen kann der Quellcode darüber hinaus noch mit Block- oder Zeilen-Kommentaren versehen werden um auf wichtige Didaktische Aspekte hinzuweisen. Auch diese in Deutsch und Englisch.
- All documentation — code, API reference, guides, and examples — MUST serve as learning material for IT application-development specialists (Fachinformatiker Anwendungsentwicklung):
- Every public type, member, parameter, and return value MUST carry complete XML documentation (<summary>, <param>, <returns>, and <exception> where applicable; <remarks> and <example> where instructive).
Comments explain the why (decision, trade-off, constraint), not only the what.
- Missing XML documentation for public API members is treated as a build error (CS1591 MUST NOT be suppressed globally).
- When API signatures or XML comments change, the docfx output MUST be regenerated in the same commit/PR.
- Use CLAUDE.md, GEMINI.md, copilot-instructions.md and AGENTS.md for runtime agent-specific development guidance.
- Es sollte imer nochmal überprüft werden, ob die Dokumentationsrichtlinien eingehalten worden sind. Wenn nicht, sollen die Fehlende Dokumentation nachgeholt werden.
- 

