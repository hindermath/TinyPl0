# Schnellprüfung / Quick Review

## Ziel / Goal

Diese Schnellprüfung führt Reviewer durch die Feature-Evidenz, ohne Sandbox, Secret-Datei oder privates Profil zu öffnen. / This quick review guides reviewers through the feature evidence without opening the Sandbox, a secret file, or a private profile.

## Lesereihenfolge / Reading Order

1. [spec.md](spec.md): Scope, Nicht-Ziele und Akzeptanz.
2. [research.md](research.md): stabile Beobachtungen und Beweisgrenzen.
3. [plan.md](plan.md): Bewertungs- und Lieferstrategie.
4. `sandbox-assessment.md`: 12/12 CL-12-Zeilen und Nutzungsentscheidung nach Implementierung.
5. `evidence-matrix.md`: Anforderung↔Nachweis↔Follow-up.
6. [checklists/sandbox-governance.md](checklists/sandbox-governance.md): Anforderungen an die Dokumentqualität.

## Fünf-Minuten-Entscheidung / Five-Minute Decision

Eine prüfende Person muss diese Fragen aus dem Text beantworten können:

1. Ist reguläre agentische TinyPl0-Schreibarbeit freigegeben?
2. Welche drei Mindestbedingungen fehlen oder gelten noch?
3. Welcher einzige Projekt-Mount dürfte später schreibbar sein?
4. Wo bleiben Agentenzustand, Build-Ausgaben und Secrets?
5. Welche Arbeit bleibt lokal, in CI oder Human-only?

Die geplante Antwort auf Frage 1 ist `Nein, derzeit Not Ready`; ein späterer Read/Build/Test-Pilot ist nur bedingt möglich.

## Read-only Identitätsprüfung / Read-Only Identity Check

PowerShell 7 ist auf diesem Host die bevorzugte Automationsbasis. Der folgende Check liest nur TinyPl0-Dateien:

```powershell
pwsh -NoLogo -NoProfile -Command '$state = Get-Content -LiteralPath "specs/005-sandbox-secure-development/autonomous-run-state.json" -Raw -Encoding UTF8 | ConvertFrom-Json; foreach ($item in $state.acceptedArtifacts) { $actual = (Get-FileHash -LiteralPath $item.path -Algorithm SHA256).Hash.ToLowerInvariant(); if ($actual -ne $item.sha256) { throw "Accepted input drift: $($item.path)" } }; "PASS: accepted inputs"'
```

Der lokale Pfad zum separaten Sandbox-Repository wird nicht versioniert. Für eine bewusste read-only Referenzprüfung kann eine Person ihn vorübergehend als `SANDBOX_REFERENCE_ROOT` setzen; der Wert gehört nicht in Logs oder Artefakte.

## Stop-Regeln / Stop Rules

Sofort stoppen, wenn:

- ein Secret oder privater Profilinhalt sichtbar wird;
- ein unerwarteter Host- oder Nebenprojektpfad beschreibbar ist;
- der beobachtete Sandbox-Commit nicht stimmt;
- eine Freigabe als erfüllt behauptet wird, obwohl nur Entwurf oder Kommentar vorliegt;
- die Delivery-Menge Produktcode, Sandbox-Dateien oder bestehende `docs/security/`-Dateien enthält.

*Stop immediately if a secret or private profile becomes visible, an unexpected host or neighboring project path is writable, the Sandbox commit differs, an approval is overstated, or the delivery set contains product, Sandbox, or existing `docs/security/` changes.*

## Ergebnisinterpretation / Result Interpretation

- `Pass` bedeutet nur, dass der jeweilige Dokumentvertrag erfüllt ist.
- `Open` bedeutet, dass Owner, Risiko, Folgeaktion, Termin, Evidenzziel und Trigger dokumentiert sein müssen.
- `N/A` bedeutet, dass Begründung und Neubewertungs-Trigger vorhanden sein müssen.
- Admin Bypass ist niemals eine menschliche Approval.
