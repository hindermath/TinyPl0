# Restrisiken / Residual Risks

## Deutsch

| Priorität | Risiko | Owner | Termin oder Trigger | Erwartete Evidence |
|---|---|---|---|---|
| Medium | Der read-only geprüfte lokale HTTP-Pfad hat keine Härtungs-Änderungsautorität; ein neuer externer oder zustandsändernder Betrieb braucht einen eigenen Intake. | CLI-Maintainer | Neuer ASVS-Befund, externe Bindung oder schreibender Endpunkt | Separater akzeptierter Intake mit rotem Test und aktualisierter ASVS-Evidence |
| Low | C5 sowie CRA-/Konformitätspflichten sind für den aktuellen nichtkommerziellen Ausbildungs- und Beispielscope `N/A`; eine unbemerkte Scopeänderung könnte diese Entscheidung veralten lassen. | Repository-Maintainer | `2026-12-31` oder früher bei Cloud-Runtime, kommerzieller Nutzung, Marktbereitstellung, Kundenübergabe, Supportvertrag oder geänderter Hersteller-/Steward-Rolle | Neue dokumentierte Scope- und Anwendbarkeitsentscheidung |
| Low | Der lokale DocFX-A11Y-Zyklus ist bestanden; Änderungen an API, HTML-Theme, Navigation, Browser-Harness oder Workflow können die Aussage veralten lassen. | Accessibility-Maintainer | Eine der genannten Flächen ändert sich oder T098 prüft den exakten PR-Head | Erneute axe-/Lynx-Evidence auf dem geänderten Kandidaten |
| Low | AI-SBOM, Zero Trust, Produktkrypto, DPIA, NIS2, EU AI Act, DORA und Sandbox-Hardening sind im engen Produktscope begründet nicht anwendbar. | Repository-Maintainer | Einer der dokumentierten Scope-Trigger tritt ein | Neue Anwendbarkeitsentscheidung mit ausdrücklicher Autorität |

Kein Critical-/High-Risiko ist offen oder vom Agenten akzeptiert. Ein neuer
Critical-/High-Befund bleibt blockierend, bis technische Evidence ihn schließt
oder ein Maintainer eine ausdrückliche, befristete Entscheidung mit
kompensierender Kontrolle trifft.

## English

The VM, baseline, supply-chain, CVD, gitignore, dependency, ASVS, and local
rendered accessibility evidence is reconciled. The accepted host cycle passed
all three axe pages and both semantic text-browser paths, so the former High
accessibility risk is closed rather than accepted. C5 and CRA/conformity duties
are N/A only for the current non-commercial training/example scope and return
to review on 2026-12-31 or an earlier scope trigger. The local HTTP surface has
no edit authority in this run. No Critical or High risk is open or accepted by
the agent.
