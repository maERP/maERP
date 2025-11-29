using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace maERP.Client.Core.Abstractions;

/// <summary>
/// Base class for view models that require asynchronous initialization.
/// Provides INotifyPropertyChanged implementation and safe async initialization handling.
/// </summary>
/// <remarks>
/// Usage pattern:
/// 1. Inherit from this class
/// 2. Override InitializeCoreAsync to perform your async initialization logic
/// 3. Call StartInitialization() at the end of your constructor
/// </remarks>
public abstract class AsyncInitializableModel : IAsyncInitializable, INotifyPropertyChanged
{
    private readonly ILogger? _logger;
    private bool _isInitialized;
    private bool _isInitializing;
    private string? _initializationError;

    /// <summary>
    /// Creates a new instance of AsyncInitializableModel.
    /// </summary>
    /// <param name="logger">Optional logger for error reporting.</param>
    protected AsyncInitializableModel(ILogger? logger = null)
    {
        _logger = logger;
    }

    /// <inheritdoc />
    public bool IsInitialized
    {
        get => _isInitialized;
        private set => SetProperty(ref _isInitialized, value);
    }

    /// <inheritdoc />
    public bool IsInitializing
    {
        get => _isInitializing;
        private set => SetProperty(ref _isInitializing, value);
    }

    /// <inheritdoc />
    public string? InitializationError
    {
        get => _initializationError;
        private set => SetProperty(ref _initializationError, value);
    }

    /// <summary>
    /// Gets a value indicating whether the model is ready (initialized without errors).
    /// </summary>
    public bool IsReady => IsInitialized && InitializationError == null;

    /// <summary>
    /// Gets a value indicating whether the model is not currently initializing.
    /// Useful for binding to IsEnabled properties.
    /// </summary>
    public bool IsNotInitializing => !IsInitializing;

    /// <inheritdoc />
    public async Task InitializeAsync(CancellationToken ct = default)
    {
        if (IsInitialized || IsInitializing)
        {
            return;
        }

        IsInitializing = true;
        InitializationError = null;

        try
        {
            await InitializeCoreAsync(ct);
            IsInitialized = true;
        }
        catch (OperationCanceledException)
        {
            // Cancellation is not an error
            _logger?.LogDebug("Initialization was cancelled for {ModelType}", GetType().Name);
        }
        catch (Exception ex)
        {
            InitializationError = ex.Message;
            _logger?.LogError(ex, "Initialization failed for {ModelType}", GetType().Name);
        }
        finally
        {
            IsInitializing = false;
        }
    }

    /// <summary>
    /// Starts the initialization process without awaiting.
    /// Call this at the end of your constructor to trigger async loading.
    /// Errors are caught and stored in InitializationError.
    /// </summary>
    protected void StartInitialization()
    {
        // Fire-and-forget with proper error handling via InitializeAsync
        _ = InitializeAsync();
    }

    /// <summary>
    /// Override this method to implement your async initialization logic.
    /// This method is called by InitializeAsync with proper error handling.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A task representing the initialization operation.</returns>
    protected abstract Task InitializeCoreAsync(CancellationToken ct);

    #region INotifyPropertyChanged

    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">Name of the property that changed.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Sets a property value and raises PropertyChanged if the value changed.
    /// </summary>
    /// <typeparam name="T">Type of the property.</typeparam>
    /// <param name="field">Reference to the backing field.</param>
    /// <param name="value">New value.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>True if the value changed, false otherwise.</returns>
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
        {
            return false;
        }

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}
