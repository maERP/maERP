@echo off
setlocal enabledelayedexpansion

echo === maERP.Server.UI MSIX Build Script ===

REM Projektverzeichnis und Name
set "ProjectDir=%~dp0"
set "ProjectName=maERP.Server.UI"
set "CertificateName=%ProjectName%_TemporaryKey"

REM Parameter verarbeiten
set "Configuration=Release"
set "Platform=x64"
set "CreateCertificate=false"
set "Clean=false"

:parse_args
if "%1"=="" goto :main
if "%1"=="-Configuration" (
    set "Configuration=%2"
    shift
    shift
    goto :parse_args
)
if "%1"=="-Platform" (
    set "Platform=%2"
    shift
    shift
    goto :parse_args
)
if "%1"=="-CreateCertificate" (
    set "CreateCertificate=true"
    shift
    goto :parse_args
)
if "%1"=="-Clean" (
    set "Clean=true"
    shift
    goto :parse_args
)
shift
goto :parse_args

:main
echo Configuration: %Configuration%
echo Platform: %Platform%
echo Create Certificate: %CreateCertificate%
echo Clean Build: %Clean%
echo.

REM Clean-Build wenn gewünscht
if "%Clean%"=="true" (
    echo Cleaning previous builds...
    dotnet clean "%ProjectDir%%ProjectName%.csproj" --configuration %Configuration%
    if exist "%ProjectDir%bin" rmdir /s /q "%ProjectDir%bin"
    if exist "%ProjectDir%obj" rmdir /s /q "%ProjectDir%obj"
)

REM Zertifikat erstellen falls gewünscht oder nicht vorhanden
set "CertificatePath=%ProjectDir%%CertificateName%.pfx"
if "%CreateCertificate%"=="true" (
    if not exist "%CertificatePath%" (
        echo Creating self-signed certificate...
        powershell -Command "& { $Cert = New-SelfSignedCertificate -Type Custom -Subject 'CN=%CertificateName%' -KeyUsage DigitalSignature -FriendlyName '%ProjectName% Development Certificate' -CertStoreLocation 'Cert:\CurrentUser\My' -NotAfter (Get-Date).AddYears(3); $CertPassword = ConvertTo-SecureString -String 'password' -Force -AsPlainText; Export-PfxCertificate -Cert $Cert -FilePath '%CertificatePath%' -Password $CertPassword }"
        if !errorlevel! neq 0 (
            echo Certificate creation failed!
            exit /b 1
        )
        echo Certificate created: %CertificatePath%
    )
)

REM Projekt bauen
echo Building project...
dotnet build "%ProjectDir%%ProjectName%.csproj" --configuration %Configuration% --platform %Platform%

if %errorlevel% neq 0 (
    echo Build failed!
    exit /b 1
)

REM MSIX-Paket erstellen
echo Creating MSIX package...
set "OutputPath=%ProjectDir%bin\%Configuration%\net9.0-windows10.0.19041.0\%Platform%\AppPackages\%ProjectName%"

REM MakeAppx.exe verwenden um das MSIX-Paket zu erstellen
set "MakeAppxPath=%ProgramFiles(x86)%\Windows Kits\10\bin\10.0.22621.0\%Platform%\MakeAppx.exe"
if exist "%MakeAppxPath%" (
    "%MakeAppxPath%" pack /d "%OutputPath%" /p "%OutputPath%\%ProjectName%.msix"
) else (
    echo MakeAppx.exe not found. Please install Windows SDK.
    exit /b 1
)

if %errorlevel% equ 0 (
    echo MSIX package created successfully!
    echo Package location: %OutputPath%\%ProjectName%.msix
) else (
    echo MSIX package creation failed!
    exit /b 1
)

echo === Build completed successfully ===
pause 