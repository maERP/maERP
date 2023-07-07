namespace maERP.Web.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticateAsync(string email, string password);
    Task<bool> RegisterAsync(string firstName, string lastName, string email, string password);
    Task LogoutAsync();
}