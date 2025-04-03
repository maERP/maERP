using Blazored.LocalStorage;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Providers;
using maERP.SharedUI.Services;
using maERP.SharedUI.Validators;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace maERP.SharedUI;

public static class UiServicesRegistration
{
    public static IServiceCollection AddUiServices(this IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();
        services.AddMudServices();
        services.AddAuthorizationCore();
        
        // Register ServerUrlProvider as singleton - this is safe and doesn't depend on scoped services
        services.AddSingleton<ServerUrlProvider>();
        
        // Register the ServerUrlService as scoped
        services.AddScoped<IServerUrlService, ServerUrlService>();
        
        // Use ServerUrlProvider to configure the HttpClient
        services.AddHttpClient<IHttpService, HttpService>((sp, client) => 
        {
            var urlProvider = sp.GetRequiredService<ServerUrlProvider>();
            client.BaseAddress = urlProvider.ServerUrl;
        });

        services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        services.AddScoped<ApiAuthenticationStateProvider>();

        services.AddScoped<AiModelsInputValidator>();
        services.AddScoped<AiPromptInputValidator>();
        services.AddScoped<CustomerInputValidator>();
        services.AddScoped<OrderInputValidator>();
        services.AddScoped<ProductInputValidator>();
        services.AddScoped<SalesChannelInputValidator>();
        services.AddScoped<TaxClassInputValidator>();
        services.AddScoped<WarehouseInputValidator>();
        
        return services;
    }
}