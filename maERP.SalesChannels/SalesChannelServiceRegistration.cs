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
        
        services.AddHostedService<ProductDownloadTask>();
        services.AddHostedService<OrderDownloadTask>();

        return services;
    }
}