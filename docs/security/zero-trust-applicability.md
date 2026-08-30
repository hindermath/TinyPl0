# Zero-Trust-Anwendbarkeit / Zero Trust Applicability: TinyPl0

**Feature / Phase**: `004-secure-development-hardening` / implement
**Datum / Date**: 2026-08-30
**Entscheidung / Decision**: `N/A`
**Owner / Review**: TinyPl0-Maintainer / unabhängige Architektur-Review

| Merkmal / Characteristic | Ja/Nein | Begründung / Rationale |
|---|---|---|
| Verteilte Produktlaufzeit / distributed product runtime | Nein / No | lokaler Prozess / local process |
| Service-basiert / service-based | Nein / No | kein Produktdienst / no product service |
| Cloud-Runtime | Nein / No | Actions/Pages sind Delivery, nicht Runtime |
| Remote verwaltet / remotely managed | Nein / No | keine Verwaltungs-API |
| Identitätsföderation / federated identity | Nein / No | keine Authentifizierung oder Konten |
| Mehrgerätezugriff / multi-device access | Nein / No | keine gemeinsame Remote-Ressource |

Deutsch: NIST SP 800-207 ist für die aktuelle lokale Compiler-/VM-Topologie
nicht anwendbar. Standort, Identität oder Gerätezustand treffen keine
Produkt-Zugriffsentscheidung. Loopback-Dokumentation ist statisch, ohne Konto,
Remote-Administration oder sensible Mehrbenutzerdaten. Allgemeine Prinzipien
wie Least Privilege, explizite Trust Boundaries und fail-safe Defaults bleiben
trotzdem in Architektur und Threat Model verbindlich.

English: NIST SP 800-207 does not apply to the current local compiler/VM
topology. Location, identity, and device state make no product access decision.
Loopback documentation is static, with no account, remote administration, or
sensitive multi-user data. General least-privilege, trust-boundary, and
fail-safe principles still apply.

Wiedervorlage wird ausgelöst durch Remote-/Cloud-Runtime, Mehrgerätezugriff,
föderierte Identität, Verwaltungs-API oder einen Dienst mit geschützten
Ressourcen. Owner ist der TinyPl0-Maintainer; erwartete Evidence wäre dann eine
Policy-Decision-/Enforcement-Sicht, Identitäts- und Gerätefluss sowie Auditlog-
Konzept. / Re-evaluate on remote/cloud runtime, multi-device access, federated
identity, management API, or a protected service. Expected evidence is a policy
decision/enforcement view, identity/device flow, and audit logging concept.
