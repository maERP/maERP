#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

ENV MAERP_SERVER_BASE_URL "https://www.maerp.de/"
ENV MAERP_REMOTE_LOG_URL true

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
# Install Python and wasm-tools workload for WebAssembly support with native build
RUN apt-get update && apt-get install -y python3 && ln -s /usr/bin/python3 /usr/bin/python
RUN dotnet workload install wasm-tools
WORKDIR /src
COPY ["src/maERP.Domain/maERP.Domain.csproj", "maERP.Domain/"]
COPY ["src/maERP.UI/maERP.UI.csproj", "maERP.UI/"]
COPY ["src/maERP.UI.Browser/maERP.UI.Browser.csproj", "maERP.UI.Browser/"]
RUN dotnet restore "maERP.UI.Browser/maERP.UI.Browser.csproj"
COPY src/ .
WORKDIR "/src/maERP.UI.Browser"
RUN dotnet build "maERP.UI.Browser.csproj" -c Release -o /app/build

FROM build AS publish
# Setze Umgebungsvariable für die Server-URL während des Builds
ARG MAERP_SERVER_BASE_URL="https://www.maerp.de/"
ENV MAERP_SERVER_BASE_URL=${MAERP_SERVER_BASE_URL}
RUN dotnet publish "maERP.UI.Browser.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
# Kopiere eine angepasste nginx.conf, die die Umgebungsvariable ins Frontend übergibt
COPY src/maERP.UI.Browser/nginx.conf /etc/nginx/conf.d/default.conf
# Kopiere ein Startup-Skript, das die Umgebungsvariable in eine JavaScript-Datei injiziert
COPY src/maERP.UI.Browser/docker-entrypoint.sh /docker-entrypoint.sh
RUN chmod +x /docker-entrypoint.sh
# Setze die Umgebungsvariable auch im NGINX-Container
ENV MAERP_SERVER_BASE_URL=${MAERP_SERVER_BASE_URL}
ENTRYPOINT ["/docker-entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]