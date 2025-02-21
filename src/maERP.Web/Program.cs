using System.Globalization;
using maERP.SharedUI;
using maERP.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddUiServices();

CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("de-DE");
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("de-DE");

await builder.Build().RunAsync();