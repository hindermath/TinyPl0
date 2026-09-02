<!-- intake-authoring:begin -->
# Lastenheft: Einbettbare PL/0-VM und öffentliche NuGet-Pakete

**Status:** ReadyForReview  
**Zielgruppe:** Auszubildende ab dem ersten Ausbildungsjahr, Lehrende und .NET-Hostanwendende  
**Vorausgesetztes Wissen:** Grundbegriffe von Compiler, virtueller Maschine und NuGet; Spec-Kit-Erfahrung wird nicht vorausgesetzt  
**Profil:** `level2-lastenheft`  
**Reihenfolge:** Rang 4 nach der abgeschlossenen Sandbox-Bewertung und vor `Lastenheft_Quellcode_Doku.md`

*Status: Ready for review. Audience: apprentices from the first training year,
teachers, and .NET host users. Basic compiler, virtual-machine, and NuGet terms
are assumed; no Spec Kit experience is required. The intake is ordered at
rank 4 after the completed Sandbox assessment and before source-code
documentation.*

## Zweck / Purpose

TinyPl0 soll seine bestehenden Compiler- und VM-Bausteine als sichere,
versionierte .NET-Hostbibliotheken bereitstellen. `TinyPl0.Core` und
`TinyPl0.Vm` werden direkt über NuGet.org veröffentlicht. Normale und
schrittweise Ausführung erhalten gemeinsame, kontrollierte Ressourcen-,
Abbruch- und Diagnosesemantik, damit TinyCalc und weitere Hosts keinen
Compiler- oder VM-Code kopieren müssen.

*TinyPl0 shall provide its compiler and VM as safe, versioned .NET host
libraries. `TinyPl0.Core` and `TinyPl0.Vm` are published directly on
NuGet.org. Normal and step execution share bounded resource, cancellation, and
diagnostic semantics so TinyCalc and other hosts do not copy compiler or VM
code.*

## Begriffe beim ersten Gebrauch / Terms At First Use

### Deutsch

- **Hostvertrag:** die dokumentierten öffentlichen Typen, Methoden, Ergebnisse
  und Fehlerregeln, auf die sich ein anderes .NET-Programm verlassen darf.
- **Run/Step-Parität:** Ein vollständiger Lauf und die wiederholte Ausführung
  einzelner Schritte liefern für dasselbe Programm dasselbe fachliche Ergebnis.
- **SemVer (Semantic Versioning):** ein Versionsschema aus Haupt-, Neben- und
  Patchnummer. Die Nummern zeigen inkompatible Änderungen, kompatible
  Erweiterungen und Fehlerkorrekturen an.
- **CancellationToken:** ein .NET-Signal für kooperatives Abbrechen. Die VM
  prüft das Signal an festgelegten Stellen und beendet sich kontrolliert.
- **SBOM (Software Bill of Materials):** eine maschinenlesbare Liste der
  Bestandteile und Abhängigkeiten eines Softwarepakets.
- **VEX (Vulnerability Exploitability eXchange):** eine dokumentierte Aussage,
  ob eine bekannte Schwachstelle das ausgelieferte Paket tatsächlich betrifft.
- **Provenance und SLSA:** Provenance verbindet Paket, Build, Quellcommit und
  Werkzeugkette. SLSA ist ein Stufenmodell zum Schutz dieser Lieferkette.
- **STRIDE und CAPEC:** STRIDE ordnet Bedrohungen in feste Kategorien ein.
  CAPEC beschreibt bekannte Angriffsmuster für die genauere Risikoanalyse.
- **OpenSSF Scorecard:** automatisierte Prüfungen bewerten öffentlich sichtbare
  Sicherheitspraktiken eines Open-Source-Repositories.
- **Trusted Publishing und OIDC:** GitHub Actions weist sich mit einem
  kurzlebigen, signierten Token gegenüber NuGet.org aus. NuGet.org tauscht ihn
  nach Prüfung einer engen Vertrauensrichtlinie gegen einen zeitlich begrenzten
  API-Key mit höchstens einer Stunde Gültigkeit; ein langlebiges
  Veröffentlichungs-Secret ist dafür nicht nötig.
- **Eingeschränkter API-Key:** ein Ersatzschlüssel mit genau festgelegter
  Operation, Paket-ID oder Namensmuster und kurzer Gültigkeitsdauer.
- **`--skip-duplicate`:** eine Push-Option, die einen HTTP-409-Konflikt nur als
  Warnung behandelt. Die Warnung beweist nicht, dass das vorhandene Paket zum
  aktuellen Release gehört.

### English

- **Host contract:** the documented public types, methods, results, and error
  rules on which another .NET application may rely.
- **Run/step parity:** a complete run and repeated single steps produce the
  same functional result for the same program.
- **SemVer (Semantic Versioning):** a version scheme with major, minor, and
  patch numbers. They indicate incompatible changes, compatible additions,
  and fixes.
- **CancellationToken:** a .NET signal for cooperative cancellation. The VM
  checks it at defined points and stops in a controlled way.
- **SBOM (Software Bill of Materials):** a machine-readable inventory of a
  software package's components and dependencies.
- **VEX (Vulnerability Exploitability eXchange):** a recorded statement about
  whether a known vulnerability actually affects the delivered package.
- **Provenance and SLSA:** provenance links a package to its build, source
  commit, and toolchain. SLSA is a maturity model for protecting that supply
  chain.
- **STRIDE and CAPEC:** STRIDE groups threats into defined categories. CAPEC
  describes known attack patterns for more detailed risk analysis.
- **OpenSSF Scorecard:** automated checks assess publicly visible security
  practices of an open-source repository.
- **Trusted publishing and OIDC:** GitHub Actions authenticates to NuGet.org
  with a short-lived signed token. NuGet.org checks a narrow trust policy and
  exchanges the token for a temporary API key valid for at most one hour, so
  no long-lived publishing secret is required.
- **Scoped API key:** a fallback key restricted to a defined operation,
  package ID or name pattern, and short validity period.
- **`--skip-duplicate`:** a push option that treats an HTTP 409 conflict as a
  warning. The warning alone does not prove that the existing package belongs
  to the current release.

## Aktueller Zustand / Current State

- `Pl0.Core` und `Pl0.Vm` sind öffentliche .NET-10-Projekte mit nutzbaren
  Compiler-, P-Code-, I/O- und VM-Typen.
- `VirtualMachine.Run()` besitzt kein verbindliches Instruktionsbudget und kann
  bei einer Endlosschleife unbegrenzt laufen.
- `SteppableVirtualMachine` ist öffentlich, aber ihre Ausführungs- und
  Diagnosesemantik ist nicht als stabiler externer Hostvertrag paketiert.
- Core und VM sind noch nicht als die geforderten Pakete auf NuGet.org
  verfügbar. Die Paket-IDs `TinyPl0.Core` und `TinyPl0.Vm` waren bei der
  Intake-Vorprüfung am 29.08.2026 nicht registriert; die Verfügbarkeit muss
  unmittelbar vor Veröffentlichung erneut geprüft werden.
- Release Please führt die Repository-SemVer unabhängig von der vierteiligen
  IDE-Dateiversion.

*Core and VM already expose useful .NET APIs, but normal execution has no
binding instruction budget, step semantics are not a stable packaged host
contract, and the required NuGet packages do not yet exist. Package-ID
availability must be checked again immediately before publication.*

## Zielzustand / Target State

- Beide VM-Ausführungsarten verwenden einen gemeinsamen, getesteten
  Sicherheits- und Zustandsvertrag.
- Hosts können Ausführung durch Instruktionsbudget und CancellationToken
  kontrollieren und erhalten strukturierte Abschlussinformationen.
- Compiler und VM sind als getrennte, zueinander passende NuGet-Pakete mit
  derselben stabilen SemVer verfügbar.
- Paketinhalt, Abhängigkeiten, Quellstand, SBOM, VEX und Provenance/SLSA sind
  nachvollziehbar; TinyCalc kann ausschließlich über den öffentlichen Feed
  reproduzierbar wiederherstellen.

*Both execution modes share a tested safety contract, hosts control execution
with an instruction budget and cancellation, and matching stable packages are
available with traceable supply-chain evidence.*

## Umfang / Scope

- Öffentlicher Hostvertrag für begrenzte normale und schrittweise VM-Ausführung.
- Gemeinsame Zustands-, Abschluss-, Diagnose- und Zählersemantik.
- Rückwärtskompatible Weiterentwicklung der vorhandenen Compiler- und VM-APIs.
- Pack-Konfiguration, Paketmetadaten, Symbole und XML-Dokumentation für
  `TinyPl0.Core` und `TinyPl0.Vm`.
- Direkte Veröffentlichung auf NuGet.org über einen abgesicherten,
  nachvollziehbaren Releasepfad.
- Unit-, Paritäts-, Grenzwert-, Abbruch-, Paket-, Consumer- und
  Cross-Platform-Tests.
- Security-, Supply-Chain-, DocFX-, A11Y- und Statistiknachweise.

*Scope covers the bounded host API, shared run/step semantics, compatible
packaging, direct NuGet.org delivery, consumer tests, and complete security,
documentation, accessibility, and supply-chain evidence.*

## Nicht-Ziele / Non-Goals

- Keine neue PL/0-Syntax, kein TinyCalc-spezifischer Dialekt und keine
  Tabellenzellen-Semantik in TinyPl0.
- Keine Gleitkomma-, Dezimal- oder Festkommaerweiterung; `integer` bleibt der
  einzige Datentyp.
- Kein JIT-, CLR-, nativer oder anderer P-Code-fremder Backendpfad.
- Keine Compileroptimierung und keine Änderung historischer PL/0-Semantik.
- Keine Paketabhängigkeit auf Terminal.Gui, TinyCalc oder die TinyPl0-IDE.
- Keine automatische Remote-Veröffentlichung ohne aktuelle Provider- und
  Secret-Berechtigung.

*The work adds no language extension, spreadsheet semantics, non-integer type,
alternate backend, compiler optimization, UI dependency, or unauthorized
remote publication.*

## Funktionale Anforderungen / Functional Requirements

- **FR-001:** `VirtualMachine` und `SteppableVirtualMachine` müssen denselben
  ausführbaren P-Code, dieselben Stackregeln und dieselben Laufzeitfehler
  semantisch gleich behandeln.
- **FR-002:** Jede Ausführung muss ein validiertes positives Instruktionsbudget
  mit sicherem endlichem Standard besitzen. Ein Überschreiten beendet den Lauf
  deterministisch mit strukturiertem Status und Diagnose.
- **FR-003:** Normale und schrittweise Ausführung müssen kooperative
  Cancellation über einen .NET-`CancellationToken` unterstützen und ohne
  weitere Instruktion in einen stabilen Endzustand wechseln.
- **FR-004:** Das Ergebnis muss mindestens Erfolg, Abschlussgrund,
  ausgeführte Instruktionszahl, Diagnosen und einen sicheren Zustands- oder
  Stack-Snapshot enthalten.
- **FR-005:** Ein Step muss exakt eine Instruktion ausführen. Nach Halt,
  Cancellation, Budgetüberschreitung oder Fehler darf kein weiterer Step
  unbeabsichtigt Programmzustand verändern.
- **FR-006:** Stackgröße, Programmlänge, Instruktionsargumente und I/O-Fehler
  müssen vor oder an ihrer Vertrauensgrenze validiert werden; Fehler dürfen
  keine internen Stacktraces ausgeben.
- **FR-007:** Die VM darf ausschließlich über das bereitgestellte `IPl0Io`
  kommunizieren und keinen Datei-, Netzwerk-, Prozess- oder Umgebungszugriff
  hinzufügen.
- **FR-008:** Compilerdiagnosen dürfen weiterhin gesammelt statt während der
  Kompilierung geworfen werden. Öffentliche Änderungen müssen vollständig
  deutsch/englisch dokumentiert sein.
- **FR-009:** Die Implementierung muss gemeinsame Ausführungslogik verwenden
  oder durch Paritätstests nachweisen, dass Run und Step nicht semantisch
  auseinanderlaufen.
- **FR-010:** Die Paket-IDs lauten `TinyPl0.Core` und `TinyPl0.Vm`.
  `TinyPl0.Vm` hängt ausschließlich in passender Version von
  `TinyPl0.Core` ab; Core erhält keine neue Laufzeitabhängigkeit.
- **FR-011:** Beide Pakete verwenden dieselbe, von Release Please abgeleitete
  SemVer. Die vierteilige IDE-Dateiversion darf die NuGet-Paketversion nicht
  bestimmen.
- **FR-012:** Pakete müssen Repository-URL, Lizenz, Beschreibung, Tags,
  README, XML-Dokumentation und Symbol-/Quellzuordnung enthalten und vor
  Veröffentlichung mit einem unabhängigen Consumer-Projekt geprüft werden.
- **FR-013:** Die Veröffentlichung muss auf NuGet.org erfolgen. Ein lokaler
  Feed darf nur zur Vorprüfung dienen und gilt nicht als Lieferabschluss.
- **FR-014:** Der Releasepfad muss die Paket-ID-Verfügbarkeit erneut prüfen,
  beide Paketdateien atomar derselben Version zuordnen und einen teilweise
  veröffentlichten Zustand sichtbar als Fehler behandeln.
- **FR-015:** Veröffentlichung benötigt aktuelle, ausdrücklich gewährte
  Provider-/Secret-Berechtigung. Ein lokaler autonomer Lauf darf nur packen,
  prüfen und Release-Evidenz vorbereiten.
- **FR-016:** Der plattformübergreifende Standardpfad muss die installierte
  .NET-SDK-Toolchain mit `dotnet pack` und `dotnet nuget push` gegen die
  HTTPS-V3-Quelle `https://api.nuget.org/v3/index.json` verwenden. Ein Einsatz
  von `nuget.exe` oder einer separaten Veröffentlichungs-App ist nicht nötig.
- **FR-017:** Für GitHub Actions muss NuGet Trusted Publishing über OIDC der
  bevorzugte Credential-Pfad sein, sobald die Funktion für das zuständige
  NuGet.org-Konto verfügbar ist. Die Policy muss GitHub-Owner, Repository,
  Workflow-Datei und bei Nutzung das Release-Environment binden. Der Job darf
  nur die nötigen Rechte `contents: read` und `id-token: write` erhalten; die
  `NuGet/login`-Action muss nach Repository-Supply-Chain-Regeln auf eine
  geprüfte, unveränderliche Revision gebunden sein. Als `user` gilt der NuGet.org-
  Profilname, nicht die E-Mail-Adresse.
- **FR-018:** Ist Trusted Publishing nachweislich nicht verfügbar oder für den
  autorisierten Veröffentlichungsschritt ungeeignet, darf ein aktueller
  NuGet.org-API-Key als dokumentierter Fallback dienen. Er muss auf Push und
  `TinyPl0.Core`/`TinyPl0.Vm` oder ein gleich enges Namensmuster beschränkt,
  kurz gültig und von CI-Schlüsseln getrennt sein. Bei NuGet 7.6 oder neuer
  darf `NUGET_API_KEY` ohne Schlüsselargument verwendet werden; die konkrete
  Toolversion muss diese Fähigkeit vorab nachweisen. Eine interaktive
  PowerShell-Eingabe muss maskiert erfolgen, und die Prozessvariable muss nach
  dem Push auch im Fehlerfall entfernt werden. Benutzername/Kennwort,
  Repositorydateien, Logs und Kommandozeilenargumente sind keine zulässigen
  Secret-Speicher.
- **FR-019:** Eine veröffentlichte Kombination aus Paket-ID und Version ist
  unveränderlich und darf nicht überschrieben oder wiederverwendet werden.
  Eine Korrektur muss die fehlerhafte Version gegebenenfalls auslisten, eine
  neue SemVer vergeben und ein neues vollständiges Release durchlaufen.
- **FR-020:** `--skip-duplicate` darf einen Releasefehler nicht pauschal in
  Erfolg umdeuten. Jeder 409-Konflikt muss in einen fail-closed
  Abgleichspfad führen, der die vorhandene ID/Version einer bereits belegten,
  beabsichtigten Veröffentlichung zuordnet; fehlt diese Evidenz, scheitert der
  Release. Beide TinyPl0-Pakete müssen weiterhin gemeinsam nachgewiesen werden.

*Requirements bind shared bounded run/step behavior, cancellation, structured
results, isolated I/O, compatible public APIs, matching `TinyPl0.Core` and
`TinyPl0.Vm` SemVer packages, independent consumer tests, OIDC-first
publishing with a narrowly scoped fallback, immutable versions, fail-closed
duplicate handling, and an explicitly authorized direct NuGet.org release.*

## Qualität und Governance / Quality And Governance

- C#/.NET 10 bleibt die speichersichere Hauptlaufzeit. Öffentliche APIs folgen
  Microsoft Secure Coding Guidelines und besitzen vollständige XML-Kommentare.
- NIST SSDF und CWE Top 25 gelten immer. STRIDE und relevante CAPEC-Muster
  prüfen manipulierten Quelltext, P-Code, I/O, Ressourcenerschöpfung und
  Paketlieferung.
- Defense in Depth besteht mindestens aus Compiler-/P-Code-Grenzen,
  Instruktionsbudget, Stacklimit, Cancellation und isoliertem I/O. Standards
  sind fail-closed und verwenden Least Privilege.
- OWASP ASVS ist `N/A`, weil die Bibliotheken keinen Web-, HTTP-, API- oder
  Authentifizierungsdienst bereitstellen. Zero Trust ist für die lokale
  Bibliotheksausführung `N/A`.
- SBOM ist für beide Pakete verpflichtend. VEX bewertet bekannte
  Schwachstellen; SLSA-Provenance ist mindestens auf dem praktisch erreichbaren
  Niveau nachzuweisen. OpenSSF Scorecard ergänzt die Releaseprüfung.
- OIDC-Policy, Workflow-Identität und jede externe Action-Revision gehören zur
  Supply-Chain-Prüfung. Ein kurzlebiger Credential ersetzt weder aktuelle
  Providerberechtigung noch Paket-, Hash- und Consumer-Evidenz.
- AI-SBOM ist `N/A`, weil keine KI-Runtime, Modelle oder Datensätze ausgeliefert
  werden. OWASP SAMM wird für das langlebige Projekt als Reifegradfolge geprüft.
- NIS2, CRA, EU AI Act und DORA werden mit `Applicable`, `N/A` oder `Open` und
  kurzer Begründung dokumentiert; die Paketveröffentlichung darf nicht
  stillschweigend als regulatorisch folgenlos gelten.
- DocFX-Änderungen erfordern Playwright/axe- und lynx-orientierte
  Textprüfungen. Lernende Dokumentation bleibt deutsch zuerst, englisch danach,
  CEFR B2 und WCAG 2.2 AA-orientiert.

*Quality applies memory-safe .NET, NIST SSDF, CWE Top 25, STRIDE/CAPEC,
defense in depth, SBOM/VEX/SLSA, OpenSSF Scorecard, regulatory applicability,
DocFX accessibility proof, and bilingual CEFR-B2 documentation. ASVS, Zero
Trust, and AI-SBOM are not applicable for the stated product scope.*

## Abhängigkeiten und Risiken / Dependencies And Risks

- Interner harter Vorgänger: die abgeschlossene Sandbox-Bewertung. Der
  bisherige VM-/CLI-Vorgänger ist durch die ausdrücklich geänderte
  Serienreihenfolge aufgehoben.
- Nachfolger in TinyPl0: `Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md`.
- Externer blockierter Verbraucher: TinyCalc
  `Lastenheft_PL0-Zellfunktionen_V1.md`.
- Risiken sind API-Brüche, Run/Step-Abweichung, Ressourcenerschöpfung,
  Cancellation-Rennen, Paketnamensverlust, teilweise Veröffentlichung,
  eine falsch gebundene OIDC-Policy, langlebige oder offengelegte API-Keys,
  durch `--skip-duplicate` verdeckte Konflikte, kompromittierte Lieferkette und
  fehlende Providerberechtigung.
- Ein NuGet-Release ist erst abgeschlossen, wenn beide Pakete derselben Version
  abrufbar und durch Consumer-, Hash- und Supply-Chain-Evidenz belegt sind.

*The completed Sandbox assessment is the internal predecessor; the former
VM/CLI predecessor is superseded by the explicitly changed series order.
TinyCalc remains a blocked external consumer. Risks include API drift,
run/step divergence, resource exhaustion, cancellation races, package-name
loss, partial publication, supply-chain compromise, and missing provider
authority.*

## Erwartete Artefakte und Evidenz / Expected Artifacts And Evidence

- Öffentliche Hostoptionen, Abschlussgrund und Ergebnis-/Step-Verträge.
- Gemeinsame oder nachweislich paritätische VM-Ausführungslogik.
- NuGet-Packkonfiguration für `TinyPl0.Core` und `TinyPl0.Vm`.
- Paket- und Consumer-Tests einschließlich Budget, Cancellation, Fehler und
  Cross-Platform-Wiederherstellung.
- NuGet.org-Release-URLs, Version, Tag, Commit, Paket-Hashes und Lockfile-Beleg.
- Dokumentierte Credential-Entscheidung: bevorzugte OIDC-Policy mit gebundenem
  Owner, Repository, Workflow und optionalem Environment oder begründeter,
  eng eingeschränkter API-Key-Fallback samt Toolversions- und Secret-
  Löschungsnachweis.
- Pro Paket ein nachvollziehbarer Push-Ausgang; bei 409 zusätzlich die
  fail-closed Zuordnung zur bereits belegten beabsichtigten Veröffentlichung.
- SBOM, VEX, Provenance/SLSA und aktualisierte Dependency-Audit-Evidenz unter
  `docs/security/`.
- Aktualisierte Architektur-, VM-, API-, DocFX- und Lernendokumentation mit
  A11Y-Nachweis.
- Aktualisierte Traceability-Matrix und Projektstatistik.

*Evidence includes public host contracts, package and consumer tests, NuGet
release identifiers, source and package provenance, security evidence,
accessible documentation, traceability, and updated statistics.*

## Abnahmekriterien / Acceptance Criteria

- **AC-001:** Endlosschleifen enden bei normaler und schrittweiser Ausführung
  reproduzierbar am Budget mit demselben Abschlussgrund.
- **AC-002:** Cancellation vor und während der Ausführung beendet beide Modi
  ohne zusätzliche Instruktion und ohne inkonsistenten VM-Zustand.
- **AC-003:** Run/Step-Paritätstests decken erfolgreiche Programme,
  Division durch null, Stackfehler, I/O-Fehler, Halt, Budget und Cancellation ab.
- **AC-004:** Bestehende Compiler-, CLI-, IDE-, Golden- und VM-Tests bleiben
  erfolgreich oder dokumentieren absichtlich geänderte Hostsemantik.
- **AC-005:** Ein unabhängiges .NET-10-Projekt kann beide gepackten Artefakte
  aus einem sauberen Feed wiederherstellen, PL/0 kompilieren, normal ausführen
  und schrittweise debuggen.
- **AC-006:** `TinyPl0.Core` und `TinyPl0.Vm` sind in derselben stabilen Version
  direkt über NuGet.org abrufbar; ein lokaler Feed allein erfüllt dies nicht.
- **AC-007:** Release-Tag, Quellcommit, Paket-Hashes, SBOM, VEX und
  Provenance/SLSA sind konsistent verknüpft und textorientiert prüfbar.
- **AC-008:** DocFX-, Playwright/axe-, lynx-, Dependency-, Security- und
  Cross-Platform-Prüfungen bestehen nach den Repository-Regeln.
- **AC-009:** Das Paket enthält keine TinyCalc-, Terminal.Gui- oder IDE-Abhängigkeit
  und führt keinen direkten Betriebssystemzugriff ein.
- **AC-010:** TinyCalc erhält einen eindeutigen Handoff mit Paketversion,
  API-Vertrag und sämtlicher Gate-Evidenz.
- **AC-011:** Der CI-Release verwendet bei Verfügbarkeit eine eng gebundene
  NuGet.org-Trusted-Publishing-Policy und keine langlebigen
  Veröffentlichungs-Secrets. Ein API-Key-Fallback erfüllt die Abnahme nur mit
  dokumentierter Nichtverfügbarkeit, minimalem Scope, kurzer Gültigkeit und
  nachgewiesener Entfernung aus dem Prozess.
- **AC-012:** Ein Wiederholungsversuch derselben Paket-ID und Version kann
  weder vorhandenen Inhalt überschreiben noch allein durch
  `--skip-duplicate` als erfolgreicher Release gelten.
- **AC-013:** Der Release-Nachweis ordnet beide Paket-Pushes, Credential-Modus,
  Toolversion, GitHub-Workflow/Revision, Quellcommit, SemVer und öffentlichen
  Consumer-Restore eindeutig demselben Release zu.

*Acceptance proves bounded and cancellable run/step parity, regression safety,
clean consumer restore, matching immutable NuGet.org packages, OIDC-first
credential evidence, fail-closed duplicate handling, complete supply-chain
evidence, accessible documentation, isolation, and an exact TinyCalc handoff.*

## Annahmen und Entscheidungen / Assumptions And Decisions

- **IAD001 – beantwortet:** Zwei getrennte Intakes und ihre Reihenfolge wurden
  mit Vorschlag `tinycalc-pl0-v1-split-v1` und SHA-256
  `f36a20d34be1c682821321dd0b1a0c8d2a5c44b6ffbfaf54c77daa027868a10d`
  ausdrücklich genehmigt.
- **IAD002 – beantwortet:** Das zweistufige TinyCalc-Gate und das Verbot einer
  lokalen `ProjectReference` als Fallback wurden ausdrücklich genehmigt.
- **IAD004 – beantwortet:** Der aktuelle Nutzerauftrag zieht dieses Intake auf
  Rang 4 als bevorzugten nächsten Serienkandidaten vor und ersetzt damit nur
  die frühere Rang-/Vorgängerentscheidung aus IAD001. Scope, Paketgates,
  Nicht-Ziele und Abnahmekriterien bleiben unverändert.
- **IAD005 – beantwortet:** Die vom Nutzer bereitgestellten und gegen die
  offiziellen NuGet-/PowerShell-Hinweise geprüften Veröffentlichungsregeln
  werden als OIDC-Vorzugsweg, eng begrenzter API-Key-Fallback,
  Versions-Unveränderlichkeit und fail-closed Duplicate-Gate verbindlich
  aufgenommen. Dadurch entsteht keine aktuelle Provider- oder
  Veröffentlichungsberechtigung.
- Die Paket-IDs `TinyPl0.Core` und `TinyPl0.Vm` sind die vorgesehenen stabilen
  IDs; ihre Live-Verfügbarkeit wird vor dem Provider-Schritt erneut geprüft.
- Delivery Authority bleibt `LocalImplementation`. Dieses Intake erteilt keine
  aktuelle NuGet-, Commit-, Push-, PR-, Merge-, Secret- oder Bypass-Berechtigung.
- Es bestehen keine offenen fachlichen Intake-Fragen.

*The approved decisions bind the split, updated rank-4 order, gate, package
identities, OIDC-first publishing policy, constrained fallback, and local-only
delivery authority. IAD004 supersedes only the earlier rank/predecessor part
of IAD001; IAD005 grants no provider or publishing authority. No material
intake question remains open.*

<!-- intake-authoring:prompts -->
## Ausführbare Spec-Kit-Prompts / Copy-Ready Spec Kit Prompts

<!-- spec-kit-command-id: speckit.specify -->
### Specify

```text
$speckit-specify Nutze requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md als verbindliches Intake. Erstelle oder aktualisiere ausschließlich die passende Feature-Spezifikation. Bewahre Scope, Nicht-Ziele, Reihenfolge, Run/Step-Parität, Ressourcen- und Abbruchgrenzen, öffentliche NuGet-Paketverträge, Security-, Supply-Chain-, A11Y-, Dokumentations- und Evidenzanforderungen. Implementiere nichts; committe und pushe nicht; veröffentliche keine Pakete; erstelle oder merge keinen Pull Request und starte kein weiteres Feature.
```

<!-- spec-kit-command-id: speckit.autonomous -->
### Autonomous

```text
$speckit-autonomous Führe genau einen vollständigen autonomen Spec-Kit-Lauf mit requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md als verbindlichem Intake aus. Delivery Mode: LocalImplementation. Implementiere und validiere lokal die sichere Host-API, Run/Step-Parität, Pack-Artefakte und Release-Evidenz, aber stoppe vor jeder Remote- oder NuGet-Veröffentlichung, solange keine aktuelle ausdrückliche Provider- und Secret-Berechtigung vorliegt. Bewahre Scope, Reihenfolge, Security-, Supply-Chain-, A11Y-, Dokumentations- und Evidenzgrenzen. Nicht pushen, keinen Pull Request erstellen oder mergen, keine Pakete veröffentlichen, keinen Bypass nutzen, keine Secrets offenlegen und kein Folgefeature starten.
```

<!-- intake-authoring:end -->
