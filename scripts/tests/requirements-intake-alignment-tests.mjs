#!/usr/bin/env node

import fs from "node:fs";
import os from "node:os";
import path from "node:path";
import process from "node:process";
import {validate} from "../validate-requirements-intake-alignment.mjs";

const root = process.cwd();
const manifestSource = "requirements/intakes/series/tinypl0-delivery/manifest.json";
const coverageSource = "specs/requirements-reconciliation-20260726/requirements-coverage.json";
const temp = fs.mkdtempSync(path.join(os.tmpdir(), "tinypl0-requirements-"));

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
    value.orderedTargets[1].status = "Pending";
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

fs.rmSync(temp, {recursive: true, force: true});
console.log("requirements/intake negative fixtures PASS (8 cases)");
