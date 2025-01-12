namespace maERP.SharedUI.Services.Base;

public class Response<T>
{
    public string Message { get; set; } = string.Empty;
    public string ValidationErrors { get; set; } = string.Empty;
    public bool Success { get; set; }
    public T? Data { get; set; }
}