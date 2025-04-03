using maERP.Application.Contracts.Logging;
using maERP.Application.Extensions;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;
using maERP.Domain.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Linq.Dynamic.Core;

namespace maERP.Application.Features.User.Queries.UserList;

public class UserListHandler : IRequestHandler<UserListQuery, PaginatedResult<UserListDto>>
{
    private readonly IAppLogger<UserListHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserListHandler(
        IAppLogger<UserListHandler> logger,
        UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    
    public async Task<PaginatedResult<UserListDto>> Handle(UserListQuery request, CancellationToken cancellationToken)
    {
        // var userFilterSpec = new UserFilterSpecification(request.SearchString);

        _logger.LogInformation("Handle UserListQuery: {0}", request);

        // Manuelles Mapping mit LINQ-Projektion statt AutoMapper
        var query = _userManager.Users
            //.Specify(userFilterSpec)
            .Select(u => new UserListDto
            {
                Id = u.Id,
                Email = u.Email ?? string.Empty,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                DateCreated = u.DateCreated
            });

        if (request.OrderBy.Any() != true)
        {
            return await query
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
        }

        var ordering = string.Join(",", request.OrderBy);

        return await query
            .OrderBy(ordering)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
    }
}