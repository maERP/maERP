using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Infrastructure.EmailService;
using maERP.Infrastructure.EmailService.Providers;
using maERP.Infrastructure.Logging;
using maERP.Infrastructure.PDF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Email Service Registration
        services.AddScoped<SmtpEmailProvider>();
        services.AddScoped<SendGridEmailProvider>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.AddScoped<IEmailService, TenantAwareEmailService>();

        // Logging
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        // PDF Service
        services.AddScoped<IPdfService, PdfService>();

        return services;
    }
}