namespace maERP.Application.Models;

public class ApiResult<T> where T : class
{
    public bool success { get; set; }
    public T? data { get; set; }
    public int total { get; set; }
    public string? errorMessage { get; set; }
}