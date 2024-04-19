using AutoMapper;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannelCommand;
using maERP.Domain.Models;
using maERP.Application.Dtos.SalesChannel;

namespace maERP.Application.MappingProfiles;

public class SalesChannelProfile : Profile
{
    public SalesChannelProfile()
    {
        CreateMap<SalesChannel, SalesChannelListDto>();
        CreateMap<SalesChannel, SalesChannelDetailDto>();
        
        CreateMap<CreateSalesChannelCommand, SalesChannel>();
        CreateMap<DeleteSalesChannelCommand, SalesChannel>();
        CreateMap<UpdateSalesChannelCommand, SalesChannel>();
    }
}