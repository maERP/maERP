namespace maERP.Application.Contracts.Logging;

// ReSharper disable once UnusedTypeParameter
public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
}
