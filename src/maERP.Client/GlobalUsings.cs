global using System.Collections.Immutable;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using maERP.Client.Core.Models;
global using maERP.Client.Features.Authentication.Models;
global using maERP.Client.Features.Authentication.Views;
global using maERP.Client.Features.Dashboard.Models;
global using maERP.Client.Features.Dashboard.Views;
global using maERP.Client.Services.Api;
global using maERP.Client.Services.Api.Clients;
global using maERP.Client.Services.Api.Handlers;
global using maERP.Client.Services.Authentication;
global using maERP.Client.Services.Tenant;
global using maERP.Domain.Dtos;
global using maERP.Domain.Wrapper;
global using ApplicationExecutionState = Windows.ApplicationModel.Activation.ApplicationExecutionState;

[assembly: Uno.Extensions.Reactive.Config.BindableGenerationTool(3)]
