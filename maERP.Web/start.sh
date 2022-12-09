#!/bin/sh

jq '.ServerBaseUrl = "$MAERP_SERVER_BASE_URLI"' "$WWWROOT/appsettings.json" | sponge "$WWWROOT/appsettings.json"
jq '.RemoteLog = $MAERP_REMOTE_LOG' "$WWWROOT/appsettings.json" | sponge "$WWWROOT/appsettings.json"