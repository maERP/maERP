using maERP.Client.Contracts.Services;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;
using maERP.Data.Dtos.User;

namespace maERP.Client.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        private string _baseUrl;

        public async Task<LoginResponseDto> Login(string server, string email, string password)
        {
            using (var client = new HttpClient())
            {
                string requestUrl = server + "/api/Account/login";
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
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
                    _baseUrl = server;

                    string result = response.Content.ReadAsStringAsync().Result;

                    var responseObj = JsonConvert.DeserializeObject<LoginResponseDto>(result);

                    response.Dispose();

                    return responseObj;
                }

                response.Dispose();
                return null;
            }
        }

        public async Task<T> Request(string method, string path, object payload = null)
        {
            using (var client = new HttpClient())
            {
                string requestUrl = _baseUrl + "/api" + path;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

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
                    Console.WriteLine(requestUrl);
                    Console.WriteLine(path);
                    Console.WriteLine(response.Headers);
                }
                else
                {
                    Console.WriteLine("HTTP " + response.StatusCode);

                    throw new Exception();
                }
            }

            throw new Exception();
        }
    }
}