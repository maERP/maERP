using AutoMapper;
using maERP.Domain;
using maERP.Application.Dtos.User;
using maERP.Application.Features.User.Commands.CreateUserCommand;
using maERP.Application.Features.User.Commands.DeleteUserCommand;
using maERP.Application.Features.User.Commands.UpdateUserCommand;

namespace maERP.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserListDto>();
        CreateMap<User, UserDetailDto>();
        
        CreateMap<CreateUserCommand, User>();
        CreateMap<DeleteUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
    }
}