using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.TaxClass;

public class TaxClassInputDto : ITaxClassInputModel
{
    public Guid Id { get; set; }
    public double TaxRate { get; set; }
}