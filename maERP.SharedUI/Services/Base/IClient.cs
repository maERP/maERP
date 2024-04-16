namespace maERP.SharedUI.Services.Base;

public partial interface IClient
{
    public HttpClient HttpClient { get; }
}