# maERP Server

[![maERP.Server](https://github.com/maERP/maERP/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/maERP/maERP/actions/workflows/dotnet.yml)

## Database migration

```bash
dotnet ef migrations add initMysql
dotnet ef database update
```

## Install dotnet-ef on macos

```bash
dotnet tool install --global dotnet-ef
export PATH="$PATH:/Users/'your user folder'/.dotnet/tools"
```

## Update dotnet tools
```
dotnet tool update --global dotnet-ef
``

## Licenses

Packages used:

- AutoMapper (https://github.com/AutoMapper/AutoMapper)