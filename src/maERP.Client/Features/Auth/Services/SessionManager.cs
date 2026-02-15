namespace maERP.Client.Features.Auth.Services;

/// <summary>
/// Singleton service that manages session lifecycle:
/// - Periodic token expiry check with proactive refresh
/// - Inactivity timeout (auto-logout after 30 min without user interaction)
/// - App suspend/resume handling
/// </summary>
public class SessionManager : ISessionManager, IDisposable
{
    private readonly IMaErpAuthenticationService _authService;
    private readonly ITokenStorageService _tokenStorage;
    private readonly IServiceProvider _serviceProvider;
    private readonly SessionSettings _settings;
    private readonly ILogger<SessionManager> _logger;

    private PeriodicTimer? _timer;
    private CancellationTokenSource? _timerCts;
    private DateTimeOffset _lastUserActivity;
    private DateTimeOffset? _suspendedAt;
    private bool _isActive;

    public bool IsActive => _isActive;

    public SessionManager(
        IMaErpAuthenticationService authService,
        ITokenStorageService tokenStorage,
        IServiceProvider serviceProvider,
        SessionSettings settings,
        ILogger<SessionManager> logger)
    {
        _authService = authService;
        _tokenStorage = tokenStorage;
        _serviceProvider = serviceProvider;
        _settings = settings;
        _logger = logger;
        _lastUserActivity = DateTimeOffset.UtcNow;
    }

    public Task StartAsync()
    {
        if (_isActive)
        {
            _logger.LogDebug("SessionManager already active, ignoring start");
            return Task.CompletedTask;
        }

        _logger.LogInformation("SessionManager starting (check interval: {Interval}s, inactivity timeout: {Timeout}min)",
            _settings.TokenRefreshCheckIntervalSeconds, _settings.InactivityTimeoutMinutes);

        _lastUserActivity = DateTimeOffset.UtcNow;
        _suspendedAt = null;
        _isActive = true;

        _timerCts = new CancellationTokenSource();
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(_settings.TokenRefreshCheckIntervalSeconds));

        // Fire-and-forget the timer loop
        _ = RunTimerLoopAsync(_timerCts.Token);

        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        if (!_isActive)
        {
            return Task.CompletedTask;
        }

        _logger.LogInformation("SessionManager stopping");
        _isActive = false;

        _timerCts?.Cancel();
        _timerCts?.Dispose();
        _timerCts = null;

        _timer?.Dispose();
        _timer = null;

        return Task.CompletedTask;
    }

    public void RecordUserActivity()
    {
        _lastUserActivity = DateTimeOffset.UtcNow;
    }

    public void OnAppSuspended()
    {
        _suspendedAt = DateTimeOffset.UtcNow;
        _logger.LogDebug("App suspended at {Time}", _suspendedAt);
    }

    public async Task OnAppResumedAsync()
    {
        if (!_isActive)
        {
            return;
        }

        var suspendedAt = _suspendedAt;
        _suspendedAt = null;

        if (suspendedAt == null)
        {
            return;
        }

        var elapsed = DateTimeOffset.UtcNow - suspendedAt.Value;
        _logger.LogInformation("App resumed after {Elapsed} (suspended at {SuspendedAt})",
            elapsed, suspendedAt);

        // Check if inactivity timeout exceeded during suspension
        if (elapsed.TotalMinutes >= _settings.InactivityTimeoutMinutes)
        {
            _logger.LogWarning("Inactivity timeout exceeded during app suspension ({Elapsed} >= {Timeout}min) - invalidating session",
                elapsed, _settings.InactivityTimeoutMinutes);
            await InvalidateSessionAsync();
            return;
        }

        // Proactively refresh the token on resume since it may have expired while suspended
        await TryRefreshTokenAsync();

        // Reset user activity since the user just came back
        RecordUserActivity();
    }

    private async Task RunTimerLoopAsync(CancellationToken cancellationToken)
    {
        try
        {
            while (await _timer!.WaitForNextTickAsync(cancellationToken))
            {
                await CheckTokenExpiryAsync();
                await CheckInactivityAsync();
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogDebug("SessionManager timer loop cancelled");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SessionManager timer loop encountered an error");
        }
    }

    private async Task CheckTokenExpiryAsync()
    {
        try
        {
            var expiry = await _tokenStorage.GetTokenExpiryAsync();
            if (expiry == null)
            {
                return;
            }

            var timeUntilExpiry = expiry.Value - DateTimeOffset.UtcNow;

            if (timeUntilExpiry.TotalMinutes <= _settings.TokenExpiryBufferMinutes)
            {
                _logger.LogInformation("Token expires in {Minutes:F1} minutes (buffer: {Buffer}min) - proactively refreshing",
                    timeUntilExpiry.TotalMinutes, _settings.TokenExpiryBufferMinutes);
                await TryRefreshTokenAsync();
            }
            else
            {
                _logger.LogDebug("Token expires in {Minutes:F1} minutes - no refresh needed",
                    timeUntilExpiry.TotalMinutes);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking token expiry");
        }
    }

    private async Task CheckInactivityAsync()
    {
        try
        {
            var inactiveDuration = DateTimeOffset.UtcNow - _lastUserActivity;

            if (inactiveDuration.TotalMinutes >= _settings.InactivityTimeoutMinutes)
            {
                _logger.LogWarning("Inactivity timeout reached ({Elapsed:F1}min >= {Timeout}min) - invalidating session",
                    inactiveDuration.TotalMinutes, _settings.InactivityTimeoutMinutes);
                await InvalidateSessionAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking inactivity");
        }
    }

    private async Task TryRefreshTokenAsync()
    {
        try
        {
            var result = await _authService.RefreshTokenAsync();

            if (result?.Succeeded == true && !string.IsNullOrEmpty(result.Token))
            {
                _logger.LogInformation("Proactive token refresh succeeded");
            }
            else
            {
                _logger.LogWarning("Proactive token refresh failed - session may expire soon");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Proactive token refresh threw an exception");
        }
    }

    private async Task InvalidateSessionAsync()
    {
        _logger.LogInformation("Invalidating session - triggering logout");

        // Stop the timer first to prevent re-entry
        await StopAsync();

        try
        {
            // Use the Uno IAuthenticationService to trigger the full logout flow
            // which will raise the LoggedOut event and update ShellModel
            var authService = _serviceProvider.GetService<IAuthenticationService>();
            if (authService != null)
            {
                await authService.LogoutAsync(CancellationToken.None);
            }
            else
            {
                _logger.LogWarning("IAuthenticationService not available for session invalidation");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during session invalidation logout");
        }
    }

    public void Dispose()
    {
        _timerCts?.Cancel();
        _timerCts?.Dispose();
        _timer?.Dispose();
    }
}
