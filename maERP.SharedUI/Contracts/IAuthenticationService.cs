namespace maERP.SharedUI.Contracts;

public interface IAuthenticationService
{
    Task<bool> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string firstName, string lastName, string username, string email, string password);
    Task LogoutAsync();
}
