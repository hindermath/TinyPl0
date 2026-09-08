# TinyPl0 IDE Worklog

## Zweck / Purpose

Die historischen IDE-Einträge 1 bis 149 bleiben unverändert in
`requirements/baseline/Pflichtenheft_IDE.pre-intake-split.2026-07-26.md`.
Neue operative Einträge werden nur noch hier ergänzt, damit die normative
Baseline stabil bleibt.

*Historical IDE entries 1 through 149 remain unchanged in the frozen IDE
baseline. New operational entries are appended only here so that the normative
baseline remains stable.*

## Einträge / Entries

150. Agent (2026-07-26): Requirements- und Intake-Struktur getrennt. Die drei
Pflichtenhefte wurden als hashgebundene Baselines eingefroren; dieser Eintrag
eröffnet das eigenständige künftige IDE-Arbeitsprotokoll.

151. Agent (2026-07-26): Die Strukturmigration wurde mit kanonischer Serie,
supersedierenden Receipts und plattformparitätischen Validatoren vorbereitet.
Ohne Build- oder Testlauf wurden die IDE-Versionsfelder für den Kerncommit auf
`1.2.274.14` ausgerichtet.

152. Agent (2026-07-26): Review- und Supersession-Evidence wurden auf den
Strukturcommit `07dae1c` gepinnt. Für diesen Evidence-Commit wurden die
IDE-Versionsfelder ohne Build- oder Testlauf auf `1.2.275.14` ausgerichtet.

153. Agent (2026-09-01): Der aktuelle Review der 15-Ziele-Intake-Serie wurde
als `Ready` dokumentiert und der ungültige Vorgänger nachvollziehbar
supersediert. Für Review-PR 78 und den finalen Delivery-Commit 491 wurden die
IDE-Versionsfelder ohne Produkt-Build oder Testlauf auf `1.78.491.42`
ausgerichtet; Laufzeit und IDE-Verhalten bleiben unverändert.

*The current 15-target intake-series review was documented as `Ready`, and the
invalid predecessor was superseded with traceable evidence. For review PR 78
and final delivery commit 491, the IDE version fields were aligned to
`1.78.491.42` without a product build or test run; runtime and IDE behavior
remain unchanged.*

154. Agent (2026-09-02): Der autonome Lauf 006 hat den gemeinsamen
VM-Ausführungskern, Run/Step-Parität und die lokalen NuGet- sowie
Dokumentationsnachweise vorbereitet. Vor jedem lokalen Build- oder Testlauf
wurde der Buildzähler fortgeschrieben. Der Windows-CI-Befund wurde durch
plattformneutrale temporäre Testpfade behoben und gezielt nachgeprüft; die
PR-bereite Arbeitsversion ist `1.6.510.72`. Die IDE nutzt weiterhin die
öffentliche Step-Schnittstelle und behält ihr sichtbares Laufzeitverhalten.

*Autonomous run 006 prepared the shared VM execution engine, Run/Step parity,
and the local NuGet and documentation evidence. The build counter was advanced
before every local build or test run. The Windows CI finding was fixed with
platform-neutral temporary test paths and rechecked with a focused test; the
PR-ready version is `1.6.510.72`. The IDE still uses the public step interface
and keeps its visible runtime behavior.*

155. Agent (2026-09-02): Die Schemaangabe der Gate-Anforderungen wurde vor dem
Release-Closeout an den installierten Vertrag `1.0` angeglichen; die
lebenszyklusgebundene PreMerge- und PostMerge-Evidenz verwendet weiterhin
Schema `2.0`. Für die beiden kausalen Release-Closeout-Commits wurde die
IDE-Version ohne Produkt-Build oder Testlauf auf `1.6.512.72` ausgerichtet.

*Before release closeout, the gate-requirements schema declaration was aligned
with the installed `1.0` contract; lifecycle-bound PreMerge and PostMerge
evidence continues to use schema `2.0`. For the two causal release-closeout
commits, the IDE version was aligned to `1.6.512.72` without a product build or
test run.*

156. Agent (2026-09-02): Nach der erfolgreichen OIDC-Veröffentlichung wurde
der öffentliche NuGet-Prüfpfad für repository-signierte Pakete und eine eng
begrenzte, veröffentlichungsfreie Wiederaufnahme korrigiert. Für den
zugehörigen Abschluss- und Review-Korrektur-Commits wurde die IDE-Version ohne
Produkt-Build oder Testlauf auf `1.6.518.72` ausgerichtet.

*After successful OIDC publication, the public NuGet verification path was
corrected for repository-signed packages and a narrowly scoped recovery that
cannot republish. The IDE version was aligned to `1.6.518.72` for the related
closeout and review-fix commits without a product build or test run.*

157. Agent (2026-09-03): Der veröffentlichungsfreie Recovery-Run hat beide
öffentlichen NuGet-Pakete `0.4.0`, ihre Repository-Signaturen, den unsignierten
Inhalt und den frischen Consumer erfolgreich bestätigt. Danach wurden Lauf
006, Intake-Archiv und Serien-Lineage kausal abgeschlossen. Über Closeout-
Commit 521 und die finale Statistik-Aktualisierung mit Commit 522 endet die
IDE-Version ohne Produkt-Build oder Testlauf bei `1.6.522.72`; ein Folgefeature
wurde nicht gestartet.

*The verification-only recovery run successfully confirmed both public NuGet
packages 0.4.0, their repository signatures, unsigned content, and the fresh
consumer. Run 006, the intake archive, and series lineage were then closed
causally. Across closeout commit 521 and the final statistics refresh at commit
522, the IDE version ends at `1.6.522.72` without a product build or test run;
no follow-up feature started.*

158. Agent (2026-09-03): Die NuGet-Beschreibungen für `TinyPl0.Core` und
`TinyPl0.Vm` wurden als ausführliche, bilinguale und textfreundliche
Paket-Landingpages überarbeitet. Der fachliche Feature-Commit verwendet
Commitzählstand 524; Review-Fix 527 verlangt beide Sprachüberschriften. Der
Folgebranch entkoppelt den Paketvertragstest von der konkreten Release-Version
und die Statistik von generierten Release-Metadaten. Nach erneutem Testlauf,
Fix-Commit 531 und Statistik-Folgecommit 532 endet die IDE-Version bei
`1.6.532.80`.

*The NuGet descriptions for `TinyPl0.Core` and `TinyPl0.Vm` were revised as
detailed, bilingual, and text-friendly package landing pages. Feature commit
524 implements the content, and review fix 527 requires both language
headings. The follow-up branch makes the package contract independent of the
specific release version and excludes generated release metadata from the
statistics. After the repeated test run, fix commit 531, and statistics
follow-up commit 532, the IDE version ends at `1.6.532.80`.*

159. Agent (2026-09-06): Assurance v0.1.2 wurde als 13. Preset ergänzt.
Die zwölf bestehenden Presets bleiben unverändert. Für Integrationscommit 536
wird ausschließlich die vorgeschriebene IDE-Versionsmetadatenpflege auf
`1.6.536.80` vorgenommen; kein lokaler Produkt-Build/Test, keine NuGet-Veröffentlichung
und keine Änderung des IDE-Verhaltens. Die Produktregression läuft in der PR-CI.

*Assurance v0.1.2 is added as the thirteenth preset, preserving the original
twelve. Integration commit 536 aligns only the required IDE version metadata
to `1.6.536.80`; no local product build/test, NuGet publication or IDE behavior
change occurs. Product regression runs in PR CI.*

Die oben genannte Version bezeichnet ausschließlich den Integrationscommit,
nicht den finalen PR-Stand. Statistikfolgecommit 537 verwendete `1.6.537.80`.
Weitere Review-/Statistikcommits schreiben den Commitzähler gemäß Repository-Regel
fort; die jeweils wirksame Version steht verbindlich in
`src/Pl0.Ide/Pl0.Ide.csproj` (alle drei Versionsfelder identisch).

*The version above describes the integration commit, not the final PR head.
Statistics follow-up commit 537 used `1.6.537.80`. Subsequent review/statistics
commits advance the counter under the repository rule. The authoritative
current version is in `src/Pl0.Ide/Pl0.Ide.csproj`, with all three fields aligned.*

160. Agent (2026-09-06): Kanonisches Assurance-Patch v0.1.3 übernommen.
Zwölf andere Presets unverändert; keine IDE-Verhaltensänderung, kein lokaler
Produkt-Build/-Test und kein NuGet-Release. Der vorgeschriebene Commitzähler
wird bei jedem Commit fortgeschrieben; die drei Felder in
`src/Pl0.Ide/Pl0.Ide.csproj` bleiben die einzige aktuelle Versionsquelle.
Nachweis: `docs/maintenance/assurance-v013-adoption.md`.

*Adopt canonical Assurance v0.1.3, preserving twelve other presets. No IDE
behavior change, local product build/test or NuGet release. Advance mandatory
commit metadata at each commit; the three csproj fields remain authoritative.
See the adoption record for verification and boundaries.*

161. Agent (2026-09-07): Fehlende Evidence-Matrix aus 157 vorhandenen Kontrollbewertungen ergänzt. Bewertungs-JSON, alle 13 Presets und generierte Befehle unverändert. Kein IDE-Verhalten, Build, lokaler Produkttest oder NuGet-Release; verpflichtende Commit-Versionsmetadaten fortgeschrieben. Status in beiden Shells weiterhin Blocked wegen fehlender baseline.json. / Add the missing index of 157 existing assessments; preserve all source evidence and presets. No IDE behavior change, local product build/test or NuGet release. Mandatory commit metadata advances; both status validators still block on absent baseline.json.

162. Agent (2026-09-07): Vier explizit gesperrte Assurance-Gate-Nachweise aus 157 Altbewertungen ergänzt; Quellen- und Shell-Paritätsprüfung dokumentiert. Keine fachliche Freigabe, keine Änderung an Alt-Evidence, Presets oder IDE-Verhalten. Vorgeschriebene Versionsmetadaten fortgeschrieben. Produktregression in PR-CI; keine lokale .NET-Ausführung. / Add four explicitly blocked gate records from 157 historical assessments with source and shell-parity inspection. Preserve historical evidence, presets and IDE behavior; advance mandatory version metadata only. Product regression runs in PR CI, not local .NET.

163. Agent (2026-09-08): Secure-Development-Baseline auf 3.2.0 synchronisiert und den technischen Assurance-Evidence-Vertrag auf `Ready` revalidiert. Die 157 fachlichen Altbewertungen bleiben unverändert; Pilot-, Projekt- und Releasefreigabe bleiben `Open`. Keine IDE-Verhaltensaenderung, kein lokaler Produkt-Build/-Test und kein NuGet-Release. Review- und Statistik-Folgecommits richten die zweisprachige Evidence und die vorgeschriebenen Versionsmetadaten abschließend auf `1.6.566.80` aus. / Synchronize secure-development baseline 3.2.0 and revalidate the technical Assurance evidence contract as `Ready`. Preserve all 157 historical domain assessments and keep pilot, project, and release approvals `Open`. No IDE behavior change, local product build/test, or NuGet release. Review and statistics follow-up commits finally align bilingual evidence and mandatory version metadata to `1.6.566.80`.
