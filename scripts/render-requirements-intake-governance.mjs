#!/usr/bin/env node

import crypto from "node:crypto";
import fs from "node:fs";
import path from "node:path";
import process from "node:process";
import {fileURLToPath} from "node:url";

class LinkedIntakeError extends Error {
  constructor(code, message) {
    super(`${code}: ${message}`);
    this.name = "LinkedIntakeError";
    this.code = code;
  }
}

const lieFail = (code, message) => {
  throw new LinkedIntakeError(code, message);
};
const lieNormalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const lieDigest = (value) =>
  crypto.createHash("sha256").update(lieNormalize(value)).digest("hex");
const lieStrictDecoder = new TextDecoder("utf-8", {fatal: true});

function lieReadText(absolutePath, subject) {
  let text;
  try {
    text = lieStrictDecoder.decode(fs.readFileSync(absolutePath));
  } catch {
    lieFail("LIE001", `input is not valid UTF-8: ${subject}`);
  }
  if (text.includes("\0")) lieFail("LIE001", `input contains NUL: ${subject}`);
  return lieNormalize(text);
}

function lieReadJson(absolutePath, subject) {
  try {
    return JSON.parse(lieReadText(absolutePath, subject));
  } catch (error) {
    if (error instanceof LinkedIntakeError) throw error;
    lieFail("LIE002", `JSON is invalid or incomplete: ${subject}`);
  }
}

function lieSafeRelative(relativePath, code = "LIE003") {
  if (typeof relativePath !== "string" || relativePath.length === 0 ||
      /[\u0000-\u001f\u007f]/u.test(relativePath) ||
      relativePath.includes("\\") || relativePath.startsWith("/") ||
      /^\/?[A-Za-z]:\//.test(relativePath) || relativePath.startsWith("//")) {
    lieFail(code, "path is not safely repository-relative");
  }
  const parts = relativePath.split("/");
  if (parts.some((part) => part === "" || part === "." || part === ".." ||
      part.startsWith("-"))) {
    lieFail(code, "path is not safely repository-relative");
  }
  const normalized = path.posix.normalize(relativePath);
  if (normalized !== relativePath) lieFail(code, "path normalization changed the input");
  return normalized;
}

function lieInsideRoot(rootPath, candidatePath, code) {
  const relative = path.relative(rootPath, candidatePath);
  if (relative === "" || (!relative.startsWith(`..${path.sep}`) && relative !== ".." &&
      !path.isAbsolute(relative))) {
    return;
  }
  lieFail(code, "resolved path leaves the repository");
}

function lieResolveExisting(rootPath, relativePath) {
  const safe = lieSafeRelative(relativePath);
  const absolute = path.join(rootPath, ...safe.split("/"));
  if (!fs.existsSync(absolute) || !fs.statSync(absolute).isFile()) {
    lieFail("LIE004", `target is missing or is not a regular file: ${safe}`);
  }
  let resolved;
  try {
    resolved = fs.realpathSync(absolute);
  } catch {
    lieFail("LIE004", `target cannot be resolved: ${safe}`);
  }
  lieInsideRoot(rootPath, resolved, "LIE005");
  return {safe, absolute, resolved};
}

function lieResolveOutput(rootPath, relativePath) {
  const safe = lieSafeRelative(relativePath);
  const absolute = path.join(rootPath, ...safe.split("/"));
  if (fs.existsSync(absolute)) {
    const entry = fs.lstatSync(absolute);
    if (entry.isSymbolicLink() || !entry.isFile()) {
      lieFail("LIE005", `output is a symlink or not a regular file: ${safe}`);
    }
    lieInsideRoot(rootPath, fs.realpathSync(absolute), "LIE005");
  }
  let ancestor = path.dirname(absolute);
  while (!fs.existsSync(ancestor) && ancestor !== path.dirname(ancestor)) {
    ancestor = path.dirname(ancestor);
  }
  lieInsideRoot(rootPath, fs.realpathSync(ancestor), "LIE005");
  return {safe, absolute};
}

function lieEscapeLabel(value) {
  if (/[\u0000-\u001f\u007f]/u.test(value)) {
    lieFail("LIE002", "display value contains control characters");
  }
  return value
    .replaceAll("\\", "\\\\")
    .replaceAll("|", "\\|")
    .replaceAll("[", "\\[")
    .replaceAll("]", "\\]");
}

function lieEncodeDestination(value, directory = false) {
  const encoded = value.split("/").map((part) =>
    part === "." || part === ".." ? part : encodeURIComponent(part)).join("/");
  return directory && !encoded.endsWith("/") ? `${encoded}/` : encoded;
}

function lieRelativeDestination(outputPath, targetPath, directory = false) {
  const outputDirectory = path.posix.dirname(outputPath);
  const relative = path.posix.relative(outputDirectory, targetPath) || ".";
  return lieEncodeDestination(relative, directory);
}

function lieValidateManifest(rootPath, manifestPath, manifestOverride) {
  const manifestRecord = lieResolveExisting(rootPath, manifestPath);
  const manifest = manifestOverride === undefined
    ? lieReadJson(manifestRecord.absolute, manifestRecord.safe)
    : manifestOverride;
  if (!manifest || typeof manifest !== "object" || Array.isArray(manifest) ||
      manifest.schemaVersion !== "1.0" || !Array.isArray(manifest.orderedTargets) ||
      !Array.isArray(manifest.dependencies) || !Array.isArray(manifest.roots)) {
    lieFail("LIE002", "series manifest is unsupported or incomplete");
  }

  const positions = new Set();
  const targets = manifest.orderedTargets.map((target, index) => {
    if (!target || typeof target !== "object" || typeof target.path !== "string" ||
        typeof target.role !== "string" || typeof target.status !== "string" ||
        typeof target.normalizedSha256 !== "string" ||
        !/^[0-9a-f]{64}$/.test(target.normalizedSha256)) {
      lieFail("LIE002", "target fields must use the documented string types");
    }
    const targetRecord = lieResolveExisting(rootPath, target.path);
    const content = lieReadText(targetRecord.absolute, targetRecord.safe);
    if (lieDigest(content) !== target.normalizedSha256) {
      lieFail("LIE009", `target hash differs from the manifest: ${targetRecord.safe}`);
    }
    const displayPosition = target.displayPosition ?? index + 1;
    if (!Number.isInteger(displayPosition) || displayPosition < 1) {
      lieFail("LIE002", "visible position must be a positive integer");
    }
    if (positions.has(displayPosition)) lieFail("LIE006", "visible position is duplicated");
    positions.add(displayPosition);
    return {...target, path: targetRecord.safe, displayPosition};
  });

  const paths = targets.map((target) => target.path);
  if (new Set(paths).size !== paths.length) lieFail("LIE006", "intake identity is duplicated");
  const targetByPath = new Map(targets.map((target) => [target.path, target]));
  const indegree = new Map(paths.map((targetPath) => [targetPath, 0]));
  const adjacency = new Map(paths.map((targetPath) => [targetPath, []]));
  const allowedBindings = new Map([
    ["HardCompletionGate", true],
    ["CommentSurfaceBaseline", true],
    ["DocumentationSurfaceBaseline", true],
    ["PreferredSerialOrder", false],
  ]);
  for (const edge of manifest.dependencies) {
    if (!edge || typeof edge !== "object" || typeof edge.from !== "string" ||
        typeof edge.to !== "string" || typeof edge.kind !== "string" ||
        typeof edge.binding !== "boolean" || !targetByPath.has(edge.from) ||
        !targetByPath.has(edge.to) || edge.from === edge.to ||
        allowedBindings.get(edge.kind) !== edge.binding ||
        targetByPath.get(edge.from).displayPosition >= targetByPath.get(edge.to).displayPosition) {
      lieFail("LIE007", "dependency edge contains an unknown endpoint or invalid values");
    }
    indegree.set(edge.to, indegree.get(edge.to) + 1);
    adjacency.get(edge.from).push(edge.to);
  }
  const calculatedRoots = [...indegree]
    .filter(([, value]) => value === 0)
    .map(([targetPath]) => targetPath)
    .sort();
  if (manifest.roots.some((rootEntry) => typeof rootEntry !== "string") ||
      JSON.stringify([...manifest.roots].sort()) !== JSON.stringify(calculatedRoots)) {
    lieFail("LIE007", "manifest roots differ from the dependency graph");
  }
  const remaining = new Map(indegree);
  const queue = calculatedRoots.slice();
  let visited = 0;
  while (queue.length > 0) {
    const current = queue.shift();
    visited++;
    for (const successor of adjacency.get(current)) {
      remaining.set(successor, remaining.get(successor) - 1);
      if (remaining.get(successor) === 0) queue.push(successor);
    }
  }
  if (visited !== targets.length) lieFail("LIE007", "series dependencies contain a cycle");
  return {manifest, manifestPath: manifestRecord.safe, targets, targetByPath};
}

function lieFeatureProofs(rootPath, target) {
  const specsPath = path.join(rootPath, "specs");
  if (!fs.existsSync(specsPath)) return [];
  const proofs = [];
  for (const directory of fs.readdirSync(specsPath, {withFileTypes: true})
    .filter((entry) => entry.isDirectory())
    .sort((left, right) => left.name.localeCompare(right.name, "en"))) {
    const stateRelative = `specs/${directory.name}/autonomous-run-state.json`;
    const stateAbsolute = path.join(rootPath, ...stateRelative.split("/"));
    if (!fs.existsSync(stateAbsolute)) continue;
    const stateRecord = lieResolveExisting(rootPath, stateRelative);
    const state = lieReadJson(stateRecord.absolute, stateRecord.safe);
    if (!state || typeof state !== "object" || Array.isArray(state)) {
      lieFail("LIE008", `feature evidence is invalid: ${stateRelative}`);
    }
    const accepted = Array.isArray(state.acceptedArtifacts)
      ? state.acceptedArtifacts.filter((artifact) => artifact?.path === target.path)
      : [];
    if (accepted.length === 0) continue;
    const closeout = state.closeout ?? {};
    const featurePath = state.featurePath;
    if (accepted.length !== 1 || accepted[0].sha256 !== target.normalizedSha256 ||
        state.status !== "Completed" || typeof featurePath !== "string" ||
        !["mergeOrPublication", "defaultBranchSync", "postMergeActions", "finalValidation"]
          .every((field) => closeout[field] === "Completed")) {
      lieFail("LIE008", `feature evidence is invalid for ${target.path}`);
    }
    const safeFeature = lieSafeRelative(featurePath, "LIE008");
    if (safeFeature !== `specs/${directory.name}`) {
      lieFail("LIE008", `feature path differs from its state location: ${stateRelative}`);
    }
    const featureAbsolute = path.join(rootPath, ...safeFeature.split("/"));
    if (!fs.existsSync(featureAbsolute) || !fs.statSync(featureAbsolute).isDirectory()) {
      lieFail("LIE008", `feature target is missing for ${target.path}`);
    }
    lieInsideRoot(rootPath, fs.realpathSync(featureAbsolute), "LIE008");
    proofs.push(safeFeature);
  }
  if (proofs.length > 1) lieFail("LIE008", `feature evidence is ambiguous for ${target.path}`);
  return proofs;
}

function lieTable(rootPath, manifestData, outputPath) {
  const lines = [
    "| Position | Status | Lastenheft/Intake | Abhängigkeiten / Dependencies | Spec-Kit-Feature |",
    "|---:|---|---|---|---|",
  ];
  for (const target of manifestData.targets) {
    const intakeLabel = lieEscapeLabel(path.posix.basename(target.path));
    const intakeDestination = lieRelativeDestination(outputPath, target.path);
    const incoming = manifestData.manifest.dependencies.filter((edge) => edge.to === target.path);
    const dependencyCell = incoming.length === 0
      ? "— (Root / keine direkte Abhängigkeit)"
      : incoming.map((edge) => {
        const label = lieEscapeLabel(path.posix.basename(edge.from));
        const destination = lieRelativeDestination(outputPath, edge.from);
        return `[${label}](${destination}) → current (\`${edge.kind}\`, binding: ${edge.binding})`;
      }).join("<br>");
    const proofs = lieFeatureProofs(rootPath, target);
    const featureCell = proofs.length === 0
      ? "— (kein Spec-Kit-Feature / no Spec Kit feature)"
      : `[${lieEscapeLabel(path.posix.basename(proofs[0]))}](${lieRelativeDestination(
        outputPath,
        proofs[0],
        true,
      )})`;
    lines.push(
      `| ${target.displayPosition} | ${lieEscapeLabel(target.status)} | [${intakeLabel}](${intakeDestination}) | ${dependencyCell} | ${featureCell} |`,
    );
  }
  return `${lines.join("\n")}\n`;
}

function lieSemanticRows(view) {
  const rows = lieNormalize(view.content).split("\n")
    .filter((line) => line.startsWith("|") && line.endsWith("|"))
    .slice(2);
  return rows.map((line) => {
    const cells = line.slice(2, -2).split(" | ");
    if (cells.length !== 5) lieFail("LIE011", "linked view does not contain exactly five fields");
    return cells.map((cell) => cell.replace(/\[([^\]]+)\]\(([^)]+)\)/g, (_match, label, destination) => {
      let decoded;
      try {
        decoded = destination.split("/").map((part) => decodeURIComponent(part)).join("/");
      } catch {
        lieFail("LIE011", "linked view contains an invalid escaped destination");
      }
      const resolved = path.posix.normalize(
        path.posix.join(path.posix.dirname(view.outputPath), decoded),
      );
      return `[${label}](${resolved})`;
    }));
  });
}

export function assertLinkedIntakeViewParity(views) {
  if (!Array.isArray(views) || views.length < 2) {
    lieFail("LIE011", "at least two linked views are required for parity");
  }
  const reference = JSON.stringify(lieSemanticRows(views[0]));
  if (views.slice(1).some((view) => JSON.stringify(lieSemanticRows(view)) !== reference)) {
    lieFail("LIE011", "root and series views do not agree semantically");
  }
  return true;
}

function lieRestoreOutputs(backups) {
  for (const backup of backups) {
    if (backup.existed) {
      fs.mkdirSync(path.dirname(backup.absolute), {recursive: true});
      fs.writeFileSync(backup.absolute, backup.content);
    } else {
      fs.rmSync(backup.absolute, {force: true});
    }
  }
}

function liePrepareLinkedIntakeViews(options = {}) {
  const rootPath = fs.realpathSync(path.resolve(options.root ?? process.cwd()));
  const manifestPath = lieSafeRelative(options.manifestPath);
  const outputPaths = options.outputPaths;
  if (!Array.isArray(outputPaths) || outputPaths.length !== 2) {
    lieFail("LIE002", "exactly two linked intake outputs are required");
  }
  const manifestData = lieValidateManifest(rootPath, manifestPath, options.manifest);
  const outputs = outputPaths.map((outputPath) => lieResolveOutput(rootPath, outputPath));
  if (new Set(outputs.map((output) => output.safe)).size !== outputs.length ||
      outputs.some((output) => output.safe === manifestData.manifestPath ||
        manifestData.targetByPath.has(output.safe))) {
    lieFail("LIE006", "output overlaps a canonical input or another output");
  }
  const tables = outputs.map((output) => ({
    ...output,
    content: lieTable(rootPath, manifestData, output.safe),
  }));
  assertLinkedIntakeViewParity(tables.map(({safe, content}) => ({
    outputPath: safe,
    content,
  })));
  const generationSha256 = lieDigest(JSON.stringify(lieSemanticRows({
    outputPath: tables[0].safe,
    content: tables[0].content,
  })));
  const views = tables.map((output) => {
    const content = typeof options.decorateOutput === "function"
      ? options.decorateOutput({
        outputPath: output.safe,
        manifestPath: manifestData.manifestPath,
        table: output.content,
        generationSha256,
      })
      : output.content;
    if (typeof content !== "string" || content.includes("\0") ||
        /[ \t]+$/m.test(content) || content.includes(rootPath)) {
      lieFail("LIE002", "generated output violates the text contract");
    }
    return {...output, content: lieNormalize(content)};
  });
  assertLinkedIntakeViewParity(views.map(({safe, content}) => ({
    outputPath: safe,
    content,
  })));
  return {rootPath, views, generationSha256};
}

function liePublishOutputs(rootPath, views, options = {}) {
  if (new Set(views.map((view) => view.safe)).size !== views.length) {
    lieFail("LIE006", "publication contains duplicate outputs");
  }

  const changed = views.filter((view) =>
    !fs.existsSync(view.absolute) ||
    lieReadText(view.absolute, view.safe) !== view.content);
  if (!options.write) {
    if (changed.length > 0) {
      lieFail("LIE009", `generated output is stale: ${changed[0].safe}`);
    }
    return {status: "Current", writes: 0, outputs: views.map((view) => view.safe)};
  }
  if (changed.length === 0) {
    return {status: "Current", writes: 0, outputs: views.map((view) => view.safe)};
  }

  const backups = changed.map((view) => ({
    absolute: view.absolute,
    existed: fs.existsSync(view.absolute),
    content: fs.existsSync(view.absolute) ? fs.readFileSync(view.absolute) : Buffer.alloc(0),
  }));
  const temporary = [];
  try {
    for (const view of changed) {
      fs.mkdirSync(path.dirname(view.absolute), {recursive: true});
      const temporaryPath = `${view.absolute}.tmp-${process.pid}-${temporary.length}`;
      fs.writeFileSync(temporaryPath, view.content, {encoding: "utf8", flag: "wx"});
      temporary.push({temporaryPath, view});
    }
    if (typeof options.beforeCommit === "function") options.beforeCommit();
    for (const item of temporary) fs.renameSync(item.temporaryPath, item.view.absolute);
    for (const view of changed) {
      if (lieReadText(view.absolute, view.safe) !== view.content) {
        throw new Error("post-write verification failed");
      }
    }
  } catch {
    for (const item of temporary) fs.rmSync(item.temporaryPath, {force: true});
    lieRestoreOutputs(backups);
    lieFail("LIE010", "publication failed; the previous outputs were restored");
  }
  return {
    status: "Updated",
    writes: changed.length,
    outputs: views.map((view) => view.safe),
  };
}

export function renderLinkedIntakeViews(options = {}) {
  const prepared = liePrepareLinkedIntakeViews(options);
  return liePublishOutputs(prepared.rootPath, prepared.views, options);
}

function tinyPl0OrderDocument({outputPath, manifestPath, table, generationSha256}) {
  const manifestDestination = lieRelativeDestination(outputPath, manifestPath);
  return `# TinyPl0 Intake-Reihenfolge / Intake Order

<!-- linked-intake-generation: ${generationSha256} -->

Diese Ansicht wird aus der kanonischen Intake-Serie abgeleitet. Verbindliche
Maschinendaten stehen im [Serienmanifest](${manifestDestination}).

*This view is derived from the canonical intake series. The linked series
manifest contains the binding machine-readable data.*

${table}
Nur \`Eligible\` bezeichnet die bevorzugte nächste Ausführung. \`Pending\` oder
\`Blocked\` erteilen keine automatische Ausführungsberechtigung.

*Only \`Eligible\` identifies the preferred next execution. \`Pending\` and
\`Blocked\` grant no automatic execution authority.*
`;
}

function runCli() {
const root = process.cwd();
const write = process.argv.includes("--write");
if (process.argv.includes("--help") || process.argv.includes("-h")) {
  console.log(`Verwendung / Usage: node scripts/render-requirements-intake-governance.mjs [--write]

Ohne Option werden alle erzeugten Intake-Governance-Artefakte schreibgeschützt
geprüft. --write veröffentlicht eine vorab validierte Generation; erkannte
Fehler werden zurückgerollt und ein gemeinsamer Marker bindet beide Ansichten.

Without an option, all generated intake-governance artifacts are checked
without writes. --write publishes one prevalidated generation, rolls back
detected failures, and binds both views with a shared marker.`);
  process.exit(0);
}
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
const seriesReceiptId = "daf8cfd0-7e7b-4242-b92a-515f9460016d";
const seriesOperationId = "2fa42de1-416d-4627-aed9-5247c400fc14";
const reviewId = "8804ad13-41b4-4feb-a10d-26d2f55333e6";
const priorReviewId = "357ed01f-f120-4634-8596-45e7baffa17d";
const createdAt = "2026-07-26T22:00:00Z";
const seriesUpdatedAt = "2026-09-02T21:55:35Z";
const reviewHead = "26a81e655b4e15f412a954f536681a842dea6e2f";
const reviewedAt = "2026-08-30T14:55:45Z";
const priorReviewArchivePath =
  "requirements/intakes/series-archive/tinypl0-delivery/20260830T145545Z-review/superseded-review.json";

const members = [
  ["constitution-change", "Lastenheft_Constitution_Change.md", "Completed"],
  ["secure-development-hardening", "Lastenheft_Secure-Development-Hardening.md", "Completed"],
  ["sandbox-gestuetzte-secure-development-haertung", "Lastenheft_Sandbox-gestuetzte-Secure-Development-Haertung.md", "Completed"],
  ["embeddable-vm-und-nuget", "Lastenheft_Embeddable-VM-und-NuGet.md", "Completed"],
  ["quellcode-doku", "Lastenheft_Quellcode_Doku.md", "Eligible"],
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
        : slug === "embeddable-vm-und-nuget"
          ? "requirements/intakes/archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md"
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
    authorityEvidence: "Thorsten explicitly authorized completion of the Embeddable VM and NuGet autonomous run in DeliveryMode MergeAndSync with narrowly scoped admin bypass and directed that no follow-up feature be started; PR 80 was explicitly approved on 2026-09-02.",
  },
  status: "Ready",
  manifest: {path: manifestPath, normalizedSha256: manifestHash},
  supersedes: {
    receiptPath: "requirements/intakes/series-archive/tinypl0-delivery/20260902T215535Z/receipt.json",
    receiptNormalizedSha256: "985b25c4a65c0ca3c14e307a43915e04a1ab88ab7fe313d6650329b2fbc518bd",
    manifestArchivePath: "requirements/intakes/series-archive/tinypl0-delivery/20260902T215535Z/manifest.json",
    manifestArchiveSha256: "c73a65227e91123ccf017b03720695ad1c21b5910eb966a79a824069c8ff0a17",
  },
  tombstone: {path: "N/A", normalizedSha256: "N/A"},
  nextAction: "$speckit-intake-series-status",
};
const seriesUpdatePaths = [
  "requirements/intakes/archive/Lastenheft_Embeddable-VM-und-NuGet.006-embeddable-vm-nuget.md",
  manifestPath,
  `${seriesRoot}/receipt.json`,
  `${seriesRoot}/operation.json`,
  `${seriesRoot}/order.md`,
  "requirements/intakes/series-archive/tinypl0-delivery/20260902T215535Z/manifest.json",
  "requirements/intakes/series-archive/tinypl0-delivery/20260902T215535Z/receipt.json",
  "requirements/intake-governance-config.json",
  "Pflichtenheft.md",
  "Lastenheft_Abarbeitungsreihenfolge.md",
  "scripts/render-requirements-intake-governance.mjs"
];
const operation = {
  schemaVersion: "1.0",
  documentType: "IntakeSeriesOperation",
  operationId: seriesOperationId,
  seriesId,
  type: "Update",
  status: "Published",
  authorityEvidence: "Thorsten explicitly authorized completion of the Embeddable VM and NuGet autonomous run in DeliveryMode MergeAndSync with narrowly scoped admin bypass and directed that no follow-up feature be started; PR 80 was explicitly approved on 2026-09-02.",
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

const linked = liePrepareLinkedIntakeViews({
  root,
  manifestPath,
  manifest,
  outputPaths: [
    "Lastenheft_Abarbeitungsreihenfolge.md",
    `${seriesRoot}/order.md`,
  ],
  decorateOutput: tinyPl0OrderDocument,
});
const rootPath = fs.realpathSync(path.resolve(root));
const legacyViews = outputs.map(([relativePath, content]) => ({
  ...lieResolveOutput(rootPath, relativePath),
  content: lieNormalize(content),
}));
liePublishOutputs(rootPath, [...legacyViews, ...linked.views], {write});
const configuredCountMismatch =
  config.schemaVersion === "1.0" && members.length !== config.activeIntakeCount;
if (configuredCountMismatch || targets.length !== new Set(targets).size) {
  throw new Error("configured active intake cardinality differs from generated members");
}
console.log(`TinyPl0 intake governance PASS (${members.length} series targets, ${dependencies.length} binding edges)`);
}

const invokedAsCli = Boolean(process.argv[1]) &&
  path.resolve(process.argv[1]) === fileURLToPath(import.meta.url);
if (invokedAsCli) runCli();
