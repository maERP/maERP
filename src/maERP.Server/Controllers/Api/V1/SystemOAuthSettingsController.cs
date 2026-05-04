using Asp.Versioning;
using maERP.Application.Features.SystemOAuthSettings.Commands.SystemOAuthSettingsUpsert;
using maERP.Application.Features.SystemOAuthSettings.Queries.SystemOAuthSettingsDetail;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.SystemOAuthSettings;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

/// <summary>
/// Bundle CRUD over the system-wide <c>OAuth.{Provider}.*</c> Setting rows. Superadmin-only —
/// these credentials apply across tenants. Tenant-level overrides live in
/// <see cref="TenantOAuthAppSettingsController"/>.
/// </summary>
[ApiController]
[Authorize(Roles = "Superadmin")]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/superadmin/oauth-app-settings")]
public class SystemOAuthSettingsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{provider}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Result<SystemOAuthSettingsDto>>> Detail(string provider)
    {
        if (!Enum.TryParse<SalesChannelType>(provider, ignoreCase: true, out var providerEnum))
        {
            return BadRequest(new { Error = $"Unknown provider '{provider}'." });
        }

        var response = await mediator.Send(new SystemOAuthSettingsDetailQuery { Provider = providerEnum });
        return response.ToActionResult();
    }

    [HttpPut("{provider}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Upsert(string provider, [FromBody] SystemOAuthSettingsInputDto input)
    {
        if (!Enum.TryParse<SalesChannelType>(provider, ignoreCase: true, out var providerEnum))
        {
            return BadRequest(new { Error = $"Unknown provider '{provider}'." });
        }

        var response = await mediator.Send(new SystemOAuthSettingsUpsertCommand
        {
            Provider = providerEnum,
            ClientId = input.ClientId,
            ClientSecret = input.ClientSecret,
            RuName = input.RuName,
            RedirectUri = input.RedirectUri,
            AuthorizationEndpoint = input.AuthorizationEndpoint,
            TokenEndpoint = input.TokenEndpoint,
            Scopes = input.Scopes,
            UseSandbox = input.UseSandbox,
            PublicBaseUrl = input.PublicBaseUrl,
        });
        return response.ToActionResult();
    }
}
