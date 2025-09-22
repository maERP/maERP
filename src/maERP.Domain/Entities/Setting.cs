using maERP.Domain.Entities.Common;

namespace maERP.Domain.Entities;

public class Setting : BaseEntityWithoutTenant, IBaseEntityWithoutTenant
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}