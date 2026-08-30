# Sicherheits-Checkliste / Security Checklist: TinyPl0

**Feature**: `004-secure-development-hardening`
**Sprache / Language**: C# 14 / .NET 10; PowerShell 7; Bash; YAML/JSON
**Reviewer**: independent security reviewer
**Stand / Date**: 2026-08-30

## Allgemein / General

- [x] NIST SSDF und CWE Top 25 sind als feste Reviewbasis benannt.
- [x] Trust Boundaries für Source, P-Code, VM, CLI, Datei, IDE, HTTP und Supply
  Chain stehen im Threat Model.
- [x] VM-Konfiguration wird vor Addition, Allokation und Ausführung validiert.
- [x] Fehler geben keine Stack-Traces, Secrets oder internen Pfade aus.
- [x] Fail-safe Defaults, Least Privilege und Defense in Depth sind dokumentiert.
- [x] Keine dynamische Codeausführung oder unsichere Deserialisierung entsteht.
- [x] Neue Dependencies benötigen Quelle, Lizenz, Wartung und CVE-Prüfung.
- [x] Security-/A11Y-/Supply-Chain-Gates bleiben voneinander unabhängig.

## C#/.NET

- [x] C# bleibt MSL; dies ersetzt keine I/O-, Ressourcen- oder API-Prüfung.
- [x] Ganzzahlgrenzen schützen `StackSize + 1` vor Überlauf.
- [x] Das Budget ist positiv und zählt Instruktionen statt Zeit.
- [x] Batch/Step liefern kontrollierte Resultate statt Konfigurations-Exceptions.
- [x] Öffentliche API erhält vollständige anwendbare XML-Dokumentation.
- [x] SQL, Razor, Auth, CORS, Anti-Forgery und Produktkrypto sind in diesem
  Compiler/VM-Scope `N/A`; Trigger ist die Einführung einer solchen Fläche.

## PowerShell, Bash und Workflows

- [x] PowerShell verwendet StrictMode, validierte Parameter und kein
  `Invoke-Expression`.
- [x] Bash verwendet `set -euo pipefail`, quotierte Variablen und `--` wo
  anwendbar.
- [x] Workflows verwenden minimale Permissions und volle Action-SHAs.
- [x] Credential-/Agent-State-Prüfung verwendet nur synthetische Sentinelpfade.

## Supply Chain, CVD, A11Y und Recovery

- [x] Vorbereitete Dependency-/Vulnerability-Inventare melden keine bekannten
  Critical-/High-Pakete; SBOM 1.7 und Tool-Pin sind lokal validiert.
- [x] CVD und RFC-9116 sind durch `.github/SECURITY.md`, veröffentlichten
  `.well-known/security.txt`-Pfad, Ablaufdatum und lokalen Grün-Validator belegt.
- [x] DocFX, axe und lynx wurden gemeinsam mit der öffentlichen API geprüft.
- [x] Golden-Dateien werden nicht regeneriert.
- [x] Nach Fehlern bleiben VM und lokaler Server in einem kontrollierten Zustand.

English: The same checklist requires boundary validation, controlled errors,
integer and resource limits, dependency/source review, pinned workflows,
coordinated disclosure, accessible documentation, and safe recovery. The local
DocFX, axe, and text-browser gate is complete; remote delivery evidence remains
separate.

## CVD-Abgleich / CVD Reconciliation

Deutsch: `FND-CVD-001` war autorisiert und wurde im kleinsten Dateisatz
umgesetzt. Die Policy nennt Kontakt, Scope, erwartete Reaktion und sicheren
Meldeweg; RFC 9116 wird durch `docfx/.well-known/security.txt` und `docfx.json`
in den öffentlichen DocFX-Pfad aufgenommen. Der gemeinsame T075–T082-Zyklus
hat die lokale HTML-/Textbrowser-Sicht bestanden. Ohne reale Veröffentlichung
wird keine Provider-Verfügbarkeit behauptet.

English: `FND-CVD-001` was authorised and implemented in its smallest file set.
The policy records contact, scope, response expectations, and a safe reporting
path; `docfx/.well-known/security.txt` is included in public DocFX output. The
shared T075–T082 cycle passed the local HTML and text-browser evidence. No
provider availability is claimed without publication.
