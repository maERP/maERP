#!/usr/bin/env bash
#
# maERP CLI — manage Server and WASM containers via docker compose profiles.
#
# Usage:
#   ./cli.sh deploy server         git pull, build & (re)start the server stack
#   ./cli.sh deploy wasm           git pull, build & (re)start the WASM stack
#   ./cli.sh start  server         Start the server stack (server + postgres if internal)
#   ./cli.sh start  wasm           Start the WASM stack
#   ./cli.sh stop   server         Stop the server stack
#   ./cli.sh stop   wasm           Stop the WASM stack
#   ./cli.sh logs   all            Tail logs of every running service
#   ./cli.sh logs   server         Tail server logs (server + postgres if internal)
#   ./cli.sh logs   wasm           Tail WASM logs
#   ./cli.sh db     backup           Dump database into ./backups/ as gzip -9
#   ./cli.sh db     restore <file>   Restore <file> from ./backups/ into the database
#   ./cli.sh db     restore <file> --clean
#                                    Drop & recreate the schema/database before restore
#
# Database backend is selected via DB_MODE in .env:
#   internal (default) → docker-compose runs an embedded postgres container.
#                        No credentials needed — managed by this script.
#   postgres           → external PostgreSQL.  Set DB_HOST/_PORT/_NAME/_USER/_PASSWORD.
#   mssql              → external Microsoft SQL Server. "         "         "
#                        (Note: db backup/restore is not implemented for mssql.)

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# Pick docker compose v2 (`docker compose`) over the legacy `docker-compose`.
if docker compose version >/dev/null 2>&1; then
    COMPOSE_BIN=(docker compose)
elif command -v docker-compose >/dev/null 2>&1; then
    COMPOSE_BIN=(docker-compose)
else
    echo "error: neither 'docker compose' nor 'docker-compose' is available." >&2
    exit 1
fi

# COMPOSE = binary + the file flags we need. Assembled in load_env() once .env
# is parsed so optional override files (e.g. postgres port expose) can be
# layered in based on the user's settings.
COMPOSE=("${COMPOSE_BIN[@]}")

usage() {
    sed -n '3,27p' "$0" | sed 's/^# \{0,1\}//'
}

# Print an error message followed by the full usage block, then exit 1.
# Used for any error caused by wrong/missing CLI arguments — distinct from
# operational errors (container not running, backup failed, …) which don't
# need the usage clutter.
die_usage() {
    echo "error: $1" >&2
    echo "" >&2
    usage >&2
    exit 1
}

# --- Database / .env resolution ----------------------------------------------
BACKUP_DIR="${SCRIPT_DIR}/backups"

# Internal-mode defaults — fixed because the container is only reachable inside
# the docker network and the user shouldn't have to configure them.
PG_INTERNAL_HOST="postgres"
PG_INTERNAL_PORT="5432"
PG_INTERNAL_DB="maerp"
PG_INTERNAL_USER="maerp"
PG_INTERNAL_PASSWORD="maerp"

# Client image used to run pg_dump against external DBs.
PG_CLIENT_IMAGE="postgres:16-alpine"

# Resolved during load_env(); used everywhere downstream.
DB_ENGINE=""             # postgres | mssql
DB_PROVIDER=""           # PostgreSQL | MSSQL  (DatabaseConfig__Provider)
DB_CONNECTION_STRING=""  # passed to DatabaseConfig__ConnectionString

load_env() {
    if [[ -f "${SCRIPT_DIR}/.env" ]]; then
        set -a
        # shellcheck disable=SC1091
        source "${SCRIPT_DIR}/.env"
        set +a
    fi

    DB_MODE="${DB_MODE:-internal}"

    case "$DB_MODE" in
        internal)
            DB_ENGINE="postgres"
            DB_HOST="$PG_INTERNAL_HOST"
            DB_PORT="$PG_INTERNAL_PORT"
            DB_NAME="$PG_INTERNAL_DB"
            DB_USER="$PG_INTERNAL_USER"
            DB_PASSWORD="$PG_INTERNAL_PASSWORD"
            ;;
        postgres)
            DB_ENGINE="postgres"
            require_external_db
            DB_PORT="${DB_PORT:-5432}"
            ;;
        mssql)
            DB_ENGINE="mssql"
            require_external_db
            DB_PORT="${DB_PORT:-1433}"
            ;;
        *)
            echo "error: invalid DB_MODE='${DB_MODE}' (expected: internal|postgres|mssql)." >&2
            exit 1
            ;;
    esac

    # Build the engine-specific .NET connection string and provider key.
    case "$DB_ENGINE" in
        postgres)
            DB_PROVIDER="PostgreSQL"
            DB_CONNECTION_STRING="Host=${DB_HOST};Port=${DB_PORT};Database=${DB_NAME};Username=${DB_USER};Password=${DB_PASSWORD};"
            ;;
        mssql)
            DB_PROVIDER="MSSQL"
            DB_CONNECTION_STRING="Server=${DB_HOST},${DB_PORT};Database=${DB_NAME};User Id=${DB_USER};Password=${DB_PASSWORD};TrustServerCertificate=True;"
            ;;
    esac

    export DB_MODE DB_ENGINE DB_HOST DB_PORT DB_NAME DB_USER DB_PASSWORD \
           DB_PROVIDER DB_CONNECTION_STRING

    # Layer in compose override files based on optional .env settings.
    COMPOSE=("${COMPOSE_BIN[@]}" -f docker-compose.yml)

    # Expose the internal postgres port to the host when DB_INTERNAL_EXPOSE_PORT
    # is set. Only meaningful for DB_MODE=internal — outside it the postgres
    # service isn't started, and the override file is a no-op.
    if [[ "$DB_MODE" == "internal" && -n "${DB_INTERNAL_EXPOSE_PORT:-}" ]]; then
        COMPOSE+=(-f docker-compose.postgres-expose.yml)
        export DB_INTERNAL_EXPOSE_PORT
        export DB_INTERNAL_EXPOSE_BIND="${DB_INTERNAL_EXPOSE_BIND:-127.0.0.1}"
    fi
}

require_external_db() {
    local missing=()
    [[ -z "${DB_HOST:-}" ]]     && missing+=(DB_HOST)
    [[ -z "${DB_NAME:-}" ]]     && missing+=(DB_NAME)
    [[ -z "${DB_USER:-}" ]]     && missing+=(DB_USER)
    [[ -z "${DB_PASSWORD:-}" ]] && missing+=(DB_PASSWORD)
    if (( ${#missing[@]} > 0 )); then
        echo "error: DB_MODE=${DB_MODE} but missing in .env: ${missing[*]}" >&2
        exit 1
    fi
}

# Profiles to activate for a given target. Internal mode adds 'postgres-internal'
# whenever 'server' is involved.
profile_args() {
    local target="$1"
    local -a args=()
    case "$target" in
        server)
            args+=(--profile server)
            [[ "$DB_MODE" == "internal" ]] && args+=(--profile postgres-internal)
            ;;
        wasm)
            args+=(--profile wasm)
            ;;
        all)
            args+=(--profile server --profile wasm)
            [[ "$DB_MODE" == "internal" ]] && args+=(--profile postgres-internal)
            ;;
    esac
    printf '%s\n' "${args[@]}"
}

# --- Helpers -----------------------------------------------------------------

require_target() {
    local cmd="$1" target="${2:-}"
    if [[ -z "$target" ]]; then
        die_usage "'$cmd' requires a target (server|wasm)."
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

postgres_internal_running() {
    local cid
    cid="$("${COMPOSE[@]}" ps -q postgres 2>/dev/null || true)"
    [[ -n "$cid" ]] && [[ "$(docker inspect -f '{{.State.Running}}' "$cid" 2>/dev/null)" == "true" ]]
}

# --- Engine-specific runners -------------------------------------------------
# Each runner takes one argument (the binary name like pg_dump / psql) plus
# extra args, and pipes stdin/stdout transparently.

# Postgres: internal → exec into the running compose container; external →
# spin up an ephemeral postgres-client container.
pg_run() {
    local bin="$1"; shift
    if [[ "$DB_MODE" == "internal" ]]; then
        if ! postgres_internal_running; then
            echo "error: internal postgres container is not running. Start it with: ./cli.sh start server" >&2
            exit 1
        fi
        "${COMPOSE[@]}" exec -T \
            -e PGPASSWORD="$DB_PASSWORD" \
            postgres "$bin" \
            -h "$DB_HOST" -p "$DB_PORT" \
            -U "$DB_USER" -d "$DB_NAME" "$@"
    else
        docker run --rm -i \
            --add-host=host.docker.internal:host-gateway \
            -e PGPASSWORD="$DB_PASSWORD" \
            "$PG_CLIENT_IMAGE" "$bin" \
            -h "$DB_HOST" -p "$DB_PORT" \
            -U "$DB_USER" -d "$DB_NAME" "$@"
    fi
}

# --- Commands ----------------------------------------------------------------

cmd_deploy() {
    local target="${1:-}"
    require_target deploy "$target"
    case "$target" in
        server|wasm)
            git_pull
            local -a profiles
            mapfile -t profiles < <(profile_args "$target")
            "${COMPOSE[@]}" "${profiles[@]}" build
            echo ">>> Starting '${target}' stack (db mode: ${DB_MODE})"
            "${COMPOSE[@]}" "${profiles[@]}" up -d
            ;;
        *)
            die_usage "unknown deploy target '$target' (expected: server|wasm)."
            ;;
    esac
}

cmd_start() {
    local target="${1:-}"
    require_target start "$target"
    case "$target" in
        server|wasm)
            local -a profiles
            mapfile -t profiles < <(profile_args "$target")
            "${COMPOSE[@]}" "${profiles[@]}" up -d
            ;;
        *)
            die_usage "unknown start target '$target' (expected: server|wasm)."
            ;;
    esac
}

cmd_stop() {
    local target="${1:-}"
    require_target stop "$target"
    case "$target" in
        server|wasm)
            local -a profiles
            mapfile -t profiles < <(profile_args "$target")
            "${COMPOSE[@]}" "${profiles[@]}" down
            ;;
        *)
            die_usage "unknown stop target '$target' (expected: server|wasm)."
            ;;
    esac
}

cmd_logs() {
    local target="${1:-}"
    require_target logs "$target"
    case "$target" in
        all)
            local -a profiles
            mapfile -t profiles < <(profile_args all)
            "${COMPOSE[@]}" "${profiles[@]}" logs -f --tail=200
            ;;
        server)
            local -a profiles
            mapfile -t profiles < <(profile_args server)
            local -a services=(server)
            [[ "$DB_MODE" == "internal" ]] && services+=(postgres)
            "${COMPOSE[@]}" "${profiles[@]}" logs -f --tail=200 "${services[@]}"
            ;;
        wasm)
            local -a profiles
            mapfile -t profiles < <(profile_args wasm)
            "${COMPOSE[@]}" "${profiles[@]}" logs -f --tail=200 wasm
            ;;
        *)
            die_usage "unknown logs target '$target' (expected: all|server|wasm)."
            ;;
    esac
}

cmd_db() {
    local sub="${1:-}"
    case "$sub" in
        backup)  db_backup ;;
        restore) shift; db_restore "$@" ;;
        "")
            die_usage "'db' requires a subcommand (backup|restore)."
            ;;
        *)
            die_usage "unknown db subcommand '$sub' (expected: backup|restore)."
            ;;
    esac
}

db_backup() {
    if [[ "$DB_ENGINE" == "mssql" ]]; then
        echo "error: 'db backup' is not implemented for DB_MODE=mssql." >&2
        echo "       Use SQL Server's native BACKUP DATABASE T-SQL or the SqlPackage tool instead." >&2
        exit 1
    fi

    mkdir -p "$BACKUP_DIR"
    local ts file
    ts="$(date +%Y%m%d-%H%M%S)"
    file="${BACKUP_DIR}/${ts}_maerp_${DB_ENGINE}.sql.gz"

    echo ">>> Dumping ${DB_ENGINE} '${DB_NAME}' from ${DB_HOST}:${DB_PORT} (mode: ${DB_MODE}) → ${file}"
    case "$DB_ENGINE" in
        postgres)
            if pg_run pg_dump | gzip -9 > "$file"; then
                echo ">>> Backup written: ${file} ($(du -h "$file" | cut -f1))"
            else
                rm -f "$file"
                echo "error: backup failed." >&2
                exit 1
            fi
            ;;
    esac
}

db_restore() {
    # Parse args: accept <file> and --clean in any order.
    local arg="" clean=0
    while [[ $# -gt 0 ]]; do
        case "$1" in
            --clean) clean=1 ;;
            -h|--help)
                echo "usage: ./cli.sh db restore <filename> [--clean]"
                echo "  <filename>  relative to ./backups/, or absolute path"
                echo "  --clean     drop & recreate the schema/database before restore"
                return 0
                ;;
            -*)
                die_usage "unknown flag '$1' for 'db restore' (expected: --clean)."
                ;;
            *)
                if [[ -n "$arg" ]]; then
                    die_usage "unexpected extra argument '$1' for 'db restore'."
                fi
                arg="$1"
                ;;
        esac
        shift
    done

    if [[ -z "$arg" ]]; then
        die_usage "'db restore' requires a filename (relative to ./backups/, or absolute)."
    fi

    if [[ "$DB_ENGINE" == "mssql" ]]; then
        echo "error: 'db restore' is not implemented for DB_MODE=mssql." >&2
        echo "       Use SQL Server's native RESTORE DATABASE T-SQL or the SqlPackage tool instead." >&2
        exit 1
    fi

    local file
    if [[ "$arg" = /* ]]; then
        file="$arg"
    else
        file="${BACKUP_DIR}/${arg}"
    fi

    if [[ ! -f "$file" ]]; then
        echo "error: backup file not found: ${file}" >&2
        exit 1
    fi

    if [[ $clean -eq 1 ]]; then
        echo ">>> --clean: dropping and recreating schema in '${DB_NAME}'"
        case "$DB_ENGINE" in
            postgres)
                pg_run psql -v ON_ERROR_STOP=1 <<SQL
SELECT pg_terminate_backend(pid)
  FROM pg_stat_activity
 WHERE datname = current_database()
   AND pid <> pg_backend_pid();
DROP SCHEMA IF EXISTS public CASCADE;
CREATE SCHEMA public;
GRANT ALL ON SCHEMA public TO ${DB_USER};
GRANT ALL ON SCHEMA public TO public;
SQL
                ;;
        esac
    fi

    echo ">>> Restoring ${file} into ${DB_ENGINE} '${DB_NAME}' on ${DB_HOST}:${DB_PORT} (mode: ${DB_MODE})"
    case "$DB_ENGINE" in
        postgres)
            if [[ "$file" == *.gz ]]; then
                gzip -dc "$file" | pg_run psql -v ON_ERROR_STOP=1
            else
                pg_run psql -v ON_ERROR_STOP=1 < "$file"
            fi
            ;;
    esac
    echo ">>> Restore complete."
}

main() {
    if [[ $# -lt 1 ]]; then
        die_usage "no command given (expected: deploy|start|stop|logs|db)."
    fi

    local command="$1"
    shift

    # `help` and friends bypass load_env so the help also works without a
    # configured .env (e.g. on a fresh checkout).
    case "$command" in
        -h|--help|help)
            usage
            return 0
            ;;
    esac

    load_env

    case "$command" in
        deploy) cmd_deploy "$@" ;;
        start)  cmd_start  "$@" ;;
        stop)   cmd_stop   "$@" ;;
        logs)   cmd_logs   "$@" ;;
        db)     cmd_db     "$@" ;;
        *)
            die_usage "unknown command '$command' (expected: deploy|start|stop|logs|db)."
            ;;
    esac
}

main "$@"
