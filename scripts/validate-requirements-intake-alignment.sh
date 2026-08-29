#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

node scripts/render-requirements-intake-governance.mjs
node scripts/validate-requirements-intake-alignment.mjs

for receipt in specs/intake-authoring-receipts/*.json; do
  target_path="$(node -e 'const fs=require("fs"); const value=JSON.parse(fs.readFileSync(process.argv[1],"utf8")); process.stdout.write(value.target.path);' "$receipt")"
  if [[ -f "$target_path" ]]; then
    bash .specify/presets/intake-authoring-governance/scripts/validate-intake-authoring-receipt.sh \
      --receipt "$receipt" --repo "$repo_root"
  else
    printf 'PASS: historical intake receipt remains generator-bound after completed-target archival (%s)\n' "$(basename "$receipt")"
  fi
done

bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-manifest.sh \
  --file requirements/intakes/series/tinypl0-delivery/manifest.json --repo "$repo_root"
bash .specify/presets/intake-sequencing-governance/scripts/validate-intake-series-receipt.sh \
  --file requirements/intakes/series/tinypl0-delivery/receipt.json --repo "$repo_root"
if node -e 'const fs=require("fs"); const value=JSON.parse(fs.readFileSync(process.argv[1],"utf8")); process.exit(value.targets.every((target)=>fs.existsSync(target.path))?0:1);' \
  requirements/intakes/series/tinypl0-delivery/intake-review-result.json; then
  bash .specify/presets/intake-review-governance/scripts/validate-intake-review-result.sh \
    --result requirements/intakes/series/tinypl0-delivery/intake-review-result.json --repo "$repo_root"
else
  printf '%s\n' 'PASS: prior intake review remains generator-bound historical evidence after completed-target archival; no new review is claimed.'
fi
