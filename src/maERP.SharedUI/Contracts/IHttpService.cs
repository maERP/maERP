namespace maERP.SharedUI.Contracts;

/// <summary>
/// Interface for making HTTP requests with authentication support
/// </summary>
public interface IHttpService
{
    /// <summary>
    /// Authenticates the user with the API using credentials
    /// </summary>
    /// <param name="email">User's email address</param>
    /// <param name="password">User's password</param>
    /// <param name="rememberMe">Remember me flag</param>
    /// <returns>True if authentication was successful, false otherwise</returns>
    Task<bool> LoginAsync(string email, string password, bool rememberMe);

    /// <summary>
    /// Logs out the current user by clearing the authentication token
    /// </summary>
    void Logout();

    /// <summary>
    /// Sends a GET request to the specified URI and returns the deserialized response
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response to</typeparam>
    /// <param name="uri">The URI to send the request to</param>
    /// <param name="requiresAuth">Whether the request requires authentication</param>
    /// <returns>The deserialized response</returns>
    Task<T?> GetAsync<T>(string uri, bool requiresAuth = true);

    /// <summary>
    /// Sends a POST request to the specified URI with the given content and returns the deserialized response
    /// </summary>
    /// <typeparam name="TRequest">Type of the request content</typeparam>
    /// <typeparam name="TResponse">Type to deserialize the response to</typeparam>
    /// <param name="uri">The URI to send the request to</param>
    /// <param name="content">The content to send with the request</param>
    /// <param name="requiresAuth">Whether the request requires authentication</param>
    /// <returns>The deserialized response</returns>
    Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content, bool requiresAuth = true);

    /// <summary>
    /// Sends a PUT request to the specified URI with the given content and returns the deserialized response
    /// </summary>
    /// <typeparam name="TRequest">Type of the request content</typeparam>
    /// <typeparam name="TResponse">Type to deserialize the response to</typeparam>
    /// <param name="uri">The URI to send the request to</param>
    /// <param name="content">The content to send with the request</param>
    /// <param name="requiresAuth">Whether the request requires authentication</param>
    /// <returns>The deserialized response</returns>
    Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest content, bool requiresAuth = true);


    /// <summary>
    /// Sends a DELETE request to the specified URI
    /// </summary>
    /// <param name="uri">The URI to send the request to</param>
    /// <param name="requiresAuth">Whether the request requires authentication</param>
    Task DeleteAsync(string uri, bool requiresAuth = true);
}
