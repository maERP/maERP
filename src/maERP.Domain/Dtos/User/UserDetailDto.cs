namespace maERP.Domain.Dtos.User;

public class UserDetailDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    public string PasswordConfirm { get; set; } = string.Empty;

    // Collection of tenant assignments
    public ICollection<UserTenantAssignmentDto> TenantAssignments { get; set; } = new List<UserTenantAssignmentDto>();
}