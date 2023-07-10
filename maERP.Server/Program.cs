#nullable disable

using maERP.Server;
using maERP.Server.Configurations;
using maERP.Server.Middleware;
using maERP.Server.Models;
using maERP.Server.ServiceRegistrations;
using maERP.Server.Repository;
using Microsoft.AspNetCore.Mvc.Versioning;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, configuration) => configuration.ReadFrom.Configuration(context.Configuration)
);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    string conString = "";

    if (Environment.GetEnvironmentVariable("DB_TYPE") == "pgsql")
    {
        conString = "Server=" + Environment.GetEnvironmentVariable("DB_HOST")
                  + ";Port=" + Environment.GetEnvironmentVariable("DB_PORT")
                  + ";Database=" + Environment.GetEnvironmentVariable("DB_NAME")
                  + ";User Id=" + Environment.GetEnvironmentVariable("DB_USER")
                  + ";Password=" + Environment.GetEnvironmentVariable("DB_PASS");

        options.UseNpgsql(conString);
    }
    else if (Environment.GetEnvironmentVariable("DB_TYPE") == "mysql")
    {
        conString = "Server=" + Environment.GetEnvironmentVariable("DB_HOST")
                  + ";Port=" + Environment.GetEnvironmentVariable("DB_PORT")
                  + ";Database=" + Environment.GetEnvironmentVariable("DB_NAME")
                  + ";Uid=" + Environment.GetEnvironmentVariable("DB_USER")
                  + ";Pwd=" + Environment.GetEnvironmentVariable("DB_PASS");

        options.UseMySql(conString, ServerVersion.AutoDetect(conString));
    }
    else
    {
        conString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseNpgsql(conString);
    }    
});

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(
        b => b.AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod());
});

builder.Services.AddIdentityServices(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "maERP.Server", Version = "v1" });
});

builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024; // 1 MB
    options.UseCaseSensitivePaths = true;
});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver")
    );
});

builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductSalesChannelRepository, ProductSalesChannelRepository>();
builder.Services.AddScoped<ISalesChannelRepository, SalesChannelRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<ITaxClassRepository, TaxClassRepository>();

builder.Services.AddHostedService<maERP.Server.Tasks.SalesChannelTasks.ProductDownloadTask>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Web}/{action=Index}/{id?}");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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
            new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = TimeSpan.FromSeconds(10)
            };

        context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
            new string[] { "Accept-Encoding" };

        await next();
    });

    app.UseExceptionHandler("/Home/Error");
    app.UseSerilogRequestLogging();

    app.MapControllers();    
}

app.UseAuthentication(); // where are you?
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

public partial class Program
{
}