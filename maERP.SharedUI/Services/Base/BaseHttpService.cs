using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace maERP.SharedUI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;
    protected readonly ILocalStorageService _localstorage;

    public BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localstorage = localStorage;
    }

    protected Response<T> ConvertApiExceptions<T>(ApiException ex)
    {
        if (ex.StatusCode == 400)
        {
            return new Response<T>
            {
                Message = "Invalid data was submitted",
                ValidationErrors = ex.Response,
                Success = false
            };
        }

        if (ex.StatusCode == 404)
        {
            return new Response<T>
            {
                Message = "Resource not found",
                Success = false
            };
        }

        return new Response<T>
        {
            Message = "An error occurred",
            Success = false
        };
    }

    protected async Task AddBearerToken()
    {
        if(await _localstorage.ContainKeyAsync("authToken"))
        {
            var token = await _localstorage.GetItemAsync<string>("authToken");
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}