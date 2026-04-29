using Asp.Versioning;
using maERP.Application.Contracts.Services;
using maERP.Domain.Dtos.ServerInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maERP.Server.Controllers.Api.V1;

[ApiController]
[AllowAnonymous]
[ApiVersion(1.0)]
[Route("/api/v{version:apiVersion}/[controller]")]
public class ServerInfoController(IServerInfoService serverInfo) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<ServerInfoResponseDto> Get() => Ok(new ServerInfoResponseDto
    {
        RegistrationEnabled = serverInfo.IsRegistrationEnabled,
        Version = serverInfo.Version
    });
}
