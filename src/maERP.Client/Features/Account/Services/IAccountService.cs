using maERP.Domain.Dtos.Account;

namespace maERP.Client.Features.Account.Services;

/// <summary>
/// Service for the current user's own account: profile retrieval, profile update, password change.
/// </summary>
public interface IAccountService
{
    Task<CurrentUserProfileDto?> GetCurrentUserAsync(CancellationToken ct = default);

    Task UpdateCurrentUserAsync(
        string email,
        string firstname,
        string lastname,
        string phoneNumber,
        CancellationToken ct = default);

    Task ChangePasswordAsync(
        string currentPassword,
        string newPassword,
        string newPasswordConfirm,
        CancellationToken ct = default);
}
