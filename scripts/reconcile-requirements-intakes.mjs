#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";

const root = process.cwd();
const outputRoot = "specs/requirements-reconciliation-20260726";
const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const hash = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");
const hashFile = (relativePath) => hash(fs.readFileSync(path.join(root, relativePath), "utf8"));
const migratedSourcePaths = new Map([
  ["Pflichtenheft_PL0_CSharp_DotNet10.md", "requirements/baseline/Pflichtenheft_PL0_CSharp_DotNet10.pre-intake-split.2026-07-26.md"],
  ["Pflichtenheft_IDE.md", "requirements/baseline/Pflichtenheft_IDE.pre-intake-split.2026-07-26.md"],
  ["Pflichtenheft_PL0_Dokumentation.md", "requirements/baseline/Pflichtenheft_PL0_Dokumentation.pre-intake-split.2026-07-26.md"],
  ["Lastenheft_Abarbeitungsreihenfolge.md", "requirements/intakes/history/pre-intake-split-20260726/Lastenheft_Abarbeitungsreihenfolge.root.md"],
  ["Lastenheft_L10N.001-l10n-backend.md", "requirements/intakes/archive/Lastenheft_L10N.001-l10n-backend.md"],
  ["Lastenheft_VM_INC_OpCode.002-vm-inc-compat.md", "requirements/intakes/archive/Lastenheft_VM_INC_OpCode.002-vm-inc-compat.md"],
]);
const sourceFile = (relativePath) => {
  if (migratedSourcePaths.has(relativePath)) return migratedSourcePaths.get(relativePath);
  if (fs.existsSync(path.join(root, relativePath))) return relativePath;
  if (relativePath.startsWith("Lastenheft_")) {
    return `requirements/intakes/history/pre-intake-split-20260726/${relativePath}`;
  }
  return relativePath;
};
const hashSource = (relativePath) => hashFile(sourceFile(relativePath));
const json = (value) => `${JSON.stringify(value, null, 2)}\n`;

const intakes = [
  ["TP-CONSTITUTION", "Lastenheft_Constitution_Change.md", "PartiallySatisfied", "Governance"],
  ["TP-SECURITY", "Lastenheft_Secure-Development-Hardening.md", "Open", "Security"],
  ["TP-SANDBOX", "Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md", "Open", "Security"],
  ["TP-COMMENTS", "Lastenheft_Quellcode_Doku.md", "Open", "Maintainability"],
  ["TP-DOC-EN", "Lastenheft_Dokumentation_EN.md", "Open", "Documentation"],
  ["TP-IDE-L10N", "Lastenheft_IDE-L10N.md", "Open", "IDE"],
  ["TP-IDE-A11Y", "Lastenheft_A11Y_IDE.md", "Open", "A11Y"],
  ["TP-OPTIONS", "Lastenheft_Options_Als_Parameter.md", "Open", "Compiler"],
  ["TP-VM-CLI", "Lastenheft_VM_CLI.md", "Open", "VM"],
  ["TP-IDE-PASM", "Lastenheft_IDE-Erweiterung-Pl0Ide_PAsm_PCod.md", "Open", "IDE"],
  ["TP-OPTIMIZATION", "Lastenheft_PL0_Optimierung.md", "Blocked", "Architecture"],
  ["TP-CLR", "Lastenheft_CLR_Assembly.md", "Blocked", "Architecture"],
  ["TP-RLSE", "Lastenheft_RL-SE-Checklist-Selbstpruefung.md", "Open", "Governance"],
  ["TP-GSDB", "Lastenheft_GSDB-Spec-Kit-Intensivpruefung.md", "Open", "Governance"],
].map(([id, intakePath, status, owner], index) => ({
  id,
  path: intakePath,
  status,
  owner,
  order: index + 1,
}));

const evidenceFor = (item) => {
  if (item.id === "TP-CONSTITUTION") return ["constitution.md", ".specify/memory/constitution.md", "AGENTS.md"];
  if (item.owner === "IDE" || item.owner === "A11Y") return ["src/Pl0.Ide", "tests/Pl0.Tests", "docs"];
  if (item.owner === "Security" || item.owner === "Governance") return ["docs/security", ".github/workflows", ".specify"];
  if (item.owner === "Architecture") return ["AGENTS.md", "docs/ARCHITECTURE.md"];
  return ["src", "tests", "docs"];
};

const requirements = intakes.map((item) => ({
  requirementId: `TP-RQ-${String(item.order).padStart(3, "0")}`,
  sourceId: item.id,
  sourcePath: item.path,
  sourceNormalizedSha256: hashSource(item.path),
  status: item.status,
  evidencePaths: evidenceFor(item),
  proposedOwnerGroup: item.owner,
  rationale: item.status === "Blocked"
    ? "The current repository rules explicitly prohibit this product direction until an architecture decision changes the boundary."
    : item.status === "PartiallySatisfied"
      ? "Relevant repository governance exists, but the dedicated intake has no closure decision."
      : "The intake is prepared and reviewed, but no dedicated implementation or closure feature is recorded.",
  residualRisk: "The complete intake acceptance criteria remain unverified until a dedicated closure decision exists.",
  reevaluationTrigger: `Before executing or archiving ${item.path}`,
}));

const baselines = [
  "Pflichtenheft_PL0_CSharp_DotNet10.md",
  "Pflichtenheft_IDE.md",
  "Pflichtenheft_PL0_Dokumentation.md",
].map((baselinePath, index) => ({
  sourceId: `TP-BASELINE-${index + 1}`,
  path: baselinePath,
  normalizedSha256: hashSource(baselinePath),
  role: "HistoricalProductBaseline",
}));
const referenceIntakes = [
  "Lastenheft_PL0_CSharp_DotNet10.md",
  "Lastenheft_IDE.md",
  "Lastenheft_PL0_Dokumentation.md",
].map((referencePath) => ({
  path: referencePath,
  normalizedSha256: hashSource(referencePath),
  role: "HistoricalReferenceIntake",
}));
const completed = [
  "Lastenheft_L10N.001-l10n-backend.md",
  "Lastenheft_VM_INC_OpCode.002-vm-inc-compat.md",
].map((completedPath) => ({
  path: completedPath,
  normalizedSha256: hashSource(completedPath),
  role: "CompletedIntake",
}));

const coverage = {
  schemaVersion: "1.0",
  documentType: "RequirementsReconciliation",
  repository: "hindermath/TinyPl0",
  reviewedAt: "2026-07-26",
  sources: [
    ...intakes.map((item) => ({
      sourceId: item.id,
      path: item.path,
      normalizedSha256: hashSource(item.path),
      role: "ActiveIntakeCandidate",
    })),
    ...baselines,
    ...referenceIntakes,
    ...completed,
    {
      sourceId: "TP-ORDER",
      path: "Lastenheft_Abarbeitungsreihenfolge.md",
      normalizedSha256: hashSource("Lastenheft_Abarbeitungsreihenfolge.md"),
      role: "CuratedOrder",
    },
  ],
  summary: {
    total: requirements.length,
    PartiallySatisfied: requirements.filter((item) => item.status === "PartiallySatisfied").length,
    Open: requirements.filter((item) => item.status === "Open").length,
    Blocked: requirements.filter((item) => item.status === "Blocked").length,
  },
  requirements,
};

const activePaths = intakes.map((item) => item.path);
const proposal = {
  schemaVersion: "1.0",
  documentType: "RequirementsIntakeMigrationProposal",
  repository: "hindermath/TinyPl0",
  canonicalIndex: "Pflichtenheft.md",
  baselineMoves: baselines.map((baseline) => ({
    from: baseline.path,
    to: `requirements/baseline/${path.basename(baseline.path, ".md")}.pre-intake-split.2026-07-26.md`,
    mode: "ByteIdentical",
  })),
  activeIntakes: activePaths.map((sourcePath) => ({
    sourcePath,
    targetPath: `requirements/intakes/active/${path.basename(sourcePath)}`,
    mode: "SupersedingCopy",
  })),
  archivedIntakes: completed.map((item) => ({
    sourcePath: item.path,
    targetPath: `requirements/intakes/archive/${path.basename(item.path)}`,
    mode: "ByteIdentical",
  })),
  historicalReferenceIntakes: referenceIntakes.map((item) => ({
    sourcePath: item.path,
    targetPath: `requirements/intakes/history/pre-intake-split-20260726/${path.basename(item.path)}`,
    mode: "ByteIdentical",
  })),
  ideWorklog: {
    source: "Pflichtenheft_IDE.md",
    target: "docs/ide-worklog.md",
    rule: "Freeze the baseline byte-identically and append future operational entries only to the separate worklog.",
  },
  canonicalSeries: {
    path: "requirements/intakes/series/tinypl0-delivery/manifest.json",
    preferredNext: "Lastenheft_Constitution_Change.md",
    orderedTargets: activePaths,
    blockedArchitectureTargets: ["Lastenheft_PL0_Optimierung.md", "Lastenheft_CLR_Assembly.md"],
  },
  constraints: [
    "No Spec Kit feature is started.",
    "No product, API, dependency, project, or runtime behavior changes.",
    "Historical specifications and hash-bound evidence remain unchanged.",
    "The three Pflichtenhefte become immutable baselines; future IDE activity moves to a separate worklog.",
  ],
};

const report = `# TinyPl0 Requirements and Intake Reconciliation

## Ergebnis / Result

Vierzehn aktive Intake-Kandidaten wurden gegen Repository, bestehende Receipts
und die kuratierte Reihenfolge geprüft. Elf bleiben offen, Constitution ist
teilweise erfüllt, und Optimierung sowie CLR bleiben durch die geltenden
Architekturregeln blockiert. Zwei branch-suffigierte Intakes sind abgeschlossen.

*Fourteen active intake candidates were checked against the repository,
existing receipts, and curated order. Eleven remain open, Constitution is
partially satisfied, and optimization plus CLR remain blocked by current
architecture rules. Two branch-suffixed intakes are complete.*

Die drei Pflichtenhefte sind historische Baselines. Insbesondere
\`Pflichtenheft_IDE.md\` vermischt die normative Basis mit 147 operativen
Agent-Einträgen. Die Migration soll die Datei unverändert einfrieren und
künftige Einträge in \`docs/ide-worklog.md\` führen.

## Grenzen / Boundaries

- Dieser Audit verschiebt oder ändert keine Anforderungen.
- Es wird kein Spec-Kit-Feature gestartet.
- Die Strukturmigration benötigt einen eigenen Folge-PR.
`;

const outputs = [
  [`${outputRoot}/requirements-coverage.json`, json(coverage)],
  [`${outputRoot}/migration-proposal.json`, json(proposal)],
  [`${outputRoot}/reconciliation-report.md`, report],
];
for (const [relativePath, content] of outputs) {
  const fullPath = path.join(root, relativePath);
  fs.mkdirSync(path.dirname(fullPath), {recursive: true});
  fs.writeFileSync(fullPath, content);
}

console.log(`TinyPl0 reconciliation PASS (${requirements.length} active intake decisions)`);
