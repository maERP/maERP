using System;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Wrapper;

namespace maERP.UI.Services;

public interface IHttpService
{
    string? ServerUrl { get; }
    string? Token { get; }
    bool IsAuthenticated { get; }

    Task<LoginResponseDto> LoginAsync(string email, string password, string serverUrl);
    Task<RegistrationResponseDto> RegisterAsync(string firstName, string lastName, string email, string password, string serverUrl);
    Task LogoutAsync();
    void SetCurrentTenant(Guid? tenantId);

    Task<PaginatedResult<T>?> GetPaginatedAsync<T>(string endpoint, int pageNumber = 0, int pageSize = 50, string searchString = "", string orderBy = "");
    Task<Result<T>?> GetAsync<T>(string endpoint);
    Task<Result<TResponse>?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<Result<TResponse>?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<Result?> DeleteAsync(string endpoint);
    Task<FileDownloadResult> DownloadFileAsync(string endpoint, string suggestedFileName);
}

public class FileDownloadResult
{
    public bool Success { get; set; }
    public string? FilePath { get; set; }
    public string? ErrorMessage { get; set; }
}

public class RegistrationResponseDto
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public string? UserId { get; set; }
}