#nullable disable

using maERP.Application;
using maERP.Ai;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

const string serviceName = "maERP.Server";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});

builder.Logging.AddOpenTelemetry(logging => {
    // The rest of your setup code goes here
    logging.AddOtlpExporter(options =>
    {
        options.Endpoint = new Uri("http://maerp.de:4318");
        options.Protocol = OtlpExportProtocol.HttpProtobuf;
    });
});

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));

builder.Services.AddOptions<DatabaseOptions>().Bind(builder.Configuration.GetSection("ConnectionStrings"));

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
builder.Services.AddOpenTelemetryServices(builder.Configuration, serviceName);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(opts =>
    opts.JsonSerializerOptions.PropertyNamingPolicy = null); // JsonNamingPolicy.CamelCase);

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024; // 1 MB
    options.UseCaseSensitivePaths = true;
});

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));

// IOptions<DatabaseOptions> dbOptions = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<DatabaseOptions>>();
// builder.Services.AddPersistenceServices(dbOptions);
var serviceScopeFactory = builder.Services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
builder.Services.AddPersistenceServices(serviceScopeFactory);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSalesChannelServices();
builder.Services.AddAiServices();

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

// app.UseExceptionHandler();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Web}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "maERP.Server v1");
    });

    app.MapControllers().AllowAnonymous();
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

    app.UseExceptionHandler("/Home/Error");
    app.UseSerilogRequestLogging();

    app.MapControllers();    
}

app.UseAuthentication(); // who are you?
app.UseAuthorization(); // what are you allowed to do?

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.IsRelational() && context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

namespace maERP.Server
{
    public class Program { }
}