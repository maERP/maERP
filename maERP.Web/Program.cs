using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using maERP.Web;
using maERP.Shared.Contracts;
using maERP.Shared.Services;
using maERP.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

Globals.ServerBaseUrl = builder.Configuration["ServerBaseUrl"];

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());
builder.Services.AddScoped(typeof(IDataService<>), typeof(DataService<>));

await builder.Build().RunAsync();