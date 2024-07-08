#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ConnectionStrings__DefaultConnection Server=localhost;Port=3306;Database=maerp_01;Uid=maerp;Password=maerp;

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["maERP.Server/maERP.Server.csproj", "maERP.Server/"]
COPY ["maERP.Application/maERP.Application.csproj", "maERP.Application/"]
COPY ["maERP.Domain/maERP.Domain.csproj", "maERP.Domain/"]
COPY ["maERP.Ai/maERP.Ai.csproj", "maERP.Ai/"]
COPY ["maERP.Analytics/maERP.Analytics.csproj", "maERP.Analytics/"]
COPY ["maERP.Identity/maERP.Identity.csproj", "maERP.Identity/"]
COPY ["maERP.Infrastructure/maERP.Infrastructure.csproj", "maERP.Infrastructure/"]
COPY ["maERP.Persistence/maERP.Persistence.csproj", "maERP.Persistence/"]
COPY ["maERP.SalesChannels/maERP.SalesChannels.csproj", "maERP.SalesChannels/"]
RUN dotnet restore "maERP.Server/maERP.Server.csproj"
COPY . .
WORKDIR "/src/maERP.Server"
RUN dotnet build "maERP.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "maERP.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "maERP.Server.dll"]