# MSIX Packaging für maERP.Server.UI

Diese Anleitung erklärt, wie Sie einen MSIX Installer für das maERP.Server.UI Projekt erstellen.

## Voraussetzungen

1. **Windows 10/11** (Build 17763 oder höher)
2. **Visual Studio 2022** mit Windows App SDK Workload
3. **Windows SDK** (Version 10.0.22621.0 oder höher)
4. **PowerShell** (als Administrator ausführen)

## Schnellstart

### 1. PowerShell als Administrator öffnen

```powershell
# Zum Projektverzeichnis wechseln
cd "D:\martin-andrich.de\maERP\src\maERP.Server.UI"

# MSIX-Paket erstellen (mit automatischer Zertifikatserstellung)
.\build-msix.ps1 -CreateCertificate
```

### 2. Alternative: Manueller Build

```powershell
# Projekt bauen
dotnet build maERP.Server.UI.csproj --configuration Release --platform x64

# Zertifikat erstellen (falls nicht vorhanden)
$Cert = New-SelfSignedCertificate -Type Custom -Subject "CN=maERP.Server.UI_TemporaryKey" -KeyUsage DigitalSignature -FriendlyName "maERP.Server.UI Development Certificate" -CertStoreLocation "Cert:\CurrentUser\My" -NotAfter (Get-Date).AddYears(3)
$CertPassword = ConvertTo-SecureString -String "password" -Force -AsPlainText
Export-PfxCertificate -Cert $Cert -FilePath "maERP.Server.UI_TemporaryKey.pfx" -Password $CertPassword
```

## PowerShell-Skript Parameter

Das `build-msix.ps1` Skript unterstützt folgende Parameter:

- `-Configuration`: Build-Konfiguration (Default: "Release")
- `-Platform`: Zielplattform (Default: "x64")
- `-CreateCertificate`: Erstellt ein neues Zertifikat
- `-Clean`: Führt einen Clean-Build durch

### Beispiele:

```powershell
# Release Build mit neuem Zertifikat
.\build-msix.ps1 -CreateCertificate

# Debug Build für x86
.\build-msix.ps1 -Configuration Debug -Platform x86

# Clean Build
.\build-msix.ps1 -Clean -CreateCertificate
```

## Projektkonfiguration

### maERP.Server.UI.csproj

Die wichtigsten MSIX-spezifischen Einstellungen:

```xml
<!-- MSIX Packaging Configuration -->
<GenerateAppInstallerFile>False</GenerateAppInstallerFile>
<AppxPackageSigningEnabled>True</AppxPackageSigningEnabled>
<AppxAutoIncrementPackageRevision>True</AppxAutoIncrementPackageRevision>
<AppxBundle>Always</AppxBundle>

<!-- Application Identity -->
<ApplicationDisplayName>maERP Server UI</ApplicationDisplayName>
<ApplicationDescription>maERP Server UI - Cross-platform ERP System</ApplicationDescription>
<ApplicationPublisher>maERP</ApplicationPublisher>
<ApplicationVersion>1.0.0.0</ApplicationVersion>
<ApplicationId>maERP.Server.UI</ApplicationId>
```

### Package.appxmanifest

Definiert die App-Identität und Capabilities:

```xml
<Identity Name="maERP.Server.UI"
          Publisher="CN=maERP"
          Version="1.0.0.0" />
```

## Installation und Deployment

### 1. Lokale Installation

```powershell
# MSIX-Paket installieren
Add-AppxPackage -Path ".\bin\Release\net9.0-windows10.0.19041.0\x64\AppPackages\maERP.Server.UI\maERP.Server.UI.msix"
```

### 2. Deinstallation

```powershell
# App entfernen
Get-AppxPackage -Name "maERP.Server.UI" | Remove-AppxPackage
```

### 3. Zertifikat installieren

```powershell
# Zertifikat in Trusted Root Certificate Store installieren
Import-Certificate -FilePath "maERP.Server.UI_TemporaryKey.pfx" -CertStoreLocation Cert:\LocalMachine\Root
```

## Troubleshooting

### Häufige Probleme:

1. **"MakeAppx.exe not found"**
   - Windows SDK installieren
   - Visual Studio Installer verwenden

2. **"Certificate not trusted"**
   - Zertifikat in Trusted Root Certificate Store installieren
   - Oder: `-CreateCertificate` Parameter verwenden

3. **"App installation failed"**
   - PowerShell als Administrator ausführen
   - Zertifikat vertrauen: `Set-ExecutionPolicy -ExecutionPolicy RemoteSigned`

### Debug-Informationen:

```powershell
# App-Pakete auflisten
Get-AppxPackage -Name "maERP.Server.UI"

# Zertifikate prüfen
Get-ChildItem Cert:\LocalMachine\Root | Where-Object {$_.Subject -like "*maERP*"}
```

## Produktions-Deployment

Für Produktionsumgebungen:

1. **Code-Signing-Zertifikat** von einer vertrauenswürdigen CA verwenden
2. **Microsoft Store** für automatische Updates
3. **Enterprise Deployment** über Intune oder SCCM
4. **App Installer** für einfache Installation

## Weitere Ressourcen

- [MSIX Packaging Documentation](https://docs.microsoft.com/en-us/windows/msix/)
- [Windows App SDK](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/)
- [App Installer](https://docs.microsoft.com/en-us/windows/msix/app-installer/) 