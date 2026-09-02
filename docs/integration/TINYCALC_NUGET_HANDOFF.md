# TinyCalc-Handoff / TinyCalc Handoff

## Deutsch

TinyCalc darf erst nach öffentlicher Verifikation auf die Pakete wechseln:

1. dieselbe stabile Version für TinyPl0.Core und TinyPl0.Vm;
2. Restore ausschließlich über https://api.nuget.org/v3/index.json;
3. keine lokale ProjectReference und kein privater Feed;
4. Run- und Step-Smoke-Test gegen die veröffentlichten Hashes;
5. dokumentierter Rückweg auf die letzte bekannte gute SemVer.

Dieser Implementierungslauf ändert TinyCalc nicht und startet kein Folgefeature.

## English

TinyCalc may switch only after public verification: use the same stable version
for both packages, restore only from NuGet.org, use no local project reference
or private feed, run both host paths against the published hashes, and retain
a documented rollback to the last known good SemVer. This implementation run
does not change TinyCalc and starts no follow-up feature.
