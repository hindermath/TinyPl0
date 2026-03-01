# Lokalisierung

## 1. Einleitung
Dieses Dokument beschreibt die Anforderungen an die Lokalisierung (L10N) für das Projekt TinyPl0. Ziel ist es, die Software für verschiedene Sprachräume (initial Deutsch und Englisch) anpassbar zu machen.

## 2. Zielsetzungen
* Trennung von Programmlogik und sprachspezifischen Ressourcen.
* Unterstützung von Deutsch (de-DE) und Englisch (en-US).
* Einfache Erweiterbarkeit um weitere Sprachen.
* Pl0.Cli soll die Lokalisierung unterstützen und einen Sprachenparameter erhalten.
* In der Pl0.Ide soll die Sprache in den Einstellungen einstellbar sein.

## 3. Anforderungen

### 3.1 Unterstützte Sprachen
* **Deutsch (Standard):** Alle Ausgaben und Fehlermeldungen in deutscher Sprache.
* **Englisch:** Alle Ausgaben und Fehlermeldungen in englischer Sprache.

### 3.2 Lokalisierte Komponenten
Folgende Elemente müssen lokalisiert werden:
* Fehlermeldungen des Compilers/Interpreters.
* Hilfe-Texte der Kommandozeile (CLI).
* Statusmeldungen während des Kompiliervorgangs.
* Das Interface der IDE (Pl0.Ide).


### 3.3 Technischer Standard
* **Zeichenkodierung:** Alle Sprachdateien müssen in UTF-8 kodiert sein.
* **Ressourcen-Management:** Verwendung von Standard-Mechanismen des .Net Core Frameworks zur Speicherung der Strings.

## 4. Glossar
* **L10N (Localization):** Anpassung der Software an einen spezifischen Zielmarkt.
* **I18N (Internationalization):** Vorbereitung der Software, um mehrere Sprachen technisch zu unterstützen.
