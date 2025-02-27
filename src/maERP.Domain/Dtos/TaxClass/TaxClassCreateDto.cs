using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.TaxClass;

public class TaxClassCreateDto : ITaxClassInputModel
{
    public int Id { get; set; }
    public double TaxRate { get; set; }
}