# S-ADR-0001: VM-Ressourcenbudget und Vorvalidierung

**Status**: Accepted
**Date**: 2026-08-30
**References**: Constitution XII/XIII, ISO A.8.27/A.8.28, NIST SSDF,
CWE-400, CWE-770, STRIDE Denial of Service

## Security context

Unvertrauenswürdiges P-Code kann nicht terminieren. Extreme Stackwerte können
vor sicherer Diagnose zu Überlauf oder großer Allokation führen.

## Security decision

Defense in Depth besteht aus zwei unabhängigen Schichten: (1) gemeinsame
Vorvalidierung `InstructionBudget > 0` und `3 <= StackSize <= 1_000_000` vor
Addition/Allokation; (2) Laufzeitprüfung vor Auswahl von Instruktion `N+1`.
Fehler sind terminal, lokalisiert und geben keine internen Details aus.

## Trade-offs and evidence

Der Stack bleibt auf etwa vier MiB je VM begrenzt; das kann absichtlich extreme
Lernprogramme ablehnen. Der Default von einer Million erhält normale Beispiele.
TDD prüft positive, Grenz-, negative und Missbrauchsfälle sowie Batch/Step.
Alternativen Timeout, Exception und breite Refaktorierung wurden wegen
Nichtdeterminismus, Informationspreisgabe oder Scope abgelehnt.

Residual risk: Das Budget ersetzt keine Sandbox. CAPEC-100-artige
Ressourcenerschöpfung wird begrenzt, nicht vollständig ausgeschlossen.
