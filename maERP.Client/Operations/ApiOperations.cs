using System.Net.Http.Headers;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace maERP.Client.Operations
{
    public class ApiOperations<T> where T : class
    {
        private readonly string _baseUrl;

        public ApiOperations()
        {
            _baseUrl = "https://localhost:7028";
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
                else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
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

            Console.WriteLine("debug 2-9");

            throw new Exception();
        }
    }
}