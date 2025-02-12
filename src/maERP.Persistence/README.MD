## Add Migration

## Database migration

### MySQL
```bash
dotnet ef migrations add initDb --project src/maERP.Persistence.MySQL/maERP.Persistence.MySQL.csproj --startup-project src/maERP.Server/maERP.Server.csproj --context maERP.Persistence.DatabaseContext.ApplicationDbContext --configuration Debug --output-dir Migrations
```

### MSSQL
```bash
dotnet ef migrations add initDb --project src/maERP.Persistence.MSSQL/maERP.Persistence.MSSQL.csproj --startup-project src/maERP.Server/maERP.Server.csproj --context maERP.Persistence.DatabaseContext.ApplicationDbContext --configuration Debug --output-dir Migrations
```

### PostgreSQL
```bash
dotnet ef migrations add initDb --project src/maERP.Persistence.PostgreSQL/maERP.Persistence.PostgreSQL.csproj --startup-project src/maERP.Server/maERP.Server.csproj --context maERP.Persistence.DatabaseContext.ApplicationDbContext --configuration Debug --output-dir Migrations

```

### Apply Migration
```
dotnet ef database update
```

## Install dotnet-ef on macos

```bash
dotnet tool install --global dotnet-ef
export PATH="$PATH:/Users/'your user folder'/.dotnet/tools"
```

## Update dotnet tools
```bash
dotnet tool update --global dotnet-ef
```