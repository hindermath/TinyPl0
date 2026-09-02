# Lieferketten-Evidenz / Supply Chain Evidence

## Feature 006: NuGet-Pakete / NuGet Packages

**Feature**: `006-embeddable-vm-nuget`
**Lauf / Run**: `a01cd5bd-fa86-49f1-b074-cb59a9c24862`
**Stand / Date**: 2026-09-02
**Evidence-Status / Evidence status**: `Pre-publication`; exakter PR-Head und
Provider-Nachweise folgen in der temporären PreMerge- und öffentlichen
Release-Evidence. / The exact PR head and provider proof follow in temporary
PreMerge and public release evidence.

Deutsch: scripts/Test-NuGetPackages.ps1 erzeugt und prüft beide Paket- und
Symbolpakete, die exakte VM-zu-Core-Abhängigkeit und einen unabhängigen
.NET-10-Consumer. scripts/New-NuGetReleaseEvidence.ps1 bindet vier Dateien,
Commit und SemVer an SHA-256, SPDX 2.3, OpenVEX und lokale in-toto-Provenienz.
Lokale Provenienz bleibt ausdrücklich unterhalb einer Provider-Attestierung.
AI-SBOM, ASVS und Zero Trust sind N/A; CRA bleibt Open. Öffentliche URLs,
Provider-Attestierung, Scorecard und Consumer-Hashes werden erst nach dem
autorisierten Remote-Release in docs/release/nuget-release-evidence.md
eingetragen.

*English: The package script validates both package pairs, the exact VM-to-Core
dependency, and an independent .NET 10 consumer. The evidence script binds all
four files, commit, and SemVer to SHA-256, SPDX 2.3, OpenVEX, and local in-toto
provenance. Local provenance is explicitly below provider attestation.
AI-SBOM, ASVS, and Zero Trust are N/A; CRA remains Open. Public evidence is
recorded only after the authorised remote release.*

## Bestehende Feature-004-Pages-Basis / Existing Feature 004 Pages Baseline

**Projekt / Project**: TinyPl0 (Level 2)
**Feature**: `004-secure-development-hardening`
**Lauf / Run**: `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7`
**Ausgewerteter Commit / Evaluated commit**: `ce14b69e9cdf529c6af44b446a2d243b7b22d501`
**Stand / Date**: 2026-08-30

Deutsch: Diese Evidence bindet den lokal erzeugten DocFX-Pages-Kandidaten an
ein normalisiertes Dateimanifest und ein CycloneDX-SBOM. Sie behauptet keine
veröffentlichte Provenienz, keine Provider-Attestierung und kein erreichte
SLSA-Stufe. English: This evidence binds the locally generated DocFX Pages
candidate to a normalized file manifest and a CycloneDX SBOM. It does not
claim published provenance, provider attestation, or an achieved SLSA level.

## Maschinenlesbarer Datensatz / Machine-readable record

Die vollständige, ordinal nach Pfad sortierte Liste liegt in
`docs/security/supply-chain-evidence.json`. Der Datensatz enthält 1.385 Dateien
aus dem temporären `_site/`-Kandidaten. Die selbstreferenzielle Seite
`docs/security/supply-chain-evidence.html` und der davon abhängige Suchindex
`index.json` sind ausdrücklich ausgeschlossen; beide können ihren eigenen Hash
nicht stabil enthalten. `_site/` selbst bleibt ignoriert und gehört nicht zum
getrackten Delivery-Set. / The ordinal path-sorted record contains 1,385 files
from the temporary `_site/` candidate. The self-referential evidence page and
its dependent search index are explicitly excluded because they cannot stably
contain their own hash. `_site/` remains ignored and is not tracked.

| Objekt / Object | Wert / Value |
|---|---|
| normalisiertes Artefaktmanifest / normalized artifact manifest | `feb58953a86cb31696d7ec4c934b0ed99f6489f4e418c1e34bb19acc5e6d0000` |
| CycloneDX-JSON / CycloneDX JSON | `46e930c23bb224f091f91e346525813b8a859504b195f0b91b1ed81f1783a899` |
| SBOM-Format | CycloneDX 1.7, 47 Komponenten / components |
| Generator | CycloneDX .NET `6.2.0` |
| NuGet-Outdated-Inventar | `6a74e4d3bcc347e1941afa6cfe7c54e4803a44645f7ffbf06a2a4dbbe07412e6` |
| NuGet-Vulnerability-Inventar | `18053740bd61ffd6e6ce709b03898f29b25f795e864f63d1565b002660978e73` |

Die beiden Paketprotokolle waren vorbereitet, vollständig und secret-geprüft.
Sie wurden offline gelesen; private Paketquellen und Zugangsdaten wurden weder
kontaktiert noch übernommen. Beide Inventare melden null bekannte Critical-
oder High-Schwachstellen. Verfügbare Updates wurden dokumentiert, aber kein
Paket wurde still geändert. / The two package logs were prepared, complete,
and secret-scanned. They were consumed offline without contacting or copying
private sources or credentials. They report zero known Critical or High
vulnerabilities. Available updates were recorded, but no package was changed.

## VEX, SLSA und Scorecard / VEX, SLSA, and Scorecard

- **VEX**: `NotRequiredNoKnownFinding`. Es gibt im geprüften Inventar keinen
  bekannten CVE-Befund, der eine Produktstatus-Aussage benötigt. Ein neuer
  Fund löst eine VEX-Bewertung aus. / No known finding currently needs a VEX
  product-status statement; a new finding triggers one.
- **SLSA**: Ist-Zustand ist ein lokaler, commitgebundener Manifest- und
  SBOM-Nachweis. Es wird keine SLSA-Stufe und keine veröffentlichte Provenienz
  behauptet. Ziel ist eine Provider-Provenienz erst nach erfolgreichem
  Remote-Workflow und Prüfung der erzeugten Attestierung. / Current evidence
  is local and commit-bound. No SLSA level or published provenance is claimed.
- **OpenSSF Scorecard**: `Open`. In dieser lokalen Phase wurde kein externer
  Scorecard-Lauf erfunden. Owner ist der TinyPl0-Maintainer; Trigger ist der
  spätere Remote-Delivery-Review, Evidenzziel ist der verlinkte Providerlauf.
  / No external Scorecard run is invented; the later remote delivery review is
  the trigger for provider evidence.
- **GitHub Actions**: Die geprüften Lieferketten-Workflows verwenden volle
  40-Hex-Action-SHA-Pins. Provider-Ausführung und Attestierung folgen erst an
  der ausdrücklich autorisierten Remote-Grenze. / Reviewed supply-chain
  workflows use full action SHA pins; provider execution remains a later gate.

## MSL und sichere Entwicklung / MSL and secure development

C#/.NET ist die primäre speichersichere Sprache (MSL). Das reduziert typische
Speicherfehler, ersetzt aber keine Eingabevalidierung, Ressourcenbegrenzung,
sichere Datei-/Netzwerkgrenzen, Dependency-Prüfung oder Fehlerbehandlung. Die
VM-Budget- und Stack-Grenzen, NIST SSDF, CWE Top 25 und die C#/.NET-Secure-
Coding-Regeln gelten weiter. / C#/.NET is the primary memory-safe language.
This reduces common memory errors but does not replace input validation,
resource limits, safe I/O boundaries, dependency review, or error handling.

## KI- und Datenschutzgrenze / AI and privacy boundary

KI wird ausschließlich als Entwicklungswerkzeug eingesetzt. TinyPl0 liefert
kein Modell, keinen Datensatz, keinen Inferenzdienst und keine KI-Runtime aus;
deshalb ist eine Produkt-**AI-SBOM** derzeit begründet `N/A`. Trigger sind ein
ausgeliefertes oder betriebenes Modell, ein Datensatz, eine Inferenzplattform
oder eine KI-Runtime. / AI is development tooling only. TinyPl0 ships no model,
dataset, inference service, or AI runtime, so a product AI-SBOM is reasoned
`N/A`; any such product component triggers reassessment.

KI-erzeugte Änderungen benötigen eine menschliche Vier-Augen-Review durch eine
vom Implementierer getrennte Rolle. Prompts dürfen keine Zugangsdaten, private
Paketquellen, personenbezogene Daten oder unveröffentlichte Geheimnisse
enthalten. Diese Phase hat nur bereinigte lokale Inventare verwendet. / AI-
generated changes require human four-eyes review by a role separate from the
implementer. Prompts must not contain credentials, private package sources,
personal data, or unpublished secrets. This phase used sanitized local inputs.

## Grenzen und Wiedervorlage / Boundaries and follow-up

Owner ist der TinyPl0-Maintainer; die unabhängige Supply-Chain-Review bleibt
eine getrennte Rolle. Erneute Prüfung erfolgt bei Paket-, Lockfile-, Workflow-,
Release- oder Provideränderung, bei einem neuen Advisory oder spätestens am
2026-11-30. Die lokale Evidence ersetzt keine externe Zertifizierung. / The
maintainer owns follow-up and the independent supply-chain reviewer remains a
separate role. Reassess on package, lock, workflow, release, provider, or
advisory change, and no later than 2026-11-30. Local evidence is not an
external certification.
