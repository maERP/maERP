# Uno Platform WebAssembly Dockerfile
# Build stage with .NET 10 SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Install python3 required by Emscripten for WASM native compilation
RUN apt-get update && apt-get install -y --no-install-recommends python3 && rm -rf /var/lib/apt/lists/*

WORKDIR /src

# Copy project files for restore
COPY ["src/maERP.Client/maERP.Client.csproj", "maERP.Client/"]
COPY ["src/maERP.Domain/maERP.Domain.csproj", "maERP.Domain/"]
COPY ["Directory.Build.props", "./"]
COPY ["Directory.Packages.props", "./"]
COPY ["global.json", "./"]
# NuGet.config carries the package sources required to resolve the
# Uno.Sdk MSBuild SDK that's pulled in via global.json's msbuild-sdks.
COPY ["NuGet.config", "./"]

# Install only the wasm-tools workload — the container build targets
# net10.0-browserwasm exclusively. We deliberately do NOT run
# `dotnet workload restore` here: that would force-install Android, iOS,
# MacCatalyst etc. workloads from the multi-target .csproj, all of which
# are unused inside this image.
RUN dotnet workload install wasm-tools

# Restore dependencies (this also primes the NuGet cache with Uno.Sdk
# so downstream MSBuild evaluations succeed).
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

# Runtime config generator — writes /usr/share/nginx/html/config.json from
# env vars before nginx starts. Picked up by /docker-entrypoint.d/.
COPY src/maERP.Client/docker-entrypoint.d/10-write-config.sh /docker-entrypoint.d/10-write-config.sh
RUN chmod +x /docker-entrypoint.d/10-write-config.sh

EXPOSE 80

HEALTHCHECK --interval=30s --timeout=3s \
    CMD wget --no-verbose --tries=1 --spider http://localhost:80/ || exit 1
