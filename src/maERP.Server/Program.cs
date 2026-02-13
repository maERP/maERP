#nullable disable

using Microsoft.AspNetCore.Mvc;
using maERP.Application;
using maERP.Domain.Enums;
using maERP.Server.Infrastructure.JsonConverters;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Identity;
using maERP.Infrastructure;
using maERP.Persistence;
using maERP.Persistence.Configurations.Options;
using maERP.Persistence.DatabaseContext;
using maERP.Persistence.Repositories;
using maERP.Persistence.Services;
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

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Logging.AddOpenTelemetry(logging =>
{
    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri(builder.Configuration["Telemetry:Endpoint"] ?? "http://localhost:4317");
        options.Protocol = OtlpExportProtocol.Grpc;
    });
});

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));

builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

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
builder.Services.AddOpenTelemetryServices(builder.Configuration, "maERP.Server");

builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = null; // JsonNamingPolicy.CamelCase);
    opts.JsonSerializerOptions.Converters.Add(new StrictEnumConverter<OrderStatus>());
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
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
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
builder.Services.AddScoped<ITenantEmailSettingsRepository, TenantEmailSettingsRepository>();

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

// Display formatted startup message
var urls = app.Urls.Any() ? app.Urls : ["http://localhost:8080"];
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

// Make the implicit Program class public so test projects can access it
namespace maERP.Server
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program { }
}
