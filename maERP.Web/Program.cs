using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Blazored.LocalStorage;
using maERP.Web;
using maERP.Shared.Contracts;
using maERP.Shared.Services;
using maERP.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthHttpProvider>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IDataService<>), typeof(DataService<>));

await builder.Build().RunAsync();