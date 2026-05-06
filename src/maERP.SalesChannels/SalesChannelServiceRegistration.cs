using maERP.Application;
using maERP.SalesChannels.Abstractions;
using maERP.SalesChannels.Connectors.Amazon;
using maERP.SalesChannels.Connectors.Ebay;
using maERP.SalesChannels.Connectors.Pos;
using maERP.SalesChannels.Connectors.Shopware5;
using maERP.SalesChannels.Connectors.Shopware6;
using maERP.SalesChannels.Connectors.WooCommerce;
using maERP.SalesChannels.Contracts;
using maERP.SalesChannels.Models.Amazon;
using maERP.SalesChannels.Models.eBay;
using maERP.SalesChannels.Models.Shopware6;
using maERP.SalesChannels.Orchestration;
using maERP.SalesChannels.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace maERP.SalesChannels;

public static class SalesChannelServiceRegistration
{
    /// <param name="includeBackgroundServices">
    /// When false, the orchestrator hosted service is not registered — useful in integration
    /// tests where the test host owns scheduling.
    /// </param>
    public static IServiceCollection AddSalesChannelServices(this IServiceCollection services, bool includeBackgroundServices = true)
    {
        services.AddScoped<IProductImportRepository, ProductImportRepository>();
        services.AddScoped<ISalesImportRepository, SalesImportRepository>();
        services.AddScoped<ICustomerImportRepository, CustomerImportRepository>();
        // Auth helpers are singletons because they hold per-channel access-token caches; they
        // resolve the scoped IOAuthAppSettingsService internally via IServiceScopeFactory.
        services.AddSingleton<EbayAuthHelper>();
        services.AddSingleton<AmazonAuthHelper>();
        services.AddSingleton<Shopware6AuthHelper>();

        // Per-channel typed HttpClients with Polly resilience. Connectors get the matching
        // client by name from IHttpClientFactory via the SalesChannelContext.
        services.AddHttpClient("shopware5").AddPollyHandlers();
        services.AddHttpClient("shopware6").AddPollyHandlers();
        services.AddHttpClient("woocommerce").AddPollyHandlers();
        services.AddHttpClient("ebay").AddPollyHandlers();
        services.AddHttpClient("amazon").AddPollyHandlers();
        // Dedicated client for the LWA token endpoint — different host (api.amazon.com) and
        // shorter Polly settings make sense; for now we share the default policy.
        services.AddHttpClient("amazon-lwa").AddPollyHandlers();

        // Connectors (one per SalesChannelType). Resolved through the registry, never via
        // direct DI — keeps the channel-specific switch in one place.
        services.AddScoped<ISalesChannelConnector, PosConnector>();
        services.AddScoped<ISalesChannelConnector, Shopware5Connector>();
        services.AddScoped<ISalesChannelConnector, Shopware6Connector>();
        services.AddScoped<ISalesChannelConnector, WooCommerceConnector>();
        services.AddScoped<ISalesChannelConnector, EbayConnector>();
        services.AddScoped<ISalesChannelConnector, AmazonConnector>();

        services.AddScoped<ISalesChannelConnectorRegistry>(sp =>
            new SalesChannelConnectorRegistry(sp.GetServices<ISalesChannelConnector>()));

        services.AddScoped<ChannelExportOutboxEnqueuer>();
        services.AddScoped<SalesChannelContextFactory>();
        services.AddScoped<SyncDispatcher>();
        services.AddScoped<OutboxDrainer>();
        if (includeBackgroundServices)
        {
            services.AddHostedService<SalesChannelOrchestrator>();
        }

        services.RegisterHandlersFromAssembly(typeof(SalesChannelServiceRegistration).Assembly);

        // Legacy per-channel hosted-service tasks (Tasks/Shopware5*, /WooCommerce*, /Ebay*) are
        // superseded by SalesChannelOrchestrator dispatching through the connectors. The .cs files
        // remain in the repo as historical reference until the PR 16 cleanup deletes them.

        return services;
    }
}
