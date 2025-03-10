#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

ENV MAERP_SERVER_BASE_URL "https://localhost:8080"
ENV MAERP_REMOTE_LOG_URL true

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/maERP.Web/maERP.Web.csproj", "maERP.Web/"]
COPY ["src/maERP.SharedUI/maERP.SharedUI.csproj", "maERP.SharedUI/"]
RUN dotnet restore "maERP.Web/maERP.Web.csproj"
COPY src/ .
WORKDIR "/src/maERP.Web"
RUN dotnet build "maERP.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "maERP.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .