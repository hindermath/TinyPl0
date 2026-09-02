# TinyPl0 Intake-Reihenfolge / Intake Order

Diese Ansicht wird aus der kanonischen Intake-Serie abgeleitet. Verbindliche
Maschinendaten stehen in
`requirements/intakes/series/tinypl0-delivery/manifest.json`.

| Rang | Intake | Zustand | Abhängigkeit |
|---:|---|---|---|
| 1 | `requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md` | `Completed` | keine |
| 2 | `requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md` | `Completed` | Constitution abgeschlossen |
| 3 | `requirements/intakes/archive/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md` | `Completed` | Security abgeschlossen |
| 4 | `requirements/intakes/archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md` | `Completed` | Sandbox abgeschlossen |
| 5 | `requirements/intakes/active/Lastenheft_Quellcode_Doku.md` | `Eligible` | Embeddable VM/NuGet abgeschlossen |
| 6 | `requirements/intakes/active/Lastenheft_Dokumentation_EN.md` | `Blocked` | Kommentarhärtung |
| 7 | `requirements/intakes/active/Lastenheft_IDE-L10N.md` | `Blocked` | Dokumentationsbasis |
| 8 | `requirements/intakes/active/Lastenheft_A11Y_IDE.md` | `Blocked` | IDE-L10N |
| 9 | `requirements/intakes/active/Lastenheft_Options_Als_Parameter.md` | `Blocked` | IDE-A11Y |
| 10 | `requirements/intakes/active/Lastenheft_VM_CLI.md` | `Blocked` | Optionsbasis |
| 11 | `requirements/intakes/active/Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md` | `Blocked` | Embeddable VM/NuGet und VM-CLI |
| 12 | `requirements/intakes/active/Lastenheft_PL0_Optimierung.md` | `Blocked` | externer Architekturentscheid |
| 13 | `requirements/intakes/active/Lastenheft_CLR_Assembly.md` | `Blocked` | externer Architekturentscheid |
| 14 | `requirements/intakes/active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md` | `Pending` | unabhängige Wurzel |
| 15 | `requirements/intakes/active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md` | `Pending` | unabhängige Wurzel |

Nur `Eligible` bezeichnet die bevorzugte nächste Ausführung. `Pending` oder
`Blocked` erteilen keine automatische Ausführungsberechtigung.
