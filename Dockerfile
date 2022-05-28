FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV MYSQL_HOST localhost
ENV MYSQL_PORT 3306
ENV MYSQL_USER maerp
ENV MYSQL_PASS maerp
ENV MYSQL_DB maerp_01

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY maERP.Data/*.csproj ./maERP.Data/
COPY maERP.Server/*.csproj ./maERP.Server/

RUN dotnet restore "maERP.Data/maERP.Data.csproj"
RUN dotnet restore "maERP.Server/maERP.Server.csproj"
COPY . .
WORKDIR /src/maERP.Data
RUN dotnet build -c Release -o /app

WORKDIR /src/maERP.Server
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "maERP.Server.dll"]