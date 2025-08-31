using System.Data.Common;
using maERP.Persistence.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using maERP.Application.Contracts.Services;

namespace maERP.Server.Tests.Infrastructure;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (dbContextDescriptor != null)
                services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor != null)
                services.Remove(dbConnectionDescriptor);

            // Use a database name per test class to avoid conflicts but share within each test class
            var testDatabaseName = Environment.GetEnvironmentVariable("TEST_DB_NAME") ?? "TestDb_" + Guid.NewGuid();
            services.AddDbContext<ApplicationDbContext>((container, options) =>
            {
                options.UseInMemoryDatabase(testDatabaseName);
            });

            // Remove existing authentication services
            var authServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IAuthenticationService));
            if (authServiceDescriptor != null)
                services.Remove(authServiceDescriptor);

            // Add test authentication
            services.AddAuthentication("Test")
                .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                    "Test", options => { });

            // Override authorization to allow all
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAssertion(_ => true)
                    .Build();
            });

            // Replace ITenantContext with test implementation
            var tenantContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITenantContext));
            if (tenantContextDescriptor != null)
                services.Remove(tenantContextDescriptor);

            // Register TestTenantContext as singleton but ensure proper request isolation through AsyncLocal  
            services.AddSingleton<ITenantContext, TestTenantContext>();
        });

        builder.UseEnvironment("Testing");
    }
}