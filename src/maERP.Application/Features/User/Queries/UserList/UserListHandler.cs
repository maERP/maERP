using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Extensions;
using maERP.Application.Specifications;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;

namespace maERP.Application.Features.User.Queries.UserList;

public class UserListHandler : IRequestHandler<UserListQuery, PaginatedResult<UserListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserListHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserListHandler(IMapper mapper,
        IAppLogger<UserListHandler> logger,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _logger = logger;
        _userManager = userManager;
    }
    public async Task<PaginatedResult<UserListDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        // var userFilterSpec = new UserFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle UserListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _userManager.Users
                //.Specify(userFilterSpec)
                .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await _userManager.Users
            //.Specify(userFilterSpec)
            .OrderBy(ordering)
            .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}