#nullable disable

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using maERP.Application;
using maERP.Application.Models.Grafana;
using maERP.Domain.Enums;
using maERP.Server.Infrastructure.JsonConverters;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Identity;
using maERP.Identity.Services;
using maERP.Infrastructure;
using maERP.Persistence;
using maERP.Persistence.Configurations.Options;
using maERP.Persistence.DatabaseContext;
using maERP.Persistence.Repositories;
using maERP.Persistence.Services;
using maERP.Server.Services;
using maERP.SalesChannels;
using maERP.Server;
using maERP.Server.ServiceRegistrations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using System.Threading.RateLimiting;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

// Out-of-band CLI mode: `dotnet maERP.Server.dll cli ...` runs an admin task
// against the configured database and exits without bringing up Kestrel.
if (args.Length > 0 && args[0] == "cli")
{
    return await maERP.Server.Cli.CliRunner.RunAsync(args[1..]);
}

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// DataProtection key persistence — required so SalesChannel credentials encrypted with
// IDataProtector survive server restarts. Filesystem-backed for v1 (Single-Server Deployments
// or shared-filesystem multi-server). For multi-server cloud deployments, swap to a
// distributed key ring (DB / Azure / Redis) without touching consumers.
var dpKeyDir = builder.Configuration["DataProtection:KeyDirectory"];
if (string.IsNullOrEmpty(dpKeyDir))
{
    dpKeyDir = Path.Combine(builder.Environment.ContentRootPath, "App_Data", "dp-keys");
}
Directory.CreateDirectory(dpKeyDir);
builder.Services.AddDataProtection()
    .SetApplicationName("maERP")
    .PersistKeysToFileSystem(new DirectoryInfo(dpKeyDir));

builder.Services.AddSingleton<ICredentialEncryptor, DataProtectionCredentialEncryptor>();

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));

// Bootstrap: load Grafana settings from the database before wiring up logging/telemetry.
// Falls back to safe defaults when persistence is not available (e.g. test environment).
var grafanaSettings = new GrafanaSettings();
if (!builder.Environment.IsEnvironment("Testing"))
{
    try
    {
        var bootstrapServices = new ServiceCollection();
        bootstrapServices.AddLogging();
        bootstrapServices.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));
        bootstrapServices.AddPersistenceServices();
        bootstrapServices.AddScoped<ITenantContext, TenantContext>();
        bootstrapServices.AddScoped<ISettingRepository, SettingRepository>();
        bootstrapServices.AddScoped<ISettingsService, SettingsService>();
        bootstrapServices.AddTransient<SettingsInitializer>();

#pragma warning disable ASP0000 // Bootstrap-only provider used to read settings before host construction
        using var bootstrapProvider = bootstrapServices.BuildServiceProvider();
#pragma warning restore ASP0000
        using var bootstrapScope = bootstrapProvider.CreateScope();

        var bootstrapDb = bootstrapScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (bootstrapDb.Database.IsRelational() && bootstrapDb.Database.GetPendingMigrations().Any())
        {
            bootstrapDb.Database.Migrate();
        }

        var bootstrapInitializer = bootstrapScope.ServiceProvider.GetRequiredService<SettingsInitializer>();
        await bootstrapInitializer.EnsureRequiredSettingsExistAsync();

        var bootstrapSettings = bootstrapScope.ServiceProvider.GetRequiredService<ISettingsService>();
        grafanaSettings = await bootstrapSettings.GetGrafanaSettingsAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Bootstrap] Failed to load Grafana settings from database: {ex.Message}");
    }
}

if (grafanaSettings.LogsEnabled && Uri.TryCreate(grafanaSettings.LokiEndpoint, UriKind.Absolute, out _))
{
    builder.Host.UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.GrafanaLoki(
            grafanaSettings.LokiEndpoint,
            labels: new[]
            {
                new LokiLabel { Key = "app", Value = "maERP.Server" },
                new LokiLabel { Key = "environment", Value = builder.Environment.EnvironmentName }
            }));
}
else
{
    builder.Host.UseSerilog(
        (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
    );
}

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});

builder.Services.AddSwaggerServices();
builder.Services.AddApiVersioningServices(builder.Configuration);
builder.Services.AddGrafanaTelemetryServices(grafanaSettings, "maERP.Server");

builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = null; // JsonNamingPolicy.CamelCase);
    opts.JsonSerializerOptions.Converters.Add(new StrictEnumConverter<SalesStatus>());
    opts.JsonSerializerOptions.Converters.Add(new StrictEnumConverter<PaymentStatus>());
    opts.JsonSerializerOptions.Converters.Add(new StrictEnumConverter<CustomerStatus>());
});

// Configure API behavior to return consistent Result<T> format for validation errors
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors.Select(e => e.ErrorMessage))
            .ToList();

        var result = new
        {
            Succeeded = false,
            StatusCode = 400,
            Messages = errors,
            Data = (object)null
        };

        return new BadRequestObjectResult(result)
        {
            ContentTypes = { "application/json" }
        };
    };
});

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024; // 1 MB
    options.UseCaseSensitivePaths = true;
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("auth", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddPersistenceServices();
    builder.Services.AddSalesChannelServices();
}
else
{
    // Tests need the connector + dispatcher graph wired so SalesChannelsController can resolve
    // its dependencies — but the orchestrator hosted service must NOT run (would chase tenants
    // across the test InMemory DB). Skip background services only.
    builder.Services.AddSalesChannelServices(includeBackgroundServices: false);
}

builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Skip Identity Services (including JWT Authentication) in test environment
// Tests use their own TestAuthenticationHandler instead
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services.AddIdentityServices(builder.Configuration);
}
// Note: In Testing environment, TestWebApplicationFactory configures TestAuthenticationHandler
// and ITenantContext is replaced by TestTenantContext

// Add health checks
builder.Services.AddHealthChecks()
    // .AddDbContextCheck<ApplicationDbContext>("Database")
    .AddCheck("Self", () => HealthCheckResult.Healthy("Service is running."));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISettingRepository, SettingRepository>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IAiModelRepository, AiModelRepository>();
builder.Services.AddScoped<IAiPromptRepository, AiPromptRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductSalesChannelRepository, ProductSalesChannelRepository>();
builder.Services.AddScoped<ISalesChannelRepository, SalesChannelRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
builder.Services.AddScoped<ITaxClassRepository, TaxClassRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGoodsReceiptRepository, GoodsReceiptRepository>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserTenantRepository, UserTenantRepository>();
builder.Services.AddScoped<ITenantPermissionService, TenantPermissionService>();
builder.Services.AddScoped<ICustomerDedupeService, CustomerDedupeService>();
builder.Services.AddScoped<IOAuthAppSettingsService, OAuthAppSettingsService>();
builder.Services.AddScoped<ITenantOAuthAppSettingsRepository, TenantOAuthAppSettingsRepository>();
builder.Services.AddScoped<IOAuthStateRepository, OAuthStateRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IOAuthTokenExchanger, HttpOAuthTokenExchanger>();
builder.Services.AddScoped<ITenantEmailSettingsRepository, TenantEmailSettingsRepository>();

// OAuth-state cleanup runs only outside the test host; the test factory owns its own lifecycle.
if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddHostedService<OAuthStateCleanupService>();
}

// Register SettingsInitializer service
builder.Services.AddTransient<SettingsInitializer>();

var app = builder.Build();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var dbOptions = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

    if (context.Database.IsRelational() && context.Database.GetPendingMigrations().Any())
    {
        app.Logger.LogInformation("Applying pending migrations for {Provider} database", dbOptions.Provider);
        context.Database.Migrate();
        app.Logger.LogInformation("Migrations applied successfully");
    }

    // Initialize settings
    var settingsInitializer = scope.ServiceProvider.GetRequiredService<SettingsInitializer>();
    await settingsInitializer.EnsureRequiredSettingsExistAsync();
    app.Logger.LogInformation("Settings initialization completed");
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

// Security headers
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["Content-Security-Policy"] = "default-src 'self'";
    context.Response.Headers["X-Permitted-Cross-Domain-Policies"] = "none";

    if (!app.Environment.IsDevelopment())
    {
        context.Response.Headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains";
    }

    await next();
});

app.UseCors();
app.UseStaticFiles();
app.UseRouting();

// DEBUG: Check BEFORE UseAuthentication
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogDebug($"🔍 DEBUG BEFORE UseAuthentication:");
    logger.LogDebug($"   Path: {context.Request.Path}");
    logger.LogDebug($"   Authorization header: {context.Request.Headers.ContainsKey("Authorization")}");
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        var authHeader = context.Request.Headers["Authorization"].ToString();
        logger.LogDebug($"   Auth header value: {authHeader.Substring(0, Math.Min(30, authHeader.Length))}...");
    }
    await next();
});

app.UseAuthentication(); // who are you?

// DEBUG: Check AFTER UseAuthentication
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogDebug($"🔍 DEBUG After UseAuthentication:");
    logger.LogDebug($"   Path: {context.Request.Path}");
    logger.LogDebug($"   User: {context.User.Identity?.Name ?? "null"}");
    logger.LogDebug($"   IsAuthenticated: {context.User.Identity?.IsAuthenticated}");
    logger.LogDebug($"   Claims count: {context.User.Claims.Count()}");
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var roles = context.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.Role).Select(c => c.Value);
        logger.LogDebug($"   Roles: {string.Join(", ", roles)}");
    }
    await next();
});

app.UseMiddleware<maERP.Server.Middleware.TenantMiddleware>(); // set tenant context
app.UseAuthorization(); // what are you allowed to do?
app.UseRateLimiter();

if (!app.Environment.IsDevelopment() && !app.Environment.IsEnvironment("Testing"))
{
    app.UseResponseCaching();

    app.Use(async (context, next) =>
    {
        context.Response.GetTypedHeaders().CacheControl =
            new CacheControlHeaderValue
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(10)
            };

        context.Response.Headers[HeaderNames.Vary] =
            new[] { "Accept-Encoding", "X-Tenant-Id" };

        await next();
    });

    app.UseSerilogRequestLogging();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "maERP.Server v1");
    });
}

// Map all endpoints after all middleware
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Web}/{action=Index}/{id?}");

// In Testing environment, allow anonymous access for test infrastructure
if (app.Environment.IsEnvironment("Testing"))
{
    app.MapControllers().AllowAnonymous();
}
else
{
    app.MapControllers();
}

// Add health check endpoint
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                description = e.Value.Description
            })
        };
        await context.Response.WriteAsJsonAsync(result);
    }
});

// Display formatted startup message.
// app.Urls is empty until Kestrel actually starts, so fall back to the configured
// URLs / ports from environment variables (ASPNETCORE_URLS, HTTP_PORTS, HTTPS_PORTS).
//
// In a container the port the server *binds to* is not the port users connect
// to — the host publishes it on a (possibly different) external port. When
// MAERP_PUBLIC_PORT is set (e.g. by docker-compose), it overrides the port
// shown in the banner so logs reflect the externally reachable address.
static IEnumerable<string> ResolveStartupUrls(WebApplication application)
{
    var publicPort = Environment.GetEnvironmentVariable("MAERP_PUBLIC_PORT");
    var sourceUrls = ResolveBoundUrls(application);

    if (string.IsNullOrWhiteSpace(publicPort))
        return sourceUrls;

    // Rewrite each URL's port to the public one.
    return sourceUrls.Select(url =>
    {
        try
        {
            var builder = new UriBuilder(url) { Port = int.Parse(publicPort) };
            return builder.Uri.ToString().TrimEnd('/');
        }
        catch
        {
            return url;
        }
    });
}

static IEnumerable<string> ResolveBoundUrls(WebApplication application)
{
    if (application.Urls.Count > 0)
        return application.Urls;

    var aspnetUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
    if (!string.IsNullOrWhiteSpace(aspnetUrls))
        return aspnetUrls.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var resolved = new List<string>();
    var httpPorts = Environment.GetEnvironmentVariable("HTTP_PORTS");
    if (!string.IsNullOrWhiteSpace(httpPorts))
        resolved.AddRange(httpPorts.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(p => $"http://localhost:{p}"));

    var httpsPorts = Environment.GetEnvironmentVariable("HTTPS_PORTS");
    if (!string.IsNullOrWhiteSpace(httpsPorts))
        resolved.AddRange(httpsPorts.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(p => $"https://localhost:{p}"));

    return resolved.Count > 0 ? resolved : ["http://localhost:5000"];
}

var urls = ResolveStartupUrls(app);
var environment = app.Environment.EnvironmentName;

Console.WriteLine();
Console.WriteLine("========================================");
Console.WriteLine("          maERP Server Started         ");
Console.WriteLine("========================================");
Console.WriteLine($"Environment: {environment}");
Console.WriteLine("Server is listening on:");

foreach (var url in urls)
{
    var uri = new Uri(url);
    var protocol = uri.Scheme.ToUpper();
    Console.WriteLine($"  • {protocol}: {url}");
}

Console.WriteLine();
if (app.Environment.IsDevelopment())
{
    Console.WriteLine($"Swagger UI: /swagger");
}
Console.WriteLine("Health Check: /health");
Console.WriteLine("Free Web-UI: https://www.maerp.de/");
Console.WriteLine("========================================");
Console.WriteLine();

app.Run();

return 0;

// Make the implicit Program class public so test projects can access it
namespace maERP.Server
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program { }
}
