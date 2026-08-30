# Recherche: Sandbox-gestützte sichere Entwicklung / Research: Sandbox-Supported Secure Development

## Zweck und Beweisgrenze / Purpose and Proof Boundary

Diese Recherche bewertet versionierte, öffentlich repository-taugliche Fakten. Sie liest keine Secret-Datei, kein Agentenprofil, keinen Cache und keinen Sitzungszustand. Der Sandbox-Kontext wird ausschließlich am Commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0` betrachtet. Änderungen seiner separaten Arbeitskopie sind weder übernommen noch als Nachweis verwendet.

*This research assesses versioned, public-ready repository facts. It reads no secret file, agent profile, cache, or session state. The Sandbox context is assessed only at commit `05d1202b3364cba3d0e48e6c32e10b34a62ba1f0`. Changes in its separate working copy are neither adopted nor used as evidence.*

## Entscheidung 1: Referenzstand / Decision 1: Reference State

**Decision**: Exakter Git-Commit plus seine versionierten Dateien ist die Beobachtungsbasis; ein gebautes Image und ein Laufzeitcontainer sind nicht belegt.

**Rationale**: Dadurch bleiben Befunde reproduzierbar, ohne fremde Arbeitskopien oder Providerzustand zu verändern. Ein Git-Commit ist jedoch kein Image-Digest und keine Betriebsfreigabe.

**Alternatives rejected**:

- Aktuelle Sandbox-Arbeitskopie: enthält separate, nicht übernommene Arbeiten und wäre keine stabile Evidenz.
- Image-Build/Start in diesem Lauf: würde externe Repository-/Runtime-Zustände verändern und überschreitet den Dokumentationsscope.

## Entscheidung 2: Technische .NET-Eignung / Decision 2: Technical .NET Suitability

**Decision**: `Plausible`, nicht `Proven`. Der referenzierte Dockerfile-Stand basiert auf einem digest-gepinnten .NET-10-SDK, nutzt einen .NET-Build-Wrapper und hält Build-Ausgaben in einem getrennten Volume. Das passt grundsätzlich zu TinyPl0.

**Missing evidence**: akzeptierter finaler Image-Digest; Toolchain-Smoke auf diesem Digest; TinyPl0 `restore/build/test`, Coverage, DocFX und A11Y auf demselben Stand; Plattform- und Fehlerprotokoll.

**Re-evaluation**: Bei genehmigtem Pilot oder neuem Sandbox-Commit.

## Entscheidung 3: Isolation und Rechte / Decision 3: Isolation and Privileges

**Decision**: Teilweise erfüllt. Positiv sind Non-Root-Laufzeit, `no-new-privileges`, `cap_drop: ALL`, getrennte Agent-/Build-Volumes, Secret-Leseverbote und deaktivierte automatische Updates/Telemetrie. Nicht ausreichend für TinyPl0-only-Agentenschreiben sind die vielen beschreibbaren Projektwurzeln, einschließlich Sandbox-Checkout und weiterer Projektfamilien.

**Risk**: Ein Agent kann trotz engem Prompt außerhalb des TinyPl0-Mounts schreiben. Prozessregeln ergänzen technische Kontrollen, ersetzen sie aber nicht.

**Required follow-up evidence**: minimaler Writable-Root-Vertrag oder technisch erzwungener read-only Agentenmodus; genehmigte symbolische Mount-Liste; negativer Schreibgrenzentest.

## Entscheidung 4: Secrets und Toolzustand / Decision 4: Secrets and Tool State

**Decision**: Konzeptuell geeignet, betrieblich offen. Agentzustand liegt in benannten Volumes, typische Secret-Variablen werden aus Shell-Unterprozessen entfernt und Secret-Pfade sind für Codex gesperrt. Provideranmeldung und echte Secret-Injektion bleiben menschlich verantwortet und wurden nicht geprüft.

**Rule**: Kein Secret-Wert, keine konkrete private Hostposition und kein Inhalt eines Agentenprofils darf TinyPl0-Evidenz werden.

## Entscheidung 5: Netzwerk / Decision 5: Network

**Decision**: `Open`. Compose nutzt freien Egress. Codex-Workspace-Shellnetz ist standardmäßig aus und Paket-/Netzwerkbefehle brauchen Approval, aber die Containergrenze selbst besitzt keine Allow-List. Das Netzwerkdokument enthält keinen ausgefüllten aktuellen Annahmezeitraum.

**Trade-off**: Paketregister, Quellabrufe und Modellprovider benötigen Verbindungen; eine pauschale Blockade würde Lern- und Buildarbeit erschweren. Eine zeitlich befristete, menschlich akzeptierte Pilotentscheidung oder eine zielbezogene Egress-Regel ist nötig.

## Entscheidung 6: Formelle Freigabe / Decision 6: Formal Approval

**Decision**: `Open / Not Fulfilled`. Die versionierte Freigabedatei nennt ausdrücklich `Entwurf, Freigabe ausstehend`; Owner, Datum und Ablauf sind nicht ausgefüllt. Ein PR, Admin-Bypass, Tool-Installationsstatus oder Agentenurteil kann diese Human-only-Entscheidung nicht ersetzen.

## Entscheidung 7: Supply Chain / Decision 7: Supply Chain

**Decision**: Teilweise erfüllt. Basisimage und viele Werkzeuge sind gepinnt, Release-Artefakte werden geprüft und Syft-Skripte sind vorhanden. Ein für TinyPl0 tatsächlich verwendetes Image-SBOM, Scan-Ergebnis, VEX-Status und Provenienzbezug zum finalen Image fehlen in diesem Lauf.

**Separation**:

- Sandbox-Image-Evidenz gehört zum Sandbox-Release.
- TinyPl0-Paket-/SBOM-/VEX-/SLSA-Evidenz gehört zum TinyPl0-Release.
- Beide werden verlinkt, aber nicht zu einer einzigen SBOM vermischt.

## Entscheidung 8: Arbeitsort / Decision 8: Work Location

| Arbeit | Bevorzugter Ort | Aktueller Status | Beweisgrenze |
|---|---|---|---|
| Spec/Plan/Tasks lesen | TinyPl0-Orchestrator lokal | `Supported` | Kein Sandbox-Nachweis. |
| Read-only Quellinspektion im Container | künftiger Sandbox-Pilot | `Open` | Formelle Pilotfreigabe und exakte Identität fehlen. |
| Restore/Build/Test/Coverage | künftiger Sandbox-Pilot, dann CI | `Open` | Toolchain plausibel, TinyPl0-Ausführung fehlt. |
| DocFX/A11Y | Sandbox oder CI nach Toolinventar | `Open` | Node-/Browser-/lynx-Pfad auf akzeptiertem Image muss belegt werden. |
| Golden-Update | Lokal nach ausdrücklicher fachlicher Änderung | `N/A` | Dieses Feature ändert keine Compilerausgabe. |
| Secret-/Provideranmeldung | geschützte lokale Betriebsfläche | `Open` | Kein Repository- oder Promptinhalt. |
| Commit/Push/PR/Merge | autorisierter TinyPl0-Orchestrator | `Supported` unter aktueller Autorität | Exact-Head-Review bleibt Pflicht. |

## Ergebnis / Outcome

`absdd-image-sandbox` ist für TinyPl0 technisch vielversprechend, aber am beobachteten Stand nicht für reguläre autonome Schreibarbeit freigegeben. Die risikoarme nächste Stufe ist ein separat genehmigter Read/Build/Test-Pilot mit minimalem Mount, read-only Nebenwurzeln, exakter Image-Identität, aktueller Egress-Entscheidung und ohne Secrets. Technische Härtung bleibt eine eigene Aufgabe.

*The Sandbox is technically promising for TinyPl0 but is not approved for regular autonomous write work at the observed state. The low-risk next step is a separately approved read/build/test pilot with a minimal mount, read-only secondary roots, exact image identity, a current egress decision, and no secrets. Technical hardening remains a separate task.*
