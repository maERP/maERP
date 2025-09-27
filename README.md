**Wichtiger Hinweis:** maERP ist keine schlüsselfertige ERP-Lösung für Endkund:innen und darf nicht ohne eigene Anpassungen im Livebetrieb eingesetzt werden. Das Projekt dient als technische Basis, auf der eigene, individuelle Unternehmenslösungen aufgebaut werden können.

# maERP

maERP ist ein kostenloses, Open-Source-ERP-System mit mandantenfähiger Architektur. Die Plattform richtet sich an Teams, die eine moderne Grundlage für eigene ERP-Erweiterungen, Integrationen und Benutzeroberflächen suchen.

## Was maERP bietet
- Mandantenisolierte REST-API für Kunden-, Produkt-, Lager- und Auftragsverwaltung
- Module für Angebote, Bestellungen, Rechnungen und Warenzugänge inklusive Nachverfolgung
- Cross-Plattform-Oberflächen auf Basis von Avalonia unterstützen Desktop- und Mobile-Clients
- Anbindung externer Vertriebskanäle und Versanddienstleister über konfigurierbare Schnittstellen
- Auswertungen zu Verkäufen, Produkten und Lagerbewegungen zur schnellen Entscheidungsfindung
- KI-gestützte Funktionen (z. B. Prompt-Verwaltung) zur Automatisierung wiederkehrender Aufgaben
- Kostenfrei, quelloffen und erweiterbar

## Für wen eignet sich maERP?
maERP adressiert Unternehmen, Integrator:innen und IT-Dienstleister, die ein funktionsreiches Fundament benötigen, um eigene ERP-Prozesse zu digitalisieren. Durch die klare Trennung von Server, UI-Clients und Integrationen bleibt das System flexibel genug, um branchenspezifische Anforderungen abzubilden. Umfangreiche automatische Tests (rund 1.500 Szenarien) sorgen für eine hohe Abdeckung von Kernprozessen und erleichtern kontinuierliche Weiterentwicklungen.

# Installation

maERP bietet verschiedene Installationsmöglichkeiten für unterschiedliche Anwendungsfälle und Infrastrukturen.

**Standard-Zugangsdaten**
- E-Mail: `admin@localhost.com`
- Passwort: `P@ssword1`

## 1. Installation per Docker Compose

Die einfachste Art, maERP mit einer vollständigen Umgebung zu starten. Alle benötigten Services (Server, Web-UI, Datenbank, Monitoring) werden automatisch konfiguriert und gestartet.

### Voraussetzungen
- Docker und Docker Compose installiert
- Git für das Klonen des Repositories

### SQLite (Standard, keine externe Datenbank erforderlich)
```bash
# Repository klonen
git clone https://github.com/maERP/maERP.git
cd maERP

# SQLite-Version starten
docker-compose up -d

# Mit Development-Einstellungen
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

### MariaDB
```bash
# MariaDB-Version starten
docker-compose -f docker-compose.mysql.yml up -d

# Mit Development-Einstellungen
docker-compose -f docker-compose.mysql.yml -f docker-compose.mysql.override.yml up -d
```

### PostgreSQL
```bash
# PostgreSQL-Version starten
docker-compose -f docker-compose.postgresql.yml up -d

# Mit Development-Einstellungen
docker-compose -f docker-compose.postgresql.yml -f docker-compose.postgresql.override.yml up -d
```

### Microsoft SQL Server
```bash
# MSSQL-Version starten
docker-compose -f docker-compose.mssql.yml up -d

# Mit Development-Einstellungen
docker-compose -f docker-compose.mssql.yml -f docker-compose.mssql.override.yml up -d
```

### Zugriff auf die Services
- **maERP Server API**: `https://localhost:8443` (Swagger: `/swagger`)
- **maERP Web UI**: `https://localhost:8444`
- **Grafana Agent**: `http://localhost:12345`
- **Datenbank-Ports** (Development):
  - MariaDB: `localhost:3307`
  - PostgreSQL: `localhost:5433`
  - MSSQL: `localhost:1434`

## 2. Installation mit externem Datenbank-Server

Für Produktionsumgebungen oder bei bereits vorhandener Datenbankinfrastruktur.

### Lokale Installation ohne Docker
1. .NET 9 SDK installieren
2. Repository klonen: `git clone https://github.com/maERP/maERP.git`
3. Abhängigkeiten laden: `dotnet restore`
4. Datenbankkonfiguration in `src/maERP.Server/appsettings.json` anpassen
5. Server starten: `dotnet run --project src/maERP.Server/maERP.Server.csproj`
6. API verfügbar unter: `https://localhost:5001` (Swagger: `/swagger`)

### Docker mit externer Datenbank
Alle Beispiele veröffentlichen den Server auf Port `8080`. Ersetze die Verbindungsparameter entsprechend Deiner Datenbankumgebung.

**SQLite (eingebaute Datei-Datenbank)**
```bash
docker run -d \
  --name maerp-server-sqlite \
  -p 8080:80 \
  -e DatabaseConfig__Provider=SQLite \
  -e DatabaseConfig__ConnectionString="Data Source=/data/maerp.db" \
  -v maerp-sqlite-data:/data \
  maerp/server:latest
```

**MySQL / MariaDB (externe Datenbank)**
```bash
docker run -d \
  --name maerp-server-mysql \
  -p 8080:80 \
  -e DatabaseConfig__Provider=MySQL \
  -e DatabaseConfig__ConnectionString="Server=mysql-host;Port=3306;Database=maerp;Uid=maerp;Pwd=maerp;" \
  maerp/server:latest
```

**PostgreSQL (externe Datenbank)**
```bash
docker run -d \
  --name maerp-server-postgres \
  -p 8080:80 \
  -e DatabaseConfig__Provider=PostgreSQL \
  -e DatabaseConfig__ConnectionString="Host=postgres-host;Port=5432;Database=maerp;Username=maerp;Password=maerp;" \
  maerp/server:latest
```

**Microsoft SQL Server (externe Datenbank)**
```bash
docker run -d \
  --name maerp-server-sqlserver \
  -p 8080:80 \
  -e DatabaseConfig__Provider=MSSQL \
  -e DatabaseConfig__ConnectionString="Server=sqlserver-host;Database=maerp;User Id=maerp;Password=maerp;TrustServerCertificate=True;" \
  maerp/server:latest
```

## 3. Installation auf Synology NAS

maERP lässt sich optimal auf Synology NAS-Systemen über den Container Manager betreiben.

### Voraussetzungen
- Synology NAS mit DSM 7.0 oder höher
- Container Manager (ehemals Docker) aus dem Paket-Zentrum installiert
- Mindestens 2 GB RAM empfohlen

### Installation über Container Manager GUI

#### Schritt 1: Projekt erstellen
1. Container Manager öffnen → **Projekt** → **Erstellen**
2. Projektname: `maerp`
3. Pfad wählen (z.B. `/docker/maerp`)

#### Schritt 2: Docker Compose Datei konfigurieren
Wähle eine der folgenden Konfigurationen je nach gewünschter Datenbank:

**Für SQLite (empfohlen für kleine bis mittlere Installationen):**
```yaml
name: maerp

services:
  maerp.server:
    image: maerp/server:latest
    container_name: maerp-server
    environment:
      - DatabaseConfig__Provider=Sqlite
      - DatabaseConfig__ConnectionString=Data Source=/app/data/maerp.db
    ports:
      - "8080:80"
      - "8443:443"
    volumes:
      - ./data:/app/data
    restart: unless-stopped

  maerp.browser:
    image: maerp/browser:latest
    container_name: maerp-browser
    environment:
      - SERVER_URL=https://maerp.server:443
    ports:
      - "8081:80"
      - "8444:443"
    depends_on:
      - maerp.server
    restart: unless-stopped
```

**Für PostgreSQL (empfohlen für größere Installationen):**
```yaml
name: maerp-postgresql

services:
  maerp.server:
    image: maerp/server:latest
    container_name: maerp-server
    environment:
      - DatabaseConfig__Provider=PostgreSQL
      - DatabaseConfig__ConnectionString=Host=postgresql;Port=5432;Database=maerp_01;Username=maerp;Password=maerp;
    ports:
      - "8080:80"
      - "8443:443"
    depends_on:
      postgresql:
        condition: service_healthy
    restart: unless-stopped

  maerp.browser:
    image: maerp/browser:latest
    container_name: maerp-browser
    environment:
      - SERVER_URL=https://maerp.server:443
    ports:
      - "8081:80"
      - "8444:443"
    depends_on:
      - maerp.server
    restart: unless-stopped

  postgresql:
    image: postgres:16-alpine
    container_name: maerp-postgres
    environment:
      POSTGRES_DB: maerp_01
      POSTGRES_USER: maerp
      POSTGRES_PASSWORD: maerp
      PGDATA: /var/lib/postgresql/data/pgdata
    volumes:
      - ./postgres-data:/var/lib/postgresql/data
    restart: unless-stopped
    healthcheck:
      test: ["CMD-SHELL", "pg_isready -U maerp -d maerp_01"]
      interval: 10s
      timeout: 5s
      retries: 5
```

#### Schritt 3: Projekt starten
1. **Erstellen** klicken
2. Warten bis alle Container heruntergeladen und gestartet sind
3. Status im Container Manager überprüfen

#### Schritt 4: Zugriff konfigurieren

**Lokaler Zugriff:**
- Server-API: `http://[NAS-IP]:8080` oder `https://[NAS-IP]:8443`
- Web-UI: `http://[NAS-IP]:8081` oder `https://[NAS-IP]:8444`

**Externe Zugriffe (optional):**
1. DSM Systemsteuerung → **Externe Zugriffe** → **Router-Konfiguration**
2. Ports weiterleiten: 8080, 8443 (Server), 8081, 8444 (Web-UI)
3. Oder Reverse Proxy in DSM konfigurieren

### Installation über SSH (Fortgeschrittene)

```bash
# Per SSH auf das NAS verbinden
ssh admin@[NAS-IP]

# In Docker-Verzeichnis wechseln
cd /volume1/docker

# maERP-Verzeichnis erstellen
sudo mkdir maerp && cd maerp

# Docker Compose Datei erstellen (SQLite-Beispiel)
sudo tee docker-compose.yml << 'EOF'
name: maerp

services:
  maerp.server:
    image: maerp/server:latest
    container_name: maerp-server
    environment:
      - DatabaseConfig__Provider=Sqlite
      - DatabaseConfig__ConnectionString=Data Source=/app/data/maerp.db
    ports:
      - "8080:80"
      - "8443:443"
    volumes:
      - ./data:/app/data
    restart: unless-stopped

  maerp.browser:
    image: maerp/browser:latest
    container_name: maerp-browser
    environment:
      - SERVER_URL=https://maerp.server:443
    ports:
      - "8081:80"
      - "8444:443"
    depends_on:
      - maerp.server
    restart: unless-stopped
EOF

# Container starten
sudo docker-compose up -d

# Status prüfen
sudo docker-compose ps
```

### Tipps für Synology NAS
- **Automatischer Start**: Container werden automatisch mit dem NAS gestartet
- **Updates**: Über Container Manager → Projekt → **Aktion** → **Erstellen**
- **Backups**: Daten-Ordner in Synology Backup-Routine einbeziehen
- **Monitoring**: Task Scheduler für Health-Checks nutzen
- **Ressourcen**: Bei mehreren Containern CPU/RAM-Limits setzen

### Troubleshooting
- **Port-Konflikte**: Andere Ports verwenden (z.B. 9080 statt 8080)
- **Berechtigungen**: Ordner-Berechtigungen für Docker-Volumes prüfen
- **Logs**: Container Manager → Container → **Details** → **Terminal** → **Logs**

## Nützliche Ressourcen
- GitHub-Repository: https://github.com/maERP/maERP
- Docker Hub: https://hub.docker.com/u/maerp
- Live-Frontend zur Benutzung mit beliebigen maERP.Server-Instanzen: https://www.maerp.de