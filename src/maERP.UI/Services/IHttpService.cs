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
    Task LogoutAsync();

    Task<PaginatedResult<T>?> GetPaginatedAsync<T>(string endpoint, int pageNumber = 0, int pageSize = 50, string searchString = "", string orderBy = "");
    Task<Result<T>?> GetAsync<T>(string endpoint);
    Task<Result<TResponse>?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<Result<TResponse>?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
    Task<Result?> DeleteAsync(string endpoint);
}