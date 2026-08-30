# Cloud-Compliance-Assurance / Cloud Compliance Assurance

Deutsch: BSI C5 wird nur als Assurance-Fragenkatalog für die genutzte GitHub-
Delivery-Fläche betrachtet. Dieses Repository besitzt keine Provider-
Auditberichte und behauptet keine C5-Testierung, Zertifizierung oder regionale
Cloud-Zusage. Lokale TinyPl0-Ausführung bleibt außerhalb eines Cloud-Runtime-
Claims. Nachweisbar sind projektseitig minimale Workflow-Rechte, volle Action-
SHAs, Lockfiles, Artefakthashes und getrennte Review-Gates.

English: BSI C5 is used only as an assurance question set for the GitHub
delivery surface. The repository holds no provider audit report and claims no
C5 attestation, certification, or regional cloud commitment. Local TinyPl0 use
is outside a cloud-runtime claim. Project evidence covers least workflow
permissions, full action SHAs, lockfiles, artefact hashes, and separate reviews.

| Assurance-Thema / Topic | Ist-Evidence / Current evidence | Grenze / Boundary |
|---|---|---|
| Organisation und Rollen | Workflow-/Review-Verantwortung | keine Provider-Organisationsprüfung |
| Change/Build integrity | Git-History, Pins, SBOM, Hashmanifest | Attestation erst nach realem Providerlauf |
| Logging/Monitoring | Provider-Checklogs bei PR/Release | keine lokale Langzeitaufbewahrung belegt |
| Portabilität | Source + DocFX + Buildskripte | Pages-/Actions-Verlauf nicht automatisch portiert |
| Incident/CVD | `SECURITY.md`, RFC-9116 `security.txt` | Provider-Incidentprozess separat |

Owner: TinyPl0-Maintainer. Wiedervorlage bei Cloud-Runtime, Providerwechsel,
vertraglicher Assurance-Anforderung oder Release. Evidence-Ziel: aktueller
Providerbericht beziehungsweise dokumentierte Nichtverfügbarkeit plus
kompensierende Kontrollen.
