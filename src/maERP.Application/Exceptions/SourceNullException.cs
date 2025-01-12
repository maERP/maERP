namespace maERP.Application.Exceptions;

/// <summary>
/// Custom exception for IEnumerable source not found errors
/// </summary>
public class SourceNullException(string message) : Exception(message);