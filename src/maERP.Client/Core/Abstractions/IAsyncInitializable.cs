namespace maERP.Client.Core.Abstractions;

/// <summary>
/// Interface for view models that require asynchronous initialization.
/// Implement this interface when a model needs to load data asynchronously
/// after construction (e.g., loading from API, database, or other async sources).
/// </summary>
public interface IAsyncInitializable
{
    /// <summary>
    /// Gets a value indicating whether the initialization has completed.
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    /// Gets a value indicating whether initialization is currently in progress.
    /// </summary>
    bool IsInitializing { get; }

    /// <summary>
    /// Gets the error message if initialization failed, or null if successful.
    /// </summary>
    string? InitializationError { get; }

    /// <summary>
    /// Initializes the model asynchronously.
    /// This method should be called after construction to load initial data.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A task representing the initialization operation.</returns>
    Task InitializeAsync(CancellationToken ct = default);
}
