## Add Migration

```
dotnet ef migrations add --project src/maERP.Persistence/maERP.Persistence.csproj --startup-project src/maERP.Server/maERP.Server.csproj --context maERP.Persistence.DatabaseContext.ApplicationDbContext --configuration Debug ApplicationDbUser --output-dir Migrations
```