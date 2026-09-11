# Sicherheit verlinkter Intake-Evidence / Linked Intake Evidence Security

## Umfang und Vertrauensgrenze / Scope and trust boundary

Der dependencyfreie Node-Renderer liest das repositorylokale
`tinypl0-delivery`-Manifest, die darin referenzierten Intake-Dateien und
vorhandene Feature-Abschlusszustände. Er erzeugt ausschließlich zwei
Markdown-Ansichten. Manifestwerte bleiben Daten: Sie werden weder als Code
noch als Kommando oder Option ausgeführt. Produkt-, Compiler-, VM-, Paket-,
Netzwerk-, Authentifizierungs- und Deploymentgrenzen ändern sich nicht.

*The dependency-free Node renderer reads the repository-local
`tinypl0-delivery` manifest, its referenced intake files, and existing feature
completion states. It generates only two Markdown views. Manifest values
remain data and are never executed as code, commands, or options. Product,
compiler, VM, package, network, authentication, and deployment boundaries do
not change.*

## Bedrohungen und Kontrollen / Threats and controls

| Risiko | CIA-Bezug | Kontrolle und Evidence |
|---|---|---|
| Manipulierte oder unvollständige Eingabe | Integrität | Striktes UTF-8, NUL-Ablehnung, Schema-/Typprüfung und normalisierte SHA-256-Bindung; `LIE001`, `LIE002`, `LIE009`. |
| Pfadtraversal oder Symlink-Flucht | Vertraulichkeit/Integrität | Nur sichere repositoryrelative Pfade; Realpath muss innerhalb des Repositorys bleiben; `LIE003`–`LIE005`. |
| Doppelte Identität oder Ausgabeüberschreibung | Integrität | Eindeutige Ziele und Positionen; Outputs dürfen weder Quellen noch einander überlappen; `LIE006`. |
| Ungültige Kante, Wurzel oder Zyklus | Integrität/Verfügbarkeit | Bekannte Kantenarten und Bindungen, vorhandene Endpunkte, Vorwärtsreihenfolge, berechnete Wurzeln und DAG-Prüfung; `LIE007`. |
| Erfundenes oder mehrdeutiges Feature-Evidence | Integrität/Nachvollziehbarkeit | Genau ein abgeschlossener Zustand mit übereinstimmendem akzeptiertem Artefakthash und vier terminalen Closeout-Feldern; `LIE008`. |
| Teilweise oder widersprüchliche Veröffentlichung | Integrität/Verfügbarkeit | Alle Kandidaten werden vorab gebildet und semantisch verglichen. Erkannte Fehler rollen den Stand zurück; ein gemeinsamer SHA-256-Generationsmarker macht einen extern unterbrochenen Mehrdateien-Replace erkennbar und beim nächsten Write reparierbar; `LIE010`, `LIE011`. |

Diagnosen nennen ausschließlich stabile Fehlerklassen und sichere relative
Subjects. Der Renderer schreibt keine Credentials, Umgebungsvariablen,
Providerlogs, absoluten privaten Pfade oder SQLite-Zustände. Ein unveränderter
zweiter Write-Lauf meldet null Schreibvorgänge.

*Diagnostics expose only stable error classes and safe relative subjects. The
renderer writes no credentials, environment variables, provider logs,
absolute private paths, or SQLite state. A second unchanged write run reports
zero writes.*

## Governance-Disposition

| Standard/Familie | Status | Begründung / Trigger |
|---|---|---|
| MSL und Secure Coding für JavaScript | Applicable, erfüllt | JavaScript bleibt die vorhandene speichersichere Skriptsprache; kein `eval`, keine neue Dependency und validierte Datei-I/O-Grenzen. Trigger: Sprach-, Runtime- oder Dependencywechsel. |
| NIST SSDF, CWE Top 25, STRIDE/CIA und CAPEC | Applicable, erfüllt | Eingabe-, Pfad-, Graph-, Proof- und Publication-Grenzen besitzen positive und negative Tests. Trigger: neue Eingabe oder Trust Boundary. |
| OWASP SAMM | Applicable, erfüllt | Requirement, Implementierung, Tests und lokale Review-Evidence sind nachvollziehbar verbunden. Trigger: Prozess- oder Reviewmodell ändert sich. |
| OWASP ASVS | N/A / Not Assessed | Keine Web-, API-, Authentifizierungs- oder Sessionfläche. Trigger: eine solche Fläche tritt in Scope. |
| SBOM, VEX, AI-SBOM, SLSA und OpenSSF Scorecard | N/A / Not Assessed | Keine neue Dependency, Paket-, Release- oder Supply-Chain-Auswahl. Trigger: entsprechender Diff. |
| Zero Trust, BSI C3A/C5, NIS2, CRA, EU AI Act und DORA | N/A / Not Assessed | Keine Netzwerk-, Cloud-, AI-System- oder regulierte Produktgrenze. Trigger: entsprechender Scope. |

Owner ist der TinyPl0 Repository Owner; Reviewer ist die Security-Rolle des
Feature-032-Slices. Restrisiko bis zum Delivery-Checkpoint sind native
Linux-/Windows-Parität und unabhängiger PR-Review. Re-Evaluation bei Eingabe-,
Pfad-, Graph-, Dependency-, Netzwerk-, Produkt- oder Trust-Boundary-Änderung.

*The owner is the TinyPl0 Repository Owner; the reviewer is the security role
for the Feature 032 slice. Residual risk until delivery is native Linux/Windows
parity and independent pull-request review. Re-evaluate on input, path, graph,
dependency, network, product, or trust-boundary changes.*
