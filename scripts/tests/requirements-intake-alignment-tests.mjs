#!/usr/bin/env node

import crypto from "node:crypto";
import {spawnSync} from "node:child_process";
import fs from "node:fs";
import os from "node:os";
import path from "node:path";
import process from "node:process";
import {validate} from "../validate-requirements-intake-alignment.mjs";

const root = process.cwd();
const manifestSource = "requirements/intakes/series/tinypl0-delivery/manifest.json";
const coverageSource = "specs/requirements-reconciliation-20260726/requirements-coverage.json";
const fixtureSource = "scripts/tests/linked-intake-evidence";
const temp = fs.mkdtempSync(path.join(os.tmpdir(), "tinypl0-requirements-"));
const normalize = (value) => value.replace(/^\uFEFF/, "").replace(/\r\n?/g, "\n");
const digest = (value) => crypto.createHash("sha256").update(normalize(value)).digest("hex");

function fixture(name, source, mutate) {
  const value = JSON.parse(fs.readFileSync(path.join(root, source), "utf8"));
  mutate(value);
  const target = path.join(temp, `${name}.json`);
  fs.writeFileSync(target, JSON.stringify(value, null, 2) + "\n");
  return target;
}

function expectFailure(name, options, pattern) {
  const errors = validate({root, ...options});
  if (!errors.some((error) => pattern.test(error))) {
    throw new Error(`${name} did not fail as expected: ${errors.join("; ")}`);
  }
}

if (validate({root}).length !== 0) throw new Error("positive fixture failed");
expectFailure("duplicate target", {
  manifestPath: fixture("duplicate-target", manifestSource, (value) =>
    value.orderedTargets.push({...value.orderedTargets[0]})),
}, /unique targets/);
expectFailure("archive target", {
  manifestPath: fixture("archive-target", manifestSource, (value) => {
    value.orderedTargets[8].path = "requirements/intakes/archive/Missing.md";
  }),
}, /directory and series targets differ|only Completed series targets/);
expectFailure("missing eligible", {
  manifestPath: fixture("missing-eligible", manifestSource, (value) => {
    value.orderedTargets.find((target) => target.status === "Eligible").status = "Pending";
  }),
}, /single explicitly Eligible/);
expectFailure("stale hash", {
  manifestPath: fixture("stale-hash", manifestSource, (value) => {
    value.orderedTargets[0].normalizedSha256 = "0".repeat(64);
  }),
}, /target hash drift/);
expectFailure("cycle", {
  manifestPath: fixture("cycle", manifestSource, (value) => {
    value.dependencies.push({
      from: value.orderedTargets[5].path,
      to: value.orderedTargets[0].path,
      kind: "HardCompletionGate",
      binding: true,
    });
    value.roots = value.roots.filter((item) => item !== value.orderedTargets[0].path);
  }),
}, /cycle/);
expectFailure("dangling target", {
  manifestPath: fixture("dangling", manifestSource, (value) => {
    value.orderedTargets[8].path = "requirements/intakes/active/Missing.md";
  }),
}, /directory and series targets differ|target is missing/);
expectFailure("duplicate requirement", {
  coveragePath: fixture("duplicate-requirement", coverageSource, (value) => {
    value.requirements[1].requirementId = value.requirements[0].requirementId;
  }),
}, /unique requirement IDs/);
expectFailure("missing owner", {
  coveragePath: fixture("missing-owner", coverageSource, (value) => {
    value.requirements[0].proposedOwnerGroup = "N/A";
  }),
}, /lacks owner/);

const rendererModule = await import("../render-requirements-intake-governance.mjs");
if (typeof rendererModule.renderLinkedIntakeViews !== "function" ||
    typeof rendererModule.assertLinkedIntakeViewParity !== "function") {
  throw new Error("RED: linked-intake renderer and parity exports are not implemented");
}
const {renderLinkedIntakeViews, assertLinkedIntakeViewParity} = rendererModule;
const executedLinkedCases = new Map();

function writeFixtureFile(fixtureRoot, relativePath, content) {
  const target = path.join(fixtureRoot, relativePath);
  fs.mkdirSync(path.dirname(target), {recursive: true});
  fs.writeFileSync(target, content);
}

function createLinkedFixture(name) {
  const fixtureRoot = path.join(temp, name);
  const definition = JSON.parse(
    fs.readFileSync(path.join(root, fixtureSource, "positive/cases.json"), "utf8"),
  );
  const contents = new Map();
  for (const entry of definition.entries) {
    const content = `# ${path.basename(entry.intakePath)}\n\n**Reihenfolge:** sichtbare Position ${entry.displayPosition}\n`;
    writeFixtureFile(fixtureRoot, entry.intakePath, content);
    contents.set(entry.intakePath, content);
  }
  const dependencies = definition.entries.flatMap((entry) => entry.incomingDependencies);
  const manifest = {
    schemaVersion: "1.0",
    documentType: "IntakeSeriesManifest",
    seriesId: "tinypl0-linked-fixture",
    title: "TinyPl0 Linked Fixture",
    policy: "fixture",
    status: "Active",
    orderedTargets: definition.entries.map((entry) => ({
      path: entry.intakePath,
      role: entry.role,
      status: entry.status,
      displayPosition: entry.displayPosition,
      normalizedSha256: digest(contents.get(entry.intakePath)),
    })),
    roots: definition.entries
      .filter((entry) => entry.incomingDependencies.length === 0)
      .map((entry) => entry.intakePath),
    dependencies,
    evidencePaths: [],
  };
  const manifestPath = "requirements/intakes/series/example/manifest.json";
  writeFixtureFile(fixtureRoot, manifestPath, JSON.stringify(manifest, null, 2) + "\n");
  const linkedEntry = definition.entries.find((entry) => entry.featureState === "Linked");
  const featurePath = linkedEntry.featureDirectory;
  writeFixtureFile(fixtureRoot, `${featurePath}/autonomous-run-state.json`, JSON.stringify({
    schemaVersion: "1.1",
    featurePath,
    status: "Completed",
    acceptedArtifacts: [{
      path: linkedEntry.intakePath,
      sha256: digest(contents.get(linkedEntry.intakePath)),
    }],
    closeout: {
      mergeOrPublication: "Completed",
      defaultBranchSync: "Completed",
      postMergeActions: "Completed",
      finalValidation: "Completed",
    },
  }, null, 2) + "\n");
  return {
    fixtureRoot,
    definition,
    manifest,
    manifestPath,
    outputs: [
      "Lastenheft_Abarbeitungsreihenfolge.md",
      "requirements/intakes/series/example/order.md",
    ],
  };
}

function renderOptions(fixtureData, overrides = {}) {
  return {
    root: fixtureData.fixtureRoot,
    manifestPath: fixtureData.manifestPath,
    outputPaths: fixtureData.outputs,
    write: false,
    ...overrides,
  };
}

function expectLinkedFailure(name, fixtureData, mutate, expectedCode, overrides = {}) {
  mutate(fixtureData);
  const absent = fixtureData.outputs.filter(
    (output) => !fs.existsSync(path.join(fixtureData.fixtureRoot, output)),
  );
  const before = new Map(
    fixtureData.outputs
      .filter((output) => fs.existsSync(path.join(fixtureData.fixtureRoot, output)))
      .map((output) => [output, fs.readFileSync(path.join(fixtureData.fixtureRoot, output))]),
  );
  let actual = "";
  try {
    renderLinkedIntakeViews(renderOptions(fixtureData, overrides));
  } catch (error) {
    actual = error?.code ?? "";
  }
  if (actual !== expectedCode) {
    throw new Error(`${name} expected ${expectedCode}, got ${actual || "success"}`);
  }
  for (const [output, content] of before) {
    if (!fs.readFileSync(path.join(fixtureData.fixtureRoot, output)).equals(content)) {
      throw new Error(`${name} changed output despite failure: ${output}`);
    }
  }
  if (absent.some((output) => fs.existsSync(path.join(fixtureData.fixtureRoot, output)))) {
    throw new Error(`${name} created output despite failure`);
  }
  executedLinkedCases.set(name, {expectedDiagnostic: actual, writes: 0});
}

const positive = createLinkedFixture("linked-positive");
const firstWrite = renderLinkedIntakeViews(renderOptions(positive, {write: true}));
if (firstWrite.writes !== 2) throw new Error("write mode did not publish both linked views");
const rootOutput = fs.readFileSync(path.join(positive.fixtureRoot, positive.outputs[0]), "utf8");
const seriesOutput = fs.readFileSync(path.join(positive.fixtureRoot, positive.outputs[1]), "utf8");
const expectedRoot = normalize(
  fs.readFileSync(path.join(root, fixtureSource, "positive/expected-root.md"), "utf8"),
);
const expectedSeries = normalize(
  fs.readFileSync(path.join(root, fixtureSource, "positive/expected-series.md"), "utf8"),
);
if (rootOutput !== expectedRoot || seriesOutput !== expectedSeries) {
  throw new Error("five-field linked intake projection differs from expected bytes");
}
assertLinkedIntakeViewParity([
  {outputPath: positive.outputs[0], content: rootOutput},
  {outputPath: positive.outputs[1], content: seriesOutput},
]);
const cleanCheck = renderLinkedIntakeViews(renderOptions(positive));
if (cleanCheck.writes !== 0 || cleanCheck.status !== "Current") {
  throw new Error("check mode did not report current output with zero writes");
}
const beforeSecondWrite = positive.outputs.map((output) =>
  digest(fs.readFileSync(path.join(positive.fixtureRoot, output), "utf8")));
const secondWrite = renderLinkedIntakeViews(renderOptions(positive, {write: true}));
const afterSecondWrite = positive.outputs.map((output) =>
  digest(fs.readFileSync(path.join(positive.fixtureRoot, output), "utf8")));
if (secondWrite.writes !== 0 ||
    JSON.stringify(beforeSecondWrite) !== JSON.stringify(afterSecondWrite)) {
  throw new Error("second unchanged write was not idempotent");
}

const generation = createLinkedFixture("generation-marker");
renderLinkedIntakeViews(renderOptions(generation, {
  write: true,
  decorateOutput: ({table, generationSha256}) =>
    `<!-- linked-intake-generation: ${generationSha256} -->\n${table}`,
}));
const generationMarkers = generation.outputs.map((output) => {
  const content = fs.readFileSync(path.join(generation.fixtureRoot, output), "utf8");
  return content.match(/linked-intake-generation: ([0-9a-f]{64})/)?.[1] ?? "";
});
if (!generationMarkers[0] || generationMarkers[0] !== generationMarkers[1]) {
  throw new Error("paired views do not share one deterministic generation marker");
}

const importOnly = createLinkedFixture("import-only");
const importResult = spawnSync(process.execPath, [
  "--input-type=module",
  "--eval",
  `import(${JSON.stringify(new URL("../render-requirements-intake-governance.mjs", import.meta.url).href)})`,
  "--",
  "--write",
], {cwd: importOnly.fixtureRoot, encoding: "utf8"});
if (importResult.status !== 0 || importOnly.outputs.some((output) =>
  fs.existsSync(path.join(importOnly.fixtureRoot, output)))) {
  throw new Error(`renderer import executed CLI publication: ${importResult.stderr}`);
}

expectLinkedFailure("missing-required-field", createLinkedFixture("missing-required"), (data) => {
  delete data.manifest.schemaVersion;
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE002");
expectLinkedFailure("non-string-target-field", createLinkedFixture("non-string-target"), (data) => {
  data.manifest.orderedTargets[0].status = 7;
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE002");
expectLinkedFailure("null-manifest", createLinkedFixture("null-manifest"), (data) => {
  writeFixtureFile(data.fixtureRoot, data.manifestPath, "null\n");
}, "LIE002");
expectLinkedFailure("control-character-status", createLinkedFixture("control-status"), (data) => {
  data.manifest.orderedTargets[0].status = "Completed\n| injected |";
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE002");
expectLinkedFailure("absolute-path", createLinkedFixture("absolute-path"), (data) => {
  data.manifest.orderedTargets[0].path = "/etc/passwd";
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE003");
expectLinkedFailure("parent-traversal", createLinkedFixture("parent-traversal"), (data) => {
  data.manifest.orderedTargets[0].path = "../outside.md";
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE003");
expectLinkedFailure("embedded-nul-path", createLinkedFixture("nul-path"), (data) => {
  data.manifest.orderedTargets[0].path = "requirements/intakes/active/NUL\0.md";
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE003");
expectLinkedFailure("missing-target", createLinkedFixture("missing-target"), (data) => {
  fs.rmSync(path.join(data.fixtureRoot, data.manifest.orderedTargets[0].path));
}, "LIE004");
expectLinkedFailure("symlink-escape", createLinkedFixture("symlink-escape"), (data) => {
  const target = data.manifest.orderedTargets[0];
  const outside = path.join(temp, "outside-intake.md");
  fs.writeFileSync(outside, "outside\n");
  fs.rmSync(path.join(data.fixtureRoot, target.path));
  fs.symlinkSync(outside, path.join(data.fixtureRoot, target.path));
}, "LIE005");
expectLinkedFailure("output-symlink", createLinkedFixture("output-symlink"), (data) => {
  const outside = path.join(temp, "outside-output.md");
  fs.writeFileSync(outside, "outside output\n");
  const output = path.join(data.fixtureRoot, data.outputs[0]);
  fs.mkdirSync(path.dirname(output), {recursive: true});
  fs.symlinkSync(outside, output);
}, "LIE005");
expectLinkedFailure("feature-state-symlink", createLinkedFixture("state-symlink"), (data) => {
  const statePath = path.join(
    data.fixtureRoot,
    "specs/032-linked-intake-evidence/autonomous-run-state.json",
  );
  const outside = path.join(temp, "outside-state.json");
  fs.writeFileSync(outside, fs.readFileSync(statePath));
  fs.rmSync(statePath);
  fs.symlinkSync(outside, statePath);
}, "LIE005");
expectLinkedFailure("duplicate-identity", createLinkedFixture("duplicate-identity"), (data) => {
  data.manifest.orderedTargets.push({...data.manifest.orderedTargets[0]});
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE006");
expectLinkedFailure("output-overlap", createLinkedFixture("output-overlap"), () => {}, "LIE006", {
  outputPaths: [
    "requirements/intakes/series/example/manifest.json",
    "requirements/intakes/series/example/order.md",
  ],
});
expectLinkedFailure("unknown-endpoint", createLinkedFixture("unknown-endpoint"), (data) => {
  data.manifest.dependencies[0].from = "requirements/intakes/active/Missing.md";
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE007");
expectLinkedFailure("self-edge", createLinkedFixture("self-edge"), (data) => {
  data.manifest.dependencies.push({
    from: data.manifest.orderedTargets[0].path,
    to: data.manifest.orderedTargets[0].path,
    kind: "HardCompletionGate",
    binding: true,
  });
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE007");
expectLinkedFailure("dependency-cycle", createLinkedFixture("cycle"), (data) => {
  data.manifest.dependencies.push({
    from: data.manifest.orderedTargets[0].path,
    to: data.manifest.orderedTargets[1].path,
    kind: "HardCompletionGate",
    binding: true,
  });
  data.manifest.roots = data.manifest.roots.filter(
    (entry) => entry !== data.manifest.orderedTargets[1].path,
  );
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE007");
expectLinkedFailure("incorrect-root-set", createLinkedFixture("roots"), (data) => {
  data.manifest.roots = [data.manifest.orderedTargets[0].path];
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE007");
expectLinkedFailure("ambiguous-feature-proof", createLinkedFixture("ambiguous-feature"), (data) => {
  const target = data.manifest.orderedTargets[0];
  writeFixtureFile(data.fixtureRoot, "specs/099-second/autonomous-run-state.json", JSON.stringify({
    schemaVersion: "1.1",
    featurePath: "specs/099-second",
    status: "Completed",
    acceptedArtifacts: [{path: target.path, sha256: target.normalizedSha256}],
    closeout: {
      mergeOrPublication: "Completed",
      defaultBranchSync: "Completed",
      postMergeActions: "Completed",
      finalValidation: "Completed",
    },
  }) + "\n");
}, "LIE008");
expectLinkedFailure("null-feature-state", createLinkedFixture("null-state"), (data) => {
  const statePath = path.join(
    data.fixtureRoot,
    "specs/032-linked-intake-evidence/autonomous-run-state.json",
  );
  fs.writeFileSync(statePath, "null\n");
}, "LIE008");
expectLinkedFailure("feature-path-mismatch", createLinkedFixture("invalid-feature"), (data) => {
  const statePath = path.join(
    data.fixtureRoot,
    "specs/032-linked-intake-evidence/autonomous-run-state.json",
  );
  const state = JSON.parse(fs.readFileSync(statePath, "utf8"));
  state.featurePath = "specs/404-missing";
  fs.writeFileSync(statePath, JSON.stringify(state) + "\n");
}, "LIE008");
expectLinkedFailure("target-hash-mismatch", createLinkedFixture("hash-mismatch"), (data) => {
  data.manifest.orderedTargets[0].normalizedSha256 = "0".repeat(64);
  writeFixtureFile(data.fixtureRoot, data.manifestPath, JSON.stringify(data.manifest) + "\n");
}, "LIE009");

const invalidUtf8 = createLinkedFixture("invalid-utf8");
fs.writeFileSync(path.join(invalidUtf8.fixtureRoot, invalidUtf8.manifestPath), Buffer.from([0xc3, 0x28]));
expectLinkedFailure("invalid-utf8", invalidUtf8, () => {}, "LIE001");
const embeddedNul = createLinkedFixture("embedded-nul");
fs.writeFileSync(
  path.join(embeddedNul.fixtureRoot, embeddedNul.manifestPath),
  Buffer.from('{"schemaVersion":"1.0",\u0000"orderedTargets":[]}'),
);
expectLinkedFailure("embedded-nul", embeddedNul, () => {}, "LIE001");

const stale = createLinkedFixture("stale-output");
renderLinkedIntakeViews(renderOptions(stale, {write: true}));
fs.appendFileSync(path.join(stale.fixtureRoot, stale.outputs[0]), "stale\n");
expectLinkedFailure("stale-check-output", stale, () => {}, "LIE009");

const transaction = createLinkedFixture("transaction");
writeFixtureFile(transaction.fixtureRoot, transaction.outputs[0], "old root\n");
writeFixtureFile(transaction.fixtureRoot, transaction.outputs[1], "old series\n");
expectLinkedFailure("simulated-publish-failure", transaction, () => {}, "LIE010", {
  write: true,
  beforeCommit: () => {
    throw new Error("simulated");
  },
});

let parityCode = "";
try {
  assertLinkedIntakeViewParity([
    {outputPath: positive.outputs[0], content: rootOutput},
    {outputPath: positive.outputs[1], content: seriesOutput.replace("| 40 |", "| 41 |")},
  ]);
} catch (error) {
  parityCode = error?.code ?? "";
}
if (parityCode !== "LIE011") throw new Error("root/series semantic drift was not rejected");
executedLinkedCases.set("root-series-drift", {expectedDiagnostic: parityCode, writes: 0});

const negativeCases = JSON.parse(
  fs.readFileSync(path.join(root, fixtureSource, "negative/cases.json"), "utf8"),
);
const diagnostics = new Set(negativeCases.cases.map((entry) => entry.expectedDiagnostic));
for (let number = 1; number <= 11; number++) {
  const code = `LIE${String(number).padStart(3, "0")}`;
  if (!diagnostics.has(code)) throw new Error(`negative fixture catalog omits ${code}`);
}
if (executedLinkedCases.size !== negativeCases.cases.length) {
  throw new Error("negative fixture catalog and executed cases differ in cardinality");
}
for (const fixtureCase of negativeCases.cases) {
  const execution = executedLinkedCases.get(fixtureCase.id);
  if (!execution || execution.expectedDiagnostic !== fixtureCase.expectedDiagnostic ||
      execution.writes !== fixtureCase.writes) {
    throw new Error(`negative fixture case is not bound to its execution: ${fixtureCase.id}`);
  }
}
const dependencyCases = JSON.parse(
  fs.readFileSync(path.join(root, fixtureSource, "dependency-cases.json"), "utf8"),
);
if (JSON.stringify(dependencyCases.cases.map((entry) => entry.incomingCount)) !== "[0,1,2]") {
  throw new Error("dependency fixture catalog must cover zero, one, and multiple edges");
}
const featureCases = JSON.parse(
  fs.readFileSync(path.join(root, fixtureSource, "feature-proof-matrix.json"), "utf8"),
);
if (!new Set(featureCases.cases.map((entry) => entry.expectedState)).has("NoEvidence") ||
    !featureCases.cases.some((entry) => entry.id === "similarity-is-not-proof")) {
  throw new Error("feature fixture catalog omits explicit fallback or proof-boundary coverage");
}

fs.rmSync(temp, {recursive: true, force: true});
console.log(`requirements/intake fixtures PASS (8 legacy, ${negativeCases.cases.length} linked cases)`);
