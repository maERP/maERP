using AutoMapper;
using maERP.Application.Dtos.User;
using maERP.Application.Features.User.Commands.CreateUserCommand;
using maERP.Application.Features.User.Commands.UpdateUserCommand;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserListDto>();
        CreateMap<User, UserDetailDto>();
        
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}