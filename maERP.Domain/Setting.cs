using maERP.Domain.Common;

namespace maERP.Domain;

public class Setting : BaseEntity
{
    public int Section { get; set; }
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}