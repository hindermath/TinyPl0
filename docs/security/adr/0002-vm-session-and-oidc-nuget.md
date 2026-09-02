# S-ADR 0002: VM-Session und OIDC-NuGet / VM Session and OIDC NuGet

## Status

Accepted — 2026-09-02

## Deutsch

Die VM validiert Optionen und P-Code an der Hostgrenze und verwendet einen
Decoder. Erwartete I/O-Fehler werden in stabile Diagnosen ohne fremde
Exception-Texte übersetzt. Öffentliche Pakete werden nur über das gebundene
GitHub-Environment nuget-release und NuGet/login mit kurzlebigem OIDC-
Credential veröffentlicht. Alle Actions sind auf vollständige Commit-SHAs
gebunden. Teilveröffentlichung, unbekannte Providerantwort oder fehlende
OIDC-Policy blockiert; ein API-Key-Fallback ist nicht autorisiert.

## English

The VM validates options and P-Code at the host boundary and uses one decoder.
Expected I/O failures become stable diagnostics without foreign exception
text. Public packages are published only through the bound nuget-release
environment and NuGet/login with a short-lived OIDC credential. Every action
is pinned to a full commit SHA. Partial publication, unknown provider state, or
missing OIDC policy blocks; an API-key fallback is not authorised.
