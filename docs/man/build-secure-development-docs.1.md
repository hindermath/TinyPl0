# build-secure-development-docs(1)

## Name / Name

`build-secure-development-docs` — Secure-Development-Baseline prüfen oder
atomar erzeugen / validate or atomically generate the secure-development baseline

## Synopsis / Synopsis

```text
pwsh -NoProfile -File scripts/build-secure-development-docs.ps1 [-Check] [-WhatIf]
bash scripts/build-secure-development-docs.sh [--check|--dry-run]
```

## Beschreibung / Description

Deutsch: PowerShell 7 ist die Engine. Sie liest zwölf kanonische Checklisten,
prüft Manifest- und Dokumentversionen, 157 eindeutige IDs sowie die Reihenfolge
im Sammelband. `-Check` schreibt nichts. `-WhatIf` zeigt die geplante atomare
Ersetzung. Fehler liefern einen Nonzero-Exit und nennen die fehlerhafte Grenze.

English: PowerShell 7 is the engine. It reads twelve canonical checklists and
validates manifest/document versions, 157 unique IDs, and compendium order.
`-Check` performs no write. `-WhatIf` describes the planned atomic replacement.
Failures return nonzero and identify the failed boundary.

## Sicherheit / Security

Die Skripte lesen keine Secrets, verwenden kein `eval` oder
`Invoke-Expression`, quotieren Pfade und erzeugen temporäre Ausgabe nur im
System-Tempverzeichnis. / The scripts read no secrets, use no dynamic execution,
quote paths, and create temporary output only in the system temporary directory.
