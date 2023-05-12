namespace maERP.Shared.Dtos;
 
public class SalesChannelListDto
{
    public virtual uint Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Url { get; set; } = string.Empty;
}