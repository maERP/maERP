using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Dtos.Tenant;

namespace maERP.UI.Services;

public interface IAuthenticationService
{
    Task<LoginResponseDto> LoginAsync(string email, string password, string serverUrl);
    Task LogoutAsync();
    bool IsAuthenticated { get; }
    string? Token { get; }
    string? ServerUrl { get; }
    List<TenantListDto>? AvailableTenants { get; }
    Guid? CurrentTenantId { get; }
}