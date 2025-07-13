# maERP Server UI mit integriertem Server - MSIX Installation

Dieses Dokument beschreibt die erweiterte MSIX-Installation von maERP.Server.UI, die automatisch den maERP.Server mit installiert und startet.

## Überblick

Die maERP.Server.UI MSIX-Installation enthält jetzt:
- **maERP.Server.UI**: Die Avalonia-basierte Benutzeroberfläche
- **maERP.Server**: Das Backend-Binary als eigenständiger Prozess  
- **Automatisches Server-Management**: Start, Stop und Neustart des Servers
- **System Tray Integration**: Minimiert in die Taskleiste mit Server-Kontrolle

## Features

### Automatischer Server-Start
- Der maERP.Server wird automatisch beim Start der UI gestartet
- Standard-Port: 5000 (http://localhost:5000)
- Läuft als separater Prozess im Hintergrund

### System Tray Integration
Die Anwendung läuft im System Tray und bietet folgende Optionen:
- **Show Window**: Hauptfenster anzeigen
- **Hide Window**: Hauptfenster ausblenden  
- **Server Status**: Zeigt aktuellen Server-Status an
- **Restart Server**: Neustart des Server-Prozesses
- **Exit**: Beendet UI und Server

### Server-Management
Der `ServerManager` kümmert sich um:
- Automatisches Finden des Server-Binaries
- Starten des Servers mit korrekten Parametern
- Überwachung des Server-Status
- Sauberes Beenden beim App-Exit

## Build-Prozess

### Automatisches Build
Das erweiterte Build-System:
1. Baut zuerst das `maERP.Server` Projekt
2. Kopiert das Server-Binary ins MSIX-Paket
3. Baut dann die `maERP.Server.UI`
4. Erstellt das MSIX-Paket mit beiden Komponenten

### Build-Skript ausführen
```powershell
# MSIX-Paket erstellen (erfordert Admin-Rechte)
.\build-msix.ps1

# Mit Clean-Build und Zertifikat-Erstellung
.\build-msix.ps1 -Clean -CreateCertificate

# Spezifische Konfiguration
.\build-msix.ps1 -Configuration Release -Platform x64
```

## Installation

### Voraussetzungen
- Windows 10 Version 1809 oder höher
- Developer Mode aktiviert ODER signiertes Zertifikat installiert

### Installation des MSIX-Pakets
1. MSIX-Paket erstellen mit `build-msix.ps1`
2. Zertifikat installieren (bei selbst-signierten Paketen)
3. MSIX-Paket installieren durch Doppelklick

### Zertifikat-Installation (nur bei selbst-signiert)
```powershell
# Zertifikat in Trusted Root installieren
certlm.msc
# Navigiere zu "Trusted Root Certification Authorities" > "Certificates"  
# Importiere das .pfx Zertifikat
```

## Verwendung

### Nach der Installation
1. Die App startet automatisch minimiert im System Tray
2. Der maERP.Server wird automatisch im Hintergrund gestartet
3. Über das Tray-Icon können Sie die App und den Server verwalten

### Server-Zugriff
- Standard-URL: http://localhost:5000
- API-Endpoints verfügbar unter `/api/`
- Swagger-Dokumentation (falls aktiviert): http://localhost:5000/swagger

### Troubleshooting

#### Server startet nicht
1. Prüfen Sie das Event Log für Fehlermeldungen
2. Stellen Sie sicher, dass Port 5000 nicht belegt ist
3. Verwenden Sie "Restart Server" im Tray-Menü

#### MSIX-Installation schlägt fehl
1. Stellen Sie sicher, dass das Zertifikat installiert ist
2. Aktivieren Sie den Developer Mode in Windows
3. Prüfen Sie die Windows Event Logs

#### Server-Binary nicht gefunden
Das System sucht das Server-Binary in folgenden Pfaden:
- `[AppDirectory]/Server/maERP.Server.exe`
- `[AppDirectory]/Server/maERP.Server/maERP.Server.exe`

## Konfiguration

### Server-Port ändern
Im `ServerManager.cs` können Sie den Standard-Port anpassen:
```csharp
public static async Task<bool> StartServerAsync(int port = 5000)
```

### Umgebungsvariablen
Der Server wird mit folgenden Umgebungsvariablen gestartet:
- `ASPNETCORE_ENVIRONMENT=Production`
- `ASPNETCORE_URLS=http://localhost:{port}`

## Logs und Debugging

### Server-Logs
- Server-Output wird in die Konsole umgeleitet
- Prüfen Sie die Windows Event Logs für Systemfehler

### UI-Logs  
- Avalonia-Logs werden in der Debug-Konsole angezeigt
- Tray-Icon Tooltip zeigt aktuellen Server-Status

## Deinstallation

1. Öffnen Sie Windows "Apps & Features"  
2. Suchen Sie nach "maERP Server UI"
3. Klicken Sie auf "Uninstall"
4. Das System entfernt automatisch alle Komponenten

## Erweiterte Konfiguration

### Custom Server-Konfiguration
Sie können eine `appsettings.json` im Server-Verzeichnis erstellen:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "your-connection-string"
  }
}
```

### Mehrere Server-Instanzen
Aktuell wird nur eine Server-Instanz unterstützt. Für mehrere Instanzen müssten separate UI-Anwendungen mit unterschiedlichen Ports konfiguriert werden.
