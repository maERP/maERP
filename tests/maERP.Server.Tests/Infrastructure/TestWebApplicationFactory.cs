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

            // For InMemory database, we need to ensure the database instance is shared
            // across all scopes to maintain consistency in tests
            services.AddSingleton<DbContextOptions<ApplicationDbContext>>(serviceProvider =>
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
                builder.UseInMemoryDatabase(testDatabaseName);
                builder.EnableSensitiveDataLogging(false);
                return builder.Options;
            });

            // Register DbContext as scoped, but using the singleton options
            services.AddScoped<ApplicationDbContext>((serviceProvider) =>
            {
                var options = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
                var tenantContext = serviceProvider.GetRequiredService<ITenantContext>();
                return new ApplicationDbContext(options, tenantContext);
            });

            // Remove existing authentication services
            var authServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IAuthenticationService));
            if (authServiceDescriptor != null)
                services.Remove(authServiceDescriptor);

            // Add test authentication
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Test";
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                })
                .AddScheme<TestAuthenticationOptions, TestAuthenticationHandler>(
                    "Test", options =>
                    {
                        // Provide defaults; tests can override via headers
                        options.UserId = CurrentUserHelper.DefaultUserId;
                        options.Username = "TestUser";
                        options.Roles = Array.Empty<string>();
                    });

            // Do not override authorization - use default ASP.NET Core authorization

            // Replace ITenantContext with test implementation
            var tenantContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITenantContext));
            if (tenantContextDescriptor != null)
                services.Remove(tenantContextDescriptor);

            // Register TestTenantContext as scoped to ensure proper isolation between tests
            services.AddScoped<ITenantContext, TestTenantContext>();

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
