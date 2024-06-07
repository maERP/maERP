using System.Globalization;

namespace maERP.Application.Exceptions;

/// <summary>
/// Custom exception for IEnumerable source not found errors
/// </summary>
public class SourceNullException : Exception
{
    public SourceNullException() : base()
    {
    }

    public SourceNullException(string message) : base(message)
    {
    }

    public SourceNullException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}