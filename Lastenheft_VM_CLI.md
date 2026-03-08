# Lastenheft VM CLI

## 1. Einleitung
- Dieses Dokument beschreibt die Funktionalitäten und Anforderungen für die VM CLI (Virtual Machine Command Line Interface) des Projekts.

## 2. CLI Parameter

## 2.CLI Parameter
- Die VM CLI unterstützt verschiedene Parameter, um die Interaktion mit den virtuellen Maschinen zu steuern.
- es gibt die folgenden Parameter
  - Hilfe mit `--help` und `-h`
  - P-Assembler-Datei mit `-a` oder `--pasm`
  - P-Code-Datei mit `-c` oder `--pcode`
  - 
## 2.2 Optionale Parameter
- Die  VM CLI unterstützt die folgenden optionalen Parameter 
  - Debug-Modus mit `-s` oder `--step`
  - Verbose-Ausgabe mit `-v` oder `--verbose`
  - Stack-Size mit `-t` oder `--stack`
  - Sprache mit `-l` oder `--language`
  
## 3. Funktionalitäten
- Die VM CLI unterstützt verschiedene Funktionalitäten, um virtuelle Maschinen zu steuern.
  - Ausführen von P-Assembler-Dateien
  - Ausführen von P-Code-Dateien
  - Debugging mit Schritt-für-Schritt-Ausführung
  - Verbose-Ausgabe für Debugging
  - Stack-Size-Einstellung für virtuelle Maschinen
  - Spracheinstellung für virtuelle Maschinen
  - Hilfe mit `-h` oder `--help`
  
## 4. Projektstruktur
- Ein eigenständiges Projekt unter src/ mit dem Projekt-Namen Pl0.Vm.Cli
- Abhängigkeiten von Pl0.Vm und dessen Abhängigkeiten

