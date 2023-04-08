namespace maERP.Shared.Dtos;
 
public class SalesChannelUpdateDto : SalesChannelBaseDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}