# Sicherheits-Qualitätsszenarien / Security Quality Scenarios

**Feature**: `004-secure-development-hardening`
**Methode / Method**: iSAQB CPSA-F quality scenarios, arc42
**Stand / Date**: 2026-08-30

| ID | Quelle und Auslöser / Source and stimulus | Umgebung / Environment | Reaktion / Response | Messwert / Measure |
|---|---|---|---|---|
| QS-01 | PL/0/P-Code erzeugt Endlosschleife | Batch und Step | vor `N+1` terminale Budgetdiagnose | genau `N`, Default `1_000_000`, gleiche Codes |
| QS-02 | Budget `0/-1` | Initialisierung | Diagnose vor Zustand/Allokation | keine Ausführung und keine Exception |
| QS-03 | Stack `<3` oder `>1_000_000` | Initialisierung | stabile Stackdiagnose | keine Addition `StackSize+1`, keine Allokation |
| QS-04 | beschädigtes P-Code/Pointer | VM-Lauf | kontrollierter Fehler | keine interne Exception an Nutzer |
| QS-05 | Pfad/Datei unlesbar | CLI oder IDE | Ursache plus nächste Aktion | kein Stack-Trace, kein Datenverlust |
| QS-06 | Traversal/unerwartete Methode | Loopback-HTTP | sicher ablehnen | ASVS-L1 Applicable IDs erfüllt; High=0 |
| QS-07 | Dependency mit Critical/High-CVE | Restore/Review | Delivery blockieren | Scan-Exit/Evidence und Owner vorhanden |
| QS-08 | veröffentlichbarer Kandidat | Pages/Release | SBOM+Hash+Pin binden | genau ein Kandidatenhash, truthful VEX/SLSA |
| QS-09 | Vulnerability report | öffentliches Repo | sicherer auffindbarer CVD-Pfad | RFC-9116-Felder und Reaktionsziele |
| QS-10 | geänderte API-Seite | DocFX | axe und Textbrowser bestehen | 0 Critical/Serious; verständliche lynx-Reihenfolge |
| QS-11 | Build-/Testfehler | lokaler serialisierter Writer | sicher stoppen | kein falscher Pass, Version bleibt nachvollziehbar |
| QS-12 | Recovery nach VM-Fehler | neuer Lauf | neuer isolierter Zustand | keine Diagnoseverdopplung im alten Step-State |

## Nicht anwendbar / Not applicable

Konten, Authentifizierung, Rollen, Produktkryptografie, personenbezogene Daten
und Cloud-Runtime existieren nicht. Trigger: Einführung einer dieser Flächen.
Die Szenarien sind interne Abnahmeevidence und keine Zertifizierung.
