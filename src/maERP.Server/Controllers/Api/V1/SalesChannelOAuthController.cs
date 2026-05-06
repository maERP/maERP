using System.Security.Claims;
using Asp.Versioning;
using maERP.Application.Features.SalesChannelOAuth.Commands.OAuthCallback;
using maERP.Application.Features.SalesChannelOAuth.Commands.OAuthDisconnect;
using maERP.Application.Features.SalesChannelOAuth.Commands.OAuthStart;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.SalesChannelOAuth;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

/// <summary>
/// OAuth Authorization-Code flow for SalesChannel providers (eBay, Amazon).
///
/// <para>
/// <c>start</c> and <c>disconnect</c> are authenticated and require the X-Tenant-Id header.
/// <c>callback</c> is anonymous because the third-party provider redirects the user's browser
/// back here without our auth context — tenant resolution happens via the persisted OAuthState
/// row inside <c>OAuthCallbackHandler</c>.
/// </para>
/// </summary>
[ApiController]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/saleschannels")]
public class SalesChannelOAuthController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// Begin OAuth flow for the channel: returns the authorize URL the Client should open in
    /// the system browser, plus the opaque state token (also embedded in that URL).
    /// </summary>
    [HttpPost("{id:guid}/oauth/{provider}/start")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<OAuthStartResponseDto>>> Start(Guid id, string provider)
    {
        if (!Enum.TryParse<SalesChannelType>(provider, ignoreCase: true, out var providerEnum))
        {
            return BadRequest(new { Error = $"Unknown provider '{provider}'." });
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                  ?? User.FindFirstValue("sub");

        var response = await mediator.Send(new OAuthStartCommand
        {
            SalesChannelId = id,
            Provider = providerEnum,
            UserId = userId,
        });
        return response.ToActionResult();
    }

    /// <summary>
    /// Browser landing page after the third-party redirect. Renders a small HTML success/failure
    /// page — the user closes the tab and returns to the Client which polls for the connected
    /// state. Anonymous: no JWT, no tenant header. State token is the CSRF token (DB-validated,
    /// single-use).
    /// </summary>
    [HttpGet("oauth/{provider}/callback")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ContentResult> Callback(
        string provider,
        [FromQuery] string? code,
        [FromQuery] string? state,
        [FromQuery] string? error,
        [FromQuery(Name = "error_description")] string? errorDescription)
    {
        if (!string.IsNullOrEmpty(error))
        {
            return Html(success: false,
                $"OAuth provider returned an error: {error}{(string.IsNullOrEmpty(errorDescription) ? string.Empty : " — " + errorDescription)}");
        }

        if (!Enum.TryParse<SalesChannelType>(provider, ignoreCase: true, out var providerEnum))
        {
            return Html(success: false, $"Unknown provider '{provider}'.");
        }

        if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(state))
        {
            return Html(success: false, "Missing 'code' or 'state' query parameter.");
        }

        var result = await mediator.Send(new OAuthCallbackCommand
        {
            Provider = providerEnum,
            Code = code,
            State = state,
        });

        if (!result.Succeeded)
        {
            return Html(success: false, string.Join(" ", result.Messages));
        }

        return Html(success: true,
            $"Successfully connected {providerEnum}. You can close this tab and return to maERP.");
    }

    /// <summary>Clear the channel's stored refresh + access tokens.</summary>
    [HttpPost("{id:guid}/oauth/{provider}/disconnect")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<int>>> Disconnect(Guid id, string provider)
    {
        // Provider parameter is informational here — the channel knows its own type. We accept
        // it in the route so the URL shape mirrors /start and /callback (consistent client UX).
        _ = provider;
        var response = await mediator.Send(new OAuthDisconnectCommand { SalesChannelId = id });
        return response.ToActionResult();
    }

    private static ContentResult Html(bool success, string message)
    {
        var color = success ? "#16a34a" : "#dc2626";
        var heading = success ? "Successfully connected" : "OAuth flow failed";
        var body = $@"<!doctype html>
<html lang=""en"">
<head>
  <meta charset=""utf-8"" />
  <title>maERP OAuth</title>
  <style>
    body {{ font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
           background: #f3f4f6; margin: 0; padding: 48px 24px; color: #111827; }}
    .card {{ max-width: 520px; margin: 0 auto; background: #fff; Border-radius: 12px;
             box-shadow: 0 4px 12px rgba(0,0,0,0.06); padding: 32px; }}
    h1 {{ color: {color}; margin-top: 0; font-size: 22px; }}
    p {{ line-height: 1.5; }}
    .hint {{ color: #6b7280; font-size: 14px; margin-top: 24px; }}
  </style>
</head>
<body>
  <div class=""card"">
    <h1>{System.Net.WebUtility.HtmlEncode(heading)}</h1>
    <p>{System.Net.WebUtility.HtmlEncode(message)}</p>
    <p class=""hint"">You can close this browser tab.</p>
  </div>
</body>
</html>";

        return new ContentResult
        {
            Content = body,
            ContentType = "text/html; charset=utf-8",
            StatusCode = StatusCodes.Status200OK,
        };
    }
}
