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
echo Building %ProjectName% MSIX package with integrated server...
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

REM Build maERP.Server first
echo Building maERP.Server...
if exist "%ProjectDir%%ServerProject%" (
    dotnet build "%ProjectDir%%ServerProject%" --configuration %Configuration%
    if !errorlevel! neq 0 (
        echo ERROR: maERP.Server build failed!
        pause
        exit /b 1
    )
    echo maERP.Server build completed successfully.
) else (
    echo WARNING: maERP.Server project not found at %ServerProject%
    echo The MSIX package will be created without the server component.
)

REM Create certificate if not exists
set "CertificateName=%ProjectName%_TemporaryKey"
set "CertificatePath=%ProjectDir%%CertificateName%.pfx"

if not exist "%CertificatePath%" (
    echo Creating self-signed certificate...
    powershell -Command "& { $Cert = New-SelfSignedCertificate -Type Custom -Subject 'CN=%CertificateName%' -KeyUsage DigitalSignature -FriendlyName '%ProjectName% Development Certificate' -CertStoreLocation 'Cert:\CurrentUser\My' -NotAfter (Get-Date).AddYears(3); $CertPassword = ConvertTo-SecureString -String 'password' -Force -AsPlainText; Export-PfxCertificate -Cert $Cert -FilePath '%CertificatePath%' -Password $CertPassword }"
    if !errorlevel! neq 0 (
        echo ERROR: Failed to create certificate!
        pause
        exit /b 1
    )
    echo Certificate created successfully.
)

REM Build UI project
echo Building maERP.Server.UI...
dotnet build "%ProjectDir%%ProjectName%.csproj" --configuration %Configuration% --platform %Platform%
if !errorlevel! neq 0 (
    echo ERROR: UI Build failed!
    pause
    exit /b 1
)

REM Create MSIX package
echo Creating MSIX package...
msbuild "%ProjectDir%%ProjectName%.csproj" /p:Configuration=%Configuration% /p:Platform=%Platform% /p:AppxBundle=Always /p:UapAppxPackageBuildMode=StoreUpload
if !errorlevel! neq 0 (
    echo ERROR: MSIX packaging failed!
    pause
    exit /b 1
)

echo.
echo === Build completed successfully! ===
echo MSIX package includes:
echo - maERP.Server.UI (Avalonia GUI)
echo - maERP.Server (Backend Binary)
echo - Automatic server management
echo.
echo MSIX package location: %ProjectDir%bin\%Platform%\%Configuration%\%ProjectName%_%Platform%.msix
echo.
echo To install:
echo 1. Install the certificate: %CertificatePath%
echo 2. Install the MSIX package
echo 3. The server will start automatically when the UI is launched
echo.
pause
    echo MSIX package created successfully!
    echo Package location: %OutputPath%\%ProjectName%.msix
) else (
    echo MSIX package creation failed!
    exit /b 1
)

echo === Build completed successfully ===
pause 