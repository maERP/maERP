using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Services;
using maERP.Infrastructure.EmailService;
using maERP.Infrastructure.EmailService.Providers;
using maERP.Infrastructure.Logging;
using maERP.Infrastructure.PDF;
using maERP.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Email Service Registration
        services.AddScoped<IEmailProvider, SmtpEmailProvider>();
        services.AddScoped<IEmailProvider, Microsoft365EmailProvider>();
        services.AddScoped<IGraphMailSender, GraphMailSender>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<IEmailService, TenantAwareEmailService>();

        // Logging
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        // PDF Service
        services.AddScoped<IPdfService, PdfService>();

        // Server info (env-var-backed, immutable after startup)
        services.AddSingleton<IServerInfoService, ServerInfoService>();

        return services;
    }
}
