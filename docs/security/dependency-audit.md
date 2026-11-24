# Abhängigkeits-Audit / Dependency Audit: TinyPl0

## Feature 006: Paketgrenze / Package Boundary

Deutsch: TinyPl0.Core besitzt keine Produkt-Laufzeitabhängigkeit. TinyPl0.Vm
besitzt genau eine Abhängigkeit: TinyPl0.Core in derselben exakten SemVer. Die
bestehenden Test- und IDE-Pakete bleiben außerhalb beider NuGet-Pakete.
dotnet-list-vulnerable ist vor Delivery erneut auszuführen; jeder bekannte
Critical-Fund blockiert. VEX bleibt bei null bekannten Funden leer und wird
bei einem Fund neu bewertet.

*English: TinyPl0.Core has no product runtime dependency. TinyPl0.Vm has one
dependency only: the exact matching TinyPl0.Core SemVer. Test and IDE packages
remain outside both packages. The vulnerability inventory must be refreshed
before delivery; any known critical finding blocks. VEX is reassessed when a
finding appears.*

**Projekt / Project**: TinyPl0 (Level 2)

**Feature / Feature**: `004-secure-development-hardening`

**Lauf / Run**: `abaa7b81-fd2c-47e7-8d59-87a852a3b2e7`

**Datum / Date**: 2026-08-30

**Owner**: TinyPl0-Maintainer

**Review**: unabhängige Security-/Supply-Chain-Review

**Standards**: NIST SSDF, CWE Top 25, SBOM/VEX, SLSA, Lizenz- und
Registry-Prüfung / licence and registry review

## DE — Aufnahmeentscheidung CycloneDX .NET 6.2.0

Der lokale Tool-Pin verwendet den offiziellen NuGet-Paketnamen `CycloneDX`,
Version `6.2.0`, und den Befehl `dotnet-CycloneDX`. `dotnet tool restore` und
die Versionsausgabe `6.2.0+55877e2...` waren erfolgreich. NuGet nennt die
CycloneDX-Organisation als Owner, Apache-2.0 als Lizenz, den 27. April 2026 als
Veröffentlichungsdatum und das öffentliche Projekt
`CycloneDX/cyclonedx-dotnet`. Die offizielle Release-Seite führt 6.2.0 als
aktuelle gepflegte Version und dokumentiert Sicherheitsverbesserungen der
6.x-Linie. Die öffentliche Repository-Sicherheitsanzeige und die zum
Prüfzeitpunkt durchsuchte GitHub Advisory Database zeigen keinen bekannten
Critical- oder High-Eintrag für das Tool 6.2.0.

Die Aufnahme ist deshalb für die lokale SBOM-Erzeugung freigegeben. Das ist
eine zeitgebundene Beobachtung, keine Garantie. Ein neuer Advisory-Eintrag,
ein Versionswechsel, ein geänderter Paket-Owner oder ein Release-Kandidat löst
eine neue Prüfung aus. Das Tool erhält keine Credentials; es verarbeitet nur
Repository- und wiederhergestellte Paketmetadaten.

## EN — CycloneDX .NET 6.2.0 admission decision

The local tool pin uses the official NuGet package ID `CycloneDX`, version
`6.2.0`, and command `dotnet-CycloneDX`. Tool restore and the version command
completed successfully. NuGet identifies the CycloneDX organisation as owner,
Apache-2.0 as the licence, 27 April 2026 as the publication date, and the public
`CycloneDX/cyclonedx-dotnet` project as source. The official release page lists
6.2.0 as the maintained current version. At review time, the public repository
security view and GitHub Advisory Database search showed no known Critical or
High advisory for tool version 6.2.0.

Admission is approved for local SBOM generation. This is a dated observation,
not a guarantee. A new advisory, version change, package-owner change, or
release candidate triggers a renewed review. The tool receives no credentials.

## Abhängigkeits- und Registry-Matrix / Dependency and Registry Matrix

| Fläche / Surface | Quelle / Source | Pin/Lock | Lizenzprüfung / Licence review | Critical/High | Zustand / State |
|---|---|---|---|---:|---|
| CycloneDX .NET tool | `https://www.nuget.org/packages/CycloneDX/6.2.0` | `.config/dotnet-tools.json`, exakt `6.2.0` | Apache-2.0, kompatibel / compatible | 0 bekannt / known | Approved |
| C#/.NET-Pakete | NuGet.org laut Projektdateien / per project files | Projektpins; kein Solution-`packages.lock.json` | T062-Inventur wird unten fortgeschrieben / recorded below by T062 | Prüfung vor Abschluss / reviewed before completion | Applicable |
| DocFX | globale vorhandene Toolchain / existing global toolchain | beobachtet `2.78.5` | Toolchain-Evidence | Prüfung vor Abschluss / reviewed before completion | Applicable |
| Playwright | npmjs, `@playwright/test` | `tests/a11y/package-lock.json`, `1.62.1` | Apache-2.0 | Prüfung vor Abschluss / reviewed before completion | Applicable |
| axe | npmjs, `@axe-core/playwright` und `axe-core` | `tests/a11y/package-lock.json`, `4.13.0` | MPL-2.0, nur Testwerkzeug / test tool only | Prüfung vor Abschluss / reviewed before completion | Applicable |
| GitHub Actions | GitHub Marketplace/Repository | vollständige 40-Hex-SHAs / full SHAs | Workflow-Quellreview / source review | 0 offen im Feature-Scope | Applicable |

## Lock-file-Stand / Lock-file status

| Datei / File | Vorhanden / Exists | Rolle / Role | Zustand / State |
|---|---:|---|---|
| `.config/dotnet-tools.json` | Ja / Yes | reproduzierbarer Tool-Pin / reproducible tool pin | `CycloneDX` 6.2.0, Roll-forward aus / off |
| `tests/a11y/package-lock.json` | Ja / Yes | reproduzierbares Node-24-Testset / reproducible Node 24 test set | `npm ci --offline` erfolgreich / passed |
| Solution-`packages.lock.json` | Nein / No | NuGet-Abhängigkeitslock / dependency lock | Restrisiko; Maintainer prüft bei Dependency-Änderung / residual risk; review on dependency change |

## Offene Prüf- und Wiederholungsgrenzen / Open review and recurrence boundaries

- Vor dem Abschluss werden direkte und transitive NuGet-Pakete mit
  `dotnet list ... --outdated` und `--vulnerable` inventarisiert. / Direct and
  transitive NuGet packages are inventoried before completion.
- npm-Pins, Lizenzen und bekannte Critical/High-Funde werden gegen das echte
  Lockfile geprüft. / npm pins, licences, and known findings are checked against
  the genuine lock file.
- Remote-Attestierung, OpenSSF Scorecard und publizierte Provenienz sind lokale
  Nichtaussagen. Sie werden erst nach realer Provider-Evidence positiv. / Remote
  attestation, Scorecard, and published provenance require provider evidence.
- Neubewertung spätestens 2026-11-30 oder bei Dependency-, Lockfile-, Workflow-
  oder Release-Änderung. / Re-evaluate by 2026-11-30 or on dependency, lock,
  workflow, or release change.

## T062-Inventur / T062 Inventory

Die vorbereiteten, secret-geprüften Inventare wurden offline ausgewertet; es
wurde keine Paketquelle kontaktiert und keine Quellenadresse in diese Evidence
übernommen. / The prepared, secret-scanned inventories were evaluated offline;
no package source was contacted and no source address is copied into this
evidence.

| Inventar / Inventory | SHA-256 | Ergebnis / Result |
|---|---|---|
| `dotnet-outdated.log` | `6a74e4d3bcc347e1941afa6cfe7c54e4803a44645f7ffbf06a2a4dbbe07412e6` | Direkte Updates sichtbar; keine stille Paketänderung / direct updates visible; no silent update |
| `dotnet-vulnerable.log` | `18053740bd61ffd6e6ce709b03898f29b25f795e864f63d1565b002660978e73` | Alle fünf Projekte: keine bekannten verwundbaren Pakete / all five projects: no known vulnerable packages |

| Direktes Paket / Direct package | Aufgelöst / Resolved | Neueste beobachtet / Latest observed | Lizenz-/Wartungsgrenze / Licence and maintenance boundary |
|---|---:|---:|---|
| `coverlet.collector` | 8.0.0 | 10.0.1 | MIT; Test-/Coverage-Werkzeug, Update getrennt prüfen |
| `Microsoft.NET.Test.Sdk` | 18.0.1 | 18.9.0 | MIT; gepflegte Testplattform |
| `xunit` | 2.9.3 | ohne direkte Updatezeile / no direct update row | Apache-2.0; gepflegtes Testframework |
| `xunit.runner.visualstudio` | 3.1.5 | 4.0.0 | Apache-2.0; Major-Update separat prüfen |
| `Terminal.Gui` | 2.0.0 | 2.4.17 | MIT; produktive IDE-Abhängigkeit, Update separat testen |

Transitive Pakete zeigen verfügbare neuere Versionen, aber keinen gemeldeten
CVE-Fund. Besonders `Newtonsoft.Json 13.0.3`, `System.Text.Json 8.0.5`,
Roslyn-/SourceLink-8.0-Komponenten und `Terminal.Gui`-Transitives bleiben beim
nächsten Dependency-PR erneut zu prüfen. Veraltet bedeutet nicht automatisch
verwundbar; deshalb wurde in dieser Phase kein Paket geändert. / Transitive
packages have newer versions available but no reported CVE. Outdated does not
mean vulnerable, so this phase changes no package.

Der npm-Lock bindet Playwright `1.62.1` und axe `4.13.0`; CycloneDX ist als
lokales Tool auf `6.2.0` gepinnt. Full-SHA-Actions, Lockfile und SBOM werden in
der Supply-Chain-Evidence getrennt gebunden. Critical/High: `0`. Lizenzkonflikt:
`0` bekannt. Nächstes Audit: 2026-11-30 oder früher bei Paket-, Lock-, Workflow-
oder Release-Änderung. / The npm lock pins Playwright `1.62.1` and axe `4.13.0`;
CycloneDX is pinned to `6.2.0`. Known Critical/High findings and licence
conflicts are both zero.

## Quellen / Sources

- `https://www.nuget.org/packages/CycloneDX/6.2.0`
- `https://github.com/CycloneDX/cyclonedx-dotnet/releases/tag/v6.2.0`
- `https://github.com/CycloneDX/cyclonedx-dotnet`
- `https://github.com/advisories`

Diese interne Evidence ersetzt keine externe Zertifizierung oder
Rechtsberatung. / This internal evidence does not replace external
certification or legal advice.

## Nachtrag PR #118 (2026-11-21) / PR #118 addendum

Deutsch: `Terminal.Gui` 2.5.0 ist im IDE-Projekt und in beiden betroffenen
Lockdateien gebunden. Das Paket nennt MIT als Lizenz und den Git-Commit
`ece73d25c37df9bb17cb0c0d2171abd4d98eeaff` als Quelle. `dotnet package
list --vulnerable --include-transitive --no-restore` meldete fuer IDE und
Testprojekt keine bekannte verwundbare Abhaengigkeit. Der Locked Restore,
Release-Build und 377 Tests bestanden lokal; CI auf den Zielplattformen steht
noch aus. NIST SSDF und CWE Top 25 gelten; die Paketaufnahme wird im
Release-SBOM erfasst, und CI-Build-Evidence folgt dem SLSA-Ziel. VEX ist mangels
bekanntem Fund derzeit `N/A`. ASVS, AI-SBOM und Zero Trust sind `N/A`, weil
dieser lokale IDE-Paketwechsel weder Web-/Auth-Dienst noch KI-Laufzeit oder
verteilte Architektur einfuehrt. CAPEC erfordert ohne neue Trust Boundary kein
neues Angriffsszenario; SAMM bleibt die periodische Projektbewertung. Die
OpenSSF-Scorecard des oeffentlichen Lieferanten ist vor einem Release erneut
zu sichten.

*English: `Terminal.Gui` 2.5.0 is pinned in the IDE project and both affected
lockfiles. The package declares the MIT licence and source commit
`ece73d25c37df9bb17cb0c0d2171abd4d98eeaff`. The transitive NuGet
vulnerability query reported no known vulnerable dependency for the IDE or
test project. Locked restore, Release build, and 377 tests passed locally;
target-platform CI is pending. NIST SSDF and CWE Top 25 apply. The dependency
belongs in the release SBOM, and CI build evidence follows the SLSA target.
VEX is currently `N/A` because no finding is known. ASVS, AI-SBOM, and Zero
Trust are `N/A`: this local IDE update adds no web or authentication service,
AI runtime, or distributed architecture. No new trust boundary calls for a
CAPEC scenario; SAMM remains a periodic project assessment. Review the public
supplier's OpenSSF Scorecard again before a release.*

## Nachtrag zu Terminal.Gui 2.6.0 (2026-11-24) / Terminal.Gui 2.6.0 addendum

Die IDE verwendet unter .NET 11 `Terminal.Gui` 2.6.0 und
`Terminal.Gui.Editor` 2.5.7. Die IDE und das Testprojekt lösen denselben
Abhängigkeitsstand auf. Zu den indirekten Änderungen gehören
`Microsoft.Extensions` 11.0.0, `TextMateSharp` 2.0.5 und `Wcwidth` 5.0.2.
Das Terminal.Gui-Paket nennt die MIT-Lizenz und den Quellcommit
`5cc059119ba9d2c8d8d8db3bb2f7980d7cea6118`. Die Abfrage bei NuGet.org
meldete für IDE und Testprojekt keine bekannten verwundbaren Abhängigkeiten.

`TinyPl0.Core` und `TinyPl0.Vm` enthalten keine Terminal.Gui-Abhängigkeit.
Das IDE-Update erfordert deshalb keine neue Version dieser NuGet-Pakete.
NIST SSDF und CWE Top 25 gelten für diese Änderung. Die IDE-Abhängigkeit
gehört in den SBOM-Nachweis für eine IDE-Auslieferung. SLSA bleibt das Ziel
für CI-Build-Evidenz. VEX ist ohne bekannten Fund `N/A`. ASVS, AI-SBOM und
Zero Trust sind `N/A`, weil die Änderung eine lokale IDE-Abhängigkeit betrifft.

*On .NET 11 the IDE uses `Terminal.Gui` 2.6.0 and `Terminal.Gui.Editor`
2.5.7. The IDE and test project resolve the same Terminal.Gui dependency
graph. The transitive changes include `Microsoft.Extensions` 11.0.0,
`TextMateSharp` 2.0.5, and `Wcwidth` 5.0.2. The Terminal.Gui package declares
the MIT licence and source commit `5cc059119ba9d2c8d8d8db3bb2f7980d7cea6118`.
The NuGet.org query reported no known vulnerable dependencies in either project.*

*The Core and VM packages have no Terminal.Gui dependency. The IDE update
requires no new version of those packages. NIST SSDF and CWE Top 25 apply.
The dependency belongs in the SBOM for an IDE release. SLSA remains the
target for CI build evidence. VEX is `N/A` with no known finding. ASVS,
AI-SBOM, and Zero Trust are `N/A` for this local IDE dependency update.*
