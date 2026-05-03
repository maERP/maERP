using maERP.Application.Mediator;
using maERP.Domain.Dtos.SalesChannelOAuth;
using maERP.Domain.Enums;
using maERP.Domain.Wrapper;

namespace maERP.Application.Features.SalesChannelOAuth.Commands.OAuthStart;

public class OAuthStartCommand : IRequest<Result<OAuthStartResponseDto>>
{
    public Guid SalesChannelId { get; set; }
    public SalesChannelType Provider { get; set; }
    public string? UserId { get; set; }
}
