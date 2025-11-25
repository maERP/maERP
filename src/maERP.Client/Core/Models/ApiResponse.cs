namespace maERP.Client.Core.Models;

/// <summary>
/// Wrapper for API responses that contain data in a nested "Data" property.
/// This matches the API envelope pattern used by maERP.Server.
/// </summary>
public class ApiResponse<T>
{
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public List<string> Messages { get; set; } = new();
    public bool Succeeded { get; set; }
}
