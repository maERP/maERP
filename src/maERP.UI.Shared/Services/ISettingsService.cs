using System.Threading.Tasks;

namespace maERP.UI.Services;

public interface ISettingsService
{
    Task SaveLoginCredentialsAsync(string email, string password, string serverUrl);
    Task<(string Email, string Password, string ServerUrl)?> LoadLoginCredentialsAsync();
    Task ClearLoginCredentialsAsync();
    Task<bool> GetRememberMeAsync();
    Task SetRememberMeAsync(bool rememberMe);
}