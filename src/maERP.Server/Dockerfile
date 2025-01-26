#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ConnectionStrings__DefaultConnection Server=localhost;Port=3306;Database=maerp_01;Uid=maerp;Password=maerp;

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/maERP.Server/maERP.Server.csproj", "maERP.Server/"]
COPY ["src/maERP.Application/maERP.Application.csproj", "maERP.Application/"]
COPY ["src/maERP.Domain/maERP.Domain.csproj", "maERP.Domain/"]
COPY ["src/maERP.Ai/maERP.Ai.csproj", "maERP.Ai/"]
COPY ["src/maERP.Analytics/maERP.Analytics.csproj", "maERP.Analytics/"]
COPY ["src/maERP.Identity/maERP.Identity.csproj", "maERP.Identity/"]
COPY ["src/maERP.Infrastructure/maERP.Infrastructure.csproj", "maERP.Infrastructure/"]
COPY ["src/maERP.Persistence/maERP.Persistence.csproj", "maERP.Persistence/"]
COPY ["src/maERP.SalesChannels/maERP.SalesChannels.csproj", "maERP.SalesChannels/"]
RUN dotnet restore "maERP.Server/maERP.Server.csproj"
COPY src/ .
WORKDIR "/src/maERP.Server"
RUN dotnet build "maERP.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "maERP.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=3s \
    CMD wget --no-verbose --tries=1 --spider http://localhost:80/health || exit 1

ENTRYPOINT ["dotnet", "maERP.Server.dll"]