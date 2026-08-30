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

## Verifikation / Verification

Deutsch: Der Secure-Code-Review gegen NIST SSDF, CWE-400/CWE-770 und den
VM-Vertrag fand keine breite Refaktorierung, keine Zeitgarantie und keine
interne Detailoffenlegung. Die Vorvalidierung geschieht in beiden VM-Wegen vor
Allokation oder Ausführung; die Budgetdiagnose entsteht vor Instruktion `N+1`.
Die Restrisiken bleiben CPU-Zeit außerhalb der Instruktionszählung, Speicher im
gültigen Stackrahmen und fehlende Prozess-/Host-Isolation.

English: Review against NIST SSDF, CWE-400/CWE-770, and the VM contract found
no broad refactor, timing guarantee, or internal-detail disclosure. Both VM
paths validate before allocation or execution, and emit the budget diagnostic
before instruction `N+1`. Residual risks remain CPU time outside instruction
counting, memory within the allowed stack limit, and absent process/host
isolation.
