using System.Linq.Dynamic.Core;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.Setting;
using maERP.Domain.Wrapper;
using MediatR;
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

        List<Domain.Entities.Setting> entities;
        
        if (request.OrderBy.Any() != true)
        {
            entities = await _SettingRepository.Entities
               .Specify(settingFilterSpec)
               .ToListAsync(cancellationToken);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            entities = await _SettingRepository.Entities
                .Specify(settingFilterSpec)
                .OrderBy(ordering)
                .ToListAsync(cancellationToken);
        }
            
        return MapToListDtoAndPaginate(entities, request.PageNumber, request.PageSize);
    }
    
    private PaginatedResult<SettingListDto> MapToListDtoAndPaginate(
        List<Domain.Entities.Setting> entities, int pageNumber, int pageSize)
    {
        var dtos = entities.Select(entity => new SettingListDto
        {
            Id = entity.Id,
            Key = entity.Key,
            Value = entity.Value
        }).ToList();
        
        // Erstelle paginierte Ergebnisse
        var totalCount = dtos.Count;
        var pagedItems = dtos
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            
        return PaginatedResult<SettingListDto>.Success(pagedItems, totalCount, pageNumber, pageSize);
    }
}