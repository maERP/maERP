# MSIX Build Script für maERP.Server.UI
# Führen Sie dieses Skript als Administrator aus

param(
    [string]$Configuration = "Release",
    [string]$Platform = "x64",
    [switch]$CreateCertificate = $false,
    [switch]$Clean = $false
)

Write-Host "=== maERP.Server.UI MSIX Build Script ===" -ForegroundColor Green

# Projektverzeichnis
$ProjectDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ProjectName = "maERP.Server.UI"
$CertificateName = "$ProjectName_TemporaryKey"

# Clean-Build wenn gewünscht
if ($Clean) {
    Write-Host "Cleaning previous builds..." -ForegroundColor Yellow
    dotnet clean $ProjectDir\$ProjectName.csproj --configuration $Configuration
    Remove-Item -Path "$ProjectDir\bin" -Recurse -Force -ErrorAction SilentlyContinue
    Remove-Item -Path "$ProjectDir\obj" -Recurse -Force -ErrorAction SilentlyContinue
}

# Zertifikat erstellen falls gewünscht oder nicht vorhanden
$CertificatePath = "$ProjectDir\$CertificateName.pfx"
if ($CreateCertificate -or -not (Test-Path $CertificatePath)) {
    Write-Host "Creating self-signed certificate..." -ForegroundColor Yellow
    
    # Zertifikat erstellen
    $Cert = New-SelfSignedCertificate -Type Custom -Subject "CN=$CertificateName" -KeyUsage DigitalSignature -FriendlyName "$ProjectName Development Certificate" -CertStoreLocation "Cert:\CurrentUser\My" -NotAfter (Get-Date).AddYears(3)
    
    # Zertifikat als PFX exportieren
    $CertPassword = ConvertTo-SecureString -String "password" -Force -AsPlainText
    Export-PfxCertificate -Cert $Cert -FilePath $CertificatePath -Password $CertPassword
    
    Write-Host "Certificate created: $CertificatePath" -ForegroundColor Green
}

# maERP.Server zuerst bauen
Write-Host "Building maERP.Server..." -ForegroundColor Yellow
$ServerProjectPath = "$ProjectDir\..\maERP.Server\maERP.Server.csproj"
if (Test-Path $ServerProjectPath) {
    dotnet build $ServerProjectPath --configuration $Configuration
    if ($LASTEXITCODE -ne 0) {
        Write-Host "maERP.Server build failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "maERP.Server build completed successfully" -ForegroundColor Green
} else {
    Write-Host "Warning: maERP.Server project not found at $ServerProjectPath" -ForegroundColor Yellow
}

# UI-Projekt bauen
Write-Host "Building maERP.Server.UI..." -ForegroundColor Yellow
dotnet build $ProjectDir\$ProjectName.csproj --configuration $Configuration --platform $Platform

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed!" -ForegroundColor Red
    exit 1
}

# MSIX-Paket erstellen
Write-Host "Creating MSIX package..." -ForegroundColor Yellow
$OutputPath = "$ProjectDir\bin\$Configuration\net9.0-windows10.0.19041.0\$Platform\AppPackages\$ProjectName"

# MakeAppx.exe verwenden um das MSIX-Paket zu erstellen
$MakeAppxPath = "${env:ProgramFiles(x86)}\Windows Kits\10\bin\10.0.22621.0\x64\MakeAppx.exe"
if (Test-Path $MakeAppxPath) {
    & $MakeAppxPath pack /d "$OutputPath" /p "$OutputPath\$ProjectName.msix"
} else {
    Write-Host "MakeAppx.exe not found. Please install Windows SDK." -ForegroundColor Red
    exit 1
}

if ($LASTEXITCODE -eq 0) {
    Write-Host "MSIX package created successfully!" -ForegroundColor Green
    Write-Host "Package location: $OutputPath\$ProjectName.msix" -ForegroundColor Cyan
} else {
    Write-Host "MSIX package creation failed!" -ForegroundColor Red
    exit 1
}

Write-Host "=== Build completed successfully ===" -ForegroundColor Green 