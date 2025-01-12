namespace maERP.SharedUI.Services.Base;

// ReSharper disable once RedundantExtendsListEntry
public partial class Client : IClient
{
    public HttpClient HttpClient
    {
        get
        {
            return _httpClient;
        }
    }
}