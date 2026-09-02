# Bedrohungsmodell / Threat Model: TinyPl0

## Ergänzung Feature 006 / Feature 006 Addendum

Deutsch: Neue Trust Boundaries sind P-Code/Optionen/Cancellation zur
gemeinsamen Session, IPl0Io zum Host sowie GitHub-OIDC über NuGet.org zum
öffentlichen Consumer. STRIDE-Risiken sind manipulierte Opcodes und Ziele
(Tampering), Ressourcenerschöpfung (Denial of Service), rohe Hostfehler
(Information Disclosure), Paketersetzung (CAPEC-438) und falsch gebundene
Publisher-Identität (Spoofing/Elevation). Defense in Depth besteht aus
Vorvalidierung plus Laufzeit-Bounds sowie aus Paarzustand, Hashvergleich,
OIDC-Environment und Provider-Attestierung.

*English: New trust boundaries cover P-Code, options, and cancellation into
the shared session, IPl0Io into the host, and GitHub OIDC through NuGet.org to
the public consumer. Risks include tampered instructions, resource exhaustion,
raw host-error disclosure, package substitution (CAPEC-438), and publisher
identity misbinding. Pre-validation plus runtime bounds and paired state plus
hashes, OIDC environment, and attestation provide defence in depth.*


**Feature**: `004-secure-development-hardening`
**Stand / Date**: 2026-08-30
**Entscheidung / Decision**: `Applicable`
**Owner / Reviewer**: TinyPl0 maintainer / independent security reviewer
**Methoden / Methods**: STRIDE, CIA, CAPEC, NIST SSDF, CWE Top 25, ISO A.8.27

Deutsch: Dieses Modell deckt PL/0-Quelltext, textuelles P-Code, VM, CLI,
Datei-I/O, Terminal-IDE, den lokalen Dokumentationsserver und die Lieferkette
ab. Es ist interne Audit-Evidence, keine Zertifizierung. English: This model
covers source, textual P-Code, VM, CLI, file I/O, IDE, loopback documentation
server, and supply chain. It is internal evidence, not certification.

## Assets und CIA / Assets and CIA

| Asset | C | I | A | Schutz / Protection |
|---|:---:|:---:|:---:|---|
| Compiler-/P-Code-Semantik | L | H | M | Golden-, Katalog- und Traceability-Tests |
| VM-Zustand und Ausgabe | L | H | H | Pointer-/Stackprüfung plus Instruktionsbudget |
| Nutzerdateien und Pfade | M | H | M | Validierung, kontrollierte Fehler, kleinste Rechte |
| IDE-/CLI-Diagnosen | L | H | M | Lokalisierte Codes, keine Stack-Traces |
| `_site` und Loopback-HTTP | L | H | M | statische Wurzel, Loopback-only, ASVS-L1-Prüfung |
| Build-/Release-Evidence | L | H | H | Pins, Hashes, SBOM und Review |

## Datenflüsse und Trust Boundaries / Data Flows and Trust Boundaries

```text
Untrusted .pl0 --TB1--> Lexer/Parser --> Instructions --TB2--> VM --> output
Untrusted .pcode --------TB3----------> Serializer/VM
CLI args + paths --------TB4----------> CLI/file I/O
Keyboard/files ----------TB5----------> Terminal IDE
Browser -----------------TB6----------> 127.0.0.1 DocFX root
Registries/Actions ------TB7----------> restore/build/Pages artefacts
```

Jede Grenze validiert Eingaben an ihrem Eintritt. Keine Komponente erhält neue
Netz-, Secret- oder Adminrechte. / Every boundary validates input at entry. No
component gains new network, secret, or administrative privilege.

## STRIDE-Analyse / STRIDE Analysis

| ID | Boundary | STRIDE | Missbrauch / Abuse | Risiko | Mitigation / Status |
|---|---|---|---|---|---|
| TM-01 | TB1/TB3 | T,D | beschädigte oder große Eingabe stört Parser/VM | Medium | vorhandene Limits, Diagnosen, Negativtests |
| TM-02 | TB2 | D | Endlosschleife verbraucht unbegrenzt CPU | High | Budgetgrenze plus Pointer-/Stackprüfung implementiert und durch unverändertes Rot→Grün belegt |
| TM-03 | TB2 | T,D | Stack `int.MaxValue` überläuft vor Allokation | High | Vorvalidierung `3..1_000_000` vor Addition/Allokation implementiert; CAPEC-100 |
| TM-04 | TB4/TB5 | I | Exception legt interne Pfade/Stack offen | Medium | kontrollierte Diagnostics, keine Stack-Traces |
| TM-05 | TB6 | T,I | Traversal, externe Bindung oder Headermissbrauch | High | ASVS-L1 read-only Gate; Produktedit nur neuer Intake |
| TM-06 | TB7 | T,R | ungepinnte oder unbelegte Artefakte | High | volle Pins, Dependency Review, Hash/SBOM, CAPEC-438 |
| TM-07 | Repo root | I,T | versehentliches Tracking sensibler neuer Dateien | High | deny-by-default `.gitignore`, synthetische Sentinels |

Spoofing und Elevation of Privilege sind für das lokale Produkt ohne Konten
oder Rollen `N/A`; sie werden bei Remote-/Auth-Scope neu bewertet. Repudiation
gilt für Build- und Review-Evidence, nicht für Benutzerkonten.

## Positive, Grenze, Negativ, Missbrauch / Test Classes

- Positiv: haltendes Programm innerhalb Budget und gültigem Stack.
- Grenze: genau `N` Instruktionen, Stack `3` und `1_000_000`.
- Negativ: Budget `0/-1`, Stack negativ, `0/1/2`, `1_000_001`, `int.MaxValue`.
- Missbrauch: Endlosschleife, beschädigtes P-Code, Traversal/unerwartete HTTP-
  Methoden, manipuliertes Dependency-/Workflow-Artefakt.

## Restrisiken / Residual Risks

Das Budget ist deterministisch, aber keine Zeit-, Speicher- oder OS-Sandbox-
Garantie. HTTP-Härtung bleibt ohne roten separaten Befund no-edit. Provider-
und Rechtsaussagen bleiben Owner-Entscheidungen. Jeder neue Critical/High-Fund
blockiert. Re-evaluation: neue Runtime, Trust Boundary, Dependency, Remote-
Bindung, Authentifizierung oder Releaseform.

Der read-only HTTP-Abgleich bestätigt eine feste `_site`-Wurzel, ASP.NET-
Static-File-Pfadnormalisierung, nur Loopback über `localhost:5000` und keine
Upload-, Auth-, Session- oder Zustandsänderungsfläche. `FND-HTTP-001` bleibt
als mittlere Follow-up-Prüfung für explizite Response-Header und reale
Deployment-Pakete offen; es wurde keine siebte Produktänderung autorisiert.

The read-only HTTP review confirms a fixed `_site` root, ASP.NET static-file
path normalization, loopback-only `localhost:5000`, and no upload, auth,
session, or state-changing surface. `FND-HTTP-001` remains a medium follow-up
for explicit response headers and real deployment packages. No seventh product
change was authorised.
