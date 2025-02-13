#nullable disable

using maERP.Application;
using maERP.Application.Contracts.Persistence;
using maERP.Identity;
using maERP.Infrastructure;
using maERP.Persistence;
using maERP.Persistence.Configurations.Options;
using maERP.Persistence.DatabaseContext;
using maERP.Persistence.Repositories;
using maERP.SalesChannels;
using maERP.Server;
using maERP.Server.Middlewares;
using maERP.Server.ServiceRegistrations;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Logging.AddOpenTelemetry(logging => {
    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://maerp.de:4317");
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
    opts.JsonSerializerOptions.PropertyNamingPolicy = null); // JsonNamingPolicy.CamelCase);

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024; // 1 MB
    options.UseCaseSensitivePaths = true;
});

// IOptions<DatabaseOptions> dbOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<DatabaseOptions>>();
// builder.Services.AddPersistenceServices(dbOptions);
#pragma warning disable ASP0000
var serviceScopeFactory = builder.Services.BuildServiceProvider().GetService<IServiceScopeFactory>();
#pragma warning restore ASP0000

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddPersistenceServices(serviceScopeFactory);    
}

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Add health checks
builder.Services.AddHealthChecks()
    // .AddDbContextCheck<ApplicationDbContext>("Database")
    .AddCheck("Self", () => HealthCheckResult.Healthy("Service is running."));

if (!builder.Environment.IsEnvironment("Testing"))
{
    builder.Services.AddSalesChannelServices();
}

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
builder.Services.AddScoped<IAiModelRepository, AiModelRepository>();
builder.Services.AddScoped<IAiPromptRepository, AiPromptRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductSalesChannelRepository, ProductSalesChannelRepository>();
builder.Services.AddScoped<ISalesChannelRepository, SalesChannelRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<ITaxClassRepository, TaxClassRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
}

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Web}/{action=Index}/{id?}");

if(app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Testing"))
{
    app.MapControllers().AllowAnonymous();
}    

if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "maERP.Server v1");
    });
}
else
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
            new[] { "Accept-Encoding" };

        await next();
    });

    app.UseMiddleware<ExceptionMiddleware>();
    app.UseExceptionHandler("/Home/Error");
    app.UseSerilogRequestLogging();

    app.MapControllers();    
}

app.UseAuthentication(); // who are you?
app.UseAuthorization(); // what are you allowed to do?

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

app.Run();

// Make the implicit Program class public so test projects can access it
namespace maERP.Server
{
    public class Program { }
}