#!/bin/sh

# Erstelle ein JavaScript-File mit der Server-URL
mkdir -p /usr/share/nginx/html/js
cat > /usr/share/nginx/html/js/config.js << EOF
window.maERP = window.maERP || {};
window.maERP.serverUrl = "${MAERP_SERVER_BASE_URL}";
EOF

# Starte NGINX
exec "$@" 