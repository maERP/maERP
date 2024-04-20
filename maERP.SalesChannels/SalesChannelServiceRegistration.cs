using maERP.Application.Contracts.SalesChannel;
using maERP.Persistence.Repositories.SalesChannelRepositories;
using maERP.SalesChannels.Repositories;
using Microsoft.Extensions.DependencyInjection;
using maERP.SalesChannels.Tasks;
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