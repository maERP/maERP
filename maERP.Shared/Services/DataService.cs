#nullable disable

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using maERP.Shared.Models.Identity;
using System.Text.Json;

namespace maERP.Shared.Services;

public interface IDataService<T> where T : class
{
    public Task<AuthResponse> Login(AuthRequest authRequest);
    public Task<RegistrationResponse> RegisterAsync(RegistrationRequest registrationRequest);
    public Task<T> Request(string method, string path, object payload = null);
}

public class DataService<T> : IDataService<T> where T : class
{
    private readonly ILocalStorageService _localStorage;
    
    public DataService(ILocalStorageService localStorage)
    {
        this._localStorage = localStorage;
    }
    
    public async Task<AuthResponse> Login(AuthRequest authRequest)
    {
        using (var client = new HttpClient())
        {

            string requestUrl = authRequest.Server + "/api/User/login";
            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000));
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var loginData = new Dictionary<string, string>
            {
                {"email", authRequest.Email},
                {"password", authRequest.Password}
            };

            var response = await client.PostAsJsonAsync(requestUrl, loginData).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                response.Dispose();

                // TODO Check format of response
                return JsonSerializer.Deserialize<AuthResponse>(result);
            }

            response.Dispose();
            return null;
        }
    }

    public async Task<T> Request(string method, string path, object payload = null)
    {
        try
        {
            using (var client = new HttpClient())
            {
                string baseUrl = await _localStorage.GetItemAsync<string>("baseUrl");
                string requestUrl = baseUrl + "/api" + path;
                // string accessToken = maERP.Shared.Globals.AccessToken;
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000));

                // add token
                if (await _localStorage.ContainKeyAsync("token"))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer",
                            await _localStorage.GetItemAsync<string>("token"));
                }
                
                HttpResponseMessage response = new HttpResponseMessage();

                if (method == "GET")
                {
                    response = await client.GetAsync(requestUrl).ConfigureAwait(false);

                }
                else if (method == "POST")
                {
                    response = await client.PostAsJsonAsync(requestUrl, payload).ConfigureAwait(false);
                }
                else if (method == "PUT")
                {
                    response = await client.PutAsJsonAsync(requestUrl, payload).ConfigureAwait(false);
                }
                else if (method == "DELETE")
                {
                    response = await client.DeleteAsync(requestUrl).ConfigureAwait(false);
                }
                else
                {
                    throw new Exception();
                }

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var responseObj = JsonSerializer.Deserialize<T>(result);
                        response.Dispose();

                        return responseObj;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    throw new Exception();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Not Found");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Not Authorized");
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        throw new Exception();
    }

    public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest registrationRequest)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}