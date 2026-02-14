# Architektur (kuratiert)

Dieses Kapitel stellt die Architektur von TinyPl0 zusammengefasst dar und bindet
die originalen Detailinformationen aus `docs/ARCHITECTURE.md` ein.

## Zusammenfassung

- Drei Projekte: CLI, Core, VM.
- Compiler-Pipeline: Lexer -> Parser -> Codegenerator.
- VM fuehrt den P-Code aus und kapselt I/O.

## Originalinhalt

[!include[](../../docs/ARCHITECTURE.md)]
