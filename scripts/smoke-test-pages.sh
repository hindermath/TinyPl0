#!/usr/bin/env bash

set -euo pipefail

if [[ $# -ne 1 ]]; then
  echo "Usage: $0 <base-url>" >&2
  exit 1
fi

base_url="${1%/}"

checks=(
  "/|TinyPl0 – Dokumentation & Ausbildung"
  "/docfx/start/quickstart.html|Schnellstart"
  "/docfx/api/index.html|API-Referenz"
  "/docfx/examples/index.html|Beispiele & Tutorials"
  "/docfx/appendix/index.html|Anhang: Beispielprogramme"
  "/docs/ARCHITECTURE.html|TinyPl0 Architektur"
)

for check in "${checks[@]}"; do
  path="${check%%|*}"
  needle="${check#*|}"
  url="${base_url}${path}"

  echo "Smoke testing ${url}"
  body="$(curl --fail --silent --show-error --location "${url}")"

  if ! BODY="${body}" python3 - "${needle}" <<'PY'
import html
import os
import sys

needle = sys.argv[1]
body = html.unescape(os.environ["BODY"])
sys.exit(0 if needle in body else 1)
PY
  then
    echo "Expected to find '${needle}' in ${url}" >&2
    exit 1
  fi
done
