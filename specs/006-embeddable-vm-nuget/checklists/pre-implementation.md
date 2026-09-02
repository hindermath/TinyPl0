# Pre-Implementation Checklist: Einbettbare VM und NuGet-Pakete / Embeddable VM and NuGet Packages

**Zweck / Purpose**: Mindestvollständige Prüfung der Anforderungsqualität und der geplanten Nachweise vor dem ersten Implementierungsschritt / Minimum complete review of requirements quality and planned evidence before the first implementation step
**Erstellt / Created**: 2026-09-02
**Feature / Feature**: [spec.md](../spec.md)
**Zielgruppe und Zeitpunkt / Audience and timing**: Feature-, Architektur-, Security-, A11Y- und Release-Review vor der Implementierung / Feature, architecture, security, accessibility, and release review before implementation

**Hinweis / Note**: Diese Liste prüft, ob Anforderungen und Abnahmekriterien vollständig, eindeutig, widerspruchsfrei und messbar beschrieben sind. Sie prüft noch keine Implementierung. / This list checks whether requirements and acceptance criteria are complete, clear, consistent, and measurable. It does not test an implementation.

## Anforderungsqualität und Abgrenzung / Requirements Quality and Scope

- [x] CHK001 Sind Umfang, Nicht-Ziele, Vorgänger, Nachfolger und TinyCalc-Handoff ohne widersprüchliche Erweiterung festgelegt? / Are scope, non-goals, predecessor, successor, and TinyCalc handoff defined without conflicting expansion? [Completeness, Consistency, Spec §Bindung; §Umfang; §Nicht-Ziele]
- [x] CHK002 Sind alle öffentlichen Vertragsbegriffe, Ressourcenlimits, Abschlussgründe und Erfolgssignale stabil benannt und objektiv messbar? / Are all public contract terms, resource limits, completion reasons, and success signals stably named and objectively measurable? [Clarity, Measurability, Spec §Öffentlicher Hostvertrag; §Schlüsselentitäten; SC-001–SC-003]
- [x] CHK003 Decken die Anforderungen Primär-, Alternativ-, Fehler-, Recovery- und nichtfunktionale Szenarien ab, ohne eine Betriebssystem-Sandbox oder neue PL/0-Semantik zu versprechen? / Do the requirements cover primary, alternate, error, recovery, and non-functional scenarios without promising an operating-system sandbox or new PL/0 semantics? [Coverage, Consistency, Spec §Nutzungsszenarien; §Grenz- und Fehlerfälle; §Nicht-Ziele]

## Run-/Step-Parität und Grenzen / Run-Step Parity and Boundaries

- [x] CHK004 Ist die geforderte Run-/Step-Paritätsmatrix für Erfolg, Halt, Budget, Cancellation, Division durch null, Stack-, P-Code- und I/O-Fehler vollständig benannt? / Is the required run-step parity matrix complete for success, halt, budget, cancellation, division by zero, stack, P-Code, and I/O failures? [Completeness, Coverage, Spec FR-001; FR-009; SC-001]
- [x] CHK005 Ist für beide Modi dieselbe Reihenfolge an jeder Instruktionsgrenze eindeutig festgelegt: Validierung, bereits terminaler Zustand, Cancellation, Budget, Dispatch? / Is the same precedence at every instruction boundary unambiguously defined for both modes: validation, existing terminal state, cancellation, budget, dispatch? [Clarity, Consistency, Spec §Grenz- und Fehlerfälle; FR-002–FR-005]
- [x] CHK006 Ist messbar definiert, wann eine Instruktion zählt, wann `N+1` ohne Seiteneffekt unterbleibt und wie vorab angeforderte Cancellation gezählt wird? / Is it measurably defined when an instruction counts, when `N+1` is prevented without side effects, and how pre-requested cancellation is counted? [Measurability, Edge Case, Spec §Öffentlicher Hostvertrag; SC-002; SC-003]
- [x] CHK007 Sind Idempotenz nach allen terminalen Gründen sowie die Grenze für Cancellation während eines bereits begonnenen Host-I/O-Aufrufs vollständig beschrieben? / Are post-terminal idempotence for every terminal reason and the cancellation boundary during an already-started host I/O call fully described? [Completeness, Recovery, Spec §Grenz- und Fehlerfälle; FR-003; FR-005]
- [x] CHK008 Verlangen Plan und Tasks konkrete positive Grenzwerte und Nachweise für Budget, Stack, Programmlänge, Sprungziele, Opcodes, Instruktionsargumente und Instruktionszeiger? / Do the plan and tasks require concrete positive limits and evidence for budget, stack, program length, jump targets, opcodes, instruction arguments, and instruction pointer? [Gap, Measurability, Spec FR-002; FR-006; RESOURCE-GATE-001]

## Öffentliche API und XML-Dokumentation / Public API and XML Documentation

- [x] CHK009 Sind alle betroffenen öffentlichen Compiler-, P-Code-, VM-, I/O-, Options-, Result-, Diagnostic- und Snapshot-Flächen sowie die Quellkompatibilitätsgrenze erfasst? / Are all affected public compiler, P-Code, VM, I/O, options, result, diagnostic, and snapshot surfaces plus the source-compatibility boundary identified? [Completeness, Spec §Öffentlicher Hostvertrag; FR-004; FR-008]
- [x] CHK010 Sind Unveränderlichkeit, sichere Datenfreigabe und die gemeinsamen Felder von Run- und Step-Ergebnissen eindeutig spezifiziert? / Are immutability, safe data exposure, and the shared fields of run and step results specified unambiguously? [Clarity, Consistency, Spec FR-004; FR-005; §Schlüsselentitäten]
- [x] CHK011 Fordern die Anforderungen für jede geänderte öffentliche API vollständige XML-Dokumentation mit Zweck, Parametern, Rückgabe, zugesicherten Ausnahmen und geeigneten Beispielen, deutsch zuerst und englisch danach? / Do the requirements demand complete XML documentation for every changed public API, covering purpose, parameters, return value, guaranteed exceptions, and suitable examples in German first and English second? [Completeness, Spec Nutzungsszenario 5; CR-005; CR-006; SC-010]

## Pakete und unabhängiger Verbrauch / Packages and Independent Consumption

- [x] CHK012 Sind Paket-IDs, gemeinsame stabile SemVer-Quelle, passende VM-zu-Core-Abhängigkeit und die Trennung von der vierteiligen IDE-Version widerspruchsfrei festgelegt? / Are package IDs, shared stable SemVer source, matching VM-to-Core dependency, and separation from the four-part IDE version defined consistently? [Consistency, Spec FR-010; FR-011; SC-006]
- [x] CHK013 Ist der erwartete Inhalt beider Pakete vollständig und prüfbar beschrieben, einschließlich Metadaten, README, XML, Symbole, Quellzuordnung und erlaubter Abhängigkeiten? / Is the expected content of both packages completely and verifiably described, including metadata, README, XML, symbols, source mapping, and permitted dependencies? [Completeness, Acceptance Criteria, Spec FR-010; FR-012; SC-006]
- [x] CHK014 Ist der saubere Consumer-Nachweis eindeutig auf .NET 10, leeren Cache, öffentlichen NuGet.org-Feed, identische Paketversionen, keine lokale `ProjectReference` und keinen privaten Feed begrenzt? / Is clean consumer evidence clearly constrained to .NET 10, an empty cache, the public NuGet.org feed, matching package versions, no local `ProjectReference`, and no private feed? [Clarity, Coverage, Spec Nutzungsszenario 3; FR-013; FR-021; SC-005]
- [x] CHK015 Verlangen Plan und Tasks reproduzierbare Pack-, Inhalts-, Restore-, Compile-, Run- und Step-Nachweise mit benannten Artefakt- und Logpfaden? / Do the plan and tasks require reproducible pack, content, restore, compile, run, and step evidence with named artifact and log paths? [Gap, Traceability, Spec FR-012; FR-021; PACKAGE-GATE-001; CONSUMER-GATE-001]

## Sicherheit und Bedrohungsmodell / Security and Threat Model

- [x] CHK016 Sind alle Trust Boundaries und Datenklassen für Quelltext, P-Code, Hostoptionen, Cancellation, `IPl0Io`, CI/OIDC, NuGet.org und Consumer vollständig dokumentiert? / Are all trust boundaries and data classes for source, P-Code, host options, cancellation, `IPl0Io`, CI/OIDC, NuGet.org, and consumers fully documented? [Completeness, Spec §Architekturanwendbarkeit; CR-002; CR-003]
- [x] CHK017 Deckt das geforderte STRIDE/CIA- und CAPEC-Modell manipulierte Eingaben, Ressourcenerschöpfung, I/O-Missbrauch, Package Substitution, Workflow-/OIDC-Fehlbindung und Teilveröffentlichung ab? / Does the required STRIDE/CIA and CAPEC model cover manipulated inputs, resource exhaustion, I/O abuse, package substitution, workflow/OIDC misbinding, and partial publication? [Coverage, Spec CR-009; §Sicherheitsstandards]
- [x] CHK018 Sind Defense-in-Depth, Least Privilege, Fail-Safe Defaults und die Verbote zusätzlichen Datei-, Netzwerk-, Prozess- und Umgebungszugriffs als prüfbare Anforderungen formuliert? / Are defense in depth, least privilege, fail-safe defaults, and the bans on added file, network, process, and environment access stated as verifiable requirements? [Acceptance Criteria, Spec FR-006; FR-007; §Architekturanwendbarkeit]
- [x] CHK019 Sind sichere Diagnosen und Fehlerpfade so definiert, dass keine Stacktraces, Hostinterna, Credentials oder restricted Identitätsdaten offengelegt werden? / Are safe diagnostics and failure paths defined so that no stack traces, host internals, credentials, or restricted identity data are exposed? [Security, Edge Case, Spec FR-006; SC-008]

## Lieferkette, OIDC und Veröffentlichung / Supply Chain, OIDC, and Publishing

- [x] CHK020 Sind SBOM, VEX, Provenance/SLSA, OpenSSF Scorecard, Lockfile, Paket-/Symbolhashes und öffentlicher Consumer-Restore eindeutig demselben Commit, Tag und Release zugeordnet? / Are SBOM, VEX, provenance/SLSA, OpenSSF Scorecard, lockfile, package/symbol hashes, and public consumer restore unambiguously bound to the same commit, tag, and release? [Traceability, Consistency, Spec FR-022; CR-008; SC-007]
- [x] CHK021 Ist der OIDC-Vertrag vollständig auf Owner, Repository, Workflow, optionales Environment, `contents: read`, `id-token: write`, unveränderliche `NuGet/login`-Revision und kurzlebiges Credential begrenzt? / Is the OIDC contract fully constrained to owner, repository, workflow, optional environment, `contents: read`, `id-token: write`, an immutable `NuGet/login` revision, and a short-lived credential? [Completeness, Security, Spec FR-017]
- [x] CHK022 Ist eindeutig festgelegt, dass eine fehlende oder nicht belegbare OIDC-Route blockiert und der API-Key-Fallback ohne neue ausdrückliche Secret-Autorität nicht ausgeführt werden darf? / Is it explicit that an unavailable or unprovable OIDC route blocks and that the API-key fallback cannot run without fresh explicit secret authority? [Clarity, Fail-Safe, Spec FR-015; FR-018; SC-008]
- [x] CHK023 Sind Paket-Unveränderlichkeit, neue SemVer für Korrekturen, fail-closed 409-Abgleich, unmittelbare ID-Prüfung und sichtbarer Teilrelease-Fehler vollständig beschrieben? / Are package immutability, a new SemVer for fixes, fail-closed 409 reconciliation, immediate ID recheck, and visible partial-release failure fully described? [Coverage, Recovery, Spec FR-014; FR-019; FR-020; SC-011]

## Plattformen und barrierefreie Dokumentation / Platforms and Accessible Documentation

- [x] CHK024 Fordern die Anforderungen gleichwertige Pack-, Test- und saubere Consumer-Nachweise auf macOS, Linux und Windows mit klarer Zuordnung je Plattform? / Do the requirements demand equivalent pack, test, and clean-consumer evidence on macOS, Linux, and Windows with clear per-platform attribution? [Coverage, Traceability, Spec FR-016; FR-021; SC-005]
- [x] CHK025 Ist der Re-Evaluation-Trigger vollständig, falls Plan oder Tasks ein neues oder geändertes Script-Tool einführen und dadurch Bash-/PowerShell-7-Parität, Hilfe, Manpage und Dry-Run-Vertrag nötig werden? / Is the re-evaluation trigger complete if the plan or tasks introduce a new or changed script tool and thereby require Bash/PowerShell 7 parity, help, man page, and dry-run contracts? [Assumption, Coverage, Spec §Plattformanwendbarkeit]
- [x] CHK026 Sind alle betroffenen Dokumentationsflächen, Zielgruppen, Leserpfade und die zweisprachige CEFR-B2-Reihenfolge ohne nur visuell vermittelte Bedeutung benannt? / Are all affected documentation surfaces, audiences, reader paths, and the bilingual CEFR-B2 order named without visual-only meaning? [Completeness, Accessibility, Spec Nutzungsszenario 5; CR-004–CR-006; §Dokumentationswirkung]
- [x] CHK027 Sind die DocFX-Abnahmekriterien für Seitensprache, Bypass-Blöcke, Tastaturfokus, Landmarken, Nicht-Text-Kontrast und lesbare Struktur objektiv prüfbar? / Are the DocFX acceptance criteria for page language, bypass blocks, keyboard focus, landmarks, non-text contrast, and readable structure objectively verifiable? [Measurability, WCAG 2.2 AA, Spec §Barrierefreiheit; SC-009]
- [x] CHK028 Verlangen Plan und Tasks für repräsentative DocFX-Seiten gemeinsam DocFX-, Playwright/axe-, `lynx`- und manuelle Text-Evidence ohne offene schwerwiegende oder kritische Funde? / Do the plan and tasks jointly require DocFX, Playwright/axe, `lynx`, and manual text evidence for representative pages with no open serious or critical findings? [Gap, Acceptance Criteria, Spec CR-006; SC-009; DOC-A11Y-GATE-001]

## Rückverfolgbarkeit, Statistik und Lieferung / Traceability, Statistics, and Delivery

- [x] CHK029 Ist eine durchgängige Zuordnung von FR-, CR- und SC-IDs über Plan, Tasks, Tests, Dokumentation und die zehn stabilen Akzeptanzgates gefordert? / Is end-to-end mapping of FR, CR, and SC IDs across plan, tasks, tests, documentation, and the ten stable acceptance gates required? [Traceability, Spec FR-023; §Anwendbarkeit des autonomen Laufs]
- [x] CHK030 Ist die Statistikaktualisierung nach abgeschlossener Implementierung mit unveränderter Methodik, dokumentierten Linienarten, Arbeitsfenster und beiden manuellen Vergleichsbaselines vorgesehen? / Is the post-implementation statistics update required with unchanged methodology, documented line classes, work window, and both manual comparison baselines? [Completeness, Spec CR-013; FR-023; SC-010]
- [x] CHK031 Verlangen Plan und Tasks vor Delivery frische Exact-Head-CI-, Review-, Authority-, Provider-, Paket- und Gate-Evidence für exakt denselben Commit? / Do the plan and tasks require fresh exact-head CI, review, authority, provider, package, and gate evidence for exactly the same commit before delivery? [Gap, Consistency, Spec FR-025; REMOTE-REVIEW-GATE-001]
- [x] CHK032 Ist Admin-Bypass ausschließlich als letzte Repository-Policy-Ausnahme nach vollständigen Technik-, Risiko-, Evidence-, Exact-Head- und Review-Gates beschrieben und ausdrücklich kein Ersatz für Review oder Approval? / Is admin bypass described solely as a final repository-policy exception after complete technical, risk, evidence, exact-head, and review gates, and explicitly not as a substitute for review or approval? [Clarity, Authority, Spec §Bindung; FR-025]
- [x] CHK033 Sind Merge, Default-Branch-Synchronisierung, öffentlicher Handoff und kausaler Closeout erst nach vollständigen Gates zulässig und als getrennte, belegbare Zustände definiert? / Are merge, default-branch synchronization, public handoff, and causal closeout allowed only after complete gates and defined as separate, evidenced states? [Completeness, Sequencing, Spec FR-024–FR-026; MERGE-CLOSEOUT-GATE-001]
- [x] CHK034 Sind das Verbot von Secret-Beschaffung oder -Offenlegung, der unveränderte aktive Intake und das Verbot eines Folgefeatures für alle Plan-, Task- und Delivery-Phasen ausdrücklich erhalten? / Are the bans on secret acquisition or disclosure, changing the active intake, and starting a follow-up feature explicitly preserved across all plan, task, and delivery phases? [Consistency, Scope, Spec §Bindung; FR-015; FR-018; FR-026; SC-008]

## Planungsdisposition / Planning Disposition

`ResolvedInPlan` bedeutet nur, dass der Plan eine konkrete Entscheidung und
einen späteren Beweispfad festlegt. Es bedeutet nicht `Implemented` oder
`GatePassed`. / `ResolvedInPlan` means that the plan records a concrete decision
and later evidence path. It does not mean implemented or gate passed.

| ID | Disposition | Auflösung / Resolution |
|---|---|---|
| CHK001 | `ResolvedInPlan` | Plan §Summary/Phases bindet Scope, Vorgänger, Handoff und kein Folgefeature. / Scope, predecessor, handoff, and no follow-up are bound. |
| CHK002 | `ResolvedInPlan` | Plan §Public Contract und Hostvertrag benennen API, Reasons, Defaults und Bounds. / API, reasons, defaults, and bounds are named. |
| CHK003 | `ResolvedInPlan` | Plan §Constitution/Tests trennt VM-Grenzen ausdrücklich von einer OS-Sandbox. / VM bounds are separated from an OS sandbox. |
| CHK004 | `ResolvedInPlan` | Plan §Tests enthält die vollständige Paritätsmatrix. / The complete parity matrix is defined. |
| CHK005 | `ResolvedInPlan` | Hostvertrag §3 bindet die exakte Präzedenz. / Exact precedence is bound. |
| CHK006 | `ResolvedInPlan` | Hostvertrag §4 bindet Zählpunkt, Cancellation und `N+1`. / Count point, cancellation, and N+1 are bound. |
| CHK007 | `ResolvedInPlan` | Hostvertrag §6 und Datenmodell binden Terminalcache und I/O-Grenze. / Terminal cache and I/O boundary are bound. |
| CHK008 | `ResolvedInPlan` | Plan §Technical Context/Tests und `RESOURCE-GATE-001` nennen positive und negative Grenzfälle. / Positive and negative boundaries are named. |
| CHK009 | `ResolvedInPlan` | Plan §Public Contract/Structure erfasst betroffene API-Flächen und Kompatibilitätsprojektionen. / Affected APIs and compatibility projections are identified. |
| CHK010 | `ResolvedInPlan` | Datenmodell §Result projections bindet defensive, gemeinsame Felder. / Defensive shared fields are bound. |
| CHK011 | `ResolvedInPlan` | Hostvertrag §8 und Constitution Check binden vollständige bilinguale XML-Elemente. / Complete bilingual XML elements are bound. |
| CHK012 | `ResolvedInPlan` | Plan §Package Design trennt Release-Please-SemVer und IDE-Version. / Package and IDE versions are separated. |
| CHK013 | `ResolvedInPlan` | Releasevertrag §2 und `PACKAGE-GATE-001` definieren prüfbares Inventar. / A verifiable package inventory is defined. |
| CHK014 | `ResolvedInPlan` | Quickstart §4 und `CONSUMER-GATE-001` binden leeren Cache, NuGet.org und drei OS. / Empty cache, NuGet.org, and three OS are bound. |
| CHK015 | `ResolvedInPlan` | `PACKAGE-GATE-001` und `CONSUMER-GATE-001` nennen Befehle und Evidence-Pfade. / Commands and evidence paths are named. |
| CHK016 | `ResolvedInPlan` | Constitution Check und Datenmodell benennen alle Trust Boundaries und Datenklassen. / Trust boundaries and data classes are named. |
| CHK017 | `ResolvedInPlan` | `SECURITY-GATE-001` bindet STRIDE/CIA, CAPEC, Ressourcen-, OIDC- und Teilrelease-Risiken. / Required threat classes are bound. |
| CHK018 | `ResolvedInPlan` | Plan §Constitution und Hostvertrag §7 binden Defense in Depth, Least Privilege und I/O-Verbote. / Security principles and I/O bans are bound. |
| CHK019 | `ResolvedInPlan` | Hostvertrag §6–7 verbietet Stacktraces, Fremdtexte, Interna und Credentials. / Unsafe diagnostic content is prohibited. |
| CHK020 | `ResolvedInPlan` | Releasevertrag §1/§6 bindet alle Supply-Chain-Artefakte an Commit, Tag und Version. / Supply-chain artifacts bind to commit, tag, and version. |
| CHK021 | `ResolvedInPlan` | Releasevertrag §3–4 bindet vollständige SHAs, Least Privilege und OIDC-Claims. / Full SHAs, least privilege, and OIDC claims are bound. |
| CHK022 | `ResolvedInPlan` | Plan/Releasevertrag blockiert bei fehlender OIDC-Evidence; API-Key braucht neue Autorität. / Missing OIDC blocks; API key needs new authority. |
| CHK023 | `ResolvedInPlan` | Releasevertrag §5 definiert `None/BothMatching/Partial/Conflict`, 409 und neue SemVer. / Publication state, 409, and recovery are defined. |
| CHK024 | `ResolvedInPlan` | `CONSUMER-GATE-001` nennt `macos-15`, `ubuntu-24.04`, `windows-2025`. / All runners are named. |
| CHK025 | `ResolvedInPlan` | Constitution Check setzt Scriptparität N/A mit Trigger bei neuem/geändertem Repo-Skript. / Script parity is N/A with a re-evaluation trigger. |
| CHK026 | `ResolvedInPlan` | Plan §Documentation impact definiert Flächen, Leserpfad, DE/EN B2 und text-first. / Surfaces, reader path, language order, and text-first delivery are defined. |
| CHK027 | `ResolvedInPlan` | `DOC-A11Y-GATE-001` bindet WCAG-2.2-AA-Kriterien und objektive Tools. / WCAG criteria and tools are bound. |
| CHK028 | `ResolvedInPlan` | Quickstart §6 und das A11Y-Gate verlangen DocFX, axe, lynx und manuelle Evidence. / All four proof paths are required. |
| CHK029 | `ResolvedInPlan` | Plan §Traceability ordnet FR/CR/SC den zehn stabilen Gates zu. / Requirements map to all stable gates. |
| CHK030 | `ResolvedInPlan` | Constitution Check/Serialized Writers und Closeout-Gate binden Statistikmethodik und Zeitpunkt. / Statistics method and timing are bound. |
| CHK031 | `ResolvedInPlan` | `REMOTE-REVIEW-GATE-001` bindet Delivery Set, Exact Head, CI, Review, Authority und Evidence. / Exact-head proof is bound. |
| CHK032 | `ResolvedInPlan` | Remote-Review-Rationale untersagt Admin-Bypass als Ersatz für Technik oder Approval. / Admin bypass cannot substitute for proof. |
| CHK033 | `ResolvedInPlan` | Releasevertrag §8 und `MERGE-CLOSEOUT-GATE-001` trennen Publish, Merge, Sync, Handoff und Closeout. / Delivery states are separated. |
| CHK034 | `ResolvedInPlan` | Plan §Summary/Phases und Specify-/Closeout-Gates erhalten No-Secret, Intake und No-Follow-up. / Boundaries remain explicit. |

## Hinweise / Notes

- Ein Häkchen bedeutet, dass die Anforderungstexte und geplanten Nachweise diese Frage vollständig beantworten; es ist kein Implementierungsnachweis. / A check mark means the requirements and planned evidence fully answer the question; it is not implementation evidence.
- Offene Punkte müssen vor dem ersten Implementierungsschritt in Spec, Plan, Tasks oder dem Gatevertrag geklärt und rückverfolgbar verlinkt werden. / Open items must be resolved in the spec, plan, tasks, or gate contract and linked traceably before the first implementation step.
