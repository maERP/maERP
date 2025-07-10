using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;

namespace maERP.Application.Features.Setting.Queries.SettingDetail;

/// <summary>
/// Query for retrieving detailed information about a specific setting.
/// Implements IRequest to work with MediatR, returning setting details wrapped in a Result.
/// </summary>
public class SettingDetailQuery : IRequest<Result<SettingDetailDto>>
{
    /// <summary>
    /// The unique identifier of the setting to retrieve
    /// </summary>
    public int Id { get; set; }
}