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

164. Agent (2026-09-08): Projektbezogenen Feldtest für das unveränderte Assurance-Preset v0.1.3 mit `ReleaseAccepted` abgeschlossen. C5 sowie CRA und formale Produktkonformität sind für den aktuellen nichtkommerziellen Ausbildungs-/Beispielscope `N/A`; technische Evidence wird am 2027-09-08, regulatorischer Scope am 2026-12-31 neu bewertet. Alle menschlichen Freigaben bleiben `Open`; keine IDE-Verhaltensänderung und kein NuGet-Release. / Close the project field test for the unchanged Assurance v0.1.3 preset with `ReleaseAccepted`. C5, CRA, and formal product conformity are `N/A` in the current non-commercial training/example scope; technical evidence is reviewed on 2027-09-08 and regulatory scope on 2026-12-31. All human approvals remain `Open`; no IDE behavior change or NuGet release.

165. Agent (2026-09-19): Statistik-Preset v0.1.0 aus hashgepruefter Quelle additiv installiert. Die 13 vorhandenen Presets bleiben unveraendert. IDE-Commitmetadaten werden in der bisherigen Maintenance-Reihe fortgeschrieben; kein lokaler Produkt-Build/Test, keine API-/Verhaltensaenderung und kein NuGet-Release. Konfiguration, Messung und native CI folgen vor der gesonderten fachlichen Abnahme. / Add the hash-verified statistics preset, preserving thirteen existing presets and advancing mandatory maintenance commit metadata only. No local product build/test, API change or release. Configuration, measurement and native CI precede separate human acceptance.

166. Agent (2026-09-26): Vier gepruefte Wartungsdateien aus Home Baseline PRs 310/311 synchronisiert: UTF-8-Preset-Installer mit Manpage sowie gestreamtes Paketinventar mit Regressionstests. Die 14er-Matrix und IDE-Logik bleiben unveraendert; nur verpflichtende Commit-Versionsmetadaten werden in der bestehenden Maintenance-Reihe fortgeschrieben. Kein lokaler .NET-Build/-Test oder Release. 18 Wartungstests auf WSL und 18 im Container bestanden; Staged-Delivery-Set, Shell-Syntax und Secret-Scan sichern die Lieferung. macOS bleibt primaer. Der Owner akzeptierte am 2026-09-26 ausschliesslich den Billing-bedingten CI-Ausfall bis zum erwarteten Reset am 2026-10-01 und bestaetigte MergeAndSync mit Admin-Bypass; nicht gestartete Tests gelten nicht als bestanden. Documentation Impact: UpdateRequired fuer Worklog und kanonische Manpage, GeneratedUpdate fuer bestehendes Statistik-Ledger; Owner Maintainer, Leserpfad PR/Worklog zur Installer-Manpage, keine neue Navigation oder Home-Verteilung des Worklogs, Wiedervorlage bei Wartungsvertragsaenderung. / Synchronize four reviewed maintenance files from Home Baseline PRs 310/311. Preserve fourteen presets and IDE behavior; advance mandatory commit metadata only. No local .NET build/test or release. Eighteen maintenance tests passed in each of WSL and the sandbox; validate staging, syntax and secrets. macOS remains primary. The owner accepted only the billing-related CI outage and reauthorized MergeAndSync/admin bypass; unexecuted jobs are not passed tests. Update this worklog and distributed manpage, generate the existing ledger, and re-evaluate when the maintenance contract changes.

<!-- dependabot-pr-114-version:start -->
167. Agent (2026-11-03): Dependabot-PR #114 wird mit den IDE-Versionsfeldern
`1.114.714.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #114 aligns the IDE version fields to `1.114.714.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-114-version:end -->

<!-- dependabot-pr-115-version:start -->
168. Agent (2026-11-03): Dependabot-PR #115 wird mit den IDE-Versionsfeldern
`1.115.729.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #115 aligns the IDE version fields to `1.115.729.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-115-version:end -->

<!-- dependabot-pr-116-version:start -->
169. Agent (2026-11-03): Dependabot-PR #116 wird mit den IDE-Versionsfeldern
`1.116.739.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #116 aligns the IDE version fields to `1.116.739.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-116-version:end -->

<!-- dependabot-pr-119-version:start -->
170. Agent (2026-11-03): Dependabot-PR #119 wird mit den IDE-Versionsfeldern
`1.119.749.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #119 aligns the IDE version fields to `1.119.749.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-119-version:end -->

<!-- dependabot-pr-120-version:start -->
171. Agent (2026-11-03): Dependabot-PR #120 wird mit den IDE-Versionsfeldern
`1.120.759.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #120 aligns the IDE version fields to `1.120.759.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-120-version:end -->

<!-- dependabot-pr-122-version:start -->
172. Agent (2026-11-03): Dependabot-PR #122 wird mit den IDE-Versionsfeldern
`1.122.770.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #122 aligns the IDE version fields to `1.122.770.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-122-version:end -->

<!-- dependabot-pr-121-version:start -->
173. Agent (2026-11-03): Dependabot-PR #121 wird mit den IDE-Versionsfeldern
`1.121.780.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #121 aligns the IDE version fields to `1.121.780.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-121-version:end -->

<!-- dependabot-pr-123-version:start -->
174. Agent (2026-11-03): Dependabot-PR #123 wird mit den IDE-Versionsfeldern
`1.123.790.94` an seinen finalen Branch-Commitzaehler gebunden. Der manuelle
Buildzaehler bleibt bei 94; es wurde kein lokaler Produkt-Build oder Test
aufgerufen. Funktions- und Abnahmegrenzen bleiben unveraendert.

*Dependabot PR #123 aligns the IDE version fields to `1.123.790.94` and the
final branch commit count. The manual build counter remains 94; no local
product build or test was invoked. Feature and acceptance boundaries are retained.*
<!-- dependabot-pr-123-version:end -->

175. Agent (2026-11-06): Die 23 Befunde des vollständigen Codex-Security-Scans
wurden in der Reihenfolge der Triage geprüft und behoben. Die IDE begrenzt nun
Syntaxhervorhebung, Formatierung und Laufzeitausgabe; Compiler, Parser,
P-Code-Leser und VM besitzen feste Ressourcen- und Zustandsgrenzen. Weitere
Korrekturen härten Dateipfade, Git-/Registry-Aufrufe, Release-Eingaben,
Cleanup-Aktionen und den Pre-Push-Scan. Ein unabhängiger Patch-Review fand
sieben zusätzliche Grenzfälle, die vor Abschluss ebenfalls korrigiert wurden.
Die lokale Regression umfasst 374/374 .NET-Tests, 98 Python-Tests mit elf
plattformbedingten Überspringungen, beide PowerShell-Governance-Suiten,
DocFX, sechs axe-Seiten und Lynx-Dumps. Der NuGet-Audit meldet keine bekannte
verwundbare Abhängigkeit mehr. Die jeweils wirksame IDE-Version steht
verbindlich in `src/Pl0.Ide/Pl0.Ide.csproj`; ein Release wurde nicht erstellt.

*The 23 findings from the complete Codex Security scan were reviewed and fixed
in triage order. The IDE now bounds highlighting, formatting, and runtime
output, while compiler, parser, P-Code loading, and VM execution enforce
resource and state limits. Additional fixes harden paths, Git and registry
execution, release inputs, cleanup actions, and pre-push scanning. An
independent patch review found seven further boundary cases, all corrected
before closeout. Local evidence covers 374/374 .NET tests, 98 Python tests with
eleven platform skips, both PowerShell governance suites, DocFX, six axe pages,
and Lynx dumps. The NuGet audit now reports no known vulnerable dependency.
The authoritative IDE version remains in `src/Pl0.Ide/Pl0.Ide.csproj`; no
release was created.*

176. Agent (2026-11-06): Der erste PR-#124-Lauf zeigte ausschließlich im
Ubuntu-Maintenance-TUI-Gate zwei fehlgeschlagene Grenztests, weil dort
`ripgrep` nicht installiert ist. Der isolierte `--apply-patch`-Pfad benötigt
keinen Repository-Scan; deshalb prüfen Bash und PowerShell `rg` nun erst vor
dem regulären Scan. Fünf Pfadgrenztests und beide Syntaxprüfungen bestehen
lokal. Produkt- und IDE-Laufzeitverhalten bleiben unverändert.

*The first PR #124 run failed only two boundary tests in the Ubuntu Maintenance
TUI gate because that image has no ripgrep. The isolated `--apply-patch` path
does not scan repository content, so Bash and PowerShell now require `rg` only
before the normal scan. Five path-boundary tests and both syntax checks pass
locally. Product and IDE runtime behavior are unchanged.*

177. Agent (2026-11-07): Sieben konkrete Review-Kommentare zu PR #124 wurden
umgesetzt. Die PowerShell-Patchgrenze vergleicht jetzt physisch aufgeloeste
Repository- und Zielpfade und deckt Verzeichnis-Symlinks sowie Windows-Junctions
ab; Bash bricht bei nicht aufloesbarer Repository-Wurzel sofort ab. Delegierte
Git-Fetches sind explizit an `origin` gebunden. `CompilerOptions` behaelt seinen
veroeffentlichten Neun-Parameter-Konstruktor und die Neun-Werte-Dekonstruktion,
waehrend die Ressourcenlimits als Init-Eigenschaften erhalten bleiben. Der
Editor sucht sortierte Highlight-Spans binaer, und das Testprojekt erzwingt
`Microsoft.Build.Tasks.Git` 10.0.303. Die Version `1.124.797.101` dokumentiert
zwei weitere Testlaeufe; ein Release wurde nicht erstellt.

*Seven actionable PR #124 review comments were implemented. The PowerShell
patch boundary now compares physically resolved repository and target paths,
with directory-symlink and Windows-junction regressions; Bash exits immediately
when the repository root cannot be resolved. Delegated Git fetches are bound
explicitly to `origin`. `CompilerOptions` retains its published nine-parameter
constructor and nine-value deconstruction while keeping resource limits as init
properties. The editor binary-searches sorted highlight spans, and the test
project pins `Microsoft.Build.Tasks.Git` 10.0.303. Version `1.124.805.101`
records two further test runs; no release was created.*

178. Agent (2026-11-21): Dependabot-PR #118 aktualisiert `Terminal.Gui` von
2.0.0 auf 2.5.0. Der rote CI-Restore (`NU1004`) fuehrte zur aktualisierten
Test-Lockdatei. Der erste lokale Build zeigte 41 API-Fehler; die IDE nutzt nun
die neuen Namespaces, `Runnable`, `OptionSelector`, Instanz-Dialoge und
Menue-Konstruktoren. Locked Restore und Release-Build bestehen. Lokal bestehen
377 von 377 Tests mit .NET-11-Rollforward, weil die .NET-10-Laufzeit fehlt.
Ein Terminal-Smoke-Test oeffnete IDE und Compiler-Einstellungen, brach den
Dialog ab und beendete die IDE ueber das Menue. Der NuGet-Audit fuer IDE und
Tests meldet keine bekannte Schwachstelle. `TextView` ist in 2.5.0 veraltet;
ein Editorwechsel bleibt ein eigener Folgeauftrag. Die IDE-Version ist auf
`1.118.858.111` ausgerichtet; ein Release wurde nicht erstellt.

*Dependabot PR #118 updates `Terminal.Gui` from 2.0.0 to 2.5.0. The red CI
restore (`NU1004`) led to an updated test lockfile. The first local build
showed 41 API errors; the IDE now uses the new namespaces, `Runnable`,
`OptionSelector`, instance dialogs, and menu constructors. Locked restore and
Release build pass. All 377 local tests pass with .NET 11 roll-forward because
the .NET 10 runtime is unavailable locally. A terminal smoke test opened the
IDE and compiler settings, canceled the dialog, and exited through the menu.
The IDE and test NuGet audits report no known vulnerability. `TextView` is
obsolete in 2.5.0; an editor migration needs a separate follow-up. The IDE
version is aligned to `1.118.858.111`; no release was created.*

179. Agent (2026-11-21): Die Kontrastkorrektur fuer PR #118 behaelt die
Turbo-Pascal-Farben bei. Die abgeleiteten Textzustaende `Editable` und
`ReadOnly` hatten Gelb auf hellem Grund erzeugt. Eine eingebettete Palette
legt jetzt Vorder- und Hintergrund fuer Text, Auswahl, Fokus, Dialoge und
Menues fest. Fuer normale Schrift pruefen Tests mindestens 4,5:1, fuer
Fokusflaechen gegen den angrenzenden Grund mindestens 3:1 nach WCAG 2.2 AA.
Auch Debug-Markierungen und Regenbogenfarben bestehen die Textpruefung.
Zuerst schlug der neue Kontrasttest fehl (1 von 1); nach der Korrektur
bestanden 9 von 9 gezielten Tests und 386 von 386 Tests insgesamt. Locked
Restore und Release-Build bestehen. Der Build meldet 13 bestehende
`TextView`-Veraltungswarnungen. Ein PTY-Smoke-Test bei 134x27 Zeichen zeigte
Editor, Menue und Compiler-Dialog mit dunklen Textflaechen und beendete die
IDE regulaer. Die PTY nutzte ANSI-Grundfarben; die tatsaechliche Palette der
beiden macOS-Terminalprofile muss bei einer Sichtpruefung beruecksichtigt
werden. Es wurde kein Release erstellt.

*The PR #118 contrast fix keeps the Turbo Pascal colors. Derived `Editable`
and `ReadOnly` states had produced yellow text on a light background. An
embedded palette now defines foreground and background colors for text,
selection, focus, dialogs, and menus. Tests require at least 4.5:1 for normal
text and 3:1 for focus areas against adjacent backgrounds under WCAG 2.2 AA.
Debug markers and rainbow colors also pass the text contrast test. The new
contrast test failed first (1 of 1); then all 9 focused tests and all 386
tests passed. Locked restore and the Release build pass. The build retains
13 `TextView` deprecation warnings. A 134x27 PTY smoke test showed the editor,
menu, and compiler dialog with dark text areas and exited normally. The PTY
used basic ANSI colors; the actual palettes of both macOS Terminal profiles
still matter for a visual check. No release was created.*

180. Agent (2026-11-21): Der Statistik-Workflow verwendet nun seine eigene
Datei als Cache-Abhaengigkeit fuer `setup-uv`. Der bisherige Windows-Hinweis
zu fehlenden Python-Manifesten ist die rote Ausgangsevidenz. Die gepinnte
Spec-Kit-Revision steht in derselben Workflow-Datei, sodass ihre Aenderung
den Cache neu schluesselt. IDE-Verhalten und Tests bleiben unveraendert;
der Buildzaehler bleibt bei 116. Der neue CI-Lauf muss die Windows-Meldung
ohne diese Warnung bestaetigen.

*The statistics workflow now uses its own file as the `setup-uv` cache
dependency. The former Windows notice about missing Python manifests is the
red baseline evidence. The pinned Spec Kit revision is in that workflow file,
so changing it changes the cache key. IDE behavior and tests are unchanged;
the build counter stays at 116. The new CI run must confirm that the Windows
job no longer reports this warning.*

181. Agent (2026-11-21): Fuer Folge-PR #138 nutzt die IDE
`Terminal.Gui.Editor` 2.5.7 statt `TextView`. Quelleditor, drei
Ausgabefenster, Debugfenster und Regenbogenansicht behalten ihre Aufgaben;
Ausgaben bleiben schreibgeschuetzt. Inhalt, Cursor und Farbmarkierungen sind
an Dokumentereignisse und visuelle Zeilen angepasst. Der neue Migrationstest
schlug auf dem unveraenderten Stand nach PR #118 fehl (1 von 1) und besteht
jetzt. Alle 392 Tests, Locked Restore und Release-Build bestehen; der Build
meldet 0 Warnungen. NuGet meldet keine bekannte Schwachstelle in den
IDE-Abhaengigkeiten. Ein PTY-Test mit 134x27 Zeichen pruefte Start, Texteingabe,
Cursorstatus und Menue. Eine Sichtpruefung in Terminal.app mit Editor,
Ausgaben, Debug, Dialog und Fokuswechsel bestaetigte Thorsten am PR-Kopf
`263bbec` in Terminal.app. Der erste Windows-CI-Lauf zeigte fuenf
fehlschlagende Formatierungstests: Der neue
Editor gab LF statt des bisherigen Plattform-Zeilenumbruchs aus. Die IDE
wandelt nun nur formatierte Texte am Editor-Eingang ins Systemformat um;
danach bestehen lokal erneut 392 Tests und ein warnungsfreier Release-Build.
Der Windows-CI-Nachlauf bestand anschliessend. Die Inhaltsversion lautet
`1.138.875.130`; ein Release wurde nicht erstellt.

*For follow-up PR #138, the IDE uses `Terminal.Gui.Editor` 2.5.7 instead of
`TextView`. The source editor, three output panes, debug pane, and rainbow
view keep their roles; output remains read-only. Content, caret, and colors
now use document events and visual lines. The new migration test failed on
the unchanged post-PR-118 revision (1 of 1) and now passes. All 392 tests,
locked restore, and Release build pass; the build reports 0 warnings. NuGet
reports no known vulnerability in IDE dependencies. A 134x27 PTY smoke test
covered startup, typing, caret status, and the menu. A visual Terminal.app
check of the editor, output, debug, dialog, and focus changes was confirmed by
Thorsten on PR head `263bbec` in Terminal.app. The first Windows CI run
failed five formatting tests:
the new editor exposed LF instead of the previous platform newline. The IDE
now converts only formatted text at the editor boundary. After the fix, all
392 local tests and a warning-free Release build pass again. A Windows CI
rerun passed. The content version is `1.138.875.130`;
no release was made.*

182. Agent (2026-11-21): Nach dem Merge von PR #137 wurde PR #138
konfliktfrei mit `main` zusammengefuehrt. Der Statistik-Workflow enthaelt
jetzt die Cache-Abhaengigkeit; die IDE-Migration bleibt erhalten. Locked
Restore, Release-Build mit 0 Warnungen und alle 392 Tests bestanden auf dem
gemeinsamen Stand. Der lokale Testhost benoetigte wegen der vorhandenen
.NET-11-Runtime `DOTNET_ROLL_FORWARD=Major`. Die beiden Statistikberichte
werden aus dieser Inhaltsrevision neu erzeugt; es entstand kein Release.

*After PR #137 was merged, PR #138 was reconciled with `main` without
remaining conflicts. The statistics workflow now contains the cache
dependency, and the IDE migration remains intact. Locked restore, a Release
build with 0 warnings, and all 392 tests passed on the combined revision.
The local test host needed `DOTNET_ROLL_FORWARD=Major` because only the .NET 11
runtime is installed. Both statistics reports are regenerated from this
content revision; no release was made.*

183. Agent (2026-11-21): Unter Ansicht > Darstellung lässt sich das
Farbschema auswählen. Turbo Pascal 5 bleibt Standard. Zwei neue TinyPl0-Paletten
orientieren sich an Turbo Pascal 6 und 7. Die Auswahl gilt sofort und wird im
Benutzerprofil gespeichert. Abbrechen verwirft den Wechsel. Bei einem
Schreibfehler bleibt die Farbe bis zum Ende der Sitzung aktiv.
Der neue Wechseltest scheiterte ohne die Paletten und besteht mit ihnen.
Locked Restore, Release-Build mit 0 Warnungen und alle 394 Tests bestehen.
Im Pseudoterminal mit 134×27 Zeichen wurde der Dialog per Tastatur geöffnet und
abgebrochen. Die Sichtprüfung in Terminal.app folgt vor dem Merge.

*View > Appearance offers three color schemes. Turbo Pascal 5 remains the
default. Two new TinyPl0 palettes take inspiration from Turbo Pascal 6 and 7.
The choice takes effect immediately and is saved in the user profile. Cancel
discards the change. If writing fails, the color remains active for the session.
The new switching test failed without the palettes and passes with them.
Locked restore, a Release build with 0 warnings, and all 394 tests passed.
The dialog was opened and canceled by keyboard in a 134×27 pseudo-terminal.
The visual check in Terminal.app follows before the merge.*

184. Agent (2026-11-21): Bei der Sichtprüfung in Terminal.app mit 134×27
Zeichen war Turbo Pascal 6 aktiv, aber die Zeile für Turbo Pascal 7 fehlte.
Der Darstellungsdialog gibt der Auswahlliste jetzt Platz für alle drei Zeilen.
OK ist die Standardschaltfläche. Der Pseudoterminal-Test bestätigte den
Fehler vor der Korrektur und danach alle drei sichtbaren Optionen.
Der Release-Build meldete 0 Warnungen. Alle 395 Tests bestanden.

*In the 134×27 Terminal.app review, Turbo Pascal 6 was active but the Turbo
Pascal 7 row was missing. The appearance dialog now leaves room for all three
choices. OK is the default button. The pseudo-terminal reproduced the
missing row and then confirmed that all three choices were visible.
The Release build reported 0 warnings. All 395 tests passed.*

185. Agent (2026-11-21): TinyPl0 prüft den Speicherpfad für das Farbschema
nach der CodeQL-Meldung zu PR #139. Fehlt ein absoluter Pfad innerhalb des
Benutzerprofils, verwendet die IDE das Standardschema und speichert den
nächsten Wechsel nur für die laufende Sitzung. Der Regressionstest zeigte
vor der Korrektur, dass ein Nachbarordner gelesen wurde. Locked Restore und
alle 396 Release-Tests bestanden danach.

*TinyPl0 checks the color scheme path after the CodeQL report on PR #139.
If it cannot identify an absolute path inside the user profile, the IDE
uses the default scheme and applies the next change for the current
session only. The regression test showed that the old code read from a
sibling folder. Locked restore and all 396 Release tests then passed.*

186. Agent (2026-11-22): Die IDE zeigt vier verschiebbare Trennlinien.
Unter Ansicht > Fenstergröße ändern wechselt Tab zur nächsten Trennlinie,
Umschalt+Tab zur vorherigen. Pfeiltasten verschieben die markierte Linie um
eine Zelle. Enter übernimmt die Aufteilung, Esc holt die vorige zurück.
Die Linien lassen sich auch mit der Maus ziehen. Ansicht > Layout
zurücksetzen stellt die Standardaufteilung wieder her. Unter 80×24 zeigt
die IDE jeweils einen Bereich. Das Menü Ansicht wählt den Bereich aus
und ist für schmale Terminals weiter links erreichbar. Beim Vergrößern
kehrt die Aufteilung der laufenden Sitzung zurück.

*The IDE shows four movable dividers. Under View > Resize panes, Tab moves
to the next divider and Shift+Tab moves to the previous one. Arrow keys
resize the highlighted divider by one cell. Enter keeps the layout and
Esc restores the previous layout. Mouse users can drag the dividers.
View > Reset layout restores the default proportions. Below 80×24, the
IDE shows one pane at a time. Use the View menu to choose a pane. The menu
is closer to the left so it remains available in narrow terminals.
Enlarging the terminal restores the session's tiled layout.*

Der erste Test zeigte, dass vorher auch unter der Grenze fünf Fenster
sichtbar blieben. Locked Restore, Release-Build mit 0 Warnungen und alle
400 Tests bestehen. Die Kontrasttests prüfen Trennlinien und Fokus in
allen drei Farben. Ein macOS-Pseudoterminal prüfte Mausziehen,
Tastatursteuerung und den Wechsel zwischen 134×27 und 79×23.
Die Sichtprüfung in Terminal.app folgt vor dem Merge.

*The first regression test showed that the old IDE kept five panes
below the threshold. Locked restore, Release build with 0 warnings,
and all 400 tests passed. Contrast tests cover the dividers and their
focus in all three schemes. A macOS pseudo-terminal exercised mouse drag,
keyboard controls, and resizing between 134×27 and 79×23.
The visual check in Terminal.app follows before the merge.*

187. Agent (2026-11-24): TinyPl0 und die Wartungs-TUI verwenden `net11.0`
und C# 15. Die IDE nutzt weiterhin Terminal.Gui 2.5.0. Der gesperrte
NuGet-Restore zeigte vor der Erneuerung der Lockdateien `NU1004` und
bestand danach. Der lokale Release-Build hatte keine Warnungen. Alle 400
TinyPl0- und 65 Wartungs-TUI-Tests bestanden. Das Golden-Master-Skript
erzeugte dieselben 22 P-Code-Referenzausgaben. Die plattformübergreifende
CI-Abnahme und PR #146 folgen separat.

*TinyPl0 and the maintenance TUI use `net11.0` and C# 15. The IDE continues
to use Terminal.Gui 2.5.0. Locked NuGet restore reported `NU1004` before the
lock files were regenerated and passed afterwards. The local Release build
had no warnings. All 400 TinyPl0 tests and 65 maintenance TUI tests passed.
The golden-master script reproduced the same 22 reference P-Code outputs.
Cross-platform CI acceptance and PR #146 follow separately.*

188. Agent (2026-11-24): Die IDE verwendet Terminal.Gui 2.6.0 auf .NET 11.
Terminal.Gui.Editor bleibt bei 2.5.7. Der gesperrte Restore reproduzierte
`NU1004` und bestand nach der gemeinsamen Erneuerung der Lockdateien von
IDE und Tests. Die transitiven Änderungen umfassen `Microsoft.Extensions`
11.0.0, `TextMateSharp` 2.0.5 und `Wcwidth` 5.0.2. Release-Build und alle
400 Tests bestanden. Im macOS-Pseudoterminal wurde eine temporäre PL/0-Datei
geöffnet, bearbeitet und gespeichert. Danach funktionierten
Tastaturmenüs, Dialogabbruch mit Esc, Programmausgabe `42`, Debug-Schritt
und der Wechsel zwischen 134×27 und 79×23 Zeichen. Thorstens manueller
Sichttest in Terminal.app mit 134×27 Zeichen zeigt den 2.6.0-Build,
das Mehrfensterlayout und einen erfolgreichen Programmlauf mit Ausgabe
300. Die CI für diesen Branch steht aus. Profil-2-Drift tritt bereits
auf dem Ausgangscommit `c89e54f` auf.

*The IDE uses Terminal.Gui 2.6.0 on .NET 11 and retains Editor 2.5.7.
Locked restore reproduced `NU1004` and passed after both the IDE and test
lock files were regenerated. The transitive updates include
`Microsoft.Extensions` 11.0.0, `TextMateSharp` 2.0.5, and `Wcwidth` 5.0.2.
The Release build and all 400 tests passed. The macOS pseudo-terminal
exercise opened, edited, and saved a temporary PL/0 file and covered keyboard
menus, dismissing a dialog with Esc,
program output `42`, a debugger step, and resizing between 134×27 and 79×23.
Thorsten tested the 2.6.0 build in Terminal.app at 134×27. His screenshot
shows the multi-pane layout and a successful run with output 300.
CI for this branch is pending.
Profile 2 drift is also present on base commit `c89e54f`.*
