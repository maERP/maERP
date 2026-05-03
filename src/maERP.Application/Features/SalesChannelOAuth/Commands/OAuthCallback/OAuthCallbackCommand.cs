using maERP.Application.Mediator;
using maERP.Domain.Dtos.SalesChannelOAuth;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SalesChannelOAuth.Commands.OAuthCallback;

public class OAuthCallbackCommand : IRequest<Result<OAuthCallbackResultDto>>
{
    public SalesChannelType Provider { get; set; }
    public string State { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
