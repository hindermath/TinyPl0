# Cloud-Autonomie / Cloud Autonomy Applicability

Deutsch: TinyPl0 läuft lokal als Compiler, VM, CLI und Terminal-IDE. GitHub
Actions, Pages sowie mögliches Release-/Artefakthosting sind Lieferdienste,
nicht die Produktlaufzeit. BSI C3A ist deshalb als Portabilitäts- und
Exit-Perspektive für die Delivery-Fläche `Applicable`, aber für die lokale
Runtime `N/A`. Source, Buildskripte und Dokumentation bleiben exportierbar;
Provider-Logs und gehostete Pages benötigen für einen echten Exit einen Export
oder Neubau bei einem anderen Anbieter.

English: TinyPl0 runs locally. GitHub Actions, Pages, and possible release
hosting are delivery services, not product runtime. BSI C3A therefore applies
as a portability and exit lens to delivery, while local runtime is N/A. Source,
build scripts, and documentation remain portable; provider logs and hosted
pages need export or rebuilding for an actual exit.

| Bereich / Area | Shared responsibility | Exit-/Portabilitätsevidence / Exit evidence |
|---|---|---|
| Lokale Runtime | Maintainer/Nutzer; kein Cloud-Provider | Solution, SDK-Pins, lokale Build-/Dokumentationspfade |
| Actions | Provider: Runnerplattform; Projekt: Workflow, Pins, minimale Rechte | YAML, full SHAs, lokaler reproduzierbarer Befehl |
| Pages | Provider: Hosting/TLS; Projekt: `_site`-Inhalt | normalisiertes Artefaktmanifest und DocFX-Quelle |
| Releases | Erst bei realem Release anwendbar | SBOM, Hash, Provenienz und Download-Inventar |

Owner: TinyPl0-Maintainer. Trigger: Providerwechsel, externer Runtime-Dienst,
nicht exportierbarer Zustand oder Releaseprozess. Evidence-Ziel: getesteter
Exit-Run und neuer Provider-/lokaler Artefakthash.
