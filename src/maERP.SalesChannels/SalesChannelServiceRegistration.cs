using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Repositories;
using maERP.SalesChannels.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.SalesChannels;

public static class SalesChannelServiceRegistration
{
    public static IServiceCollection AddSalesChannelServices(this IServiceCollection services)
    {
        services.AddScoped<IProductImportRepository, ProductImportRepository>();
        services.AddScoped<IOrderImportRepository, OrderImportRepository>();

        services.AddHostedService<Shopware5ProductImportTask>();
        services.AddHostedService<Shopware5OrderImportTask>();
        services.AddHostedService<WooCommerceProductImportTask>();
        services.AddHostedService<WooCommerceOrderImportTask>();

        return services;
    }
}