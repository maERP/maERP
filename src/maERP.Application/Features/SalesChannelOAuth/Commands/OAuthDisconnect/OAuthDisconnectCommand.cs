using maERP.Application.Mediator;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SalesChannelOAuth.Commands.OAuthDisconnect;

public class OAuthDisconnectCommand : IRequest<Result<int>>
{
    public Guid SalesChannelId { get; set; }
}
