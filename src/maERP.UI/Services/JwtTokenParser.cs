using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text.Json;
using maERP.Domain.Dtos.Tenant;

namespace maERP.UI.Services;

public static class JwtTokenParser
{
    public static List<TenantListDto> ExtractAvailableTenants(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var tenantsClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "availableTenants")?.Value;

            if (!string.IsNullOrEmpty(tenantsClaim))
            {
                Console.WriteLine($"Found tenantsClaim: {tenantsClaim}");
                var tenantsFromToken = JsonSerializer.Deserialize<List<TenantInfo>>(tenantsClaim);
                return tenantsFromToken?.Select(t => new TenantListDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    TenantCode = t.TenantCode
                }).ToList() ?? new List<TenantListDto>();
            }
            else
            {
                Console.WriteLine("No availableTenants claim found in JWT");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JWT parsing error: {ex.Message}");
        }

        return new List<TenantListDto>();
    }

    public static Guid? ExtractCurrentTenantId(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var tenantIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;

            if (!string.IsNullOrEmpty(tenantIdClaim) && Guid.TryParse(tenantIdClaim, out Guid tenantId))
            {
                return tenantId == Guid.Empty ? null : tenantId;
            }
        }
        catch (Exception)
        {
            // Ignore parsing errors
        }

        return null;
    }

    private class TenantInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TenantCode { get; set; } = string.Empty;
    }
}