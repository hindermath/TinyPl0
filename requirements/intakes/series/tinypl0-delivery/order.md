# TinyPl0 Intake-Reihenfolge / Intake Order

Diese Ansicht wird aus der kanonischen Intake-Serie abgeleitet. Verbindliche
Maschinendaten stehen im [Serienmanifest](manifest.json).

*This view is derived from the canonical intake series. The linked series
manifest contains the binding machine-readable data.*

| Position | Status | Lastenheft/Intake | Abhängigkeiten / Dependencies | Spec-Kit-Feature |
|---:|---|---|---|---|
| 1 | Completed | [Lastenheft_Constitution_Change.003-constitution-change.md](../../archive/Lastenheft_Constitution_Change.003-constitution-change.md) | — (Root / keine direkte Abhängigkeit) | [003-constitution-change](../../../../specs/003-constitution-change/) |
| 2 | Completed | [Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md](../../archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md) | [Lastenheft_Constitution_Change.003-constitution-change.md](../../archive/Lastenheft_Constitution_Change.003-constitution-change.md) → current (`HardCompletionGate`, binding: true) | [004-secure-development-hardening](../../../../specs/004-secure-development-hardening/) |
| 3 | Completed | [Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md](../../archive/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md) | [Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md](../../archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md) → current (`HardCompletionGate`, binding: true) | [005-sandbox-secure-development](../../../../specs/005-sandbox-secure-development/) |
| 4 | Completed | [Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md](../../archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md) | [Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md](../../archive/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md) → current (`HardCompletionGate`, binding: true) | [006-embeddable-vm-nuget](../../../../specs/006-embeddable-vm-nuget/) |
| 5 | Eligible | [Lastenheft_Quellcode_Doku.md](../../active/Lastenheft_Quellcode_Doku.md) | [Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md](../../archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md) → current (`CommentSurfaceBaseline`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 6 | Blocked | [Lastenheft_Dokumentation_EN.md](../../active/Lastenheft_Dokumentation_EN.md) | [Lastenheft_Quellcode_Doku.md](../../active/Lastenheft_Quellcode_Doku.md) → current (`DocumentationSurfaceBaseline`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 7 | Blocked | [Lastenheft_IDE-L10N.md](../../active/Lastenheft_IDE-L10N.md) | [Lastenheft_Dokumentation_EN.md](../../active/Lastenheft_Dokumentation_EN.md) → current (`DocumentationSurfaceBaseline`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 8 | Blocked | [Lastenheft_A11Y_IDE.md](../../active/Lastenheft_A11Y_IDE.md) | [Lastenheft_IDE-L10N.md](../../active/Lastenheft_IDE-L10N.md) → current (`HardCompletionGate`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 9 | Blocked | [Lastenheft_Options_Als_Parameter.md](../../active/Lastenheft_Options_Als_Parameter.md) | [Lastenheft_A11Y_IDE.md](../../active/Lastenheft_A11Y_IDE.md) → current (`HardCompletionGate`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 10 | Blocked | [Lastenheft_VM_CLI.md](../../active/Lastenheft_VM_CLI.md) | [Lastenheft_Options_Als_Parameter.md](../../active/Lastenheft_Options_Als_Parameter.md) → current (`HardCompletionGate`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 11 | Blocked | [Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md](../../active/Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md) | [Lastenheft_VM_CLI.md](../../active/Lastenheft_VM_CLI.md) → current (`HardCompletionGate`, binding: true)<br>[Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md](../../archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md) → current (`HardCompletionGate`, binding: true) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 12 | Blocked | [Lastenheft_PL0_Optimierung.md](../../active/Lastenheft_PL0_Optimierung.md) | — (Root / keine direkte Abhängigkeit) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 13 | Blocked | [Lastenheft_CLR_Assembly.md](../../active/Lastenheft_CLR_Assembly.md) | — (Root / keine direkte Abhängigkeit) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 14 | Pending | [Lastenheft_RL-SE-Checklist-Selbstpruefung.md](../../active/Lastenheft_RL-SE-Checklist-Selbstpruefung.md) | — (Root / keine direkte Abhängigkeit) | — (kein Spec-Kit-Feature / no Spec Kit feature) |
| 15 | Pending | [Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md](../../active/Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md) | — (Root / keine direkte Abhängigkeit) | — (kein Spec-Kit-Feature / no Spec Kit feature) |

Nur `Eligible` bezeichnet die bevorzugte nächste Ausführung. `Pending` oder
`Blocked` erteilen keine automatische Ausführungsberechtigung.

*Only `Eligible` identifies the preferred next execution. `Pending` and
`Blocked` grant no automatic execution authority.*
