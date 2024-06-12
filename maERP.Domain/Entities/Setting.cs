using maERP.Domain.Models.Common;

namespace maERP.Domain.Models;

public class Setting : BaseEntity, IBaseEntity
{
    public int Section { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}