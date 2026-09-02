# Forschungsentscheidungen / Research Decisions

**Status**: Aufgelöst vor Implementierung / Resolved before implementation
**Datum / Date**: 2026-09-02
**Bindung / Binding**: [spec.md](spec.md), [plan.md](plan.md) und aktiver Lauf
`a01cd5bd-fa86-49f1-b074-cb59a9c24862` / spec, plan, and active run

## R1 — Gemeinsamer Ausführungskern / Shared execution kernel

**Entscheidung / Decision**: Eine interne `VmExecutionSession` besitzt Zustand,
Validierung, Zähler, Terminalcache und den einzigen Opcode-/OPR-Dispatch.
`Run` wiederholt `ExecuteNext`; `Step` ruft dieselbe Methode einmal auf. / One
internal session owns state, validation, counters, terminal caching, and the
only opcode/OPR dispatcher. `Run` loops over `ExecuteNext`; `Step` calls it once.

**Begründung / Rationale**: Beobachtbare Parität entsteht durch eine
Implementierung und wird zusätzlich getestet. Zwei Decoder mit nur
vergleichenden Tests wurden verworfen. / Observable parity follows from one
implementation and is additionally tested. Two decoders guarded only by
comparison tests were rejected.

## R2 — Validierung, Grenzen und Cancellation / Validation, bounds, and cancellation

**Entscheidung / Decision**: Optionen werden in der Reihenfolge Stack, Budget,
Programmlänge und Sprache geprüft; danach wird das gesamte Programm geprüft.
An jeder Laufgrenze gilt Terminalcache, Cancellation, Budget, Fetch/Zählen und
Dispatch. Der Token wird bei `Run` oder `Initialize` übergeben; `Step` bleibt
parameterlos. / Options are checked in stack, budget, program-length, and
language order, followed by whole-program validation. Every execution boundary
uses terminal cache, cancellation, budget, fetch/count, and dispatch order. The
token is supplied to `Run` or `Initialize`; `Step` remains parameterless.

**Grenzen / Bounds**: Stack `3..1_000_000` (Standard/default `500`), Budget
`1..10_000_000` (Standard/default `1_000_000`), Programmlänge
`1..100_000` (Standard/default `10_000`) und lexikalische Ebene `0..3`.

**Begründung / Rationale**: Vorabfehler verursachen weder Allokation noch
Dispatch. Ein begonnener Dispatch zählt genau einmal; Cancellation während
Host-I/O wirkt an der nächsten Grenze. Exceptions als normale Cancellation-
Steuerung und Zeitlimits als falsche Sandbox-Zusage wurden verworfen. / Early
failure causes neither allocation nor dispatch. A started dispatch counts once;
cancellation during host I/O takes effect at the next boundary. Exception-based
normal cancellation and time limits presented as a sandbox guarantee were
rejected.

## R3 — Ergebnis- und Kompatibilitätsvertrag / Result and compatibility contract

**Entscheidung / Decision**: `VmCompletionReason` benennt `Running` und alle
terminalen Gründe stabil. Run und Step liefern Reason, kumulierten Zähler,
defensive Zustandsprojektion und sichere Diagnosen. Bestehende Signaturen,
Konstruktoren und Deconstructs bleiben als kompatible Projektionen erhalten. /
`VmCompletionReason` stably names `Running` and every terminal reason. Run and
Step expose reason, cumulative count, defensive state, and safe diagnostics.
Existing signatures, constructors, and deconstructors remain as compatible
projections.

**Begründung / Rationale**: Hosts können ohne Textanalyse reagieren. Fremde
Exceptiontexte, veränderliche interne Arrays und ein zweiter Resultvertrag
wurden verworfen. / Hosts can react without parsing text. Foreign exception
messages, mutable internal arrays, and a second result contract were rejected.

## R4 — Paketversion und Abhängigkeit / Package version and dependency

**Entscheidung / Decision**: `eng/TinyPl0.PackageVersion.props` ist die einzige
Paket-SemVer-Quelle und ein Release-Please-`extra-files`-Ziel. Core und VM
verwenden dieselbe Version; VM referenziert Core exakt als
`[$(TinyPl0PackageVersion)]`. Die vierteilige IDE-Version importiert die
Property nicht. / The props file is the sole package SemVer source and a
Release Please `extra-files` target. Core and VM use the same version; VM uses
the exact Core range. The four-part IDE version does not import the property.

**Begründung / Rationale**: Das untrennbare Paketpaar bleibt reproduzierbar,
während IDE-Buildzählung und öffentliche SemVer getrennt bleiben. Unabhängige
Paketversionen und IDE-Wiederverwendung wurden verworfen. Die enge Range ist
eine bewusste Ausnahme von der allgemeinen NuGet-Empfehlung für lose
Bibliotheken. / The inseparable package pair remains reproducible while IDE
build counting and public SemVer stay separate. Independent versions and IDE
reuse were rejected. The exact range is an intentional exception to general
guidance for loosely coupled libraries.

Referenz / Reference: [NuGet version ranges](https://learn.microsoft.com/nuget/concepts/package-versioning#version-ranges).

## R5 — OIDC-First und unveränderliche Actions / OIDC-first and immutable actions

**Entscheidung / Decision**: NuGet Trusted Publishing ist der einzige unter
dieser Autorität ausführbare Pfad. Die Policy bindet `hindermath/TinyPl0`,
`release-please.yml`, Environment `nuget-release` und die beiden Paket-IDs.
Jede Drittanbieter-Action ist mit dem vollständigen Commit-SHA aus
[plan.md](plan.md) gepinnt. / NuGet Trusted Publishing is the only executable
path under this authority. The policy binds repository, workflow, environment,
and both package IDs. Every third-party action is pinned to the full commit SHA
listed in the plan.

**Begründung / Rationale**: OIDC liefert kurzlebige Zugangsdaten und vermeidet
ein langlebiges Publishing-Secret. Fehlt oder driftet die Policy, blockiert der
Release. Ein API-Key-Fallback ist ohne neue ausdrückliche Secret-Autorität nicht
zulässig. / OIDC supplies short-lived credentials and avoids a long-lived
publishing secret. A missing or drifting policy blocks release. API-key fallback
is not permitted without fresh explicit secret authority.

Referenzen / References: [NuGet Trusted Publishing](https://learn.microsoft.com/nuget/nuget-org/trusted-publishing),
[GitHub action hardening](https://docs.github.com/actions/security-for-github-actions/security-guides/security-hardening-for-github-actions#using-third-party-actions).

## R6 — Fail-closed Veröffentlichung / Fail-closed publication

**Entscheidung / Decision**: Ein credential-freier Verifier klassifiziert vor
und nach Push `None`, `BothMatching`, `Partial` oder `Conflict`. Nur `None` darf
einen neuen Push beginnen; `BothMatching` ist idempotenter Erfolg. `Partial`,
`Conflict`, unbekannter Status und ein nicht nachgewiesener 409 blockieren.
`--skip-duplicate` wird nicht verwendet. / A credential-free verifier classifies
the pair before and after push as `None`, `BothMatching`, `Partial`, or
`Conflict`. Only `None` may start a new push; `BothMatching` is idempotent
success. Partial, conflict, unknown state, and unreconciled 409 block.

**Begründung / Rationale**: NuGet-Versionen sind unveränderlich, aber zwei
Paket-Pushes sind nicht atomar. Korrektur nach Teilrelease verlangt deshalb
eine neue SemVer und einen vollständigen neuen Release. / NuGet versions are
immutable, while two package pushes are not atomic. Recovery from a partial
release therefore requires a new SemVer and a complete new release.

## R7 — Supply Chain und Nachweisniveau / Supply chain and assurance level

**Entscheidung / Decision**: Beide Pakete erhalten Hashmanifest, CycloneDX-
SBOM, VEX, Dependency-Audit und GitHub Artifact Attestation. Der Anspruch ist
SLSA Build Level 2, soweit die GitHub-hosted Provenance-Evidence dies belegt;
höhere Stufen werden nicht behauptet. / Both packages receive a hash manifest,
CycloneDX SBOM, VEX, dependency audit, and GitHub Artifact Attestation. The
claim is SLSA Build Level 2 where supported by GitHub-hosted provenance; no
higher level is claimed.

Referenz / Reference: [GitHub artifact attestations](https://docs.github.com/actions/security-for-github-actions/using-artifact-attestations/using-artifact-attestations-to-establish-provenance-for-builds).

## R8 — Plattform, Dokumentation und Barrierefreiheit / Platform, docs, and accessibility

**Entscheidung / Decision**: Pack, Tests und sauberer Consumer laufen auf
`macos-15`, `ubuntu-24.04` und `windows-2025`. Geänderte APIs erzeugen DocFX;
repräsentative Seiten werden gemeinsam mit Playwright/axe, `lynx` und manueller
Textprüfung geprüft. Deutsch steht zuerst, Englisch danach, CEFR B2 und WCAG
2.2 AA bilden die Basis. / Pack, tests, and the clean consumer run on all three
named runners. Changed APIs regenerate DocFX; representative pages are checked
with Playwright/axe, lynx, and manual text review. German precedes English,
CEFR B2 and WCAG 2.2 AA form the baseline.

**Verworfen / Rejected**: Nur ein Betriebssystem, nur visueller Browsertest
oder nur automatisch erzeugte API-Seiten. / One operating system, visual-only
browser testing, or generated API pages without the text-oriented path.
