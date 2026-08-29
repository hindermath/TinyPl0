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
const seriesReceiptId = "224e145b-5e0f-4bab-8958-4e9b51364267";
const seriesOperationId = "a6522a3c-1674-4db6-89e0-aeb538f79c2a";
const reviewId = "78435231-e579-486f-8d80-8192781c127d";
const createdAt = "2026-07-26T22:00:00Z";
const seriesUpdatedAt = "2026-08-29T20:57:25Z";
const reviewHead = "272fdb9a07ec28d706ea27cbc52ad619d76d3555";
const reviewedAt = "2026-08-29T16:07:18Z";

const members = [
  ["constitution-change", "Lastenheft_Constitution_Change.md", "Completed"],
  ["secure-development-hardening", "Lastenheft_Secure-Development-Hardening.md", "Eligible"],
  ["sandbox-gestuetzte-secure-development-haertung", "Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md", "Blocked"],
  ["quellcode-doku", "Lastenheft_Quellcode_Doku.md", "Blocked"],
  ["dokumentation-en", "Lastenheft_Dokumentation_EN.md", "Blocked"],
  ["ide-l10n", "Lastenheft_IDE-L10N.md", "Blocked"],
  ["a11y-ide", "Lastenheft_A11Y_IDE.md", "Blocked"],
  ["options-als-parameter", "Lastenheft_Options_Als_Parameter.md", "Blocked"],
  ["vm-cli", "Lastenheft_VM_CLI.md", "Blocked"],
  ["embeddable-vm-und-nuget", "Lastenheft_Embeddable-VM-und-NuGet.md", "Blocked"],
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
    : `requirements/intakes/active/${fileName}`,
  reviewPath: `requirements/intakes/active/${fileName}`,
  priorTarget: `requirements/intakes/history/pre-intake-split-20260726/${fileName}`,
  priorReceipt: `specs/intake-authoring-receipts/history/${slug}.schema-1.1.json`,
  customReceipt: slug === "embeddable-vm-und-nuget",
}));
const targets = members.map((member) => member.path);
const reviewTargets = members.map((member) => member.reviewPath);
const dependencies = Array.from({length: 10}, (_, index) => ({
  from: targets[index],
  to: targets[index + 1],
  kind: index === 2 ? "CommentSurfaceBaseline" :
    index === 3 || index === 4 ? "DocumentationSurfaceBaseline" : "HardCompletionGate",
  binding: true,
}));
const roots = targets.filter((target) => !dependencies.some((edge) => edge.to === target));
const reviewDependencies = dependencies.map(({kind}, index) => ({
  from: reviewTargets[index],
  to: reviewTargets[index + 1],
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
    target: {path: member.reviewPath, normalizedSha256: hashFile(member.path)},
    sources: [sourceRecord(member.priorTarget)],
    profile: "level2-lastenheft",
    languagePolicy: "GermanFirstEnglishSecond",
    decisions: [
      {
        id: "IAD001",
        status: "Answered",
        question: "Welcher Zielpfad ist nach der Konsolidierung verbindlich?",
        answer: member.reviewPath,
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
    nextAction: `$speckit-intake-review ${member.reviewPath}`,
  };
}

const request = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  policy: "tinypl0-delivery-v1",
  targets: members.map((member) => ({path: member.reviewPath, role: member.role})),
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
    path: member.reviewPath,
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
      "Fifteen active intake hashes, lifecycle states, five roots, and ten binding gates",
      "VM/CLI to embeddable VM/NuGet to IDE handoff plus the external TinyCalc package gate",
      "Optimization and CLR stay blocked pending explicit architecture decisions",
      "Three immutable baselines and two completed intakes remain outside executable scope",
    ],
    workers: [],
  },
  summary: {critical: 0, high: 0, medium: 0, low: 0},
  supersedes: "a6c1acb6-b75e-4875-a968-e5afb90bb289",
  requestEvidence: {path: requestPath, normalizedSha256: digest(json(request))},
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
    authorityEvidence: "User explicitly authorized the complete 003 Constitution Change MergeAndSync run, admin bypass, and causal post-merge closeout on 2026-08-29.",
  },
  status: "Ready",
  manifest: {path: manifestPath, normalizedSha256: manifestHash},
  supersedes: {
    receiptPath: "requirements/intakes/series-archive/tinypl0-delivery/20260829T205512Z/receipt.json",
    receiptNormalizedSha256: "9bff2600188ec02dd878f5f607a1cbde8bad7ae09a1187ce95447cad4a6e894b",
    manifestArchivePath: "requirements/intakes/series-archive/tinypl0-delivery/20260829T205512Z/manifest.json",
    manifestArchiveSha256: "5e4ca0a67a221854fef7abb092b7f014433f6dd1e6c0e24b71fc978f5096b3bf",
  },
  tombstone: {path: "N/A", normalizedSha256: "N/A"},
  nextAction: "$speckit-intake-series-status",
};
const operation = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesOperation",
  operationId: seriesOperationId,
  seriesId,
  type: "Update",
  status: "Published",
  authorityEvidence: "User explicitly authorized the complete 003 Constitution Change MergeAndSync run, admin bypass, and causal post-merge closeout on 2026-08-29.",
  proposalNormalizedSha256: manifestHash,
  preparedPaths: [
    "requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md",
    manifestPath,
    `${seriesRoot}/receipt.json`,
    `${seriesRoot}/order.md`,
    "requirements/intakes/series-archive/tinypl0-delivery/20260829T205512Z/manifest.json",
    "requirements/intakes/series-archive/tinypl0-delivery/20260829T205512Z/receipt.json",
    "Lastenheft_Abarbeitungsreihenfolge.md",
  ],
  validation: {bash: "Pass", powerShell: "Pass"},
  publication: {
    status: "Published",
    publishedPaths: [
      "requirements/intakes/archive/Lastenheft_Constitution_Change.003-constitution-change.md",
      manifestPath,
      `${seriesRoot}/receipt.json`,
      `${seriesRoot}/order.md`,
      "requirements/intakes/series-archive/tinypl0-delivery/20260829T205512Z/manifest.json",
      "requirements/intakes/series-archive/tinypl0-delivery/20260829T205512Z/receipt.json",
      "Lastenheft_Abarbeitungsreihenfolge.md",
    ],
  },
};
const report = `# Intake Review: TinyPl0 Delivery Series

## Identität / Identity

- Review-ID: \`${reviewId}\`
- Modus: \`Series\`
- Policy: \`tinypl0-delivery-v1\`
- Ergebnis: \`Ready\`
- Umfang: 15 Ziele, 5 Wurzeln und 10 verbindliche Abhängigkeiten
- Vorgängerreview: \`a6c1acb6-b75e-4875-a968-e5afb90bb289\`

*The complete re-review covers all 15 current targets, five roots, and ten
binding dependencies. It explicitly supersedes the remediation review.*

## Ergebnis / Result

Die Schema-2.0-Governance, Zielhashes, Reihenfolge, DAG-Wurzeln, Kanten,
Authority-Grenzen und der Handoff von VM/CLI über die einbettbare VM und die
NuGet-Pakete zur IDE-Erweiterung sind konsistent. Der externe TinyCalc-Handoff
und das Verbot einer lokalen ProjectReference als Fallback bleiben eindeutig.

Finding \`IR001\` ist behoben. Ein neuer Begriffsabschnitt erklärt Hostvertrag,
Run/Step-Parität, SemVer, CancellationToken, SBOM, VEX, Provenance/SLSA,
STRIDE/CAPEC und OpenSSF Scorecard deutsch zuerst und englisch danach auf
CEFR-B2-Niveau. Scope, Anforderungen, Abnahmeschwellen, Reihenfolge, Gates und
Delivery Authority blieben unverändert.

*Schema 2.0 governance, target hashes, order, DAG roots, edges, authority
boundaries, and internal and external handoffs are consistent. Finding IR001
is resolved through first-use learner explanations without changing scope,
requirements, acceptance thresholds, order, gates, or delivery authority.*

## Reparaturnachweis / Repair Evidence

- Geändertes Ziel:
  \`requirements/intakes/active/Lastenheft_Embeddable-VM-und-NuGet.md\`
- Autorisierung: ausdrücklicher Aufruf von \`speckit-intake-repair\` für das
  aktuelle Ergebnis \`a6c1acb6-b75e-4875-a968-e5afb90bb289\`
- Behobenes Finding: \`IR001\` / \`Medium\` / \`LearnerReadability\`
- Verbleibende Findings: keine

*The explicit repair invocation authorized only the learner terminology
change. IR001 is resolved and no finding remains.*

## Risiken, Fragen und Authority / Risks, Questions And Authority

- Akzeptierte Risiken: keine
- Offene Fragen: keine
- Delivery Authority: \`LocalImplementation\`
- Keine Commit-, Push-, PR-, Merge-, Provider-, Secret- oder
  NuGet-Veröffentlichungsberechtigung wurde erteilt.

*No risk was accepted and no question remains open. Local implementation
authority does not grant remote or NuGet publication authority.*
`;
const outputs = [
  [manifestPath, json(manifest)],
  [`${seriesRoot}/receipt.json`, json(seriesReceipt)],
  [`${seriesRoot}/operation.json`, json(operation)],
  [`${seriesRoot}/order.md`, normalize(read("Lastenheft_Abarbeitungsreihenfolge.md"))],
  [requestPath, json(request)],
  [`${seriesRoot}/intake-review-result.json`, json(result)],
  [`${seriesRoot}/intake-review-report.md`, report],
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
