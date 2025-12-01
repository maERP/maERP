# Uno Platform WebAssembly Dockerfile
# Build stage with .NET 10 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

# Copy project files for restore
COPY ["src/maERP.Client/maERP.Client.csproj", "maERP.Client/"]
COPY ["src/maERP.Domain/maERP.Domain.csproj", "maERP.Domain/"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["global.json", "./"]

# Install all required workloads for the project
RUN dotnet workload restore "maERP.Client/maERP.Client.csproj"

# Restore dependencies
RUN dotnet restore "maERP.Client/maERP.Client.csproj"

# Copy source files
COPY src/maERP.Client/ maERP.Client/
COPY src/maERP.Domain/ maERP.Domain/

# Build and publish
WORKDIR "/src/maERP.Client"
RUN dotnet publish "maERP.Client.csproj" -c Release -f net10.0-browserwasm -o /app/publish

# Runtime stage with nginx
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy published WASM files
COPY --from=build /app/publish/wwwroot .

# Copy custom nginx config for SPA routing
COPY src/maERP.Client/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

HEALTHCHECK --interval=30s --timeout=3s \
    CMD wget --no-verbose --tries=1 --spider http://localhost:80/ || exit 1
