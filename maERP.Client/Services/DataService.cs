using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using maERP.Shared.Dtos.User;
using maERP.Client.Contracts;

// using static Android.Provider.Settings;

namespace maERP.Client.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        public async Task<bool> Login(string server, string email, string password)
        {
            using (var client = new HttpClient())
            {
                string requestUrl = server + "/api/Account/login";
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var loginData = new Dictionary<string, string>
                {
                    {"email", email},
                    {"password", password}
                };

                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsJsonAsync(requestUrl, loginData); // .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);

                    var responseObj = JsonConvert.DeserializeObject<LoginResponseDto>(result);

                    Globals.ServerBaseUrl = server;
                    Globals.AccessToken = responseObj.Token;
                    Globals.RefreshToken = responseObj.Token;

                    response.Dispose();

                    return true;
                }

                response.Dispose();
                return false;
            }
        }

        public async Task<T> Request(string method, string path, object payload = null)
        {
            using (var client = new HttpClient())
            {
                string requestUrl = Globals.ServerBaseUrl + "/api" + path;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Globals.AccessToken);

                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                HttpResponseMessage response = new HttpResponseMessage();

                if (method == "GET")
                {
                    response = await client.GetAsync(requestUrl).ConfigureAwait(false);
                }
                else if (method == "POST")
                {
                    response = await client.PostAsJsonAsync(requestUrl, payload).ConfigureAwait(false);
                }
                else
                {
                    Console.WriteLine("Bearer: " + Globals.AccessToken);
                    Console.WriteLine(requestUrl);
                    Console.WriteLine(response.Headers);
                    Console.WriteLine(response.TrailingHeaders);
                    Console.WriteLine(response.Content.ReadAsStream());

                    throw new Exception();
                }

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;

                    var responseObj = JsonConvert.DeserializeObject<T>(result);

                    response.Dispose();

                    return responseObj;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("Bearer: " + Globals.AccessToken);
                    Console.WriteLine(requestUrl);
                    Console.WriteLine(response.Headers);
                    Console.WriteLine(response.TrailingHeaders);
                    Console.WriteLine(response.Content.ReadAsStream());
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Bearer: " + Globals.AccessToken);
                    Console.WriteLine(requestUrl);
                    Console.WriteLine(response.Headers);
                    Console.WriteLine(response.TrailingHeaders);
                    Console.WriteLine(response.Content.ReadAsStream());
                }
                else
                {
                    throw new Exception();
                }
            }

            throw new Exception();
        }
    }
}