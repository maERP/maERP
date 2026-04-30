using Asp.Versioning;
using maERP.Application.Features.Account.Commands.ChangePassword;
using maERP.Application.Features.Account.Commands.UpdateCurrentUser;
using maERP.Application.Features.Account.Queries.GetCurrentUser;
using maERP.Application.Mediator;
using maERP.Domain.Dtos.Account;
using maERP.Domain.Wrapper;
using maERP.Server.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[Authorize]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class AccountController(IMediator mediator) : ControllerBase
{
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Result<CurrentUserProfileDto>>> GetMe()
    {
        var response = await mediator.Send(new GetCurrentUserQuery());
        return response.ToActionResult();
    }

    [HttpPut("me")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateMe(UpdateCurrentUserCommand command)
    {
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }

    [HttpPost("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var response = await mediator.Send(command);
        return response.ToActionResult();
    }
}
