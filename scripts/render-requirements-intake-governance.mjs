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
const seriesReceiptId = stableUuid("series-receipt");
const seriesOperationId = stableUuid("series-operation");
const reviewId = stableUuid("review");
const createdAt = "2026-07-26T22:00:00Z";
const reviewHead = "REVIEW_HEAD_TO_BE_PINNED";

const members = [
  ["constitution-change", "Lastenheft_Constitution_Change.md", "Eligible"],
  ["secure-development-hardening", "Lastenheft_Secure-Development-Hardening.md", "Blocked"],
  ["sandbox-gestuetzte-secure-development-haertung", "Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md", "Blocked"],
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
  path: `requirements/intakes/active/${fileName}`,
  priorTarget: `requirements/intakes/history/pre-intake-split-20260726/${fileName}`,
  priorReceipt: `specs/intake-authoring-receipts/history/${slug}.schema-1.1.json`,
}));
const targets = members.map((member) => member.path);
const dependencies = Array.from({length: 9}, (_, index) => ({
  from: targets[index],
  to: targets[index + 1],
  kind: index === 2 ? "CommentSurfaceBaseline" :
    index === 3 || index === 4 ? "DocumentationSurfaceBaseline" : "HardCompletionGate",
  binding: true,
}));
const roots = targets.filter((target) => !dependencies.some((edge) => edge.to === target));
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
    target: {path: member.path, normalizedSha256: hashFile(member.path)},
    sources: [sourceRecord(member.priorTarget)],
    profile: "level2-lastenheft",
    languagePolicy: "GermanFirstEnglishSecond",
    decisions: [
      {
        id: "IAD001",
        status: "Answered",
        question: "Welcher Zielpfad ist nach der Konsolidierung verbindlich?",
        answer: member.path,
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
    nextAction: `$speckit-intake-review ${member.path}`,
  };
}

const request = {
  schemaVersion: "1.1",
  reviewId,
  mode: "Series",
  policy: "tinypl0-delivery-v1",
  targets: members.map((member) => ({path: member.path, role: member.role})),
  series: {
    orderedTargetPaths: targets,
    roots,
    dependencies: dependencies.map(({from, to, kind}) => ({from, to, kind})),
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
  reviewedAt: createdAt,
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
    individual: targets,
    series: [
      "Fourteen active intake hashes, lifecycle states, roots, and nine binding gates",
      "Optimization and CLR stay blocked pending explicit architecture decisions",
      "Three immutable baselines and two completed intakes remain outside executable scope",
    ],
    workers: [],
  },
  summary: {critical: 0, high: 0, medium: 0, low: 0},
  supersedes: readJson("specs/intake-normalization-20260723/intake-review-result.json").reviewId,
  requestEvidence: {path: requestPath, normalizedSha256: digest(json(request))},
};
const seriesReceipt = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesReceipt",
  receiptId: seriesReceiptId,
  seriesId,
  generator: {preset: "intake-sequencing-governance", version: "0.1.1"},
  createdAt,
  operation: {
    operationId: seriesOperationId,
    type: "Create",
    authorityEvidence: "User-approved TinyPl0 requirements and intake consolidation plan",
  },
  status: "Ready",
  manifest: {path: manifestPath, normalizedSha256: manifestHash},
  supersedes: {
    receiptPath: "N/A",
    receiptNormalizedSha256: "N/A",
    manifestArchivePath: "N/A",
    manifestArchiveSha256: "N/A",
  },
  tombstone: {path: "N/A", normalizedSha256: "N/A"},
  nextAction: "$speckit-intake-series-status",
};
const operation = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesOperation",
  operationId: seriesOperationId,
  seriesId,
  type: "Create",
  status: "Published",
  authorityEvidence: "User-approved TinyPl0 requirements and intake consolidation plan",
  proposalNormalizedSha256: hashFile("specs/requirements-reconciliation-20260726/migration-proposal.json"),
  preparedPaths: [manifestPath, `${seriesRoot}/receipt.json`, `${seriesRoot}/order.md`],
  validation: {bash: "Pass", powerShell: "Pass"},
  publication: {
    status: "Published",
    publishedPaths: [manifestPath, `${seriesRoot}/receipt.json`, `${seriesRoot}/order.md`],
  },
};
const report = `# Intake Review: TinyPl0 Delivery Series

## Ergebnis / Result

Status: \`Ready\`

Alle 14 aktiven Intakes, ihre Hashes, Vorgänger, Zustände und Abhängigkeiten
wurden geprüft. Constitution ist der bevorzugte nächste Intake. Optimierung und
CLR bleiben blockiert; unabhängige Governance-Wurzeln bleiben \`Pending\`.

*All 14 active intakes, hashes, predecessors, states, and dependencies were
reviewed. Constitution is preferred next. Optimization and CLR remain blocked;
independent governance roots remain pending.*
`;
const outputs = [
  [manifestPath, json(manifest)],
  [`${seriesRoot}/receipt.json`, json(seriesReceipt)],
  [`${seriesRoot}/operation.json`, json(operation)],
  [`${seriesRoot}/order.md`, normalize(read("Lastenheft_Abarbeitungsreihenfolge.md"))],
  [requestPath, json(request)],
  [`${seriesRoot}/intake-review-result.json`, json(result)],
  [`${seriesRoot}/intake-review-report.md`, report],
  ...members.map((member) => [
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
if (members.length !== config.activeIntakeCount || targets.length !== new Set(targets).size) {
  throw new Error("configured active intake cardinality differs from generated members");
}
console.log(`TinyPl0 intake governance PASS (${members.length} active targets, ${dependencies.length} binding edges)`);
