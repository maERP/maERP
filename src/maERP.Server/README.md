# maERP Server

[![maERP.Server](https://github.com/maERP/maERP/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/maERP/maERP/actions/workflows/dotnet.yml)

A headless REST API server for the maERP (Enterprise Resource Planning) system built with .NET 9 and ASP.NET Core.

## Overview

maERP.Server provides the backend API for the maERP system, implementing Clean Architecture principles with CQRS pattern. It supports multi-tenancy and provides RESTful endpoints for all business operations.

## Features

- **Multi-tenant architecture** - Complete tenant isolation
- **JWT Authentication** - Secure token-based authentication
- **Clean Architecture** - Separation of concerns with CQRS
- **Multi-database support** - MySQL, PostgreSQL, MSSQL, SQLite
- **API Versioning** - Built-in versioning support
- **OpenTelemetry** - Comprehensive observability
- **Swagger/OpenAPI** - API documentation

## Quick Start

### Prerequisites

- .NET 9 SDK
- Supported database (MySQL, PostgreSQL, MSSQL, or SQLite)

### Running the Server

```bash
dotnet run --project src/maERP.Server/maERP.Server.csproj
```

The API will be available at `https://localhost:5001` by default.

## Database Configuration

### MySQL
```
DatabaseConfig__ConnectionStrings__MySQL="Server=dein-server;Port=3306;Database=maerp_01;Uid=user;Pwd=password;"
```

### MSSQL
```
DatabaseConfig__ConnectionStrings__MSSQL="Server=dein-server;Database=maerp_01;User Id=user;Password=password;TrustServerCertificate=True;"
```

### PostgreSQL
```
DatabaseConfig__ConnectionStrings__PostgreSQL="Host=dein-server;Port=5432;Database=maerp_01;Username=user;Password=password;"
```

### SQLite
```
DatabaseConfig__ConnectionStrings__SQLite="Data Source=pfad/zu/deiner.db"
```

### Provider Configuration

You can also specify which database provider to use:
```
DatabaseConfig__Provider="MySQL"
```

## API Documentation

Once running, access the Swagger UI at `https://localhost:5001/swagger` for interactive API documentation.

## Multi-Platform Clients

This server works with the following maERP clients:
- **maERP.Client** - Cross-platform Uno Platform app

## License

Open source ERP system developed by Martin Andrich.
