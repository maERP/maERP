using System.Reflection;
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

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpClient<IHttpService, HttpService>(client => client.BaseAddress = new Uri("https://localhost:8443/"));

        services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        services.AddScoped<ApiAuthenticationStateProvider>();

        services.AddScoped<AiModelCreateValidator>();
        services.AddScoped<AiModelUpdateValidator>();
        services.AddScoped<AiPromptCreateValidator>();
        services.AddScoped<AiPromptUpdateValidator>();
        services.AddScoped<CustomerCreateValidator>();
        services.AddScoped<CustomerUpdateValidator>();
        services.AddScoped<OrderCreateValidator>();
        services.AddScoped<OrderUpdateValidator>();
        services.AddScoped<ProductCreateValidator>();
        services.AddScoped<ProductUpdateValidator>();
        services.AddScoped<SalesChannelCreateValidator>();
        services.AddScoped<SalesChannelUpdateValidator>();
        services.AddScoped<TaxClassCreateValidator>();
        services.AddScoped<TaxClassUpdateValidator>();
        services.AddScoped<WarehouseCreateValidator>();
        services.AddScoped<WarehouseUpdateValidator>();
        
        return services;
    }
}