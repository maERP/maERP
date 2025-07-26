# maERP Server

[![maERP.Server](https://github.com/maERP/maERP/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/maERP/maERP/actions/workflows/dotnet.yml)

## Starting maERP.Server

### Database Configuration

#### MySQL
```
DatabaseConfig__ConnectionStrings__MySQL="Server=dein-server;Port=3306;Database=maerp_01;Uid=user;Pwd=password;"
```

#### MSSQL
```
DatabaseConfig__ConnectionStrings__MSSQL="Server=dein-server;Database=maerp_01;User Id=user;Password=password;TrustServerCertificate=True;"
```

#### PostgreSQL
```
DatabaseConfig__ConnectionStrings__PostgreSQL="Host=dein-server;Port=5432;Database=maerp_01;Username=user;Password=password;"
```

#### SQLite
```
DatabaseConfig__ConnectionStrings__SQLite="Data Source=pfad/zu/deiner.db"
```

### Provider Configuration

You can also specify which database provider to use:
```
DatabaseConfig__Provider="MySQL"
```
