using Blazored.LocalStorage;
using MudBlazor.Services;
using System.Reflection;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using maERP.SharedUI.Services;
using maERP.SharedUI.Services.Base;
using maERP.SharedUI.Contracts;
using maERP.SharedUI.Providers;

namespace maERP.SharedUI;

public static class UIServicesRegistration
{
    public static IServiceCollection AddUIServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBlazoredLocalStorage();
        services.AddMudServices();
        services.AddAuthorizationCore();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddHttpClient<IClient, Client>(Client => Client.BaseAddress = new Uri("https://localhost:8443/"));

        services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISalesChannelService, SalesChannelService>();
        services.AddScoped<ITaxClassService, TaxClassService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWarehouseService, WarehouseService>();

        return services;
    }
}