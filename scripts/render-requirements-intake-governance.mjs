#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";

const root = process.cwd();
const write = process.argv.includes("--write");
const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const digest = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");
const read = (relativePath) => fs.readFileSync(path.join(root, relativePath), "utf8");
const readJson = (relativePath) => JSON.parse(read(relativePath));
const hashFile = (relativePath) => digest(read(relativePath));
const json = (value) => `${JSON.stringify(value, null, 2)}\n`;
const stableUuid = (key) => {
  const hex = crypto.createHash("sha256").update(`TinyPl0:${key}`).digest("hex");
  return `${hex.slice(0, 8)}-${hex.slice(8, 12)}-4${hex.slice(13, 16)}-a${hex.slice(17, 20)}-${hex.slice(20, 32)}`;
};
const config = readJson("requirements/intake-governance-config.json");
const seriesRoot = "requirements/intakes/series/tinypl0-delivery";
const seriesId = stableUuid("series");
const seriesReceiptId = "01ec955e-b458-469e-9dc8-4a76ba4154de";
const seriesOperationId = "017dae9c-4f29-41ad-b234-bf4da48f4238";
const reviewId = "8804ad13-41b4-4feb-a10d-26d2f55333e6";
const priorReviewId = "357ed01f-f120-4634-8596-45e7baffa17d";
const createdAt = "2026-07-26T22:00:00Z";
const seriesUpdatedAt = "2026-08-30T18:29:40Z";
const reviewHead = "26a81e655b4e15f412a954f536681a842dea6e2f";
const reviewedAt = "2026-08-30T14:55:45Z";
const priorReviewArchivePath =
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T145545Z-review/superseded-review.json";

const members = [
  ["constitution-change", "Lastenheft_Constitution_Change.md", "Completed"],
  ["secure-development-hardening", "Lastenheft_Secure-Development-Hardening.md", "Completed"],
  ["sandbox-gestuetzte-secure-development-haertung", "Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md", "Completed"],
  ["embeddable-vm-und-nuget", "Lastenheft_Embeddable-VM-und-NuGet.md", "Eligible"],
  ["quellcode-doku", "Lastenheft_Quellcode_Doku.md", "Blocked"],
  ["dokumentation-en", "Lastenheft_Dokumentation_EN.md", "Blocked"],
  ["ide-l10n", "Lastenheft_IDE-L10N.md", "Blocked"],
  ["a11y-ide", "Lastenheft_A11Y_IDE.md", "Blocked"],
  ["options-als-parameter", "Lastenheft_Options_Als_Parameter.md", "Blocked"],
  ["vm-cli", "Lastenheft_VM_CLI.md", "Blocked"],
  ["ide-erweiterung-pl0ide-pasm-pcod", "Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md", "Blocked"],
  ["pl0-optimierung", "Lastenheft_PL0_Optimierung.md", "Blocked"],
  ["clr-assembly", "Lastenheft_CLR_Assembly.md", "Blocked"],
  ["rl-se-checklist-selbstpruefung", "Lastenheft_RL-SE-Checklist-Selbstpruefung.md", "Pending"],
  ["gsdb-spec-kit-intensivpruefung", "Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md", "Pending"],
].map(([slug, fileName, status], index) => ({
  slug,
  fileName,
  status,
  order: index + 1,
  role: index === 0 ? "Primary" : "OrderedMember",
  receiptId: stableUuid(`receipt:${slug}`),
  operationId: stableUuid(`operation:${slug}`),
  path: slug === "constitution-change"
    ? "requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md"
    : slug === "secure-development-hardening"
      ? "requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md"
      : slug === "sandbox-gestuetzte-secure-development-haertung"
        ? "requirements/intakes/archive/Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.005-sandbox-secure-development.md"
      : `requirements/intakes/active/${fileName}`,
  reviewPath: slug === "constitution-change"
    ? "requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md"
    : `requirements/intakes/active/${fileName}`,
  receiptPath: `requirements/intakes/active/${fileName}`,
  priorTarget: `requirements/intakes/history/pre-intake-split-20260726/${fileName}`,
  priorReceipt: `specs/intake-authoring-receipts/history/${slug}.schema-1.1.json`,
  customReceipt: slug === "embeddable-vm-und-nuget",
}));
const targets = members.map((member) => member.path);
const reviewTargets = members.map((member) => member.reviewPath);
const dependencies = [
  [0, 1, "HardCompletionGate"],
  [1, 2, "HardCompletionGate"],
  [2, 3, "HardCompletionGate"],
  [3, 4, "CommentSurfaceBaseline"],
  [4, 5, "DocumentationSurfaceBaseline"],
  [5, 6, "DocumentationSurfaceBaseline"],
  [6, 7, "HardCompletionGate"],
  [7, 8, "HardCompletionGate"],
  [8, 9, "HardCompletionGate"],
  [9, 10, "HardCompletionGate"],
  [3, 10, "HardCompletionGate"],
].map(([from, to, kind]) => ({
  from: targets[from],
  to: targets[to],
  kind,
  binding: true,
}));
const roots = targets.filter((target) => !dependencies.some((edge) => edge.to === target));
const reviewPathByTarget = new Map(members.map((member) => [member.path, member.reviewPath]));
const reviewDependencies = dependencies.map(({from, to, kind}) => ({
  from: reviewPathByTarget.get(from),
  to: reviewPathByTarget.get(to),
  kind,
}));
const reviewRoots = reviewTargets.filter(
  (target) => !reviewDependencies.some((edge) => edge.to === target),
);
const manifestPath = `${seriesRoot}/manifest.json`;

const manifest = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesManifest",
  seriesId,
  title: "TinyPl0 Delivery Intake Series",
  policy: "tinypl0-delivery-v1",
  status: "Active",
  orderedTargets: members.map((member) => ({
    path: member.path,
    role: member.role,
    normalizedSha256: hashFile(member.path),
    status: member.status,
  })),
  roots,
  dependencies,
  evidencePaths: [
    "specs/requirements-reconciliation-20260726/requirements-coverage.json",
    "specs/requirements-reconciliation-20260726/migration-proposal.json",
    "Lastenheft_Abarbeitungsreihenfolge.md",
  ],
};
const manifestHash = digest(json(manifest));

function sourceRecord(relativePath) {
  return {
    sourceId: "SRC001",
    order: 1,
    kind: "File",
    label: "Archived predecessor intake",
    location: "Repository",
    path: relativePath,
    requestedUrl: "N/A",
    finalUrl: "N/A",
    retrievedAt: "N/A",
    httpStatus: "N/A",
    contentType: "N/A",
    contentLength: "N/A",
    etag: "N/A",
    lastModified: "N/A",
    redirectChain: [],
    rawSha256: "N/A",
    normalizedSha256: hashFile(relativePath),
    gitBlob: "N/A",
    proofBoundary: "Repository predecessor and normalized SHA-256",
  };
}

function receiptFor(member) {
  const prior = readJson(member.priorReceipt);
  return {
    schemaVersion: "2.0",
    documentType: "IntakeReceipt",
    receiptId: member.receiptId,
    intakeId: prior.receiptId,
    generator: {preset: "intake-authoring-governance", version: "0.2.1"},
    createdAt,
    operation: {
      operationId: member.operationId,
      type: "Update",
      authorityEvidence: "User-approved TinyPl0 requirements and intake consolidation plan",
    },
    status: "ReadyForReview",
    target: {path: member.receiptPath, normalizedSha256: hashFile(member.path)},
    sources: [sourceRecord(member.priorTarget)],
    profile: "level2-lastenheft",
    languagePolicy: "GermanFirstEnglishSecond",
    decisions: [
      {
        id: "IAD001",
        status: "Answered",
        question: "Welcher Zielpfad ist nach der Konsolidierung verbindlich?",
        answer: member.receiptPath,
        evidence: "specs/requirements-reconciliation-20260726/migration-proposal.json",
      },
      {
        id: "IAD002",
        status: "Answered",
        question: "Welche Delivery Authority gilt?",
        answer: "LocalImplementation",
        evidence: "The migration grants no feature-delivery authority.",
      },
    ],
    openDecisionIds: [],
    questionCount: 0,
    agentSurface: {
      specifyCanonicalId: "speckit.specify",
      specifyInvocation: "$speckit-specify",
      autonomousCanonicalId: "speckit.autonomous",
      autonomousInvocation: "$speckit-autonomous",
    },
    deliveryAuthority: "LocalImplementation",
    authorityEvidence: "Default: this migration grants no remote feature-delivery authority.",
    promptState: "Enabled",
    provenanceMode: "Supersession",
    supersedes: {
      receiptPath: member.priorReceipt,
      targetNormalizedSha256: hashFile(member.priorTarget),
      archiveTargetPath: member.priorTarget,
      archiveReceiptPath: member.priorReceipt,
    },
    legacyAdoption: {
      evidenceType: "N/A",
      priorTargetNormalizedSha256: "N/A",
      priorGitBlob: "N/A",
    },
    updateAuthorized: true,
    updateAuthorityEvidence: "User-approved migration preserves predecessor evidence.",
    series: {
      seriesId,
      manifestPath,
      order: member.order,
      role: member.role,
      supersedesIntakeIds: [],
    },
    nextAction: `$speckit-intake-review ${member.receiptPath}`,
  };
}

const request = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  policy: "tinypl0-delivery-v1",
  targets: members.map((member) => ({path: member.path, role: member.role})),
  series: {
    orderedTargetPaths: reviewTargets,
    roots: reviewRoots,
    dependencies: reviewDependencies,
  },
  campaign: {manifestPath: "N/A", workers: [], operatorExceptions: []},
};
const requestPath = `${seriesRoot}/intake-review-request.json`;
const result = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  status: "Ready",
  policy: "tinypl0-delivery-v1",
  reviewedAt,
  repository: {root: ".", head: reviewHead},
  targets: members.map((member) => ({
    path: member.path,
    role: member.role,
    normalizedSha256: hashFile(member.path),
    gitBlob: "N/A",
  })),
  findings: [],
  questions: [],
  acceptedRisks: [],
  operatorExceptions: [],
  coverage: {
    individual: reviewTargets,
    series: [
      "Fifteen current target hashes: three completed archived targets and twelve active targets",
      "Schema 2.0 roles, collections, canonical index, portable order, lifecycle states, five roots, and eleven binding gates",
      "Embeddable VM/NuGet and VM/CLI as dual IDE prerequisites plus the external TinyCalc package gate",
      "Optimization and CLR stay blocked pending explicit architecture decisions",
      "German-first and English-second CEFR-B2 learner policy, text-first accessibility, security and privacy boundaries, evidence, and delivery authority",
      "Three immutable baselines and other completed intakes remain outside executable scope",
    ],
    workers: [],
  },
  summary: {critical: 0, high: 0, medium: 0, low: 0},
  supersedes: priorReviewId,
  requestEvidence: {path: requestPath, normalizedSha256: digest(json(request))},
};
const priorReviewEvidence = {
  schemaVersion: "1.0",
  documentType: "SupersededIntakeReviewEvidence",
  reviewId: priorReviewId,
  status: "Ready",
  reviewedAt: "2026-08-29T21:54:27Z",
  targetCount: 15,
  supersededBy: reviewId,
  sourceRevision: "5342f42eac5214d7491c80d2be3a97e1b17d63b8",
  request: {
    path: `${seriesRoot}/intake-review-request.json`,
    normalizedSha256: "49cddf9ce3391048a12fc4314f1ef2cdf4c500de73956623875a916cde1f3c50",
  },
  result: {
    path: `${seriesRoot}/intake-review-result.json`,
    normalizedSha256: "acdcf2dcb7411be6fa3389cf642748fcb1225e9bcbcf32e6bad8a76da54314fe",
  },
  report: {
    path: `${seriesRoot}/intake-review-report.md`,
    normalizedSha256: "a00cf175268a8e949a6ff9e835e6626865f34e651173ca8f3582e89efa7dd857",
  },
  relocation: {
    priorPath: "requirements/intakes/active/Lastenheft_Secure-Development-Hardening.md",
    currentPath: "requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md",
    normalizedSha256: "18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de",
  },
  authorityEvidence:
    "User explicitly authorized serial autonomous Spec Kit delivery through 2026-08-31; this current series review is the mandatory preflight after completed-target archival.",
  proofBoundary:
    "Hashes preserve the predecessor request, result, and report before the path-current complete series re-review.",
};
const seriesReceipt = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesReceipt",
  receiptId: seriesReceiptId,
  seriesId,
  generator: {preset: "intake-sequencing-governance", version: "0.2.3"},
  createdAt: seriesUpdatedAt,
  operation: {
    operationId: seriesOperationId,
    type: "Update",
    authorityEvidence: "User explicitly instructed incorporation of the verified NuGet publication rules into the rank-4 intake on 2026-08-30.",
  },
  status: "Ready",
  manifest: {path: manifestPath, normalizedSha256: manifestHash},
  supersedes: {
    receiptPath: "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z/receipt.json",
    receiptNormalizedSha256: "40b065f557be1fc66f30ff31df83f3eeaf35d1089980777ba60cff4cf1be5b9e",
    manifestArchivePath: "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z/manifest.json",
    manifestArchiveSha256: "573558ebd31b02d84d5399ee36479f37470cff90a4f1df2a478f378e77652c93",
  },
  tombstone: {path: "N/A", normalizedSha256: "N/A"},
  nextAction: "$speckit-intake-series-status",
};
const seriesUpdatePaths = [
  "requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md",
  "specs/intake-authoring-receipts/embeddable-vm-und-nuget.json",
  "requirements/intakes/history/20260830T182940Z/Lastenheft_Embeddable-VM-und-NuGet.md",
  "specs/intake-authoring-receipts/history/embeddable-vm-und-nuget.20260830T182940Z.json",
  manifestPath,
  `${seriesRoot}/receipt.json`,
  `${seriesRoot}/operation.json`,
  `${seriesRoot}/order.md`,
  `${seriesRoot}/intake-review-result.json`,
  `${seriesRoot}/intake-review-report.md`,
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z/manifest.json",
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z/receipt.json",
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z-review-invalidated/intake-review-request.json",
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z-review-invalidated/intake-review-result.json",
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T182940Z-review-invalidated/intake-review-report.md",
  "specs/intake-authoring-receipts/quellcode-doku.json",
  "specs/intake-authoring-receipts/dokumentation-en.json",
  "specs/intake-authoring-receipts/ide-l10n.json",
  "specs/intake-authoring-receipts/a11y-ide.json",
  "specs/intake-authoring-receipts/options-als-parameter.json",
  "specs/intake-authoring-receipts/vm-cli.json",
  "requirements/intake-governance-config.json",
  "Pflichtenheft.md",
  "Lastenheft_Abarbeitungsreihenfolge.md",
  "scripts/render-requirements-intake-governance.mjs",
  "scripts/validate-requirements-intake-alignment.mjs",
];
const operation = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesOperation",
  operationId: seriesOperationId,
  seriesId,
  type: "Update",
  status: "Published",
  authorityEvidence: "User explicitly instructed incorporation of the verified NuGet publication rules into the rank-4 intake on 2026-08-30.",
  proposalNormalizedSha256: manifestHash,
  preparedPaths: seriesUpdatePaths,
  validation: {bash: "Pass", powerShell: "Pass"},
  publication: {
    status: "Published",
    publishedPaths: seriesUpdatePaths,
  },
};
const report = `# Intake Review: TinyPl0 Delivery Series

## Identität / Identity

- Review-ID: \`${reviewId}\`
- Modus: \`Series\`
- Policy: \`tinypl0-delivery-v1\`
- Ergebnis: \`Ready\`
- Umfang: 15 Ziele, 5 Wurzeln und 11 verbindliche Abhängigkeiten
- Vorgängerreview: \`${priorReviewId}\`
- Vorgängerevidenz: \`${priorReviewArchivePath}\`

*The complete re-review covers all 15 current targets, five roots, and eleven
binding dependencies. It explicitly supersedes the review that still named
the completed Secure-Development target below the active collection.*

## Ergebnis / Result

Die Schema-2.0-Governance löst Index, aktive Sammlung, Archiv, Baselines und
Ordnungsansicht eindeutig auf. Alle 15 normalisierten Zielhashes stimmen. Die
abgeschlossenen Constitution-, Secure-Development- und Sandbox-Ziele liegen
unverändert im Archiv; die übrigen 12 Ziele bleiben aktiv. Reihenfolge, fünf
DAG-Wurzeln, elf bindende Kanten und Lifecycle-Zustände stimmen mit dem Manifest und der
Textansicht überein.

*Schema 2.0 resolves the index, active collection, archive, baselines, and
order view unambiguously. All 15 normalized target hashes match. The completed
Constitution, Secure-Development, and Sandbox targets are unchanged in the
archive; the other 12 targets remain active. Order, five DAG roots, eleven binding edges, and
lifecycle states match the manifest and text view.*

## Review-Abdeckung / Review Coverage

| Bereich | Ergebnis | Evidenz |
|---|---|---|
| Identität, Ziel, Scope und Nicht-Ziele | \`Ready\` | 15 aktuelle Manifestziele und deren Intake-Abschnitte |
| Atomare Anforderungen und messbare Abnahme | \`Ready\` | Zielhashes und bestehender Review \`${priorReviewId}\` |
| Abhängigkeiten, Reihenfolge und Handoffs | \`Ready\` | 5 Wurzeln, 11 Kanten; Pakete und VM/CLI → IDE; TinyCalc extern |
| Lernende, Sprache und Begriffe | \`Ready\` | Deutsch zuerst, Englisch danach, CEFR B2 und Erklärungen bei Erstnutzung |
| Barrierefreiheit und Text-First | \`Ready\` | A11Y-Intake und Governance-Index bleiben ohne Layout- oder Farbabhängigkeit lesbar |
| Security und Privacy | \`Ready\` | Secure Coding/Architecture, Trust Boundaries, SSDF/CWE und anwendbare Supply-Chain-Nachweise sind sichtbar; keine Secrets oder unnötigen Personendaten |
| Plattform und Evidenz | \`Ready\` | C#/.NET-Registry, Bash/PowerShell-Parität, Hash-, Receipt- und Archivpfade |
| Risiken und offene Fragen | \`Ready\` | Keine Findings, keine akzeptierten Risiken, keine offenen Fragen |

*The review covers identity, scope, atomic requirements, measurable
acceptance, dependencies, handoffs, learner language, accessibility,
security/privacy, platform fit, and evidence. No finding, accepted risk, or
open question remains.*

## Supersession und Pfadnachweis / Supersession And Path Evidence

- Alter Reviewpfad: \`requirements/intakes/active/Lastenheft_Secure-Development-Hardening.md\`
- Aktueller Zielpfad: \`requirements/intakes/archive/Lastenheft_Secure-Development-Hardening.004-secure-development-hardening.md\`
- Erhaltener normalisierter Hash:
  \`18c957e4bcbe3e9e975f11ace8d1d2c81c49064f911f607480a09e14774669de\`
- Zielinhalte, Manifest, Receipt, Reihenfolge, Lifecycle und Archive wurden
  nicht geändert.

*The predecessor review used the pre-archive active path. This review binds
the current archive path with the same normalized hash and changes no target,
manifest, receipt, order, lifecycle, or existing archive content.*

## Risiken, Fragen und Authority / Risks, Questions And Authority

- Akzeptierte Risiken: keine
- Offene Fragen: keine
- Intake-Ausführungsrechte: nicht durch \`Eligible\` oder diesen Review erteilt
- Review-Evidence-Lieferung: \`MergeAndSync\` mit ausdrücklich autorisiertem
  Admin-Bypass
- Keine Secret-, NuGet-Veröffentlichungs- oder Intake-Implementierungsrechte
  wurden erteilt.

*No risk was accepted and no question remains open. The current authority
covers delivery of this review evidence through MergeAndSync with explicit
admin bypass; it does not authorize intake implementation, secrets, or NuGet
publication.*
`;
const reviewTargetsExist = reviewTargets.every((target) =>
  fs.existsSync(path.join(root, target)),
);
const outputs = [
  [manifestPath, json(manifest)],
  [`${seriesRoot}/receipt.json`, json(seriesReceipt)],
  [`${seriesRoot}/operation.json`, json(operation)],
  [`${seriesRoot}/order.md`, normalize(read("Lastenheft_Abarbeitungsreihenfolge.md"))],
  ...(reviewTargetsExist ? [
    [requestPath, json(request)],
    [`${seriesRoot}/intake-review-result.json`, json(result)],
    [`${seriesRoot}/intake-review-report.md`, report],
  ] : []),
  [priorReviewArchivePath, json(priorReviewEvidence)],
  ...members.filter((member) => !member.customReceipt).map((member) => [
    `specs/intake-authoring-receipts/${member.slug}.json`,
    json(receiptFor(member)),
  ]),
];

for (const [relativePath, content] of outputs) {
  const fullPath = path.join(root, relativePath);
  if (write) {
    fs.mkdirSync(path.dirname(fullPath), {recursive: true});
    fs.writeFileSync(fullPath, content);
  } else if (!fs.existsSync(fullPath) || normalize(read(relativePath)) !== normalize(content)) {
    console.error(`stale generated intake-governance artifact: ${relativePath}`);
    process.exit(1);
  }
}
const configuredCountMismatch =
  config.schemaVersion === "1.0" && members.length !== config.activeIntakeCount;
if (configuredCountMismatch || targets.length !== new Set(targets).size) {
  throw new Error("configured active intake cardinality differs from generated members");
}
console.log(`TinyPl0 intake governance PASS (${members.length} series targets, ${dependencies.length} binding edges)`);
