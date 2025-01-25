namespace maERP.SharedUI.Models.User;

public class UserVm
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordConfirm { get; set; } = string.Empty;
}