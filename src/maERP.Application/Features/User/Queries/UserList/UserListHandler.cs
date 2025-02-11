using System.Linq.Dynamic.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using maERP.Application.Contracts.Logging;
using maERP.Application.Contracts.Persistence;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace maERP.Application.Features.User.Queries.UserList;

public class UserListHandler : IRequestHandler<UserListQuery, PaginatedResult<UserListDto>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<UserListHandler> _logger;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserListHandler(IMapper mapper,
        IAppLogger<UserListHandler> logger, 
        IUserRepository userRepository,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
        _userManager = userManager;
    }
    public async Task<PaginatedResult<UserListDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handle UserListQuery: {0}", request);

        if (request.OrderBy.Any() != true)
        {
            return await _userManager.Users
                .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
        else
        {
            var ordering = string.Join(",", request.OrderBy);

            return await _userManager.Users
                .OrderBy(ordering)
                .ProjectTo<UserListDto>(_mapper.ConfigurationProvider)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}