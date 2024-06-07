using AutoMapper;
using maERP.Application.Features.TaxClass.Commands.CreateTaxClass;
using maERP.Application.Features.TaxClass.Commands.DeleteTaxClass;
using maERP.Application.Features.TaxClass.Commands.UpdateTaxClass;
using maERP.Application.Features.TaxClass.Queries.GetTaxClassDetail;
using maERP.Application.Features.TaxClass.Queries.GetTaxClasses;
using maERP.Domain.Models;

namespace maERP.Application.MappingProfiles;

public class TaxClassProfile : Profile
{
    public TaxClassProfile()
    {
        CreateMap<TaxClass, GetTaxClassesResponse>();
        CreateMap<TaxClass, GetTaxClassDetailResponse>();
        
        CreateMap<CreateTaxClassCommand, TaxClass>();
        CreateMap<DeleteTaxClassCommand, TaxClass>();
        CreateMap<UpdateTaxClassCommand, TaxClass>();
    }
}