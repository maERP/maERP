using System.Text;
using maERP.Application.Contracts.Identity;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Contracts.Services;
using maERP.Application.Models.Identity;
using maERP.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace maERP.Identity;

public static class IdentityServicesRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Configure JwtSettings from database
        services.AddScoped<JwtSettings>(serviceProvider =>
        {
            var settingsService = serviceProvider.GetRequiredService<ISettingsService>();
            return settingsService.GetJwtSettingsAsync().GetAwaiter().GetResult();
        });

        services.AddTransient<Application.Contracts.Identity.IAuthService, AuthService>();
        services.AddTransient<Application.Contracts.Identity.IUserService, UserService>();
        services.AddScoped<ITenantContext, TenantContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

        return services;
    }
}
