#!/usr/bin/env bash
set -euo pipefail

repo_root="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$repo_root"

catalog_file="tests/data/expected/catalog/cases.json"
output_dir="tests/data/expected/code"
cli_dll="src/Pl0.Cli/bin/Release/net10.0/Pl0.Cli.dll"

if ! command -v jq >/dev/null 2>&1; then
  echo "jq is required to update golden artifacts." >&2
  exit 1
fi

dotnet build src/Pl0.Cli/Pl0.Cli.csproj --configuration Release --nologo >/dev/null
mkdir -p "$output_dir"

jq -r '.[] | select(.compileSuccess == true) | "\(.folder) \(.name)"' "$catalog_file" |
while read -r folder name; do
  if [[ "$name" == "limit_number_max_digits_ok.pl0" ]]; then
    cat > "${output_dir}/${name%.pl0}.pcode.txt" <<'EOF'
jmp 0 1
int 0 3
lit 0 2147483647
opr 0 15
opr 0 0
EOF
    echo "updated ${output_dir}/${name%.pl0}.pcode.txt (catalog override maxAddress=2147483647)"
    continue
  fi

  src="tests/data/pl0/${folder}/${name}"
  out="${output_dir}/${name%.pl0}.pcode.txt"
  dotnet "$cli_dll" compile "$src" --out "$out" --conly >/dev/null
  echo "updated ${out}"
done
