#!/usr/bin/env bash
#
# maERP CLI — manage Server and WASM containers via docker compose profiles.
#
# Usage:
#   ./cli.sh deploy server         Build the server image (and grafana-agent)
#   ./cli.sh deploy wasm           Build the WASM image
#   ./cli.sh start  server         Start the server stack (server + grafana-agent)
#   ./cli.sh start  wasm           Start the WASM stack
#   ./cli.sh stop   server         Stop the server stack
#   ./cli.sh stop   wasm           Stop the WASM stack
#   ./cli.sh logs   all            Tail logs of every running service
#   ./cli.sh logs   server         Tail server logs (server + grafana-agent)
#   ./cli.sh logs   wasm           Tail WASM logs
#   ./cli.sh db     backup         Dump postgres into ./backups/ as gzip -9
#   ./cli.sh db     restore <file> Restore <file> from ./backups/ into postgres

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
    sed -n '3,16p' "$0" | sed 's/^# \{0,1\}//'
}

# --- Postgres helpers ---------------------------------------------------------
PG_SERVICE="postgres"
PG_USER="maerp"
PG_DB="maerp"
BACKUP_DIR="${SCRIPT_DIR}/backups"

postgres_running() {
    local cid
    cid="$("${COMPOSE[@]}" ps -q "$PG_SERVICE" 2>/dev/null || true)"
    [[ -n "$cid" ]] && [[ "$(docker inspect -f '{{.State.Running}}' "$cid" 2>/dev/null)" == "true" ]]
}

require_target() {
    local cmd="$1" target="${2:-}"
    if [[ -z "$target" ]]; then
        echo "error: '$cmd' requires a target (server|wasm)." >&2
        usage >&2
        exit 1
    fi
}

git_pull() {
    if ! command -v git >/dev/null 2>&1; then
        echo "warning: 'git' not found, skipping pull." >&2
        return 0
    fi
    if ! git rev-parse --is-inside-work-tree >/dev/null 2>&1; then
        echo "warning: not a git working tree, skipping pull." >&2
        return 0
    fi
    echo ">>> git pull --ff-only"
    git pull --ff-only
}

cmd_deploy() {
    local target="${1:-}"
    require_target deploy "$target"
    case "$target" in
        server|wasm)
            git_pull
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

cmd_db() {
    local sub="${1:-}"
    case "$sub" in
        backup)  db_backup ;;
        restore) shift; db_restore "${1:-}" ;;
        "")
            echo "error: 'db' requires a subcommand (backup|restore)." >&2
            usage >&2
            exit 1
            ;;
        *)
            echo "error: unknown db subcommand '$sub' (expected: backup|restore)." >&2
            exit 1
            ;;
    esac
}

db_backup() {
    if ! postgres_running; then
        echo "error: postgres container is not running. Start it with: ./cli.sh start server" >&2
        exit 1
    fi

    mkdir -p "$BACKUP_DIR"
    local ts file
    ts="$(date +%Y%m%d-%H%M%S)"
    file="${BACKUP_DIR}/${ts}_maerp.sql.gz"

    echo ">>> Dumping database '${PG_DB}' to ${file}"
    # pg_dump → stdout → gzip -9 → file. -T disables TTY so the stream stays binary-clean.
    if "${COMPOSE[@]}" exec -T "$PG_SERVICE" pg_dump -U "$PG_USER" -d "$PG_DB" \
        | gzip -9 > "$file"; then
        echo ">>> Backup written: ${file} ($(du -h "$file" | cut -f1))"
    else
        rm -f "$file"
        echo "error: backup failed." >&2
        exit 1
    fi
}

db_restore() {
    local arg="${1:-}"
    if [[ -z "$arg" ]]; then
        echo "error: 'db restore' requires a filename." >&2
        echo "usage: ./cli.sh db restore <filename>   (relative to ./backups/, or absolute)" >&2
        exit 1
    fi

    # Resolve the file: accept absolute path, or basename inside ./backups/.
    local file
    if [[ "$arg" = /* ]]; then
        file="$arg"
    elif [[ -f "${BACKUP_DIR}/${arg}" ]]; then
        file="${BACKUP_DIR}/${arg}"
    else
        file="${BACKUP_DIR}/${arg}"
    fi

    if [[ ! -f "$file" ]]; then
        echo "error: backup file not found: ${file}" >&2
        exit 1
    fi

    if ! postgres_running; then
        echo "error: postgres container is not running. Start it with: ./cli.sh start server" >&2
        exit 1
    fi

    echo ">>> Restoring ${file} into database '${PG_DB}'"
    # Decompress on the host, stream the SQL into psql inside the container.
    # gzip -dc handles both .gz and (passes through) plain SQL only when extension is .gz;
    # if a plain .sql is passed, we cat instead.
    if [[ "$file" == *.gz ]]; then
        gzip -dc "$file" | "${COMPOSE[@]}" exec -T "$PG_SERVICE" psql -v ON_ERROR_STOP=1 -U "$PG_USER" -d "$PG_DB"
    else
        "${COMPOSE[@]}" exec -T "$PG_SERVICE" psql -v ON_ERROR_STOP=1 -U "$PG_USER" -d "$PG_DB" < "$file"
    fi
    echo ">>> Restore complete."
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
        db)     cmd_db     "$@" ;;
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
