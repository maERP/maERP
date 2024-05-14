using maERP.Application.Contracts.SalesChannel;
using maERP.SalesChannels.Repositories;
using maERP.SalesChannels.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.SalesChannels;

public static class SalesChannelServiceRegistration
{
    public static IServiceCollection AddSalesChannelServices(this IServiceCollection services)
    {
        services.AddScoped<IProductImportRepository, ProductImportRepository>();
        services.AddScoped<IShopware5Repository, Shopware5Repository>();
        
        services.AddHostedService<Shopware5OrderImportTask>();
        services.AddHostedService<Shopware5ProductImportTask>();
        services.AddHostedService<WooCommerceProductImportTask>();

        return services;
    }
}