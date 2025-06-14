#!/bin/bash

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Default values
MIGRATION_NAME=""
DATABASE_TYPE="all"
APPLY_MIGRATION=false
STARTUP_PROJECT="src/maERP.Server/maERP.Server.csproj"
DB_CONTEXT="maERP.Persistence.DatabaseContext.ApplicationDbContext"
CONFIGURATION="Debug"
OUTPUT_DIR="Migrations"
OFFLINE_MODE=false
APPSETTINGS_PATH="src/maERP.Server/appsettings.Development.json"

# Function to display help
show_help() {
    echo -e "${BLUE}maERP Database Migration Tool${NC}"
    echo ""
    echo "Usage: $0 [options]"
    echo ""
    echo "Options:"
    echo "  -n, --name NAME                 Name of the migration (required)"
    echo "  -d, --database TYPE             Database type (mysql, mssql, postgresql, sqlite or all) [default: all]"
    echo "  -a, --apply                     Apply migrations after creation"
    echo "  -c, --connection-string STRING  Custom connection string for the selected database type"
    echo "  -o, --offline                   Create migrations without connecting to the database (MySQL only)"
    echo "  -h, --help                      Show this help"
    echo ""
    echo "Example: $0 -n AddCustomerTable -d mysql -a"
    echo "Example with custom connection: $0 -n AddCustomerTable -d mysql -c \"Server=localhost;Port=3306;Database=mydb;Uid=myuser;Pwd=mypass;\""
    echo "Example offline mode: $0 -n AddCustomerTable -d mysql -o"
}

# Function to read connection strings from appsettings.json
read_connection_strings() {
    if ! [ -f "$APPSETTINGS_PATH" ]; then
        echo -e "${RED}Error: appsettings.json not found at $APPSETTINGS_PATH${NC}"
        exit 1
    fi

    echo -e "${BLUE}Reading connection strings from $APPSETTINGS_PATH...${NC}"

    # Try to use jq if available
    if command -v jq &> /dev/null; then
        MYSQL_CONNECTION=$(jq -r '.DatabaseConfig.ConnectionStrings.MySQL' "$APPSETTINGS_PATH")
        MSSQL_CONNECTION=$(jq -r '.DatabaseConfig.ConnectionStrings.MSSQL' "$APPSETTINGS_PATH")
        POSTGRESQL_CONNECTION=$(jq -r '.DatabaseConfig.ConnectionStrings.PostgreSQL' "$APPSETTINGS_PATH")
        SQLITE_CONNECTION=$(jq -r '.DatabaseConfig.ConnectionStrings.SQLite' "$APPSETTINGS_PATH")
    else
        # Fallback to grep/sed if jq is not available
        echo -e "${YELLOW}jq not found, using grep/sed to parse JSON (less reliable)${NC}"
        
        # Extract connection strings using grep/sed
        MYSQL_CONNECTION=$(grep -o '"MySQL": *"[^"]*"' "$APPSETTINGS_PATH" | sed 's/"MySQL": *"\(.*\)"/\1/')
        MSSQL_CONNECTION=$(grep -o '"MSSQL": *"[^"]*"' "$APPSETTINGS_PATH" | sed 's/"MSSQL": *"\(.*\)"/\1/')
        POSTGRESQL_CONNECTION=$(grep -o '"PostgreSQL": *"[^"]*"' "$APPSETTINGS_PATH" | sed 's/"PostgreSQL": *"\(.*\)"/\1/')
        SQLITE_CONNECTION=$(grep -o '"SQLite": *"[^"]*"' "$APPSETTINGS_PATH" | sed 's/"SQLite": *"\(.*\)"/\1/')
    fi

    # Verify that connection strings were extracted successfully
    if [ -z "$MYSQL_CONNECTION" ] || [ -z "$MSSQL_CONNECTION" ] || [ -z "$POSTGRESQL_CONNECTION" ] || [ -z "$SQLITE_CONNECTION" ]; then
        echo -e "${RED}Error: Failed to read connection strings from appsettings.json${NC}"
        echo -e "${YELLOW}Using default connection strings...${NC}"
        
        # Default connection strings as fallback
        MYSQL_CONNECTION="Server=127.0.0.1;Port=3306;Database=maerp_migration;Uid=root;Pwd=root;AllowPublicKeyRetrieval=True;"
        MSSQL_CONNECTION="Server=localhost;Database=maerp_migration;User Id=maerp;Password=maerp;TrustServerCertificate=True;"
        POSTGRESQL_CONNECTION="Host=localhost;Port=5432;Database=maerp_migration;Username=maerp;Password=maerp;"
        SQLITE_CONNECTION="Data Source=maerp_migration.db"
    else
        echo -e "${GREEN}Connection strings successfully loaded from appsettings.json${NC}"
    fi
}

# Check prerequisites
check_prerequisites() {
    echo -e "${BLUE}Checking prerequisites...${NC}"
    
    if ! command -v dotnet &> /dev/null; then
        echo -e "${RED}dotnet is not installed. Please install the .NET SDK.${NC}"
        exit 1
    fi
    
    if ! dotnet tool list -g | grep dotnet-ef &> /dev/null; then
        echo -e "${YELLOW}dotnet-ef is not globally installed. Installing now...${NC}"
        dotnet tool install --global dotnet-ef
        
        echo -e "${YELLOW}Please ensure that the path to the dotnet tools is included in PATH:${NC}"
        echo -e "${YELLOW}export PATH=\"\$PATH:\$HOME/.dotnet/tools\"${NC}"
    else
        # Update Entity Framework tools to latest version
        echo -e "${BLUE}Updating Entity Framework tools to the latest version...${NC}"
        dotnet tool update --global dotnet-ef
    fi
    
    echo -e "${GREEN}All prerequisites met.${NC}"
}

# Process command line arguments
CUSTOM_CONNECTION_STRING=""

while [[ $# -gt 0 ]]; do
    key="$1"
    case $key in
        -n|--name)
            MIGRATION_NAME="$2"
            shift 2
            ;;
        -d|--database)
            DATABASE_TYPE="$2"
            shift 2
            ;;
        -a|--apply)
            APPLY_MIGRATION=true
            shift
            ;;
        -c|--connection-string)
            CUSTOM_CONNECTION_STRING="$2"
            shift 2
            ;;
        -o|--offline)
            OFFLINE_MODE=true
            shift
            ;;
        -h|--help)
            show_help
            exit 0
            ;;
        *)
            echo -e "${RED}Unknown option: $1${NC}"
            show_help
            exit 1
            ;;
    esac
done

# Check if migration name is provided
if [ -z "$MIGRATION_NAME" ]; then
    echo -e "${RED}Error: Migration name is required${NC}"
    show_help
    exit 1
fi

# Validate database type
if [[ "$DATABASE_TYPE" != "mysql" && "$DATABASE_TYPE" != "mssql" && "$DATABASE_TYPE" != "postgresql" && "$DATABASE_TYPE" != "sqlite" && "$DATABASE_TYPE" != "all" ]]; then
    echo -e "${RED}Error: Invalid database type. Allowed values: mysql, mssql, postgresql, sqlite, all${NC}"
    show_help
    exit 1
fi

# Main function to create migration
create_migration() {
    local db_type=$1
    local project=""
    local db_provider=""
    local connection_string=""
    local offline_args=""
    
    case $db_type in
        mysql)
            project="src/maERP.Persistence.MySQL/maERP.Persistence.MySQL.csproj"
            db_provider="MYSQL"
            connection_string=$MYSQL_CONNECTION
            # If offline mode is enabled and we're dealing with MySQL, add the --no-build flag
            if [ "$OFFLINE_MODE" = true ]; then
                offline_args="--no-build"
                echo -e "${YELLOW}Using offline mode for MySQL migration${NC}"
            fi
            ;;
        mssql)
            project="src/maERP.Persistence.MSSQL/maERP.Persistence.MSSQL.csproj"
            db_provider="MSSQL"
            connection_string=$MSSQL_CONNECTION
            ;;
        postgresql)
            project="src/maERP.Persistence.PostgreSQL/maERP.Persistence.PostgreSQL.csproj"
            db_provider="POSTGRESQL"
            connection_string=$POSTGRESQL_CONNECTION
            ;;
        sqlite)
            project="src/maERP.Persistence.SQLite/maERP.Persistence.SQLite.csproj"
            db_provider="SQLITE"
            connection_string=$SQLITE_CONNECTION
            ;;
    esac
    
    # Override with custom connection string if provided and we're operating on the selected database type
    if [[ ! -z "$CUSTOM_CONNECTION_STRING" && ("$DATABASE_TYPE" == "$db_type" || "$DATABASE_TYPE" == "all") ]]; then
        echo -e "${YELLOW}Using custom connection string for $db_type${NC}"
        connection_string=$CUSTOM_CONNECTION_STRING
    fi
    
    echo -e "${BLUE}Creating migration for $db_type...${NC}"
    if [ "$OFFLINE_MODE" != true ] || [ "$db_type" != "mysql" ]; then
        echo -e "${BLUE}Using connection: ${YELLOW}$connection_string${NC}"
    fi
    
    # Set environment variables to force the correct database provider and connection strings
    export DatabaseConfig__Provider=$db_provider
    export DatabaseConfig__ConnectionStrings__MYSQL=$MYSQL_CONNECTION
    export DatabaseConfig__ConnectionStrings__MSSQL=$MSSQL_CONNECTION
    export DatabaseConfig__ConnectionStrings__POSTGRESQL=$POSTGRESQL_CONNECTION
    export DatabaseConfig__ConnectionStrings__SQLITE=$SQLITE_CONNECTION
    
    # Override the specific connection string for the current database type
    case $db_type in
        mysql)
            export DatabaseConfig__ConnectionStrings__MYSQL=$connection_string
            ;;
        mssql)
            export DatabaseConfig__ConnectionStrings__MSSQL=$connection_string
            ;;
        postgresql)
            export DatabaseConfig__ConnectionStrings__POSTGRESQL=$connection_string
            ;;
        sqlite)
            export DatabaseConfig__ConnectionStrings__SQLITE=$connection_string
            ;;
    esac
    
    # First attempt: try normal migration creation
    if [ "$db_type" != "mysql" ] || [ "$OFFLINE_MODE" != true ]; then
        dotnet ef migrations add $MIGRATION_NAME \
            --project $project \
            --startup-project $STARTUP_PROJECT \
            --context $DB_CONTEXT \
            --configuration $CONFIGURATION \
            --output-dir $OUTPUT_DIR \
            $offline_args
        
        local migration_result=$?
        
        # If MySQL failed, try again with offline mode
        if [ $migration_result -ne 0 ] && [ "$db_type" = "mysql" ] && [ "$OFFLINE_MODE" != true ]; then
            echo -e "${YELLOW}MySQL migration failed. Trying offline mode...${NC}"
            OFFLINE_MODE=true
            create_migration "mysql"
            return $?
        fi
    else
        # For MySQL in offline mode, use a special approach
        # First build the project
        dotnet build $project -c $CONFIGURATION
        
        # Then create the migration with --no-build
        dotnet ef migrations add $MIGRATION_NAME \
            --project $project \
            --startup-project $STARTUP_PROJECT \
            --context $DB_CONTEXT \
            --configuration $CONFIGURATION \
            --output-dir $OUTPUT_DIR \
            --no-build
        
        migration_result=$?
    fi
    
    if [ $migration_result -eq 0 ]; then
        echo -e "${GREEN}Migration for $db_type successfully created.${NC}"
        
        if [ "$APPLY_MIGRATION" = true ] && [ "$OFFLINE_MODE" != true ]; then
            echo -e "${BLUE}Applying migration for $db_type...${NC}"
            
            dotnet ef database update \
                --project $project \
                --startup-project $STARTUP_PROJECT \
                --context $DB_CONTEXT \
                --configuration $CONFIGURATION
            
            if [ $? -eq 0 ]; then
                echo -e "${GREEN}Migration for $db_type successfully applied.${NC}"
            else
                echo -e "${RED}Error applying migration for $db_type.${NC}"
                # Unset environment variables
                unset DatabaseConfig__Provider
                unset DatabaseConfig__ConnectionStrings__MYSQL
                unset DatabaseConfig__ConnectionStrings__MSSQL
                unset DatabaseConfig__ConnectionStrings__POSTGRESQL
                unset DatabaseConfig__ConnectionStrings__SQLITE
                return 1
            fi
        elif [ "$APPLY_MIGRATION" = true ] && [ "$OFFLINE_MODE" = true ]; then
            echo -e "${YELLOW}Cannot apply migration in offline mode${NC}"
        fi
    else
        echo -e "${RED}Error creating migration for $db_type.${NC}"
        # Unset environment variables
        unset DatabaseConfig__Provider
        unset DatabaseConfig__ConnectionStrings__MYSQL
        unset DatabaseConfig__ConnectionStrings__MSSQL
        unset DatabaseConfig__ConnectionStrings__POSTGRESQL
        unset DatabaseConfig__ConnectionStrings__SQLITE
        return 1
    fi
    
    # Unset environment variables
    unset DatabaseConfig__Provider
    unset DatabaseConfig__ConnectionStrings__MYSQL
    unset DatabaseConfig__ConnectionStrings__MSSQL
    unset DatabaseConfig__ConnectionStrings__POSTGRESQL
    unset DatabaseConfig__ConnectionStrings__SQLITE
    return 0
}

# Main program
check_prerequisites
read_connection_strings

echo -e "${BLUE}Starting database migration creation...${NC}"
echo -e "${BLUE}Migration name: ${YELLOW}$MIGRATION_NAME${NC}"
echo -e "${BLUE}Database type: ${YELLOW}$DATABASE_TYPE${NC}"
echo -e "${BLUE}Apply migrations: ${YELLOW}$APPLY_MIGRATION${NC}"
echo -e "${BLUE}Offline mode: ${YELLOW}$OFFLINE_MODE${NC}"
if [ ! -z "$CUSTOM_CONNECTION_STRING" ]; then
    echo -e "${BLUE}Custom connection string: ${YELLOW}Provided${NC}"
fi
echo ""

if [ "$DATABASE_TYPE" = "all" ]; then
    databases=("mysql" "mssql" "postgresql" "sqlite")
    success=true
    
    for db in "${databases[@]}"; do
        create_migration $db
        if [ $? -ne 0 ]; then
            success=false
        fi
    done
    
    if [ "$success" = true ]; then
        echo -e "${GREEN}All migrations were successfully created.${NC}"
    else
        echo -e "${RED}There were problems creating some migrations.${NC}"
        exit 1
    fi
else
    create_migration $DATABASE_TYPE
    if [ $? -ne 0 ]; then
        exit 1
    fi
fi

echo -e "${GREEN}Done!${NC}"
exit 0 