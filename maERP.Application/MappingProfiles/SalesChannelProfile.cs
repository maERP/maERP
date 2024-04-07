using AutoMapper;
using maERP.Domain;
using maERP.Application.Dtos.SalesChannel;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannelCommand;
using maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannelCommand;

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