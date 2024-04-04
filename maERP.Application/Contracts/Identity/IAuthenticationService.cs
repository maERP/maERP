namespace maERP.Shared.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password, bool rememberMe);
    Task<bool> RegisterAsync(string firstName, string lastName, string email, string password);
    Task LogoutAsync();
}