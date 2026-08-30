#!/usr/bin/env bash
set -euo pipefail

usage() {
    printf '%s\n' \
        'DE: Nutzung: build-secure-development-docs.sh [--check|--dry-run]' \
        'EN: Usage: build-secure-development-docs.sh [--check|--dry-run]'
}

script_dir="$(cd -- "$(dirname -- "${BASH_SOURCE[0]}")" && pwd)"
args=(-NoProfile -File "${script_dir}/build-secure-development-docs.ps1")
case "${1:-}" in
    '') ;;
    --check) args+=(-Check) ;;
    --dry-run) args+=(-WhatIf) ;;
    --help|-h) usage; exit 0 ;;
    *) usage >&2; exit 2 ;;
esac

pwsh "${args[@]}"
