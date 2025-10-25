using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using maERP.Domain.Dtos.Auth;
using maERP.Domain.Dtos.Tenant;

namespace maERP.UI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IHttpService _httpService;
    private readonly ITenantService _tenantService;

    public AuthenticationService(IHttpService httpService, ITenantService tenantService)
    {
        _httpService = httpService;
        _tenantService = tenantService;
    }

    public bool IsAuthenticated => _httpService.IsAuthenticated;
    public string? Token => _httpService.Token;
    public string? ServerUrl => _httpService.ServerUrl;
    public List<TenantListDto>? AvailableTenants => _tenantService.AvailableTenants.ToList();
    public Guid? CurrentTenantId => _tenantService.CurrentTenant?.Id;

    public async Task<LoginResponseDto> LoginAsync(string email, string password, string serverUrl)
    {
        var result = await _httpService.LoginAsync(email, password, serverUrl);

        // Nach erfolgreichem Login die verfügbaren Tenants setzen
        if (result.Succeeded && result.AvailableTenants != null)
        {
            _tenantService.SetAvailableTenants(result.AvailableTenants);

            // If no current tenant is set, automatically select the first available tenant
            var tenantIdToSet = result.CurrentTenantId ?? result.AvailableTenants.FirstOrDefault()?.Id;
            _tenantService.SetCurrentTenant(tenantIdToSet);
        }

        return result;
    }

    public async Task<RegistrationResult> RegisterAsync(string firstName, string lastName, string email, string password, string serverUrl)
    {
        var result = await _httpService.RegisterAsync(firstName, lastName, email, password, serverUrl);

        return new RegistrationResult
        {
            Succeeded = result.Succeeded,
            Message = result.Message,
            UserId = result.UserId
        };
    }

    public async Task LogoutAsync()
    {
        await _httpService.LogoutAsync();

        // Tenant-Informationen beim Logout löschen
        _tenantService.SetAvailableTenants(new List<TenantListDto>());
        _tenantService.SetCurrentTenant(null);
    }
}