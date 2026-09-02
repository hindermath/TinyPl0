# Release- und Evidence-Vertrag / Release and Evidence Contract

**Geltung / Scope**: `TinyPl0.Core` und `TinyPl0.Vm` als ein unveränderliches
öffentliches Paketpaar. / Both packages as one immutable public package pair.

## 1. Versionsvertrag / Version contract

- `eng/TinyPl0.PackageVersion.props` enthält die einzige stabile
  `TinyPl0PackageVersion`. / The props file contains the sole stable package
  version.
- Release Please aktualisiert diese Property per `extra-files` und erzeugt
  Manifest, Release-PR und Tag. / Release Please updates the property and
  creates manifest, release PR, and tag.
- Core und VM verwenden exakt dieselbe SemVer; VM deklariert nur
  `TinyPl0.Core [version]` als Runtime-Abhängigkeit. / Both packages use exactly
  the same SemVer; VM declares only the exact Core version.
- IDE `Version`, `AssemblyVersion` und `FileVersion` folgen weiterhin dem
  getrennten vierteiligen PR-/Commit-/Build-Vertrag und importieren die
  Paketproperty nicht. / IDE version fields remain governed separately.
- Tag, Manifest, Props, nupkg, snupkg, Nuspec, Hashmanifest, SBOM, VEX,
  Attestation und Consumer-Evidence müssen dieselbe Version und denselben
  Commit nennen. / Every artifact names one version and commit.

Abweichung ist ein harter Fehler; kein Build darf still eine zweite Version
ableiten. / Any mismatch is a hard failure; no build silently derives another
version.

## 2. Paketinhalt / Package contents

Jedes nupkg enthält Assembly, XML-Dokumentation, paketbezogene zweisprachige
README, Repository URL/Type, MIT-Lizenzmetadaten, Autor, Beschreibung, Tags und
SourceLink-Metadaten. Jedes Paket besitzt ein passendes `.snupkg` mit portablem
PDB. Deterministic CI build, `PublishRepositoryUrl`, `EmbedUntrackedSources`
und Lockfiles sind aktiviert. / Each package contains the assembly, XML docs,
bilingual package README, repository/license metadata, author, description,
tags, and SourceLink metadata, plus a matching symbol package and portable PDB.
Deterministic build and locks are enabled.

Core besitzt keine Runtime-Abhängigkeit. VM besitzt nur die exakte Core-
Abhängigkeit. Ein Inventartest öffnet beide ZIPs und Nuspecs und vergleicht
Pfad, Version, Inhalt und lowercase SHA-256. / Core has no runtime dependency;
VM has only exact Core. An inventory test inspects ZIPs and nuspecs and compares
path, version, contents, and hashes.

## 3. Workflow- und Berechtigungsvertrag / Workflow and permission contract

`.github/workflows/release-please.yml` trennt:

1. `build-release`: `contents: read`, `id-token: write`,
   `attestations: write`; einmal restore/build/test/pack, Hashes, SBOM, VEX,
   Attestation und Artefakt-Upload. / Build the evidence set once.
2. `publish-nuget`: `contents: read`, `id-token: write`, Environment
   `nuget-release`; nur denselben heruntergeladenen Satz prüfen und pushen. /
   Verify and publish only that downloaded set.
3. `verify-public`: `contents: read`; öffentliche Dateien neu laden, Hashes
   vergleichen und sauberen Consumer ausführen. / Redownload, compare, and run
   the clean consumer.

Jobberechtigungen werden am Job gesetzt, nicht global erweitert. Kein Job
schreibt Repositoryinhalt oder erzeugt Providerpolicies. / Permissions are set
per job and no job writes repository content or creates provider policies.

Vollständige Action-Pins / Full action pins:

| Action | Commit SHA | dokumentierte Version / documented release |
|---|---|---|
| `googleapis/release-please-action` | `45996ed1f6d02564a971a2fa1b5860e934307cf7` | current bound revision |
| `actions/checkout` | `3d3c42e5aac5ba805825da76410c181273ba90b1` | v7.0.1 |
| `actions/setup-dotnet` | `d4c94342e560b34958eacfc5d055d21461ed1c5d` | v5 |
| `actions/upload-artifact` | `ea165f8d65b6e75b540449e92b4886f43607fa02` | v4.6.2 |
| `actions/download-artifact` | `d3f86a106a0bac45b974a628896c90dbdf5c8093` | v4.3.0 |
| `NuGet/login` | `8d196754b4036150537f80ac539e15c2f1028841` | v1.2.0 |
| `actions/attest` | `508db95dd578ae2727ebd6217d5ba78e4fbda05d` | v4.2.1 |

Ein Versionskommentar darf dem SHA folgen, ersetzt ihn aber nicht. Dependabot-
Änderungen müssen Tests, Security Review und Pinaktualisierung gemeinsam
durchlaufen. / A version comment may follow the SHA but never replace it.

## 4. OIDC-Vertrag / OIDC contract

NuGet Trusted Publishing bindet:

| Claim / policy field | Wert / Value |
|---|---|
| Owner | `hindermath` |
| Repository | `TinyPl0` |
| Workflow | `release-please.yml` |
| Environment | `nuget-release` |
| Package scope | `TinyPl0.Core`, `TinyPl0.Vm` |

`NuGet/login` tauscht das GitHub-OIDC-Token gegen einen kurzlebigen NuGet-Key.
Der Key steht ausschließlich als Step-Environment `NUGET_API_KEY` bereit und
erscheint nie in Argument, Ausgabe, Artefakt, Cache oder Datei. / NuGet/login
exchanges OIDC for a short-lived key exposed only as step environment and never
in arguments, logs, artifacts, caches, or files.

Policy-Evidence speichert nur erlaubte Identitätsfelder, Workflow-/Environment-
Bindung, Prüfzeit und Ergebnis, niemals Token oder Key. Fehlende, nicht
beweisbare oder abweichende Policy blockiert. API-Key-Fallback bleibt unter der
aktuellen No-Secret-Autorität nicht ausführbar. / Policy evidence stores only
allowed identity metadata. Missing or drifting policy blocks. API-key fallback
is not executable under current no-secret authority.

## 5. Zustandsautomat und 409 / State machine and 409

Vor dem ersten Push und nach jedem Pushversuch fragt
`tools/Pl0.ReleaseVerifier` beide öffentlichen ID-/Versionspaare ohne
Credentials ab und klassifiziert:

```text
None          -> push Core, reconcile; only then push VM, reconcile
BothMatching  -> success without push
Partial       -> fail closed; no completion push; require new SemVer
Conflict      -> fail closed; investigate; require new SemVer
Unknown       -> fail closed
```

Die praktische Zwei-Push-Reihenfolge beseitigt keine Nicht-Atomarität. Deshalb
ist schon der Zwischenabgleich nach Core Pflicht; jedes unerwartete Ergebnis
stoppt vor VM. Ein Prozess-Exitcode oder HTTP 409 ist nur Beobachtung. Erfolg
entsteht erst durch `BothMatching` mit öffentlichen nupkg-Hashes aus dem
gebundenen Set. `--skip-duplicate` ist verboten. / The intermediate Core check
is mandatory. Exit code or HTTP 409 is only an observation; success requires
BothMatching with public hashes. Skip-duplicate is prohibited.

Nach `Partial` oder `Conflict` wird nichts gelöscht oder überschrieben. Der
Release bleibt sichtbar fehlgeschlagen und eine Korrektur verwendet eine neue
SemVer für beide Pakete. / Nothing is deleted or overwritten after partial or
conflicting publication. Recovery uses a new SemVer for both packages.

## 6. Evidence-Set / Evidence set

Pflichtartefakte / Required artifacts:

- `artifacts/release/package-manifest.json`: Commit, Tag, Version, Dateipfade,
  Größen und SHA-256;
- `artifacts/release/sbom/`: CycloneDX JSON für Core und VM;
- `artifacts/release/vex/`: VEX JSON mit Disposition/Begründung/Owner/Trigger;
- `artifacts/release/attestations/`: GitHub Attestation-Referenzen und
  Verifikation;
- `artifacts/release/dependency-audit.json`: direkte/transitive Pakete,
  Vulnerability- und Lizenzstatus;
- `artifacts/release/publication-state.json`: Preflight, beide Pushausgänge,
  Zwischen-/Endabgleich und öffentliche URLs;
- `artifacts/consumer/{macos-15,ubuntu-24.04,windows-2025}/`: Restore-, Compile-,
  Run- und Step-Logs aus leerem Cache nur über NuGet.org;
- aktualisierte `docs/security/`-, `docs/architecture/`-, Traceability-, A11Y-,
  Handoff- und Statistik-Evidence.

Alle JSON-Dateien sind UTF-8, verwenden stabile IDs, repo-relative Pfade und
lowercase SHA-256. Zeitstempel stehen in UTC. Keine Datei enthält Secret,
Token, Key, vollständige fremde Exception oder private Providerantwort. / JSON
uses stable IDs, relative paths, lowercase hashes, and UTC. No evidence file
contains restricted values.

## 7. Supply-Chain- und Compliance-Disposition / Supply-chain and compliance disposition

- NIST SSDF und CWE Top 25: immer anwendbar / always applicable.
- SBOM und SLSA: anwendbar; Attestation belegt maximal SLSA Build Level 2. /
  applicable; claim capped at evidenced Build Level 2.
- VEX: anwendbar, sobald geprüfte Komponenten bewertet werden. / applicable
  when assessed components require disposition.
- OpenSSF Scorecard und SAMM: dokumentierte Bewertung mit Owner/Trigger. /
  documented assessment with owner/trigger.
- STRIDE/CIA, CAPEC, BSI C3A/C5 und CRA: projektbezogene Evidence und
  Restrisiko. / project-specific evidence and residual risk.
- ASVS, Zero Trust, AI-SBOM, NIS2, EU AI Act und DORA: derzeit begründet N/A;
  die Trigger aus Spec/Plan bleiben sichtbar. / currently justified N/A with
  visible reevaluation triggers.

## 8. Abnahmeregel / Acceptance rule

`NUGET-PUBLISH-GATE-001` ist nur erfüllt, wenn beide IDs derselben stabilen
Version öffentlich verfügbar, hashgleich, attestiert und auf allen drei
Plattformen aus leerem Cache restaurierbar sind. `MERGE-CLOSEOUT-GATE-001`
folgt erst nach Exact-Head Review, Merge, Default-Branch-Sync, öffentlichem
Handoff und kausalem Intake-Closeout. Kein lokaler Feed, API-Exitcode, 409,
Admin-Bypass oder manuelles Paketlisting ersetzt diese Evidence. / Publication
passes only with both matching public packages and the complete evidence chain.
Closeout follows only after all causal remote states. No local or partial signal
substitutes for this proof.
