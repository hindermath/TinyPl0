# PL0-Code Optimierung 

## Beschreibung
In diesem Abschnitt werden die Strategien und Techniken zur Optimierung des generierten PL/0-Codes beschrieben. Ziel ist es, die Effizienz des Zielcodes zu steigern und die Ausführungszeit sowie den Speicherbedarf zu minimieren.

## Optimierungstechniken
* **Constant Folding:** Berechnungen mit konstanten Werten werden bereits zur Kompilierzeit durchgeführt.
* **Dead Code Elimination:** Nicht erreichbarer Code oder Anweisungen ohne Auswirkung werden entfernt.
* **Algebraische Vereinfachungen:** Mathematische Ausdrücke werden vereinfacht (z.B. `x + 0` oder `x * 1`).
* **Register-Optimierung:** Effizientere Nutzung der virtuellen Register der Zielmaschine.

## Compiler-Switches
| Switch            | Bedeutung                       | Hinweis/Beispiel                                     |
|-------------------|---------------------------------|------------------------------------------------------|
| `--opt=fold`      | Constant Folding                | z. B. konstante Ausdrücke zur Compile-Zeit auswerten |
| `--opt=dce`       | Dead Code Elimination           | nicht erreichbaren/wirkungslosen Code entfernen      |
| `--opt=alg`       | Algebraische Vereinfachungen    | z. B. `x + 0`, `x * 1` vereinfachen                  |
| `--opt=reg`       | Register-Optimierung            | effizientere Nutzung der (virtuellen) Register       |
| `--opt=all`       | alle Optimierungen aktivieren   | Sammelschalter                                       |
| `--no-opt`        | alle Optimierungen deaktivieren | überschreibt ggf. gesetzte `--opt=...`               |
| `--no-opt=<name>` | eine Optimierung deaktivieren   | z. B. `--no-opt=fold`                                |
So kann man in Tests einzelne Optimierungen gezielt prüfen.
