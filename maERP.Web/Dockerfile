#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV SERVER_URL "https://localhost:8080"

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
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["maERP.Web/nginx.conf", "/etc/nginx/nginx.conf"]