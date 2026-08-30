# Architektur der Secure-Development-Härtung / Hardening Architecture

## Kontext / Context

Unvertrauenswürdiger PL/0-Quelltext und P-Code durchlaufen Core und VM. CLI und
IDE bilden Datei-/Nutzergrenzen; DocFX/Pages und Restore bilden Liefergrenzen.
Das Feature ändert nur die VM-Ressourcenpolicy bedingungslos.

```text
Source -> Pl0.Core -> Instructions -> Pl0.Vm -> Result
Files/CLI ---------------------------> |
IDE --------------------------------> |
Docs/registries -> build/evidence (separate delivery boundary)
```

## Building Blocks

- `Pl0.Core`: unverändert, keine Projektabhängigkeit.
- `Pl0.Vm`: Optionen, gemeinsame Vorvalidierung, Batch und Step.
- `Pl0.Cli`/`Pl0.Ide`: unveränderte Verbraucher von Core+VM.
- `Pl0.Tests`: Budget-, Options-, L10N-, Golden- und Architekturbeweis.

## Runtime und Deployment / Runtime and Deployment

Vor einer VM-Allokation werden Stack und Budget geprüft. Jede erfolgreiche
Instruktion verbraucht genau eine Einheit. Bei ausgeschöpftem Budget folgt vor
der nächsten Auswahl ein terminaler Fehler. Deployment, Modulgraph und VM-
Instruction Set bleiben sonst unverändert. Es entsteht kein Dienst, Port oder
neues Privileg.

## Quality Attributes

- Sicherheit: fail-safe Vorvalidierung und Defense in Depth.
- Zuverlässigkeit: deterministische `N`/`N+1`-Grenze und stabile Diagnosen.
- Wartbarkeit: kleiner gemeinsamer Validator, keine breite VM-Zusammenlegung.
- Lehrwert: DE-/EN-Warum-Kommentare erklären Zählpunkt und Allokationsgrenze.
- Kompatibilität: alter Vier-Parameter-Aufruf, Opcodes und Golden bleiben.

## Risiken und technische Schuld / Risks and Debt

Das Budget ist keine Zeit-/Sandboxgarantie. Batch/Step behalten getrennte
Loops, wodurch Parität dauerhaft getestet werden muss. HTTP- und UI-Härtung
bleiben getrennte Findings. Re-evaluation: neue VM-Option, Runtime, Modul-
Abhängigkeit oder Deploymentform. Methodik: arc42 und iSAQB CPSA-F.
