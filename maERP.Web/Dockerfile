#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV MAERP_SERVER_BASE_URL "https://localhost:8080"
ENV MAERP_REMOTE_LOG true

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["maERP.Web/maERP.Web.csproj", "maERP.Web/"]
COPY ["maERP.Shared/maERP.Shared.csproj", "maERP.Shared/"]
RUN dotnet restore "maERP.Web/maERP.Web.csproj"
COPY . .
WORKDIR "/src/maERP.Web"
RUN dotnet build "maERP.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "maERP.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
RUN apk add --no-cache jq moreutils
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
RUN jq '.ServerBaseUrl = "$MAERP_SERVER_BASE_URLI"' /app/publish/wwwroot/appsettings.json | sponge /app/publish/wwwroot/appsettings.json
RUN jq '.RemoteLog = $MAERP_REMOTE_LOG' /app/publish/wwwroot/appsettings.json | sponge /app/publish/wwwroot/appsettings.json