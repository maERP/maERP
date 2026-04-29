namespace maERP.Application.Contracts.Services;

public interface IServerInfoService
{
    bool IsRegistrationEnabled { get; }
    string Version { get; }
}
