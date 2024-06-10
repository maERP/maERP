using AutoMapper;
using maERP.Application.Features.TaxClass.Commands.TaxClassCreate;
using maERP.Application.Features.TaxClass.Commands.TaxClassDelete;
using maERP.Application.Features.TaxClass.Commands.TaxClassUpdate;
using maERP.Application.Features.TaxClass.Queries.TaxClassDetail;
using maERP.Application.Features.TaxClass.Queries.TaxClassList;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class TaxClassProfile : Profile
{
    public TaxClassProfile()
    {
        CreateMap<TaxClass, TaxClassListResponse>();
        CreateMap<TaxClass, TaxClassDetailResponse>();
        
        CreateMap<TaxClassCreateCommand, TaxClass>();
        CreateMap<TaxClassDeleteCommand, TaxClass>();
        CreateMap<TaxClassUpdateCommand, TaxClass>();
    }
}