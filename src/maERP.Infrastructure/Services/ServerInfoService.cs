using System.Reflection;
using maERP.Application.Contracts.Services;

namespace maERP.Infrastructure.Services;

public class ServerInfoService : IServerInfoService
{
    private const string RegistrationEnabledVar = "SERVER_REGISTRATION_ENABLED";

    public ServerInfoService()
    {
        var raw = Environment.GetEnvironmentVariable(RegistrationEnabledVar);
        IsRegistrationEnabled = bool.TryParse(raw, out var parsed) && parsed;
        Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0";
    }

    public bool IsRegistrationEnabled { get; }

    public string Version { get; }
}
