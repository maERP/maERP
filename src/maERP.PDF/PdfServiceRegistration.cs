using Microsoft.Extensions.DependencyInjection;

namespace maERP.PDF;

public static class PdfServiceRegistration
{
    public static IServiceCollection AddPdfServices(this IServiceCollection services)
    {
        // services.AddScoped<IPdfService, PdfService>();
        // services.AddScoped<IPdfGenerator, PdfGenerator>();
        return services;
    }
}