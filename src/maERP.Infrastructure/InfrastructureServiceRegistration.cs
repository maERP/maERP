using maERP.Application.Contracts.Infrastructure;
using maERP.Application.Contracts.Logging;
using maERP.Application.Models.Email;
using maERP.Infrastructure.Logging;
using maERP.Infrastructure.PDF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace maERP.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService.EmailService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
        
        // services.Configure<PdfOptions>(configuration.GetSection("PdfOptions"));
        services.AddScoped<IPdfService, PdfService>();

        return services;
    }
}