# arc42 Abschnitt 8: Sicherheits-Querschnittskonzepte / Security Concepts

## Feature 006: Host und Veröffentlichung / Host and Publication

Deutsch: Die Session erlaubt höchstens Stack 1.000.000, Budget 10.000.000 und
Programmlimit 100.000; ungültige Werte werden vor Allokation verworfen. Die VM
greift weder auf Dateien, Netzwerk, Prozesse noch Umgebung zu und kommuniziert
nur über IPl0Io. Wiederholte terminale Steps sind read-only. Der Releasepfad
trennt Release-PR, Build und Attestierung, OIDC-Publish und öffentliche
Verifikation in Jobs mit kleinsten Rechten.

*English: The session caps stack, budget, and program length at 1,000,000,
10,000,000, and 100,000; invalid values fail before allocation. The VM has no
file, network, process, or environment access and communicates only through
IPl0Io. Repeated terminal steps are read-only. Release PR, build and
attestation, OIDC publish, and public verification are separate
least-privilege jobs.*


**Feature**: `004-secure-development-hardening`
**Stand / Date**: 2026-08-30
**Methodik / Method**: arc42, iSAQB CPSA-F, NIST SSDF, CWE Top 25
**Entscheidung / Decision**: `Applicable`

## Eingabevalidierung / Input Validation

PL/0, P-Code, CLI-Argumente, Pfade, IDE-Dateien und HTTP-Anfragen sind
unvertrauenswürdig. Lexer/Parser/Serializer validieren Format und historische
Grenzen. Die VM validiert `InstructionBudget > 0` und
`3 <= StackSize <= 1_000_000` vor Addition, Allokation oder Ausführung.
HTTP- und Dateigrenzen bleiben separat; ein grüner VM-Test belegt sie nicht.

## Ressourcen und Defense in Depth / Resources and Defense in Depth

Schicht 1 prüft Optionen fail-safe vor Zustandserzeugung. Schicht 2 prüft
Pointer, Stack und das positive Instruktionsbudget im Ausführungsloop. Bei `N`
erfolgreichen Instruktionen entsteht vor Auswahl von `N+1` eine terminale
Diagnose. Batch und Step verwenden dieselbe Bedeutung. Das Budget ersetzt keine
OS-Sandbox und keine bestehende Bounds-Prüfung.

## Fehler und Informationspreisgabe / Errors and Disclosure

Nutzerflächen erhalten stabile lokalisierte Codes mit Ursache und nächster
Aktion, aber keine Stack-Traces, internen Pfade, Connection Strings oder rohe
Exceptions. Ungültige Konfiguration fällt in einen erfolglosen Batch-Resultat-
beziehungsweise terminalen Step-Error-State zurück. Weitere Steps duplizieren
keine Diagnose.

## Least Privilege und sichere Defaults / Least Privilege and Safe Defaults

- Lokaler Dokumentationsserver bindet nur Loopback und liest nur `_site`.
- Workflows behalten minimale `contents/pages/id-token/attestations`-Rechte je
  tatsächlichem Job; neue Rechte werden nicht pauschal erteilt.
- Keine Secrets werden gelesen, geloggt oder in Evidence kopiert.
- Unbekannte Finding-ID, fehlende Evidence oder ungültige Option verweigert
  Fortschritt standardmäßig.

## Logging, Auth, Krypto und Datenschutz / Logging, Auth, Crypto, Privacy

TinyPl0 besitzt keine Produktkonten, Rollen, Sessions, Telemetrie oder
Produktkryptografie. Authentifizierung, Autorisierung, Key Management und DPIA
sind deshalb `N/A` mit Scope-Trigger, nicht „erfüllt“. Diagnosen und CI-Logs
enthalten keine Secrets. Ein späterer Remote-/Account-/Krypto-Scope verlangt
eine neue Architekturentscheidung.

## Dateien, HTTP und Deployment / Files, HTTP, and Deployment

CLI/IDE begrenzen Pfade durch ihre vorhandenen APIs und kontrollierte Fehler.
Der statische HTTP-Pfad wird gegen Loopback, Methoden, Root/Traversal, Header
und Fehlerantworten als ASVS 5.0.0 L1 geprüft. Deployment bleibt lokales .NET-
Programm plus GitHub Pages/Release-Lieferfläche; keine Cloud-Produktlaufzeit
wird behauptet.

## Abhängigkeiten und Supply Chain / Dependencies and Supply Chain

NuGet/npm/.NET-Tools stammen aus verifizierten Registries, neue Tools sind
versions- und lockfilegebunden. Critical/High-CVEs blockieren. SBOM und
Artefakthash binden den konkreten Kandidaten; VEX entsteht nur bei Fund und SLSA
wird nicht höher als die reale Evidence behauptet. `release-please.yml` bleibt
in diesem Feature read-only.

## Recovery und Risiken / Recovery and Risks

Fehlerzustände sind terminal und reproduzierbar; ein neuer Lauf beginnt mit
neuer Initialisierung. Golden-Artefakte bleiben unverändert. Offene HTTP-,
Provider-, Rechts- oder Sandboxpunkte besitzen Owner/Trigger und eröffnen
keine siebte Änderungserlaubnis.
