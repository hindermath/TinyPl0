# Installation und Nutzung

## Installation

### 1. .NET SDK 10 installieren

Stellen Sie sicher, dass das .NET 10 SDK auf Ihrem System installiert ist. Sie koennen dies ueberpruefen mit:

```bash
dotnet --version
```

Falls nicht installiert, laden Sie es von der [offiziellen Microsoft-Website](https://dotnet.microsoft.com/download/dotnet/10.0) herunter.

### 2. Repository klonen

Klonen Sie das Repository auf Ihren lokalen Rechner. Verwenden Sie dazu Git in Ihrem Terminal oder Ihrer bevorzugten Git-GUI.

**Per HTTPS:**
```bash
git clone https://github.com/thorstenhindermann/TinyPl0.git
```

**Per SSH:**
```bash
git clone git@github.com:thorstenhindermann/TinyPl0.git
```

**Per GitHub CLI:**
```bash
gh repo clone thorstenhindermann/TinyPl0
```

Wechseln Sie anschliessend in das Projektverzeichnis:
```bash
cd TinyPl0
```

### 3. Projekt bauen

Stellen Sie die Abhaengigkeiten wieder her und bauen Sie das Projekt:

```bash
dotnet restore
dotnet build
```

Um sicherzustellen, dass alles korrekt funktioniert, koennen Sie die Tests ausfuehren:

```bash
dotnet test
```

## Nutzung

Das Projekt verfuegt ueber ein Command-Line Interface (CLI), das verschiedene Befehle unterstuetzt.

Der allgemeine Aufruf lautet:
`dotnet run --project src/Pl0.Cli -- <command> [optionen] <datei>`

### Unterstuetzte Commands

- `compile`: Kompiliert eine PL/0-Quelldatei in P-Code.
- `run`: Kompiliert eine PL/0-Quelldatei und fuehrt sie direkt aus.
- `run-pcode`: Fuehrt eine bereits kompilierte P-Code-Datei aus.

### Beispiele

**PL/0-Datei direkt ausfuehren:**
```bash
dotnet run --project src/Pl0.Cli -- run tests/data/pl0/valid/feature_const_var_assignment.pl0
```

**PL/0-Datei kompilieren:**
```bash
dotnet run --project src/Pl0.Cli -- compile tests/data/pl0/valid/feature_const_var_assignment.pl0 --out mein_programm.pcode
```

**P-Code-Datei ausfuehren:**
```bash
dotnet run --project src/Pl0.Cli -- run-pcode mein_programm.pcode
```
