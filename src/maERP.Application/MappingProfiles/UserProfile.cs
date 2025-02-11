using AutoMapper;
using maERP.Application.Features.User.Commands.UserCreate;
using maERP.Application.Features.User.Commands.UserUpdate;
using maERP.Domain.Dtos.User;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDetailDto>();
        CreateMap<ApplicationUser, UserListDto>();
        
        CreateMap<UserCreateCommand, ApplicationUser>();
        CreateMap<UserUpdateCommand, ApplicationUser>();
    }
}