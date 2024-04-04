using maERP.Application.Models.Identity;

namespace maERP.Shared.Contracts;

public interface IApiDataProvider<T> where T : class
{
    public Task<AuthResponse> Login(AuthRequest authRequest);
    public Task<RegistrationResponse> RegisterAsync(RegistrationRequest registrationRequest);
    public Task<T> Request(string method, string path, object payload = null);
}
