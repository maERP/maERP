# Process command line arguments
param (
    [string]$name,
    [string]$database = "all",
    [switch]$apply,
    [string]$connectionString,
    [switch]$help
)

# maERP Database Migration Tool
# PowerShell script to create and apply Entity Framework Core migrations

# Colors for output
$RED = [ConsoleColor]::Red
$GREEN = [ConsoleColor]::Green
$YELLOW = [ConsoleColor]::Yellow
$BLUE = [ConsoleColor]::Cyan
$NC = [ConsoleColor]::White

# Default values
$MIGRATION_NAME = ""
$DATABASE_TYPE = "all"
$APPLY_MIGRATION = $false
$STARTUP_PROJECT = "src/maERP.Server/maERP.Server.csproj"
$DB_CONTEXT = "maERP.Persistence.DatabaseContext.ApplicationDbContext"
$CONFIGURATION = "Debug"
$OUTPUT_DIR = "Migrations"
$APPSETTINGS_PATH = "src/maERP.Server/appsettings.Development.json"

# Function to display help
function Show-Help {
    Write-Host "maERP Database Migration Tool" -ForegroundColor $BLUE
    Write-Host ""
    Write-Host "Usage: .\create-migrations.ps1 [options]"
    Write-Host ""
    Write-Host "Options:"
    Write-Host "  -name NAME                     Name of the migration (required)"
    Write-Host "  -database TYPE                 Database type (mssql, postgresql, sqlite or all) [default: all]"
    Write-Host "  -apply                         Apply migrations after creation"
    Write-Host "  -connectionString STRING       Custom connection string for the selected database type"
    Write-Host "  -help                          Show this help"
    Write-Host ""
    Write-Host "Example: .\create-migrations.ps1 -name AddCustomerTable -database postgresql -apply"
    Write-Host "Example with custom connection: .\create-migrations.ps1 -name AddCustomerTable -database postgresql -connectionString `"Host=localhost;Port=5432;Database=mydb;Username=myuser;Password=mypass;`""
}

# Function to read connection strings from appsettings.json
function Read-ConnectionStrings {
    if (-not (Test-Path $APPSETTINGS_PATH)) {
        Write-Host "Error: appsettings.json not found at $APPSETTINGS_PATH" -ForegroundColor $RED
        exit 1
    }

    Write-Host "Reading connection strings from $APPSETTINGS_PATH..." -ForegroundColor $BLUE

    try {
        $appSettings = Get-Content -Path $APPSETTINGS_PATH -Raw | ConvertFrom-Json
        $MSSQL_CONNECTION = $appSettings.DatabaseConfig.ConnectionStrings.MSSQL
        $POSTGRESQL_CONNECTION = $appSettings.DatabaseConfig.ConnectionStrings.PostgreSQL
        $SQLITE_CONNECTION = $appSettings.DatabaseConfig.ConnectionStrings.SQLite
    }
    catch {
        Write-Host "Error: Failed to read connection strings from appsettings.json" -ForegroundColor $RED
        Write-Host "Using default connection strings..." -ForegroundColor $YELLOW

        # Default connection strings as fallback
        $MSSQL_CONNECTION = "Server=localhost;Database=maerp_migration;User Id=maerp;Password=maerp;TrustServerCertificate=True;"
        $POSTGRESQL_CONNECTION = "Host=localhost;Port=5432;Database=maerp_migration;Username=maerp;Password=maerp;"
        $SQLITE_CONNECTION = "Data Source=maerp_migration.db"
    }

    if ([string]::IsNullOrEmpty($MSSQL_CONNECTION) -or [string]::IsNullOrEmpty($POSTGRESQL_CONNECTION) -or [string]::IsNullOrEmpty($SQLITE_CONNECTION)) {
        Write-Host "Error: Failed to read connection strings from appsettings.json" -ForegroundColor $RED
        Write-Host "Using default connection strings..." -ForegroundColor $YELLOW

        # Default connection strings as fallback
        $MSSQL_CONNECTION = "Server=localhost;Database=maerp_migration;User Id=maerp;Password=maerp;TrustServerCertificate=True;"
        $POSTGRESQL_CONNECTION = "Host=localhost;Port=5432;Database=maerp_migration;Username=maerp;Password=maerp;"
        $SQLITE_CONNECTION = "Data Source=maerp_migration.db"
    }
    else {
        Write-Host "Connection strings successfully loaded from appsettings.json" -ForegroundColor $GREEN
    }

    # Create a hashtable to store connection strings
    $script:ConnectionStrings = @{
        "mssql" = $MSSQL_CONNECTION
        "postgresql" = $POSTGRESQL_CONNECTION
        "sqlite" = $SQLITE_CONNECTION
    }
}

# Check prerequisites
function Check-Prerequisites {
    Write-Host "Checking prerequisites..." -ForegroundColor $BLUE

    try {
        $dotnetVersion = dotnet --version
        Write-Host "Found .NET SDK version: $dotnetVersion" -ForegroundColor $GREEN
    }
    catch {
        Write-Host "dotnet is not installed. Please install the .NET SDK." -ForegroundColor $RED
        exit 1
    }

    $efTool = dotnet tool list -g | Select-String "dotnet-ef"
    if (-not $efTool) {
        Write-Host "dotnet-ef is not globally installed. Installing now..." -ForegroundColor $YELLOW
        dotnet tool install --global dotnet-ef
        if ($LASTEXITCODE -ne 0) {
            Write-Host "Failed to install dotnet-ef tool." -ForegroundColor $RED
            exit 1
        }

        Write-Host "Please ensure that the path to the dotnet tools is included in PATH." -ForegroundColor $YELLOW
    }
    else {
        # Update Entity Framework tools to latest version
        Write-Host "Updating Entity Framework tools to the latest version..." -ForegroundColor $BLUE
        dotnet tool update --global dotnet-ef
    }

    Write-Host "All prerequisites met." -ForegroundColor $GREEN
}

# Main function to create migration
function Create-Migration {
    param (
        [string]$DbType
    )

    $project = ""
    $dbProvider = ""
    $connectionString = ""

    switch ($DbType) {
        "mssql" {
            $project = "src/maERP.Persistence.MSSQL/maERP.Persistence.MSSQL.csproj"
            $dbProvider = "MSSQL"
            $connectionString = $ConnectionStrings["mssql"]
        }
        "postgresql" {
            $project = "src/maERP.Persistence.PostgreSQL/maERP.Persistence.PostgreSQL.csproj"
            $dbProvider = "POSTGRESQL"
            $connectionString = $ConnectionStrings["postgresql"]
        }
        "sqlite" {
            $project = "src/maERP.Persistence.SQLite/maERP.Persistence.SQLite.csproj"
            $dbProvider = "SQLITE"
            $connectionString = $ConnectionStrings["sqlite"]
        }
    }

    # Override with custom connection string if provided and we're operating on the selected database type
    if (-not [string]::IsNullOrEmpty($CUSTOM_CONNECTION_STRING) -and ($DATABASE_TYPE -eq $DbType -or $DATABASE_TYPE -eq "all")) {
        Write-Host "Using custom connection string for $DbType" -ForegroundColor $YELLOW
        $connectionString = $CUSTOM_CONNECTION_STRING
    }

    Write-Host "Creating migration for $DbType..." -ForegroundColor $BLUE
    Write-Host "Using connection: $connectionString" -ForegroundColor $YELLOW

    # Set environment variables to force the correct database provider and connection strings
    $env:DatabaseConfig__Provider = $dbProvider
    $env:DatabaseConfig__ConnectionStrings__MSSQL = $ConnectionStrings["mssql"]
    $env:DatabaseConfig__ConnectionStrings__POSTGRESQL = $ConnectionStrings["postgresql"]
    $env:DatabaseConfig__ConnectionStrings__SQLITE = $ConnectionStrings["sqlite"]

    # Override the specific connection string for the current database type
    switch ($DbType) {
        "mssql" { $env:DatabaseConfig__ConnectionStrings__MSSQL = $connectionString }
        "postgresql" { $env:DatabaseConfig__ConnectionStrings__POSTGRESQL = $connectionString }
        "sqlite" { $env:DatabaseConfig__ConnectionStrings__SQLITE = $connectionString }
    }

    $efParams = @(
        "ef", "migrations", "add", $MIGRATION_NAME,
        "--project", $project,
        "--startup-project", $STARTUP_PROJECT,
        "--context", $DB_CONTEXT,
        "--configuration", $CONFIGURATION,
        "--output-dir", $OUTPUT_DIR
    )

    & dotnet $efParams
    $migrationResult = $LASTEXITCODE

    if ($migrationResult -eq 0) {
        Write-Host "Migration for $DbType successfully created." -ForegroundColor $GREEN

        if ($APPLY_MIGRATION) {
            Write-Host "Applying migration for $DbType..." -ForegroundColor $BLUE

            $efParams = @(
                "ef", "database", "update",
                "--project", $project,
                "--startup-project", $STARTUP_PROJECT,
                "--context", $DB_CONTEXT,
                "--configuration", $CONFIGURATION
            )

            & dotnet $efParams

            if ($LASTEXITCODE -eq 0) {
                Write-Host "Migration for $DbType successfully applied." -ForegroundColor $GREEN
            }
            else {
                Write-Host "Error applying migration for $DbType." -ForegroundColor $RED
                Remove-Item env:DatabaseConfig__Provider -ErrorAction SilentlyContinue
                Remove-Item env:DatabaseConfig__ConnectionStrings__MSSQL -ErrorAction SilentlyContinue
                Remove-Item env:DatabaseConfig__ConnectionStrings__POSTGRESQL -ErrorAction SilentlyContinue
                Remove-Item env:DatabaseConfig__ConnectionStrings__SQLITE -ErrorAction SilentlyContinue
                return $false
            }
        }
    }
    else {
        Write-Host "Error creating migration for $DbType." -ForegroundColor $RED
        Remove-Item env:DatabaseConfig__Provider -ErrorAction SilentlyContinue
        Remove-Item env:DatabaseConfig__ConnectionStrings__MSSQL -ErrorAction SilentlyContinue
        Remove-Item env:DatabaseConfig__ConnectionStrings__POSTGRESQL -ErrorAction SilentlyContinue
        Remove-Item env:DatabaseConfig__ConnectionStrings__SQLITE -ErrorAction SilentlyContinue
        return $false
    }

    # Remove environment variables
    Remove-Item env:DatabaseConfig__Provider -ErrorAction SilentlyContinue
    Remove-Item env:DatabaseConfig__ConnectionStrings__MSSQL -ErrorAction SilentlyContinue
    Remove-Item env:DatabaseConfig__ConnectionStrings__POSTGRESQL -ErrorAction SilentlyContinue
    Remove-Item env:DatabaseConfig__ConnectionStrings__SQLITE -ErrorAction SilentlyContinue
    return $true
}

# Set variables based on parameters

# If help switch is set, show help and exit
if ($help) {
    Show-Help
    exit 0
}

# Set variables based on parameters
$MIGRATION_NAME = $name
$DATABASE_TYPE = $database
$APPLY_MIGRATION = $apply
$CUSTOM_CONNECTION_STRING = $connectionString

# Check if migration name is provided
if ([string]::IsNullOrEmpty($MIGRATION_NAME)) {
    Write-Host "Error: Migration name is required" -ForegroundColor $RED
    Show-Help
    exit 1
}

# Validate database type
if ($DATABASE_TYPE -ne "mssql" -and $DATABASE_TYPE -ne "postgresql" -and $DATABASE_TYPE -ne "sqlite" -and $DATABASE_TYPE -ne "all") {
    Write-Host "Error: Invalid database type. Allowed values: mssql, postgresql, sqlite, all" -ForegroundColor $RED
    Show-Help
    exit 1
}

# Main program
Check-Prerequisites
Read-ConnectionStrings

Write-Host "Starting database migration creation..." -ForegroundColor $BLUE
Write-Host "Migration name: $MIGRATION_NAME" -ForegroundColor $BLUE
Write-Host "Database type: $DATABASE_TYPE" -ForegroundColor $BLUE
Write-Host "Apply migrations: $APPLY_MIGRATION" -ForegroundColor $BLUE
if (-not [string]::IsNullOrEmpty($CUSTOM_CONNECTION_STRING)) {
    Write-Host "Custom connection string: Provided" -ForegroundColor $BLUE
}
Write-Host ""

if ($DATABASE_TYPE -eq "all") {
    $databases = @("mssql", "postgresql", "sqlite")
    $success = $true

    foreach ($db in $databases) {
        $result = Create-Migration -DbType $db
        if (-not $result) {
            $success = $false
        }
    }

    if ($success) {
        Write-Host "All migrations were successfully created." -ForegroundColor $GREEN
    }
    else {
        Write-Host "There were problems creating some migrations." -ForegroundColor $RED
        exit 1
    }
}
else {
    $result = Create-Migration -DbType $DATABASE_TYPE
    if (-not $result) {
        exit 1
    }
}

Write-Host "Done!" -ForegroundColor $GREEN
exit 0
