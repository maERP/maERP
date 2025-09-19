using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace maERP.Application.Extensions;

public static class UserContextExtensions
{
    private const string TestUserIdHeader = "X-Test-UserId";

    public static string? GetUserId(this HttpContext? context)
    {
        if (context == null)
        {
            return null;
        }

        var user = context.User;
        var userId = user?.FindFirst("uid")?.Value
                     ?? user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!string.IsNullOrWhiteSpace(userId))
        {
            return userId;
        }

        if (context.Request?.Headers.TryGetValue(TestUserIdHeader, out var headerValues) == true)
        {
            var headerUserId = headerValues.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(headerUserId))
            {
                return headerUserId;
            }
        }

        return null;
    }
}
