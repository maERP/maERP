#!/bin/sh
#
# Generates /usr/share/nginx/html/config.json from environment variables.
# Picked up automatically by the official nginx image — every executable in
# /docker-entrypoint.d/ runs once before nginx starts.
#
# The WASM client fetches /config.json on launch and applies the values.

set -eu

CONFIG_PATH="/usr/share/nginx/html/config.json"

# JSON-escape a value: replace backslashes and double quotes.
escape_json() {
    printf '%s' "$1" | sed -e 's/\\/\\\\/g' -e 's/"/\\"/g'
}

restrict="$(escape_json "${MAERP_RESTRICT_SERVER_URL:-}")"

cat > "$CONFIG_PATH" <<EOF
{
  "restrictServerUrl": "${restrict}"
}
EOF

echo "[maerp-wasm] wrote ${CONFIG_PATH}: restrictServerUrl='${MAERP_RESTRICT_SERVER_URL:-}'"
