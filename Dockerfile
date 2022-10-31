FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV DB_TYPE mysql
ENV DB_HOST localhost
ENV DB_PORT 3306
ENV DB_USER maerp
ENV DB_PASS maerp
ENV DB_NAME maerp_01

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY maERP.Shared/*.csproj ./maERP.Shared/
COPY maERP.Server/*.csproj ./maERP.Server/

RUN dotnet restore "maERP.Shared/maERP.Shared.csproj"
RUN dotnet restore "maERP.Server/maERP.Server.csproj"
COPY . .
WORKDIR /src/maERP.Shared
RUN dotnet build -c Release -o /app

WORKDIR /src/maERP.Server
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "maERP.Server.dll"]
