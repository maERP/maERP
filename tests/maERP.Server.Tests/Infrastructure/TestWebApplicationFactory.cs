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
using maERP.Domain.Entities;
using Microsoft.AspNetCore.Identity;

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

            // Ensure DbContext is created per request with proper tenant context injection
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseInMemoryDatabase(testDatabaseName);
                // Ensure sensitive data logging is disabled for performance
                options.EnableSensitiveDataLogging(false);
            }, ServiceLifetime.Scoped);

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

            // Register TestTenantContext as singleton to ensure the same instance is used across all requests
            // This is crucial for Global Query Filters to work correctly in tests
            services.AddSingleton<ITenantContext, TestTenantContext>();

            // Add Identity services for tests
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        });

        builder.UseEnvironment("Testing");

        // Ensure the environment variable is set for ApplicationDbContext
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }
}