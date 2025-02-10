using AutoMapper;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelCreate;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelDelete;
using maERP.Application.Features.SalesChannel.Commands.SalesChannelUpdate;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelDetail;
using maERP.Application.Features.SalesChannel.Queries.SalesChannelList;
using maERP.Domain.Dtos.SalesChannel;
using maERP.Domain.Entities;

namespace maERP.Application.MappingProfiles;

public class SalesChannelProfile : Profile
{
    public SalesChannelProfile()
    {
        CreateMap<SalesChannel, SalesChannelDetailDto>();
        CreateMap<SalesChannel, SalesChannelListDto>();
        
        CreateMap<SalesChannelCreateCommand, SalesChannel>();
        CreateMap<SalesChanneLDeleteCommand, SalesChannel>();
        CreateMap<SalesChannelUpdateCommand, SalesChannel>();
    }
}