using AutoMapper;
using maERP.Application.Features.SalesChannel.Commands.CreateSalesChannel;
using maERP.Application.Features.SalesChannel.Commands.DeleteSalesChannel;
using maERP.Application.Features.SalesChannel.Commands.UpdateSalesChannel;
using maERP.Application.Features.SalesChannel.Queries.GetSalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.GetSalesChannels;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class SalesChannelProfile : Profile
{
    public SalesChannelProfile()
    {
        CreateMap<SalesChannel, GetSalesChannelsResponse>();
        CreateMap<SalesChannel, GetSalesChannelDetailResponse>();
        
        CreateMap<CreateSalesChannelCommand, SalesChannel>();
        CreateMap<DeleteSalesChannelCommand, SalesChannel>();
        CreateMap<UpdateSalesChannelCommand, SalesChannel>();
    }
}