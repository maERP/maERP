#nullable disable

using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using maERP.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace maERP.SalesChannels.Models.eBay;

public class EbayAuthHelper
{
    private readonly ILogger<EbayAuthHelper> _logger;

    public EbayAuthHelper(ILogger<EbayAuthHelper> logger)
    {
        _logger = logger;
    }

    // OAuth-Token erhalten oder aktualisieren
    public async Task<string> GetAccessTokenAsync(SalesChannel salesChannel)
    {
        // Im Produktionssystem würde man ein Token-Caching-System implementieren
        // Und die Berechtigung-Scopes entsprechend konfigurieren
        try
        {
            var client = new HttpClient();
            string tokenUrl = "https://api.ebay.com/identity/v1/oauth2/token";
            
            var request = new HttpRequestMessage(HttpMethod.Post, tokenUrl);
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Basic", 
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{salesChannel.Username}:{salesChannel.Password}"))
            );
            
            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "scope", "https://api.ebay.com/oauth/api_scope" }
            });
            
            request.Content = content;
            
            var response = await client.SendAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonSerializer.Deserialize<EbayTokenResponse>(result);
                return tokenResponse.AccessToken;
            }
            else
            {
                _logger.LogError($"Failed to get eBay token: {response.StatusCode}");
                throw new Exception($"Failed to get eBay token: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error getting eBay token: {ex.Message}");
            throw;
        }
    }
}

public class EbayTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
} 