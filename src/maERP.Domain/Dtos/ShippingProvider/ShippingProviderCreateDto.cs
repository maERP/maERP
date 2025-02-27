using maERP.Domain.Interfaces;

namespace maERP.Domain.Dtos.ShippingProvider;

public class ShippingProviderCreateDto : IShippingProviderInputModel
{
    public string Name { get; set; } = string.Empty;
}