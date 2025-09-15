using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using maERP.Application.Mediator;
using Microsoft.EntityFrameworkCore;

namespace maERP.Application.Features.Setting.Queries.SettingList;

public class SettingListHandler : IRequestHandler<SettingListQuery, PaginatedResult<SettingListDto>>
{
    private readonly IAppLogger<SettingListHandler> _logger;
    private readonly ISettingRepository _SettingRepository;

    public SettingListHandler(
        IAppLogger<SettingListHandler> logger,
        ISettingRepository settingRepository)
    {
        _logger = logger;
        _SettingRepository = settingRepository;
    }

    public async Task<PaginatedResult<SettingListDto>> Handle(SettingListQuery request, CancellationToken cancellationToken)
    {
        var settingFilterSpec = new SettingFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle SettingListQuery: {0}", request);

        IQueryable<Domain.Entities.Setting> query = _SettingRepository.Entities.Specify(settingFilterSpec);

        if (request.OrderBy.Any())
        {
            var ordering = string.Join(",", request.OrderBy);
            query = query.OrderBy(ordering);
        }

        // Use the standard pagination extension which handles zero-based pagination correctly
        var paginatedEntities = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);

        // Map to DTOs
        var dtos = paginatedEntities.Data.Select(entity => new SettingListDto
        {
            Id = entity.Id,
            Key = entity.Key,
            Value = entity.Value
        }).ToList();

        return PaginatedResult<SettingListDto>.Success(dtos, paginatedEntities.TotalCount, paginatedEntities.CurrentPage, paginatedEntities.PageSize);
    }
}