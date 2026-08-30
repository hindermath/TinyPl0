# Restrisiken / Residual Risks

## Deutsch

| Priorität | Risiko | Owner | Termin oder Trigger | Erwartete Evidence |
|---|---|---|---|---|
| High | Sechs bestätigte Kontrolllücken sind bis zu unverändertem Rot, kleinstem Fix und Grün offen. | TinyPl0-Maintainer | Vor dem ersten bedingten Edit und vor T046 | Validator-Exit, Hash, exakter Dateisatz und Regression |
| High | VM-Schleifen und ungültige Stack-/Budgetwerte sind bis zur TDD-Scheibe unbegrenzt oder exception-anfällig. | VM-Maintainer | T048–T061 | Identische Testhashes, Rot→Grün, Batch-/Step-Parität |
| High | ASVS-, Dependency-, SBOM-, CVD- und A11Y-Evidence ist noch nicht vollständig. | Security-Maintainer | T062–T082 | Commitgebundene JSON-/Markdown-, Scan-, DocFX-, axe- und lynx-Evidence |
| Medium | Rechtliche Hersteller-/Steward- und Providerrollen sind nicht belegt. | Repository-Maintainer | Geschäfts- oder Vertragsmodell ändert sich | Schriftliche Rollen- und Vertragsentscheidung |
| Medium | Der lokale HTTP-Pfad ist außerhalb der sechs Änderungspakete. | CLI-Maintainer | Neuer ASVS-Befund oder externer Serverbetrieb | Separater akzeptierter Intake mit rotem Test |
| Low | Sandbox, AI-SBOM, Zero Trust, Produktkrypto und DPIA sind nicht anwendbar. | Repository-Maintainer | Einer der dokumentierten Scope-Trigger tritt ein | Neue Anwendbarkeitsentscheidung mit ausdrücklicher Autorität |

Kein Critical-/High-Risiko ist vom Agenten akzeptiert. High-Befunde bleiben
blockierend, bis technische Evidence sie schließt oder ein Maintainer eine
ausdrückliche, befristete Entscheidung mit kompensierender Kontrolle trifft.

## English

The six confirmed control gaps, VM resource boundaries, and incomplete ASVS,
dependency, supply-chain, disclosure, and accessibility evidence remain open
until their exact gates pass. Legal/provider facts remain maintainer decisions.
The local HTTP surface has no edit authority in this run. Sandbox, AI-SBOM,
Zero Trust, product cryptography, and DPIA are re-evaluated only when their
documented scope trigger occurs. The agent accepts no Critical or High risk.
