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

- [ ] Dependency-/Vulnerability-Scan und SBOM-Evidence folgen in T062–T073.
- [ ] CVD und RFC-9116 werden in T034/T035 grün validiert.
- [ ] DocFX, axe und lynx werden gemeinsam mit der öffentlichen API geprüft.
- [x] Golden-Dateien werden nicht regeneriert.
- [x] Nach Fehlern bleiben VM und lokaler Server in einem kontrollierten Zustand.

English: The same checklist requires boundary validation, controlled errors,
integer and resource limits, dependency/source review, pinned workflows,
coordinated disclosure, accessible documentation, and safe recovery. Items that
depend on later executable evidence stay unchecked until those gates pass.
