#!/bin/bash

# Create SSL certificates directory if it doesn't exist
mkdir -p nginx/certs

# Check if certificates already exist
if [[ -f "nginx/certs/server.crt" && -f "nginx/certs/server.key" ]]; then
    echo "SSL certificates already exist in nginx/certs/"
    echo "To regenerate, delete the existing files and run this script again."
    exit 0
fi

# Generate self-signed certificate for development
echo "Generating self-signed SSL certificate for development..."

openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
    -keyout nginx/certs/server.key \
    -out nginx/certs/server.crt \
    -subj "/C=DE/ST=State/L=City/O=maERP/CN=localhost" \
    -addext "subjectAltName=DNS:localhost,DNS:127.0.0.1,IP:127.0.0.1"

echo "SSL certificate generated successfully!"
echo "Certificate: nginx/certs/server.crt"
echo "Private key: nginx/certs/server.key"
echo ""
echo "To use your own certificate, replace these files with your certificate and private key."