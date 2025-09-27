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

## Schnellstart: maERP.Server
Der Server bildet das Herzstück von maERP und stellt alle Funktionen über eine gesicherte REST-API bereit.

**Standard-Zugangsdaten**
- E-Mail: `admin@localhost.com`
- Passwort: `P@ssword1`

**Lokal ohne Docker ausführen**
1. .NET 9 SDK installieren.
2. Im Projektverzeichnis die Abhängigkeiten laden: `dotnet restore`
3. Datenbankkonfiguration in der Datei `src/maERP.Server/appsettings.json` vornehmen.
4. Server starten: `dotnet run --project src/maERP.Server/maERP.Server.csproj`
5. Die API ist anschließend unter `https://localhost:5001` erreichbar (Swagger: `/swagger`).

### Docker-Beispiele
Alle Beispiele veröffentlichen den Server auf Port `8080` und setzen die erforderlichen Datenbankvariablen. Ersetze Platzhalter wie Pfade oder ConnectionStrings passend zu Deiner Umgebung.

**SQLite (built in)**
```bash
docker run -d \
  --name maerp-server-sqlite \
  -p 8080:80 \
  -e DatabaseConfig__Provider=SQLite \
  -e DatabaseConfig__ConnectionString="Data Source=/data/maerp.db" \
  -v maerp-sqlite-data:/data \
  maerp/server:latest
```

**MySQL / MariaDB (extern)**
```bash
docker run -d \
  --name maerp-server-mysql \
  -p 8080:80 \
  -e DatabaseConfig__Provider=MySQL \
  -e DatabaseConfig__ConnectionString="Server=mysql-host;Port=3306;Database=maerp;Uid=maerp;Pwd=maerp;" \
  maerp/server:latest
```

**PostgreSQL (extern)**
```bash
docker run -d \
  --name maerp-server-postgres \
  -p 8080:80 \
  -e DatabaseConfig__Provider=PostgreSQL \
  -e DatabaseConfig__ConnectionString="Host=postgres-host;Port=5432;Database=maerp;Username=maerp;Password=maerp;" \
  maerp/server:latest
```

**Microsoft SQL Server (extern)**
```bash
docker run -d \
  --name maerp-server-sqlserver \
  -p 8080:80 \
  -e DatabaseConfig__Provider=MSSQL \
  -e DatabaseConfig__ConnectionString="Server=sqlserver-host;Database=maerp;User Id=maerp;Password=maerp;TrustServerCertificate=True;" \
  maerp/server:latest
```

## Nützliche Ressourcen
- GitHub-Repository: https://github.com/maERP/maERP
- Docker Hub: https://hub.docker.com/u/maerp
- Live-Frontend zur Benutzung mit beliebigen maERP.Server-Instanzen: https://www.maerp.de