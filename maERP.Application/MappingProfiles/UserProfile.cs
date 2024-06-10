using AutoMapper;
using maERP.Application.Features.User.Commands.UserCreate;
using maERP.Application.Features.User.Commands.UserUpdate;
using maERP.Application.Features.User.Queries.UserDetail;
using maERP.Application.Features.User.Queries.UserList;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserListResponse>();
        CreateMap<ApplicationUser, UserDetailResponse>();
        
        CreateMap<UserCreateCommand, ApplicationUser>();
        CreateMap<UserUpdateCommand, ApplicationUser>();
    }
}