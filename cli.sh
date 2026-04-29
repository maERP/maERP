#!/usr/bin/env bash
#
# maERP CLI — manage Server and WASM containers via docker compose profiles.
#
# Usage:
#   ./cli.sh deploy server      Build the server image (and grafana-agent)
#   ./cli.sh deploy wasm        Build the WASM image
#   ./cli.sh start  server      Start the server stack (server + grafana-agent)
#   ./cli.sh start  wasm        Start the WASM stack
#   ./cli.sh stop   server      Stop the server stack
#   ./cli.sh stop   wasm        Stop the WASM stack
#   ./cli.sh logs   all         Tail logs of every running service
#   ./cli.sh logs   server      Tail server logs (server + grafana-agent)
#   ./cli.sh logs   wasm        Tail WASM logs

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# Pick docker compose v2 (`docker compose`) over the legacy `docker-compose`.
if docker compose version >/dev/null 2>&1; then
    COMPOSE=(docker compose)
elif command -v docker-compose >/dev/null 2>&1; then
    COMPOSE=(docker-compose)
else
    echo "error: neither 'docker compose' nor 'docker-compose' is available." >&2
    exit 1
fi

usage() {
    sed -n '3,14p' "$0" | sed 's/^# \{0,1\}//'
}

require_target() {
    local cmd="$1" target="${2:-}"
    if [[ -z "$target" ]]; then
        echo "error: '$cmd' requires a target (server|wasm)." >&2
        usage >&2
        exit 1
    fi
}

cmd_deploy() {
    local target="${1:-}"
    require_target deploy "$target"
    case "$target" in
        server|wasm)
            "${COMPOSE[@]}" --profile "$target" build
            ;;
        *)
            echo "error: unknown deploy target '$target' (expected: server|wasm)." >&2
            exit 1
            ;;
    esac
}

cmd_start() {
    local target="${1:-}"
    require_target start "$target"
    case "$target" in
        server|wasm)
            "${COMPOSE[@]}" --profile "$target" up -d
            ;;
        *)
            echo "error: unknown start target '$target' (expected: server|wasm)." >&2
            exit 1
            ;;
    esac
}

cmd_stop() {
    local target="${1:-}"
    require_target stop "$target"
    case "$target" in
        server|wasm)
            "${COMPOSE[@]}" --profile "$target" down
            ;;
        *)
            echo "error: unknown stop target '$target' (expected: server|wasm)." >&2
            exit 1
            ;;
    esac
}

cmd_logs() {
    local target="${1:-}"
    require_target logs "$target"
    case "$target" in
        all)
            "${COMPOSE[@]}" --profile server --profile wasm logs -f --tail=200
            ;;
        server)
            "${COMPOSE[@]}" --profile server logs -f --tail=200 server grafana-agent
            ;;
        wasm)
            "${COMPOSE[@]}" --profile wasm logs -f --tail=200 wasm
            ;;
        *)
            echo "error: unknown logs target '$target' (expected: all|server|wasm)." >&2
            exit 1
            ;;
    esac
}

main() {
    if [[ $# -lt 1 ]]; then
        usage >&2
        exit 1
    fi

    local command="$1"
    shift

    case "$command" in
        deploy) cmd_deploy "$@" ;;
        start)  cmd_start  "$@" ;;
        stop)   cmd_stop   "$@" ;;
        logs)   cmd_logs   "$@" ;;
        -h|--help|help)
            usage
            ;;
        *)
            echo "error: unknown command '$command'." >&2
            usage >&2
            exit 1
            ;;
    esac
}

main "$@"
